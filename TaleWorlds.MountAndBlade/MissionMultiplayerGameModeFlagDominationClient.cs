using System;
using System.Collections.Generic;
using System.Linq;
using NetworkMessages.FromClient;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.MissionRepresentatives;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.MountAndBlade.Objects;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002A0 RID: 672
	public class MissionMultiplayerGameModeFlagDominationClient : MissionMultiplayerGameModeBaseClient, ICommanderInfo, IMissionBehavior
	{
		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x060023E5 RID: 9189 RVA: 0x00084D02 File Offset: 0x00082F02
		public override bool IsGameModeUsingGold
		{
			get
			{
				return this.GameType != MultiplayerGameType.Captain;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x060023E6 RID: 9190 RVA: 0x00084D10 File Offset: 0x00082F10
		public override bool IsGameModeTactical
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x060023E7 RID: 9191 RVA: 0x00084D13 File Offset: 0x00082F13
		public override bool IsGameModeUsingRoundCountdown
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x060023E8 RID: 9192 RVA: 0x00084D16 File Offset: 0x00082F16
		public override MultiplayerGameType GameType
		{
			get
			{
				return this._currentGameType;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x060023E9 RID: 9193 RVA: 0x00084D1E File Offset: 0x00082F1E
		public override bool IsGameModeUsingCasualGold
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x060023EA RID: 9194 RVA: 0x00084D24 File Offset: 0x00082F24
		// (remove) Token: 0x060023EB RID: 9195 RVA: 0x00084D5C File Offset: 0x00082F5C
		public event Action<NetworkCommunicator> OnBotsControlledChangedEvent;

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x060023EC RID: 9196 RVA: 0x00084D94 File Offset: 0x00082F94
		// (remove) Token: 0x060023ED RID: 9197 RVA: 0x00084DCC File Offset: 0x00082FCC
		public event Action<BattleSideEnum, float> OnTeamPowerChangedEvent;

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x060023EE RID: 9198 RVA: 0x00084E04 File Offset: 0x00083004
		// (remove) Token: 0x060023EF RID: 9199 RVA: 0x00084E3C File Offset: 0x0008303C
		public event Action<BattleSideEnum, float> OnMoraleChangedEvent;

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x060023F0 RID: 9200 RVA: 0x00084E74 File Offset: 0x00083074
		// (remove) Token: 0x060023F1 RID: 9201 RVA: 0x00084EAC File Offset: 0x000830AC
		public event Action OnFlagNumberChangedEvent;

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x060023F2 RID: 9202 RVA: 0x00084EE4 File Offset: 0x000830E4
		// (remove) Token: 0x060023F3 RID: 9203 RVA: 0x00084F1C File Offset: 0x0008311C
		public event Action<FlagCapturePoint, Team> OnCapturePointOwnerChangedEvent;

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x060023F4 RID: 9204 RVA: 0x00084F54 File Offset: 0x00083154
		// (remove) Token: 0x060023F5 RID: 9205 RVA: 0x00084F8C File Offset: 0x0008318C
		public event Action<GoldGain> OnGoldGainEvent;

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x060023F6 RID: 9206 RVA: 0x00084FC1 File Offset: 0x000831C1
		// (set) Token: 0x060023F7 RID: 9207 RVA: 0x00084FC9 File Offset: 0x000831C9
		public IEnumerable<FlagCapturePoint> AllCapturePoints { get; private set; }

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x060023F8 RID: 9208 RVA: 0x00084FD2 File Offset: 0x000831D2
		public bool AreMoralesIndependent
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060023F9 RID: 9209 RVA: 0x00084FD8 File Offset: 0x000831D8
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._scoreboardComponent = Mission.Current.GetMissionBehavior<MissionScoreboardComponent>();
			if (MultiplayerOptions.OptionType.SingleSpawn.GetBoolValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions))
			{
				this._currentGameType = ((MultiplayerOptions.OptionType.NumberOfBotsPerFormation.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) > 0) ? MultiplayerGameType.Captain : MultiplayerGameType.Battle);
			}
			else
			{
				this._currentGameType = MultiplayerGameType.Skirmish;
			}
			this.ResetTeamPowers(1f);
			this._capturePointOwners = new Team[3];
			this.AllCapturePoints = Mission.Current.MissionObjects.FindAllWithType<FlagCapturePoint>();
			base.RoundComponent.OnPreparationEnded += this.OnPreparationEnded;
			base.MissionNetworkComponent.OnMyClientSynchronized += this.OnMyClientSynchronized;
		}

		// Token: 0x060023FA RID: 9210 RVA: 0x0008507D File Offset: 0x0008327D
		public override void OnRemoveBehavior()
		{
			base.OnRemoveBehavior();
			base.RoundComponent.OnPreparationEnded -= this.OnPreparationEnded;
			base.MissionNetworkComponent.OnMyClientSynchronized -= this.OnMyClientSynchronized;
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x000850B3 File Offset: 0x000832B3
		private void OnMyClientSynchronized()
		{
			this._myRepresentative = GameNetwork.MyPeer.GetComponent<FlagDominationMissionRepresentative>();
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x000850C5 File Offset: 0x000832C5
		public override void AfterStart()
		{
			Mission.Current.SetMissionMode(MissionMode.Battle, true);
		}

		// Token: 0x060023FD RID: 9213 RVA: 0x000850D4 File Offset: 0x000832D4
		protected override void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegistererContainer registerer)
		{
			if (GameNetwork.IsClient)
			{
				registerer.RegisterBaseHandler<BotsControlledChange>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventBotsControlledChangeEvent));
				registerer.RegisterBaseHandler<FlagDominationMoraleChangeMessage>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleMoraleChangedMessage));
				registerer.RegisterBaseHandler<SyncGoldsForSkirmish>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventUpdateGold));
				registerer.RegisterBaseHandler<FlagDominationFlagsRemovedMessage>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleFlagsRemovedMessage));
				registerer.RegisterBaseHandler<FlagDominationCapturePointMessage>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventPointCapturedMessage));
				registerer.RegisterBaseHandler<FormationWipedMessage>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventFormationWipedMessage));
				registerer.RegisterBaseHandler<GoldGain>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventPersonalGoldGain));
			}
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x00085168 File Offset: 0x00083368
		public void OnPreparationEnded()
		{
			this.AllCapturePoints = Mission.Current.MissionObjects.FindAllWithType<FlagCapturePoint>();
			Action onFlagNumberChangedEvent = this.OnFlagNumberChangedEvent;
			if (onFlagNumberChangedEvent != null)
			{
				onFlagNumberChangedEvent();
			}
			foreach (FlagCapturePoint arg in this.AllCapturePoints)
			{
				Action<FlagCapturePoint, Team> onCapturePointOwnerChangedEvent = this.OnCapturePointOwnerChangedEvent;
				if (onCapturePointOwnerChangedEvent != null)
				{
					onCapturePointOwnerChangedEvent(arg, null);
				}
			}
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x000851E8 File Offset: 0x000833E8
		public override SpectatorCameraTypes GetMissionCameraLockMode(bool lockedToMainPlayer)
		{
			SpectatorCameraTypes result = SpectatorCameraTypes.Invalid;
			MissionPeer missionPeer = GameNetwork.IsMyPeerReady ? GameNetwork.MyPeer.GetComponent<MissionPeer>() : null;
			if (!lockedToMainPlayer && missionPeer != null)
			{
				if (missionPeer.Team != base.Mission.SpectatorTeam)
				{
					if (this.GameType == MultiplayerGameType.Captain && base.IsRoundInProgress)
					{
						Formation controlledFormation = missionPeer.ControlledFormation;
						if (controlledFormation != null)
						{
							if (controlledFormation.HasUnitsWithCondition((Agent agent) => !agent.IsPlayerControlled && agent.IsActive()))
							{
								result = SpectatorCameraTypes.LockToPlayerFormation;
							}
						}
					}
				}
				else
				{
					result = SpectatorCameraTypes.Free;
				}
			}
			return result;
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x00085270 File Offset: 0x00083470
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow blow)
		{
			if (base.IsRoundInProgress && !affectedAgent.IsMount)
			{
				Team team = affectedAgent.Team;
				if (this.IsGameModeUsingGold)
				{
					this.UpdateTeamPowerBasedOnGold(team);
					return;
				}
				this.UpdateTeamPowerBasedOnTroopCount(team);
			}
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x000852AC File Offset: 0x000834AC
		public override void OnClearScene()
		{
			this._informedAboutFlagRemoval = false;
			this.AllCapturePoints = Mission.Current.MissionObjects.FindAllWithType<FlagCapturePoint>();
			foreach (FlagCapturePoint flagCapturePoint in this.AllCapturePoints)
			{
				this._capturePointOwners[flagCapturePoint.FlagIndex] = null;
			}
			this.ResetTeamPowers(1f);
			if (this._bellSoundEvent != null)
			{
				this._remainingTimeForBellSoundToStop = float.MinValue;
				this._bellSoundEvent.Stop();
				this._bellSoundEvent = null;
			}
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x0008534C File Offset: 0x0008354C
		protected override int GetWarningTimer()
		{
			int result = 0;
			if (base.IsRoundInProgress)
			{
				float num = -1f;
				switch (this.GameType)
				{
				case MultiplayerGameType.Battle:
					num = 210f;
					break;
				case MultiplayerGameType.Captain:
					num = 180f;
					break;
				case MultiplayerGameType.Skirmish:
					num = 120f;
					break;
				default:
					Debug.FailedAssert("A flag domination mode cannot be " + this.GameType + ".", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Multiplayer\\MissionNetworkLogics\\MultiplayerGameModeLogics\\ClientGameModeLogics\\MissionMultiplayerGameModeFlagDominationClient.cs", "GetWarningTimer", 207);
					break;
				}
				float num2 = (float)MultiplayerOptions.OptionType.RoundTimeLimit.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) - num;
				float num3 = num2 + 30f;
				if (base.RoundComponent.RemainingRoundTime <= num3 && base.RoundComponent.RemainingRoundTime > num2)
				{
					result = MathF.Ceiling(30f - (num3 - base.RoundComponent.RemainingRoundTime));
					if (!this._informedAboutFlagRemoval)
					{
						this._informedAboutFlagRemoval = true;
						base.NotificationsComponent.FlagsWillBeRemovedInXSeconds(30);
					}
				}
			}
			return result;
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x00085437 File Offset: 0x00083637
		public Team GetFlagOwner(FlagCapturePoint flag)
		{
			return this._capturePointOwners[flag.FlagIndex];
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x00085448 File Offset: 0x00083648
		private void HandleServerEventBotsControlledChangeEvent(GameNetworkMessage baseMessage)
		{
			BotsControlledChange botsControlledChange = (BotsControlledChange)baseMessage;
			MissionPeer component = botsControlledChange.Peer.GetComponent<MissionPeer>();
			this.OnBotsControlledChanged(component, botsControlledChange.AliveCount, botsControlledChange.TotalCount);
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x0008547C File Offset: 0x0008367C
		private void HandleMoraleChangedMessage(GameNetworkMessage baseMessage)
		{
			FlagDominationMoraleChangeMessage flagDominationMoraleChangeMessage = (FlagDominationMoraleChangeMessage)baseMessage;
			this.OnMoraleChanged(flagDominationMoraleChangeMessage.Morale);
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x0008549C File Offset: 0x0008369C
		private void HandleServerEventUpdateGold(GameNetworkMessage baseMessage)
		{
			SyncGoldsForSkirmish syncGoldsForSkirmish = (SyncGoldsForSkirmish)baseMessage;
			FlagDominationMissionRepresentative component = syncGoldsForSkirmish.VirtualPlayer.GetComponent<FlagDominationMissionRepresentative>();
			this.OnGoldAmountChangedForRepresentative(component, syncGoldsForSkirmish.GoldAmount);
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x000854C9 File Offset: 0x000836C9
		private void HandleFlagsRemovedMessage(GameNetworkMessage baseMessage)
		{
			this.OnNumberOfFlagsChanged();
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x000854D4 File Offset: 0x000836D4
		private void HandleServerEventPointCapturedMessage(GameNetworkMessage baseMessage)
		{
			FlagDominationCapturePointMessage flagDominationCapturePointMessage = (FlagDominationCapturePointMessage)baseMessage;
			foreach (FlagCapturePoint flagCapturePoint in this.AllCapturePoints)
			{
				if (flagCapturePoint.FlagIndex == flagDominationCapturePointMessage.FlagIndex)
				{
					this.OnCapturePointOwnerChanged(flagCapturePoint, Mission.MissionNetworkHelper.GetTeamFromTeamIndex(flagDominationCapturePointMessage.OwnerTeamIndex));
					break;
				}
			}
		}

		// Token: 0x06002409 RID: 9225 RVA: 0x00085544 File Offset: 0x00083744
		private void HandleServerEventFormationWipedMessage(GameNetworkMessage baseMessage)
		{
			MatrixFrame cameraFrame = Mission.Current.GetCameraFrame();
			Vec3 position = cameraFrame.origin + cameraFrame.rotation.u;
			MBSoundEvent.PlaySound(SoundEvent.GetEventIdFromString("event:/alerts/report/squad_wiped"), position);
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x00085584 File Offset: 0x00083784
		private void HandleServerEventPersonalGoldGain(GameNetworkMessage baseMessage)
		{
			GoldGain obj = (GoldGain)baseMessage;
			Action<GoldGain> onGoldGainEvent = this.OnGoldGainEvent;
			if (onGoldGainEvent == null)
			{
				return;
			}
			onGoldGainEvent(obj);
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x000855A9 File Offset: 0x000837A9
		public void OnTeamPowerChanged(BattleSideEnum teamSide, float power)
		{
			Action<BattleSideEnum, float> onTeamPowerChangedEvent = this.OnTeamPowerChangedEvent;
			if (onTeamPowerChangedEvent == null)
			{
				return;
			}
			onTeamPowerChangedEvent(teamSide, power);
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x000855C0 File Offset: 0x000837C0
		public void OnMoraleChanged(float morale)
		{
			for (int i = 0; i < 2; i++)
			{
				float num = (morale + 1f) / 2f;
				if (i == 0)
				{
					Action<BattleSideEnum, float> onMoraleChangedEvent = this.OnMoraleChangedEvent;
					if (onMoraleChangedEvent != null)
					{
						onMoraleChangedEvent(BattleSideEnum.Defender, 1f - num);
					}
				}
				else if (i == 1)
				{
					Action<BattleSideEnum, float> onMoraleChangedEvent2 = this.OnMoraleChangedEvent;
					if (onMoraleChangedEvent2 != null)
					{
						onMoraleChangedEvent2(BattleSideEnum.Attacker, num);
					}
				}
			}
			FlagDominationMissionRepresentative myRepresentative = this._myRepresentative;
			if (((myRepresentative != null) ? myRepresentative.MissionPeer.Team : null) != null && this._myRepresentative.MissionPeer.Team.Side != BattleSideEnum.None)
			{
				float num2 = MathF.Abs(morale);
				if (this._remainingTimeForBellSoundToStop < 0f)
				{
					if (num2 >= 0.6f && num2 < 1f)
					{
						this._remainingTimeForBellSoundToStop = float.MaxValue;
					}
					else
					{
						this._remainingTimeForBellSoundToStop = float.MinValue;
					}
					if (this._remainingTimeForBellSoundToStop > 0f)
					{
						BattleSideEnum side = this._myRepresentative.MissionPeer.Team.Side;
						if ((side == BattleSideEnum.Defender && morale >= 0.6f) || (side == BattleSideEnum.Attacker && morale <= -0.6f))
						{
							this._bellSoundEvent = SoundEvent.CreateEventFromString("event:/multiplayer/warning_bells_defender", base.Mission.Scene);
						}
						else
						{
							this._bellSoundEvent = SoundEvent.CreateEventFromString("event:/multiplayer/warning_bells_attacker", base.Mission.Scene);
						}
						MatrixFrame globalFrame = (from cp in this.AllCapturePoints
						where !cp.IsDeactivated
						select cp).GetRandomElementInefficiently<FlagCapturePoint>().GameEntity.GetGlobalFrame();
						this._bellSoundEvent.PlayInPosition(globalFrame.origin + globalFrame.rotation.u * 3f);
						return;
					}
				}
				else if (num2 >= 1f || num2 < 0.6f)
				{
					this._remainingTimeForBellSoundToStop = float.MinValue;
				}
			}
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x0008578C File Offset: 0x0008398C
		public override void OnGoldAmountChangedForRepresentative(MissionRepresentativeBase representative, int goldAmount)
		{
			if (representative != null)
			{
				MissionPeer component = representative.GetComponent<MissionPeer>();
				if (component != null)
				{
					representative.UpdateGold(goldAmount);
					this._scoreboardComponent.PlayerPropertiesChanged(component);
					if (this.IsGameModeUsingGold && base.IsRoundInProgress && component.Team != null && component.Team.Side != BattleSideEnum.None)
					{
						this.UpdateTeamPowerBasedOnGold(component.Team);
					}
				}
			}
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x000857EB File Offset: 0x000839EB
		public void OnNumberOfFlagsChanged()
		{
			Action onFlagNumberChangedEvent = this.OnFlagNumberChangedEvent;
			if (onFlagNumberChangedEvent == null)
			{
				return;
			}
			onFlagNumberChangedEvent();
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x000857FD File Offset: 0x000839FD
		public void OnBotsControlledChanged(MissionPeer missionPeer, int botAliveCount, int botTotalCount)
		{
			missionPeer.BotsUnderControlAlive = botAliveCount;
			missionPeer.BotsUnderControlTotal = botTotalCount;
			Action<NetworkCommunicator> onBotsControlledChangedEvent = this.OnBotsControlledChangedEvent;
			if (onBotsControlledChangedEvent == null)
			{
				return;
			}
			onBotsControlledChangedEvent(missionPeer.GetNetworkPeer());
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x00085824 File Offset: 0x00083A24
		public void OnCapturePointOwnerChanged(FlagCapturePoint flagCapturePoint, Team ownerTeam)
		{
			this._capturePointOwners[flagCapturePoint.FlagIndex] = ownerTeam;
			Action<FlagCapturePoint, Team> onCapturePointOwnerChangedEvent = this.OnCapturePointOwnerChangedEvent;
			if (onCapturePointOwnerChangedEvent != null)
			{
				onCapturePointOwnerChangedEvent(flagCapturePoint, ownerTeam);
			}
			if (this._myRepresentative != null && this._myRepresentative.MissionPeer.Team != null)
			{
				MatrixFrame cameraFrame = Mission.Current.GetCameraFrame();
				Vec3 position = cameraFrame.origin + cameraFrame.rotation.u;
				if (this._myRepresentative.MissionPeer.Team == ownerTeam)
				{
					MBSoundEvent.PlaySound(SoundEvent.GetEventIdFromString("event:/alerts/report/flag_captured"), position);
					return;
				}
				MBSoundEvent.PlaySound(SoundEvent.GetEventIdFromString("event:/alerts/report/flag_lost"), position);
			}
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x000858C4 File Offset: 0x00083AC4
		public void OnRequestForfeitSpawn()
		{
			if (GameNetwork.IsClient)
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new RequestForfeitSpawn());
				GameNetwork.EndModuleEventAsClient();
				return;
			}
			Mission.Current.GetMissionBehavior<MissionMultiplayerFlagDomination>().ForfeitSpawning(GameNetwork.MyPeer);
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x000858F6 File Offset: 0x00083AF6
		private void ResetTeamPowers(float value = 1f)
		{
			Action<BattleSideEnum, float> onTeamPowerChangedEvent = this.OnTeamPowerChangedEvent;
			if (onTeamPowerChangedEvent != null)
			{
				onTeamPowerChangedEvent(BattleSideEnum.Attacker, value);
			}
			Action<BattleSideEnum, float> onTeamPowerChangedEvent2 = this.OnTeamPowerChangedEvent;
			if (onTeamPowerChangedEvent2 == null)
			{
				return;
			}
			onTeamPowerChangedEvent2(BattleSideEnum.Defender, value);
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x00085920 File Offset: 0x00083B20
		private void UpdateTeamPowerBasedOnGold(Team team)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			foreach (NetworkCommunicator networkPeer in GameNetwork.NetworkPeers)
			{
				MissionPeer component = networkPeer.GetComponent<MissionPeer>();
				if (((component != null) ? component.Team : null) != null && component.Team.Side == team.Side)
				{
					int gold = component.GetComponent<FlagDominationMissionRepresentative>().Gold;
					if (gold >= 100)
					{
						num2 += gold;
					}
					if (component.ControlledAgent != null && component.ControlledAgent.IsActive())
					{
						MultiplayerClassDivisions.MPHeroClass mpheroClassForCharacter = MultiplayerClassDivisions.GetMPHeroClassForCharacter(component.ControlledAgent.Character);
						num2 += ((this._currentGameType == MultiplayerGameType.Battle) ? mpheroClassForCharacter.TroopBattleCost : mpheroClassForCharacter.TroopCost);
					}
					num++;
				}
			}
			if (this._currentGameType == MultiplayerGameType.Battle)
			{
				num3 = 120;
			}
			else
			{
				num3 = 300;
			}
			num += ((team.Side == BattleSideEnum.Attacker) ? MultiplayerOptions.OptionType.NumberOfBotsTeam1.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) : MultiplayerOptions.OptionType.NumberOfBotsTeam2.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions));
			using (List<Agent>.Enumerator enumerator2 = team.ActiveAgents.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.MissionPeer == null)
					{
						num2 += num3;
					}
				}
			}
			int num4 = num * num3;
			float num5 = (num4 == 0) ? 0f : ((float)num2 / (float)num4);
			num5 = MathF.Min(1f, num5);
			Action<BattleSideEnum, float> onTeamPowerChangedEvent = this.OnTeamPowerChangedEvent;
			if (onTeamPowerChangedEvent == null)
			{
				return;
			}
			onTeamPowerChangedEvent(team.Side, num5);
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x00085AB8 File Offset: 0x00083CB8
		private void UpdateTeamPowerBasedOnTroopCount(Team team)
		{
			int count = team.ActiveAgents.Count;
			int num = count + team.QuerySystem.DeathCount;
			float arg = (float)count / (float)num;
			Action<BattleSideEnum, float> onTeamPowerChangedEvent = this.OnTeamPowerChangedEvent;
			if (onTeamPowerChangedEvent == null)
			{
				return;
			}
			onTeamPowerChangedEvent(team.Side, arg);
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x00085AFC File Offset: 0x00083CFC
		public override List<CompassItemUpdateParams> GetCompassTargets()
		{
			List<CompassItemUpdateParams> list = new List<CompassItemUpdateParams>();
			if (!GameNetwork.IsMyPeerReady || !base.IsRoundInProgress)
			{
				return list;
			}
			MissionPeer component = GameNetwork.MyPeer.GetComponent<MissionPeer>();
			if (component == null || component.Team == null || component.Team.Side == BattleSideEnum.None)
			{
				return list;
			}
			foreach (FlagCapturePoint flagCapturePoint in from cp in this.AllCapturePoints
			where !cp.IsDeactivated
			select cp)
			{
				int targetType = 17 + flagCapturePoint.FlagIndex;
				list.Add(new CompassItemUpdateParams(flagCapturePoint, (TargetIconType)targetType, flagCapturePoint.Position, flagCapturePoint.GetFlagColor(), flagCapturePoint.GetFlagColor2()));
			}
			bool flag = true;
			foreach (NetworkCommunicator networkPeer in GameNetwork.NetworkPeers)
			{
				MissionPeer component2 = networkPeer.GetComponent<MissionPeer>();
				if (((component2 != null) ? component2.Team : null) != null && component2.Team.Side != BattleSideEnum.None)
				{
					bool flag2 = component2.ControlledFormation != null;
					if (!flag2)
					{
						flag = false;
					}
					if (flag || component2.Team == component.Team)
					{
						MultiplayerClassDivisions.MPHeroClass mpheroClassForPeer = MultiplayerClassDivisions.GetMPHeroClassForPeer(component2, false);
						if (flag2)
						{
							Formation controlledFormation = component2.ControlledFormation;
							if (controlledFormation.CountOfUnits != 0)
							{
								WorldPosition medianPosition = controlledFormation.QuerySystem.MedianPosition;
								Vec2 vec = controlledFormation.SmoothedAverageUnitPosition;
								if (!vec.IsValid)
								{
									vec = controlledFormation.QuerySystem.AveragePosition;
								}
								medianPosition.SetVec2(vec);
								BannerCode bannerCode = null;
								bool isAttacker = false;
								bool isAlly = false;
								if (controlledFormation.Team != null)
								{
									if (controlledFormation.Banner == null)
									{
										controlledFormation.Banner = new Banner(controlledFormation.BannerCode, controlledFormation.Team.Color, controlledFormation.Team.Color2);
									}
									isAttacker = controlledFormation.Team.IsAttacker;
									isAlly = controlledFormation.Team.IsPlayerAlly;
									bannerCode = BannerCode.CreateFrom(controlledFormation.Banner);
								}
								TargetIconType targetType2 = (mpheroClassForPeer != null) ? mpheroClassForPeer.IconType : TargetIconType.None;
								list.Add(new CompassItemUpdateParams(controlledFormation, targetType2, medianPosition.GetNavMeshVec3(), bannerCode, isAttacker, isAlly));
							}
						}
						else
						{
							Agent controlledAgent = component2.ControlledAgent;
							if (controlledAgent != null && controlledAgent.IsActive() && controlledAgent.Controller != Agent.ControllerType.Player)
							{
								BannerCode bannerCode2 = BannerCode.CreateFrom(new Banner(component2.Peer.BannerCode, component2.Team.Color, component2.Team.Color2));
								list.Add(new CompassItemUpdateParams(controlledAgent, mpheroClassForPeer.IconType, controlledAgent.Position, bannerCode2, component2.Team.IsAttacker, component2.Team.IsPlayerAlly));
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x00085E08 File Offset: 0x00084008
		public override int GetGoldAmount()
		{
			if (this._myRepresentative != null)
			{
				return this._myRepresentative.Gold;
			}
			return 0;
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x00085E20 File Offset: 0x00084020
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			if (this._remainingTimeForBellSoundToStop > 0f)
			{
				this._remainingTimeForBellSoundToStop -= dt;
			}
			if (this._bellSoundEvent != null && (this._remainingTimeForBellSoundToStop <= 0f || base.MissionLobbyComponent.CurrentMultiplayerState != MissionLobbyComponent.MultiplayerGameState.Playing))
			{
				this._remainingTimeForBellSoundToStop = float.MinValue;
				this._bellSoundEvent.Stop();
				this._bellSoundEvent = null;
			}
		}

		// Token: 0x04000D24 RID: 3364
		private const float MySideMoraleDropThreshold = 0.4f;

		// Token: 0x04000D25 RID: 3365
		private float _remainingTimeForBellSoundToStop = float.MinValue;

		// Token: 0x04000D26 RID: 3366
		private SoundEvent _bellSoundEvent;

		// Token: 0x04000D27 RID: 3367
		private FlagDominationMissionRepresentative _myRepresentative;

		// Token: 0x04000D28 RID: 3368
		private MissionScoreboardComponent _scoreboardComponent;

		// Token: 0x04000D29 RID: 3369
		private MultiplayerGameType _currentGameType;

		// Token: 0x04000D2A RID: 3370
		private Team[] _capturePointOwners;

		// Token: 0x04000D2C RID: 3372
		private bool _informedAboutFlagRemoval;
	}
}
