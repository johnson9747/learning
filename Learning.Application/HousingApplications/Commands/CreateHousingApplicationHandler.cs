using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.HousingApplications.Commands
{
    public class CreateHousingApplicationHandler : IRequestHandler<CreateHousingApplicationCommand, int>
    {
        public CreateHousingApplicationHandler()
        {
            
        }
        public async Task<int> Handle(CreateHousingApplicationCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
