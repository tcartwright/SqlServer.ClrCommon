using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{
	/// <summary>
	/// Returns the calendar age of the specified birth date in years.
	/// </summary>
	/// <param name="birthDate">The birth date.</param>
	/// <example>
	/// <code>
	/// SELECT clr.xfnAgeInYears('7/22/1965')
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Age</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">50</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns>The age in years from the given birth date to now.</returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static int xfnAgeInYears(DateTime birthDate)
	{
		DateTime now = DateTime.Now;
		int age = now.Year - birthDate.Year;
		if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day)) { age--; }
		return age;
	}

	/// <summary>
	/// Returns the calendar age of the specified birth date in years at a specific date in time.
	/// </summary>
	/// <param name="birthDate">The birth date.</param>
	/// <param name="atDate">The end date at which to calculate the age.</param>
	/// <returns>
	/// The age in years from the given birth date to now.
	/// </returns>
	/// <example>
	/// <code>
	/// SELECT clr.xfnAgeInYearsAtDate('7/22/1965', '8/22/2000')
	/// </code>
	///   <br />
	///   <span style="font-weight: bold;font-size;14;">Results:</span>
	///   <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	///     <thead>
	///       <tr>
	///         <th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Age</th>
	///       </tr>
	///     </thead>
	///     <tbody>
	///       <tr style="background-color:#DDDDDD;font-size:12;">
	///         <td style="font-family: Tahoma;font-size;10;">35</td>
	///       </tr>
	///     </tbody>
	///   </table>
	/// </example>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static int xfnAgeInYearsAtDate(DateTime birthDate, DateTime atDate)
	{
		DateTime dt = atDate;
		int age = dt.Year - birthDate.Year;
		if (dt.Month < birthDate.Month || (dt.Month == birthDate.Month && dt.Day < birthDate.Day)) { age--; }
		return age;
	}

	/// <summary>
	/// Returns the start of the given date or midnight.
	/// </summary>
	/// <param name="date">The date.</param>
	/// <example>
	/// <code>
	/// SELECT [StartOfDay] = clr.xfnStartOfDay('2015-08-03 11:15:22.000')
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">StartOfDay</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">8/3/2015 12:00:00 AM</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns></returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlDateTime xfnStartOfDay(SqlDateTime date)
	{
		if (date.IsNull)
		{
			return SqlDateTime.Null;
		}
		return date.Value.Date;
	}

	/// <summary>
	/// Returns the end of the day by advancing one day into the future, and setting the time to midnight. This is so that a sql 
	/// where clause can use this date to be inclusive of all other dates regardless of precision.
	/// </summary>
	/// <param name="date">The date.</param>
	/// <example>
	/// <code>
	/// SELECT [EndOfDay] = clr.xfnEndOfDay('2015-08-03 11:15:22.000')
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">EndOfDay</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">8/4/2015 12:00:00 AM</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns></returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlDateTime xfnEndOfDay(SqlDateTime date)
	{
		if (date.IsNull)
		{
			return SqlDateTime.Null;
		}
		/* This end of day code presents a problem as the 997 precision is the highest a datetime type can go to. however a datetime2(7) can go to 9999999.
		  * this could potentially lead to data being left out. One option would be to advance one day to midnight. 
		 
			DECLARE @dt1 datetime = '2000-01-01 23:59:59.999', @dt2 datetime2 = '2000-01-01 23:59:59.999'
			SELECT @dt1, @dt2
		 */
		//return new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, 23, 59, 59, 997);

		//assume midnight, and advance one day so that datetime2's can be caught as well.
		return date.Value.Date.AddDays(1);
	}


	/// <summary>
	/// Returns the start of the week for the given date.
	/// </summary>
	/// <param name="date">The date.</param>
	/// <example>
	/// <code>
	/// SELECT [StartOfWeek] = clr.xfnStartOfWeek('2015-08-03 11:15:22.000')
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">StartOfWeek</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">8/2/2015 12:00:00 AM</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns></returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlDateTime xfnStartOfWeek(SqlDateTime date)
	{
		if (date.IsNull)
		{
			return SqlDateTime.Null;
		}
		return date.Value.Date.AddDays(-((int)date.Value.DayOfWeek));
	}

	/// <summary>
	/// Returns the end of the week for the given date.
	/// </summary>
	/// <param name="date">The date.</param>
	/// <example>
	/// <code>
	/// SELECT [EndOfWeek] = clr.xfnEndOfWeek('2015-08-03 11:15:22.000')
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">EndOfWeek</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">8/9/2015 12:00:00 AM</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns></returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlDateTime xfnEndOfWeek(SqlDateTime date)
	{
		if (date.IsNull)
		{
			return SqlDateTime.Null;
		}
		return xfnEndOfDay(xfnStartOfWeek(date).Value.AddDays(6));
	}

	/// <summary>
	/// Returns the start of the month for the given date.
	/// </summary>
	/// <param name="date">The date.</param>
	/// <example>
	/// <code>
	/// SELECT [StartOfMonth] = clr.xfnStartOfMonth('2015-08-03 11:15:22.000')
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">StartOfMonth</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">8/1/2015 12:00:00 AM</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns></returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlDateTime xfnStartOfMonth(SqlDateTime date)
	{
		if (date.IsNull)
		{
			return SqlDateTime.Null;
		}
		return new DateTime(date.Value.Year, date.Value.Month, 1);
	}

	/// <summary>
	/// Returns the end of the month for the given date.
	/// </summary>
	/// <param name="date">The date.</param>
	/// <example>
	/// <code>
	/// SELECT [EndOfMonth] = clr.xfnEndOfMonth('2015-08-03 11:15:22.000')
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">EndOfMonth</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">9/1/2015 12:00:00 AM</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns></returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlDateTime xfnEndOfMonth(SqlDateTime date)
	{
		if (date.IsNull)
		{
			return SqlDateTime.Null;
		}
		return xfnEndOfDay(xfnStartOfMonth(date).Value.AddMonths(1).AddDays(-1));
	}

	/// <summary>
	/// Returns the start of the year for the given date.
	/// </summary>
	/// <param name="date">The date.</param>
	/// <example>
	/// <code>
	/// SELECT [StartOfYear] = clr.xfnStartOfYear('2015-08-03 11:15:22.000')
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">StartOfYear</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">1/1/2015 12:00:00 AM</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns></returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlDateTime xfnStartOfYear(SqlDateTime date)
	{
		if (date.IsNull)
		{
			return SqlDateTime.Null;
		}
		return new DateTime(date.Value.Year, 1, 1);
	}

	/// <summary>
	/// Returns the end of the year for the given date.
	/// </summary>
	/// <param name="date">The date.</param>
	/// <example>
	/// <code>
	/// SELECT [EndOfYear] = clr.xfnEndOfYear('2015-08-03 11:15:22.000')
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">EndOfYear</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">1/1/2016 12:00:00 AM</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns></returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlDateTime xfnEndOfYear(SqlDateTime date)
	{
		if (date.IsNull)
		{
			return SqlDateTime.Null;
		}
		return xfnEndOfDay(xfnStartOfYear(date).Value.AddYears(1).AddDays(-1));
	}

	/// <summary>
	/// Returns the number of days in the month for the given date.
	/// </summary>
	/// <param name="date">The date.</param>
	/// <example>
	/// <code>
	/// SELECT [DaysInMonth] = clr.xfnDaysInMonth('2015-08-03 11:15:22.000')
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">DaysInMonth</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">31</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns></returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlInt32 xfnDaysInMonth(SqlDateTime date)
	{
		if (date.IsNull)
		{
			return SqlInt32.Null;
		}
		return DateTime.DaysInMonth(date.Value.Year, date.Value.Month);
	}

	/// <summary>
	/// Determines whether or not the given date falls in a leap year.
	/// </summary>
	/// <param name="date">The date.</param>
	/// <example>
	/// <code>
	/// SELECT [IsLeapYear] = clr.xfnIsLeapYear('2015-08-03 11:15:22.000')
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">IsLeapYear</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">False</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns></returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlBoolean xfnIsLeapYear(SqlDateTime date)
	{
		if (date.IsNull)
		{
			return SqlBoolean.Null;
		}
		return DateTime.IsLeapYear(date.Value.Year);
	}
	/// <summary>
	/// Returns the name of the day for the given date.
	/// </summary>
	/// <param name="date">The date.</param>
	/// <example>
	/// <code>
	/// SELECT [DayOfWeek] = clr.xfnDayOfWeek('2015-08-03 11:15:22.000')
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">DayOfWeek</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Monday</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns></returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static string xfnDayOfWeek(SqlDateTime date)
	{
		if (date.IsNull)
		{
			return null;
		}

		return date.Value.DayOfWeek.ToString();
	}
	/// <summary>
	/// Returns the number of the day for the given date. 
	/// </summary>
	/// <param name="date">The date.</param>
	/// <example>
	/// <code>
	/// SELECT [DayOfYear] = clr.xfnDayOfYear('2015-08-03 11:15:22.000')
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">DayOfYear</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">215</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns></returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlInt32 xfnDayOfYear(SqlDateTime date)
	{
		if (date.IsNull)
		{
			return SqlInt32.Null;
		}

		return date.Value.DayOfYear;
	}
	/// <summary>
	/// Determines whether or not the given date falls within daylight savings time.
	/// </summary>
	/// <param name="date">The date.</param>
	/// <example>
	/// <code>
	/// SELECT [IsDaylightSavingTime] = clr.xfnIsDaylightSavingTime('2015-08-03 11:15:22.000')
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">IsDaylightSavingTime</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">True</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns></returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlBoolean xfnIsDaylightSavingTime(SqlDateTime date)
	{
		if (date.IsNull)
		{
			return SqlBoolean.Null;
		}
		return date.Value.IsDaylightSavingTime();
	}

	/// <summary>
	/// Determines whether or not the given date is a business day.
	/// </summary>
	/// <param name="date">The date.</param>
	/// <example>
	/// <code>
	/// SELECT [IsBusinessDay] = clr.xfnIsBusinessDay('2015-08-03 11:15:22.000')
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">IsBusinessDay</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">True</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns></returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlBoolean xfnIsBusinessDay(SqlDateTime date)
	{
		if (date.IsNull)
		{
			return SqlBoolean.Null;
		}
		return date.Value.DayOfWeek != DayOfWeek.Saturday && date.Value.DayOfWeek != DayOfWeek.Sunday;
	}

	/// <summary>
	/// Gets the count of business days between the <paramref name="start"/> and <paramref name="end"/> dates.
	/// </summary>
	/// <param name="start">The start.</param>
	/// <param name="end">The end.</param>
	/// <example>
	/// <code>
	/// SELECT [GetBusinessDays] = clr.xfnGetBusinessDays('2015-07-03 11:15:22.000', '2015-08-03 11:15:22.000')
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">GetBusinessDays</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">22</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns></returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static int xfnGetBusinessDays(DateTime start, DateTime end)
	{
		//https://alecpojidaev.wordpress.com/2009/10/29/work-days-calculation-with-c/
		double calcBusinessDays = 1 + ((end - start).TotalDays * 5 - (start.DayOfWeek - end.DayOfWeek) * 2) / 7;

		if (end.DayOfWeek == DayOfWeek.Saturday) calcBusinessDays--;
		if (start.DayOfWeek == DayOfWeek.Sunday) calcBusinessDays--;

		return (int)calcBusinessDays;
	}



	#region Quarters

	/// <summary>
	/// Enum for quarters.
	/// </summary>
	public enum Quarter
	{
		/// <summary>
		/// The first quarter.
		/// </summary>
		First = 1,
		/// <summary>
		/// The second quarter.
		/// </summary>
		Second = 2,
		/// <summary>
		/// The third quarter.
		/// </summary>
		Third = 3,
		/// <summary>
		/// The fourth quarter.
		/// </summary>
		Fourth = 4
	}

	/// <summary>
	/// Enum for moths.
	/// </summary>
	public enum Month
	{
		/// <summary>
		/// January
		/// </summary>
		January = 1,
		/// <summary>
		/// February
		/// </summary>
		February = 2,
		/// <summary>
		/// march
		/// </summary>
		March = 3,
		/// <summary>
		/// April
		/// </summary>
		April = 4,
		/// <summary>
		/// may
		/// </summary>
		May = 5,
		/// <summary>
		/// June
		/// </summary>
		June = 6,
		/// <summary>
		/// July
		/// </summary>
		July = 7,
		/// <summary>
		/// august
		/// </summary>
		August = 8,
		/// <summary>
		/// September
		/// </summary>
		September = 9,
		/// <summary>
		/// October
		/// </summary>
		October = 10,
		/// <summary>
		/// November
		/// </summary>
		November = 11,
		/// <summary>
		/// December
		/// </summary>
		December = 12
	}

	/// <summary>
	/// Gets the start of quarter for the given date and the <paramref name="quarter"/>.
	/// </summary>
	/// <param name="date">The date.</param>
	/// <param name="quarter">The quarter. Valid values are 1-4.</param>
	/// <example>
	/// <code>
	/// SELECT [StartOfQuarter1] = clr.xfnStartOfQuarter('2015-08-03 11:15:22.000', 1),
	///     [StartOfQuarter2] = clr.xfnStartOfQuarter('2015-08-03 11:15:22.000', 2),
	///     [StartOfQuarter3] = clr.xfnStartOfQuarter('2015-08-03 11:15:22.000', 3),
	///     [StartOfQuarter4] = clr.xfnStartOfQuarter('2015-08-03 11:15:22.000', 4)
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">StartOfQuarter1</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">StartOfQuarter2</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">StartOfQuarter3</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">StartOfQuarter4</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">1/1/2015 12:00:00 AM</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">4/1/2015 12:00:00 AM</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">7/1/2015 12:00:00 AM</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">10/1/2015 12:00:00 AM</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns></returns>
	/// <exception cref="ArgumentOutOfRangeException">Quarter must an int from 1 to 4.</exception>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlDateTime xfnStartOfQuarter(SqlDateTime date, int quarter)
	{
		if (date.IsNull)
		{
			return SqlDateTime.Null;
		}
		if (!Enum.IsDefined(typeof(Quarter), quarter))
		{
			throw new ArgumentOutOfRangeException("Quarter must an int from 1 to 4.");
		}

		Quarter Qtr = (Quarter)quarter;
		int Year = date.Value.Year;

		if (Qtr == Quarter.First)
		{
			return new DateTime(Year, 1, 1);
		}
		else if (Qtr == Quarter.Second)
		{
			return new DateTime(Year, 4, 1);
		}
		else if (Qtr == Quarter.Third)
		{
			return new DateTime(Year, 7, 1);
		}
		else
		{
			return new DateTime(Year, 10, 1);
		}
	}

	/// <summary>
	/// Gets the end of quarter for the given date and the <paramref name="quarter"/>.
	/// </summary>
	/// <param name="date">The date.</param>
	/// <param name="quarter">The quarter. Valid values are 1-4.</param>
	/// <example>
	/// <code>
	/// SELECT [EndOfQuarter1] = clr.xfnEndOfQuarter('2015-08-03 11:15:22.000', 1),
	///     [EndOfQuarter2] = clr.xfnEndOfQuarter('2015-08-03 11:15:22.000', 2),
	///     [EndOfQuarter3] = clr.xfnEndOfQuarter('2015-08-03 11:15:22.000', 3),
	///     [EndOfQuarter4] = clr.xfnEndOfQuarter('2015-08-03 11:15:22.000', 4)
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">EndOfQuarter1</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">EndOfQuarter2</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">EndOfQuarter3</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">EndOfQuarter4</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">4/1/2015 12:00:00 AM</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">7/1/2015 12:00:00 AM</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">10/1/2015 12:00:00 AM</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">1/1/2016 12:00:00 AM</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns></returns>
	/// <exception cref="ArgumentOutOfRangeException">Quarter must an int from 1 to 4.</exception>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlDateTime xfnEndOfQuarter(SqlDateTime date, int quarter)
	{
		if (date.IsNull)
		{
			return SqlDateTime.Null;
		}
		if (!Enum.IsDefined(typeof(Quarter), quarter))
		{
			throw new ArgumentOutOfRangeException("Quarter must an int from 1 to 4.");
		}

		Quarter Qtr = (Quarter)quarter;
		int Year = date.Value.Year;

		if (Qtr == Quarter.First)
		{
			return xfnEndOfDay(new DateTime(Year, 3, DateTime.DaysInMonth(Year, 3)));
		}
		else if (Qtr == Quarter.Second)
		{
			return xfnEndOfDay(new DateTime(Year, 6, DateTime.DaysInMonth(Year, 6)));
		}
		else if (Qtr == Quarter.Third)
		{
			return xfnEndOfDay(new DateTime(Year, 9, DateTime.DaysInMonth(Year, 9)));
		}
		else
		{
			return xfnEndOfDay(new DateTime(Year, 12, DateTime.DaysInMonth(Year, 12)));
		}
	}

	/// <summary>
	/// Returns what quarter the specific month number falls under.
	/// </summary>
	/// <param name="month">The month.</param>
	/// <example>
	/// <code>
	/// SELECT [GetQuarter] = clr.xfnGetQuarter(9) -- 9 == September
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">GetQuarter</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">3</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns></returns>
	/// <exception cref="ArgumentOutOfRangeException">Month must an integer from 1 to 12.</exception>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static int xfnGetQuarter(int month)
	{
		if (!Enum.IsDefined(typeof(Month), month))
		{
			throw new ArgumentOutOfRangeException("Month must an integer from 1 to 12.");
		}

		Month mth = (Month)month;

		if (mth <= Month.March)
		{
			return (int)Quarter.First;
		}
		else if ((mth >= Month.April) && (mth <= Month.June))
		{
			return (int)Quarter.Second;
		}
		else if ((mth >= Month.July) && (mth <= Month.September))
		{
			return (int)Quarter.Third;
		}
		else
		{
			return (int)Quarter.Fourth;
		}
	}

	/// <summary>
	/// Gets the start of current quarter for the current date.
	/// </summary>
	/// <example>
	/// <code>
	/// SELECT clr.xfnStartOfCurrentQuarter()
	/// </code>
	/// </example>
	/// <returns>The start of the quarter that the current date falls under.</returns>
	[SqlFunction(IsDeterministic = false, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlDateTime xfnStartOfCurrentQuarter()
	{
		return xfnStartOfQuarter(DateTime.Now, xfnGetQuarter(DateTime.Now.Month));
	}

	/// <summary>
	/// Gets the end of current quarter for the current date.
	/// </summary>
	/// <example>
	/// <code>
	/// SELECT clr.xfnEndOfCurrentQuarter()
	/// </code>
	/// </example>
	/// <returns>The end of the quarter that the current date falls under.</returns>
	[SqlFunction(IsDeterministic = false, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlDateTime xfnEndOfCurrentQuarter()
	{
		return xfnEndOfQuarter(DateTime.Now, xfnGetQuarter(DateTime.Now.Month));
	}
	#endregion


}
