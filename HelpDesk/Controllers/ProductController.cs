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
using HelpDesk.Entities.DataTransferObjects.Product;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HelpDesk.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        public ProductController(IRepositoryWrapper repository, IMapper mapper)
        {
            this._mapper = mapper;
            this._repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto product)
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

                // convert incoming Dto to actual Model instance
                var productEntity = _mapper.Map<ProductModel>(product);

                _repository.Product.CreateProduct(productEntity);
                await _repository.Save();

                // convert the model back to a DTO for output
                var createdProduct = _mapper.Map<ProductDto>(productEntity);
                if (createdProduct.CompanyName == null)
                {
                    var company = await _repository.Company.GetCompanyById(new Guid(createdProduct.CompanyId));
                    createdProduct.CompanyName = company.CompanyName;
                }

                return Ok(createdProduct);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }


        [HttpGet]

        public async Task<IActionResult> GetProducts()
        {
            var userType = User.Claims.FirstOrDefault(x => x.Type.Equals("UserType", StringComparison.InvariantCultureIgnoreCase)).Value;
            var userRole = User.Claims.FirstOrDefault(x => x.Type.Equals("UserRole", StringComparison.InvariantCultureIgnoreCase)).Value;
            var userCompanyId = User.Claims.FirstOrDefault(x => x.Type.Equals("CompanyId", StringComparison.InvariantCultureIgnoreCase)).Value;

            var ProductList = new List<ProductDto>();

            try
            {
                if (userRole == "Manager")
                {
                    var products = await _repository.Product.GetProductsByCondition(userType, userCompanyId);
                    foreach (var product in products)
                    {
                        var TempProductDto = _mapper.Map<ProductDto>(product);
                        if (product.CompanyId != null)
                        {
                            var company = await _repository.Company.GetCompanyById(new Guid(product.CompanyId));
                            if (company != null)
                            {
                                TempProductDto.CompanyName = company.CompanyName;
                                ProductList.Add(TempProductDto);
                            }

                        }


                    }
                    return Ok(ProductList);
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
        [Route("[controller]/{productId}/{companyId}")]
        public async Task<IActionResult> GetProductById(String productId, String companyId)
        {
            try
            {
                var product = await _repository.Product.GetProductById(productId, companyId);

                if (product == null)
                {
                    return StatusCode(404, "Not Found");
                }
                else
                {
                    var resProduct = _mapper.Map<ProductDto>(product);
                    if (product.CompanyId != null)
                    {
                        var company = await _repository.Company.GetCompanyById(new Guid(product.CompanyId));
                        resProduct.CompanyName = company.CompanyName;
                    }
                    return Ok(resProduct);
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
            var ProductList = new List<ProductDto>();
            try
            {
                var products = await _repository.Product.GetProductsByCompanyId(id);

                if (products == null)
                {
                    return StatusCode(404, "Not Found");
                }
                else
                {
                    foreach (var product in products)
                    {
                        var TempProductDto = _mapper.Map<ProductDto>(product);
                        if (product.CompanyId != null)
                        {
                            var company = await _repository.Company.GetCompanyById(new Guid(product.CompanyId));
                            if (company != null)
                            {
                                TempProductDto.CompanyName = company.CompanyName;
                                ProductList.Add(TempProductDto);
                            }

                        }

                    }
                    return Ok(ProductList);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }



        [HttpDelete]
        [Route("[controller]/{productId}/{companyId}")]
        public async Task<IActionResult> DeleteProduct(String productId, String companyId)
        {
            try
            {
                var product = await _repository.Product.GetProductById(productId, companyId);
                if (product == null)
                {
                    return NotFound();
                }

                // if there are tickets, don't delete the product.
                var tickets = await _repository.Ticket.GetTicketsByProduct(product);
                if (tickets.Count() > 0)
                {
                    return BadRequest("This product has tickets. Delete the tickets first.");
                }

                _repository.Product.DeleteProduct(product);
                await _repository.Save();
                var deletedProduct = _mapper.Map<ProductDto>(product);
                return Ok(deletedProduct);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto product)
        {
            if (product != null)
            {
                var _product = await _repository.Product.GetProductById(product.ProductId, product.CompanyId);
                if (_product != null)
                {
                    var __product = _mapper.Map<ProductModel>(product);
                    _product.ProductName = __product.ProductName;
                    _repository.Product.UpdateProduct(_product);
                    await _repository.Save();

                    var updatedProduct = _mapper.Map<ProductDto>(__product);

                    var company = await _repository.Company.GetCompanyById(new Guid(updatedProduct.CompanyId));

                    if (company != null)
                    {
                        updatedProduct.CompanyName = company.CompanyName;

                    }

                    return Ok(updatedProduct);

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
