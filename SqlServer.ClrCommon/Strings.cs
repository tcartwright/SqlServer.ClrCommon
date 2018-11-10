using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

//TDC: Note: this summary should only be defined once as this is a partial class, and sandcastle will pick it up multiple times if defined on each class
/// <summary>
/// A set of sql clr functions designed to provide extra functionality for Sql Server and performance benefits when compared to standard user defined functions.
/// </summary>
public partial class UserDefinedFunctions
{

	/// <summary>
	/// A better quotename function. Will split apart the data, and quote the parts. Useful for blocking sql injection in the field list and object names when you are forced into dynamic sql.
	/// </summary>
	/// <param name="nameSet">The object name, or column list.</param>
	/// <example>
	///	<code>
	/// SELECT [OldFNResults] = QuoteName('dbo.TABLE_NAME'), 
	/// 	[Results1] = clr.xfnBetterQuoteName('TABLE_NAME'), 
	/// 	[Results2] = clr.xfnBetterQuoteName('dbo.TABLE_NAME'), 
	/// 	[Results3] = clr.xfnBetterQuoteName('dbo.OBJECT_NAME ObjAlias'),
	/// 	[Results4] = clr.xfnBetterQuoteName('DB_NAME..OBJECT_NAME.COLUMN_NAME'),
	/// 	[Results5] = clr.xfnBetterQuoteName('db..tbl.Column1 foo, foo = db.dbo.tbl.Column2, tbl.Column3 as col3, tbl.Column4 ''col4'', tbl.Column5, tbl.Column6, tbl2.*, ;here is my sql injection; --')
	///	</code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">OldFNResults</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results1</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results2</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results3</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results4</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results5</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">[dbo.TABLE_NAME]</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">[TABLE_NAME]</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">[dbo].[TABLE_NAME]</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">[dbo].[OBJECT_NAME] As [ObjAlias]</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">[DB_NAME]..[OBJECT_NAME].[COLUMN_NAME]</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">[db]..[tbl].[Column1] As [foo], [foo] = [db].[dbo].[tbl].[Column2], [tbl].[Column3] As [col3], [tbl].[Column4] As [col4], [tbl].[Column5], [tbl].[Column6], [tbl2].*, [;here] As [is my sql injection; --]</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns>Returns a Unicode string with the delimiters added to make the input string a valid SQL Server delimited identifier.</returns>
	[return: SqlFacet(MaxSize = -1)]
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlString xfnBetterQuoteName(
		[SqlFacet(MaxSize = -1)]
		SqlString nameSet)
	{
		string ret = null;
		if (nameSet.IsNull || SqlServer.ClrCommon.Utils.IsNullOrWhiteSpace(nameSet.Value)) { return SqlString.Null; }

		string data = nameSet.Value;
		//strip out pre-existing brackets
		data = data.Replace("[", "").Replace("]", "");

		if (!data.Contains(","))
		{
			ret = QuotePart(data);
		}
		else
		{
			var tmp = data.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(column => QuotePart(column));

#if DOTNET45
			ret = String.Join(", ", tmp);
#else
			ret = String.Join(", ", tmp.ToArray());
#endif
			ret = ret.Replace("[*]", "*");	//sigh, handle the * even though it should not be sent in as a best practice. 
		}

		ret = ret.Replace("[]", ""); //handle empty name spaces like in dbname..tablename.columnname
		return new SqlString(ret);
	}

	private static string QuotePart(string part)
	{
		string ret = null;
		Thread.Sleep(0); //release the thread so the sql server thread manager wont think we are hung in case they send in a large number of columns
		var quotedCol = part.Split(new char[] { '.' }).Select(colPart => QuoteSubPart(colPart));

#if DOTNET45
		ret = "[" + String.Join("].[", quotedCol) + "]";
#else
		ret = "[" + String.Join("].[", quotedCol.ToArray()) + "]";
#endif
		return ret;
	}

