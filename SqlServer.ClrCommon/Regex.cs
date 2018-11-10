using Microsoft.SqlServer.Server;
using System;
using System.Collections;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;

public partial class UserDefinedFunctions
{

	/// <summary>
	/// The <see cref="System.Text.RegularExpressions.RegexOptions">RegexOptions</see> used by all of the regex calls. Defaults to IgnorePatternWhitespace and Singleline. 
	/// </summary>
	public static readonly RegexOptions Options = RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline;

	/// <summary>
	/// The <see cref="System.Text.RegularExpressions.Regex">Regex</see> timeout used to protect from <a href="http://www.regular-expressions.info/catastrophic.html">catastrophic back tracking</a> and long running regular expressions.
	/// </summary>
	public static readonly TimeSpan RegexTimeout = TimeSpan.FromSeconds(3);

	#region validators
	/// <summary>
	/// Verifies that the SSN is valid and passes rules as defined here http://en.wikipedia.org/wiki/Social_Security_number#Valid_SSNs.
	/// </summary>
	/// <param name="input">The input.</param>
	/// <example>
	/// <code>
	/// select [Results1] = clr.xfnRegexIsValidSSN(N'000-12-1234'), 
	///     [Results2] = clr.xfnRegexIsValidSSN(N'487-12-1234')
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results1</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results2</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">False</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">True</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns>1 if valid, otherwise 0.</returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlBoolean xfnRegexIsValidSSN([SqlFacet(MaxSize = 15)] SqlString input)
	{
		if (input.IsNull) return false;

		//http://regexlib.com/REDetails.aspx?regexp_id=2850
		string pattern = @"^((?!000)(?!666)(?:[0-6]\d{2}|7[0-2][0-9]|73[0-3]|7[5-6][0-9]|77[0-2]))-((?!00)\d{2})-((?!0000)\d{4})$";
#if DOTNET45
		return Regex.IsMatch(input.Value, pattern, Options, RegexTimeout);
#else
		return Regex.IsMatch(input.Value, pattern, Options);
#endif
	}

	/// <summary>
	/// Verifies the zip is a valid US zip code.
	/// </summary>
	/// <param name="input">The input.</param>
	/// <example>
	/// <code>
	/// SELECT [Results1] = clr.xfnRegexIsValidUSZip(N'12345'), 
	///     [Results2] = clr.xfnRegexIsValidUSZip(N'12345-6789')
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results1</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results2</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">True</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">True</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns>1 if valid, otherwise 0.</returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlBoolean xfnRegexIsValidUSZip([SqlFacet(MaxSize = 10)] SqlString input)
	{
		if (input.IsNull) return false;
		string pattern = @"^((?!0{5})(?:\d{5})(?!-?0{4})(?:|-\d{4})?)$";
#if DOTNET45
		return Regex.IsMatch(input.Value, pattern, Options, RegexTimeout);
#else
		return Regex.IsMatch(input.Value, pattern, Options);
#endif
	}

	/// <summary>
	/// Verifies the phone passed in is a valid US phone.
	/// </summary>
	/// <param name="input">The input.</param>
	/// <example>
	/// <code>
	/// SELECT [Results1] = clr.xfnRegexIsValidUSPhone(N'123-456-7890')
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results1</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">True</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns>1 if valid, otherwise 0.</returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlBoolean xfnRegexIsValidUSPhone([SqlFacet(MaxSize = 20)]SqlString input)
	{
		if (input.IsNull) return false;
		string pattern = @"^1?[-\. ]?(\(\d{3}\)?[-\. ]?|\d{3}?[-\. ]?)?\d{3}?[-\. ]?\d{4}$";
#if DOTNET45
		return Regex.IsMatch(input.Value, pattern, Options, RegexTimeout);
#else
		return Regex.IsMatch(input.Value, pattern, Options);
#endif
	}

