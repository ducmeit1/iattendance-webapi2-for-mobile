using IAttendanceWebAPI.Persistence;
using IAttendanceWebAPI.Persistence.Migrations;
using System.Data.Entity.Migrations;

namespace IAttendanceWebAPI.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Persistence\Migrations";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            SeedingData.SeedUsers2(context);
        }
    }
}