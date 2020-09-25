using HelpDesk.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Entities.Contracts
{
    public interface IDashboardRepository : IRepositoryBase<DashboardModel>
    {
        Task<DashboardModel> GetDashboardDetails(string userType);

    }
}
