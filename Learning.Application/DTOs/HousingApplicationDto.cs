using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.DTOs
{
    public class HousingApplicationDto
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
