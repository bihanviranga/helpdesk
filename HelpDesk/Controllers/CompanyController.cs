using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.DataTransferObjects;
using HelpDesk.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Controllers
{
    [Route("company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public CompanyController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            try
            {
                // Get the results from repo, and convert (map) them to a DTO.
                var companies = await _repository.Company.GetAllCompanies();
                var companiesResult = _mapper.Map<IEnumerable<CompanyDetailDto>>(companies);
                return Ok(companiesResult);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }


        // The NAME parameter in the decorator is needed for CreateCompany method's result.
        [HttpGet("{id}", Name = "CompanyById")]
        public async Task<IActionResult> GetCompanyById(Guid id)
        {
            try
            {
                var company = await _repository.Company.GetCompanyById(id);
                if (company == null)
                {
                    return NotFound();
                }
                else
                {
                    var companyResult = _mapper.Map<CompanyDto>(company);
                    return Ok(companyResult);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        // This endpoint is an example to demonstrate DTOs. We can return different data using different DTOs.
        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetCompanyDetailsById(Guid id)
        {
            try
            {
                var company = await _repository.Company.GetCompanyById(id);
                if (company == null)
                {
                    return NotFound();
                }
                else
                {
                    var companyResult = _mapper.Map<CompanyDetailDto>(company);
                    return Ok(companyResult);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyCreateDto company)
        {
            try
            {
                if (company == null)
                {
                    return BadRequest("Company object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid company object");
                }

                // convert incoming CompanyCreateDto to actual CompanyModel instance
                var companyEntity = _mapper.Map<CompanyModel>(company);

                _repository.Company.CreateCompany(companyEntity);
                await _repository.Save();

                // convert the model back to a DTO for output
                var createdCompany = _mapper.Map<CompanyDto>(companyEntity);

                return CreatedAtRoute("CompanyById", new { id = companyEntity.CompanyId }, createdCompany);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] CompanyUpdateDto company)
        {
            try
            {
                if (company == null)
                {
                    return BadRequest("Company object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid company object");
                }

                var companyEntity = await _repository.Company.GetCompanyById(id);
                if (companyEntity == null)
                {
                    return NotFound();
                }

                // Map changed fields from CompanyUpdateDto to CompanyModel instance
                _mapper.Map(company, companyEntity);

                // Update and save the changed entity
                _repository.Company.UpdateCompany(companyEntity);
                await _repository.Save();

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            try
            {
                var company = await _repository.Company.GetCompanyById(id);
                if (company == null)
                {
                    return NotFound();
                }

                _repository.Company.DeleteCompany(company);
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
