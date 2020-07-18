using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebApi
{
    public class PokemonSuper
    {
        public long Id { get; set; }

        public PokemonName Name { get; set; }
        
        public List<string> Type { get; set; }

        public PokemonStat Base { get; set; }
        public string Deparment { get; set; }
    }
}