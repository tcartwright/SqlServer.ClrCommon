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
	public class PathTest : SqlDatabaseTestClass
	{

		public PathTest()
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
		public void PathTests()
		{
			SqlDatabaseTestActions testActions = this.PathTestsData;
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
		public void PathNullChecks()
		{
			SqlDatabaseTestActions testActions = this.PathNullChecksData;
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
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction PathTests_TestAction;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PathTest));
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition PathGetDirectoryName;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition PathGetFileName;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition PathGetFileNameWithoutExtension;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition PathGetGetExtension;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition PathGetPathRoot;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition PathIsPathRooted;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition PathCombine;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition PathGetPathRootUNC;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction PathNullChecks_TestAction;
			Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ChecksumCondition PathNullChecksChecksum;
			this.PathTestsData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
			this.PathNullChecksData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
			PathTests_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
			PathGetDirectoryName = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			PathGetFileName = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			PathGetFileNameWithoutExtension = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			PathGetGetExtension = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			PathGetPathRoot = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			PathIsPathRooted = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			PathCombine = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			PathGetPathRootUNC = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
			PathNullChecks_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
			PathNullChecksChecksum = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ChecksumCondition();
			// 
			// PathTestsData
			// 
			this.PathTestsData.PosttestAction = null;
			this.PathTestsData.PretestAction = null;
			this.PathTestsData.TestAction = PathTests_TestAction;
			// 
			// PathTests_TestAction
			// 
			PathTests_TestAction.Conditions.Add(PathGetDirectoryName);
			PathTests_TestAction.Conditions.Add(PathGetFileName);
			PathTests_TestAction.Conditions.Add(PathGetFileNameWithoutExtension);
			PathTests_TestAction.Conditions.Add(PathGetGetExtension);
			PathTests_TestAction.Conditions.Add(PathGetPathRoot);
			PathTests_TestAction.Conditions.Add(PathIsPathRooted);
			PathTests_TestAction.Conditions.Add(PathCombine);
			PathTests_TestAction.Conditions.Add(PathGetPathRootUNC);
			resources.ApplyResources(PathTests_TestAction, "PathTests_TestAction");
			// 
			// PathGetDirectoryName
			// 
			PathGetDirectoryName.ColumnNumber = 1;
			PathGetDirectoryName.Enabled = true;
			PathGetDirectoryName.ExpectedValue = "C:\\Program Files (x86)\\Microsoft SDKs\\Microsoft Sync Framework\\v1.0\\Runtime\\ADO.N" +
				"ET\\V2.0\\Samples\\blah\\sub directory";
			PathGetDirectoryName.Name = "PathGetDirectoryName";
			PathGetDirectoryName.NullExpected = false;
			PathGetDirectoryName.ResultSet = 1;
			PathGetDirectoryName.RowNumber = 1;
			// 
			// PathGetFileName
			// 
			PathGetFileName.ColumnNumber = 2;
			PathGetFileName.Enabled = true;
			PathGetFileName.ExpectedValue = "demo.sql";
			PathGetFileName.Name = "PathGetFileName";
			PathGetFileName.NullExpected = false;
			PathGetFileName.ResultSet = 1;
			PathGetFileName.RowNumber = 1;
			// 
			// PathGetFileNameWithoutExtension
			// 
			PathGetFileNameWithoutExtension.ColumnNumber = 3;
			PathGetFileNameWithoutExtension.Enabled = true;
			PathGetFileNameWithoutExtension.ExpectedValue = "demo";
			PathGetFileNameWithoutExtension.Name = "PathGetFileNameWithoutExtension";
			PathGetFileNameWithoutExtension.NullExpected = false;
			PathGetFileNameWithoutExtension.ResultSet = 1;
			PathGetFileNameWithoutExtension.RowNumber = 1;
			// 
			// PathGetGetExtension
			// 
			PathGetGetExtension.ColumnNumber = 4;
			PathGetGetExtension.Enabled = true;
			PathGetGetExtension.ExpectedValue = ".sql";
			PathGetGetExtension.Name = "PathGetGetExtension";
			PathGetGetExtension.NullExpected = false;
			PathGetGetExtension.ResultSet = 1;
			PathGetGetExtension.RowNumber = 1;
			// 
			// PathGetPathRoot
			// 
			PathGetPathRoot.ColumnNumber = 5;
			PathGetPathRoot.Enabled = true;
			PathGetPathRoot.ExpectedValue = "C:\\";
			PathGetPathRoot.Name = "PathGetPathRoot";
			PathGetPathRoot.NullExpected = false;
			PathGetPathRoot.ResultSet = 1;
			PathGetPathRoot.RowNumber = 1;
			// 
			// PathIsPathRooted
			// 
			PathIsPathRooted.ColumnNumber = 6;
			PathIsPathRooted.Enabled = true;
			PathIsPathRooted.ExpectedValue = "true";
			PathIsPathRooted.Name = "PathIsPathRooted";
			PathIsPathRooted.NullExpected = false;
			PathIsPathRooted.ResultSet = 1;
			PathIsPathRooted.RowNumber = 1;
			// 
			// PathCombine
			// 
			PathCombine.ColumnNumber = 7;
			PathCombine.Enabled = true;
			PathCombine.ExpectedValue = "C:\\Program Files (x86)\\Microsoft SDKs\\Microsoft Sync Framework\\v1.0\\Runtime\\ADO.N" +
				"ET\\V2.0\\Samples\\blah\\sub directory\\foo.sql";
			PathCombine.Name = "PathCombine";
			PathCombine.NullExpected = false;
			PathCombine.ResultSet = 1;
			PathCombine.RowNumber = 1;
			// 
			// PathGetPathRootUNC
			// 
			PathGetPathRootUNC.ColumnNumber = 8;
			PathGetPathRootUNC.Enabled = true;
			PathGetPathRootUNC.ExpectedValue = "\\\\server\\share";
			PathGetPathRootUNC.Name = "PathGetPathRootUNC";
			PathGetPathRootUNC.NullExpected = false;
			PathGetPathRootUNC.ResultSet = 1;
			PathGetPathRootUNC.RowNumber = 1;
			// 
			// PathNullChecksData
			// 
			this.PathNullChecksData.PosttestAction = null;
			this.PathNullChecksData.PretestAction = null;
			this.PathNullChecksData.TestAction = PathNullChecks_TestAction;
			// 
			// PathNullChecks_TestAction
			// 
			PathNullChecks_TestAction.Conditions.Add(PathNullChecksChecksum);
			resources.ApplyResources(PathNullChecks_TestAction, "PathNullChecks_TestAction");
			// 
			// PathNullChecksChecksum
			// 
			PathNullChecksChecksum.Checksum = "-487856238";
			PathNullChecksChecksum.Enabled = true;
			PathNullChecksChecksum.Name = "PathNullChecksChecksum";
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

		private SqlDatabaseTestActions PathTestsData;
		private SqlDatabaseTestActions PathNullChecksData;
	}
}
