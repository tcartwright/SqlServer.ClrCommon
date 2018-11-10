using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlServer.ClrCommon.Tests.Properties;

namespace SqlServer.ClrCommon.Tests
{
	[TestClass()]
	public class SqlDatabaseSetup
	{
		//http://blogs.interknowlogy.com/2014/08/29/unit-testing-using-localdb/

		[AssemblyInitialize()]
		public static void InitializeAssembly(TestContext ctx)
		{
            // Setup the test database based on setting in the
            // configuration file
            SqlDatabaseTestClass.TestService.DeployDatabaseProject();
			SqlDatabaseTestClass.TestService.GenerateData();

			//custom scripts to run against the db once it is deployed
			var pctx = SqlDatabaseTestClass.TestService.OpenPrivilegedContext();

			using (pctx.Connection)
			{
				if (pctx.Connection.State != ConnectionState.Open)
					pctx.Connection.Open();

				var sqlCmds = Regex.Split(Resources.InitScript, "GO --split");
				foreach (string sql in sqlCmds)
				{
					using (var cmd = pctx.Connection.CreateCommand())
					{
						cmd.CommandType = CommandType.Text;
						cmd.CommandText = sql;
						cmd.ExecuteNonQuery();
					}
				}
			}
		}
	}
}
