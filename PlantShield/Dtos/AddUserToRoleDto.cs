using System.ComponentModel.DataAnnotations;

namespace PlantShield.Dtos
{
    public class AddUserToRoleDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
