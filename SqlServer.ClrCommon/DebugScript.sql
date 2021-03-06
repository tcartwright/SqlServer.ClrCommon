

/* 
	TIM C: This script can be used to debug any of the code here, by just changing what gets executed

	Step 1: Modify this script to run the function you want to debug.
	Step 2: Add breakpoints for normal debug to the clr code.
	Step 3: Hit F5

	NOTE(s):	a) Ensure the project is in Debug mode. 
				b) For this to work, this script has to be set as the start up script in the project properties debug tab
*/


DECLARE @lorem varchar(max) = 'Lorem,ipsum,dolor,sit,amet,,consectetur,adipiscing,elit.,Vestibulum,laoreet,,enim,sed,venenatis,elementum,,velit,neque,fermentum,nulla,,sit,amet,eleifend,libero,sapien,eu,erat.'
DECLARE @lorem100 varchar(max) = replicate(@lorem, 100)

SELECT xs.RowId, xs.[Value] FROM clr.xfnSplit(@lorem, ',', 0) xs

SELECT xs.RowId, xs.[Value] FROM clr.xfnSplit(@lorem100, ',', 1) xs