[CmdletBinding()]
param (
    [Parameter(Mandatory=$true)]
    [string]
    $dacpacPath,
    [Parameter(Mandatory=$true)]
    [string]
    $binaryPath,
    [Parameter(Mandatory=$true)]
    [string]
    $pfxPath,
    [Parameter(Mandatory=$true)]
    [string]
    $databaseName
)

Set-StrictMode -Version 3.0
Clear-Host

$principal = New-Object Security.Principal.WindowsPrincipal([Security.Principal.WindowsIdentity]::GetCurrent())
$isAdmin = $principal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator);
$scriptRoot = [System.IO.Directory]::GetParent($MyInvocation.MyCommand.Definition).FullName
$rootPath = Split-Path $binaryPath -Parent

Set-location $rootPath

function InstallPackage($packageName, $dllName, $folderMatch = "lib\\net\d{1,4}", $source = "nuget.org", [switch]$force, [switch]$SkipDependencies) {
    if (!$dllName) {
        $dllName = "$($packageName).dll"
    }

    $package = Get-Package $packageName -ErrorAction SilentlyContinue
    if (-not $package -or $force.IsPresent) {
        if ($isAdmin) {
            Write-Host "Installing module: $packageName"
            Install-Package -Name $packageName -Force:$force.IsPresent -Source $source -SkipDependencies:$SkipDependencies.IsPresent
        } else {
            Write-Host "Installing module: $packageName in CurrentUser scope"
            Install-Package -Name $packageName -Force:$force.IsPresent -scope CurrentUser -Source $source -SkipDependencies:$SkipDependencies.IsPresent
        }
        $package = Get-Package $packageName -ErrorAction SilentlyContinue
    }

    $path = get-childItem ($package.Source | Split-Path -Parent) -Filter $dllName -Recurse `
        | Where-Object { $_.FullName -match $folderMatch } `
        | Sort-Object -Descending { $_.FullName } `
        | Select-Object -First 1 -ExpandProperty FullName

    #$asm = [Reflection.Assembly]::LoadFile($path)
    Add-Type -Path $path | Out-Null
    return $path
}

function FileToBinString ([string]$fileName, $chunkSize = 2500 ) {
    $byteArray = Get-Content $fileName -Encoding Byte -ReadCount $chunkSize

    $line = New-Object System.Text.StringBuilder (($chunkSize * 2) + 2)
    $clr = New-Object System.Text.StringBuilder (($chunkSize * 2) * $byteArray.Length)

    foreach ($bytes in $byteArray) {
        $line.Clear() | Out-Null
    
        foreach ($byte in $bytes) {
            $line.Append($byte.ToString("X2")) | Out-Null
        }
        if ($line.Length -ge ($chunkSize * 2)) {
            $line.Append("\") | Out-Null
        }
        $clr.AppendLine($line) | Out-Null
    }
    return $clr.ToString()
}

Write-Output "Installing package: Microsoft.SqlServer.DacFx.x64"
InstallPackage -packageName "Microsoft.SqlServer.DacFx.x64" -dllName "Microsoft.SqlServer.Dac.dll" -SkipDependencies | Out-Null

# we have to get the binary AFTER we sign it
Write-Output "Get contents of $pfxPath as binary string"
$certBinString = FileToBinString -fileName $pfxPath -chunkSize 50

Write-Output "Get contents of $binaryPath as binary string, now that it is signed"
$clrBinString = FileToBinString -fileName $binaryPath

$setupSql = @"
:setvar DatabaseName "$databaseName"
USE master
GO
IF DB_ID('$databaseName') IS NULL BEGIN
	CREATE DATABASE [$databaseName] -- create the database with all of the default options
END 
GO
IF NOT EXISTS (SELECT * FROM sys.certificates c WHERE c.name = 'SQLServerClrCert') BEGIN
	RAISERROR( 'CREATE CERTIFICATE [SQLServerClrCert]', 0, 1) WITH NOWAIT
    CREATE CERTIFICATE [SQLServerClrCert]
    FROM BINARY = 0x$($certBinString);
END
GO
IF NOT EXISTS (SELECT * FROM sys.server_principals sp WHERE sp.name = 'SQLServerClrLogin') BEGIN 
	RAISERROR( 'CREATE LOGIN [SQLServerClrLogin] FROM CERTIFICATE [SQLServerClrCert]', 0, 1) WITH NOWAIT
    CREATE LOGIN [SQLServerClrLogin]
    FROM CERTIFICATE [SQLServerClrCert];
END
GO
GRANT UNSAFE ASSEMBLY TO [SQLServerClrLogin];
GO
USE [`$(DatabaseName)];
GO
EXEC sp_configure @configname=clr_enabled, @configvalue=1
GO
RECONFIGURE
GO
"@

$dacOptions = [Microsoft.SqlServer.Dac.DacDeployOptions]::new()
$dacOptions.CreateNewDatabase = $false
$dacOptions.IncludeCompositeObjects = $true
$dacOptions.BlockOnPossibleDataLoss = $true
$dacOptions.NoAlterStatementsToChangeClrTypes = $true
$dacOptions.AllowDropBlockingAssemblies = $true
$dacOptions.CommentOutSetVarDeclarations = $false
$dacOptions.ScriptDatabaseOptions = $false
$dacOptions.AllowIncompatiblePlatform = $true
$dacOptions.VerifyCollationCompatibility = $false

$dacpacPathkage = [Microsoft.SqlServer.Dac.DacPackage]::Load($dacpacPath)

Write-Output "Generate the create sql script"
$script = [Microsoft.SqlServer.Dac.DacServices]::GenerateCreateScript($dacpacPathkage, $databaseName, $dacOptions)

Write-Output "Replace the schema creates with if not exist create"
$script = $script -ireplace "(?im:^\bCREATE\b\s*\bSCHEMA\b\s*\[([A-z]+)\]\s*[\w\W\s.]*?;)", "IF SCHEMA_ID('`$1') IS NULL BEGIN EXEC('CREATE SCHEMA [`$1];') END";

Write-Output "Replace the create assembly statement with ours"
$script = $script -ireplace "CREATE\s+ASSEMBLY\s+(\[.*?\])[\w\W]*?GO", "CREATE ASSEMBLY `$1`r`n`tAUTHORIZATION [dbo]`r`n`tFROM 0x$($clrBinString)`r`n`r`nGO`r`n"

Write-Output "Remove the alter assembly statement. Not needed and only bloats the script"
$script = $script -ireplace "ALTER\s+ASSEMBLY[\w\W]*?GO", ""

Write-Output "Set up script written to '$rootPath\$databaseName-setup.sql'"
[System.IO.File]::WriteAllText("$rootPath\$databaseName-setup.sql", "$setupSql`r`n$script")
