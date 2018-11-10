using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{
	private static readonly double _OneThird = 0.33333333333333331;

	/// <summary>
	/// Determines whether the specified number is a prime number.
	/// </summary>
	/// <param name="value">The number.</param>
	/// <example>
	/// <code>
	/// SELECT [xfnIsPrime(4)] = clr.xfnIsPrime(4) /*false*/, 
	/// 	[xfnIsPrime(7)] = clr.xfnIsPrime(7)/*true*/, 
	/// 	[xfnIsPrime(501)] = clr.xfnIsPrime(501)/*false*/, 
	/// 	[xfnIsPrime(571)] = clr.xfnIsPrime(571)/*true*/
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">xfnIsPrime(4)</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">xfnIsPrime(7)</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">xfnIsPrime(501)</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">xfnIsPrime(571)</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">False</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">True</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">False</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">True</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns>True if prime, otherwise false.</returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlBoolean xfnIsPrime(SqlInt64 value)
	{
		if (value <= 1 || value.IsNull)
		{
			return false;
		}
		else if (value == 2 || value == 3)
		{
			return true;
		}
		else if (value % 2 == 0 || value % 3 == 0)
		{
			return false;
		}

		for (int i = 3; i < value / 2; i += 2)
		{
			if (value % i == 0)
			{
				return false;
			}
		}

		return true;
	}
	/// <summary>
	/// Returns the cube root of the number.
	/// </summary>
	/// <param name="value">The number.</param>
	/// <example>
	/// <code>SELECT clr.xfnCubeRoot(125) /*5*/</code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Column1</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">5</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns>The cubed root of the number.</returns>
	[SqlFunction(IsDeterministic = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlDouble xfnCubeRoot(SqlDouble value)
	{
		if (value.IsNull)
		{
			return SqlDouble.Null;
		}

		return Math.Pow(value.Value, _OneThird);
	}

	/// <summary>
	/// Converts the hexadecimal string to a binary string.
	/// </summary>
	/// <param name="value">The hexadecimal string.</param>
	/// <example>
	/// <code>
	/// SELECT [Results] = clr.xfnHexToBinary('845FED')
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">100001000101111111101101</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns>A string of 0s and 1s representing the binary value.</returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlString xfnHexToBinary(SqlChars value)
	{
		if (value.IsNull)
		{
			return SqlString.Null;
		}
		char[] hexString = value.Value;

#if DOTNET45
		string binarystring = String.Join(String.Empty, hexString.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
#else
		string binarystring = String.Join(String.Empty, hexString.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')).ToArray());
#endif

		return new SqlString(binarystring);
	}
	/// <summary>
	/// Converts the binary string to a hexadecimal string.
	/// </summary>
	/// <param name="binary">The binary string.</param>
	/// <example>
	/// <code>
	/// SELECT [Results] = clr.xfnBinaryToHex('100001000101111111101101')
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">845FED</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns>A hexadecimal string representation of the value.</returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlString xfnBinaryToHex(SqlString binary)
	{
		if (binary.IsNull || SqlServer.ClrCommon.Utils.IsNullOrWhiteSpace(binary.Value))
		{
			return SqlString.Null;
		}
		string tmp = binary.Value;

		if (!Regex.IsMatch(tmp, "^[01]*$"))
		{
			throw new FormatException("The binary string was in an invalid format.");
		}

		StringBuilder result = new StringBuilder(tmp.Length / 8 + 1);

		int mod4Len = tmp.Length % 8;
		if (mod4Len != 0)
		{
			// pad to length multiple of 8
			tmp = tmp.PadLeft(((tmp.Length / 8) + 1) * 8, '0');
		}

		for (int i = 0; i < tmp.Length; i += 8)
		{
			string eightBits = tmp.Substring(i, 8);
			result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
		}

		return result.ToString();
	}

	/// <summary>
	/// Converts the decimal to hexadecimal.
	/// </summary>
	/// <param name="value">The decimal value.</param>
	/// <example>
	/// <code>
	/// SELECT [Results] = clr.xfnDecimalToHex('8675309')
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">845FED</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>	
	/// <returns>A hexadecimal string representation of the value.</returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlString xfnDecimalToHex(SqlInt32 value)
	{
		if (value.IsNull)
		{
			return SqlString.Null;
		}

		return value.Value.ToString("X");
	}

	/// <summary>
	/// Converts the hexadecimal string to a decimal.
	/// </summary>
	/// <param name="value">The hexadecimal.</param>
	/// <example>
	/// <code>
	/// SELECT [Results] = clr.xfnHexToDecimal('845FED')
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">8675309</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>	
	/// <returns>A decimal value.</returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlInt32 xfnHexToDecimal(SqlString value)
	{
		if (value.IsNull)
		{
			return SqlInt32.Null;
		}

		return int.Parse(value.Value, System.Globalization.NumberStyles.HexNumber);
	}

	/// <summary>
	/// Converts the decimal to a binary string.
	/// </summary>
	/// <param name="value">The decimal value.</param>
	/// <example>
	/// <code>
	/// SELECT [Results] = clr.xfnDecimalToBinary('8675309')
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">100001000101111111101101</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>	
	/// <returns>A string of 0s and 1s representing the binary value.</returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlString xfnDecimalToBinary(SqlInt32 value)
	{
		if (value.IsNull)
		{
			return SqlString.Null;
		}
		return Convert.ToString(value.Value, 2);
	}

	/// <summary>
	/// Converts the binary string to a decimal.
	/// </summary>
	/// <param name="binary">The binary.</param>
	/// <example>
	/// <code>
	/// SELECT [Results] = clr.xfnBinaryToDecimal('100001000101111111101101')
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">8675309</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>	
	/// <returns>The decimal value.</returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlInt32 xfnBinaryToDecimal(SqlString binary)
	{
		if (binary.IsNull || SqlServer.ClrCommon.Utils.IsNullOrWhiteSpace(binary.Value))
		{
			return SqlInt32.Null;
		}

		if (!Regex.IsMatch(binary.Value, "^[01]*$"))
		{
			throw new FormatException("The binary string was in an invalid format.");
		}

		return Convert.ToInt32(binary.Value, 2);
	}


	/// <summary>
	/// Returns the smaller value of the two values.
	/// </summary>
	/// <param name="val1">The val1.</param>
	/// <param name="val2">The val2.</param>
	/// <returns>Parameter val1 or val2, whichever is smaller.</returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlInt32 xfnMinInt(SqlInt32 val1, SqlInt32 val2)
	{
		if (val1.IsNull && val2.IsNull) return SqlInt32.Null;
		if (val1.IsNull) return val2.Value;
		if (val2.IsNull) return val1.Value;

		return new SqlInt32(Math.Min(val1.Value, val2.Value));
	}

	/// <summary>
	/// Returns the smaller value of the two values.
	/// </summary>
	/// <param name="val1">The val1.</param>
	/// <param name="val2">The val2.</param>
	/// <returns>Parameter val1 or val2, whichever is smaller.</returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlInt64 xfnMinBigInt(SqlInt64 val1, SqlInt64 val2)
	{
		if (val1.IsNull && val2.IsNull) return SqlInt64.Null;
		if (val1.IsNull) return val2.Value;
		if (val2.IsNull) return val1.Value;

		return new SqlInt64(Math.Min(val1.Value, val2.Value));
	}

	/// <summary>
	/// Returns the smaller value of the two values.
	/// </summary>
	/// <param name="val1">The val1.</param>
	/// <param name="val2">The val2.</param>
	/// <returns>Parameter val1 or val2, whichever is smaller.</returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlDouble xfnMinDecimal(SqlDouble val1, SqlDouble val2)
	{
		if (val1.IsNull && val2.IsNull) return SqlDouble.Null;
		if (val1.IsNull) return val2.Value;
		if (val2.IsNull) return val1.Value;

		return new SqlDouble(Math.Min(val1.Value, val2.Value));
	}

	/// <summary>
	/// Returns the maximum value of the two values.
	/// </summary>
	/// <param name="val1">The val1.</param>
	/// <param name="val2">The val2.</param>
	/// <returns>Parameter val1 or val2, whichever is greater.</returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlInt32 xfnMaxInt(SqlInt32 val1, SqlInt32 val2)
	{
		if (val1.IsNull && val2.IsNull) return SqlInt32.Null;
		if (val1.IsNull) return val2.Value;
		if (val2.IsNull) return val1.Value;

		return new SqlInt32(Math.Max(val1.Value, val2.Value));
	}

	/// <summary>
	/// Returns the maximum value of the two values.
	/// </summary>
	/// <param name="val1">The val1.</param>
	/// <param name="val2">The val2.</param>
	/// <returns>Parameter val1 or val2, whichever is greater.</returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlInt64 xfnMaxBigInt(SqlInt64 val1, SqlInt64 val2)
	{
		if (val1.IsNull && val2.IsNull) return SqlInt64.Null;
		if (val1.IsNull) return val2.Value;
		if (val2.IsNull) return val1.Value;

		return new SqlInt64(Math.Max(val1.Value, val2.Value));
	}

	/// <summary>
	/// Returns the maximum value of the two values.
	/// </summary>
	/// <param name="val1">The val1.</param>
	/// <param name="val2">The val2.</param>
	/// <returns>Parameter val1 or val2, whichever is greater.</returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlDouble xfnMaxDecimal(SqlDouble val1, SqlDouble val2)
	{
		if (val1.IsNull && val2.IsNull) return SqlDouble.Null;
		if (val1.IsNull) return val2.Value;
		if (val2.IsNull) return val1.Value;

		return new SqlDouble(Math.Max(val1.Value, val2.Value));
	}


}
