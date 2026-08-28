namespace MyLoginRegistration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserDepartmentAndIsActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserAccounts", "Role", c => c.String(nullable: false));
            AddColumn("dbo.UserAccounts", "DepartmentId", c => c.Int(nullable: false));
            AddColumn("dbo.UserAccounts", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserAccounts", "IsActive");
            DropColumn("dbo.UserAccounts", "DepartmentId");
            DropColumn("dbo.UserAccounts", "Role");
        }
    }
}
