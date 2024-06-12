using System.ComponentModel.DataAnnotations;

namespace PlantShield.Models
{
    public class Irrigation
    {
        public int Id { get; set; }
        public bool IsAutomatic { get; set; }
        [Required]
        //[MaxLength(10)]
        public bool PumpState { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Station Station { get; set; }
        public string StationId { get; set; }
    }
}
