using Learning.Application.Common.Interfaces;
using Learning.Application.DTOs;
using Learning.Domain.Entities;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Learning.Application.Locations.Queries
{
    public class GetLocationByIdQueryHandler
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Location> _locationRepository;

        public GetLocationByIdQueryHandler(IRepository<Location> locationRepository,IMapper mapper)
        {
            _mapper = mapper;
            _locationRepository = locationRepository;
        }
        public async Task<LocationDetailsDto> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _locationRepository.GetWhereWithIncludeAsync(x=>x.Id==request.Id, include: q => q.Include(x => x.HousingApplications), cancellationToken);
            return _mapper.Map<LocationDetailsDto>(result.FirstOrDefault());
        }
    }
}
