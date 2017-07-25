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
        public virtual string FriendlyName
        {
            get
            {
                string friendlyName = string.IsNullOrWhiteSpace(FullName) ? UserName : FullName;

                if (!string.IsNullOrWhiteSpace(JobTitle))
                    friendlyName = JobTitle + " " + friendlyName;

                return friendlyName;
            }
        }

        public string JobTitle { get; set; }
        public string FullName { get; set; }
        public string Configuration { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsLockedOut => this.LockoutEnabled && this.LockoutEnd >= DateTimeOffset.UtcNow;

        public ICollection<Order> Orders { get; set; }
    }
}