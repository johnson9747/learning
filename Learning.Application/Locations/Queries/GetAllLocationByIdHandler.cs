using Learning.Application.Common.Interfaces;
using Learning.Application.DTOs;
using Learning.Domain.Entities;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Locations.Queries
{
    public class GetAllLocationByIdHandler : IRequestHandler<GetAllLocationByIdQuery, LocationDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Location> _locationRepository;

        public GetAllLocationByIdHandler(IRepository<Location> locationRepository,IMapper mapper)
        {
            _mapper = mapper;
            _locationRepository = locationRepository;
        }
        public async Task<LocationDto> Handle(GetAllLocationByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _locationRepository.GetByIdAsync(request.Id,cancellationToken);
            return _mapper.Map<LocationDto>(result);
        }
    }
}
