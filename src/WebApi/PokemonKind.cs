using System;

namespace WebApi
{
    public class PokemonKind : IEquatable<PokemonKind>
    {
        public static readonly PokemonKind Fire = new PokemonKind(nameof(Fire));

        public PokemonKind(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override bool Equals(object obj)
        {
            return Equals(obj as PokemonKind);
        }

        public bool Equals(PokemonKind other)
        {
            return other != null &&
                   Name == other.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}