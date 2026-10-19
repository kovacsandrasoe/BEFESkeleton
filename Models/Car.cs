using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEFESkeleton.Models
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [StringLength(200)]
        public string Brand { get; set; } = string.Empty;

        [StringLength(200)]
        public string Model { get; set; } = string.Empty;
    }
}
