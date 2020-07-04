using AutoMapper;
using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.DataTransferObjects.Ticket;
using HelpDesk.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Controllers
{
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
        public async Task<IActionResult> GetAllTickets()
        {
            try
            {
                return Ok(await _repository.Ticket.GetAllTicket());
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet]
        [Route("[controller]/{id}")]
        public async Task<IActionResult> GetTicketById(Guid id)
        {
            try
            {
                var ticket = await _repository.Ticket.GetTicketById(id);
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
        public async Task<IActionResult> CreateTicket([FromBody]CreateTicketDto CreateTicket)
        {
            try
            {
                if (CreateTicket == null)
                {
                    return BadRequest("Ticket object is null");
                }

                // convert incoming CompanyCreateDto to actual CompanyModel instance
                var creatTicketEntity = _mapper.Map<TicketModel>(CreateTicket);

                _repository.Ticket.CreateTicket(creatTicketEntity);
                await _repository.Save();

                // convert the model back to a DTO for output
                //var createdCompany = _mapper.Map<CompanyDto>(companyEntity);

                return Json("Ticket has been created");
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
