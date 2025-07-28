using PokemonReviewWebApi.Dto;
using PokemonReviewWebApi.Interfaces;
using PokemonReviewWebApi.Models;

namespace PokemonReviewWebApi.Helper
{
    public class PokemonReadDtoToPokemonMapper : IMapper<PokemonReadDto, Pokemon>
    {
        public Pokemon Map(PokemonReadDto source)
        {
            return new Pokemon
            {
                Id = source.Id,
                Name = source.Name,
                BirthDate = source.BirthDate
            };
        }
    }
}
