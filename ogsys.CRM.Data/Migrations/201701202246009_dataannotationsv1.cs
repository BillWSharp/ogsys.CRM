namespace ogsys.CRM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dataannotationsv1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Firstname", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Customers", "Lastname", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Customers", "Company", c => c.String(maxLength: 40));
            AlterColumn("dbo.Customers", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.CustomerNotes", "Body", c => c.String(nullable: false, maxLength: 512));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CustomerNotes", "Body", c => c.String());
            AlterColumn("dbo.Customers", "Phone", c => c.String());
            AlterColumn("dbo.Customers", "Address", c => c.String());
            AlterColumn("dbo.Customers", "Email", c => c.String());
            AlterColumn("dbo.Customers", "Company", c => c.String());
            AlterColumn("dbo.Customers", "Lastname", c => c.String());
            AlterColumn("dbo.Customers", "Firstname", c => c.String());
        }
    }
}
