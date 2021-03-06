Why this was done: 
	SQL Server 2008 is tied to .net 3.5. Even if higher frameworks are installed on the sql server host machine sql server will not use them. Sql Server 2012 and on
	can use .net framework 4/4.5. So when targeting sql server 2008 the project must be built and published for .net framework 3.5 and sql server 2012 and on can be 
	built against .net framework 4.5.

NOTE: The project should already be properly set up. This is a guideline to how it was done in case it needs to be reproduced. This might possibly happen if the project 
build properties are modified through the designer.

How to set up the project build to both 3.5 and 4.51:
1) Add two new build configurations named Debug4.5 (based off of Debug) and Release4.5 (based off of Release) to the solution if they do not already exist. Choose 
to have them create project builds as well.

They will look like this in the project *.sqlproj file:

	<PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug4.5|AnyCPU' ">
		<OutputPath>bin\Debug4.5\</OutputPath>
		<BuildScriptName>$(MSBuildProjectName).sql</BuildScriptName>
		<TreatWarningsAsErrors>false</TreatWarningsAsErrors>
		<DebugSymbols>true</DebugSymbols>
		<DebugType>full</DebugType>
		<Optimize>false</Optimize>
		<DefineDebug>true</DefineDebug>
		<DefineTrace>true</DefineTrace>
		<ErrorReport>prompt</ErrorReport>
		<WarningLevel>4</WarningLevel>
		<DefineConstants>DOTNET45</DefineConstants>
	</PropertyGroup>
	<PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release4.5|AnyCPU' ">
		<OutputPath>bin\Release4.5\</OutputPath>
		<BuildScriptName>$(MSBuildProjectName).sql</BuildScriptName>
		<TreatWarningsAsErrors>False</TreatWarningsAsErrors>
		<DebugType>none</DebugType>
		<Optimize>true</Optimize>
		<DefineDebug>false</DefineDebug>
		<DefineTrace>true</DefineTrace>
		<ErrorReport>prompt</ErrorReport>
		<WarningLevel>4</WarningLevel>
		<DocumentationFile>bin\Release\SqlServer.ClrCommon.xml</DocumentationFile>
		<DefineConstants>DOTNET45</DefineConstants>
	</PropertyGroup>

3) Add the conditional compilation constant <DefineConstants>DOTNET45</DefineConstants> to each of the new builds. Change the OutputPath to a new path.


4) In the default property group, find and replace the <TargetFrameworkVersion> element with the below two lines. The target framework has to be 
handled in this manner otherwise the publish will not work properly. Also the platform must not be part of the conditional for the same reason.

	<TargetFrameworkVersion Condition=" '$(Configuration)' == 'Debug' OR '$(Configuration)' == 'Release' ">v3.5</TargetFrameworkVersion>
	<TargetFrameworkVersion Condition=" '$(Configuration)' == 'Debug4.5' OR '$(Configuration)' == 'Release4.5' ">v4.5.1</TargetFrameworkVersion>

5) Add conditional compilation statements as needed to code using the DOTNET45 constant defined in the build configurations.
Example:
#if DOTNET45
		return Regex.IsMatch(input.Value, @"^((?!0{5})(?:\d{5})(?!-?0{4})(?:|-\d{4})?)$", Options, RegexTimeout);
#else
		return Regex.IsMatch(input.Value, @"^((?!0{5})(?:\d{5})(?!-?0{4})(?:|-\d{4})?)$", Options);
#endif
