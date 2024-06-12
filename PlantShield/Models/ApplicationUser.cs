using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PlantShield.Models
{
    public class ApplicationUser:IdentityUser
    {

        [Required,MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        public ICollection<SensorData> Sensors { get; set; } = new HashSet<SensorData>();
        public ICollection<Irrigation> Irrigations { get; set; } = new HashSet<Irrigation>();
        public ICollection<Station> Stations { get; set; } = new HashSet<Station>();


    }
}
