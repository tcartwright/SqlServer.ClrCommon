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
	public class SplitTests : SqlDatabaseTestClass
	{

		public SplitTests()
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
		public void SplitStringTests()
		{
			SqlDatabaseTestActions testActions = this.SplitStringTestsData;
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
		public void SplitIntsTests()
		{
			SqlDatabaseTestActions testActions = this.SplitIntsTestsData;
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
		[TestMethod()]
		public void SplitBigIntsTests()
		{
			SqlDatabaseTestActions testActions = this.SplitBigIntsTestsData;
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
		[TestMethod()]
		public void SplitFloatsTests()
		{
			SqlDatabaseTestActions testActions = this.SplitFloatsTestsData;
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
		[TestMethod()]
		public void SplitStringEmptyTests()
		{
			SqlDatabaseTestActions testActions = this.SplitStringEmptyTestsData;
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
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction SplitStringTests_TestAction;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplitTests));
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ChecksumCondition SplitStringsDataCheckSum;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ExecutionTimeCondition TotalExecutionTime;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction SplitIntsTests_TestAction;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ChecksumCondition SplitIntsChecksum;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ExecutionTimeCondition SplitIntsTotalExecutionTime;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction SplitBigIntsTests_TestAction;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ChecksumCondition SplitBigIntsChecksum;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ExecutionTimeCondition SplitBigIntsTotalExecutionTime;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction SplitFloatsTests_TestAction;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ChecksumCondition SplitFloatsChecksum;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ExecutionTimeCondition SplitFloatsTotalExecutionTime;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction SplitStringEmptyTests_TestAction;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition StringSplitEmptyRowDataRowCount;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition SplitStringRemoveEmptiesRowCount;
			this.SplitStringTestsData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
			this.SplitIntsTestsData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
			this.SplitBigIntsTestsData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
			this.SplitFloatsTestsData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
			this.SplitStringEmptyTestsData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
			SplitStringTests_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
			SplitStringsDataCheckSum = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ChecksumCondition();
			TotalExecutionTime = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ExecutionTimeCondition();
			SplitIntsTests_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
			SplitIntsChecksum = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ChecksumCondition();
			SplitIntsTotalExecutionTime = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ExecutionTimeCondition();
			SplitBigIntsTests_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
			SplitBigIntsChecksum = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ChecksumCondition();
			SplitBigIntsTotalExecutionTime = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ExecutionTimeCondition();
			SplitFloatsTests_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
			SplitFloatsChecksum = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ChecksumCondition();
			SplitFloatsTotalExecutionTime = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ExecutionTimeCondition();
			SplitStringEmptyTests_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
			StringSplitEmptyRowDataRowCount = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
			SplitStringRemoveEmptiesRowCount = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
			// 
			// SplitStringTests_TestAction
			// 
			SplitStringTests_TestAction.Conditions.Add(SplitStringsDataCheckSum);
			SplitStringTests_TestAction.Conditions.Add(TotalExecutionTime);
			resources.ApplyResources(SplitStringTests_TestAction, "SplitStringTests_TestAction");
			// 
			// SplitStringsDataCheckSum
			// 
			SplitStringsDataCheckSum.Checksum = "-898979040";
			SplitStringsDataCheckSum.Enabled = true;
			SplitStringsDataCheckSum.Name = "SplitStringsDataCheckSum";
			// 
			// TotalExecutionTime
			// 
			TotalExecutionTime.Enabled = true;
			TotalExecutionTime.ExecutionTime = System.TimeSpan.Parse("00:00:00.5000000");
			TotalExecutionTime.Name = "TotalExecutionTime";
			// 
			// SplitIntsTests_TestAction
			// 
			SplitIntsTests_TestAction.Conditions.Add(SplitIntsChecksum);
			SplitIntsTests_TestAction.Conditions.Add(SplitIntsTotalExecutionTime);
			resources.ApplyResources(SplitIntsTests_TestAction, "SplitIntsTests_TestAction");
			// 
			// SplitIntsChecksum
			// 
			SplitIntsChecksum.Checksum = "689696137";
			SplitIntsChecksum.Enabled = true;
			SplitIntsChecksum.Name = "SplitIntsChecksum";
			// 
			// SplitIntsTotalExecutionTime
			// 
			SplitIntsTotalExecutionTime.Enabled = true;
			SplitIntsTotalExecutionTime.ExecutionTime = System.TimeSpan.Parse("00:00:00.5000000");
			SplitIntsTotalExecutionTime.Name = "SplitIntsTotalExecutionTime";
			// 
			// SplitBigIntsTests_TestAction
			// 
			SplitBigIntsTests_TestAction.Conditions.Add(SplitBigIntsChecksum);
			SplitBigIntsTests_TestAction.Conditions.Add(SplitBigIntsTotalExecutionTime);
			resources.ApplyResources(SplitBigIntsTests_TestAction, "SplitBigIntsTests_TestAction");
			// 
			// SplitBigIntsChecksum
			// 
			SplitBigIntsChecksum.Checksum = "275330368";
			SplitBigIntsChecksum.Enabled = true;
			SplitBigIntsChecksum.Name = "SplitBigIntsChecksum";
			// 
			// SplitBigIntsTotalExecutionTime
			// 
			SplitBigIntsTotalExecutionTime.Enabled = true;
			SplitBigIntsTotalExecutionTime.ExecutionTime = System.TimeSpan.Parse("00:00:00.7500000");
			SplitBigIntsTotalExecutionTime.Name = "SplitBigIntsTotalExecutionTime";
			// 
			// SplitFloatsTests_TestAction
			// 
			SplitFloatsTests_TestAction.Conditions.Add(SplitFloatsChecksum);
			SplitFloatsTests_TestAction.Conditions.Add(SplitFloatsTotalExecutionTime);
			resources.ApplyResources(SplitFloatsTests_TestAction, "SplitFloatsTests_TestAction");
			// 
			// SplitFloatsChecksum
			// 
			SplitFloatsChecksum.Checksum = "-1905467329";
			SplitFloatsChecksum.Enabled = true;
			SplitFloatsChecksum.Name = "SplitFloatsChecksum";
			// 
			// SplitFloatsTotalExecutionTime
			// 
			SplitFloatsTotalExecutionTime.Enabled = true;
			SplitFloatsTotalExecutionTime.ExecutionTime = System.TimeSpan.Parse("00:00:00.5000000");
			SplitFloatsTotalExecutionTime.Name = "SplitFloatsTotalExecutionTime";
			// 
			// SplitStringEmptyTests_TestAction
			// 
			SplitStringEmptyTests_TestAction.Conditions.Add(StringSplitEmptyRowDataRowCount);
			SplitStringEmptyTests_TestAction.Conditions.Add(SplitStringRemoveEmptiesRowCount);
			resources.ApplyResources(SplitStringEmptyTests_TestAction, "SplitStringEmptyTests_TestAction");
			// 
			// StringSplitEmptyRowDataRowCount
			// 
			StringSplitEmptyRowDataRowCount.Enabled = true;
			StringSplitEmptyRowDataRowCount.Name = "StringSplitEmptyRowDataRowCount";
			StringSplitEmptyRowDataRowCount.ResultSet = 1;
			StringSplitEmptyRowDataRowCount.RowCount = 3;
			// 
			// SplitStringRemoveEmptiesRowCount
			// 
			SplitStringRemoveEmptiesRowCount.Enabled = true;
			SplitStringRemoveEmptiesRowCount.Name = "SplitStringRemoveEmptiesRowCount";
			SplitStringRemoveEmptiesRowCount.ResultSet = 2;
			SplitStringRemoveEmptiesRowCount.RowCount = 2;
			// 
			// SplitStringTestsData
			// 
			this.SplitStringTestsData.PosttestAction = null;
			this.SplitStringTestsData.PretestAction = null;
			this.SplitStringTestsData.TestAction = SplitStringTests_TestAction;
			// 
			// SplitIntsTestsData
			// 
			this.SplitIntsTestsData.PosttestAction = null;
			this.SplitIntsTestsData.PretestAction = null;
			this.SplitIntsTestsData.TestAction = SplitIntsTests_TestAction;
			// 
			// SplitBigIntsTestsData
			// 
			this.SplitBigIntsTestsData.PosttestAction = null;
			this.SplitBigIntsTestsData.PretestAction = null;
			this.SplitBigIntsTestsData.TestAction = SplitBigIntsTests_TestAction;
			// 
			// SplitFloatsTestsData
			// 
			this.SplitFloatsTestsData.PosttestAction = null;
			this.SplitFloatsTestsData.PretestAction = null;
			this.SplitFloatsTestsData.TestAction = SplitFloatsTests_TestAction;
			// 
			// SplitStringEmptyTestsData
			// 
			this.SplitStringEmptyTestsData.PosttestAction = null;
			this.SplitStringEmptyTestsData.PretestAction = null;
			this.SplitStringEmptyTestsData.TestAction = SplitStringEmptyTests_TestAction;
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

		private SqlDatabaseTestActions SplitStringTestsData;
		private SqlDatabaseTestActions SplitIntsTestsData;
		private SqlDatabaseTestActions SplitBigIntsTestsData;
		private SqlDatabaseTestActions SplitFloatsTestsData;
		private SqlDatabaseTestActions SplitStringEmptyTestsData;
	}
}
