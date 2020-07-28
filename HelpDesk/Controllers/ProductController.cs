using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HelpDesk.Entities.Contracts;
using System.Security.Claims;
using HelpDesk.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HelpDesk.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        public ProductController(IRepositoryWrapper repository , IMapper mapper)
        {
            this._mapper = mapper;
            this._repository = repository;
        }
        [HttpGet]
        
        public async Task<IActionResult> GetProducts()
        {
            var userType = User.Claims.FirstOrDefault(x => x.Type.Equals("UserType", StringComparison.InvariantCultureIgnoreCase)).Value; 
            var userRole = User.Claims.FirstOrDefault(x => x.Type.Equals("UserRole", StringComparison.InvariantCultureIgnoreCase)).Value;
            var userCompanyId = User.Claims.FirstOrDefault(x => x.Type.Equals("CompanyId", StringComparison.InvariantCultureIgnoreCase)).Value;

            try
            {
                if (userRole == "Manager")
                {

                    return Ok(await _repository.Product.GetProductsByCondition(userType, userCompanyId));
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
        [Route("[controller]/{id}")]
        public async Task<IActionResult> GetProductById(String id)
        {
            try
            {
                var product = await _repository.Product.GetProductById(id);
                
                if (product == null)
                {
                    return StatusCode(404, "Not Found");
                }
                else
                {
                    //mappers not use -> ** should dev in future
                    //var productResult = _mapper.Map<ProductDto>(product);
                    return Ok(product);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet]
        [Route("[controller]/Company/{id}")]
        public async Task<IActionResult> GetProductsByCompanyId(String id)
        {
            try
            {
                var products = await _repository.Product.GetProductsByCompanyId(id);
                
                if (products == null)
                {
                    return StatusCode(404, "Not Found");
                }
                else
                {
                    //mappers not use -> ** should dev in future
                    //var productResult = _mapper.Map<ProductDto>(product);
                    return Ok(products);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ProductCompany([FromBody]ProductModel product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("Company object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid company object");
                }

                // convert incoming CompanyCreateDto to actual CompanyModel instance
               // var companyEntity = _mapper.Map<CompanyModel>(company);

                _repository.Product.CreateProduct(product);
                await _repository.Save();

                // convert the model back to a DTO for output
                //var createdCompany = _mapper.Map<CompanyDto>(companyEntity);

                return Json("product has been created");
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpDelete]
        [Route("[controller]/{id}")]
        public async Task<IActionResult> DeleteProduct(String id)
        {
            var product = await _repository.Product.GetProductById(id);
            if (product == null)
            {
                return StatusCode(500, "User Not Found");
            }
            else
            {
                _repository.Product.DeleteProduct(product);
                await _repository.Save();
                return Json("product Successfully removed");
            }
        }
    }
}
