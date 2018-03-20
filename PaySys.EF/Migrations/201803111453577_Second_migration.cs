namespace PaySys.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second_migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "NationalNo", c => c.String());
            AddColumn("dbo.Employees", "IdCardNo", c => c.String());
            AddColumn("dbo.Employees", "IdCardExportPlace", c => c.String());
            AddColumn("dbo.Employees", "IdCardExportDate", c => c.String());
            DropColumn("dbo.Employees", "NationalCardNo");
            DropColumn("dbo.Employees", "BirthCertificateNo");
            DropColumn("dbo.Employees", "BirthCertificatePlace");
            DropColumn("dbo.Employees", "BirthCertificateDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "BirthCertificateDate", c => c.String());
            AddColumn("dbo.Employees", "BirthCertificatePlace", c => c.String());
            AddColumn("dbo.Employees", "BirthCertificateNo", c => c.String());
            AddColumn("dbo.Employees", "NationalCardNo", c => c.String());
            DropColumn("dbo.Employees", "IdCardExportDate");
            DropColumn("dbo.Employees", "IdCardExportPlace");
            DropColumn("dbo.Employees", "IdCardNo");
            DropColumn("dbo.Employees", "NationalNo");
        }
    }
}
