namespace PaySys.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpersonnelCodetoEmployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "PersonnelCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "PersonnelCode");
        }
    }
}
