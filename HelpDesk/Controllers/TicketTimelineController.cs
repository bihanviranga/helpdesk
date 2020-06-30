using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Controllers
{
    [Route("timeline")]
    [ApiController]
    public class TicketTimelineController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public TicketTimelineController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("ticket/{ticketId}")]
        public async Task<IActionResult> GetEntriesByTicketId(Guid ticketId)
        {
            try
            {
                var entries = await _repository.TicketTimeline.GetEntriesByTicketId(ticketId);
                var entriesResult = _mapper.Map<IEnumerable<TicketTimelineDto>>(entries);
                return Ok(entriesResult);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet("user/{username}")]
        public async Task<IActionResult> GetEntriesByUsername(string username)
        {
            try
            {
                var entries = await _repository.TicketTimeline.GetEntriesByUsername(username);
                var entriesResult = _mapper.Map<IEnumerable<TicketTimelineDto>>(entries);
                return Ok(entriesResult);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet("event/{tktEvent}")]
        public async Task<IActionResult> GetEntriesByEvent(string tktEvent)
        {
            try
            {
                var entries = await _repository.TicketTimeline.GetEntriesByEvent(tktEvent);
                var entriesResult = _mapper.Map<IEnumerable<TicketTimelineDto>>(entries);
                return Ok(entriesResult);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
