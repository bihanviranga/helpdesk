using HelpDesk.Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Entities.Contracts
{
    public interface ITicketRepository : IRepositoryBase<TicketModel>
    {
        Task<IEnumerable<TicketModel>> GetAllTicket();
        Task<TicketModel> GetTicketById(Guid id, Boolean noTracking = false);
        Task<IEnumerable<TicketModel>> GetTicketByCondition(Guid id, string userRole, string userName);
        Task<string> GetTicketCodesByCondition(string id);
        Task<IEnumerable<TicketModel>> GetTicketsByBrand(CompanyBrandModel brand);
        Task<IEnumerable<TicketModel>> GetTicketsByCompany(CompanyModel company);
        Task<IEnumerable<TicketModel>> GetTicketsByCategory(CategoryModel category);
        Task<IEnumerable<TicketModel>> GetTicketsByProduct(ProductModel product);
        Task<IEnumerable<TicketModel>> GetTicketsByModule(ModuleModel module);
        void CreateTicket(TicketModel ticket);
        void UpdateTicket(TicketModel ticket);
        void DeleteTicket(TicketModel ticket);

        Task<string> UploadAttachment(IFormFile attachment, string ticketId);
        Task<FileStream> GetAttachment(Guid ticketId);
    }
}
