using System;
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
        public async Task<IActionResult> CreateModule([FromBody]ModuleModel module)
        {
            if (module != null)
            {
                _repository.Module.Create(module);
                await _repository.Save();
                return Ok(Json("Done"));
            }
            else
            {
                return Json("Something Went worng");
            }

        }

        [HttpGet]
        [Route("[controller]/{id}")]
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
            try
            {
                var modules = await _repository.Module.GetAllModules();
                return Ok(modules);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went worng !!");
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
