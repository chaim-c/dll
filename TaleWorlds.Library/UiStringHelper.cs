using System;

namespace TaleWorlds.Library
{
	// Token: 0x0200002E RID: 46
	public static class UiStringHelper
	{
		// Token: 0x06000194 RID: 404 RVA: 0x00006938 File Offset: 0x00004B38
		public static bool IsStringNoneOrEmptyForUi(string str)
		{
			return string.IsNullOrEmpty(str) || str == "none";
		}
	}
}
