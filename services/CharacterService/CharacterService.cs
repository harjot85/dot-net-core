using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetcore.Models;

namespace dotnetcore.services.CharacterService
{

    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>()
        {
            new Character(),
            new Character() { Name = "Hulk", Id = 100, Class = RpgClass.Athletics },
            new Character() { Name = "Strange", Id = 105, Class = RpgClass.Magic },
            new Character() { Name = "Wolverine", Id = 110, Class = RpgClass.Mutation }
        };
        public async Task<ServiceResponse<Character>> GetCharacterById(int id)
        {
            ServiceResponse<Character> serviceResponse = new ServiceResponse<Character>();
            serviceResponse.Data = characters.FirstOrDefault(c => c.Id == id);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Character>>> GetCharacters()
        {
            ServiceResponse<List<Character>> serviceResponse = new ServiceResponse<List<Character>>();
            serviceResponse.Data = characters;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Character>>> PostCharacter(Character character)
        {
            ServiceResponse<List<Character>> serviceResponse = new ServiceResponse<List<Character>>();
            characters.Add(character);
            serviceResponse.Data = characters;
            return serviceResponse;
        }
    }
}