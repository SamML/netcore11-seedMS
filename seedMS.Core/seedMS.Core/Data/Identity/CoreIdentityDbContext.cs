using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using seedMS.Core.DomainModels.Identity;

namespace seedMS.Core.Data.Identity
{
    public class CoreIdentityDbContext : IdentityDbContext<CoreIdentityUser, CoreIdentityRole, string>
    {
        public CoreIdentityDbContext(DbContextOptions<CoreIdentityDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}