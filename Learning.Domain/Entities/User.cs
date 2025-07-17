using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = default!;
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
