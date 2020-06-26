using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Controllers
{
    [Route("notification")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public NotificationController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetNotificationsForUser(string userId)
        {
            try
            {
                var notifications = await _repository.Notification.GetNotificationsForUser(userId);
                var notificationsResult = _mapper.Map<IEnumerable<NotificationDto>>(notifications);
                return Ok(notificationsResult);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotificationById(Guid id)
        {
            try
            {
                var notification = await _repository.Notification.GetNotificationById(id);
                if (notification == null)
                {
                    return NotFound();
                }
                else
                {
                    var notificationResult = _mapper.Map<NotificationDto>(notification);
                    return Ok(notificationResult);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> MarkNotification(Guid id, [FromBody] NotificationMarkDto notification)
        {
            try
            {
                if (notification == null)
                {
                    return BadRequest("Notification object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid notification object");
                }

                var notificationEntity = await _repository.Notification.GetNotificationById(id);
                if (notificationEntity == null)
                {
                    return NotFound();
                }

                _mapper.Map(notification, notificationEntity);

                _repository.Notification.UpdateNotification(notificationEntity);
                await _repository.Save();

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
