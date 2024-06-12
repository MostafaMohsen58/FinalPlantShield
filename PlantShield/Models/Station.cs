using System.ComponentModel.DataAnnotations;

namespace PlantShield.Models
{
    public class Station
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SoilType { get; set; }
        public string PlantType { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }= DateTime.Now;
    }
}
