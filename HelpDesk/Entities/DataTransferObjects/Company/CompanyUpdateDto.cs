using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Entities.DataTransferObjects
{
    public class CompanyUpdateDto
    {
        // This class is the same as CompanyCreateDto. I created a seperate one for clarity of code.

        // This error message will be displayed after doing ModelState.IsValid in the controller
        [Required(ErrorMessage = "Company name is required")]
        public string CompanyName { get; set; }
    }
}
