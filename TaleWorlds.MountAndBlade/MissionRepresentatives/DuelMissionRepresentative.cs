using System;
using System.Collections.Generic;
using System.Linq;
using NetworkMessages.FromClient;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace TaleWorlds.MountAndBlade.MissionRepresentatives
{
	// Token: 0x020003A1 RID: 929
	public class DuelMissionRepresentative : MissionRepresentativeBase
	{
		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x0600325A RID: 12890 RVA: 0x000D02B3 File Offset: 0x000CE4B3
		// (set) Token: 0x0600325B RID: 12891 RVA: 0x000D02BB File Offset: 0x000CE4BB
		public int Bounty { get; private set; }

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x0600325C RID: 12892 RVA: 0x000D02C4 File Offset: 0x000CE4C4
		// (set) Token: 0x0600325D RID: 12893 RVA: 0x000D02CC File Offset: 0x000CE4CC
		public int Score { get; private set; }

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x0600325E RID: 12894 RVA: 0x000D02D5 File Offset: 0x000CE4D5
		// (set) Token: 0x0600325F RID: 12895 RVA: 0x000D02DD File Offset: 0x000CE4DD
		public int NumberOfWins { get; private set; }

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06003260 RID: 12896 RVA: 0x000D02E6 File Offset: 0x000CE4E6
		private bool _isInDuel
		{
			get
			{
				return base.MissionPeer != null && base.MissionPeer.Team != null && base.MissionPeer.Team.IsDefender;
			}
		}

		// Token: 0x06003261 RID: 12897 RVA: 0x000D030F File Offset: 0x000CE50F
		public override void Initialize()
		{
			this._requesters = new List<Tuple<MissionPeer, MissionTime>>();
			if (GameNetwork.IsServerOrRecorder)
			{
				this._missionMultiplayerDuel = Mission.Current.GetMissionBehavior<MissionMultiplayerDuel>();
			}
			Mission.Current.SetMissionMode(MissionMode.Duel, true);
		}

		// Token: 0x06003262 RID: 12898 RVA: 0x000D0340 File Offset: 0x000CE540
		public void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode mode)
		{
			if (GameNetwork.IsClient)
			{
				GameNetwork.NetworkMessageHandlerRegisterer networkMessageHandlerRegisterer = new GameNetwork.NetworkMessageHandlerRegisterer(mode);
				networkMessageHandlerRegisterer.Register<NetworkMessages.FromServer.DuelRequest>(new GameNetworkMessage.ServerMessageHandlerDelegate<NetworkMessages.FromServer.DuelRequest>(this.HandleServerEventDuelRequest));
				networkMessageHandlerRegisterer.Register<DuelSessionStarted>(new GameNetworkMessage.ServerMessageHandlerDelegate<DuelSessionStarted>(this.HandleServerEventDuelSessionStarted));
				networkMessageHandlerRegisterer.Register<DuelPreparationStartedForTheFirstTime>(new GameNetworkMessage.ServerMessageHandlerDelegate<DuelPreparationStartedForTheFirstTime>(this.HandleServerEventDuelStarted));
				networkMessageHandlerRegisterer.Register<DuelEnded>(new GameNetworkMessage.ServerMessageHandlerDelegate<DuelEnded>(this.HandleServerEventDuelEnded));
				networkMessageHandlerRegisterer.Register<DuelRoundEnded>(new GameNetworkMessage.ServerMessageHandlerDelegate<DuelRoundEnded>(this.HandleServerEventDuelRoundEnded));
				networkMessageHandlerRegisterer.Register<DuelPointsUpdateMessage>(new GameNetworkMessage.ServerMessageHandlerDelegate<DuelPointsUpdateMessage>(this.HandleServerPointUpdate));
			}
		}

		// Token: 0x06003263 RID: 12899 RVA: 0x000D03C8 File Offset: 0x000CE5C8
		public void OnInteraction()
		{
			if (this._focusedObject != null)
			{
				DuelZoneLandmark duelZoneLandmark;
				Agent focusedAgent;
				if ((focusedAgent = (this._focusedObject as Agent)) != null)
				{
					if (focusedAgent.IsActive())
					{
						if (this._requesters.Any((Tuple<MissionPeer, MissionTime> req) => req.Item1 == focusedAgent.MissionPeer))
						{
							for (int i = 0; i < this._requesters.Count; i++)
							{
								if (this._requesters[i].Item1 == base.MissionPeer)
								{
									this._requesters.Remove(this._requesters[i]);
									break;
								}
							}
							MissionRepresentativeBase.PlayerTypes playerType = base.PlayerType;
							if (playerType == MissionRepresentativeBase.PlayerTypes.Client)
							{
								GameNetwork.BeginModuleEventAsClient();
								GameNetwork.WriteMessage(new DuelResponse(focusedAgent.MissionRepresentative.Peer.Communicator as NetworkCommunicator, true));
								GameNetwork.EndModuleEventAsClient();
								return;
							}
							if (playerType != MissionRepresentativeBase.PlayerTypes.Server)
							{
								return;
							}
							this._missionMultiplayerDuel.DuelRequestAccepted(focusedAgent, base.ControlledAgent);
							return;
						}
						else
						{
							MissionRepresentativeBase.PlayerTypes playerType = base.PlayerType;
							if (playerType == MissionRepresentativeBase.PlayerTypes.Client)
							{
								Action<MissionPeer> onDuelRequestSentEvent = this.OnDuelRequestSentEvent;
								if (onDuelRequestSentEvent != null)
								{
									onDuelRequestSentEvent(focusedAgent.MissionPeer);
								}
								GameNetwork.BeginModuleEventAsClient();
								GameNetwork.WriteMessage(new NetworkMessages.FromClient.DuelRequest(focusedAgent.Index));
								GameNetwork.EndModuleEventAsClient();
								return;
							}
							if (playerType != MissionRepresentativeBase.PlayerTypes.Server)
							{
								return;
							}
							this._missionMultiplayerDuel.DuelRequestReceived(base.MissionPeer, focusedAgent.MissionPeer);
							return;
						}
					}
				}
				else if ((duelZoneLandmark = (this._focusedObject as DuelZoneLandmark)) != null)
				{
					if (this._isInDuel)
					{
						InformationManager.DisplayMessage(new InformationMessage(new TextObject("{=v5EqMSlD}Can't change arena preference while in duel.", null).ToString()));
						return;
					}
					GameNetwork.BeginModuleEventAsClient();
					GameNetwork.WriteMessage(new RequestChangePreferredTroopType(duelZoneLandmark.ZoneTroopType));
					GameNetwork.EndModuleEventAsClient();
					Action<TroopType> onMyPreferredZoneChanged = this.OnMyPreferredZoneChanged;
					if (onMyPreferredZoneChanged == null)
					{
						return;
					}
					onMyPreferredZoneChanged(duelZoneLandmark.ZoneTroopType);
				}
			}
		}

		// Token: 0x06003264 RID: 12900 RVA: 0x000D059C File Offset: 0x000CE79C
		private void HandleServerEventDuelRequest(NetworkMessages.FromServer.DuelRequest message)
		{
			Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(message.RequesterAgentIndex, false);
			Mission.MissionNetworkHelper.GetAgentFromIndex(message.RequestedAgentIndex, false);
			this.DuelRequested(agentFromIndex, message.SelectedAreaTroopType);
		}

		// Token: 0x06003265 RID: 12901 RVA: 0x000D05D0 File Offset: 0x000CE7D0
		private void HandleServerEventDuelSessionStarted(DuelSessionStarted message)
		{
			this.OnDuelPreparation(message.RequesterPeer.GetComponent<MissionPeer>(), message.RequestedPeer.GetComponent<MissionPeer>());
		}

		// Token: 0x06003266 RID: 12902 RVA: 0x000D05F0 File Offset: 0x000CE7F0
		private void HandleServerEventDuelStarted(DuelPreparationStartedForTheFirstTime message)
		{
			MissionPeer component = message.RequesterPeer.GetComponent<MissionPeer>();
			MissionPeer component2 = message.RequesteePeer.GetComponent<MissionPeer>();
			Action<MissionPeer, MissionPeer, int> onDuelPreparationStartedForTheFirstTimeEvent = this.OnDuelPreparationStartedForTheFirstTimeEvent;
			if (onDuelPreparationStartedForTheFirstTimeEvent == null)
			{
				return;
			}
			onDuelPreparationStartedForTheFirstTimeEvent(component, component2, message.AreaIndex);
		}

		// Token: 0x06003267 RID: 12903 RVA: 0x000D062D File Offset: 0x000CE82D
		private void HandleServerEventDuelEnded(DuelEnded message)
		{
			Action<MissionPeer> onDuelEndedEvent = this.OnDuelEndedEvent;
			if (onDuelEndedEvent == null)
			{
				return;
			}
			onDuelEndedEvent(message.WinnerPeer.GetComponent<MissionPeer>());
		}

		// Token: 0x06003268 RID: 12904 RVA: 0x000D064A File Offset: 0x000CE84A
		private void HandleServerEventDuelRoundEnded(DuelRoundEnded message)
		{
			Action<MissionPeer> onDuelRoundEndedEvent = this.OnDuelRoundEndedEvent;
			if (onDuelRoundEndedEvent == null)
			{
				return;
			}
			onDuelRoundEndedEvent(message.WinnerPeer.GetComponent<MissionPeer>());
		}

		// Token: 0x06003269 RID: 12905 RVA: 0x000D0667 File Offset: 0x000CE867
		private void HandleServerPointUpdate(DuelPointsUpdateMessage message)
		{
			DuelMissionRepresentative component = message.NetworkCommunicator.GetComponent<DuelMissionRepresentative>();
			component.Bounty = message.Bounty;
			component.Score = message.Score;
			component.NumberOfWins = message.NumberOfWins;
		}

		// Token: 0x0600326A RID: 12906 RVA: 0x000D0698 File Offset: 0x000CE898
		public void DuelRequested(Agent requesterAgent, TroopType selectedAreaTroopType)
		{
			this._requesters.Add(new Tuple<MissionPeer, MissionTime>(requesterAgent.MissionPeer, MissionTime.Now + MissionTime.Seconds(10f)));
			switch (base.PlayerType)
			{
			case MissionRepresentativeBase.PlayerTypes.Bot:
				this._missionMultiplayerDuel.DuelRequestAccepted(requesterAgent, base.ControlledAgent);
				return;
			case MissionRepresentativeBase.PlayerTypes.Client:
			{
				if (!base.IsMine)
				{
					GameNetwork.BeginModuleEventAsServer(base.Peer);
					GameNetwork.WriteMessage(new NetworkMessages.FromServer.DuelRequest(requesterAgent.Index, base.ControlledAgent.Index, selectedAreaTroopType));
					GameNetwork.EndModuleEventAsServer();
					return;
				}
				Action<MissionPeer, TroopType> onDuelRequestedEvent = this.OnDuelRequestedEvent;
				if (onDuelRequestedEvent == null)
				{
					return;
				}
				onDuelRequestedEvent(requesterAgent.MissionPeer, selectedAreaTroopType);
				return;
			}
			case MissionRepresentativeBase.PlayerTypes.Server:
			{
				Action<MissionPeer, TroopType> onDuelRequestedEvent2 = this.OnDuelRequestedEvent;
				if (onDuelRequestedEvent2 == null)
				{
					return;
				}
				onDuelRequestedEvent2(requesterAgent.MissionPeer, selectedAreaTroopType);
				return;
			}
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x0600326B RID: 12907 RVA: 0x000D0768 File Offset: 0x000CE968
		public bool CheckHasRequestFromAndRemoveRequestIfNeeded(MissionPeer requestOwner)
		{
			if (requestOwner != null && requestOwner.Representative == this)
			{
				this._requesters.Clear();
				return false;
			}
			Tuple<MissionPeer, MissionTime> tuple = this._requesters.FirstOrDefault((Tuple<MissionPeer, MissionTime> req) => req.Item1 == requestOwner);
			if (tuple == null)
			{
				return false;
			}
			if (requestOwner.ControlledAgent == null || !requestOwner.ControlledAgent.IsActive())
			{
				this._requesters.Remove(tuple);
				return false;
			}
			if (!tuple.Item2.IsPast)
			{
				return true;
			}
			this._requesters.Remove(tuple);
			return false;
		}

		// Token: 0x0600326C RID: 12908 RVA: 0x000D0810 File Offset: 0x000CEA10
		public void OnDuelPreparation(MissionPeer requesterPeer, MissionPeer requesteePeer)
		{
			MissionRepresentativeBase.PlayerTypes playerType = base.PlayerType;
			if (playerType != MissionRepresentativeBase.PlayerTypes.Client)
			{
				if (playerType == MissionRepresentativeBase.PlayerTypes.Server)
				{
					Action<MissionPeer, int> onDuelPrepStartedEvent = this.OnDuelPrepStartedEvent;
					if (onDuelPrepStartedEvent != null)
					{
						onDuelPrepStartedEvent((base.MissionPeer == requesterPeer) ? requesteePeer : requesterPeer, 3);
					}
				}
			}
			else if (base.IsMine)
			{
				Action<MissionPeer, int> onDuelPrepStartedEvent2 = this.OnDuelPrepStartedEvent;
				if (onDuelPrepStartedEvent2 != null)
				{
					onDuelPrepStartedEvent2((base.MissionPeer == requesterPeer) ? requesteePeer : requesterPeer, 3);
				}
			}
			else
			{
				GameNetwork.BeginModuleEventAsServer(base.Peer);
				GameNetwork.WriteMessage(new DuelSessionStarted(requesterPeer.GetNetworkPeer(), requesteePeer.GetNetworkPeer()));
				GameNetwork.EndModuleEventAsServer();
			}
			Tuple<MissionPeer, MissionTime> tuple = this._requesters.FirstOrDefault((Tuple<MissionPeer, MissionTime> req) => req.Item1 == requesterPeer);
			if (tuple != null)
			{
				this._requesters.Remove(tuple);
			}
		}

		// Token: 0x0600326D RID: 12909 RVA: 0x000D08EF File Offset: 0x000CEAEF
		public void OnObjectFocused(IFocusable focusedObject)
		{
			this._focusedObject = focusedObject;
		}

		// Token: 0x0600326E RID: 12910 RVA: 0x000D08F8 File Offset: 0x000CEAF8
		public void OnObjectFocusLost()
		{
			this._focusedObject = null;
		}

		// Token: 0x0600326F RID: 12911 RVA: 0x000D0901 File Offset: 0x000CEB01
		public override void OnAgentSpawned()
		{
			if (base.ControlledAgent.Team != null && base.ControlledAgent.Team.Side == BattleSideEnum.Attacker)
			{
				Action onAgentSpawnedWithoutDuelEvent = this.OnAgentSpawnedWithoutDuelEvent;
				if (onAgentSpawnedWithoutDuelEvent == null)
				{
					return;
				}
				onAgentSpawnedWithoutDuelEvent();
			}
		}

		// Token: 0x06003270 RID: 12912 RVA: 0x000D0933 File Offset: 0x000CEB33
		public void ResetBountyAndNumberOfWins()
		{
			this.Bounty = 0;
			this.NumberOfWins = 0;
		}

		// Token: 0x06003271 RID: 12913 RVA: 0x000D0944 File Offset: 0x000CEB44
		public void OnDuelWon(float gainedScore)
		{
			this.Bounty += (int)(gainedScore / 5f);
			this.Score += (int)gainedScore;
			int numberOfWins = this.NumberOfWins;
			this.NumberOfWins = numberOfWins + 1;
		}

		// Token: 0x040015B0 RID: 5552
		public const int DuelPrepTime = 3;

		// Token: 0x040015B1 RID: 5553
		public Action<MissionPeer, TroopType> OnDuelRequestedEvent;

		// Token: 0x040015B2 RID: 5554
		public Action<MissionPeer> OnDuelRequestSentEvent;

		// Token: 0x040015B3 RID: 5555
		public Action<MissionPeer, int> OnDuelPrepStartedEvent;

		// Token: 0x040015B4 RID: 5556
		public Action OnAgentSpawnedWithoutDuelEvent;

		// Token: 0x040015B5 RID: 5557
		public Action<MissionPeer, MissionPeer, int> OnDuelPreparationStartedForTheFirstTimeEvent;

		// Token: 0x040015B6 RID: 5558
		public Action<MissionPeer> OnDuelEndedEvent;

		// Token: 0x040015B7 RID: 5559
		public Action<MissionPeer> OnDuelRoundEndedEvent;

		// Token: 0x040015B8 RID: 5560
		public Action<TroopType> OnMyPreferredZoneChanged;

		// Token: 0x040015B9 RID: 5561
		private List<Tuple<MissionPeer, MissionTime>> _requesters;

		// Token: 0x040015BA RID: 5562
		private MissionMultiplayerDuel _missionMultiplayerDuel;

		// Token: 0x040015BB RID: 5563
		private IFocusable _focusedObject;
	}
}
