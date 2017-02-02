using System.Data.Entity.Migrations;

namespace Musicly.Migrations
{
    public partial class AddAttendance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendances",
                c => new
                {
                    GigId = c.Int(nullable: false),
                    ArtistId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.GigId, t.ArtistId })
                .ForeignKey("dbo.AspNetUsers", t => t.ArtistId, cascadeDelete: true)
                .ForeignKey("dbo.Gigs", t => t.GigId)
                .Index(t => t.GigId)
                .Index(t => t.ArtistId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "GigId", "dbo.Gigs");
            DropForeignKey("dbo.Attendances", "ArtistId", "dbo.AspNetUsers");
            DropIndex("dbo.Attendances", new[] { "ArtistId" });
            DropIndex("dbo.Attendances", new[] { "GigId" });
            DropTable("dbo.Attendances");
        }
    }
}
