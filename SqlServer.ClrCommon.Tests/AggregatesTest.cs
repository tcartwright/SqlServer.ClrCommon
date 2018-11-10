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
	public class AggregatesTest : SqlDatabaseTestClass
	{

		public AggregatesTest()
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
		public void AggregatesConcatTests()
		{
			SqlDatabaseTestActions testActions = this.AggregatesConcatTestsData;
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
		public void AggregatesMedianTests()
		{
			SqlDatabaseTestActions testActions = this.AggregatesMedianTestsData;
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
		public void AggregatesPercentileTests()
		{
			SqlDatabaseTestActions testActions = this.AggregatesPercentileTestsData;
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
		public void AggregatesWeightedAvgTests()
		{
			SqlDatabaseTestActions testActions = this.AggregatesWeightedAvgTestsData;
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
		public void AggregatesTestDataNotEmpty()
		{
			SqlDatabaseTestActions testActions = this.AggregatesTestDataNotEmptyData;
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
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction AggregatesConcatTests_TestAction;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AggregatesTest));
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition ConcatRowCount;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ChecksumCondition ConcatChecksum;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction AggregatesMedianTests_TestAction;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition Median1;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition MedianAdams1;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition MedianAdams2;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition MedianAllen1;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition MedianAllen2;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition MedianAnderson1;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition MedianAnderson2;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction AggregatesPercentileTests_TestAction;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition PercentileAdams1;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition PercentileAdams2;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition PercentileAllen1;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition PercentileAllen2;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction AggregatesWeightedAvgTests_TestAction;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition WeightedAvg1;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition WeightedAvg2;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction testInitializeAction;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction testCleanupAction;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction AggregatesTestDataNotEmpty_TestAction;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition TestDataRowCount;
			this.AggregatesConcatTestsData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
			this.AggregatesMedianTestsData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
			this.AggregatesPercentileTestsData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
			this.AggregatesWeightedAvgTestsData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
			this.AggregatesTestDataNotEmptyData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
			AggregatesConcatTests_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
			ConcatRowCount = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
			ConcatChecksum = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ChecksumCondition();
			AggregatesMedianTests_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
			Median1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			MedianAdams1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			MedianAdams2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			MedianAllen1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			MedianAllen2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			MedianAnderson1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			MedianAnderson2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			AggregatesPercentileTests_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
			PercentileAdams1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			PercentileAdams2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			PercentileAllen1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			PercentileAllen2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			AggregatesWeightedAvgTests_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
			WeightedAvg1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			WeightedAvg2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			testInitializeAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
			testCleanupAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
			AggregatesTestDataNotEmpty_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
			TestDataRowCount = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
			// 
			// AggregatesConcatTests_TestAction
			// 
			AggregatesConcatTests_TestAction.Conditions.Add(ConcatRowCount);
			AggregatesConcatTests_TestAction.Conditions.Add(ConcatChecksum);
			resources.ApplyResources(AggregatesConcatTests_TestAction, "AggregatesConcatTests_TestAction");
			// 
			// ConcatRowCount
			// 
			ConcatRowCount.Enabled = true;
			ConcatRowCount.Name = "ConcatRowCount";
			ConcatRowCount.ResultSet = 1;
			ConcatRowCount.RowCount = 2;
			// 
			// ConcatChecksum
			// 
			ConcatChecksum.Checksum = "1096366309";
			ConcatChecksum.Enabled = true;
			ConcatChecksum.Name = "ConcatChecksum";
			// 
			// AggregatesMedianTests_TestAction
			// 
			AggregatesMedianTests_TestAction.Conditions.Add(Median1);
			AggregatesMedianTests_TestAction.Conditions.Add(MedianAdams1);
			AggregatesMedianTests_TestAction.Conditions.Add(MedianAdams2);
			AggregatesMedianTests_TestAction.Conditions.Add(MedianAllen1);
			AggregatesMedianTests_TestAction.Conditions.Add(MedianAllen2);
			AggregatesMedianTests_TestAction.Conditions.Add(MedianAnderson1);
			AggregatesMedianTests_TestAction.Conditions.Add(MedianAnderson2);
			resources.ApplyResources(AggregatesMedianTests_TestAction, "AggregatesMedianTests_TestAction");
			// 
			// Median1
			// 
			Median1.ColumnNumber = 1;
			Median1.Enabled = true;
			Median1.ExpectedValue = "250.5";
			Median1.Name = "Median1";
			Median1.NullExpected = false;
			Median1.ResultSet = 1;
			Median1.RowNumber = 1;
			// 
			// MedianAdams1
			// 
			MedianAdams1.ColumnNumber = 1;
			MedianAdams1.Enabled = true;
			MedianAdams1.ExpectedValue = "Adams";
			MedianAdams1.Name = "MedianAdams1";
			MedianAdams1.NullExpected = false;
			MedianAdams1.ResultSet = 2;
			MedianAdams1.RowNumber = 1;
			// 
			// MedianAdams2
			// 
			MedianAdams2.ColumnNumber = 2;
			MedianAdams2.Enabled = true;
			MedianAdams2.ExpectedValue = "41";
			MedianAdams2.Name = "MedianAdams2";
			MedianAdams2.NullExpected = false;
			MedianAdams2.ResultSet = 2;
			MedianAdams2.RowNumber = 1;
			// 
			// MedianAllen1
			// 
			MedianAllen1.ColumnNumber = 1;
			MedianAllen1.Enabled = true;
			MedianAllen1.ExpectedValue = "Allen";
			MedianAllen1.Name = "MedianAllen1";
			MedianAllen1.NullExpected = false;
			MedianAllen1.ResultSet = 2;
			MedianAllen1.RowNumber = 2;
			// 
			// MedianAllen2
			// 
			MedianAllen2.ColumnNumber = 2;
			MedianAllen2.Enabled = true;
			MedianAllen2.ExpectedValue = "39";
			MedianAllen2.Name = "MedianAllen2";
			MedianAllen2.NullExpected = false;
			MedianAllen2.ResultSet = 2;
			MedianAllen2.RowNumber = 2;
			// 
			// MedianAnderson1
			// 
			MedianAnderson1.ColumnNumber = 1;
			MedianAnderson1.Enabled = true;
			MedianAnderson1.ExpectedValue = "Anderson";
			MedianAnderson1.Name = "MedianAnderson1";
			MedianAnderson1.NullExpected = false;
			MedianAnderson1.ResultSet = 2;
			MedianAnderson1.RowNumber = 3;
			// 
			// MedianAnderson2
			// 
			MedianAnderson2.ColumnNumber = 2;
			MedianAnderson2.Enabled = true;
			MedianAnderson2.ExpectedValue = "48.5";
			MedianAnderson2.Name = "MedianAnderson2";
			MedianAnderson2.NullExpected = false;
			MedianAnderson2.ResultSet = 2;
			MedianAnderson2.RowNumber = 3;
			// 
			// AggregatesPercentileTests_TestAction
			// 
			AggregatesPercentileTests_TestAction.Conditions.Add(PercentileAdams1);
			AggregatesPercentileTests_TestAction.Conditions.Add(PercentileAdams2);
			AggregatesPercentileTests_TestAction.Conditions.Add(PercentileAllen1);
			AggregatesPercentileTests_TestAction.Conditions.Add(PercentileAllen2);
			resources.ApplyResources(AggregatesPercentileTests_TestAction, "AggregatesPercentileTests_TestAction");
			// 
			// PercentileAdams1
			// 
			PercentileAdams1.ColumnNumber = 1;
			PercentileAdams1.Enabled = true;
			PercentileAdams1.ExpectedValue = "Adams";
			PercentileAdams1.Name = "PercentileAdams1";
			PercentileAdams1.NullExpected = false;
			PercentileAdams1.ResultSet = 1;
			PercentileAdams1.RowNumber = 1;
			// 
			// PercentileAdams2
			// 
			PercentileAdams2.ColumnNumber = 2;
			PercentileAdams2.Enabled = true;
			PercentileAdams2.ExpectedValue = "52";
			PercentileAdams2.Name = "PercentileAdams2";
			PercentileAdams2.NullExpected = false;
			PercentileAdams2.ResultSet = 1;
			PercentileAdams2.RowNumber = 1;
			// 
			// PercentileAllen1
			// 
			PercentileAllen1.ColumnNumber = 1;
			PercentileAllen1.Enabled = true;
			PercentileAllen1.ExpectedValue = "Allen";
			PercentileAllen1.Name = "PercentileAllen1";
			PercentileAllen1.NullExpected = false;
			PercentileAllen1.ResultSet = 1;
			PercentileAllen1.RowNumber = 2;
			// 
			// PercentileAllen2
			// 
			PercentileAllen2.ColumnNumber = 2;
			PercentileAllen2.Enabled = true;
			PercentileAllen2.ExpectedValue = "57";
			PercentileAllen2.Name = "PercentileAllen2";
			PercentileAllen2.NullExpected = false;
			PercentileAllen2.ResultSet = 1;
			PercentileAllen2.RowNumber = 2;
			// 
			// AggregatesWeightedAvgTests_TestAction
			// 
			AggregatesWeightedAvgTests_TestAction.Conditions.Add(WeightedAvg1);
			AggregatesWeightedAvgTests_TestAction.Conditions.Add(WeightedAvg2);
			resources.ApplyResources(AggregatesWeightedAvgTests_TestAction, "AggregatesWeightedAvgTests_TestAction");
			// 
			// WeightedAvg1
			// 
			WeightedAvg1.ColumnNumber = 1;
			WeightedAvg1.Enabled = true;
			WeightedAvg1.ExpectedValue = "2";
			WeightedAvg1.Name = "WeightedAvg1";
			WeightedAvg1.NullExpected = false;
			WeightedAvg1.ResultSet = 2;
			WeightedAvg1.RowNumber = 1;
			// 
			// WeightedAvg2
			// 
			WeightedAvg2.ColumnNumber = 2;
			WeightedAvg2.Enabled = true;
			WeightedAvg2.ExpectedValue = "3";
			WeightedAvg2.Name = "WeightedAvg2";
			WeightedAvg2.NullExpected = false;
			WeightedAvg2.ResultSet = 2;
			WeightedAvg2.RowNumber = 1;
			// 
			// testInitializeAction
			// 
			resources.ApplyResources(testInitializeAction, "testInitializeAction");
			// 
			// testCleanupAction
			// 
			resources.ApplyResources(testCleanupAction, "testCleanupAction");
			// 
			// AggregatesTestDataNotEmpty_TestAction
			// 
			AggregatesTestDataNotEmpty_TestAction.Conditions.Add(TestDataRowCount);
			resources.ApplyResources(AggregatesTestDataNotEmpty_TestAction, "AggregatesTestDataNotEmpty_TestAction");
			// 
			// TestDataRowCount
			// 
			TestDataRowCount.Enabled = true;
			TestDataRowCount.Name = "TestDataRowCount";
			TestDataRowCount.ResultSet = 1;
			TestDataRowCount.RowCount = 500;
			// 
			// AggregatesConcatTestsData
			// 
			this.AggregatesConcatTestsData.PosttestAction = null;
			this.AggregatesConcatTestsData.PretestAction = null;
			this.AggregatesConcatTestsData.TestAction = AggregatesConcatTests_TestAction;
			// 
			// AggregatesMedianTestsData
			// 
			this.AggregatesMedianTestsData.PosttestAction = null;
			this.AggregatesMedianTestsData.PretestAction = null;
			this.AggregatesMedianTestsData.TestAction = AggregatesMedianTests_TestAction;
			// 
			// AggregatesPercentileTestsData
			// 
			this.AggregatesPercentileTestsData.PosttestAction = null;
			this.AggregatesPercentileTestsData.PretestAction = null;
			this.AggregatesPercentileTestsData.TestAction = AggregatesPercentileTests_TestAction;
			// 
			// AggregatesWeightedAvgTestsData
			// 
			this.AggregatesWeightedAvgTestsData.PosttestAction = null;
			this.AggregatesWeightedAvgTestsData.PretestAction = null;
			this.AggregatesWeightedAvgTestsData.TestAction = AggregatesWeightedAvgTests_TestAction;
			// 
			// AggregatesTestDataNotEmptyData
			// 
			this.AggregatesTestDataNotEmptyData.PosttestAction = null;
			this.AggregatesTestDataNotEmptyData.PretestAction = null;
			this.AggregatesTestDataNotEmptyData.TestAction = AggregatesTestDataNotEmpty_TestAction;
			// 
			// AggregatesTest
			// 
			this.TestCleanupAction = testCleanupAction;
			this.TestInitializeAction = testInitializeAction;
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

		private SqlDatabaseTestActions AggregatesConcatTestsData;
		private SqlDatabaseTestActions AggregatesMedianTestsData;
		private SqlDatabaseTestActions AggregatesPercentileTestsData;
		private SqlDatabaseTestActions AggregatesWeightedAvgTestsData;
		private SqlDatabaseTestActions AggregatesTestDataNotEmptyData;
	}
}
