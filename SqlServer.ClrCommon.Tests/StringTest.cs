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
	public class StringTest : SqlDatabaseTestClass
	{

		public StringTest()
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
		public void Base64Tests()
		{
			SqlDatabaseTestActions testActions = this.Base64TestsData;
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
		public void BetterQuoteNameTests()
		{
			SqlDatabaseTestActions testActions = this.BetterQuoteNameTestsData;
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
		public void FormatStringNTests()
		{
			SqlDatabaseTestActions testActions = this.FormatStringNTestsData;
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
		public void FormatTests()
		{
			SqlDatabaseTestActions testActions = this.FormatTestsData;
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
		public void PadTests()
		{
			SqlDatabaseTestActions testActions = this.PadTestsData;
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
		public void ProperCasesTests()
		{
			SqlDatabaseTestActions testActions = this.ProperCasesTestsData;
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
		public void StringCountAndIndexTests()
		{
			SqlDatabaseTestActions testActions = this.StringCountAndIndexTestsData;
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
		public void TryParseTests()
		{
			SqlDatabaseTestActions testActions = this.TryParseTestsData;
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
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction Base64Tests_TestAction;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StringTest));
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition Base641;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition Base642;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition Base643;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition Base644;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition Base645;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction BetterQuoteNameTests_TestAction;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition BetterQuoteName1;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition BetterQuoteName2;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition BetterQuoteName3;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition BetterQuoteName4;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition BetterQuoteName5;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition BetterQuoteName6;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition BetterQuoteNameNullValue;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition BetterQuoteNameEmptyString;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction FormatStringNTests_TestAction;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition FormatString3_Test1;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ChecksumCondition FormatString_CheckSum;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction FormatTests_TestAction;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition Format_MMddyyyy;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition Format_yyyyMMdd;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition FormatNumberCase1;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition FormatNumberCase2;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition FormatNullValue;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction PadTests_TestAction;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition PadLeft;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition PadRight;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition PadLeftNull;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition PadRightNull;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition PadLeftNoPad;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition PadRightNoPad;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition PadLeftEmptyString;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition PadRightEmptyString;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction ProperCasesTests_TestAction;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition ProperCase;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition ProperCaseNullValue;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition ProperCaseEmptyString;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction StringCountAndIndexTests_TestAction;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition CountString1;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition CountStringNullValue;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition CountStringEmptyString;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition IndexOf1;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition IndexOfNullValue;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition IndexOfEmptyString;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition LastIndexOf1;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition LastIndexOfNullValue;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition LastIndexOfEmptyString;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition NthIndexOf1;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition NthIndexOf2;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition NthIndexOf3;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition NthIndexOfNullValue;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction TryParseTests_TestAction;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition TryParseInt1;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition TryParseInt2;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition TryParseInt3;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition TryParseInt4;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition TryParseInt5;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition TryParseInt6;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition TryParseDbl1;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition TryParseDbl2;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition TryParseDbl3;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition TryParseDbl4;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition TryParseDbl5;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition TryParseDbl6;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition BetterQuoteName7;
			this.Base64TestsData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
			this.BetterQuoteNameTestsData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
			this.FormatStringNTestsData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
			this.FormatTestsData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
			this.PadTestsData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
			this.ProperCasesTestsData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
			this.StringCountAndIndexTestsData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
			this.TryParseTestsData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
			Base64Tests_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
			Base641 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			Base642 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			Base643 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			Base644 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			Base645 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			BetterQuoteNameTests_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
			BetterQuoteName1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			BetterQuoteName2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			BetterQuoteName3 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			BetterQuoteName4 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			BetterQuoteName5 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			BetterQuoteName6 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			BetterQuoteNameNullValue = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			BetterQuoteNameEmptyString = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			FormatStringNTests_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
			FormatString3_Test1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			FormatString_CheckSum = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ChecksumCondition();
			FormatTests_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
			Format_MMddyyyy = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			Format_yyyyMMdd = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			FormatNumberCase1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			FormatNumberCase2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			FormatNullValue = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			PadTests_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
			PadLeft = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			PadRight = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			PadLeftNull = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			PadRightNull = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			PadLeftNoPad = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			PadRightNoPad = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			PadLeftEmptyString = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			PadRightEmptyString = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			ProperCasesTests_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
			ProperCase = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			ProperCaseNullValue = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			ProperCaseEmptyString = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			StringCountAndIndexTests_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
			CountString1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			CountStringNullValue = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			CountStringEmptyString = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			IndexOf1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			IndexOfNullValue = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			IndexOfEmptyString = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			LastIndexOf1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			LastIndexOfNullValue = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			LastIndexOfEmptyString = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			NthIndexOf1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			NthIndexOf2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			NthIndexOf3 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			NthIndexOfNullValue = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			TryParseTests_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
			TryParseInt1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			TryParseInt2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			TryParseInt3 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			TryParseInt4 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			TryParseInt5 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			TryParseInt6 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			TryParseDbl1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			TryParseDbl2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			TryParseDbl3 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			TryParseDbl4 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			TryParseDbl5 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			TryParseDbl6 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			BetterQuoteName7 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			// 
			// Base64Tests_TestAction
			// 
			Base64Tests_TestAction.Conditions.Add(Base641);
			Base64Tests_TestAction.Conditions.Add(Base642);
			Base64Tests_TestAction.Conditions.Add(Base643);
			Base64Tests_TestAction.Conditions.Add(Base644);
			Base64Tests_TestAction.Conditions.Add(Base645);
			resources.ApplyResources(Base64Tests_TestAction, "Base64Tests_TestAction");
			// 
			// Base641
			// 
			Base641.ColumnNumber = 1;
			Base641.Enabled = true;
			Base641.ExpectedValue = "1";
			Base641.Name = "Base641";
			Base641.NullExpected = false;
			Base641.ResultSet = 1;
			Base641.RowNumber = 1;
			// 
			// Base642
			// 
			Base642.ColumnNumber = 2;
			Base642.Enabled = true;
			Base642.ExpectedValue = "1";
			Base642.Name = "Base642";
			Base642.NullExpected = false;
			Base642.ResultSet = 1;
			Base642.RowNumber = 1;
			// 
			// Base643
			// 
			Base643.ColumnNumber = 3;
			Base643.Enabled = true;
			Base643.ExpectedValue = "1";
			Base643.Name = "Base643";
			Base643.NullExpected = false;
			Base643.ResultSet = 1;
			Base643.RowNumber = 1;
			// 
			// Base644
			// 
			Base644.ColumnNumber = 4;
			Base644.Enabled = true;
			Base644.ExpectedValue = "1";
			Base644.Name = "Base644";
			Base644.NullExpected = false;
			Base644.ResultSet = 1;
			Base644.RowNumber = 1;
			// 
			// Base645
			// 
			Base645.ColumnNumber = 5;
			Base645.Enabled = true;
			Base645.ExpectedValue = "1";
			Base645.Name = "Base645";
			Base645.NullExpected = false;
			Base645.ResultSet = 1;
			Base645.RowNumber = 1;
			// 
			// BetterQuoteNameTests_TestAction
			// 
			BetterQuoteNameTests_TestAction.Conditions.Add(BetterQuoteName1);
			BetterQuoteNameTests_TestAction.Conditions.Add(BetterQuoteName2);
			BetterQuoteNameTests_TestAction.Conditions.Add(BetterQuoteName3);
			BetterQuoteNameTests_TestAction.Conditions.Add(BetterQuoteName4);
			BetterQuoteNameTests_TestAction.Conditions.Add(BetterQuoteName5);
			BetterQuoteNameTests_TestAction.Conditions.Add(BetterQuoteName6);
			BetterQuoteNameTests_TestAction.Conditions.Add(BetterQuoteName7);
			BetterQuoteNameTests_TestAction.Conditions.Add(BetterQuoteNameNullValue);
			BetterQuoteNameTests_TestAction.Conditions.Add(BetterQuoteNameEmptyString);
			resources.ApplyResources(BetterQuoteNameTests_TestAction, "BetterQuoteNameTests_TestAction");
			// 
			// BetterQuoteName1
			// 
			BetterQuoteName1.ColumnNumber = 1;
			BetterQuoteName1.Enabled = true;
			BetterQuoteName1.ExpectedValue = "[dbo.TABLE_NAME]";
			BetterQuoteName1.Name = "BetterQuoteName1";
			BetterQuoteName1.NullExpected = false;
			BetterQuoteName1.ResultSet = 1;
			BetterQuoteName1.RowNumber = 1;
			// 
			// BetterQuoteName2
			// 
			BetterQuoteName2.ColumnNumber = 2;
			BetterQuoteName2.Enabled = true;
			BetterQuoteName2.ExpectedValue = "[dbo].[TABLE_NAME] As [tbl]";
			BetterQuoteName2.Name = "BetterQuoteName2";
			BetterQuoteName2.NullExpected = false;
			BetterQuoteName2.ResultSet = 1;
			BetterQuoteName2.RowNumber = 1;
			// 
			// BetterQuoteName3
			// 
			BetterQuoteName3.ColumnNumber = 3;
			BetterQuoteName3.Enabled = true;
			BetterQuoteName3.ExpectedValue = "[dbo].[OBJECT_NAME] As [OBJECT_ALIAS]";
			BetterQuoteName3.Name = "BetterQuoteName3";
			BetterQuoteName3.NullExpected = false;
			BetterQuoteName3.ResultSet = 1;
			BetterQuoteName3.RowNumber = 1;
			// 
			// BetterQuoteName4
			// 
			BetterQuoteName4.ColumnNumber = 4;
			BetterQuoteName4.Enabled = true;
			BetterQuoteName4.ExpectedValue = "[Column1], [Column2], [Column3], [Column4], [Column5], [Column6], [;here] As [is " +
				"my sql injection; --]";
			BetterQuoteName4.Name = "BetterQuoteName4";
			BetterQuoteName4.NullExpected = false;
			BetterQuoteName4.ResultSet = 1;
			BetterQuoteName4.RowNumber = 1;
			// 
			// BetterQuoteName5
			// 
			BetterQuoteName5.ColumnNumber = 5;
			BetterQuoteName5.Enabled = true;
			BetterQuoteName5.ExpectedValue = "[tbl].[Column1], [tbl].[Column2], [tbl].[Column3]";
			BetterQuoteName5.Name = "BetterQuoteName5";
			BetterQuoteName5.NullExpected = false;
			BetterQuoteName5.ResultSet = 1;
			BetterQuoteName5.RowNumber = 1;
			// 
			// BetterQuoteName6
			// 
			BetterQuoteName6.ColumnNumber = 6;
			BetterQuoteName6.Enabled = true;
			BetterQuoteName6.ExpectedValue = "[dbname].[tbl].[Column1], [dbname].[tbl].[Column2], [tbl].[Column3]";
			BetterQuoteName6.Name = "BetterQuoteName6";
			BetterQuoteName6.NullExpected = false;
			BetterQuoteName6.ResultSet = 1;
			BetterQuoteName6.RowNumber = 1;
			// 
			// BetterQuoteNameNullValue
			// 
			BetterQuoteNameNullValue.ColumnNumber = 8;
			BetterQuoteNameNullValue.Enabled = true;
			BetterQuoteNameNullValue.ExpectedValue = null;
			BetterQuoteNameNullValue.Name = "BetterQuoteNameNullValue";
			BetterQuoteNameNullValue.NullExpected = true;
			BetterQuoteNameNullValue.ResultSet = 1;
			BetterQuoteNameNullValue.RowNumber = 1;
			// 
			// BetterQuoteNameEmptyString
			// 
			BetterQuoteNameEmptyString.ColumnNumber = 9;
			BetterQuoteNameEmptyString.Enabled = true;
			BetterQuoteNameEmptyString.ExpectedValue = null;
			BetterQuoteNameEmptyString.Name = "BetterQuoteNameEmptyString";
			BetterQuoteNameEmptyString.NullExpected = true;
			BetterQuoteNameEmptyString.ResultSet = 1;
			BetterQuoteNameEmptyString.RowNumber = 1;
			// 
			// FormatStringNTests_TestAction
			// 
			FormatStringNTests_TestAction.Conditions.Add(FormatString3_Test1);
			FormatStringNTests_TestAction.Conditions.Add(FormatString_CheckSum);
			resources.ApplyResources(FormatStringNTests_TestAction, "FormatStringNTests_TestAction");
			// 
			// FormatString3_Test1
			// 
			FormatString3_Test1.ColumnNumber = 1;
			FormatString3_Test1.Enabled = true;
			FormatString3_Test1.ExpectedValue = "This is arg 0:(foo), arg 1:(bar), and arg 2 as a date: (2015-07-27), and arg 2 as" +
				" a time: (13:05:10:477)";
			FormatString3_Test1.Name = "FormatString3_Test1";
			FormatString3_Test1.NullExpected = false;
			FormatString3_Test1.ResultSet = 1;
			FormatString3_Test1.RowNumber = 1;
			// 
			// FormatString_CheckSum
			// 
			FormatString_CheckSum.Checksum = "-1850417100";
			FormatString_CheckSum.Enabled = true;
			FormatString_CheckSum.Name = "FormatString_CheckSum";
			// 
			// FormatTests_TestAction
			// 
			FormatTests_TestAction.Conditions.Add(Format_MMddyyyy);
			FormatTests_TestAction.Conditions.Add(Format_yyyyMMdd);
			FormatTests_TestAction.Conditions.Add(FormatNumberCase1);
			FormatTests_TestAction.Conditions.Add(FormatNumberCase2);
			FormatTests_TestAction.Conditions.Add(FormatNullValue);
			resources.ApplyResources(FormatTests_TestAction, "FormatTests_TestAction");
			// 
			// Format_MMddyyyy
			// 
			Format_MMddyyyy.ColumnNumber = 1;
			Format_MMddyyyy.Enabled = true;
			Format_MMddyyyy.ExpectedValue = "07/27/2015";
			Format_MMddyyyy.Name = "Format_MMddyyyy";
			Format_MMddyyyy.NullExpected = false;
			Format_MMddyyyy.ResultSet = 1;
			Format_MMddyyyy.RowNumber = 1;
			// 
			// Format_yyyyMMdd
			// 
			Format_yyyyMMdd.ColumnNumber = 2;
			Format_yyyyMMdd.Enabled = true;
			Format_yyyyMMdd.ExpectedValue = "2015-07-27";
			Format_yyyyMMdd.Name = "Format_yyyyMMdd";
			Format_yyyyMMdd.NullExpected = false;
			Format_yyyyMMdd.ResultSet = 1;
			Format_yyyyMMdd.RowNumber = 1;
			// 
			// FormatNumberCase1
			// 
			FormatNumberCase1.ColumnNumber = 3;
			FormatNumberCase1.Enabled = true;
			FormatNumberCase1.ExpectedValue = "1,234.43";
			FormatNumberCase1.Name = "FormatNumberCase1";
			FormatNumberCase1.NullExpected = false;
			FormatNumberCase1.ResultSet = 1;
			FormatNumberCase1.RowNumber = 1;
			// 
			// FormatNumberCase2
			// 
			FormatNumberCase2.ColumnNumber = 4;
			FormatNumberCase2.Enabled = true;
			FormatNumberCase2.ExpectedValue = "1,234.00";
			FormatNumberCase2.Name = "FormatNumberCase2";
			FormatNumberCase2.NullExpected = false;
			FormatNumberCase2.ResultSet = 1;
			FormatNumberCase2.RowNumber = 1;
			// 
			// FormatNullValue
			// 
			FormatNullValue.ColumnNumber = 5;
			FormatNullValue.Enabled = true;
			FormatNullValue.ExpectedValue = null;
			FormatNullValue.Name = "FormatNullValue";
			FormatNullValue.NullExpected = true;
			FormatNullValue.ResultSet = 1;
			FormatNullValue.RowNumber = 1;
			// 
			// PadTests_TestAction
			// 
			PadTests_TestAction.Conditions.Add(PadLeft);
			PadTests_TestAction.Conditions.Add(PadRight);
			PadTests_TestAction.Conditions.Add(PadLeftNull);
			PadTests_TestAction.Conditions.Add(PadRightNull);
			PadTests_TestAction.Conditions.Add(PadLeftNoPad);
			PadTests_TestAction.Conditions.Add(PadRightNoPad);
			PadTests_TestAction.Conditions.Add(PadLeftEmptyString);
			PadTests_TestAction.Conditions.Add(PadRightEmptyString);
			resources.ApplyResources(PadTests_TestAction, "PadTests_TestAction");
			// 
			// PadLeft
			// 
			PadLeft.ColumnNumber = 1;
			PadLeft.Enabled = true;
			PadLeft.ExpectedValue = "0000009999";
			PadLeft.Name = "PadLeft";
			PadLeft.NullExpected = false;
			PadLeft.ResultSet = 1;
			PadLeft.RowNumber = 1;
			// 
			// PadRight
			// 
			PadRight.ColumnNumber = 2;
			PadRight.Enabled = true;
			PadRight.ExpectedValue = "9999000000";
			PadRight.Name = "PadRight";
			PadRight.NullExpected = false;
			PadRight.ResultSet = 1;
			PadRight.RowNumber = 1;
			// 
			// PadLeftNull
			// 
			PadLeftNull.ColumnNumber = 3;
			PadLeftNull.Enabled = true;
			PadLeftNull.ExpectedValue = "0000000000";
			PadLeftNull.Name = "PadLeftNull";
			PadLeftNull.NullExpected = false;
			PadLeftNull.ResultSet = 1;
			PadLeftNull.RowNumber = 1;
			// 
			// PadRightNull
			// 
			PadRightNull.ColumnNumber = 4;
			PadRightNull.Enabled = true;
			PadRightNull.ExpectedValue = "0000000000";
			PadRightNull.Name = "PadRightNull";
			PadRightNull.NullExpected = false;
			PadRightNull.ResultSet = 1;
			PadRightNull.RowNumber = 1;
			// 
			// PadLeftNoPad
			// 
			PadLeftNoPad.ColumnNumber = 5;
			PadLeftNoPad.Enabled = true;
			PadLeftNoPad.ExpectedValue = "9999";
			PadLeftNoPad.Name = "PadLeftNoPad";
			PadLeftNoPad.NullExpected = false;
			PadLeftNoPad.ResultSet = 1;
			PadLeftNoPad.RowNumber = 1;
			// 
			// PadRightNoPad
			// 
			PadRightNoPad.ColumnNumber = 6;
			PadRightNoPad.Enabled = true;
			PadRightNoPad.ExpectedValue = "9999";
			PadRightNoPad.Name = "PadRightNoPad";
			PadRightNoPad.NullExpected = false;
			PadRightNoPad.ResultSet = 1;
			PadRightNoPad.RowNumber = 1;
			// 
			// PadLeftEmptyString
			// 
			PadLeftEmptyString.ColumnNumber = 7;
			PadLeftEmptyString.Enabled = true;
			PadLeftEmptyString.ExpectedValue = "0000";
			PadLeftEmptyString.Name = "PadLeftEmptyString";
			PadLeftEmptyString.NullExpected = false;
			PadLeftEmptyString.ResultSet = 1;
			PadLeftEmptyString.RowNumber = 1;
			// 
			// PadRightEmptyString
			// 
			PadRightEmptyString.ColumnNumber = 8;
			PadRightEmptyString.Enabled = true;
			PadRightEmptyString.ExpectedValue = "0000";
			PadRightEmptyString.Name = "PadRightEmptyString";
			PadRightEmptyString.NullExpected = false;
			PadRightEmptyString.ResultSet = 1;
			PadRightEmptyString.RowNumber = 1;
			// 
			// ProperCasesTests_TestAction
			// 
			ProperCasesTests_TestAction.Conditions.Add(ProperCase);
			ProperCasesTests_TestAction.Conditions.Add(ProperCaseNullValue);
			ProperCasesTests_TestAction.Conditions.Add(ProperCaseEmptyString);
			resources.ApplyResources(ProperCasesTests_TestAction, "ProperCasesTests_TestAction");
			// 
			// ProperCase
			// 
			ProperCase.ColumnNumber = 1;
			ProperCase.Enabled = true;
			ProperCase.ExpectedValue = "John Doe";
			ProperCase.Name = "ProperCase";
			ProperCase.NullExpected = false;
			ProperCase.ResultSet = 1;
			ProperCase.RowNumber = 1;
			// 
			// ProperCaseNullValue
			// 
			ProperCaseNullValue.ColumnNumber = 2;
			ProperCaseNullValue.Enabled = true;
			ProperCaseNullValue.ExpectedValue = null;
			ProperCaseNullValue.Name = "ProperCaseNullValue";
			ProperCaseNullValue.NullExpected = true;
			ProperCaseNullValue.ResultSet = 1;
			ProperCaseNullValue.RowNumber = 1;
			// 
			// ProperCaseEmptyString
			// 
			ProperCaseEmptyString.ColumnNumber = 3;
			ProperCaseEmptyString.Enabled = true;
			ProperCaseEmptyString.ExpectedValue = null;
			ProperCaseEmptyString.Name = "ProperCaseEmptyString";
			ProperCaseEmptyString.NullExpected = true;
			ProperCaseEmptyString.ResultSet = 1;
			ProperCaseEmptyString.RowNumber = 1;
			// 
			// StringCountAndIndexTests_TestAction
			// 
			StringCountAndIndexTests_TestAction.Conditions.Add(CountString1);
			StringCountAndIndexTests_TestAction.Conditions.Add(CountStringNullValue);
			StringCountAndIndexTests_TestAction.Conditions.Add(CountStringEmptyString);
			StringCountAndIndexTests_TestAction.Conditions.Add(IndexOf1);
			StringCountAndIndexTests_TestAction.Conditions.Add(IndexOfNullValue);
			StringCountAndIndexTests_TestAction.Conditions.Add(IndexOfEmptyString);
			StringCountAndIndexTests_TestAction.Conditions.Add(LastIndexOf1);
			StringCountAndIndexTests_TestAction.Conditions.Add(LastIndexOfNullValue);
			StringCountAndIndexTests_TestAction.Conditions.Add(LastIndexOfEmptyString);
			StringCountAndIndexTests_TestAction.Conditions.Add(NthIndexOf1);
			StringCountAndIndexTests_TestAction.Conditions.Add(NthIndexOf2);
			StringCountAndIndexTests_TestAction.Conditions.Add(NthIndexOf3);
			StringCountAndIndexTests_TestAction.Conditions.Add(NthIndexOfNullValue);
			resources.ApplyResources(StringCountAndIndexTests_TestAction, "StringCountAndIndexTests_TestAction");
			// 
			// CountString1
			// 
			CountString1.ColumnNumber = 1;
			CountString1.Enabled = true;
			CountString1.ExpectedValue = "2";
			CountString1.Name = "CountString1";
			CountString1.NullExpected = false;
			CountString1.ResultSet = 1;
			CountString1.RowNumber = 1;
			// 
			// CountStringNullValue
			// 
			CountStringNullValue.ColumnNumber = 2;
			CountStringNullValue.Enabled = true;
			CountStringNullValue.ExpectedValue = "0";
			CountStringNullValue.Name = "CountStringNullValue";
			CountStringNullValue.NullExpected = false;
			CountStringNullValue.ResultSet = 1;
			CountStringNullValue.RowNumber = 1;
			// 
			// CountStringEmptyString
			// 
			CountStringEmptyString.ColumnNumber = 3;
			CountStringEmptyString.Enabled = true;
			CountStringEmptyString.ExpectedValue = "0";
			CountStringEmptyString.Name = "CountStringEmptyString";
			CountStringEmptyString.NullExpected = false;
			CountStringEmptyString.ResultSet = 1;
			CountStringEmptyString.RowNumber = 1;
			// 
			// IndexOf1
			// 
			IndexOf1.ColumnNumber = 1;
			IndexOf1.Enabled = true;
			IndexOf1.ExpectedValue = "6";
			IndexOf1.Name = "IndexOf1";
			IndexOf1.NullExpected = false;
			IndexOf1.ResultSet = 2;
			IndexOf1.RowNumber = 1;
			// 
			// IndexOfNullValue
			// 
			IndexOfNullValue.ColumnNumber = 2;
			IndexOfNullValue.Enabled = true;
			IndexOfNullValue.ExpectedValue = "-1";
			IndexOfNullValue.Name = "IndexOfNullValue";
			IndexOfNullValue.NullExpected = false;
			IndexOfNullValue.ResultSet = 2;
			IndexOfNullValue.RowNumber = 1;
			// 
			// IndexOfEmptyString
			// 
			IndexOfEmptyString.ColumnNumber = 3;
			IndexOfEmptyString.Enabled = true;
			IndexOfEmptyString.ExpectedValue = "-1";
			IndexOfEmptyString.Name = "IndexOfEmptyString";
			IndexOfEmptyString.NullExpected = false;
			IndexOfEmptyString.ResultSet = 2;
			IndexOfEmptyString.RowNumber = 1;
			// 
			// LastIndexOf1
			// 
			LastIndexOf1.ColumnNumber = 1;
			LastIndexOf1.Enabled = true;
			LastIndexOf1.ExpectedValue = "34";
			LastIndexOf1.Name = "LastIndexOf1";
			LastIndexOf1.NullExpected = false;
			LastIndexOf1.ResultSet = 3;
			LastIndexOf1.RowNumber = 1;
			// 
			// LastIndexOfNullValue
			// 
			LastIndexOfNullValue.ColumnNumber = 2;
			LastIndexOfNullValue.Enabled = true;
			LastIndexOfNullValue.ExpectedValue = "-1";
			LastIndexOfNullValue.Name = "LastIndexOfNullValue";
			LastIndexOfNullValue.NullExpected = false;
			LastIndexOfNullValue.ResultSet = 3;
			LastIndexOfNullValue.RowNumber = 1;
			// 
			// LastIndexOfEmptyString
			// 
			LastIndexOfEmptyString.ColumnNumber = 3;
			LastIndexOfEmptyString.Enabled = true;
			LastIndexOfEmptyString.ExpectedValue = "-1";
			LastIndexOfEmptyString.Name = "LastIndexOfEmptyString";
			LastIndexOfEmptyString.NullExpected = false;
			LastIndexOfEmptyString.ResultSet = 3;
			LastIndexOfEmptyString.RowNumber = 1;
			// 
			// NthIndexOf1
			// 
			NthIndexOf1.ColumnNumber = 1;
			NthIndexOf1.Enabled = true;
			NthIndexOf1.ExpectedValue = "6";
			NthIndexOf1.Name = "NthIndexOf1";
			NthIndexOf1.NullExpected = false;
			NthIndexOf1.ResultSet = 4;
			NthIndexOf1.RowNumber = 1;
			// 
			// NthIndexOf2
			// 
			NthIndexOf2.ColumnNumber = 2;
			NthIndexOf2.Enabled = true;
			NthIndexOf2.ExpectedValue = "34";
			NthIndexOf2.Name = "NthIndexOf2";
			NthIndexOf2.NullExpected = false;
			NthIndexOf2.ResultSet = 4;
			NthIndexOf2.RowNumber = 1;
			// 
			// NthIndexOf3
			// 
			NthIndexOf3.ColumnNumber = 3;
			NthIndexOf3.Enabled = true;
			NthIndexOf3.ExpectedValue = "-1";
			NthIndexOf3.Name = "NthIndexOf3";
			NthIndexOf3.NullExpected = false;
			NthIndexOf3.ResultSet = 4;
			NthIndexOf3.RowNumber = 1;
			// 
			// NthIndexOfNullValue
			// 
			NthIndexOfNullValue.ColumnNumber = 4;
			NthIndexOfNullValue.Enabled = true;
			NthIndexOfNullValue.ExpectedValue = "-1";
			NthIndexOfNullValue.Name = "NthIndexOfNullValue";
			NthIndexOfNullValue.NullExpected = false;
			NthIndexOfNullValue.ResultSet = 4;
			NthIndexOfNullValue.RowNumber = 1;
			// 
			// TryParseTests_TestAction
			// 
			TryParseTests_TestAction.Conditions.Add(TryParseInt1);
			TryParseTests_TestAction.Conditions.Add(TryParseInt2);
			TryParseTests_TestAction.Conditions.Add(TryParseInt3);
			TryParseTests_TestAction.Conditions.Add(TryParseInt4);
			TryParseTests_TestAction.Conditions.Add(TryParseInt5);
			TryParseTests_TestAction.Conditions.Add(TryParseInt6);
			TryParseTests_TestAction.Conditions.Add(TryParseDbl1);
			TryParseTests_TestAction.Conditions.Add(TryParseDbl2);
			TryParseTests_TestAction.Conditions.Add(TryParseDbl3);
			TryParseTests_TestAction.Conditions.Add(TryParseDbl4);
			TryParseTests_TestAction.Conditions.Add(TryParseDbl5);
			TryParseTests_TestAction.Conditions.Add(TryParseDbl6);
			resources.ApplyResources(TryParseTests_TestAction, "TryParseTests_TestAction");
			// 
			// TryParseInt1
			// 
			TryParseInt1.ColumnNumber = 1;
			TryParseInt1.Enabled = true;
			TryParseInt1.ExpectedValue = "123";
			TryParseInt1.Name = "TryParseInt1";
			TryParseInt1.NullExpected = false;
			TryParseInt1.ResultSet = 1;
			TryParseInt1.RowNumber = 1;
			// 
			// TryParseInt2
			// 
			TryParseInt2.ColumnNumber = 2;
			TryParseInt2.Enabled = true;
			TryParseInt2.ExpectedValue = "1";
			TryParseInt2.Name = "TryParseInt2";
			TryParseInt2.NullExpected = false;
			TryParseInt2.ResultSet = 1;
			TryParseInt2.RowNumber = 1;
			// 
			// TryParseInt3
			// 
			TryParseInt3.ColumnNumber = 3;
			TryParseInt3.Enabled = true;
			TryParseInt3.ExpectedValue = "2";
			TryParseInt3.Name = "TryParseInt3";
			TryParseInt3.NullExpected = false;
			TryParseInt3.ResultSet = 1;
			TryParseInt3.RowNumber = 1;
			// 
			// TryParseInt4
			// 
			TryParseInt4.ColumnNumber = 4;
			TryParseInt4.Enabled = true;
			TryParseInt4.ExpectedValue = "3";
			TryParseInt4.Name = "TryParseInt4";
			TryParseInt4.NullExpected = false;
			TryParseInt4.ResultSet = 1;
			TryParseInt4.RowNumber = 1;
			// 
			// TryParseInt5
			// 
			TryParseInt5.ColumnNumber = 5;
			TryParseInt5.Enabled = true;
			TryParseInt5.ExpectedValue = "4";
			TryParseInt5.Name = "TryParseInt5";
			TryParseInt5.NullExpected = false;
			TryParseInt5.ResultSet = 1;
			TryParseInt5.RowNumber = 1;
			// 
			// TryParseInt6
			// 
			TryParseInt6.ColumnNumber = 6;
			TryParseInt6.Enabled = true;
			TryParseInt6.ExpectedValue = "5";
			TryParseInt6.Name = "TryParseInt6";
			TryParseInt6.NullExpected = false;
			TryParseInt6.ResultSet = 1;
			TryParseInt6.RowNumber = 1;
			// 
			// TryParseDbl1
			// 
			TryParseDbl1.ColumnNumber = 1;
			TryParseDbl1.Enabled = true;
			TryParseDbl1.ExpectedValue = "123.45";
			TryParseDbl1.Name = "TryParseDbl1";
			TryParseDbl1.NullExpected = false;
			TryParseDbl1.ResultSet = 2;
			TryParseDbl1.RowNumber = 1;
			// 
			// TryParseDbl2
			// 
			TryParseDbl2.ColumnNumber = 2;
			TryParseDbl2.Enabled = true;
			TryParseDbl2.ExpectedValue = "1.1";
			TryParseDbl2.Name = "TryParseDbl2";
			TryParseDbl2.NullExpected = false;
			TryParseDbl2.ResultSet = 2;
			TryParseDbl2.RowNumber = 1;
			// 
			// TryParseDbl3
			// 
			TryParseDbl3.ColumnNumber = 3;
			TryParseDbl3.Enabled = true;
			TryParseDbl3.ExpectedValue = "1.2";
			TryParseDbl3.Name = "TryParseDbl3";
			TryParseDbl3.NullExpected = false;
			TryParseDbl3.ResultSet = 2;
			TryParseDbl3.RowNumber = 1;
			// 
			// TryParseDbl4
			// 
			TryParseDbl4.ColumnNumber = 4;
			TryParseDbl4.Enabled = true;
			TryParseDbl4.ExpectedValue = "1.3";
			TryParseDbl4.Name = "TryParseDbl4";
			TryParseDbl4.NullExpected = false;
			TryParseDbl4.ResultSet = 2;
			TryParseDbl4.RowNumber = 1;
			// 
			// TryParseDbl5
			// 
			TryParseDbl5.ColumnNumber = 5;
			TryParseDbl5.Enabled = true;
			TryParseDbl5.ExpectedValue = "1.4";
			TryParseDbl5.Name = "TryParseDbl5";
			TryParseDbl5.NullExpected = false;
			TryParseDbl5.ResultSet = 2;
			TryParseDbl5.RowNumber = 1;
			// 
			// TryParseDbl6
			// 
			TryParseDbl6.ColumnNumber = 6;
			TryParseDbl6.Enabled = true;
			TryParseDbl6.ExpectedValue = "1.5";
			TryParseDbl6.Name = "TryParseDbl6";
			TryParseDbl6.NullExpected = false;
			TryParseDbl6.ResultSet = 2;
			TryParseDbl6.RowNumber = 1;
			// 
			// Base64TestsData
			// 
			this.Base64TestsData.PosttestAction = null;
			this.Base64TestsData.PretestAction = null;
			this.Base64TestsData.TestAction = Base64Tests_TestAction;
			// 
			// BetterQuoteNameTestsData
			// 
			this.BetterQuoteNameTestsData.PosttestAction = null;
			this.BetterQuoteNameTestsData.PretestAction = null;
			this.BetterQuoteNameTestsData.TestAction = BetterQuoteNameTests_TestAction;
			// 
			// FormatStringNTestsData
			// 
			this.FormatStringNTestsData.PosttestAction = null;
			this.FormatStringNTestsData.PretestAction = null;
			this.FormatStringNTestsData.TestAction = FormatStringNTests_TestAction;
			// 
			// FormatTestsData
			// 
			this.FormatTestsData.PosttestAction = null;
			this.FormatTestsData.PretestAction = null;
			this.FormatTestsData.TestAction = FormatTests_TestAction;
			// 
			// PadTestsData
			// 
			this.PadTestsData.PosttestAction = null;
			this.PadTestsData.PretestAction = null;
			this.PadTestsData.TestAction = PadTests_TestAction;
			// 
			// ProperCasesTestsData
			// 
			this.ProperCasesTestsData.PosttestAction = null;
			this.ProperCasesTestsData.PretestAction = null;
			this.ProperCasesTestsData.TestAction = ProperCasesTests_TestAction;
			// 
			// StringCountAndIndexTestsData
			// 
			this.StringCountAndIndexTestsData.PosttestAction = null;
			this.StringCountAndIndexTestsData.PretestAction = null;
			this.StringCountAndIndexTestsData.TestAction = StringCountAndIndexTests_TestAction;
			// 
			// TryParseTestsData
			// 
			this.TryParseTestsData.PosttestAction = null;
			this.TryParseTestsData.PretestAction = null;
			this.TryParseTestsData.TestAction = TryParseTests_TestAction;
			// 
			// BetterQuoteName7
			// 
			BetterQuoteName7.ColumnNumber = 7;
			BetterQuoteName7.Enabled = true;
			BetterQuoteName7.ExpectedValue = "[dbo].[OBJECT_NAME] As [OBJECT_ALIAS]";
			BetterQuoteName7.Name = "BetterQuoteName7";
			BetterQuoteName7.NullExpected = false;
			BetterQuoteName7.ResultSet = 1;
			BetterQuoteName7.RowNumber = 1;
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

		private SqlDatabaseTestActions Base64TestsData;
		private SqlDatabaseTestActions BetterQuoteNameTestsData;
		private SqlDatabaseTestActions FormatStringNTestsData;
		private SqlDatabaseTestActions FormatTestsData;
		private SqlDatabaseTestActions PadTestsData;
		private SqlDatabaseTestActions ProperCasesTestsData;
		private SqlDatabaseTestActions StringCountAndIndexTestsData;
		private SqlDatabaseTestActions TryParseTestsData;
	}
}
