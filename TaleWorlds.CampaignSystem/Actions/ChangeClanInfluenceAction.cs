using System;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x0200042A RID: 1066
	public static class ChangeClanInfluenceAction
	{
		// Token: 0x06004054 RID: 16468 RVA: 0x0013D263 File Offset: 0x0013B463
		private static void ApplyInternal(Clan clan, float amount)
		{
			clan.Influence += amount;
			CampaignEventDispatcher.Instance.OnClanInfluenceChanged(clan, amount);
		}

		// Token: 0x06004055 RID: 16469 RVA: 0x0013D27F File Offset: 0x0013B47F
		public static void Apply(Clan clan, float amount)
		{
			ChangeClanInfluenceAction.ApplyInternal(clan, amount);
		}
	}
}
