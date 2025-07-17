using Learning.Application.Common.Interfaces;
using Learning.Domain.Entities;
using Learning.Infrastructure.Persistence;
using Learning.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Infrastructure.Identity
{
    internal class CustomClaimsTransformer : IClaimsTransformation
    {
        private readonly IRepository<User> _repository;

        public CustomClaimsTransformer(IRepository<User> repository)
        {
            _repository = repository;
        }
        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var identity = (ClaimsIdentity)principal.Identity!;
            var email = principal.FindFirst(ClaimTypes.Email)?.Value;
            if (!string.IsNullOrWhiteSpace(email) && !identity.HasClaim(c => c.Type == ClaimTypes.Role))
            {
                var user = await _repository.GetSingleWithIncludeAsync(
                    u => u.UserName == email,
                     q => q.Include(u => u.UserRoles)
          .ThenInclude(ur => ur.Role));

                if (user != null)
                {
                    foreach (var role in user.UserRoles.Select(r => r.Role.Name))
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, role));
                    }
                }
            }

            return principal;
        }
    }
}
