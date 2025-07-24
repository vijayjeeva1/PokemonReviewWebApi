using PokemonReviewWebApi.Dto;
using PokemonReviewWebApi.Models;

namespace PokemonReviewWebApi.Interfaces
{
    public interface IPokemonRepository
    {
        ICollection<PokemonDto> GetPokemons();

        PokemonDto GetPokemon(int id);
        PokemonDto GetPokemon(string name);
        decimal GetPokemonRating(int id);
        bool PokemonExists(int id);
    }
}
