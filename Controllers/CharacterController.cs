using Microsoft.AspNetCore.Mvc;
using dotnetcore.Models;
using dotnetcore.services.CharacterService;
using System.Threading.Tasks;

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
        public async Task<IActionResult> PostCharacter(Character character)
        {
            if (string.IsNullOrEmpty(character.Name) || character.Id < 1) return BadRequest("Id cannot be zero or negative number. Name is required.");

            var result = await _characterService.PostCharacter(character);
            return Ok($"Character added. Updated List: {result}");
        }
    }
}