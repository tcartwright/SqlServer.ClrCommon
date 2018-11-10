/*	Source http://www.andrewburrow.net/drop-sql-clr-assembly-dependencies/ 
	Some slight modifications by Tim Cartwright
*/

declare @assemblyid int,
	@assemblyName varchar(2048) 

select @assemblyid = assembly_id 
	, @assemblyName = name
from sys.assemblies where name = 'SqlServer.ClrCommon'

/* review schema information */
--select  [schema].Name,
--	   [modules].object_id,
--	   [modules].assembly_id,
--	   [modules].assembly_class,
--	   [modules].assembly_method,
--	   [objects].name,
--	   [objects].type,
--	   [objects].type_desc
--from    sys.assembly_modules [modules]
--join    sys.objects [objects] with (nolock)
--on        [modules].object_id = [objects].object_id
--join    sys.schemas [schema] with (nolock)
--on        [objects].schema_id = [schema].schema_id
--where    [modules].assembly_id = @assemblyid
--ORDER BY [objects].type, [objects].name

if @assemblyid is null begin
	print 'No assembly found matching that name!'
	return;
end
ELSE
BEGIN
	begin try
	   /* drop clr dependencies */
	   declare @sql nvarchar(max)
		  , @schema nvarchar(200)
		  , @name nvarchar(200)
		  , @type nvarchar(200)

	   declare db_cursor cursor for
		  select  [schema].Name,
		  so.name,
		  so.type_desc
		  from sys.assembly_modules [modules]
		  join sys.objects [so] with (nolock) ON [modules].object_id = so.object_id
		  join sys.schemas [schema] with (nolock) ON [so].schema_id = [schema].schema_id
		  where [modules].assembly_id = @assemblyid
		  ORDER BY [so].type, [so].name

		open db_cursor
		fetch next from db_cursor into @schema, @name, @type

		begin transaction;

		while @@FETCH_STATUS = 0
		begin
		  set @sql = 
			 case @type
				when N'CLR_STORED_PROCEDURE' then N'drop procedure ' + @schema + N'.' + @name + N';'
				when N'CLR_SCALAR_FUNCTION' then N'drop function ' + @schema + N'.' + @name + N';'
				when N'CLR_TABLE_VALUED_FUNCTION' then N'drop function ' + @schema + N'.' + @name + N';'
				when N'AGGREGATE_FUNCTION' then N'drop aggregate ' + @schema + N'.' + @name + N';'
				ELSE NULL
			 end

		  if @sql IS NULL 
			 RAISERROR (N'CLR object type [%s] not recognised [%s].[%s]', 16, 1, @type, @schema, @name);

		  print @sql;
		  exec sp_executesql @sql;
			
		  fetch next from db_cursor into @schema, @name, @type
		end

		close db_cursor
		deallocate db_cursor

		/* drop assembly once free of dependencies */
		set @sql = 'drop assembly [' + @assemblyName + '];'
		print @sql
		exec sp_executesql @sql

		if @@trancount > 0 commit transaction;

	end try
	begin catch
	   if @@trancount > 0 rollback transaction;
	   /* server 2008 rethrow :( */
	   DECLARE
		  @ErrorMessage nvarchar(4000) = ERROR_MESSAGE(),
		  @ErrorNumber int = ERROR_NUMBER(),
		  @ErrorSeverity int = ERROR_SEVERITY(),
		  @ErrorState int = ERROR_STATE(),
		  @ErrorLine int = ERROR_LINE(),
		  @ErrorProcedure nvarchar(200) = ISNULL(ERROR_PROCEDURE(), '-');

	   SELECT @ErrorMessage = N'Error %d, Level %d, State %d, Procedure %s, Line %d, Message: ' + @ErrorMessage;
	   RAISERROR (@ErrorMessage, @ErrorSeverity, 1, @ErrorNumber, @ErrorSeverity, @ErrorState, @ErrorProcedure, @ErrorLine)
	end catch
END
