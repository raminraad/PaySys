namespace PaySys.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subgroupmakemaingroupidrequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SubGroups", "MainGroup_MainGroupId", "dbo.MainGroups");
            DropIndex("dbo.SubGroups", new[] { "MainGroup_MainGroupId" });
            AlterColumn("dbo.SubGroups", "MainGroup_MainGroupId", c => c.Int(nullable: false));
            CreateIndex("dbo.SubGroups", "MainGroup_MainGroupId");
            AddForeignKey("dbo.SubGroups", "MainGroup_MainGroupId", "dbo.MainGroups", "MainGroupId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubGroups", "MainGroup_MainGroupId", "dbo.MainGroups");
            DropIndex("dbo.SubGroups", new[] { "MainGroup_MainGroupId" });
            AlterColumn("dbo.SubGroups", "MainGroup_MainGroupId", c => c.Int());
            CreateIndex("dbo.SubGroups", "MainGroup_MainGroupId");
            AddForeignKey("dbo.SubGroups", "MainGroup_MainGroupId", "dbo.MainGroups", "MainGroupId");
        }
    }
}
