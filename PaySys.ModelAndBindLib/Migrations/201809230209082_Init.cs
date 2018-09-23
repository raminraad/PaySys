namespace PaySys.ModelAndBindLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Distance = c.Int(nullable: false),
                        Percentage = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Missions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        AmountResident = c.Int(nullable: false),
                        AmountNonResident = c.Int(nullable: false),
                        VehicleType = c.Int(nullable: false),
                        TravelExpense = c.Double(nullable: false),
                        MissionContractNo = c.String(),
                        City_Id = c.Int(),
                        ContractMaster_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.City_Id)
                .ForeignKey("dbo.ContractMasters", t => t.ContractMaster_Id)
                .Index(t => t.City_Id)
                .Index(t => t.ContractMaster_Id);
            
            CreateTable(
                "dbo.ContractMasters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContractNo = c.String(),
                        DateExport = c.DateTime(nullable: false),
                        DateExecution = c.DateTime(nullable: false),
                        DateEmployment = c.DateTime(nullable: false),
                        MaritalStatus = c.Int(nullable: false),
                        HardshipFactor = c.Double(nullable: false),
                        InsuranceNo = c.String(),
                        EducationStand = c.Int(nullable: false),
                        EmploymentType = c.Int(nullable: false),
                        SacrificeStand = c.Int(nullable: false),
                        ServiceStand = c.Int(nullable: false),
                        AccountNoGov = c.String(),
                        AccountNoEmp = c.String(),
                        IsCurrent = c.Boolean(nullable: false),
                        Employee_Id = c.Int(),
                        SubGroup_Id = c.Int(),
                        Job_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Employee_Id)
                .ForeignKey("dbo.SubGroups", t => t.SubGroup_Id)
                .ForeignKey("dbo.Jobs", t => t.Job_Id)
                .Index(t => t.Employee_Id)
                .Index(t => t.SubGroup_Id)
                .Index(t => t.Job_Id);
            
            CreateTable(
                "dbo.ContractDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Double(nullable: false),
                        ContractField_Id = c.Int(),
                        ContractMaster_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContractFields", t => t.ContractField_Id)
                .ForeignKey("dbo.ContractMasters", t => t.ContractMaster_Id)
                .Index(t => t.ContractField_Id)
                .Index(t => t.ContractMaster_Id);
            
            CreateTable(
                "dbo.ContractFields",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        Title = c.String(),
                        Alias = c.String(),
                        IsEditable = c.Boolean(nullable: false),
                        Index = c.Int(nullable: false),
                        IndexInRetirementReport = c.Int(nullable: false),
                        MainGroup_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MainGroups", t => t.MainGroup_Id)
                .Index(t => t.MainGroup_Id);
            
            CreateTable(
                "dbo.ExpenseArticleOfContractFieldForSubGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Month = c.Int(nullable: false),
                        ContractField_Id = c.Int(),
                        ExpenseArticle_Id = c.Int(),
                        SubGroup_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContractFields", t => t.ContractField_Id)
                .ForeignKey("dbo.ExpenseArticles", t => t.ExpenseArticle_Id)
                .ForeignKey("dbo.SubGroups", t => t.SubGroup_Id)
                .Index(t => t.ContractField_Id)
                .Index(t => t.ExpenseArticle_Id)
                .Index(t => t.SubGroup_Id);
            
            CreateTable(
                "dbo.ExpenseArticles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Code = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Miscs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        ExpenseArticle_Id = c.Int(),
                        SubGroup_Id = c.Int(),
                        MiscTitle_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExpenseArticles", t => t.ExpenseArticle_Id)
                .ForeignKey("dbo.SubGroups", t => t.SubGroup_Id)
                .ForeignKey("dbo.MiscTitles", t => t.MiscTitle_Id)
                .Index(t => t.ExpenseArticle_Id)
                .Index(t => t.SubGroup_Id)
                .Index(t => t.MiscTitle_Id);
            
            CreateTable(
                "dbo.MiscRecharges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Double(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Employee_Id = c.Int(),
                        Misc_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Employee_Id)
                .ForeignKey("dbo.Miscs", t => t.Misc_Id)
                .Index(t => t.Employee_Id)
                .Index(t => t.Misc_Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FName = c.String(),
                        LName = c.String(),
                        NationalCardNo = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        BirthPlace = c.String(),
                        FatherName = c.String(),
                        HomeTel = c.String(),
                        Address = c.String(),
                        DossierNo = c.String(),
                        Sex = c.Int(nullable: false),
                        CellNo = c.String(),
                        PostalCode = c.String(),
                        PersonnelCode = c.String(),
                        IdCardExportPlace = c.String(),
                        IdCardExportDate = c.DateTime(nullable: false),
                        IdCardNo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MiscValueForEmployees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Double(nullable: false),
                        ValueSubtraction = c.Double(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Employee_Id = c.Int(),
                        Misc_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Employee_Id)
                .ForeignKey("dbo.Miscs", t => t.Misc_Id)
                .Index(t => t.Employee_Id)
                .Index(t => t.Misc_Id);
            
            CreateTable(
                "dbo.VariableValueForEmployees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumericValue = c.Double(),
                        StringValue = c.String(),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        DateValue = c.DateTime(),
                        Employee_Id = c.Int(),
                        Variable_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Employee_Id)
                .ForeignKey("dbo.Variables", t => t.Variable_Id)
                .Index(t => t.Employee_Id)
                .Index(t => t.Variable_Id);
            
            CreateTable(
                "dbo.Variables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Index = c.Int(nullable: false),
                        FromYear = c.Int(nullable: false),
                        FromMonth = c.Int(nullable: false),
                        ToYear = c.Int(nullable: false),
                        ToMonth = c.Int(nullable: false),
                        Title = c.String(),
                        Alias = c.String(),
                        ValueType = c.Int(nullable: false),
                        MainGroup_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MainGroups", t => t.MainGroup_Id)
                .Index(t => t.MainGroup_Id);
            
            CreateTable(
                "dbo.MainGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Alias = c.String(),
                        ItemColor = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Alias = c.String(),
                        WorkshopCode = c.String(),
                        Title = c.String(),
                        ItemColor = c.Int(nullable: false),
                        Is31 = c.Boolean(nullable: false),
                        IsFreeZone = c.Boolean(nullable: false),
                        ExpenseArticleOfEmployer_Id = c.Int(),
                        ExpenseArticleOfMission_Id = c.Int(),
                        ExpenseArticleOfOvertime_Id = c.Int(),
                        MainGroup_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExpenseArticles", t => t.ExpenseArticleOfEmployer_Id)
                .ForeignKey("dbo.ExpenseArticles", t => t.ExpenseArticleOfMission_Id)
                .ForeignKey("dbo.ExpenseArticles", t => t.ExpenseArticleOfOvertime_Id)
                .ForeignKey("dbo.MainGroups", t => t.MainGroup_Id, cascadeDelete: true)
                .Index(t => t.ExpenseArticleOfEmployer_Id)
                .Index(t => t.ExpenseArticleOfMission_Id)
                .Index(t => t.ExpenseArticleOfOvertime_Id)
                .Index(t => t.MainGroup_Id);
            
            CreateTable(
                "dbo.HandselFormulas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DaysCount = c.Int(nullable: false),
                        Min = c.Double(nullable: false),
                        Max = c.Double(nullable: false),
                        TaxFreeValue = c.Double(nullable: false),
                        TaxRate = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        SubGroup_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubGroups", t => t.SubGroup_Id)
                .Index(t => t.SubGroup_Id);
            
            CreateTable(
                "dbo.MissionFormulas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DivideFactor = c.Double(nullable: false),
                        AddFactor = c.Double(nullable: false),
                        MaxFactor = c.Double(nullable: false),
                        SubtractFactor = c.Double(nullable: false),
                        PerKmFactor = c.Double(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        SubGroup_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubGroups", t => t.SubGroup_Id)
                .Index(t => t.SubGroup_Id);
            
            CreateTable(
                "dbo.MissionFormulaInvolvedContractFields",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContractField_Id = c.Int(),
                        MissionFormula_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContractFields", t => t.ContractField_Id)
                .ForeignKey("dbo.MissionFormulas", t => t.MissionFormula_Id)
                .Index(t => t.ContractField_Id)
                .Index(t => t.MissionFormula_Id);
            
            CreateTable(
                "dbo.Parameters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Double(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        ParameterTitle_Id = c.Int(),
                        SubGroup_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ParameterTitles", t => t.ParameterTitle_Id)
                .ForeignKey("dbo.SubGroups", t => t.SubGroup_Id)
                .Index(t => t.ParameterTitle_Id)
                .Index(t => t.SubGroup_Id);
            
            CreateTable(
                "dbo.ParameterInvolvedContractFields",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContractField_Id = c.Int(),
                        Parameter_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContractFields", t => t.ContractField_Id)
                .ForeignKey("dbo.Parameters", t => t.Parameter_Id)
                .Index(t => t.ContractField_Id)
                .Index(t => t.Parameter_Id);
            
            CreateTable(
                "dbo.ParameterInvolvedMiscs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Misc_Id = c.Int(),
                        Parameter_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Miscs", t => t.Misc_Id)
                .ForeignKey("dbo.Parameters", t => t.Parameter_Id)
                .Index(t => t.Misc_Id)
                .Index(t => t.Parameter_Id);
            
            CreateTable(
                "dbo.ParameterTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Alias = c.String(),
                        ParameterTitle_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ParameterTitles", t => t.ParameterTitle_Id)
                .Index(t => t.ParameterTitle_Id);
            
            CreateTable(
                "dbo.TaxTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        SubGroup_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubGroups", t => t.SubGroup_Id)
                .Index(t => t.SubGroup_Id);
            
            CreateTable(
                "dbo.TaxRows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ValueTo = c.Double(nullable: false),
                        Factor = c.Double(nullable: false),
                        TaxTable_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaxTables", t => t.TaxTable_Id)
                .Index(t => t.TaxTable_Id);
            
            CreateTable(
                "dbo.MiscTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Index = c.Int(nullable: false),
                        Alias = c.String(),
                        IsPayment = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        JobNo = c.String(),
                        Description = c.String(),
                        ItemColor = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContractDifferences",
                c => new
                    {
                        ContractMasterId = c.Int(nullable: false),
                        DateFrom = c.DateTime(nullable: false),
                        DateTo = c.DateTime(nullable: false),
                        FirstMonth = c.Double(nullable: false),
                        PayYear = c.Int(nullable: false),
                        PayMonth = c.Int(nullable: false),
                        Contract2Nd_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ContractMasterId)
                .ForeignKey("dbo.ContractMasters", t => t.ContractMasterId)
                .ForeignKey("dbo.ContractMasters", t => t.Contract2Nd_Id)
                .Index(t => t.ContractMasterId)
                .Index(t => t.Contract2Nd_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContractDifferences", "Contract2Nd_Id", "dbo.ContractMasters");
            DropForeignKey("dbo.ContractDifferences", "ContractMasterId", "dbo.ContractMasters");
            DropForeignKey("dbo.Missions", "ContractMaster_Id", "dbo.ContractMasters");
            DropForeignKey("dbo.ContractMasters", "Job_Id", "dbo.Jobs");
            DropForeignKey("dbo.ContractDetails", "ContractMaster_Id", "dbo.ContractMasters");
            DropForeignKey("dbo.Miscs", "MiscTitle_Id", "dbo.MiscTitles");
            DropForeignKey("dbo.MiscRecharges", "Misc_Id", "dbo.Miscs");
            DropForeignKey("dbo.VariableValueForEmployees", "Variable_Id", "dbo.Variables");
            DropForeignKey("dbo.Variables", "MainGroup_Id", "dbo.MainGroups");
            DropForeignKey("dbo.TaxRows", "TaxTable_Id", "dbo.TaxTables");
            DropForeignKey("dbo.TaxTables", "SubGroup_Id", "dbo.SubGroups");
            DropForeignKey("dbo.Parameters", "SubGroup_Id", "dbo.SubGroups");
            DropForeignKey("dbo.Parameters", "ParameterTitle_Id", "dbo.ParameterTitles");
            DropForeignKey("dbo.ParameterTitles", "ParameterTitle_Id", "dbo.ParameterTitles");
            DropForeignKey("dbo.ParameterInvolvedMiscs", "Parameter_Id", "dbo.Parameters");
            DropForeignKey("dbo.ParameterInvolvedMiscs", "Misc_Id", "dbo.Miscs");
            DropForeignKey("dbo.ParameterInvolvedContractFields", "Parameter_Id", "dbo.Parameters");
            DropForeignKey("dbo.ParameterInvolvedContractFields", "ContractField_Id", "dbo.ContractFields");
            DropForeignKey("dbo.MissionFormulas", "SubGroup_Id", "dbo.SubGroups");
            DropForeignKey("dbo.MissionFormulaInvolvedContractFields", "MissionFormula_Id", "dbo.MissionFormulas");
            DropForeignKey("dbo.MissionFormulaInvolvedContractFields", "ContractField_Id", "dbo.ContractFields");
            DropForeignKey("dbo.Miscs", "SubGroup_Id", "dbo.SubGroups");
            DropForeignKey("dbo.SubGroups", "MainGroup_Id", "dbo.MainGroups");
            DropForeignKey("dbo.HandselFormulas", "SubGroup_Id", "dbo.SubGroups");
            DropForeignKey("dbo.SubGroups", "ExpenseArticleOfOvertime_Id", "dbo.ExpenseArticles");
            DropForeignKey("dbo.SubGroups", "ExpenseArticleOfMission_Id", "dbo.ExpenseArticles");
            DropForeignKey("dbo.SubGroups", "ExpenseArticleOfEmployer_Id", "dbo.ExpenseArticles");
            DropForeignKey("dbo.ExpenseArticleOfContractFieldForSubGroups", "SubGroup_Id", "dbo.SubGroups");
            DropForeignKey("dbo.ContractMasters", "SubGroup_Id", "dbo.SubGroups");
            DropForeignKey("dbo.ContractFields", "MainGroup_Id", "dbo.MainGroups");
            DropForeignKey("dbo.VariableValueForEmployees", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.MiscValueForEmployees", "Misc_Id", "dbo.Miscs");
            DropForeignKey("dbo.MiscValueForEmployees", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.MiscRecharges", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.ContractMasters", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.Miscs", "ExpenseArticle_Id", "dbo.ExpenseArticles");
            DropForeignKey("dbo.ExpenseArticleOfContractFieldForSubGroups", "ExpenseArticle_Id", "dbo.ExpenseArticles");
            DropForeignKey("dbo.ExpenseArticleOfContractFieldForSubGroups", "ContractField_Id", "dbo.ContractFields");
            DropForeignKey("dbo.ContractDetails", "ContractField_Id", "dbo.ContractFields");
            DropForeignKey("dbo.Missions", "City_Id", "dbo.Cities");
            DropIndex("dbo.ContractDifferences", new[] { "Contract2Nd_Id" });
            DropIndex("dbo.ContractDifferences", new[] { "ContractMasterId" });
            DropIndex("dbo.TaxRows", new[] { "TaxTable_Id" });
            DropIndex("dbo.TaxTables", new[] { "SubGroup_Id" });
            DropIndex("dbo.ParameterTitles", new[] { "ParameterTitle_Id" });
            DropIndex("dbo.ParameterInvolvedMiscs", new[] { "Parameter_Id" });
            DropIndex("dbo.ParameterInvolvedMiscs", new[] { "Misc_Id" });
            DropIndex("dbo.ParameterInvolvedContractFields", new[] { "Parameter_Id" });
            DropIndex("dbo.ParameterInvolvedContractFields", new[] { "ContractField_Id" });
            DropIndex("dbo.Parameters", new[] { "SubGroup_Id" });
            DropIndex("dbo.Parameters", new[] { "ParameterTitle_Id" });
            DropIndex("dbo.MissionFormulaInvolvedContractFields", new[] { "MissionFormula_Id" });
            DropIndex("dbo.MissionFormulaInvolvedContractFields", new[] { "ContractField_Id" });
            DropIndex("dbo.MissionFormulas", new[] { "SubGroup_Id" });
            DropIndex("dbo.HandselFormulas", new[] { "SubGroup_Id" });
            DropIndex("dbo.SubGroups", new[] { "MainGroup_Id" });
            DropIndex("dbo.SubGroups", new[] { "ExpenseArticleOfOvertime_Id" });
            DropIndex("dbo.SubGroups", new[] { "ExpenseArticleOfMission_Id" });
            DropIndex("dbo.SubGroups", new[] { "ExpenseArticleOfEmployer_Id" });
            DropIndex("dbo.Variables", new[] { "MainGroup_Id" });
            DropIndex("dbo.VariableValueForEmployees", new[] { "Variable_Id" });
            DropIndex("dbo.VariableValueForEmployees", new[] { "Employee_Id" });
            DropIndex("dbo.MiscValueForEmployees", new[] { "Misc_Id" });
            DropIndex("dbo.MiscValueForEmployees", new[] { "Employee_Id" });
            DropIndex("dbo.MiscRecharges", new[] { "Misc_Id" });
            DropIndex("dbo.MiscRecharges", new[] { "Employee_Id" });
            DropIndex("dbo.Miscs", new[] { "MiscTitle_Id" });
            DropIndex("dbo.Miscs", new[] { "SubGroup_Id" });
            DropIndex("dbo.Miscs", new[] { "ExpenseArticle_Id" });
            DropIndex("dbo.ExpenseArticleOfContractFieldForSubGroups", new[] { "SubGroup_Id" });
            DropIndex("dbo.ExpenseArticleOfContractFieldForSubGroups", new[] { "ExpenseArticle_Id" });
            DropIndex("dbo.ExpenseArticleOfContractFieldForSubGroups", new[] { "ContractField_Id" });
            DropIndex("dbo.ContractFields", new[] { "MainGroup_Id" });
            DropIndex("dbo.ContractDetails", new[] { "ContractMaster_Id" });
            DropIndex("dbo.ContractDetails", new[] { "ContractField_Id" });
            DropIndex("dbo.ContractMasters", new[] { "Job_Id" });
            DropIndex("dbo.ContractMasters", new[] { "SubGroup_Id" });
            DropIndex("dbo.ContractMasters", new[] { "Employee_Id" });
            DropIndex("dbo.Missions", new[] { "ContractMaster_Id" });
            DropIndex("dbo.Missions", new[] { "City_Id" });
            DropTable("dbo.ContractDifferences");
            DropTable("dbo.Jobs");
            DropTable("dbo.MiscTitles");
            DropTable("dbo.TaxRows");
            DropTable("dbo.TaxTables");
            DropTable("dbo.ParameterTitles");
            DropTable("dbo.ParameterInvolvedMiscs");
            DropTable("dbo.ParameterInvolvedContractFields");
            DropTable("dbo.Parameters");
            DropTable("dbo.MissionFormulaInvolvedContractFields");
            DropTable("dbo.MissionFormulas");
            DropTable("dbo.HandselFormulas");
            DropTable("dbo.SubGroups");
            DropTable("dbo.MainGroups");
            DropTable("dbo.Variables");
            DropTable("dbo.VariableValueForEmployees");
            DropTable("dbo.MiscValueForEmployees");
            DropTable("dbo.Employees");
            DropTable("dbo.MiscRecharges");
            DropTable("dbo.Miscs");
            DropTable("dbo.ExpenseArticles");
            DropTable("dbo.ExpenseArticleOfContractFieldForSubGroups");
            DropTable("dbo.ContractFields");
            DropTable("dbo.ContractDetails");
            DropTable("dbo.ContractMasters");
            DropTable("dbo.Missions");
            DropTable("dbo.Cities");
        }
    }
}
