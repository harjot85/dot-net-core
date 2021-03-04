using Microsoft.AspNetCore.Mvc;
using dotnetcore.Models;
using System.Collections.Generic;
using System.Linq;

namespace dotnetcore.Controllers
{

    [ApiController]
    [Route("[Controller]")]
    public class CharacterController : ControllerBase
    {
        private static List<Character> characters = new List<Character>() 
        {
            new Character(),
            new Character() { Name = "Hulk", Id = 100, Class = RpgClass.Athletics },
            new Character() { Name = "Strange", Id = 105, Class = RpgClass.Magic },
            new Character() { Name = "Wolverine", Id = 110, Class = RpgClass.Mutation }
        };

        [HttpGet("{id}")]
        public IActionResult GetCharacterById(int id)
        {
            return Ok(characters.FirstOrDefault(c => c.Id == id));
        }


        [Route("")]
        public IActionResult GetCharacters() 
        {
            return Ok(characters);
        }
    }
}