using System.Data.Entity.Migrations;
using Musicly.Core.Models;

namespace Musicly.Persistence.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Persistence\Migrations";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            context.Genres.AddOrUpdate(gnr => gnr.Name, new Genre[]
            {
                new Genre() {Name = "Jazz"},
                new Genre() {Name = "Blues"},
                new Genre() {Name = "Rock"},
                new Genre() {Name = "Country"}
            });
            context.SaveChanges();
        }
    }
}
