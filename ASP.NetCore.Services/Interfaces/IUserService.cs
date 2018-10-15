using ASP.NetCore.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASP.NetCore.Services.Interfaces
{
    public interface IUserService
    {
        User Login(User user);

        IEnumerable<User> Users();
    }
}
