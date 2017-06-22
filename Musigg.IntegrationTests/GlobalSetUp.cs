using Musicly.Persistence;
using Musicly.Persistence.Migrations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Musicly.Core.Models;

namespace Musigg.IntegrationTests
{
    public class GlobalSetUp
    {
        public static void SetUp()
        {
            UpdateDatabaseToLatestVersion();

            Seed();
        }

        private static void UpdateDatabaseToLatestVersion()
        {
            var config = new Configuration();
            var migrator = new DbMigrator(config);
            migrator.Update();
        }

        public static void Seed()
        {
            var dbContext = new ApplicationDbContext();

            if (dbContext.Users.Any())
                return;

            dbContext.Users.Add(new ApplicationUser() { Name="User1", UserName="User1",Email="user1@domain.com",PasswordHash="_" }); ;

            dbContext.Users.Add(new ApplicationUser() { Name = "User2", UserName = "User2", Email = "user2@domain.com", PasswordHash = "_" }); ;

            dbContext.Users.Add(new ApplicationUser() { Name = "User3", UserName = "User3", Email = "user3@domain.com", PasswordHash = "_" }); ;

            dbContext.SaveChanges();
        }
    }
}
