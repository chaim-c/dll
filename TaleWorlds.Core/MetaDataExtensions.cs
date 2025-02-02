using System;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.Core
{
	// Token: 0x020000AD RID: 173
	public static class MetaDataExtensions
	{
		// Token: 0x06000888 RID: 2184 RVA: 0x0001CD64 File Offset: 0x0001AF64
		public static DateTime GetCreationTime(this MetaData metaData)
		{
			string text = (metaData != null) ? metaData["CreationTime"] : null;
			if (text != null)
			{
				DateTime result;
				if (DateTime.TryParse(text, out result))
				{
					return result;
				}
				long ticks;
				if (long.TryParse(text, out ticks))
				{
					return new DateTime(ticks);
				}
			}
			return DateTime.MinValue;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0001CDA8 File Offset: 0x0001AFA8
		public static ApplicationVersion GetApplicationVersion(this MetaData metaData)
		{
			string text = (metaData != null) ? metaData["ApplicationVersion"] : null;
			if (text == null)
			{
				return ApplicationVersion.Empty;
			}
			return ApplicationVersion.FromString(text, 45697);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0001CDDC File Offset: 0x0001AFDC
		public static string[] GetModules(this MetaData metaData)
		{
			string text;
			if (metaData == null || !metaData.TryGetValue("Modules", out text))
			{
				return new string[0];
			}
			return text.Split(new char[]
			{
				';'
			});
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0001CE14 File Offset: 0x0001B014
		public static ApplicationVersion GetModuleVersion(this MetaData metaData, string moduleName)
		{
			string key = "Module_" + moduleName;
			string versionAsString;
			if (metaData != null && metaData.TryGetValue(key, out versionAsString))
			{
				try
				{
					return ApplicationVersion.FromString(versionAsString, 45697);
				}
				catch (Exception ex)
				{
					Debug.FailedAssert(ex.Message, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.Core\\MetaDataExtensions.cs", "GetModuleVersion", 53);
				}
			}
			return ApplicationVersion.Empty;
		}
	}
}
