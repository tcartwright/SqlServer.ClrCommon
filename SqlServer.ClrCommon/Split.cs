using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{
	// Original Author: Adam Machanic http://sqlblog.com/blogs/adam_machanic/archive/2009/04/28/sqlclr-string-splitting-part-2-even-faster-even-more-scalable.aspx
	// Changes and enhancements: Tim Cartwright

	/// <summary>
	/// Splits the string into a table of int32s.
	/// </summary>
	/// <param name="Input">The input.</param>
	/// <param name="Delimiter">The delimiter.</param>
	/// <param name="removeEmptyEntries">If true the return value does not include array elements that contain an empty string</param>
	/// <example>
	/// <code>
	///	DECLARE @ints varchar(max) = '9688982,ABC,6330859,,8372639,3598278'
	///
	///	SELECT xs.RowId, xs.[Value] FROM clr.xfnSplitInts(@ints, ',', 0) xs
	///	SELECT xs.RowId, xs.[Value] FROM clr.xfnSplitInts(@ints, ',', 1) xs
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>1st Result Set:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">RowId</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Value</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">1</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">9688982</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">2</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">NULL</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">3</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">6330859</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">4</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">NULL</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">5</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">8372639</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">6</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">3598278</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>2nd Result Sets:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">RowId</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Value</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">1</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">9688982</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">2</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">NULL</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">3</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">6330859</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">4</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">8372639</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">5</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">3598278</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns>A table containing the values obtained by splitting the string into its parts.</returns>
	/// <remarks>
	/// </remarks>
	[SqlFunction(FillRowMethodName = "FillRow_MultiInt32", TableDefinition = "[RowId] int, [Value] int", IsDeterministic = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static IEnumerator xfnSplitInts(
		[SqlFacet(MaxSize = -1)]
		SqlChars Input,
		[SqlFacet(MaxSize = 255, IsNullable = false)]
		SqlChars Delimiter,
		[SqlFacet(IsNullable = false)]
		SqlBoolean removeEmptyEntries)
	{
		return (
			Input.IsNull || Delimiter.IsNull || Input.Value.Length == 0 || Delimiter.Value.Length == 0 ?
			new SplitStringMulti(new char[0], new char[0], removeEmptyEntries.Value) :
			new SplitStringMulti(Input.Value, Delimiter.Value, removeEmptyEntries.Value));
	}

	/// <summary>
	/// Fills the row_ multi int32.
	/// </summary>
	/// <param name="obj">The object.</param>
	/// <param name="rowid">The rowid.</param>
	/// <param name="item">The item.</param>
	public static void FillRow_MultiInt32(object obj, out SqlInt32 rowid, out SqlInt32 item)
	{
		SplitStringRow row = (SplitStringRow)obj;
		rowid = row.RowId;

        if (int.TryParse(row.Value, out int tmp))
        {
            item = tmp;
        }
        else
        {
            item = SqlInt32.Null;
        }
    }

	/// <summary>
	/// Splits the string into a table of int64s.
	/// </summary>
	/// <param name="Input">The input.</param>
	/// <param name="Delimiter">The delimiter.</param>
	/// <param name="removeEmptyEntries">If true the return value does not include array elements that contain an empty string</param>
	/// <example>
	/// <code>
	/// DECLARE @bigints varchar(max) = '62844761503424,,32011638416242,ABC,14978476014566,42522905323296'
	/// 
	/// SELECT xs.RowId, xs.[Value] FROM clr.xfnSplitBigInts(@bigints, ',', 0) xs
	/// SELECT xs.RowId, xs.[Value] FROM clr.xfnSplitBigInts(@bigints, ',', 1) xs
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>1st Result Set:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">RowId</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Value</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">1</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">62844761503424</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">2</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">NULL</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">3</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">32011638416242</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">4</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">NULL</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">5</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">14978476014566</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">6</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">42522905323296</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>2nd Result Set:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">RowId</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Value</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">1</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">62844761503424</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">2</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">32011638416242</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">3</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">NULL</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">4</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">14978476014566</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">5</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">42522905323296</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns>A table containing the values obtained by splitting the string into its parts.</returns>
	/// <remarks>
	/// </remarks>
	[SqlFunction(FillRowMethodName = "FillRow_MultiInt64", TableDefinition = "[RowId] int, [Value] bigint", IsDeterministic = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static IEnumerator xfnSplitBigInts(
		[SqlFacet(MaxSize = -1)]
		SqlChars Input,
		[SqlFacet(MaxSize = 255, IsNullable = false)]
		SqlChars Delimiter,
		[SqlFacet(IsNullable = false)]
		SqlBoolean removeEmptyEntries)
	{
		return (
			Input.IsNull || Delimiter.IsNull || Input.Value.Length == 0 || Delimiter.Value.Length == 0 ?
			new SplitStringMulti(new char[0], new char[0], removeEmptyEntries.Value) :
			new SplitStringMulti(Input.Value, Delimiter.Value, removeEmptyEntries.Value));
	}

	/// <summary>
	/// Fills the row_ multi int64.
	/// </summary>
	/// <param name="obj">The object.</param>
	/// <param name="rowid">The rowid.</param>
	/// <param name="item">The item.</param>
	public static void FillRow_MultiInt64(object obj, out SqlInt32 rowid, out SqlInt64 item)
	{
		SplitStringRow row = (SplitStringRow)obj;
		rowid = row.RowId;

        if (Int64.TryParse(row.Value, out long tmp))
        {
            item = tmp;
        }
        else
        {
            item = SqlInt64.Null;
        }
    }

	/// <summary>
	/// Splits the string into a table of doubles.
	/// </summary>
	/// <param name="Input">The input.</param>
	/// <param name="Delimiter">The delimiter.</param>
	/// <param name="removeEmptyEntries">If true the return value does not include array elements that contain an empty string</param>
	/// <example>
	/// <code>
	/// DECLARE @Floats varchar(max) = '6000193.6465576,,2202454.2474793,ABC,2198445.3170504,6612013.2746950'
	/// 
	/// SELECT xs.RowId, xs.[Value] FROM clr.xfnSplitFloats(@Floats, ',', 0) xs
	/// SELECT xs.RowId, xs.[Value] FROM clr.xfnSplitFloats(@Floats, ',', 1) xs
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>1st Result Set:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">RowId</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Value</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">1</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">6000193.6465576</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">2</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">NULL</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">3</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">2202454.2474793</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">4</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">NULL</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">5</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">2198445.3170504</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">6</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">6612013.274695</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>2nd Result Set:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">RowId</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Value</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">1</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">6000193.6465576</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">2</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">2202454.2474793</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">3</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">NULL</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">4</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">2198445.3170504</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">5</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">6612013.274695</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns>A table containing the values obtained by splitting the string into its parts.</returns>
	/// <remarks>
	/// </remarks>
	[SqlFunction(FillRowMethodName = "FillRow_MultiDbls", TableDefinition = "[RowId] int, [Value] float", IsDeterministic = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static IEnumerator xfnSplitFloats(
		[SqlFacet(MaxSize = -1)]
		SqlChars Input,
		[SqlFacet(MaxSize = 255, IsNullable = false)]
		SqlChars Delimiter,
		[SqlFacet(IsNullable = false)]
		SqlBoolean removeEmptyEntries)
	{
		return (
			Input.IsNull || Delimiter.IsNull || Input.Value.Length == 0 || Delimiter.Value.Length == 0 ?
			new SplitStringMulti(new char[0], new char[0], removeEmptyEntries.Value) :
			new SplitStringMulti(Input.Value, Delimiter.Value, removeEmptyEntries.Value));
	}

	/// <summary>
	/// Fills the row_ multi DBLS.
	/// </summary>
	/// <param name="obj">The object.</param>
	/// <param name="rowid">The rowid.</param>
	/// <param name="item">The item.</param>
	public static void FillRow_MultiDbls(object obj, out SqlInt32 rowid, out SqlDouble item)
	{
		SplitStringRow row = (SplitStringRow)obj;
		rowid = row.RowId;

        if (double.TryParse(row.Value, out double tmp))
        {
            item = tmp;
        }
        else
        {
            item = SqlDouble.Null;
        }
    }

	/// <summary>
	/// Splits the string into a table of nvarchars.
	/// </summary>
	/// <param name="Input">The input.</param>
	/// <param name="Delimiter">The delimiter.</param>
	/// <param name="removeEmptyEntries">If true the return value does not include array elements that contain an empty string</param>
	/// <example>
	/// <code>
	/// DECLARE @lorem varchar(max) = 'Lorem,ipsum,dolor,sit,amet,,consectetur,adipiscing,elit.'
	/// 
	/// SELECT xs.RowId, xs.[Value] FROM clr.xfnSplit(@lorem, ',', 0) xs
	/// SELECT xs.RowId, xs.[Value] FROM clr.xfnSplit(@lorem, ',', 1) xs
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>1st Result Set:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">RowId</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Value</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">1</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">Lorem</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">2</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">ipsum</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">3</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">dolor</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">4</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">sit</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">5</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">amet</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">6</td>
	/// 			<td style="font-family: Tahoma;font-size;10;"></td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">7</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">consectetur</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">8</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">adipiscing</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">9</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">elit.</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">RowId</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Value</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">1</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">Lorem</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">2</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">ipsum</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">3</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">dolor</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">4</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">sit</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">5</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">amet</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">6</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">consectetur</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">7</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">adipiscing</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">8</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">elit.</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns>A table containing the values obtained by splitting the string into its parts.</returns>
	/// <remarks>
	/// </remarks>
	[SqlFunction(FillRowMethodName = "FillRow_MultiString", TableDefinition = "[RowId] int, [Value] nvarchar(4000)", IsDeterministic = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static IEnumerator xfnSplit(
		[SqlFacet(MaxSize = -1)]
		SqlChars Input,
		[SqlFacet(MaxSize = 255, IsNullable = false)]
		SqlChars Delimiter,
		[SqlFacet(IsNullable = false)]
		SqlBoolean removeEmptyEntries)
	{
		return (
			Input.IsNull || Delimiter.IsNull || Input.Value.Length == 0 || Delimiter.Value.Length == 0 ?
			new SplitStringMulti(new char[0], new char[0], removeEmptyEntries.Value) :
			new SplitStringMulti(Input.Value, Delimiter.Value, removeEmptyEntries.Value));
	}

	/// <summary>
	/// Fills the row_ multi string.
	/// </summary>
	/// <param name="obj">The object.</param>
	/// <param name="rowid">The rowid.</param>
	/// <param name="item">The item.</param>
	public static void FillRow_MultiString(object obj, out SqlInt32 rowid, out SqlString item)
	{
		SplitStringRow row = (SplitStringRow)obj;
		rowid = row.RowId;
		item = row.Value;
	}

	/// <summary>
	/// 
	/// </summary>
	public class SplitStringMulti : IEnumerator
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SplitStringMulti" /> class.
		/// </summary>
		/// <param name="TheString">The string.</param>
		/// <param name="Delimiter">The delimiter.</param>
		/// <param name="removeEmptyEntries">If true the return value does not include array elements that contain an empty string.</param>
		public SplitStringMulti(char[] TheString, char[] Delimiter, bool removeEmptyEntries = false)
		{
			rowData = new SplitStringRow();
			theString = TheString;
			stringLen = TheString.Length;
			delimiter = Delimiter;
			delimiterLen = (byte)(Delimiter.Length);
			isSingleCharDelim = (delimiterLen == 1);
			this.removeEmptyEntries = removeEmptyEntries;

			lastPos = 0;
			nextPos = delimiterLen * -1;
		}

		#region IEnumerator Members

		/// <summary>
		/// Gets the current.
		/// </summary>
		/// <value>
		/// The current.
		/// </value>
		public object Current
		{
			get
			{
				rowData.RowId = rowid++;
				rowData.Value = new string(theString, lastPos, nextPos - lastPos);
				return rowData;
			}
		}

		/// <summary>
		/// Moves the next.
		/// </summary>
		/// <returns></returns>
		public bool MoveNext()
		{
			if (nextPos >= stringLen)
			{
				return false;
			}
			else
			{
				lastPos = nextPos + delimiterLen;

				for (int i = lastPos; i < stringLen; i++)
				{
					bool matches = true;

					//Optimize for single-character delimiters
					if (isSingleCharDelim)
					{
						if (theString[i] != delimiter[0])
						{
							matches = false;
						}
					}
					else
					{
						for (byte j = 0; j < delimiterLen; j++)
						{
							if (((i + j) >= stringLen) || (theString[i + j] != delimiter[j]))
							{
								matches = false;
								break;
							}
						}
					}

					if (matches)
					{
						nextPos = i;

						//Deal with consecutive delimiters
						if ((nextPos - lastPos) > 0 || (!removeEmptyEntries && (nextPos - lastPos) >= 0))
						{
							return true;
						}
						else
						{
							i += (delimiterLen - 1);
							lastPos += delimiterLen;
						}
					}
				}

				lastPos = nextPos + delimiterLen;
				nextPos = stringLen;

				if ((nextPos - lastPos) > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		/// <summary>
		/// Resets this instance.
		/// </summary>
		public void Reset()
		{
			rowData = new SplitStringRow();
			rowid = 1;
			lastPos = 0;
			nextPos = delimiterLen * -1;
		}

		#endregion
		private SplitStringRow rowData = null;
		private int rowid = 1;
		private int lastPos;
		private int nextPos;

		private readonly char[] theString;
		private readonly char[] delimiter;
		private readonly int stringLen;
		private readonly byte delimiterLen;
		private readonly bool isSingleCharDelim;
		private readonly bool removeEmptyEntries;
	}

	internal class SplitStringRow
	{
		public int RowId { get; set; }
		public string Value { get; set; }

	}
}
