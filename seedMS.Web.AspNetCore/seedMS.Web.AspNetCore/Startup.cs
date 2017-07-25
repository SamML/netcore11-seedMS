using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using seedMS.Core.Data;
using seedMS.Core.Data.Identity;
using seedMS.Core.Data.Repositories;
using seedMS.Core.DomainModels.Identity;
using seedMS.Core.DomainModels.Repositories;
using seedMS.Core.Extensions.Identity;
using seedMS.Core.Extensions.Repositories;
using seedMS.Core.Interfaces.Identity;
using seedMS.Core.Interfaces.Repositories;
using seedMS.Misc.Utils;
using seedMS.Web.AspNetCore.Services;
using System;
using AppPermissions = seedMS.Core.Extensions.Repositories.ApplicationPermissions;

namespace seedMS.Web.AspNetCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // ADD DB CONTEXTS
            services.AddDbContext<CoreIdentityDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("CoreIdentityDbContextConnection")));

            services.AddIdentity<CoreIdentityUser, CoreIdentityRole>()
                .AddEntityFrameworkStores<CoreIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddDbContext<CoreRepositoriesDbContext>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("CoreIdentityDbContextConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<CoreRepositoriesDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // User settings
                options.User.RequireUniqueEmail = true;

                //    //// Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                //    //// Lockout settings
                //    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                //    //options.Lockout.MaxFailedAccessAttempts = 10;

                //options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                //options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                //options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            });

            //ADD MVC
            services.AddMvc();
            // Add CookieTempDataProvider after AddMvc and include ViewFeatures.
            // using Microsoft.AspNetCore.Mvc.ViewFeatures;
            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();

            //ADD AUTHORIZATION OPTIONS
            services.AddAuthorization(options =>
            {
                //Set default authorization
                options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireRole("administrator").Build();

                options.AddPolicy(AuthPolicies.ViewUserByUserIdPolicy, policy => policy.Requirements.Add(new ViewUserByIdRequirement()));

                options.AddPolicy(AuthPolicies.ViewUsersPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ViewUsers));

                options.AddPolicy(AuthPolicies.ManageUserByUserIdPolicy, policy => policy.Requirements.Add(new ManageUserByIdRequirement()));

                options.AddPolicy(AuthPolicies.ManageUsersPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageUsers));

                options.AddPolicy(AuthPolicies.ViewRoleByRoleNamePolicy, policy => policy.Requirements.Add(new ViewRoleByNameRequirement()));

                options.AddPolicy(AuthPolicies.ViewRolesPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ViewRoles));

                options.AddPolicy(AuthPolicies.AssignRolesPolicy, policy => policy.Requirements.Add(new AssignRolesRequirement()));

                options.AddPolicy(AuthPolicies.ManageRolesPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageRoles));

                //options.AddPolicy("Users", policy => policy.RequireAuthenticatedUser().RequireRole("user"));

                //options.AddPolicy("AddEditUser", policy => {
                //    policy.RequireClaim("Add User", "Add User");
                //    policy.RequireClaim("Edit User", "Edit User");
                //});
                //options.AddPolicy("DeleteUser", policy => policy.RequireClaim("Delete User", "Delete User"));
            });

            //set to apply default authorization for empty controllers and [Authorize] declarations
            services.TryAddEnumerable(ServiceDescriptor.Transient<IApplicationModelProvider, OverridableDefaultAuthorizationApplicationModelProvider>());

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            //ADD Account Manager Services
            services.AddScoped<ICoreAccountManager, CoreAccountManager>();
            services.AddScoped<IRepositoriesAccountManager, RepositoriesAccountManager>();
            // DB Creation and Seeding
            services.AddTransient<IDatabaseInitializer, CoreIdentityDbInitializer>();
            services.AddTransient<IDatabaseInitializer, CoreRepositoriesDbInitializer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IDatabaseInitializer databaseInitializer)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //Configure custom Logger
            Utils.ConfigureLogger(loggerFactory);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areaRoute",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });
            });

            try
            {
                databaseInitializer.SeedAsync().Wait();
            }
            catch (Exception ex)
            {
                Utils.CreateLogger<Startup>().LogCritical(LoggingEvents.INIT_DATABASE, ex, LoggingEvents.INIT_DATABASE.Name);
                throw;
            }
        }
    }
}