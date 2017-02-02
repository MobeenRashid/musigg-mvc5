using System.Data.Entity.Migrations;

namespace Musicly.Persistence.Migrations
{
    public partial class AddDateAddedDescriptionToGig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gigs", "DateAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.Gigs", "Description", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Gigs", "Description");
            DropColumn("dbo.Gigs", "DateAdded");
        }
    }
}
