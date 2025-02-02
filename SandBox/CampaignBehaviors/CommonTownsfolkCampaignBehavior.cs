using System;
using System.Collections.Generic;
using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.AgentOrigins;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.CampaignBehaviors
{
	// Token: 0x020000A5 RID: 165
	public class CommonTownsfolkCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06000711 RID: 1809 RVA: 0x00034079 File Offset: 0x00032279
		private float GetSpawnRate(Settlement settlement)
		{
			return this.TimeOfDayPercentage() * this.GetProsperityMultiplier(settlement.SettlementComponent) * this.GetWeatherEffectMultiplier(settlement);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00034096 File Offset: 0x00032296
		private float GetConfigValue()
		{
			return BannerlordConfig.CivilianAgentCount;
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0003409D File Offset: 0x0003229D
		private float GetProsperityMultiplier(SettlementComponent settlement)
		{
			return ((float)settlement.GetProsperityLevel() + 1f) / 3f;
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x000340B4 File Offset: 0x000322B4
		private float GetWeatherEffectMultiplier(Settlement settlement)
		{
			MapWeatherModel.WeatherEvent weatherEventInPosition = Campaign.Current.Models.MapWeatherModel.GetWeatherEventInPosition(settlement.GatePosition);
			if (weatherEventInPosition == MapWeatherModel.WeatherEvent.HeavyRain)
			{
				return 0.15f;
			}
			if (weatherEventInPosition != MapWeatherModel.WeatherEvent.Blizzard)
			{
				return 1f;
			}
			return 0.4f;
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x000340F8 File Offset: 0x000322F8
		private float TimeOfDayPercentage()
		{
			return 1f - MathF.Abs(CampaignTime.Now.CurrentHourInDay - 15f) / 15f;
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00034129 File Offset: 0x00032329
		public override void RegisterEvents()
		{
			CampaignEvents.LocationCharactersAreReadyToSpawnEvent.AddNonSerializedListener(this, new Action<Dictionary<string, int>>(this.LocationCharactersAreReadyToSpawn));
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00034142 File Offset: 0x00032342
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00034144 File Offset: 0x00032344
		private void LocationCharactersAreReadyToSpawn(Dictionary<string, int> unusedUsablePointCount)
		{
			Settlement settlement = PlayerEncounter.LocationEncounter.Settlement;
			if (!settlement.IsCastle)
			{
				Location locationWithId = settlement.LocationComplex.GetLocationWithId("center");
				Location locationWithId2 = settlement.LocationComplex.GetLocationWithId("tavern");
				if (CampaignMission.Current.Location == locationWithId)
				{
					this.AddPeopleToTownCenter(settlement, unusedUsablePointCount, CampaignTime.Now.IsDayTime);
				}
				if (CampaignMission.Current.Location == locationWithId2)
				{
					this.AddPeopleToTownTavern(settlement, unusedUsablePointCount);
				}
			}
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x000341C0 File Offset: 0x000323C0
		private void AddPeopleToTownTavern(Settlement settlement, Dictionary<string, int> unusedUsablePointCount)
		{
			Location locationWithId = settlement.LocationComplex.GetLocationWithId("tavern");
			int num;
			unusedUsablePointCount.TryGetValue("npc_common", out num);
			MapWeatherModel.WeatherEvent weatherEventInPosition = Campaign.Current.Models.MapWeatherModel.GetWeatherEventInPosition(settlement.GatePosition);
			bool flag = weatherEventInPosition == MapWeatherModel.WeatherEvent.HeavyRain || weatherEventInPosition == MapWeatherModel.WeatherEvent.Blizzard;
			if (num > 0)
			{
				int num2 = (int)((float)num * (0.3f + (flag ? 0.2f : 0f)));
				if (num2 > 0)
				{
					locationWithId.AddLocationCharacters(new CreateLocationCharacterDelegate(CommonTownsfolkCampaignBehavior.CreateTownsManForTavern), settlement.Culture, LocationCharacter.CharacterRelations.Neutral, num2);
				}
				int num3 = (int)((float)num * (0.1f + (flag ? 0.2f : 0f)));
				if (num3 > 0)
				{
					locationWithId.AddLocationCharacters(new CreateLocationCharacterDelegate(CommonTownsfolkCampaignBehavior.CreateTownsWomanForTavern), settlement.Culture, LocationCharacter.CharacterRelations.Neutral, num3);
				}
			}
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00034290 File Offset: 0x00032490
		private void AddPeopleToTownCenter(Settlement settlement, Dictionary<string, int> unusedUsablePointCount, bool isDayTime)
		{
			Location locationWithId = settlement.LocationComplex.GetLocationWithId("center");
			CultureObject culture = settlement.Culture;
			int num;
			unusedUsablePointCount.TryGetValue("npc_common", out num);
			int num2;
			unusedUsablePointCount.TryGetValue("npc_common_limited", out num2);
			float num3 = (float)(num + num2) * 0.65000004f;
			if (num3 != 0f)
			{
				float num4 = MBMath.ClampFloat(this.GetConfigValue() / num3, 0f, 1f);
				float num5 = this.GetSpawnRate(settlement) * num4;
				if (num > 0)
				{
					int num6 = (int)((float)num * 0.2f * num5);
					if (num6 > 0)
					{
						locationWithId.AddLocationCharacters(new CreateLocationCharacterDelegate(CommonTownsfolkCampaignBehavior.CreateTownsMan), culture, LocationCharacter.CharacterRelations.Neutral, num6);
					}
					int num7 = (int)((float)num * 0.15f * num5);
					if (num7 > 0)
					{
						locationWithId.AddLocationCharacters(new CreateLocationCharacterDelegate(CommonTownsfolkCampaignBehavior.CreateTownsWoman), culture, LocationCharacter.CharacterRelations.Neutral, num7);
					}
				}
				MapWeatherModel.WeatherEvent weatherEventInPosition = Campaign.Current.Models.MapWeatherModel.GetWeatherEventInPosition(settlement.GatePosition);
				bool flag = weatherEventInPosition == MapWeatherModel.WeatherEvent.HeavyRain || weatherEventInPosition == MapWeatherModel.WeatherEvent.Blizzard;
				if (isDayTime && !flag)
				{
					if (num2 > 0)
					{
						int num8 = (int)((float)num2 * 0.15f * num5);
						if (num8 > 0)
						{
							locationWithId.AddLocationCharacters(new CreateLocationCharacterDelegate(CommonTownsfolkCampaignBehavior.CreateTownsManCarryingStuff), culture, LocationCharacter.CharacterRelations.Neutral, num8);
						}
						int num9 = (int)((float)num2 * 0.1f * num5);
						if (num9 > 0)
						{
							locationWithId.AddLocationCharacters(new CreateLocationCharacterDelegate(CommonTownsfolkCampaignBehavior.CreateTownsWomanCarryingStuff), culture, LocationCharacter.CharacterRelations.Neutral, num9);
						}
						int num10 = (int)((float)num2 * 0.05f * num5);
						if (num10 > 0)
						{
							locationWithId.AddLocationCharacters(new CreateLocationCharacterDelegate(CommonTownsfolkCampaignBehavior.CreateMaleChild), culture, LocationCharacter.CharacterRelations.Neutral, num10);
							locationWithId.AddLocationCharacters(new CreateLocationCharacterDelegate(CommonTownsfolkCampaignBehavior.CreateFemaleChild), culture, LocationCharacter.CharacterRelations.Neutral, num10);
							locationWithId.AddLocationCharacters(new CreateLocationCharacterDelegate(CommonTownsfolkCampaignBehavior.CreateMaleTeenager), culture, LocationCharacter.CharacterRelations.Neutral, num10);
							locationWithId.AddLocationCharacters(new CreateLocationCharacterDelegate(CommonTownsfolkCampaignBehavior.CreateFemaleTeenager), culture, LocationCharacter.CharacterRelations.Neutral, num10);
						}
					}
					int num11 = 0;
					if (unusedUsablePointCount.TryGetValue("spawnpoint_cleaner", out num11))
					{
						locationWithId.AddLocationCharacters(new CreateLocationCharacterDelegate(CommonTownsfolkCampaignBehavior.CreateBroomsWoman), culture, LocationCharacter.CharacterRelations.Neutral, num11);
					}
					if (unusedUsablePointCount.TryGetValue("npc_dancer", out num11))
					{
						locationWithId.AddLocationCharacters(new CreateLocationCharacterDelegate(CommonTownsfolkCampaignBehavior.CreateDancer), culture, LocationCharacter.CharacterRelations.Neutral, num11);
					}
					if (settlement.IsTown && unusedUsablePointCount.TryGetValue("npc_beggar", out num11))
					{
						locationWithId.AddLocationCharacters(new CreateLocationCharacterDelegate(CommonTownsfolkCampaignBehavior.CreateFemaleBeggar), culture, LocationCharacter.CharacterRelations.Neutral, (num11 == 1) ? 0 : (num11 / 2));
						locationWithId.AddLocationCharacters(new CreateLocationCharacterDelegate(CommonTownsfolkCampaignBehavior.CreateMaleBeggar), culture, LocationCharacter.CharacterRelations.Neutral, (num11 == 1) ? 1 : (num11 / 2));
					}
				}
			}
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00034508 File Offset: 0x00032708
		public static string GetActionSetSuffixAndMonsterForItem(string itemId, int race, bool isFemale, out Monster monster)
		{
			monster = TaleWorlds.Core.FaceGen.GetMonsterWithSuffix(race, "_settlement");
			uint num = <PrivateImplementationDetails>.ComputeStringHash(itemId);
			if (num <= 2354022098U)
			{
				if (num <= 524654717U)
				{
					if (num != 330511441U)
					{
						if (num != 423989003U)
						{
							if (num != 524654717U)
							{
								goto IL_20B;
							}
							if (!(itemId == "_to_carry_bed_convolute_g"))
							{
								goto IL_20B;
							}
							return "_villager_carry_on_shoulder";
						}
						else
						{
							if (!(itemId == "_to_carry_bed_convolute_a"))
							{
								goto IL_20B;
							}
							return "_villager_carry_front";
						}
					}
					else
					{
						if (!(itemId == "_to_carry_bd_basket_a"))
						{
							goto IL_20B;
						}
						return "_villager_with_backpack";
					}
				}
				else if (num != 1406916035U)
				{
					if (num != 1726492488U)
					{
						if (num != 2354022098U)
						{
							goto IL_20B;
						}
						if (!(itemId == "_to_carry_kitchen_pot_c"))
						{
							goto IL_20B;
						}
						return "_villager_carry_right_hand";
					}
					else if (!(itemId == "_to_carry_foods_watermelon_a"))
					{
						goto IL_20B;
					}
				}
				else
				{
					if (!(itemId == "_to_carry_arm_kitchen_pot_c"))
					{
						goto IL_20B;
					}
					return "_villager_carry_right_arm";
				}
			}
			else if (num <= 3512086304U)
			{
				if (num != 2481184366U)
				{
					if (num != 3004030871U)
					{
						if (num != 3512086304U)
						{
							goto IL_20B;
						}
						if (!(itemId == "_to_carry_bd_fabric_c"))
						{
							goto IL_20B;
						}
					}
					else
					{
						if (!(itemId == "_to_carry_foods_basket_apple"))
						{
							goto IL_20B;
						}
						return "_villager_carry_over_head_v2";
					}
				}
				else
				{
					if (!(itemId == "_to_carry_kitchen_pitcher_a"))
					{
						goto IL_20B;
					}
					return "_villager_carry_over_head";
				}
			}
			else if (num <= 3737849652U)
			{
				if (num != 3710634116U)
				{
					if (num != 3737849652U)
					{
						goto IL_20B;
					}
					if (!(itemId == "_to_carry_merchandise_hides_b"))
					{
						goto IL_20B;
					}
					return "_villager_with_backpack";
				}
				else
				{
					if (!(itemId == "practice_spear_t1"))
					{
						goto IL_20B;
					}
					return "_villager_with_staff";
				}
			}
			else if (num != 4035495654U)
			{
				if (num != 4038602446U)
				{
					goto IL_20B;
				}
				if (!(itemId == "simple_sparth_axe_t2"))
				{
					goto IL_20B;
				}
				return "_villager_carry_axe";
			}
			else
			{
				if (!(itemId == "_to_carry_foods_pumpkin_a"))
				{
					goto IL_20B;
				}
				return "_villager_carry_front_v2";
			}
			return "_villager_carry_right_side";
			IL_20B:
			return "_villager_carry_right_hand";
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00034728 File Offset: 0x00032928
		public static Tuple<string, Monster> GetRandomTownsManActionSetAndMonster(int race)
		{
			int num = MBRandom.RandomInt(3);
			Monster monsterWithSuffix;
			if (num == 0)
			{
				monsterWithSuffix = TaleWorlds.Core.FaceGen.GetMonsterWithSuffix(race, "_settlement");
				return new Tuple<string, Monster>(ActionSetCode.GenerateActionSetNameWithSuffix(monsterWithSuffix, false, "_villager"), monsterWithSuffix);
			}
			if (num != 1)
			{
				monsterWithSuffix = TaleWorlds.Core.FaceGen.GetMonsterWithSuffix(race, "_settlement");
				return new Tuple<string, Monster>(ActionSetCode.GenerateActionSetNameWithSuffix(monsterWithSuffix, false, "_villager_3"), monsterWithSuffix);
			}
			monsterWithSuffix = TaleWorlds.Core.FaceGen.GetMonsterWithSuffix(race, "_settlement_slow");
			return new Tuple<string, Monster>(ActionSetCode.GenerateActionSetNameWithSuffix(monsterWithSuffix, false, "_villager_2"), monsterWithSuffix);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x000347A4 File Offset: 0x000329A4
		public static Tuple<string, Monster> GetRandomTownsWomanActionSetAndMonster(int race)
		{
			Monster monsterWithSuffix;
			if (MBRandom.RandomInt(4) == 0)
			{
				monsterWithSuffix = TaleWorlds.Core.FaceGen.GetMonsterWithSuffix(race, "_settlement_fast");
				return new Tuple<string, Monster>(ActionSetCode.GenerateActionSetNameWithSuffix(monsterWithSuffix, true, "_villager"), monsterWithSuffix);
			}
			monsterWithSuffix = TaleWorlds.Core.FaceGen.GetMonsterWithSuffix(race, "_settlement_slow");
			return new Tuple<string, Monster>(ActionSetCode.GenerateActionSetNameWithSuffix(monsterWithSuffix, true, "_villager_2"), monsterWithSuffix);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x000347F8 File Offset: 0x000329F8
		private static LocationCharacter CreateTownsMan(CultureObject culture, LocationCharacter.CharacterRelations relation)
		{
			CharacterObject townsman = culture.Townsman;
			Tuple<string, Monster> randomTownsManActionSetAndMonster = CommonTownsfolkCampaignBehavior.GetRandomTownsManActionSetAndMonster(townsman.Race);
			int minValue;
			int maxValue;
			Campaign.Current.Models.AgeModel.GetAgeLimitForLocation(townsman, out minValue, out maxValue, "");
			return new LocationCharacter(new AgentData(new SimpleAgentOrigin(townsman, -1, null, default(UniqueTroopDescriptor))).Monster(randomTownsManActionSetAndMonster.Item2).Age(MBRandom.RandomInt(minValue, maxValue)), new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddOutdoorWandererBehaviors), "npc_common", false, relation, randomTownsManActionSetAndMonster.Item1, true, false, null, false, false, true);
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00034894 File Offset: 0x00032A94
		private static LocationCharacter CreateTownsManForTavern(CultureObject culture, LocationCharacter.CharacterRelations relation)
		{
			CharacterObject townsman = culture.Townsman;
			Monster monsterWithSuffix = TaleWorlds.Core.FaceGen.GetMonsterWithSuffix(townsman.Race, "_settlement_slow");
			string actionSetCode;
			if (culture.StringId.ToLower() == "aserai" || culture.StringId.ToLower() == "khuzait")
			{
				actionSetCode = ActionSetCode.GenerateActionSetNameWithSuffix(monsterWithSuffix, townsman.IsFemale, "_villager_in_aserai_tavern");
			}
			else
			{
				actionSetCode = ActionSetCode.GenerateActionSetNameWithSuffix(monsterWithSuffix, townsman.IsFemale, "_villager_in_tavern");
			}
			int minValue;
			int maxValue;
			Campaign.Current.Models.AgeModel.GetAgeLimitForLocation(townsman, out minValue, out maxValue, "TavernVisitor");
			return new LocationCharacter(new AgentData(new SimpleAgentOrigin(townsman, -1, null, default(UniqueTroopDescriptor))).Monster(monsterWithSuffix).Age(MBRandom.RandomInt(minValue, maxValue)), new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddWandererBehaviors), "npc_common", true, relation, actionSetCode, true, false, null, false, false, true);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00034980 File Offset: 0x00032B80
		private static LocationCharacter CreateTownsWomanForTavern(CultureObject culture, LocationCharacter.CharacterRelations relation)
		{
			CharacterObject townswoman = culture.Townswoman;
			Monster monsterWithSuffix = TaleWorlds.Core.FaceGen.GetMonsterWithSuffix(townswoman.Race, "_settlement_slow");
			string actionSetCode;
			if (culture.StringId.ToLower() == "aserai" || culture.StringId.ToLower() == "khuzait")
			{
				actionSetCode = ActionSetCode.GenerateActionSetNameWithSuffix(monsterWithSuffix, townswoman.IsFemale, "_warrior_in_aserai_tavern");
			}
			else
			{
				actionSetCode = ActionSetCode.GenerateActionSetNameWithSuffix(monsterWithSuffix, townswoman.IsFemale, "_warrior_in_tavern");
			}
			int minValue;
			int maxValue;
			Campaign.Current.Models.AgeModel.GetAgeLimitForLocation(townswoman, out minValue, out maxValue, "TavernVisitor");
			return new LocationCharacter(new AgentData(new SimpleAgentOrigin(townswoman, -1, null, default(UniqueTroopDescriptor))).Monster(monsterWithSuffix).Age(MBRandom.RandomInt(minValue, maxValue)), new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddWandererBehaviors), "npc_common", true, relation, actionSetCode, true, false, null, false, false, true);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00034A6C File Offset: 0x00032C6C
		private static LocationCharacter CreateTownsManCarryingStuff(CultureObject culture, LocationCharacter.CharacterRelations relation)
		{
			CharacterObject townsman = culture.Townsman;
			string randomStuff = SettlementHelper.GetRandomStuff(false);
			Monster monster;
			string actionSetSuffixAndMonsterForItem = CommonTownsfolkCampaignBehavior.GetActionSetSuffixAndMonsterForItem(randomStuff, townsman.Race, false, out monster);
			int minValue;
			int maxValue;
			Campaign.Current.Models.AgeModel.GetAgeLimitForLocation(townsman, out minValue, out maxValue, "TownsfolkCarryingStuff");
			AgentData agentData = new AgentData(new SimpleAgentOrigin(townsman, -1, null, default(UniqueTroopDescriptor))).Monster(monster).Age(MBRandom.RandomInt(minValue, maxValue));
			ItemObject @object = Game.Current.ObjectManager.GetObject<ItemObject>(randomStuff);
			LocationCharacter locationCharacter = new LocationCharacter(agentData, new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddWandererBehaviors), "npc_common_limited", false, relation, ActionSetCode.GenerateActionSetNameWithSuffix(agentData.AgentMonster, townsman.IsFemale, actionSetSuffixAndMonsterForItem), true, false, @object, false, false, true);
			if (@object == null)
			{
				locationCharacter.PrefabNamesForBones.Add(agentData.AgentMonster.MainHandItemBoneIndex, randomStuff);
			}
			return locationCharacter;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00034B54 File Offset: 0x00032D54
		private static LocationCharacter CreateTownsWoman(CultureObject culture, LocationCharacter.CharacterRelations relation)
		{
			CharacterObject townswoman = culture.Townswoman;
			Tuple<string, Monster> randomTownsWomanActionSetAndMonster = CommonTownsfolkCampaignBehavior.GetRandomTownsWomanActionSetAndMonster(townswoman.Race);
			int minValue;
			int maxValue;
			Campaign.Current.Models.AgeModel.GetAgeLimitForLocation(townswoman, out minValue, out maxValue, "");
			return new LocationCharacter(new AgentData(new SimpleAgentOrigin(townswoman, -1, null, default(UniqueTroopDescriptor))).Monster(randomTownsWomanActionSetAndMonster.Item2).Age(MBRandom.RandomInt(minValue, maxValue)), new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddOutdoorWandererBehaviors), "npc_common", false, relation, randomTownsWomanActionSetAndMonster.Item1, true, false, null, false, false, true);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00034BF0 File Offset: 0x00032DF0
		private static LocationCharacter CreateMaleChild(CultureObject culture, LocationCharacter.CharacterRelations relation)
		{
			CharacterObject townsmanChild = culture.TownsmanChild;
			Monster monsterWithSuffix = TaleWorlds.Core.FaceGen.GetMonsterWithSuffix(townsmanChild.Race, "_child");
			int minValue;
			int maxValue;
			Campaign.Current.Models.AgeModel.GetAgeLimitForLocation(townsmanChild, out minValue, out maxValue, "Child");
			AgentData agentData = new AgentData(new SimpleAgentOrigin(townsmanChild, -1, null, default(UniqueTroopDescriptor))).Monster(monsterWithSuffix).Age(MBRandom.RandomInt(minValue, maxValue));
			return new LocationCharacter(agentData, new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddOutdoorWandererBehaviors), "npc_common_limited", false, relation, ActionSetCode.GenerateActionSetNameWithSuffix(agentData.AgentMonster, townsmanChild.IsFemale, "_child"), true, false, null, false, false, true);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00034CA0 File Offset: 0x00032EA0
		private static LocationCharacter CreateFemaleChild(CultureObject culture, LocationCharacter.CharacterRelations relation)
		{
			CharacterObject townswomanChild = culture.TownswomanChild;
			Monster monsterWithSuffix = TaleWorlds.Core.FaceGen.GetMonsterWithSuffix(townswomanChild.Race, "_child");
			int minValue;
			int maxValue;
			Campaign.Current.Models.AgeModel.GetAgeLimitForLocation(townswomanChild, out minValue, out maxValue, "Child");
			AgentData agentData = new AgentData(new SimpleAgentOrigin(townswomanChild, -1, null, default(UniqueTroopDescriptor))).Monster(monsterWithSuffix).Age(MBRandom.RandomInt(minValue, maxValue));
			return new LocationCharacter(agentData, new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddOutdoorWandererBehaviors), "npc_common_limited", false, relation, ActionSetCode.GenerateActionSetNameWithSuffix(agentData.AgentMonster, townswomanChild.IsFemale, "_child"), true, false, null, false, false, true);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00034D50 File Offset: 0x00032F50
		private static LocationCharacter CreateMaleTeenager(CultureObject culture, LocationCharacter.CharacterRelations relation)
		{
			CharacterObject townsmanTeenager = culture.TownsmanTeenager;
			Monster monsterWithSuffix = TaleWorlds.Core.FaceGen.GetMonsterWithSuffix(townsmanTeenager.Race, "_child");
			int minValue;
			int maxValue;
			Campaign.Current.Models.AgeModel.GetAgeLimitForLocation(townsmanTeenager, out minValue, out maxValue, "Teenager");
			AgentData agentData = new AgentData(new SimpleAgentOrigin(townsmanTeenager, -1, null, default(UniqueTroopDescriptor))).Monster(monsterWithSuffix).Age(MBRandom.RandomInt(minValue, maxValue));
			return new LocationCharacter(agentData, new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddOutdoorWandererBehaviors), "npc_common_limited", false, relation, ActionSetCode.GenerateActionSetNameWithSuffix(agentData.AgentMonster, townsmanTeenager.IsFemale, "_villager"), true, false, null, false, false, true);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00034E00 File Offset: 0x00033000
		private static LocationCharacter CreateFemaleTeenager(CultureObject culture, LocationCharacter.CharacterRelations relation)
		{
			CharacterObject townswomanTeenager = culture.TownswomanTeenager;
			Monster monsterWithSuffix = TaleWorlds.Core.FaceGen.GetMonsterWithSuffix(townswomanTeenager.Race, "_child");
			int minValue;
			int maxValue;
			Campaign.Current.Models.AgeModel.GetAgeLimitForLocation(townswomanTeenager, out minValue, out maxValue, "Teenager");
			AgentData agentData = new AgentData(new SimpleAgentOrigin(townswomanTeenager, -1, null, default(UniqueTroopDescriptor))).Monster(monsterWithSuffix).Age(MBRandom.RandomInt(minValue, maxValue));
			return new LocationCharacter(agentData, new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddOutdoorWandererBehaviors), "npc_common_limited", false, relation, ActionSetCode.GenerateActionSetNameWithSuffix(agentData.AgentMonster, townswomanTeenager.IsFemale, "_villager"), true, false, null, false, false, true);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00034EB0 File Offset: 0x000330B0
		private static LocationCharacter CreateTownsWomanCarryingStuff(CultureObject culture, LocationCharacter.CharacterRelations relation)
		{
			CharacterObject townswoman = culture.Townswoman;
			string randomStuff = SettlementHelper.GetRandomStuff(true);
			Monster monster;
			string actionSetSuffixAndMonsterForItem = CommonTownsfolkCampaignBehavior.GetActionSetSuffixAndMonsterForItem(randomStuff, townswoman.Race, false, out monster);
			int minValue;
			int maxValue;
			Campaign.Current.Models.AgeModel.GetAgeLimitForLocation(townswoman, out minValue, out maxValue, "TownsfolkCarryingStuff");
			AgentData agentData = new AgentData(new SimpleAgentOrigin(townswoman, -1, null, default(UniqueTroopDescriptor))).Monster(monster).Age(MBRandom.RandomInt(minValue, maxValue));
			ItemObject @object = Game.Current.ObjectManager.GetObject<ItemObject>(randomStuff);
			LocationCharacter locationCharacter = new LocationCharacter(agentData, new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddWandererBehaviors), "npc_common_limited", false, relation, ActionSetCode.GenerateActionSetNameWithSuffix(agentData.AgentMonster, townswoman.IsFemale, actionSetSuffixAndMonsterForItem), true, false, @object, false, false, true);
			if (@object == null)
			{
				locationCharacter.PrefabNamesForBones.Add(agentData.AgentMonster.MainHandItemBoneIndex, randomStuff);
			}
			return locationCharacter;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00034F98 File Offset: 0x00033198
		public static LocationCharacter CreateBroomsWoman(CultureObject culture, LocationCharacter.CharacterRelations relation)
		{
			CharacterObject townswoman = culture.Townswoman;
			Monster monsterWithSuffix = TaleWorlds.Core.FaceGen.GetMonsterWithSuffix(townswoman.Race, "_settlement");
			int minValue;
			int maxValue;
			Campaign.Current.Models.AgeModel.GetAgeLimitForLocation(townswoman, out minValue, out maxValue, "BroomsWoman");
			return new LocationCharacter(new AgentData(new SimpleAgentOrigin(townswoman, -1, null, default(UniqueTroopDescriptor))).Monster(monsterWithSuffix).Age(MBRandom.RandomInt(minValue, maxValue)), new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddOutdoorWandererBehaviors), "spawnpoint_cleaner", false, relation, null, true, false, null, false, false, true);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00035030 File Offset: 0x00033230
		private static LocationCharacter CreateDancer(CultureObject culture, LocationCharacter.CharacterRelations relation)
		{
			CharacterObject femaleDancer = culture.FemaleDancer;
			Monster monsterWithSuffix = TaleWorlds.Core.FaceGen.GetMonsterWithSuffix(femaleDancer.Race, "_settlement");
			int minValue;
			int maxValue;
			Campaign.Current.Models.AgeModel.GetAgeLimitForLocation(femaleDancer, out minValue, out maxValue, "Dancer");
			AgentData agentData = new AgentData(new SimpleAgentOrigin(femaleDancer, -1, null, default(UniqueTroopDescriptor))).Monster(monsterWithSuffix).Age(MBRandom.RandomInt(minValue, maxValue));
			return new LocationCharacter(agentData, new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddWandererBehaviors), "npc_dancer", true, relation, ActionSetCode.GenerateActionSetNameWithSuffix(agentData.AgentMonster, agentData.AgentIsFemale, "_dancer"), true, false, null, false, false, true);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x000350E0 File Offset: 0x000332E0
		public static LocationCharacter CreateMaleBeggar(CultureObject culture, LocationCharacter.CharacterRelations relation)
		{
			CharacterObject beggar = culture.Beggar;
			Monster monsterWithSuffix = TaleWorlds.Core.FaceGen.GetMonsterWithSuffix(beggar.Race, "_settlement");
			int minValue;
			int maxValue;
			Campaign.Current.Models.AgeModel.GetAgeLimitForLocation(beggar, out minValue, out maxValue, "Beggar");
			AgentData agentData = new AgentData(new SimpleAgentOrigin(beggar, -1, null, default(UniqueTroopDescriptor))).Monster(monsterWithSuffix).Age(MBRandom.RandomInt(minValue, maxValue));
			return new LocationCharacter(agentData, new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddWandererBehaviors), "npc_beggar", true, relation, ActionSetCode.GenerateActionSetNameWithSuffix(agentData.AgentMonster, agentData.AgentIsFemale, "_beggar"), true, false, null, false, false, true);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00035190 File Offset: 0x00033390
		public static LocationCharacter CreateFemaleBeggar(CultureObject culture, LocationCharacter.CharacterRelations relation)
		{
			CharacterObject femaleBeggar = culture.FemaleBeggar;
			Monster monsterWithSuffix = TaleWorlds.Core.FaceGen.GetMonsterWithSuffix(femaleBeggar.Race, "_settlement");
			int minValue;
			int maxValue;
			Campaign.Current.Models.AgeModel.GetAgeLimitForLocation(femaleBeggar, out minValue, out maxValue, "Beggar");
			AgentData agentData = new AgentData(new SimpleAgentOrigin(femaleBeggar, -1, null, default(UniqueTroopDescriptor))).Monster(monsterWithSuffix).Age(MBRandom.RandomInt(minValue, maxValue));
			return new LocationCharacter(agentData, new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddWandererBehaviors), "npc_beggar", true, relation, ActionSetCode.GenerateActionSetNameWithSuffix(agentData.AgentMonster, agentData.AgentIsFemale, "_beggar"), true, false, null, false, false, true);
		}

		// Token: 0x040002E7 RID: 743
		public const float TownsmanSpawnPercentageMale = 0.2f;

		// Token: 0x040002E8 RID: 744
		public const float TownsmanSpawnPercentageFemale = 0.15f;

		// Token: 0x040002E9 RID: 745
		public const float TownsmanSpawnPercentageLimitedMale = 0.15f;

		// Token: 0x040002EA RID: 746
		public const float TownsmanSpawnPercentageLimitedFemale = 0.1f;

		// Token: 0x040002EB RID: 747
		public const float TownOtherPeopleSpawnPercentage = 0.05f;

		// Token: 0x040002EC RID: 748
		public const float TownsmanSpawnPercentageTavernMale = 0.3f;

		// Token: 0x040002ED RID: 749
		public const float TownsmanSpawnPercentageTavernFemale = 0.1f;

		// Token: 0x040002EE RID: 750
		public const float BeggarSpawnPercentage = 0.33f;
	}
}
