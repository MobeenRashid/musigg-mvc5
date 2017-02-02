using System.Data.Entity.Migrations;

namespace Musicly.Persistence.Migrations
{
    public partial class AddCountryAddressAboutInUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Country", c => c.String());
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            AddColumn("dbo.AspNetUsers", "About", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "About");
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "Country");
        }
    }
}
