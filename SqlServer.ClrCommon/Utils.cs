using System;
using System.Collections.Generic;
using System.Text;

namespace SqlServer.ClrCommon
{
	internal class Utils
	{
		/// <summary>Indicates whether a specified string is null, empty, or consists only of white-space characters.</summary>
		/// <returns>true if the <paramref name="value" /> parameter is null or <see cref="F:System.String.Empty" />, or if <paramref name="value" /> consists exclusively of white-space characters. </returns>
		/// <param name="value">The string to test.</param>
		public static bool IsNullOrWhiteSpace(string value)
		{
			//borrowed from mscorlib system.string, recreated here so this can be used from 3.5 code as well as 4.0+
			if (value == null)
			{
				return true;
			}
			for (int i = 0; i < value.Length; i++)
			{
				if (!char.IsWhiteSpace(value[i]))
				{
					return false;
				}
			}
			return true;
		}
	}
}
