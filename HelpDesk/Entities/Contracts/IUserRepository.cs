using HelpDesk.Entities.DataTransferObjects;
using HelpDesk.Entities.DataTransferObjects.User;
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
        Task<UserModel> GetUserByUserName(string userName);
        Task<UserModel> LoginUser(UserLoginDto userLoginDto);
        Task<IEnumerable<UserModel>> GetUsersByCondition(string userType , string userCompanyId);

        Task<string> CheckAvaibality(CheckAvaibalityDto data);
        void CreateUser(UserModel tktUser);
        void DeleteUser(UserModel user);
        void UpdateUser(UserModel user);
    }
}
