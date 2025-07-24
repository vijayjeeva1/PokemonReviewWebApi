using PokemonReviewWebApi.Dto;
using PokemonReviewWebApi.Interfaces;
using PokemonReviewWebApi.Models;

namespace PokemonReviewWebApi.Helper
{
    public class PokemonToDtoMapper : IMapper<Pokemon, PokemonDto>
    {
        public PokemonDto Map(Pokemon source)
        {
            return new PokemonDto
            {
                Id = source.Id,
                Name = source.Name,
                BirthDate = source.BirthDate
            };
        }
    }
}
