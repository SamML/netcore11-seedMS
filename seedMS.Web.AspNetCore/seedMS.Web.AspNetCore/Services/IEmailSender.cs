using System.Threading.Tasks;

namespace seedMS.Web.AspNetCore.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}