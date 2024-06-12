using PlantShield.Models;
using System.ComponentModel.DataAnnotations;

namespace PlantShield.Dtos
{
    public class IrrigationDto
    {
        public string Email { get; set; }
        [Required]
        public bool IsAutomatic { get; set; }
        [Required]
        //[MaxLength(10)]
        public bool PumpState { get; set; }
        public string StationId { get; set; }
    }
}
