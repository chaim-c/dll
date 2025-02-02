using System;
using System.Collections.Generic;
using System.Linq;
using NetworkMessages.FromClient;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000297 RID: 663
	public abstract class MissionLobbyComponent : MissionNetwork
	{
		// Token: 0x1400003C RID: 60
		// (add) Token: 0x0600228C RID: 8844 RVA: 0x0007D57C File Offset: 0x0007B77C
		// (remove) Token: 0x0600228D RID: 8845 RVA: 0x0007D5B4 File Offset: 0x0007B7B4
		public event Action OnPostMatchEnded;

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x0600228E RID: 8846 RVA: 0x0007D5EC File Offset: 0x0007B7EC
		// (remove) Token: 0x0600228F RID: 8847 RVA: 0x0007D624 File Offset: 0x0007B824
		public event Action OnCultureSelectionRequested;

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06002290 RID: 8848 RVA: 0x0007D65C File Offset: 0x0007B85C
		// (remove) Token: 0x06002291 RID: 8849 RVA: 0x0007D694 File Offset: 0x0007B894
		public event Action<string, bool> OnAdminMessageRequested;

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06002292 RID: 8850 RVA: 0x0007D6CC File Offset: 0x0007B8CC
		// (remove) Token: 0x06002293 RID: 8851 RVA: 0x0007D704 File Offset: 0x0007B904
		public event Action OnClassRestrictionChanged;

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06002294 RID: 8852 RVA: 0x0007D739 File Offset: 0x0007B939
		public bool IsInWarmup
		{
			get
			{
				return this._warmupComponent != null && this._warmupComponent.IsInWarmup;
			}
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x0007D750 File Offset: 0x0007B950
		static MissionLobbyComponent()
		{
			MissionLobbyComponent.AddLobbyComponentType(typeof(MissionBattleSchedulerClientComponent), LobbyMissionType.Matchmaker, false);
			MissionLobbyComponent.AddLobbyComponentType(typeof(MissionCustomGameClientComponent), LobbyMissionType.Custom, false);
			MissionLobbyComponent.AddLobbyComponentType(typeof(MissionCommunityClientComponent), LobbyMissionType.Community, false);
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x0007D7AE File Offset: 0x0007B9AE
		public static void AddLobbyComponentType(Type type, LobbyMissionType missionType, bool isSeverComponent)
		{
			MissionLobbyComponent._lobbyComponentTypes.Add(new Tuple<LobbyMissionType, bool>(missionType, isSeverComponent), type);
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x0007D7C4 File Offset: 0x0007B9C4
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this.CurrentMultiplayerState = MissionLobbyComponent.MultiplayerGameState.WaitingFirstPlayers;
			if (GameNetwork.IsServerOrRecorder)
			{
				MissionMultiplayerGameModeBase missionBehavior = Mission.Current.GetMissionBehavior<MissionMultiplayerGameModeBase>();
				if (missionBehavior != null && !missionBehavior.AllowCustomPlayerBanners())
				{
					this._usingFixedBanners = true;
					return;
				}
			}
			else
			{
				this._inactivityTimer = new Timer(base.Mission.CurrentTime, MissionLobbyComponent.InactivityThreshold, true);
			}
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x0007D820 File Offset: 0x0007BA20
		protected override void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegistererContainer registerer)
		{
			if (GameNetwork.IsClient)
			{
				registerer.RegisterBaseHandler<KillDeathCountChange>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventKillDeathCountChangeEvent));
				registerer.RegisterBaseHandler<MissionStateChange>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventMissionStateChange));
				registerer.RegisterBaseHandler<NetworkMessages.FromServer.CreateBanner>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventCreateBannerForPeer));
				registerer.RegisterBaseHandler<ChangeCulture>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventChangeCulture));
				registerer.RegisterBaseHandler<ChangeClassRestrictions>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventChangeClassRestrictions));
				return;
			}
			if (GameNetwork.IsClientOrReplay)
			{
				registerer.RegisterBaseHandler<ChangeCulture>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventChangeCulture));
				return;
			}
			if (GameNetwork.IsServer)
			{
				registerer.RegisterBaseHandler<NetworkMessages.FromClient.CreateBanner>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventCreateBannerForPeer));
				registerer.RegisterBaseHandler<RequestCultureChange>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventRequestCultureChange));
				registerer.RegisterBaseHandler<RequestChangeCharacterMessage>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventRequestChangeCharacterMessage));
			}
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x0007D8E6 File Offset: 0x0007BAE6
		protected override void OnUdpNetworkHandlerClose()
		{
			if (GameNetwork.IsServerOrRecorder || this._usingFixedBanners)
			{
				this._usingFixedBanners = false;
			}
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x0007D8FE File Offset: 0x0007BAFE
		public static MissionLobbyComponent CreateBehavior()
		{
			return (MissionLobbyComponent)Activator.CreateInstance(MissionLobbyComponent._lobbyComponentTypes[new Tuple<LobbyMissionType, bool>(BannerlordNetwork.LobbyMissionType, GameNetwork.IsDedicatedServer)]);
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x0007D923 File Offset: 0x0007BB23
		public virtual void QuitMission()
		{
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x0007D928 File Offset: 0x0007BB28
		public override void AfterStart()
		{
			base.Mission.MakeDefaultDeploymentPlans();
			this._missionScoreboardComponent = base.Mission.GetMissionBehavior<MissionScoreboardComponent>();
			this._gameMode = base.Mission.GetMissionBehavior<MissionMultiplayerGameModeBase>();
			this._timerComponent = base.Mission.GetMissionBehavior<MultiplayerTimerComponent>();
			this._roundComponent = base.Mission.GetMissionBehavior<IRoundComponent>();
			this._warmupComponent = base.Mission.GetMissionBehavior<MultiplayerWarmupComponent>();
			if (GameNetwork.IsClient)
			{
				base.Mission.GetMissionBehavior<MissionNetworkComponent>().OnMyClientSynchronized += this.OnMyClientSynchronized;
			}
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x0007D9B8 File Offset: 0x0007BBB8
		private void OnMyClientSynchronized()
		{
			base.Mission.GetMissionBehavior<MissionNetworkComponent>().OnMyClientSynchronized -= this.OnMyClientSynchronized;
			MissionPeer component = GameNetwork.MyPeer.GetComponent<MissionPeer>();
			if (component != null && component.Culture == null)
			{
				this.RequestCultureSelection();
			}
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x0007DA00 File Offset: 0x0007BC00
		public override void EarlyStart()
		{
			if (GameNetwork.IsServer)
			{
				base.Mission.SpectatorTeam = base.Mission.Teams.Add(BattleSideEnum.None, uint.MaxValue, uint.MaxValue, null, true, false, true);
			}
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x0007DA38 File Offset: 0x0007BC38
		public override void OnMissionTick(float dt)
		{
			if (GameNetwork.IsClient && this._inactivityTimer.Check(base.Mission.CurrentTime))
			{
				NetworkMain.GameClient.IsInCriticalState = (MBAPI.IMBNetwork.ElapsedTimeSinceLastUdpPacketArrived() > (double)MissionLobbyComponent.InactivityThreshold);
			}
			if (this.CurrentMultiplayerState == MissionLobbyComponent.MultiplayerGameState.WaitingFirstPlayers)
			{
				if (GameNetwork.IsServer && (this._warmupComponent == null || (!this._warmupComponent.IsInWarmup && this._timerComponent.CheckIfTimerPassed())))
				{
					int num = GameNetwork.NetworkPeers.Count((NetworkCommunicator x) => x.IsSynchronized);
					int num2 = MultiplayerOptions.OptionType.NumberOfBotsTeam1.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) + MultiplayerOptions.OptionType.NumberOfBotsTeam2.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
					int intValue = MultiplayerOptions.OptionType.MinNumberOfPlayersForMatchStart.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
					if (num + num2 >= intValue || MBCommon.CurrentGameType == MBCommon.GameType.MultiClientServer)
					{
						this.SetStatePlayingAsServer();
						return;
					}
				}
			}
			else if (this.CurrentMultiplayerState == MissionLobbyComponent.MultiplayerGameState.Playing)
			{
				bool flag = this._timerComponent.CheckIfTimerPassed();
				if (GameNetwork.IsServerOrRecorder && this._gameMode.RoundController == null && (flag || this._gameMode.CheckForMatchEnd()))
				{
					this._gameMode.GetWinnerTeam();
					this._gameMode.SpawnComponent.SpawningBehavior.RequestStopSpawnSession();
					this._gameMode.SpawnComponent.SpawningBehavior.SetRemainingAgentsInvulnerable();
					this.SetStateEndingAsServer();
				}
			}
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x0007DB8A File Offset: 0x0007BD8A
		protected override void OnUdpNetworkHandlerTick()
		{
			if (this.CurrentMultiplayerState == MissionLobbyComponent.MultiplayerGameState.Ending && this._timerComponent.CheckIfTimerPassed() && GameNetwork.IsServer)
			{
				this.EndGameAsServer();
			}
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x0007DBAF File Offset: 0x0007BDAF
		public override void OnRemoveBehavior()
		{
			NetworkCommunicator myPeer = GameNetwork.MyPeer;
			this.QuitMission();
			base.OnRemoveBehavior();
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x0007DBC3 File Offset: 0x0007BDC3
		public bool IsClassAvailable(FormationClass formationClass)
		{
			return !this._classRestrictions[(int)formationClass];
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x0007DBD0 File Offset: 0x0007BDD0
		public void ChangeClassRestriction(FormationClass classToChangeRestriction, bool value)
		{
			this._classRestrictions[(int)classToChangeRestriction] = value;
			Action onClassRestrictionChanged = this.OnClassRestrictionChanged;
			if (onClassRestrictionChanged == null)
			{
				return;
			}
			onClassRestrictionChanged();
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x0007DBEC File Offset: 0x0007BDEC
		private void HandleServerEventMissionStateChange(GameNetworkMessage baseMessage)
		{
			MissionStateChange missionStateChange = (MissionStateChange)baseMessage;
			this.CurrentMultiplayerState = missionStateChange.CurrentState;
			if (this.CurrentMultiplayerState != MissionLobbyComponent.MultiplayerGameState.WaitingFirstPlayers)
			{
				if (this.CurrentMultiplayerState == MissionLobbyComponent.MultiplayerGameState.Playing && this._warmupComponent != null)
				{
					base.Mission.RemoveMissionBehavior(this._warmupComponent);
					this._warmupComponent = null;
				}
				float duration = (this.CurrentMultiplayerState == MissionLobbyComponent.MultiplayerGameState.Playing) ? ((float)(MultiplayerOptions.OptionType.MapTimeLimit.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) * 60)) : MissionLobbyComponent.PostMatchWaitDuration;
				this._timerComponent.StartTimerAsClient(missionStateChange.StateStartTimeInSeconds, duration);
			}
			if (this.CurrentMultiplayerState == MissionLobbyComponent.MultiplayerGameState.Ending)
			{
				this.SetStateEndingAsClient();
			}
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x0007DC7C File Offset: 0x0007BE7C
		private void HandleServerEventKillDeathCountChangeEvent(GameNetworkMessage baseMessage)
		{
			KillDeathCountChange killDeathCountChange = (KillDeathCountChange)baseMessage;
			if (killDeathCountChange.VictimPeer != null)
			{
				MissionPeer component = killDeathCountChange.VictimPeer.GetComponent<MissionPeer>();
				NetworkCommunicator attackerPeer = killDeathCountChange.AttackerPeer;
				MissionPeer missionPeer = (attackerPeer != null) ? attackerPeer.GetComponent<MissionPeer>() : null;
				if (component != null)
				{
					component.KillCount = killDeathCountChange.KillCount;
					component.AssistCount = killDeathCountChange.AssistCount;
					component.DeathCount = killDeathCountChange.DeathCount;
					component.Score = killDeathCountChange.Score;
					if (missionPeer != null)
					{
						missionPeer.OnKillAnotherPeer(component);
					}
					if (killDeathCountChange.KillCount == 0 && killDeathCountChange.AssistCount == 0 && killDeathCountChange.DeathCount == 0 && killDeathCountChange.Score == 0)
					{
						component.ResetKillRegistry();
					}
				}
				if (this._missionScoreboardComponent != null)
				{
					this._missionScoreboardComponent.PlayerPropertiesChanged(killDeathCountChange.VictimPeer);
				}
			}
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x0007DD38 File Offset: 0x0007BF38
		private void HandleServerEventCreateBannerForPeer(GameNetworkMessage baseMessage)
		{
			NetworkMessages.FromServer.CreateBanner createBanner = (NetworkMessages.FromServer.CreateBanner)baseMessage;
			MissionPeer component = createBanner.Peer.GetComponent<MissionPeer>();
			if (component != null)
			{
				component.Peer.BannerCode = createBanner.BannerCode;
			}
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x0007DD6C File Offset: 0x0007BF6C
		private void HandleServerEventChangeCulture(GameNetworkMessage baseMessage)
		{
			ChangeCulture changeCulture = (ChangeCulture)baseMessage;
			MissionPeer component = changeCulture.Peer.GetComponent<MissionPeer>();
			if (component != null)
			{
				component.Culture = changeCulture.Culture;
			}
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x0007DD9C File Offset: 0x0007BF9C
		private void HandleServerEventChangeClassRestrictions(GameNetworkMessage baseMessage)
		{
			ChangeClassRestrictions changeClassRestrictions = (ChangeClassRestrictions)baseMessage;
			this.ChangeClassRestriction(changeClassRestrictions.ClassToChangeRestriction, changeClassRestrictions.NewValue);
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x0007DDC4 File Offset: 0x0007BFC4
		private bool HandleClientEventRequestCultureChange(NetworkCommunicator peer, GameNetworkMessage baseMessage)
		{
			RequestCultureChange requestCultureChange = (RequestCultureChange)baseMessage;
			MissionPeer component = peer.GetComponent<MissionPeer>();
			if (component != null && this._gameMode.CheckIfPlayerCanDespawn(component))
			{
				component.Culture = requestCultureChange.Culture;
				this.DespawnPlayer(component);
			}
			return true;
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x0007DE04 File Offset: 0x0007C004
		private bool HandleClientEventCreateBannerForPeer(NetworkCommunicator peer, GameNetworkMessage baseMessage)
		{
			NetworkMessages.FromClient.CreateBanner createBanner = (NetworkMessages.FromClient.CreateBanner)baseMessage;
			MissionMultiplayerGameModeBase missionBehavior = Mission.Current.GetMissionBehavior<MissionMultiplayerGameModeBase>();
			if (missionBehavior == null || !missionBehavior.AllowCustomPlayerBanners())
			{
				return false;
			}
			MissionPeer component = peer.GetComponent<MissionPeer>();
			if (component == null)
			{
				return false;
			}
			component.Peer.BannerCode = createBanner.BannerCode;
			MissionLobbyComponent.SyncBannersToAllClients(createBanner.BannerCode, component.GetNetworkPeer());
			return true;
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x0007DE60 File Offset: 0x0007C060
		private bool HandleClientEventRequestChangeCharacterMessage(NetworkCommunicator peer, GameNetworkMessage baseMessage)
		{
			MissionPeer component = ((RequestChangeCharacterMessage)baseMessage).NetworkPeer.GetComponent<MissionPeer>();
			if (component != null && this._gameMode.CheckIfPlayerCanDespawn(component))
			{
				this.DespawnPlayer(component);
			}
			return true;
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x0007DE97 File Offset: 0x0007C097
		private static void SyncBannersToAllClients(string bannerCode, NetworkCommunicator ownerPeer)
		{
			GameNetwork.BeginBroadcastModuleEvent();
			GameNetwork.WriteMessage(new NetworkMessages.FromServer.CreateBanner(ownerPeer, bannerCode));
			GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.ExcludeTargetPlayer, ownerPeer);
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x0007DEB1 File Offset: 0x0007C0B1
		protected override void HandleNewClientConnect(PlayerConnectionInfo clientConnectionInfo)
		{
			base.HandleNewClientConnect(clientConnectionInfo);
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x0007DEBA File Offset: 0x0007C0BA
		protected override void HandleLateNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
			if (!networkPeer.IsServerPeer)
			{
				this.SendExistingObjectsToPeer(networkPeer);
			}
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x0007DECC File Offset: 0x0007C0CC
		private void SendExistingObjectsToPeer(NetworkCommunicator peer)
		{
			long stateStartTimeInTicks = 0L;
			if (this.CurrentMultiplayerState != MissionLobbyComponent.MultiplayerGameState.WaitingFirstPlayers)
			{
				stateStartTimeInTicks = this._timerComponent.GetCurrentTimerStartTime().NumberOfTicks;
			}
			GameNetwork.BeginModuleEventAsServer(peer);
			GameNetwork.WriteMessage(new MissionStateChange(this.CurrentMultiplayerState, stateStartTimeInTicks));
			GameNetwork.EndModuleEventAsServer();
			this.SendPeerInformationsToPeer(peer);
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x0007DF1C File Offset: 0x0007C11C
		private void SendPeerInformationsToPeer(NetworkCommunicator peer)
		{
			foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeersIncludingDisconnectedPeers)
			{
				bool flag = networkCommunicator.VirtualPlayer != GameNetwork.VirtualPlayers[networkCommunicator.VirtualPlayer.Index];
				if (flag || networkCommunicator.IsSynchronized || networkCommunicator.JustReconnecting)
				{
					MissionPeer component = networkCommunicator.GetComponent<MissionPeer>();
					if (component != null)
					{
						GameNetwork.BeginModuleEventAsServer(peer);
						GameNetwork.WriteMessage(new KillDeathCountChange(component.GetNetworkPeer(), null, component.KillCount, component.AssistCount, component.DeathCount, component.Score));
						GameNetwork.EndModuleEventAsServer();
						if (component.BotsUnderControlAlive != 0 || component.BotsUnderControlTotal != 0)
						{
							GameNetwork.BeginModuleEventAsServer(peer);
							GameNetwork.WriteMessage(new BotsControlledChange(component.GetNetworkPeer(), component.BotsUnderControlAlive, component.BotsUnderControlTotal));
							GameNetwork.EndModuleEventAsServer();
						}
					}
					else
					{
						Debug.Print(">#< SendPeerInformationsToPeer MissionPeer is null.", 0, Debug.DebugColor.BrightWhite, 17179869184UL);
					}
				}
				else
				{
					Debug.Print(string.Concat(new string[]
					{
						">#< Can't send the info of ",
						networkCommunicator.UserName,
						" to ",
						peer.UserName,
						"."
					}), 0, Debug.DebugColor.BrightWhite, 17179869184UL);
					Debug.Print(string.Format("isDisconnectedPeer: {0}", flag), 0, Debug.DebugColor.BrightWhite, 17179869184UL);
					Debug.Print(string.Format("networkPeer.IsSynchronized: {0}", networkCommunicator.IsSynchronized), 0, Debug.DebugColor.BrightWhite, 17179869184UL);
					Debug.Print(string.Format("peer == networkPeer: {0}", peer == networkCommunicator), 0, Debug.DebugColor.BrightWhite, 17179869184UL);
					Debug.Print(string.Format("networkPeer.JustReconnecting: {0}", networkCommunicator.JustReconnecting), 0, Debug.DebugColor.BrightWhite, 17179869184UL);
				}
			}
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x0007E118 File Offset: 0x0007C318
		public void DespawnPlayer(MissionPeer missionPeer)
		{
			if (missionPeer.ControlledAgent != null && missionPeer.ControlledAgent.IsActive())
			{
				Agent controlledAgent = missionPeer.ControlledAgent;
				if (controlledAgent == null)
				{
					return;
				}
				controlledAgent.FadeOut(true, true);
			}
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x0007E141 File Offset: 0x0007C341
		public override void OnScoreHit(Agent affectedAgent, Agent affectorAgent, WeaponComponentData attackerWeapon, bool isBlocked, bool isSiegeEngineHit, in Blow blow, in AttackCollisionData collisionData, float damagedHp, float hitDistance, float shotDifficulty)
		{
			if (affectorAgent != null && GameNetwork.IsServer && !isBlocked && affectorAgent != affectedAgent && affectorAgent.MissionPeer != null && damagedHp > 0f)
			{
				affectedAgent.AddHitter(affectorAgent.MissionPeer, damagedHp, affectorAgent.IsFriendOf(affectedAgent));
			}
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x0007E17C File Offset: 0x0007C37C
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow)
		{
			base.OnAgentRemoved(affectedAgent, affectorAgent, agentState, killingBlow);
			if (GameNetwork.IsServer)
			{
				if (this.CurrentMultiplayerState == MissionLobbyComponent.MultiplayerGameState.Ending)
				{
					return;
				}
				if ((agentState == AgentState.Killed || agentState == AgentState.Unconscious || agentState == AgentState.Routed) && affectedAgent != null && affectedAgent.IsHuman)
				{
					MissionPeer affectorPeer = ((affectorAgent != null) ? affectorAgent.MissionPeer : null) ?? ((affectorAgent != null) ? affectorAgent.OwningAgentMissionPeer : null);
					MissionPeer assistorPeer = this.RemoveHittersAndGetAssistorPeer((affectorAgent != null) ? affectorAgent.MissionPeer : null, affectedAgent);
					if (affectedAgent.MissionPeer != null)
					{
						this.OnPlayerDies(affectedAgent.MissionPeer, affectorPeer, assistorPeer);
					}
					else
					{
						this.OnBotDies(affectedAgent, affectorPeer, assistorPeer);
					}
					if (affectorAgent != null && affectorAgent.IsHuman)
					{
						if (affectorAgent != affectedAgent)
						{
							if (affectorAgent.MissionPeer != null)
							{
								this.OnPlayerKills(affectorAgent.MissionPeer, affectedAgent, assistorPeer);
								return;
							}
							this.OnBotKills(affectorAgent, affectedAgent);
							return;
						}
						else if (affectorAgent.MissionPeer != null)
						{
							affectorAgent.MissionPeer.Score -= (int)((float)this._gameMode.GetScoreForKill(affectedAgent) * 1.5f);
							this._missionScoreboardComponent.PlayerPropertiesChanged(affectorAgent.MissionPeer.GetNetworkPeer());
							GameNetwork.BeginBroadcastModuleEvent();
							GameNetwork.WriteMessage(new KillDeathCountChange(affectorAgent.MissionPeer.GetNetworkPeer(), affectedAgent.MissionPeer.GetNetworkPeer(), affectorAgent.MissionPeer.KillCount, affectorAgent.MissionPeer.AssistCount, affectorAgent.MissionPeer.DeathCount, affectorAgent.MissionPeer.Score));
							GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
						}
					}
				}
			}
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x0007E2F0 File Offset: 0x0007C4F0
		public override void OnAgentBuild(Agent agent, Banner banner)
		{
			if (GameNetwork.IsServer)
			{
				if (agent.IsMount)
				{
					return;
				}
				if (agent.MissionPeer == null)
				{
					if (agent.OwningAgentMissionPeer != null)
					{
						MissionPeer owningAgentMissionPeer = agent.OwningAgentMissionPeer;
						int num = owningAgentMissionPeer.BotsUnderControlAlive;
						owningAgentMissionPeer.BotsUnderControlAlive = num + 1;
						MissionPeer owningAgentMissionPeer2 = agent.OwningAgentMissionPeer;
						num = owningAgentMissionPeer2.BotsUnderControlTotal;
						owningAgentMissionPeer2.BotsUnderControlTotal = num + 1;
						return;
					}
					this._missionScoreboardComponent.Sides[(int)agent.Team.Side].BotScores.AliveCount++;
				}
			}
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x0007E374 File Offset: 0x0007C574
		protected virtual void OnPlayerKills(MissionPeer killerPeer, Agent killedAgent, MissionPeer assistorPeer)
		{
			if (killedAgent.MissionPeer == null)
			{
				NetworkCommunicator networkCommunicator = GameNetwork.NetworkPeers.SingleOrDefault((NetworkCommunicator x) => x.GetComponent<MissionPeer>() != null && x.GetComponent<MissionPeer>().ControlledFormation != null && x.GetComponent<MissionPeer>().ControlledFormation == killedAgent.Formation);
				if (networkCommunicator != null)
				{
					MissionPeer component = networkCommunicator.GetComponent<MissionPeer>();
					killerPeer.OnKillAnotherPeer(component);
				}
			}
			else
			{
				killerPeer.OnKillAnotherPeer(killedAgent.MissionPeer);
			}
			if (killerPeer.Team.IsEnemyOf(killedAgent.Team))
			{
				killerPeer.Score += this._gameMode.GetScoreForKill(killedAgent);
				int killCount = killerPeer.KillCount;
				killerPeer.KillCount = killCount + 1;
			}
			else
			{
				killerPeer.Score -= (int)((float)this._gameMode.GetScoreForKill(killedAgent) * 1.5f);
				int killCount = killerPeer.KillCount;
				killerPeer.KillCount = killCount - 1;
			}
			this._missionScoreboardComponent.PlayerPropertiesChanged(killerPeer.GetNetworkPeer());
			GameNetwork.BeginBroadcastModuleEvent();
			GameNetwork.WriteMessage(new KillDeathCountChange(killerPeer.GetNetworkPeer(), null, killerPeer.KillCount, killerPeer.AssistCount, killerPeer.DeathCount, killerPeer.Score));
			GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x0007E49C File Offset: 0x0007C69C
		protected virtual void OnPlayerDies(MissionPeer peer, MissionPeer affectorPeer, MissionPeer assistorPeer)
		{
			if (assistorPeer != null)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new KillDeathCountChange(assistorPeer.GetNetworkPeer(), null, assistorPeer.KillCount, assistorPeer.AssistCount, assistorPeer.DeathCount, assistorPeer.Score));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
			}
			int deathCount = peer.DeathCount;
			peer.DeathCount = deathCount + 1;
			peer.SpawnTimer.Reset(Mission.Current.CurrentTime, (float)MissionLobbyComponent.GetSpawnPeriodDurationForPeer(peer));
			peer.WantsToSpawnAsBot = false;
			peer.HasSpawnTimerExpired = false;
			this._missionScoreboardComponent.PlayerPropertiesChanged(peer.GetNetworkPeer());
			GameNetwork.BeginBroadcastModuleEvent();
			GameNetwork.WriteMessage(new KillDeathCountChange(peer.GetNetworkPeer(), (affectorPeer != null) ? affectorPeer.GetNetworkPeer() : null, peer.KillCount, peer.AssistCount, peer.DeathCount, peer.Score));
			GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x0007E56C File Offset: 0x0007C76C
		protected virtual void OnBotKills(Agent botAgent, Agent killedAgent)
		{
			Agent botAgent2 = botAgent;
			if (((botAgent2 != null) ? botAgent2.Team : null) != null)
			{
				Formation formation = botAgent.Formation;
				if (((formation != null) ? formation.PlayerOwner : null) != null)
				{
					NetworkCommunicator networkCommunicator = GameNetwork.NetworkPeers.SingleOrDefault((NetworkCommunicator x) => x.GetComponent<MissionPeer>() != null && x.GetComponent<MissionPeer>().ControlledFormation == botAgent.Formation);
					if (networkCommunicator != null)
					{
						MissionPeer component = networkCommunicator.GetComponent<MissionPeer>();
						MissionPeer missionPeer = killedAgent.MissionPeer;
						NetworkCommunicator networkCommunicator2 = (missionPeer != null) ? missionPeer.GetNetworkPeer() : null;
						if (killedAgent.MissionPeer == null)
						{
							NetworkCommunicator networkCommunicator3 = GameNetwork.NetworkPeers.SingleOrDefault((NetworkCommunicator x) => x.GetComponent<MissionPeer>() != null && x.GetComponent<MissionPeer>().ControlledFormation == killedAgent.Formation);
							if (networkCommunicator3 != null)
							{
								NetworkCommunicator networkPeer = networkCommunicator3;
								component.OnKillAnotherPeer(networkPeer.GetComponent<MissionPeer>());
							}
						}
						else
						{
							component.OnKillAnotherPeer(killedAgent.MissionPeer);
						}
						if (botAgent.Team.IsEnemyOf(killedAgent.Team))
						{
							MissionPeer missionPeer2 = component;
							int killCount = missionPeer2.KillCount;
							missionPeer2.KillCount = killCount + 1;
							component.Score += this._gameMode.GetScoreForKill(killedAgent);
						}
						else
						{
							MissionPeer missionPeer3 = component;
							int killCount = missionPeer3.KillCount;
							missionPeer3.KillCount = killCount - 1;
							component.Score -= (int)((float)this._gameMode.GetScoreForKill(killedAgent) * 1.5f);
						}
						this._missionScoreboardComponent.PlayerPropertiesChanged(networkCommunicator);
						GameNetwork.BeginBroadcastModuleEvent();
						GameNetwork.WriteMessage(new KillDeathCountChange(networkCommunicator, null, component.KillCount, component.AssistCount, component.DeathCount, component.Score));
						GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
					}
				}
				else
				{
					MissionScoreboardComponent.MissionScoreboardSide sideSafe = this._missionScoreboardComponent.GetSideSafe(botAgent.Team.Side);
					BotData botScores = sideSafe.BotScores;
					if (botAgent.Team.IsEnemyOf(killedAgent.Team))
					{
						botScores.KillCount++;
					}
					else
					{
						botScores.KillCount--;
					}
					this._missionScoreboardComponent.BotPropertiesChanged(sideSafe.Side);
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new BotData(sideSafe.Side, botScores.KillCount, botScores.AssistCount, botScores.DeathCount, botScores.AliveCount));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
				}
				this._missionScoreboardComponent.BotPropertiesChanged(botAgent.Team.Side);
			}
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x0007E7D8 File Offset: 0x0007C9D8
		protected virtual void OnBotDies(Agent botAgent, MissionPeer affectorPeer, MissionPeer assistorPeer)
		{
			if (assistorPeer != null)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new KillDeathCountChange(assistorPeer.GetNetworkPeer(), (affectorPeer != null) ? affectorPeer.GetNetworkPeer() : null, assistorPeer.KillCount, assistorPeer.AssistCount, assistorPeer.DeathCount, assistorPeer.Score));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
			}
			if (botAgent != null)
			{
				Formation formation = botAgent.Formation;
				if (((formation != null) ? formation.PlayerOwner : null) != null)
				{
					NetworkCommunicator networkCommunicator = GameNetwork.NetworkPeers.SingleOrDefault((NetworkCommunicator x) => x.GetComponent<MissionPeer>() != null && x.GetComponent<MissionPeer>().ControlledFormation == botAgent.Formation);
					if (networkCommunicator != null)
					{
						MissionPeer component = networkCommunicator.GetComponent<MissionPeer>();
						MissionPeer missionPeer = component;
						int num = missionPeer.DeathCount;
						missionPeer.DeathCount = num + 1;
						MissionPeer missionPeer2 = component;
						num = missionPeer2.BotsUnderControlAlive;
						missionPeer2.BotsUnderControlAlive = num - 1;
						this._missionScoreboardComponent.PlayerPropertiesChanged(networkCommunicator);
						GameNetwork.BeginBroadcastModuleEvent();
						GameNetwork.WriteMessage(new KillDeathCountChange(networkCommunicator, (affectorPeer != null) ? affectorPeer.GetNetworkPeer() : null, component.KillCount, component.AssistCount, component.DeathCount, component.Score));
						GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
						GameNetwork.BeginBroadcastModuleEvent();
						GameNetwork.WriteMessage(new BotsControlledChange(networkCommunicator, component.BotsUnderControlAlive, component.BotsUnderControlTotal));
						GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
					}
				}
				else
				{
					MissionScoreboardComponent.MissionScoreboardSide sideSafe = this._missionScoreboardComponent.GetSideSafe(botAgent.Team.Side);
					BotData botScores = sideSafe.BotScores;
					botScores.DeathCount++;
					botScores.AliveCount--;
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new BotData(sideSafe.Side, botScores.KillCount, botScores.AssistCount, botScores.DeathCount, botScores.AliveCount));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
				}
				this._missionScoreboardComponent.BotPropertiesChanged(botAgent.Team.Side);
			}
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x0007E9A0 File Offset: 0x0007CBA0
		public override void OnClearScene()
		{
			foreach (NetworkCommunicator networkPeer in GameNetwork.NetworkPeersIncludingDisconnectedPeers)
			{
				MissionPeer component = networkPeer.GetComponent<MissionPeer>();
				if (component != null)
				{
					component.BotsUnderControlAlive = 0;
					component.BotsUnderControlTotal = 0;
					component.ControlledFormation = null;
				}
			}
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x0007EA04 File Offset: 0x0007CC04
		public static int GetSpawnPeriodDurationForPeer(MissionPeer peer)
		{
			return Mission.Current.GetMissionBehavior<SpawnComponent>().GetMaximumReSpawnPeriodForPeer(peer);
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x0007EA18 File Offset: 0x0007CC18
		public virtual void SetStateEndingAsServer()
		{
			this.CurrentMultiplayerState = MissionLobbyComponent.MultiplayerGameState.Ending;
			MBDebug.Print("Multiplayer game mission ending", 0, Debug.DebugColor.White, 17592186044416UL);
			this._timerComponent.StartTimerAsServer(MissionLobbyComponent.PostMatchWaitDuration);
			GameNetwork.BeginBroadcastModuleEvent();
			GameNetwork.WriteMessage(new MissionStateChange(this.CurrentMultiplayerState, this._timerComponent.GetCurrentTimerStartTime().NumberOfTicks));
			GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
			Debug.Print(string.Format("Current multiplayer state sent to clients: {0}", this.CurrentMultiplayerState), 0, Debug.DebugColor.White, 17592186044416UL);
			Action onPostMatchEnded = this.OnPostMatchEnded;
			if (onPostMatchEnded == null)
			{
				return;
			}
			onPostMatchEnded();
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x0007EAB8 File Offset: 0x0007CCB8
		private void SetStatePlayingAsServer()
		{
			this._warmupComponent = null;
			this.CurrentMultiplayerState = MissionLobbyComponent.MultiplayerGameState.Playing;
			this._timerComponent.StartTimerAsServer((float)(MultiplayerOptions.OptionType.MapTimeLimit.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) * 60));
			GameNetwork.BeginBroadcastModuleEvent();
			GameNetwork.WriteMessage(new MissionStateChange(this.CurrentMultiplayerState, this._timerComponent.GetCurrentTimerStartTime().NumberOfTicks));
			GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x0007EB19 File Offset: 0x0007CD19
		protected virtual void EndGameAsServer()
		{
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x0007EB1C File Offset: 0x0007CD1C
		private MissionPeer RemoveHittersAndGetAssistorPeer(MissionPeer killerPeer, Agent killedAgent)
		{
			Agent.Hitter assistingHitter = killedAgent.GetAssistingHitter(killerPeer);
			if (((assistingHitter != null) ? assistingHitter.HitterPeer : null) != null)
			{
				if (!assistingHitter.IsFriendlyHit)
				{
					MissionPeer hitterPeer = assistingHitter.HitterPeer;
					int assistCount = hitterPeer.AssistCount;
					hitterPeer.AssistCount = assistCount + 1;
				}
				else
				{
					MissionPeer hitterPeer2 = assistingHitter.HitterPeer;
					int assistCount = hitterPeer2.AssistCount;
					hitterPeer2.AssistCount = assistCount - 1;
				}
			}
			if (assistingHitter == null)
			{
				return null;
			}
			return assistingHitter.HitterPeer;
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x0007EB7E File Offset: 0x0007CD7E
		private void SetStateEndingAsClient()
		{
			Action onPostMatchEnded = this.OnPostMatchEnded;
			if (onPostMatchEnded == null)
			{
				return;
			}
			onPostMatchEnded();
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x0007EB90 File Offset: 0x0007CD90
		public void RequestCultureSelection()
		{
			Action onCultureSelectionRequested = this.OnCultureSelectionRequested;
			if (onCultureSelectionRequested == null)
			{
				return;
			}
			onCultureSelectionRequested();
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x0007EBA2 File Offset: 0x0007CDA2
		public void RequestAdminMessage(string message, bool isBroadcast)
		{
			Action<string, bool> onAdminMessageRequested = this.OnAdminMessageRequested;
			if (onAdminMessageRequested == null)
			{
				return;
			}
			onAdminMessageRequested(message, isBroadcast);
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x0007EBB8 File Offset: 0x0007CDB8
		public void RequestTroopSelection()
		{
			if (GameNetwork.IsClient)
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new RequestChangeCharacterMessage(GameNetwork.MyPeer));
				GameNetwork.EndModuleEventAsClient();
				return;
			}
			if (GameNetwork.IsServer)
			{
				MissionPeer component = GameNetwork.MyPeer.GetComponent<MissionPeer>();
				if (component != null && this._gameMode.CheckIfPlayerCanDespawn(component))
				{
					this.DespawnPlayer(component);
				}
			}
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x0007EC10 File Offset: 0x0007CE10
		public void OnCultureSelected(BasicCultureObject culture)
		{
			if (GameNetwork.IsClient)
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new RequestCultureChange(culture));
				GameNetwork.EndModuleEventAsClient();
				return;
			}
			if (GameNetwork.IsServer)
			{
				MissionPeer component = GameNetwork.MyPeer.GetComponent<MissionPeer>();
				if (component != null && this._gameMode.CheckIfPlayerCanDespawn(component))
				{
					component.Culture = culture;
					this.DespawnPlayer(component);
				}
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x060022C4 RID: 8900 RVA: 0x0007EC6B File Offset: 0x0007CE6B
		// (set) Token: 0x060022C5 RID: 8901 RVA: 0x0007EC73 File Offset: 0x0007CE73
		public MultiplayerGameType MissionType { get; set; }

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x060022C6 RID: 8902 RVA: 0x0007EC7C File Offset: 0x0007CE7C
		// (set) Token: 0x060022C7 RID: 8903 RVA: 0x0007EC84 File Offset: 0x0007CE84
		public MissionLobbyComponent.MultiplayerGameState CurrentMultiplayerState
		{
			get
			{
				return this._currentMultiplayerState;
			}
			private set
			{
				if (this._currentMultiplayerState != value)
				{
					this._currentMultiplayerState = value;
					Action<MissionLobbyComponent.MultiplayerGameState> currentMultiplayerStateChanged = this.CurrentMultiplayerStateChanged;
					if (currentMultiplayerStateChanged == null)
					{
						return;
					}
					currentMultiplayerStateChanged(value);
				}
			}
		}

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x060022C8 RID: 8904 RVA: 0x0007ECA8 File Offset: 0x0007CEA8
		// (remove) Token: 0x060022C9 RID: 8905 RVA: 0x0007ECE0 File Offset: 0x0007CEE0
		public event Action<MissionLobbyComponent.MultiplayerGameState> CurrentMultiplayerStateChanged;

		// Token: 0x060022CA RID: 8906 RVA: 0x0007ED15 File Offset: 0x0007CF15
		public int GetRandomFaceSeedForCharacter(BasicCharacterObject character, int addition = 0)
		{
			IRoundComponent roundComponent = this._roundComponent;
			return character.GetDefaultFaceSeed(addition + ((roundComponent != null) ? roundComponent.RoundCount : 0)) % 2000;
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x0007ED38 File Offset: 0x0007CF38
		[CommandLineFunctionality.CommandLineArgumentFunction("kill_player", "mp_host")]
		public static string MPHostChangeParam(List<string> strings)
		{
			if (Mission.Current == null)
			{
				return "kill_player can only be called within a mission.";
			}
			if (!GameNetwork.IsServer)
			{
				return "kill_player can only be called by the server.";
			}
			if (strings == null || strings.Count == 0)
			{
				return "usage: kill_player {UserName}.";
			}
			foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
			{
				if (networkCommunicator.UserName == strings[0] && networkCommunicator.ControlledAgent != null)
				{
					Mission.Current.KillAgentCheat(networkCommunicator.ControlledAgent);
					return "Success.";
				}
			}
			return "Could not find the player " + strings[0] + " or the agent.";
		}

		// Token: 0x04000CDE RID: 3294
		private static readonly float InactivityThreshold = 2f;

		// Token: 0x04000CDF RID: 3295
		public static readonly float PostMatchWaitDuration = 15f;

		// Token: 0x04000CE2 RID: 3298
		private bool[] _classRestrictions = new bool[8];

		// Token: 0x04000CE5 RID: 3301
		private MissionScoreboardComponent _missionScoreboardComponent;

		// Token: 0x04000CE6 RID: 3302
		private MissionMultiplayerGameModeBase _gameMode;

		// Token: 0x04000CE7 RID: 3303
		private MultiplayerTimerComponent _timerComponent;

		// Token: 0x04000CE8 RID: 3304
		private IRoundComponent _roundComponent;

		// Token: 0x04000CE9 RID: 3305
		private Timer _inactivityTimer;

		// Token: 0x04000CEA RID: 3306
		private MultiplayerWarmupComponent _warmupComponent;

		// Token: 0x04000CEB RID: 3307
		private static readonly Dictionary<Tuple<LobbyMissionType, bool>, Type> _lobbyComponentTypes = new Dictionary<Tuple<LobbyMissionType, bool>, Type>();

		// Token: 0x04000CEC RID: 3308
		private bool _usingFixedBanners;

		// Token: 0x04000CEE RID: 3310
		private MissionLobbyComponent.MultiplayerGameState _currentMultiplayerState;

		// Token: 0x02000542 RID: 1346
		public enum MultiplayerGameState
		{
			// Token: 0x04001CBC RID: 7356
			WaitingFirstPlayers,
			// Token: 0x04001CBD RID: 7357
			Playing,
			// Token: 0x04001CBE RID: 7358
			Ending
		}
	}
}
