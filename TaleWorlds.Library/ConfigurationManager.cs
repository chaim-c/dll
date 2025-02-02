using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000023 RID: 35
	public static class ConfigurationManager
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x00005218 File Offset: 0x00003418
		public static void SetConfigurationManager(IConfigurationManager configurationManager)
		{
			ConfigurationManager._configurationManager = configurationManager;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005220 File Offset: 0x00003420
		public static string GetAppSettings(string name)
		{
			if (ConfigurationManager._configurationManager != null)
			{
				return ConfigurationManager._configurationManager.GetAppSettings(name);
			}
			return null;
		}

		// Token: 0x0400006A RID: 106
		private static IConfigurationManager _configurationManager;
	}
}
