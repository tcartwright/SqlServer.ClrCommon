using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using Microsoft.SqlServer.Server;


public partial class UserDefinedFunctions
{
	/// <summary>
	/// Returns the directory information for the specified path string.
	/// </summary>
	/// <param name="path">The path.</param>
	/// <returns>
	/// Directory information for path, or null if path denotes a root directory or is null. Returns String.Empty if path does not contain directory information.
	/// </returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlString xfnPathGetDirectoryName([SqlFacet(MaxSize = 2000)] SqlString path)
	{
		if (path.IsNull || SqlServer.ClrCommon.Utils.IsNullOrWhiteSpace(path.Value)) return SqlString.Null;

		return new SqlString(Path.GetDirectoryName(path.Value));
	}

	/// <summary>
	/// Returns the file name and extension of the specified path string.
	/// </summary>
	/// <param name="path">The path.</param>
	/// <returns>
	/// The characters after the last directory character in path. If the last character of path is a directory or volume separator character, this method returns String.Empty. If path is null, this method returns null.
	/// </returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlString xfnPathGetFileName([SqlFacet(MaxSize = 2000)] SqlString path)
	{
		if (path.IsNull || SqlServer.ClrCommon.Utils.IsNullOrWhiteSpace(path.Value)) return SqlString.Null;
		return new SqlString(Path.GetFileName(path.Value));
	}

	/// <summary>
	/// Returns the file name of the specified path string without the extension.
	/// </summary>
	/// <param name="path">The path.</param>
	/// <returns>
	/// The string returned by GetFileName, minus the last period (.) and all characters following it.
	/// </returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlString xfnPathGetFileNameWithoutExtension([SqlFacet(MaxSize = 2000)] SqlString path)
	{
		if (path.IsNull || SqlServer.ClrCommon.Utils.IsNullOrWhiteSpace(path.Value)) return SqlString.Null;

		return new SqlString(Path.GetFileNameWithoutExtension(path.Value));
	}
	/// <summary>
	/// Returns the extension of the specified path string.
	/// </summary>
	/// <param name="path">The path.</param>
	/// <returns>
	/// The extension for the path.
	/// </returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlString xfnPathGetGetExtension([SqlFacet(MaxSize = 2000)] SqlString path)
	{
		if (path.IsNull || SqlServer.ClrCommon.Utils.IsNullOrWhiteSpace(path.Value)) return SqlString.Null;

		return new SqlString(Path.GetExtension(path.Value));
	}
	/// <summary>
	/// Combines strings into a path.
	/// </summary>
	/// <param name="path">The path.</param>
	/// <param name="fileName">Name of the file.</param>
	/// <returns>
	/// The combined paths. If one of the specified paths is a zero-length string, this method returns the other path. If path2 contains an absolute path, this method returns path2.
	/// </returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlString xfnPathCombine([SqlFacet(MaxSize = 2000)] SqlString path, [SqlFacet(MaxSize = 500)] SqlString fileName)
	{
		if (path.IsNull || SqlServer.ClrCommon.Utils.IsNullOrWhiteSpace(path.Value))
        {
            return SqlString.Null;
        }

        return new SqlString(Path.Combine(path.Value, fileName.Value));
	}
	/// <summary>
	/// Gets the root directory information of the specified path.
	/// </summary>
	/// <param name="path">The path.</param>
	/// <returns>
	/// The root directory of path, such as "C:\" or "\\server\sharename", or null if path is null, or an empty string if path does not contain root directory information.
	/// </returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlString xfnPathGetPathRoot([SqlFacet(MaxSize = 2000)] SqlString path)
	{
		if (path.IsNull || SqlServer.ClrCommon.Utils.IsNullOrWhiteSpace(path.Value)) return SqlString.Null;

		return new SqlString(Path.GetPathRoot(path.Value));
	}
	/// <summary>
	/// Gets a value indicating whether the specified path string contains a root.
	/// </summary>
	/// <param name="path">The path.</param>
	/// <returns>
	/// 1 if path contains a root; otherwise, 0.
	/// </returns>
	[SqlFunction(IsDeterministic = true, IsPrecise = true, SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlBoolean xfnPathIsPathRooted([SqlFacet(MaxSize = 2000)] SqlString path)
	{
		if (path.IsNull || SqlServer.ClrCommon.Utils.IsNullOrWhiteSpace(path.Value)) return false;

		return Path.IsPathRooted(path.Value);
	}
	/// <summary>
	/// Returns a random folder name or file name.
	/// </summary>
	/// <returns>
	/// A random folder name or file name.
	/// </returns>
	/// <remarks>
	/// The GetRandomFileName method returns a cryptographically strong, random string that can be used as either a folder name or a file name. Unlike GetTempFileName, GetRandomFileName does not create a file. When the security of your file system is paramount, this method should be used instead of GetTempFileName.
	/// </remarks>
	[SqlFunction(SystemDataAccess = SystemDataAccessKind.None)]
	public static SqlString xfnPathGetRandomFileName()
	{
		return new SqlString(Path.GetRandomFileName());
	}


}
