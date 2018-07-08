namespace PaySys.ModelAndBindLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCityPercentage : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cities", "Percentage", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cities", "Percentage", c => c.Int(nullable: false));
        }
    }
}
