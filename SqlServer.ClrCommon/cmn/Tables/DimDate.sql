CREATE TABLE [cmn].[DimDate] (
    [DateValInt]                 INT            NOT NULL,
    [DateVal]                    DATE           NOT NULL,
    [NextDayDate]                DATE           NOT NULL,
    [CalendarYear]               SMALLINT       NOT NULL,
    [CalendarYearQuarter]        INT            NOT NULL,
    [CalendarYearMonth]          INT            NOT NULL,
    [CalendarYearDayOfYear]      INT            NOT NULL,
    [CalendarQuarter]            TINYINT        NOT NULL,
    [CalendarMonth]              TINYINT        NOT NULL,
    [CalendarDayOfYear]          SMALLINT       NOT NULL,
    [CalendarDayOfMonth]         SMALLINT       NOT NULL,
    [CalendarDayOfWeek]          TINYINT        NOT NULL,
    [CalendarYearName]           VARCHAR (4)    NOT NULL,
    [CalendarYearQuarterName]    VARCHAR (7)    NOT NULL,
    [CalendarYearMonthName]      VARCHAR (8)    NOT NULL,
    [CalendarYearMonthNameLong]  VARCHAR (14)   NOT NULL,
    [CalendarQuarterName]        VARCHAR (2)    NOT NULL,
    [CalendarMonthName]          VARCHAR (3)    NOT NULL,
    [CalendarMonthNameLong]      VARCHAR (9)    NOT NULL,
    [WeekdayName]                VARCHAR (3)    NOT NULL,
    [WeekdayNameLong]            VARCHAR (9)    NOT NULL,
    [CalendarStartOfYearDate]    DATE           NOT NULL,
    [CalendarEndOfYearDate]      DATE           NOT NULL,
    [CalendarStartOfQuarterDate] DATE           NOT NULL,
    [CalendarEndOfQuarterDate]   DATE           NOT NULL,
    [CalendarStartOfMonthDate]   DATE           NOT NULL,
    [CalendarEndOfMonthDate]     DATE           NOT NULL,
    [QuarterSeqNo]               INT            NOT NULL,
    [MonthSeqNo]                 INT            NOT NULL,
    [FiscalYearName]             SMALLINT       NOT NULL,
    [FiscalYearPeriod]           NUMERIC (6, 2) NOT NULL,
    [FiscalYearDayOfYear]        NUMERIC (7, 3) NOT NULL,
    [FiscalYearWeekName]         CHAR (9)       NOT NULL,
    [FiscalSemester]             TINYINT        NOT NULL,
    [FiscalQuarter]              TINYINT        NOT NULL,
    [FiscalPeriod]               TINYINT        NOT NULL,
    [FiscalDayOfYear]            SMALLINT       NOT NULL,
    [FiscalDayOfPeriod]          TINYINT        NOT NULL,
    [FiscalWeekName]             NUMERIC (3, 1) NOT NULL,
    [FiscalStartOfYearDate]      DATE           NOT NULL,
    [FiscalEndOfYearDate]        DATE           NOT NULL,
    [FiscalStartOfPeriodDate]    DATE           NOT NULL,
    [FiscalEndOfPeriodDate]      DATE           NOT NULL,
    [ISODate]                    CHAR (10)      NOT NULL,
    [ISOYearWeekNo]              INT            NOT NULL,
    [ISOWeekNo]                  SMALLINT       NOT NULL,
    [ISODayOfWeek]               TINYINT        NOT NULL,
    [ISOYearWeekName]            VARCHAR (8)    NOT NULL,
    [ISOYearWeekDayOfWeekName]   VARCHAR (10)   NOT NULL,
    [DateFormatYYYYMMDD]         CHAR (10)      NOT NULL,
    [DateFormatYYYYMD]           VARCHAR (10)   NOT NULL,
    [DateFormatMMDDYEAR]         CHAR (10)      NOT NULL,
    [DateFormatMDYEAR]           VARCHAR (10)   NOT NULL,
    [DateFormatMMMDYYYY]         VARCHAR (12)   NOT NULL,
    [DateFormatMMMMMMMMMDYYYY]   VARCHAR (18)   NOT NULL,
    [DateFormatMMDDYY]           CHAR (8)       NOT NULL,
    [DateFormatMDYY]             VARCHAR (8)    NOT NULL,
    [WorkDay]                    VARCHAR (8)    NOT NULL,
    [IsWorkDay]                  BIT            NOT NULL,
    CONSTRAINT [PK_DimDate] PRIMARY KEY CLUSTERED ([DateValInt] ASC)
);


GO
CREATE NONCLUSTERED INDEX [idx_DimDate_NextDayDate]
    ON [cmn].[DimDate]([NextDayDate] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_DimDate_DayDate]
    ON [cmn].[DimDate]([DateVal] ASC);

