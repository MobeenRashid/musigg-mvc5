using System.Data.Entity.Migrations;

namespace Musicly.Persistence.Migrations
{
    public partial class AddAvatarToAppUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Avatar", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Avatar");
        }
    }
}
