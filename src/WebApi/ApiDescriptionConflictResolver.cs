using Esquio.Abstractions;
using Esquio.AspNetCore.Endpoints.Metadata;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WebApi
{
    internal static class ApiDescriptionConflictResolver
    {
        internal static IServiceProvider serviceProvider;


        public static void ResolveConflictingActionsByFeatureToggles(this SwaggerGenOptions swaggerGenOptions)
        {
            swaggerGenOptions.ResolveConflictingActions(ResolveConflict);
        }


        public static void UseResolveConflictingActionsByFeatureToggles(this IApplicationBuilder applicationServices)
        {
            serviceProvider = applicationServices.ApplicationServices;
        }

        private static ApiDescription ResolveConflict(IEnumerable<ApiDescription> descriptions)
        {
            foreach (var apiDescription in descriptions)
            {
                switch(apiDescription.ActionDescriptor)
                {
                    case ControllerActionDescriptor controllerActionDescriptor:
                        var filter = controllerActionDescriptor.MethodInfo.GetCustomAttribute<FeatureFilter>();
                        if (filter != null)
                        {
                            bool featureEnabled = FeatureEnabled(filter.Name);
                            if (featureEnabled)
                            {
                                return apiDescription;
                            }
                        }
                        break;
                    case null:
                        throw new ArgumentNullException(nameof(apiDescription.ActionDescriptor));
                }
            }
            return descriptions.First();
        }

        private static IServiceScope GetScope()
        {
            if (serviceProvider == null)
                throw new Exception("Conflict resolver not configured, please use UseResolveConflictingActionsByFeatureToggles on your startup.cs (Configure method)");
            return serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        }

        private static bool FeatureEnabled(string featureName)
        {
            bool featureEnabled;
            using (var serviceScope = GetScope())
            {
                var featureService = serviceScope.ServiceProvider.GetService<IFeatureService>();
                featureEnabled = featureService.IsEnabledAsync(featureName).GetAwaiter().GetResult();
            }
            return featureEnabled;
        }

    }
}
