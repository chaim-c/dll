using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.Extensions
{
	// Token: 0x02000150 RID: 336
	public static class SiegeEngineTypes
	{
		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001867 RID: 6247 RVA: 0x0007C64E File Offset: 0x0007A84E
		public static MBReadOnlyList<SiegeEngineType> All
		{
			get
			{
				return Campaign.Current.AllSiegeEngineTypes;
			}
		}
	}
}
