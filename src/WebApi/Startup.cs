using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.Infrastructure;
using Swashbuckle.AspNetCore.EsquioResolver;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<PokemonReader>();
            services.AddEsquio()
                .AddAspNetCoreDefaultServices()
                //.AddConfigurationStore(Configuration)
                .AddHttpStore(options =>
                {
                    options
                        .UseBaseAddress("http://localhost:8090/")
                        .UseApiKey("ZgZ9/qcwJGe/Utefuym5YS/84mE8/9x7kIrx2V/aIxc=");
                })
                ;

            services.AddSwaggerGen(configuration =>
            {
                configuration.ResolveConflictingActionsByFeatureToggles();
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseResolveConflictingActionsByFeatureToggles();
            app.UseCors(options => options.AllowAnyOrigin());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapEsquio();
            });

            
        }
    }
}
