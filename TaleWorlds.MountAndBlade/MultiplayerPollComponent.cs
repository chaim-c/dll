using System;
using System.Collections.Generic;
using System.Linq;
using NetworkMessages.FromClient;
using NetworkMessages.FromServer;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002AC RID: 684
	public class MultiplayerPollComponent : MissionNetwork
	{
		// Token: 0x06002530 RID: 9520 RVA: 0x0008D776 File Offset: 0x0008B976
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._missionLobbyComponent = base.Mission.GetMissionBehavior<MissionLobbyComponent>();
			this._notificationsComponent = base.Mission.GetMissionBehavior<MultiplayerGameNotificationsComponent>();
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x0008D7A0 File Offset: 0x0008B9A0
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			MultiplayerPollComponent.MultiplayerPoll ongoingPoll = this._ongoingPoll;
			if (ongoingPoll == null)
			{
				return;
			}
			ongoingPoll.Tick();
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x0008D7BC File Offset: 0x0008B9BC
		public void Vote(bool accepted)
		{
			if (GameNetwork.IsServer)
			{
				if (GameNetwork.MyPeer != null)
				{
					this.ApplyVote(GameNetwork.MyPeer, accepted);
					return;
				}
			}
			else if (this._ongoingPoll != null && this._ongoingPoll.IsOpen)
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new PollResponse(accepted));
				GameNetwork.EndModuleEventAsClient();
			}
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x0008D810 File Offset: 0x0008BA10
		private void ApplyVote(NetworkCommunicator peer, bool accepted)
		{
			if (this._ongoingPoll != null && this._ongoingPoll.ApplyVote(peer, accepted))
			{
				List<NetworkCommunicator> pollProgressReceivers = this._ongoingPoll.GetPollProgressReceivers();
				int count = pollProgressReceivers.Count;
				for (int i = 0; i < count; i++)
				{
					GameNetwork.BeginModuleEventAsServer(pollProgressReceivers[i]);
					GameNetwork.WriteMessage(new PollProgress(this._ongoingPoll.AcceptedCount, this._ongoingPoll.RejectedCount));
					GameNetwork.EndModuleEventAsServer();
				}
				this.UpdatePollProgress(this._ongoingPoll.AcceptedCount, this._ongoingPoll.RejectedCount);
			}
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x0008D8A0 File Offset: 0x0008BAA0
		private void RejectPollOnServer(NetworkCommunicator pollCreatorPeer, MultiplayerPollRejectReason rejectReason)
		{
			if (pollCreatorPeer.IsMine)
			{
				this.RejectPoll(rejectReason);
				return;
			}
			GameNetwork.BeginModuleEventAsServer(pollCreatorPeer);
			GameNetwork.WriteMessage(new PollRequestRejected((int)rejectReason));
			GameNetwork.EndModuleEventAsServer();
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x0008D8C8 File Offset: 0x0008BAC8
		private void RejectPoll(MultiplayerPollRejectReason rejectReason)
		{
			if (!GameNetwork.IsDedicatedServer)
			{
				this._notificationsComponent.PollRejected(rejectReason);
			}
			Action<MultiplayerPollRejectReason> onPollRejected = this.OnPollRejected;
			if (onPollRejected == null)
			{
				return;
			}
			onPollRejected(rejectReason);
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x0008D8EE File Offset: 0x0008BAEE
		private void UpdatePollProgress(int votesAccepted, int votesRejected)
		{
			Action<int, int> onPollUpdated = this.OnPollUpdated;
			if (onPollUpdated == null)
			{
				return;
			}
			onPollUpdated(votesAccepted, votesRejected);
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x0008D902 File Offset: 0x0008BB02
		private void CancelPoll()
		{
			if (this._ongoingPoll != null)
			{
				this._ongoingPoll.Cancel();
				this._ongoingPoll = null;
			}
			Action onPollCancelled = this.OnPollCancelled;
			if (onPollCancelled == null)
			{
				return;
			}
			onPollCancelled();
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x0008D930 File Offset: 0x0008BB30
		private void OnPollCancelledOnServer(MultiplayerPollComponent.MultiplayerPoll multiplayerPoll)
		{
			List<NetworkCommunicator> pollProgressReceivers = multiplayerPoll.GetPollProgressReceivers();
			int count = pollProgressReceivers.Count;
			for (int i = 0; i < count; i++)
			{
				GameNetwork.BeginModuleEventAsServer(pollProgressReceivers[i]);
				GameNetwork.WriteMessage(new PollCancelled());
				GameNetwork.EndModuleEventAsServer();
			}
			this.CancelPoll();
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x0008D978 File Offset: 0x0008BB78
		public void RequestKickPlayerPoll(NetworkCommunicator peer, bool banPlayer)
		{
			if (GameNetwork.IsServer)
			{
				if (GameNetwork.MyPeer != null)
				{
					this.OpenKickPlayerPollOnServer(GameNetwork.MyPeer, peer, banPlayer);
					return;
				}
			}
			else
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new KickPlayerPollRequested(peer, banPlayer));
				GameNetwork.EndModuleEventAsClient();
			}
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x0008D9AC File Offset: 0x0008BBAC
		private void OpenKickPlayerPollOnServer(NetworkCommunicator pollCreatorPeer, NetworkCommunicator targetPeer, bool banPlayer)
		{
			if (this._ongoingPoll == null)
			{
				bool flag = pollCreatorPeer != null && pollCreatorPeer.IsConnectionActive;
				bool flag2 = targetPeer != null && targetPeer.IsConnectionActive;
				if (flag && flag2)
				{
					if (!targetPeer.IsSynchronized)
					{
						this.RejectPollOnServer(pollCreatorPeer, MultiplayerPollRejectReason.KickPollTargetNotSynced);
						return;
					}
					MissionPeer component = pollCreatorPeer.GetComponent<MissionPeer>();
					if (component != null)
					{
						if (component.RequestedKickPollCount >= 2)
						{
							this.RejectPollOnServer(pollCreatorPeer, MultiplayerPollRejectReason.TooManyPollRequests);
							return;
						}
						List<NetworkCommunicator> list = new List<NetworkCommunicator>();
						foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
						{
							if (networkCommunicator != targetPeer && networkCommunicator.IsSynchronized)
							{
								MissionPeer component2 = networkCommunicator.GetComponent<MissionPeer>();
								if (component2 != null && component2.Team == component.Team)
								{
									list.Add(networkCommunicator);
								}
							}
						}
						int count = list.Count;
						if (count + 1 >= 3)
						{
							this.OpenKickPlayerPoll(targetPeer, pollCreatorPeer, false, list);
							for (int i = 0; i < count; i++)
							{
								GameNetwork.BeginModuleEventAsServer(this._ongoingPoll.ParticipantsToVote[i]);
								GameNetwork.WriteMessage(new KickPlayerPollOpened(pollCreatorPeer, targetPeer, banPlayer));
								GameNetwork.EndModuleEventAsServer();
							}
							GameNetwork.BeginModuleEventAsServer(targetPeer);
							GameNetwork.WriteMessage(new KickPlayerPollOpened(pollCreatorPeer, targetPeer, banPlayer));
							GameNetwork.EndModuleEventAsServer();
							component.IncrementRequestedKickPollCount();
							return;
						}
						this.RejectPollOnServer(pollCreatorPeer, MultiplayerPollRejectReason.NotEnoughPlayersToOpenPoll);
						return;
					}
				}
			}
			else
			{
				this.RejectPollOnServer(pollCreatorPeer, MultiplayerPollRejectReason.HasOngoingPoll);
			}
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x0008DB14 File Offset: 0x0008BD14
		private void OpenKickPlayerPoll(NetworkCommunicator targetPeer, NetworkCommunicator pollCreatorPeer, bool banPlayer, List<NetworkCommunicator> participantsToVote)
		{
			MissionPeer component = pollCreatorPeer.GetComponent<MissionPeer>();
			MissionPeer component2 = targetPeer.GetComponent<MissionPeer>();
			this._ongoingPoll = new MultiplayerPollComponent.KickPlayerPoll(this._missionLobbyComponent.MissionType, participantsToVote, targetPeer, component.Team);
			if (GameNetwork.IsServer)
			{
				MultiplayerPollComponent.MultiplayerPoll ongoingPoll = this._ongoingPoll;
				ongoingPoll.OnClosedOnServer = (Action<MultiplayerPollComponent.MultiplayerPoll>)Delegate.Combine(ongoingPoll.OnClosedOnServer, new Action<MultiplayerPollComponent.MultiplayerPoll>(this.OnKickPlayerPollClosedOnServer));
				MultiplayerPollComponent.MultiplayerPoll ongoingPoll2 = this._ongoingPoll;
				ongoingPoll2.OnCancelledOnServer = (Action<MultiplayerPollComponent.MultiplayerPoll>)Delegate.Combine(ongoingPoll2.OnCancelledOnServer, new Action<MultiplayerPollComponent.MultiplayerPoll>(this.OnPollCancelledOnServer));
			}
			Action<MissionPeer, MissionPeer, bool> onKickPollOpened = this.OnKickPollOpened;
			if (onKickPollOpened != null)
			{
				onKickPollOpened(component, component2, banPlayer);
			}
			if (GameNetwork.MyPeer == pollCreatorPeer)
			{
				this.Vote(true);
			}
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x0008DBC8 File Offset: 0x0008BDC8
		private void OnKickPlayerPollClosedOnServer(MultiplayerPollComponent.MultiplayerPoll multiplayerPoll)
		{
			MultiplayerPollComponent.KickPlayerPoll kickPlayerPoll = multiplayerPoll as MultiplayerPollComponent.KickPlayerPoll;
			bool flag = kickPlayerPoll.GotEnoughAcceptVotesToEnd();
			GameNetwork.BeginBroadcastModuleEvent();
			GameNetwork.WriteMessage(new KickPlayerPollClosed(kickPlayerPoll.TargetPeer, flag));
			GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
			this.CloseKickPlayerPoll(flag, kickPlayerPoll.TargetPeer);
			if (flag)
			{
				DisconnectInfo disconnectInfo = kickPlayerPoll.TargetPeer.PlayerConnectionInfo.GetParameter<DisconnectInfo>("DisconnectInfo") ?? new DisconnectInfo();
				disconnectInfo.Type = DisconnectType.KickedByPoll;
				kickPlayerPoll.TargetPeer.PlayerConnectionInfo.AddParameter("DisconnectInfo", disconnectInfo);
				GameNetwork.AddNetworkPeerToDisconnectAsServer(kickPlayerPoll.TargetPeer);
			}
		}

		// Token: 0x0600253D RID: 9533 RVA: 0x0008DC58 File Offset: 0x0008BE58
		private void CloseKickPlayerPoll(bool accepted, NetworkCommunicator targetPeer)
		{
			if (this._ongoingPoll != null)
			{
				this._ongoingPoll.Close();
				this._ongoingPoll = null;
			}
			Action onPollClosed = this.OnPollClosed;
			if (onPollClosed != null)
			{
				onPollClosed();
			}
			if (!GameNetwork.IsDedicatedServer && accepted && !targetPeer.IsMine)
			{
				this._notificationsComponent.PlayerKicked(targetPeer);
			}
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x0008DCB0 File Offset: 0x0008BEB0
		private void OnBanPlayerPollClosedOnServer(MultiplayerPollComponent.MultiplayerPoll multiplayerPoll)
		{
			MissionPeer component = (multiplayerPoll as MultiplayerPollComponent.BanPlayerPoll).TargetPeer.GetComponent<MissionPeer>();
			if (component != null)
			{
				NetworkCommunicator networkPeer = component.GetNetworkPeer();
				DisconnectInfo disconnectInfo = networkPeer.PlayerConnectionInfo.GetParameter<DisconnectInfo>("DisconnectInfo") ?? new DisconnectInfo();
				disconnectInfo.Type = DisconnectType.BannedByPoll;
				networkPeer.PlayerConnectionInfo.AddParameter("DisconnectInfo", disconnectInfo);
				GameNetwork.AddNetworkPeerToDisconnectAsServer(networkPeer);
				if (GameNetwork.IsServer)
				{
					CustomGameBannedPlayerManager.AddBannedPlayer(component.Peer.Id, Environment.TickCount + 600000);
				}
				if (GameNetwork.IsDedicatedServer)
				{
					throw new NotImplementedException();
				}
				NetworkMain.GameClient.KickPlayer(component.Peer.Id, true);
			}
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x0008DD58 File Offset: 0x0008BF58
		private void StartChangeGamePollOnServer(NetworkCommunicator pollCreatorPeer, string gameType, string scene)
		{
			if (this._ongoingPoll == null)
			{
				List<NetworkCommunicator> participantsToVote = GameNetwork.NetworkPeers.ToList<NetworkCommunicator>();
				this._ongoingPoll = new MultiplayerPollComponent.ChangeGamePoll(this._missionLobbyComponent.MissionType, participantsToVote, gameType, scene);
				if (GameNetwork.IsServer)
				{
					MultiplayerPollComponent.MultiplayerPoll ongoingPoll = this._ongoingPoll;
					ongoingPoll.OnClosedOnServer = (Action<MultiplayerPollComponent.MultiplayerPoll>)Delegate.Combine(ongoingPoll.OnClosedOnServer, new Action<MultiplayerPollComponent.MultiplayerPoll>(this.OnChangeGamePollClosedOnServer));
				}
				if (!GameNetwork.IsDedicatedServer)
				{
					this.ShowChangeGamePoll(gameType, scene);
				}
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new NetworkMessages.FromServer.ChangeGamePoll(gameType, scene));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
				return;
			}
			this.RejectPollOnServer(pollCreatorPeer, MultiplayerPollRejectReason.HasOngoingPoll);
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x0008DDEF File Offset: 0x0008BFEF
		private void StartChangeGamePoll(string gameType, string map)
		{
			if (GameNetwork.IsServer)
			{
				if (GameNetwork.MyPeer != null)
				{
					this.StartChangeGamePollOnServer(GameNetwork.MyPeer, gameType, map);
					return;
				}
			}
			else
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new NetworkMessages.FromClient.ChangeGamePoll(gameType, map));
				GameNetwork.EndModuleEventAsClient();
			}
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x0008DE23 File Offset: 0x0008C023
		private void ShowChangeGamePoll(string gameType, string scene)
		{
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x0008DE28 File Offset: 0x0008C028
		private void OnChangeGamePollClosedOnServer(MultiplayerPollComponent.MultiplayerPoll multiplayerPoll)
		{
			MultiplayerPollComponent.ChangeGamePoll changeGamePoll = multiplayerPoll as MultiplayerPollComponent.ChangeGamePoll;
			MultiplayerOptions.OptionType.GameType.SetValue(changeGamePoll.GameType, MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
			MultiplayerOptions.Instance.OnGameTypeChanged(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
			MultiplayerOptions.OptionType.Map.SetValue(changeGamePoll.MapName, MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
			this._missionLobbyComponent.SetStateEndingAsServer();
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x0008DE70 File Offset: 0x0008C070
		protected override void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegistererContainer registerer)
		{
			if (GameNetwork.IsClient)
			{
				registerer.RegisterBaseHandler<PollRequestRejected>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventPollRequestRejected));
				registerer.RegisterBaseHandler<PollProgress>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventUpdatePollProgress));
				registerer.RegisterBaseHandler<PollCancelled>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventPollCancelled));
				registerer.RegisterBaseHandler<KickPlayerPollOpened>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventKickPlayerPollOpened));
				registerer.RegisterBaseHandler<KickPlayerPollClosed>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventKickPlayerPollClosed));
				registerer.RegisterBaseHandler<NetworkMessages.FromServer.ChangeGamePoll>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventChangeGamePoll));
				return;
			}
			if (GameNetwork.IsServer)
			{
				registerer.RegisterBaseHandler<PollResponse>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventPollResponse));
				registerer.RegisterBaseHandler<KickPlayerPollRequested>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventKickPlayerPollRequested));
				registerer.RegisterBaseHandler<NetworkMessages.FromClient.ChangeGamePoll>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventChangeGamePoll));
			}
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x0008DF30 File Offset: 0x0008C130
		private bool HandleClientEventChangeGamePoll(NetworkCommunicator peer, GameNetworkMessage baseMessage)
		{
			NetworkMessages.FromClient.ChangeGamePoll changeGamePoll = (NetworkMessages.FromClient.ChangeGamePoll)baseMessage;
			this.StartChangeGamePollOnServer(peer, changeGamePoll.GameType, changeGamePoll.Map);
			return true;
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x0008DF58 File Offset: 0x0008C158
		private bool HandleClientEventKickPlayerPollRequested(NetworkCommunicator peer, GameNetworkMessage baseMessage)
		{
			KickPlayerPollRequested kickPlayerPollRequested = (KickPlayerPollRequested)baseMessage;
			this.OpenKickPlayerPollOnServer(peer, kickPlayerPollRequested.PlayerPeer, kickPlayerPollRequested.BanPlayer);
			return true;
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x0008DF80 File Offset: 0x0008C180
		private bool HandleClientEventPollResponse(NetworkCommunicator peer, GameNetworkMessage baseMessage)
		{
			PollResponse pollResponse = (PollResponse)baseMessage;
			this.ApplyVote(peer, pollResponse.Accepted);
			return true;
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x0008DFA4 File Offset: 0x0008C1A4
		private void HandleServerEventChangeGamePoll(GameNetworkMessage baseMessage)
		{
			NetworkMessages.FromServer.ChangeGamePoll changeGamePoll = (NetworkMessages.FromServer.ChangeGamePoll)baseMessage;
			this.ShowChangeGamePoll(changeGamePoll.GameType, changeGamePoll.Map);
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x0008DFCC File Offset: 0x0008C1CC
		private void HandleServerEventKickPlayerPollOpened(GameNetworkMessage baseMessage)
		{
			KickPlayerPollOpened kickPlayerPollOpened = (KickPlayerPollOpened)baseMessage;
			this.OpenKickPlayerPoll(kickPlayerPollOpened.PlayerPeer, kickPlayerPollOpened.InitiatorPeer, kickPlayerPollOpened.BanPlayer, null);
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x0008DFFC File Offset: 0x0008C1FC
		private void HandleServerEventUpdatePollProgress(GameNetworkMessage baseMessage)
		{
			PollProgress pollProgress = (PollProgress)baseMessage;
			this.UpdatePollProgress(pollProgress.VotesAccepted, pollProgress.VotesRejected);
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x0008E022 File Offset: 0x0008C222
		private void HandleServerEventPollCancelled(GameNetworkMessage baseMessage)
		{
			this.CancelPoll();
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x0008E02C File Offset: 0x0008C22C
		private void HandleServerEventKickPlayerPollClosed(GameNetworkMessage baseMessage)
		{
			KickPlayerPollClosed kickPlayerPollClosed = (KickPlayerPollClosed)baseMessage;
			this.CloseKickPlayerPoll(kickPlayerPollClosed.Accepted, kickPlayerPollClosed.PlayerPeer);
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x0008E054 File Offset: 0x0008C254
		private void HandleServerEventPollRequestRejected(GameNetworkMessage baseMessage)
		{
			PollRequestRejected pollRequestRejected = (PollRequestRejected)baseMessage;
			this.RejectPoll((MultiplayerPollRejectReason)pollRequestRejected.Reason);
		}

		// Token: 0x04000DC3 RID: 3523
		public const int MinimumParticipantCountRequired = 3;

		// Token: 0x04000DC4 RID: 3524
		public Action<MissionPeer, MissionPeer, bool> OnKickPollOpened;

		// Token: 0x04000DC5 RID: 3525
		public Action<MultiplayerPollRejectReason> OnPollRejected;

		// Token: 0x04000DC6 RID: 3526
		public Action<int, int> OnPollUpdated;

		// Token: 0x04000DC7 RID: 3527
		public Action OnPollClosed;

		// Token: 0x04000DC8 RID: 3528
		public Action OnPollCancelled;

		// Token: 0x04000DC9 RID: 3529
		private MissionLobbyComponent _missionLobbyComponent;

		// Token: 0x04000DCA RID: 3530
		private MultiplayerGameNotificationsComponent _notificationsComponent;

		// Token: 0x04000DCB RID: 3531
		private MultiplayerPollComponent.MultiplayerPoll _ongoingPoll;

		// Token: 0x02000570 RID: 1392
		private abstract class MultiplayerPoll
		{
			// Token: 0x170009A5 RID: 2469
			// (get) Token: 0x060039BF RID: 14783 RVA: 0x000E5B79 File Offset: 0x000E3D79
			public MultiplayerPollComponent.MultiplayerPoll.Type PollType { get; }

			// Token: 0x170009A6 RID: 2470
			// (get) Token: 0x060039C0 RID: 14784 RVA: 0x000E5B81 File Offset: 0x000E3D81
			// (set) Token: 0x060039C1 RID: 14785 RVA: 0x000E5B89 File Offset: 0x000E3D89
			public bool IsOpen { get; private set; }

			// Token: 0x170009A7 RID: 2471
			// (get) Token: 0x060039C2 RID: 14786 RVA: 0x000E5B92 File Offset: 0x000E3D92
			private int OpenTime { get; }

			// Token: 0x170009A8 RID: 2472
			// (get) Token: 0x060039C3 RID: 14787 RVA: 0x000E5B9A File Offset: 0x000E3D9A
			// (set) Token: 0x060039C4 RID: 14788 RVA: 0x000E5BA2 File Offset: 0x000E3DA2
			private int CloseTime { get; set; }

			// Token: 0x170009A9 RID: 2473
			// (get) Token: 0x060039C5 RID: 14789 RVA: 0x000E5BAB File Offset: 0x000E3DAB
			public List<NetworkCommunicator> ParticipantsToVote
			{
				get
				{
					return this._participantsToVote;
				}
			}

			// Token: 0x060039C6 RID: 14790 RVA: 0x000E5BB4 File Offset: 0x000E3DB4
			protected MultiplayerPoll(MultiplayerGameType gameType, MultiplayerPollComponent.MultiplayerPoll.Type pollType, List<NetworkCommunicator> participantsToVote)
			{
				this._gameType = gameType;
				this.PollType = pollType;
				if (participantsToVote != null)
				{
					this._participantsToVote = participantsToVote;
				}
				this.OpenTime = Environment.TickCount;
				this.CloseTime = 0;
				this.AcceptedCount = 0;
				this.RejectedCount = 0;
				this.IsOpen = true;
			}

			// Token: 0x060039C7 RID: 14791 RVA: 0x000E5C06 File Offset: 0x000E3E06
			public virtual bool IsCancelled()
			{
				return false;
			}

			// Token: 0x060039C8 RID: 14792 RVA: 0x000E5C09 File Offset: 0x000E3E09
			public virtual List<NetworkCommunicator> GetPollProgressReceivers()
			{
				return GameNetwork.NetworkPeers.ToList<NetworkCommunicator>();
			}

			// Token: 0x060039C9 RID: 14793 RVA: 0x000E5C18 File Offset: 0x000E3E18
			public void Tick()
			{
				if (GameNetwork.IsServer)
				{
					for (int i = this._participantsToVote.Count - 1; i >= 0; i--)
					{
						if (!this._participantsToVote[i].IsConnectionActive)
						{
							this._participantsToVote.RemoveAt(i);
						}
					}
					if (this.IsCancelled())
					{
						Action<MultiplayerPollComponent.MultiplayerPoll> onCancelledOnServer = this.OnCancelledOnServer;
						if (onCancelledOnServer == null)
						{
							return;
						}
						onCancelledOnServer(this);
						return;
					}
					else if (this.OpenTime < Environment.TickCount - 30000 || this.ResultsFinalized())
					{
						Action<MultiplayerPollComponent.MultiplayerPoll> onClosedOnServer = this.OnClosedOnServer;
						if (onClosedOnServer == null)
						{
							return;
						}
						onClosedOnServer(this);
					}
				}
			}

			// Token: 0x060039CA RID: 14794 RVA: 0x000E5CAB File Offset: 0x000E3EAB
			public void Close()
			{
				this.CloseTime = Environment.TickCount;
				this.IsOpen = false;
			}

			// Token: 0x060039CB RID: 14795 RVA: 0x000E5CBF File Offset: 0x000E3EBF
			public void Cancel()
			{
				this.Close();
			}

			// Token: 0x060039CC RID: 14796 RVA: 0x000E5CC8 File Offset: 0x000E3EC8
			public bool ApplyVote(NetworkCommunicator peer, bool accepted)
			{
				bool result = false;
				if (this._participantsToVote.Contains(peer))
				{
					if (accepted)
					{
						this.AcceptedCount++;
					}
					else
					{
						this.RejectedCount++;
					}
					this._participantsToVote.Remove(peer);
					result = true;
				}
				return result;
			}

			// Token: 0x060039CD RID: 14797 RVA: 0x000E5D18 File Offset: 0x000E3F18
			public bool GotEnoughAcceptVotesToEnd()
			{
				bool result;
				if (this._gameType == MultiplayerGameType.Skirmish || this._gameType == MultiplayerGameType.Captain)
				{
					result = this.AcceptedByAllParticipants();
				}
				else
				{
					result = this.AcceptedByMajority();
				}
				return result;
			}

			// Token: 0x060039CE RID: 14798 RVA: 0x000E5D50 File Offset: 0x000E3F50
			private bool GotEnoughRejectVotesToEnd()
			{
				bool result;
				if (this._gameType == MultiplayerGameType.Skirmish || this._gameType == MultiplayerGameType.Captain)
				{
					result = this.RejectedByAtLeastOneParticipant();
				}
				else
				{
					result = this.RejectedByMajority();
				}
				return result;
			}

			// Token: 0x060039CF RID: 14799 RVA: 0x000E5D87 File Offset: 0x000E3F87
			private bool AcceptedByAllParticipants()
			{
				return this.AcceptedCount == this.GetPollParticipantCount();
			}

			// Token: 0x060039D0 RID: 14800 RVA: 0x000E5D97 File Offset: 0x000E3F97
			private bool AcceptedByMajority()
			{
				return (float)this.AcceptedCount / (float)this.GetPollParticipantCount() > 0.50001f;
			}

			// Token: 0x060039D1 RID: 14801 RVA: 0x000E5DAF File Offset: 0x000E3FAF
			private bool RejectedByAtLeastOneParticipant()
			{
				return this.RejectedCount > 0;
			}

			// Token: 0x060039D2 RID: 14802 RVA: 0x000E5DBA File Offset: 0x000E3FBA
			private bool RejectedByMajority()
			{
				return (float)this.RejectedCount / (float)this.GetPollParticipantCount() > 0.50001f;
			}

			// Token: 0x060039D3 RID: 14803 RVA: 0x000E5DD2 File Offset: 0x000E3FD2
			private int GetPollParticipantCount()
			{
				return this._participantsToVote.Count + this.AcceptedCount + this.RejectedCount;
			}

			// Token: 0x060039D4 RID: 14804 RVA: 0x000E5DED File Offset: 0x000E3FED
			private bool ResultsFinalized()
			{
				return this.GotEnoughAcceptVotesToEnd() || this.GotEnoughRejectVotesToEnd() || this._participantsToVote.Count == 0;
			}

			// Token: 0x04001D30 RID: 7472
			private const int TimeoutInSeconds = 30;

			// Token: 0x04001D31 RID: 7473
			public Action<MultiplayerPollComponent.MultiplayerPoll> OnClosedOnServer;

			// Token: 0x04001D32 RID: 7474
			public Action<MultiplayerPollComponent.MultiplayerPoll> OnCancelledOnServer;

			// Token: 0x04001D33 RID: 7475
			public int AcceptedCount;

			// Token: 0x04001D34 RID: 7476
			public int RejectedCount;

			// Token: 0x04001D35 RID: 7477
			private readonly List<NetworkCommunicator> _participantsToVote;

			// Token: 0x04001D36 RID: 7478
			private readonly MultiplayerGameType _gameType;

			// Token: 0x02000687 RID: 1671
			public enum Type
			{
				// Token: 0x0400218F RID: 8591
				KickPlayer,
				// Token: 0x04002190 RID: 8592
				BanPlayer,
				// Token: 0x04002191 RID: 8593
				ChangeGame
			}
		}

		// Token: 0x02000571 RID: 1393
		private class KickPlayerPoll : MultiplayerPollComponent.MultiplayerPoll
		{
			// Token: 0x170009AA RID: 2474
			// (get) Token: 0x060039D5 RID: 14805 RVA: 0x000E5E0F File Offset: 0x000E400F
			public NetworkCommunicator TargetPeer { get; }

			// Token: 0x060039D6 RID: 14806 RVA: 0x000E5E17 File Offset: 0x000E4017
			public KickPlayerPoll(MultiplayerGameType gameType, List<NetworkCommunicator> participantsToVote, NetworkCommunicator targetPeer, Team team) : base(gameType, MultiplayerPollComponent.MultiplayerPoll.Type.KickPlayer, participantsToVote)
			{
				this.TargetPeer = targetPeer;
				this._team = team;
			}

			// Token: 0x060039D7 RID: 14807 RVA: 0x000E5E31 File Offset: 0x000E4031
			public override bool IsCancelled()
			{
				return !this.TargetPeer.IsConnectionActive || this.TargetPeer.QuitFromMission;
			}

			// Token: 0x060039D8 RID: 14808 RVA: 0x000E5E50 File Offset: 0x000E4050
			public override List<NetworkCommunicator> GetPollProgressReceivers()
			{
				List<NetworkCommunicator> list = new List<NetworkCommunicator>();
				foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
				{
					MissionPeer component = networkCommunicator.GetComponent<MissionPeer>();
					if (component != null && component.Team == this._team)
					{
						list.Add(networkCommunicator);
					}
				}
				return list;
			}

			// Token: 0x04001D3B RID: 7483
			public const int RequestLimitPerPeer = 2;

			// Token: 0x04001D3C RID: 7484
			private readonly Team _team;
		}

		// Token: 0x02000572 RID: 1394
		private class BanPlayerPoll : MultiplayerPollComponent.MultiplayerPoll
		{
			// Token: 0x170009AB RID: 2475
			// (get) Token: 0x060039D9 RID: 14809 RVA: 0x000E5EC4 File Offset: 0x000E40C4
			public NetworkCommunicator TargetPeer { get; }

			// Token: 0x060039DA RID: 14810 RVA: 0x000E5ECC File Offset: 0x000E40CC
			public BanPlayerPoll(MultiplayerGameType gameType, List<NetworkCommunicator> participantsToVote, NetworkCommunicator targetPeer) : base(gameType, MultiplayerPollComponent.MultiplayerPoll.Type.BanPlayer, participantsToVote)
			{
				this.TargetPeer = targetPeer;
			}
		}

		// Token: 0x02000573 RID: 1395
		private class ChangeGamePoll : MultiplayerPollComponent.MultiplayerPoll
		{
			// Token: 0x170009AC RID: 2476
			// (get) Token: 0x060039DB RID: 14811 RVA: 0x000E5EDE File Offset: 0x000E40DE
			public string GameType { get; }

			// Token: 0x170009AD RID: 2477
			// (get) Token: 0x060039DC RID: 14812 RVA: 0x000E5EE6 File Offset: 0x000E40E6
			public string MapName { get; }

			// Token: 0x060039DD RID: 14813 RVA: 0x000E5EEE File Offset: 0x000E40EE
			public ChangeGamePoll(MultiplayerGameType currentGameType, List<NetworkCommunicator> participantsToVote, string gameType, string scene) : base(currentGameType, MultiplayerPollComponent.MultiplayerPoll.Type.ChangeGame, participantsToVote)
			{
				this.GameType = gameType;
				this.MapName = scene;
			}
		}
	}
}
