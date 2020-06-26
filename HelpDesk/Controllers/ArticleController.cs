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
    public class ArticleController : Controller
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        public ArticleController(IRepositoryWrapper repository, IMapper mapper)
        {
            this._mapper = mapper;
            this._repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle([FromBody]ArticleModel article)
        {
            if(article != null)
            {
                 _repository.Article.Create(article);
                await _repository.Save();
                return Ok(Json("Done"));
            }
            else
            {
                return Json("Something Went worng");
            }
           
        }

        [HttpGet]
        [Route("[controller]/{articleId}")]
        public async Task<IActionResult> GetArticleById(String articleId)
        {
            try
            {
                var article = await _repository.Article.GetArticleById(articleId);
                return Ok(article);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went worng !!");
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllArticles()
        {
            try
            {
                var articles = await _repository.Article.GetAllArticles();
                return Ok(articles);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went worng !!");
            }
        }

      

        [HttpDelete]
        [Route("[controller]/{articleId}")]
        public async Task<IActionResult> DeleteArticle(string articleId)
        {
            var article = await _repository.Article.GetArticleById(articleId);
            if (article != null)
            {
                _repository.Article.Delete(article);
                await _repository.Save();
                return Ok(Json("user has been deleted"));
            }
            else
            {
                return StatusCode(500, "Something went worng !!");
            }
        }
    }
}
