using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.DataTransferObjects.ResTemplate;
using HelpDesk.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Controllers
{
    [Route("restemplate")]
    [ApiController]
    public class ResTemplateController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public ResTemplateController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllResTemplates()
        {
            try
            {
                var templates = await _repository.ResTemplate.GetAllResTemplates();
                var templatesResult = _mapper.Map<IEnumerable<ResTemplateDto>>(templates);
                return Ok(templatesResult);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet("{id}", Name = "ResTemplateById")]
        public async Task<IActionResult> GetResTemplateById(int id)
        {
            try
            {
                var template = await _repository.ResTemplate.GetResTemplateById(id);
                if (template == null)
                {
                    return NotFound();
                }
                else
                {
                    var templateResult = _mapper.Map<ResTemplateDto>(template);
                    return Ok(templateResult);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateResTemplate([FromBody] ResTemplateCreateDto template)
        {
            try
            {
                if (template == null)
                {
                    return BadRequest("Template object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid ResponseTemplate object");
                }

                var templateEntity = _mapper.Map<ResTemplateModel>(template);
                _repository.ResTemplate.CreateResTemplate(templateEntity);
                await _repository.Save();

                var createdTemplate = _mapper.Map<ResTemplateDto>(templateEntity);

                return CreatedAtRoute("ResTemplateById", new { id = templateEntity.TemplateId }, createdTemplate);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResTemplate(int id, [FromBody] ResTemplateCreateDto template)
        {
            try
            {
                if (template == null)
                {
                    return BadRequest("Template object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid ResponseTemplate object");
                }

                var templateEntity = await _repository.ResTemplate.GetResTemplateById(id);
                if (templateEntity == null)
                {
                    return NotFound();
                }

                _mapper.Map(template, templateEntity);

                _repository.ResTemplate.UpdateResTemplate(templateEntity);
                await _repository.Save();

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResTemplate(int id)
        {
            try
            {
                var template = await _repository.ResTemplate.GetResTemplateById(id);
                if (template == null)
                {
                    return NotFound();
                }

                _repository.ResTemplate.DeleteResTemplate(template);
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
