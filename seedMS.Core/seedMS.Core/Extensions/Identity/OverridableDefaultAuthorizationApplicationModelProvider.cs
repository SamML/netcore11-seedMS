﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.Linq;

namespace seedMS.Core.Extensions.Identity
{
    public class OverridableDefaultAuthorizationApplicationModelProvider : IApplicationModelProvider
    {
        private readonly AuthorizationOptions _authorizationOptions;

        
       

        public OverridableDefaultAuthorizationApplicationModelProvider(IOptions<AuthorizationOptions> authorizationOptionsAccessor)
        {
            _authorizationOptions = authorizationOptionsAccessor.Value;
            
            
        }

        public int Order
        {
            //It will be executed after AuthorizationApplicationModelProvider, which has order -990
            get { return 0; }
        }

        public void OnProvidersExecuted(ApplicationModelProviderContext context)
        {
            foreach (var controllerModel in context.Result.Controllers)
            {
                if (controllerModel.Filters.OfType<IAsyncAuthorizationFilter>().FirstOrDefault() == null)
                {
                    //default policy only used when there is no authorize filter in the controller
                    controllerModel.Filters.Add(new AuthorizeFilter(_authorizationOptions.DefaultPolicy));
                    
                }
            }
        }

        public void OnProvidersExecuting(ApplicationModelProviderContext context)
        {
            
            //empty
        }
    }
}