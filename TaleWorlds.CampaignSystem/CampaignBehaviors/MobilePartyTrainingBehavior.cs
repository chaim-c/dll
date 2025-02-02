using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003B0 RID: 944
	public class MobilePartyTrainingBehavior : CampaignBehaviorBase
	{
		// Token: 0x060039EE RID: 14830 RVA: 0x00110418 File Offset: 0x0010E618
		public override void RegisterEvents()
		{
			CampaignEvents.HourlyTickPartyEvent.AddNonSerializedListener(this, new Action<MobileParty>(this.HourlyTickParty));
			CampaignEvents.DailyTickPartyEvent.AddNonSerializedListener(this, new Action<MobileParty>(this.OnDailyTickParty));
			CampaignEvents.PlayerUpgradedTroopsEvent.AddNonSerializedListener(this, new Action<CharacterObject, CharacterObject, int>(this.OnPlayerUpgradedTroops));
		}

		// Token: 0x060039EF RID: 14831 RVA: 0x0011046A File Offset: 0x0010E66A
		private void OnPlayerUpgradedTroops(CharacterObject troop, CharacterObject upgrade, int number)
		{
			SkillLevelingManager.OnUpgradeTroops(PartyBase.MainParty, troop, upgrade, number);
		}

		// Token: 0x060039F0 RID: 14832 RVA: 0x0011047C File Offset: 0x0010E67C
		private void HourlyTickParty(MobileParty mobileParty)
		{
			if (mobileParty.LeaderHero != null)
			{
				if (mobileParty.BesiegerCamp != null)
				{
					SkillLevelingManager.OnSieging(mobileParty);
				}
				if (mobileParty.Army != null && mobileParty.Army.LeaderParty == mobileParty && mobileParty.AttachedParties.Count > 0)
				{
					SkillLevelingManager.OnLeadingArmy(mobileParty);
				}
				if (mobileParty.IsActive)
				{
					this.WorkSkills(mobileParty);
				}
			}
		}

		// Token: 0x060039F1 RID: 14833 RVA: 0x001104D8 File Offset: 0x0010E6D8
		private void OnDailyTickParty(MobileParty mobileParty)
		{
			foreach (TroopRosterElement troopRosterElement in mobileParty.MemberRoster.GetTroopRoster())
			{
				ExplainedNumber effectiveDailyExperience = Campaign.Current.Models.PartyTrainingModel.GetEffectiveDailyExperience(mobileParty, troopRosterElement);
				if (!troopRosterElement.Character.IsHero)
				{
					mobileParty.Party.MemberRoster.AddXpToTroop(MathF.Round(effectiveDailyExperience.ResultNumber * (float)troopRosterElement.Number), troopRosterElement.Character);
				}
			}
			if (mobileParty.HasPerk(DefaultPerks.Bow.Trainer, false) && !mobileParty.IsDisbanding)
			{
				Hero hero = null;
				int num = int.MaxValue;
				foreach (TroopRosterElement troopRosterElement2 in mobileParty.MemberRoster.GetTroopRoster())
				{
					if (troopRosterElement2.Character.IsHero)
					{
						int skillValue = troopRosterElement2.Character.HeroObject.GetSkillValue(DefaultSkills.Bow);
						if (skillValue < num)
						{
							num = skillValue;
							hero = troopRosterElement2.Character.HeroObject;
						}
					}
				}
				if (hero != null)
				{
					hero.AddSkillXp(DefaultSkills.Bow, DefaultPerks.Bow.Trainer.PrimaryBonus);
				}
			}
		}

		// Token: 0x060039F2 RID: 14834 RVA: 0x00110634 File Offset: 0x0010E834
		private void CheckScouting(MobileParty mobileParty)
		{
			if (mobileParty.EffectiveScout != null && mobileParty.IsMoving)
			{
				TerrainType faceTerrainType = Campaign.Current.MapSceneWrapper.GetFaceTerrainType(mobileParty.CurrentNavigationFace);
				if (mobileParty != MobileParty.MainParty)
				{
					SkillLevelingManager.OnAIPartiesTravel(mobileParty.EffectiveScout, mobileParty.IsCaravan, faceTerrainType);
				}
				SkillLevelingManager.OnTraverseTerrain(mobileParty, faceTerrainType);
			}
		}

		// Token: 0x060039F3 RID: 14835 RVA: 0x00110688 File Offset: 0x0010E888
		private void WorkSkills(MobileParty mobileParty)
		{
			if (mobileParty.IsMoving)
			{
				this.CheckScouting(mobileParty);
				if (CampaignTime.Now.GetHourOfDay % 4 == 1)
				{
					this.CheckMovementSkills(mobileParty);
				}
			}
		}

		// Token: 0x060039F4 RID: 14836 RVA: 0x001106C0 File Offset: 0x0010E8C0
		private void CheckMovementSkills(MobileParty mobileParty)
		{
			if (mobileParty == MobileParty.MainParty)
			{
				using (List<TroopRosterElement>.Enumerator enumerator = mobileParty.MemberRoster.GetTroopRoster().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TroopRosterElement troopRosterElement = enumerator.Current;
						if (troopRosterElement.Character.IsHero)
						{
							if (troopRosterElement.Character.Equipment.Horse.IsEmpty)
							{
								SkillLevelingManager.OnTravelOnFoot(troopRosterElement.Character.HeroObject, mobileParty.Speed);
							}
							else
							{
								SkillLevelingManager.OnTravelOnHorse(troopRosterElement.Character.HeroObject, mobileParty.Speed);
							}
						}
					}
					return;
				}
			}
			if (mobileParty.LeaderHero != null)
			{
				if (mobileParty.LeaderHero.CharacterObject.Equipment.Horse.IsEmpty)
				{
					SkillLevelingManager.OnTravelOnFoot(mobileParty.LeaderHero, mobileParty.Speed);
					return;
				}
				SkillLevelingManager.OnTravelOnHorse(mobileParty.LeaderHero, mobileParty.Speed);
			}
		}

		// Token: 0x060039F5 RID: 14837 RVA: 0x001107BC File Offset: 0x0010E9BC
		public override void SyncData(IDataStore dataStore)
		{
		}
	}
}
