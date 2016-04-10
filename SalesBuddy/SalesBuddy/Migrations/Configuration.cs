using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SalesBuddy.Models;

namespace SalesBuddy.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SalesBuddy.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SalesBuddy.Models.ApplicationDbContext context)
        {
            //if (!(context.Users.Any(u => u.UserName == "cpetersensalesbuddy@gmail.com")))
            //{
            //    RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);
            //    RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);
            //    UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(context);
            //    UserManager<ApplicationUser> userManager = new ApplicationUserManager(userStore);
            //    var userToInsert = new ApplicationUser
            //    {
            //        UserName = "cpetersensalesbuddy@gmail.com",
            //        FirstName = "Christian",
            //        LastName = "Petersen",
            //        Email = "cpetersensalesbuddy@gmail.com",
            //        EmailConfirmed = true
            //    };
            //    userManager.Create(userToInsert, password: "ltftso24it");
            //    roleManager.Create(new IdentityRole { Name = "admin" });
            //    userManager.AddToRole(userToInsert.Id, "admin");

            //}
        }
    }
}
