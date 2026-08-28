namespace MyLoginRegistration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGoogleAuthenticationFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserAccounts", "GoogleId", c => c.String());
            AddColumn("dbo.UserAccounts", "LoginProvider", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserAccounts", "LoginProvider");
            DropColumn("dbo.UserAccounts", "GoogleId");
        }
    }
}
