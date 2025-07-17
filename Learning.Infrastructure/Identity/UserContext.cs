using Learning.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Infrastructure.Identity
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

        public string FirstName => User?.FindFirst(ClaimTypes.GivenName)?.Value ?? "";
        public string LastName => User?.FindFirst(ClaimTypes.Surname)?.Value ?? "";
        public string Email => User?.FindFirst(ClaimTypes.Email)?.Value ?? "";
        public List<string> Roles
        {
            get
            {
                var roles = User?.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();
                return roles ?? new List<string>();
            }
        }
    }
}
