@cd "%~dp0"

powershell.exe -ExecutionPolicy Bypass -NoLogo -file "%~dpn0" 

@echo.
@echo Done.
@pause