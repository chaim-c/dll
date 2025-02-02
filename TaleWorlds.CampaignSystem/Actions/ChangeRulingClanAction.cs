using System;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x02000435 RID: 1077
	public class ChangeRulingClanAction
	{
		// Token: 0x06004083 RID: 16515 RVA: 0x0013E355 File Offset: 0x0013C555
		private static void ApplyInternal(Kingdom kingdom, Clan clan)
		{
			kingdom.RulingClan = clan;
			CampaignEventDispatcher.Instance.OnRulingClanChanged(kingdom, clan);
		}

		// Token: 0x06004084 RID: 16516 RVA: 0x0013E36A File Offset: 0x0013C56A
		public static void Apply(Kingdom kingdom, Clan clan)
		{
			ChangeRulingClanAction.ApplyInternal(kingdom, clan);
		}
	}
}