	private static string QuoteSubPart(string colPart)
	{
		if (SqlServer.ClrCommon.Utils.IsNullOrWhiteSpace(colPart)) return String.Empty;

		colPart = colPart.Trim().Replace("'", "");
		if (Regex.IsMatch(colPart, @"(?i)\s*\bas\b\s*"))
		{
			colPart = Regex.Replace(colPart, @"(?i)\s*\bas\b\s*", "] As [");
		}
		else if (Regex.IsMatch(colPart, @"\s*=\s*"))
		{
			colPart = Regex.Replace(colPart, @"\s*=\s*", "] = [");
		}
		else if (Regex.IsMatch(colPart, @"^[^\s]*\s+.*$"))
		{
			colPart = Regex.Replace(colPart, @"^([^\s]*)\s+(.*)$", "$1] As [$2");
		}

		return colPart;
	}

	/// <summary>
	/// Converts the value of the specified object to its equivalent string representation using the specified formatting information.
	/// </summary>
	/// <param name="val0">The val0.</param>
	/// <param name="format">The format.</param>
	/// <example>
	/// <code>
	/// SELECT [Results1] = clr.xfnFormat('2015-07-27 09:42:26.1552697', 'MM/dd/yyyy'),
	/// 	[Results2] = clr.xfnFormat('2015-07-27 09:42:26.1552697', 'yyyy-MM-dd'),
	/// 	[Results3] = clr.xfnFormat('1234.4321', '$##,###.00'),
	/// 	[Results4] = clr.xfnFormat('1234', '##,###.00'),
	/// 	[Results5] = clr.xfnFormat(null, '##,###.00')
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results1</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results2</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results3</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results4</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results5</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">07/27/2015</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">2015-07-27</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">$1,234.43</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">1,234.00</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">NULL</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns>The string representation of value. If value is null, the method returns null. </returns>
	[return: SqlFacet(MaxSize = -1)]
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlString xfnFormat(
		[SqlFacet()]
		object val0, //sqlvariant
		[SqlFacet(IsNullable = false, MaxSize = -1)]
		SqlString format)
	{
		if (SqlServer.ClrCommon.Utils.IsNullOrWhiteSpace(Convert.ToString(val0)))
		{
			return SqlString.Null;
		}
		string value = Convert.ToString(val0);

		int iVal = 0;
		if (Int32.TryParse(value, out iVal))
		{
			return new SqlString(iVal.ToString(format.Value));
		}
		long lVal = 0;
		if (Int64.TryParse(value, out lVal))
		{
			return new SqlString(lVal.ToString(format.Value));
		}
		Double dblVal = 0;
		if (Double.TryParse(value, out dblVal))
		{
			return new SqlString(dblVal.ToString(format.Value));
		}
		DateTime dtVal = DateTime.MinValue;
		if (DateTime.TryParse(value, out dtVal))
		{
			return new SqlString(dtVal.ToString(format.Value));
		}
#if DOTNET45
		TimeSpan tsVal = TimeSpan.MinValue;
		if (TimeSpan.TryParse(value, out tsVal))
		{
			return new SqlString(tsVal.ToString(format.Value));
		}
#endif
		//fallback to default string format. it only works for one value however.
		return new SqlString(String.Format(format.Value, value));
	}


	/// <summary>
	/// Converts the specified string to title case (except for words that are entirely in uppercase, which are considered to be acronyms).
	/// </summary>
	/// <param name="str0">The string to convert.</param>
	/// <example>
	/// <code>
	/// SELECT [Results] = clr.xfnProperCase('john doe')
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">John Doe</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns>The specified string converted to title case.</returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static string xfnProperCase(string str0)
	{
		if (SqlServer.ClrCommon.Utils.IsNullOrWhiteSpace(str0))
		{
			return null;
		}

		TextInfo textInfo = Thread.CurrentThread.CurrentCulture.TextInfo;

		return textInfo.ToTitleCase(str0);
	}

	/// <summary>
	/// Counts the how many time the value occurs within the string.
	/// </summary>
	/// <param name="str0">The initial string.</param>
	/// <param name="value">The value.</param>
	/// <example>
	/// <code>
	/// SELECT [Results] = clr.xfnCountString('Lorem ipsum dolor sit amet? Lorem ipsum dolor sit amet?', 'ipsum')
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">2</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns></returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static int xfnCountString(string str0, string value)
	{
		if (SqlServer.ClrCommon.Utils.IsNullOrWhiteSpace(str0) || SqlServer.ClrCommon.Utils.IsNullOrWhiteSpace(value))
		{
			return 0;
		}

		str0 = (str0 ?? "").ToLower();
		value = (value ?? "").ToLower();

		if (str0.Length <= 0 || value.Length <= 0)
			return 0;

		string tmp = str0.Replace(value, "");

		return (str0.Length - tmp.Length) / value.Length;
	}

