using HelpDesk.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Entities.Contracts
{
    public interface IArticleRepository : IRepositoryBase<ArticleModel>
    {
        Task<IEnumerable<ArticleModel>> GetAllArticles();
        Task<ArticleModel> GetArticleById(String articleId);
        void CreateArticle(ArticleModel article);
        void DeleteArticle(ArticleModel article);
        void UpdateArticle(ArticleModel article);
    }
}
