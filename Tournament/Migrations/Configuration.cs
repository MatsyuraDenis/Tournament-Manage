using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Tournament.Models;

namespace Tournament.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Tournament.Models.Helpers;

    internal sealed class Configuration : DbMigrationsConfiguration<Tournament.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Tournament.Models.ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var adminRole = new IdentityRole(RoleHelper.Admin);
            var userRole = new IdentityRole(RoleHelper.User);

            if (!roleManager.RoleExists(RoleHelper.Admin))
                roleManager.Create(adminRole);

            if (!roleManager.RoleExists(RoleHelper.User))
                roleManager.Create(userRole);

            var users = context.Users.ToList();

            var user = userManager.FindByName("Admin");

            if(user != null && !userManager.IsInRole(user.Id, adminRole.Name))
                userManager.AddToRole(user.Id, adminRole.Name);

            base.Seed(context);
        }
    }
}
