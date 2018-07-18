namespace PaySys.ModelAndBindLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmployeeMiscRemains", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.EmployeeMiscRemains", "Misc_MiscId", "dbo.Miscs");
            DropIndex("dbo.EmployeeMiscRemains", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.EmployeeMiscRemains", new[] { "Misc_MiscId" });
            CreateTable(
                "dbo.EmployeeMiscRecharges",
                c => new
                    {
                        EmployeeMiscRechargeId = c.Int(nullable: false, identity: true),
                        Value = c.Single(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Employee_EmployeeId = c.Int(),
                        Misc_MiscId = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeMiscRechargeId)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeId)
                .ForeignKey("dbo.Miscs", t => t.Misc_MiscId)
                .Index(t => t.Employee_EmployeeId)
                .Index(t => t.Misc_MiscId);
            
            DropTable("dbo.EmployeeMiscRemains");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EmployeeMiscRemains",
                c => new
                    {
                        EmployeeMiscRemainId = c.Int(nullable: false, identity: true),
                        Value = c.Single(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Employee_EmployeeId = c.Int(),
                        Misc_MiscId = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeMiscRemainId);
            
            DropForeignKey("dbo.EmployeeMiscRecharges", "Misc_MiscId", "dbo.Miscs");
            DropForeignKey("dbo.EmployeeMiscRecharges", "Employee_EmployeeId", "dbo.Employees");
            DropIndex("dbo.EmployeeMiscRecharges", new[] { "Misc_MiscId" });
            DropIndex("dbo.EmployeeMiscRecharges", new[] { "Employee_EmployeeId" });
            DropTable("dbo.EmployeeMiscRecharges");
            CreateIndex("dbo.EmployeeMiscRemains", "Misc_MiscId");
            CreateIndex("dbo.EmployeeMiscRemains", "Employee_EmployeeId");
            AddForeignKey("dbo.EmployeeMiscRemains", "Misc_MiscId", "dbo.Miscs", "MiscId");
            AddForeignKey("dbo.EmployeeMiscRemains", "Employee_EmployeeId", "dbo.Employees", "EmployeeId");
        }
    }
}
