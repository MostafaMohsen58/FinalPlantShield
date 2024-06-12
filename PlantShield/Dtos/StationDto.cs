using PlantShield.Models;
using System.ComponentModel.DataAnnotations;

namespace PlantShield.Dtos
{
    public class StationDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string SoilType { get; set; }
        public string PlantType { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}
