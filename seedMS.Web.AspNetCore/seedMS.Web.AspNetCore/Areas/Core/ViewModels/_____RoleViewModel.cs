﻿// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
//
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

//using Quick_Application1.Helpers;
using System.ComponentModel.DataAnnotations;

namespace seedMS.Web.AspNetCore.Areas.Core.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Role name is required"), StringLength(200, MinimumLength = 2, ErrorMessage = "Role name must be between 2 and 200 characters")]
        public string Name { get; set; }

        public string Description { get; set; }

        public int UsersCount { get; set; }

        public PermissionViewModel[] Permissions { get; set; }
    }
}