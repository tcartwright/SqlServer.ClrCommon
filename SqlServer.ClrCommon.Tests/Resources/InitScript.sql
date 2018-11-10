


/* Enable CLR Integration */
EXEC sys.sp_configure 'show advanced options', 1;
GO --split
RECONFIGURE;
GO --split
EXEC sys.sp_configure 'clr enabled', 1;
GO --split
RECONFIGURE;
GO --split

