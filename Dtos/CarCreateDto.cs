using System.ComponentModel.DataAnnotations;

namespace BEFESkeleton.Dtos
{
    public class CarCreateDto
    {
        [StringLength(200)]
        public string Brand { get; set; } = string.Empty;

        [StringLength(200)]
        public string Model { get; set; } = string.Empty;
    }
}
