using ASP.NetCore.Models.Models;
using ASP.NetCore.Models.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASP.NetCore.Models.UnitWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ASPNetCoreContext _context;
        private IGenericRepository<User> _userRepository;

        public UnitOfWork(ASPNetCoreContext Context)
        {
            this._context = Context;
        }
        public IGenericRepository<User> UserRepository
        {
            get { return this._userRepository ?? (this._userRepository = new GenericRepository<User>(_context)); }
        }

        public void Save()
        {

            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
