using System.ComponentModel.DataAnnotations;

namespace seedMS.Web.AspNetCore.Areas.Core.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}