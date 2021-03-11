using Microsoft.AspNetCore.Mvc;
using dotnetcore.Models;
using dotnetcore.services.CharacterService;
using System.Threading.Tasks;
using dotnetcore.DTOs.Character;

namespace dotnetcore.Controllers
{

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
    }
}