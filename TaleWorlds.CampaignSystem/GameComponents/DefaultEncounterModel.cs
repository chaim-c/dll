﻿using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.GameComponents
{
	// Token: 0x02000105 RID: 261
	public class DefaultEncounterModel : EncounterModel
	{
		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x060015AD RID: 5549 RVA: 0x0006765F File Offset: 0x0006585F
		public override float EstimatedMaximumMobilePartySpeedExceptPlayer
		{
			get
			{
				return 10f;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x060015AE RID: 5550 RVA: 0x00067666 File Offset: 0x00065866
		public override float NeededMaximumDistanceForEncounteringMobileParty
		{
			get
			{
				return 0.5f;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x060015AF RID: 5551 RVA: 0x0006766D File Offset: 0x0006586D
		public override float MaximumAllowedDistanceForEncounteringMobilePartyInArmy
		{
			get
			{
				return 1.5f;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x00067674 File Offset: 0x00065874
		public override float NeededMaximumDistanceForEncounteringTown
		{
			get
			{
				return 0.05f;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x060015B1 RID: 5553 RVA: 0x0006767B File Offset: 0x0006587B
		public override float NeededMaximumDistanceForEncounteringVillage
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x00067682 File Offset: 0x00065882
		public override bool IsEncounterExemptFromHostileActions(PartyBase side1, PartyBase side2)
		{
			return side1 == null || side2 == null || (side1.IsMobile && side1.MobileParty.AvoidHostileActions) || (side2.IsMobile && side2.MobileParty.AvoidHostileActions);
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x000676B6 File Offset: 0x000658B6
		public override Hero GetLeaderOfSiegeEvent(SiegeEvent siegeEvent, BattleSideEnum side)
		{
			return this.GetLeaderOfEventInternal(siegeEvent.GetSiegeEventSide(side).GetInvolvedPartiesForEventType(MapEvent.BattleTypes.Siege).ToList<PartyBase>());
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x000676D0 File Offset: 0x000658D0
		public override Hero GetLeaderOfMapEvent(MapEvent mapEvent, BattleSideEnum side)
		{
			return this.GetLeaderOfEventInternal((from x in mapEvent.GetMapEventSide(side).Parties
			select x.Party).ToList<PartyBase>());
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x0006770D File Offset: 0x0006590D
		private bool IsArmyLeader(Hero hero)
		{
			MobileParty partyBelongedTo = hero.PartyBelongedTo;
			return ((partyBelongedTo != null) ? partyBelongedTo.Army : null) != null && hero.PartyBelongedTo.Army.LeaderParty == hero.PartyBelongedTo;
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x0006773D File Offset: 0x0006593D
		private int GetLeadingScore(Hero hero)
		{
			if (!hero.IsKingdomLeader && !this.IsArmyLeader(hero))
			{
				return this.GetCharacterSergeantScore(hero);
			}
			return (int)hero.PartyBelongedTo.GetTotalStrengthWithFollowers(true);
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x00067768 File Offset: 0x00065968
		private Hero GetLeaderOfEventInternal(List<PartyBase> allPartiesThatBelongToASide)
		{
			Hero hero = null;
			int num = 0;
			foreach (PartyBase partyBase in allPartiesThatBelongToASide)
			{
				Hero leaderHero = partyBase.LeaderHero;
				if (leaderHero != null)
				{
					int leadingScore = this.GetLeadingScore(leaderHero);
					if (hero == null)
					{
						hero = leaderHero;
						num = leadingScore;
					}
					bool isKingdomLeader = leaderHero.IsKingdomLeader;
					bool flag = this.IsArmyLeader(leaderHero);
					bool isKingdomLeader2 = hero.IsKingdomLeader;
					bool flag2 = this.IsArmyLeader(hero);
					if (isKingdomLeader)
					{
						if (!isKingdomLeader2 || leadingScore > num)
						{
							hero = leaderHero;
							num = leadingScore;
						}
					}
					else if (flag)
					{
						if ((!isKingdomLeader2 && !flag2) || (flag2 && !isKingdomLeader2 && leadingScore > num))
						{
							hero = leaderHero;
							num = leadingScore;
						}
					}
					else if (!isKingdomLeader2 && !flag2 && leadingScore > num)
					{
						hero = leaderHero;
						num = leadingScore;
					}
				}
			}
			return hero;
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x00067838 File Offset: 0x00065A38
		public override int GetCharacterSergeantScore(Hero hero)
		{
			int num = 0;
			Clan clan = hero.Clan;
			if (clan != null)
			{
				num += clan.Tier * ((hero == clan.Leader) ? 100 : 20);
				if (clan.Kingdom != null && clan.Kingdom.Leader == hero)
				{
					num += 2000;
				}
			}
			MobileParty partyBelongedTo = hero.PartyBelongedTo;
			if (partyBelongedTo != null)
			{
				if (partyBelongedTo.Army != null && partyBelongedTo.Army.LeaderParty == partyBelongedTo)
				{
					num += partyBelongedTo.Army.Parties.Count * 200;
				}
				num += partyBelongedTo.MemberRoster.TotalManCount - partyBelongedTo.MemberRoster.TotalWounded;
			}
			return num;
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x000678DC File Offset: 0x00065ADC
		public override IEnumerable<PartyBase> GetDefenderPartiesOfSettlement(Settlement settlement, MapEvent.BattleTypes mapEventType)
		{
			if (settlement.IsFortification)
			{
				return settlement.Town.GetDefenderParties(mapEventType);
			}
			if (settlement.IsVillage)
			{
				return settlement.Village.GetDefenderParties(mapEventType);
			}
			if (settlement.IsHideout)
			{
				return settlement.Hideout.GetDefenderParties(mapEventType);
			}
			return null;
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x0006792C File Offset: 0x00065B2C
		public override PartyBase GetNextDefenderPartyOfSettlement(Settlement settlement, ref int partyIndex, MapEvent.BattleTypes mapEventType)
		{
			if (settlement.IsFortification)
			{
				return settlement.Town.GetNextDefenderParty(ref partyIndex, mapEventType);
			}
			if (settlement.IsVillage)
			{
				return settlement.Village.GetNextDefenderParty(ref partyIndex, mapEventType);
			}
			if (settlement.IsHideout)
			{
				return settlement.Hideout.GetNextDefenderParty(ref partyIndex, mapEventType);
			}
			return null;
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x0006797C File Offset: 0x00065B7C
		public override MapEventComponent CreateMapEventComponentForEncounter(PartyBase attackerParty, PartyBase defenderParty, MapEvent.BattleTypes battleType)
		{
			MapEventComponent result = null;
			switch (battleType)
			{
			case MapEvent.BattleTypes.FieldBattle:
				result = FieldBattleEventComponent.CreateFieldBattleEvent(attackerParty, defenderParty);
				break;
			case MapEvent.BattleTypes.Raid:
				result = RaidEventComponent.CreateRaidEvent(attackerParty, defenderParty);
				break;
			case MapEvent.BattleTypes.Siege:
				Campaign.Current.MapEventManager.StartSiegeMapEvent(attackerParty, defenderParty);
				break;
			case MapEvent.BattleTypes.Hideout:
				result = HideoutEventComponent.CreateHideoutEvent(attackerParty, defenderParty);
				break;
			case MapEvent.BattleTypes.SallyOut:
				Campaign.Current.MapEventManager.StartSallyOutMapEvent(attackerParty, defenderParty);
				break;
			case MapEvent.BattleTypes.SiegeOutside:
				Campaign.Current.MapEventManager.StartSiegeOutsideMapEvent(attackerParty, defenderParty);
				break;
			}
			return result;
		}
	}
}
