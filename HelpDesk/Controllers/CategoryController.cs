using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.DataTransferObjects;
using HelpDesk.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HelpDesk.Controllers
{
    [Authorize]
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
        
        public async Task<IActionResult> GetCategories()
        {
            var userType = User.Claims.FirstOrDefault(x => x.Type.Equals("UserType", StringComparison.InvariantCultureIgnoreCase)).Value;
            var userRole = User.Claims.FirstOrDefault(x => x.Type.Equals("UserRole", StringComparison.InvariantCultureIgnoreCase)).Value;
            var userCompanyId = User.Claims.FirstOrDefault(x => x.Type.Equals("CompanyId", StringComparison.InvariantCultureIgnoreCase)).Value;

            var CategoryList = new List<CategoryDto>();

            try
            {
                if (userRole == "Manager")
                {
                    var categories = await _repository.Category.GetCategoriesByCondition(userType, userCompanyId);
                    foreach (var category in categories)
                    {
                        var TempCategoryDto = _mapper.Map<CategoryDto>(category);
                        if (category.CompanyId != null)
                        {
                            var company = await _repository.Company.GetCompanyById(new Guid(category.CompanyId));
                            if (company != null)
                            {
                                TempCategoryDto.CompanyName = company.CompanyName;
                                CategoryList.Add(TempCategoryDto);
                            }

                        }


                    }
                    return Ok(CategoryList);
                }
                else if (userRole == "Client")
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
            var CategoryList = new List<CategoryDto>();
            try
            {
                var categories = await _repository.Category.GetCategoriesByCompanyId(id);

                if (categories == null)
                {
                    return StatusCode(404, "Not Found");
                }
                else
                {
                    foreach (var category in categories)
                    {
                        var TempCategoryDto = _mapper.Map<CategoryDto>(category);
                        if (category.CompanyId != null)
                        {
                            var company = await _repository.Company.GetCompanyById(new Guid(category.CompanyId));
                            if (company != null)
                            {
                                TempCategoryDto.CompanyName = company.CompanyName;
                                CategoryList.Add(TempCategoryDto);
                            }

                        }

                    }
                    return Ok(CategoryList);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet]
        [Route("[controller]/{categoryId}/{companyId}")]
        
        public async Task<IActionResult> GetCategoryById(String categoryId, String companyId)
        {
            try
            {
                var category = await _repository.Category.GetCategoryById(categoryId, companyId);

                if (category == null)
                {
                    return StatusCode(404, "Not Found");
                }
                else
                {
                    var resCategory = _mapper.Map<CategoryDto>(category);
                    if (category.CompanyId != null)
                    {
                        var company = await _repository.Company.GetCompanyById(new Guid(category.CompanyId));
                        resCategory.CompanyName = company.CompanyName;
                    }
                    return Ok(resCategory);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPost]
        
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto category)
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

                // convert incoming Dto to actual Model instance
                var categoryEntity = _mapper.Map<CategoryModel>(category);

                _repository.Category.CreateCategory(categoryEntity);
                await _repository.Save();

                // convert the model back to a DTO for output
                var createdCategory = _mapper.Map<CategoryDto>(categoryEntity);

                if (createdCategory.CompanyName == null)
                {
                    var company = await _repository.Company.GetCompanyById(new Guid(createdCategory.CompanyId));
                    createdCategory.CompanyName = company.CompanyName;
                }

                return Ok(createdCategory);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpDelete]
        [Route("[controller]/{categoryId}/{companyId}")]
        
        public async Task<IActionResult> DeleteCategory(String categoryId  , String companyId)
        {
            var category= await _repository.Category.GetCategoryById(categoryId, companyId);
            if (category == null)
            {
                return StatusCode(500, "User Not Found");
            }
            else
            {
                _repository.Category.DeleteCategory(category);
                await _repository.Save();
                var deletedCategory = _mapper.Map<CategoryDto>(category);
                return Ok(deletedCategory);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryDto category)
        {
            if (category != null)
            {
                var _category = await _repository.Category.GetCategoryById(category.CategoryId, category.CompanyId);
                if (_category != null)
                {
                    var __category = _mapper.Map<CategoryModel>(category);
                    _category.CategoryName = __category.CategoryName;
                    _repository.Category.UpdateCategory(_category);
                    await _repository.Save();

                    var updatedCategory = _mapper.Map<CategoryDto>(__category);

                    var company = await _repository.Company.GetCompanyById(new Guid(updatedCategory.CompanyId));

                    if (company != null)
                    {
                        updatedCategory.CompanyName = company.CompanyName;

                    }

                    return Ok(updatedCategory);

                }
                else
                {
                    return StatusCode(500, "Something went wrong");
                }
            }
            else
            {
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
