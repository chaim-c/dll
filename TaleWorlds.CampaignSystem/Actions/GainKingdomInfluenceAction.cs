using System;
using Helpers;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x02000442 RID: 1090
	public static class GainKingdomInfluenceAction
	{
		// Token: 0x060040BD RID: 16573 RVA: 0x0013F0B0 File Offset: 0x0013D2B0
		private static void ApplyInternal(Hero hero, MobileParty party, float gainedInfluence, GainKingdomInfluenceAction.InfluenceGainingReason detail)
		{
			Clan clan = null;
			if (hero != null)
			{
				if (hero.CompanionOf != null)
				{
					clan = hero.CompanionOf;
				}
				else if (hero.Clan != null)
				{
					clan = hero.Clan;
				}
			}
			else if (party.ActualClan != null)
			{
				clan = party.ActualClan;
			}
			else if (party.Owner != null)
			{
				clan = party.Owner.Clan;
			}
			if (clan == null || clan.Kingdom == null)
			{
				return;
			}
			MobileParty mobileParty = party ?? hero.PartyBelongedTo;
			if (detail != GainKingdomInfluenceAction.InfluenceGainingReason.BeingAtArmy && detail == GainKingdomInfluenceAction.InfluenceGainingReason.ClanSupport)
			{
				gainedInfluence = 0.5f;
			}
			if (detail != GainKingdomInfluenceAction.InfluenceGainingReason.Default && detail != GainKingdomInfluenceAction.InfluenceGainingReason.GivingFood && detail != GainKingdomInfluenceAction.InfluenceGainingReason.JoinFaction && detail != GainKingdomInfluenceAction.InfluenceGainingReason.ClanSupport && ((Kingdom)clan.MapFaction).ActivePolicies.Contains(DefaultPolicies.MilitaryCoronae))
			{
				gainedInfluence *= 1.2f;
			}
			ExplainedNumber explainedNumber = new ExplainedNumber(gainedInfluence, false, null);
			if (detail == GainKingdomInfluenceAction.InfluenceGainingReason.Battle && gainedInfluence > 0f)
			{
				PerkHelper.AddPerkBonusForParty(DefaultPerks.Tactics.PreBattleManeuvers, mobileParty, true, ref explainedNumber);
			}
			if (detail == GainKingdomInfluenceAction.InfluenceGainingReason.CaptureSettlement && (hero != null || mobileParty.LeaderHero != null))
			{
				Hero hero2 = hero ?? mobileParty.LeaderHero;
				PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Tactics.Besieged, hero2.CharacterObject, false, ref explainedNumber);
			}
			ChangeClanInfluenceAction.Apply(clan, explainedNumber.ResultNumber);
			if (detail == GainKingdomInfluenceAction.InfluenceGainingReason.Battle && hero == Hero.MainHero)
			{
				MBTextManager.SetTextVariable("INFLUENCE", (int)gainedInfluence);
				MBTextManager.SetTextVariable("NEW_INFLUENCE", (int)clan.Influence);
				InformationManager.DisplayMessage(new InformationMessage(GameTexts.FindText("str_influence_gain_message", null).ToString()));
			}
			if (detail == GainKingdomInfluenceAction.InfluenceGainingReason.SiegeSafePassage && hero == Hero.MainHero)
			{
				MBTextManager.SetTextVariable("INFLUENCE", -(int)gainedInfluence);
				InformationManager.DisplayMessage(new InformationMessage(GameTexts.FindText("str_leave_siege_lose_influence_message", null).ToString()));
			}
		}

		// Token: 0x060040BE RID: 16574 RVA: 0x0013F23C File Offset: 0x0013D43C
		public static void ApplyForBattle(Hero hero, float value)
		{
			GainKingdomInfluenceAction.ApplyInternal(hero, null, value, GainKingdomInfluenceAction.InfluenceGainingReason.Battle);
		}

		// Token: 0x060040BF RID: 16575 RVA: 0x0013F247 File Offset: 0x0013D447
		public static void ApplyForGivingFood(Hero hero1, Hero hero2, float value)
		{
			GainKingdomInfluenceAction.ApplyInternal(hero1, null, value, GainKingdomInfluenceAction.InfluenceGainingReason.GivingFood);
			GainKingdomInfluenceAction.ApplyInternal(hero2, null, -value, GainKingdomInfluenceAction.InfluenceGainingReason.GivingFood);
		}

		// Token: 0x060040C0 RID: 16576 RVA: 0x0013F25C File Offset: 0x0013D45C
		public static void ApplyForDefault(Hero hero, float value)
		{
			GainKingdomInfluenceAction.ApplyInternal(hero, null, value, GainKingdomInfluenceAction.InfluenceGainingReason.Default);
		}

		// Token: 0x060040C1 RID: 16577 RVA: 0x0013F267 File Offset: 0x0013D467
		public static void ApplyForJoiningFaction(Hero hero, float value)
		{
			GainKingdomInfluenceAction.ApplyInternal(hero, null, value, GainKingdomInfluenceAction.InfluenceGainingReason.JoinFaction);
		}

		// Token: 0x060040C2 RID: 16578 RVA: 0x0013F272 File Offset: 0x0013D472
		public static void ApplyForDonatePrisoners(MobileParty donatingParty, float value)
		{
			GainKingdomInfluenceAction.ApplyInternal(null, donatingParty, value, GainKingdomInfluenceAction.InfluenceGainingReason.DonatePrisoners);
		}

		// Token: 0x060040C3 RID: 16579 RVA: 0x0013F27E File Offset: 0x0013D47E
		public static void ApplyForRaidingEnemyVillage(MobileParty side1Party, float value)
		{
			GainKingdomInfluenceAction.ApplyInternal(null, side1Party, value, GainKingdomInfluenceAction.InfluenceGainingReason.Raiding);
		}

		// Token: 0x060040C4 RID: 16580 RVA: 0x0013F289 File Offset: 0x0013D489
		public static void ApplyForBesiegingEnemySettlement(MobileParty side1Party, float value)
		{
			GainKingdomInfluenceAction.ApplyInternal(null, side1Party, value, GainKingdomInfluenceAction.InfluenceGainingReason.Besieging);
		}

		// Token: 0x060040C5 RID: 16581 RVA: 0x0013F294 File Offset: 0x0013D494
		public static void ApplyForSiegeSafePassageBarter(MobileParty side1Party, float value)
		{
			GainKingdomInfluenceAction.ApplyInternal(null, side1Party, value, GainKingdomInfluenceAction.InfluenceGainingReason.SiegeSafePassage);
		}

		// Token: 0x060040C6 RID: 16582 RVA: 0x0013F2A0 File Offset: 0x0013D4A0
		public static void ApplyForCapturingEnemySettlement(MobileParty side1Party, float value)
		{
			GainKingdomInfluenceAction.ApplyInternal(null, side1Party, value, GainKingdomInfluenceAction.InfluenceGainingReason.CaptureSettlement);
		}

		// Token: 0x060040C7 RID: 16583 RVA: 0x0013F2AB File Offset: 0x0013D4AB
		public static void ApplyForLeavingTroopToGarrison(Hero hero, float value)
		{
			GainKingdomInfluenceAction.ApplyInternal(hero, null, value, GainKingdomInfluenceAction.InfluenceGainingReason.LeaveGarrison);
		}

		// Token: 0x060040C8 RID: 16584 RVA: 0x0013F2B6 File Offset: 0x0013D4B6
		public static void ApplyForBoardGameWon(Hero hero, float value)
		{
			GainKingdomInfluenceAction.ApplyInternal(hero, null, value, GainKingdomInfluenceAction.InfluenceGainingReason.BoardGameWon);
		}

		// Token: 0x02000771 RID: 1905
		private enum InfluenceGainingReason
		{
			// Token: 0x04001F20 RID: 7968
			Default,
			// Token: 0x04001F21 RID: 7969
			BeingAtArmy,
			// Token: 0x04001F22 RID: 7970
			Battle,
			// Token: 0x04001F23 RID: 7971
			Raiding,
			// Token: 0x04001F24 RID: 7972
			Besieging,
			// Token: 0x04001F25 RID: 7973
			CaptureSettlement,
			// Token: 0x04001F26 RID: 7974
			JoinFaction,
			// Token: 0x04001F27 RID: 7975
			GivingFood,
			// Token: 0x04001F28 RID: 7976
			LeaveGarrison,
			// Token: 0x04001F29 RID: 7977
			BoardGameWon,
			// Token: 0x04001F2A RID: 7978
			ClanSupport,
			// Token: 0x04001F2B RID: 7979
			DonatePrisoners,
			// Token: 0x04001F2C RID: 7980
			SiegeSafePassage
		}
	}
}
