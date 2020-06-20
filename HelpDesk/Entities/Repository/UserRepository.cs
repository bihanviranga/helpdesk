using HelpDesk.Entities;
using HelpDesk.Entities.Contracts;
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
        public UserRepository(HelpDeskContext helpDeskContext) : base(helpDeskContext) { }

        public void CreateUser(UserModel user)
        {
            //user.CompanyId = Guid.NewGuid().ToString();
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

        public async Task<UserModel> GetUserById(String id)
        {
            return await FindByCondition(cmp => cmp.CompanyId.Equals(id.ToString())).FirstOrDefaultAsync();
        }
    }
}
