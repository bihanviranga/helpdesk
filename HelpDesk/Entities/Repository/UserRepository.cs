using HelpDesk.Entities;
using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Entities.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly HelpDeskContext context;
        public UserRepository(HelpDeskContext context)
        {
            this.context = context;
        }

        public UserModel Add(UserModel User)
        {
            context.Add(User);
            context.SaveChanges();
            return User;
        }
    }
}
