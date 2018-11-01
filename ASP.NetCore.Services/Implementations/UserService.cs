

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
        public string AddUsers(User user)
        {
            try
            {
                if (user.Id > 0)
                {
                    _unitOfWork.UserRepository.Update(user);

                    return "User Updated Successfully !";
                }
                else
                {
                    _unitOfWork.UserRepository.Insert(user);

                    return "User Insert Successfully !";
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string DeleteUser(long id)
        {

            _unitOfWork.UserRepository.Delete(id);

            return "User Deleted Successfully !";
        }

        public bool CheckUser(User user)
        {
            if (user.Id > 0)
            {
                return _unitOfWork.UserRepository.GetManyQueryable(x => x.Id == user.Id && x.Email == user.Email).Any();
            }
            else
            {
                return _unitOfWork.UserRepository.GetManyQueryable(x => x.Email == user.Email).Any();
            }
        }
    }
}
