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
	public class MiscTest : SqlDatabaseTestClass
	{

		public MiscTest()
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
		public void RandomTests()
		{
			SqlDatabaseTestActions testActions = this.RandomTestsData;
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
		public void GeneratePasswordTests()
		{
			SqlDatabaseTestActions testActions = this.GeneratePasswordTestsData;
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
		public void IsPrimeAndCubeRootTests()
		{
			SqlDatabaseTestActions testActions = this.IsPrimeAndCubeRootTestsData;
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
		public void ConversionTests()
		{
			SqlDatabaseTestActions testActions = this.ConversionTestsData;
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
		public void IsNumericTests()
		{
			SqlDatabaseTestActions testActions = this.IsNumericTestsData;
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
		public void LuhnTests()
		{
			SqlDatabaseTestActions testActions = this.LuhnTestsData;
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
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction RandomTests_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MiscTest));
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition Random1;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition Random2;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition Random3;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction GeneratePasswordTests_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition GeneratePasswordHasData;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction IsPrimeAndCubeRootTests_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ChecksumCondition IsPrimeAndCubeRootChecksum;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction ConversionTests_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition DecimalToBinary;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition DecimalToHex;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition HexToDecimal;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition HexToBinary;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition BinaryToDecimal;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition BinaryToHex;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition DecimalToBinaryNullValue;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition DecimalToHexNullValue;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition HexToDecimalNullValue;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition HexToBinaryNullValue;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition BinaryToDecimalNullValue;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition BinaryToHexNullValue;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction IsNumericTests_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ChecksumCondition IsNumericChecksum;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction LuhnTests_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ChecksumCondition LuhnValidationCheckSum;
            this.RandomTestsData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.GeneratePasswordTestsData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.IsPrimeAndCubeRootTestsData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.ConversionTestsData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.IsNumericTestsData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.LuhnTestsData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            RandomTests_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            Random1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            Random2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            Random3 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            GeneratePasswordTests_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            GeneratePasswordHasData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition();
            IsPrimeAndCubeRootTests_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            IsPrimeAndCubeRootChecksum = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ChecksumCondition();
            ConversionTests_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            DecimalToBinary = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            DecimalToHex = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            HexToDecimal = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            HexToBinary = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            BinaryToDecimal = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            BinaryToHex = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            DecimalToBinaryNullValue = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            DecimalToHexNullValue = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            HexToDecimalNullValue = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            HexToBinaryNullValue = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            BinaryToDecimalNullValue = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            BinaryToHexNullValue = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            IsNumericTests_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            IsNumericChecksum = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ChecksumCondition();
            LuhnTests_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            LuhnValidationCheckSum = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ChecksumCondition();
            // 
            // RandomTests_TestAction
            // 
            RandomTests_TestAction.Conditions.Add(Random1);
            RandomTests_TestAction.Conditions.Add(Random2);
            RandomTests_TestAction.Conditions.Add(Random3);
            resources.ApplyResources(RandomTests_TestAction, "RandomTests_TestAction");
            // 
            // Random1
            // 
            Random1.ColumnNumber = 1;
            Random1.Enabled = true;
            Random1.ExpectedValue = "Success";
            Random1.Name = "Random1";
            Random1.NullExpected = false;
            Random1.ResultSet = 1;
            Random1.RowNumber = 1;
            // 
            // Random2
            // 
            Random2.ColumnNumber = 2;
            Random2.Enabled = true;
            Random2.ExpectedValue = "Success";
            Random2.Name = "Random2";
            Random2.NullExpected = false;
            Random2.ResultSet = 1;
            Random2.RowNumber = 1;
            // 
            // Random3
            // 
            Random3.ColumnNumber = 1;
            Random3.Enabled = true;
            Random3.ExpectedValue = "668";
            Random3.Name = "Random3";
            Random3.NullExpected = false;
            Random3.ResultSet = 2;
            Random3.RowNumber = 1;
            // 
            // GeneratePasswordTests_TestAction
            // 
            GeneratePasswordTests_TestAction.Conditions.Add(GeneratePasswordHasData);
            resources.ApplyResources(GeneratePasswordTests_TestAction, "GeneratePasswordTests_TestAction");
            // 
            // GeneratePasswordHasData
            // 
            GeneratePasswordHasData.Enabled = true;
            GeneratePasswordHasData.Name = "GeneratePasswordHasData";
            GeneratePasswordHasData.ResultSet = 1;
            // 
            // IsPrimeAndCubeRootTests_TestAction
            // 
            IsPrimeAndCubeRootTests_TestAction.Conditions.Add(IsPrimeAndCubeRootChecksum);
            resources.ApplyResources(IsPrimeAndCubeRootTests_TestAction, "IsPrimeAndCubeRootTests_TestAction");
            // 
            // IsPrimeAndCubeRootChecksum
            // 
            IsPrimeAndCubeRootChecksum.Checksum = "603366486";
            IsPrimeAndCubeRootChecksum.Enabled = true;
            IsPrimeAndCubeRootChecksum.Name = "IsPrimeAndCubeRootChecksum";
            // 
            // ConversionTests_TestAction
            // 
            ConversionTests_TestAction.Conditions.Add(DecimalToBinary);
            ConversionTests_TestAction.Conditions.Add(DecimalToHex);
            ConversionTests_TestAction.Conditions.Add(HexToDecimal);
            ConversionTests_TestAction.Conditions.Add(HexToBinary);
            ConversionTests_TestAction.Conditions.Add(BinaryToDecimal);
            ConversionTests_TestAction.Conditions.Add(BinaryToHex);
            ConversionTests_TestAction.Conditions.Add(DecimalToBinaryNullValue);
            ConversionTests_TestAction.Conditions.Add(DecimalToHexNullValue);
            ConversionTests_TestAction.Conditions.Add(HexToDecimalNullValue);
            ConversionTests_TestAction.Conditions.Add(HexToBinaryNullValue);
            ConversionTests_TestAction.Conditions.Add(BinaryToDecimalNullValue);
            ConversionTests_TestAction.Conditions.Add(BinaryToHexNullValue);
            resources.ApplyResources(ConversionTests_TestAction, "ConversionTests_TestAction");
            // 
            // DecimalToBinary
            // 
            DecimalToBinary.ColumnNumber = 1;
            DecimalToBinary.Enabled = true;
            DecimalToBinary.ExpectedValue = "100001000101111111101101";
            DecimalToBinary.Name = "DecimalToBinary";
            DecimalToBinary.NullExpected = false;
            DecimalToBinary.ResultSet = 1;
            DecimalToBinary.RowNumber = 1;
            // 
            // DecimalToHex
            // 
            DecimalToHex.ColumnNumber = 2;
            DecimalToHex.Enabled = true;
            DecimalToHex.ExpectedValue = "845FED";
            DecimalToHex.Name = "DecimalToHex";
            DecimalToHex.NullExpected = false;
            DecimalToHex.ResultSet = 1;
            DecimalToHex.RowNumber = 1;
            // 
            // HexToDecimal
            // 
            HexToDecimal.ColumnNumber = 3;
            HexToDecimal.Enabled = true;
            HexToDecimal.ExpectedValue = "8675309";
            HexToDecimal.Name = "HexToDecimal";
            HexToDecimal.NullExpected = false;
            HexToDecimal.ResultSet = 1;
            HexToDecimal.RowNumber = 1;
            // 
            // HexToBinary
            // 
            HexToBinary.ColumnNumber = 4;
            HexToBinary.Enabled = true;
            HexToBinary.ExpectedValue = "100001000101111111101101";
            HexToBinary.Name = "HexToBinary";
            HexToBinary.NullExpected = false;
            HexToBinary.ResultSet = 1;
            HexToBinary.RowNumber = 1;
            // 
            // BinaryToDecimal
            // 
            BinaryToDecimal.ColumnNumber = 5;
            BinaryToDecimal.Enabled = true;
            BinaryToDecimal.ExpectedValue = "8675309";
            BinaryToDecimal.Name = "BinaryToDecimal";
            BinaryToDecimal.NullExpected = false;
            BinaryToDecimal.ResultSet = 1;
            BinaryToDecimal.RowNumber = 1;
            // 
            // BinaryToHex
            // 
            BinaryToHex.ColumnNumber = 6;
            BinaryToHex.Enabled = true;
            BinaryToHex.ExpectedValue = "845FED";
            BinaryToHex.Name = "BinaryToHex";
            BinaryToHex.NullExpected = false;
            BinaryToHex.ResultSet = 1;
            BinaryToHex.RowNumber = 1;
            // 
            // DecimalToBinaryNullValue
            // 
            DecimalToBinaryNullValue.ColumnNumber = 1;
            DecimalToBinaryNullValue.Enabled = true;
            DecimalToBinaryNullValue.ExpectedValue = null;
            DecimalToBinaryNullValue.Name = "DecimalToBinaryNullValue";
            DecimalToBinaryNullValue.NullExpected = true;
            DecimalToBinaryNullValue.ResultSet = 2;
            DecimalToBinaryNullValue.RowNumber = 1;
            // 
            // DecimalToHexNullValue
            // 
            DecimalToHexNullValue.ColumnNumber = 2;
            DecimalToHexNullValue.Enabled = true;
            DecimalToHexNullValue.ExpectedValue = null;
            DecimalToHexNullValue.Name = "DecimalToHexNullValue";
            DecimalToHexNullValue.NullExpected = true;
            DecimalToHexNullValue.ResultSet = 2;
            DecimalToHexNullValue.RowNumber = 1;
            // 
            // HexToDecimalNullValue
            // 
            HexToDecimalNullValue.ColumnNumber = 3;
            HexToDecimalNullValue.Enabled = true;
            HexToDecimalNullValue.ExpectedValue = null;
            HexToDecimalNullValue.Name = "HexToDecimalNullValue";
            HexToDecimalNullValue.NullExpected = true;
            HexToDecimalNullValue.ResultSet = 2;
            HexToDecimalNullValue.RowNumber = 1;
            // 
            // HexToBinaryNullValue
            // 
            HexToBinaryNullValue.ColumnNumber = 4;
            HexToBinaryNullValue.Enabled = true;
            HexToBinaryNullValue.ExpectedValue = null;
            HexToBinaryNullValue.Name = "HexToBinaryNullValue";
            HexToBinaryNullValue.NullExpected = true;
            HexToBinaryNullValue.ResultSet = 2;
            HexToBinaryNullValue.RowNumber = 1;
            // 
            // BinaryToDecimalNullValue
            // 
            BinaryToDecimalNullValue.ColumnNumber = 5;
            BinaryToDecimalNullValue.Enabled = true;
            BinaryToDecimalNullValue.ExpectedValue = null;
            BinaryToDecimalNullValue.Name = "BinaryToDecimalNullValue";
            BinaryToDecimalNullValue.NullExpected = true;
            BinaryToDecimalNullValue.ResultSet = 2;
            BinaryToDecimalNullValue.RowNumber = 1;
            // 
            // BinaryToHexNullValue
            // 
            BinaryToHexNullValue.ColumnNumber = 6;
            BinaryToHexNullValue.Enabled = true;
            BinaryToHexNullValue.ExpectedValue = null;
            BinaryToHexNullValue.Name = "BinaryToHexNullValue";
            BinaryToHexNullValue.NullExpected = true;
            BinaryToHexNullValue.ResultSet = 2;
            BinaryToHexNullValue.RowNumber = 1;
            // 
            // IsNumericTests_TestAction
            // 
            IsNumericTests_TestAction.Conditions.Add(IsNumericChecksum);
            resources.ApplyResources(IsNumericTests_TestAction, "IsNumericTests_TestAction");
            // 
            // IsNumericChecksum
            // 
            IsNumericChecksum.Checksum = "603943827";
            IsNumericChecksum.Enabled = true;
            IsNumericChecksum.Name = "IsNumericChecksum";
            // 
            // LuhnTests_TestAction
            // 
            LuhnTests_TestAction.Conditions.Add(LuhnValidationCheckSum);
            resources.ApplyResources(LuhnTests_TestAction, "LuhnTests_TestAction");
            // 
            // LuhnValidationCheckSum
            // 
            LuhnValidationCheckSum.Checksum = "249993599";
            LuhnValidationCheckSum.Enabled = true;
            LuhnValidationCheckSum.Name = "LuhnValidationCheckSum";
            // 
            // RandomTestsData
            // 
            this.RandomTestsData.PosttestAction = null;
            this.RandomTestsData.PretestAction = null;
            this.RandomTestsData.TestAction = RandomTests_TestAction;
            // 
            // GeneratePasswordTestsData
            // 
            this.GeneratePasswordTestsData.PosttestAction = null;
            this.GeneratePasswordTestsData.PretestAction = null;
            this.GeneratePasswordTestsData.TestAction = GeneratePasswordTests_TestAction;
            // 
            // IsPrimeAndCubeRootTestsData
            // 
            this.IsPrimeAndCubeRootTestsData.PosttestAction = null;
            this.IsPrimeAndCubeRootTestsData.PretestAction = null;
            this.IsPrimeAndCubeRootTestsData.TestAction = IsPrimeAndCubeRootTests_TestAction;
            // 
            // ConversionTestsData
            // 
            this.ConversionTestsData.PosttestAction = null;
            this.ConversionTestsData.PretestAction = null;
            this.ConversionTestsData.TestAction = ConversionTests_TestAction;
            // 
            // IsNumericTestsData
            // 
            this.IsNumericTestsData.PosttestAction = null;
            this.IsNumericTestsData.PretestAction = null;
            this.IsNumericTestsData.TestAction = IsNumericTests_TestAction;
            // 
            // LuhnTestsData
            // 
            this.LuhnTestsData.PosttestAction = null;
            this.LuhnTestsData.PretestAction = null;
            this.LuhnTestsData.TestAction = LuhnTests_TestAction;
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

        private SqlDatabaseTestActions RandomTestsData;
		private SqlDatabaseTestActions GeneratePasswordTestsData;
		private SqlDatabaseTestActions IsPrimeAndCubeRootTestsData;
		private SqlDatabaseTestActions ConversionTestsData;
		private SqlDatabaseTestActions IsNumericTestsData;
		private SqlDatabaseTestActions LuhnTestsData;
	}
}
