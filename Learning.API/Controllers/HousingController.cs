using Learning.Application.DTOs;
using Learning.Application.HousingApplications.Commands;
using Learning.Application.Locations.Queries;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace Learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HousingController : ControllerBase
    {
        private readonly IMessageBus _bus;

        public HousingController(IMessageBus bus)
        {
            _bus = bus;
        }
        [HttpGet]
        [Route("locations")]
        public async Task<List<LocationDto>> GetLocations()
        {
              return await _bus.InvokeAsync<List<LocationDto>>(new GetAllLocationsQuery());
        }
        [HttpGet]
        [Route("locations/{id}")]
        public async Task<LocationDetailsDto> GetLocationById(int id)
        {
            return await _bus.InvokeAsync<LocationDetailsDto>(new GetLocationByIdQuery(id));
        }
        [Authorize]
        [HttpPost]
        [Route("enquiry")]
        public async Task<bool> AddEnquiry([FromBody] HousingApplicationDto applicationDto)
        {
            var command = applicationDto.Adapt<CreateHousingApplicationCommand>();
            return await _bus.InvokeAsync<bool>(command);
        }

    }
}

