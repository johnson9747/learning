using Learning.Application.DTOs;
using Learning.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Common.Mappings
{
    public class LocationMappingProfile: IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Location,LocationDto>();
        }
    }
}
