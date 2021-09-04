param (
    [Parameter(Mandatory=$true)]
    [string]
    $serverName,
    [string]
    $databaseName = "CommonDB"
)

$scriptRoot = [System.IO.Directory]::GetParent($MyInvocation.MyCommand.Definition).FullName
Set-Location $scriptRoot

$version = (Invoke-Sqlcmd -ServerInstance $serverName -Database "master" -Query "SELECT [ProductMajorVersion] = SERVERPROPERTY('ProductMajorVersion')").ProductMajorVersion

$variables = "DatabaseName='$databaseName'"
Invoke-Sqlcmd -ServerInstance $serverName -Database "master" -InputFile "$scriptRoot\setup-start.sql" -Variable $variables

if ($version -le 11) {
    Invoke-Sqlcmd -ServerInstance $serverName -Database "master" -InputFile "$scriptRoot\CommonDB-2012-and-lower-setup.sql" -Variable $variables
} else {
    Invoke-Sqlcmd -ServerInstance $serverName -Database "master" -InputFile "$scriptRoot\CommonDB-2014-and-greater-setup.sql" -Variable $variables
}

