using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Web.Models;

namespace Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Web.Models.ApplicationDbContext";
        }

        protected override void Seed(Web.Models.ApplicationDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "Retail" });
                roleManager.Create(new IdentityRole { Name = "Wholesale" });
            }
            // Create Admin user
            var user = new ApplicationUser()
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                EmailConfirmed = true
            };
            manager.Create(user, "Admin-1234");
            var admin = manager.FindByName("admin@admin.com");
            manager.AddToRoles(admin.Id, new string[] { "Admin" });
            // Create Retail User
            var user2 = new ApplicationUser()
            {
                UserName = "retail@retail.com",
                Email = "retail@retail.com",
                EmailConfirmed = true
            };
            manager.Create(user2, "Retail-1234");
            var retail = manager.FindByName("retail@retail.com");
            manager.AddToRoles(retail.Id, new string[] { "Retail" });

            // Create Wholesale User
            var user3 = new ApplicationUser()
            {
                UserName = "wholesale@wholesale.com",
                Email = "wholesale@wholesale.com",
                EmailConfirmed = true
            };
            manager.Create(user3, "Wholesale-1234");
            var wholesale = manager.FindByName("wholesale@wholesale.com");
            manager.AddToRoles(wholesale.Id, new string[] { "Wholesale" });
        }
    }
}
