using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Esquio.AspNetCore.Endpoints.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApi.Infrastructure;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EsquioController : ControllerBase
    {
        private readonly ILogger<EsquioController> logger;
        private readonly IConfiguration configuration;

        public EsquioController(ILogger<EsquioController> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }

        [HttpGet]
        [ActionName("Get")]
        public List<FeatureToggle> Get([FromQuery(Name = "featureName")]  List<string> featureNames)
        {
            return new List<FeatureToggle>{ new FeatureToggle
            {
                enabled = true,
                name = "Casa"
            } };
        }


    }

    public class FeatureToggle
    {
        public Boolean enabled { get; set; }
        public string name { get; set; }
    }
}
