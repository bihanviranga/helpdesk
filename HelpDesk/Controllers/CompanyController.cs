using System;
using System.Collections.Generic;
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
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public CompanyController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllCompanies()
        {
            try
            {
                // Get the results from repo, and convert (map) them to a DTO.
                var companies = _repository.Company.GetAllCompanies();
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
        public IActionResult GetCompanyById(Guid id)
        {
            try
            {
                var company = _repository.Company.GetCompanyById(id);
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
        public IActionResult GetCompanyDetailsById(Guid id)
        {
            try
            {
                var company = _repository.Company.GetCompanyById(id);
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
        public IActionResult CreateCompany([FromBody] CompanyCreateDto company)
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
                _repository.Save();

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
        public IActionResult UpdateCompany(Guid id, [FromBody] CompanyUpdateDto company)
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

                var companyEntity = _repository.Company.GetCompanyById(id);
                if (companyEntity == null)
                {
                    return NotFound();
                }

                // Map changed fields from CompanyUpdateDto to CompanyModel instance
                _mapper.Map(company, companyEntity);

                // Update and save the changed entity
                _repository.Company.UpdateCompany(companyEntity);
                _repository.Save();

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCompany(Guid id)
        {
            try
            {
                var company = _repository.Company.GetCompanyById(id);
                if (company == null)
                {
                    return NotFound();
                }

                _repository.Company.DeleteCompany(company);
                _repository.Save();

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
