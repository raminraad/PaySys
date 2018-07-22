namespace PaySys.ModelAndBindLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeMiscForSubGroupPerYear : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Miscs", "Month");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Miscs", "Month", c => c.Int(nullable: false));
        }
    }
}