	/// <summary>
	/// Reports the zero-based index of the first occurrence of the specified string in this instance.
	/// </summary>
	/// <param name="str0">The initial string.</param>
	/// <param name="value">The value.</param>
	/// <example>
	/// <code>
	/// SELECT [Results1] = clr.xfnIndexOf('Lorem ipsum dolor sit amet? Lorem ipsum dolor sit amet?', 'ipsum'),
	///     [Results2] = clr.xfnIndexOf('Lorem ipsum dolor sit amet? Lorem ipsum dolor sit amet?', 'NOT FOUND')	
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
	/// 			<td style="font-family: Tahoma;font-size;10;">6</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">-1</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns>The zero-based index position of value if that string is found, or -1 if it is not. If value is String.Empty, the return value is 0.</returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static int xfnIndexOf(string str0, string value)
	{
		if (SqlServer.ClrCommon.Utils.IsNullOrWhiteSpace(str0) || SqlServer.ClrCommon.Utils.IsNullOrWhiteSpace(value))
		{
			return -1;
		}

		return str0.IndexOf(value);
	}

	/// <summary>
	/// Reports the zero-based index position of the last occurrence of a specified string within this instance.
	/// </summary>
	/// <param name="str0">The initial string.</param>
	/// <param name="value">The value.</param>
	/// <example>
	/// <code>
	/// SELECT [Results1] = clr.xfnLastIndexOf('Lorem ipsum dolor sit amet? Lorem ipsum dolor sit amet?', 'ipsum'),
	///     [Results2] = clr.xfnLastIndexOf('Lorem ipsum dolor sit amet? Lorem ipsum dolor sit amet?', 'NOT FOUND')
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
	/// 			<td style="font-family: Tahoma;font-size;10;">34</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">-1</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns>The zero-based starting index position of value if that string is found, or -1 if it is not. If value is String.Empty, the return value is the last index position in this instance.</returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static int xfnLastIndexOf(string str0, string value)
	{
		if (SqlServer.ClrCommon.Utils.IsNullOrWhiteSpace(str0) || SqlServer.ClrCommon.Utils.IsNullOrWhiteSpace(value))
		{
			return -1;
		}

		return str0.LastIndexOf(value);
	}

	/// <summary>
	/// Reports location of the value at the the index of the occurrence of a specified Unicode character or string within this instance. The method returns -1 if the character or string is not found in this instance.
	/// </summary>
	/// <param name="str0">The initial string.</param>
	/// <param name="value">The value.</param>
	/// <param name="occurence">The occurence.</param>
	/// <example>
	/// <code>
	/// SELECT [Results1] = clr.xfnNthIndexOf('Lorem ipsum dolor sit amet? Lorem ipsum dolor sit amet?', 'ipsum', 1), 
	///     [Results2] = clr.xfnNthIndexOf('Lorem ipsum dolor sit amet? Lorem ipsum dolor sit amet?', 'ipsum', 2), 
	///     [Results3] = clr.xfnNthIndexOf('Lorem ipsum dolor sit amet? Lorem ipsum dolor sit amet?', 'ipsum', 3)
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
	/// 			<td style="font-family: Tahoma;font-size;10;">6</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">34</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">-1</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns>The zero-based starting index position of value if that string is found, or -1 if it is not. If value is String.Empty, the return value is the last index position in this instance.</returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static int xfnNthIndexOf(string str0, string value, int occurence)
	{
		if (SqlServer.ClrCommon.Utils.IsNullOrWhiteSpace(str0) || SqlServer.ClrCommon.Utils.IsNullOrWhiteSpace(value))
		{
			return -1;
		}

		int ret = -1;
		for (int i = 0; i < occurence; i++)
		{
			ret = str0.IndexOf(value, ret + 1);
			if (ret == -1) break;
		}
		return ret;
	}

