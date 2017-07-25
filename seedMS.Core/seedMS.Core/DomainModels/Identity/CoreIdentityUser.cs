// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
//
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using seedMS.Core.DomainModels.Repositories;
using System;
using System.Collections.Generic;

namespace seedMS.Core.DomainModels.Identity
{
    public class CoreIdentityUser : IdentityUser
    {
        
        public bool IsEnabled { get; set; }
        public bool IsLockedOut => this.LockoutEnabled && this.LockoutEnd >= DateTimeOffset.UtcNow;

        
    }
}