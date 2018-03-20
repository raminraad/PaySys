namespace PaySys.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtitletosubgroup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubGroups", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubGroups", "Title");
        }
    }
}
