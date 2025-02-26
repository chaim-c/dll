﻿using System;
using System.Collections.Generic;
using Helpers;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.MapEvents
{
	// Token: 0x020002B7 RID: 695
	public class MapEventParty
	{
		// Token: 0x060028E1 RID: 10465 RVA: 0x000AE9A1 File Offset: 0x000ACBA1
		internal static void AutoGeneratedStaticCollectObjectsMapEventParty(object o, List<object> collectedObjects)
		{
			((MapEventParty)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x000AE9AF File Offset: 0x000ACBAF
		protected virtual void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			collectedObjects.Add(this._roster);
			collectedObjects.Add(this._woundedInBattle);
			collectedObjects.Add(this._diedInBattle);
			collectedObjects.Add(this.Party);
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x000AE9E1 File Offset: 0x000ACBE1
		internal static object AutoGeneratedGetMemberValueParty(object o)
		{
			return ((MapEventParty)o).Party;
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x000AE9EE File Offset: 0x000ACBEE
		internal static object AutoGeneratedGetMemberValueGainedRenown(object o)
		{
			return ((MapEventParty)o).GainedRenown;
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x000AEA00 File Offset: 0x000ACC00
		internal static object AutoGeneratedGetMemberValueGainedInfluence(object o)
		{
			return ((MapEventParty)o).GainedInfluence;
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x000AEA12 File Offset: 0x000ACC12
		internal static object AutoGeneratedGetMemberValueMoraleChange(object o)
		{
			return ((MapEventParty)o).MoraleChange;
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x000AEA24 File Offset: 0x000ACC24
		internal static object AutoGeneratedGetMemberValuePlunderedGold(object o)
		{
			return ((MapEventParty)o).PlunderedGold;
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x000AEA36 File Offset: 0x000ACC36
		internal static object AutoGeneratedGetMemberValueGoldLost(object o)
		{
			return ((MapEventParty)o).GoldLost;
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x000AEA48 File Offset: 0x000ACC48
		internal static object AutoGeneratedGetMemberValue_roster(object o)
		{
			return ((MapEventParty)o)._roster;
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x000AEA55 File Offset: 0x000ACC55
		internal static object AutoGeneratedGetMemberValue_contributionToBattle(object o)
		{
			return ((MapEventParty)o)._contributionToBattle;
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x000AEA67 File Offset: 0x000ACC67
		internal static object AutoGeneratedGetMemberValue_healthyManCountAtStart(object o)
		{
			return ((MapEventParty)o)._healthyManCountAtStart;
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x000AEA79 File Offset: 0x000ACC79
		internal static object AutoGeneratedGetMemberValue_woundedInBattle(object o)
		{
			return ((MapEventParty)o)._woundedInBattle;
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x000AEA86 File Offset: 0x000ACC86
		internal static object AutoGeneratedGetMemberValue_diedInBattle(object o)
		{
			return ((MapEventParty)o)._diedInBattle;
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x060028EE RID: 10478 RVA: 0x000AEA93 File Offset: 0x000ACC93
		// (set) Token: 0x060028EF RID: 10479 RVA: 0x000AEA9B File Offset: 0x000ACC9B
		[SaveableProperty(1)]
		public PartyBase Party { get; private set; }

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x060028F0 RID: 10480 RVA: 0x000AEAA4 File Offset: 0x000ACCA4
		public int HealthyManCountAtStart
		{
			get
			{
				return this._healthyManCountAtStart;
			}
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x060028F1 RID: 10481 RVA: 0x000AEAAC File Offset: 0x000ACCAC
		internal TroopRoster DiedInBattle
		{
			get
			{
				return this._diedInBattle;
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x060028F2 RID: 10482 RVA: 0x000AEAB4 File Offset: 0x000ACCB4
		internal TroopRoster WoundedInBattle
		{
			get
			{
				return this._woundedInBattle;
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x060028F3 RID: 10483 RVA: 0x000AEABC File Offset: 0x000ACCBC
		public int ContributionToBattle
		{
			get
			{
				return this._contributionToBattle;
			}
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x000AEAC4 File Offset: 0x000ACCC4
		internal void ResetContributionToBattleToStrength()
		{
			this._contributionToBattle = (int)MathF.Sqrt(this.Party.TotalStrength);
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x060028F5 RID: 10485 RVA: 0x000AEADD File Offset: 0x000ACCDD
		public FlattenedTroopRoster Troops
		{
			get
			{
				return this._roster;
			}
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x000AEAE8 File Offset: 0x000ACCE8
		internal MapEventParty(PartyBase party)
		{
			this.Party = party;
			this.Update();
			this._healthyManCountAtStart = party.NumberOfHealthyMembers;
		}

		// Token: 0x060028F7 RID: 10487 RVA: 0x000AEB38 File Offset: 0x000ACD38
		public void Update()
		{
			if (this._roster == null)
			{
				this._roster = new FlattenedTroopRoster(this.Party.MemberRoster.TotalManCount);
			}
			this._roster.Clear();
			foreach (TroopRosterElement troopRosterElement in this.Party.MemberRoster.GetTroopRoster())
			{
				if (troopRosterElement.Character.IsHero)
				{
					if (!this._woundedInBattle.Contains(troopRosterElement.Character) && !this._diedInBattle.Contains(troopRosterElement.Character))
					{
						this._roster.Add(troopRosterElement.Character, troopRosterElement.Character.HeroObject.IsWounded, troopRosterElement.Xp);
					}
				}
				else
				{
					this._roster.Add(troopRosterElement.Character, troopRosterElement.Number, troopRosterElement.WoundedNumber);
				}
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x060028F8 RID: 10488 RVA: 0x000AEC40 File Offset: 0x000ACE40
		public bool IsNpcParty
		{
			get
			{
				return this.Party != PartyBase.MainParty;
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x060028F9 RID: 10489 RVA: 0x000AEC52 File Offset: 0x000ACE52
		public TroopRoster RosterToReceiveLootMembers
		{
			get
			{
				if (!this.IsNpcParty)
				{
					return PlayerEncounter.Current.RosterToReceiveLootMembers;
				}
				return this.Party.MemberRoster;
			}
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x060028FA RID: 10490 RVA: 0x000AEC72 File Offset: 0x000ACE72
		public TroopRoster RosterToReceiveLootPrisoners
		{
			get
			{
				if (!this.IsNpcParty)
				{
					return PlayerEncounter.Current.RosterToReceiveLootPrisoners;
				}
				return this.Party.PrisonRoster;
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x060028FB RID: 10491 RVA: 0x000AEC92 File Offset: 0x000ACE92
		public ItemRoster RosterToReceiveLootItems
		{
			get
			{
				if (!this.IsNpcParty)
				{
					return PlayerEncounter.Current.RosterToReceiveLootItems;
				}
				return this.Party.ItemRoster;
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x060028FC RID: 10492 RVA: 0x000AECB2 File Offset: 0x000ACEB2
		// (set) Token: 0x060028FD RID: 10493 RVA: 0x000AECBA File Offset: 0x000ACEBA
		[SaveableProperty(7)]
		public float GainedRenown { get; set; }

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x060028FE RID: 10494 RVA: 0x000AECC3 File Offset: 0x000ACEC3
		// (set) Token: 0x060028FF RID: 10495 RVA: 0x000AECCB File Offset: 0x000ACECB
		[SaveableProperty(8)]
		public float GainedInfluence { get; set; }

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06002900 RID: 10496 RVA: 0x000AECD4 File Offset: 0x000ACED4
		// (set) Token: 0x06002901 RID: 10497 RVA: 0x000AECDC File Offset: 0x000ACEDC
		[SaveableProperty(9)]
		public float MoraleChange { get; set; }

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06002902 RID: 10498 RVA: 0x000AECE5 File Offset: 0x000ACEE5
		// (set) Token: 0x06002903 RID: 10499 RVA: 0x000AECED File Offset: 0x000ACEED
		[SaveableProperty(10)]
		public int PlunderedGold { get; set; }

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06002904 RID: 10500 RVA: 0x000AECF6 File Offset: 0x000ACEF6
		// (set) Token: 0x06002905 RID: 10501 RVA: 0x000AECFE File Offset: 0x000ACEFE
		[SaveableProperty(11)]
		public int GoldLost { get; set; }

		// Token: 0x06002906 RID: 10502 RVA: 0x000AED08 File Offset: 0x000ACF08
		public void OnTroopKilled(UniqueTroopDescriptor troopSeed)
		{
			FlattenedTroopRosterElement flattenedTroopRosterElement = this._roster[troopSeed];
			CharacterObject troop = flattenedTroopRosterElement.Troop;
			this.Party.MemberRoster.AddTroopTempXp(troop, -flattenedTroopRosterElement.XpGained);
			if (!troop.IsHero && this.Party.IsActive)
			{
				this.Party.MemberRoster.RemoveTroop(troop, 1, troopSeed, 0);
			}
			this._roster.OnTroopKilled(troopSeed);
			this.DiedInBattle.AddToCounts(this._roster[troopSeed].Troop, 1, false, 0, 0, true, -1);
			this._contributionToBattle++;
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x000AEDAC File Offset: 0x000ACFAC
		public void OnTroopWounded(UniqueTroopDescriptor troopSeed)
		{
			this.Party.MemberRoster.WoundTroop(this._roster[troopSeed].Troop, 1, troopSeed);
			this._roster.OnTroopWounded(troopSeed);
			this.WoundedInBattle.AddToCounts(this._roster[troopSeed].Troop, 1, false, 1, 0, true, -1);
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x000AEE11 File Offset: 0x000AD011
		public void OnTroopRouted(UniqueTroopDescriptor troopSeed)
		{
		}

		// Token: 0x06002909 RID: 10505 RVA: 0x000AEE14 File Offset: 0x000AD014
		public CharacterObject GetTroop(UniqueTroopDescriptor troopSeed)
		{
			return this._roster[troopSeed].Troop;
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x000AEE38 File Offset: 0x000AD038
		public void OnTroopScoreHit(UniqueTroopDescriptor attackerTroopDesc, CharacterObject attackedTroop, int damage, bool isFatal, bool isTeamKill, WeaponComponentData attackerWeapon, bool isSimulatedHit)
		{
			CharacterObject troop = this._roster[attackerTroopDesc].Troop;
			if (!isTeamKill)
			{
				int num;
				Campaign.Current.Models.CombatXpModel.GetXpFromHit(troop, null, attackedTroop, this.Party, damage, isFatal, isSimulatedHit ? CombatXpModel.MissionTypeEnum.SimulationBattle : CombatXpModel.MissionTypeEnum.Battle, out num);
				num += MBRandom.RoundRandomized((float)num);
				if (!troop.IsHero)
				{
					if (num > 0)
					{
						int gainedXp = this._roster.OnTroopGainXp(attackerTroopDesc, num);
						this.Party.MemberRoster.AddTroopTempXp(troop, gainedXp);
					}
				}
				else
				{
					CampaignEventDispatcher.Instance.OnHeroCombatHit(troop, attackedTroop, this.Party, attackerWeapon, isFatal, num);
				}
				this._contributionToBattle += num;
			}
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x000AEEE8 File Offset: 0x000AD0E8
		public void CommitXpGain()
		{
			if (this.Party.MobileParty == null)
			{
				return;
			}
			int num = 0;
			foreach (FlattenedTroopRosterElement troopRosterElement in this._roster)
			{
				CharacterObject troop = troopRosterElement.Troop;
				int num2;
				if (!troopRosterElement.IsKilled && troopRosterElement.XpGained > 0 && MobilePartyHelper.CanTroopGainXp(this.Party, troop, out num2))
				{
					int num3 = Campaign.Current.Models.PartyTrainingModel.CalculateXpGainFromBattles(troopRosterElement, this.Party);
					int num4 = Campaign.Current.Models.PartyTrainingModel.GenerateSharedXp(troop, num3, this.Party.MobileParty);
					if (num4 > 0)
					{
						num += num4;
						num3 -= num4;
					}
					if (!troop.IsHero)
					{
						this.Party.MemberRoster.AddXpToTroop(num3, troop);
					}
				}
			}
			MobilePartyHelper.PartyAddSharedXp(this.Party.MobileParty, (float)num);
			SkillLevelingManager.OnBattleEnd(this.Party, this._roster);
		}

		// Token: 0x04000C60 RID: 3168
		[SaveableField(2)]
		private FlattenedTroopRoster _roster;

		// Token: 0x04000C61 RID: 3169
		[SaveableField(3)]
		private int _contributionToBattle = 1;

		// Token: 0x04000C62 RID: 3170
		[SaveableField(9)]
		private int _healthyManCountAtStart = 1;

		// Token: 0x04000C63 RID: 3171
		[SaveableField(7)]
		private TroopRoster _woundedInBattle = TroopRoster.CreateDummyTroopRoster();

		// Token: 0x04000C64 RID: 3172
		[SaveableField(8)]
		private TroopRoster _diedInBattle = TroopRoster.CreateDummyTroopRoster();
	}
}
