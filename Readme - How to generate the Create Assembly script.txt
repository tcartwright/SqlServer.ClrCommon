
1) First pick the appropriate build. If building for sql server 2008 choose "Release", for sql server 2012 and on choose "Release4.5".

2) Double click the "SqlServer.ClrCommon.Publish.xml" file in the solution to start the publish wizard. None of the settings should require 
change in here, but you can if so desired.

3) Click the "Generate Script" button. If everything builds successfully a script will open in the visual studio window.

4) Find the lines: 
	GO
	PRINT N'Creating [SqlServer.ClrCommon]...';""

5) Select everything from here to the end of the file. 

6) Copy the selection to your new file. This will be your create script.

7) The drop script will always be "Drop CLR Assembly.sql".