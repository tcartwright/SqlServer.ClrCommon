using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{

	/// <summary>
	/// Uses the Luhn formula to validate a credit card number.
	/// </summary>
	/// <param name="cardNumber">The credit card number.</param>
	/// <example>
	/// <code>
	/// SELECT [clr].[xfnLuhnCardValidate]('378282246310005') 		--American Express					
	///		,[clr].[xfnLuhnCardValidate]('378734493671000') 		--American Express Corporate	
	///		,[clr].[xfnLuhnCardValidate]('5610591081018250') 		--Australian BankCard				
	///		,[clr].[xfnLuhnCardValidate]('30569309025904') 			--Diners Club							
	///		,[clr].[xfnLuhnCardValidate]('6011111111111117') 		--Discover								
	///		,[clr].[xfnLuhnCardValidate]('5555555555554444') 		--MasterCard							
	///		,[clr].[xfnLuhnCardValidate]('4012888888881881') 		--Visa		
	///		
	///	--negatives
	///	SELECT [clr].[xfnLuhnCardValidate]('38282246310005') 		
	///		,[clr].[xfnLuhnCardValidate]('371449635398430') 		
	///		,[clr].[xfnLuhnCardValidate]('1111') 		
	///		,[clr].[xfnLuhnCardValidate]('') 		
	///		,[clr].[xfnLuhnCardValidate](null) 			
	///		,[clr].[xfnLuhnCardValidate]('2222222222222222')
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results Set 1:</span>
	///	<table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	///		<thead><tr>
	///			<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Column1</th>
	///			<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Column2</th>
	///			<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Column3</th>
	///			<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Column4</th>
	///			<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Column5</th>
	///			<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Column6</th>
	///			<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Column7</th>
	///		</tr></thead>
	///		<tbody>
	///			<tr style="background-color:#DDDDDD;font-size:12;">
	///				<td style="font-family: Tahoma;font-size;10;">True</td>
	///				<td style="font-family: Tahoma;font-size;10;">True</td>
	///				<td style="font-family: Tahoma;font-size;10;">True</td>
	///				<td style="font-family: Tahoma;font-size;10;">True</td>
	///				<td style="font-family: Tahoma;font-size;10;">True</td>
	///				<td style="font-family: Tahoma;font-size;10;">True</td>
	///				<td style="font-family: Tahoma;font-size;10;">True</td>
	///			</tr>
	///		</tbody>
	///	</table>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results Set 2:</span>
	///	<table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	///		<thead><tr>
	///			<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Column1</th>
	///			<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Column2</th>
	///			<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Column3</th>
	///			<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Column4</th>
	///			<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Column5</th>
	///			<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Column6</th>
	///		</tr></thead>
	///		<tbody>
	///			<tr style="background-color:#DDDDDD;font-size:12;">
	///				<td style="font-family: Tahoma;font-size;10;">False</td>
	///				<td style="font-family: Tahoma;font-size;10;">False</td>
	///				<td style="font-family: Tahoma;font-size;10;">False</td>
	///				<td style="font-family: Tahoma;font-size;10;">False</td>
	///				<td style="font-family: Tahoma;font-size;10;">False</td>
	///				<td style="font-family: Tahoma;font-size;10;">False</td>
	///			</tr>
	///		</tbody>
	///	</table>
	/// </example>
	/// <returns>True if the card number passes the Luhn formula validation.</returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlBoolean xfnLuhnCardValidate([SqlFacet(MaxSize = 64)] SqlString cardNumber)
	{
		if (cardNumber.IsNull) { return false; }
		string card = new String(cardNumber.Value.Where(ch => Char.IsDigit(ch)).ToArray());

		if (String.IsNullOrEmpty(card)) { return false; }

		int checksum = card
		   .Select((c, i) => (c - '0') << ((card.Length - i - 1) & 1))
		   .Sum(n => n > 9 ? n - 9 : n);

		return (checksum % 10) == 0 && checksum > 0;
	}

	/// <summary>
	/// Returns a random integer that is within a specified range.
	/// </summary>
	/// <param name="seed">The seed controls the generation of the random. Pass in null to have the seed generated for you.</param>
	/// <param name="lower">The lower.</param>
	/// <param name="upper">The upper.</param>
	/// <example>
	/// <code>
	/// SELECT [Rand1] = clr.xfnRandom(null, 1, 100), -- generate a random between 1 and 100
	///		[Rand2] = clr.xfnRandom(11, 1, 100) -- control the seed, as the seed is not random, the result will always be 47
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Rand1</th>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Rand2</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">## Random Value</td>
	/// 			<td style="font-family: Tahoma;font-size;10;">47</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns>A 32-bit signed integer greater than or equal to minValue and less than maxValue; that is, the range of return values includes minValue but not maxValue. If minValue equals maxValue, minValue is returned.</returns>
	/// <exception cref="SqlTypeException">Lower must be less than Upper.</exception>
	[SqlFunction(IsDeterministic = false, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlInt32 xfnRandom(SqlInt32 seed, SqlInt32 lower, SqlInt32 upper)
	{
		if (lower.Value > upper.Value)
		{
			throw new SqlTypeException("Lower must be less than Upper.");
		}
		if (seed.IsNull)
		{
			return new Random(Guid.NewGuid().GetHashCode()).Next(lower.Value, upper.Value);
		}
		return new Random(seed.Value).Next(lower.Value, upper.Value);
	}

	/// <summary>
	/// Returns the dot net framework version of this assembly. For informational purposes only.
	/// </summary>
	/// <returns></returns>
	[return: SqlFacet(MaxSize = 256)]
	[SqlFunction(IsDeterministic = false, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlString xfnAssemblyDotNetVersion()
	{
		return new SqlString(System.Reflection.Assembly.GetAssembly(typeof(UserDefinedFunctions)).ImageRuntimeVersion);
	}

#if ALLOW_GZIP // without this variable defined, this effectively remove gzip from the build. 
	/// <summary>
	/// Writes compressed bytes to the underlying stream from the specified string.
	/// </summary>
	/// <param name="data">The data.</param>
	/// <returns></returns>
	/// <example>
	/// <code>
	/// SELECT [Results] = clr.xfnGZip(convert(varbinary(max), 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum laoreet, enim sed venenatis elementum, velit neque fermentum nulla, sit amet eleifend libero sapien eu erat. Etiam blandit ullamcorper justo ultrices ullamcorper. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nulla convallis tortor id varius mattis. Phasellus aliquam feugiat lectus et maximus. Suspendisse non urna erat. Suspendisse potenti. Ut rutrum tortor non neque suscipit commodo. Mauris scelerisque nulla eu pharetra tempor. Suspendisse et consectetur felis. Donec tincidunt, purus ut volutpat placerat, lacus erat accumsan nibh, non fermentum dolor ante in sapien. Duis eget orci ligula.'))
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">0x1F8B0800000000000400EDBD07601C499625262F6DCA7B7F4AF54AD7E074A10880601324D8904010ECC188CDE692EC1D69472329AB2A81CA6556655D661640CCED9DBCF7DE7BEFBDF7DE7BEFBDF7BA3B9D4E27F7DFFF3F5C6664016CF6CE4ADAC99E2180AAC81F3F7E7C1F3F229E5775BE488B55B35EA4B3AAACEAB429DA345BE4ED289D56CB269FB679BBAED36C56AC8A665A2C2FD2BC2CDA71FA9379D316937549EF951901C10BF9B258A44D3E4B2FF365BECCDAA2A1C6F9225FB6EBC5883EA417D365FE8BD6797A9ED7F271BA5C976536B2BDE285E23C5FCED2B298E4759536D9AAC89769BE4EF33AA37E4FDB225BA493325BCEE815BCBC9856F52AAFD39F5E376D459FB47531CD1BFFAB717A52664D9366AB963A4DDB6C5AB445DA54D3A26A7ED19AC6469DB5559DA5F40F61474D008F86BF9E1459BAAC9AB62614F159B19CE6ABB66AD27941F86779D58CD317E809AD2FB3B2A4211310FA5F5A1019B2BA5837E9226B8914E3F4E53C6BF2B2A44FB2B2F8456B1AC679BEBE28B2362D89CCF4310D7F91BD2B166B6AFC7ADDAC880C45D3E484C1325DD7CB4C49E07FB5AA30A4629C7ED5A6F5BAAD89A2DA3F5E1262376B9AB815516B5A2D16D5AC1AA75F64EB9A306DA6446DFA058D781E40E6D53CABF316C4C817ABAA0EBBCB01C471C5394D29E1FAB45AE6D3B425E214B3F5921861B5AE6938EB36BDACCA75BBA211AECA6C0AEC47C42D530C957E4FB3E974BD6832C2B398CC478CB1630CE1C66CD9E644756503EA6A0DA6BA203CAA7A5AD0BC5DACCB6CFCFF0019CE3029C6020000</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	[SqlFunction(SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlBytes xfnGZip(SqlBytes data)
	{
		if (data.Length == 0L)
		{
			return SqlBytes.Null;
		}
		byte[] raw = data.Value;
		MemoryStream memory = null;

		using (memory = new MemoryStream())
		using (GZipStream gzip = new GZipStream(memory, CompressionMode.Compress, true))
		{
			gzip.Write(raw, 0, raw.Length);
		}
		return new SqlBytes(memory.ToArray());
	}
	/// <summary>
	/// Converts a zipped set of binary bytes back into a string
	/// </summary>
	/// <param name="data">The data.</param>
	/// <returns></returns>
	/// <example>
	/// <code>
	/// SELECT [Results] = convert(varchar(max), clr.xfnGUnzip(0x1F8B080000000000040055526D8A1B310CBD8A0E30CC25B6FDD72E85D2FE573C4AA2625B8E25853D7E9F37A1DD856130B2E4F7A1F7CDA634D2E1D9E8B06A935C83B8496C54ACBB9490C8497CE8502FDA2F245563A7DFE2A1A7AC98AB8C47D680746DE472D05DBA740E75344B931ED9361431485D6E297496F92853CF5A79FB87BA06F42CFDA0AA279946CE43A59324C964E07E0DE546A7CAFDC0C81A6EC5E690497FD2C35089A945FCE3D54E2F95DD89470094828B86925B51F35B421BC0C226137E608796F51EE4E74999BA794C505C35ED454698D355C19FC57CA7D785B4BAEF5C2B24E3117CA4B081A7A653E380153BFDB8B24BADA870D55B42C659F2A21C546133CA90DFF84D5BA2F967FA800DEA2E60D02967E7A7051FAF862D49BAD3AFA09931E1E8137F0D3DCCF6C4E206DC2AD69A1DB6D377CE09A65EE0360EABE97D0FCBE671E529B1CC90366C7E8693F8948A33560AAE5FAC4BA180397A644710464EC8C9A0BBD58C0185A37259EC37A4A52CA938139792CD193CF574DDDE19FF0FC6238DDC43E0FA330680CA15AA0B78D82C8ABD5DB2F2FE1719CE3029C6020000))
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum laoreet, enim sed venenatis elementum, velit neque fermentum nulla, sit amet eleifend libero sapien eu erat. Etiam blandit ullamcorper justo ultrices ullamcorper. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nulla convallis tortor id varius mattis. Phasellus aliquam feugiat lectus et maximus. Suspendisse non urna erat. Suspendisse potenti. Ut rutrum tortor non neque suscipit commodo. Mauris scelerisque nulla eu pharetra tempor. Suspendisse et consectetur felis. Donec tincidunt, purus ut volutpat placerat, lacus erat accumsan nibh, non fermentum dolor ante in sapien. Duis eget orci ligula.</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	[SqlFunction(SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlBytes xfnGUnzip(SqlBytes data)
	{
		SqlBytes result;
		using (MemoryStream memoryStream = new MemoryStream(data.Value))
		using (GZipStream gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
		using (MemoryStream memoryStream2 = new MemoryStream())
		{
			for (int num = gZipStream.ReadByte(); num != -1; num = gZipStream.ReadByte())
			{
				memoryStream2.WriteByte((byte)num);
			}
			result = new SqlBytes(memoryStream2.ToArray());
		}
		return result;
	}
#endif

	/// <summary>
	/// Returns a Boolean value indicating whether an expression can be evaluated as a number.
	/// </summary>
	/// <param name="numericString">The string to evaluate.</param>
	/// <example>
	/// <code>
	/// ;WITH Tally (n) AS
	/// (
	/// 	SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL))
	/// 	FROM (	 VALUES(0),(0),(0),(0),(0),(0),(0),(0),(0),(0)) a(n)		--10 rows
	/// 	CROSS JOIN (VALUES(0),(0),(0),(0),(0),(0),(0),(0),(0),(0)) b(n)		--100
	/// 	CROSS JOIN (VALUES(0),(0),(0),(0),(0)) c(n)							--500
	/// )
	/// 
	/// SELECT	[Ascii Code] = STR(t.n),
	/// 		[Ascii Character] = CHAR(t.n),
	/// 		[ISNUMERIC Returns] = ISNUMERIC(CHAR(t.n)),
	/// 		[XFNISNUMERIC Returns] = clr.xfnIsNumeric(CHAR(t.n)) 
	/// FROM Tally t 
	/// WHERE ISNUMERIC(CHAR(t.n)) = 1
	/// </code>
	/// </example>
	/// <remarks>
	///	The built-in IsNumeric has issues. Please read <a href='http://www.sqlservercentral.com/articles/ISNUMERIC()/71512/'> Why doesn’t SQL SERVER ISNUMERIC work correctly?</a>
	/// </remarks>
	/// <returns>
	/// <div>IsNumeric returns True if the data type of Expression is Boolean, Byte, Decimal, Double, Integer, Long, SByte, Short, Single, 
	/// UInteger, ULong, or UShort, or an Object that contains one of those numeric types. It also returns True if Expression is a Char or 
	/// String that can be successfully converted to a number.
	/// </div><br />
	/// <div>
	/// IsNumeric returns False if Expression is of data type Date or of data type Object and it does not contain a numeric type. 
	/// IsNumeric returns False if Expression is a Char or String that cannot be converted to a number.
	/// </div>
	/// </returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static bool xfnIsNumeric(
		[SqlFacet(MaxSize = 60)]
		string numericString
	)
	{
		if (SqlServer.ClrCommon.Utils.IsNullOrWhiteSpace(numericString))
		{
			return false;
		}

		double tmp;
		return double.TryParse(numericString, out tmp);
	}

	/// <summary>
	/// Converts a string to its equivalent string representation that is encoded with base-64 digits.
	/// </summary>
	/// <param name="unencodedValue">The unencoded value.</param>
	/// <example>
	/// <code>
	/// SELECT [Results] = clr.xfnToBase64(convert(varbinary(max), 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum laoreet, enim sed venenatis elementum, velit neque fermentum nulla, sit amet eleifend libero sapien eu erat. Etiam blandit ullamcorper justo ultrices ullamcorper. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nulla convallis tortor id varius mattis. Phasellus aliquam feugiat lectus et maximus. Suspendisse non urna erat. Suspendisse potenti. Ut rutrum tortor non neque suscipit commodo. Mauris scelerisque nulla eu pharetra tempor. Suspendisse et consectetur felis. Donec tincidunt, purus ut volutpat placerat, lacus erat accumsan nibh, non fermentum dolor ante in sapien. Duis eget orci ligula. Sed eu velit odio. Aliquam eu erat est. Sed interdum augue vel elit molestie, at euismod dui pharetra. Cras hendrerit rhoncus congue. Curabitur vel commodo nisl, ac egestas urna. Quisque quis ante quis turpis posuere finibus vitae in magna. Nulla rhoncus, purus ut vehicula scelerisque, velit nulla pharetra ante, ut blandit erat sem in odio. Ut nec nisl laoreet, finibus nulla non, consectetur velit. Nam mollis turpis vel congue tincidunt. Proin elementum luctus sapien, vitae sodales felis. Sed maximus, ligula a condimentum congue, metus nulla imperdiet dui, vel facilisis justo tellus vel elit. Nulla elit risus, maximus nec mollis vitae, aliquam nec metus.'))
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">TG9yZW0gaXBzdW0gZG9sb3Igc2l0IGFtZXQsIGNvbnNlY3RldHVyIGFkaXBpc2NpbmcgZWxpdC4gVmVzdGlidWx1bSBsYW9yZWV0LCBlbmltIHNlZCB2ZW5lbmF0aXMgZWxlbWVudHVtLCB2ZWxpdCBuZXF1ZSBmZXJtZW50dW0gbnVsbGEsIHNpdCBhbWV0IGVsZWlmZW5kIGxpYmVybyBzYXBpZW4gZXUgZXJhdC4gRXRpYW0gYmxhbmRpdCB1bGxhbWNvcnBlciBqdXN0byB1bHRyaWNlcyB1bGxhbWNvcnBlci4gQ2xhc3MgYXB0ZW50IHRhY2l0aSBzb2Npb3NxdSBhZCBsaXRvcmEgdG9ycXVlbnQgcGVyIGNvbnViaWEgbm9zdHJhLCBwZXIgaW5jZXB0b3MgaGltZW5hZW9zLiBOdWxsYSBjb252YWxsaXMgdG9ydG9yIGlkIHZhcml1cyBtYXR0aXMuIFBoYXNlbGx1cyBhbGlxdWFtIGZldWdpYXQgbGVjdHVzIGV0IG1heGltdXMuIFN1c3BlbmRpc3NlIG5vbiB1cm5hIGVyYXQuIFN1c3BlbmRpc3NlIHBvdGVudGkuIFV0IHJ1dHJ1bSB0b3J0b3Igbm9uIG5lcXVlIHN1c2NpcGl0IGNvbW1vZG8uIE1hdXJpcyBzY2VsZXJpc3F1ZSBudWxsYSBldSBwaGFyZXRyYSB0ZW1wb3IuIFN1c3BlbmRpc3NlIGV0IGNvbnNlY3RldHVyIGZlbGlzLiBEb25lYyB0aW5jaWR1bnQsIHB1cnVzIHV0IHZvbHV0cGF0IHBsYWNlcmF0LCBsYWN1cyBlcmF0IGFjY3Vtc2FuIG5pYmgsIG5vbiBmZXJtZW50dW0gZG9sb3IgYW50ZSBpbiBzYXBpZW4uIER1aXMgZWdldCBvcmNpIGxpZ3VsYS4gU2VkIGV1IHZlbGl0IG9kaW8uIEFsaXF1YW0gZXUgZXJhdCBlc3QuIFNlZCBpbnRlcmR1bSBhdWd1ZSB2ZWwgZWxpdCBtb2xlc3RpZSwgYXQgZXVpc21vZCBkdWkgcGhhcmV0cmEuIENyYXMgaGVuZHJlcml0IHJob25jdXMgY29uZ3VlLiBDdXJhYml0dXIgdmVsIGNvbW1vZG8gbmlzbCwgYWMgZWdlc3RhcyB1cm5hLiBRdWlzcXVlIHF1aXMgYW50ZSBxdWlzIHR1cnBpcyBwb3N1ZXJlIGZpbmlidXMgdml0YWUgaW4gbWFnbmEuIE51bGxhIHJob25jdXMsIHB1cnVzIHV0IHZlaGljdWxhIHNjZWxlcmlzcXVlLCB2ZWxpdCBudWxsYSBwaGFyZXRyYSBhbnRlLCB1dCBibGFuZGl0IGVyYXQgc2VtIGluIG9kaW8uIFV0IG5lYyBuaXNsIGxhb3JlZXQsIGZpbmlidXMgbnVsbGEgbm9uLCBjb25zZWN0ZXR1ciB2ZWxpdC4gTmFtIG1vbGxpcyB0dXJwaXMgdmVsIGNvbmd1ZSB0aW5jaWR1bnQuIFByb2luIGVsZW1lbnR1bSBsdWN0dXMgc2FwaWVuLCB2aXRhZSBzb2RhbGVzIGZlbGlzLiBTZWQgbWF4aW11cywgbGlndWxhIGEgY29uZGltZW50dW0gY29uZ3VlLCBtZXR1cyBudWxsYSBpbXBlcmRpZXQgZHVpLCB2ZWwgZmFjaWxpc2lzIGp1c3RvIHRlbGx1cyB2ZWwgZWxpdC4gTnVsbGEgZWxpdCByaXN1cywgbWF4aW11cyBuZWMgbW9sbGlzIHZpdGFlLCBhbGlxdWFtIG5lYyBtZXR1cy4=</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns>The string representation, in base 64, of the contents of the value.</returns>
	[SqlFunction(IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlChars xfnToBase64(SqlBytes unencodedValue)
	{
		if (unencodedValue.IsNull || unencodedValue.Value.Length == 0)
		{
			return SqlChars.Null;
		}
		return new SqlChars(Convert.ToBase64String(unencodedValue.Value, Base64FormattingOptions.None).ToCharArray());
	}

	/// <summary>
	/// Converts a subset of a Unicode character array, which encodes binary data as base-64 digits, to an equivalent string. 
	/// </summary>
	/// <param name="encodedValue">The encoded value.</param>
	/// <example>
	/// <code>
	/// SELECT [Results] = convert(varchar(max), clr.xfnFromBase64('TG9yZW0gaXBzdW0gZG9sb3Igc2l0IGFtZXQsIGNvbnNlY3RldHVyIGFkaXBpc2NpbmcgZWxpdC4gVmVzdGlidWx1bSBsYW9yZWV0LCBlbmltIHNlZCB2ZW5lbmF0aXMgZWxlbWVudHVtLCB2ZWxpdCBuZXF1ZSBmZXJtZW50dW0gbnVsbGEsIHNpdCBhbWV0IGVsZWlmZW5kIGxpYmVybyBzYXBpZW4gZXUgZXJhdC4gRXRpYW0gYmxhbmRpdCB1bGxhbWNvcnBlciBqdXN0byB1bHRyaWNlcyB1bGxhbWNvcnBlci4gQ2xhc3MgYXB0ZW50IHRhY2l0aSBzb2Npb3NxdSBhZCBsaXRvcmEgdG9ycXVlbnQgcGVyIGNvbnViaWEgbm9zdHJhLCBwZXIgaW5jZXB0b3MgaGltZW5hZW9zLiBOdWxsYSBjb252YWxsaXMgdG9ydG9yIGlkIHZhcml1cyBtYXR0aXMuIFBoYXNlbGx1cyBhbGlxdWFtIGZldWdpYXQgbGVjdHVzIGV0IG1heGltdXMuIFN1c3BlbmRpc3NlIG5vbiB1cm5hIGVyYXQuIFN1c3BlbmRpc3NlIHBvdGVudGkuIFV0IHJ1dHJ1bSB0b3J0b3Igbm9uIG5lcXVlIHN1c2NpcGl0IGNvbW1vZG8uIE1hdXJpcyBzY2VsZXJpc3F1ZSBudWxsYSBldSBwaGFyZXRyYSB0ZW1wb3IuIFN1c3BlbmRpc3NlIGV0IGNvbnNlY3RldHVyIGZlbGlzLiBEb25lYyB0aW5jaWR1bnQsIHB1cnVzIHV0IHZvbHV0cGF0IHBsYWNlcmF0LCBsYWN1cyBlcmF0IGFjY3Vtc2FuIG5pYmgsIG5vbiBmZXJtZW50dW0gZG9sb3IgYW50ZSBpbiBzYXBpZW4uIER1aXMgZWdldCBvcmNpIGxpZ3VsYS4gU2VkIGV1IHZlbGl0IG9kaW8uIEFsaXF1YW0gZXUgZXJhdCBlc3QuIFNlZCBpbnRlcmR1bSBhdWd1ZSB2ZWwgZWxpdCBtb2xlc3RpZSwgYXQgZXVpc21vZCBkdWkgcGhhcmV0cmEuIENyYXMgaGVuZHJlcml0IHJob25jdXMgY29uZ3VlLiBDdXJhYml0dXIgdmVsIGNvbW1vZG8gbmlzbCwgYWMgZWdlc3RhcyB1cm5hLiBRdWlzcXVlIHF1aXMgYW50ZSBxdWlzIHR1cnBpcyBwb3N1ZXJlIGZpbmlidXMgdml0YWUgaW4gbWFnbmEuIE51bGxhIHJob25jdXMsIHB1cnVzIHV0IHZlaGljdWxhIHNjZWxlcmlzcXVlLCB2ZWxpdCBudWxsYSBwaGFyZXRyYSBhbnRlLCB1dCBibGFuZGl0IGVyYXQgc2VtIGluIG9kaW8uIFV0IG5lYyBuaXNsIGxhb3JlZXQsIGZpbmlidXMgbnVsbGEgbm9uLCBjb25zZWN0ZXR1ciB2ZWxpdC4gTmFtIG1vbGxpcyB0dXJwaXMgdmVsIGNvbmd1ZSB0aW5jaWR1bnQuIFByb2luIGVsZW1lbnR1bSBsdWN0dXMgc2FwaWVuLCB2aXRhZSBzb2RhbGVzIGZlbGlzLiBTZWQgbWF4aW11cywgbGlndWxhIGEgY29uZGltZW50dW0gY29uZ3VlLCBtZXR1cyBudWxsYSBpbXBlcmRpZXQgZHVpLCB2ZWwgZmFjaWxpc2lzIGp1c3RvIHRlbGx1cyB2ZWwgZWxpdC4gTnVsbGEgZWxpdCByaXN1cywgbWF4aW11cyBuZWMgbW9sbGlzIHZpdGFlLCBhbGlxdWFtIG5lYyBtZXR1cy4='))
	/// </code>
	/// <br />
	/// <span style='font-weight: bold;font-size;14;'>Results:</span>
	/// <table border="0" style="font-size:10; font-family: Lucida-Console; border-width: 4px; cell-spacing:0px; cell-padding:2px;">
	/// 	<thead><tr>
	/// 		<th style="font-weight:bold;background-color:LightSteelBlue;font-size:14;">Results</th>
	/// 	</tr></thead>
	/// 	<tbody>
	/// 		<tr style="background-color:#DDDDDD;font-size:12;">
	/// 			<td style="font-family: Tahoma;font-size;10;">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum laoreet, enim sed venenatis elementum, velit neque fermentum nulla, sit amet eleifend libero sapien eu erat. Etiam blandit ullamcorper justo ultrices ullamcorper. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nulla convallis tortor id varius mattis. Phasellus aliquam feugiat lectus et maximus. Suspendisse non urna erat. Suspendisse potenti. Ut rutrum tortor non neque suscipit commodo. Mauris scelerisque nulla eu pharetra tempor. Suspendisse et consectetur felis. Donec tincidunt, purus ut volutpat placerat, lacus erat accumsan nibh, non fermentum dolor ante in sapien. Duis eget orci ligula. Sed eu velit odio. Aliquam eu erat est. Sed interdum augue vel elit molestie, at euismod dui pharetra. Cras hendrerit rhoncus congue. Curabitur vel commodo nisl, ac egestas urna. Quisque quis ante quis turpis posuere finibus vitae in magna. Nulla rhoncus, purus ut vehicula scelerisque, velit nulla pharetra ante, ut blandit erat sem in odio. Ut nec nisl laoreet, finibus nulla non, consectetur velit. Nam mollis turpis vel congue tincidunt. Proin elementum luctus sapien, vitae sodales felis. Sed maximus, ligula a condimentum congue, metus nulla imperdiet dui, vel facilisis justo tellus vel elit. Nulla elit risus, maximus nec mollis vitae, aliquam nec metus.</td>
	/// 		</tr>
	/// 	</tbody>
	/// </table>
	/// </example>
	/// <returns>The decoded string as a byte array.</returns>
	[SqlFunction(IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlBytes xfnFromBase64(SqlChars encodedValue)
	{
		if (encodedValue.IsNull || encodedValue.Value.Length == 0)
		{
			return SqlBytes.Null;
		}
		return new SqlBytes(Convert.FromBase64CharArray(encodedValue.Value, 0, encodedValue.Value.Length));
	}

	#region Membership.GeneratePassword
	// TIM C: I pulled this in from System.Web as it is not recommended to reference System.Web.dll in a sql clr dll.

	private static readonly char[] punctuations = "!@#$%^&*()_-+=[{]};:>|./?".ToCharArray();
	private static readonly char[] startingChars = new char[] { '<', '&' };

	/// <summary>
	/// Generates a random password of the specified length.
	/// </summary>
	/// <param name="length">The number of characters in the generated password. The length must be between 1 and 128 characters.</param>
	/// <param name="numberOfNonAlphanumericCharacters">The minimum number of punctuation characters in the generated password.</param>
	/// <returns>
	/// A random password of the specified length.
	/// </returns>
	/// <exception cref="ArgumentException">
	/// Password length incorrect, must be between 1 and 128.
	/// or
	/// Non alphas cannot exceed the length of the password or be less than zero.
	/// </exception>
	/// <exception cref="T:System.ArgumentException"><paramref name="length" /> is less than 1 or greater than 128 -or-<paramref name="numberOfNonAlphanumericCharacters" /> is less than 0 or greater than <paramref name="length" />.</exception>
	[SqlFunction(SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlString xfnGeneratePassword(int length, int numberOfNonAlphanumericCharacters)
	{
		// source: System.Web.Security.Membership

		if (length < 1 || length > 128)
		{
			throw new ArgumentException("Password length incorrect, must be between 1 and 128.");
		}
		if (numberOfNonAlphanumericCharacters > length || numberOfNonAlphanumericCharacters < 0)
		{
			throw new ArgumentException("Non alphas cannot exceed the length of the password or be less than zero.");
		}
		string text;
		int num4;
		do
		{
			byte[] array = new byte[length];
			char[] array2 = new char[length];
			int num = 0;
			new RNGCryptoServiceProvider().GetBytes(array);
			for (int i = 0; i < length; i++)
			{
				int num2 = (int)(array[i] % 87);
				if (num2 < 10)
				{
					array2[i] = (char)(48 + num2);
				}
				else if (num2 < 36)
				{
					array2[i] = (char)(65 + num2 - 10);
				}
				else if (num2 < 62)
				{
					array2[i] = (char)(97 + num2 - 36);
				}
				else
				{
					array2[i] = punctuations[num2 - 62];
					num++;
				}
			}
			if (num < numberOfNonAlphanumericCharacters)
			{
				Random random = new Random();
				for (int j = 0; j < numberOfNonAlphanumericCharacters - num; j++)
				{
					int num3;
					do
					{
						num3 = random.Next(0, length);
					}
					while (!char.IsLetterOrDigit(array2[num3]));
					array2[num3] = punctuations[random.Next(0, punctuations.Length)];
				}
			}
			text = new string(array2);
		}
		while (IsDangerousString(text, out num4));
		return text;
	}

	// System.Web.CrossSiteScriptingValidation
	private static bool IsDangerousString(string s, out int matchIndex)
	{
		matchIndex = 0;
		int startIndex = 0;
		while (true)
		{
			int num = s.IndexOfAny(startingChars, startIndex);
			if (num < 0)
			{
				break;
			}
			if (num == s.Length - 1)
			{
				return false;
			}
			matchIndex = num;
			char c = s[num];
			if (c != '&')
			{
				if (c == '<' && (IsAtoZ(s[num + 1]) || s[num + 1] == '!' || s[num + 1] == '/' || s[num + 1] == '?'))
				{
					return true;
				}
			}
			else if (s[num + 1] == '#')
			{
				return true;
			}
			startIndex = num + 1;
		}
		return false;
	}

	private static bool IsAtoZ(char c)
	{
		return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
	}

	#endregion
}
