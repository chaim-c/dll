using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.GameComponents
{
	// Token: 0x02000106 RID: 262
	public class DefaultEquipmentSelectionModel : EquipmentSelectionModel
	{
		// Token: 0x060015BD RID: 5565 RVA: 0x00067A18 File Offset: 0x00065C18
		public override MBList<MBEquipmentRoster> GetEquipmentRostersForHeroComeOfAge(Hero hero, bool isCivilian)
		{
			MBList<MBEquipmentRoster> mblist = new MBList<MBEquipmentRoster>();
			bool flag = this.IsHeroCombatant(hero);
			foreach (MBEquipmentRoster mbequipmentRoster in MBEquipmentRosterExtensions.All)
			{
				if (this.IsRosterAppropriateForHeroAsTemplate(mbequipmentRoster, hero, EquipmentFlags.IsNobleTemplate, false))
				{
					if (flag)
					{
						if (isCivilian)
						{
							if (mbequipmentRoster.HasEquipmentFlags(EquipmentFlags.IsCombatantTemplate | EquipmentFlags.IsCivilianTemplate))
							{
								mblist.Add(mbequipmentRoster);
							}
						}
						else if (mbequipmentRoster.HasEquipmentFlags(EquipmentFlags.IsMediumTemplate))
						{
							mblist.Add(mbequipmentRoster);
						}
					}
					else if (isCivilian)
					{
						if (mbequipmentRoster.HasEquipmentFlags(EquipmentFlags.IsNoncombatantTemplate))
						{
							mblist.Add(mbequipmentRoster);
						}
					}
					else if (mbequipmentRoster.HasEquipmentFlags(EquipmentFlags.IsMediumTemplate))
					{
						mblist.Add(mbequipmentRoster);
					}
				}
			}
			return mblist;
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x00067AD8 File Offset: 0x00065CD8
		public override MBList<MBEquipmentRoster> GetEquipmentRostersForHeroReachesTeenAge(Hero hero)
		{
			EquipmentFlags suitableFlags = EquipmentFlags.IsNobleTemplate | EquipmentFlags.IsTeenagerEquipmentTemplate;
			MBList<MBEquipmentRoster> result = new MBList<MBEquipmentRoster>();
			this.AddEquipmentsToRoster(hero, suitableFlags, ref result, true);
			return result;
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x00067B00 File Offset: 0x00065D00
		public override MBList<MBEquipmentRoster> GetEquipmentRostersForInitialChildrenGeneration(Hero hero)
		{
			bool flag = hero.Age < (float)Campaign.Current.Models.AgeModel.BecomeTeenagerAge;
			EquipmentFlags suitableFlags = EquipmentFlags.IsNobleTemplate | (flag ? EquipmentFlags.IsChildEquipmentTemplate : EquipmentFlags.IsTeenagerEquipmentTemplate);
			MBList<MBEquipmentRoster> result = new MBList<MBEquipmentRoster>();
			this.AddEquipmentsToRoster(hero, suitableFlags, ref result, true);
			return result;
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x00067B50 File Offset: 0x00065D50
		public override MBList<MBEquipmentRoster> GetEquipmentRostersForDeliveredOffspring(Hero hero)
		{
			EquipmentFlags suitableFlags = EquipmentFlags.IsNobleTemplate | EquipmentFlags.IsChildEquipmentTemplate;
			MBList<MBEquipmentRoster> result = new MBList<MBEquipmentRoster>();
			this.AddEquipmentsToRoster(hero, suitableFlags, ref result, true);
			return result;
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x00067B78 File Offset: 0x00065D78
		public override MBList<MBEquipmentRoster> GetEquipmentRostersForCompanion(Hero hero, bool isCivilian)
		{
			EquipmentFlags suitableFlags = isCivilian ? (EquipmentFlags.IsCivilianTemplate | EquipmentFlags.IsNobleTemplate) : (EquipmentFlags.IsNobleTemplate | EquipmentFlags.IsMediumTemplate);
			MBList<MBEquipmentRoster> result = new MBList<MBEquipmentRoster>();
			this.AddEquipmentsToRoster(hero, suitableFlags, ref result, isCivilian);
			return result;
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x00067BA4 File Offset: 0x00065DA4
		private bool IsRosterAppropriateForHeroAsTemplate(MBEquipmentRoster equipmentRoster, Hero hero, EquipmentFlags customFlags = EquipmentFlags.None, bool shouldMatchGender = false)
		{
			bool result = false;
			if (equipmentRoster.IsEquipmentTemplate() && (!shouldMatchGender || equipmentRoster.HasEquipmentFlags(EquipmentFlags.IsFemaleTemplate) == hero.IsFemale) && equipmentRoster.EquipmentCulture == hero.Culture && (customFlags == EquipmentFlags.None || equipmentRoster.HasEquipmentFlags(customFlags)))
			{
				bool flag = equipmentRoster.HasEquipmentFlags(EquipmentFlags.IsNomadTemplate) || equipmentRoster.HasEquipmentFlags(EquipmentFlags.IsWoodlandTemplate);
				bool flag2 = !hero.IsChild && (equipmentRoster.HasEquipmentFlags(EquipmentFlags.IsChildEquipmentTemplate) || equipmentRoster.HasEquipmentFlags(EquipmentFlags.IsTeenagerEquipmentTemplate));
				if (!flag && !flag2)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x00067C38 File Offset: 0x00065E38
		private bool IsHeroCombatant(Hero hero)
		{
			return !hero.IsFemale || hero.Clan == Hero.MainHero.Clan || (hero.Mother != null && !hero.Mother.IsNoncombatant) || (hero.RandomIntWithSeed(17U, 0, 1) == 0 && hero.GetTraitLevel(DefaultTraits.Valor) == 1);
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x00067C94 File Offset: 0x00065E94
		private void AddEquipmentsToRoster(Hero hero, EquipmentFlags suitableFlags, ref MBList<MBEquipmentRoster> roster, bool shouldMatchGender = false)
		{
			foreach (MBEquipmentRoster mbequipmentRoster in MBEquipmentRosterExtensions.All)
			{
				if (this.IsRosterAppropriateForHeroAsTemplate(mbequipmentRoster, hero, suitableFlags, shouldMatchGender))
				{
					roster.Add(mbequipmentRoster);
				}
			}
		}
	}
}
