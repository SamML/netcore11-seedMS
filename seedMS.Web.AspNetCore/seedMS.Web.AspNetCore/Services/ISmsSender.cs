using System.Threading.Tasks;

namespace seedMS.Web.AspNetCore.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}