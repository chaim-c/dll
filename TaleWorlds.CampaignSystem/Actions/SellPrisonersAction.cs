using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x02000456 RID: 1110
	public static class SellPrisonersAction
	{
		// Token: 0x0600410A RID: 16650 RVA: 0x001413B8 File Offset: 0x0013F5B8
		private static void ApplyInternal(PartyBase sellerParty, PartyBase buyerParty, TroopRoster prisoners, bool applyConsequences)
		{
			Settlement settlement = sellerParty.Settlement ?? ((buyerParty != null) ? buyerParty.Settlement : null);
			TroopRoster troopRoster = TroopRoster.CreateDummyTroopRoster();
			int num = 0;
			foreach (TroopRosterElement troopRosterElement in prisoners.GetTroopRoster())
			{
				CharacterObject character = troopRosterElement.Character;
				bool flag = false;
				if (!character.IsHero)
				{
					if (applyConsequences)
					{
						sellerParty.PrisonRoster.AddToCounts(character, -troopRosterElement.Number, false, -troopRosterElement.WoundedNumber, 0, true, -1);
					}
				}
				else if (character.HeroObject != Hero.MainHero)
				{
					if (buyerParty != null)
					{
						if (!buyerParty.MapFaction.IsAtWarWith(character.HeroObject.MapFaction))
						{
							if (character.HeroObject.Clan == Clan.PlayerClan)
							{
								EndCaptivityAction.ApplyByReleasedByCompensation(character.HeroObject);
							}
							else
							{
								EndCaptivityAction.ApplyByRansom(character.HeroObject, null);
							}
						}
						else
						{
							if (sellerParty.MapFaction == buyerParty.MapFaction && sellerParty != PartyBase.MainParty)
							{
								flag = true;
								troopRoster.Add(troopRosterElement);
							}
							TransferPrisonerAction.Apply(character, sellerParty, buyerParty);
						}
					}
					if (settlement != null)
					{
						CampaignEventDispatcher.Instance.OnPrisonersChangeInSettlement(settlement, null, character.HeroObject, false);
					}
				}
				if (applyConsequences && !flag && character != Hero.MainHero.CharacterObject)
				{
					int num2 = Campaign.Current.Models.RansomValueCalculationModel.PrisonerRansomValue(character, (sellerParty != null) ? sellerParty.LeaderHero : null);
					num += troopRosterElement.Number * num2;
				}
			}
			if (applyConsequences)
			{
				if (sellerParty.IsMobile)
				{
					GiveGoldAction.ApplyBetweenCharacters(null, sellerParty.LeaderHero ?? sellerParty.Owner, num, false);
				}
				else
				{
					PartyBase giverParty = null;
					Settlement settlement2 = sellerParty.Settlement;
					int amount = num;
					Clan clan;
					if (buyerParty == null)
					{
						clan = null;
					}
					else
					{
						Settlement settlement3 = buyerParty.Settlement;
						clan = ((settlement3 != null) ? settlement3.OwnerClan : null);
					}
					GiveGoldAction.ApplyForPartyToSettlement(giverParty, settlement2, amount, clan != Clan.PlayerClan);
				}
			}
			if (sellerParty.IsMobile)
			{
				SkillLevelingManager.OnPrisonerSell(sellerParty.MobileParty, prisoners);
			}
			CampaignEventDispatcher.Instance.OnPrisonerSold(sellerParty, buyerParty, prisoners);
			if (settlement != null && troopRoster.Count > 0)
			{
				CampaignEventDispatcher.Instance.OnPrisonerDonatedToSettlement(sellerParty.MobileParty, troopRoster.ToFlattenedRoster(), settlement);
			}
		}

		// Token: 0x0600410B RID: 16651 RVA: 0x001415F8 File Offset: 0x0013F7F8
		public static void ApplyForAllPrisoners(PartyBase sellerParty, PartyBase buyerParty)
		{
			SellPrisonersAction.ApplyInternal(sellerParty, buyerParty, sellerParty.PrisonRoster.CloneRosterData(), true);
		}

		// Token: 0x0600410C RID: 16652 RVA: 0x0014160D File Offset: 0x0013F80D
		public static void ApplyForSelectedPrisoners(PartyBase sellerParty, PartyBase buyerParty, TroopRoster prisoners)
		{
			SellPrisonersAction.ApplyInternal(sellerParty, buyerParty, prisoners, true);
		}

		// Token: 0x0600410D RID: 16653 RVA: 0x00141618 File Offset: 0x0013F818
		public static void ApplyByPartyScreen(TroopRoster prisoners)
		{
			SellPrisonersAction.ApplyInternal(PartyBase.MainParty, Hero.MainHero.CurrentSettlement.Party, prisoners, false);
		}
	}
}
