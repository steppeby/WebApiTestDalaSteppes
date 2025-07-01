using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class CreateAnimal
    {
        public string Name { get; set; }
        public int Sex { get; set; }
        public DateOnly ArrivalDate { get; set; }
        public int ArrivalAge { get; set; }
        public int BreedId { get; set; }
        public int? ParentId { get; set; }
    }
}
