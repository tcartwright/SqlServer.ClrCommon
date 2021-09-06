@cd "%~dp0"

powershell.exe -ExecutionPolicy Bypass -NoLogo -file "%~dpn0.ps1" 

@echo.
@echo Done.
@pause
