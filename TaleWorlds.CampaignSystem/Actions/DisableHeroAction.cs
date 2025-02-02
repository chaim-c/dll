using System;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x0200043C RID: 1084
	public static class DisableHeroAction
	{
		// Token: 0x060040A0 RID: 16544 RVA: 0x0013EA28 File Offset: 0x0013CC28
		private static void ApplyInternal(Hero hero)
		{
			if (hero.IsAlive)
			{
				if (hero.PartyBelongedTo != null)
				{
					if (hero.PartyBelongedTo.LeaderHero == hero)
					{
						DestroyPartyAction.Apply(null, hero.PartyBelongedTo);
					}
					else
					{
						hero.PartyBelongedTo.MemberRoster.RemoveTroop(hero.CharacterObject, 1, default(UniqueTroopDescriptor), 0);
					}
				}
				if (hero.StayingInSettlement != null)
				{
					hero.ChangeState(Hero.CharacterStates.Disabled);
					hero.StayingInSettlement = null;
				}
				if (hero.CurrentSettlement != null)
				{
					LeaveSettlementAction.ApplyForCharacterOnly(hero);
				}
				if (hero.IsPrisoner)
				{
					EndCaptivityAction.ApplyByEscape(hero, null);
				}
				hero.ChangeState(Hero.CharacterStates.Disabled);
			}
		}

		// Token: 0x060040A1 RID: 16545 RVA: 0x0013EABC File Offset: 0x0013CCBC
		public static void Apply(Hero hero)
		{
			DisableHeroAction.ApplyInternal(hero);
		}
	}
}
