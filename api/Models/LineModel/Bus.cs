using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.LineModel
{
    public class Bus
    {
        public int Id { get; set; }
        public string? Route { get; set; }
        public List<Stop> Stops { get; set; } = new();
    }
}
