namespace PaySys.ModelAndBindLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMissionFormula : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MissionFormulas", "PerKmFactor", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MissionFormulas", "PerKmFactor");
        }
    }
}
