using System.Collections.Generic;
using System.Threading.Tasks;
using HelpDesk.Entities.Models;

namespace HelpDesk.Entities.Contracts
{
    public interface IResTemplateRepository : IRepositoryBase<ResTemplateModel>
    {
        void CreateResTemplate(ResTemplateModel template);
        void UpdateResTemplate(ResTemplateModel template);
        void DeleteResTemplate(ResTemplateModel template);
        Task<ResTemplateModel> GetResTemplateById(int id);
        Task<IEnumerable<ResTemplateModel>> GetAllResTemplates();
    }
}