	/// <summary>
	/// Returns a new string that right-aligns the characters in this instance by padding them on the left with a specified Unicode character, for a specified total length.
	/// </summary>
	/// <param name="str0">The initial string.</param>
	/// <param name="padChar">The value.</param>
	/// <param name="maxLenth">The maximum lenth.</param>
	/// <example>
	/// <code>
	/// SELECT [Results] = clr.xfnPadLeft('9999', '0', 10)
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">0000009999</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns>A new string that is equivalent to this instance, but right-aligned and padded on the left with as many paddingChar characters as needed to 
	/// create a length of totalWidth. However, if totalWidth is less than the length of this instance, the method returns a reference to the existing 
	/// instance. If totalWidth is equal to the length of this instance, the method returns a new string that is identical to this instance.</returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static string xfnPadLeft(string str0, char? padChar, int maxLenth)
	{
		if (!padChar.HasValue)
		{
			return null;
		}
		str0 = (str0 ?? "");
		return str0.PadLeft(maxLenth, padChar.Value);
	}

	/// <summary>
	/// Returns a new string that left-aligns the characters in this string by padding them on the right with a specified Unicode character, for a specified total length.
	/// </summary>
	/// <param name="str0">The initial string.</param>
	/// <param name="padChar">The value.</param>
	/// <param name="maxLenth">The maximum lenth.</param>
	/// <example>
	/// <code>
	/// SELECT [Results] = clr.xfnPadRight('9999', '0', 10)
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">9999000000</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns>A new string that is equivalent to this instance, but left-aligned and padded on the right with as many paddingChar characters as needed 
	/// to create a length of totalWidth. However, if totalWidth is less than the length of this instance, the method returns a reference to the existing 
	/// instance. If totalWidth is equal to the length of this instance, the method returns a new string that is identical to this instance.</returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static string xfnPadRight(string str0, char? padChar, int maxLenth)
	{
		if (!padChar.HasValue)
		{
			return null;
		}
		str0 = (str0 ?? "");
		return str0.PadRight(maxLenth, padChar.Value);
	}

	/// <summary>
	/// Converts the string representation of a number to its 32-bit signed integer equivalent. 
	/// </summary>
	/// <param name="str0">The initial string.</param>
	/// <param name="defaultValue">The default value.</param>
	/// <example>
	/// <code>
	/// SELECT [Results1] = clr.xfnTryParseToInt('123', 0), 
	///     [Results2] = clr.xfnTryParseToInt('123a', -1)
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
	/// 			<td style="font-family: Tahoma;font-size;10;">123</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">-1</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns>The value of the string if was was converted successfully; otherwise, the default value.</returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static int xfnTryParseToInt(
		[SqlFacet(MaxSize = 30)]
		string str0, int defaultValue
	)
	{
		if (SqlServer.ClrCommon.Utils.IsNullOrWhiteSpace(str0))
		{
			return defaultValue;
		}

        if (int.TryParse(str0, out int ret))
        {
            return ret;
        }
        else
        {
            return defaultValue;
        }

    }

	/// <summary>
	/// Converts the string representation of a number to its double-precision floating-point number equivalent. 
	/// </summary>
	/// <param name="str0">The initial string.</param>
	/// <param name="defaultValue">The default value.</param>
	/// <example>
	/// <code>
	/// SELECT [Results1] = clr.xfnTryParseToDbl('123.45', 0), 
	///     [Results2] = clr.xfnTryParseToDbl('123.45a', -1),
	///     [Results3] = clr.xfnTryParseToDbl(NULL, -1)
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	///<table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	///	<thead><tr>
	///		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results1</th>
	///		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results2</th>
	///		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results3</th>
	///	</tr></thead>
	///	<tbody>
	///		<tr style="background-color:#DDDDDD;font-size:12;">
	///			<td style="font-family: Tahoma;font-size;10;">123.45</td>
	///			<td style="font-family: Tahoma;font-size;10;">-1</td>
	///			<td style="font-family: Tahoma;font-size;10;">-1</td>
	///		</tr>
	///	</tbody>
	///</table>
	/// </example>
	/// <returns>The value of the string if was was converted successfully; otherwise, the default value.</returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static double xfnTryParseToDbl(
		[SqlFacet(MaxSize = 60, IsNullable = false)]
		string str0, double defaultValue
	)
	{
		if (SqlServer.ClrCommon.Utils.IsNullOrWhiteSpace(str0))
		{
			return defaultValue;
		}

        if (double.TryParse(str0, out double ret))
        {
            return ret;
        }
        else
        {
            return defaultValue;
        }

    }

}
