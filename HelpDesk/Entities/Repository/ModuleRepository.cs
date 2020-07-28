using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Entities.Repository
{
    public class ModuleRepository : RepositoryBase<ModuleModel> , IModuleRepository
    {
        public ModuleRepository(HelpDeskContext helpDeskContext) : base(helpDeskContext) { }
        public void CreateModule(ModuleModel article)
        {
            Create(article);
        }

        public void DeleteModule(ModuleModel article)
        {
            Delete(article);
        }

        public async Task<IEnumerable<ModuleModel>> GetAllModules()
        {
            return await FindAll().OrderBy(m => m.ModuleId).ToListAsync();
        }

        public async Task<IEnumerable<ModuleModel>> GetModuleByCondition(string userType, string userCompanyId)
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


        public async Task<ModuleModel> GetModuleById(String id)
        {
            return await FindByCondition(m => m.ModuleId.Equals(id.ToString())).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ModuleModel>> GetModulesByCompanyId(string id)
        {
            return await FindByCondition(m => m.CompanyId.Equals(id.ToString())).ToListAsync();
        }

        public void UpdateModule(ModuleModel module)
        {
            Update(module);
        }

    }
}

