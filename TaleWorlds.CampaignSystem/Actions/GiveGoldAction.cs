using System;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x02000445 RID: 1093
	public static class GiveGoldAction
	{
		// Token: 0x060040CD RID: 16589 RVA: 0x0013F338 File Offset: 0x0013D538
		private static void ApplyInternal(Hero giverHero, PartyBase giverParty, Hero recipientHero, PartyBase recipientParty, int goldAmount, bool showQuickInformation, string transactionStringId = "")
		{
			if (giverHero != null)
			{
				goldAmount = MathF.Min(giverHero.Gold, goldAmount);
				giverHero.ChangeHeroGold(-goldAmount);
			}
			else if (giverParty != null && giverParty.IsMobile)
			{
				goldAmount = MathF.Min(giverParty.MobileParty.PartyTradeGold, goldAmount);
				giverParty.MobileParty.PartyTradeGold -= goldAmount;
			}
			else if (giverParty != null && giverParty.IsSettlement)
			{
				SettlementComponent settlementComponent = giverParty.Settlement.SettlementComponent;
				goldAmount = MathF.Min(settlementComponent.Gold, goldAmount);
				settlementComponent.ChangeGold(-goldAmount);
			}
			if (recipientHero != null)
			{
				recipientHero.ChangeHeroGold(goldAmount);
			}
			else if (recipientParty != null && recipientParty.IsMobile)
			{
				recipientParty.MobileParty.PartyTradeGold += goldAmount;
			}
			else if (recipientParty != null && recipientParty.IsSettlement)
			{
				recipientParty.Settlement.SettlementComponent.ChangeGold(goldAmount);
			}
			CampaignEventDispatcher.Instance.OnHeroOrPartyTradedGold(new ValueTuple<Hero, PartyBase>(giverHero, giverParty), new ValueTuple<Hero, PartyBase>(recipientHero, recipientParty), new ValueTuple<int, string>(goldAmount, transactionStringId), showQuickInformation);
		}

		// Token: 0x060040CE RID: 16590 RVA: 0x0013F432 File Offset: 0x0013D632
		public static void ApplyForQuestBetweenCharacters(Hero giverHero, Hero recipientHero, int amount, bool disableNotification = false)
		{
			GiveGoldAction.ApplyInternal(giverHero, null, recipientHero, null, amount, !disableNotification && (giverHero == Hero.MainHero || recipientHero == Hero.MainHero), "");
		}

		// Token: 0x060040CF RID: 16591 RVA: 0x0013F45C File Offset: 0x0013D65C
		public static void ApplyBetweenCharacters(Hero giverHero, Hero recipientHero, int amount, bool disableNotification = false)
		{
			GiveGoldAction.ApplyInternal(giverHero, null, recipientHero, null, amount, !disableNotification && (giverHero == Hero.MainHero || recipientHero == Hero.MainHero), "");
		}

		// Token: 0x060040D0 RID: 16592 RVA: 0x0013F486 File Offset: 0x0013D686
		public static void ApplyForCharacterToSettlement(Hero giverHero, Settlement settlement, int amount, bool disableNotification = false)
		{
			GiveGoldAction.ApplyInternal(giverHero, null, null, settlement.Party, amount, !disableNotification && giverHero == Hero.MainHero, "");
		}

		// Token: 0x060040D1 RID: 16593 RVA: 0x0013F4AA File Offset: 0x0013D6AA
		public static void ApplyForSettlementToCharacter(Settlement giverSettlement, Hero recipientHero, int amount, bool disableNotification = false)
		{
			GiveGoldAction.ApplyInternal(recipientHero, null, null, giverSettlement.Party, -amount, !disableNotification && recipientHero == Hero.MainHero, "");
		}

		// Token: 0x060040D2 RID: 16594 RVA: 0x0013F4CF File Offset: 0x0013D6CF
		public static void ApplyForSettlementToParty(Settlement giverSettlement, PartyBase recipientParty, int amount, bool disableNotification = false)
		{
			GiveGoldAction.ApplyInternal(null, giverSettlement.Party, null, recipientParty, amount, !disableNotification && recipientParty.LeaderHero == Hero.MainHero, "");
		}

		// Token: 0x060040D3 RID: 16595 RVA: 0x0013F4F8 File Offset: 0x0013D6F8
		public static void ApplyForPartyToSettlement(PartyBase giverParty, Settlement settlement, int amount, bool disableNotification = false)
		{
			GiveGoldAction.ApplyInternal(null, giverParty, null, settlement.Party, amount, !disableNotification && ((giverParty != null) ? giverParty.LeaderHero : null) == Hero.MainHero, "");
		}

		// Token: 0x060040D4 RID: 16596 RVA: 0x0013F527 File Offset: 0x0013D727
		public static void ApplyForPartyToCharacter(PartyBase giverParty, Hero recipientHero, int amount, bool disableNotification = false)
		{
			GiveGoldAction.ApplyInternal(null, giverParty, recipientHero, null, amount, !disableNotification && giverParty != null && (giverParty.LeaderHero == Hero.MainHero || recipientHero == Hero.MainHero), "");
		}

		// Token: 0x060040D5 RID: 16597 RVA: 0x0013F559 File Offset: 0x0013D759
		public static void ApplyForCharacterToParty(Hero giverHero, PartyBase receipentParty, int amount, bool disableNotification = false)
		{
			GiveGoldAction.ApplyInternal(giverHero, null, null, receipentParty, amount, !disableNotification && (giverHero == Hero.MainHero || receipentParty.LeaderHero == Hero.MainHero), "");
		}

		// Token: 0x060040D6 RID: 16598 RVA: 0x0013F588 File Offset: 0x0013D788
		public static void ApplyForPartyToParty(PartyBase giverParty, PartyBase receipentParty, int amount, bool disableNotification = false)
		{
			GiveGoldAction.ApplyInternal(null, giverParty, null, receipentParty, amount, !disableNotification && (giverParty.LeaderHero == Hero.MainHero || receipentParty.LeaderHero == Hero.MainHero), "");
		}
	}
}
