using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using Microsoft.SqlServer.Server;


/// <summary>
/// Median finds the middle number in a set of data. Please look at the <see cref="Accumulate"/> method for an example.
/// </summary>
[Serializable]
[SqlUserDefinedAggregate(
	Format.UserDefined,
	IsInvariantToDuplicates = false,
	IsInvariantToNulls = false,
	IsNullIfEmpty = true,
	IsInvariantToOrder = true,
	MaxByteSize = -1
)]
public struct xagMedian : IBinarySerialize
{
	//http://devnambi.com/2011/t-sql-tuesday-aggregates/
	/// <summary>
	/// Initializes this instance.
	/// </summary>
	public void Init()
	{
		// Put your code here
		doubleList = new List<double>();
	}

	/// <summary>
	/// Accumulates the specified numbers so that a median can be found.
	/// </summary>
	/// <param name="input">The input.</param>
	/// <example>
	/// <span style='font-weight: bold;font-size;14;'>Example 1 (even number of rows):</span>
	/// <code>
	/// SELECT [Median] = clr.xagMedian(t.v), 
	///     [Average] = AVG(t.v)
	/// FROM ( VALUES (1),(3),(9),(10)  ) t(v) -- 3 + 9 = 12 / 2 = 6 median
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Median</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Average</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">6</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">5</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// 
	/// 
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Example 2 (odd number of rows):</span>
	/// <code>
	/// SELECT [Median] = clr.xagMedian(t.v), 
	///     [Average] = AVG(t.v)
	/// FROM ( VALUES (3),(9),(10) ) t(v)
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Median</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Average</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">9</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">7</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <remarks>To find the Median, place the numbers you are given in value order and find the middle number. If there are two middle numbers, you average them.</remarks>
	public void Accumulate(SqlDouble input)
	{
		// Put your code here
		if (input.IsNull != true)
		{
			doubleList.Add(input.Value);
		}
	}

	/// <summary>
	/// Merges the specified group.
	/// </summary>
	/// <param name="Group">The group.</param>
	public void Merge(xagMedian Group)
	{
		// Put your code here
		List<double> secondDoubleList = Group.DoubleList;
		foreach (double d in secondDoubleList)
		{
			doubleList.Add(d);
		}
	}

	/// <summary>
	/// Terminates this instance.
	/// </summary>
	/// <returns></returns>
	public SqlDouble Terminate()
	{
		// Put your code here
		doubleList.Sort();
		double medianValue;
		if (doubleList.Count % 2 != 0)
		{
			medianValue = doubleList[doubleList.Count / 2];
		}
		else
		{
			// if there are an equal number of values, the median is the mean of the 2 middle values
			medianValue = (doubleList[doubleList.Count / 2] + doubleList[(doubleList.Count - 1) / 2]) / 2.0;
		}
		return new SqlDouble(medianValue);
	}


	/*
	 * The binary layout is as follows:
	 * Each 8 bytes is a double
	 */
	/// <summary>
	/// Reads the specified binary reader.
	/// </summary>
	/// <param name="binaryReader">The binary reader.</param>
	public void Read(BinaryReader binaryReader)
	{
		if (doubleList == null)
		{
			doubleList = new List<double>();
		}
		long readerLength = binaryReader.BaseStream.Length;
		while (binaryReader.BaseStream.Position <= (readerLength - 8))
		{
			doubleList.Add(binaryReader.ReadDouble());
		}
	}

	/// <summary>
	/// Writes the specified binary writer.
	/// </summary>
	/// <param name="binaryWriter">The binary writer.</param>
	public void Write(BinaryWriter binaryWriter)
	{
		foreach (double d in doubleList)
		{
			binaryWriter.Write(d);
		}
	}

	// This is a place-holder member field
	private List<double> doubleList;

	/// <summary>
	/// Gets the double list.
	/// </summary>
	/// <value>
	/// The double list.
	/// </value>
	public List<double> DoubleList
	{
		get
		{
			return this.doubleList;
		}
	}

}
