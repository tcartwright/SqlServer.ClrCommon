using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{

    /// <summary>
    /// Replaces one or more format items in a specified string with the string representation of a specified object.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="arg0">The arg0.</param>
    /// <example>
    /// <code>
    /// SELECT TOP 10 [FormatString] = clr.xfnFormatString1(N'[{0}]', c.COLUMN_NAME) 
    /// FROM msdb.INFORMATION_SCHEMA.COLUMNS c 
    /// WHERE c.TABLE_NAME = 'backupset'	
    /// </code>
    /// <br />
    /// <span style='font-weight: bold;font-size;14;'>Results:</span>
    /// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
    /// 	<thead><tr>
    /// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">FormatString</th>
    /// 	</tr></thead>
    /// 	<tbody>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[backup_set_id]</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[backup_set_uuid]</td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[media_set_id]</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[first_family_number]</td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[first_media_number]</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[last_family_number]</td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[last_media_number]</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[catalog_family_number]</td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[catalog_media_number]</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[position]</td>
    /// 		</tr>
    /// 	</tbody>
    /// </table>
    /// </example>
    /// <returns>A copy of the format in which any format items are replaced by the string representation of the args.</returns>
    [SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
    public static SqlString xfnFormatString1(SqlString format, object arg0)
    {
        return format.IsNull ? SqlString.Null : string.Format(format.Value, GetSqlValue(arg0));
    }

    /// <summary>
    /// Replaces one or more format items in a specified string with the string representation of a specified object.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="arg0">The arg0.</param>
    /// <param name="arg1">The arg1.</param>
    /// <example>
    /// <code>
    /// SELECT TOP 10 [FormatString] = clr.xfnFormatString2(N'[{0}].[{1}]', c.TABLE_NAME, c.COLUMN_NAME) 
    /// FROM msdb.INFORMATION_SCHEMA.COLUMNS c 
    /// WHERE c.TABLE_NAME = 'backupset'
    /// </code>
    /// <br />
    /// <span style='font-weight: bold;font-size;14;'>Results:</span>
    /// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
    /// 	<thead><tr>
    /// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">FormatString</th>
    /// 	</tr></thead>
    /// 	<tbody>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[backupset].[backup_set_id]</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[backupset].[backup_set_uuid]</td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[backupset].[media_set_id]</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[backupset].[first_family_number]</td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[backupset].[first_media_number]</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[backupset].[last_family_number]</td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[backupset].[last_media_number]</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[backupset].[catalog_family_number]</td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[backupset].[catalog_media_number]</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[backupset].[position]</td>
    /// 		</tr>
    /// 	</tbody>
    /// </table>
    /// </example>
    /// <returns>A copy of the format in which any format items are replaced by the string representation of the args.</returns>
    [SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
    public static SqlString xfnFormatString2(SqlString format, object arg0, object arg1)
    {
        return format.IsNull ? SqlString.Null : string.Format(format.Value, GetSqlValues(arg0, arg1));
    }

    /// <summary>
    /// Replaces one or more format items in a specified string with the string representation of a specified object.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="arg0">The arg0.</param>
    /// <param name="arg1">The arg1.</param>
    /// <param name="arg2">The arg2.</param>
    /// <example>
    /// <code>
    /// SELECT TOP 10 [FormatString] = clr.xfnFormatString3(N'[{0}].[{1}].[{2}]', c.TABLE_SCHEMA, c.TABLE_NAME, c.COLUMN_NAME) 
    /// FROM msdb.INFORMATION_SCHEMA.COLUMNS c 
    /// WHERE c.TABLE_NAME = 'backupset'
    /// </code>
    /// <br />
    /// <span style='font-weight: bold;font-size;14;'>Results:</span>
    /// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
    /// 	<thead><tr>
    /// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">FormatString</th>
    /// 	</tr></thead>
    /// 	<tbody>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[dbo].[backupset].[backup_set_id]</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[dbo].[backupset].[backup_set_uuid]</td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[dbo].[backupset].[media_set_id]</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[dbo].[backupset].[first_family_number]</td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[dbo].[backupset].[first_media_number]</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[dbo].[backupset].[last_family_number]</td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[dbo].[backupset].[last_media_number]</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[dbo].[backupset].[catalog_family_number]</td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[dbo].[backupset].[catalog_media_number]</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[dbo].[backupset].[position]</td>
    /// 		</tr>
    /// 	</tbody>
    /// </table>
    /// </example>
    /// <returns>A copy of the format in which any format items are replaced by the string representation of the args.</returns>
    [SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
    public static SqlString xfnFormatString3(SqlString format, object arg0, object arg1, object arg2)
    {
        return format.IsNull ? SqlString.Null : string.Format(format.Value, GetSqlValues(arg0, arg1, arg2));
    }

    /// <summary>
    /// Replaces one or more format items in a specified string with the string representation of a specified object.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="arg0">The arg0.</param>
    /// <param name="arg1">The arg1.</param>
    /// <param name="arg2">The arg2.</param>
    /// <param name="arg3">The arg3.</param>
    /// <example>
    /// <code>
    /// SELECT TOP 10 [FormatString] = clr.xfnFormatString4(N'[{0}].[{1}].[{2}].[{3}]', 'msdb', c.TABLE_SCHEMA, c.TABLE_NAME, c.COLUMN_NAME) 
    /// FROM msdb.INFORMATION_SCHEMA.COLUMNS c 
    /// WHERE c.TABLE_NAME = 'backupset'
    /// </code>
    /// <br />
    /// <span style='font-weight: bold;font-size;14;'>Results:</span>
    /// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
    /// 	<thead><tr>
    /// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">FormatString</th>
    /// 	</tr></thead>
    /// 	<tbody>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[backup_set_id]</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[backup_set_uuid]</td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[media_set_id]</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[first_family_number]</td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[first_media_number]</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[last_family_number]</td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[last_media_number]</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[catalog_family_number]</td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[catalog_media_number]</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[position]</td>
    /// 		</tr>
    /// 	</tbody>
    /// </table>
    /// </example>
    /// <returns>A copy of the format in which any format items are replaced by the string representation of the args.</returns>
    [SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
    public static SqlString xfnFormatString4(SqlString format, object arg0, object arg1, object arg2, object arg3)
    {
        return format.IsNull ? SqlString.Null : string.Format(format.Value, GetSqlValues(arg0, arg1, arg2, arg3));
    }

    /// <summary>
    /// Replaces one or more format items in a specified string with the string representation of a specified object.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="arg0">The arg0.</param>
    /// <param name="arg1">The arg1.</param>
    /// <param name="arg2">The arg2.</param>
    /// <param name="arg3">The arg3.</param>
    /// <param name="arg4">The arg4.</param>
    /// <example>
    /// <code>
    /// SELECT TOP 10 [FormatString] = clr.xfnFormatString5(N'[{0}].[{1}].[{2}].[{3}] {4}', 'msdb', c.TABLE_SCHEMA, c.TABLE_NAME, c.COLUMN_NAME, c.DATA_TYPE) 
    /// FROM msdb.INFORMATION_SCHEMA.COLUMNS c 
    /// WHERE c.TABLE_NAME = 'backupset'
    /// </code>
    /// <br />
    /// <span style='font-weight: bold;font-size;14;'>Results:</span>
    /// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
    /// 	<thead><tr>
    /// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">FormatString</th>
    /// 	</tr></thead>
    /// 	<tbody>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[backup_set_id] int</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[backup_set_uuid] uniqueidentifier</td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[media_set_id] int</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[first_family_number] tinyint</td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[first_media_number] smallint</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[last_family_number] tinyint</td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[last_media_number] smallint</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[catalog_family_number] tinyint</td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[catalog_media_number] smallint</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[position] int</td>
    /// 		</tr>
    /// 	</tbody>
    /// </table>
    /// </example>
    /// <returns>A copy of the format in which any format items are replaced by the string representation of the args.</returns>
    [SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
    public static SqlString xfnFormatString5(SqlString format, object arg0, object arg1, object arg2, object arg3, object arg4)
    {
        return format.IsNull ? SqlString.Null : string.Format(format.Value, GetSqlValues(arg0, arg1, arg2, arg3, arg4));
    }

    /// <summary>
    /// Replaces one or more format items in a specified string with the string representation of a specified object.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="arg0">The arg0.</param>
    /// <param name="arg1">The arg1.</param>
    /// <param name="arg2">The arg2.</param>
    /// <param name="arg3">The arg3.</param>
    /// <param name="arg4">The arg4.</param>
    /// <param name="arg5">The arg5.</param>
    /// <example>
    /// <code>
    /// SELECT TOP 10 [FormatString] = clr.xfnFormatString6(N'[{0}].[{1}].[{2}].[{3}] {4}{5}', 'msdb', c.TABLE_SCHEMA, c.TABLE_NAME, c.COLUMN_NAME, c.DATA_TYPE, fn.TYPE_LEN) 
    /// FROM msdb.INFORMATION_SCHEMA.COLUMNS c 
    /// CROSS APPLY (SELECT 
    ///     TYPE_LEN = CASE 
    /// 		WHEN c.CHARACTER_MAXIMUM_LENGTH IS NULL THEN ''
    /// 		WHEN c.CHARACTER_MAXIMUM_LENGTH = -1 THEN '(MAX)'
    /// 		ELSE '(' + CAST(c.CHARACTER_MAXIMUM_LENGTH AS varchar(30)) + ')'
    /// 	   END
    /// ) fn
    /// WHERE c.TABLE_NAME = 'backupset'
    /// </code>
    /// <br />
    /// <span style='font-weight: bold;font-size;14;'>Results:</span>
    /// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
    /// 	<thead><tr>
    /// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">FormatString</th>
    /// 	</tr></thead>
    /// 	<tbody>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[backup_set_id] int</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[backup_set_uuid] uniqueidentifier</td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[media_set_id] int</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[first_family_number] tinyint</td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[first_media_number] smallint</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[last_family_number] tinyint</td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[last_media_number] smallint</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[catalog_family_number] tinyint</td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[catalog_media_number] smallint</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[position] int</td>
    /// 		</tr>
    /// 	</tbody>
    /// </table>
    /// </example>
    /// <returns>A copy of the format in which any format items are replaced by the string representation of the args.</returns>
    [SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
    public static SqlString xfnFormatString6(SqlString format, object arg0, object arg1, object arg2, object arg3, object arg4, object arg5)
    {
        return format.IsNull ? SqlString.Null : string.Format(format.Value, GetSqlValues(arg0, arg1, arg2, arg3, arg4, arg5));
    }
    /// <summary>
    /// Replaces one or more format items in a specified string with the string representation of a specified object.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="arg0">The arg0.</param>
    /// <param name="arg1">The arg1.</param>
    /// <param name="arg2">The arg2.</param>
    /// <param name="arg3">The arg3.</param>
    /// <param name="arg4">The arg4.</param>
    /// <param name="arg5">The arg5.</param>
    /// <param name="arg6">The arg6.</param>
    /// <example>
    /// <code>
    /// SELECT TOP 10 [FormatString] = clr.xfnFormatString7(N'[{0}].[{1}].[{2}].[{3}] {4}{5}{6}', 'msdb', c.TABLE_SCHEMA, c.TABLE_NAME, c.COLUMN_NAME, c.DATA_TYPE, fn.TYPE_LEN, fn.NULLABLE) 
    /// FROM msdb.INFORMATION_SCHEMA.COLUMNS c 
    /// CROSS APPLY (SELECT 
    ///     TYPE_LEN = CASE 
    /// 		WHEN c.CHARACTER_MAXIMUM_LENGTH IS NULL THEN ''
    /// 		WHEN c.CHARACTER_MAXIMUM_LENGTH = -1 THEN '(MAX)'
    /// 		ELSE '(' + CAST(c.CHARACTER_MAXIMUM_LENGTH AS varchar(30)) + ')'
    /// 	   END,
    ///     NULLABLE = CASE
    /// 		  WHEN c.IS_NULLABLE = 'NO' THEN ' NOT NULL'
    /// 		  ELSE ' NULL '
    /// 	   END
    /// ) fn
    /// WHERE c.TABLE_NAME = 'backupset'
    /// </code>
    /// <br />
    /// <span style='font-weight: bold;font-size;14;'>Results:</span>
    /// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
    /// 	<thead><tr>
    /// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">FormatString</th>
    /// 	</tr></thead>
    /// 	<tbody>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[backup_set_id] int NOT NULL</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[backup_set_uuid] uniqueidentifier NOT NULL</td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[media_set_id] int NOT NULL</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[first_family_number] tinyint NULL </td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[first_media_number] smallint NULL </td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[last_family_number] tinyint NULL </td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[last_media_number] smallint NULL </td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[catalog_family_number] tinyint NULL </td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[catalog_media_number] smallint NULL </td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[position] int NULL </td>
    /// 		</tr>
    /// 	</tbody>
    /// </table>
    /// </example>
    /// <returns>A copy of the format in which any format items are replaced by the string representation of the args.</returns>
    [SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
    public static SqlString xfnFormatString7(SqlString format, object arg0, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6)
    {
        return format.IsNull ? SqlString.Null : string.Format(format.Value, GetSqlValues(arg0, arg1, arg2, arg3, arg4, arg5, arg6));
    }
    /// <summary>
    /// Replaces one or more format items in a specified string with the string representation of a specified object.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="arg0">The arg0.</param>
    /// <param name="arg1">The arg1.</param>
    /// <param name="arg2">The arg2.</param>
    /// <param name="arg3">The arg3.</param>
    /// <param name="arg4">The arg4.</param>
    /// <param name="arg5">The arg5.</param>
    /// <param name="arg6">The arg6.</param>
    /// <param name="arg7">The arg7.</param>
    /// <example>
    /// <code>
    /// SELECT TOP 10 [FormatString] = clr.xfnFormatString8(N'[{0}].[{1}].[{2}].[{3}] {4}{5}{6}{7}', 'msdb', c.TABLE_SCHEMA, c.TABLE_NAME, c.COLUMN_NAME, c.DATA_TYPE, fn.TYPE_LEN, fn.NULLABLE, fn.DEFAULTVAL) 
    /// FROM msdb.INFORMATION_SCHEMA.COLUMNS c 
    /// CROSS APPLY (SELECT 
    ///     TYPE_LEN = CASE 
    /// 		WHEN c.CHARACTER_MAXIMUM_LENGTH IS NULL THEN ''
    /// 		WHEN c.CHARACTER_MAXIMUM_LENGTH = -1 THEN '(MAX)'
    /// 		ELSE '(' + CAST(c.CHARACTER_MAXIMUM_LENGTH AS varchar(30)) + ')'
    /// 	   END,
    ///     NULLABLE = CASE
    /// 		  WHEN c.IS_NULLABLE = 'NO' THEN ' NOT NULL'
    /// 		  ELSE ' NULL '
    /// 	   END,
    ///     DEFAULTVAL = CASE
    /// 		  WHEN c.COLUMN_DEFAULT IS NOT NULL THEN ' DEFAULT (' + c.COLUMN_DEFAULT + ') '
    /// 		  ELSE ''
    /// 	   END 
    /// ) fn
    /// WHERE c.TABLE_NAME = 'backupset'
    /// </code>
    /// <br />
    /// <span style='font-weight: bold;font-size;14;'>Results:</span>
    /// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
    /// 	<thead><tr>
    /// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">FormatString</th>
    /// 	</tr></thead>
    /// 	<tbody>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[backup_set_id] int NOT NULL</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[backup_set_uuid] uniqueidentifier NOT NULL</td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[media_set_id] int NOT NULL</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[first_family_number] tinyint NULL </td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[first_media_number] smallint NULL </td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[last_family_number] tinyint NULL </td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[last_media_number] smallint NULL </td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[catalog_family_number] tinyint NULL </td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[catalog_media_number] smallint NULL </td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[backupset].[position] int NULL </td>
    /// 		</tr>
    /// 	</tbody>
    /// </table>
    /// </example>
    /// <returns>A copy of the format in which any format items are replaced by the string representation of the args.</returns>
    [SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
    public static SqlString xfnFormatString8(SqlString format, object arg0, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7)
    {
        return format.IsNull ? SqlString.Null : string.Format(format.Value, GetSqlValues(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7));
    }
    /// <summary>
    /// Replaces one or more format items in a specified string with the string representation of a specified object.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="arg0">The arg0.</param>
    /// <param name="arg1">The arg1.</param>
    /// <param name="arg2">The arg2.</param>
    /// <param name="arg3">The arg3.</param>
    /// <param name="arg4">The arg4.</param>
    /// <param name="arg5">The arg5.</param>
    /// <param name="arg6">The arg6.</param>
    /// <param name="arg7">The arg7.</param>
    /// <param name="arg8">The arg8.</param>
    /// <example>
    /// <code>
    /// SELECT TOP 10 [FormatString] = clr.xfnFormatString9(N'[{0}].[{1}].[{2}].[{3}] {4}{5}{6}{7}{8}', 'msdb', c.TABLE_SCHEMA, c.TABLE_NAME, c.COLUMN_NAME, c.DATA_TYPE, fn.TYPE_LEN, fn.NULLABLE, fn.DEFAULTVAL, fn.COLLATION) 
    /// FROM msdb.INFORMATION_SCHEMA.COLUMNS c 
    /// CROSS APPLY (SELECT 
    ///     TYPE_LEN = CASE 
    /// 		WHEN c.CHARACTER_MAXIMUM_LENGTH IS NULL THEN ''
    /// 		WHEN c.CHARACTER_MAXIMUM_LENGTH = -1 THEN '(MAX)'
    /// 		ELSE '(' + CAST(c.CHARACTER_MAXIMUM_LENGTH AS varchar(30)) + ')'
    /// 	   END,
    ///     NULLABLE = CASE
    /// 		  WHEN c.IS_NULLABLE = 'NO' THEN ' NOT NULL'
    /// 		  ELSE ' NULL '
    /// 	   END,
    ///     DEFAULTVAL = CASE
    /// 		  WHEN c.COLUMN_DEFAULT IS NOT NULL THEN ' DEFAULT (' + c.COLUMN_DEFAULT + ') '
    /// 		  ELSE ''
    /// 	   END,
    ///     COLLATION = CASE
    /// 		  WHEN c.COLLATION_NAME IS NOT NULL THEN ' COLLATION ' + c.COLLATION_NAME + ' '
    /// 		  ELSE ''
    /// 	   END 
    /// ) fn
    /// </code>
    /// <br />
    /// <span style='font-weight: bold;font-size;14;'>Results:</span>
    /// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
    /// 	<thead><tr>
    /// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">FormatString</th>
    /// 	</tr></thead>
    /// 	<tbody>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[syspolicy_target_sets].[target_set_id] int NOT NULL</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[syspolicy_target_sets].[object_set_id] int NOT NULL</td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[syspolicy_target_sets].[type_skeleton] nvarchar(440) NOT NULL COLLATION SQL_Latin1_General_CP1_CI_AS </td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[syspolicy_target_sets].[type] nvarchar(128) NOT NULL COLLATION SQL_Latin1_General_CP1_CI_AS </td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[syspolicy_target_sets].[enabled] bit NOT NULL</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[syspolicy_target_set_levels].[target_set_level_id] int NOT NULL</td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[syspolicy_target_set_levels].[target_set_id] int NOT NULL</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[syspolicy_target_set_levels].[type_skeleton] nvarchar(440) NOT NULL COLLATION SQL_Latin1_General_CP1_CI_AS </td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[syspolicy_target_set_levels].[condition_id] int NULL </td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">[msdb].[dbo].[syspolicy_target_set_levels].[level_name] nvarchar(128) NOT NULL COLLATION SQL_Latin1_General_CP1_CI_AS </td>
    /// 		</tr>
    /// 	</tbody>
    /// </table>
    /// </example>
    /// <returns>A copy of the format in which any format items are replaced by the string representation of the args.</returns>
    [SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
    public static SqlString xfnFormatString9(SqlString format, object arg0, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8)
    {
        return format.IsNull ? SqlString.Null : string.Format(format.Value, GetSqlValues(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
    }
    /// <summary>
    /// Replaces one or more format items in a specified string with the string representation of a specified object.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="arg0">The arg0.</param>
    /// <param name="arg1">The arg1.</param>
    /// <param name="arg2">The arg2.</param>
    /// <param name="arg3">The arg3.</param>
    /// <param name="arg4">The arg4.</param>
    /// <param name="arg5">The arg5.</param>
    /// <param name="arg6">The arg6.</param>
    /// <param name="arg7">The arg7.</param>
    /// <param name="arg8">The arg8.</param>
    /// <param name="arg9">The arg9.</param>
    /// <example>
    /// <code>
    /// SELECT TOP 10 [FormatString] = clr.xfnFormatString10(N'{9}. [{0}].[{1}].[{2}].[{3}] {4}{5}{6}{7}{8}', 'msdb', c.TABLE_SCHEMA, c.TABLE_NAME, c.COLUMN_NAME, c.DATA_TYPE, fn.TYPE_LEN, fn.NULLABLE, fn.DEFAULTVAL, fn.COLLATION, c.ORDINAL_POSITION) 
    /// FROM msdb.INFORMATION_SCHEMA.COLUMNS c 
    /// CROSS APPLY (SELECT 
    ///     TYPE_LEN = CASE 
    /// 		WHEN c.CHARACTER_MAXIMUM_LENGTH IS NULL THEN ''
    /// 		WHEN c.CHARACTER_MAXIMUM_LENGTH = -1 THEN '(MAX)'
    /// 		ELSE '(' + CAST(c.CHARACTER_MAXIMUM_LENGTH AS varchar(30)) + ')'
    /// 	   END,
    ///     NULLABLE = CASE
    /// 		  WHEN c.IS_NULLABLE = 'NO' THEN ' NOT NULL'
    /// 		  ELSE ' NULL '
    /// 	   END,
    ///     DEFAULTVAL = CASE
    /// 		  WHEN c.COLUMN_DEFAULT IS NOT NULL THEN ' DEFAULT (' + c.COLUMN_DEFAULT + ') '
    /// 		  ELSE ''
    /// 	   END,
    ///     COLLATION = CASE
    /// 		  WHEN c.COLLATION_NAME IS NOT NULL THEN ' COLLATION ' + c.COLLATION_NAME + ' '
    /// 		  ELSE ''
    /// 	   END 
    /// ) fn
    /// </code>
    /// <br />
    /// <span style='font-weight: bold;font-size;14;'>Results:</span>
    /// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
    /// 	<thead><tr>
    /// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">FormatString</th>
    /// 	</tr></thead>
    /// 	<tbody>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">1. [msdb].[dbo].[syspolicy_target_sets].[target_set_id] int NOT NULL</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">2. [msdb].[dbo].[syspolicy_target_sets].[object_set_id] int NOT NULL</td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">3. [msdb].[dbo].[syspolicy_target_sets].[type_skeleton] nvarchar(440) NOT NULL COLLATION SQL_Latin1_General_CP1_CI_AS </td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">4. [msdb].[dbo].[syspolicy_target_sets].[type] nvarchar(128) NOT NULL COLLATION SQL_Latin1_General_CP1_CI_AS </td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">5. [msdb].[dbo].[syspolicy_target_sets].[enabled] bit NOT NULL</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">1. [msdb].[dbo].[syspolicy_target_set_levels].[target_set_level_id] int NOT NULL</td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">2. [msdb].[dbo].[syspolicy_target_set_levels].[target_set_id] int NOT NULL</td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">3. [msdb].[dbo].[syspolicy_target_set_levels].[type_skeleton] nvarchar(440) NOT NULL COLLATION SQL_Latin1_General_CP1_CI_AS </td>
    /// 		</tr>
    /// 		<tr style="background-color:#DDDDDD;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">4. [msdb].[dbo].[syspolicy_target_set_levels].[condition_id] int NULL </td>
    /// 		</tr>
    /// 		<tr style="background-color:White;font-size:12;">
    /// 			<td style="font-family: Tahoma;font-size;10;">5. [msdb].[dbo].[syspolicy_target_set_levels].[level_name] nvarchar(128) NOT NULL COLLATION SQL_Latin1_General_CP1_CI_AS </td>
    /// 		</tr>
    /// 	</tbody>
    /// </table>
    /// </example>
    /// <returns>A copy of the format in which any format items are replaced by the string representation of the args.</returns>
    [SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
    public static SqlString xfnFormatString10(SqlString format, object arg0, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9)
    {
        return format.IsNull ? SqlString.Null : string.Format(format.Value, GetSqlValues(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
    }

    private static object[] GetSqlValues(params object[] sqlValue)
    {
        return Array.ConvertAll<object, object>(sqlValue, GetSqlValue);
    }

    private static object GetSqlValue(object sqlValue)
    {
        if (!(sqlValue is INullable isNull) || isNull.IsNull)
        {
            return null;
        } 
        var propInfo = sqlValue.GetType().GetProperty("Value");
        if (propInfo != null)
        {
            return propInfo.GetValue(sqlValue, null);
        }

        return sqlValue;
    }


}
