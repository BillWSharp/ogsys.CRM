namespace ogsys.CRM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dataannotationsv2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Address", c => c.String(nullable: false, maxLength: 40));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Address", c => c.String(nullable: false));
        }
    }
}
