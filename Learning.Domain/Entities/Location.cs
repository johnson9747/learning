using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string City { get; set; } = default!;
        public string State { get; set; } = default!;
        public string Photo { get; set; }= default!;
        public int AvailableUnits { get; set; }
        public bool Wifi { get; set; }
        public bool Laundry { get; set; }
        // Navigation property to related HousingApplications
        public ICollection<HousingApplication> HousingApplications { get; set; } = new List<HousingApplication>();
    }
}
