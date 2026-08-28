namespace MyLoginRegistration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakePasswordOptionalForGoogleUsers : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserAccounts", "Password", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserAccounts", "Password", c => c.String(nullable: false));
        }
    }
}
