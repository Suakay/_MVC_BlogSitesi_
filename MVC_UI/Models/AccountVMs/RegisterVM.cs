using System.ComponentModel.DataAnnotations;

namespace MVC_UI.Models.AccountVMs
{
    public class RegisterVM
    {
        public string FirstName {  get; set; }  
        public string LastName { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid e-mail address")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}
