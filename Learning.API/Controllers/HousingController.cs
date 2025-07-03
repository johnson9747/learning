using Learning.Application.DTOs;
using Learning.Application.Locations.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HousingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HousingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("locations")]
        public async Task<List<LocationDto>> GetLocations()
        {
              return await _mediator.Send(new GetAllLocationsQuery());
        }
        [HttpGet]
        [Route("locations/{id}")]
        public async Task<LocationDto> GetLocationById(int id)
        {
            return await _mediator.Send(new GetAllLocationByIdQuery() { Id=id});
        }

    }
}

