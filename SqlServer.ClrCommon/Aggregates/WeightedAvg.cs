using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;


/// <summary>
/// An average in which each quantity to be averaged is assigned a weight. These weightings determine the relative importance of each quantity on the average. 
/// Weightings are the equivalent of having that many like items with the same value involved in the average. Please look at the <see cref="Accumulate"/> method for an example.
/// </summary>
[Serializable]
[SqlUserDefinedAggregate(
	Format.Native,
	IsInvariantToDuplicates = false,
	IsInvariantToNulls = true,
	IsInvariantToOrder = true,
	IsNullIfEmpty = true
)]
public struct xagWeightedAvg
{
	//https://msdn.microsoft.com/en-us/library/ms131056.aspx

	/// <summary>
	/// The variable that holds the intermediate sum of all values multiplied by their weight
	/// </summary>
	private long sum;

	/// <summary>
	/// The variable that holds the intermediate sum of all weights
	/// </summary>
	private int count;

	/// <summary>
	/// Initialize the internal data structures
	/// </summary>
	public void Init()
	{
		sum = 0;
		count = 0;
	}

	/// <summary>
	/// Accumulate the next value, not if the value is null
	/// </summary>
	/// <param name="Value">Next value to be aggregated</param>
	/// <param name="Weight">The weight of the value passed to Value parameter</param>
	/// <example>
	/// <code>
	/// DECLARE @tbl TABLE (ItemValue int, ItemWeight int);
	/// 
	/// INSERT INTO @tbl 
	/// VALUES(1, 4), (6, 1);
	/// 
	/// SELECT [WeightedAvg] = clr.xagWeightedAvg(t.ItemValue, t.ItemWeight), 
	///     [Average] = AVG(t.ItemValue) 
	/// FROM @tbl t;
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">WeightedAvg</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Average</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">2</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">3</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>   
	public void Accumulate(SqlInt32 Value, SqlInt32 Weight)
	{
		if (!Value.IsNull && !Weight.IsNull)
		{
			sum += (long)Value * (long)Weight;
			count += (int)Weight;
		}
	}

	/// <summary>
	/// Merge the partially computed aggregate with this aggregate
	/// </summary>
	/// <param name="Group">The other partial results to be merged</param>
	public void Merge(xagWeightedAvg Group)
	{
		sum += Group.sum;
		count += Group.count;
	}

	/// <summary>
	/// Called at the end of aggregation, to return the results of the aggregation.
	/// </summary>
	/// <returns>The weighted average of all inputed values</returns>
	public SqlInt32 Terminate()
	{
		if (count > 0)
		{
			int value = (int)(sum / count);
			return new SqlInt32(value);
		}
		else
		{
			return SqlInt32.Null;
		}
	}
}
