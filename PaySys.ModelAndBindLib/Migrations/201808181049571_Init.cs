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
                        Percentage = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.CityId);
            
            CreateTable(
                "dbo.Missions",
                c => new
                    {
                        MissionId = c.Int(nullable: false, identity: true),
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
                        City_CityId = c.Int(),
                        ContractMaster_ContractMasterId = c.Int(),
                    })
                .PrimaryKey(t => t.MissionId)
                .ForeignKey("dbo.Cities", t => t.City_CityId)
                .ForeignKey("dbo.ContractMasters", t => t.ContractMaster_ContractMasterId)
                .Index(t => t.City_CityId)
                .Index(t => t.ContractMaster_ContractMasterId);
            
            CreateTable(
                "dbo.ContractMasters",
                c => new
                    {
                        ContractMasterId = c.Int(nullable: false, identity: true),
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
                        AccountNoGov = c.String(),
                        AccountNoEmp = c.String(),
                        IsCurrent = c.Boolean(nullable: false),
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
                        Value = c.Double(nullable: false),
                        ContractMaster_ContractMasterId = c.Int(),
                        SubGroupContractField_SubGroupContractFieldId = c.Int(),
                    })
                .PrimaryKey(t => t.ContractDetailId)
                .ForeignKey("dbo.ContractMasters", t => t.ContractMaster_ContractMasterId)
                .ForeignKey("dbo.SubGroupContractFields", t => t.SubGroupContractField_SubGroupContractFieldId)
                .Index(t => t.ContractMaster_ContractMasterId)
                .Index(t => t.SubGroupContractField_SubGroupContractFieldId);
            
            CreateTable(
                "dbo.SubGroupContractFields",
                c => new
                    {
                        SubGroupContractFieldId = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        ContractFieldTitle_ContractFieldTitleId = c.Int(),
                        SubGroup_SubGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.SubGroupContractFieldId)
                .ForeignKey("dbo.ContractFieldTitles", t => t.ContractFieldTitle_ContractFieldTitleId)
                .ForeignKey("dbo.SubGroups", t => t.SubGroup_SubGroupId)
                .Index(t => t.ContractFieldTitle_ContractFieldTitleId)
                .Index(t => t.SubGroup_SubGroupId);
            
            CreateTable(
                "dbo.ContractFieldTitles",
                c => new
                    {
                        ContractFieldTitleId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Alias = c.String(),
                    })
                .PrimaryKey(t => t.ContractFieldTitleId);
            
            CreateTable(
                "dbo.ExpenseArticleOfContractFieldForSubGroups",
                c => new
                    {
                        ExpenseArticleOfContractFieldForSubGroupId = c.Int(nullable: false, identity: true),
                        Month = c.Int(nullable: false),
                        ExpenseArticle_ExpenseArticleId = c.Int(),
                        SubGroupContractField_SubGroupContractFieldId = c.Int(),
                    })
                .PrimaryKey(t => t.ExpenseArticleOfContractFieldForSubGroupId)
                .ForeignKey("dbo.ExpenseArticles", t => t.ExpenseArticle_ExpenseArticleId)
                .ForeignKey("dbo.SubGroupContractFields", t => t.SubGroupContractField_SubGroupContractFieldId)
                .Index(t => t.ExpenseArticle_ExpenseArticleId)
                .Index(t => t.SubGroupContractField_SubGroupContractFieldId);
            
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
                "dbo.Miscs",
                c => new
                    {
                        MiscId = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Index = c.Int(nullable: false),
                        ExpenseArticle_ExpenseArticleId = c.Int(),
                        SubGroup_SubGroupId = c.Int(),
                        MiscTitle_MiscTitleId = c.Int(),
                    })
                .PrimaryKey(t => t.MiscId)
                .ForeignKey("dbo.ExpenseArticles", t => t.ExpenseArticle_ExpenseArticleId)
                .ForeignKey("dbo.SubGroups", t => t.SubGroup_SubGroupId)
                .ForeignKey("dbo.MiscTitles", t => t.MiscTitle_MiscTitleId)
                .Index(t => t.ExpenseArticle_ExpenseArticleId)
                .Index(t => t.SubGroup_SubGroupId)
                .Index(t => t.MiscTitle_MiscTitleId);
            
            CreateTable(
                "dbo.MiscRecharges",
                c => new
                    {
                        MiscRechargeId = c.Int(nullable: false, identity: true),
                        Value = c.Double(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Employee_EmployeeId = c.Int(),
                        Misc_MiscId = c.Int(),
                    })
                .PrimaryKey(t => t.MiscRechargeId)
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
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.MiscValueForEmployees",
                c => new
                    {
                        MiscValueForEmployeeId = c.Int(nullable: false, identity: true),
                        Value = c.Double(nullable: false),
                        ValueSubtraction = c.Double(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Employee_EmployeeId = c.Int(),
                        Misc_MiscId = c.Int(),
                    })
                .PrimaryKey(t => t.MiscValueForEmployeeId)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeId)
                .ForeignKey("dbo.Miscs", t => t.Misc_MiscId)
                .Index(t => t.Employee_EmployeeId)
                .Index(t => t.Misc_MiscId);
            
            CreateTable(
                "dbo.VariableValueForEmployees",
                c => new
                    {
                        VariableValueForEmployeeId = c.Int(nullable: false, identity: true),
                        NumericValue = c.Double(),
                        StringValue = c.String(),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        DateValue = c.DateTime(),
                        Employee_EmployeeId = c.Int(),
                        SubGroupVariable_SubGroupVariableId = c.Int(),
                    })
                .PrimaryKey(t => t.VariableValueForEmployeeId)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeId)
                .ForeignKey("dbo.SubGroupVariables", t => t.SubGroupVariable_SubGroupVariableId)
                .Index(t => t.Employee_EmployeeId)
                .Index(t => t.SubGroupVariable_SubGroupVariableId);
            
            CreateTable(
                "dbo.SubGroupVariables",
                c => new
                    {
                        SubGroupVariableId = c.Int(nullable: false, identity: true),
                        FromYear = c.Int(nullable: false),
                        FromMonth = c.Int(nullable: false),
                        ToYear = c.Int(nullable: false),
                        ToMonth = c.Int(nullable: false),
                        ExpenseArticle_ExpenseArticleId = c.Int(),
                        SubGroup_SubGroupId = c.Int(),
                        VariableTitle_VariableTitleId = c.Int(),
                    })
                .PrimaryKey(t => t.SubGroupVariableId)
                .ForeignKey("dbo.ExpenseArticles", t => t.ExpenseArticle_ExpenseArticleId)
                .ForeignKey("dbo.SubGroups", t => t.SubGroup_SubGroupId)
                .ForeignKey("dbo.VariableTitles", t => t.VariableTitle_VariableTitleId)
                .Index(t => t.ExpenseArticle_ExpenseArticleId)
                .Index(t => t.SubGroup_SubGroupId)
                .Index(t => t.VariableTitle_VariableTitleId);
            
            CreateTable(
                "dbo.SubGroups",
                c => new
                    {
                        SubGroupId = c.Int(nullable: false, identity: true),
                        Alias = c.String(),
                        Title = c.String(),
                        ItemColor = c.Int(nullable: false),
                        Is31 = c.Boolean(nullable: false),
                        IsFreeZone = c.Boolean(nullable: false),
                        MainGroup_MainGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubGroupId)
                .ForeignKey("dbo.MainGroups", t => t.MainGroup_MainGroupId, cascadeDelete: true)
                .Index(t => t.MainGroup_MainGroupId);
            
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
                        Alias = c.String(),
                        ItemColor = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MainGroupId);
            
            CreateTable(
                "dbo.MissionFormulas",
                c => new
                    {
                        MissionFormulaId = c.Int(nullable: false, identity: true),
                        DivideFactor = c.Double(nullable: false),
                        AddFactor = c.Double(nullable: false),
                        MaxFactor = c.Double(nullable: false),
                        SubtractFactor = c.Double(nullable: false),
                        PerKmFactor = c.Double(nullable: false),
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
                        MissionFormula_MissionFormulaId = c.Int(),
                        SubGroupContractField_SubGroupContractFieldId = c.Int(),
                    })
                .PrimaryKey(t => t.MissionFormulaInvolvedContractFieldId)
                .ForeignKey("dbo.MissionFormulas", t => t.MissionFormula_MissionFormulaId)
                .ForeignKey("dbo.SubGroupContractFields", t => t.SubGroupContractField_SubGroupContractFieldId)
                .Index(t => t.MissionFormula_MissionFormulaId)
                .Index(t => t.SubGroupContractField_SubGroupContractFieldId);
            
            CreateTable(
                "dbo.Parameters",
                c => new
                    {
                        ParameterId = c.Int(nullable: false, identity: true),
                        Value = c.Double(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        ParameterTitle_ParameterTitleId = c.Int(),
                        SubGroup_SubGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.ParameterId)
                .ForeignKey("dbo.ParameterTitles", t => t.ParameterTitle_ParameterTitleId)
                .ForeignKey("dbo.SubGroups", t => t.SubGroup_SubGroupId)
                .Index(t => t.ParameterTitle_ParameterTitleId)
                .Index(t => t.SubGroup_SubGroupId);
            
            CreateTable(
                "dbo.ParameterInvolvedContractFields",
                c => new
                    {
                        ParameterInvolvedContractFieldId = c.Int(nullable: false, identity: true),
                        Parameter_ParameterId = c.Int(),
                        SubGroupContractField_SubGroupContractFieldId = c.Int(),
                    })
                .PrimaryKey(t => t.ParameterInvolvedContractFieldId)
                .ForeignKey("dbo.Parameters", t => t.Parameter_ParameterId)
                .ForeignKey("dbo.SubGroupContractFields", t => t.SubGroupContractField_SubGroupContractFieldId)
                .Index(t => t.Parameter_ParameterId)
                .Index(t => t.SubGroupContractField_SubGroupContractFieldId);
            
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
                "dbo.ParameterTitles",
                c => new
                    {
                        ParameterTitleId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ValueType = c.Int(nullable: false),
                        Alias = c.String(),
                        ParameterTitle_ParameterTitleId = c.Int(),
                    })
                .PrimaryKey(t => t.ParameterTitleId)
                .ForeignKey("dbo.ParameterTitles", t => t.ParameterTitle_ParameterTitleId)
                .Index(t => t.ParameterTitle_ParameterTitleId);
            
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
                        ValueTo = c.Double(nullable: false),
                        Factor = c.Double(nullable: false),
                        TaxTable_TaxTableId = c.Int(),
                    })
                .PrimaryKey(t => t.TaxRowId)
                .ForeignKey("dbo.TaxTables", t => t.TaxTable_TaxTableId)
                .Index(t => t.TaxTable_TaxTableId);
            
            CreateTable(
                "dbo.VariableTitles",
                c => new
                    {
                        VariableTitleId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Alias = c.String(),
                        ValueType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VariableTitleId);
            
            CreateTable(
                "dbo.MiscTitles",
                c => new
                    {
                        MiscTitleId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Alias = c.String(),
                        IsPayment = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MiscTitleId);
            
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
                        DateFrom = c.DateTime(nullable: false),
                        DateTo = c.DateTime(nullable: false),
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
            DropForeignKey("dbo.ExpenseArticleOfContractFieldForSubGroups", "SubGroupContractField_SubGroupContractFieldId", "dbo.SubGroupContractFields");
            DropForeignKey("dbo.Miscs", "MiscTitle_MiscTitleId", "dbo.MiscTitles");
            DropForeignKey("dbo.MiscRecharges", "Misc_MiscId", "dbo.Miscs");
            DropForeignKey("dbo.VariableValueForEmployees", "SubGroupVariable_SubGroupVariableId", "dbo.SubGroupVariables");
            DropForeignKey("dbo.SubGroupVariables", "VariableTitle_VariableTitleId", "dbo.VariableTitles");
            DropForeignKey("dbo.TaxRows", "TaxTable_TaxTableId", "dbo.TaxTables");
            DropForeignKey("dbo.TaxTables", "SubGroup_SubGroupId", "dbo.SubGroups");
            DropForeignKey("dbo.SubGroupVariables", "SubGroup_SubGroupId", "dbo.SubGroups");
            DropForeignKey("dbo.SubGroupContractFields", "SubGroup_SubGroupId", "dbo.SubGroups");
            DropForeignKey("dbo.Parameters", "SubGroup_SubGroupId", "dbo.SubGroups");
            DropForeignKey("dbo.Parameters", "ParameterTitle_ParameterTitleId", "dbo.ParameterTitles");
            DropForeignKey("dbo.ParameterTitles", "ParameterTitle_ParameterTitleId", "dbo.ParameterTitles");
            DropForeignKey("dbo.ParameterInvolvedMiscs", "Parameter_ParameterId", "dbo.Parameters");
            DropForeignKey("dbo.ParameterInvolvedMiscs", "Misc_MiscId", "dbo.Miscs");
            DropForeignKey("dbo.ParameterInvolvedContractFields", "SubGroupContractField_SubGroupContractFieldId", "dbo.SubGroupContractFields");
            DropForeignKey("dbo.ParameterInvolvedContractFields", "Parameter_ParameterId", "dbo.Parameters");
            DropForeignKey("dbo.MissionFormulas", "SubGroup_SubGroupId", "dbo.SubGroups");
            DropForeignKey("dbo.MissionFormulaInvolvedContractFields", "SubGroupContractField_SubGroupContractFieldId", "dbo.SubGroupContractFields");
            DropForeignKey("dbo.MissionFormulaInvolvedContractFields", "MissionFormula_MissionFormulaId", "dbo.MissionFormulas");
            DropForeignKey("dbo.Miscs", "SubGroup_SubGroupId", "dbo.SubGroups");
            DropForeignKey("dbo.SubGroups", "MainGroup_MainGroupId", "dbo.MainGroups");
            DropForeignKey("dbo.HandselFormulas", "SubGroup_SubGroupId", "dbo.SubGroups");
            DropForeignKey("dbo.ContractMasters", "SubGroup_SubGroupId", "dbo.SubGroups");
            DropForeignKey("dbo.SubGroupVariables", "ExpenseArticle_ExpenseArticleId", "dbo.ExpenseArticles");
            DropForeignKey("dbo.VariableValueForEmployees", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.MiscValueForEmployees", "Misc_MiscId", "dbo.Miscs");
            DropForeignKey("dbo.MiscValueForEmployees", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.MiscRecharges", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.ContractMasters", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Miscs", "ExpenseArticle_ExpenseArticleId", "dbo.ExpenseArticles");
            DropForeignKey("dbo.ExpenseArticleOfContractFieldForSubGroups", "ExpenseArticle_ExpenseArticleId", "dbo.ExpenseArticles");
            DropForeignKey("dbo.SubGroupContractFields", "ContractFieldTitle_ContractFieldTitleId", "dbo.ContractFieldTitles");
            DropForeignKey("dbo.ContractDetails", "SubGroupContractField_SubGroupContractFieldId", "dbo.SubGroupContractFields");
            DropForeignKey("dbo.ContractDetails", "ContractMaster_ContractMasterId", "dbo.ContractMasters");
            DropForeignKey("dbo.Missions", "City_CityId", "dbo.Cities");
            DropIndex("dbo.ContractDifferences", new[] { "Contract2Nd_ContractMasterId" });
            DropIndex("dbo.ContractDifferences", new[] { "ContractMasterId" });
            DropIndex("dbo.TaxRows", new[] { "TaxTable_TaxTableId" });
            DropIndex("dbo.TaxTables", new[] { "SubGroup_SubGroupId" });
            DropIndex("dbo.ParameterTitles", new[] { "ParameterTitle_ParameterTitleId" });
            DropIndex("dbo.ParameterInvolvedMiscs", new[] { "Parameter_ParameterId" });
            DropIndex("dbo.ParameterInvolvedMiscs", new[] { "Misc_MiscId" });
            DropIndex("dbo.ParameterInvolvedContractFields", new[] { "SubGroupContractField_SubGroupContractFieldId" });
            DropIndex("dbo.ParameterInvolvedContractFields", new[] { "Parameter_ParameterId" });
            DropIndex("dbo.Parameters", new[] { "SubGroup_SubGroupId" });
            DropIndex("dbo.Parameters", new[] { "ParameterTitle_ParameterTitleId" });
            DropIndex("dbo.MissionFormulaInvolvedContractFields", new[] { "SubGroupContractField_SubGroupContractFieldId" });
            DropIndex("dbo.MissionFormulaInvolvedContractFields", new[] { "MissionFormula_MissionFormulaId" });
            DropIndex("dbo.MissionFormulas", new[] { "SubGroup_SubGroupId" });
            DropIndex("dbo.HandselFormulas", new[] { "SubGroup_SubGroupId" });
            DropIndex("dbo.SubGroups", new[] { "MainGroup_MainGroupId" });
            DropIndex("dbo.SubGroupVariables", new[] { "VariableTitle_VariableTitleId" });
            DropIndex("dbo.SubGroupVariables", new[] { "SubGroup_SubGroupId" });
            DropIndex("dbo.SubGroupVariables", new[] { "ExpenseArticle_ExpenseArticleId" });
            DropIndex("dbo.VariableValueForEmployees", new[] { "SubGroupVariable_SubGroupVariableId" });
            DropIndex("dbo.VariableValueForEmployees", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.MiscValueForEmployees", new[] { "Misc_MiscId" });
            DropIndex("dbo.MiscValueForEmployees", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.MiscRecharges", new[] { "Misc_MiscId" });
            DropIndex("dbo.MiscRecharges", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.Miscs", new[] { "MiscTitle_MiscTitleId" });
            DropIndex("dbo.Miscs", new[] { "SubGroup_SubGroupId" });
            DropIndex("dbo.Miscs", new[] { "ExpenseArticle_ExpenseArticleId" });
            DropIndex("dbo.ExpenseArticleOfContractFieldForSubGroups", new[] { "SubGroupContractField_SubGroupContractFieldId" });
            DropIndex("dbo.ExpenseArticleOfContractFieldForSubGroups", new[] { "ExpenseArticle_ExpenseArticleId" });
            DropIndex("dbo.SubGroupContractFields", new[] { "SubGroup_SubGroupId" });
            DropIndex("dbo.SubGroupContractFields", new[] { "ContractFieldTitle_ContractFieldTitleId" });
            DropIndex("dbo.ContractDetails", new[] { "SubGroupContractField_SubGroupContractFieldId" });
            DropIndex("dbo.ContractDetails", new[] { "ContractMaster_ContractMasterId" });
            DropIndex("dbo.ContractMasters", new[] { "Job_JobId" });
            DropIndex("dbo.ContractMasters", new[] { "SubGroup_SubGroupId" });
            DropIndex("dbo.ContractMasters", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.Missions", new[] { "ContractMaster_ContractMasterId" });
            DropIndex("dbo.Missions", new[] { "City_CityId" });
            DropTable("dbo.ContractDifferences");
            DropTable("dbo.Jobs");
            DropTable("dbo.MiscTitles");
            DropTable("dbo.VariableTitles");
            DropTable("dbo.TaxRows");
            DropTable("dbo.TaxTables");
            DropTable("dbo.ParameterTitles");
            DropTable("dbo.ParameterInvolvedMiscs");
            DropTable("dbo.ParameterInvolvedContractFields");
            DropTable("dbo.Parameters");
            DropTable("dbo.MissionFormulaInvolvedContractFields");
            DropTable("dbo.MissionFormulas");
            DropTable("dbo.MainGroups");
            DropTable("dbo.HandselFormulas");
            DropTable("dbo.SubGroups");
            DropTable("dbo.SubGroupVariables");
            DropTable("dbo.VariableValueForEmployees");
            DropTable("dbo.MiscValueForEmployees");
            DropTable("dbo.Employees");
            DropTable("dbo.MiscRecharges");
            DropTable("dbo.Miscs");
            DropTable("dbo.ExpenseArticles");
            DropTable("dbo.ExpenseArticleOfContractFieldForSubGroups");
            DropTable("dbo.ContractFieldTitles");
            DropTable("dbo.SubGroupContractFields");
            DropTable("dbo.ContractDetails");
            DropTable("dbo.ContractMasters");
            DropTable("dbo.Missions");
            DropTable("dbo.Cities");
        }
    }
}
