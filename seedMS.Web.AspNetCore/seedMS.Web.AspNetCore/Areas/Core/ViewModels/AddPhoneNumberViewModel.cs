using System.ComponentModel.DataAnnotations;

namespace seedMS.Web.AspNetCore.Areas.Core.ViewModels
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}