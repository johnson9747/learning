using Learning.Application.Common.Interfaces;
using Learning.Domain.Entities;

namespace Learning.Application.HousingApplications.Commands
{
    public class CreateHousingApplicationCommandHandler
    {
        private readonly IRepository<HousingApplication> _repository;

        public CreateHousingApplicationCommandHandler(IRepository<HousingApplication> repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(CreateHousingApplicationCommand request, CancellationToken cancellationToken)
        {
            var entity = new HousingApplication
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                LocationId = request.LocationId
            };
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
