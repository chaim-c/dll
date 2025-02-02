using System;
using System.Collections.Generic;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002EF RID: 751
	public class LobbyNetworkComponent : UdpNetworkComponent
	{
		// Token: 0x060028E2 RID: 10466 RVA: 0x0009D0D2 File Offset: 0x0009B2D2
		protected override void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegistererContainer registerer)
		{
			if (GameNetwork.IsClientOrReplay)
			{
				registerer.RegisterBaseHandler<InitializeLobbyPeer>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventInitializeLobbyPeer));
			}
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x0009D0F0 File Offset: 0x0009B2F0
		private void HandleServerEventInitializeLobbyPeer(GameNetworkMessage baseMessage)
		{
			InitializeLobbyPeer initializeLobbyPeer = (InitializeLobbyPeer)baseMessage;
			NetworkCommunicator peer = initializeLobbyPeer.Peer;
			VirtualPlayer virtualPlayer = peer.VirtualPlayer;
			virtualPlayer.Id = initializeLobbyPeer.ProvidedId;
			virtualPlayer.IsFemale = initializeLobbyPeer.IsFemale;
			virtualPlayer.BannerCode = initializeLobbyPeer.BannerCode;
			virtualPlayer.BodyProperties = initializeLobbyPeer.BodyProperties;
			virtualPlayer.ChosenBadgeIndex = initializeLobbyPeer.ChosenBadgeIndex;
			peer.ForcedAvatarIndex = initializeLobbyPeer.ForcedAvatarIndex;
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x0009D158 File Offset: 0x0009B358
		public override void HandleEarlyNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
			PlayerData parameter = networkPeer.PlayerConnectionInfo.GetParameter<PlayerData>("PlayerData");
			Dictionary<int, List<int>> parameter2 = networkPeer.PlayerConnectionInfo.GetParameter<Dictionary<int, List<int>>>("UsedCosmetics");
			VirtualPlayer virtualPlayer = networkPeer.VirtualPlayer;
			virtualPlayer.Id = parameter.PlayerId;
			virtualPlayer.BannerCode = parameter.Sigil;
			virtualPlayer.BodyProperties = parameter.BodyProperties;
			virtualPlayer.IsFemale = parameter.IsFemale;
			virtualPlayer.ChosenBadgeIndex = parameter.ShownBadgeIndex;
			virtualPlayer.UsedCosmetics = parameter2;
			networkPeer.IsMuted = parameter.IsMuted;
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x0009D1DC File Offset: 0x0009B3DC
		public override void HandleNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
			VirtualPlayer virtualPlayer = networkPeer.VirtualPlayer;
			GameNetwork.BeginBroadcastModuleEvent();
			GameNetwork.WriteMessage(new InitializeLobbyPeer(networkPeer, virtualPlayer, -1));
			GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord | GameNetwork.EventBroadcastFlags.DontSendToPeers, null);
			foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeersIncludingDisconnectedPeers)
			{
				if (networkCommunicator.IsSynchronized || networkCommunicator == networkPeer)
				{
					bool flag = GameNetwork.VirtualPlayers[networkCommunicator.VirtualPlayer.Index] != networkCommunicator.VirtualPlayer;
					if (networkCommunicator != networkPeer && !flag && networkCommunicator != GameNetwork.MyPeer)
					{
						GameNetwork.BeginModuleEventAsServer(networkCommunicator);
						GameNetwork.WriteMessage(new InitializeLobbyPeer(networkPeer, virtualPlayer, -1));
						GameNetwork.EndModuleEventAsServer();
					}
					if (!networkPeer.IsServerPeer)
					{
						GameNetwork.BeginModuleEventAsServer(networkPeer);
						GameNetwork.WriteMessage(new InitializeLobbyPeer(networkCommunicator, networkCommunicator.VirtualPlayer, -1));
						GameNetwork.EndModuleEventAsServer();
					}
				}
			}
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x0009D2C0 File Offset: 0x0009B4C0
		public override void HandleLateNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x0009D2C2 File Offset: 0x0009B4C2
		public override void HandlePlayerDisconnect(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x0009D2C4 File Offset: 0x0009B4C4
		public override void OnUdpNetworkHandlerTick(float dt)
		{
		}

		// Token: 0x04000F6E RID: 3950
		public const int MaxForcedAvatarIndex = 100;
	}
}
