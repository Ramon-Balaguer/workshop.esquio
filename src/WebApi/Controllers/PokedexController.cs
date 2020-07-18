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
    public class PokedexController : ControllerBase
    {
        private readonly ILogger<PokedexController> logger;
        private readonly IConfiguration configuration;
        private readonly PokemonReader pokemonReader;

        public PokedexController(ILogger<PokedexController> logger, IConfiguration configuration, PokemonReader pokemonReader)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.pokemonReader = pokemonReader;
        }

        [HttpGet]
        [ActionName("Get")]
        public IEnumerable<Pokemon> Get()
        {
            return pokemonReader.Read(configuration.GetValue<string>("PokedexPath"));
        }

        [FeatureFilter(Name = Flags.Pokedex)]
        [HttpGet]
        [ActionName("Get")]
        public IEnumerable<PokemonSuper> GetNew()
        {
            return new List<PokemonSuper>
            {
                new PokemonSuper
                {
                    Name = new PokemonName{
                        English = "Meri"
                    },
                    Id = 4444,
                    Deparment = "Payments"
                }
            };
        }
    }
}
