USE CommonDB

-- STRINGS

BEGIN 
	DECLARE @lorem varchar(max) = 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum laoreet, enim sed venenatis elementum, velit neque fermentum nulla, sit amet eleifend libero sapien eu erat.'
	DECLARE @lorem100 varchar(max) = replicate(@lorem, 100)
		, @lorem1000 varchar(max) = replicate(@lorem, 1000)
		, @lorem10000 varchar(max) = replicate(@lorem, 10000)
		, @lorem100000 varchar(max) = replicate(@lorem, 100000)

	SELECT xs.* FROM CommonDb.clr.xfnSplit(@lorem, ' ', 0) xs
	SELECT xs.* FROM CommonDb.clr.xfnSplit(@lorem100, ' ', 0) xs
	SELECT xs.* FROM CommonDb.clr.xfnSplit(@lorem1000, ' ', 0) xs
	SELECT xs.* FROM CommonDb.clr.xfnSplit(@lorem10000, ' ', 0) xs
	SELECT xs.* FROM CommonDb.clr.xfnSplit(@lorem100000, ' ', 0) xs
END
GO

BEGIN
	DECLARE @ints varchar(max) = '987654,323,423234,2323432,234234,555,777,1234,987654,323,423234,2323432,234234,555,777,1234,987654,323,423234,2323432,234234,555,777,1234,'
	DECLARE @ints100 varchar(max) = replicate(@ints, 100)
		, @ints1000 varchar(max) = replicate(@ints, 1000)
		, @ints10000 varchar(max) = replicate(@ints, 10000)
		, @ints100000 varchar(max) = replicate(@ints, 100000)

	SELECT xs.* FROM CommonDb.clr.xfnSplitInts(@ints, ',', 0) xs
	SELECT xs.* FROM CommonDb.clr.xfnSplitInts(@ints100, ',', 0) xs
	SELECT xs.* FROM CommonDb.clr.xfnSplitInts(@ints1000, ',', 0) xs
	SELECT xs.* FROM CommonDb.clr.xfnSplitInts(@ints10000, ',', 0) xs
	SELECT xs.* FROM CommonDb.clr.xfnSplitInts(@ints100000, ',', 0) xs
END

GO

--https://msdn.microsoft.com/en-us/library/26etazsy(v=vs.110).aspx
SELECT CommonDb.clr.xfnFormat(SYSDATETIME(), 'MM/dd/yyyy'),
	CommonDb.clr.xfnFormat(SYSDATETIME(), 'yyyy-MM-dd'),
	CommonDb.clr.xfnFormat('1234.4321', '##,###.00'),
	CommonDb.clr.xfnFormat('1234', '##,###.00')

