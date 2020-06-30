using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Entities.DataTransferObjects
{
    public class CompanyCreateDto
    {
        // This error message will be displayed after doing ModelState.IsValid in the controller
        [Required(ErrorMessage = "Company name is required")]
        public string CompanyName { get; set; }
    }
}
