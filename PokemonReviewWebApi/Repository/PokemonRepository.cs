using Microsoft.EntityFrameworkCore;
using PokemonReviewWebApi.Data;
using PokemonReviewWebApi.Dto;
using PokemonReviewWebApi.Helper;
using PokemonReviewWebApi.Interfaces;
using PokemonReviewWebApi.Models;

namespace PokemonReviewWebApi.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;
        private readonly MapperService _mapperService = new MapperService();

        public PokemonRepository(DataContext context) 
        {
            _context = context;
            _mapperService.RegisterMapper(new PokemonToDtoMapper());
        }

        public PokemonDto GetPokemon(int id)
        {
            var pokemon = _context.Pokemon.FirstOrDefault(p => p.Id == id);
            return _mapperService.Map<Pokemon, PokemonDto>(pokemon);
        }

        public PokemonDto GetPokemon(string name)
        {
            var pokemon = _context.Pokemon.FirstOrDefault(p => p.Name == name);
            return _mapperService.Map<Pokemon, PokemonDto>(pokemon);
        }

        public decimal GetPokemonRating(int id)
        {
            var reviews = _context.Reviews.Where(r => r.PokemonId == id);

            if (reviews.Count() < 1)
            {
                return 0;
            }

            return ((decimal)reviews.Sum(r => r.Rating) / reviews.Count());
        }

        public ICollection<PokemonDto> GetPokemons()
        {
            var pokemons = _context.Pokemon.OrderBy(p => p.Id).ToList();
            List<PokemonDto> pokemonDtos = new List<PokemonDto>();
            foreach (var pokemon in pokemons)
            {
                var pokemonDto = _mapperService.Map<Pokemon, PokemonDto>(pokemon);
                pokemonDtos.Add(pokemonDto);
            }

            return pokemonDtos;
        }

        public bool PokemonExists(int id)
        {
            return _context.Pokemon.Any(p => p.Id == id);
        }
    }
}
