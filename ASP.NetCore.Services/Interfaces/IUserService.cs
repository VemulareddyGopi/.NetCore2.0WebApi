using ASP.NetCore.Models.Models;
using System.Collections.Generic;

namespace ASP.NetCore.Services.Interfaces
{
    public interface IUserService
    {
        User Login(User user);

        IEnumerable<User> Users();

        string AddUsers(User user);

        bool CheckUser(User user);

        string DeleteUser(long id);
    }
}
