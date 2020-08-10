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
                        if(c != null) _tkt.CategoryName = c.CategoryName;
                         
                        var p = await _repository.Product.GetProductById(tkt.ProductId);
                        if(p != null) _tkt.ProductName = p.ProductName;
                         
                        var m = await _repository.Module.GetModuleById(tkt.ModuleId);
                        if (m != null)  _tkt.ModuleName = m.ModuleName;
                         
                        var companyName = await _repository.Company.GetCompanyById(new Guid(tkt.CompanyId));
                        if(companyName != null) _tkt.CompanyName = companyName.CompanyName;
                        

                        ticketList.Add(_tkt);
                    }
                    return Ok(ticketList);
                }
                else if(userType == "Client")
                {
                    var tkts = await _repository.Ticket.GetTicketByCondition(new Guid(userCompanyId), userRole, userName);
                    foreach(TicketModel tkt in tkts)
                    {
                        var _tkt = _mapper.Map<TicketDto>(tkt);

                        var c = await _repository.Category.GetCategoryById(tkt.CategoryId);
                        if (c != null) _tkt.CategoryName = c.CategoryName;
                        
                        var p = await _repository.Product.GetProductById(tkt.ProductId);
                        if (p != null) _tkt.ProductName = p.ProductName;
                         
                        var m = await _repository.Module.GetModuleById(tkt.ModuleId);
                        if (m != null) _tkt.ModuleName = m.ModuleName;
                         
                        var companyName = await _repository.Company.GetCompanyById(new Guid(tkt.CompanyId));
                        if (companyName != null) _tkt.CompanyName = companyName.CompanyName;
                         

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

                var _ticket = await _repository.Ticket.GetTicketById(new Guid(id));
                if (_ticket == null)
                {
                    return NotFound();
                }
                else
                {
                    var _tkt = _mapper.Map<TicketDto>(_ticket);

                    var c = await _repository.Category.GetCategoryById(_ticket.CategoryId);
                    if (c != null )_tkt.CategoryName = c.CategoryName;

                    var p = await _repository.Product.GetProductById(_ticket.ProductId);
                    if (p != null)  _tkt.ProductName = p.ProductName;

                    var m = await _repository.Module.GetModuleById(_ticket.ModuleId);
                    if (m != null) _tkt.ModuleName = m.ModuleName;

                    var companyName = await _repository.Company.GetCompanyById(new Guid(_ticket.CompanyId));
                    if (companyName != null)  _tkt.CompanyName = companyName.CompanyName;

                   

                    return Ok(_tkt);
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
        public async Task<IActionResult> DeleteTicket(Guid id)
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

        [HttpPut]
        public async Task<IActionResult> UpdateTicket([FromBody]TicketDto _tkt)
        {
            try
            {
                var tkt = _mapper.Map<TicketModel>(_tkt);
                _repository.Ticket.UpdateTicket(tkt);
                await _repository.Save();

                var updatedTkt = _mapper.Map<TicketDto>(tkt);

                var c = await _repository.Category.GetCategoryById(updatedTkt.CategoryId);
                if (c != null)  updatedTkt.CategoryName = c.CategoryName;

                var p = await _repository.Product.GetProductById(updatedTkt.ProductId);
                if (p != null) updatedTkt.ProductName = p.ProductName;

                var m = await _repository.Module.GetModuleById(updatedTkt.ModuleId);
                if (m != null)  updatedTkt.ModuleName = m.ModuleName;

                var companyName = await _repository.Company.GetCompanyById(new Guid(updatedTkt.CompanyId));
                if (companyName != null)  updatedTkt.CompanyName = companyName.CompanyName;

                return Ok(updatedTkt);


            }
            catch
            {
                return Json("Somthing Went worng");
            }
        }
    }
}
