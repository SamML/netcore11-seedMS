using System.ComponentModel.DataAnnotations;

namespace seedMS.Web.AspNetCore.Areas.Core.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}