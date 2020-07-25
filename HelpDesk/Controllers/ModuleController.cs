using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.DataTransferObjects;
using HelpDesk.Entities.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HelpDesk.Controllers
{
    public class ModuleController : Controller
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        public ModuleController(IRepositoryWrapper repository, IMapper mapper)
        {
            this._mapper = mapper;
            this._repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateModule([FromBody] ModuleCreateDto module)
        {
            try
            {
                if (module == null)
                {
                    return BadRequest("Module object is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid module object");
                }

                // incoming dto --> model
                var moduleEntity = _mapper.Map<ModuleModel>(module);

                _repository.Module.CreateModule(moduleEntity);
                await _repository.Save();

                // created entity --> outgoing dto
                var createdModule = _mapper.Map<ModuleDto>(moduleEntity);

                return CreatedAtRoute("ModuleById", new { id = moduleEntity.ModuleId }, createdModule);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet]
        [Route("[controller]/{id}", Name = "ModuleById")]
        public async Task<IActionResult> GetModuleById(String id)
        {
            try
            {
                var module = await _repository.Module.GetModuleById(id);
                return Ok(module);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went worng !!");
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllModules()
        {
            var userType = User.Claims.FirstOrDefault(x => x.Type.Equals("UserType", StringComparison.InvariantCultureIgnoreCase)).Value;
            var userRole = User.Claims.FirstOrDefault(x => x.Type.Equals("UserRole", StringComparison.InvariantCultureIgnoreCase)).Value;
            var userCompanyId = User.Claims.FirstOrDefault(x => x.Type.Equals("CompanyId", StringComparison.InvariantCultureIgnoreCase)).Value;

            try
            {
                if (userRole == "Manager")
                {

                    return Ok(await _repository.Module.GetModuleByCondition(userType, userCompanyId));
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
        [Route("[controller]/Company/{id}")]
        public async Task<IActionResult> GetModulesByCompanyId(String id)
        {
            try
            {
                var modules = await _repository.Module.GetModulesByCompanyId(id);

                if (modules == null)
                {
                    return StatusCode(404, "Not Found");
                }
                else
                {
                    //mappers not use -> ** should dev in future
                    //var productResult = _mapper.Map<ProductDto>(product);
                    return Ok(modules);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }



        [HttpDelete]
        [Route("[controller]/{id}")]
        public async Task<IActionResult> DeleteModule(String id)
        {
            var module = await _repository.Module.GetModuleById(id);
            if (module != null)
            {
                _repository.Module.Delete(module);
                await _repository.Save();
                return Ok(Json("module has been deleted"));
            }
            else
            {
                return StatusCode(500, "Something went worng !!");
            }
        }
    }
}
