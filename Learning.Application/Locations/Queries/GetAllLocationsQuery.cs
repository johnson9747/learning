using Learning.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Locations.Queries
{
    public class GetAllLocationsQuery: IRequest<List<LocationDto>>
    {
    }
}
