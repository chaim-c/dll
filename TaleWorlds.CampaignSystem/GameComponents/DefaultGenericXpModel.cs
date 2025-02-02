using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.ComponentInterfaces;

namespace TaleWorlds.CampaignSystem.GameComponents
{
	// Token: 0x02000109 RID: 265
	public class DefaultGenericXpModel : GenericXpModel
	{
		// Token: 0x060015D9 RID: 5593 RVA: 0x000681F6 File Offset: 0x000663F6
		public override float GetXpMultiplier(Hero hero)
		{
			if (hero.IsPlayerCompanion && Hero.MainHero.GetPerkValue(DefaultPerks.Charm.NaturalLeader))
			{
				return 1.2f;
			}
			return 1f;
		}
	}
}
