using AutoMapper;
using PokemonReviewWebApi.Dto;
using PokemonReviewWebApi.Models;

namespace PokemonReviewWebApi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pokemon, PokemonDto>();
        }
    }
}
