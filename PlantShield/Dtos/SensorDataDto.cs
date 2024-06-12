using System.ComponentModel.DataAnnotations;

namespace PlantShield.Dtos
{
    public class SensorDataDto
    {
        public string Email { get; set; }
        public string StationId { get; set; }
        public double TemperatureInCelicus { get; set; }
        public double TemperatureInFerhnient { get; set; }
        public double Humdity { get; set; }
        public double Moisture { get; set; }
        
    }
}
