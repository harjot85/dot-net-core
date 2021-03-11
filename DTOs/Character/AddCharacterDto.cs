using dotnetcore.Models;

namespace dotnetcore.DTOs.Character
{
    public class AddCharacterDto
    {
        public string Name { get; set; } = "Iron Man";
        public int HitPoints { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int Intelligence { get; set; }
        public RpgClass Class { get; set; } = RpgClass.Technology;
    }
}