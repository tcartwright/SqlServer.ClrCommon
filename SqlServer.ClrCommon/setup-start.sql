IF OBJECT_ID (N'tempdb..#DropCLRAssembly') IS NOT NULL
   DROP PROCEDURE #DropCLRAssembly
GO

RAISERROR( 'CREATE PROC #DropCLRAssembly', 0, 1) WITH NOWAIT
GO

CREATE PROCEDURE #DropCLRAssembly (
	@assemblyName varchar(2048)
)
AS 
BEGIN 
	/*	Source http://www.andrewburrow.net/drop-sql-clr-assembly-dependencies/ 
		Some slight modifications by Tim Cartwright
	*/

	DECLARE @assemblyid INT

	SELECT @assemblyid = assembly_id 
		, @assemblyName = name
	FROM sys.assemblies WHERE name = @assemblyName

	/* 
		-- review schema information 
		select  [schema].Name,
			   [modules].object_id,
			   [modules].assembly_id,
			   [modules].assembly_class,
			   [modules].assembly_method,
			   [objects].name,
			   [objects].type,
			   [objects].type_desc
		from    sys.assembly_modules [modules]
		join    sys.objects [objects] with (nolock)
		on        [modules].object_id = [objects].object_id
		join    sys.schemas [schema] with (nolock)
		on        [objects].schema_id = [schema].schema_id
		where    [modules].assembly_id = @assemblyid
		ORDER BY [objects].type, [objects].name
	*/

	IF @assemblyid IS NULL BEGIN
		RAISERROR ('No assembly found matching that name!', 0, 1) WITH NOWAIT;
		RETURN;
	END
	ELSE
	BEGIN
		BEGIN TRANSACTION;
		DECLARE db_cursor CURSOR FOR
			SELECT  [schema].Name,
			so.name,
			so.type_desc
			FROM sys.assembly_modules [modules]
			JOIN sys.objects [so] WITH (NOLOCK) ON [modules].object_id = so.object_id
			JOIN sys.schemas [schema] WITH (NOLOCK) ON [so].schema_id = [schema].schema_id
			WHERE [modules].assembly_id = @assemblyid
			ORDER BY [so].type, [so].name

		BEGIN TRY
		   /* drop clr dependencies */
		   declare @sql NVARCHAR(MAX)
			  , @schema NVARCHAR(200)
			  , @name NVARCHAR(200)
			  , @type NVARCHAR(200)

			OPEN db_cursor
			FETCH NEXT FROM db_cursor into @schema, @name, @type

			WHILE @@FETCH_STATUS = 0
			BEGIN
			  SET @sql = 
				 CASE @type
					WHEN N'CLR_STORED_PROCEDURE' THEN N'drop procedure ' + @schema + N'.' + @name + N';'
					WHEN N'CLR_SCALAR_FUNCTION' THEN N'drop function ' + @schema + N'.' + @name + N';'
					WHEN N'CLR_TABLE_VALUED_FUNCTION' THEN N'drop function ' + @schema + N'.' + @name + N';'
					WHEN N'AGGREGATE_FUNCTION' THEN N'drop aggregate ' + @schema + N'.' + @name + N';'
					ELSE NULL
				 END

			  IF @sql IS NULL 
				 RAISERROR (N'CLR object type [%s] not recognised [%s].[%s]', 16, 1, @type, @schema, @name);

			  PRINT @sql;
			  EXEC sp_executesql @sql;
			
			  FETCH NEXT FROM db_cursor INTO @schema, @name, @type
			END

			/* drop assembly once free of dependencies */
			SET @sql = 'drop assembly [' + @assemblyName + '];'
			PRINT @sql
			EXEC sp_executesql @sql

			IF @@trancount > 0 COMMIT TRANSACTION;
		END TRY
		BEGIN CATCH
		   IF @@trancount > 0 ROLLBACK TRANSACTION;
		   /* server 2008 rethrow :( */
		   DECLARE
			  @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE(),
			  @ErrorNumber INT = ERROR_NUMBER(),
			  @ErrorSeverity INT = ERROR_SEVERITY(),
			  @ErrorState INT = ERROR_STATE(),
			  @ErrorLine INT = ERROR_LINE(),
			  @ErrorProcedure NVARCHAR(200) = ISNULL(ERROR_PROCEDURE(), '-');

		   SELECT @ErrorMessage = N'Error %d, Level %d, State %d, Procedure %s, Line %d, Message: ' + @ErrorMessage;
		   RAISERROR (@ErrorMessage, @ErrorSeverity, 1, @ErrorNumber, @ErrorSeverity, @ErrorState, @ErrorProcedure, @ErrorLine)
		END CATCH

		CLOSE db_cursor
		DEALLOCATE db_cursor
	END
END

GO

USE [master]
GO
IF DB_ID('$(DatabaseName)') IS NULL BEGIN
	-- create the database with all of the default options
	CREATE DATABASE [$(DatabaseName)] 
END ELSE BEGIN
	-- else drop the clr from the db
    USE [$(DatabaseName)]
    EXEC #DropCLRAssembly @assemblyName = 'SqlServer.ClrCommon'
END