using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace Models
{
    public class Weighting
    {
        public int Id { get; set; }
        public int AnimalId { get; set; }
        [JsonIgnore]
        public Animal? Animal { get; set; }
        public int Weight { get; set; }
        public DateOnly WeightDate { get; set; }
        public string? AssignedToUserId { get; set; }
        [JsonIgnore]
        public IdentityUser? AssignedToUser { get; set; }

    }
}
