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

namespace HelpDesk.Controllers
{
    
    [Authorize]
    public class BrandController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public BrandController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<IActionResult> GetBrands()
        {
            var userType = User.Claims.FirstOrDefault(x => x.Type.Equals("UserType", StringComparison.InvariantCultureIgnoreCase)).Value;
            var userRole = User.Claims.FirstOrDefault(x => x.Type.Equals("UserRole", StringComparison.InvariantCultureIgnoreCase)).Value;
            var userCompanyId = User.Claims.FirstOrDefault(x => x.Type.Equals("CompanyId", StringComparison.InvariantCultureIgnoreCase)).Value;

            try
            {
                if (userRole == "Manager")
                {

                    return Ok(await _repository.Brand.GetBrandsByCondition(userType, userCompanyId));
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

        public async Task<IActionResult> GetBrandsByCompanyId(String id)
        {
            try
            {
                var brands = await _repository.Brand.GetBrandsByCompanyId(id);

                if (brands == null)
                {
                    return StatusCode(404, "Not Found");
                }
                else
                {
                    //mappers not use -> ** should dev in future
                    //var productResult = _mapper.Map<ProductDto>(product);
                    return Ok(brands);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet]
        [Route("[controller]/{id}", Name = "BrandById")]

        public async Task<IActionResult> GetBrandById(String id)
        {
            try
            {
                var brand = await _repository.Brand.GetBrandById(id);
                if (brand == null)
                {
                    return NotFound();
                }
                else
                {
                    //mappers not use -> ** should dev in future
                    //var productResult = _mapper.Map<ProductDto>(product);
                    return Ok(brand);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPost]

        public async Task<IActionResult> CreateBrand([FromBody] BrandCreateDto brand)
        {
            try
            {
                if (brand == null)
                {
                    return BadRequest("Company object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid company object");
                }

                // convert incoming Dto to actual Model instance
                var brandEntity = _mapper.Map<CompanyBrandModel>(brand);

                _repository.Brand.CreateBrand(brandEntity);
                await _repository.Save();

                // convert the model back to a DTO for output
                var createdBrand = _mapper.Map<BrandDto>(brandEntity);

                return CreatedAtRoute("CategoryById", new { id = brandEntity.BrandId }, createdBrand);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(string id)
        {
            try
            {
                var brand = await _repository.Brand.GetBrandById(id);
                if (brand == null)
                {
                    return NotFound();
                }

                _repository.Brand.DeleteBrand(brand);
                await _repository.Save();

                var brandResult = _mapper.Map<BrandDto>(brand);
                return Ok(brandResult);

            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
