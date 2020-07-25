using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.DataTransferObjects;
using HelpDesk.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Controllers
{
    [Route("brand")]
    [ApiController]
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
        public async Task<IActionResult> GetAllBrands()
        {
            try
            {
                var brands = await _repository.Brand.GetAllBrands();
                var brandsResult = _mapper.Map<BrandDto>(brands);
                return Ok(brandsResult);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }

        }


        [HttpGet("{id}", Name = "BrandById")]
        public async Task<IActionResult> GetBrandById(string id)
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
                    var brandResult = _mapper.Map<BrandDto>(brand);
                    return Ok(brandResult);
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
                    return BadRequest("Brand object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid brand object");
                }

                // incoming dto --> model
                var brandEntity = _mapper.Map<CompanyBrandModel>(brand);

                _repository.Brand.CreateBrand(brandEntity);
                await _repository.Save();

                // convert the model back to a DTO for output
                var createdBrand = _mapper.Map<BrandDto>(brandEntity);

                return CreatedAtRoute("BrandById", new { id = brandEntity.BrandId }, createdBrand);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand(string id, [FromBody] BrandCreateDto brand)
        {
            try
            {
                if (brand == null)
                {
                    return BadRequest("Brand object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid brand object");
                }

                var brandEntity = await _repository.Brand.GetBrandById(id);
                if (brandEntity == null)
                {
                    return NotFound();
                }

                // Map changed fields from CompanyUpdateDto to CompanyModel instance
                _mapper.Map(brand, brandEntity);

                // Update and save the changed entity
                _repository.Brand.UpdateBrand(brandEntity);
                await _repository.Save();

                return NoContent();
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
