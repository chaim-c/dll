using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.AgentOrigins;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003DA RID: 986
	public class TownMerchantsCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003D1C RID: 15644 RVA: 0x0012A26D File Offset: 0x0012846D
		public override void RegisterEvents()
		{
			CampaignEvents.LocationCharactersAreReadyToSpawnEvent.AddNonSerializedListener(this, new Action<Dictionary<string, int>>(this.LocationCharactersAreReadyToSpawn));
		}

		// Token: 0x06003D1D RID: 15645 RVA: 0x0012A286 File Offset: 0x00128486
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06003D1E RID: 15646 RVA: 0x0012A288 File Offset: 0x00128488
		private void LocationCharactersAreReadyToSpawn(Dictionary<string, int> unusedUsablePointCount)
		{
			Location locationWithId = PlayerEncounter.LocationEncounter.Settlement.LocationComplex.GetLocationWithId("center");
			if (CampaignMission.Current.Location == locationWithId && Campaign.Current.IsDay)
			{
				this.AddTradersToCenter(unusedUsablePointCount);
			}
		}

		// Token: 0x06003D1F RID: 15647 RVA: 0x0012A2D0 File Offset: 0x001284D0
		private void AddTradersToCenter(Dictionary<string, int> unusedUsablePointCount)
		{
			Location locationWithId = Settlement.CurrentSettlement.LocationComplex.GetLocationWithId("center");
			int count;
			if (unusedUsablePointCount.TryGetValue("sp_merchant", out count))
			{
				locationWithId.AddLocationCharacters(new CreateLocationCharacterDelegate(TownMerchantsCampaignBehavior.CreateMerchant), Settlement.CurrentSettlement.Culture, LocationCharacter.CharacterRelations.Neutral, count);
			}
			if (unusedUsablePointCount.TryGetValue("sp_horse_merchant", out count))
			{
				locationWithId.AddLocationCharacters(new CreateLocationCharacterDelegate(TownMerchantsCampaignBehavior.CreateHorseTrader), Settlement.CurrentSettlement.Culture, LocationCharacter.CharacterRelations.Neutral, count);
			}
			if (unusedUsablePointCount.TryGetValue("sp_armorer", out count))
			{
				locationWithId.AddLocationCharacters(new CreateLocationCharacterDelegate(TownMerchantsCampaignBehavior.CreateArmorer), Settlement.CurrentSettlement.Culture, LocationCharacter.CharacterRelations.Neutral, count);
			}
			if (unusedUsablePointCount.TryGetValue("sp_weaponsmith", out count))
			{
				locationWithId.AddLocationCharacters(new CreateLocationCharacterDelegate(TownMerchantsCampaignBehavior.CreateWeaponsmith), Settlement.CurrentSettlement.Culture, LocationCharacter.CharacterRelations.Neutral, count);
			}
			if (unusedUsablePointCount.TryGetValue("sp_blacksmith", out count))
			{
				locationWithId.AddLocationCharacters(new CreateLocationCharacterDelegate(TownMerchantsCampaignBehavior.CreateBlacksmith), Settlement.CurrentSettlement.Culture, LocationCharacter.CharacterRelations.Neutral, count);
			}
		}

		// Token: 0x06003D20 RID: 15648 RVA: 0x0012A3D4 File Offset: 0x001285D4
		private static LocationCharacter CreateBlacksmith(CultureObject culture, LocationCharacter.CharacterRelations relation)
		{
			CharacterObject blacksmith = culture.Blacksmith;
			Monster monsterWithSuffix = FaceGen.GetMonsterWithSuffix(blacksmith.Race, "_settlement");
			int minValue;
			int maxValue;
			Campaign.Current.Models.AgeModel.GetAgeLimitForLocation(blacksmith, out minValue, out maxValue, "");
			return new LocationCharacter(new AgentData(new SimpleAgentOrigin(blacksmith, -1, null, default(UniqueTroopDescriptor))).Monster(monsterWithSuffix).Age(MBRandom.RandomInt(minValue, maxValue)), new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddWandererBehaviors), "sp_blacksmith", true, relation, null, true, false, null, false, false, true);
		}

		// Token: 0x06003D21 RID: 15649 RVA: 0x0012A46C File Offset: 0x0012866C
		private static LocationCharacter CreateMerchant(CultureObject culture, LocationCharacter.CharacterRelations relation)
		{
			CharacterObject merchant = culture.Merchant;
			Monster monsterWithSuffix = FaceGen.GetMonsterWithSuffix(merchant.Race, "_settlement");
			int minValue;
			int maxValue;
			Campaign.Current.Models.AgeModel.GetAgeLimitForLocation(merchant, out minValue, out maxValue, "");
			AgentData agentData = new AgentData(new SimpleAgentOrigin(merchant, -1, null, default(UniqueTroopDescriptor))).Monster(monsterWithSuffix).Age(MBRandom.RandomInt(minValue, maxValue));
			return new LocationCharacter(agentData, new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddWandererBehaviors), "sp_merchant", true, relation, ActionSetCode.GenerateActionSetNameWithSuffix(agentData.AgentMonster, agentData.AgentIsFemale, "_seller"), true, false, null, false, false, true);
		}

		// Token: 0x06003D22 RID: 15650 RVA: 0x0012A51C File Offset: 0x0012871C
		private static LocationCharacter CreateHorseTrader(CultureObject culture, LocationCharacter.CharacterRelations relation)
		{
			CharacterObject horseMerchant = culture.HorseMerchant;
			Monster monsterWithSuffix = FaceGen.GetMonsterWithSuffix(horseMerchant.Race, "_settlement");
			int minValue;
			int maxValue;
			Campaign.Current.Models.AgeModel.GetAgeLimitForLocation(horseMerchant, out minValue, out maxValue, "");
			AgentData agentData = new AgentData(new SimpleAgentOrigin(horseMerchant, -1, null, default(UniqueTroopDescriptor))).Monster(monsterWithSuffix).Age(MBRandom.RandomInt(minValue, maxValue));
			return new LocationCharacter(agentData, new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddWandererBehaviors), "sp_horse_merchant", true, relation, ActionSetCode.GenerateActionSetNameWithSuffix(agentData.AgentMonster, agentData.AgentIsFemale, "_seller"), true, false, null, false, false, true);
		}

		// Token: 0x06003D23 RID: 15651 RVA: 0x0012A5CC File Offset: 0x001287CC
		private static LocationCharacter CreateArmorer(CultureObject culture, LocationCharacter.CharacterRelations relation)
		{
			CharacterObject armorer = culture.Armorer;
			Monster monsterWithSuffix = FaceGen.GetMonsterWithSuffix(armorer.Race, "_settlement");
			int minValue;
			int maxValue;
			Campaign.Current.Models.AgeModel.GetAgeLimitForLocation(armorer, out minValue, out maxValue, "");
			AgentData agentData = new AgentData(new SimpleAgentOrigin(armorer, -1, null, default(UniqueTroopDescriptor))).Monster(monsterWithSuffix).Age(MBRandom.RandomInt(minValue, maxValue));
			return new LocationCharacter(agentData, new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddWandererBehaviors), "sp_armorer", true, relation, ActionSetCode.GenerateActionSetNameWithSuffix(agentData.AgentMonster, agentData.AgentIsFemale, "_seller"), true, false, null, false, false, true);
		}

		// Token: 0x06003D24 RID: 15652 RVA: 0x0012A67C File Offset: 0x0012887C
		private static LocationCharacter CreateWeaponsmith(CultureObject culture, LocationCharacter.CharacterRelations relation)
		{
			CharacterObject weaponsmith = culture.Weaponsmith;
			Monster monsterWithSuffix = FaceGen.GetMonsterWithSuffix(weaponsmith.Race, "_settlement");
			int minValue;
			int maxValue;
			Campaign.Current.Models.AgeModel.GetAgeLimitForLocation(weaponsmith, out minValue, out maxValue, "");
			AgentData agentData = new AgentData(new SimpleAgentOrigin(weaponsmith, -1, null, default(UniqueTroopDescriptor))).Monster(monsterWithSuffix).Age(MBRandom.RandomInt(minValue, maxValue));
			return new LocationCharacter(agentData, new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddWandererBehaviors), "sp_weaponsmith", true, relation, ActionSetCode.GenerateActionSetNameWithSuffix(agentData.AgentMonster, agentData.AgentIsFemale, "_weaponsmith"), true, false, null, false, false, true);
		}
	}
}
