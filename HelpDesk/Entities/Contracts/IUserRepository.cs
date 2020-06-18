using HelpDesk.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Entities.Contracts
{
    public interface IUserRepository
    {
        //UserModel Add(UserModel tktUser);
        void CreateUser(UserModel tktUser);
    }
}
