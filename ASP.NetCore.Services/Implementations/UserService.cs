

using ASP.NetCore.Models.Models;
using ASP.NetCore.Models.UnitWork;
using ASP.NetCore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASP.NetCore.Services.Implementations
{
    public class UserService : IUserService
    {
        IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }




        public User Login(User user)
        {
            try
            {
                return _unitOfWork.UserRepository.GetManyQueryable(x => x.Email == user.Email && x.PassWord == user.PassWord).FirstOrDefault();

            }
            catch (Exception e)
            {
                return null;
            }

        }

        public IEnumerable<User> Users()
        {
            try
            {
                return _unitOfWork.UserRepository.Get();

            }
            catch (Exception e)
            {
                return null;
            }



        }
    }
}
