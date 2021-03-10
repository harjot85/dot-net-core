using dotnetcore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetcore.services.CharacterService
{
    public interface ICharacterService
    {
          Task<ServiceResponse<List<Character>>> GetCharacters();
          Task<ServiceResponse<Character>> GetCharacterById(int id);
          Task<ServiceResponse<List<Character>>> PostCharacter(Character character);
    }
}