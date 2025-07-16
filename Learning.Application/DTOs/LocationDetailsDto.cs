using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.DTOs
{
    public class LocationDetailsDto: LocationDto
    {
        public List<EnquiriesDto> Enquiries { get; set; } = new();
    }
}
