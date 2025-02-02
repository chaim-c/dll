using System;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002AD RID: 685
	public class MultiplayerRoundComponent : MissionNetwork, IRoundComponent, IMissionBehavior
	{
		// Token: 0x1400005D RID: 93
		// (add) Token: 0x0600254E RID: 9550 RVA: 0x0008E07C File Offset: 0x0008C27C
		// (remove) Token: 0x0600254F RID: 9551 RVA: 0x0008E0B4 File Offset: 0x0008C2B4
		public event Action OnRoundStarted;

		// Token: 0x1400005E RID: 94
		// (add) Token: 0x06002550 RID: 9552 RVA: 0x0008E0EC File Offset: 0x0008C2EC
		// (remove) Token: 0x06002551 RID: 9553 RVA: 0x0008E124 File Offset: 0x0008C324
		public event Action OnPreparationEnded;

		// Token: 0x1400005F RID: 95
		// (add) Token: 0x06002552 RID: 9554 RVA: 0x0008E15C File Offset: 0x0008C35C
		// (remove) Token: 0x06002553 RID: 9555 RVA: 0x0008E194 File Offset: 0x0008C394
		public event Action OnPreRoundEnding;

		// Token: 0x14000060 RID: 96
		// (add) Token: 0x06002554 RID: 9556 RVA: 0x0008E1CC File Offset: 0x0008C3CC
		// (remove) Token: 0x06002555 RID: 9557 RVA: 0x0008E204 File Offset: 0x0008C404
		public event Action OnRoundEnding;

		// Token: 0x14000061 RID: 97
		// (add) Token: 0x06002556 RID: 9558 RVA: 0x0008E23C File Offset: 0x0008C43C
		// (remove) Token: 0x06002557 RID: 9559 RVA: 0x0008E274 File Offset: 0x0008C474
		public event Action OnPostRoundEnded;

		// Token: 0x14000062 RID: 98
		// (add) Token: 0x06002558 RID: 9560 RVA: 0x0008E2AC File Offset: 0x0008C4AC
		// (remove) Token: 0x06002559 RID: 9561 RVA: 0x0008E2E4 File Offset: 0x0008C4E4
		public event Action OnCurrentRoundStateChanged;

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x0600255A RID: 9562 RVA: 0x0008E319 File Offset: 0x0008C519
		public float RemainingRoundTime
		{
			get
			{
				return this._gameModeClient.TimerComponent.GetRemainingTime(true);
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x0600255B RID: 9563 RVA: 0x0008E32C File Offset: 0x0008C52C
		// (set) Token: 0x0600255C RID: 9564 RVA: 0x0008E334 File Offset: 0x0008C534
		public float LastRoundEndRemainingTime { get; private set; }

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x0600255D RID: 9565 RVA: 0x0008E33D File Offset: 0x0008C53D
		// (set) Token: 0x0600255E RID: 9566 RVA: 0x0008E345 File Offset: 0x0008C545
		public MultiplayerRoundState CurrentRoundState { get; private set; }

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x0600255F RID: 9567 RVA: 0x0008E34E File Offset: 0x0008C54E
		// (set) Token: 0x06002560 RID: 9568 RVA: 0x0008E356 File Offset: 0x0008C556
		public int RoundCount { get; private set; }

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06002561 RID: 9569 RVA: 0x0008E35F File Offset: 0x0008C55F
		// (set) Token: 0x06002562 RID: 9570 RVA: 0x0008E367 File Offset: 0x0008C567
		public BattleSideEnum RoundWinner { get; private set; }

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06002563 RID: 9571 RVA: 0x0008E370 File Offset: 0x0008C570
		// (set) Token: 0x06002564 RID: 9572 RVA: 0x0008E378 File Offset: 0x0008C578
		public RoundEndReason RoundEndReason { get; private set; }

		// Token: 0x06002565 RID: 9573 RVA: 0x0008E381 File Offset: 0x0008C581
		public override void AfterStart()
		{
			this.AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Add);
			this._gameModeClient = Mission.Current.GetMissionBehavior<MissionMultiplayerGameModeBaseClient>();
		}

		// Token: 0x06002566 RID: 9574 RVA: 0x0008E39A File Offset: 0x0008C59A
		protected override void OnUdpNetworkHandlerClose()
		{
			this.AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Remove);
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x0008E3A4 File Offset: 0x0008C5A4
		private void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode mode)
		{
			GameNetwork.NetworkMessageHandlerRegisterer networkMessageHandlerRegisterer = new GameNetwork.NetworkMessageHandlerRegisterer(mode);
			if (GameNetwork.IsClient)
			{
				networkMessageHandlerRegisterer.Register<RoundStateChange>(new GameNetworkMessage.ServerMessageHandlerDelegate<RoundStateChange>(this.HandleServerEventChangeRoundState));
				networkMessageHandlerRegisterer.Register<RoundCountChange>(new GameNetworkMessage.ServerMessageHandlerDelegate<RoundCountChange>(this.HandleServerEventRoundCountChange));
				networkMessageHandlerRegisterer.Register<RoundWinnerChange>(new GameNetworkMessage.ServerMessageHandlerDelegate<RoundWinnerChange>(this.HandleServerEventRoundWinnerChange));
				networkMessageHandlerRegisterer.Register<RoundEndReasonChange>(new GameNetworkMessage.ServerMessageHandlerDelegate<RoundEndReasonChange>(this.HandleServerEventRoundEndReasonChange));
			}
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x0008E408 File Offset: 0x0008C608
		private void HandleServerEventChangeRoundState(RoundStateChange message)
		{
			if (this.CurrentRoundState == MultiplayerRoundState.InProgress)
			{
				this.LastRoundEndRemainingTime = (float)message.RemainingTimeOnPreviousState;
			}
			this.CurrentRoundState = message.RoundState;
			switch (this.CurrentRoundState)
			{
			case MultiplayerRoundState.Preparation:
				this._gameModeClient.TimerComponent.StartTimerAsClient(message.StateStartTimeInSeconds, (float)MultiplayerOptions.OptionType.RoundPreparationTimeLimit.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions));
				if (this.OnRoundStarted != null)
				{
					this.OnRoundStarted();
				}
				break;
			case MultiplayerRoundState.InProgress:
				this._gameModeClient.TimerComponent.StartTimerAsClient(message.StateStartTimeInSeconds, (float)MultiplayerOptions.OptionType.RoundTimeLimit.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions));
				if (this.OnPreparationEnded != null)
				{
					this.OnPreparationEnded();
				}
				break;
			case MultiplayerRoundState.Ending:
				this._gameModeClient.TimerComponent.StartTimerAsClient(message.StateStartTimeInSeconds, 3f);
				if (this.OnPreRoundEnding != null)
				{
					this.OnPreRoundEnding();
				}
				if (this.OnRoundEnding != null)
				{
					this.OnRoundEnding();
				}
				break;
			case MultiplayerRoundState.Ended:
				this._gameModeClient.TimerComponent.StartTimerAsClient(message.StateStartTimeInSeconds, 5f);
				if (this.OnPostRoundEnded != null)
				{
					this.OnPostRoundEnded();
				}
				break;
			case MultiplayerRoundState.MatchEnded:
				this._gameModeClient.TimerComponent.StartTimerAsClient(message.StateStartTimeInSeconds, 5f);
				break;
			}
			Action onCurrentRoundStateChanged = this.OnCurrentRoundStateChanged;
			if (onCurrentRoundStateChanged == null)
			{
				return;
			}
			onCurrentRoundStateChanged();
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x0008E571 File Offset: 0x0008C771
		private void HandleServerEventRoundCountChange(RoundCountChange message)
		{
			this.RoundCount = message.RoundCount;
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x0008E57F File Offset: 0x0008C77F
		private void HandleServerEventRoundWinnerChange(RoundWinnerChange message)
		{
			this.RoundWinner = message.RoundWinner;
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x0008E58D File Offset: 0x0008C78D
		private void HandleServerEventRoundEndReasonChange(RoundEndReasonChange message)
		{
			this.RoundEndReason = message.RoundEndReason;
		}

		// Token: 0x04000DCC RID: 3532
		public const int RoundEndDelayTime = 3;

		// Token: 0x04000DCD RID: 3533
		public const int RoundEndWaitTime = 8;

		// Token: 0x04000DCE RID: 3534
		public const int MatchEndWaitTime = 5;

		// Token: 0x04000DCF RID: 3535
		public const int WarmupEndWaitTime = 30;

		// Token: 0x04000DD6 RID: 3542
		private MissionMultiplayerGameModeBaseClient _gameModeClient;
	}
}
