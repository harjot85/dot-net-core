using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using dotnetcore.Data;
using dotnetcore.DTOs.Character;
using dotnetcore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace dotnetcore.services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _context = context;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        
        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new();
            List<Character> characters = await _context.Characters.ToListAsync();
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(characters.FirstOrDefault(c => c.Id == id && c.User.Id == GetUserId()));
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new();
            List<Character> characters = await _context.Characters.Where(c => c.User.Id == GetUserId()).ToListAsync();
            serviceResponse.Data = _mapper.Map<List<GetCharacterDto>>(characters);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new();
            Character character = _mapper.Map<Character>(newCharacter);
            character.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            
            await _context.Characters.AddAsync(character);
            await _context.SaveChangesAsync();
            
            serviceResponse.Data = _context.Characters.Where(c => c.User.Id == GetUserId()).Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                List<Character> characters = await _context.Characters.ToListAsync();
                Character character = characters.FirstOrDefault(c => c.Id == updateCharacter.Id);
                character.Name = updateCharacter.Name;
                character.Class = updateCharacter.Class;
                character.Strength = updateCharacter.Strength;
                character.HitPoints = updateCharacter.HitPoints;
                character.Intelligence = updateCharacter.Intelligence;
                character.Defense = updateCharacter.Defense;

                _context.Characters.Update(character);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                serviceResponse.IsSuccess = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new();

            try
            {
                Character character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id && c.Id == GetUserId());

                if (character != null)
                {
                    _context.Characters.Remove(character);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _context.Characters.Where(c => c.User.Id == GetUserId())
                        .Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
                }
                else
                {
                    serviceResponse.IsSuccess = false;
                    serviceResponse.Message = "Character not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.IsSuccess = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}