namespace PaySys.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccessForms",
                c => new
                    {
                        AccessFormId = c.Int(nullable: false, identity: true),
                        SelectAccess = c.Boolean(nullable: false),
                        UpdateAccess = c.Boolean(nullable: false),
                        InsertAccess = c.Boolean(nullable: false),
                        DeleteAccess = c.Boolean(nullable: false),
                        Rule_RuleId = c.Int(),
                        SysForm_SysFormId = c.Int(),
                    })
                .PrimaryKey(t => t.AccessFormId)
                .ForeignKey("dbo.Rules", t => t.Rule_RuleId)
                .ForeignKey("dbo.SysForms", t => t.SysForm_SysFormId)
                .Index(t => t.Rule_RuleId)
                .Index(t => t.SysForm_SysFormId);
            
            CreateTable(
                "dbo.Rules",
                c => new
                    {
                        RuleId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.RuleId);
            
            CreateTable(
                "dbo.SysForms",
                c => new
                    {
                        SysFormId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.SysFormId);
            
            CreateTable(
                "dbo.AccessMenus",
                c => new
                    {
                        AccessMenuId = c.Int(nullable: false, identity: true),
                        ViewAccess = c.Boolean(nullable: false),
                        ExecuteAccess = c.Boolean(nullable: false),
                        Rule_RuleId = c.Int(),
                    })
                .PrimaryKey(t => t.AccessMenuId)
                .ForeignKey("dbo.Rules", t => t.Rule_RuleId)
                .Index(t => t.Rule_RuleId);
            
            CreateTable(
                "dbo.AddDedBenEmployees",
                c => new
                    {
                        AddDedBenEmployeeId = c.Int(nullable: false, identity: true),
                        ValueCurrent = c.Double(nullable: false),
                        ValueRemain = c.Double(nullable: false),
                        ExecYear = c.Int(nullable: false),
                        ExecMonth = c.Int(nullable: false),
                        AdditionalDeductionOrBenefit_AdditionalDeductionOrBenefitId = c.Int(),
                        Employee_EmployeeId = c.Int(),
                    })
                .PrimaryKey(t => t.AddDedBenEmployeeId)
                .ForeignKey("dbo.AdditionalDeductionOrBenefits", t => t.AdditionalDeductionOrBenefit_AdditionalDeductionOrBenefitId)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeId)
                .Index(t => t.AdditionalDeductionOrBenefit_AdditionalDeductionOrBenefitId)
                .Index(t => t.Employee_EmployeeId);
            
            CreateTable(
                "dbo.AdditionalDeductionOrBenefits",
                c => new
                    {
                        AdditionalDeductionOrBenefitId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Index = c.Int(nullable: false),
                        IsBenefit = c.Boolean(nullable: false),
                        Alias = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AdditionalDeductionOrBenefitId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        FName = c.String(),
                        LName = c.String(),
                        NationalCardNo = c.String(),
                        BirthCertificateNo = c.String(),
                        DateBirth = c.String(),
                        BirthCertificatePlace = c.String(),
                        BirthPlace = c.String(),
                        FatherName = c.String(),
                        HomeTel = c.String(),
                        Address = c.String(),
                        DossierNo = c.String(),
                        BirthCertificateDate = c.String(),
                        Sex = c.Int(nullable: false),
                        CellNo = c.String(),
                        PostalCode = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.AddDedBenGroupParameterTitles",
                c => new
                    {
                        AddDedBenGroupParameterTitleId = c.Int(nullable: false, identity: true),
                        Value = c.Boolean(nullable: false),
                        AdditionalDeductionOrBenefit_AdditionalDeductionOrBenefitId = c.Int(),
                        GroupParameterTitle_GroupParameterTitleId = c.Int(),
                    })
                .PrimaryKey(t => t.AddDedBenGroupParameterTitleId)
                .ForeignKey("dbo.AdditionalDeductionOrBenefits", t => t.AdditionalDeductionOrBenefit_AdditionalDeductionOrBenefitId)
                .ForeignKey("dbo.GroupParameterTitles", t => t.GroupParameterTitle_GroupParameterTitleId)
                .Index(t => t.AdditionalDeductionOrBenefit_AdditionalDeductionOrBenefitId)
                .Index(t => t.GroupParameterTitle_GroupParameterTitleId);
            
            CreateTable(
                "dbo.GroupParameterTitles",
                c => new
                    {
                        GroupParameterTitleId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Alias = c.String(),
                    })
                .PrimaryKey(t => t.GroupParameterTitleId);
            
            CreateTable(
                "dbo.AddDedBenMainGroups",
                c => new
                    {
                        AddDedBenMainGroupId = c.Int(nullable: false, identity: true),
                        ExecYear = c.Int(nullable: false),
                        ExecMonth = c.Int(nullable: false),
                        AdditionalDeductionOrBenefit_AdditionalDeductionOrBenefitId = c.Int(),
                        MainGroup_MainGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.AddDedBenMainGroupId)
                .ForeignKey("dbo.AdditionalDeductionOrBenefits", t => t.AdditionalDeductionOrBenefit_AdditionalDeductionOrBenefitId)
                .ForeignKey("dbo.MainGroups", t => t.MainGroup_MainGroupId)
                .Index(t => t.AdditionalDeductionOrBenefit_AdditionalDeductionOrBenefitId)
                .Index(t => t.MainGroup_MainGroupId);
            
            CreateTable(
                "dbo.MainGroups",
                c => new
                    {
                        MainGroupId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.MainGroupId);
            
            CreateTable(
                "dbo.ContractFieldTitles",
                c => new
                    {
                        ContractFieldTitleId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        IsCalcAuto = c.Boolean(nullable: false),
                        MainGroup_MainGroupId = c.Int(),
                        RetirementFormField_RetirementFormFieldId = c.Int(),
                    })
                .PrimaryKey(t => t.ContractFieldTitleId)
                .ForeignKey("dbo.MainGroups", t => t.MainGroup_MainGroupId)
                .ForeignKey("dbo.RetirementFormFields", t => t.RetirementFormField_RetirementFormFieldId)
                .Index(t => t.MainGroup_MainGroupId)
                .Index(t => t.RetirementFormField_RetirementFormFieldId);
            
            CreateTable(
                "dbo.RetirementFormFields",
                c => new
                    {
                        RetirementFormFieldId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.RetirementFormFieldId);
            
            CreateTable(
                "dbo.SubGroups",
                c => new
                    {
                        SubGroupId = c.Int(nullable: false, identity: true),
                        MainGroup_MainGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.SubGroupId)
                .ForeignKey("dbo.MainGroups", t => t.MainGroup_MainGroupId)
                .Index(t => t.MainGroup_MainGroupId);
            
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
                "dbo.ContractDifferences",
                c => new
                    {
                        ContractDifferenceId = c.Int(nullable: false, identity: true),
                        DateFrom = c.String(),
                        DateTo = c.String(),
                        FirstMonth = c.Double(nullable: false),
                        PayYear = c.Int(nullable: false),
                        PayMonth = c.Int(nullable: false),
                        Contract_ContractId = c.Int(),
                    })
                .PrimaryKey(t => t.ContractDifferenceId)
                .ForeignKey("dbo.Contracts", t => t.Contract_ContractId)
                .Index(t => t.Contract_ContractId);
            
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        ContractId = c.Int(nullable: false, identity: true),
                        ContractNo = c.String(),
                        DateExport = c.String(),
                        DateExecution = c.String(),
                        DateEmployment = c.String(),
                        IsMarried = c.Boolean(nullable: false),
                        HardshipFactor = c.Double(nullable: false),
                        InsuranceNo = c.String(),
                        EducSt = c.Int(nullable: false),
                        EmpType = c.Int(nullable: false),
                        JobCat = c.Int(nullable: false),
                        Employee_EmployeeId = c.Int(),
                        Job_JobId = c.Int(),
                        SubGroup_SubGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.ContractId)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeId)
                .ForeignKey("dbo.Jobs", t => t.Job_JobId)
                .ForeignKey("dbo.SubGroups", t => t.SubGroup_SubGroupId)
                .Index(t => t.Employee_EmployeeId)
                .Index(t => t.Job_JobId)
                .Index(t => t.SubGroup_SubGroupId);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        JobId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        JobNo = c.String(),
                        Alias = c.String(),
                        Parentjob = c.Int(nullable: false),
                        MaxOccupy = c.Int(nullable: false),
                        Description = c.String(),
                        Department_DepartmentId = c.Int(),
                    })
                .PrimaryKey(t => t.JobId)
                .ForeignKey("dbo.Departments", t => t.Department_DepartmentId)
                .Index(t => t.Department_DepartmentId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        DepartmentNo = c.String(),
                        Alias = c.String(),
                        ParentDepartment_DepartmentId = c.Int(),
                    })
                .PrimaryKey(t => t.DepartmentId)
                .ForeignKey("dbo.Departments", t => t.ParentDepartment_DepartmentId)
                .Index(t => t.ParentDepartment_DepartmentId);
            
            CreateTable(
                "dbo.ContractFieldValues",
                c => new
                    {
                        ContractFieldValueId = c.Int(nullable: false, identity: true),
                        Value = c.Double(nullable: false),
                        Contract_ContractId = c.Int(),
                        ContractFieldTitle_ContractFieldTitleId = c.Int(),
                    })
                .PrimaryKey(t => t.ContractFieldValueId)
                .ForeignKey("dbo.Contracts", t => t.Contract_ContractId)
                .ForeignKey("dbo.ContractFieldTitles", t => t.ContractFieldTitle_ContractFieldTitleId)
                .Index(t => t.Contract_ContractId)
                .Index(t => t.ContractFieldTitle_ContractFieldTitleId);
            
            CreateTable(
                "dbo.ExpArtAddDedBens",
                c => new
                    {
                        ExpArtAddDedBenId = c.Int(nullable: false, identity: true),
                        AdditionalDeductionOrBenefit_AdditionalDeductionOrBenefitId = c.Int(),
                        ExpenseArticle_ExpenseArticleId = c.Int(),
                    })
                .PrimaryKey(t => t.ExpArtAddDedBenId)
                .ForeignKey("dbo.AdditionalDeductionOrBenefits", t => t.AdditionalDeductionOrBenefit_AdditionalDeductionOrBenefitId)
                .ForeignKey("dbo.ExpenseArticles", t => t.ExpenseArticle_ExpenseArticleId)
                .Index(t => t.AdditionalDeductionOrBenefit_AdditionalDeductionOrBenefitId)
                .Index(t => t.ExpenseArticle_ExpenseArticleId);
            
            CreateTable(
                "dbo.ExpenseArticles",
                c => new
                    {
                        ExpenseArticleId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.ExpenseArticleId);
            
            CreateTable(
                "dbo.ExpArtCntFieldTitles",
                c => new
                    {
                        ExpArtCntFieldTitleId = c.Int(nullable: false, identity: true),
                        ContractFieldTitle_ContractFieldTitleId = c.Int(),
                        ExpenseArticle_ExpenseArticleId = c.Int(),
                    })
                .PrimaryKey(t => t.ExpArtCntFieldTitleId)
                .ForeignKey("dbo.ContractFieldTitles", t => t.ContractFieldTitle_ContractFieldTitleId)
                .ForeignKey("dbo.ExpenseArticles", t => t.ExpenseArticle_ExpenseArticleId)
                .Index(t => t.ContractFieldTitle_ContractFieldTitleId)
                .Index(t => t.ExpenseArticle_ExpenseArticleId);
            
            CreateTable(
                "dbo.GroupParameterValues",
                c => new
                    {
                        GroupParameterValueId = c.Int(nullable: false, identity: true),
                        Value = c.Boolean(nullable: false),
                        ContractFieldTitle_ContractFieldTitleId = c.Int(),
                        GroupParameterTitle_GroupParameterTitleId = c.Int(),
                    })
                .PrimaryKey(t => t.GroupParameterValueId)
                .ForeignKey("dbo.ContractFieldTitles", t => t.ContractFieldTitle_ContractFieldTitleId)
                .ForeignKey("dbo.GroupParameterTitles", t => t.GroupParameterTitle_GroupParameterTitleId)
                .Index(t => t.ContractFieldTitle_ContractFieldTitleId)
                .Index(t => t.GroupParameterTitle_GroupParameterTitleId);
            
            CreateTable(
                "dbo.GroupValueTitles",
                c => new
                    {
                        GroupValueTitleId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Alias = c.String(),
                    })
                .PrimaryKey(t => t.GroupValueTitleId);
            
            CreateTable(
                "dbo.GroupVariableValues",
                c => new
                    {
                        GroupVariableValueId = c.Int(nullable: false, identity: true),
                        Value = c.Double(nullable: false),
                        ContentType = c.Int(nullable: false),
                        GroupValueTitle_GroupValueTitleId = c.Int(),
                        MainGroup_MainGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.GroupVariableValueId)
                .ForeignKey("dbo.GroupValueTitles", t => t.GroupValueTitle_GroupValueTitleId)
                .ForeignKey("dbo.MainGroups", t => t.MainGroup_MainGroupId)
                .Index(t => t.GroupValueTitle_GroupValueTitleId)
                .Index(t => t.MainGroup_MainGroupId);
            
            CreateTable(
                "dbo.HandselAdvances",
                c => new
                    {
                        HandselAdvanceId = c.Int(nullable: false, identity: true),
                        Value = c.Double(nullable: false),
                        ExecYear = c.Int(nullable: false),
                        Employee_EmployeeId = c.Int(),
                    })
                .PrimaryKey(t => t.HandselAdvanceId)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeId)
                .Index(t => t.Employee_EmployeeId);
            
            CreateTable(
                "dbo.HandselFormulas",
                c => new
                    {
                        HandselFormulaId = c.Int(nullable: false, identity: true),
                        ExecYear = c.Int(nullable: false),
                        DaysCount = c.Int(nullable: false),
                        Min = c.Double(nullable: false),
                        Max = c.Double(nullable: false),
                        TaxFreeValue = c.Double(nullable: false),
                        TaxRate = c.Int(nullable: false),
                        MainGroup_MainGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.HandselFormulaId)
                .ForeignKey("dbo.MainGroups", t => t.MainGroup_MainGroupId)
                .Index(t => t.MainGroup_MainGroupId);
            
            CreateTable(
                "dbo.HandselInclusions",
                c => new
                    {
                        HandselInclusionId = c.Int(nullable: false, identity: true),
                        ExecYear = c.Int(nullable: false),
                        InclusionValue = c.Int(nullable: false),
                        Employee_EmployeeId = c.Int(),
                    })
                .PrimaryKey(t => t.HandselInclusionId)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeId)
                .Index(t => t.Employee_EmployeeId);
            
            CreateTable(
                "dbo.MainGroupExpenseArticles",
                c => new
                    {
                        MainGroupExpenseArticleId = c.Int(nullable: false, identity: true),
                        ExpenseArticle_ExpenseArticleId = c.Int(),
                        MainGroup_MainGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.MainGroupExpenseArticleId)
                .ForeignKey("dbo.ExpenseArticles", t => t.ExpenseArticle_ExpenseArticleId)
                .ForeignKey("dbo.MainGroups", t => t.MainGroup_MainGroupId)
                .Index(t => t.ExpenseArticle_ExpenseArticleId)
                .Index(t => t.MainGroup_MainGroupId);
            
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
                        TravelerExpense = c.Double(nullable: false),
                        City_CityId = c.Int(),
                        Contract_ContractId = c.Int(),
                    })
                .PrimaryKey(t => t.MissionId)
                .ForeignKey("dbo.Cities", t => t.City_CityId)
                .ForeignKey("dbo.Contracts", t => t.Contract_ContractId)
                .Index(t => t.City_CityId)
                .Index(t => t.Contract_ContractId);
            
            CreateTable(
                "dbo.MonthlyDataContracts",
                c => new
                    {
                        MonthlyDataContractId = c.Int(nullable: false, identity: true),
                        ExecYear = c.Int(nullable: false),
                        ExecMonth = c.Int(nullable: false),
                        Value = c.Double(nullable: false),
                        Contract_ContractId = c.Int(),
                        MonthlyData_MonthlyDataId = c.Int(),
                    })
                .PrimaryKey(t => t.MonthlyDataContractId)
                .ForeignKey("dbo.Contracts", t => t.Contract_ContractId)
                .ForeignKey("dbo.MonthlyDatas", t => t.MonthlyData_MonthlyDataId)
                .Index(t => t.Contract_ContractId)
                .Index(t => t.MonthlyData_MonthlyDataId);
            
            CreateTable(
                "dbo.MonthlyDatas",
                c => new
                    {
                        MonthlyDataId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Alias = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MonthlyDataId);
            
            CreateTable(
                "dbo.MonthlyDataMainGroups",
                c => new
                    {
                        MonthlyDataMainGroupId = c.Int(nullable: false, identity: true),
                        ExecYear = c.Int(nullable: false),
                        ExecMonth = c.Int(nullable: false),
                        MainGroup_MainGroupId = c.Int(),
                        MonthlyData_MonthlyDataId = c.Int(),
                    })
                .PrimaryKey(t => t.MonthlyDataMainGroupId)
                .ForeignKey("dbo.MainGroups", t => t.MainGroup_MainGroupId)
                .ForeignKey("dbo.MonthlyDatas", t => t.MonthlyData_MonthlyDataId)
                .Index(t => t.MainGroup_MainGroupId)
                .Index(t => t.MonthlyData_MonthlyDataId);
            
            CreateTable(
                "dbo.SysMenus",
                c => new
                    {
                        SysMenuId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.SysMenuId);
            
            CreateTable(
                "dbo.TaxItems",
                c => new
                    {
                        TaxItemId = c.Int(nullable: false, identity: true),
                        ValueTo = c.Double(nullable: false),
                        Factor = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.TaxItemId);
            
            CreateTable(
                "dbo.Taxes",
                c => new
                    {
                        TaxId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Alias = c.String(),
                        DateAppliance = c.String(),
                        MainGroup_MainGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.TaxId)
                .ForeignKey("dbo.MainGroups", t => t.MainGroup_MainGroupId)
                .Index(t => t.MainGroup_MainGroupId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Rule_RuleId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Rules", t => t.Rule_RuleId)
                .Index(t => t.Rule_RuleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Rule_RuleId", "dbo.Rules");
            DropForeignKey("dbo.Taxes", "MainGroup_MainGroupId", "dbo.MainGroups");
            DropForeignKey("dbo.MonthlyDataMainGroups", "MonthlyData_MonthlyDataId", "dbo.MonthlyDatas");
            DropForeignKey("dbo.MonthlyDataMainGroups", "MainGroup_MainGroupId", "dbo.MainGroups");
            DropForeignKey("dbo.MonthlyDataContracts", "MonthlyData_MonthlyDataId", "dbo.MonthlyDatas");
            DropForeignKey("dbo.MonthlyDataContracts", "Contract_ContractId", "dbo.Contracts");
            DropForeignKey("dbo.Missions", "Contract_ContractId", "dbo.Contracts");
            DropForeignKey("dbo.Missions", "City_CityId", "dbo.Cities");
            DropForeignKey("dbo.MainGroupExpenseArticles", "MainGroup_MainGroupId", "dbo.MainGroups");
            DropForeignKey("dbo.MainGroupExpenseArticles", "ExpenseArticle_ExpenseArticleId", "dbo.ExpenseArticles");
            DropForeignKey("dbo.HandselInclusions", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.HandselFormulas", "MainGroup_MainGroupId", "dbo.MainGroups");
            DropForeignKey("dbo.HandselAdvances", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.GroupVariableValues", "MainGroup_MainGroupId", "dbo.MainGroups");
            DropForeignKey("dbo.GroupVariableValues", "GroupValueTitle_GroupValueTitleId", "dbo.GroupValueTitles");
            DropForeignKey("dbo.GroupParameterValues", "GroupParameterTitle_GroupParameterTitleId", "dbo.GroupParameterTitles");
            DropForeignKey("dbo.GroupParameterValues", "ContractFieldTitle_ContractFieldTitleId", "dbo.ContractFieldTitles");
            DropForeignKey("dbo.ExpArtCntFieldTitles", "ExpenseArticle_ExpenseArticleId", "dbo.ExpenseArticles");
            DropForeignKey("dbo.ExpArtCntFieldTitles", "ContractFieldTitle_ContractFieldTitleId", "dbo.ContractFieldTitles");
            DropForeignKey("dbo.ExpArtAddDedBens", "ExpenseArticle_ExpenseArticleId", "dbo.ExpenseArticles");
            DropForeignKey("dbo.ExpArtAddDedBens", "AdditionalDeductionOrBenefit_AdditionalDeductionOrBenefitId", "dbo.AdditionalDeductionOrBenefits");
            DropForeignKey("dbo.ContractFieldValues", "ContractFieldTitle_ContractFieldTitleId", "dbo.ContractFieldTitles");
            DropForeignKey("dbo.ContractFieldValues", "Contract_ContractId", "dbo.Contracts");
            DropForeignKey("dbo.ContractDifferences", "Contract_ContractId", "dbo.Contracts");
            DropForeignKey("dbo.Contracts", "SubGroup_SubGroupId", "dbo.SubGroups");
            DropForeignKey("dbo.Contracts", "Job_JobId", "dbo.Jobs");
            DropForeignKey("dbo.Jobs", "Department_DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Departments", "ParentDepartment_DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Contracts", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.AddDedBenMainGroups", "MainGroup_MainGroupId", "dbo.MainGroups");
            DropForeignKey("dbo.SubGroups", "MainGroup_MainGroupId", "dbo.MainGroups");
            DropForeignKey("dbo.ContractFieldTitles", "RetirementFormField_RetirementFormFieldId", "dbo.RetirementFormFields");
            DropForeignKey("dbo.ContractFieldTitles", "MainGroup_MainGroupId", "dbo.MainGroups");
            DropForeignKey("dbo.AddDedBenMainGroups", "AdditionalDeductionOrBenefit_AdditionalDeductionOrBenefitId", "dbo.AdditionalDeductionOrBenefits");
            DropForeignKey("dbo.AddDedBenGroupParameterTitles", "GroupParameterTitle_GroupParameterTitleId", "dbo.GroupParameterTitles");
            DropForeignKey("dbo.AddDedBenGroupParameterTitles", "AdditionalDeductionOrBenefit_AdditionalDeductionOrBenefitId", "dbo.AdditionalDeductionOrBenefits");
            DropForeignKey("dbo.AddDedBenEmployees", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.AddDedBenEmployees", "AdditionalDeductionOrBenefit_AdditionalDeductionOrBenefitId", "dbo.AdditionalDeductionOrBenefits");
            DropForeignKey("dbo.AccessMenus", "Rule_RuleId", "dbo.Rules");
            DropForeignKey("dbo.AccessForms", "SysForm_SysFormId", "dbo.SysForms");
            DropForeignKey("dbo.AccessForms", "Rule_RuleId", "dbo.Rules");
            DropIndex("dbo.Users", new[] { "Rule_RuleId" });
            DropIndex("dbo.Taxes", new[] { "MainGroup_MainGroupId" });
            DropIndex("dbo.MonthlyDataMainGroups", new[] { "MonthlyData_MonthlyDataId" });
            DropIndex("dbo.MonthlyDataMainGroups", new[] { "MainGroup_MainGroupId" });
            DropIndex("dbo.MonthlyDataContracts", new[] { "MonthlyData_MonthlyDataId" });
            DropIndex("dbo.MonthlyDataContracts", new[] { "Contract_ContractId" });
            DropIndex("dbo.Missions", new[] { "Contract_ContractId" });
            DropIndex("dbo.Missions", new[] { "City_CityId" });
            DropIndex("dbo.MainGroupExpenseArticles", new[] { "MainGroup_MainGroupId" });
            DropIndex("dbo.MainGroupExpenseArticles", new[] { "ExpenseArticle_ExpenseArticleId" });
            DropIndex("dbo.HandselInclusions", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.HandselFormulas", new[] { "MainGroup_MainGroupId" });
            DropIndex("dbo.HandselAdvances", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.GroupVariableValues", new[] { "MainGroup_MainGroupId" });
            DropIndex("dbo.GroupVariableValues", new[] { "GroupValueTitle_GroupValueTitleId" });
            DropIndex("dbo.GroupParameterValues", new[] { "GroupParameterTitle_GroupParameterTitleId" });
            DropIndex("dbo.GroupParameterValues", new[] { "ContractFieldTitle_ContractFieldTitleId" });
            DropIndex("dbo.ExpArtCntFieldTitles", new[] { "ExpenseArticle_ExpenseArticleId" });
            DropIndex("dbo.ExpArtCntFieldTitles", new[] { "ContractFieldTitle_ContractFieldTitleId" });
            DropIndex("dbo.ExpArtAddDedBens", new[] { "ExpenseArticle_ExpenseArticleId" });
            DropIndex("dbo.ExpArtAddDedBens", new[] { "AdditionalDeductionOrBenefit_AdditionalDeductionOrBenefitId" });
            DropIndex("dbo.ContractFieldValues", new[] { "ContractFieldTitle_ContractFieldTitleId" });
            DropIndex("dbo.ContractFieldValues", new[] { "Contract_ContractId" });
            DropIndex("dbo.Departments", new[] { "ParentDepartment_DepartmentId" });
            DropIndex("dbo.Jobs", new[] { "Department_DepartmentId" });
            DropIndex("dbo.Contracts", new[] { "SubGroup_SubGroupId" });
            DropIndex("dbo.Contracts", new[] { "Job_JobId" });
            DropIndex("dbo.Contracts", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.ContractDifferences", new[] { "Contract_ContractId" });
            DropIndex("dbo.SubGroups", new[] { "MainGroup_MainGroupId" });
            DropIndex("dbo.ContractFieldTitles", new[] { "RetirementFormField_RetirementFormFieldId" });
            DropIndex("dbo.ContractFieldTitles", new[] { "MainGroup_MainGroupId" });
            DropIndex("dbo.AddDedBenMainGroups", new[] { "MainGroup_MainGroupId" });
            DropIndex("dbo.AddDedBenMainGroups", new[] { "AdditionalDeductionOrBenefit_AdditionalDeductionOrBenefitId" });
            DropIndex("dbo.AddDedBenGroupParameterTitles", new[] { "GroupParameterTitle_GroupParameterTitleId" });
            DropIndex("dbo.AddDedBenGroupParameterTitles", new[] { "AdditionalDeductionOrBenefit_AdditionalDeductionOrBenefitId" });
            DropIndex("dbo.AddDedBenEmployees", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.AddDedBenEmployees", new[] { "AdditionalDeductionOrBenefit_AdditionalDeductionOrBenefitId" });
            DropIndex("dbo.AccessMenus", new[] { "Rule_RuleId" });
            DropIndex("dbo.AccessForms", new[] { "SysForm_SysFormId" });
            DropIndex("dbo.AccessForms", new[] { "Rule_RuleId" });
            DropTable("dbo.Users");
            DropTable("dbo.Taxes");
            DropTable("dbo.TaxItems");
            DropTable("dbo.SysMenus");
            DropTable("dbo.MonthlyDataMainGroups");
            DropTable("dbo.MonthlyDatas");
            DropTable("dbo.MonthlyDataContracts");
            DropTable("dbo.Missions");
            DropTable("dbo.MainGroupExpenseArticles");
            DropTable("dbo.HandselInclusions");
            DropTable("dbo.HandselFormulas");
            DropTable("dbo.HandselAdvances");
            DropTable("dbo.GroupVariableValues");
            DropTable("dbo.GroupValueTitles");
            DropTable("dbo.GroupParameterValues");
            DropTable("dbo.ExpArtCntFieldTitles");
            DropTable("dbo.ExpenseArticles");
            DropTable("dbo.ExpArtAddDedBens");
            DropTable("dbo.ContractFieldValues");
            DropTable("dbo.Departments");
            DropTable("dbo.Jobs");
            DropTable("dbo.Contracts");
            DropTable("dbo.ContractDifferences");
            DropTable("dbo.Cities");
            DropTable("dbo.SubGroups");
            DropTable("dbo.RetirementFormFields");
            DropTable("dbo.ContractFieldTitles");
            DropTable("dbo.MainGroups");
            DropTable("dbo.AddDedBenMainGroups");
            DropTable("dbo.GroupParameterTitles");
            DropTable("dbo.AddDedBenGroupParameterTitles");
            DropTable("dbo.Employees");
            DropTable("dbo.AdditionalDeductionOrBenefits");
            DropTable("dbo.AddDedBenEmployees");
            DropTable("dbo.AccessMenus");
            DropTable("dbo.SysForms");
            DropTable("dbo.Rules");
            DropTable("dbo.AccessForms");
        }
    }
}
