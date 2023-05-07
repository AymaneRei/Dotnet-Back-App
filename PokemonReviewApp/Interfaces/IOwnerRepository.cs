using System;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
	public interface IOwnerRepository
	{
        ICollection<Owner> GetOwners();
        Owner GetOwner(long ownerId);
        ICollection<Owner> GetOwnerOfAPokemon(long pokemonId);
        ICollection<Pokemon> GetPokemonByOwner(long ownerId);
        bool OwnerExists(long ownerId);
        bool CreateOwner(Owner owner);
        bool UpdateOwner(Owner owner);
        bool DeleteOwner(Owner owner);
        bool Save();
    }
}

