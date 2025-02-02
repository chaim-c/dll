using System;
using System.Collections.Generic;
using System.Linq;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002C1 RID: 705
	public class FlagDominationSpawningBehavior : SpawningBehaviorBase
	{
		// Token: 0x060026B3 RID: 9907 RVA: 0x000923DA File Offset: 0x000905DA
		public FlagDominationSpawningBehavior()
		{
			this._enforcedSpawnTimers = new List<KeyValuePair<MissionPeer, Timer>>();
		}

		// Token: 0x060026B4 RID: 9908 RVA: 0x000923F0 File Offset: 0x000905F0
		public override void Initialize(SpawnComponent spawnComponent)
		{
			base.Initialize(spawnComponent);
			this._flagDominationMissionController = base.Mission.GetMissionBehavior<MissionMultiplayerFlagDomination>();
			this._roundController = base.Mission.GetMissionBehavior<MultiplayerRoundController>();
			this._roundController.OnRoundStarted += this.RequestStartSpawnSession;
			this._roundController.OnRoundEnding += base.RequestStopSpawnSession;
			this._roundController.OnRoundEnding += base.SetRemainingAgentsInvulnerable;
			if (MultiplayerOptions.OptionType.NumberOfBotsPerFormation.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) == 0)
			{
				this._roundController.EnableEquipmentUpdate();
			}
			base.OnAllAgentsFromPeerSpawnedFromVisuals += this.OnAllAgentsFromPeerSpawnedFromVisuals;
			base.OnPeerSpawnedFromVisuals += this.OnPeerSpawnedFromVisuals;
		}

		// Token: 0x060026B5 RID: 9909 RVA: 0x000924A8 File Offset: 0x000906A8
		public override void Clear()
		{
			base.Clear();
			this._roundController.OnRoundStarted -= this.RequestStartSpawnSession;
			this._roundController.OnRoundEnding -= base.SetRemainingAgentsInvulnerable;
			this._roundController.OnRoundEnding -= base.RequestStopSpawnSession;
			base.OnAllAgentsFromPeerSpawnedFromVisuals -= this.OnAllAgentsFromPeerSpawnedFromVisuals;
			base.OnPeerSpawnedFromVisuals -= this.OnPeerSpawnedFromVisuals;
		}

		// Token: 0x060026B6 RID: 9910 RVA: 0x00092528 File Offset: 0x00090728
		public override void OnTick(float dt)
		{
			if (this._spawningTimerTicking)
			{
				this._spawningTimer += dt;
			}
			if (this.IsSpawningEnabled)
			{
				if (!this._roundInitialSpawnOver && this.IsRoundInProgress())
				{
					foreach (NetworkCommunicator networkPeer in GameNetwork.NetworkPeers)
					{
						MissionPeer component = networkPeer.GetComponent<MissionPeer>();
						if (((component != null) ? component.Team : null) != null && component.Team.Side != BattleSideEnum.None)
						{
							this.SpawnComponent.SetEarlyAgentVisualsDespawning(component, true);
						}
					}
					this._roundInitialSpawnOver = true;
					base.Mission.AllowAiTicking = true;
				}
				this.SpawnAgents();
				if (this._roundInitialSpawnOver && this._flagDominationMissionController.GameModeUsesSingleSpawning && this._spawningTimer > (float)MultiplayerOptions.OptionType.RoundPreparationTimeLimit.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions))
				{
					this.IsSpawningEnabled = false;
					this._spawningTimer = 0f;
					this._spawningTimerTicking = false;
				}
			}
			base.OnTick(dt);
		}

		// Token: 0x060026B7 RID: 9911 RVA: 0x00092638 File Offset: 0x00090838
		public override void RequestStartSpawnSession()
		{
			if (!this.IsSpawningEnabled)
			{
				Mission.Current.SetBattleAgentCount(-1);
				this.IsSpawningEnabled = true;
				this._spawningTimerTicking = true;
				base.ResetSpawnCounts();
				base.ResetSpawnTimers();
			}
		}

		// Token: 0x060026B8 RID: 9912 RVA: 0x00092668 File Offset: 0x00090868
		protected override void SpawnAgents()
		{
			BasicCultureObject @object = MBObjectManager.Instance.GetObject<BasicCultureObject>(MultiplayerOptions.OptionType.CultureTeam1.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions));
			BasicCultureObject object2 = MBObjectManager.Instance.GetObject<BasicCultureObject>(MultiplayerOptions.OptionType.CultureTeam2.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions));
			int intValue = MultiplayerOptions.OptionType.NumberOfBotsPerFormation.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
			foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
			{
				MissionPeer component = networkCommunicator.GetComponent<MissionPeer>();
				if (networkCommunicator.IsSynchronized && component.Team != null && component.Team.Side != BattleSideEnum.None && (intValue != 0 || !this.CheckIfEnforcedSpawnTimerExpiredForPeer(component)))
				{
					Team team = component.Team;
					bool flag = team == base.Mission.AttackerTeam;
					Team defenderTeam = base.Mission.DefenderTeam;
					BasicCultureObject basicCultureObject = flag ? @object : object2;
					MultiplayerClassDivisions.MPHeroClass mpheroClassForPeer = MultiplayerClassDivisions.GetMPHeroClassForPeer(component, false);
					int num = (this._flagDominationMissionController.GetMissionType() == MultiplayerGameType.Battle) ? mpheroClassForPeer.TroopBattleCost : mpheroClassForPeer.TroopCost;
					if (component.ControlledAgent == null && !component.HasSpawnedAgentVisuals && component.Team != null && component.Team != base.Mission.SpectatorTeam && component.TeamInitialPerkInfoReady && component.SpawnTimer.Check(base.Mission.CurrentTime))
					{
						int currentGoldForPeer = this._flagDominationMissionController.GetCurrentGoldForPeer(component);
						if (mpheroClassForPeer == null || (this._flagDominationMissionController.UseGold() && num > currentGoldForPeer))
						{
							if (currentGoldForPeer >= MultiplayerClassDivisions.GetMinimumTroopCost(basicCultureObject) && component.SelectedTroopIndex != 0)
							{
								component.SelectedTroopIndex = 0;
								GameNetwork.BeginBroadcastModuleEvent();
								GameNetwork.WriteMessage(new UpdateSelectedTroopIndex(networkCommunicator, 0));
								GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.ExcludeOtherTeamPlayers, networkCommunicator);
							}
						}
						else
						{
							if (intValue == 0)
							{
								this.CreateEnforcedSpawnTimerForPeer(component, 15);
							}
							Formation formation = component.ControlledFormation;
							if (intValue > 0 && formation == null)
							{
								FormationClass formationIndex = component.Team.FormationsIncludingEmpty.First((Formation x) => x.PlayerOwner == null && !x.ContainsAgentVisuals && x.CountOfUnits == 0).FormationIndex;
								formation = team.GetFormation(formationIndex);
								formation.ContainsAgentVisuals = true;
								if (string.IsNullOrEmpty(formation.BannerCode))
								{
									formation.BannerCode = component.Peer.BannerCode;
								}
							}
							BasicCharacterObject heroCharacter = mpheroClassForPeer.HeroCharacter;
							AgentBuildData agentBuildData = new AgentBuildData(heroCharacter).MissionPeer(component).Team(component.Team).VisualsIndex(0).Formation(formation).MakeUnitStandOutOfFormationDistance(7f).IsFemale(component.Peer.IsFemale).BodyProperties(base.GetBodyProperties(component, (component.Team == base.Mission.AttackerTeam) ? @object : object2)).ClothingColor1((team == base.Mission.AttackerTeam) ? basicCultureObject.Color : basicCultureObject.ClothAlternativeColor).ClothingColor2((team == base.Mission.AttackerTeam) ? basicCultureObject.Color2 : basicCultureObject.ClothAlternativeColor2);
							MPPerkObject.MPOnSpawnPerkHandler onSpawnPerkHandler = MPPerkObject.GetOnSpawnPerkHandler(component);
							Equipment equipment = heroCharacter.Equipment.Clone(false);
							IEnumerable<ValueTuple<EquipmentIndex, EquipmentElement>> enumerable = (onSpawnPerkHandler != null) ? onSpawnPerkHandler.GetAlternativeEquipments(true) : null;
							if (enumerable != null)
							{
								foreach (ValueTuple<EquipmentIndex, EquipmentElement> valueTuple in enumerable)
								{
									equipment[valueTuple.Item1] = valueTuple.Item2;
								}
							}
							int amountOfAgentVisualsForPeer = component.GetAmountOfAgentVisualsForPeer();
							bool flag2 = amountOfAgentVisualsForPeer > 0;
							agentBuildData.Equipment(equipment);
							if (intValue == 0)
							{
								if (!flag2)
								{
									MatrixFrame spawnFrame = this.SpawnComponent.GetSpawnFrame(component.Team, equipment[EquipmentIndex.ArmorItemEndSlot].Item != null, true);
									agentBuildData.InitialPosition(spawnFrame.origin);
									AgentBuildData agentBuildData2 = agentBuildData;
									Vec2 vec = spawnFrame.rotation.f.AsVec2;
									vec = vec.Normalized();
									agentBuildData2.InitialDirection(vec);
								}
								else
								{
									MatrixFrame frame = component.GetAgentVisualForPeer(0).GetFrame();
									agentBuildData.InitialPosition(frame.origin);
									AgentBuildData agentBuildData3 = agentBuildData;
									Vec2 vec = frame.rotation.f.AsVec2;
									vec = vec.Normalized();
									agentBuildData3.InitialDirection(vec);
								}
							}
							if (this.GameMode.ShouldSpawnVisualsForServer(networkCommunicator))
							{
								base.AgentVisualSpawnComponent.SpawnAgentVisualsForPeer(component, agentBuildData, component.SelectedTroopIndex, false, 0);
								if (agentBuildData.AgentVisualsIndex == 0)
								{
									component.HasSpawnedAgentVisuals = true;
									component.EquipmentUpdatingExpired = false;
								}
							}
							this.GameMode.HandleAgentVisualSpawning(networkCommunicator, agentBuildData, 0, true);
							component.ControlledFormation = formation;
							if (intValue > 0)
							{
								int troopCount = MPPerkObject.GetTroopCount(mpheroClassForPeer, intValue, onSpawnPerkHandler);
								IEnumerable<ValueTuple<EquipmentIndex, EquipmentElement>> alternativeEquipments = (onSpawnPerkHandler != null) ? onSpawnPerkHandler.GetAlternativeEquipments(false) : null;
								for (int i = 0; i < troopCount; i++)
								{
									if (i + 1 >= amountOfAgentVisualsForPeer)
									{
										flag2 = false;
									}
									this.SpawnBotVisualsInPlayerFormation(component, i + 1, team, basicCultureObject, mpheroClassForPeer.TroopCharacter.StringId, formation, flag2, troopCount, alternativeEquipments);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x00092BA0 File Offset: 0x00090DA0
		private new void OnPeerSpawnedFromVisuals(MissionPeer peer)
		{
			if (peer.ControlledFormation != null)
			{
				peer.ControlledAgent.Team.AssignPlayerAsSergeantOfFormation(peer, peer.ControlledFormation.FormationIndex);
			}
		}

		// Token: 0x060026BA RID: 9914 RVA: 0x00092BC8 File Offset: 0x00090DC8
		private new void OnAllAgentsFromPeerSpawnedFromVisuals(MissionPeer peer)
		{
			if (peer.ControlledFormation != null)
			{
				peer.ControlledFormation.OnFormationDispersed();
				peer.ControlledFormation.SetMovementOrder(MovementOrder.MovementOrderFollow(peer.ControlledAgent));
				NetworkCommunicator networkPeer = peer.GetNetworkPeer();
				if (peer.BotsUnderControlAlive != 0 || peer.BotsUnderControlTotal != 0)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new BotsControlledChange(networkPeer, peer.BotsUnderControlAlive, peer.BotsUnderControlTotal));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
					base.Mission.GetMissionBehavior<MissionMultiplayerGameModeFlagDominationClient>().OnBotsControlledChanged(peer, peer.BotsUnderControlAlive, peer.BotsUnderControlTotal);
				}
				if (peer.Team == base.Mission.AttackerTeam)
				{
					base.Mission.NumOfFormationsSpawnedTeamOne++;
				}
				else
				{
					base.Mission.NumOfFormationsSpawnedTeamTwo++;
				}
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new SetSpawnedFormationCount(base.Mission.NumOfFormationsSpawnedTeamOne, base.Mission.NumOfFormationsSpawnedTeamTwo));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
			}
			if (this._flagDominationMissionController.UseGold())
			{
				bool flag = peer.Team == base.Mission.AttackerTeam;
				Team defenderTeam = base.Mission.DefenderTeam;
				MultiplayerClassDivisions.MPHeroClass mpheroClass = MultiplayerClassDivisions.GetMPHeroClasses(MBObjectManager.Instance.GetObject<BasicCultureObject>(flag ? MultiplayerOptions.OptionType.CultureTeam1.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) : MultiplayerOptions.OptionType.CultureTeam2.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions))).ElementAt(peer.SelectedTroopIndex);
				int num = (this._flagDominationMissionController.GetMissionType() == MultiplayerGameType.Battle) ? mpheroClass.TroopBattleCost : mpheroClass.TroopCost;
				this._flagDominationMissionController.ChangeCurrentGoldForPeer(peer, this._flagDominationMissionController.GetCurrentGoldForPeer(peer) - num);
			}
		}

		// Token: 0x060026BB RID: 9915 RVA: 0x00092D58 File Offset: 0x00090F58
		private void BotFormationSpawned(Team team)
		{
			if (team == base.Mission.AttackerTeam)
			{
				base.Mission.NumOfFormationsSpawnedTeamOne++;
				return;
			}
			if (team == base.Mission.DefenderTeam)
			{
				base.Mission.NumOfFormationsSpawnedTeamTwo++;
			}
		}

		// Token: 0x060026BC RID: 9916 RVA: 0x00092DA8 File Offset: 0x00090FA8
		private void AllBotFormationsSpawned()
		{
			if (base.Mission.NumOfFormationsSpawnedTeamOne != 0 || base.Mission.NumOfFormationsSpawnedTeamTwo != 0)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new SetSpawnedFormationCount(base.Mission.NumOfFormationsSpawnedTeamOne, base.Mission.NumOfFormationsSpawnedTeamTwo));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
			}
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x00092DFC File Offset: 0x00090FFC
		public override bool AllowEarlyAgentVisualsDespawning(MissionPeer lobbyPeer)
		{
			if (MultiplayerOptions.OptionType.NumberOfBotsPerFormation.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) != 0)
			{
				return false;
			}
			if (!this._roundController.IsRoundInProgress)
			{
				return false;
			}
			if (!lobbyPeer.HasSpawnTimerExpired && lobbyPeer.SpawnTimer.Check(Mission.Current.CurrentTime))
			{
				lobbyPeer.HasSpawnTimerExpired = true;
			}
			return lobbyPeer.HasSpawnTimerExpired;
		}

		// Token: 0x060026BE RID: 9918 RVA: 0x00092E50 File Offset: 0x00091050
		protected override bool IsRoundInProgress()
		{
			return this._roundController.IsRoundInProgress;
		}

		// Token: 0x060026BF RID: 9919 RVA: 0x00092E60 File Offset: 0x00091060
		private void CreateEnforcedSpawnTimerForPeer(MissionPeer peer, int durationInSeconds)
		{
			if (this._enforcedSpawnTimers.Any((KeyValuePair<MissionPeer, Timer> pair) => pair.Key == peer))
			{
				return;
			}
			this._enforcedSpawnTimers.Add(new KeyValuePair<MissionPeer, Timer>(peer, new Timer(base.Mission.CurrentTime, (float)durationInSeconds, true)));
			Debug.Print(string.Concat(new object[]
			{
				"EST for ",
				peer.Name,
				" set to ",
				durationInSeconds,
				" seconds."
			}), 0, Debug.DebugColor.Yellow, 64UL);
		}

		// Token: 0x060026C0 RID: 9920 RVA: 0x00092F04 File Offset: 0x00091104
		private bool CheckIfEnforcedSpawnTimerExpiredForPeer(MissionPeer peer)
		{
			KeyValuePair<MissionPeer, Timer> keyValuePair = this._enforcedSpawnTimers.FirstOrDefault((KeyValuePair<MissionPeer, Timer> pr) => pr.Key == peer);
			if (keyValuePair.Key == null)
			{
				return false;
			}
			if (peer.ControlledAgent != null)
			{
				this._enforcedSpawnTimers.RemoveAll((KeyValuePair<MissionPeer, Timer> p) => p.Key == peer);
				Debug.Print("EST for " + peer.Name + " is no longer valid (spawned already).", 0, Debug.DebugColor.Yellow, 64UL);
				return false;
			}
			Timer value = keyValuePair.Value;
			if (peer.HasSpawnedAgentVisuals && value.Check(Mission.Current.CurrentTime))
			{
				this.SpawnComponent.SetEarlyAgentVisualsDespawning(peer, true);
				this._enforcedSpawnTimers.RemoveAll((KeyValuePair<MissionPeer, Timer> p) => p.Key == peer);
				Debug.Print("EST for " + peer.Name + " has expired.", 0, Debug.DebugColor.Yellow, 64UL);
				return true;
			}
			return false;
		}

		// Token: 0x060026C1 RID: 9921 RVA: 0x00093006 File Offset: 0x00091206
		public override void OnClearScene()
		{
			base.OnClearScene();
			this._enforcedSpawnTimers.Clear();
			this._roundInitialSpawnOver = false;
		}

		// Token: 0x060026C2 RID: 9922 RVA: 0x00093020 File Offset: 0x00091220
		protected void SpawnBotInBotFormation(int visualsIndex, Team agentTeam, BasicCultureObject cultureLimit, BasicCharacterObject character, Formation formation)
		{
			AgentBuildData agentBuildData = new AgentBuildData(character).Team(agentTeam).TroopOrigin(new BasicBattleAgentOrigin(character)).VisualsIndex(visualsIndex).EquipmentSeed(this.MissionLobbyComponent.GetRandomFaceSeedForCharacter(character, visualsIndex)).Formation(formation).IsFemale(character.IsFemale).ClothingColor1((agentTeam.Side == BattleSideEnum.Attacker) ? cultureLimit.Color : cultureLimit.ClothAlternativeColor).ClothingColor2((agentTeam.Side == BattleSideEnum.Attacker) ? cultureLimit.Color2 : cultureLimit.ClothAlternativeColor2);
			agentBuildData.Equipment(Equipment.GetRandomEquipmentElements(character, !GameNetwork.IsMultiplayer, false, agentBuildData.AgentEquipmentSeed));
			agentBuildData.BodyProperties(BodyProperties.GetRandomBodyProperties(agentBuildData.AgentRace, agentBuildData.AgentIsFemale, character.GetBodyPropertiesMin(false), character.GetBodyPropertiesMax(), (int)agentBuildData.AgentOverridenSpawnEquipment.HairCoverType, agentBuildData.AgentEquipmentSeed, character.HairTags, character.BeardTags, character.TattooTags));
			base.Mission.SpawnAgent(agentBuildData, false).AIStateFlags |= Agent.AIStateFlag.Alarmed;
		}

		// Token: 0x060026C3 RID: 9923 RVA: 0x00093130 File Offset: 0x00091330
		protected void SpawnBotVisualsInPlayerFormation(MissionPeer missionPeer, int visualsIndex, Team agentTeam, BasicCultureObject cultureLimit, string troopName, Formation formation, bool updateExistingAgentVisuals, int totalCount, IEnumerable<ValueTuple<EquipmentIndex, EquipmentElement>> alternativeEquipments)
		{
			BasicCharacterObject @object = MBObjectManager.Instance.GetObject<BasicCharacterObject>(troopName);
			AgentBuildData agentBuildData = new AgentBuildData(@object).Team(agentTeam).OwningMissionPeer(missionPeer).VisualsIndex(visualsIndex).TroopOrigin(new BasicBattleAgentOrigin(@object)).EquipmentSeed(this.MissionLobbyComponent.GetRandomFaceSeedForCharacter(@object, visualsIndex)).Formation(formation).IsFemale(@object.IsFemale).ClothingColor1((agentTeam.Side == BattleSideEnum.Attacker) ? cultureLimit.Color : cultureLimit.ClothAlternativeColor).ClothingColor2((agentTeam.Side == BattleSideEnum.Attacker) ? cultureLimit.Color2 : cultureLimit.ClothAlternativeColor2);
			Equipment randomEquipmentElements = Equipment.GetRandomEquipmentElements(@object, !GameNetwork.IsMultiplayer, false, MBRandom.RandomInt());
			if (alternativeEquipments != null)
			{
				foreach (ValueTuple<EquipmentIndex, EquipmentElement> valueTuple in alternativeEquipments)
				{
					randomEquipmentElements[valueTuple.Item1] = valueTuple.Item2;
				}
			}
			agentBuildData.Equipment(randomEquipmentElements);
			agentBuildData.BodyProperties(BodyProperties.GetRandomBodyProperties(agentBuildData.AgentRace, agentBuildData.AgentIsFemale, @object.GetBodyPropertiesMin(false), @object.GetBodyPropertiesMax(), (int)agentBuildData.AgentOverridenSpawnEquipment.HairCoverType, agentBuildData.AgentEquipmentSeed, @object.HairTags, @object.BeardTags, @object.TattooTags));
			NetworkCommunicator networkPeer = missionPeer.GetNetworkPeer();
			if (this.GameMode.ShouldSpawnVisualsForServer(networkPeer))
			{
				base.AgentVisualSpawnComponent.SpawnAgentVisualsForPeer(missionPeer, agentBuildData, -1, true, totalCount);
				if (agentBuildData.AgentVisualsIndex == 0)
				{
					missionPeer.HasSpawnedAgentVisuals = true;
					missionPeer.EquipmentUpdatingExpired = false;
				}
			}
			this.GameMode.HandleAgentVisualSpawning(networkPeer, agentBuildData, totalCount, false);
		}

		// Token: 0x04000E66 RID: 3686
		private const int EnforcedSpawnTimeInSeconds = 15;

		// Token: 0x04000E67 RID: 3687
		private float _spawningTimer;

		// Token: 0x04000E68 RID: 3688
		private bool _spawningTimerTicking;

		// Token: 0x04000E69 RID: 3689
		private bool _roundInitialSpawnOver;

		// Token: 0x04000E6A RID: 3690
		private MissionMultiplayerFlagDomination _flagDominationMissionController;

		// Token: 0x04000E6B RID: 3691
		private MultiplayerRoundController _roundController;

		// Token: 0x04000E6C RID: 3692
		private List<KeyValuePair<MissionPeer, Timer>> _enforcedSpawnTimers;
	}
}
