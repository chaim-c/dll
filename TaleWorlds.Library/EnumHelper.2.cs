using System;

namespace TaleWorlds.Library
{
	// Token: 0x0200002D RID: 45
	public static class EnumHelper
	{
		// Token: 0x06000192 RID: 402 RVA: 0x00006880 File Offset: 0x00004A80
		public static ulong GetCombinedULongEnumFlagsValue(Type type)
		{
			ulong num = 0UL;
			foreach (object obj in Enum.GetValues(type))
			{
				num |= (ulong)obj;
			}
			return num;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000068DC File Offset: 0x00004ADC
		public static uint GetCombinedUIntEnumFlagsValue(Type type)
		{
			uint num = 0U;
			foreach (object obj in Enum.GetValues(type))
			{
				num |= (uint)obj;
			}
			return num;
		}
	}
}
