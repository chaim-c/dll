using System;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x0200042D RID: 1069
	public static class ChangeGovernorAction
	{
		// Token: 0x0600405B RID: 16475 RVA: 0x0013D558 File Offset: 0x0013B758
		private static void ApplyInternal(Town fortification, Hero governor)
		{
			Hero governor2 = fortification.Governor;
			if (governor == null)
			{
				fortification.Governor = null;
			}
			else if (governor.CurrentSettlement == fortification.Settlement && !governor.IsPrisoner)
			{
				fortification.Governor = governor;
				TeleportHeroAction.ApplyImmediateTeleportToSettlement(governor, fortification.Settlement);
			}
			else
			{
				fortification.Governor = null;
				TeleportHeroAction.ApplyDelayedTeleportToSettlementAsGovernor(governor, fortification.Settlement);
			}
			if (governor2 != null)
			{
				governor2.GovernorOf = null;
			}
			CampaignEventDispatcher.Instance.OnGovernorChanged(fortification, governor2, governor);
			if (governor != null)
			{
				CampaignEventDispatcher.Instance.OnHeroGetsBusy(governor, HeroGetsBusyReasons.BecomeGovernor);
			}
		}

		// Token: 0x0600405C RID: 16476 RVA: 0x0013D5E0 File Offset: 0x0013B7E0
		private static void ApplyGiveUpInternal(Hero governor)
		{
			Town governorOf = governor.GovernorOf;
			governorOf.Governor = null;
			governor.GovernorOf = null;
			CampaignEventDispatcher.Instance.OnGovernorChanged(governorOf, governor, null);
		}

		// Token: 0x0600405D RID: 16477 RVA: 0x0013D60F File Offset: 0x0013B80F
		public static void Apply(Town fortification, Hero governor)
		{
			ChangeGovernorAction.ApplyInternal(fortification, governor);
		}

		// Token: 0x0600405E RID: 16478 RVA: 0x0013D618 File Offset: 0x0013B818
		public static void RemoveGovernorOf(Hero governor)
		{
			ChangeGovernorAction.ApplyGiveUpInternal(governor);
		}

		// Token: 0x0600405F RID: 16479 RVA: 0x0013D620 File Offset: 0x0013B820
		public static void RemoveGovernorOfIfExists(Town town)
		{
			if (town.Governor != null)
			{
				ChangeGovernorAction.ApplyGiveUpInternal(town.Governor);
			}
		}
	}
}
