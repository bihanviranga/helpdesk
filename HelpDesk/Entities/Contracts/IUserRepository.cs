using HelpDesk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Models.Users
{
    public interface IUserRepository
    {
        UserModel Add(UserModel tktUser);
    }
}
