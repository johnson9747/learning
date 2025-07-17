using Learning.Application.Common.Interfaces;
using Learning.Domain.Entities;

namespace Learning.Application.HousingApplications.Commands
{
    public class CreateHousingApplicationCommandHandler
    {
        private readonly IRepository<HousingApplication> _repository;
        private readonly IUserContext _userContext;

        public CreateHousingApplicationCommandHandler(IRepository<HousingApplication> repository, IUserContext userContext)
        {
            _repository = repository;
            _userContext = userContext;
        }
        public async Task<bool> Handle(CreateHousingApplicationCommand request, CancellationToken cancellationToken)
        {
            var entity = new HousingApplication
            {
                Email = _userContext.Email,
                FirstName = _userContext.FirstName,
                LastName = _userContext.LastName,
                LocationId = request.LocationId,
                Details = request.Details
            };
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
