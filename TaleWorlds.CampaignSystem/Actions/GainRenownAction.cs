using System;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x02000443 RID: 1091
	public static class GainRenownAction
	{
		// Token: 0x060040C9 RID: 16585 RVA: 0x0013F2C2 File Offset: 0x0013D4C2
		private static void ApplyInternal(Hero hero, float gainedRenown, bool doNotNotify)
		{
			if (gainedRenown > 0f)
			{
				hero.Clan.AddRenown(gainedRenown, true);
				CampaignEventDispatcher.Instance.OnRenownGained(hero, (int)gainedRenown, doNotNotify);
			}
		}

		// Token: 0x060040CA RID: 16586 RVA: 0x0013F2E7 File Offset: 0x0013D4E7
		public static void Apply(Hero hero, float renownValue, bool doNotNotify = false)
		{
			GainRenownAction.ApplyInternal(hero, renownValue, doNotNotify);
		}
	}
}
