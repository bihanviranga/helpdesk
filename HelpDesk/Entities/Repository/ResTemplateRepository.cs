using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Entities.Repository
{
    public class ResTemplateRepository : RepositoryBase<ResTemplateModel>, IResTemplateRepository
    {
        public ResTemplateRepository(HelpDeskContext helpDeskContext) : base(helpDeskContext) { }

        public void CreateResTemplate(ResTemplateModel template)
        {
            Create(template);
        }

        public void DeleteResTemplate(ResTemplateModel template)
        {
            Delete(template);
        }

        public async Task<IEnumerable<ResTemplateModel>> GetAllResTemplates()
        {
            return await FindAll().OrderBy(tmp => tmp.TemplateName).ToListAsync();
        }

        public async Task<ResTemplateModel> GetResTemplateById(int id)
        {
            return await FindByCondition(tmp => tmp.TemplateId.Equals(id)).FirstOrDefaultAsync();
        }

        public void UpdateResTemplate(ResTemplateModel template)
        {
            Update(template);
        }
    }
}
