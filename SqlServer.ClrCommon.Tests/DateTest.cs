using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SqlServer.ClrCommon.Tests
{
	[TestClass()]
	public class DateTest : SqlDatabaseTestClass
	{

		public DateTest()
		{
			InitializeComponent();
		}

		[TestInitialize()]
		public void TestInitialize()
		{
			base.InitializeTest();
		}
		[TestCleanup()]
		public void TestCleanup()
		{
			base.CleanupTest();
		}

		[TestMethod()]
		public void DateMiscTests()
		{
			SqlDatabaseTestActions testActions = this.DateMiscTestsData;
			// Execute the pre-test script
			// 
			System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
			SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
			// Execute the test script
			// 
			System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
			SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
			// Execute the post-test script
			// 
			System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
			SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
		}
		[TestMethod()]
		public void DateReportingFunctionTests()
		{
			SqlDatabaseTestActions testActions = this.DateReportingFunctionTestsData;
			// Execute the pre-test script
			// 
			System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
			SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
			try
			{
				// Execute the test script
				// 
				System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
				SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
			}
			finally
			{
				// Execute the post-test script
				// 
				System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
				SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
			}
		}


		#region Designer support code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction DateMiscTests_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DateTest));
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition AgeInYears;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition IsLeapYear;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition DaysInMonth;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition IsDaylightSavingTime;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition IsBusinessDay;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition DayOfWeek;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition DayOfYear;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition DayOfWeek2;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition DayOfYear2;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition AgeInYearsAtDate1;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition AgeInYearsAtDate2;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction DateReportingFunctionTests_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ChecksumCondition DataChecksumCondition;
            this.DateMiscTestsData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.DateReportingFunctionTestsData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            DateMiscTests_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            AgeInYears = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            IsLeapYear = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            DaysInMonth = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            IsDaylightSavingTime = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            IsBusinessDay = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            DayOfWeek = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            DayOfYear = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            DayOfWeek2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            DayOfYear2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            AgeInYearsAtDate1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            AgeInYearsAtDate2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            DateReportingFunctionTests_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            DataChecksumCondition = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ChecksumCondition();
            // 
            // DateMiscTests_TestAction
            // 
            DateMiscTests_TestAction.Conditions.Add(AgeInYears);
            DateMiscTests_TestAction.Conditions.Add(IsLeapYear);
            DateMiscTests_TestAction.Conditions.Add(DaysInMonth);
            DateMiscTests_TestAction.Conditions.Add(IsDaylightSavingTime);
            DateMiscTests_TestAction.Conditions.Add(IsBusinessDay);
            DateMiscTests_TestAction.Conditions.Add(DayOfWeek);
            DateMiscTests_TestAction.Conditions.Add(DayOfYear);
            DateMiscTests_TestAction.Conditions.Add(DayOfWeek2);
            DateMiscTests_TestAction.Conditions.Add(DayOfYear2);
            DateMiscTests_TestAction.Conditions.Add(AgeInYearsAtDate1);
            DateMiscTests_TestAction.Conditions.Add(AgeInYearsAtDate2);
            resources.ApplyResources(DateMiscTests_TestAction, "DateMiscTests_TestAction");
            // 
            // AgeInYears
            // 
            AgeInYears.ColumnNumber = 1;
            AgeInYears.Enabled = true;
            AgeInYears.ExpectedValue = "68";
            AgeInYears.Name = "AgeInYears";
            AgeInYears.NullExpected = false;
            AgeInYears.ResultSet = 1;
            AgeInYears.RowNumber = 1;
            // 
            // IsLeapYear
            // 
            IsLeapYear.ColumnNumber = 2;
            IsLeapYear.Enabled = true;
            IsLeapYear.ExpectedValue = "false";
            IsLeapYear.Name = "IsLeapYear";
            IsLeapYear.NullExpected = false;
            IsLeapYear.ResultSet = 1;
            IsLeapYear.RowNumber = 1;
            // 
            // DaysInMonth
            // 
            DaysInMonth.ColumnNumber = 3;
            DaysInMonth.Enabled = true;
            DaysInMonth.ExpectedValue = "31";
            DaysInMonth.Name = "DaysInMonth";
            DaysInMonth.NullExpected = false;
            DaysInMonth.ResultSet = 1;
            DaysInMonth.RowNumber = 1;
            // 
            // IsDaylightSavingTime
            // 
            IsDaylightSavingTime.ColumnNumber = 4;
            IsDaylightSavingTime.Enabled = true;
            IsDaylightSavingTime.ExpectedValue = "false";
            IsDaylightSavingTime.Name = "IsDaylightSavingTime";
            IsDaylightSavingTime.NullExpected = false;
            IsDaylightSavingTime.ResultSet = 1;
            IsDaylightSavingTime.RowNumber = 1;
            // 
            // IsBusinessDay
            // 
            IsBusinessDay.ColumnNumber = 5;
            IsBusinessDay.Enabled = true;
            IsBusinessDay.ExpectedValue = "false";
            IsBusinessDay.Name = "IsBusinessDay";
            IsBusinessDay.NullExpected = false;
            IsBusinessDay.ResultSet = 1;
            IsBusinessDay.RowNumber = 1;
            // 
            // DayOfWeek
            // 
            DayOfWeek.ColumnNumber = 1;
            DayOfWeek.Enabled = true;
            DayOfWeek.ExpectedValue = "Monday";
            DayOfWeek.Name = "DayOfWeek";
            DayOfWeek.NullExpected = false;
            DayOfWeek.ResultSet = 2;
            DayOfWeek.RowNumber = 1;
            // 
            // DayOfYear
            // 
            DayOfYear.ColumnNumber = 2;
            DayOfYear.Enabled = true;
            DayOfYear.ExpectedValue = "208";
            DayOfYear.Name = "DayOfYear";
            DayOfYear.NullExpected = false;
            DayOfYear.ResultSet = 2;
            DayOfYear.RowNumber = 1;
            // 
            // DayOfWeek2
            // 
            DayOfWeek2.ColumnNumber = 3;
            DayOfWeek2.Enabled = true;
            DayOfWeek2.ExpectedValue = "Thursday";
            DayOfWeek2.Name = "DayOfWeek2";
            DayOfWeek2.NullExpected = false;
            DayOfWeek2.ResultSet = 2;
            DayOfWeek2.RowNumber = 1;
            // 
            // DayOfYear2
            // 
            DayOfYear2.ColumnNumber = 4;
            DayOfYear2.Enabled = true;
            DayOfYear2.ExpectedValue = "152";
            DayOfYear2.Name = "DayOfYear2";
            DayOfYear2.NullExpected = false;
            DayOfYear2.ResultSet = 2;
            DayOfYear2.RowNumber = 1;
            // 
            // AgeInYearsAtDate1
            // 
            AgeInYearsAtDate1.ColumnNumber = 1;
            AgeInYearsAtDate1.Enabled = true;
            AgeInYearsAtDate1.ExpectedValue = "35";
            AgeInYearsAtDate1.Name = "AgeInYearsAtDate1";
            AgeInYearsAtDate1.NullExpected = false;
            AgeInYearsAtDate1.ResultSet = 3;
            AgeInYearsAtDate1.RowNumber = 1;
            // 
            // AgeInYearsAtDate2
            // 
            AgeInYearsAtDate2.ColumnNumber = 2;
            AgeInYearsAtDate2.Enabled = true;
            AgeInYearsAtDate2.ExpectedValue = "34";
            AgeInYearsAtDate2.Name = "AgeInYearsAtDate2";
            AgeInYearsAtDate2.NullExpected = false;
            AgeInYearsAtDate2.ResultSet = 3;
            AgeInYearsAtDate2.RowNumber = 1;
            // 
            // DateReportingFunctionTests_TestAction
            // 
            DateReportingFunctionTests_TestAction.Conditions.Add(DataChecksumCondition);
            resources.ApplyResources(DateReportingFunctionTests_TestAction, "DateReportingFunctionTests_TestAction");
            // 
            // DataChecksumCondition
            // 
            DataChecksumCondition.Checksum = "1802541957";
            DataChecksumCondition.Enabled = true;
            DataChecksumCondition.Name = "DataChecksumCondition";
            // 
            // DateMiscTestsData
            // 
            this.DateMiscTestsData.PosttestAction = null;
            this.DateMiscTestsData.PretestAction = null;
            this.DateMiscTestsData.TestAction = DateMiscTests_TestAction;
            // 
            // DateReportingFunctionTestsData
            // 
            this.DateReportingFunctionTestsData.PosttestAction = null;
            this.DateReportingFunctionTestsData.PretestAction = null;
            this.DateReportingFunctionTestsData.TestAction = DateReportingFunctionTests_TestAction;
        }

        #endregion


        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        #endregion

        private SqlDatabaseTestActions DateMiscTestsData;
		private SqlDatabaseTestActions DateReportingFunctionTestsData;
	}
}
