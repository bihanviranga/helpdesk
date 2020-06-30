using HelpDesk.Entities;
using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.DataTransferObjects;
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
            user.UserName = Guid.NewGuid().ToString();
            Create(user);
        }

        public void DeleteUser(UserModel user)
        {
            Delete(user);
        }

        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            return await FindAll().OrderBy(cmp => cmp.CompanyId).ToListAsync();
        }

        public async Task<UserModel> GetUserByUserName(Guid userName)
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
    }
}
