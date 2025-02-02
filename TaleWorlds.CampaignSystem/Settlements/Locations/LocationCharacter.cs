using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.AgentOrigins;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.Settlements.Locations
{
	// Token: 0x0200036D RID: 877
	public class LocationCharacter
	{
		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x06003398 RID: 13208 RVA: 0x000D62F9 File Offset: 0x000D44F9
		public CharacterObject Character
		{
			get
			{
				return (CharacterObject)this.AgentData.AgentCharacter;
			}
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06003399 RID: 13209 RVA: 0x000D630B File Offset: 0x000D450B
		public IAgentOriginBase AgentOrigin
		{
			get
			{
				return this.AgentData.AgentOrigin;
			}
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x0600339A RID: 13210 RVA: 0x000D6318 File Offset: 0x000D4518
		public AgentData AgentData { get; }

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x0600339B RID: 13211 RVA: 0x000D6320 File Offset: 0x000D4520
		public bool UseCivilianEquipment { get; }

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x0600339C RID: 13212 RVA: 0x000D6328 File Offset: 0x000D4528
		public string ActionSetCode { get; }

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x0600339D RID: 13213 RVA: 0x000D6330 File Offset: 0x000D4530
		public string AlarmedActionSetCode { get; }

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x0600339E RID: 13214 RVA: 0x000D6338 File Offset: 0x000D4538
		// (set) Token: 0x0600339F RID: 13215 RVA: 0x000D6340 File Offset: 0x000D4540
		public string SpecialTargetTag { get; set; }

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x060033A0 RID: 13216 RVA: 0x000D6349 File Offset: 0x000D4549
		public LocationCharacter.AddBehaviorsDelegate AddBehaviors { get; }

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x060033A1 RID: 13217 RVA: 0x000D6351 File Offset: 0x000D4551
		public bool FixedLocation { get; }

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x060033A2 RID: 13218 RVA: 0x000D6359 File Offset: 0x000D4559
		// (set) Token: 0x060033A3 RID: 13219 RVA: 0x000D6361 File Offset: 0x000D4561
		public Alley MemberOfAlley { get; private set; }

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x060033A4 RID: 13220 RVA: 0x000D636A File Offset: 0x000D456A
		public ItemObject SpecialItem { get; }

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x060033A5 RID: 13221 RVA: 0x000D6372 File Offset: 0x000D4572
		// (set) Token: 0x060033A6 RID: 13222 RVA: 0x000D637A File Offset: 0x000D457A
		public bool IsHidden { get; set; }

		// Token: 0x060033A7 RID: 13223 RVA: 0x000D6384 File Offset: 0x000D4584
		public LocationCharacter(AgentData agentData, LocationCharacter.AddBehaviorsDelegate addBehaviorsDelegate, string spawnTag, bool fixedLocation, LocationCharacter.CharacterRelations characterRelation, string actionSetCode, bool useCivilianEquipment, bool isFixedCharacter = false, ItemObject specialItem = null, bool isHidden = false, bool isVisualTracked = false, bool overrideBodyProperties = true)
		{
			this.AgentData = agentData;
			if (Campaign.Current.GameMode == CampaignGameMode.Campaign)
			{
				int seed = -2;
				if (overrideBodyProperties)
				{
					seed = (isFixedCharacter ? (Settlement.CurrentSettlement.StringId.GetDeterministicHashCode() + this.Character.StringId.GetDeterministicHashCode()) : agentData.AgentEquipmentSeed);
				}
				this.AgentData.BodyProperties(this.Character.GetBodyProperties(this.Character.Equipment, seed));
			}
			this.AddBehaviors = addBehaviorsDelegate;
			this.SpecialTargetTag = spawnTag;
			this.FixedLocation = fixedLocation;
			this.ActionSetCode = (actionSetCode ?? TaleWorlds.Core.ActionSetCode.GenerateActionSetNameWithSuffix(this.AgentData.AgentMonster, this.AgentData.AgentCharacter.IsFemale, "_villager"));
			this.AlarmedActionSetCode = TaleWorlds.Core.ActionSetCode.GenerateActionSetNameWithSuffix(this.AgentData.AgentMonster, this.AgentData.AgentIsFemale, "_villager");
			this.PrefabNamesForBones = new Dictionary<sbyte, string>();
			this.CharacterRelation = characterRelation;
			this.SpecialItem = specialItem;
			this.UseCivilianEquipment = useCivilianEquipment;
			this.IsHidden = isHidden;
			this.IsVisualTracked = isVisualTracked;
		}

		// Token: 0x060033A8 RID: 13224 RVA: 0x000D64A2 File Offset: 0x000D46A2
		public void SetAlleyOfCharacter(Alley alley)
		{
			this.MemberOfAlley = alley;
		}

		// Token: 0x060033A9 RID: 13225 RVA: 0x000D64AC File Offset: 0x000D46AC
		public static LocationCharacter CreateBodyguardHero(Hero hero, MobileParty party, LocationCharacter.AddBehaviorsDelegate addBehaviorsDelegate)
		{
			UniqueTroopDescriptor uniqueNo = new UniqueTroopDescriptor(FlattenedTroopRoster.GenerateUniqueNoFromParty(party, 0));
			Monster monsterWithSuffix = FaceGen.GetMonsterWithSuffix(hero.CharacterObject.Race, "_settlement");
			return new LocationCharacter(new AgentData(new PartyAgentOrigin(PartyBase.MainParty, hero.CharacterObject, -1, uniqueNo, false)).Monster(monsterWithSuffix).NoHorses(true), addBehaviorsDelegate, null, false, LocationCharacter.CharacterRelations.Friendly, null, !PlayerEncounter.LocationEncounter.Settlement.IsVillage, false, null, false, false, true);
		}

		// Token: 0x060033AA RID: 13226 RVA: 0x000D6522 File Offset: 0x000D4722
		public override string ToString()
		{
			return this.Character.Name.ToString();
		}

		// Token: 0x040010A0 RID: 4256
		public bool IsVisualTracked;

		// Token: 0x040010A7 RID: 4263
		public Dictionary<sbyte, string> PrefabNamesForBones;

		// Token: 0x040010AA RID: 4266
		public LocationCharacter.CharacterRelations CharacterRelation;

		// Token: 0x020006B3 RID: 1715
		// (Invoke) Token: 0x0600569B RID: 22171
		public delegate void AddBehaviorsDelegate(IAgent agent);

		// Token: 0x020006B4 RID: 1716
		public enum CharacterRelations
		{
			// Token: 0x04001BD5 RID: 7125
			Neutral,
			// Token: 0x04001BD6 RID: 7126
			Friendly,
			// Token: 0x04001BD7 RID: 7127
			Enemy
		}
	}
}
