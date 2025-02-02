using System;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x02000425 RID: 1061
	public static class AdoptHeroAction
	{
		// Token: 0x06004043 RID: 16451 RVA: 0x0013C6CB File Offset: 0x0013A8CB
		private static void ApplyInternal(Hero adoptedHero)
		{
			if (Hero.MainHero.IsFemale)
			{
				adoptedHero.Mother = Hero.MainHero;
			}
			else
			{
				adoptedHero.Father = Hero.MainHero;
			}
			adoptedHero.Clan = Clan.PlayerClan;
		}

		// Token: 0x06004044 RID: 16452 RVA: 0x0013C6FC File Offset: 0x0013A8FC
		public static void Apply(Hero adoptedHero)
		{
			AdoptHeroAction.ApplyInternal(adoptedHero);
		}
	}
}
