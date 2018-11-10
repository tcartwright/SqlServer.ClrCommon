create function cmn.F_TABLE_DATE
(
	@FIRST_DATE		datetime,
	@LAST_DATE		datetime
)
returns  @DATE table (
    [DateId]	[int]		not null, 
    --primary key clustered,
    [Date]						[Date]	not null
    primary key clustered,
    [NextDayDate]					[Date]	not null ,
    [CalendarYear]					[smallint]	not null ,
    [CalendarYearQuarter]			[int]	not null ,
    [CalendarYearMonth]				[int]		not null ,
    [CalendarYearDayOfYear]			[int]		not null ,
    [CalendarQuarter]				[tinyint]	not null ,
    [CalendarMonth]					[tinyint]	not null ,
    [CalendarDayOfYear]				[smallint]	not null ,
    [CalendarDayOfMonth]				[smallint]	not null ,
    [CalendarDayOfWeek]				[tinyint]	not null ,
    [CalendarYearName]				[varchar] (4)	not null ,
    [CalendarYearQuarterName]			[varchar] (7)	not null ,
    [CalendarYearMonthName]			[varchar] (8)	not null ,
    [CalendarYearMonthNameLong]		[varchar] (14)	not null ,
    [CalendarQuarterName]			[varchar] (2)	not null ,
    [CalendarMonthName]				[varchar] (3)	not null ,
    [CalendarMonthNameLong]			[varchar] (9)	not null ,
    [WeekdayName]					[varchar] (3)	not null ,
    [WeekdayNameLong]				[varchar] (9)	not null ,
    [CalendarStartOfYearDate]			[Date]	not null ,
    [CalendarEndOfYearDate]			[Date]	not null ,
    [CalendarStartOfQuarterDate]		[Date]	not null ,
    [CalendarEndOfQuarterDate]		[Date]	not null ,
    [CalendarStartOfMonthDate]		[Date]	not null ,
    [CalendarEndOfMonthDate]			[Date]	not null ,
    [QuarterSeqNo]					[int]		not null ,
    [MonthSeqNo]					[int]	not	 null ,
    -------
    [FiscalYearName]				[smallint] 	not null ,
    [FiscalYearPeriod]				[numeric] (6,2)		not null ,
    [FiscalYearDayOfYear]			[numeric] (7,3) 		not null ,
    [FiscalYearWeekName]				Char(9) not null,
    [FiscalSemester]				[tinyint]  not null,
    [FiscalQuarter]					[tinyint]  not null,
    [FiscalPeriod]					[tinyint]	not null ,
    [FiscalDayOfYear]				[smallint]	not null ,
    [FiscalDayOfPeriod]				[tinyint]	not null ,
    [FiscalWeekName]				[numeric](3,1)  not null,
    [FiscalStartOfYearDate]			[Date]	not null ,
    [FiscalEndOfYearDate]			[Date]	not null ,
    [FiscalStartOfPeriodDate]			[Date]	not null ,
    [FiscalEndOfPeriodDate]			[Date]	not null ,
    [ISODate]						[char](10)		not null ,
    [ISOYearWeekNo]					[int]			not null ,
    [ISOWeekNo]					[smallint]		not null ,
    [ISODayOfWeek]					[tinyint]		not null ,
    [ISOYearWeekName]				[varchar](8)	not null ,
    [ISOYearWeekDayOfWeekName]		[varchar](10)	not null ,
    [DateFormatYYYYMMDD]				[char](10)		not null ,
    [DateFormatYYYYMD]				[varchar](10)	not null ,
    [DateFormatMMDDYEAR]				[char](10)		not null ,
    [DateFormatMDYEAR]				[varchar](10)	not null ,
    [DateFormatMMMDYYYY]				[varchar](12)	not null ,
    [DateFormatMMMMMMMMMDYYYY]		[varchar](18)	not null ,
    [DateFormatMMDDYY]				[char](8)		not null ,
    [DateFormatMDYY]				[varchar](8)	not null ,
    [WorkDay]						[varchar](8)	not null ,
    [IsWorkDay]					[bit]			not null
) 
as
begin
    declare @cr			varchar(2)
    select @cr			= char(13)+Char(10)
    declare @ErrorMessage		varchar(400)
    declare @START_DATE		datetime
    declare @END_DATE		datetime
    declare @LOW_DATE	datetime

    declare	@start_no	int
    declare	@end_no	int

    -- Verify @FIRST_DATE is not null 
    if @FIRST_DATE is null
	    begin
	    select @ErrorMessage =
		    '@FIRST_DATE cannot be null'
	    goto Error_Exit
	    end

    -- Verify @LAST_DATE is not null 
    if @LAST_DATE is null
	    begin
	    select @ErrorMessage =
		    '@LAST_DATE cannot be null'
	    goto Error_Exit
	    end

    -- Verify @FIRST_DATE is not before 1754-01-01
    IF  @FIRST_DATE < '17540101'	begin
	    select @ErrorMessage =
		    '@FIRST_DATE cannot before 1754-01-01'+
		    ', @FIRST_DATE = '+
		    isnull(convert(varchar(40),@FIRST_DATE,121),'NULL')
	    goto Error_Exit
	    end

    -- Verify @LAST_DATE is not after 9997-12-31
    IF  @LAST_DATE > '99971231'	begin
	    select @ErrorMessage =
		    '@LAST_DATE cannot be after 9997-12-31'+
		    ', @LAST_DATE = '+
		    isnull(convert(varchar(40),@LAST_DATE,121),'NULL')
	    goto Error_Exit
	    end

    -- Verify @FIRST_DATE is not after @LAST_DATE
    if @FIRST_DATE > @LAST_DATE
	    begin
	    select @ErrorMessage =
		    '@FIRST_DATE cannot be after @LAST_DATE'+
		    ', @FIRST_DATE = '+
		    isnull(convert(varchar(40),@FIRST_DATE,121),'NULL')+
		    ', @LAST_DATE = '+
		    isnull(convert(varchar(40),@LAST_DATE,121),'NULL')
	    goto Error_Exit
	    end

    -- Set @START_DATE = @FIRST_DATE at midnight
    select @START_DATE	= dateadd(dd,datediff(dd,0,@FIRST_DATE),0)
    -- Set @END_DATE = @LAST_DATE at midnight
    select @END_DATE	= dateadd(dd,datediff(dd,0,@LAST_DATE),0)
    -- Set @LOW_DATE = earliest possible SQL Server datetime
    select @LOW_DATE	= convert(datetime,'17530101')

    -- Find the number of day from 1753-01-01 to @START_DATE and @END_DATE
    select	@start_no	= datediff(dd,@LOW_DATE,@START_DATE) ,
	    @end_no	= datediff(dd,@LOW_DATE,@END_DATE)

    -- Declare number tables
    declare @num1 table (NUMBER int not null primary key clustered)
    declare @num2 table (NUMBER int not null primary key clustered)
    declare @num3 table (NUMBER int not null primary key clustered)

    -- Declare table of ISO Week ranges
    declare @ISO_WEEK table
    (
    [ISO_WEEK_YEAR] 		int		not null
	    primary key clustered,
    [ISO_WEEK_YEAR_START_DATE]	datetime	not null,
    [ISO_WEEK_YEAR_END_DATE]	Datetime	not null
    )

    -- Find rows needed in number tables
    declare	@rows_needed		int
    declare	@rows_needed_root	int
    select	@rows_needed		= @end_no - @start_no + 1
    select  @rows_needed		=
		    case
		    when @rows_needed < 10
		    then 10
		    else @rows_needed
		    end
    select	@rows_needed_root	= convert(int,ceiling(sqrt(@rows_needed)))

    -- Load number 0 to 16
    insert into @num1 (NUMBER)
    select NUMBER = 0 union all select  1 union all select  2 union all
    select          3 union all select  4 union all select  5 union all
    select          6 union all select  7 union all select  8 union all
    select          9 union all select 10 union all select 11 union all
    select         12 union all select 13 union all select 14 union all
    select         15
    order by
	    1
    -- Load table with numbers zero thru square root of the number of rows needed +1
    insert into @num2 (NUMBER)
    select
	    NUMBER = a.NUMBER+(16*b.NUMBER)+(256*c.NUMBER)
    from
	    @num1 a cross join @num1 b cross join @num1 c
    where
	    a.NUMBER+(16*b.NUMBER)+(256*c.NUMBER) <
	    @rows_needed_root
    order by
	    1

    -- Load table with the number of rows needed for the date range
    insert into @num3 (NUMBER)
    select
	    NUMBER = a.NUMBER+(@rows_needed_root*b.NUMBER)
    from
	    @num2 a
	    cross join
	    @num2 b
    where
	    a.NUMBER+(@rows_needed_root*b.NUMBER) < @rows_needed
    order by
	    1

    declare	@iso_start_year	int
    declare	@iso_end_year	int

    select	@iso_start_year	= datepart(year,dateadd(year,-1,@start_date))
    select	@iso_end_year	= datepart(year,dateadd(year,1,@end_date))

    -- Load table with start and end dates for ISO week years
    insert into @ISO_WEEK
	    (
	    [ISO_WEEK_YEAR],
	    [ISO_WEEK_YEAR_START_DATE],
	    [ISO_WEEK_YEAR_END_DATE]
	    )
    select
	    [ISO_WEEK_YEAR] = a.NUMBER,
	    [0ISO_WEEK_YEAR_START_DATE]	=
		    dateadd(dd,(datediff(dd,@LOW_DATE,
		    dateadd(day,3,dateadd(year,a.[NUMBER]-1900,0))
		    )/7)*7,@LOW_DATE),
	    [ISO_WEEK_YEAR_END_DATE]	=
		    dateadd(dd,-1,dateadd(dd,(datediff(dd,@LOW_DATE,
		    dateadd(day,3,dateadd(year,a.[NUMBER]+1-1900,0))
		    )/7)*7,@LOW_DATE))
    from
	    (
	    select
		    NUMBER = NUMBER+@iso_start_year
	    from
		    @num3
	    where
		    NUMBER+@iso_start_year <= @iso_end_year
	    ) a
    order by
	    a.NUMBER

    -- Load Date table
    insert into @DATE
    select
	    [DateId]			= a.[DateId] ,
	    [Date]				= a.[DATE] ,

	    [NextDayDate]			=
		    dateadd(day,1,a.[DATE]) ,

	    [CalendarYear]			=
		    datepart(year,a.[DATE]) ,
	    [CalendarYearQuarter]		=
		    (10*datepart(year,a.[DATE]))+datepart(quarter,a.[DATE]) ,

	    [CalendarYearMonth]		=
		    (100*datepart(year,a.[DATE]))+datepart(month,a.[DATE]) ,
	    [CalendarYearDayOfYear]		=
		    (1000*datepart(year,a.[DATE]))+
		    datediff(dd,dateadd(yy,datediff(yy,0,a.[DATE]),0),a.[DATE])+1 ,
	    [CalendarQuarter]		=
		    datepart(quarter,a.[DATE]) ,
	    [CalendarMonth]		=
		    datepart(month,a.[DATE]) ,
	    [CalendarDayOfYear]			=
		    datediff(dd,dateadd(yy,datediff(yy,0,a.[DATE]),0),a.[DATE])+1 ,
	    [CalendarDayOfMonth]			=
		    datepart(day,a.[DATE]) ,
	    [CalendarDayOfWeek]		=
		    -- Sunday = 1, Monday = 2, ,,,Saturday = 7
		    (datediff(dd,'17530107',a.[DATE])%7)+1  ,
	    [CalendarYearName]		=
		    datename(year,a.[DATE]) ,
	    [CalendarYearQuarterName]	=
		    datename(year,a.[DATE])+' Q'+datename(quarter,a.[DATE]) ,
	    [CalendarYearMonthName]	=
		    datename(year,a.[DATE])+' '+left(datename(month,a.[DATE]),3) ,
	    [CalendarYearMonthNameLong]	=
		    datename(year,a.[DATE])+' '+datename(month,a.[DATE]) ,
	    [CalendarQuarterName]		=
		    'Q'+datename(quarter,a.[DATE]) ,
	    [CalendarMonthName]		=
		    left(datename(month,a.[DATE]),3) ,
	    [CalendarMonthNameLong]	=
		    datename(month,a.[DATE]) ,
	    [WeekdayName]		=
		    left(datename(weekday,a.[DATE]),3) ,
	    [WeekdayNameLong]	=
		    datename(weekday,a.[DATE]),

	    [CalendarStartOfYearDate]	=
		    dateadd(year,datediff(year,0,a.[DATE]),0) ,
	    [CalendarEndOfYearDate]	=
		    dateadd(day,-1,dateadd(year,datediff(year,0,a.[DATE])+1,0)) ,

	    [CalendarStartOfQuarterDate]	=
		    dateadd(quarter,datediff(quarter,0,a.[DATE]),0) ,
	    [CalendarEndOfQuarterDate]	=
		    dateadd(day,-1,dateadd(quarter,datediff(quarter,0,a.[DATE])+1,0)) ,

	    [CalendarStartOfMonthDate]	=
		    dateadd(month,datediff(month,0,a.[DATE]),0) ,
	    [CalendarEndOfMonthDate]	=
		    dateadd(day,-1,dateadd(month,datediff(month,0,a.[DATE])+1,0)),
	    [QuarterSeqNo]				= 
		    datediff(quarter,@LOW_DATE,a.[DATE]),
	    [MonthSeqNo]					=
		    datediff(month,@LOW_DATE,a.[DATE]),
  
	   -----------
 	    [FiscalYearName]		= 
		    datename(year,a.[DATE]) ,
	    [FiscalYearPeriod]		= 
	   (datepart(year,a.[DATE]))+ (cast((datepart(month,a.[DATE])/100.00) as decimal (2,2))),
	    [FiscalYearDayOfYear]		= 
	   (datepart(year,a.[DATE])+ cast((datediff(dd,dateadd(yy,datediff(yy,0,a.[DATE]),0),a.[DATE])+1)/1000.00 as numeric (3,3))),
	[FiscalYearWeekName] = '',
	[FiscalSemester] = case 
	when datepart(quarter,a.[DATE]) <= 2 then 1
	else 2
	end,
	[FiscalQuarter] = datepart(quarter,a.[DATE]),
	    [FiscalPeriod]		=	datepart(month,a.[DATE]) ,
	    [FiscalDayOfYear]=datediff(dd,dateadd(yy,datediff(yy,0,a.[DATE]),0),a.[DATE])+1 ,
	    [FiscalDayOfPeriod]=datepart(day,a.[DATE]) ,
	[FiscalWeekName] = 0, --cast(datepart(month,a.[DATE]) as char(2)) + '.0' ,
	    [FiscalStartOfYearDate]	=
		    dateadd(year,datediff(year,0,a.[DATE]),0) ,
	    [FiscalEndOfYearDate]	= 
		    dateadd(day,-1,dateadd(year,datediff(year,0,a.[DATE])+1,0)) ,
	    [FiscalStartOfPeriodDate]	= 
		    dateadd(month,datediff(month,0,a.[DATE]),0) ,
	    [FiscalEndOfPeriodDate]	=
		    dateadd(day,-1,dateadd(month,datediff(month,0,a.[DATE])+1,0)),
    
	    [ISODate]		=
		    replace(convert(char(10),a.[DATE],111),'/','-') ,

	    [ISOYearWeekNo]		=
		    (100*b.[ISO_WEEK_YEAR])+
		    (datediff(dd,b.[ISO_WEEK_YEAR_START_DATE],a.[DATE])/7)+1 ,

	    [ISOWeekNo]		=
		    (datediff(dd,b.[ISO_WEEK_YEAR_START_DATE],a.[DATE])/7)+1 ,

	    [ISODayOfWeek]		=
		    -- Sunday = 1, Monday = 2, ,,,Saturday = 7
		    (datediff(dd,@LOW_DATE,a.[DATE])%7)+1  ,

	    [ISOYearWeekName]		=
		    convert(varchar(4),b.[ISO_WEEK_YEAR])+'-W'+
		    right('00'+convert(varchar(2),(datediff(dd,b.[ISO_WEEK_YEAR_START_DATE],a.[DATE])/7)+1),2) ,

	    [ISOYearWeekDayOfWeekName]		=
		    convert(varchar(4),b.[ISO_WEEK_YEAR])+'-W'+
		    right('00'+convert(varchar(2),(datediff(dd,b.[ISO_WEEK_YEAR_START_DATE],a.[DATE])/7)+1),2) +
		    '-'+convert(varchar(1),(datediff(dd,@LOW_DATE,a.[DATE])%7)+1) ,

	    [DateFormatYYYYMMDD]		=
		    convert(char(10),a.[DATE],111) ,
	    [DateFormatYYYYMD]		= 
		    convert(varchar(10),
		    convert(varchar(4),year(a.[DATE]))+'/'+
		    convert(varchar(2),day(a.[DATE]))+'/'+
		    convert(varchar(2),month(a.[DATE]))),
	    [DateFormatMMDDYEAR]		= 
		    convert(char(10),a.[DATE],101) ,
	    [DateFormatMDYEAR]		= 
		    convert(varchar(10),
		    convert(varchar(2),month(a.[DATE]))+'/'+
		    convert(varchar(2),day(a.[DATE]))+'/'+
		    convert(varchar(4),year(a.[DATE]))),
	    [DateFormatMMMDYYYY]		= 
		    convert(varchar(12),
		    left(datename(month,a.[DATE]),3)+' '+
		    convert(varchar(2),day(a.[DATE]))+', '+
		    convert(varchar(4),year(a.[DATE]))),
	    [DateFormatMMMMMMMMMDYYYY]	= 
		    convert(varchar(18),
		    datename(month,a.[DATE])+' '+
		    convert(varchar(2),day(a.[DATE]))+', '+
		    convert(varchar(4),year(a.[DATE]))),
	    [DateFormatMMDDYY]		=
		    convert(char(8),a.[DATE],1) ,
	    [DateFormatMDYY]		= 
		    convert(varchar(8),
		    convert(varchar(2),month(a.[DATE]))+'/'+
		    convert(varchar(2),day(a.[DATE]))+'/'+
		    right(convert(varchar(4),year(a.[DATE])),2)),
	 [WorkDay] = CASE WHEN ((datediff(dd,'17530107',a.[DATE])%7)+1 ) IN (1, 7) THEN 'No Work' ELSE 'Work Day' END,

	 [IsWorkDay]= CASE WHEN ((datediff(dd,'17530107',a.[DATE])%7)+1 ) IN (1, 7) THEN 0 ELSE 1 END
	    --[WorkDay] = space(8),
	    --[IsWorkDay] = 0
    from
	    (
	    -- Derived table is all dates needed for date range
	    select	top 100 percent
		    [DateId]	= aa.[NUMBER],
		    [DATE]		=
			    dateadd(dd,aa.[NUMBER],@LOW_DATE)
	    from
		    (
		    select
			    NUMBER = NUMBER+@start_no 
		    from
			    @num3
		    where
			    NUMBER+@start_no <= @end_no
		    ) aa
	    order by
		    aa.[NUMBER]
	    ) a
	    join
	    -- Match each date to the proper ISO week year
	    @ISO_WEEK b
	    on a.[DATE] between 
		    b.[ISO_WEEK_YEAR_START_DATE] and 
		    b.[ISO_WEEK_YEAR_END_DATE]
    order by
	    a.[DateId]
  


    /*
    ALTER TABLE @Date
	ADD PRIMARY KEY NONCLUSTERED ([Date] ASC) WITH (DATA_COMPRESSION = NONE) ON [PRIMARY]
    GO
    */

    /*
	    Update TST
	    Set TST.DateID = TST.Date
	    FROM    @Date TST
	 */   
  
	    INSERT INTO @Date
		 (DateId,    Date,    NextDayDate, CalendarYear, CalendarYearQuarter, CalendarYearMonth, CalendarYearDayOfYear, CalendarQuarter, CalendarMonth, CalendarDayOfYear, CalendarDayOfMonth, CalendarDayOfWeek, CalendarYearName, CalendarYearQuarterName, CalendarYearMonthName, CalendarYearMonthNameLong, CalendarQuarterName, CalendarMonthName, CalendarMonthNameLong, WeekdayName, WeekdayNameLong, CalendarStartOfYearDate, CalendarEndOfYearDate, CalendarStartOfQuarterDate, CalendarEndOfQuarterDate, CalendarStartOfMonthDate, CalendarEndOfMonthDate, QuarterSeqNo, MonthSeqNo, FiscalYearName, FiscalYearPeriod, FiscalYearDayOfYear, FiscalSemester, FiscalQuarter, FiscalPeriod, FiscalDayOfYear, FiscalDayOfPeriod, FiscalWeekName, FiscalStartOfYearDate, FiscalEndOfYearDate, FiscalStartOfPeriodDate, FiscalEndOfPeriodDate, ISODate, ISOYearWeekNo, ISOWeekNo, ISODayOfWeek, ISOYearWeekName, ISOYearWeekDayOfWeekName, DateFormatYYYYMMDD, DateFormatYYYYMD, DateFormatMMDDYEAR, DateFormatMDYEAR, DateFormatMMMDYYYY, DateFormatMMMMMMMMMDYYYY, DateFormatMMDDYY, DateFormatMDYY, WorkDay, IsWorkDay, FiscalYearWeekName) 
    VALUES (19000101, '1/1/1900', '1/2/1900',    1900,     19001,               190001 ,           19000001,              1,                 1,               1,                 1,                   1,                'TBD',              'NA',                  'TBD',                'TBD',                      'NA',              'TBD',              'TBD',              'TBD',        'TBD',            '1/1/1900',           '12/31/1900',             '1/1/1900',               '1/1/1900',                '1/1/1900',                '1/1/1900',              0 , 0 ,1900,               0,                  0,                 0,              0,               0,            0,               0,                0.0,            '1/1/1900',           '12/31/1900',           '1/1/1900',             '1/1/1900',     '1900-01-01',   1,            1,          1,          'TBD',            'TBD',                  '1900/01/01',     '1900/1/1',         '01/01/1900',     '1/1/1900',         'TBD',                'TBD',                      'TBD',         'TBD',     'TBD',    0, '')

     
    return

    Error_Exit:

    -- Return a pesudo error message by trying to
    -- convert an error message string to an int.
    -- This method is used because the error displays
    -- the string it was trying to convert, and so the
    -- calling application sees a formatted error message.

    declare @error int

    set @error = convert(int,@cr+@cr+
    '*******************************************************************'+@cr+
    '* Error in function F_TABLE_DATE:'+@cr+'* '+
    isnull(@ErrorMessage,'Unknown Error')+@cr+
    '*******************************************************************'+@cr+@cr)

    return

end