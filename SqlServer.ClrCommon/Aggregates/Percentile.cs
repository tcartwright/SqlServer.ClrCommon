using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using Microsoft.SqlServer.Server;


/// <summary>
/// Percentile finds the number under which X percent of the values fall. Please look at the <see cref="Accumulate"/> method for an example.
/// </summary>
[Serializable]
[SqlUserDefinedAggregate(
	Format.UserDefined,
	IsInvariantToDuplicates = false,
	IsInvariantToNulls = true,
	IsNullIfEmpty = true,
	IsInvariantToOrder = true,
	MaxByteSize = -1
)]
public struct xagPercentile : IBinarySerialize
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
	/// Accumulates the specified value to determine the data that matches the percentile.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <param name="percentile">The percentile.</param>
	/// <example>
	/// <code>
	/// SET NOCOUNT ON
	/// 
	/// IF OBJECT_ID('tempdb..#AggregatesTest_TestData') IS NOT NULL
	/// 	DROP TABLE dbo.#AggregatesTest_TestData
	/// 
	/// CREATE TABLE dbo.#AggregatesTest_TestData (
	/// 	RowId int NOT NULL IDENTITY PRIMARY KEY,
	/// 	FirstName varchar(255) NOT NULL,
	/// 	LastName varchar(255) NOT NULL,
	/// 	[Age] int NOT NULL 
	/// )
	/// 
	/// INSERT INTO dbo.#AggregatesTest_TestData VALUES
	/// ('Hunter','Ramirez','58'), ('Mason','Evans','62'), ('Caden','Clark','47'), ('Andrew','Clark','21'), ('Owen','Adams','33'), ('Dominic','Martin','49'), ('Jackson','Nelson','29'), ('Nathan','Perez','28'), 
	/// ('Jayden','Williams','33'), ('Elijah','Miller','25'), ('Connor','Thomas','59'), ('Oliver','Phillips','22'), ('William','Thompson','26'), ('Aiden','Ramirez','18'), ('Dominic','Young','26'), ('David','Scott','45'), 
	/// ('Connor','Phillips','37'), ('Caleb','Carter','58'), ('Caleb','Robinson','30'), ('Mason','Davis','28'), ('James','Green','21'), ('Jayden','Wilson','24'), ('Wyatt','Walker','24'), ('Caden','Gonzalez','38'), 
	/// ('Dominic','Thompson','29'), ('Caden','King','58'), ('Isaac','Perez','54'), ('Carter','Phillips','56'), ('Ethan','Sanchez','37'), ('Lucas','Martin','19'), ('James','Harris','19'), ('Henry','Martin','34'), 
	/// ('Gavin','Gonzalez','27'), ('Nathan','Mitchell','43'), ('Caden','Phillips','40'), ('Alexander','Davis','24'), ('Luke','Adams','22'), ('Brayden','Taylor','34'), ('Ethan','Miller','49'), ('Isaac','Phillips','20'), 
	/// ('Gavin','Ramirez','60'), ('Samuel','Phillips','33'), ('Carter','Martin','31'), ('Ryan','Johnson','57'), ('Samuel','Adams','39'), ('David','Sanchez','63'), ('Liam','Robinson','42'), ('Dylan','Jones','36'), 
	/// ('Jayce','Smith','50'), ('Gabriel','Lee','40'), ('Benjamin','Wright','27'), ('Evan','Lewis','32'), ('Henry','Hernandez','25'), ('James','Garcia','28'), ('Hunter','Hall','60'), ('Benjamin','Smith','27'), 
	/// ('David','Martin','49'), ('Dominic','Thomas','19'), ('David','Nelson','23'), ('Nicholas','Thomas','64'), ('Michael','Lee','21'), ('Landon','Lopez','60'), ('Liam','Wright','27'), ('Nathan','Ramirez','55'), 
	/// ('Noah','White','31'), ('Aiden','Thomas','33'), ('Ethan','Harris','31'), ('Jack','Baker','24'), ('Gabriel','Williams','60'), ('William','Anderson','60'), ('Liam','Nelson','24'), ('Oliver','Lee','37'), 
	/// ('Isaac','Harris','44'), ('Lucas','Evans','19'), ('Luke','Brown','23'), ('Mason','Davis','42'), ('Jackson','Taylor','64'), ('Caleb','Rodriguez','28'), ('Connor','Young','26'), ('Oliver','Martinez','42'),
	/// ('Jayce','Rodriguez','35'), ('Grayson','Martinez','36'), ('Ryan','Green','42'), ('Evan','Gonzalez','48'), ('Owen','Davis','62'), ('Gabriel','Hernandez','34'), ('Connor','Taylor','62'), ('Oliver','Jones','51'), 
	/// ('Joshua','Lewis','19'), ('Isaac','Thompson','56'), ('Daniel','Thompson','42'), ('Eli','Robinson','20'), ('Michael','Thompson','40'), ('Henry','Taylor','38'), ('James','Moore','40'), ('Grayson','Thomas','27'), 
	/// ('William','Campbell','20'), ('Connor','King','52'), ('Caleb','Carter','27'), ('Benjamin','Adams','52'), ('Liam','Gonzalez','50'), ('Elijah','Walker','41'), ('Ryan','King','41'), ('Dominic','Hill','31'), 
	/// ('Michael','Miller','59'), ('Alexander','Smith','28'), ('Andrew','Wilson','25'), ('Samuel','Robinson','33'), ('Oliver','Taylor','37'), ('Eli','Wilson','20'), ('Jacob','Johnson','19'), ('Nathan','Robinson','23'), 
	/// ('David','Hernandez','59'), ('Landon','Ramirez','59'), ('Samuel','Lopez','46'), ('Sebastian','Phillips','58'), ('Lucas','Martinez','47'), ('Alexander','Johnson','41'), ('Gabriel','Thompson','40'), 
	/// ('Gabriel','Adams','49'), ('Owen','Jackson','45'), ('Nicholas','Wilson','58'), ('Dominic','Thompson','49'), ('Grayson','Mitchell','21'), ('Aiden','Hill','61'), ('Landon','Johnson','50'), ('Liam','Green','33'), 
	/// ('David','Lee','46'), ('Jayce','Evans','36'), ('Brayden','Williams','49'), ('Brayden','Martin','61'), ('Jack','Green','28'), ('William','King','21'), ('Nicholas','Martin','54'), ('Eli','Evans','57'), ('David','Gonzalez','22'), 
	/// ('Matthew','Robinson','21'), ('Dominic','Martinez','58'), ('Jayce','Harris','39'), ('Ryan','Hill','60'), ('Liam','Lopez','50'), ('William','Jackson','23'), ('Wyatt','Taylor','36'), ('Nicholas','Williams','51')
	/// 
	/// SELECT t.LastName, clr.xagPercentile(t.Age, 80)
	/// FROM dbo.#AggregatesTest_TestData t
	/// GROUP BY t.LastName
	/// ORDER BY t.LastName
	/// 
	/// IF OBJECT_ID('tempdb..#AggregatesTest_TestData') IS NOT NULL
	/// 	DROP TABLE dbo.#AggregatesTest_TestData
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">LastName</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">80th Percentile</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Adams</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">52</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Anderson</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">60</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Baker</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">24</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Brown</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">23</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Campbell</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">20</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Carter</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">58</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Clark</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">47</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Davis</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">62</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Evans</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">62</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Garcia</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">28</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Gonzalez</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">50</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Green</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">42</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Hall</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">60</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Harris</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">44</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Hernandez</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">59</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Hill</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">61</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Jackson</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">45</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Johnson</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">57</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Jones</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">51</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">King</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">58</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Lee</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">46</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Lewis</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">32</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Lopez</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">60</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Martin</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">61</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Martinez</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">58</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Miller</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">59</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Mitchell</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">43</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Moore</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">40</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Nelson</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">29</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Perez</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">54</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Phillips</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">58</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Ramirez</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">60</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Robinson</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">42</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Rodriguez</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">35</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Sanchez</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">63</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Scott</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">45</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Smith</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">50</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Taylor</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">64</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Thomas</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">64</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Thompson</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">56</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Walker</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">41</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">White</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">31</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Williams</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">60</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Wilson</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">58</td>
	/// 		</tr>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Wright</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">27</td>
	/// 		</tr>
	/// 		<tr style="background-color:White;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Young</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">26</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	public void Accumulate(SqlDouble value, SqlInt16 percentile)
	{
		// Put your code here
		if (value.IsNull != true)
		{
			doubleList.Add(value.Value);
		}

		if (percentile.IsNull != true)
		{
			if (percentile.Value > 100)
			{
				this.percentile = 100;
			}
			else
			{
				this.percentile = percentile.Value;
			}
		}
	}

	/// <summary>
	/// Merges the specified group.
	/// </summary>
	/// <param name="Group">The group.</param>
	public void Merge(xagPercentile Group)
	{
		// Put your code here
		List<double> secondDoubleList = Group.DoubleList;
		foreach (double d in secondDoubleList)
		{
			doubleList.Add(d);
		}

		this.percentile = Group.PercentileValue;
	}

	/// <summary>
	/// Terminates this instance.
	/// </summary>
	/// <returns></returns>
	public SqlDouble Terminate()
	{
		// Put your code here
		doubleList.Sort();
		double percentileResult;
		double indexToParse;
		int indexToRetrieve;

		indexToParse = this.percentile / 100.0 * doubleList.Count + 0.50;

		if (indexToParse < 0)
		{
			indexToParse = 0;
		}

		indexToRetrieve = Convert.ToInt32(indexToParse);
		if (indexToRetrieve >= doubleList.Count)
		{
			indexToRetrieve = doubleList.Count - 1;
		}

		percentileResult = doubleList[indexToRetrieve];


		return new SqlDouble(percentileResult);
	}


	/*
	 * The binary layout is as follows:
	 * The first 2 bytes are the percentile
	 * * Each 8 bytes is a double
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

		this.percentile = binaryReader.ReadInt16();

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
		binaryWriter.Write(PercentileValue);
		foreach (double d in doubleList)
		{
			binaryWriter.Write(d);
		}
	}

	// This is a place-holder member field
	private List<double> doubleList;
	private short percentile;

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

	/// <summary>
	/// Gets the percentile value.
	/// </summary>
	/// <value>
	/// The percentile value.
	/// </value>
	public short PercentileValue
	{
		get
		{
			return this.percentile;
		}
	}

}
