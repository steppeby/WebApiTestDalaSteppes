using System.Text.Json.Serialization;

namespace Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sex { get; set; }
        public DateTime ArrivalDate { get; set; }
        public int ArrivalAge { get; set; }
        public int BreedId { get; set; }
        [JsonIgnore]
        public Breed? Breed { get; set; }
        public int? ParentId { get; set; }
    }
}
