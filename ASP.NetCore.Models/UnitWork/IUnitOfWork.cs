using ASP.NetCore.Models.Models;
using ASP.NetCore.Models.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASP.NetCore.Models.UnitWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> UserRepository { get; }

        void Save();
    }
}
