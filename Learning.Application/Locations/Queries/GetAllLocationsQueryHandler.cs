using Learning.Application.Common.Interfaces;
using Learning.Application.DTOs;
using Learning.Domain.Entities;
using MapsterMapper;

namespace Learning.Application.Locations.Queries
{
    public class GetAllLocationsQueryHandler 
    {
        private readonly IRepository<Location> _locationRepository;
        private readonly IMapper _mapper;

        public GetAllLocationsQueryHandler(IRepository<Location> locationRepository,IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }
        public async Task<List<LocationDto>> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
        {
            var result= await _locationRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<LocationDto>>(result);
        }
    }
}
