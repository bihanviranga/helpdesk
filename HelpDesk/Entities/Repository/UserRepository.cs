using HelpDesk.Entities;
using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.Models;
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
    }
}
