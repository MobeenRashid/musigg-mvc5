using System.Data.Entity.Migrations;

namespace Musicly.Migrations
{
    public partial class AddIsCancelToGig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gigs", "IsCancel", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Gigs", "IsCancel");
        }
    }
}
