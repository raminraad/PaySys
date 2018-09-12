namespace PaySys.ModelAndBindLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ParameterTitles", "ValueType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ParameterTitles", "ValueType", c => c.Int(nullable: false));
        }
    }
}
