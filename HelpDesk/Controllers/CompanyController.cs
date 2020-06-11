using System;
using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Controllers
{
    [Route("company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private IRepositoryWrapper _repository;

        public CompanyController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllCompanies()
        {
            try
            {
                var companies = _repository.Company.GetAllCompanies();
                return Ok(companies);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        /*
         * The NAME parameter in the decorator is needed for CreateCompany method's result.
         */
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
                    return Ok(company);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody] CompanyModel company)
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

                _repository.Company.CreateCompany(company);
                _repository.Save();

                return CreatedAtRoute("CompanyById", new { id = company.CompanyId }, company);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCompany(Guid id, [FromBody] CompanyModel company)
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

                _repository.Company.UpdateCompany(company);
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
