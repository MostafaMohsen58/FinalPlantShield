using System.ComponentModel.DataAnnotations;

namespace PlantShield.Dtos
{
    public class ReturnStationDto
    {
        public string Id { get; set; }
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
