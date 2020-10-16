using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.DataTransferObjects.Conversation;
using HelpDesk.Entities.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HelpDesk.Controllers
{
    public class ConversationController : Controller
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        public ConversationController(IRepositoryWrapper repository, IMapper mapper)
        {
            this._mapper = mapper;
            this._repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateConversation([FromBody] ConversationCreateDto conversation)
        {
            try
            {
                if (conversation == null)
                {
                    return BadRequest("conversation object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid conversation object");
                }

                // convert incoming Dto to actual Model instance
                var conversationEntity = _mapper.Map<ConversationModel>(conversation);

                _repository.Conversation.CreateConversation(conversationEntity);
                await _repository.Save();

                // Notification event
                // Conversation notifications go to 2 people,
                // either the ticket owner, or the assigned user (if any).
                var ticket = await _repository.Ticket.GetTicketById(new Guid(conversation.TicketId));
                if (conversation.CvSender == ticket.TktAssignedTo)
                {
                    _repository.Notification.CreateNotification("tktReplied", conversation.TicketId, ticket.TktCreatedBy);
                }
                else if ((conversation.CvSender == ticket.TktCreatedBy) && (ticket.TktAssignedTo != null))
                {
                    _repository.Notification.CreateNotification("tktReplied", conversation.TicketId, ticket.TktAssignedTo);
                }
                await _repository.Save();

                // convert the model back to a DTO for output
                var createdConversation = _mapper.Map<ConversationDto>(conversationEntity);

                return Ok(createdConversation);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet]
        [Route("[controller]/{TicketId}")]
        public async Task<IActionResult> GetConversation(string TicketId)
        {
            var Ticket = await _repository.Ticket.GetTicketById(new Guid(TicketId));
            var conversationList = new List<ConversationDto>();
            if (Ticket != null)
            {
                var convercations = await _repository.Conversation.GetConversationByTicketId(new Guid(Ticket.TicketId));

                foreach (var conversation in convercations)
                {
                    var TempConversationDto = _mapper.Map<ConversationDto>(conversation);
                    conversationList.Add(TempConversationDto);
                }

                return Ok(conversationList);
            }

            return StatusCode(500, "Something went wrong");

        }


    }
}
