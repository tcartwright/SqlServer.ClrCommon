param (
    [Parameter(Mandatory=$true)]
    [string]
    $serverName,

    [Parameter(Mandatory=$true)]
    [string]
    [AllowEmptyString()]
    $databaseName 
)
Clear-Host

if (!$databaseName) {
    $databaseName = "CommonDB"
}
try {
    $scriptRoot = [System.IO.Directory]::GetParent($MyInvocation.MyCommand.Definition).FullName
    Set-Location $scriptRoot

    $version = (Invoke-Sqlcmd -ServerInstance $serverName -Database "master" -Query "SELECT [ProductMajorVersion] = SERVERPROPERTY('ProductMajorVersion')").ProductMajorVersion

    $variables = @(
        "DatabaseName = $databaseName", 
        "AssemblyName = SqlServer.ClrCommon"
    )

    $createDB = "USE master 
    GO
    IF DB_ID('`$(DatabaseName)') IS NULL BEGIN 
	    PRINT 'CREATE DATABASE [`$(DatabaseName)]'
	    CREATE DATABASE [`$(DatabaseName)] 
    END ELSE BEGIN
        PRINT 'DATABASE [`$(DatabaseName)] ALEADY EXISTS!'
    END
    GO"

    Invoke-Sqlcmd -ServerInstance $serverName -Database "master" -Query $createDB -Verbose -Variable $variables

    Invoke-Sqlcmd -ServerInstance $serverName -Database "master" -InputFile "$scriptRoot\drop-assembly.sql" -Verbose -Variable $variables

    if ($version -le 10) {
        $inputFile = "$scriptRoot\SqlClrCommon-2008-and-lower-setup.sql"
    } else {
        $inputFile = "$scriptRoot\SqlClrCommon-2012-and-greater-setup.sql"
    }

    Write-Output "Installing using $inputFile"
    Invoke-Sqlcmd -ServerInstance $serverName -Database "master" -InputFile $inputFile -Verbose  -Variable $variables
} finally { 
    # just bail
}
