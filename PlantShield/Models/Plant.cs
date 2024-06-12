namespace PlantShield.Models
{
    public class Plant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Disease> Diseases { get; set; } = new HashSet<Disease>();
    }
}
