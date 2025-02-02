using System;
using System.Collections.Generic;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002C3 RID: 707
	public abstract class SpawningBehaviorBase
	{
		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x060026CD RID: 9933 RVA: 0x0009374E File Offset: 0x0009194E
		// (set) Token: 0x060026CE RID: 9934 RVA: 0x00093756 File Offset: 0x00091956
		private protected MultiplayerMissionAgentVisualSpawnComponent AgentVisualSpawnComponent { protected get; private set; }

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x060026CF RID: 9935 RVA: 0x0009375F File Offset: 0x0009195F
		protected Mission Mission
		{
			get
			{
				return this.SpawnComponent.Mission;
			}
		}

		// Token: 0x14000074 RID: 116
		// (add) Token: 0x060026D0 RID: 9936 RVA: 0x0009376C File Offset: 0x0009196C
		// (remove) Token: 0x060026D1 RID: 9937 RVA: 0x000937A4 File Offset: 0x000919A4
		protected event Action<MissionPeer> OnAllAgentsFromPeerSpawnedFromVisuals;

		// Token: 0x14000075 RID: 117
		// (add) Token: 0x060026D2 RID: 9938 RVA: 0x000937DC File Offset: 0x000919DC
		// (remove) Token: 0x060026D3 RID: 9939 RVA: 0x00093814 File Offset: 0x00091A14
		protected event Action<MissionPeer> OnPeerSpawnedFromVisuals;

		// Token: 0x14000076 RID: 118
		// (add) Token: 0x060026D4 RID: 9940 RVA: 0x0009384C File Offset: 0x00091A4C
		// (remove) Token: 0x060026D5 RID: 9941 RVA: 0x00093884 File Offset: 0x00091A84
		public event SpawningBehaviorBase.OnSpawningEndedEventDelegate OnSpawningEnded;

		// Token: 0x060026D6 RID: 9942 RVA: 0x000938BC File Offset: 0x00091ABC
		public virtual void Initialize(SpawnComponent spawnComponent)
		{
			this.SpawnComponent = spawnComponent;
			this.AgentVisualSpawnComponent = this.Mission.GetMissionBehavior<MultiplayerMissionAgentVisualSpawnComponent>();
			this.GameMode = this.Mission.GetMissionBehavior<MissionMultiplayerGameModeBase>();
			this.MissionLobbyComponent = this.Mission.GetMissionBehavior<MissionLobbyComponent>();
			this.MissionLobbyEquipmentNetworkComponent = this.Mission.GetMissionBehavior<MissionLobbyEquipmentNetworkComponent>();
			this.MissionLobbyEquipmentNetworkComponent.OnEquipmentRefreshed += this.OnPeerEquipmentUpdated;
			this._spawnCheckTimer = new Timer(Mission.Current.CurrentTime, 0.2f, true);
			this._agentsToBeSpawnedCache = new List<AgentBuildData>();
			this._nextTimeToCleanUpMounts = MissionTime.Now;
			this._botsCountForSides = new int[2];
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x00093968 File Offset: 0x00091B68
		public virtual void Clear()
		{
			this.MissionLobbyEquipmentNetworkComponent.OnEquipmentRefreshed -= this.OnPeerEquipmentUpdated;
			this._agentsToBeSpawnedCache = null;
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x00093988 File Offset: 0x00091B88
		public virtual void OnTick(float dt)
		{
			int count = Mission.Current.AllAgents.Count;
			int num = 0;
			this._agentsToBeSpawnedCache.Clear();
			foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
			{
				if (networkCommunicator.IsSynchronized)
				{
					MissionPeer component = networkCommunicator.GetComponent<MissionPeer>();
					if (component != null && component.ControlledAgent == null && component.HasSpawnedAgentVisuals && !this.CanUpdateSpawnEquipment(component))
					{
						MultiplayerClassDivisions.MPHeroClass mpheroClassForPeer = MultiplayerClassDivisions.GetMPHeroClassForPeer(component, false);
						MPPerkObject.MPOnSpawnPerkHandler onSpawnPerkHandler = MPPerkObject.GetOnSpawnPerkHandler(component);
						GameNetwork.BeginBroadcastModuleEvent();
						GameNetwork.WriteMessage(new SyncPerksForCurrentlySelectedTroop(networkCommunicator, component.Perks[component.SelectedTroopIndex]));
						GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.ExcludeOtherTeamPlayers, networkCommunicator);
						int num2 = 0;
						bool flag = false;
						int intValue = MultiplayerOptions.OptionType.NumberOfBotsPerFormation.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
						if (intValue > 0 && (this.GameMode.WarmupComponent == null || !this.GameMode.WarmupComponent.IsInWarmup))
						{
							num2 = MPPerkObject.GetTroopCount(mpheroClassForPeer, intValue, onSpawnPerkHandler);
							using (List<MPPerkObject>.Enumerator enumerator2 = component.SelectedPerks.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									if (enumerator2.Current.HasBannerBearer)
									{
										flag = true;
										break;
									}
								}
							}
						}
						if (num2 > 0)
						{
							num2 = (int)((float)num2 * this.GameMode.GetTroopNumberMultiplierForMissingPlayer(component));
						}
						num2 += (flag ? 2 : 1);
						IEnumerable<ValueTuple<EquipmentIndex, EquipmentElement>> enumerable = (onSpawnPerkHandler != null) ? onSpawnPerkHandler.GetAlternativeEquipments(false) : null;
						int i = 0;
						while (i < num2)
						{
							bool flag2 = i == 0;
							BasicCharacterObject basicCharacterObject = flag2 ? mpheroClassForPeer.HeroCharacter : ((flag && i == 1) ? mpheroClassForPeer.BannerBearerCharacter : mpheroClassForPeer.TroopCharacter);
							uint color = (!this.GameMode.IsGameModeUsingOpposingTeams || component.Team == this.Mission.AttackerTeam) ? component.Culture.Color : component.Culture.ClothAlternativeColor;
							uint color2 = (!this.GameMode.IsGameModeUsingOpposingTeams || component.Team == this.Mission.AttackerTeam) ? component.Culture.Color2 : component.Culture.ClothAlternativeColor2;
							uint color3 = (!this.GameMode.IsGameModeUsingOpposingTeams || component.Team == this.Mission.AttackerTeam) ? component.Culture.BackgroundColor1 : component.Culture.BackgroundColor2;
							uint color4 = (!this.GameMode.IsGameModeUsingOpposingTeams || component.Team == this.Mission.AttackerTeam) ? component.Culture.ForegroundColor1 : component.Culture.ForegroundColor2;
							Banner banner = new Banner(component.Peer.BannerCode, color3, color4);
							AgentBuildData agentBuildData = new AgentBuildData(basicCharacterObject).VisualsIndex(i).Team(component.Team).TroopOrigin(new BasicBattleAgentOrigin(basicCharacterObject)).Formation(component.ControlledFormation).IsFemale(flag2 ? component.Peer.IsFemale : basicCharacterObject.IsFemale).ClothingColor1(color).ClothingColor2(color2).Banner(banner);
							if (flag2)
							{
								agentBuildData.MissionPeer(component);
							}
							else
							{
								agentBuildData.OwningMissionPeer(component);
							}
							Equipment equipment = flag2 ? basicCharacterObject.Equipment.Clone(false) : Equipment.GetRandomEquipmentElements(basicCharacterObject, false, false, MBRandom.RandomInt());
							IEnumerable<ValueTuple<EquipmentIndex, EquipmentElement>> enumerable2 = flag2 ? ((onSpawnPerkHandler != null) ? onSpawnPerkHandler.GetAlternativeEquipments(true) : null) : enumerable;
							if (enumerable2 != null)
							{
								foreach (ValueTuple<EquipmentIndex, EquipmentElement> valueTuple in enumerable2)
								{
									equipment[valueTuple.Item1] = valueTuple.Item2;
								}
							}
							agentBuildData.Equipment(equipment);
							if (flag2)
							{
								this.GameMode.AddCosmeticItemsToEquipment(equipment, this.GameMode.GetUsedCosmeticsFromPeer(component, basicCharacterObject));
							}
							if (flag2)
							{
								agentBuildData.BodyProperties(this.GetBodyProperties(component, component.Culture));
								agentBuildData.Age((int)agentBuildData.AgentBodyProperties.Age);
							}
							else
							{
								agentBuildData.EquipmentSeed(this.MissionLobbyComponent.GetRandomFaceSeedForCharacter(basicCharacterObject, agentBuildData.AgentVisualsIndex));
								agentBuildData.BodyProperties(BodyProperties.GetRandomBodyProperties(agentBuildData.AgentRace, agentBuildData.AgentIsFemale, basicCharacterObject.GetBodyPropertiesMin(false), basicCharacterObject.GetBodyPropertiesMax(), (int)agentBuildData.AgentOverridenSpawnEquipment.HairCoverType, agentBuildData.AgentEquipmentSeed, basicCharacterObject.HairTags, basicCharacterObject.BeardTags, basicCharacterObject.TattooTags));
							}
							if (component.ControlledFormation != null && component.ControlledFormation.Banner == null)
							{
								component.ControlledFormation.Banner = banner;
							}
							MatrixFrame spawnFrame = this.SpawnComponent.GetSpawnFrame(component.Team, equipment[EquipmentIndex.ArmorItemEndSlot].Item != null, component.SpawnCountThisRound == 0);
							if (spawnFrame.IsIdentity)
							{
								goto IL_587;
							}
							Vec2 v;
							if (!(spawnFrame.origin != agentBuildData.AgentInitialPosition))
							{
								v = spawnFrame.rotation.f.AsVec2.Normalized();
								Vec2? agentInitialDirection = agentBuildData.AgentInitialDirection;
								if (!(v != agentInitialDirection))
								{
									goto IL_587;
								}
							}
							agentBuildData.InitialPosition(spawnFrame.origin);
							AgentBuildData agentBuildData2 = agentBuildData;
							v = spawnFrame.rotation.f.AsVec2;
							v = v.Normalized();
							agentBuildData2.InitialDirection(v);
							IL_5A0:
							if (component.ControlledAgent != null && !flag2)
							{
								MatrixFrame frame = component.ControlledAgent.Frame;
								frame.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
								MatrixFrame matrixFrame = frame;
								matrixFrame.origin -= matrixFrame.rotation.f.NormalizedCopy() * 3.5f;
								Mat3 rotation = matrixFrame.rotation;
								rotation.MakeUnit();
								bool flag3 = !basicCharacterObject.Equipment[EquipmentIndex.ArmorItemEndSlot].IsEmpty;
								int num3 = MathF.Min(num2, 10);
								MatrixFrame matrixFrame2 = Formation.GetFormationFramesForBeforeFormationCreation((float)num3 * Formation.GetDefaultUnitDiameter(flag3) + (float)(num3 - 1) * Formation.GetDefaultMinimumInterval(flag3), num2, flag3, new WorldPosition(Mission.Current.Scene, matrixFrame.origin), rotation)[i - 1].ToGroundMatrixFrame();
								agentBuildData.InitialPosition(matrixFrame2.origin);
								AgentBuildData agentBuildData3 = agentBuildData;
								v = matrixFrame2.rotation.f.AsVec2;
								v = v.Normalized();
								agentBuildData3.InitialDirection(v);
							}
							this._agentsToBeSpawnedCache.Add(agentBuildData);
							num++;
							if (!agentBuildData.AgentOverridenSpawnEquipment[EquipmentIndex.ArmorItemEndSlot].IsEmpty)
							{
								num++;
							}
							i++;
							continue;
							IL_587:
							Debug.FailedAssert("Spawn frame could not be found.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Multiplayer\\SpawnBehaviors\\SpawningBehaviors\\SpawningBehaviorBase.cs", "OnTick", 216);
							goto IL_5A0;
						}
					}
				}
			}
			int num4 = num + count;
			if (num4 > SpawningBehaviorBase.AgentCountThreshold && this._nextTimeToCleanUpMounts.IsPast)
			{
				this._nextTimeToCleanUpMounts = MissionTime.SecondsFromNow(5f);
				for (int j = Mission.Current.MountsWithoutRiders.Count - 1; j >= 0; j--)
				{
					KeyValuePair<Agent, MissionTime> keyValuePair = Mission.Current.MountsWithoutRiders[j];
					Agent key = keyValuePair.Key;
					if (keyValuePair.Value.ElapsedSeconds > 30f)
					{
						key.FadeOut(false, false);
					}
				}
			}
			int num5 = SpawningBehaviorBase.MaxAgentCount - num4;
			if (num5 >= 0)
			{
				for (int k = this._agentsToBeSpawnedCache.Count - 1; k >= 0; k--)
				{
					AgentBuildData agentBuildData4 = this._agentsToBeSpawnedCache[k];
					bool flag4 = agentBuildData4.AgentMissionPeer != null;
					MissionPeer missionPeer = flag4 ? agentBuildData4.AgentMissionPeer : agentBuildData4.OwningAgentMissionPeer;
					MPPerkObject.MPOnSpawnPerkHandler onSpawnPerkHandler2 = MPPerkObject.GetOnSpawnPerkHandler(missionPeer);
					Agent agent = this.Mission.SpawnAgent(agentBuildData4, true);
					agent.AddComponent(new MPPerksAgentComponent(agent));
					Agent mountAgent = agent.MountAgent;
					if (mountAgent != null)
					{
						mountAgent.UpdateAgentProperties();
					}
					agent.HealthLimit += ((onSpawnPerkHandler2 != null) ? onSpawnPerkHandler2.GetHitpoints(flag4) : 0f);
					agent.Health = agent.HealthLimit;
					if (!flag4)
					{
						agent.SetWatchState(Agent.WatchState.Alarmed);
					}
					agent.WieldInitialWeapons(Agent.WeaponWieldActionType.InstantAfterPickUp, Equipment.InitialWeaponEquipPreference.Any);
					if (flag4)
					{
						MissionPeer missionPeer2 = missionPeer;
						int spawnCountThisRound = missionPeer2.SpawnCountThisRound;
						missionPeer2.SpawnCountThisRound = spawnCountThisRound + 1;
						Action<MissionPeer> onPeerSpawnedFromVisuals = this.OnPeerSpawnedFromVisuals;
						if (onPeerSpawnedFromVisuals != null)
						{
							onPeerSpawnedFromVisuals(missionPeer);
						}
						Action<MissionPeer> onAllAgentsFromPeerSpawnedFromVisuals = this.OnAllAgentsFromPeerSpawnedFromVisuals;
						if (onAllAgentsFromPeerSpawnedFromVisuals != null)
						{
							onAllAgentsFromPeerSpawnedFromVisuals(missionPeer);
						}
						this.AgentVisualSpawnComponent.RemoveAgentVisuals(missionPeer, true);
						if (GameNetwork.IsServerOrRecorder)
						{
							GameNetwork.BeginBroadcastModuleEvent();
							GameNetwork.WriteMessage(new RemoveAgentVisualsForPeer(missionPeer.GetNetworkPeer()));
							GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
						}
						missionPeer.HasSpawnedAgentVisuals = false;
						MPPerkObject.MPPerkHandler perkHandler = MPPerkObject.GetPerkHandler(missionPeer);
						if (perkHandler != null)
						{
							perkHandler.OnEvent(MPPerkCondition.PerkEventFlags.SpawnEnd);
						}
					}
				}
				int intValue2 = MultiplayerOptions.OptionType.NumberOfBotsTeam1.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
				int intValue3 = MultiplayerOptions.OptionType.NumberOfBotsTeam2.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
				if (this.GameMode.IsGameModeUsingOpposingTeams && (intValue2 > 0 || intValue3 > 0))
				{
					ValueTuple<Team, BasicCultureObject, int>[] array = new ValueTuple<Team, BasicCultureObject, int>[]
					{
						new ValueTuple<Team, BasicCultureObject, int>(this.Mission.DefenderTeam, MBObjectManager.Instance.GetObject<BasicCultureObject>(MultiplayerOptions.OptionType.CultureTeam2.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions)), intValue3 - this._botsCountForSides[0]),
						new ValueTuple<Team, BasicCultureObject, int>(this.Mission.AttackerTeam, MBObjectManager.Instance.GetObject<BasicCultureObject>(MultiplayerOptions.OptionType.CultureTeam1.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions)), intValue2 - this._botsCountForSides[1])
					};
					if (num5 >= 4)
					{
						for (int l = 0; l < Math.Min(num5 / 2, array[0].Item3 + array[1].Item3); l++)
						{
							this.SpawnBot(array[l % 2].Item1, array[l % 2].Item2);
						}
					}
				}
			}
			if (!this.IsSpawningEnabled && this.IsRoundInProgress())
			{
				if (this.SpawningDelayTimer >= this.SpawningEndDelay && !this._hasCalledSpawningEnded)
				{
					Mission.Current.AllowAiTicking = true;
					if (this.OnSpawningEnded != null)
					{
						this.OnSpawningEnded();
					}
					this._hasCalledSpawningEnded = true;
				}
				this.SpawningDelayTimer += dt;
			}
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x00094444 File Offset: 0x00092644
		public bool AreAgentsSpawning()
		{
			return this.IsSpawningEnabled;
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x0009444C File Offset: 0x0009264C
		protected void ResetSpawnCounts()
		{
			foreach (NetworkCommunicator networkPeer in GameNetwork.NetworkPeers)
			{
				MissionPeer component = networkPeer.GetComponent<MissionPeer>();
				if (component != null)
				{
					component.SpawnCountThisRound = 0;
				}
			}
		}

		// Token: 0x060026DB RID: 9947 RVA: 0x000944A8 File Offset: 0x000926A8
		protected void ResetSpawnTimers()
		{
			foreach (NetworkCommunicator networkPeer in GameNetwork.NetworkPeers)
			{
				MissionPeer component = networkPeer.GetComponent<MissionPeer>();
				if (component != null)
				{
					component.SpawnTimer.Reset(Mission.Current.CurrentTime, 0f);
				}
			}
		}

		// Token: 0x060026DC RID: 9948 RVA: 0x00094518 File Offset: 0x00092718
		public virtual void RequestStartSpawnSession()
		{
			this.IsSpawningEnabled = true;
			this.SpawningDelayTimer = 0f;
			this._hasCalledSpawningEnded = false;
			this.ResetSpawnCounts();
		}

		// Token: 0x060026DD RID: 9949 RVA: 0x0009453C File Offset: 0x0009273C
		public void RequestStopSpawnSession()
		{
			this.IsSpawningEnabled = false;
			this.SpawningDelayTimer = 0f;
			this._hasCalledSpawningEnded = false;
			foreach (NetworkCommunicator networkPeer in GameNetwork.NetworkPeers)
			{
				MissionPeer component = networkPeer.GetComponent<MissionPeer>();
				if (component != null)
				{
					this.AgentVisualSpawnComponent.RemoveAgentVisuals(component, true);
					if (GameNetwork.IsServerOrRecorder)
					{
						GameNetwork.BeginBroadcastModuleEvent();
						GameNetwork.WriteMessage(new RemoveAgentVisualsForPeer(component.GetNetworkPeer()));
						GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
					}
					component.HasSpawnedAgentVisuals = false;
				}
			}
			foreach (NetworkCommunicator networkCommunicator in GameNetwork.DisconnectedNetworkPeers)
			{
				MissionPeer missionPeer = (networkCommunicator != null) ? networkCommunicator.GetComponent<MissionPeer>() : null;
				if (missionPeer != null)
				{
					this.AgentVisualSpawnComponent.RemoveAgentVisuals(missionPeer, false);
					if (GameNetwork.IsServerOrRecorder)
					{
						GameNetwork.BeginBroadcastModuleEvent();
						GameNetwork.WriteMessage(new RemoveAgentVisualsForPeer(missionPeer.GetNetworkPeer()));
						GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
					}
					missionPeer.HasSpawnedAgentVisuals = false;
				}
			}
		}

		// Token: 0x060026DE RID: 9950 RVA: 0x00094660 File Offset: 0x00092860
		public void SetRemainingAgentsInvulnerable()
		{
			foreach (Agent agent in this.Mission.Agents)
			{
				agent.SetMortalityState(Agent.MortalityState.Invulnerable);
			}
		}

		// Token: 0x060026DF RID: 9951
		protected abstract void SpawnAgents();

		// Token: 0x060026E0 RID: 9952 RVA: 0x000946B8 File Offset: 0x000928B8
		protected BodyProperties GetBodyProperties(MissionPeer missionPeer, BasicCultureObject cultureLimit)
		{
			NetworkCommunicator networkPeer = missionPeer.GetNetworkPeer();
			if (networkPeer != null)
			{
				return networkPeer.PlayerConnectionInfo.GetParameter<PlayerData>("PlayerData").BodyProperties;
			}
			Debug.FailedAssert("networkCommunicator != null", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Multiplayer\\SpawnBehaviors\\SpawningBehaviors\\SpawningBehaviorBase.cs", "GetBodyProperties", 510);
			Team team = missionPeer.Team;
			BasicCharacterObject troopCharacter = MultiplayerClassDivisions.GetMPHeroClasses(cultureLimit).ToMBList<MultiplayerClassDivisions.MPHeroClass>().GetRandomElement<MultiplayerClassDivisions.MPHeroClass>().TroopCharacter;
			MatrixFrame spawnFrame = this.SpawnComponent.GetSpawnFrame(team, troopCharacter.HasMount(), true);
			AgentBuildData agentBuildData = new AgentBuildData(troopCharacter).Team(team).InitialPosition(spawnFrame.origin);
			Vec2 vec = spawnFrame.rotation.f.AsVec2;
			vec = vec.Normalized();
			AgentBuildData agentBuildData2 = agentBuildData.InitialDirection(vec).TroopOrigin(new BasicBattleAgentOrigin(troopCharacter)).EquipmentSeed(this.MissionLobbyComponent.GetRandomFaceSeedForCharacter(troopCharacter, 0)).ClothingColor1((team.Side == BattleSideEnum.Attacker) ? cultureLimit.Color : cultureLimit.ClothAlternativeColor).ClothingColor2((team.Side == BattleSideEnum.Attacker) ? cultureLimit.Color2 : cultureLimit.ClothAlternativeColor2).IsFemale(troopCharacter.IsFemale);
			agentBuildData2.Equipment(Equipment.GetRandomEquipmentElements(troopCharacter, !GameNetwork.IsMultiplayer, false, agentBuildData2.AgentEquipmentSeed));
			agentBuildData2.BodyProperties(BodyProperties.GetRandomBodyProperties(agentBuildData2.AgentRace, agentBuildData2.AgentIsFemale, troopCharacter.GetBodyPropertiesMin(false), troopCharacter.GetBodyPropertiesMax(), (int)agentBuildData2.AgentOverridenSpawnEquipment.HairCoverType, agentBuildData2.AgentEquipmentSeed, troopCharacter.HairTags, troopCharacter.BeardTags, troopCharacter.TattooTags));
			return agentBuildData2.AgentBodyProperties;
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x00094840 File Offset: 0x00092A40
		protected void SpawnBot(Team agentTeam, BasicCultureObject cultureLimit)
		{
			BasicCharacterObject troopCharacter = MultiplayerClassDivisions.GetMPHeroClasses(cultureLimit).ToMBList<MultiplayerClassDivisions.MPHeroClass>().GetRandomElement<MultiplayerClassDivisions.MPHeroClass>().TroopCharacter;
			MatrixFrame spawnFrame = this.SpawnComponent.GetSpawnFrame(agentTeam, troopCharacter.HasMount(), true);
			AgentBuildData agentBuildData = new AgentBuildData(troopCharacter).Team(agentTeam).InitialPosition(spawnFrame.origin);
			Vec2 vec = spawnFrame.rotation.f.AsVec2;
			vec = vec.Normalized();
			AgentBuildData agentBuildData2 = agentBuildData.InitialDirection(vec).TroopOrigin(new BasicBattleAgentOrigin(troopCharacter)).EquipmentSeed(this.MissionLobbyComponent.GetRandomFaceSeedForCharacter(troopCharacter, 0)).ClothingColor1((agentTeam.Side == BattleSideEnum.Attacker) ? cultureLimit.Color : cultureLimit.ClothAlternativeColor).ClothingColor2((agentTeam.Side == BattleSideEnum.Attacker) ? cultureLimit.Color2 : cultureLimit.ClothAlternativeColor2).IsFemale(troopCharacter.IsFemale);
			agentBuildData2.Equipment(Equipment.GetRandomEquipmentElements(troopCharacter, !GameNetwork.IsMultiplayer, false, agentBuildData2.AgentEquipmentSeed));
			agentBuildData2.BodyProperties(BodyProperties.GetRandomBodyProperties(agentBuildData2.AgentRace, agentBuildData2.AgentIsFemale, troopCharacter.GetBodyPropertiesMin(false), troopCharacter.GetBodyPropertiesMax(), (int)agentBuildData2.AgentOverridenSpawnEquipment.HairCoverType, agentBuildData2.AgentEquipmentSeed, troopCharacter.HairTags, troopCharacter.BeardTags, troopCharacter.TattooTags));
			Agent agent = this.Mission.SpawnAgent(agentBuildData2, false);
			agent.AIStateFlags |= Agent.AIStateFlag.Alarmed;
			this._botsCountForSides[(int)agent.Team.Side]++;
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x000949B0 File Offset: 0x00092BB0
		private void OnPeerEquipmentUpdated(MissionPeer peer)
		{
			if (this.IsSpawningEnabled && this.CanUpdateSpawnEquipment(peer))
			{
				peer.HasSpawnedAgentVisuals = false;
				Debug.Print("HasSpawnedAgentVisuals = false for peer: " + peer.Name + " because he just updated his equipment", 0, Debug.DebugColor.White, 17592186044416UL);
				if (peer.ControlledFormation != null)
				{
					peer.ControlledFormation.HasBeenPositioned = false;
					peer.ControlledFormation.SetSpawnIndex(0);
				}
			}
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x00094A1B File Offset: 0x00092C1B
		public virtual bool CanUpdateSpawnEquipment(MissionPeer missionPeer)
		{
			return !missionPeer.EquipmentUpdatingExpired && !this._equipmentUpdatingExpired;
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x00094A30 File Offset: 0x00092C30
		public void ToggleUpdatingSpawnEquipment(bool canUpdate)
		{
			this._equipmentUpdatingExpired = !canUpdate;
		}

		// Token: 0x060026E5 RID: 9957
		public abstract bool AllowEarlyAgentVisualsDespawning(MissionPeer missionPeer);

		// Token: 0x060026E6 RID: 9958 RVA: 0x00094A3C File Offset: 0x00092C3C
		public virtual int GetMaximumReSpawnPeriodForPeer(MissionPeer peer)
		{
			return 3;
		}

		// Token: 0x060026E7 RID: 9959
		protected abstract bool IsRoundInProgress();

		// Token: 0x060026E8 RID: 9960 RVA: 0x00094A40 File Offset: 0x00092C40
		public virtual void OnClearScene()
		{
			for (int i = 0; i < this._botsCountForSides.Length; i++)
			{
				this._botsCountForSides[i] = 0;
			}
		}

		// Token: 0x060026E9 RID: 9961 RVA: 0x00094A69 File Offset: 0x00092C69
		public void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow blow)
		{
			if (affectedAgent.IsHuman && affectedAgent.MissionPeer == null && affectedAgent.OwningAgentMissionPeer == null)
			{
				this._botsCountForSides[(int)affectedAgent.Team.Side]--;
			}
		}

		// Token: 0x04000E6D RID: 3693
		private static int MaxAgentCount = MBAPI.IMBAgent.GetMaximumNumberOfAgents();

		// Token: 0x04000E6E RID: 3694
		private static int AgentCountThreshold = (int)((float)SpawningBehaviorBase.MaxAgentCount * 0.9f);

		// Token: 0x04000E6F RID: 3695
		private const float SecondsToWaitForEachMountBeforeSelectingToFadeOut = 30f;

		// Token: 0x04000E70 RID: 3696
		private const float SecondsToWaitBeforeNextMountCleanup = 5f;

		// Token: 0x04000E72 RID: 3698
		protected MissionMultiplayerGameModeBase GameMode;

		// Token: 0x04000E73 RID: 3699
		protected SpawnComponent SpawnComponent;

		// Token: 0x04000E74 RID: 3700
		private bool _equipmentUpdatingExpired;

		// Token: 0x04000E75 RID: 3701
		protected bool IsSpawningEnabled;

		// Token: 0x04000E76 RID: 3702
		protected Timer _spawnCheckTimer;

		// Token: 0x04000E77 RID: 3703
		protected float SpawningEndDelay = 1f;

		// Token: 0x04000E78 RID: 3704
		protected float SpawningDelayTimer;

		// Token: 0x04000E79 RID: 3705
		private bool _hasCalledSpawningEnded;

		// Token: 0x04000E7A RID: 3706
		protected MissionLobbyComponent MissionLobbyComponent;

		// Token: 0x04000E7B RID: 3707
		protected MissionLobbyEquipmentNetworkComponent MissionLobbyEquipmentNetworkComponent;

		// Token: 0x04000E7C RID: 3708
		public static readonly ActionIndexCache PoseActionInfantry = ActionIndexCache.Create("act_walk_idle_unarmed");

		// Token: 0x04000E7D RID: 3709
		public static readonly ActionIndexCache PoseActionCavalry = ActionIndexCache.Create("act_horse_stand_1");

		// Token: 0x04000E80 RID: 3712
		private List<AgentBuildData> _agentsToBeSpawnedCache;

		// Token: 0x04000E81 RID: 3713
		private MissionTime _nextTimeToCleanUpMounts;

		// Token: 0x04000E83 RID: 3715
		private int[] _botsCountForSides;

		// Token: 0x02000586 RID: 1414
		// (Invoke) Token: 0x06003A20 RID: 14880
		public delegate void OnSpawningEndedEventDelegate();
	}
}
