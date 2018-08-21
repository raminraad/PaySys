namespace PaySys.ModelAndBindLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContractFieldTitles", "IsEditable", c => c.Boolean(nullable: false));
            AddColumn("dbo.ContractFieldTitles", "Index", c => c.Int(nullable: false));
            AddColumn("dbo.ContractFieldTitles", "IndexInRetirementReport", c => c.Int(nullable: false));
            AddColumn("dbo.MiscTitles", "Index", c => c.Int(nullable: false));
            DropColumn("dbo.Miscs", "Index");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Miscs", "Index", c => c.Int(nullable: false));
            DropColumn("dbo.MiscTitles", "Index");
            DropColumn("dbo.ContractFieldTitles", "IndexInRetirementReport");
            DropColumn("dbo.ContractFieldTitles", "Index");
            DropColumn("dbo.ContractFieldTitles", "IsEditable");
        }
    }
}
