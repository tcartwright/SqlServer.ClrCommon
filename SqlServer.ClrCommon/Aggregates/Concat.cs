using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Text;

/// <summary>
/// Concatenates a column together using a delimiter when combined with a group by. Please look at the <see cref="Accumulate"/> method for an example.
/// </summary>
[Serializable]
[SqlUserDefinedAggregate(
	Format.UserDefined, // Binary Serialization because of StringBuilder
	IsInvariantToOrder = false, // order changes the result
	IsInvariantToNulls = true,  // nulls don't change the result
	IsInvariantToDuplicates = false, // duplicates change the result
	MaxByteSize = -1
 )]
public struct xagConcat : IBinarySerialize
{
	//http://www.mssqltips.com/sqlservertip/2022/concat-aggregates-sql-server-clr-function/

	private StringBuilder _accumulator;
	private string _delimiter;

	/// <summary>
	/// IsNull property
	/// </summary>
	public Boolean IsNull { get; private set; }

	/// <summary>
	/// Initializes this instance.
	/// </summary>
	public void Init()
	{
		_accumulator = new StringBuilder();
		_delimiter = string.Empty;
		this.IsNull = true;
	}

	/// <summary>
	/// Accumulates the specified value.
	/// </summary>
	/// <param name="Value">The value.</param>
	/// <param name="Delimiter">The delimiter.</param>
	/// <example>
	/// <code>
	/// DECLARE @tbl TABLE (
	/// 	RowId int NOT NULL IDENTITY(1, 1),
	/// 	Name varchar(50),
	/// 	StateProvince char(2)
	/// )
	/// 
	/// INSERT INTO @tbl
	/// VALUES
	/// 	('Foo','TX') 
	/// 	,('Foo','CA') 
	/// 	,('Foo','WA') 
	/// 	,('Bar','OK') 
	/// 	,('Bar','MI') 
	/// 
	/// SELECT Name, clr.xagConcat(t.StateProvince, ',') , clr.xagConcat(t.RowId, ',')  
	/// FROM @tbl t
	/// GROUP BY t.Name
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Name</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Column1</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Column2</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Bar</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">OK,MI</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">4,5</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Foo</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">TX,CA,WA</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">1,2,3</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	public void Accumulate(
		SqlString Value,
		[SqlFacet(MaxSize = 10)]
		SqlString Delimiter)
	{
		if (!Delimiter.IsNull
			& Delimiter.Value.Length > 0)
		{
			_delimiter = Delimiter.Value; // save for Merge
			if (_accumulator.Length > 0) _accumulator.Append(Delimiter.Value);

		}
		_accumulator.Append(Value.Value);
		if (Value.IsNull == false) this.IsNull = false;
	}
	/// <summary>
	/// Merge onto the end
	/// </summary>
	/// <param name="group">The group.</param>
	public void Merge(xagConcat group)
	{
		// add the delimiter between strings
		if (_accumulator.Length > 0 & group._accumulator.Length > 0) { _accumulator.Append(_delimiter); }

		//_accumulator += Group._accumulator;
		_accumulator.Append(group._accumulator.ToString());

	}

	/// <summary>
	/// Terminates this instance.
	/// </summary>
	/// <returns></returns>
	public SqlString Terminate()
	{
		// Put your code here
		return new SqlString(_accumulator.ToString());
	}

	/// <summary>
	/// deserialize from the reader to recreate the struct
	/// </summary>
	/// <param name="r">BinaryReader</param>
	void IBinarySerialize.Read(System.IO.BinaryReader r)
	{
		_delimiter = r.ReadString();
		_accumulator = new StringBuilder(r.ReadString());

		if (_accumulator.Length != 0) { this.IsNull = false; }
	}

	/// <summary>
	/// searialize the struct.
	/// </summary>
	/// <param name="w">BinaryWriter</param>
	void IBinarySerialize.Write(System.IO.BinaryWriter w)
	{
		w.Write(_delimiter);
		w.Write(_accumulator.ToString());
	}
}