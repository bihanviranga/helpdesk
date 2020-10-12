using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.DataTransferObjects.Article;
using HelpDesk.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HelpDesk.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ArticleController(IRepositoryWrapper repository, IMapper mapper , IWebHostEnvironment hostEnvironment )
        {
            this._mapper = mapper;
            this._hostEnvironment = hostEnvironment;
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

                var p = (await _repository.Product.GetProductById(article.ProductId, article.CompanyId));
                if (p != null) _article.ProductName = p.ProductName;

                try{ 
                    var c = (await _repository.Company.GetCompanyById( new Guid(article.CompanyId)));
                    if (c != null) _article.CompanyName = c.CompanyName;
                }
                catch
                {
                    _article.CompanyName = null;
                }
                

                var m = (await _repository.Module.GetModuleById(article.ModuleId, article.CompanyId));
                if (m != null) _article.ModuleName = m.ModuleName;

                var categotyname = (await _repository.Category.GetCategoryById(article.CategoryId, article.CompanyId));
                if (categotyname != null) _article.CategoryName = categotyname.CategoryName;

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
        [Authorize]
        [Route("[controller]/{articleId}")]
        public async Task<IActionResult> DeleteArticle(string articleId)
        {

            var userType = User.Claims.FirstOrDefault(x => x.Type.Equals("UserType", StringComparison.InvariantCultureIgnoreCase)).Value;
            var userRole = User.Claims.FirstOrDefault(x => x.Type.Equals("UserRole", StringComparison.InvariantCultureIgnoreCase)).Value;

            if(userRole == "Manager" && userType == "HelpDesk")
            {
                var article = await _repository.Article.GetArticleById(articleId);
                if (article != null)
                {
                    _repository.Article.Delete(article);
                    await _repository.Save();
                    return Ok(article.ArticleId);
                }
                else
                {
                    return StatusCode(404, "Article not found");
                }

            }
            else
            {
                return StatusCode(401, "You dont have a permission for this action");
            }
            
        }
    }
}
