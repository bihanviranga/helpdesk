using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Entities.Repository
{
    public class TicketRepository : RepositoryBase<TicketModel>, ITicketRepository
    {
        public TicketRepository(HelpDeskContext helpDeskContext) : base(helpDeskContext) { }
        public void CreateTicket(TicketModel ticket)
        {
            ticket.TicketId = Guid.NewGuid().ToString();
            Create(ticket);
        }

        public void DeleteTicket(TicketModel ticket)
        {
            Delete(ticket);
        }

        public async Task<IEnumerable<TicketModel>> GetAllTicket()
        {
            return await FindAll().OrderBy(tkt => tkt.TktCreatedDate).ToListAsync();
        }

        public async Task<FileStream> GetAttachment(Guid ticketId)
        {
            var ticket = await GetTicketById(ticketId);
            var path = ticket.TktAttachment;
            var stream = new FileStream(path, FileMode.Open);
            return stream;
        }

        public async Task<IEnumerable<TicketModel>> GetTicketByCondition(Guid id, string userRole, string userName)
        {
            if (userRole == "Manager")
            {
                return await FindByCondition(tkt => tkt.CompanyId.Equals(id.ToString())).ToListAsync();
            }
            else if (userRole == "User")
            {
                return await FindByCondition(tkt => tkt.CompanyId == id.ToString() && tkt.TktCreatedBy == userName).ToListAsync();
            }

            return null;

        }

        public async Task<TicketModel> GetTicketById(Guid id)
        {
            return await FindByCondition(tkt => tkt.TicketId.Equals(id.ToString())).FirstOrDefaultAsync();
        }

        public async Task<string> GetTicketCodesByCondition(string id)
        {
            return await FindByCondition(tkt => tkt.CompanyId.Equals(id)).OrderByDescending(s => s.TicketCode).Select(tkt => tkt.TicketCode).FirstOrDefaultAsync();
        }

        public void UpdateTicket(TicketModel ticket)
        {
            Update(ticket);
        }

        public async Task<string> UploadAttachment(IFormFile attachment, string ticketId)
        {
            try
            {
                string fileName = ticketId;

                var storagePath = Path.Combine(Directory.GetCurrentDirectory(), "AttachmentStorage");
                if (!Directory.Exists(storagePath))
                {
                    Directory.CreateDirectory(storagePath);
                }

                var path = Path.Combine(storagePath, fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await attachment.CopyToAsync(stream);
                }
                return path;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
