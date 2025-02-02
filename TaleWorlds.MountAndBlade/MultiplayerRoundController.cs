using System;
using System.Linq;
using NetworkMessages.FromClient;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002B0 RID: 688
	public class MultiplayerRoundController : MissionNetwork, IRoundComponent, IMissionBehavior
	{
		// Token: 0x14000063 RID: 99
		// (add) Token: 0x0600256D RID: 9581 RVA: 0x0008E5A4 File Offset: 0x0008C7A4
		// (remove) Token: 0x0600256E RID: 9582 RVA: 0x0008E5DC File Offset: 0x0008C7DC
		public event Action OnRoundStarted;

		// Token: 0x14000064 RID: 100
		// (add) Token: 0x0600256F RID: 9583 RVA: 0x0008E614 File Offset: 0x0008C814
		// (remove) Token: 0x06002570 RID: 9584 RVA: 0x0008E64C File Offset: 0x0008C84C
		public event Action OnPreparationEnded;

		// Token: 0x14000065 RID: 101
		// (add) Token: 0x06002571 RID: 9585 RVA: 0x0008E684 File Offset: 0x0008C884
		// (remove) Token: 0x06002572 RID: 9586 RVA: 0x0008E6BC File Offset: 0x0008C8BC
		public event Action OnPreRoundEnding;

		// Token: 0x14000066 RID: 102
		// (add) Token: 0x06002573 RID: 9587 RVA: 0x0008E6F4 File Offset: 0x0008C8F4
		// (remove) Token: 0x06002574 RID: 9588 RVA: 0x0008E72C File Offset: 0x0008C92C
		public event Action OnRoundEnding;

		// Token: 0x14000067 RID: 103
		// (add) Token: 0x06002575 RID: 9589 RVA: 0x0008E764 File Offset: 0x0008C964
		// (remove) Token: 0x06002576 RID: 9590 RVA: 0x0008E79C File Offset: 0x0008C99C
		public event Action OnPostRoundEnded;

		// Token: 0x14000068 RID: 104
		// (add) Token: 0x06002577 RID: 9591 RVA: 0x0008E7D4 File Offset: 0x0008C9D4
		// (remove) Token: 0x06002578 RID: 9592 RVA: 0x0008E80C File Offset: 0x0008CA0C
		public event Action OnCurrentRoundStateChanged;

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06002579 RID: 9593 RVA: 0x0008E841 File Offset: 0x0008CA41
		// (set) Token: 0x0600257A RID: 9594 RVA: 0x0008E849 File Offset: 0x0008CA49
		public int RoundCount
		{
			get
			{
				return this._roundCount;
			}
			set
			{
				if (this._roundCount != value)
				{
					this._roundCount = value;
					if (GameNetwork.IsServer)
					{
						GameNetwork.BeginBroadcastModuleEvent();
						GameNetwork.WriteMessage(new RoundCountChange(this._roundCount));
						GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
					}
				}
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x0600257B RID: 9595 RVA: 0x0008E87E File Offset: 0x0008CA7E
		// (set) Token: 0x0600257C RID: 9596 RVA: 0x0008E886 File Offset: 0x0008CA86
		public BattleSideEnum RoundWinner
		{
			get
			{
				return this._roundWinner;
			}
			set
			{
				if (this._roundWinner != value)
				{
					this._roundWinner = value;
					if (GameNetwork.IsServer)
					{
						GameNetwork.BeginBroadcastModuleEvent();
						GameNetwork.WriteMessage(new RoundWinnerChange(value));
						GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
					}
				}
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x0600257D RID: 9597 RVA: 0x0008E8B6 File Offset: 0x0008CAB6
		// (set) Token: 0x0600257E RID: 9598 RVA: 0x0008E8BE File Offset: 0x0008CABE
		public RoundEndReason RoundEndReason
		{
			get
			{
				return this._roundEndReason;
			}
			set
			{
				if (this._roundEndReason != value)
				{
					this._roundEndReason = value;
					if (GameNetwork.IsServer)
					{
						GameNetwork.BeginBroadcastModuleEvent();
						GameNetwork.WriteMessage(new RoundEndReasonChange(value));
						GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
					}
				}
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x0600257F RID: 9599 RVA: 0x0008E8EE File Offset: 0x0008CAEE
		// (set) Token: 0x06002580 RID: 9600 RVA: 0x0008E8F6 File Offset: 0x0008CAF6
		public bool IsMatchEnding { get; private set; }

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06002581 RID: 9601 RVA: 0x0008E8FF File Offset: 0x0008CAFF
		// (set) Token: 0x06002582 RID: 9602 RVA: 0x0008E907 File Offset: 0x0008CB07
		public float LastRoundEndRemainingTime { get; private set; }

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06002583 RID: 9603 RVA: 0x0008E910 File Offset: 0x0008CB10
		public float RemainingRoundTime
		{
			get
			{
				return this._gameModeServer.TimerComponent.GetRemainingTime(false);
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06002584 RID: 9604 RVA: 0x0008E923 File Offset: 0x0008CB23
		// (set) Token: 0x06002585 RID: 9605 RVA: 0x0008E92B File Offset: 0x0008CB2B
		public MultiplayerRoundState CurrentRoundState { get; private set; }

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06002586 RID: 9606 RVA: 0x0008E934 File Offset: 0x0008CB34
		public bool IsRoundInProgress
		{
			get
			{
				return this.CurrentRoundState == MultiplayerRoundState.InProgress;
			}
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x0008E93F File Offset: 0x0008CB3F
		public void EnableEquipmentUpdate()
		{
			this._equipmentUpdateDisabled = false;
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x0008E948 File Offset: 0x0008CB48
		public override void AfterStart()
		{
			this.AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Add);
			if (GameNetwork.IsServerOrRecorder)
			{
				this._gameModeServer = Mission.Current.GetMissionBehavior<MissionMultiplayerGameModeBase>();
			}
			this._missionLobbyComponent = Mission.Current.GetMissionBehavior<MissionLobbyComponent>();
			this._roundCount = 0;
			this._gameModeServer.TimerComponent.StartTimerAsServer(8f);
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x0008E9A0 File Offset: 0x0008CBA0
		private void EndRound()
		{
			if (this.OnPreRoundEnding != null)
			{
				this.OnPreRoundEnding();
			}
			this.ChangeRoundState(MultiplayerRoundState.Ending);
			this._gameModeServer.TimerComponent.StartTimerAsServer(3f);
			this._roundTimeOver = false;
			if (this.OnRoundEnding != null)
			{
				this.OnRoundEnding();
			}
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x0008E9F6 File Offset: 0x0008CBF6
		private bool CheckPostEndRound()
		{
			return this._gameModeServer.TimerComponent.CheckIfTimerPassed();
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x0008EA08 File Offset: 0x0008CC08
		private bool CheckPostMatchEnd()
		{
			return this._gameModeServer.TimerComponent.CheckIfTimerPassed();
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x0008EA1C File Offset: 0x0008CC1C
		private void PostRoundEnd()
		{
			this._gameModeServer.TimerComponent.StartTimerAsServer(5f);
			this.ChangeRoundState(MultiplayerRoundState.Ended);
			if (this._roundCount == MultiplayerOptions.OptionType.RoundTotal.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) || this.CheckForMatchEndEarly() || !this.HasEnoughCharactersOnBothSides())
			{
				this.IsMatchEnding = true;
			}
			if (this.OnPostRoundEnded != null)
			{
				this.OnPostRoundEnded();
			}
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x0008EA7F File Offset: 0x0008CC7F
		private void PostMatchEnd()
		{
			this._gameModeServer.TimerComponent.StartTimerAsServer(5f);
			this.ChangeRoundState(MultiplayerRoundState.MatchEnded);
			this._missionLobbyComponent.SetStateEndingAsServer();
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x0008EAA8 File Offset: 0x0008CCA8
		public override void OnRemoveBehavior()
		{
			GameNetwork.RemoveNetworkHandler(this);
			base.OnRemoveBehavior();
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x0008EAB8 File Offset: 0x0008CCB8
		private void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode mode)
		{
			GameNetwork.NetworkMessageHandlerRegisterer networkMessageHandlerRegisterer = new GameNetwork.NetworkMessageHandlerRegisterer(mode);
			if (!GameNetwork.IsClient && GameNetwork.IsServer)
			{
				networkMessageHandlerRegisterer.Register<CultureVoteClient>(new GameNetworkMessage.ClientMessageHandlerDelegate<CultureVoteClient>(this.HandleClientEventCultureSelect));
			}
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x0008EAEC File Offset: 0x0008CCEC
		protected override void OnUdpNetworkHandlerClose()
		{
			this.AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Remove);
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x0008EAF8 File Offset: 0x0008CCF8
		public override void OnPreDisplayMissionTick(float dt)
		{
			if (GameNetwork.IsServer)
			{
				if (this._missionLobbyComponent.CurrentMultiplayerState != MissionLobbyComponent.MultiplayerGameState.WaitingFirstPlayers)
				{
					if (!this.IsMatchEnding && this._missionLobbyComponent.CurrentMultiplayerState != MissionLobbyComponent.MultiplayerGameState.Ending && (this.CurrentRoundState == MultiplayerRoundState.WaitingForPlayers || this.CurrentRoundState == MultiplayerRoundState.Ended))
					{
						if (this.CheckForNewRound())
						{
							this.BeginNewRound();
							return;
						}
						if (this.IsMatchEnding)
						{
							this.PostMatchEnd();
							return;
						}
					}
					else if (this.CurrentRoundState == MultiplayerRoundState.Preparation)
					{
						if (this.CheckForPreparationEnd())
						{
							this.EndPreparation();
							this.StartSpawning(this._equipmentUpdateDisabled);
							return;
						}
					}
					else if (this.CurrentRoundState == MultiplayerRoundState.InProgress)
					{
						if (this.CheckForRoundEnd())
						{
							this.EndRound();
							return;
						}
					}
					else if (this.CurrentRoundState == MultiplayerRoundState.Ending)
					{
						if (this.CheckPostEndRound())
						{
							this.PostRoundEnd();
							return;
						}
					}
					else if (this.CurrentRoundState == MultiplayerRoundState.Ended && this.IsMatchEnding && this.CheckPostMatchEnd())
					{
						this.PostMatchEnd();
						return;
					}
				}
			}
			else
			{
				this._gameModeServer.TimerComponent.CheckIfTimerPassed();
			}
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x0008EBEC File Offset: 0x0008CDEC
		private void ChangeRoundState(MultiplayerRoundState newRoundState)
		{
			if (this.CurrentRoundState != newRoundState)
			{
				if (this.CurrentRoundState == MultiplayerRoundState.InProgress)
				{
					this.LastRoundEndRemainingTime = this.RemainingRoundTime;
				}
				this.CurrentRoundState = newRoundState;
				this._currentRoundStateStartTime = MissionTime.Now;
				Action onCurrentRoundStateChanged = this.OnCurrentRoundStateChanged;
				if (onCurrentRoundStateChanged != null)
				{
					onCurrentRoundStateChanged();
				}
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new RoundStateChange(newRoundState, this._currentRoundStateStartTime.NumberOfTicks, MathF.Ceiling(this.LastRoundEndRemainingTime)));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
			}
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x0008EC67 File Offset: 0x0008CE67
		protected override void HandleLateNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x0008EC69 File Offset: 0x0008CE69
		public bool HandleClientEventCultureSelect(NetworkCommunicator peer, CultureVoteClient message)
		{
			peer.GetComponent<MissionPeer>().HandleVoteChange(message.VotedType, message.VotedCulture);
			return true;
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x0008EC84 File Offset: 0x0008CE84
		private bool CheckForRoundEnd()
		{
			if (!this._roundTimeOver)
			{
				this._roundTimeOver = this._gameModeServer.TimerComponent.CheckIfTimerPassed();
			}
			return (!this._gameModeServer.CheckIfOvertime() && this._roundTimeOver) || this._gameModeServer.CheckForRoundEnd();
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x0008ECD4 File Offset: 0x0008CED4
		private bool CheckForNewRound()
		{
			if (this.CurrentRoundState != MultiplayerRoundState.WaitingForPlayers && !this._gameModeServer.TimerComponent.CheckIfTimerPassed())
			{
				return false;
			}
			int[] array = new int[2];
			foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
			{
				MissionPeer component = networkCommunicator.GetComponent<MissionPeer>();
				if (networkCommunicator.IsSynchronized && ((component != null) ? component.Team : null) != null && component.Team.Side != BattleSideEnum.None)
				{
					array[(int)component.Team.Side]++;
				}
			}
			if (array.Sum() < MultiplayerOptions.OptionType.MinNumberOfPlayersForMatchStart.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) && this.RoundCount == 0)
			{
				this.IsMatchEnding = true;
				return false;
			}
			return true;
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x0008EDAC File Offset: 0x0008CFAC
		private bool HasEnoughCharactersOnBothSides()
		{
			bool flag;
			if (MultiplayerOptions.OptionType.NumberOfBotsTeam1.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) <= 0)
			{
				flag = (GameNetwork.NetworkPeers.Count((NetworkCommunicator q) => q.GetComponent<MissionPeer>() != null && q.GetComponent<MissionPeer>().Team == Mission.Current.AttackerTeam) > 0);
			}
			else
			{
				flag = true;
			}
			bool flag2;
			if (MultiplayerOptions.OptionType.NumberOfBotsTeam2.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) <= 0)
			{
				flag2 = (GameNetwork.NetworkPeers.Count((NetworkCommunicator q) => q.GetComponent<MissionPeer>() != null && q.GetComponent<MissionPeer>().Team == Mission.Current.DefenderTeam) > 0);
			}
			else
			{
				flag2 = true;
			}
			bool flag3 = flag2;
			return flag && flag3;
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x0008EE30 File Offset: 0x0008D030
		private void BeginNewRound()
		{
			if (this.CurrentRoundState == MultiplayerRoundState.WaitingForPlayers)
			{
				this._gameModeServer.ClearPeerCounts();
			}
			this.ChangeRoundState(MultiplayerRoundState.Preparation);
			int roundCount = this.RoundCount;
			this.RoundCount = roundCount + 1;
			Mission.Current.ResetMission();
			this._gameModeServer.MultiplayerTeamSelectComponent.BalanceTeams();
			this._gameModeServer.TimerComponent.StartTimerAsServer((float)MultiplayerOptions.OptionType.RoundPreparationTimeLimit.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions));
			Action onRoundStarted = this.OnRoundStarted;
			if (onRoundStarted != null)
			{
				onRoundStarted();
			}
			this._gameModeServer.SpawnComponent.ToggleUpdatingSpawnEquipment(true);
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x0008EEBC File Offset: 0x0008D0BC
		private bool CheckForPreparationEnd()
		{
			return this.CurrentRoundState == MultiplayerRoundState.Preparation && this._gameModeServer.TimerComponent.CheckIfTimerPassed();
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x0008EED9 File Offset: 0x0008D0D9
		private void EndPreparation()
		{
			if (this.OnPreparationEnded != null)
			{
				this.OnPreparationEnded();
			}
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x0008EEEE File Offset: 0x0008D0EE
		private void StartSpawning(bool disableEquipmentUpdate = true)
		{
			this._gameModeServer.TimerComponent.StartTimerAsServer((float)MultiplayerOptions.OptionType.RoundTimeLimit.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions));
			if (disableEquipmentUpdate)
			{
				this._gameModeServer.SpawnComponent.ToggleUpdatingSpawnEquipment(false);
			}
			this.ChangeRoundState(MultiplayerRoundState.InProgress);
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x0008EF24 File Offset: 0x0008D124
		private bool CheckForMatchEndEarly()
		{
			bool result = false;
			MissionScoreboardComponent missionBehavior = Mission.Current.GetMissionBehavior<MissionScoreboardComponent>();
			if (missionBehavior != null)
			{
				for (int i = 0; i < 2; i++)
				{
					if (missionBehavior.GetRoundScore((BattleSideEnum)i) > MultiplayerOptions.OptionType.RoundTotal.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) / 2)
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x0008EF68 File Offset: 0x0008D168
		protected override void HandleNewClientAfterSynchronized(NetworkCommunicator networkPeer)
		{
			if (!networkPeer.IsServerPeer)
			{
				GameNetwork.BeginModuleEventAsServer(networkPeer);
				GameNetwork.WriteMessage(new RoundStateChange(this.CurrentRoundState, this._currentRoundStateStartTime.NumberOfTicks, MathF.Ceiling(this.LastRoundEndRemainingTime)));
				GameNetwork.EndModuleEventAsServer();
				GameNetwork.BeginModuleEventAsServer(networkPeer);
				GameNetwork.WriteMessage(new RoundWinnerChange(this.RoundWinner));
				GameNetwork.EndModuleEventAsServer();
				GameNetwork.BeginModuleEventAsServer(networkPeer);
				GameNetwork.WriteMessage(new RoundCountChange(this.RoundCount));
				GameNetwork.EndModuleEventAsServer();
			}
		}

		// Token: 0x04000DEF RID: 3567
		private MissionMultiplayerGameModeBase _gameModeServer;

		// Token: 0x04000DF0 RID: 3568
		private int _roundCount;

		// Token: 0x04000DF1 RID: 3569
		private BattleSideEnum _roundWinner;

		// Token: 0x04000DF2 RID: 3570
		private RoundEndReason _roundEndReason;

		// Token: 0x04000DF3 RID: 3571
		private MissionLobbyComponent _missionLobbyComponent;

		// Token: 0x04000DF5 RID: 3573
		private bool _roundTimeOver;

		// Token: 0x04000DF7 RID: 3575
		private MissionTime _currentRoundStateStartTime;

		// Token: 0x04000DF9 RID: 3577
		private bool _equipmentUpdateDisabled = true;
	}
}
