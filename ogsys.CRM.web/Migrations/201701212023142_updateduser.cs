namespace ogsys.CRM.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateduser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Firstname", c => c.String(maxLength: 40));
            AddColumn("dbo.AspNetUsers", "Lastname", c => c.String(maxLength: 40));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Lastname");
            DropColumn("dbo.AspNetUsers", "Firstname");
        }
    }
}
