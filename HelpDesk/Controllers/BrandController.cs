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

            var BrandList = new List<BrandDto>();

            try
            {
                if (userRole == "Manager")
                {
                    var brands = await _repository.Brand.GetBrandsByCondition(userType, userCompanyId);
                    foreach (var brand in brands)
                    {
                        var TempBrandDto = _mapper.Map<BrandDto>(brand);
                        if (brand.CompanyId != null)
                        {
                            var company = await _repository.Company.GetCompanyById(new Guid(brand.CompanyId));
                            if (company != null)
                            {
                                TempBrandDto.CompanyName = company.CompanyName;
                                BrandList.Add(TempBrandDto);
                            }

                        }


                    }
                    return Ok(BrandList);
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
            var BrandList = new List<BrandDto>();
            try
            {
                var brands = await _repository.Brand.GetBrandsByCompanyId(id);

                if (brands == null)
                {
                    return StatusCode(404, "Not Found");
                }
                else
                {
                    foreach (var brand in brands)
                    {
                        var TempBrandDto = _mapper.Map<BrandDto>(brand);
                        if (brand.CompanyId != null)
                        {
                            var company = await _repository.Company.GetCompanyById(new Guid(brand.CompanyId));
                            if (company != null)
                            {
                                TempBrandDto.CompanyName = company.CompanyName;
                                BrandList.Add(TempBrandDto);
                            }

                        }

                    }
                    return Ok(BrandList);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet]
        [Route("[controller]/{brandId}/{companyId}", Name = "BrandById")]

        public async Task<IActionResult> GetBrandById(String brandId , String companyId)
        {
            try
            {
                var brand = await _repository.Brand.GetBrandById(brandId, companyId);

                if (brand == null)
                {
                    return StatusCode(404, "Not Found");
                }
                else
                {
                    var resBrand = _mapper.Map<BrandDto>(brand);
                    if (brand.CompanyId != null)
                    {
                        var company = await _repository.Company.GetCompanyById(new Guid(brand.CompanyId));
                        resBrand.CompanyName = company.CompanyName;
                    }
                    return Ok(resBrand);
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

                if (createdBrand.CompanyName == null)
                {
                    var company = await _repository.Company.GetCompanyById(new Guid(createdBrand.CompanyId));
                    createdBrand.CompanyName = company.CompanyName;
                }

                return CreatedAtRoute("CategoryById", new { id = brandEntity.BrandId }, createdBrand);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpDelete]
        [Route("[controller]/{productId}/{companyId}")]
        public async Task<IActionResult> DeleteBrand(string productId , string companyId)
        {
            try
            {
                var brand = await _repository.Brand.GetBrandById(productId , companyId);
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

        [HttpPatch]
        public async Task<IActionResult> UpdateBrand([FromBody] BrandDto brand)
        {
            if (brand != null)
            {
                var _brand = await _repository.Brand.GetBrandById(brand.BrandId, brand.CompanyId);
                if (_brand != null)
                {
                    var __brand = _mapper.Map<CompanyBrandModel>(brand);
                    _brand.BrandName = __brand.BrandName;
                    _repository.Brand.UpdateBrand(_brand);
                    await _repository.Save();

                    var updatedBrand = _mapper.Map<BrandDto>(__brand);

                    var company = await _repository.Company.GetCompanyById(new Guid(updatedBrand.CompanyId));

                    if (company != null)
                    {
                        updatedBrand.CompanyName = company.CompanyName;

                    }

                    return Ok(updatedBrand);

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
