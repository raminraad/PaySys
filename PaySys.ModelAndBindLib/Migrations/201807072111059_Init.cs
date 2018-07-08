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
                        MissionContractNo = c.String(),
                        City_CityId = c.Int(),
                        Employee_EmployeeId = c.Int(),
                        ContractMaster_ContractMasterId = c.Int(),
                    })
                .PrimaryKey(t => t.MissionId)
                .ForeignKey("dbo.Cities", t => t.City_CityId)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeId)
                .ForeignKey("dbo.ContractMasters", t => t.ContractMaster_ContractMasterId)
                .Index(t => t.City_CityId)
                .Index(t => t.Employee_EmployeeId)
                .Index(t => t.ContractMaster_ContractMasterId);
            
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
                        EducationStand = c.Int(nullable: false),
                        EmploymentType = c.Int(nullable: false),
                        SacrificeStand = c.Int(nullable: false),
                        AccountNoGov = c.String(),
                        AccountNoEmp = c.String(),
                        IsCurrentContract = c.Boolean(nullable: false),
                        Employee_EmployeeId = c.Int(),
                        SubGroup_SubGroupId = c.Int(),
                        Job_JobId = c.Int(),
                    })
                .PrimaryKey(t => t.ContractMasterId)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeId)
                .ForeignKey("dbo.SubGroups", t => t.SubGroup_SubGroupId)
                .ForeignKey("dbo.Jobs", t => t.Job_JobId)
                .Index(t => t.Employee_EmployeeId)
                .Index(t => t.SubGroup_SubGroupId)
                .Index(t => t.Job_JobId);
            
            CreateTable(
                "dbo.ContractDetails",
                c => new
                    {
                        ContractDetailId = c.Int(nullable: false, identity: true),
                        Value = c.Single(nullable: false),
                        ContractField_ContractFieldId = c.Int(),
                        ContractMaster_ContractMasterId = c.Int(),
                    })
                .PrimaryKey(t => t.ContractDetailId)
                .ForeignKey("dbo.ContractFields", t => t.ContractField_ContractFieldId)
                .ForeignKey("dbo.ContractMasters", t => t.ContractMaster_ContractMasterId)
                .Index(t => t.ContractField_ContractFieldId)
                .Index(t => t.ContractMaster_ContractMasterId);
            
            CreateTable(
                "dbo.ContractFields",
                c => new
                    {
                        ContractFieldId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Year = c.Int(nullable: false),
                        SubGroup_SubGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.ContractFieldId)
                .ForeignKey("dbo.SubGroups", t => t.SubGroup_SubGroupId)
                .Index(t => t.SubGroup_SubGroupId);
            
            CreateTable(
                "dbo.ExpenseArticleOfContractFieldForSubGroups",
                c => new
                    {
                        ExpenseArticleOfContractFieldForSubGroupId = c.Int(nullable: false, identity: true),
                        Month = c.Int(nullable: false),
                        ContractField_ContractFieldId = c.Int(),
                        ExpenseArticle_ExpenseArticleId = c.Int(),
                        SubGroup_SubGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.ExpenseArticleOfContractFieldForSubGroupId)
                .ForeignKey("dbo.ContractFields", t => t.ContractField_ContractFieldId)
                .ForeignKey("dbo.ExpenseArticles", t => t.ExpenseArticle_ExpenseArticleId)
                .ForeignKey("dbo.SubGroups", t => t.SubGroup_SubGroupId)
                .Index(t => t.ContractField_ContractFieldId)
                .Index(t => t.ExpenseArticle_ExpenseArticleId)
                .Index(t => t.SubGroup_SubGroupId);
            
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
                "dbo.ExpenseArticleOfMiscForSubGroups",
                c => new
                    {
                        ExpenseArticleOfMiscForSubGroupId = c.Int(nullable: false, identity: true),
                        Month = c.Int(nullable: false),
                        ExpenseArticle_ExpenseArticleId = c.Int(),
                        Misc_MiscId = c.Int(),
                        SubGroup_SubGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.ExpenseArticleOfMiscForSubGroupId)
                .ForeignKey("dbo.ExpenseArticles", t => t.ExpenseArticle_ExpenseArticleId)
                .ForeignKey("dbo.Miscs", t => t.Misc_MiscId)
                .ForeignKey("dbo.SubGroups", t => t.SubGroup_SubGroupId)
                .Index(t => t.ExpenseArticle_ExpenseArticleId)
                .Index(t => t.Misc_MiscId)
                .Index(t => t.SubGroup_SubGroupId);
            
            CreateTable(
                "dbo.Miscs",
                c => new
                    {
                        MiscId = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Title = c.String(),
                        IsPayment = c.Boolean(nullable: false),
                        Index = c.Int(nullable: false),
                        SubGroup_SubGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.MiscId)
                .ForeignKey("dbo.SubGroups", t => t.SubGroup_SubGroupId)
                .Index(t => t.SubGroup_SubGroupId);
            
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
                .PrimaryKey(t => t.EmployeeMiscRemainId)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeId)
                .ForeignKey("dbo.Miscs", t => t.Misc_MiscId)
                .Index(t => t.Employee_EmployeeId)
                .Index(t => t.Misc_MiscId);
            
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
                        IdCardExportPlace = c.String(),
                        IdCardExportDate = c.String(),
                        IdCardNo = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.PayslipEmployeeMiscs",
                c => new
                    {
                        PayslipEmployeeMiscId = c.Int(nullable: false, identity: true),
                        Value = c.Single(nullable: false),
                        PayslipValue = c.Single(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Employee_EmployeeId = c.Int(),
                        Misc_MiscId = c.Int(),
                    })
                .PrimaryKey(t => t.PayslipEmployeeMiscId)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeId)
                .ForeignKey("dbo.Miscs", t => t.Misc_MiscId)
                .Index(t => t.Employee_EmployeeId)
                .Index(t => t.Misc_MiscId);
            
            CreateTable(
                "dbo.PayslipEmployeeOvertimes",
                c => new
                    {
                        PayslipEmployeeOvertimeId = c.Int(nullable: false, identity: true),
                        Value = c.Single(nullable: false),
                        PayslipValue = c.Single(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Employee_EmployeeId = c.Int(),
                    })
                .PrimaryKey(t => t.PayslipEmployeeOvertimeId)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeId)
                .Index(t => t.Employee_EmployeeId);
            
            CreateTable(
                "dbo.ParameterInvolvedMiscs",
                c => new
                    {
                        ParameterInvolvedMiscId = c.Int(nullable: false, identity: true),
                        Misc_MiscId = c.Int(),
                        Parameter_ParameterId = c.Int(),
                    })
                .PrimaryKey(t => t.ParameterInvolvedMiscId)
                .ForeignKey("dbo.Miscs", t => t.Misc_MiscId)
                .ForeignKey("dbo.Parameters", t => t.Parameter_ParameterId)
                .Index(t => t.Misc_MiscId)
                .Index(t => t.Parameter_ParameterId);
            
            CreateTable(
                "dbo.Parameters",
                c => new
                    {
                        ParameterId = c.Int(nullable: false, identity: true),
                        Value = c.Single(nullable: false),
                        ValueType = c.Int(nullable: false),
                        Title = c.String(),
                        Alias = c.String(),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        SubGroup_SubGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.ParameterId)
                .ForeignKey("dbo.SubGroups", t => t.SubGroup_SubGroupId)
                .Index(t => t.SubGroup_SubGroupId);
            
            CreateTable(
                "dbo.ParameterInvolvedContractFields",
                c => new
                    {
                        ParameterInvolvedContractFieldId = c.Int(nullable: false, identity: true),
                        ContractField_ContractFieldId = c.Int(),
                        Parameter_ParameterId = c.Int(),
                    })
                .PrimaryKey(t => t.ParameterInvolvedContractFieldId)
                .ForeignKey("dbo.ContractFields", t => t.ContractField_ContractFieldId)
                .ForeignKey("dbo.Parameters", t => t.Parameter_ParameterId)
                .Index(t => t.ContractField_ContractFieldId)
                .Index(t => t.Parameter_ParameterId);
            
            CreateTable(
                "dbo.SubGroups",
                c => new
                    {
                        SubGroupId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ItemColor = c.Int(nullable: false),
                        Is31 = c.Boolean(nullable: false),
                        MainGroup_MainGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubGroupId)
                .ForeignKey("dbo.MainGroups", t => t.MainGroup_MainGroupId, cascadeDelete: true)
                .Index(t => t.MainGroup_MainGroupId);
            
            CreateTable(
                "dbo.ExpenseArticleOfOverTimeForSubGroups",
                c => new
                    {
                        ExpenseArticleOfOverTimeForSubGroupId = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        ExpenseArticle_ExpenseArticleId = c.Int(),
                        SubGroup_SubGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.ExpenseArticleOfOverTimeForSubGroupId)
                .ForeignKey("dbo.ExpenseArticles", t => t.ExpenseArticle_ExpenseArticleId)
                .ForeignKey("dbo.SubGroups", t => t.SubGroup_SubGroupId)
                .Index(t => t.ExpenseArticle_ExpenseArticleId)
                .Index(t => t.SubGroup_SubGroupId);
            
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
                "dbo.MainGroups",
                c => new
                    {
                        MainGroupId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ItemColor = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MainGroupId);
            
            CreateTable(
                "dbo.MissionFormulas",
                c => new
                    {
                        MissionFormulaId = c.Int(nullable: false, identity: true),
                        DivideFactor = c.Single(nullable: false),
                        AddFactor = c.Single(nullable: false),
                        MaxFactor = c.Single(nullable: false),
                        SubtractFactor = c.Single(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        SubGroup_SubGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.MissionFormulaId)
                .ForeignKey("dbo.SubGroups", t => t.SubGroup_SubGroupId)
                .Index(t => t.SubGroup_SubGroupId);
            
            CreateTable(
                "dbo.MissionFormulaInvolvedContractFields",
                c => new
                    {
                        MissionFormulaInvolvedContractFieldId = c.Int(nullable: false, identity: true),
                        ContractField_ContractFieldId = c.Int(),
                        MissionFormula_MissionFormulaId = c.Int(),
                    })
                .PrimaryKey(t => t.MissionFormulaInvolvedContractFieldId)
                .ForeignKey("dbo.ContractFields", t => t.ContractField_ContractFieldId)
                .ForeignKey("dbo.MissionFormulas", t => t.MissionFormula_MissionFormulaId)
                .Index(t => t.ContractField_ContractFieldId)
                .Index(t => t.MissionFormula_MissionFormulaId);
            
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
                "dbo.PayslipContractDetails",
                c => new
                    {
                        PayslipContractDetailId = c.Int(nullable: false, identity: true),
                        PayslipValue = c.Single(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        ContractDetail_ContractDetailId = c.Int(),
                    })
                .PrimaryKey(t => t.PayslipContractDetailId)
                .ForeignKey("dbo.ContractDetails", t => t.ContractDetail_ContractDetailId)
                .Index(t => t.ContractDetail_ContractDetailId);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        JobId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        JobNo = c.String(),
                        Description = c.String(),
                        ItemColor = c.Int(nullable: false),
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
            DropForeignKey("dbo.Missions", "ContractMaster_ContractMasterId", "dbo.ContractMasters");
            DropForeignKey("dbo.ContractMasters", "Job_JobId", "dbo.Jobs");
            DropForeignKey("dbo.PayslipContractDetails", "ContractDetail_ContractDetailId", "dbo.ContractDetails");
            DropForeignKey("dbo.ContractDetails", "ContractMaster_ContractMasterId", "dbo.ContractMasters");
            DropForeignKey("dbo.TaxRows", "TaxTable_TaxTableId", "dbo.TaxTables");
            DropForeignKey("dbo.TaxTables", "SubGroup_SubGroupId", "dbo.SubGroups");
            DropForeignKey("dbo.Parameters", "SubGroup_SubGroupId", "dbo.SubGroups");
            DropForeignKey("dbo.MissionFormulas", "SubGroup_SubGroupId", "dbo.SubGroups");
            DropForeignKey("dbo.MissionFormulaInvolvedContractFields", "MissionFormula_MissionFormulaId", "dbo.MissionFormulas");
            DropForeignKey("dbo.MissionFormulaInvolvedContractFields", "ContractField_ContractFieldId", "dbo.ContractFields");
            DropForeignKey("dbo.Miscs", "SubGroup_SubGroupId", "dbo.SubGroups");
            DropForeignKey("dbo.SubGroups", "MainGroup_MainGroupId", "dbo.MainGroups");
            DropForeignKey("dbo.HandselFormulas", "SubGroup_SubGroupId", "dbo.SubGroups");
            DropForeignKey("dbo.ExpenseArticleOfOverTimeForSubGroups", "SubGroup_SubGroupId", "dbo.SubGroups");
            DropForeignKey("dbo.ExpenseArticleOfOverTimeForSubGroups", "ExpenseArticle_ExpenseArticleId", "dbo.ExpenseArticles");
            DropForeignKey("dbo.ExpenseArticleOfMiscForSubGroups", "SubGroup_SubGroupId", "dbo.SubGroups");
            DropForeignKey("dbo.ExpenseArticleOfContractFieldForSubGroups", "SubGroup_SubGroupId", "dbo.SubGroups");
            DropForeignKey("dbo.ContractMasters", "SubGroup_SubGroupId", "dbo.SubGroups");
            DropForeignKey("dbo.ContractFields", "SubGroup_SubGroupId", "dbo.SubGroups");
            DropForeignKey("dbo.ParameterInvolvedMiscs", "Parameter_ParameterId", "dbo.Parameters");
            DropForeignKey("dbo.ParameterInvolvedContractFields", "Parameter_ParameterId", "dbo.Parameters");
            DropForeignKey("dbo.ParameterInvolvedContractFields", "ContractField_ContractFieldId", "dbo.ContractFields");
            DropForeignKey("dbo.ParameterInvolvedMiscs", "Misc_MiscId", "dbo.Miscs");
            DropForeignKey("dbo.ExpenseArticleOfMiscForSubGroups", "Misc_MiscId", "dbo.Miscs");
            DropForeignKey("dbo.EmployeeMiscRemains", "Misc_MiscId", "dbo.Miscs");
            DropForeignKey("dbo.PayslipEmployeeOvertimes", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.PayslipEmployeeMiscs", "Misc_MiscId", "dbo.Miscs");
            DropForeignKey("dbo.PayslipEmployeeMiscs", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Missions", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.EmployeeMiscRemains", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.ContractMasters", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.ExpenseArticleOfMiscForSubGroups", "ExpenseArticle_ExpenseArticleId", "dbo.ExpenseArticles");
            DropForeignKey("dbo.ExpenseArticleOfContractFieldForSubGroups", "ExpenseArticle_ExpenseArticleId", "dbo.ExpenseArticles");
            DropForeignKey("dbo.ExpenseArticleOfContractFieldForSubGroups", "ContractField_ContractFieldId", "dbo.ContractFields");
            DropForeignKey("dbo.ContractDetails", "ContractField_ContractFieldId", "dbo.ContractFields");
            DropForeignKey("dbo.Missions", "City_CityId", "dbo.Cities");
            DropIndex("dbo.ContractDifferences", new[] { "Contract2Nd_ContractMasterId" });
            DropIndex("dbo.ContractDifferences", new[] { "ContractMasterId" });
            DropIndex("dbo.PayslipContractDetails", new[] { "ContractDetail_ContractDetailId" });
            DropIndex("dbo.TaxRows", new[] { "TaxTable_TaxTableId" });
            DropIndex("dbo.TaxTables", new[] { "SubGroup_SubGroupId" });
            DropIndex("dbo.MissionFormulaInvolvedContractFields", new[] { "MissionFormula_MissionFormulaId" });
            DropIndex("dbo.MissionFormulaInvolvedContractFields", new[] { "ContractField_ContractFieldId" });
            DropIndex("dbo.MissionFormulas", new[] { "SubGroup_SubGroupId" });
            DropIndex("dbo.HandselFormulas", new[] { "SubGroup_SubGroupId" });
            DropIndex("dbo.ExpenseArticleOfOverTimeForSubGroups", new[] { "SubGroup_SubGroupId" });
            DropIndex("dbo.ExpenseArticleOfOverTimeForSubGroups", new[] { "ExpenseArticle_ExpenseArticleId" });
            DropIndex("dbo.SubGroups", new[] { "MainGroup_MainGroupId" });
            DropIndex("dbo.ParameterInvolvedContractFields", new[] { "Parameter_ParameterId" });
            DropIndex("dbo.ParameterInvolvedContractFields", new[] { "ContractField_ContractFieldId" });
            DropIndex("dbo.Parameters", new[] { "SubGroup_SubGroupId" });
            DropIndex("dbo.ParameterInvolvedMiscs", new[] { "Parameter_ParameterId" });
            DropIndex("dbo.ParameterInvolvedMiscs", new[] { "Misc_MiscId" });
            DropIndex("dbo.PayslipEmployeeOvertimes", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.PayslipEmployeeMiscs", new[] { "Misc_MiscId" });
            DropIndex("dbo.PayslipEmployeeMiscs", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.EmployeeMiscRemains", new[] { "Misc_MiscId" });
            DropIndex("dbo.EmployeeMiscRemains", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.Miscs", new[] { "SubGroup_SubGroupId" });
            DropIndex("dbo.ExpenseArticleOfMiscForSubGroups", new[] { "SubGroup_SubGroupId" });
            DropIndex("dbo.ExpenseArticleOfMiscForSubGroups", new[] { "Misc_MiscId" });
            DropIndex("dbo.ExpenseArticleOfMiscForSubGroups", new[] { "ExpenseArticle_ExpenseArticleId" });
            DropIndex("dbo.ExpenseArticleOfContractFieldForSubGroups", new[] { "SubGroup_SubGroupId" });
            DropIndex("dbo.ExpenseArticleOfContractFieldForSubGroups", new[] { "ExpenseArticle_ExpenseArticleId" });
            DropIndex("dbo.ExpenseArticleOfContractFieldForSubGroups", new[] { "ContractField_ContractFieldId" });
            DropIndex("dbo.ContractFields", new[] { "SubGroup_SubGroupId" });
            DropIndex("dbo.ContractDetails", new[] { "ContractMaster_ContractMasterId" });
            DropIndex("dbo.ContractDetails", new[] { "ContractField_ContractFieldId" });
            DropIndex("dbo.ContractMasters", new[] { "Job_JobId" });
            DropIndex("dbo.ContractMasters", new[] { "SubGroup_SubGroupId" });
            DropIndex("dbo.ContractMasters", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.Missions", new[] { "ContractMaster_ContractMasterId" });
            DropIndex("dbo.Missions", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.Missions", new[] { "City_CityId" });
            DropTable("dbo.ContractDifferences");
            DropTable("dbo.Jobs");
            DropTable("dbo.PayslipContractDetails");
            DropTable("dbo.TaxRows");
            DropTable("dbo.TaxTables");
            DropTable("dbo.MissionFormulaInvolvedContractFields");
            DropTable("dbo.MissionFormulas");
            DropTable("dbo.MainGroups");
            DropTable("dbo.HandselFormulas");
            DropTable("dbo.ExpenseArticleOfOverTimeForSubGroups");
            DropTable("dbo.SubGroups");
            DropTable("dbo.ParameterInvolvedContractFields");
            DropTable("dbo.Parameters");
            DropTable("dbo.ParameterInvolvedMiscs");
            DropTable("dbo.PayslipEmployeeOvertimes");
            DropTable("dbo.PayslipEmployeeMiscs");
            DropTable("dbo.Employees");
            DropTable("dbo.EmployeeMiscRemains");
            DropTable("dbo.Miscs");
            DropTable("dbo.ExpenseArticleOfMiscForSubGroups");
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