	/// <summary>
	/// Verifies that the email is valid.
	/// </summary>
	/// <param name="input">The input.</param>
	/// <example>
	/// <code>
	/// SELECT [Results1] = clr.xfnRegexIsValidEmail(N'john.doe@insperity.com'), 
	///     [Results2] = clr.xfnRegexIsValidEmail(N'john.doe-insperity.com'), 
	///     [Results3] = clr.xfnRegexIsValidEmail(N'john.doe@foo@insperity.com')
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results1</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results2</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results3</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">True</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">False</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">False</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns>1 if valid, otherwise 0.</returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlBoolean xfnRegexIsValidEmail([SqlFacet(MaxSize = 512)]SqlString input)
	{
		if (input.IsNull) return false;
		string pattern = @"^([0-9a-zA-Z]+(?:[0-9a-zA-Z]*[-._+])*[0-9a-zA-Z]+)@([0-9a-zA-Z]+(?:[-.][0-9a-zA-Z]+)*(?:[0-9a-zA-Z]*[.])[a-zA-Z]{2,6})$";
#if DOTNET45
		return Regex.IsMatch(input.Value, pattern, Options, RegexTimeout);
#else
		return Regex.IsMatch(input.Value, pattern, Options);
#endif

	}


	#endregion


	/// <summary>
	/// In a specified input string, replaces strings that match a regular expression pattern with a specified replacement string.
	/// </summary>
	/// <param name="input">The input.</param>
	/// <param name="pattern">The pattern.</param>
	/// <param name="newvalue">The newvalue.</param>
	/// <example>
	/// <code>
	/// DECLARE @text nvarchar(max) = N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum laoreet, enim sed venenatis elementum, velit neque fermentum nulla, sit amet eleifend libero sapien eu erat.'
	/// 
	/// SELECT [Results1] = clr.xfnRegexReplace(@text, '(?i)(ARE)|(ET)|(PER)', ' *MY NEW TEXT* ' ), -- case insensitive regex
	///     [Results2] = clr.xfnRegexReplace(@text, '(ARE)|(ET)|(PER)', '*MY NEW TEXT*' ) -- case sensitive regex, hence no replacments made
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results1</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results2</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Lorem ipsum dolor sit am *MY NEW TEXT* , consect *MY NEW TEXT* ur adipiscing elit. Vestibulum laore *MY NEW TEXT* , enim sed venenatis elementum, velit neque fermentum nulla, sit am *MY NEW TEXT*  eleifend libero sapien eu erat.</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum laoreet, enim sed venenatis elementum, velit neque fermentum nulla, sit amet eleifend libero sapien eu erat.</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns>
	/// A new string that is identical to the input string, except that the replacement string takes the place of each matched string. If the regular expression pattern is not matched in the current instance, the method returns the current instance unchanged.
	/// </returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlString xfnRegexReplace(
		[SqlFacet(MaxSize = -1)]
		SqlString input,
		[SqlFacet(IsNullable = false, MaxSize = 4000)]
		SqlString pattern,
		[SqlFacet(MaxSize = 4000)]
		SqlString newvalue)
	{
		if (input.IsNull) { return SqlString.Null; }
        if (newvalue.IsNull) { newvalue = new SqlString(String.Empty); }

#if DOTNET45
		return Regex.Replace(input.Value, pattern.Value, newvalue.Value, Options, RegexTimeout);
#else
		return Regex.Replace(input.Value, pattern.Value, newvalue.Value, Options);
#endif
	}

	/// <summary>
	/// Indicates whether the regular expression finds a match in the input string.
	/// </summary>
	/// <param name="input">The input.</param>
	/// <param name="pattern">The pattern.</param>
	/// <example>
	/// <code>
	/// SELECT [Results1] = clr.xfnRegexMatch(N'123-45-6789', N'^\d{3}-\d{2}-\d{4}$' ),
	///		[Results2] = clr.xfnRegexMatch(NULL, N'^\d{3}-\d{2}-\d{4}$' ),
	///		[Results3] = clr.xfnRegexMatch('clr.xfnRegexMatch', N'(?i)xfn(reg|format).*'), /*case insensitive regex*/
	///		[Results4] = clr.xfnRegexMatch('clr.xfnRegexMatch', N'xfn(reg|format).*') /*case sensitive regex by default*/
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results1</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results2</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results3</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results4</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">True</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">False</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">True</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">False</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns>1 if the regular expression finds a match; otherwise, 0.</returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlBoolean xfnRegexMatch(
		[SqlFacet(MaxSize = -1)] 
		SqlString input,
		[SqlFacet(IsNullable = false, MaxSize = 4000)] 
		SqlString pattern)
	{
        if (input.IsNull) { return false; }

#if DOTNET45
		return Regex.IsMatch(input.Value, pattern.Value, Options, RegexTimeout);
#else
		return Regex.IsMatch(input.Value, pattern.Value, Options);
#endif

	}

	/// <summary>
	/// Returns the specified group from the collection of groups matched by the regular expression.
	/// </summary>
	/// <param name="input">The input.</param>
	/// <param name="pattern">The pattern.</param>
	/// <param name="name">The name.</param>
	/// <example>
	/// <code>
	/// SELECT [Results1] = clr.xfnRegexGroup(N'Lorem ipsum1 dolor sit amet, consectetur adipiscing elit. Lorem ipsum2 dolor sit amet, consectetur adipiscing elit. Lorem3 ipsum dolor sit amet, consectetur adipiscing elit. ', '(?&lt;Foo&gt;ipsum\d?)', 'Foo') ,
	/// 	[Results2] = clr.xfnRegexGroup(N'1234567,Abraham Lincoln,A,M', '^(?&lt;CustomerId&gt;\d{7}),(?&lt;Name&gt;[^,]*),(?&lt;Type&gt;[A-Z]),(?&lt;Gender&gt;M|F)$', 'Name')
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results1</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results2</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">ipsum1</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">Abraham Lincoln</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns>The value of the group if found within the input value.</returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlString xfnRegexGroup(
		[SqlFacet(MaxSize = -1)]
		SqlString input,
		[SqlFacet(IsNullable = false, MaxSize = 4000)]
		SqlString pattern,
		[SqlFacet(IsNullable = false, MaxSize = 50)]
		SqlString name)
	{
        if (input.IsNull) { return SqlString.Null; }

#if DOTNET45
		Regex regex = new Regex(pattern.Value, Options, RegexTimeout);
#else
		Regex regex = new Regex(pattern.Value, Options);
#endif

		Match match = regex.Match(input.Value);
		return match.Success ? new SqlString(match.Groups[name.Value].Value) : SqlString.Null;
	}

	/// <summary>
	/// Searches the specified input string for all occurrences of a specified regular expression.
	/// </summary>
	/// <param name="input">The input.</param>
	/// <param name="pattern">The pattern.</param>
	/// <example>
	/// <code>
	/// --case sensitive by default, no rows returned
	/// SELECT [TableName] = o.name, [ColumnName] = c.name 
	/// FROM msdb.sys.columns c  
	/// INNER JOIN msdb.sys.objects o ON o.object_id = c.object_id
	/// WHERE clr.xfnRegexMatch(c.name, '.*(COND|CREA).*') = 1 
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 	</tbody>
	/// </table>
	/// <br />
	/// <code>
	/// --explicitly turn on case in-sensitivity, rows should be returned
	/// SELECT [TableName] = o.name, [ColumnName] = c.name 
	/// FROM msdb.sys.columns c  
	/// INNER JOIN msdb.sys.objects o ON o.object_id = c.object_id
	/// WHERE clr.xfnRegexMatch(c.name, '(?i).*(COND|CREA).*') = 1 
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">TableName</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">ColumnName</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">syspolicy_target_set_levels</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">condition_id</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">backupset</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">database_creation_date</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">backupfile</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">create_lsn</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">sysdac_instances</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">created_by</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">sysdac_instances</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">date_created</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">syspolicy_conditions</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">date_created</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">syspolicy_conditions</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">created_by</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">syspolicy_conditions</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">condition_id</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">syspolicy_conditions</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">is_name_condition</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">syspolicy_policies</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">root_condition_id</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">syspolicy_policies</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">condition_id</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">syspolicy_policies</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">created_by</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">syspolicy_policies</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">date_created</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns>A table containing the matches found by the search. If no matches are found, the method returns an empty table.</returns>
	[SqlFunction(FillRowMethodName = "FillMatchRow", TableDefinition = "[RowId] int, [Text] nvarchar(max)")]
	public static IEnumerable xfnRegexMatches(
		[SqlFacet(MaxSize = -1)]
		SqlString input,
		[SqlFacet(IsNullable = false, MaxSize = 4000)]
		SqlString pattern)
	{
        if (input.IsNull) { return null; }

		return new MatchIterator(input.Value, pattern.Value);
	}

	/// <summary>
	/// Fills the match row.
	/// </summary>
	/// <param name="data">The data.</param>
	/// <param name="rowid">The rowid.</param>
	/// <param name="text">The text.</param>
	public static void FillMatchRow(object data, out SqlInt32 rowid, out SqlChars text)
	{
		MatchNode node = (MatchNode)data;
		rowid = new SqlInt32(node.RowId);
		text = new SqlChars(node.Value.ToCharArray());
	}


	/// <summary>
	/// Returns the groups and matches made by a pattern against the input.
	/// </summary>
	/// <param name="input">The input.</param>
	/// <param name="pattern">The pattern.</param>
	/// <example>
	/// <span style='font-weight: bold;font-size;14;'>Example 1 (Pivot data):</span>
	/// <code>
	/// DECLARE @listpattern nvarchar(500) = N&#39;(?&lt;CustomerId&gt;\d{7}),(?&lt;Name&gt;[^,]*),(?&lt;Type&gt;[A-Za-z]),(?&lt;Gender&gt;[Mm]|[Ff])\r?\n&#39;
	///     ,@list varchar(1000) = 
	///         &#39;1234567,Abraham Lincoln,A,M
	///         9876453,George Washington,B,M
	///         8647530,Samuel Adams,C,M
	///         4561237,Thomas Jefferson,D,M
	///         2589631,Andrew Jackson,D,M
	///         &#39;
	/// --SELECT * FROM clr.xfnRegexGroups( @list, @listpattern ) regex
	/// SELECT
	///     --f.[0],
	///     f.[CustomerId],
	///     f.[Name],
	///     f.[Type],
	///     f.[Gender]
	/// from clr.xfnRegexGroups( @list, @listpattern ) regex
	/// pivot
	/// (
	///     max([Text])
	///     for [Group]
	///     in ([0], [CustomerId], [Name], [Type], [Gender] )
	/// ) as f
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">CustomerId</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Name</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Type</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Gender</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">1234567</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">Abraham Lincoln</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">A</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">M</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">9876453</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">George Washington</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">B</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">M</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">8647530</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">Samuel Adams</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">C</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">M</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">4561237</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">Thomas Jefferson</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">D</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">M</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">2589631</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">Andrew Jackson</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">D</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">M</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Example 2 (no pivot):</span>
	/// <code>
	/// DECLARE @listpattern nvarchar(500) = N&#39;(?&lt;CustomerId&gt;\d{7}),(?&lt;Name&gt;[^,]*),(?&lt;Type&gt;[A-Za-z]),(?&lt;Gender&gt;[Mm]|[Ff])\r?\n&#39;
	///     ,@list varchar(1000) = 
	///         &#39;1234567,Abraham Lincoln,A,M
	///         9876453,George Washington,B,M
	///         8647530,Samuel Adams,C,M
	///         4561237,Thomas Jefferson,D,M
	///         2589631,Andrew Jackson,D,M
	///         &#39;
	/// SELECT * FROM clr.xfnRegexGroups( @list, @listpattern ) regex
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">RowId</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Group</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Text</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">1</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">0</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">1234567,Abraham Lincoln,A,M</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">1</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">CustomerId</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">1234567</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">1</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">Name</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">Abraham Lincoln</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">1</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">Type</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">A</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">1</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">Gender</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">M</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">2</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">0</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">9876453,George Washington,B,M</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">2</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">CustomerId</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">9876453</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">2</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">Name</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">George Washington</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">2</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">Type</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">B</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">2</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">Gender</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">M</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">3</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">0</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">8647530,Samuel Adams,C,M</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">3</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">CustomerId</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">8647530</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">3</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">Name</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">Samuel Adams</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">3</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">Type</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">C</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">3</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">Gender</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">M</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">4</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">0</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">4561237,Thomas Jefferson,D,M</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">4</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">CustomerId</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">4561237</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">4</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">Name</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">Thomas Jefferson</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">4</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">Type</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">D</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">4</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">Gender</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">M</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">5</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">0</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">2589631,Andrew Jackson,D,M</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">5</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">CustomerId</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">2589631</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">5</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">Name</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">Andrew Jackson</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">5</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">Type</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">D</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">5</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">Gender</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">M</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	///	</example>
	/// <returns>A table defining all the groups and matches for the regular expression.</returns>
	[SqlFunction(FillRowMethodName = "FillGroupRow", TableDefinition = "[RowId] int,[Group] nvarchar(max),[Text] nvarchar(max)")]
	public static IEnumerable xfnRegexGroups(
		[SqlFacet(MaxSize = -1)]
		SqlString input,
		[SqlFacet(IsNullable = false, MaxSize = 4000)]
		SqlString pattern)
	{
        if (input.IsNull) { return null; }

		return new GroupIterator(input.Value, pattern.Value);
	}

	/// <summary>
	/// Fills the group row.
	/// </summary>
	/// <param name="data">The data.</param>
	/// <param name="rowid">The rowid.</param>
	/// <param name="group">The group.</param>
	/// <param name="text">The text.</param>
	public static void FillGroupRow(object data, out SqlInt32 rowid, out SqlChars group, out SqlChars text)
	{
		GroupNode node = (GroupNode)data;
		rowid = new SqlInt32(node.RowId);
		group = new SqlChars(node.Name.ToCharArray());
		text = new SqlChars(node.Value.ToCharArray());
	}


	#region internals
	internal class MatchNode
	{
		private int _rowid;
		public int RowId { get { return _rowid; } }

		private string _value;
		public string Value { get { return _value; } }

		public MatchNode(int rowid, string value)
		{
			_rowid = rowid;
			_value = value;
		}
	}

	internal class MatchIterator : IEnumerable
	{
		private Regex _regex;
		private string _input;

		public MatchIterator(string input, string pattern)
		{
#if DOTNET45
			_regex = new Regex(pattern, UserDefinedFunctions.Options, UserDefinedFunctions.RegexTimeout);
#else
			_regex = new Regex(pattern, UserDefinedFunctions.Options);
#endif

			_input = input;
		}

		public IEnumerator GetEnumerator()
		{
			int rowid = 0;
			Match current = null;
			do
			{
				current = (current == null) ? _regex.Match(_input) : current.NextMatch();
				if (current.Success)
				{
					System.Threading.Thread.Sleep(0);
					yield return new MatchNode(++rowid, current.Value);
				}
			}
			while (current.Success);
		}
	}

	internal class GroupNode
	{
		private int _rowid;
		public int RowId { get { return _rowid; } }

		private string _name;
		public string Name { get { return _name; } }

		private string _value;
		public string Value { get { return _value; } }

		public GroupNode(int rowid, string group, string value)
		{
			_rowid = rowid;
			_name = group;
			_value = value;
		}
	}

	internal class GroupIterator : IEnumerable
	{
		private Regex _regex;
		private string _input;

		public GroupIterator(string input, string pattern)
		{
#if DOTNET45
			_regex = new Regex(pattern, UserDefinedFunctions.Options, UserDefinedFunctions.RegexTimeout);
#else
			_regex = new Regex(pattern, UserDefinedFunctions.Options);
#endif

			_input = input;
		}

		public IEnumerator GetEnumerator()
		{
			int rowid = 0;
			Match current = null;
			string[] names = _regex.GetGroupNames();
			do
			{
				rowid++;
				current = (current == null) ? _regex.Match(_input) : current.NextMatch();
				if (current.Success)
				{
					foreach (string name in names)
					{
						Group group = current.Groups[name];
						if (group.Success)
						{
							System.Threading.Thread.Sleep(0);
							yield return new GroupNode(rowid, name, group.Value);
						}
					}
				}
			}
			while (current.Success);
		}
	}
	#endregion

}
