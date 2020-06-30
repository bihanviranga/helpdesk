using System;

namespace HelpDesk.Entities.DataTransferObjects.ResTemplate
{
    public class ResTemplateCreateDto
    {
        public string TemplateName { get; set; }
        public string TemplateDescription { get; set; }
        public string TemplateContent { get; set; }
        public string TemplateAddedBy { get; set; }
        public DateTime TemplateAddedDate { get; set; }
    }
}
