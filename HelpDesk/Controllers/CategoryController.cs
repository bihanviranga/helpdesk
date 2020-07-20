﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HelpDesk.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        public CategoryController(IRepositoryWrapper repository, IMapper mapper)
        {
            this._mapper = mapper;
            this._repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCaregories()
        {
            var userType = User.Claims.FirstOrDefault(x => x.Type.Equals("UserType", StringComparison.InvariantCultureIgnoreCase)).Value;
            var userRole = User.Claims.FirstOrDefault(x => x.Type.Equals("UserRole", StringComparison.InvariantCultureIgnoreCase)).Value;
            var userCompanyId = User.Claims.FirstOrDefault(x => x.Type.Equals("CompanyId", StringComparison.InvariantCultureIgnoreCase)).Value;

            try
            {
                if (userRole == "Manager") {
                   
                    return Ok(await _repository.Category.GetCategoriesByCondition(userType , userCompanyId));
                }
                else if(userRole == "Client")
                {
                    return StatusCode(401, "401 Unauthorized  Access");
                }
                else
                {
                    return StatusCode(500, "Something went wrong");
                }
                    
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet]
        [Route("[controller]/company/{id}")]
        public async Task<IActionResult> GetCategoriesByCompanyId(String id)
        {
            try
            {
                var categories = await _repository.Category.GetCategoriesByCompanyId(id);

                if (categories == null)
                {
                    return StatusCode(404, "Not Found");
                }
                else
                {
                    //mappers not use -> ** should dev in future
                    //var productResult = _mapper.Map<ProductDto>(product);
                    return Ok(categories);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet]
        [Route("[controller]/{id}")]
        public async Task<IActionResult> GetCategoryById(String id)
        {
            try
            {
                var category = await _repository.Category.GetCategoryById(id);
                if (category == null)
                {
                    return NotFound();
                }
                else
                {
                    //mappers not use -> ** should dev in future
                    //var productResult = _mapper.Map<ProductDto>(product);
                    return Ok(category);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCAtegory([FromBody]CategoryModel category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest("Company object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid company object");
                }

                // convert incoming CompanyCreateDto to actual CompanyModel instance
                // var companyEntity = _mapper.Map<CompanyModel>(company);

                _repository.Category.CreateCategory(category);
                await _repository.Save();

                // convert the model back to a DTO for output
                //var createdCompany = _mapper.Map<CompanyDto>(companyEntity);

                return Json("category has been created");
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpDelete]
        [Route("[controller]/{id}")]
        public async Task<IActionResult> DeleteCategory(String id)
        {
            var category = await _repository.Category.GetCategoryById(id);
            if (category == null)
            {
                return StatusCode(500, "User Not Found");
            }
            else
            {
                _repository.Category.DeleteCategory(category);
                await _repository.Save();
                return Json("category successfully removed");
            }
        }
    }
}
