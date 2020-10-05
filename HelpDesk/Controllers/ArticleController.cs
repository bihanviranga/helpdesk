using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.DataTransferObjects.Article;
using HelpDesk.Entities.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<IActionResult> CreateArticle([FromBody]ArticleCreateDto article)
        {
                var userType = User.Claims.FirstOrDefault(x => x.Type.Equals("UserType", StringComparison.InvariantCultureIgnoreCase)).Value;
                var userRole = User.Claims.FirstOrDefault(x => x.Type.Equals("UserRole", StringComparison.InvariantCultureIgnoreCase)).Value;
                var userCompanyId = User.Claims.FirstOrDefault(x => x.Type.Equals("CompanyId", StringComparison.InvariantCultureIgnoreCase)).Value;
                var userName = User.Claims.FirstOrDefault(x => x.Type.Equals("UserName", StringComparison.InvariantCultureIgnoreCase)).Value;

            if (article != null)
            {

                if (userType == "HelpDesk" && userRole == "Manager")
                {
                    var _article = _mapper.Map<ArticleModel>(article);

                    var user = await _repository.User.GetUserByUserName(userName);
                    if (user != null)
                    {
                        _article.CreatedBy = user.UserName;
                        _article.ArticleId = (Guid.NewGuid()).ToString();
                        _article.CreatedDate = DateTime.Now;
                    }

                    _repository.Article.Create(_article);

                    await _repository.Save();

                    var insertArticle = await _repository.Article.GetArticleById(_article.ArticleId);
                    
                    

                    return Ok( _mapper.Map<ArticleDto>(insertArticle) );
                }
                else
                {
                    return StatusCode(401, "Unauthorized Access");
                }
            }
            else
            {
                return StatusCode(400 , "Bad Request");
            }

           
        }

        [HttpGet]
        [Route("[controller]/{articleId}")]
        public async Task<IActionResult> GetArticleById(String articleId)
        {
            try
            {
                var article = await _repository.Article.GetArticleById(articleId);
                var _article = _mapper.Map<ArticleDto>(article);
                return Ok(_article);
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
