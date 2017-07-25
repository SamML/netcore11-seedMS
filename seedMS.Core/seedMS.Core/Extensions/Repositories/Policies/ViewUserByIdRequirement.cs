// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using seedMS.Core.DomainModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace seedMS.Core.Extensions.Repositories
{
    public class ViewUserByIdRequirement : IAuthorizationRequirement
    {

    }


    public class ViewUserByIdHandler : AuthorizationHandler<ViewUserByIdRequirement, string>
    {
        private UserManager<ApplicationUser> usermanager;

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ViewUserByIdRequirement requirement, string targetUserId)
        {
            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ViewUsers) || GetIsSameUser(context.User, targetUserId))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }


        private bool GetIsSameUser(ClaimsPrincipal user, string targetUserId)
        {
            return usermanager.GetUserId(user) == targetUserId;
           
        }
    }
}
