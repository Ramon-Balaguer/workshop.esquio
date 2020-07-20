using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Esquio.AspNetCore.Endpoints.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApi.Infrastructure;
using WebApi.Model;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokedexController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly PokemonReader pokemonReader;

        public PokedexController(IConfiguration configuration, PokemonReader pokemonReader)
        {
            this.configuration = configuration;
            this.pokemonReader = pokemonReader;
        }

        [HttpGet]
        public IEnumerable<Pokemon> Get([FromQuery]string name)
        {
            var pokemons = pokemonReader.Read(configuration.GetValue<string>("PokedexPath"));
            var pokemonsByName = FilterPokemonByName(pokemons, name);
            return pokemonsByName;
        }

        private static IEnumerable<Pokemon> FilterPokemonByName(List<Pokemon> pokemons, string name)
        {
            if (string.IsNullOrEmpty(name))
                return pokemons;
            return pokemons.Where(pokemon => pokemon.Name.English.ToLower().StartsWith(name.ToLower()));
        }
    }
}
