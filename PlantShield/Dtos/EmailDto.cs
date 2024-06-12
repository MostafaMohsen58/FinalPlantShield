using System.ComponentModel.DataAnnotations;

namespace PlantShield.Dtos
{
    public class EmailDto
    {
        [Required]
        public string Email { get; set; }
    }
}
