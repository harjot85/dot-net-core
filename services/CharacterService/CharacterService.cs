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
        public async Task<Character> GetCharacterById(int id)
        {
            return characters.FirstOrDefault(c => c.Id == id);
        }

        public async Task<List<Character>> GetCharacters()
        {
            return characters;
        }

        public async Task<List<Character>> PostCharacter(Character character)
        {
             characters.Add(character);
             return characters;
        }
    }
}