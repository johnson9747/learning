using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.Entities
{
    public class HousingApplication
    {
        public int Id { get; set; }

        // Foreign key to Location
        public int LocationId { get; set; }

        // Navigation property
        public Location Location { get; set; } = default!;

        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Details { get; set; } = default!;
    }
}
