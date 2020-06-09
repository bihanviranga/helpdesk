using System;
using System.Collections.Generic;

namespace HelpDesk.Model
{
    public partial class TktResTemplate
    {
        public string TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string TemplateDescription { get; set; }
        public string TemplateContent { get; set; }
        public string TemplateAddedBy { get; set; }
        public DateTime? TemplateAddedDate { get; set; }
    }
}
