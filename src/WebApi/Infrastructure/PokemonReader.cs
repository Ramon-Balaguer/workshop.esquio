using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApi.Infrastructure
{
    public class PokemonReader
    {
        public List<Pokemon> Read(string fileName)
        {
            var jsonString = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<List<Pokemon>>(jsonString);
        }
    }
}
