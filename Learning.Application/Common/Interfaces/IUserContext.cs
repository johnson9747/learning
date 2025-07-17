﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Common.Interfaces
{
    public interface IUserContext
    {
        string FirstName { get; }
        string LastName { get; }
        string Email { get; }
        List<string> Roles { get; }
    }
}
