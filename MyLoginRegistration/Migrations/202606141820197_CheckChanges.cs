namespace MyLoginRegistration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CheckChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserAccounts", "Role", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserAccounts", "Role", c => c.String(nullable: false));
        }
    }
}
