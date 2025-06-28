using System.Text.Json.Serialization;

namespace Models
{
    public class Breed
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AnimalTypeId { get; set; }
        [JsonIgnore]
        public AnimalType? AnimalType { get; set; }
    }
}