SELECT CommonDb.clr.xfnProperCase('john doe')
SELECT CommonDb.clr.xfnPadLeft('9999', '0', 10), CommonDb.clr.xfnPadRight('9999', '0', 10)
SELECT CommonDb.clr.xfnTryParseToInt('123', 0), CommonDb.clr.xfnTryParseToInt('123a', 0)
SELECT CommonDb.clr.xfnTryParseToDbl('123.45', 0), CommonDb.clr.xfnTryParseToDbl('123.45a', 0)
SELECT CommonDb.clr.xfnCountString('Lorem ipsum dolor sit amet? Lorem ipsum dolor sit amet?', 'ipsum')
SELECT CommonDb.clr.xfnIndexOf('Lorem ipsum dolor sit amet? Lorem ipsum dolor sit amet?', 'ipsum')
SELECT CommonDb.clr.xfnLastIndexOf('Lorem ipsum dolor sit amet? Lorem ipsum dolor sit amet?', 'ipsum')
SELECT CommonDb.clr.xfnNthIndexOf('Lorem ipsum dolor sit amet? Lorem ipsum dolor sit amet?', 'ipsum', 1), CommonDb.clr.xfnNthIndexOf('Lorem ipsum dolor sit amet? Lorem ipsum dolor sit amet?', 'ipsum', 2), CommonDb.clr.xfnNthIndexOf('Lorem ipsum dolor sit amet? Lorem ipsum dolor sit amet?', 'ipsum', 3)
-- http://www.sqlservercentral.com/articles/ISNUMERIC()/71512/ Why doesn’t SQL SERVER ISNUMERIC work correctly? (SQL Spackle)
SELECT [Ascii Code] = STR(Number),[Ascii Character] = CHAR(Number),[ISNUMERIC Returns] = ISNUMERIC(CHAR(Number)),[XFNISNUMERIC Returns] = CommonDb.clr.xfnIsNumeric(CHAR(Number)) FROM Master.dbo.spt_Values WHERE Type = 'P' AND ISNUMERIC(CHAR(Number)) = 1
--as sql server does not allow overloads, the formatstring methods are delineated by a number representing how many arguments each one accepts. In the example below : xfnFormatString3 takes 3 arguments.
SELECT CommonDb.clr.xfnFormatString3(N'This is arg 0:({0}), arg 1:({1}), and arg 2 as a date: ({2:yyyy-MM-dd}), and arg 2 as a time: ({2:HH:mm:ss:fff})', 'foo', 'bar', GETDATE())
--http://en.wikipedia.org/wiki/Base64
SELECT CommonDb.clr.xfnToBase64(convert(varbinary(max), 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum laoreet, enim sed venenatis elementum, velit neque fermentum nulla, sit amet eleifend libero sapien eu erat. Etiam blandit ullamcorper justo ultrices ullamcorper. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nulla convallis tortor id varius mattis. Phasellus aliquam feugiat lectus et maximus. Suspendisse non urna erat. Suspendisse potenti. Ut rutrum tortor non neque suscipit commodo. Mauris scelerisque nulla eu pharetra tempor. Suspendisse et consectetur felis. Donec tincidunt, purus ut volutpat placerat, lacus erat accumsan nibh, non fermentum dolor ante in sapien. Duis eget orci ligula. Sed eu velit odio. Aliquam eu erat est. Sed interdum augue vel elit molestie, at euismod dui pharetra. Cras hendrerit rhoncus congue. Curabitur vel commodo nisl, ac egestas urna. Quisque quis ante quis turpis posuere finibus vitae in magna. Nulla rhoncus, purus ut vehicula scelerisque, velit nulla pharetra ante, ut blandit erat sem in odio. Ut nec nisl laoreet, finibus nulla non, consectetur velit. Nam mollis turpis vel congue tincidunt. Proin elementum luctus sapien, vitae sodales felis. Sed maximus, ligula a condimentum congue, metus nulla imperdiet dui, vel facilisis justo tellus vel elit. Nulla elit risus, maximus nec mollis vitae, aliquam nec metus.'))
SELECT convert(varchar(max), CommonDb.clr.xfnFromBase64('TG9yZW0gaXBzdW0gZG9sb3Igc2l0IGFtZXQsIGNvbnNlY3RldHVyIGFkaXBpc2NpbmcgZWxpdC4gVmVzdGlidWx1bSBsYW9yZWV0LCBlbmltIHNlZCB2ZW5lbmF0aXMgZWxlbWVudHVtLCB2ZWxpdCBuZXF1ZSBmZXJtZW50dW0gbnVsbGEsIHNpdCBhbWV0IGVsZWlmZW5kIGxpYmVybyBzYXBpZW4gZXUgZXJhdC4gRXRpYW0gYmxhbmRpdCB1bGxhbWNvcnBlciBqdXN0byB1bHRyaWNlcyB1bGxhbWNvcnBlci4gQ2xhc3MgYXB0ZW50IHRhY2l0aSBzb2Npb3NxdSBhZCBsaXRvcmEgdG9ycXVlbnQgcGVyIGNvbnViaWEgbm9zdHJhLCBwZXIgaW5jZXB0b3MgaGltZW5hZW9zLiBOdWxsYSBjb252YWxsaXMgdG9ydG9yIGlkIHZhcml1cyBtYXR0aXMuIFBoYXNlbGx1cyBhbGlxdWFtIGZldWdpYXQgbGVjdHVzIGV0IG1heGltdXMuIFN1c3BlbmRpc3NlIG5vbiB1cm5hIGVyYXQuIFN1c3BlbmRpc3NlIHBvdGVudGkuIFV0IHJ1dHJ1bSB0b3J0b3Igbm9uIG5lcXVlIHN1c2NpcGl0IGNvbW1vZG8uIE1hdXJpcyBzY2VsZXJpc3F1ZSBudWxsYSBldSBwaGFyZXRyYSB0ZW1wb3IuIFN1c3BlbmRpc3NlIGV0IGNvbnNlY3RldHVyIGZlbGlzLiBEb25lYyB0aW5jaWR1bnQsIHB1cnVzIHV0IHZvbHV0cGF0IHBsYWNlcmF0LCBsYWN1cyBlcmF0IGFjY3Vtc2FuIG5pYmgsIG5vbiBmZXJtZW50dW0gZG9sb3IgYW50ZSBpbiBzYXBpZW4uIER1aXMgZWdldCBvcmNpIGxpZ3VsYS4gU2VkIGV1IHZlbGl0IG9kaW8uIEFsaXF1YW0gZXUgZXJhdCBlc3QuIFNlZCBpbnRlcmR1bSBhdWd1ZSB2ZWwgZWxpdCBtb2xlc3RpZSwgYXQgZXVpc21vZCBkdWkgcGhhcmV0cmEuIENyYXMgaGVuZHJlcml0IHJob25jdXMgY29uZ3VlLiBDdXJhYml0dXIgdmVsIGNvbW1vZG8gbmlzbCwgYWMgZWdlc3RhcyB1cm5hLiBRdWlzcXVlIHF1aXMgYW50ZSBxdWlzIHR1cnBpcyBwb3N1ZXJlIGZpbmlidXMgdml0YWUgaW4gbWFnbmEuIE51bGxhIHJob25jdXMsIHB1cnVzIHV0IHZlaGljdWxhIHNjZWxlcmlzcXVlLCB2ZWxpdCBudWxsYSBwaGFyZXRyYSBhbnRlLCB1dCBibGFuZGl0IGVyYXQgc2VtIGluIG9kaW8uIFV0IG5lYyBuaXNsIGxhb3JlZXQsIGZpbmlidXMgbnVsbGEgbm9uLCBjb25zZWN0ZXR1ciB2ZWxpdC4gTmFtIG1vbGxpcyB0dXJwaXMgdmVsIGNvbmd1ZSB0aW5jaWR1bnQuIFByb2luIGVsZW1lbnR1bSBsdWN0dXMgc2FwaWVuLCB2aXRhZSBzb2RhbGVzIGZlbGlzLiBTZWQgbWF4aW11cywgbGlndWxhIGEgY29uZGltZW50dW0gY29uZ3VlLCBtZXR1cyBudWxsYSBpbXBlcmRpZXQgZHVpLCB2ZWwgZmFjaWxpc2lzIGp1c3RvIHRlbGx1cyB2ZWwgZWxpdC4gTnVsbGEgZWxpdCByaXN1cywgbWF4aW11cyBuZWMgbW9sbGlzIHZpdGFlLCBhbGlxdWFtIG5lYyBtZXR1cy4='))

SELECT CommonDb.clr.xfnBetterQuoteName('CommonDb.clr.TABLE_NAME'), CommonDb.clr.xfnBetterQuoteName('CommonDb.clr.OBJECT_NAME'), 
			[FieldList1] = CommonDb.clr.xfnBetterQuoteName('Column1, Column2, Column3, Column4, Column5, Column6, ;here is my sql injection; --'),
			[FieldList2] = CommonDb.clr.xfnBetterQuoteName('tbl.Column1, tbl.Column2, tbl.Column3'),
			[FieldList3] = CommonDb.clr.xfnBetterQuoteName('dbname.tbl.Column1, dbname.tbl.Column2, tbl.Column3')

-- DATES
--all of the end dates are converted for where clauses, by advancing to the next day at midnight. This allows where clauses to be written like so:
--	   where date >= '2015-04-15 00:00:00.000' and date < '2015-04-16 00:00:00.000'  - in this example 2015-04-15 was the actual end date.
--	   this allows for a proper range inclusion of all dates within the range regardless of datetime precision issues.

SELECT [xfnAgeInYears('1/1/1950')] = CommonDb.clr.xfnAgeInYears('1/1/1950'), [xfnIsLeapYear('1/1/1950')] = CommonDb.clr.xfnIsLeapYear('1/1/1950'), [xfnDaysInMonth('1/1/1950')] = CommonDb.clr.xfnDaysInMonth('1/1/1950'), [xfnIsDaylightSavingTime('1/1/1950')] = CommonDb.clr.xfnIsDaylightSavingTime('1/1/1950'), [xfnIsBusinessDay('1/1/1950')] = CommonDb.clr.xfnIsBusinessDay('1/1/1950')
SELECT [xfnDayOfWeek(GETDATE())] = CommonDb.clr.xfnDayOfWeek(GETDATE()), [xfnDayOfYear(GETDATE())] = CommonDb.clr.xfnDayOfYear(GETDATE()), [xfnDayOfWeek('6/1/1950')] = CommonDb.clr.xfnDayOfWeek('6/1/1950'), [xfnDayOfYear('6/1/1950')] = CommonDb.clr.xfnDayOfYear('6/1/1950')
SELECT CommonDb.clr.xfnStartOfDay(GETDATE()), CommonDb.clr.xfnEndOfDay(GETDATE())
SELECT CommonDb.clr.xfnStartOfWeek(GETDATE()), CommonDb.clr.xfnEndOfWeek(GETDATE())
SELECT CommonDb.clr.xfnStartOfMonth(GETDATE()), CommonDb.clr.xfnEndOfMonth(GETDATE())
SELECT CommonDb.clr.xfnStartOfYear(GETDATE()), CommonDb.clr.xfnEndOfYear(GETDATE())
SELECT CommonDb.clr.xfnStartOfQuarter(GETDATE(), 1), CommonDb.clr.xfnEndOfQuarter(GETDATE(), 1)
SELECT CommonDb.clr.xfnStartOfQuarter(GETDATE(), 2), CommonDb.clr.xfnEndOfQuarter(GETDATE(), 2)
SELECT CommonDb.clr.xfnStartOfQuarter(GETDATE(), 3), CommonDb.clr.xfnEndOfQuarter(GETDATE(), 3)
SELECT CommonDb.clr.xfnStartOfQuarter(GETDATE(), 4), CommonDb.clr.xfnEndOfQuarter(GETDATE(), 4)
SELECT CommonDb.clr.xfnStartOfCurrentQuarter(), CommonDb.clr.xfnEndOfCurrentQuarter()

-- MISC 
SELECT CommonDb.clr.xfnRandom(null, 1, 100), CommonDb.clr.xfnRandom(null, 1, 100)
SELECT CommonDb.clr.xfnRandom(42, 1, 1000) 

SELECT CommonDb.clr.xfnGZip(convert(varbinary(max), 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum laoreet, enim sed venenatis elementum, velit neque fermentum nulla, sit amet eleifend libero sapien eu erat. Etiam blandit ullamcorper justo ultrices ullamcorper. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nulla convallis tortor id varius mattis. Phasellus aliquam feugiat lectus et maximus. Suspendisse non urna erat. Suspendisse potenti. Ut rutrum tortor non neque suscipit commodo. Mauris scelerisque nulla eu pharetra tempor. Suspendisse et consectetur felis. Donec tincidunt, purus ut volutpat placerat, lacus erat accumsan nibh, non fermentum dolor ante in sapien. Duis eget orci ligula. Sed eu velit odio. Aliquam eu erat est. Sed interdum augue vel elit molestie, at euismod dui pharetra. Cras hendrerit rhoncus congue. Curabitur vel commodo nisl, ac egestas urna. Quisque quis ante quis turpis posuere finibus vitae in magna. Nulla rhoncus, purus ut vehicula scelerisque, velit nulla pharetra ante, ut blandit erat sem in odio. Ut nec nisl laoreet, finibus nulla non, consectetur velit. Nam mollis turpis vel congue tincidunt. Proin elementum luctus sapien, vitae sodales felis. Sed maximus, ligula a condimentum congue, metus nulla imperdiet dui, vel facilisis justo tellus vel elit. Nulla elit risus, maximus nec mollis vitae, aliquam nec metus.'))
SELECT convert(varchar(max), CommonDb.clr.xfnGUnzip(0x1F8B080000000000040055945B8EDB300C45B7C20518DE43D1F66F3A68514CFF19994958E8E14862D0E5F752769C0C301804B2F8B887977A2B5512E9DA2CD15262A9D4B41327E91385929B842EDD2AF1A2ABB6A0F94212B5CFF4475AD79345C44546120F90AC899A2C74972C99BB365C9624B95B9A708840CA7233A1B3D4ED98B2C5C8D351D503F42C79A1A827A9851AAF2A99C4482AA3EEF7AE9CE814392F08F1E0144A5DA5D25F6BBDE0A4570DD25E3FCDF435726BC46B4751EA1CB42BB512B4B49B411B8AF55299F00FDDE18AE7837C3B29532EAD57B4E8679A83ACBD34BA2AFA67296DA677AFE4B7EF1C23242309FE488181AB5AA3C41D2866FA79E52631E284A3DE0C32CE6217E54E1198710CF989FF69325CFE6D6D05066D4DD04126AB997704AF9FD6E29274A68F4ED57A05D1BDBE076DB09B61702B6885925259CA4C3FD82A3A6D01B4F1C32F8D3938E6F5CA55BAC390B496FAB99CF44FAE3863A4E8F55BC912A8038E2E966184D52AE458A77B89D657285C2307EF7E825B824BC56FE2102C35469F7ABA4EA3E3A731363772EE02EABB0D50CADC5417F4516A50CCED6291D1234C87DE378B9545A1F1CBCE78770EC1AFDB3D45CABAA002DB05C211332C4DA944F7B44CE4B75107AC68313D88C04695317AC0A8C006E0D7925D0C9020133E5BE5933A184FBAD386B816913378DBAD23818F72A65FB681BFB9A22173FC4234160D736D26158BA260831277ED3C4024BE78F466BABD8157E072D50024AFB33D566FC41CF3F59A93C73C9669606AFE1CE49DE187EF6B180A9E5BFE68694B87A17D7E29EEDBFBF00EF4201A9F9236264EEA69152C452D28773C13146DACC236EF69D7DDCAC218CEC36F3EC57D51A6DD02343670D13DCB566722BC2847A79AB0C18BC23A18EA604267BC04C888E6B6C7A36FEBF9B0C403F3A007965E6EAF3BB8ECFA468FD3B1D4E38BD79DFF03ADE340B95D050000))

SELECT CommonDb.clr.xfnGeneratePassword(20, 1)

--MATH/NUMERIC CONVERSIONS
SELECT [xfnIsPrime(4)] = CommonDb.clr.xfnIsPrime(4) /*false*/, [xfnIsPrime(7)] = CommonDb.clr.xfnIsPrime(7)/*true*/, [xfnIsPrime(501)] = CommonDb.clr.xfnIsPrime(501)/*false*/, [xfnIsPrime(571)] = CommonDb.clr.xfnIsPrime(571)/*true*/
SELECT sv.number, [IsPrime] = CommonDb.clr.xfnIsPrime(sv.number), [CubeRoot] = CommonDb.clr.xfnCubeRoot(sv.number)  FROM master.dbo.spt_values sv WHERE sv.number > -5 AND sv.type = 'P' ORDER BY sv.number 
SELECT CommonDb.clr.xfnCubeRoot(125) /*5*/
SELECT CommonDb.clr.xfnDecimalToBinary('8675309')
SELECT CommonDb.clr.xfnDecimalToHex('8675309')
SELECT CommonDb.clr.xfnHexToDecimal('845FED')
SELECT CommonDb.clr.xfnHexToBinary('845FED')
SELECT CommonDb.clr.xfnBinaryToDecimal('100001000101111111101101')
SELECT CommonDb.clr.xfnBinaryToHex('100001000101111111101101')

--REGEX
select CommonDb.clr.xfnRegexIsValidEmail(N'notreal@fakeemail.com'), CommonDb.clr.xfnRegexIsValidEmail(N'notreal-fakeemail.com'), CommonDb.clr.xfnRegexIsValidEmail(N'notreal@fakeemail.com')
select CommonDb.clr.xfnRegexIsValidSSN(N'000-12-1234'), CommonDb.clr.xfnRegexIsValidSSN(N'487-12-1234')
select CommonDb.clr.xfnRegexIsValidUSPhone(N'123-456-7890')
select CommonDb.clr.xfnRegexIsValidUSZip(N'12345'), CommonDb.clr.xfnRegexIsValidUSZip(N'12345-6789')

--REGEX MATCH
select CommonDb.clr.xfnRegexMatch(N'123-45-6789', N'^\d{3}-\d{2}-\d{4}$' )
select CommonDb.clr.xfnRegexMatch(NULL, N'^\d{3}-\d{2}-\d{4}$' )
SELECT * FROM sys.objects o WHERE CommonDb.clr.xfnRegexMatch(o.name, N'xfn(?i-:reg|format).*') = 1

--REGEX MATCHES
BEGIN
	declare @text nvarchar(max) = N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum laoreet, enim sed venenatis elementum, velit neque fermentum nulla, sit amet eleifend libero sapien eu erat. Etiam blandit ullamcorper justo ultrices ullamcorper. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nulla convallis tortor id varius mattis. Phasellus aliquam feugiat lectus et maximus. Suspendisse non urna erat. Suspendisse potenti. Ut rutrum tortor non neque suscipit commodo. Mauris scelerisque nulla eu pharetra tempor. Suspendisse et consectetur felis. Donec tincidunt, purus ut volutpat placerat, lacus erat accumsan nibh, non fermentum dolor ante in sapien. Duis eget orci ligula. Sed eu velit odio. Aliquam eu erat est. Sed interdum augue vel elit molestie, at euismod dui pharetra. Cras hendrerit rhoncus congue. Curabitur vel commodo nisl, ac egestas urna. Quisque quis ante quis turpis posuere finibus vitae in magna. Nulla rhoncus, purus ut vehicula scelerisque, velit nulla pharetra ante, ut blandit erat sem in odio. Ut nec nisl laoreet, finibus nulla non, consectetur velit. Nam mollis turpis vel congue tincidunt. Proin elementum luctus sapien, vitae sodales felis. Sed maximus, ligula a condimentum congue, metus nulla imperdiet dui, vel facilisis justo tellus vel elit. Nulla elit risus, maximus nec mollis vitae, aliquam nec metus.'
	select count(distinct [Text]) from CommonDb.clr.xfnRegexMatches( @text, '\w+' )
	select * from CommonDb.clr.xfnRegexMatches( @text, '(are)|(et)|(per)' )

	select CommonDb.clr.xfnRegexReplace(@text, '(?i)(ARE)|(ET)|(PER)', ' *FOOBAR* ' ) -- case insensitive regex
	select CommonDb.clr.xfnRegexReplace(@text, '(ARE)|(ET)|(PER)', 'FOOBAR' ) -- case sensitive regex, hence no replacments made
END

--REGEX GROUP
SELECT CommonDb.clr.xfnRegexGroup(N'Lorem ipsum1 dolor sit amet, consectetur adipiscing elit. Lorem ipsum2 dolor sit amet, consectetur adipiscing elit. Lorem3 ipsum dolor sit amet, consectetur adipiscing elit. ', '(?<Foo>ipsum\d?)', 'Foo')
SELECT CommonDb.clr.xfnRegexGroup(N'1234567,Abraham Lincoln,A,M', '^(?<CustomerId>\d{7}),(?<Name>[^,]*),(?<Type>[A-Z]),(?<Gender>M|F)$', 'Name')

--REGEX GROUPS
SELECT * FROM CommonDb.clr.xfnRegexGroups(N'1234567,Abraham Lincoln,A,M', '^(?<CustomerId>\d{7}),(?<Name>[^,]*),(?<Type>[A-Z]),(?<Gender>M|F)$')

BEGIN
	DECLARE @listpattern nvarchar(500) = N'(?<CustomerId>\d{7}),(?<Name>[^,]*),(?<Type>[A-Za-z]),(?<Gender>[Mm]|[Ff])\r?\n'
		,@list varchar(1000) = 
			'1234567,Abraham Lincoln,A,M
			9876453,George Washington,B,M
			8647530,Samuel Adams,C,M
			4561237,Thomas Jefferson,D,M
			2589631,Andrew Jackson,D,M
			'
	--SELECT * FROM CommonDb.clr.xfnRegexGroups( @list, @listpattern ) regex
	SELECT
		--f.[0],
		f.[CustomerId],
		f.[Name],
		f.[Type],
		f.[Gender]
	from CommonDb.clr.xfnRegexGroups( @list, @listpattern ) regex
	pivot
	(
		max([Text])
		for [Group]
		in ([0], [CustomerId], [Name], [Type], [Gender] )
	) as f
END

GO

BEGIN 
	DECLARE @listpattern nvarchar(500) = N'(\d{7}),([^,]*),([A-Z]),([Mm]|[Ff])\r?\n'
		,@list varchar(1000) = 
			'1234567,Abraham Lincoln,A,M
			9876453,George Washington,B,M
			8647530,Samuel Adams,C,M
			4561237,Thomas Jefferson,D,M
			2589631,Andrew Jackson,D,M
			'
	--SELECT * FROM CommonDb.clr.xfnRegexGroups( @list, @listpattern ) regex
	SELECT
	   --f.[0],
	   f.[1],
	   f.[2],
	   f.[3],
	   f.[4]
	from CommonDb.clr.xfnRegexGroups( @list, @listpattern ) regex
	pivot
	(
	   max([Text])
	   for [Group]
	   in ([0], [1], [2], [3], [4] )
	) as f
END

-- AGGREGATES
BEGIN 
	DECLARE @tbl TABLE (
		RowId int NOT NULL IDENTITY(1, 1),
		Name varchar(50),
		StateProvince char(2)
	)

	INSERT INTO @tbl
	VALUES
	('Foo','TX') 
	,('Foo','CA') 
	,('Foo','WA') 
	,('Bar','OK') 
	,('Bar','MI') 

	SELECT Name, CommonDb.clr.xagConcat(t.StateProvince, ',') , CommonDb.clr.xagConcat(t.RowId, ',')  
	FROM @tbl t
	GROUP BY t.Name
END

--this below aggregate queries do not show any meaningful results, just more of an example on how to call them

SELECT sv.name, CommonDb.clr.xagMedian(sv.number)
FROM master..spt_values sv 
GROUP BY sv.name

SELECT sv.name, CommonDb.clr.xagPercentile(sv.number, 20), CommonDb.clr.xagPercentile(sv.number, 80)
FROM master..spt_values sv 
GROUP BY sv.name
HAVING CommonDb.clr.xagPercentile(sv.number, 20) <> CommonDb.clr.xagPercentile(sv.number, 80)
ORDER BY 2 desc, 3 DESC

SELECT sv.name, CommonDb.clr.xagWeightedAvg(sv.number, cast(RAND(CHECKSUM(NEWID())) * (50 + 1) AS int)) --this weight should come from another column in the table, not be a random weight
FROM master..spt_values sv 
GROUP BY sv.name


