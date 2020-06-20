using HelpDesk.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Entities.Contracts
{
    public interface IUserRepository : IRepositoryBase<UserModel>
    {
        Task<IEnumerable<UserModel>> GetAllUsers();
        Task<UserModel> GetUserByUserName(Guid userName);
        void CreateUser(UserModel tktUser);
        void DeleteUser(UserModel user);
    }
}
