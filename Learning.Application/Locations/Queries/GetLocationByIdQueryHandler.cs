using Learning.Application.Common.Interfaces;
using Learning.Application.DTOs;
using Learning.Domain.Entities;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System;

namespace Learning.Application.Locations.Queries
{
    public class GetLocationByIdQueryHandler
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Location> _locationRepository;
        private readonly IUserContext _userContext;

        public GetLocationByIdQueryHandler(IRepository<Location> locationRepository,IMapper mapper,IUserContext userContext)
        {
            _mapper = mapper;
            _locationRepository = locationRepository;
            _userContext = userContext;
        }
        public async Task<LocationDetailsDto> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
        {

            var result = await _locationRepository.GetSingleWithIncludeAsync(x=>x.Id==request.Id, include: q => q.Include(x => x.HousingApplications), cancellationToken);
            if (!_userContext.Roles.Contains("Admin"))
            {
                result.HousingApplications = result.HousingApplications
                    .Where(h => h.Email == _userContext.Email)
                    .ToList();
            }
            return _mapper.Map<LocationDetailsDto>(result);
        }
    }
}
