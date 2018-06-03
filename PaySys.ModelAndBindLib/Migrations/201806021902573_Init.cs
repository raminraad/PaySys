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
                        CityId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Distance = c.Int(nullable: false),
                        Percentage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CityId);
            
            CreateTable(
                "dbo.Missions",
                c => new
                    {
                        MissionId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        DateStart = c.String(),
                        DateEnd = c.String(),
                        TimeStart = c.String(),
                        TimeEnd = c.String(),
                        AmountResident = c.Int(nullable: false),
                        AmountNonResident = c.Int(nullable: false),
                        VehicleType = c.Int(nullable: false),
                        TravelExpense = c.Double(nullable: false),
                        City_CityId = c.Int(),
                        Employee_EmployeeId = c.Int(),
                    })
                .PrimaryKey(t => t.MissionId)
                .ForeignKey("dbo.Cities", t => t.City_CityId)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeId)
                .Index(t => t.City_CityId)
                .Index(t => t.Employee_EmployeeId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        FName = c.String(),
                        LName = c.String(),
                        NationalCardNo = c.String(),
                        BirthDate = c.String(),
                        BirthPlace = c.String(),
                        FatherName = c.String(),
                        HomeTel = c.String(),
                        Address = c.String(),
                        DossierNo = c.String(),
                        Sex = c.Int(nullable: false),
                        CellNo = c.String(),
                        PostalCode = c.String(),
                        PersonnelCode = c.String(),
                        IdCardNo = c.String(),
                        IdCardExportPlace = c.String(),
                        IdCardExportDate = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.ContractMasters",
                c => new
                    {
                        ContractMasterId = c.Int(nullable: false, identity: true),
                        ContractNo = c.String(),
                        DateExport = c.String(),
                        DateExecution = c.String(),
                        DateEmployment = c.String(),
                        MaritalStatus = c.Int(nullable: false),
                        HardshipFactor = c.Double(nullable: false),
                        InsuranceNo = c.String(),
                        IsCurrentContract = c.Boolean(nullable: false),
                        EducationStand = c.Int(nullable: false),
                        EmploymentType = c.Int(nullable: false),
                        SacrificeStand = c.Int(nullable: false),
                        AccountNoGov = c.String(),
                        AccountNoEmp = c.String(),
                        SubGroup_SubGroupId = c.Int(),
                        Employee_EmployeeId = c.Int(),
                        Job_JobId = c.Int(),
                    })
                .PrimaryKey(t => t.ContractMasterId)
                .ForeignKey("dbo.SubGroups", t => t.SubGroup_SubGroupId)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeId)
                .ForeignKey("dbo.Jobs", t => t.Job_JobId)
                .Index(t => t.SubGroup_SubGroupId)
                .Index(t => t.Employee_EmployeeId)
                .Index(t => t.Job_JobId);
            
            CreateTable(
                "dbo.ContractDetails",
                c => new
                    {
                        ContractDetailId = c.Int(nullable: false, identity: true),
                        Value = c.Single(nullable: false),
                        ContractMaster_ContractMasterId = c.Int(),
                        GroupContractFieldTitle_GroupContractFieldTitleId = c.Int(),
                    })
                .PrimaryKey(t => t.ContractDetailId)
                .ForeignKey("dbo.ContractMasters", t => t.ContractMaster_ContractMasterId)
                .ForeignKey("dbo.GroupContractFieldTitles", t => t.GroupContractFieldTitle_GroupContractFieldTitleId)
                .Index(t => t.ContractMaster_ContractMasterId)
                .Index(t => t.GroupContractFieldTitle_GroupContractFieldTitleId);
            
            CreateTable(
                "dbo.GroupContractFieldTitles",
                c => new
                    {
                        GroupContractFieldTitleId = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        ContractFieldTitle_ContractFieldTitleId = c.Int(),
                        ExpenseArticle_ExpenseArticleId = c.Int(),
                        MainGroup_MainGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.GroupContractFieldTitleId)
                .ForeignKey("dbo.ContractFieldTitles", t => t.ContractFieldTitle_ContractFieldTitleId)
                .ForeignKey("dbo.ExpenseArticles", t => t.ExpenseArticle_ExpenseArticleId)
                .ForeignKey("dbo.MainGroups", t => t.MainGroup_MainGroupId)
                .Index(t => t.ContractFieldTitle_ContractFieldTitleId)
                .Index(t => t.ExpenseArticle_ExpenseArticleId)
                .Index(t => t.MainGroup_MainGroupId);
            
            CreateTable(
                "dbo.ContractFieldTitles",
                c => new
                    {
                        ContractFieldTitleId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.ContractFieldTitleId);
            
            CreateTable(
                "dbo.ExpenseArticles",
                c => new
                    {
                        ExpenseArticleId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Code = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ExpenseArticleId);
            
            CreateTable(
                "dbo.GroupMiscs",
                c => new
                    {
                        GroupMiscId = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        ExpenseArticle_ExpenseArticleId = c.Int(),
                        MainGroup_MainGroupId = c.Int(),
                        MiscTitle_MiscTitleId = c.Int(),
                    })
                .PrimaryKey(t => t.GroupMiscId)
                .ForeignKey("dbo.ExpenseArticles", t => t.ExpenseArticle_ExpenseArticleId)
                .ForeignKey("dbo.MainGroups", t => t.MainGroup_MainGroupId)
                .ForeignKey("dbo.MiscTitles", t => t.MiscTitle_MiscTitleId)
                .Index(t => t.ExpenseArticle_ExpenseArticleId)
                .Index(t => t.MainGroup_MainGroupId)
                .Index(t => t.MiscTitle_MiscTitleId);
            
            CreateTable(
                "dbo.EmployeeMiscs",
                c => new
                    {
                        EmployeeMiscId = c.Int(nullable: false, identity: true),
                        Value = c.Single(nullable: false),
                        Employee_EmployeeId = c.Int(),
                        GroupMisc_GroupMiscId = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeMiscId)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeId)
                .ForeignKey("dbo.GroupMiscs", t => t.GroupMisc_GroupMiscId)
                .Index(t => t.Employee_EmployeeId)
                .Index(t => t.GroupMisc_GroupMiscId);
            
            CreateTable(
                "dbo.MainGroups",
                c => new
                    {
                        MainGroupId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ItemColorPallet = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MainGroupId);
            
            CreateTable(
                "dbo.GroupMonthlyVariables",
                c => new
                    {
                        GroupMonthlyVariableId = c.Int(nullable: false, identity: true),
                        IsActive = c.Boolean(nullable: false),
                        ContractFieldTitle_ContractFieldTitleId = c.Int(),
                        MainGroup_MainGroupId = c.Int(),
                        MonthlyVariableTitle_MonthlyVariableTitleId = c.Int(),
                        ExpenseArticle_ExpenseArticleId = c.Int(),
                    })
                .PrimaryKey(t => t.GroupMonthlyVariableId)
                .ForeignKey("dbo.ContractFieldTitles", t => t.ContractFieldTitle_ContractFieldTitleId)
                .ForeignKey("dbo.MainGroups", t => t.MainGroup_MainGroupId)
                .ForeignKey("dbo.MonthlyVariableTitles", t => t.MonthlyVariableTitle_MonthlyVariableTitleId)
                .ForeignKey("dbo.ExpenseArticles", t => t.ExpenseArticle_ExpenseArticleId)
                .Index(t => t.ContractFieldTitle_ContractFieldTitleId)
                .Index(t => t.MainGroup_MainGroupId)
                .Index(t => t.MonthlyVariableTitle_MonthlyVariableTitleId)
                .Index(t => t.ExpenseArticle_ExpenseArticleId);
            
            CreateTable(
                "dbo.EmployeeMonthlyVariables",
                c => new
                    {
                        EmployeeMonthlyVariableId = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        ValueNum = c.Single(nullable: false),
                        ValueStr = c.Single(nullable: false),
                        Employee_EmployeeId = c.Int(),
                        GroupMonthlyVariable_GroupMonthlyVariableId = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeMonthlyVariableId)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeId)
                .ForeignKey("dbo.GroupMonthlyVariables", t => t.GroupMonthlyVariable_GroupMonthlyVariableId)
                .Index(t => t.Employee_EmployeeId)
                .Index(t => t.GroupMonthlyVariable_GroupMonthlyVariableId);
            
            CreateTable(
                "dbo.MonthlyVariableTitles",
                c => new
                    {
                        MonthlyVariableTitleId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Alias = c.String(),
                        ValueType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MonthlyVariableTitleId);
            
            CreateTable(
                "dbo.SubGroups",
                c => new
                    {
                        SubGroupId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ItemColorPallet = c.Int(nullable: false),
                        MainGroup_MainGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubGroupId)
                .ForeignKey("dbo.MainGroups", t => t.MainGroup_MainGroupId, cascadeDelete: true)
                .Index(t => t.MainGroup_MainGroupId);
            
            CreateTable(
                "dbo.GroupSpecValues",
                c => new
                    {
                        GroupSpecValueId = c.Int(nullable: false, identity: true),
                        Value = c.Single(nullable: false),
                        ContentType = c.Int(nullable: false),
                        GroupSpecTitle_GroupSpecTitleId = c.Int(),
                        SubGroup_SubGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.GroupSpecValueId)
                .ForeignKey("dbo.GroupSpecTitles", t => t.GroupSpecTitle_GroupSpecTitleId)
                .ForeignKey("dbo.SubGroups", t => t.SubGroup_SubGroupId)
                .Index(t => t.GroupSpecTitle_GroupSpecTitleId)
                .Index(t => t.SubGroup_SubGroupId);
            
            CreateTable(
                "dbo.GroupSpecTitles",
                c => new
                    {
                        GroupSpecTitleId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Alias = c.String(),
                    })
                .PrimaryKey(t => t.GroupSpecTitleId);
            
            CreateTable(
                "dbo.SpecContractFields",
                c => new
                    {
                        SpecContractFieldId = c.Int(nullable: false, identity: true),
                        GroupContractFieldTitle_GroupContractFieldTitleId = c.Int(),
                        GroupSpecValue_GroupSpecValueId = c.Int(),
                    })
                .PrimaryKey(t => t.SpecContractFieldId)
                .ForeignKey("dbo.GroupContractFieldTitles", t => t.GroupContractFieldTitle_GroupContractFieldTitleId)
                .ForeignKey("dbo.GroupSpecValues", t => t.GroupSpecValue_GroupSpecValueId)
                .Index(t => t.GroupContractFieldTitle_GroupContractFieldTitleId)
                .Index(t => t.GroupSpecValue_GroupSpecValueId);
            
            CreateTable(
                "dbo.SpecMiscs",
                c => new
                    {
                        SpecMiscId = c.Int(nullable: false, identity: true),
                        GroupMisc_GroupMiscId = c.Int(),
                        GroupSpecValue_GroupSpecValueId = c.Int(),
                    })
                .PrimaryKey(t => t.SpecMiscId)
                .ForeignKey("dbo.GroupMiscs", t => t.GroupMisc_GroupMiscId)
                .ForeignKey("dbo.GroupSpecValues", t => t.GroupSpecValue_GroupSpecValueId)
                .Index(t => t.GroupMisc_GroupMiscId)
                .Index(t => t.GroupSpecValue_GroupSpecValueId);
            
            CreateTable(
                "dbo.HandselFormulas",
                c => new
                    {
                        HandselFormulaId = c.Int(nullable: false, identity: true),
                        DaysCount = c.Int(nullable: false),
                        Min = c.Double(nullable: false),
                        Max = c.Double(nullable: false),
                        TaxFreeValue = c.Double(nullable: false),
                        TaxRate = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        SubGroup_SubGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.HandselFormulaId)
                .ForeignKey("dbo.SubGroups", t => t.SubGroup_SubGroupId)
                .Index(t => t.SubGroup_SubGroupId);
            
            CreateTable(
                "dbo.MissionFormulas",
                c => new
                    {
                        MissionFormulaId = c.Int(nullable: false, identity: true),
                        SubGroup_SubGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.MissionFormulaId)
                .ForeignKey("dbo.SubGroups", t => t.SubGroup_SubGroupId)
                .Index(t => t.SubGroup_SubGroupId);
            
            CreateTable(
                "dbo.TaxTables",
                c => new
                    {
                        TaxTableId = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        SubGroup_SubGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.TaxTableId)
                .ForeignKey("dbo.SubGroups", t => t.SubGroup_SubGroupId)
                .Index(t => t.SubGroup_SubGroupId);
            
            CreateTable(
                "dbo.TaxRows",
                c => new
                    {
                        TaxRowId = c.Int(nullable: false, identity: true),
                        ValueTo = c.Single(nullable: false),
                        Factor = c.Single(nullable: false),
                        TaxTable_TaxTableId = c.Int(),
                    })
                .PrimaryKey(t => t.TaxRowId)
                .ForeignKey("dbo.TaxTables", t => t.TaxTable_TaxTableId)
                .Index(t => t.TaxTable_TaxTableId);
            
            CreateTable(
                "dbo.MiscTitles",
                c => new
                    {
                        MiscTitleId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Index = c.Int(nullable: false),
                        IsPayment = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MiscTitleId);
            
            CreateTable(
                "dbo.EmployeeMiscRemains",
                c => new
                    {
                        EmployeeMiscRemainId = c.Int(nullable: false, identity: true),
                        Value = c.Single(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Employee_EmployeeId = c.Int(),
                        MiscTitle_MiscTitleId = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeMiscRemainId)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeId)
                .ForeignKey("dbo.MiscTitles", t => t.MiscTitle_MiscTitleId)
                .Index(t => t.Employee_EmployeeId)
                .Index(t => t.MiscTitle_MiscTitleId);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        JobId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        JobNo = c.String(),
                        ItemColorPallet = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.JobId);
            
            CreateTable(
                "dbo.ContractDifferences",
                c => new
                    {
                        ContractMasterId = c.Int(nullable: false),
                        DateFrom = c.String(),
                        DateTo = c.String(),
                        FirstMonth = c.Double(nullable: false),
                        PayYear = c.Int(nullable: false),
                        PayMonth = c.Int(nullable: false),
                        Contract2Nd_ContractMasterId = c.Int(),
                    })
                .PrimaryKey(t => t.ContractMasterId)
                .ForeignKey("dbo.ContractMasters", t => t.ContractMasterId)
                .ForeignKey("dbo.ContractMasters", t => t.Contract2Nd_ContractMasterId)
                .Index(t => t.ContractMasterId)
                .Index(t => t.Contract2Nd_ContractMasterId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContractDifferences", "Contract2Nd_ContractMasterId", "dbo.ContractMasters");
            DropForeignKey("dbo.ContractDifferences", "ContractMasterId", "dbo.ContractMasters");
            DropForeignKey("dbo.Missions", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.ContractMasters", "Job_JobId", "dbo.Jobs");
            DropForeignKey("dbo.ContractMasters", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.GroupMonthlyVariables", "ExpenseArticle_ExpenseArticleId", "dbo.ExpenseArticles");
            DropForeignKey("dbo.GroupMiscs", "MiscTitle_MiscTitleId", "dbo.MiscTitles");
            DropForeignKey("dbo.EmployeeMiscRemains", "MiscTitle_MiscTitleId", "dbo.MiscTitles");
            DropForeignKey("dbo.EmployeeMiscRemains", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.TaxRows", "TaxTable_TaxTableId", "dbo.TaxTables");
            DropForeignKey("dbo.TaxTables", "SubGroup_SubGroupId", "dbo.SubGroups");
            DropForeignKey("dbo.MissionFormulas", "SubGroup_SubGroupId", "dbo.SubGroups");
            DropForeignKey("dbo.SubGroups", "MainGroup_MainGroupId", "dbo.MainGroups");
            DropForeignKey("dbo.HandselFormulas", "SubGroup_SubGroupId", "dbo.SubGroups");
            DropForeignKey("dbo.GroupSpecValues", "SubGroup_SubGroupId", "dbo.SubGroups");
            DropForeignKey("dbo.SpecMiscs", "GroupSpecValue_GroupSpecValueId", "dbo.GroupSpecValues");
            DropForeignKey("dbo.SpecMiscs", "GroupMisc_GroupMiscId", "dbo.GroupMiscs");
            DropForeignKey("dbo.SpecContractFields", "GroupSpecValue_GroupSpecValueId", "dbo.GroupSpecValues");
            DropForeignKey("dbo.SpecContractFields", "GroupContractFieldTitle_GroupContractFieldTitleId", "dbo.GroupContractFieldTitles");
            DropForeignKey("dbo.GroupSpecValues", "GroupSpecTitle_GroupSpecTitleId", "dbo.GroupSpecTitles");
            DropForeignKey("dbo.ContractMasters", "SubGroup_SubGroupId", "dbo.SubGroups");
            DropForeignKey("dbo.GroupMonthlyVariables", "MonthlyVariableTitle_MonthlyVariableTitleId", "dbo.MonthlyVariableTitles");
            DropForeignKey("dbo.GroupMonthlyVariables", "MainGroup_MainGroupId", "dbo.MainGroups");
            DropForeignKey("dbo.EmployeeMonthlyVariables", "GroupMonthlyVariable_GroupMonthlyVariableId", "dbo.GroupMonthlyVariables");
            DropForeignKey("dbo.EmployeeMonthlyVariables", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.GroupMonthlyVariables", "ContractFieldTitle_ContractFieldTitleId", "dbo.ContractFieldTitles");
            DropForeignKey("dbo.GroupMiscs", "MainGroup_MainGroupId", "dbo.MainGroups");
            DropForeignKey("dbo.GroupContractFieldTitles", "MainGroup_MainGroupId", "dbo.MainGroups");
            DropForeignKey("dbo.GroupMiscs", "ExpenseArticle_ExpenseArticleId", "dbo.ExpenseArticles");
            DropForeignKey("dbo.EmployeeMiscs", "GroupMisc_GroupMiscId", "dbo.GroupMiscs");
            DropForeignKey("dbo.EmployeeMiscs", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.GroupContractFieldTitles", "ExpenseArticle_ExpenseArticleId", "dbo.ExpenseArticles");
            DropForeignKey("dbo.GroupContractFieldTitles", "ContractFieldTitle_ContractFieldTitleId", "dbo.ContractFieldTitles");
            DropForeignKey("dbo.ContractDetails", "GroupContractFieldTitle_GroupContractFieldTitleId", "dbo.GroupContractFieldTitles");
            DropForeignKey("dbo.ContractDetails", "ContractMaster_ContractMasterId", "dbo.ContractMasters");
            DropForeignKey("dbo.Missions", "City_CityId", "dbo.Cities");
            DropIndex("dbo.ContractDifferences", new[] { "Contract2Nd_ContractMasterId" });
            DropIndex("dbo.ContractDifferences", new[] { "ContractMasterId" });
            DropIndex("dbo.EmployeeMiscRemains", new[] { "MiscTitle_MiscTitleId" });
            DropIndex("dbo.EmployeeMiscRemains", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.TaxRows", new[] { "TaxTable_TaxTableId" });
            DropIndex("dbo.TaxTables", new[] { "SubGroup_SubGroupId" });
            DropIndex("dbo.MissionFormulas", new[] { "SubGroup_SubGroupId" });
            DropIndex("dbo.HandselFormulas", new[] { "SubGroup_SubGroupId" });
            DropIndex("dbo.SpecMiscs", new[] { "GroupSpecValue_GroupSpecValueId" });
            DropIndex("dbo.SpecMiscs", new[] { "GroupMisc_GroupMiscId" });
            DropIndex("dbo.SpecContractFields", new[] { "GroupSpecValue_GroupSpecValueId" });
            DropIndex("dbo.SpecContractFields", new[] { "GroupContractFieldTitle_GroupContractFieldTitleId" });
            DropIndex("dbo.GroupSpecValues", new[] { "SubGroup_SubGroupId" });
            DropIndex("dbo.GroupSpecValues", new[] { "GroupSpecTitle_GroupSpecTitleId" });
            DropIndex("dbo.SubGroups", new[] { "MainGroup_MainGroupId" });
            DropIndex("dbo.EmployeeMonthlyVariables", new[] { "GroupMonthlyVariable_GroupMonthlyVariableId" });
            DropIndex("dbo.EmployeeMonthlyVariables", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.GroupMonthlyVariables", new[] { "ExpenseArticle_ExpenseArticleId" });
            DropIndex("dbo.GroupMonthlyVariables", new[] { "MonthlyVariableTitle_MonthlyVariableTitleId" });
            DropIndex("dbo.GroupMonthlyVariables", new[] { "MainGroup_MainGroupId" });
            DropIndex("dbo.GroupMonthlyVariables", new[] { "ContractFieldTitle_ContractFieldTitleId" });
            DropIndex("dbo.EmployeeMiscs", new[] { "GroupMisc_GroupMiscId" });
            DropIndex("dbo.EmployeeMiscs", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.GroupMiscs", new[] { "MiscTitle_MiscTitleId" });
            DropIndex("dbo.GroupMiscs", new[] { "MainGroup_MainGroupId" });
            DropIndex("dbo.GroupMiscs", new[] { "ExpenseArticle_ExpenseArticleId" });
            DropIndex("dbo.GroupContractFieldTitles", new[] { "MainGroup_MainGroupId" });
            DropIndex("dbo.GroupContractFieldTitles", new[] { "ExpenseArticle_ExpenseArticleId" });
            DropIndex("dbo.GroupContractFieldTitles", new[] { "ContractFieldTitle_ContractFieldTitleId" });
            DropIndex("dbo.ContractDetails", new[] { "GroupContractFieldTitle_GroupContractFieldTitleId" });
            DropIndex("dbo.ContractDetails", new[] { "ContractMaster_ContractMasterId" });
            DropIndex("dbo.ContractMasters", new[] { "Job_JobId" });
            DropIndex("dbo.ContractMasters", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.ContractMasters", new[] { "SubGroup_SubGroupId" });
            DropIndex("dbo.Missions", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.Missions", new[] { "City_CityId" });
            DropTable("dbo.ContractDifferences");
            DropTable("dbo.Jobs");
            DropTable("dbo.EmployeeMiscRemains");
            DropTable("dbo.MiscTitles");
            DropTable("dbo.TaxRows");
            DropTable("dbo.TaxTables");
            DropTable("dbo.MissionFormulas");
            DropTable("dbo.HandselFormulas");
            DropTable("dbo.SpecMiscs");
            DropTable("dbo.SpecContractFields");
            DropTable("dbo.GroupSpecTitles");
            DropTable("dbo.GroupSpecValues");
            DropTable("dbo.SubGroups");
            DropTable("dbo.MonthlyVariableTitles");
            DropTable("dbo.EmployeeMonthlyVariables");
            DropTable("dbo.GroupMonthlyVariables");
            DropTable("dbo.MainGroups");
            DropTable("dbo.EmployeeMiscs");
            DropTable("dbo.GroupMiscs");
            DropTable("dbo.ExpenseArticles");
            DropTable("dbo.ContractFieldTitles");
            DropTable("dbo.GroupContractFieldTitles");
            DropTable("dbo.ContractDetails");
            DropTable("dbo.ContractMasters");
            DropTable("dbo.Employees");
            DropTable("dbo.Missions");
            DropTable("dbo.Cities");
        }
    }
}
