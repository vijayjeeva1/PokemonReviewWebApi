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
            _mapperService.RegisterMapper(new PokemonDtoToPokemonMapper());
        }

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var pokemonOwnerEntity = _context.Owners.Where(a => a.Id == ownerId).FirstOrDefault();
            var category = _context.Categories.Where(a => a.Id == categoryId).FirstOrDefault();

            // Need to create pokemonOwner and pokemonCategory as these are part of the many to many relationships
            var pokemonOwner = new PokemonOwner()
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon,
            };

            _context.Add(pokemonOwner);

            var pokemonCategory = new PokemonCategory()
            {
                Category = category,
                Pokemon = pokemon,
            };

            _context.Add(pokemonCategory);

            // If there were no relationships, method would simply be this line and Save()
            _context.Add(pokemon);

            return Save();
        }

        public PokemonReadDto GetPokemon(int id)
        {
            var pokemon = _context.Pokemon.FirstOrDefault(p => p.Id == id);
            return _mapperService.Map<Pokemon, PokemonReadDto>(pokemon);
        }

        public PokemonReadDto GetPokemon(string name)
        {
            var pokemon = _context.Pokemon.FirstOrDefault(p => p.Name == name);
            if (pokemon == null) return null;
            return _mapperService.Map<Pokemon, PokemonReadDto>(pokemon);
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

        public ICollection<PokemonReadDto> GetPokemonsDto()
        {
            var pokemons = _context.Pokemon.OrderBy(p => p.Id).ToList();
            List<PokemonReadDto> pokemonDtos = new List<PokemonReadDto>();
            foreach (var pokemon in pokemons)
            {
                var pokemonDto = _mapperService.Map<Pokemon, PokemonReadDto>(pokemon);
                pokemonDtos.Add(pokemonDto);
            }

            return pokemonDtos;
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemon.OrderBy(p => p.Id).ToList();
        }

        public bool PokemonExists(int id)
        {
            return _context.Pokemon.Any(p => p.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
