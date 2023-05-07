using System;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Models;
namespace PokemonReviewApp.Interfaces
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();
        Pokemon GetPokemon(long pokemonId);
        Pokemon GetPokemon(string name);
        Pokemon GetPokemonTrimToUpper(PokemonDto pokemonCreate);
        decimal GetPokemonRating(long pokemonId);
        bool PokemonExists(long pokemonId);
        bool CreatePokemon(long ownerId, long categoryId, Pokemon pokemon);
        bool UpdatePokemon(long ownerId, long categoryId, Pokemon pokemon);
        bool DeletePokemon(Pokemon pokemon);
        bool Save();
    }
}

