using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace seedMS.Core.DomainModels.Identity
{
    public class CoreIdentityUser : IdentityUser
    {
        public string FullName { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsLockedOut => this.LockoutEnabled && this.LockoutEnd >= DateTimeOffset.UtcNow;
    }
}