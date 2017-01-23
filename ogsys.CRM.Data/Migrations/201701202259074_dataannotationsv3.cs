namespace ogsys.CRM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dataannotationsv3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Email", c => c.String(nullable: false, maxLength: 254));
            AlterColumn("dbo.Customers", "Phone", c => c.String(nullable: false, maxLength: 24));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Email", c => c.String(nullable: false));
        }
    }
}
