using PokemonReviewWebApi.Dto;
using PokemonReviewWebApi.Interfaces;
using PokemonReviewWebApi.Models;

namespace PokemonReviewWebApi.Helper
{
    public class PokemonWriteDtoToPokemonMapper : IMapper<PokemonWriteDto, Pokemon>
    {
        public Pokemon Map(PokemonWriteDto source)
        {
            return new Pokemon
            {
                Name = source.Name,
                BirthDate = source.BirthDate
            };
        }
    }
}
