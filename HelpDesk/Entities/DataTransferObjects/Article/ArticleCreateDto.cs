using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Entities.DataTransferObjects.Article
{
    public class ArticleCreateDto
    {
        public string ArticleTitle { get; set; }
        public string ArticleContent { get; set; }
        public string ArticleAttachment { get; set; }
        public string CompanyId { get; set; }
        public string ProductId { get; set; }
        public string CategoryId { get; set; }
        public string BrandId { get; set; }
        public string ModuleId { get; set; }

    }
}
