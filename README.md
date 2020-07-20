# workshop.esquio


dotnet new webapi -o src/WebApi -n WebApi
cd src/WebApi
dotnet add package Esquio.AspNetCore
dotnet add package Esquio.Configuration.Store
dotnet add package Esquio.Http.Store
dotnet add package Swashbuckle.AspNetCore


cd..
ng new spa

# Primera US
- desde carpeta SPA>
ng generate component about-us  
---------------
- modificar > app-routing.module.ts
import { AboutUsComponent } from './about-us/about-us.component';
  {
    path: "about-us",
    component: AboutUsComponent
  }
- Ahora ejecutar y puedes entrar sin la US acabada.
- Podemos ver que está publica la pagina
---------------
- modificar > app-routing.module.ts
  {
    path: "about-us",
    component: AboutUsComponent,
    canActivate: [FeatureFlagGuardService],
    data: { featureName: 'AboutUs' }
  }
- Configurar ESQUIO
- Ahora ejecutar y puedes no puedes entrar
- Jugar a activar y desactivar
- Poner texto
---------------
# Segunda US
- Agregar nuevo método
        [HttpGet]
        public IEnumerable<Pokemon> Get([FromQuery]string name)
        {
            var pokemons = pokemonReader.Read(configuration.GetValue<string>("PokedexPath"));
            var pokemonsByName = FilterPokemonByName(pokemons, name);
            return pokemonsByName;
        }
- Falla el compilador
- Agregamos el action name
        [HttpGet]
        [ActionName("Get")]
        public IEnumerable<Pokemon> GetRemoveWaterKindPokemon([FromQuery]string name)
        {
            var pokemons = pokemonReader.Read(configuration.GetValue<string>("PokedexPath"));
            var pokemonsByName = FilterPokemonByName(pokemons, name);
            return pokemonsByName;
        }
- Hay que agregar el ActionName al get actual
        [HttpGet]
        [ActionName("Get")]
        public IEnumerable<Pokemon> Get([FromQuery]string name)
        {
            var pokemons = pokemonReader.Read(configuration.GetValue<string>("PokedexPath"));
            var pokemonsByName = FilterPokemonByName(pokemons, name);
            return pokemonsByName;
        }
- Ahora todo aqui hay un asterisco por swagger
- probamos la api miramos el contrato todo OK
- seguimos desarrollando funcionalidad		
        [HttpGet]
        [FeatureFilter(Name = Flags.RemoveWaterType)]
        [ActionName("Get")]
        public IEnumerable<Pokemon> GetRemoveWaterKindPokemon([FromQuery] string name)
        {
            var pokemons = pokemonReader.Read(configuration.GetValue<string>("PokedexPath"));
            var pokemonsByName = FilterPokemonByName(pokemons, name);
            return FilterPokemonByType(pokemonsByName, "Water");
        }
		
		 private static IEnumerable<Pokemon> FilterPokemonByType(IEnumerable<Pokemon> pokemons, string type)
        {
            var pokemonsOrigin = new List<Pokemon>(pokemons);
            pokemonsOrigin.RemoveAll(pokemon => pokemon.Type.Select(type=>type.ToLower()).Contains(type.ToLower()));
            return pokemonsOrigin;
        }
---------------
# Tercera US
- editamos el boton pokemon-grid.component.html
        <button id="searchButton" (click)="submitFilter()" class="btn btn-primary" *removeIfFeatureOff="'Danger'">Submit</button>
        <button id="searchButton" (click)="submitFilter()" class="btn btn-danger" *removeIfFeatureOn="'Danger'">Submit</button>

- FIN


		[FeatureFilter(Name = Flags.Pokedex)]
        [HttpGet]
        [ActionName("Get")]
        public IEnumerable<PokemonSuper> GetNew([FromQuery] string name)
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