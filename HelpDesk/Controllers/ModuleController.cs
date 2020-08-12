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
        [Route("[controller]/{moduleId}/{companyId}")]
        public async Task<IActionResult> GetModuleById(String moduleId , String companyId)
        {
            try
            {
                var module = await _repository.Module.GetModuleById(moduleId, companyId);

                if (module == null)
                {
                    return StatusCode(404, "Not Found");
                }
                else
                {
                    var resModule = _mapper.Map<ModuleDto>(module);
                    if (module.CompanyId != null)
                    {
                        var company = await _repository.Company.GetCompanyById(new Guid(module.CompanyId));
                        resModule.CompanyName = company.CompanyName;
                    }
                    return Ok(resModule);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }


        [HttpGet]
        
        public async Task<IActionResult> GetAllModules()
        {
            var userType = User.Claims.FirstOrDefault(x => x.Type.Equals("UserType", StringComparison.InvariantCultureIgnoreCase)).Value;
            var userRole = User.Claims.FirstOrDefault(x => x.Type.Equals("UserRole", StringComparison.InvariantCultureIgnoreCase)).Value;
            var userCompanyId = User.Claims.FirstOrDefault(x => x.Type.Equals("CompanyId", StringComparison.InvariantCultureIgnoreCase)).Value;

            var ModuleList = new List<ModuleDto>();

            try
            {
                if (userRole == "Manager")
                {
                    var modules = await _repository.Module.GetModuleByCondition(userType, userCompanyId);
                    foreach (var module in modules)
                    {
                        var TempModuleDto = _mapper.Map<ModuleDto>(module);
                        if (module.CompanyId != null)
                        {
                            var company = await _repository.Company.GetCompanyById(new Guid(module.CompanyId));
                            if (company != null)
                            {
                                TempModuleDto.CompanyName = company.CompanyName;
                                ModuleList.Add(TempModuleDto);
                            }

                        }


                    }
                    return Ok(ModuleList);
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
            var ModuleList = new List<ModuleDto>();
            try
            {
                var modules = await _repository.Module.GetModulesByCompanyId(id);

                if (modules == null)
                {
                    return StatusCode(404, "Not Found");
                }
                else
                {
                    foreach (var module in modules)
                    {
                        var TempModuleDto = _mapper.Map<ModuleDto>(module);
                        if (module.CompanyId != null)
                        {
                            var company = await _repository.Company.GetCompanyById(new Guid(module.CompanyId));
                            if (company != null)
                            {
                                TempModuleDto.CompanyName = company.CompanyName;
                                ModuleList.Add(TempModuleDto);
                            }

                        }

                    }
                    return Ok(ModuleList);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }



        [HttpDelete]
        [Route("[controller]/{moduleId}/{companyId}")]
        public async Task<IActionResult> DeleteModule(String moduleId , String companyId)
        {
            var module = await _repository.Module.GetModuleById(moduleId, companyId);
            if (module == null)
            {
                return StatusCode(500, "User Not Found");
            }
            else
            {
                _repository.Module.DeleteModule(module);
                await _repository.Save();
                var deletedModule = _mapper.Map<ModuleDto>(module);
                return Ok(deletedModule);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateModule([FromBody] ModuleDto module)
        {
            if (module != null)
            {
                var _module = await _repository.Module.GetModuleById(module.ModuleId, module.CompanyId);
                if (_module != null)
                {
                    var __module = _mapper.Map<ModuleModel>(module);
                    _module.ModuleName = __module.ModuleName;
                    _repository.Module.UpdateModule(_module);
                    await _repository.Save();

                    var updatedModule = _mapper.Map<ModuleDto>(__module);

                    var company = await _repository.Company.GetCompanyById(new Guid(updatedModule.CompanyId));

                    if (company != null)
                    {
                        updatedModule.CompanyName = company.CompanyName;

                    }

                    return Ok(updatedModule);

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
