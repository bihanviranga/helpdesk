using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Entities.DataTransferObjects.Article
{
    public class ArticleDto
    {
        public string ArticleId { get; set; }
        public string ProductId { get; set; }
        public string CreatedBy { get; set; }
        public string AcceptedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? AcceptedDate { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleContent { get; set; }
        public DateTime? LastEditedDate { get; set; }
        public string LastEditedBy { get; set; }
        public string ArticleAttachment { get; set; }
    }
}
