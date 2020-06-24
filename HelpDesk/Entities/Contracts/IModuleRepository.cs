﻿using HelpDesk.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Entities.Contracts
{
    public interface IModuleRepository : IRepositoryBase<ModuleModel>
    {
        Task<IEnumerable<ModuleModel>> GetAllModules();
        Task<ModuleModel> GetModuleById(String id);
        void CreateModule(ModuleModel module);
        void DeleteModule(ModuleModel module);
    }
}
