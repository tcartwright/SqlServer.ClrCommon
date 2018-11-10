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
	public class RegExTest : SqlDatabaseTestClass
	{

		public RegExTest()
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
		public void RegexValidationEmail()
		{
			SqlDatabaseTestActions testActions = this.RegexValidationEmailData;
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
		public void RegexValidationSSN()
		{
			SqlDatabaseTestActions testActions = this.RegexValidationSSNData;
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
		public void RegexValidationUSPhone()
		{
			SqlDatabaseTestActions testActions = this.RegexValidationUSPhoneData;
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
		public void RegexValidationUSZip()
		{
			SqlDatabaseTestActions testActions = this.RegexValidationUSZipData;
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
		public void RegexMatchTests()
		{
			SqlDatabaseTestActions testActions = this.RegexMatchTestsData;
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
		public void RegexMatchesTests()
		{
			SqlDatabaseTestActions testActions = this.RegexMatchesTestsData;
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
		public void RegexGroupTests()
		{
			SqlDatabaseTestActions testActions = this.RegexGroupTestsData;
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
		public void RegexGroupsTest()
		{
			SqlDatabaseTestActions testActions = this.RegexGroupsTestData;
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
		public void RegexReplaceTests()
		{
			SqlDatabaseTestActions testActions = this.RegexReplaceTestsData;
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
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction RegexValidationEmail_TestAction;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegExTest));
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition RegexIsValidEmail1;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition RegexIsValidEmail2;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition RegexIsValidEmail3;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition RegexIsValidEmail4;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition RegexIsValidEmailValueIsNull;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition RegexIsValidUSZip1;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition RegexIsValidUSZip2;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition RegexIsValidUSZip3;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition RegexIsValidUSZipValueIsNull;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition RegexIsValidUSPhone1;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition RegexIsValidUSPhone2;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition RegexIsValidUSPhoneValueIsNull;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition RegexIsValidSSN1;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition RegexIsValidSSN2;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition RegexIsValidSSN3;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition RegexIsValidSSNValueIsNull;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction RegexValidationSSN_TestAction;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction RegexValidationUSPhone_TestAction;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction RegexValidationUSZip_TestAction;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction RegexMatchTests_TestAction;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition RegexMatch1;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition RegexMatch2;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition RegexMatch3;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition RegexMatch4;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction RegexMatchesTests_TestAction;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition RegexMatches1;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ChecksumCondition RegexMatchesChecksum;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction RegexGroupTests_TestAction;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition RegexGroup1;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition RegexGroup2;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction RegexGroupsTest_TestAction;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ChecksumCondition RegexGroupsChecksum;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition RegexGroupsRowCount;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction RegexReplaceTests_TestAction;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition RegexReplaceCaseInsensitive;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition RegexReplaceCaseSensitive;
			this.RegexValidationEmailData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
			this.RegexValidationSSNData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
			this.RegexValidationUSPhoneData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
			this.RegexValidationUSZipData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
			this.RegexMatchTestsData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
			this.RegexMatchesTestsData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
			this.RegexGroupTestsData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
			this.RegexGroupsTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
			this.RegexReplaceTestsData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
			RegexValidationEmail_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
			RegexIsValidEmail1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			RegexIsValidEmail2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			RegexIsValidEmail3 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			RegexIsValidEmail4 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			RegexIsValidEmailValueIsNull = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			RegexIsValidUSZip1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			RegexIsValidUSZip2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			RegexIsValidUSZip3 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			RegexIsValidUSZipValueIsNull = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			RegexIsValidUSPhone1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			RegexIsValidUSPhone2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			RegexIsValidUSPhoneValueIsNull = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			RegexIsValidSSN1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			RegexIsValidSSN2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			RegexIsValidSSN3 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			RegexIsValidSSNValueIsNull = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			RegexValidationSSN_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
			RegexValidationUSPhone_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
			RegexValidationUSZip_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
			RegexMatchTests_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
			RegexMatch1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			RegexMatch2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			RegexMatch3 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			RegexMatch4 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			RegexMatchesTests_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
			RegexMatches1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			RegexMatchesChecksum = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ChecksumCondition();
			RegexGroupTests_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
			RegexGroup1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			RegexGroup2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			RegexGroupsTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
			RegexGroupsChecksum = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ChecksumCondition();
			RegexGroupsRowCount = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
			RegexReplaceTests_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
			RegexReplaceCaseInsensitive = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			RegexReplaceCaseSensitive = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			// 
			// RegexValidationEmail_TestAction
			// 
			RegexValidationEmail_TestAction.Conditions.Add(RegexIsValidEmail1);
			RegexValidationEmail_TestAction.Conditions.Add(RegexIsValidEmail2);
			RegexValidationEmail_TestAction.Conditions.Add(RegexIsValidEmail3);
			RegexValidationEmail_TestAction.Conditions.Add(RegexIsValidEmail4);
			RegexValidationEmail_TestAction.Conditions.Add(RegexIsValidEmailValueIsNull);
			resources.ApplyResources(RegexValidationEmail_TestAction, "RegexValidationEmail_TestAction");
			// 
			// RegexIsValidEmail1
			// 
			RegexIsValidEmail1.ColumnNumber = 1;
			RegexIsValidEmail1.Enabled = true;
			RegexIsValidEmail1.ExpectedValue = "true";
			RegexIsValidEmail1.Name = "RegexIsValidEmail1";
			RegexIsValidEmail1.NullExpected = false;
			RegexIsValidEmail1.ResultSet = 1;
			RegexIsValidEmail1.RowNumber = 1;
			// 
			// RegexIsValidEmail2
			// 
			RegexIsValidEmail2.ColumnNumber = 2;
			RegexIsValidEmail2.Enabled = true;
			RegexIsValidEmail2.ExpectedValue = "false";
			RegexIsValidEmail2.Name = "RegexIsValidEmail2";
			RegexIsValidEmail2.NullExpected = false;
			RegexIsValidEmail2.ResultSet = 1;
			RegexIsValidEmail2.RowNumber = 1;
			// 
			// RegexIsValidEmail3
			// 
			RegexIsValidEmail3.ColumnNumber = 3;
			RegexIsValidEmail3.Enabled = true;
			RegexIsValidEmail3.ExpectedValue = "false";
			RegexIsValidEmail3.Name = "RegexIsValidEmail3";
			RegexIsValidEmail3.NullExpected = false;
			RegexIsValidEmail3.ResultSet = 1;
			RegexIsValidEmail3.RowNumber = 1;
			// 
			// RegexIsValidEmail4
			// 
			RegexIsValidEmail4.ColumnNumber = 4;
			RegexIsValidEmail4.Enabled = true;
			RegexIsValidEmail4.ExpectedValue = "false";
			RegexIsValidEmail4.Name = "RegexIsValidEmail4";
			RegexIsValidEmail4.NullExpected = false;
			RegexIsValidEmail4.ResultSet = 1;
			RegexIsValidEmail4.RowNumber = 1;
			// 
			// RegexIsValidEmailValueIsNull
			// 
			RegexIsValidEmailValueIsNull.ColumnNumber = 5;
			RegexIsValidEmailValueIsNull.Enabled = true;
			RegexIsValidEmailValueIsNull.ExpectedValue = "false";
			RegexIsValidEmailValueIsNull.Name = "RegexIsValidEmailValueIsNull";
			RegexIsValidEmailValueIsNull.NullExpected = false;
			RegexIsValidEmailValueIsNull.ResultSet = 1;
			RegexIsValidEmailValueIsNull.RowNumber = 1;
			// 
			// RegexIsValidUSZip1
			// 
			RegexIsValidUSZip1.ColumnNumber = 1;
			RegexIsValidUSZip1.Enabled = true;
			RegexIsValidUSZip1.ExpectedValue = "true";
			RegexIsValidUSZip1.Name = "RegexIsValidUSZip1";
			RegexIsValidUSZip1.NullExpected = false;
			RegexIsValidUSZip1.ResultSet = 1;
			RegexIsValidUSZip1.RowNumber = 1;
			// 
			// RegexIsValidUSZip2
			// 
			RegexIsValidUSZip2.ColumnNumber = 2;
			RegexIsValidUSZip2.Enabled = true;
			RegexIsValidUSZip2.ExpectedValue = "true";
			RegexIsValidUSZip2.Name = "RegexIsValidUSZip2";
			RegexIsValidUSZip2.NullExpected = false;
			RegexIsValidUSZip2.ResultSet = 1;
			RegexIsValidUSZip2.RowNumber = 1;
			// 
			// RegexIsValidUSZip3
			// 
			RegexIsValidUSZip3.ColumnNumber = 3;
			RegexIsValidUSZip3.Enabled = true;
			RegexIsValidUSZip3.ExpectedValue = "false";
			RegexIsValidUSZip3.Name = "RegexIsValidUSZip3";
			RegexIsValidUSZip3.NullExpected = false;
			RegexIsValidUSZip3.ResultSet = 1;
			RegexIsValidUSZip3.RowNumber = 1;
			// 
			// RegexIsValidUSZipValueIsNull
			// 
			RegexIsValidUSZipValueIsNull.ColumnNumber = 4;
			RegexIsValidUSZipValueIsNull.Enabled = true;
			RegexIsValidUSZipValueIsNull.ExpectedValue = "false";
			RegexIsValidUSZipValueIsNull.Name = "RegexIsValidUSZipValueIsNull";
			RegexIsValidUSZipValueIsNull.NullExpected = false;
			RegexIsValidUSZipValueIsNull.ResultSet = 1;
			RegexIsValidUSZipValueIsNull.RowNumber = 1;
			// 
			// RegexIsValidUSPhone1
			// 
			RegexIsValidUSPhone1.ColumnNumber = 1;
			RegexIsValidUSPhone1.Enabled = true;
			RegexIsValidUSPhone1.ExpectedValue = "true";
			RegexIsValidUSPhone1.Name = "RegexIsValidUSPhone1";
			RegexIsValidUSPhone1.NullExpected = false;
			RegexIsValidUSPhone1.ResultSet = 1;
			RegexIsValidUSPhone1.RowNumber = 1;
			// 
			// RegexIsValidUSPhone2
			// 
			RegexIsValidUSPhone2.ColumnNumber = 2;
			RegexIsValidUSPhone2.Enabled = true;
			RegexIsValidUSPhone2.ExpectedValue = "false";
			RegexIsValidUSPhone2.Name = "RegexIsValidUSPhone2";
			RegexIsValidUSPhone2.NullExpected = false;
			RegexIsValidUSPhone2.ResultSet = 1;
			RegexIsValidUSPhone2.RowNumber = 1;
			// 
			// RegexIsValidUSPhoneValueIsNull
			// 
			RegexIsValidUSPhoneValueIsNull.ColumnNumber = 3;
			RegexIsValidUSPhoneValueIsNull.Enabled = true;
			RegexIsValidUSPhoneValueIsNull.ExpectedValue = "false";
			RegexIsValidUSPhoneValueIsNull.Name = "RegexIsValidUSPhoneValueIsNull";
			RegexIsValidUSPhoneValueIsNull.NullExpected = false;
			RegexIsValidUSPhoneValueIsNull.ResultSet = 1;
			RegexIsValidUSPhoneValueIsNull.RowNumber = 1;
			// 
			// RegexIsValidSSN1
			// 
			RegexIsValidSSN1.ColumnNumber = 1;
			RegexIsValidSSN1.Enabled = true;
			RegexIsValidSSN1.ExpectedValue = "false";
			RegexIsValidSSN1.Name = "RegexIsValidSSN1";
			RegexIsValidSSN1.NullExpected = false;
			RegexIsValidSSN1.ResultSet = 1;
			RegexIsValidSSN1.RowNumber = 1;
			// 
			// RegexIsValidSSN2
			// 
			RegexIsValidSSN2.ColumnNumber = 2;
			RegexIsValidSSN2.Enabled = true;
			RegexIsValidSSN2.ExpectedValue = "true";
			RegexIsValidSSN2.Name = "RegexIsValidSSN2";
			RegexIsValidSSN2.NullExpected = false;
			RegexIsValidSSN2.ResultSet = 1;
			RegexIsValidSSN2.RowNumber = 1;
			// 
			// RegexIsValidSSN3
			// 
			RegexIsValidSSN3.ColumnNumber = 3;
			RegexIsValidSSN3.Enabled = true;
			RegexIsValidSSN3.ExpectedValue = "false";
			RegexIsValidSSN3.Name = "RegexIsValidSSN3";
			RegexIsValidSSN3.NullExpected = false;
			RegexIsValidSSN3.ResultSet = 1;
			RegexIsValidSSN3.RowNumber = 1;
			// 
			// RegexIsValidSSNValueIsNull
			// 
			RegexIsValidSSNValueIsNull.ColumnNumber = 4;
			RegexIsValidSSNValueIsNull.Enabled = true;
			RegexIsValidSSNValueIsNull.ExpectedValue = "false";
			RegexIsValidSSNValueIsNull.Name = "RegexIsValidSSNValueIsNull";
			RegexIsValidSSNValueIsNull.NullExpected = false;
			RegexIsValidSSNValueIsNull.ResultSet = 1;
			RegexIsValidSSNValueIsNull.RowNumber = 1;
			// 
			// RegexValidationSSN_TestAction
			// 
			RegexValidationSSN_TestAction.Conditions.Add(RegexIsValidSSN1);
			RegexValidationSSN_TestAction.Conditions.Add(RegexIsValidSSN2);
			RegexValidationSSN_TestAction.Conditions.Add(RegexIsValidSSN3);
			RegexValidationSSN_TestAction.Conditions.Add(RegexIsValidSSNValueIsNull);
			resources.ApplyResources(RegexValidationSSN_TestAction, "RegexValidationSSN_TestAction");
			// 
			// RegexValidationUSPhone_TestAction
			// 
			RegexValidationUSPhone_TestAction.Conditions.Add(RegexIsValidUSPhone1);
			RegexValidationUSPhone_TestAction.Conditions.Add(RegexIsValidUSPhone2);
			RegexValidationUSPhone_TestAction.Conditions.Add(RegexIsValidUSPhoneValueIsNull);
			resources.ApplyResources(RegexValidationUSPhone_TestAction, "RegexValidationUSPhone_TestAction");
			// 
			// RegexValidationUSZip_TestAction
			// 
			RegexValidationUSZip_TestAction.Conditions.Add(RegexIsValidUSZip1);
			RegexValidationUSZip_TestAction.Conditions.Add(RegexIsValidUSZip2);
			RegexValidationUSZip_TestAction.Conditions.Add(RegexIsValidUSZip3);
			RegexValidationUSZip_TestAction.Conditions.Add(RegexIsValidUSZipValueIsNull);
			resources.ApplyResources(RegexValidationUSZip_TestAction, "RegexValidationUSZip_TestAction");
			// 
			// RegexMatchTests_TestAction
			// 
			RegexMatchTests_TestAction.Conditions.Add(RegexMatch1);
			RegexMatchTests_TestAction.Conditions.Add(RegexMatch2);
			RegexMatchTests_TestAction.Conditions.Add(RegexMatch3);
			RegexMatchTests_TestAction.Conditions.Add(RegexMatch4);
			resources.ApplyResources(RegexMatchTests_TestAction, "RegexMatchTests_TestAction");
			// 
			// RegexMatch1
			// 
			RegexMatch1.ColumnNumber = 1;
			RegexMatch1.Enabled = true;
			RegexMatch1.ExpectedValue = "true";
			RegexMatch1.Name = "RegexMatch1";
			RegexMatch1.NullExpected = false;
			RegexMatch1.ResultSet = 1;
			RegexMatch1.RowNumber = 1;
			// 
			// RegexMatch2
			// 
			RegexMatch2.ColumnNumber = 2;
			RegexMatch2.Enabled = true;
			RegexMatch2.ExpectedValue = "false";
			RegexMatch2.Name = "RegexMatch2";
			RegexMatch2.NullExpected = false;
			RegexMatch2.ResultSet = 1;
			RegexMatch2.RowNumber = 1;
			// 
			// RegexMatch3
			// 
			RegexMatch3.ColumnNumber = 3;
			RegexMatch3.Enabled = true;
			RegexMatch3.ExpectedValue = "true";
			RegexMatch3.Name = "RegexMatch3";
			RegexMatch3.NullExpected = false;
			RegexMatch3.ResultSet = 1;
			RegexMatch3.RowNumber = 1;
			// 
			// RegexMatch4
			// 
			RegexMatch4.ColumnNumber = 4;
			RegexMatch4.Enabled = true;
			RegexMatch4.ExpectedValue = "false";
			RegexMatch4.Name = "RegexMatch4";
			RegexMatch4.NullExpected = false;
			RegexMatch4.ResultSet = 1;
			RegexMatch4.RowNumber = 1;
			// 
			// RegexMatchesTests_TestAction
			// 
			RegexMatchesTests_TestAction.Conditions.Add(RegexMatches1);
			RegexMatchesTests_TestAction.Conditions.Add(RegexMatchesChecksum);
			resources.ApplyResources(RegexMatchesTests_TestAction, "RegexMatchesTests_TestAction");
			// 
			// RegexMatches1
			// 
			RegexMatches1.ColumnNumber = 1;
			RegexMatches1.Enabled = true;
			RegexMatches1.ExpectedValue = "117";
			RegexMatches1.Name = "RegexMatches1";
			RegexMatches1.NullExpected = false;
			RegexMatches1.ResultSet = 1;
			RegexMatches1.RowNumber = 1;
			// 
			// RegexMatchesChecksum
			// 
			RegexMatchesChecksum.Checksum = "1901336314";
			RegexMatchesChecksum.Enabled = true;
			RegexMatchesChecksum.Name = "RegexMatchesChecksum";
			// 
			// RegexGroupTests_TestAction
			// 
			RegexGroupTests_TestAction.Conditions.Add(RegexGroup1);
			RegexGroupTests_TestAction.Conditions.Add(RegexGroup2);
			resources.ApplyResources(RegexGroupTests_TestAction, "RegexGroupTests_TestAction");
			// 
			// RegexGroup1
			// 
			RegexGroup1.ColumnNumber = 1;
			RegexGroup1.Enabled = true;
			RegexGroup1.ExpectedValue = "ipsum1";
			RegexGroup1.Name = "RegexGroup1";
			RegexGroup1.NullExpected = false;
			RegexGroup1.ResultSet = 1;
			RegexGroup1.RowNumber = 1;
			// 
			// RegexGroup2
			// 
			RegexGroup2.ColumnNumber = 2;
			RegexGroup2.Enabled = true;
			RegexGroup2.ExpectedValue = "Sir Mix a Lot";
			RegexGroup2.Name = "RegexGroup2";
			RegexGroup2.NullExpected = false;
			RegexGroup2.ResultSet = 1;
			RegexGroup2.RowNumber = 1;
			// 
			// RegexGroupsTest_TestAction
			// 
			RegexGroupsTest_TestAction.Conditions.Add(RegexGroupsChecksum);
			RegexGroupsTest_TestAction.Conditions.Add(RegexGroupsRowCount);
			resources.ApplyResources(RegexGroupsTest_TestAction, "RegexGroupsTest_TestAction");
			// 
			// RegexGroupsChecksum
			// 
			RegexGroupsChecksum.Checksum = "832971409";
			RegexGroupsChecksum.Enabled = true;
			RegexGroupsChecksum.Name = "RegexGroupsChecksum";
			// 
			// RegexGroupsRowCount
			// 
			RegexGroupsRowCount.Enabled = true;
			RegexGroupsRowCount.Name = "RegexGroupsRowCount";
			RegexGroupsRowCount.ResultSet = 1;
			RegexGroupsRowCount.RowCount = 5;
			// 
			// RegexValidationEmailData
			// 
			this.RegexValidationEmailData.PosttestAction = null;
			this.RegexValidationEmailData.PretestAction = null;
			this.RegexValidationEmailData.TestAction = RegexValidationEmail_TestAction;
			// 
			// RegexValidationSSNData
			// 
			this.RegexValidationSSNData.PosttestAction = null;
			this.RegexValidationSSNData.PretestAction = null;
			this.RegexValidationSSNData.TestAction = RegexValidationSSN_TestAction;
			// 
			// RegexValidationUSPhoneData
			// 
			this.RegexValidationUSPhoneData.PosttestAction = null;
			this.RegexValidationUSPhoneData.PretestAction = null;
			this.RegexValidationUSPhoneData.TestAction = RegexValidationUSPhone_TestAction;
			// 
			// RegexValidationUSZipData
			// 
			this.RegexValidationUSZipData.PosttestAction = null;
			this.RegexValidationUSZipData.PretestAction = null;
			this.RegexValidationUSZipData.TestAction = RegexValidationUSZip_TestAction;
			// 
			// RegexMatchTestsData
			// 
			this.RegexMatchTestsData.PosttestAction = null;
			this.RegexMatchTestsData.PretestAction = null;
			this.RegexMatchTestsData.TestAction = RegexMatchTests_TestAction;
			// 
			// RegexMatchesTestsData
			// 
			this.RegexMatchesTestsData.PosttestAction = null;
			this.RegexMatchesTestsData.PretestAction = null;
			this.RegexMatchesTestsData.TestAction = RegexMatchesTests_TestAction;
			// 
			// RegexGroupTestsData
			// 
			this.RegexGroupTestsData.PosttestAction = null;
			this.RegexGroupTestsData.PretestAction = null;
			this.RegexGroupTestsData.TestAction = RegexGroupTests_TestAction;
			// 
			// RegexGroupsTestData
			// 
			this.RegexGroupsTestData.PosttestAction = null;
			this.RegexGroupsTestData.PretestAction = null;
			this.RegexGroupsTestData.TestAction = RegexGroupsTest_TestAction;
			// 
			// RegexReplaceTestsData
			// 
			this.RegexReplaceTestsData.PosttestAction = null;
			this.RegexReplaceTestsData.PretestAction = null;
			this.RegexReplaceTestsData.TestAction = RegexReplaceTests_TestAction;
			// 
			// RegexReplaceTests_TestAction
			// 
			RegexReplaceTests_TestAction.Conditions.Add(RegexReplaceCaseInsensitive);
			RegexReplaceTests_TestAction.Conditions.Add(RegexReplaceCaseSensitive);
			resources.ApplyResources(RegexReplaceTests_TestAction, "RegexReplaceTests_TestAction");
			// 
			// RegexReplaceCaseInsensitive
			// 
			RegexReplaceCaseInsensitive.ColumnNumber = 1;
			RegexReplaceCaseInsensitive.Enabled = true;
			RegexReplaceCaseInsensitive.ExpectedValue = resources.GetString("RegexReplaceCaseInsensitive.ExpectedValue");
			RegexReplaceCaseInsensitive.Name = "RegexReplaceCaseInsensitive";
			RegexReplaceCaseInsensitive.NullExpected = false;
			RegexReplaceCaseInsensitive.ResultSet = 1;
			RegexReplaceCaseInsensitive.RowNumber = 1;
			// 
			// RegexReplaceCaseSensitive
			// 
			RegexReplaceCaseSensitive.ColumnNumber = 1;
			RegexReplaceCaseSensitive.Enabled = true;
			RegexReplaceCaseSensitive.ExpectedValue = resources.GetString("RegexReplaceCaseSensitive.ExpectedValue");
			RegexReplaceCaseSensitive.Name = "RegexReplaceCaseSensitive";
			RegexReplaceCaseSensitive.NullExpected = false;
			RegexReplaceCaseSensitive.ResultSet = 2;
			RegexReplaceCaseSensitive.RowNumber = 1;
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

		private SqlDatabaseTestActions RegexValidationEmailData;
		private SqlDatabaseTestActions RegexValidationSSNData;
		private SqlDatabaseTestActions RegexValidationUSPhoneData;
		private SqlDatabaseTestActions RegexValidationUSZipData;
		private SqlDatabaseTestActions RegexMatchTestsData;
		private SqlDatabaseTestActions RegexMatchesTestsData;
		private SqlDatabaseTestActions RegexGroupTestsData;
		private SqlDatabaseTestActions RegexGroupsTestData;
		private SqlDatabaseTestActions RegexReplaceTestsData;
	}
}
