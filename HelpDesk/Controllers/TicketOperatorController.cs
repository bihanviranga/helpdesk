using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Controllers
{
    [Route("ticketoperator")]
    [ApiController]
    public class TicketOperatorController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public TicketOperatorController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("user/{username}")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            try
            {
                var entries = await _repository.TicketOperator.GetByUsername(username);
                var entriesResult = _mapper.Map<IEnumerable<TicketOperatorDto>>(entries);
                return Ok(entriesResult);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet("ticket/{ticketId}")]
        public async Task<IActionResult> GetByTicketId(Guid ticketId)
        {
            try
            {
                var entries = await _repository.TicketOperator.GetByTicketId(ticketId);
                var entriesResult = _mapper.Map<IEnumerable<TicketOperatorDto>>(entries);
                return Ok(entriesResult);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
