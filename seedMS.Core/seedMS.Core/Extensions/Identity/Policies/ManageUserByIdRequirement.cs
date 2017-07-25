// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using seedMS.Core.DomainModels.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace seedMS.Core.Extensions.Identity
{
    public class ManageUserByIdRequirement : IAuthorizationRequirement
    {

    }


    public class ManageUserByIdHandler : AuthorizationHandler<ManageUserByIdRequirement, string>
    {
        private UserManager<ApplicationUser> usermanager;

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ManageUserByIdRequirement requirement, string userId)
        {
            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageUsers) || GetIsSameUser(context.User, userId))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }


        private bool GetIsSameUser(ClaimsPrincipal user, string userId)
        {
            return usermanager.GetUserId(user) == userId;
        }
    }
}
