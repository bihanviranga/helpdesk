using HelpDesk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Models.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly helpdeskContext context;
        public UserRepository(helpdeskContext context)
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
