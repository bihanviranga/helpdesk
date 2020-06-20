using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Entities.Repository
{
    public class ArticleRepository : RepositoryBase<ArticleModel>, IArticleRepository
    {
        public ArticleRepository(HelpDeskContext helpDeskContext) : base(helpDeskContext) { }
        public void CreateArticle(ArticleModel article)
        {
            Create(article);
        }

        public void DeleteArticle(ArticleModel article)
        {
            Delete(article);
        }

        public async Task<IEnumerable<ArticleModel>> GetAllArticles()
        {
            return await FindAll().OrderBy(cmp => cmp.CreatedDate).ToListAsync();
        }

        public async Task<ArticleModel> GetArticleById(String articleId)
        {
            return await FindByCondition(art => art.ArticleId.Equals(articleId.ToString())).FirstOrDefaultAsync();
        }
    }
}
