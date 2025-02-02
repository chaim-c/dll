using System;
using System.Collections.Generic;
using System.Linq;
using NetworkMessages.FromClient;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;
using TaleWorlds.MountAndBlade.MissionRepresentatives;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002A3 RID: 675
	public class MissionMultiplayerDuel : MissionMultiplayerGameModeBase
	{
		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x0600244E RID: 9294 RVA: 0x00086D2B File Offset: 0x00084F2B
		public override bool IsGameModeHidingAllAgentVisuals
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x0600244F RID: 9295 RVA: 0x00086D2E File Offset: 0x00084F2E
		public override bool IsGameModeUsingOpposingTeams
		{
			get
			{
				return false;
			}
		}

		// Token: 0x14000057 RID: 87
		// (add) Token: 0x06002450 RID: 9296 RVA: 0x00086D34 File Offset: 0x00084F34
		// (remove) Token: 0x06002451 RID: 9297 RVA: 0x00086D6C File Offset: 0x00084F6C
		public event MissionMultiplayerDuel.OnDuelEndedDelegate OnDuelEnded;

		// Token: 0x06002452 RID: 9298 RVA: 0x00086DA1 File Offset: 0x00084FA1
		public override MultiplayerGameType GetMissionType()
		{
			return MultiplayerGameType.Duel;
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x00086DA4 File Offset: 0x00084FA4
		public override void AfterStart()
		{
			base.AfterStart();
			Mission.Current.SetMissionCorpseFadeOutTimeInSeconds(1f);
			BasicCultureObject @object = MBObjectManager.Instance.GetObject<BasicCultureObject>(MultiplayerOptions.OptionType.CultureTeam1.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions));
			Banner banner = new Banner(@object.BannerKey, @object.BackgroundColor1, @object.ForegroundColor1);
			base.Mission.Teams.Add(BattleSideEnum.Attacker, @object.BackgroundColor1, @object.ForegroundColor1, banner, false, false, true);
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x00086E14 File Offset: 0x00085014
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._duelAreaFlags.AddRange(Mission.Current.Scene.FindEntitiesWithTagExpression("area_flag(_\\d+)*"));
			List<GameEntity> list = new List<GameEntity>();
			list.AddRange(Mission.Current.Scene.FindEntitiesWithTagExpression("area_box(_\\d+)*"));
			this._cachedSelectedAreaFlags = new KeyValuePair<int, TroopType>[this._duelAreaFlags.Count];
			for (int i = 0; i < list.Count; i++)
			{
				VolumeBox firstScriptOfType = list[i].GetFirstScriptOfType<VolumeBox>();
				this._areaBoxes.Add(firstScriptOfType);
			}
			this._cachedSelectedVolumeBoxes = new VolumeBox[this._areaBoxes.Count];
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x00086EBC File Offset: 0x000850BC
		protected override void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegistererContainer registerer)
		{
			registerer.RegisterBaseHandler<NetworkMessages.FromClient.DuelRequest>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventDuelRequest));
			registerer.RegisterBaseHandler<DuelResponse>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventDuelRequestAccepted));
			registerer.RegisterBaseHandler<RequestChangePreferredTroopType>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventDuelRequestChangePreferredTroopType));
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x00086EF4 File Offset: 0x000850F4
		protected override void HandleEarlyNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
			networkPeer.AddComponent<DuelMissionRepresentative>();
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x00086F00 File Offset: 0x00085100
		protected override void HandleNewClientAfterSynchronized(NetworkCommunicator networkPeer)
		{
			MissionPeer component = networkPeer.GetComponent<MissionPeer>();
			component.Team = base.Mission.AttackerTeam;
			this._peersAndSelections.Add(new KeyValuePair<MissionPeer, TroopType>(component, TroopType.Invalid));
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x00086F38 File Offset: 0x00085138
		private bool HandleClientEventDuelRequest(NetworkCommunicator peer, GameNetworkMessage baseMessage)
		{
			NetworkMessages.FromClient.DuelRequest duelRequest = (NetworkMessages.FromClient.DuelRequest)baseMessage;
			MissionPeer missionPeer = (peer != null) ? peer.GetComponent<MissionPeer>() : null;
			if (missionPeer != null)
			{
				Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(duelRequest.RequestedAgentIndex, false);
				if (agentFromIndex != null && agentFromIndex.IsActive())
				{
					this.DuelRequestReceived(missionPeer, agentFromIndex.MissionPeer);
				}
			}
			return true;
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x00086F84 File Offset: 0x00085184
		private bool HandleClientEventDuelRequestAccepted(NetworkCommunicator peer, GameNetworkMessage baseMessage)
		{
			DuelResponse duelResponse = (DuelResponse)baseMessage;
			if (((peer != null) ? peer.GetComponent<MissionPeer>() : null) != null && peer.GetComponent<MissionPeer>().ControlledAgent != null)
			{
				NetworkCommunicator peer2 = duelResponse.Peer;
				if (((peer2 != null) ? peer2.GetComponent<MissionPeer>() : null) != null && duelResponse.Peer.GetComponent<MissionPeer>().ControlledAgent != null)
				{
					this.DuelRequestAccepted(duelResponse.Peer.GetComponent<DuelMissionRepresentative>().ControlledAgent, peer.GetComponent<DuelMissionRepresentative>().ControlledAgent);
				}
			}
			return true;
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x00086FFC File Offset: 0x000851FC
		private bool HandleClientEventDuelRequestChangePreferredTroopType(NetworkCommunicator peer, GameNetworkMessage baseMessage)
		{
			RequestChangePreferredTroopType requestChangePreferredTroopType = (RequestChangePreferredTroopType)baseMessage;
			this.OnPeerSelectedPreferredTroopType(peer.GetComponent<MissionPeer>(), requestChangePreferredTroopType.TroopType);
			return true;
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x00087024 File Offset: 0x00085224
		public override bool CheckIfPlayerCanDespawn(MissionPeer missionPeer)
		{
			for (int i = 0; i < this._activeDuels.Count; i++)
			{
				if (this._activeDuels[i].IsPeerInThisDuel(missionPeer))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x0008705E File Offset: 0x0008525E
		public void OnPlayerDespawn(MissionPeer missionPeer)
		{
			missionPeer.GetComponent<DuelMissionRepresentative>();
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x00087068 File Offset: 0x00085268
		public void DuelRequestReceived(MissionPeer requesterPeer, MissionPeer requesteePeer)
		{
			if (!this.IsThereARequestBetweenPeers(requesterPeer, requesteePeer) && !this.IsHavingDuel(requesterPeer) && !this.IsHavingDuel(requesteePeer))
			{
				MissionMultiplayerDuel.DuelInfo duelInfo = new MissionMultiplayerDuel.DuelInfo(requesterPeer, requesteePeer, this.GetNextAvailableDuelAreaIndex(requesterPeer.ControlledAgent));
				this._duelRequests.Add(duelInfo);
				(requesteePeer.Representative as DuelMissionRepresentative).DuelRequested(requesterPeer.ControlledAgent, duelInfo.DuelAreaTroopType);
			}
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x000870D0 File Offset: 0x000852D0
		private KeyValuePair<int, TroopType> GetNextAvailableDuelAreaIndex(Agent requesterAgent)
		{
			TroopType troopType = TroopType.Invalid;
			for (int i = 0; i < this._peersAndSelections.Count; i++)
			{
				if (this._peersAndSelections[i].Key == requesterAgent.MissionPeer)
				{
					troopType = this._peersAndSelections[i].Value;
					break;
				}
			}
			if (troopType == TroopType.Invalid)
			{
				troopType = this.GetAgentTroopType(requesterAgent);
			}
			bool flag = false;
			int num = 0;
			for (int j = 0; j < this._duelAreaFlags.Count; j++)
			{
				GameEntity gameEntity = this._duelAreaFlags[j];
				int num2 = int.Parse(gameEntity.Tags.Single((string ft) => ft.StartsWith("area_flag_")).Replace("area_flag_", ""));
				int flagIndex = num2 - 1;
				if (this._activeDuels.All((MissionMultiplayerDuel.DuelInfo ad) => ad.DuelAreaIndex != flagIndex) && this._restartingDuels.All((MissionMultiplayerDuel.DuelInfo ad) => ad.DuelAreaIndex != flagIndex) && this._restartPreparationDuels.All((MissionMultiplayerDuel.DuelInfo ad) => ad.DuelAreaIndex != flagIndex))
				{
					TroopType troopType2 = gameEntity.HasTag("flag_infantry") ? TroopType.Infantry : (gameEntity.HasTag("flag_archery") ? TroopType.Ranged : TroopType.Cavalry);
					if (!flag && troopType2 == troopType)
					{
						flag = true;
						num = 0;
					}
					if (!flag || troopType2 == troopType)
					{
						this._cachedSelectedAreaFlags[num] = new KeyValuePair<int, TroopType>(flagIndex, troopType2);
						num++;
					}
				}
			}
			return this._cachedSelectedAreaFlags[MBRandom.RandomInt(num)];
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x00087278 File Offset: 0x00085478
		public void DuelRequestAccepted(Agent requesterAgent, Agent requesteeAgent)
		{
			MissionMultiplayerDuel.DuelInfo duelInfo = this._duelRequests.FirstOrDefault((MissionMultiplayerDuel.DuelInfo dr) => dr.IsPeerInThisDuel(requesterAgent.MissionPeer) && dr.IsPeerInThisDuel(requesteeAgent.MissionPeer));
			if (duelInfo != null)
			{
				this.PrepareDuel(duelInfo);
			}
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x000872BB File Offset: 0x000854BB
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			this.CheckRestartPreparationDuels();
			this.CheckForRestartingDuels();
			this.CheckDuelsToStart();
			this.CheckDuelRequestTimeouts();
			this.CheckEndedDuels();
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x000872E4 File Offset: 0x000854E4
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow blow)
		{
			if (!affectedAgent.IsHuman)
			{
				return;
			}
			if (affectedAgent.MissionPeer.Team.IsDefender)
			{
				MissionMultiplayerDuel.DuelInfo duelInfo = null;
				for (int i = 0; i < this._activeDuels.Count; i++)
				{
					if (this._activeDuels[i].IsPeerInThisDuel(affectedAgent.MissionPeer))
					{
						duelInfo = this._activeDuels[i];
					}
				}
				if (duelInfo != null && !this._endingDuels.Contains(duelInfo))
				{
					duelInfo.OnDuelEnding();
					this._endingDuels.Add(duelInfo);
					return;
				}
			}
			else
			{
				for (int j = this._duelRequests.Count - 1; j >= 0; j--)
				{
					if (this._duelRequests[j].IsPeerInThisDuel(affectedAgent.MissionPeer))
					{
						this._duelRequests.RemoveAt(j);
					}
				}
			}
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x000873AB File Offset: 0x000855AB
		private Team ActivateAndGetDuelTeam()
		{
			if (this._deactiveDuelTeams.Count <= 0)
			{
				return base.Mission.Teams.Add(BattleSideEnum.Defender, uint.MaxValue, uint.MaxValue, null, true, false, false);
			}
			return this._deactiveDuelTeams.Dequeue();
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x000873DE File Offset: 0x000855DE
		private void DeactivateDuelTeam(Team team)
		{
			this._deactiveDuelTeams.Enqueue(team);
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x000873EC File Offset: 0x000855EC
		private bool IsHavingDuel(MissionPeer peer)
		{
			return this._activeDuels.AnyQ((MissionMultiplayerDuel.DuelInfo d) => d.IsPeerInThisDuel(peer));
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x00087420 File Offset: 0x00085620
		private bool IsThereARequestBetweenPeers(MissionPeer requesterAgent, MissionPeer requesteeAgent)
		{
			for (int i = 0; i < this._duelRequests.Count; i++)
			{
				if (this._duelRequests[i].IsPeerInThisDuel(requesterAgent) && this._duelRequests[i].IsPeerInThisDuel(requesteeAgent))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x00087470 File Offset: 0x00085670
		private void CheckDuelsToStart()
		{
			for (int i = this._activeDuels.Count - 1; i >= 0; i--)
			{
				MissionMultiplayerDuel.DuelInfo duelInfo = this._activeDuels[i];
				if (!duelInfo.Started && duelInfo.Timer.IsPast && duelInfo.IsDuelStillValid(false))
				{
					this.StartDuel(duelInfo);
				}
			}
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x000874CC File Offset: 0x000856CC
		private void CheckDuelRequestTimeouts()
		{
			for (int i = this._duelRequests.Count - 1; i >= 0; i--)
			{
				MissionMultiplayerDuel.DuelInfo duelInfo = this._duelRequests[i];
				if (duelInfo.Timer.IsPast)
				{
					this._duelRequests.Remove(duelInfo);
				}
			}
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x0008751C File Offset: 0x0008571C
		private void CheckForRestartingDuels()
		{
			for (int i = this._restartingDuels.Count - 1; i >= 0; i--)
			{
				if (!this._restartingDuels[i].IsDuelStillValid(true))
				{
					Debug.Print("!_restartingDuels[i].IsDuelStillValid(true)", 0, Debug.DebugColor.White, 17592186044416UL);
				}
				this._duelRequests.Add(this._restartingDuels[i]);
				this.PrepareDuel(this._restartingDuels[i]);
				this._restartingDuels.RemoveAt(i);
			}
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x000875A0 File Offset: 0x000857A0
		private void CheckEndedDuels()
		{
			for (int i = this._endingDuels.Count - 1; i >= 0; i--)
			{
				MissionMultiplayerDuel.DuelInfo duelInfo = this._endingDuels[i];
				if (duelInfo.Timer.IsPast)
				{
					this.EndDuel(duelInfo);
					this._endingDuels.RemoveAt(i);
					if (!duelInfo.ChallengeEnded)
					{
						this._restartPreparationDuels.Add(duelInfo);
					}
				}
			}
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x0008760C File Offset: 0x0008580C
		private void CheckRestartPreparationDuels()
		{
			for (int i = this._restartPreparationDuels.Count - 1; i >= 0; i--)
			{
				MissionMultiplayerDuel.DuelInfo duelInfo = this._restartPreparationDuels[i];
				Agent controlledAgent = duelInfo.RequesterPeer.ControlledAgent;
				Agent controlledAgent2 = duelInfo.RequesteePeer.ControlledAgent;
				if ((controlledAgent == null || controlledAgent.IsActive()) && (controlledAgent2 == null || controlledAgent2.IsActive()))
				{
					this._restartPreparationDuels.RemoveAt(i);
					this._restartingDuels.Add(duelInfo);
				}
			}
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x00087684 File Offset: 0x00085884
		private void PrepareDuel(MissionMultiplayerDuel.DuelInfo duel)
		{
			this._duelRequests.Remove(duel);
			if (!this.IsHavingDuel(duel.RequesteePeer) && !this.IsHavingDuel(duel.RequesterPeer))
			{
				this._activeDuels.Add(duel);
				Team duelTeam = duel.Started ? duel.DuelingTeam : this.ActivateAndGetDuelTeam();
				duel.OnDuelPreparation(duelTeam);
				for (int i = 0; i < this._duelRequests.Count; i++)
				{
					if (this._duelRequests[i].DuelAreaIndex == duel.DuelAreaIndex)
					{
						this._duelRequests[i].UpdateDuelAreaIndex(this.GetNextAvailableDuelAreaIndex(this._duelRequests[i].RequesterPeer.ControlledAgent));
					}
				}
				return;
			}
			Debug.FailedAssert("IsHavingDuel(duel.RequesteePeer) || IsHavingDuel(duel.RequesterPeer)", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Multiplayer\\MissionNetworkLogics\\MultiplayerGameModeLogics\\ServerGameModeLogics\\MissionMultiplayerDuel.cs", "PrepareDuel", 707);
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x00087760 File Offset: 0x00085960
		private void StartDuel(MissionMultiplayerDuel.DuelInfo duel)
		{
			duel.OnDuelStarted();
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x00087768 File Offset: 0x00085968
		private void EndDuel(MissionMultiplayerDuel.DuelInfo duel)
		{
			this._activeDuels.Remove(duel);
			duel.OnDuelEnded();
			this.CleanSpawnedEntitiesInDuelArea(duel.DuelAreaIndex);
			if (duel.ChallengeEnded)
			{
				TroopType troopType = TroopType.Invalid;
				MissionPeer challengeWinnerPeer = duel.ChallengeWinnerPeer;
				if (((challengeWinnerPeer != null) ? challengeWinnerPeer.ControlledAgent : null) != null)
				{
					troopType = this.GetAgentTroopType(challengeWinnerPeer.ControlledAgent);
				}
				MissionMultiplayerDuel.OnDuelEndedDelegate onDuelEnded = this.OnDuelEnded;
				if (onDuelEnded != null)
				{
					onDuelEnded(challengeWinnerPeer, troopType);
				}
				this.DeactivateDuelTeam(duel.DuelingTeam);
				this.HandleEndedChallenge(duel);
			}
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x000877E8 File Offset: 0x000859E8
		private TroopType GetAgentTroopType(Agent requesterAgent)
		{
			TroopType result = TroopType.Invalid;
			switch (requesterAgent.Character.DefaultFormationClass)
			{
			case FormationClass.Infantry:
			case FormationClass.HeavyInfantry:
				result = TroopType.Infantry;
				break;
			case FormationClass.Ranged:
				result = TroopType.Ranged;
				break;
			case FormationClass.Cavalry:
			case FormationClass.HorseArcher:
			case FormationClass.LightCavalry:
			case FormationClass.HeavyCavalry:
				result = TroopType.Cavalry;
				break;
			}
			return result;
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x00087838 File Offset: 0x00085A38
		private void CleanSpawnedEntitiesInDuelArea(int duelAreaIndex)
		{
			int num = duelAreaIndex + 1;
			int num2 = 0;
			for (int i = 0; i < this._areaBoxes.Count; i++)
			{
				if (this._areaBoxes[i].GameEntity.HasTag(string.Format("{0}_{1}", "area_box", num)))
				{
					this._cachedSelectedVolumeBoxes[num2] = this._areaBoxes[i];
					num2++;
				}
			}
			for (int j = 0; j < Mission.Current.ActiveMissionObjects.Count; j++)
			{
				SpawnedItemEntity spawnedItemEntity;
				if ((spawnedItemEntity = (Mission.Current.ActiveMissionObjects[j] as SpawnedItemEntity)) != null && !spawnedItemEntity.IsDeactivated)
				{
					for (int k = 0; k < num2; k++)
					{
						if (this._cachedSelectedVolumeBoxes[k].IsPointIn(spawnedItemEntity.GameEntity.GlobalPosition))
						{
							spawnedItemEntity.RequestDeletionOnNextTick();
							break;
						}
					}
				}
			}
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x00087918 File Offset: 0x00085B18
		private void HandleEndedChallenge(MissionMultiplayerDuel.DuelInfo duel)
		{
			MissionPeer challengeWinnerPeer = duel.ChallengeWinnerPeer;
			MissionPeer challengeLoserPeer = duel.ChallengeLoserPeer;
			if (challengeWinnerPeer != null)
			{
				DuelMissionRepresentative component = challengeWinnerPeer.GetComponent<DuelMissionRepresentative>();
				DuelMissionRepresentative component2 = challengeLoserPeer.GetComponent<DuelMissionRepresentative>();
				MultiplayerClassDivisions.MPHeroClass mpheroClassForPeer = MultiplayerClassDivisions.GetMPHeroClassForPeer(challengeWinnerPeer, true);
				MultiplayerClassDivisions.MPHeroClass mpheroClassForPeer2 = MultiplayerClassDivisions.GetMPHeroClassForPeer(challengeLoserPeer, true);
				float gainedScore = (float)MathF.Max(100, component2.Bounty) * MathF.Max(1f, (float)mpheroClassForPeer.TroopCasualCost / (float)mpheroClassForPeer2.TroopCasualCost) * MathF.Pow(2.7182817f, (float)component.NumberOfWins / 10f);
				component.OnDuelWon(gainedScore);
				if (challengeWinnerPeer.Peer.Communicator.IsConnectionActive)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new DuelPointsUpdateMessage(component));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
				}
				component2.ResetBountyAndNumberOfWins();
				if (challengeLoserPeer.Peer.Communicator.IsConnectionActive)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new DuelPointsUpdateMessage(component2));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
				}
			}
			PeerComponent peerComponent = challengeWinnerPeer ?? duel.RequesterPeer;
			GameNetwork.BeginBroadcastModuleEvent();
			GameNetwork.WriteMessage(new DuelEnded(peerComponent.GetNetworkPeer()));
			GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x00087A24 File Offset: 0x00085C24
		public int GetDuelAreaIndexIfDuelTeam(Team team)
		{
			if (team.IsDefender)
			{
				return this._activeDuels.FirstOrDefaultQ((MissionMultiplayerDuel.DuelInfo ad) => ad.DuelingTeam == team).DuelAreaIndex;
			}
			return -1;
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x00087A6C File Offset: 0x00085C6C
		public override void OnAgentBuild(Agent agent, Banner banner)
		{
			if (agent.IsHuman && agent.Team != null && agent.Team.IsDefender)
			{
				for (int i = 0; i < this._activeDuels.Count; i++)
				{
					if (this._activeDuels[i].IsPeerInThisDuel(agent.MissionPeer))
					{
						this._activeDuels[i].OnAgentBuild(agent);
						return;
					}
				}
			}
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x00087AD8 File Offset: 0x00085CD8
		protected override void HandleLateNewClientAfterSynchronized(NetworkCommunicator networkPeer)
		{
			if (!networkPeer.IsServerPeer)
			{
				foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
				{
					DuelMissionRepresentative component = networkCommunicator.GetComponent<DuelMissionRepresentative>();
					if (component != null)
					{
						GameNetwork.BeginModuleEventAsServer(networkPeer);
						GameNetwork.WriteMessage(new DuelPointsUpdateMessage(component));
						GameNetwork.EndModuleEventAsServer();
					}
					if (networkPeer != networkCommunicator)
					{
						MissionPeer component2 = networkCommunicator.GetComponent<MissionPeer>();
						if (component2 != null)
						{
							GameNetwork.BeginModuleEventAsServer(networkPeer);
							GameNetwork.WriteMessage(new SyncPerksForCurrentlySelectedTroop(networkCommunicator, component2.Perks[component2.SelectedTroopIndex]));
							GameNetwork.EndModuleEventAsServer();
						}
					}
				}
				for (int i = 0; i < this._activeDuels.Count; i++)
				{
					GameNetwork.BeginModuleEventAsServer(networkPeer);
					GameNetwork.WriteMessage(new DuelPreparationStartedForTheFirstTime(this._activeDuels[i].RequesterPeer.GetNetworkPeer(), this._activeDuels[i].RequesteePeer.GetNetworkPeer(), this._activeDuels[i].DuelAreaIndex));
					GameNetwork.EndModuleEventAsServer();
				}
			}
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x00087BF4 File Offset: 0x00085DF4
		protected override void HandleEarlyPlayerDisconnect(NetworkCommunicator networkPeer)
		{
			MissionPeer component = networkPeer.GetComponent<MissionPeer>();
			for (int i = 0; i < this._peersAndSelections.Count; i++)
			{
				if (this._peersAndSelections[i].Key == component)
				{
					this._peersAndSelections.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x00087C44 File Offset: 0x00085E44
		protected override void HandlePlayerDisconnect(NetworkCommunicator networkPeer)
		{
			MissionPeer component = networkPeer.GetComponent<MissionPeer>();
			if (component != null)
			{
				component.Team = null;
			}
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x00087C64 File Offset: 0x00085E64
		private void OnPeerSelectedPreferredTroopType(MissionPeer missionPeer, TroopType troopType)
		{
			for (int i = 0; i < this._peersAndSelections.Count; i++)
			{
				if (this._peersAndSelections[i].Key == missionPeer)
				{
					this._peersAndSelections[i] = new KeyValuePair<MissionPeer, TroopType>(missionPeer, troopType);
					return;
				}
			}
		}

		// Token: 0x04000D4D RID: 3405
		public const float DuelRequestTimeOutInSeconds = 10f;

		// Token: 0x04000D4E RID: 3406
		private const int MinBountyGain = 100;

		// Token: 0x04000D4F RID: 3407
		private const string AreaBoxTagPrefix = "area_box";

		// Token: 0x04000D50 RID: 3408
		private const string AreaFlagTagPrefix = "area_flag";

		// Token: 0x04000D51 RID: 3409
		public const int NumberOfDuelAreas = 16;

		// Token: 0x04000D52 RID: 3410
		public const float DuelEndInSeconds = 2f;

		// Token: 0x04000D53 RID: 3411
		private const float DuelRequestTimeOutServerToleranceInSeconds = 0.5f;

		// Token: 0x04000D54 RID: 3412
		private const float CorpseFadeOutTimeInSeconds = 1f;

		// Token: 0x04000D56 RID: 3414
		private List<GameEntity> _duelAreaFlags = new List<GameEntity>();

		// Token: 0x04000D57 RID: 3415
		private List<VolumeBox> _areaBoxes = new List<VolumeBox>();

		// Token: 0x04000D58 RID: 3416
		private List<MissionMultiplayerDuel.DuelInfo> _duelRequests = new List<MissionMultiplayerDuel.DuelInfo>();

		// Token: 0x04000D59 RID: 3417
		private List<MissionMultiplayerDuel.DuelInfo> _activeDuels = new List<MissionMultiplayerDuel.DuelInfo>();

		// Token: 0x04000D5A RID: 3418
		private List<MissionMultiplayerDuel.DuelInfo> _endingDuels = new List<MissionMultiplayerDuel.DuelInfo>();

		// Token: 0x04000D5B RID: 3419
		private List<MissionMultiplayerDuel.DuelInfo> _restartingDuels = new List<MissionMultiplayerDuel.DuelInfo>();

		// Token: 0x04000D5C RID: 3420
		private List<MissionMultiplayerDuel.DuelInfo> _restartPreparationDuels = new List<MissionMultiplayerDuel.DuelInfo>();

		// Token: 0x04000D5D RID: 3421
		private readonly Queue<Team> _deactiveDuelTeams = new Queue<Team>();

		// Token: 0x04000D5E RID: 3422
		private List<KeyValuePair<MissionPeer, TroopType>> _peersAndSelections = new List<KeyValuePair<MissionPeer, TroopType>>();

		// Token: 0x04000D5F RID: 3423
		private VolumeBox[] _cachedSelectedVolumeBoxes;

		// Token: 0x04000D60 RID: 3424
		private KeyValuePair<int, TroopType>[] _cachedSelectedAreaFlags;

		// Token: 0x0200055A RID: 1370
		private class DuelInfo
		{
			// Token: 0x1700099B RID: 2459
			// (get) Token: 0x06003966 RID: 14694 RVA: 0x000E4D5C File Offset: 0x000E2F5C
			public MissionPeer RequesterPeer
			{
				get
				{
					return this._challengers[0].MissionPeer;
				}
			}

			// Token: 0x1700099C RID: 2460
			// (get) Token: 0x06003967 RID: 14695 RVA: 0x000E4D6F File Offset: 0x000E2F6F
			public MissionPeer RequesteePeer
			{
				get
				{
					return this._challengers[1].MissionPeer;
				}
			}

			// Token: 0x1700099D RID: 2461
			// (get) Token: 0x06003968 RID: 14696 RVA: 0x000E4D82 File Offset: 0x000E2F82
			// (set) Token: 0x06003969 RID: 14697 RVA: 0x000E4D8A File Offset: 0x000E2F8A
			public int DuelAreaIndex { get; private set; }

			// Token: 0x1700099E RID: 2462
			// (get) Token: 0x0600396A RID: 14698 RVA: 0x000E4D93 File Offset: 0x000E2F93
			// (set) Token: 0x0600396B RID: 14699 RVA: 0x000E4D9B File Offset: 0x000E2F9B
			public TroopType DuelAreaTroopType { get; private set; }

			// Token: 0x1700099F RID: 2463
			// (get) Token: 0x0600396C RID: 14700 RVA: 0x000E4DA4 File Offset: 0x000E2FA4
			// (set) Token: 0x0600396D RID: 14701 RVA: 0x000E4DAC File Offset: 0x000E2FAC
			public MissionTime Timer { get; private set; }

			// Token: 0x170009A0 RID: 2464
			// (get) Token: 0x0600396E RID: 14702 RVA: 0x000E4DB5 File Offset: 0x000E2FB5
			// (set) Token: 0x0600396F RID: 14703 RVA: 0x000E4DBD File Offset: 0x000E2FBD
			public Team DuelingTeam { get; private set; }

			// Token: 0x170009A1 RID: 2465
			// (get) Token: 0x06003970 RID: 14704 RVA: 0x000E4DC6 File Offset: 0x000E2FC6
			// (set) Token: 0x06003971 RID: 14705 RVA: 0x000E4DCE File Offset: 0x000E2FCE
			public bool Started { get; private set; }

			// Token: 0x170009A2 RID: 2466
			// (get) Token: 0x06003972 RID: 14706 RVA: 0x000E4DD7 File Offset: 0x000E2FD7
			// (set) Token: 0x06003973 RID: 14707 RVA: 0x000E4DDF File Offset: 0x000E2FDF
			public bool ChallengeEnded { get; private set; }

			// Token: 0x170009A3 RID: 2467
			// (get) Token: 0x06003974 RID: 14708 RVA: 0x000E4DE8 File Offset: 0x000E2FE8
			public MissionPeer ChallengeWinnerPeer
			{
				get
				{
					if (this._winnerChallengerType != MissionMultiplayerDuel.DuelInfo.ChallengerType.None)
					{
						return this._challengers[(int)this._winnerChallengerType].MissionPeer;
					}
					return null;
				}
			}

			// Token: 0x170009A4 RID: 2468
			// (get) Token: 0x06003975 RID: 14709 RVA: 0x000E4E0B File Offset: 0x000E300B
			public MissionPeer ChallengeLoserPeer
			{
				get
				{
					if (this._winnerChallengerType != MissionMultiplayerDuel.DuelInfo.ChallengerType.None)
					{
						return this._challengers[(this._winnerChallengerType == MissionMultiplayerDuel.DuelInfo.ChallengerType.Requester) ? 1 : 0].MissionPeer;
					}
					return null;
				}
			}

			// Token: 0x06003976 RID: 14710 RVA: 0x000E4E34 File Offset: 0x000E3034
			public DuelInfo(MissionPeer requesterPeer, MissionPeer requesteePeer, KeyValuePair<int, TroopType> duelAreaPair)
			{
				this.DuelAreaIndex = duelAreaPair.Key;
				this.DuelAreaTroopType = duelAreaPair.Value;
				this._challengers = new MissionMultiplayerDuel.DuelInfo.Challenger[2];
				this._challengers[0] = new MissionMultiplayerDuel.DuelInfo.Challenger(requesterPeer);
				this._challengers[1] = new MissionMultiplayerDuel.DuelInfo.Challenger(requesteePeer);
				this.Timer = MissionTime.Now + MissionTime.Seconds(10.5f);
			}

			// Token: 0x06003977 RID: 14711 RVA: 0x000E4EB4 File Offset: 0x000E30B4
			private void DecideRoundWinner()
			{
				bool isConnectionActive = this._challengers[0].MissionPeer.Peer.Communicator.IsConnectionActive;
				bool isConnectionActive2 = this._challengers[1].MissionPeer.Peer.Communicator.IsConnectionActive;
				if (!this.Started)
				{
					if (isConnectionActive == isConnectionActive2)
					{
						this.ChallengeEnded = true;
					}
					else
					{
						this._winnerChallengerType = (isConnectionActive ? MissionMultiplayerDuel.DuelInfo.ChallengerType.Requester : MissionMultiplayerDuel.DuelInfo.ChallengerType.Requestee);
					}
				}
				else
				{
					Agent duelingAgent = this._challengers[0].DuelingAgent;
					Agent duelingAgent2 = this._challengers[1].DuelingAgent;
					if (duelingAgent.IsActive())
					{
						this._winnerChallengerType = MissionMultiplayerDuel.DuelInfo.ChallengerType.Requester;
					}
					else if (duelingAgent2.IsActive())
					{
						this._winnerChallengerType = MissionMultiplayerDuel.DuelInfo.ChallengerType.Requestee;
					}
					else
					{
						if (!isConnectionActive && !isConnectionActive2)
						{
							this.ChallengeEnded = true;
						}
						this._winnerChallengerType = MissionMultiplayerDuel.DuelInfo.ChallengerType.None;
					}
				}
				if (this._winnerChallengerType != MissionMultiplayerDuel.DuelInfo.ChallengerType.None)
				{
					this._challengers[(int)this._winnerChallengerType].IncreaseWinCount();
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new DuelRoundEnded(this._challengers[(int)this._winnerChallengerType].NetworkPeer));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
					if (this._challengers[(int)this._winnerChallengerType].KillCountInDuel == MultiplayerOptions.OptionType.MinScoreToWinDuel.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) || !isConnectionActive || !isConnectionActive2)
					{
						this.ChallengeEnded = true;
					}
				}
			}

			// Token: 0x06003978 RID: 14712 RVA: 0x000E4FF8 File Offset: 0x000E31F8
			public void OnDuelPreparation(Team duelTeam)
			{
				if (!this.Started)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new DuelPreparationStartedForTheFirstTime(this._challengers[0].MissionPeer.GetNetworkPeer(), this._challengers[1].MissionPeer.GetNetworkPeer(), this.DuelAreaIndex));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
				}
				this.Started = false;
				this.DuelingTeam = duelTeam;
				this._winnerChallengerType = MissionMultiplayerDuel.DuelInfo.ChallengerType.None;
				for (int i = 0; i < 2; i++)
				{
					this._challengers[i].OnDuelPreparation(this.DuelingTeam);
					this._challengers[i].MissionPeer.GetComponent<DuelMissionRepresentative>().OnDuelPreparation(this._challengers[0].MissionPeer, this._challengers[1].MissionPeer);
				}
				this.Timer = MissionTime.Now + MissionTime.Seconds(3f);
			}

			// Token: 0x06003979 RID: 14713 RVA: 0x000E50E4 File Offset: 0x000E32E4
			public void OnDuelStarted()
			{
				this.Started = true;
				this.DuelingTeam.SetIsEnemyOf(this.DuelingTeam, true);
			}

			// Token: 0x0600397A RID: 14714 RVA: 0x000E50FF File Offset: 0x000E32FF
			public void OnDuelEnding()
			{
				this.Timer = MissionTime.Now + MissionTime.Seconds(2f);
			}

			// Token: 0x0600397B RID: 14715 RVA: 0x000E511C File Offset: 0x000E331C
			public void OnDuelEnded()
			{
				if (this.Started)
				{
					this.DuelingTeam.SetIsEnemyOf(this.DuelingTeam, false);
				}
				this.DecideRoundWinner();
				for (int i = 0; i < 2; i++)
				{
					this._challengers[i].OnDuelEnded();
					Agent agent = this._challengers[i].DuelingAgent ?? this._challengers[i].MissionPeer.ControlledAgent;
					if (this.ChallengeEnded && agent != null && agent.IsActive())
					{
						agent.FadeOut(true, false);
					}
					this._challengers[i].MissionPeer.HasSpawnedAgentVisuals = true;
				}
				for (int j = 0; j < 2; j++)
				{
					if (this._challengers[j].MountAgent != null && this._challengers[j].MountAgent.IsActive() && (this.ChallengeEnded || this._challengers[j].MountAgent.RiderAgent == null))
					{
						this._challengers[j].MountAgent.FadeOut(true, false);
					}
				}
			}

			// Token: 0x0600397C RID: 14716 RVA: 0x000E5234 File Offset: 0x000E3434
			public void OnAgentBuild(Agent agent)
			{
				for (int i = 0; i < 2; i++)
				{
					if (this._challengers[i].MissionPeer == agent.MissionPeer)
					{
						this._challengers[i].SetAgents(agent);
						return;
					}
				}
			}

			// Token: 0x0600397D RID: 14717 RVA: 0x000E527C File Offset: 0x000E347C
			public bool IsDuelStillValid(bool doNotCheckAgent = false)
			{
				for (int i = 0; i < 2; i++)
				{
					if (!this._challengers[i].MissionPeer.Peer.Communicator.IsConnectionActive || (!doNotCheckAgent && !this._challengers[i].MissionPeer.IsControlledAgentActive))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0600397E RID: 14718 RVA: 0x000E52D8 File Offset: 0x000E34D8
			public bool IsPeerInThisDuel(MissionPeer peer)
			{
				for (int i = 0; i < 2; i++)
				{
					if (this._challengers[i].MissionPeer == peer)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600397F RID: 14719 RVA: 0x000E5308 File Offset: 0x000E3508
			public void UpdateDuelAreaIndex(KeyValuePair<int, TroopType> duelAreaPair)
			{
				this.DuelAreaIndex = duelAreaPair.Key;
				this.DuelAreaTroopType = duelAreaPair.Value;
			}

			// Token: 0x04001CE6 RID: 7398
			private const float DuelStartCountdown = 3f;

			// Token: 0x04001CE7 RID: 7399
			private readonly MissionMultiplayerDuel.DuelInfo.Challenger[] _challengers;

			// Token: 0x04001CE8 RID: 7400
			private MissionMultiplayerDuel.DuelInfo.ChallengerType _winnerChallengerType = MissionMultiplayerDuel.DuelInfo.ChallengerType.None;

			// Token: 0x02000683 RID: 1667
			private enum ChallengerType
			{
				// Token: 0x04002180 RID: 8576
				None = -1,
				// Token: 0x04002181 RID: 8577
				Requester,
				// Token: 0x04002182 RID: 8578
				Requestee,
				// Token: 0x04002183 RID: 8579
				NumChallengerType
			}

			// Token: 0x02000684 RID: 1668
			private struct Challenger
			{
				// Token: 0x17000A39 RID: 2617
				// (get) Token: 0x06003DE5 RID: 15845 RVA: 0x000EDFA1 File Offset: 0x000EC1A1
				// (set) Token: 0x06003DE6 RID: 15846 RVA: 0x000EDFA9 File Offset: 0x000EC1A9
				public Agent DuelingAgent { get; private set; }

				// Token: 0x17000A3A RID: 2618
				// (get) Token: 0x06003DE7 RID: 15847 RVA: 0x000EDFB2 File Offset: 0x000EC1B2
				// (set) Token: 0x06003DE8 RID: 15848 RVA: 0x000EDFBA File Offset: 0x000EC1BA
				public Agent MountAgent { get; private set; }

				// Token: 0x17000A3B RID: 2619
				// (get) Token: 0x06003DE9 RID: 15849 RVA: 0x000EDFC3 File Offset: 0x000EC1C3
				// (set) Token: 0x06003DEA RID: 15850 RVA: 0x000EDFCB File Offset: 0x000EC1CB
				public int KillCountInDuel { get; private set; }

				// Token: 0x06003DEB RID: 15851 RVA: 0x000EDFD4 File Offset: 0x000EC1D4
				public Challenger(MissionPeer missionPeer)
				{
					this.MissionPeer = missionPeer;
					MissionPeer missionPeer2 = this.MissionPeer;
					this.NetworkPeer = ((missionPeer2 != null) ? missionPeer2.GetNetworkPeer() : null);
					this.DuelingAgent = null;
					this.MountAgent = null;
					this.KillCountInDuel = 0;
				}

				// Token: 0x06003DEC RID: 15852 RVA: 0x000EE00A File Offset: 0x000EC20A
				public void OnDuelPreparation(Team duelingTeam)
				{
					Agent controlledAgent = this.MissionPeer.ControlledAgent;
					if (controlledAgent != null)
					{
						controlledAgent.FadeOut(true, true);
					}
					this.MissionPeer.Team = duelingTeam;
					this.MissionPeer.HasSpawnedAgentVisuals = true;
				}

				// Token: 0x06003DED RID: 15853 RVA: 0x000EE03C File Offset: 0x000EC23C
				public void OnDuelEnded()
				{
					if (this.MissionPeer.Peer.Communicator.IsConnectionActive)
					{
						this.MissionPeer.Team = Mission.Current.AttackerTeam;
					}
				}

				// Token: 0x06003DEE RID: 15854 RVA: 0x000EE06C File Offset: 0x000EC26C
				public void IncreaseWinCount()
				{
					int killCountInDuel = this.KillCountInDuel;
					this.KillCountInDuel = killCountInDuel + 1;
				}

				// Token: 0x06003DEF RID: 15855 RVA: 0x000EE089 File Offset: 0x000EC289
				public void SetAgents(Agent agent)
				{
					this.DuelingAgent = agent;
					this.MountAgent = this.DuelingAgent.MountAgent;
				}

				// Token: 0x04002184 RID: 8580
				public readonly MissionPeer MissionPeer;

				// Token: 0x04002185 RID: 8581
				public readonly NetworkCommunicator NetworkPeer;
			}
		}

		// Token: 0x0200055B RID: 1371
		// (Invoke) Token: 0x06003981 RID: 14721
		public delegate void OnDuelEndedDelegate(MissionPeer winnerPeer, TroopType troopType);
	}
}
