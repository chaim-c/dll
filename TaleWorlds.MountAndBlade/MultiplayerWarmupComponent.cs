using System;
using System.Collections.Generic;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002B3 RID: 691
	public class MultiplayerWarmupComponent : MissionNetwork
	{
		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x060025C2 RID: 9666 RVA: 0x0008FD17 File Offset: 0x0008DF17
		public static float TotalWarmupDuration
		{
			get
			{
				return (float)(MultiplayerOptions.OptionType.WarmupTimeLimit.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) * 60);
			}
		}

		// Token: 0x1400006D RID: 109
		// (add) Token: 0x060025C3 RID: 9667 RVA: 0x0008FD28 File Offset: 0x0008DF28
		// (remove) Token: 0x060025C4 RID: 9668 RVA: 0x0008FD60 File Offset: 0x0008DF60
		public event Action OnWarmupEnding;

		// Token: 0x1400006E RID: 110
		// (add) Token: 0x060025C5 RID: 9669 RVA: 0x0008FD98 File Offset: 0x0008DF98
		// (remove) Token: 0x060025C6 RID: 9670 RVA: 0x0008FDD0 File Offset: 0x0008DFD0
		public event Action OnWarmupEnded;

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x060025C7 RID: 9671 RVA: 0x0008FE05 File Offset: 0x0008E005
		public bool IsInWarmup
		{
			get
			{
				return this.WarmupState != MultiplayerWarmupComponent.WarmupStates.Ended;
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x060025C8 RID: 9672 RVA: 0x0008FE13 File Offset: 0x0008E013
		// (set) Token: 0x060025C9 RID: 9673 RVA: 0x0008FE1C File Offset: 0x0008E01C
		private MultiplayerWarmupComponent.WarmupStates WarmupState
		{
			get
			{
				return this._warmupState;
			}
			set
			{
				this._warmupState = value;
				if (GameNetwork.IsServer)
				{
					this._currentStateStartTime = MissionTime.Now;
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new WarmupStateChange(this._warmupState, this._currentStateStartTime.NumberOfTicks));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
				}
			}
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x0008FE69 File Offset: 0x0008E069
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._gameMode = base.Mission.GetMissionBehavior<MissionMultiplayerGameModeBase>();
			this._timerComponent = base.Mission.GetMissionBehavior<MultiplayerTimerComponent>();
			this._lobbyComponent = base.Mission.GetMissionBehavior<MissionLobbyComponent>();
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x0008FEA4 File Offset: 0x0008E0A4
		public override void AfterStart()
		{
			base.AfterStart();
			this.AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Add);
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x0008FEB3 File Offset: 0x0008E0B3
		protected override void OnUdpNetworkHandlerClose()
		{
			this.AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Remove);
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x0008FEBC File Offset: 0x0008E0BC
		private void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode mode)
		{
			GameNetwork.NetworkMessageHandlerRegisterer networkMessageHandlerRegisterer = new GameNetwork.NetworkMessageHandlerRegisterer(mode);
			if (GameNetwork.IsClient)
			{
				networkMessageHandlerRegisterer.Register<WarmupStateChange>(new GameNetworkMessage.ServerMessageHandlerDelegate<WarmupStateChange>(this.HandleServerEventWarmupStateChange));
			}
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x0008FEE9 File Offset: 0x0008E0E9
		public bool CheckForWarmupProgressEnd()
		{
			return this._gameMode.CheckForWarmupEnd() || this._timerComponent.GetRemainingTime(false) <= 30f;
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x0008FF10 File Offset: 0x0008E110
		public override void OnPreDisplayMissionTick(float dt)
		{
			if (GameNetwork.IsServer && this._lobbyComponent.CurrentMultiplayerState != MissionLobbyComponent.MultiplayerGameState.Ending)
			{
				switch (this.WarmupState)
				{
				case MultiplayerWarmupComponent.WarmupStates.WaitingForPlayers:
					this.BeginWarmup();
					return;
				case MultiplayerWarmupComponent.WarmupStates.InProgress:
					if (this.CheckForWarmupProgressEnd())
					{
						this.EndWarmupProgress();
						return;
					}
					break;
				case MultiplayerWarmupComponent.WarmupStates.Ending:
					if (this._timerComponent.CheckIfTimerPassed())
					{
						this.EndWarmup();
						return;
					}
					break;
				case MultiplayerWarmupComponent.WarmupStates.Ended:
					if (this._timerComponent.CheckIfTimerPassed())
					{
						base.Mission.RemoveMissionBehavior(this);
						return;
					}
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
			}
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x0008FF9C File Offset: 0x0008E19C
		private void BeginWarmup()
		{
			this.WarmupState = MultiplayerWarmupComponent.WarmupStates.InProgress;
			Mission.Current.ResetMission();
			this._gameMode.MultiplayerTeamSelectComponent.BalanceTeams();
			this._timerComponent.StartTimerAsServer(MultiplayerWarmupComponent.TotalWarmupDuration);
			this._gameMode.SpawnComponent.SpawningBehavior.Clear();
			SpawnComponent.SetWarmupSpawningBehavior();
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x0008FFF4 File Offset: 0x0008E1F4
		public void EndWarmupProgress()
		{
			this.WarmupState = MultiplayerWarmupComponent.WarmupStates.Ending;
			this._timerComponent.StartTimerAsServer(30f);
			Action onWarmupEnding = this.OnWarmupEnding;
			if (onWarmupEnding == null)
			{
				return;
			}
			onWarmupEnding();
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x00090020 File Offset: 0x0008E220
		private void EndWarmup()
		{
			this.WarmupState = MultiplayerWarmupComponent.WarmupStates.Ended;
			this._timerComponent.StartTimerAsServer(3f);
			Action onWarmupEnded = this.OnWarmupEnded;
			if (onWarmupEnded != null)
			{
				onWarmupEnded();
			}
			if (!GameNetwork.IsDedicatedServer)
			{
				this.PlayBattleStartingSound();
			}
			Mission.Current.ResetMission();
			this._gameMode.MultiplayerTeamSelectComponent.BalanceTeams();
			this._gameMode.SpawnComponent.SpawningBehavior.Clear();
			SpawnComponent.SetSpawningBehaviorForCurrentGameType(this._gameMode.GetMissionType());
			if (!this.CanMatchStartAfterWarmup())
			{
				this._lobbyComponent.SetStateEndingAsServer();
			}
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x000900B4 File Offset: 0x0008E2B4
		public bool CanMatchStartAfterWarmup()
		{
			bool[] array = new bool[2];
			foreach (NetworkCommunicator networkPeer in GameNetwork.NetworkPeers)
			{
				MissionPeer component = networkPeer.GetComponent<MissionPeer>();
				if (((component != null) ? component.Team : null) != null && component.Team.Side != BattleSideEnum.None)
				{
					array[(int)component.Team.Side] = true;
				}
				if (array[1] && array[0])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x00090148 File Offset: 0x0008E348
		public override void OnRemoveBehavior()
		{
			base.OnRemoveBehavior();
			this.OnWarmupEnding = null;
			this.OnWarmupEnded = null;
			if (GameNetwork.IsServer && !this._gameMode.UseRoundController() && this._lobbyComponent.CurrentMultiplayerState != MissionLobbyComponent.MultiplayerGameState.Ending)
			{
				this._gameMode.SpawnComponent.SpawningBehavior.RequestStartSpawnSession();
			}
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x000901A0 File Offset: 0x0008E3A0
		protected override void HandleNewClientAfterSynchronized(NetworkCommunicator networkPeer)
		{
			if (this.IsInWarmup && !networkPeer.IsServerPeer)
			{
				GameNetwork.BeginModuleEventAsServer(networkPeer);
				GameNetwork.WriteMessage(new WarmupStateChange(this._warmupState, this._currentStateStartTime.NumberOfTicks));
				GameNetwork.EndModuleEventAsServer();
			}
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x000901D8 File Offset: 0x0008E3D8
		private void HandleServerEventWarmupStateChange(WarmupStateChange message)
		{
			this.WarmupState = message.WarmupState;
			switch (this.WarmupState)
			{
			case MultiplayerWarmupComponent.WarmupStates.InProgress:
				this._timerComponent.StartTimerAsClient(message.StateStartTimeInSeconds, MultiplayerWarmupComponent.TotalWarmupDuration);
				return;
			case MultiplayerWarmupComponent.WarmupStates.Ending:
			{
				this._timerComponent.StartTimerAsClient(message.StateStartTimeInSeconds, 30f);
				Action onWarmupEnding = this.OnWarmupEnding;
				if (onWarmupEnding == null)
				{
					return;
				}
				onWarmupEnding();
				return;
			}
			case MultiplayerWarmupComponent.WarmupStates.Ended:
			{
				this._timerComponent.StartTimerAsClient(message.StateStartTimeInSeconds, 3f);
				Action onWarmupEnded = this.OnWarmupEnded;
				if (onWarmupEnded != null)
				{
					onWarmupEnded();
				}
				this.PlayBattleStartingSound();
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x00090278 File Offset: 0x0008E478
		private void PlayBattleStartingSound()
		{
			MatrixFrame cameraFrame = Mission.Current.GetCameraFrame();
			Vec3 position = cameraFrame.origin + cameraFrame.rotation.u;
			NetworkCommunicator myPeer = GameNetwork.MyPeer;
			MissionPeer missionPeer = (myPeer != null) ? myPeer.GetComponent<MissionPeer>() : null;
			if (((missionPeer != null) ? missionPeer.Team : null) != null)
			{
				string text = (missionPeer.Team.Side == BattleSideEnum.Attacker) ? MultiplayerOptions.OptionType.CultureTeam1.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) : MultiplayerOptions.OptionType.CultureTeam2.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
				MBSoundEvent.PlaySound(SoundEvent.GetEventIdFromString("event:/alerts/rally/" + text.ToLower()), position);
				return;
			}
			MBSoundEvent.PlaySound(SoundEvent.GetEventIdFromString("event:/alerts/rally/generic"), position);
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x00090318 File Offset: 0x0008E518
		[CommandLineFunctionality.CommandLineArgumentFunction("end_warmup", "mp_host")]
		public static string CommandEndWarmup(List<string> strings)
		{
			if (Mission.Current == null)
			{
				return "end_warmup can only be called within a mission.";
			}
			if (!GameNetwork.IsServer)
			{
				return "end_warmup can only be called by the server.";
			}
			MultiplayerWarmupComponent missionBehavior = Mission.Current.GetMissionBehavior<MultiplayerWarmupComponent>();
			if (missionBehavior == null)
			{
				return "end_warmup can only be called when the game is in warmup.";
			}
			missionBehavior.EndWarmupProgress();
			return "Success";
		}

		// Token: 0x04000E05 RID: 3589
		public const int RespawnPeriodInWarmup = 3;

		// Token: 0x04000E06 RID: 3590
		public const int WarmupEndWaitTime = 30;

		// Token: 0x04000E09 RID: 3593
		private MissionMultiplayerGameModeBase _gameMode;

		// Token: 0x04000E0A RID: 3594
		private MultiplayerTimerComponent _timerComponent;

		// Token: 0x04000E0B RID: 3595
		private MissionLobbyComponent _lobbyComponent;

		// Token: 0x04000E0C RID: 3596
		private MissionTime _currentStateStartTime;

		// Token: 0x04000E0D RID: 3597
		private MultiplayerWarmupComponent.WarmupStates _warmupState;

		// Token: 0x0200057A RID: 1402
		public enum WarmupStates
		{
			// Token: 0x04001D4B RID: 7499
			WaitingForPlayers,
			// Token: 0x04001D4C RID: 7500
			InProgress,
			// Token: 0x04001D4D RID: 7501
			Ending,
			// Token: 0x04001D4E RID: 7502
			Ended
		}
	}
}
