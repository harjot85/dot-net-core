using Microsoft.AspNetCore.Mvc;
using dotnetcore.Models;
using dotnetcore.services.CharacterService;
using System.Threading.Tasks;
using dotnetcore.DTOs.Character;
using Microsoft.AspNetCore.Authorization;

namespace dotnetcore.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCharacterById(int id)
        {
            var result = await _characterService.GetCharacterById(id);
            return Ok(result);
        }

        [AllowAnonymous]
        [Route("")]
        public async Task<IActionResult> GetCharacters()
        {
            var result = await _characterService.GetCharacters();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostCharacter(AddCharacterDto character)
        {
            if (string.IsNullOrEmpty(character.Name)) return BadRequest("Id cannot be zero or negative number. Name is required.");

            var result = await _characterService.PostCharacter(character);
            return Ok($"Character added. Updated List: {result}");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCharacter(UpdateCharacterDto character)
        {
            if (string.IsNullOrEmpty(character.Name)) return BadRequest("Id cannot be zero or negative number. Name is required.");

            ServiceResponse<GetCharacterDto> response = await _characterService.UpdateCharacter(character);
            if (response.Data == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            ServiceResponse<GetCharacterDto> response = await _characterService.DeleteCharacter(id);
            if (response.Data == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}