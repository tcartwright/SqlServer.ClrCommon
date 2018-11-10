

Unfortunately the tests will not auto deploy the db project. 
So you must first publish the database project to (localdb)\MSSQLLocalDB
or wherever you have your tests project set to run against.

If you set the test project to automatically deploy the db you may get this error 
trying to run your tests:

	Message: Failed to deploy database project ****\SqlServer.ClrCommon.sqlproj.

Which means that any time you change code, you will need to republish 
befoore you can run your tests again.