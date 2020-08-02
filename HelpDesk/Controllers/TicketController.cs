using AutoMapper;
using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.DataTransferObjects.Ticket;
using HelpDesk.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        public TicketController(IRepositoryWrapper repository, IMapper mapper)
        {
            this._mapper = mapper;
            this._repository = repository;
        }

        [HttpGet]
      
        public async Task<IActionResult> GetTickets( )
        {
            var ticketList = new List<TicketDto>();

            var userType = User.Claims.FirstOrDefault(x => x.Type.Equals("UserType", StringComparison.InvariantCultureIgnoreCase)).Value;
            var userRole = User.Claims.FirstOrDefault(x => x.Type.Equals("UserRole", StringComparison.InvariantCultureIgnoreCase)).Value;
            var userCompanyId = User.Claims.FirstOrDefault(x => x.Type.Equals("CompanyId", StringComparison.InvariantCultureIgnoreCase)).Value;
            var userName = User.Claims.FirstOrDefault(x => x.Type.Equals("UserName", StringComparison.InvariantCultureIgnoreCase)).Value;

            try
            {
                if ( userType == "HelpDesk") 
                {
                    var tkts = await _repository.Ticket.GetAllTicket();

                    foreach (TicketModel tkt in tkts)
                    {
                        var _tkt = _mapper.Map<TicketDto>(tkt);
                        var c = await _repository.Category.GetCategoryById(tkt.CategoryId);
                        _tkt.CategoryName = c.CategoryName;

                        var p = await _repository.Product.GetProductById(tkt.ProductId);
                        _tkt.ProductName = p.ProductName;

                        var m = await _repository.Module.GetModuleById(tkt.ModuleId);
                        _tkt.ModuleName = m.ModuleName;

                        var companyName = await _repository.Company.GetCompanyById(new Guid(tkt.CompanyId));
                        _tkt.CompanyName = companyName.CompanyName;

                        ticketList.Add(_tkt);
                    }
                    return Ok(ticketList);
                }
                else if(userType == "Client")
                {
                    var tkts = await _repository.Ticket.GetTicketByCondition(new Guid(userCompanyId), userRole, userName);
                    foreach(TicketModel tkt in tkts)
                    {
                        var _tkt = _mapper.Map<TicketDto>(tkts);
                        var c = await _repository.Category.GetCategoryById(tkt.CategoryId);
                        _tkt.CategoryName = c.CategoryName;

                        var p = await _repository.Product.GetProductById(tkt.ProductId);
                        _tkt.ProductName = p.ProductName;

                        var m = await _repository.Module.GetModuleById(tkt.ModuleId);
                        _tkt.ModuleName = m.ModuleName;

                        var companyName = await _repository.Company.GetCompanyById(new Guid(tkt.CompanyId));
                        _tkt.CompanyName = companyName.CompanyName;

                        ticketList.Add(_tkt);
                    }
                    return Ok(ticketList);
                }
                return StatusCode(500, "Something went wrong");

            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

       

        [HttpGet]
        [Route("[controller]/{id}", Name = "TicketById")]
        public async Task<IActionResult> GetTicketById(string id)
        {
            try
            {

                var ticket = await _repository.Ticket.GetTicketById(new Guid(id));
                if (ticket == null)
                {
                    return NotFound();
                }
                else
                {
                    //mappers not use -> ** should dev in future
                    //var productResult = _mapper.Map<ProductDto>(product);
                    return Ok(ticket);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketDto CreateTicket)
        {
            try
            {
                if (CreateTicket == null)
                {
                    return BadRequest("Ticket object is null");
                }

                // convert incoming Dto to actual Model instance
                var createTicketEntity = _mapper.Map<TicketModel>(CreateTicket);

                _repository.Ticket.CreateTicket(createTicketEntity);
                await _repository.Save();

                // convert the model back to a DTO for output
                var createdTicket = _mapper.Map<TicketDto>(createTicketEntity);

                return CreatedAtRoute("TicketById", new { id = createTicketEntity.TicketId }, createdTicket);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpDelete]
        [Route("[controller]/{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var ticket = await _repository.Ticket.GetTicketById(id);
            if (ticket == null)
            {
                return StatusCode(500, "Ticket Not Found");
            }
            else
            {
                _repository.Ticket.DeleteTicket(ticket);
                await _repository.Save();
                return Json("Ticket successfully removed");
            }
        }
    }
}
