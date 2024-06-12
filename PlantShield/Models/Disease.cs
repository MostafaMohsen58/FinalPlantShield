namespace PlantShield.Models
{
    public class Disease
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Solution { get; set; }
        public string Other_Treatment { get; set; }
        public Plant Plant { get; set; }
        public int PlantId { get; set; }




    }
}
