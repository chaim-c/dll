using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Helpers;
using SandBox.Missions.AgentBehaviors;
using SandBox.Objects.AnimationPoints;
using SandBox.Objects.Usables;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.AgentOrigins;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Objects;
using TaleWorlds.MountAndBlade.Source.Objects;
using TaleWorlds.ObjectSystem;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x02000055 RID: 85
	public class MissionAgentHandler : MissionLogic
	{
		// Token: 0x06000352 RID: 850 RVA: 0x00015410 File Offset: 0x00013610
		public bool HasPassages()
		{
			List<UsableMachine> list;
			return this._usablePoints.TryGetValue("npc_passage", out list) && list.Count > 0;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0001543C File Offset: 0x0001363C
		public List<UsableMachine> TownPassageProps
		{
			get
			{
				List<UsableMachine> result;
				this._usablePoints.TryGetValue("npc_passage", out result);
				return result;
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00015460 File Offset: 0x00013660
		public MissionAgentHandler(Location location, CharacterObject conversationCharacter, string playerSpecialSpawnTag = null)
		{
			this._currentLocation = location;
			this._previousLocation = ((Campaign.Current.GameMode == CampaignGameMode.Campaign) ? Campaign.Current.GameMenuManager.PreviousLocation : null);
			if (this._previousLocation != null && !this._currentLocation.LocationsOfPassages.Contains(this._previousLocation))
			{
				Debug.FailedAssert(string.Concat(new object[]
				{
					"No passage from ",
					this._previousLocation.DoorName,
					" to ",
					this._currentLocation.DoorName
				}), "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox\\Missions\\MissionLogics\\MissionAgentHandler.cs", ".ctor", 77);
				this._previousLocation = null;
			}
			this._usablePoints = new Dictionary<string, List<UsableMachine>>();
			this._pairedUsablePoints = new Dictionary<string, List<UsableMachine>>();
			this._disabledPassages = new List<UsableMachine>();
			this._checkPossibleQuestTimer = new BasicMissionTimer();
			this._playerSpecialSpawnTag = playerSpecialSpawnTag;
			this._conversationCharacter = conversationCharacter;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00015548 File Offset: 0x00013748
		public override void OnCreated()
		{
			if (this._currentLocation != null)
			{
				CampaignMission.Current.Location = this._currentLocation;
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00015564 File Offset: 0x00013764
		public override void EarlyStart()
		{
			this._passageUsageTime = base.Mission.CurrentTime + 30f;
			this.GetAllProps();
			MapWeatherModel.WeatherEvent weatherEventInPosition = Campaign.Current.Models.MapWeatherModel.GetWeatherEventInPosition(Settlement.CurrentSettlement.Position2D);
			if (weatherEventInPosition != MapWeatherModel.WeatherEvent.HeavyRain && weatherEventInPosition != MapWeatherModel.WeatherEvent.Blizzard)
			{
				this.InitializePairedUsableObjects();
			}
			base.Mission.SetReportStuckAgentsMode(true);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x000155C7 File Offset: 0x000137C7
		public override void OnRenderingStarted()
		{
		}

		// Token: 0x06000358 RID: 856 RVA: 0x000155CC File Offset: 0x000137CC
		public override void OnMissionTick(float dt)
		{
			float currentTime = base.Mission.CurrentTime;
			if (currentTime > this._passageUsageTime)
			{
				this._passageUsageTime = currentTime + 30f;
				if (PlayerEncounter.LocationEncounter != null && LocationComplex.Current != null)
				{
					LocationComplex.Current.AgentPassageUsageTick();
				}
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00015614 File Offset: 0x00013814
		public override void OnRemoveBehavior()
		{
			foreach (Location location in LocationComplex.Current.GetListOfLocations())
			{
				if (location.StringId == "center" || location.StringId == "village_center" || location.StringId == "lordshall" || location.StringId == "prison" || location.StringId == "tavern" || location.StringId == "alley")
				{
					location.RemoveAllCharacters((LocationCharacter x) => !x.Character.IsHero);
				}
			}
			base.OnRemoveBehavior();
		}

		// Token: 0x0600035A RID: 858 RVA: 0x000156FC File Offset: 0x000138FC
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow)
		{
			if (affectedAgent.IsHuman && (agentState == AgentState.Killed || agentState == AgentState.Unconscious))
			{
				LocationCharacter locationCharacter = CampaignMission.Current.Location.GetLocationCharacter(affectedAgent.Origin);
				if (locationCharacter != null)
				{
					CampaignMission.Current.Location.RemoveLocationCharacter(locationCharacter);
					if (PlayerEncounter.LocationEncounter.GetAccompanyingCharacter(locationCharacter) != null && affectedAgent.State == AgentState.Killed)
					{
						PlayerEncounter.LocationEncounter.RemoveAccompanyingCharacter(locationCharacter);
					}
				}
			}
			foreach (Agent agent in base.Mission.Agents)
			{
				CampaignAgentComponent component = agent.GetComponent<CampaignAgentComponent>();
				if (component != null)
				{
					component.OnAgentRemoved(affectedAgent);
				}
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x000157B8 File Offset: 0x000139B8
		public override void OnMissionModeChange(MissionMode oldMissionMode, bool atStart)
		{
			if (!atStart)
			{
				foreach (Agent agent in base.Mission.Agents)
				{
					if (agent.IsHuman)
					{
						agent.SetAgentExcludeStateForFaceGroupId(MissionAgentHandler._disabledFaceId, agent.CurrentWatchState != Agent.WatchState.Alarmed);
					}
				}
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0001582C File Offset: 0x00013A2C
		private void InitializePairedUsableObjects()
		{
			Dictionary<string, List<UsableMachine>> dictionary = new Dictionary<string, List<UsableMachine>>();
			foreach (KeyValuePair<string, List<UsableMachine>> keyValuePair in this._usablePoints)
			{
				foreach (UsableMachine usableMachine in keyValuePair.Value)
				{
					using (List<StandingPoint>.Enumerator enumerator3 = usableMachine.StandingPoints.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							AnimationPoint animationPoint;
							if ((animationPoint = (enumerator3.Current as AnimationPoint)) != null && animationPoint.PairEntity != null)
							{
								if (this._pairedUsablePoints.ContainsKey(keyValuePair.Key))
								{
									if (!this._pairedUsablePoints[keyValuePair.Key].Contains(usableMachine))
									{
										this._pairedUsablePoints[keyValuePair.Key].Add(usableMachine);
									}
								}
								else
								{
									this._pairedUsablePoints.Add(keyValuePair.Key, new List<UsableMachine>
									{
										usableMachine
									});
								}
								if (dictionary.ContainsKey(keyValuePair.Key))
								{
									dictionary[keyValuePair.Key].Add(usableMachine);
								}
								else
								{
									dictionary.Add(keyValuePair.Key, new List<UsableMachine>
									{
										usableMachine
									});
								}
							}
						}
					}
				}
			}
			foreach (KeyValuePair<string, List<UsableMachine>> keyValuePair2 in dictionary)
			{
				foreach (KeyValuePair<string, List<UsableMachine>> keyValuePair3 in this._usablePoints)
				{
					foreach (UsableMachine item in dictionary[keyValuePair2.Key])
					{
						keyValuePair3.Value.Remove(item);
					}
				}
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00015AE4 File Offset: 0x00013CE4
		private void GetAllProps()
		{
			GameEntity gameEntity = base.Mission.Scene.FindEntityWithTag("navigation_mesh_deactivator");
			if (gameEntity != null)
			{
				NavigationMeshDeactivator firstScriptOfType = gameEntity.GetFirstScriptOfType<NavigationMeshDeactivator>();
				MissionAgentHandler._disabledFaceId = firstScriptOfType.DisableFaceWithId;
				MissionAgentHandler._disabledFaceIdForAnimals = firstScriptOfType.DisableFaceWithIdForAnimals;
			}
			this._usablePoints.Clear();
			foreach (UsableMachine usableMachine in base.Mission.MissionObjects.FindAllWithType<UsableMachine>())
			{
				foreach (string key in usableMachine.GameEntity.Tags)
				{
					if (!this._usablePoints.ContainsKey(key))
					{
						this._usablePoints.Add(key, new List<UsableMachine>());
					}
					this._usablePoints[key].Add(usableMachine);
				}
			}
			if (Settlement.CurrentSettlement.IsTown || Settlement.CurrentSettlement.IsVillage)
			{
				foreach (AreaMarker areaMarker in base.Mission.ActiveMissionObjects.FindAllWithType<AreaMarker>().ToList<AreaMarker>())
				{
					string tag = areaMarker.Tag;
					List<UsableMachine> usableMachinesInRange = areaMarker.GetUsableMachinesInRange(areaMarker.Tag.Contains("workshop") ? "unaffected_by_area" : null);
					if (!this._usablePoints.ContainsKey(tag))
					{
						this._usablePoints.Add(tag, new List<UsableMachine>());
					}
					foreach (UsableMachine usableMachine2 in usableMachinesInRange)
					{
						foreach (KeyValuePair<string, List<UsableMachine>> keyValuePair in this._usablePoints)
						{
							if (keyValuePair.Value.Contains(usableMachine2))
							{
								keyValuePair.Value.Remove(usableMachine2);
							}
						}
						if (usableMachine2.GameEntity.HasTag("hold_tag_always"))
						{
							string text = usableMachine2.GameEntity.Tags[0] + "_" + areaMarker.Tag;
							usableMachine2.GameEntity.AddTag(text);
							if (!this._usablePoints.ContainsKey(text))
							{
								this._usablePoints.Add(text, new List<UsableMachine>());
								this._usablePoints[text].Add(usableMachine2);
							}
							else
							{
								this._usablePoints[text].Add(usableMachine2);
							}
						}
						else
						{
							foreach (UsableMachine usableMachine3 in usableMachinesInRange)
							{
								if (!usableMachine3.GameEntity.HasTag(tag))
								{
									usableMachine3.GameEntity.AddTag(tag);
								}
							}
						}
					}
					if (this._usablePoints.ContainsKey(tag))
					{
						usableMachinesInRange.RemoveAll((UsableMachine x) => this._usablePoints[tag].Contains(x));
						if (usableMachinesInRange.Count > 0)
						{
							this._usablePoints[tag].AddRange(usableMachinesInRange);
						}
					}
					foreach (UsableMachine usableMachine4 in areaMarker.GetUsableMachinesWithTagInRange("unaffected_by_area"))
					{
						string key2 = usableMachine4.GameEntity.Tags[0];
						foreach (KeyValuePair<string, List<UsableMachine>> keyValuePair2 in this._usablePoints)
						{
							if (keyValuePair2.Value.Contains(usableMachine4))
							{
								keyValuePair2.Value.Remove(usableMachine4);
							}
						}
						if (this._usablePoints.ContainsKey(key2))
						{
							this._usablePoints[key2].Add(usableMachine4);
						}
						else
						{
							this._usablePoints.Add(key2, new List<UsableMachine>());
							this._usablePoints[key2].Add(usableMachine4);
						}
					}
				}
			}
			this.DisableUnavailableWaypoints();
			this.RemoveDeactivatedUsablePlacesFromList();
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00015FE4 File Offset: 0x000141E4
		[Conditional("DEBUG")]
		public void DetectMissingEntities()
		{
			if (CampaignMission.Current.Location != null && !Utilities.CommandLineArgumentExists("CampaignGameplayTest"))
			{
				IEnumerable<LocationCharacter> characterList = CampaignMission.Current.Location.GetCharacterList();
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				foreach (LocationCharacter locationCharacter in characterList)
				{
					if (locationCharacter.SpecialTargetTag != null && !locationCharacter.IsHidden)
					{
						if (dictionary.ContainsKey(locationCharacter.SpecialTargetTag))
						{
							Dictionary<string, int> dictionary2 = dictionary;
							string specialTargetTag = locationCharacter.SpecialTargetTag;
							int num = dictionary2[specialTargetTag];
							dictionary2[specialTargetTag] = num + 1;
						}
						else
						{
							dictionary.Add(locationCharacter.SpecialTargetTag, 1);
						}
					}
				}
				foreach (KeyValuePair<string, int> keyValuePair in dictionary)
				{
					string key = keyValuePair.Key;
					int value = keyValuePair.Value;
					int num2 = 0;
					if (this._usablePoints.ContainsKey(key))
					{
						num2 += this._usablePoints[key].Count;
						foreach (UsableMachine usableMachine in this._usablePoints[key])
						{
							num2 += MissionAgentHandler.GetPointCountOfUsableMachine(usableMachine, false);
						}
					}
					if (this._pairedUsablePoints.ContainsKey(key))
					{
						num2 += this._pairedUsablePoints[key].Count;
						foreach (UsableMachine usableMachine2 in this._pairedUsablePoints[key])
						{
							num2 += MissionAgentHandler.GetPointCountOfUsableMachine(usableMachine2, false);
						}
					}
					if (num2 < value)
					{
						string.Concat(new object[]
						{
							"Trying to spawn ",
							value,
							" npc with \"",
							key,
							"\" but there are ",
							num2,
							" suitable spawn points in scene ",
							base.Mission.SceneName
						});
						if (TestCommonBase.BaseInstance != null)
						{
							bool isTestEnabled = TestCommonBase.BaseInstance.IsTestEnabled;
						}
					}
				}
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0001627C File Offset: 0x0001447C
		public void RemoveDeactivatedUsablePlacesFromList()
		{
			Dictionary<string, List<UsableMachine>> dictionary = new Dictionary<string, List<UsableMachine>>();
			foreach (KeyValuePair<string, List<UsableMachine>> keyValuePair in this._usablePoints)
			{
				foreach (UsableMachine usableMachine in keyValuePair.Value)
				{
					if (usableMachine.IsDeactivated)
					{
						if (dictionary.ContainsKey(keyValuePair.Key))
						{
							dictionary[keyValuePair.Key].Add(usableMachine);
						}
						else
						{
							dictionary.Add(keyValuePair.Key, new List<UsableMachine>());
							dictionary[keyValuePair.Key].Add(usableMachine);
						}
					}
				}
			}
			foreach (KeyValuePair<string, List<UsableMachine>> keyValuePair2 in dictionary)
			{
				foreach (UsableMachine item in keyValuePair2.Value)
				{
					this._usablePoints[keyValuePair2.Key].Remove(item);
				}
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x000163F4 File Offset: 0x000145F4
		private Dictionary<string, int> FindUnusedUsablePointCount()
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			foreach (KeyValuePair<string, List<UsableMachine>> keyValuePair in this._usablePoints)
			{
				int num = 0;
				foreach (UsableMachine usableMachine in keyValuePair.Value)
				{
					num += MissionAgentHandler.GetPointCountOfUsableMachine(usableMachine, true);
				}
				if (num > 0)
				{
					dictionary.Add(keyValuePair.Key, num);
				}
			}
			foreach (KeyValuePair<string, List<UsableMachine>> keyValuePair2 in this._pairedUsablePoints)
			{
				int num2 = 0;
				foreach (UsableMachine usableMachine2 in keyValuePair2.Value)
				{
					num2 += MissionAgentHandler.GetPointCountOfUsableMachine(usableMachine2, true);
				}
				if (num2 > 0)
				{
					if (!dictionary.ContainsKey(keyValuePair2.Key))
					{
						dictionary.Add(keyValuePair2.Key, num2);
					}
					else
					{
						Dictionary<string, int> dictionary2 = dictionary;
						string key = keyValuePair2.Key;
						dictionary2[key] += num2;
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0001657C File Offset: 0x0001477C
		private CharacterObject GetPlayerCharacter()
		{
			CharacterObject characterObject = CharacterObject.PlayerCharacter;
			if (characterObject == null)
			{
				characterObject = Game.Current.ObjectManager.GetObject<CharacterObject>("main_hero_for_perf");
			}
			return characterObject;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x000165A8 File Offset: 0x000147A8
		public void SpawnPlayer(bool civilianEquipment = false, bool noHorses = false, bool noWeapon = false, bool wieldInitialWeapons = false, bool isStealth = false, string spawnTag = "")
		{
			if (Campaign.Current.GameMode != CampaignGameMode.Campaign)
			{
				civilianEquipment = false;
			}
			MatrixFrame matrixFrame = MatrixFrame.Identity;
			GameEntity gameEntity = base.Mission.Scene.FindEntityWithTag("spawnpoint_player");
			if (gameEntity != null)
			{
				matrixFrame = gameEntity.GetGlobalFrame();
				matrixFrame.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
			}
			bool flag = Campaign.Current.GameMode == CampaignGameMode.Campaign && PlayerEncounter.IsActive && (Settlement.CurrentSettlement.IsTown || Settlement.CurrentSettlement.IsCastle) && !Campaign.Current.IsNight && CampaignMission.Current.Location.StringId == "center" && !PlayerEncounter.LocationEncounter.IsInsideOfASettlement;
			bool flag2 = false;
			if (this._playerSpecialSpawnTag != null)
			{
				GameEntity gameEntity2 = null;
				UsableMachine usableMachine = this.GetAllUsablePointsWithTag(this._playerSpecialSpawnTag).FirstOrDefault<UsableMachine>();
				if (usableMachine != null)
				{
					StandingPoint standingPoint = usableMachine.StandingPoints.FirstOrDefault<StandingPoint>();
					gameEntity2 = ((standingPoint != null) ? standingPoint.GameEntity : null);
				}
				if (gameEntity2 == null)
				{
					gameEntity2 = base.Mission.Scene.FindEntityWithTag(this._playerSpecialSpawnTag);
				}
				if (gameEntity2 != null)
				{
					matrixFrame = gameEntity2.GetGlobalFrame();
					matrixFrame.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
				}
			}
			else if (CampaignMission.Current.Location.StringId == "arena")
			{
				GameEntity gameEntity3 = base.Mission.Scene.FindEntityWithTag("sp_player_near_arena_master");
				if (gameEntity3 != null)
				{
					matrixFrame = gameEntity3.GetGlobalFrame();
					matrixFrame.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
				}
			}
			else if (this._previousLocation != null)
			{
				matrixFrame = this.GetSpawnFrameOfPassage(this._previousLocation);
				matrixFrame.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
				noHorses = true;
				flag2 = true;
			}
			else if (flag)
			{
				GameEntity gameEntity4 = base.Mission.Scene.FindEntityWithTag(isStealth ? "sp_player_stealth" : "spawnpoint_player_outside");
				if (gameEntity4 != null)
				{
					matrixFrame = gameEntity4.GetGlobalFrame();
					matrixFrame.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
				}
			}
			else
			{
				GameEntity gameEntity5 = base.Mission.Scene.FindEntityWithTag("spawnpoint_player");
				if (gameEntity5 != null)
				{
					matrixFrame = gameEntity5.GetGlobalFrame();
					matrixFrame.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
				}
			}
			if (PlayerEncounter.LocationEncounter is TownEncounter)
			{
				PlayerEncounter.LocationEncounter.IsInsideOfASettlement = true;
			}
			CharacterObject playerCharacter = this.GetPlayerCharacter();
			AgentBuildData agentBuildData = new AgentBuildData(playerCharacter).Team(base.Mission.PlayerTeam).InitialPosition(matrixFrame.origin);
			Vec2 vec = matrixFrame.rotation.f.AsVec2;
			vec = vec.Normalized();
			AgentBuildData agentBuildData2 = agentBuildData.InitialDirection(vec).CivilianEquipment(civilianEquipment).NoHorses(noHorses).NoWeapons(noWeapon).ClothingColor1(base.Mission.PlayerTeam.Color).ClothingColor2(base.Mission.PlayerTeam.Color2).TroopOrigin(new PartyAgentOrigin(PartyBase.MainParty, this.GetPlayerCharacter(), -1, default(UniqueTroopDescriptor), false)).MountKey(MountCreationKey.GetRandomMountKeyString(playerCharacter.Equipment[EquipmentIndex.ArmorItemEndSlot].Item, playerCharacter.GetMountKeySeed())).Controller(Agent.ControllerType.Player);
			Hero heroObject = playerCharacter.HeroObject;
			if (((heroObject != null) ? heroObject.ClanBanner : null) != null)
			{
				agentBuildData2.Banner(playerCharacter.HeroObject.ClanBanner);
			}
			if (Campaign.Current.GameMode != CampaignGameMode.Campaign)
			{
				agentBuildData2.TroopOrigin(new SimpleAgentOrigin(CharacterObject.PlayerCharacter, -1, null, default(UniqueTroopDescriptor)));
			}
			if (isStealth)
			{
				agentBuildData2.Equipment(this.GetStealthEquipmentForPlayer());
			}
			else if (Campaign.Current.IsMainHeroDisguised)
			{
				Equipment defaultEquipment = MBObjectManager.Instance.GetObject<MBEquipmentRoster>("npc_disguised_hero_equipment_template").DefaultEquipment;
				Equipment firstCivilianEquipment = CharacterObject.PlayerCharacter.FirstCivilianEquipment;
				for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumAllWeaponSlots; equipmentIndex++)
				{
					ItemObject item = firstCivilianEquipment[equipmentIndex].Item;
					defaultEquipment.AddEquipmentToSlotWithoutAgent(equipmentIndex, new EquipmentElement(item, null, null, false));
				}
				agentBuildData2.Equipment(defaultEquipment);
			}
			Agent agent = base.Mission.SpawnAgent(agentBuildData2, false);
			if (wieldInitialWeapons)
			{
				agent.WieldInitialWeapons(Agent.WeaponWieldActionType.InstantAfterPickUp, Equipment.InitialWeaponEquipPreference.Any);
			}
			if (flag2)
			{
				base.Mission.MakeSound(MiscSoundContainer.SoundCodeMovementFoleyDoorClose, matrixFrame.origin, true, false, -1, -1);
			}
			this.SpawnCharactersAccompanyingPlayer(noHorses);
			for (int i = 0; i < 3; i++)
			{
				Agent.Main.AgentVisuals.GetSkeleton().TickAnimations(0.1f, Agent.Main.AgentVisuals.GetGlobalFrame(), true);
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00016A28 File Offset: 0x00014C28
		private Equipment GetStealthEquipmentForPlayer()
		{
			Equipment equipment = new Equipment();
			equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Body, new EquipmentElement(Game.Current.ObjectManager.GetObject<ItemObject>("ragged_robes"), null, null, false));
			equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.NumAllWeaponSlots, new EquipmentElement(Game.Current.ObjectManager.GetObject<ItemObject>("pilgrim_hood"), null, null, false));
			equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Leg, new EquipmentElement(Game.Current.ObjectManager.GetObject<ItemObject>("ragged_boots"), null, null, false));
			equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Gloves, new EquipmentElement(Game.Current.ObjectManager.GetObject<ItemObject>("ragged_armwraps"), null, null, false));
			for (int i = 0; i < 5; i++)
			{
				EquipmentElement equipmentFromSlot = CharacterObject.PlayerCharacter.Equipment.GetEquipmentFromSlot((EquipmentIndex)i);
				if (equipmentFromSlot.Item != null)
				{
					equipment.AddEquipmentToSlotWithoutAgent((EquipmentIndex)i, new EquipmentElement(equipmentFromSlot.Item, null, null, false));
				}
				else if (i >= 0 && i <= 3)
				{
					equipment.AddEquipmentToSlotWithoutAgent((EquipmentIndex)i, new EquipmentElement(Game.Current.ObjectManager.GetObject<ItemObject>("throwing_stone"), null, null, false));
				}
			}
			return equipment;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00016B34 File Offset: 0x00014D34
		private MatrixFrame GetSpawnFrameOfPassage(Location location)
		{
			MatrixFrame result = MatrixFrame.Identity;
			UsableMachine usableMachine = this.TownPassageProps.FirstOrDefault((UsableMachine x) => ((Passage)x).ToLocation == location) ?? this._disabledPassages.FirstOrDefault((UsableMachine x) => ((Passage)x).ToLocation == location);
			if (usableMachine != null)
			{
				MatrixFrame globalFrame = usableMachine.PilotStandingPoint.GameEntity.GetGlobalFrame();
				globalFrame.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
				globalFrame.origin.z = base.Mission.Scene.GetGroundHeightAtPosition(globalFrame.origin, BodyFlags.CommonCollisionExcludeFlags);
				globalFrame.rotation.RotateAboutUp(3.1415927f);
				result = globalFrame;
			}
			return result;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00016BE4 File Offset: 0x00014DE4
		public void DisableUnavailableWaypoints()
		{
			bool isNight = Campaign.Current.IsNight;
			string text = "";
			int num = 0;
			foreach (KeyValuePair<string, List<UsableMachine>> keyValuePair in this._usablePoints)
			{
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				int i = 0;
				while (i < keyValuePair.Value.Count)
				{
					UsableMachine usableMachine = keyValuePair.Value[i];
					if (!Mission.Current.IsPositionInsideBoundaries(usableMachine.GameEntity.GlobalPosition.AsVec2))
					{
						foreach (StandingPoint standingPoint in usableMachine.StandingPoints)
						{
							standingPoint.IsDeactivated = true;
							num++;
						}
					}
					if (usableMachine is Chair)
					{
						using (List<StandingPoint>.Enumerator enumerator2 = usableMachine.StandingPoints.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								StandingPoint standingPoint2 = enumerator2.Current;
								Vec3 origin = standingPoint2.GameEntity.GetGlobalFrame().origin;
								PathFaceRecord nullFaceRecord = PathFaceRecord.NullFaceRecord;
								base.Mission.Scene.GetNavMeshFaceIndex(ref nullFaceRecord, origin, true);
								if (!nullFaceRecord.IsValid() || (MissionAgentHandler._disabledFaceId != -1 && nullFaceRecord.FaceGroupIndex == MissionAgentHandler._disabledFaceId))
								{
									standingPoint2.IsDeactivated = true;
									num2++;
								}
							}
							goto IL_2B2;
						}
						goto IL_146;
					}
					goto IL_146;
					IL_2B2:
					i++;
					continue;
					IL_146:
					if (usableMachine is Passage)
					{
						Passage passage = usableMachine as Passage;
						if (passage.ToLocation == null || !passage.ToLocation.CanPlayerSee())
						{
							foreach (StandingPoint standingPoint3 in passage.StandingPoints)
							{
								standingPoint3.IsDeactivated = true;
							}
							passage.Disable();
							this._disabledPassages.Add(usableMachine);
							Location toLocation = passage.ToLocation;
							keyValuePair.Value.RemoveAt(i);
							i--;
							num3++;
							goto IL_2B2;
						}
						goto IL_2B2;
					}
					else
					{
						if (usableMachine is UsablePlace)
						{
							foreach (StandingPoint standingPoint4 in usableMachine.StandingPoints)
							{
								Vec3 origin2 = standingPoint4.GameEntity.GetGlobalFrame().origin;
								PathFaceRecord nullFaceRecord2 = PathFaceRecord.NullFaceRecord;
								base.Mission.Scene.GetNavMeshFaceIndex(ref nullFaceRecord2, origin2, true);
								if (!nullFaceRecord2.IsValid() || (MissionAgentHandler._disabledFaceId != -1 && nullFaceRecord2.FaceGroupIndex == MissionAgentHandler._disabledFaceId) || (isNight && usableMachine.GameEntity.HasTag("disable_at_night")) || (!isNight && usableMachine.GameEntity.HasTag("enable_at_night")))
								{
									standingPoint4.IsDeactivated = true;
									num4++;
								}
							}
							goto IL_2B2;
						}
						goto IL_2B2;
					}
				}
				if (num4 + num2 + num3 > 0)
				{
					text = text + "_____________________________________________\n\"" + keyValuePair.Key + "\" :\n";
					if (num4 > 0)
					{
						text = string.Concat(new object[]
						{
							text,
							"Disabled standing point : ",
							num4,
							"\n"
						});
					}
					if (num2 > 0)
					{
						text = string.Concat(new object[]
						{
							text,
							"Disabled chair use point : ",
							num2,
							"\n"
						});
					}
					if (num3 > 0)
					{
						text = string.Concat(new object[]
						{
							text,
							"Disabled passage info : ",
							num3,
							"\n"
						});
					}
				}
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0001700C File Offset: 0x0001520C
		public void SpawnLocationCharacters(string overridenTagValue = null)
		{
			Dictionary<string, int> unusedUsablePointCount = this.FindUnusedUsablePointCount();
			IEnumerable<LocationCharacter> characterList = CampaignMission.Current.Location.GetCharacterList();
			if (PlayerEncounter.LocationEncounter.Settlement.IsTown && CampaignMission.Current.Location == LocationComplex.Current.GetLocationWithId("center"))
			{
				foreach (LocationCharacter element in LocationComplex.Current.GetLocationWithId("alley").GetCharacterList())
				{
					characterList.Append(element);
				}
			}
			CampaignEventDispatcher.Instance.LocationCharactersAreReadyToSpawn(unusedUsablePointCount);
			foreach (LocationCharacter locationCharacter in characterList)
			{
				if (!this.IsAlreadySpawned(locationCharacter.AgentOrigin) && !locationCharacter.IsHidden)
				{
					if (!string.IsNullOrEmpty(overridenTagValue))
					{
						locationCharacter.SpecialTargetTag = overridenTagValue;
					}
					MissionAgentHandler.SetAgentExcludeFaceGroupIdAux(this.SpawnLocationCharacter(locationCharacter, false), MissionAgentHandler._disabledFaceId);
				}
			}
			List<Passage> list = new List<Passage>();
			if (this.TownPassageProps != null)
			{
				foreach (UsableMachine usableMachine in this.TownPassageProps)
				{
					Passage passage;
					if ((passage = (usableMachine as Passage)) != null && !usableMachine.IsDeactivated)
					{
						passage.Deactivate();
						list.Add(passage);
					}
				}
			}
			foreach (Agent agent in base.Mission.Agents)
			{
				this.SimulateAgent(agent);
			}
			foreach (Passage passage2 in list)
			{
				passage2.Activate();
			}
			CampaignEventDispatcher.Instance.LocationCharactersSimulated();
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00017224 File Offset: 0x00015424
		private bool IsAlreadySpawned(IAgentOriginBase agentOrigin)
		{
			return Mission.Current != null && Mission.Current.Agents.Any((Agent x) => x.Origin == agentOrigin);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00017264 File Offset: 0x00015464
		public Agent SpawnLocationCharacter(LocationCharacter locationCharacter, bool simulateAgentAfterSpawn = false)
		{
			Agent agent = this.SpawnWanderingAgent(locationCharacter);
			if (simulateAgentAfterSpawn)
			{
				this.SimulateAgent(agent);
			}
			if (locationCharacter.IsVisualTracked)
			{
				VisualTrackerMissionBehavior missionBehavior = Mission.Current.GetMissionBehavior<VisualTrackerMissionBehavior>();
				if (missionBehavior != null)
				{
					missionBehavior.RegisterLocalOnlyObject(agent);
				}
			}
			return agent;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000172A4 File Offset: 0x000154A4
		public void SimulateAgent(Agent agent)
		{
			if (agent.IsHuman)
			{
				AgentNavigator agentNavigator = agent.GetComponent<CampaignAgentComponent>().AgentNavigator;
				int num = MBRandom.RandomInt(35, 50);
				agent.PreloadForRendering();
				for (int i = 0; i < num; i++)
				{
					if (agentNavigator != null)
					{
						agentNavigator.Tick(0.1f, true);
					}
					if (agent.IsUsingGameObject)
					{
						agent.CurrentlyUsedGameObject.SimulateTick(0.1f);
					}
				}
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00017308 File Offset: 0x00015508
		public void SpawnThugs()
		{
			IEnumerable<LocationCharacter> characterList = CampaignMission.Current.Location.GetCharacterList();
			List<MatrixFrame> list = (from x in base.Mission.Scene.FindEntitiesWithTag("spawnpoint_thug")
			select x.GetGlobalFrame()).ToList<MatrixFrame>();
			int num = 0;
			foreach (LocationCharacter locationCharacter in characterList)
			{
				if (locationCharacter.CharacterRelation == LocationCharacter.CharacterRelations.Enemy)
				{
					MissionAgentHandler.SetAgentExcludeFaceGroupIdAux(this.SpawnWanderingAgentWithInitialFrame(locationCharacter, list[num % list.Count], true), MissionAgentHandler._disabledFaceId);
					num++;
				}
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x000173C8 File Offset: 0x000155C8
		private void GetFrameForFollowingAgent(Agent followedAgent, out MatrixFrame frame)
		{
			frame = followedAgent.Frame;
			frame.origin += -(frame.rotation.f * 1.5f);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00017408 File Offset: 0x00015608
		public void SpawnCharactersAccompanyingPlayer(bool noHorse)
		{
			int num = 0;
			bool flag = PlayerEncounter.LocationEncounter.CharactersAccompanyingPlayer.Any((AccompanyingCharacter c) => c.IsFollowingPlayerAtMissionStart);
			GameEntity gameEntity = base.Mission.Scene.FindEntityWithTag("navigation_mesh_deactivator");
			foreach (AccompanyingCharacter accompanyingCharacter in PlayerEncounter.LocationEncounter.CharactersAccompanyingPlayer)
			{
				bool flag2 = accompanyingCharacter.LocationCharacter.Character.IsHero && accompanyingCharacter.LocationCharacter.Character.HeroObject.IsWounded;
				if ((this._currentLocation.GetCharacterList().Contains(accompanyingCharacter.LocationCharacter) || !flag2) && accompanyingCharacter.CanEnterLocation(this._currentLocation))
				{
					this._currentLocation.AddCharacter(accompanyingCharacter.LocationCharacter);
					if (accompanyingCharacter.IsFollowingPlayerAtMissionStart || (!flag && num == 0))
					{
						WorldFrame worldFrame = base.Mission.MainAgent.GetWorldFrame();
						worldFrame.Origin.SetVec2(base.Mission.GetRandomPositionAroundPoint(worldFrame.Origin.GetNavMeshVec3(), 0.5f, 2f, false).AsVec2);
						Agent agent = this.SpawnWanderingAgentWithInitialFrame(accompanyingCharacter.LocationCharacter, worldFrame.ToGroundMatrixFrame(), noHorse);
						if (gameEntity != null)
						{
							int disableFaceWithId = gameEntity.GetFirstScriptOfType<NavigationMeshDeactivator>().DisableFaceWithId;
							if (disableFaceWithId != -1)
							{
								agent.SetAgentExcludeStateForFaceGroupId(disableFaceWithId, false);
							}
						}
						int num2 = 0;
						for (;;)
						{
							Agent agent2 = agent;
							Vec2 asVec = base.Mission.MainAgent.Position.AsVec2;
							if (agent2.CanMoveDirectlyToPosition(asVec) || num2 >= 50)
							{
								break;
							}
							worldFrame.Origin.SetVec2(base.Mission.GetRandomPositionAroundPoint(worldFrame.Origin.GetNavMeshVec3(), 0.5f, 4f, false).AsVec2);
							agent.TeleportToPosition(worldFrame.ToGroundMatrixFrame().origin);
							num2++;
						}
						agent.SetTeam(base.Mission.PlayerTeam, true);
						num++;
					}
					else
					{
						this.SpawnWanderingAgent(accompanyingCharacter.LocationCharacter).SetTeam(base.Mission.PlayerTeam, true);
					}
					foreach (Agent agent3 in base.Mission.Agents)
					{
						LocationCharacter locationCharacter = CampaignMission.Current.Location.GetLocationCharacter(agent3.Origin);
						AccompanyingCharacter accompanyingCharacter2 = PlayerEncounter.LocationEncounter.GetAccompanyingCharacter(locationCharacter);
						if (agent3.GetComponent<CampaignAgentComponent>().AgentNavigator != null && accompanyingCharacter2 != null)
						{
							DailyBehaviorGroup behaviorGroup = agent3.GetComponent<CampaignAgentComponent>().AgentNavigator.GetBehaviorGroup<DailyBehaviorGroup>();
							if (accompanyingCharacter.IsFollowingPlayerAtMissionStart)
							{
								FollowAgentBehavior followAgentBehavior = behaviorGroup.GetBehavior<FollowAgentBehavior>() ?? behaviorGroup.AddBehavior<FollowAgentBehavior>();
								behaviorGroup.SetScriptedBehavior<FollowAgentBehavior>();
								followAgentBehavior.SetTargetAgent(Agent.Main);
							}
							else
							{
								behaviorGroup.Behaviors.Clear();
							}
						}
					}
				}
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00017754 File Offset: 0x00015954
		public void FadeoutExitingLocationCharacter(LocationCharacter locationCharacter)
		{
			foreach (Agent agent in Mission.Current.Agents)
			{
				if ((CharacterObject)agent.Character == locationCharacter.Character)
				{
					agent.FadeOut(false, true);
					break;
				}
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x000177C4 File Offset: 0x000159C4
		public void SpawnEnteringLocationCharacter(LocationCharacter locationCharacter, Location fromLocation)
		{
			if (fromLocation != null)
			{
				foreach (UsableMachine usableMachine in this.TownPassageProps)
				{
					Passage passage = usableMachine as Passage;
					if (passage.ToLocation == fromLocation)
					{
						MatrixFrame globalFrame = passage.PilotStandingPoint.GameEntity.GetGlobalFrame();
						globalFrame.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
						globalFrame.origin.z = base.Mission.Scene.GetGroundHeightAtPosition(globalFrame.origin, BodyFlags.CommonCollisionExcludeFlags);
						Vec3 f = globalFrame.rotation.f;
						f.Normalize();
						globalFrame.origin -= 0.3f * f;
						globalFrame.rotation.RotateAboutUp(3.1415927f);
						Agent agent = this.SpawnWanderingAgentWithInitialFrame(locationCharacter, globalFrame, true);
						MissionAgentHandler.SetAgentExcludeFaceGroupIdAux(agent, MissionAgentHandler._disabledFaceId);
						base.Mission.MakeSound(MiscSoundContainer.SoundCodeMovementFoleyDoorClose, globalFrame.origin, true, false, -1, -1);
						agent.FadeIn();
						break;
					}
				}
				return;
			}
			this.SpawnLocationCharacter(locationCharacter, true);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00017900 File Offset: 0x00015B00
		private static void SimulateAnimalAnimations(Agent agent)
		{
			int num = 10 + MBRandom.RandomInt(90);
			for (int i = 0; i < num; i++)
			{
				agent.TickActionChannels(0.1f);
				Vec3 v = agent.ComputeAnimationDisplacement(0.1f);
				if (v.LengthSquared > 0f)
				{
					agent.TeleportToPosition(agent.Position + v);
				}
				agent.AgentVisuals.GetSkeleton().TickAnimations(0.1f, agent.AgentVisuals.GetGlobalFrame(), true);
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0001797C File Offset: 0x00015B7C
		public static void SpawnSheeps()
		{
			foreach (GameEntity gameEntity in Mission.Current.Scene.FindEntitiesWithTag("sp_sheep"))
			{
				MatrixFrame globalFrame = gameEntity.GetGlobalFrame();
				ItemRosterElement itemRosterElement = new ItemRosterElement(Game.Current.ObjectManager.GetObject<ItemObject>("sheep"), 0, null);
				globalFrame.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
				Mission mission = Mission.Current;
				ItemRosterElement rosterElement = itemRosterElement;
				ItemRosterElement harnessRosterElement = default(ItemRosterElement);
				Vec2 asVec = globalFrame.rotation.f.AsVec2;
				Agent agent = mission.SpawnMonster(rosterElement, harnessRosterElement, globalFrame.origin, asVec, -1);
				MissionAgentHandler.SetAgentExcludeFaceGroupIdAux(agent, MissionAgentHandler._disabledFaceId);
				MissionAgentHandler.SetAgentExcludeFaceGroupIdAux(agent, MissionAgentHandler._disabledFaceIdForAnimals);
				AnimalSpawnSettings.CheckAndSetAnimalAgentFlags(gameEntity, agent);
				MissionAgentHandler.SimulateAnimalAnimations(agent);
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00017A5C File Offset: 0x00015C5C
		public static void SpawnCows()
		{
			foreach (GameEntity gameEntity in Mission.Current.Scene.FindEntitiesWithTag("sp_cow"))
			{
				MatrixFrame globalFrame = gameEntity.GetGlobalFrame();
				ItemRosterElement itemRosterElement = new ItemRosterElement(Game.Current.ObjectManager.GetObject<ItemObject>("cow"), 0, null);
				globalFrame.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
				Mission mission = Mission.Current;
				ItemRosterElement rosterElement = itemRosterElement;
				ItemRosterElement harnessRosterElement = default(ItemRosterElement);
				Vec2 asVec = globalFrame.rotation.f.AsVec2;
				Agent agent = mission.SpawnMonster(rosterElement, harnessRosterElement, globalFrame.origin, asVec, -1);
				MissionAgentHandler.SetAgentExcludeFaceGroupIdAux(agent, MissionAgentHandler._disabledFaceId);
				MissionAgentHandler.SetAgentExcludeFaceGroupIdAux(agent, MissionAgentHandler._disabledFaceIdForAnimals);
				AnimalSpawnSettings.CheckAndSetAnimalAgentFlags(gameEntity, agent);
				MissionAgentHandler.SimulateAnimalAnimations(agent);
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00017B3C File Offset: 0x00015D3C
		public static void SpawnGeese()
		{
			foreach (GameEntity gameEntity in Mission.Current.Scene.FindEntitiesWithTag("sp_goose"))
			{
				MatrixFrame globalFrame = gameEntity.GetGlobalFrame();
				ItemRosterElement itemRosterElement = new ItemRosterElement(Game.Current.ObjectManager.GetObject<ItemObject>("goose"), 0, null);
				globalFrame.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
				Mission mission = Mission.Current;
				ItemRosterElement rosterElement = itemRosterElement;
				ItemRosterElement harnessRosterElement = default(ItemRosterElement);
				Vec2 asVec = globalFrame.rotation.f.AsVec2;
				Agent agent = mission.SpawnMonster(rosterElement, harnessRosterElement, globalFrame.origin, asVec, -1);
				MissionAgentHandler.SetAgentExcludeFaceGroupIdAux(agent, MissionAgentHandler._disabledFaceId);
				MissionAgentHandler.SetAgentExcludeFaceGroupIdAux(agent, MissionAgentHandler._disabledFaceIdForAnimals);
				AnimalSpawnSettings.CheckAndSetAnimalAgentFlags(gameEntity, agent);
				MissionAgentHandler.SimulateAnimalAnimations(agent);
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00017C1C File Offset: 0x00015E1C
		public static void SpawnChicken()
		{
			foreach (GameEntity gameEntity in Mission.Current.Scene.FindEntitiesWithTag("sp_chicken"))
			{
				MatrixFrame globalFrame = gameEntity.GetGlobalFrame();
				ItemRosterElement itemRosterElement = new ItemRosterElement(Game.Current.ObjectManager.GetObject<ItemObject>("chicken"), 0, null);
				globalFrame.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
				Mission mission = Mission.Current;
				ItemRosterElement rosterElement = itemRosterElement;
				ItemRosterElement harnessRosterElement = default(ItemRosterElement);
				Vec2 asVec = globalFrame.rotation.f.AsVec2;
				Agent agent = mission.SpawnMonster(rosterElement, harnessRosterElement, globalFrame.origin, asVec, -1);
				MissionAgentHandler.SetAgentExcludeFaceGroupIdAux(agent, MissionAgentHandler._disabledFaceId);
				MissionAgentHandler.SetAgentExcludeFaceGroupIdAux(agent, MissionAgentHandler._disabledFaceIdForAnimals);
				AnimalSpawnSettings.CheckAndSetAnimalAgentFlags(gameEntity, agent);
				MissionAgentHandler.SimulateAnimalAnimations(agent);
			}
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00017CFC File Offset: 0x00015EFC
		public static void SpawnHogs()
		{
			foreach (GameEntity gameEntity in Mission.Current.Scene.FindEntitiesWithTag("sp_hog"))
			{
				MatrixFrame globalFrame = gameEntity.GetGlobalFrame();
				ItemRosterElement itemRosterElement = new ItemRosterElement(Game.Current.ObjectManager.GetObject<ItemObject>("hog"), 0, null);
				globalFrame.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
				Mission mission = Mission.Current;
				ItemRosterElement rosterElement = itemRosterElement;
				ItemRosterElement harnessRosterElement = default(ItemRosterElement);
				Vec2 asVec = globalFrame.rotation.f.AsVec2;
				Agent agent = mission.SpawnMonster(rosterElement, harnessRosterElement, globalFrame.origin, asVec, -1);
				MissionAgentHandler.SetAgentExcludeFaceGroupIdAux(agent, MissionAgentHandler._disabledFaceId);
				MissionAgentHandler.SetAgentExcludeFaceGroupIdAux(agent, MissionAgentHandler._disabledFaceIdForAnimals);
				AnimalSpawnSettings.CheckAndSetAnimalAgentFlags(gameEntity, agent);
				MissionAgentHandler.SimulateAnimalAnimations(agent);
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00017DDC File Offset: 0x00015FDC
		public static List<Agent> SpawnHorses()
		{
			List<Agent> list = new List<Agent>();
			foreach (GameEntity gameEntity in Mission.Current.Scene.FindEntitiesWithTag("sp_horse"))
			{
				MatrixFrame globalFrame = gameEntity.GetGlobalFrame();
				string objectName = gameEntity.Tags[1];
				ItemObject @object = MBObjectManager.Instance.GetObject<ItemObject>(objectName);
				ItemRosterElement itemRosterElement = new ItemRosterElement(@object, 1, null);
				ItemRosterElement itemRosterElement2 = default(ItemRosterElement);
				if (@object.HasHorseComponent)
				{
					globalFrame.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
					Mission mission = Mission.Current;
					ItemRosterElement rosterElement = itemRosterElement;
					ItemRosterElement harnessRosterElement = itemRosterElement2;
					Vec2 asVec = globalFrame.rotation.f.AsVec2;
					Agent agent = mission.SpawnMonster(rosterElement, harnessRosterElement, globalFrame.origin, asVec, -1);
					AnimalSpawnSettings.CheckAndSetAnimalAgentFlags(gameEntity, agent);
					MissionAgentHandler.SimulateAnimalAnimations(agent);
					list.Add(agent);
				}
			}
			return list;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00017EC8 File Offset: 0x000160C8
		public IEnumerable<string> GetAllSpawnTags()
		{
			return this._usablePoints.Keys.ToList<string>().Concat(this._pairedUsablePoints.Keys.ToList<string>());
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00017EF0 File Offset: 0x000160F0
		public List<UsableMachine> GetAllUsablePointsWithTag(string tag)
		{
			List<UsableMachine> list = new List<UsableMachine>();
			List<UsableMachine> collection = new List<UsableMachine>();
			if (this._usablePoints.TryGetValue(tag, out collection))
			{
				list.AddRange(collection);
			}
			List<UsableMachine> collection2 = new List<UsableMachine>();
			if (this._pairedUsablePoints.TryGetValue(tag, out collection2))
			{
				list.AddRange(collection2);
			}
			return list;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00017F40 File Offset: 0x00016140
		private bool GetSpawnDataForTag(string targetTag, out MatrixFrame spawnFrame, out UsableMachine usableMachine)
		{
			List<UsableMachine> allUsablePointsWithTag = this.GetAllUsablePointsWithTag(targetTag);
			spawnFrame = MatrixFrame.Identity;
			usableMachine = null;
			if (allUsablePointsWithTag.Count > 0)
			{
				foreach (UsableMachine usableMachine2 in allUsablePointsWithTag)
				{
					MatrixFrame matrixFrame;
					if (this.GetSpawnFrameFromUsableMachine(usableMachine2, out matrixFrame))
					{
						spawnFrame = matrixFrame;
						usableMachine = usableMachine2;
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00017FC4 File Offset: 0x000161C4
		private bool GetSpawnDataInUsablePointsList(Dictionary<string, List<UsableMachine>> list, out MatrixFrame spawnFrame, out UsableMachine usableMachine)
		{
			spawnFrame = MatrixFrame.Identity;
			usableMachine = null;
			foreach (KeyValuePair<string, List<UsableMachine>> keyValuePair in list)
			{
				if (keyValuePair.Value.Count > 0)
				{
					foreach (UsableMachine usableMachine2 in keyValuePair.Value)
					{
						MatrixFrame matrixFrame;
						if (this.GetSpawnFrameFromUsableMachine(usableMachine2, out matrixFrame))
						{
							spawnFrame = matrixFrame;
							usableMachine = usableMachine2;
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00018084 File Offset: 0x00016284
		public Agent SpawnWanderingAgent(LocationCharacter locationCharacter)
		{
			bool flag = false;
			MatrixFrame identity = MatrixFrame.Identity;
			UsableMachine usableMachine = null;
			if (locationCharacter.SpecialTargetTag != null)
			{
				flag = this.GetSpawnDataForTag(locationCharacter.SpecialTargetTag, out identity, out usableMachine);
			}
			if (!flag)
			{
				flag = this.GetSpawnDataForTag("npc_common_limited", out identity, out usableMachine);
			}
			if (!flag)
			{
				flag = this.GetSpawnDataForTag("npc_common", out identity, out usableMachine);
			}
			if (!flag && this._usablePoints.Count > 0)
			{
				flag = this.GetSpawnDataInUsablePointsList(this._usablePoints, out identity, out usableMachine);
			}
			if (!flag && this._pairedUsablePoints.Count > 0)
			{
				flag = this.GetSpawnDataInUsablePointsList(this._pairedUsablePoints, out identity, out usableMachine);
			}
			identity.rotation.f.z = 0f;
			identity.rotation.f.Normalize();
			identity.rotation.u = Vec3.Up;
			identity.rotation.s = Vec3.CrossProduct(identity.rotation.f, identity.rotation.u);
			identity.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
			Agent agent;
			if (usableMachine != null && locationCharacter.Character != this._conversationCharacter)
			{
				agent = this.SpawnWanderingAgentWithUsableMachine(locationCharacter, usableMachine);
			}
			else
			{
				agent = this.SpawnWanderingAgentWithInitialFrame(locationCharacter, identity, true);
			}
			MissionAgentHandler.SetAgentExcludeFaceGroupIdAux(agent, MissionAgentHandler._disabledFaceId);
			return agent;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x000181C0 File Offset: 0x000163C0
		private bool GetSpawnFrameFromUsableMachine(UsableMachine usableMachine, out MatrixFrame frame)
		{
			frame = MatrixFrame.Identity;
			StandingPoint randomElementWithPredicate = usableMachine.StandingPoints.GetRandomElementWithPredicate((StandingPoint x) => !x.HasUser && !x.IsDeactivated && !x.IsDisabled);
			if (randomElementWithPredicate != null)
			{
				frame = randomElementWithPredicate.GameEntity.GetGlobalFrame();
				return true;
			}
			return false;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0001821C File Offset: 0x0001641C
		private Agent SpawnWanderingAgentWithUsableMachine(LocationCharacter locationCharacter, UsableMachine usableMachine)
		{
			MatrixFrame spawnPointFrame;
			this.GetSpawnFrameFromUsableMachine(usableMachine, out spawnPointFrame);
			Agent agent = this.SpawnWanderingAgentWithInitialFrame(locationCharacter, spawnPointFrame, true);
			agent.GetComponent<CampaignAgentComponent>().AgentNavigator.SetTarget(usableMachine, true);
			return agent;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00018250 File Offset: 0x00016450
		private Agent SpawnWanderingAgentWithInitialFrame(LocationCharacter locationCharacter, MatrixFrame spawnPointFrame, bool noHorses = true)
		{
			Team team = Team.Invalid;
			switch (locationCharacter.CharacterRelation)
			{
			case LocationCharacter.CharacterRelations.Neutral:
				team = Team.Invalid;
				break;
			case LocationCharacter.CharacterRelations.Friendly:
				team = base.Mission.PlayerAllyTeam;
				break;
			case LocationCharacter.CharacterRelations.Enemy:
				team = base.Mission.PlayerEnemyTeam;
				break;
			}
			spawnPointFrame.origin.z = base.Mission.Scene.GetGroundHeightAtPosition(spawnPointFrame.origin, BodyFlags.CommonCollisionExcludeFlags);
			ValueTuple<uint, uint> agentSettlementColors = MissionAgentHandler.GetAgentSettlementColors(locationCharacter);
			AgentBuildData agentBuildData = locationCharacter.GetAgentBuildData().Team(team).InitialPosition(spawnPointFrame.origin);
			Vec2 vec = spawnPointFrame.rotation.f.AsVec2;
			vec = vec.Normalized();
			AgentBuildData agentBuildData2 = agentBuildData.InitialDirection(vec).ClothingColor1(agentSettlementColors.Item1).ClothingColor2(agentSettlementColors.Item2).CivilianEquipment(locationCharacter.UseCivilianEquipment).NoHorses(noHorses);
			CharacterObject character = locationCharacter.Character;
			Banner banner;
			if (character == null)
			{
				banner = null;
			}
			else
			{
				Hero heroObject = character.HeroObject;
				if (heroObject == null)
				{
					banner = null;
				}
				else
				{
					Clan clan = heroObject.Clan;
					banner = ((clan != null) ? clan.Banner : null);
				}
			}
			AgentBuildData agentBuildData3 = agentBuildData2.Banner(banner);
			Agent agent = base.Mission.SpawnAgent(agentBuildData3, false);
			MissionAgentHandler.SetAgentExcludeFaceGroupIdAux(agent, MissionAgentHandler._disabledFaceId);
			AnimationSystemData animationSystemData = agentBuildData3.AgentMonster.FillAnimationSystemData(MBGlobals.GetActionSet(locationCharacter.ActionSetCode), locationCharacter.Character.GetStepSize(), false);
			agent.SetActionSet(ref animationSystemData);
			agent.GetComponent<CampaignAgentComponent>().CreateAgentNavigator(locationCharacter);
			locationCharacter.AddBehaviors(agent);
			return agent;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x000183C1 File Offset: 0x000165C1
		private static void SetAgentExcludeFaceGroupIdAux(Agent agent, int _disabledFaceId)
		{
			if (_disabledFaceId != -1)
			{
				agent.SetAgentExcludeStateForFaceGroupId(_disabledFaceId, true);
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x000183CF File Offset: 0x000165CF
		public static uint GetRandomTournamentTeamColor(int teamIndex)
		{
			return MissionAgentHandler._tournamentTeamColors[teamIndex % MissionAgentHandler._tournamentTeamColors.Length];
		}

		// Token: 0x06000380 RID: 896 RVA: 0x000183E0 File Offset: 0x000165E0
		[return: TupleElementNames(new string[]
		{
			"color1",
			"color2"
		})]
		public static ValueTuple<uint, uint> GetAgentSettlementColors(LocationCharacter locationCharacter)
		{
			CharacterObject character = locationCharacter.Character;
			if (character.IsHero)
			{
				if (character.HeroObject.Clan == CharacterObject.PlayerCharacter.HeroObject.Clan)
				{
					return new ValueTuple<uint, uint>(Clan.PlayerClan.Color, Clan.PlayerClan.Color2);
				}
				if (!character.HeroObject.IsNotable)
				{
					return new ValueTuple<uint, uint>(locationCharacter.AgentData.AgentClothingColor1, locationCharacter.AgentData.AgentClothingColor2);
				}
				return CharacterHelper.GetDeterministicColorsForCharacter(character);
			}
			else
			{
				if (character.IsSoldier)
				{
					return new ValueTuple<uint, uint>(Settlement.CurrentSettlement.MapFaction.Color, Settlement.CurrentSettlement.MapFaction.Color2);
				}
				return new ValueTuple<uint, uint>(MissionAgentHandler._villagerClothColors[MBRandom.RandomInt(MissionAgentHandler._villagerClothColors.Length)], MissionAgentHandler._villagerClothColors[MBRandom.RandomInt(MissionAgentHandler._villagerClothColors.Length)]);
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x000184B7 File Offset: 0x000166B7
		public UsableMachine FindUnusedPointWithTagForAgent(Agent agent, string tag)
		{
			return this.FindUnusedPointForAgent(agent, this._pairedUsablePoints, tag) ?? this.FindUnusedPointForAgent(agent, this._usablePoints, tag);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x000184DC File Offset: 0x000166DC
		private UsableMachine FindUnusedPointForAgent(Agent agent, Dictionary<string, List<UsableMachine>> usableMachinesList, string primaryTag)
		{
			List<UsableMachine> list;
			if (usableMachinesList.TryGetValue(primaryTag, out list) && list.Count > 0)
			{
				int num = MBRandom.RandomInt(0, list.Count);
				for (int i = 0; i < list.Count; i++)
				{
					UsableMachine usableMachine = list[(num + i) % list.Count];
					if (!usableMachine.IsDisabled && !usableMachine.IsDestroyed && usableMachine.IsStandingPointAvailableForAgent(agent))
					{
						return usableMachine;
					}
				}
			}
			return null;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00018548 File Offset: 0x00016748
		public List<UsableMachine> FindAllUnusedPoints(Agent agent, string primaryTag)
		{
			List<UsableMachine> list = new List<UsableMachine>();
			List<UsableMachine> list2 = new List<UsableMachine>();
			List<UsableMachine> list3;
			this._usablePoints.TryGetValue(primaryTag, out list3);
			List<UsableMachine> list4;
			this._pairedUsablePoints.TryGetValue(primaryTag, out list4);
			list4 = ((list4 != null) ? list4.Distinct<UsableMachine>().ToList<UsableMachine>() : null);
			if (list3 != null && list3.Count > 0)
			{
				list.AddRange(list3);
			}
			if (list4 != null && list4.Count > 0)
			{
				list.AddRange(list4);
			}
			if (list.Count > 0)
			{
				Predicate<StandingPoint> <>9__0;
				foreach (UsableMachine usableMachine in list)
				{
					List<StandingPoint> standingPoints = usableMachine.StandingPoints;
					Predicate<StandingPoint> match;
					if ((match = <>9__0) == null)
					{
						match = (<>9__0 = ((StandingPoint sp) => (sp.IsInstantUse || (!sp.HasUser && !sp.HasAIMovingTo)) && !sp.IsDisabledForAgent(agent)));
					}
					if (standingPoints.Exists(match))
					{
						list2.Add(usableMachine);
					}
				}
			}
			return list2;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00018648 File Offset: 0x00016848
		public void RemovePropReference(List<GameEntity> props)
		{
			foreach (KeyValuePair<string, List<UsableMachine>> keyValuePair in this._usablePoints)
			{
				foreach (GameEntity gameEntity in props)
				{
					if (gameEntity.HasTag(keyValuePair.Key))
					{
						UsableMachine firstScriptOfType = gameEntity.GetFirstScriptOfType<UsableMachine>();
						keyValuePair.Value.Remove(firstScriptOfType);
					}
				}
			}
			foreach (KeyValuePair<string, List<UsableMachine>> keyValuePair2 in this._pairedUsablePoints)
			{
				foreach (GameEntity gameEntity2 in props)
				{
					if (gameEntity2.HasTag(keyValuePair2.Key))
					{
						UsableMachine firstScriptOfType2 = gameEntity2.GetFirstScriptOfType<UsableMachine>();
						keyValuePair2.Value.Remove(firstScriptOfType2);
					}
				}
			}
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0001878C File Offset: 0x0001698C
		public void AddPropReference(List<GameEntity> props)
		{
			foreach (KeyValuePair<string, List<UsableMachine>> keyValuePair in this._usablePoints)
			{
				foreach (GameEntity gameEntity in props)
				{
					UsableMachine firstScriptOfType = gameEntity.GetFirstScriptOfType<UsableMachine>();
					if (firstScriptOfType != null && gameEntity.HasTag(keyValuePair.Key))
					{
						keyValuePair.Value.Add(firstScriptOfType);
					}
				}
			}
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00018838 File Offset: 0x00016A38
		public void TeleportTargetAgentNearReferenceAgent(Agent referenceAgent, Agent teleportAgent, bool teleportFollowers, bool teleportOpposite)
		{
			Vec3 vec = referenceAgent.Position + referenceAgent.LookDirection.NormalizedCopy() * 4f;
			Vec3 position;
			if (teleportOpposite)
			{
				position = vec;
				position.z = base.Mission.Scene.GetGroundHeightAtPosition(position, BodyFlags.CommonCollisionExcludeFlags);
			}
			else
			{
				position = Mission.Current.GetRandomPositionAroundPoint(referenceAgent.Position, 2f, 4f, true);
				position.z = base.Mission.Scene.GetGroundHeightAtPosition(position, BodyFlags.CommonCollisionExcludeFlags);
			}
			WorldFrame worldFrame = new WorldFrame(referenceAgent.Frame.rotation, new WorldPosition(base.Mission.Scene, referenceAgent.Frame.origin));
			Vec3 vec2 = new Vec3(worldFrame.Origin.AsVec2 - position.AsVec2, 0f, -1f);
			teleportAgent.LookDirection = vec2.NormalizedCopy();
			teleportAgent.TeleportToPosition(position);
			if (teleportFollowers && teleportAgent.Controller == Agent.ControllerType.Player)
			{
				foreach (Agent agent in base.Mission.Agents)
				{
					LocationCharacter locationCharacter = CampaignMission.Current.Location.GetLocationCharacter(agent.Origin);
					AccompanyingCharacter accompanyingCharacter = PlayerEncounter.LocationEncounter.GetAccompanyingCharacter(locationCharacter);
					if (agent.GetComponent<CampaignAgentComponent>().AgentNavigator != null && accompanyingCharacter != null && accompanyingCharacter.IsFollowingPlayerAtMissionStart)
					{
						MatrixFrame matrixFrame;
						this.GetFrameForFollowingAgent(teleportAgent, out matrixFrame);
						agent.TeleportToPosition(matrixFrame.origin);
					}
				}
			}
		}

		// Token: 0x06000387 RID: 903 RVA: 0x000189E4 File Offset: 0x00016BE4
		public static int GetPointCountOfUsableMachine(UsableMachine usableMachine, bool checkForUnusedOnes)
		{
			int num = 0;
			List<AnimationPoint> list = new List<AnimationPoint>();
			foreach (StandingPoint standingPoint in usableMachine.StandingPoints)
			{
				AnimationPoint animationPoint = standingPoint as AnimationPoint;
				if (animationPoint != null && animationPoint.IsActive && !standingPoint.IsDeactivated && !standingPoint.IsDisabled && !standingPoint.IsInstantUse && (!checkForUnusedOnes || (!standingPoint.HasUser && !standingPoint.HasAIMovingTo)))
				{
					List<AnimationPoint> alternatives = animationPoint.GetAlternatives();
					if (alternatives.Count == 0)
					{
						num++;
					}
					else if (!list.Contains(animationPoint))
					{
						if (checkForUnusedOnes)
						{
							if (alternatives.Any((AnimationPoint x) => x.HasUser && x.HasAIMovingTo))
							{
								continue;
							}
						}
						list.AddRange(alternatives);
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x0400019B RID: 411
		private const float PassageUsageDeltaTime = 30f;

		// Token: 0x0400019C RID: 412
		private static readonly uint[] _tournamentTeamColors = new uint[]
		{
			4294110933U,
			4290269521U,
			4291535494U,
			4286151096U,
			4290286497U,
			4291600739U,
			4291868275U,
			4287285710U,
			4283204487U,
			4287282028U,
			4290300789U
		};

		// Token: 0x0400019D RID: 413
		private static readonly uint[] _villagerClothColors = new uint[]
		{
			4292860590U,
			4291351206U,
			4289117081U,
			4288460959U,
			4287541416U,
			4288922566U,
			4292654718U,
			4289243320U,
			4290286483U,
			4290288531U,
			4290156159U,
			4291136871U,
			4289233774U,
			4291205980U,
			4291735684U,
			4292722283U,
			4293119406U,
			4293911751U,
			4294110933U,
			4291535494U,
			4289955192U,
			4289631650U,
			4292133587U,
			4288785593U,
			4286288275U,
			4286222496U,
			4287601851U,
			4286622134U,
			4285898909U,
			4285638289U,
			4289830302U,
			4287593853U,
			4289957781U,
			4287071646U,
			4284445583U
		};

		// Token: 0x0400019E RID: 414
		private static int _disabledFaceId = -1;

		// Token: 0x0400019F RID: 415
		private static int _disabledFaceIdForAnimals = 1;

		// Token: 0x040001A0 RID: 416
		private readonly Dictionary<string, List<UsableMachine>> _usablePoints;

		// Token: 0x040001A1 RID: 417
		private readonly Dictionary<string, List<UsableMachine>> _pairedUsablePoints;

		// Token: 0x040001A2 RID: 418
		private List<UsableMachine> _disabledPassages;

		// Token: 0x040001A3 RID: 419
		private readonly Location _previousLocation;

		// Token: 0x040001A4 RID: 420
		private readonly Location _currentLocation;

		// Token: 0x040001A5 RID: 421
		private readonly string _playerSpecialSpawnTag;

		// Token: 0x040001A6 RID: 422
		private BasicMissionTimer _checkPossibleQuestTimer;

		// Token: 0x040001A7 RID: 423
		private float _passageUsageTime;

		// Token: 0x040001A8 RID: 424
		private CharacterObject _conversationCharacter;
	}
}
