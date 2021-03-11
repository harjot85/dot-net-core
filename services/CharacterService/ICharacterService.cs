using dotnetcore.Models;
using dotnetcore.DTOs.Character;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetcore.services.CharacterService
{
    public interface ICharacterService
    {
          Task<ServiceResponse<List<GetCharacterDto>>> GetCharacters();
          Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
          Task<ServiceResponse<List<GetCharacterDto>>> PostCharacter(AddCharacterDto character);
          Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto character);
          Task<ServiceResponse<GetCharacterDto>> DeleteCharacter(int id);
    }
}