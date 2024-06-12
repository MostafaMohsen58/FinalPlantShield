namespace PlantShield.Models
{
    public class SensorData
    {
        public int Id { get; set; }
        public double TemperatureInCelicus { get; set; } = default;
        public double TemperatureInFerhnient { get; set; }=default;
        public double Humdity { get; set; } = default;
        public double Moisture { get; set; } = default;
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
        public Station Station { get; set; }
        public string StationId { get; set; }
    }
}
