using System.Threading.Tasks;

namespace seedMS.Core.Data
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync();
    }
}