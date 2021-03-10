using dotnetcore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetcore.services.CharacterService
{
    public interface ICharacterService
    {
          Task<List<Character>> GetCharacters();
          Task<Character> GetCharacterById(int id);
          Task<List<Character>> PostCharacter(Character character);
    }
}