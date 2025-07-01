using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class CreateWeighting
    {
        public int AnimalId { get; set; }
        public int Weight { get; set; }
        public DateOnly WeightDate { get; set; }
        public string? AssignedToUserId { get; set; }
    }
}
