namespace PaySys.ModelAndBindLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubGroups", "WorkshopCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubGroups", "WorkshopCode");
        }
    }
}
