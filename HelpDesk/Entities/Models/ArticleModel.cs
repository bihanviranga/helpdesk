using System;
using System.Collections.Generic;

namespace HelpDesk.Entities.Models
{
    public partial class ArticleModel
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
        public DateTime? LastEditedBy { get; set; }
    }
}
