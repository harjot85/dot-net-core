using AutoMapper;
using dotnetcore.DTOs.Character;
using dotnetcore.Models;

namespace dotnetcore
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
        }
    }
}