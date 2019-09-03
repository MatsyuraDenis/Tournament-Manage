namespace Tournament.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGenderToUserTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Gender", c => c.String());
            AlterColumn("dbo.AspNetUsers", "DateOfRegistration", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "DateOfRegistration", c => c.DateTime());
            DropColumn("dbo.AspNetUsers", "Gender");
        }
    }
}
