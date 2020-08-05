using HelpDesk.Entities;
using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.DataTransferObjects;
using HelpDesk.Entities.DataTransferObjects.User;
using HelpDesk.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Entities.Repository
{
    public class UserRepository : RepositoryBase<UserModel> , IUserRepository
    {
        public UserRepository(HelpDeskContext helpDeskContext) : base(helpDeskContext) {  }

        public void CreateUser(UserModel user)
        {
         
            Create(user);
        }

        public void DeleteUser(UserModel user)
        {
            Delete(user);
        }

        public void UpdateUser(UserModel user)
        {
            Update(user);
        }

        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            return await FindAll().OrderBy(cmp => cmp.CompanyId).ToListAsync();
        }

        public async Task<IEnumerable<UserModel>> GetUsersByCondition(string userType , string userCompanyId)
        {
            if (userType == "Client")
            {
                 return await FindByCondition(u => u.CompanyId.Equals(userCompanyId.ToString()))
                        .OrderBy(cmp => cmp.CompanyId).ToListAsync();             
            }
            else if (userType == "HelpDesk")
            {
                 return await FindAll().OrderBy(cmp => cmp.CompanyId).ToListAsync();
            }

            return null;
            
        }

        public async Task<UserModel> GetUserByUserName(string userName)
        {
            return await FindByCondition(u => u.UserName.Equals(userName.ToString())).FirstOrDefaultAsync();
        }

        public async Task<UserModel> LoginUser(UserLoginDto userLoginDto)
        {
            return await HelpDeskContext.Set<UserModel>()
                .Where(
                    u => u.UserName.Equals(userLoginDto.UserNameOrEmail.ToString()) 
                    || u.Email.Equals(userLoginDto.UserNameOrEmail.ToString())
                    )
                .Where(u => u.PasswordHash.Equals(userLoginDto.Password.ToString()))
                .FirstOrDefaultAsync();
        }

        public async Task<string> CheckAvaibality(CheckAvaibalityDto data)
        {
            var query1 = await HelpDeskContext.Set<UserModel>()
                .Where(  u => u.UserName.Equals(data.UserName.ToString()) ) .FirstOrDefaultAsync();
            
            var query2 = await HelpDeskContext.Set<UserModel>()
                .Where(  u =>  u.Email.Equals(data.Email.ToString()) ) .FirstOrDefaultAsync();

            if(query1 == null && query2 == null)
            {
                return "NotTakenYet";
            }
            else if(query1 != null)
            {
                return "UserName_AlreadyTaken";
            }
            else if (query2 != null)
            {
                return "Email_AlreadyTaken";
            }
            else
            {
                return "Both_AlreadyTaken";
            }

        }
    }
}
