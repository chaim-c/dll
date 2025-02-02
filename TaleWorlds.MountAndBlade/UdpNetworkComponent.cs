using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000314 RID: 788
	public abstract class UdpNetworkComponent : IUdpNetworkHandler
	{
		// Token: 0x06002AB5 RID: 10933 RVA: 0x000A5564 File Offset: 0x000A3764
		protected UdpNetworkComponent()
		{
			this._missionNetworkMessageHandlerRegisterer = new GameNetwork.NetworkMessageHandlerRegistererContainer();
			this.AddRemoveMessageHandlers(this._missionNetworkMessageHandlerRegisterer);
			this._missionNetworkMessageHandlerRegisterer.RegisterMessages();
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x000A558E File Offset: 0x000A378E
		protected virtual void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegistererContainer registerer)
		{
		}

		// Token: 0x06002AB7 RID: 10935 RVA: 0x000A5590 File Offset: 0x000A3790
		public virtual void OnUdpNetworkHandlerClose()
		{
			GameNetwork.NetworkMessageHandlerRegistererContainer missionNetworkMessageHandlerRegisterer = this._missionNetworkMessageHandlerRegisterer;
			if (missionNetworkMessageHandlerRegisterer != null)
			{
				missionNetworkMessageHandlerRegisterer.UnregisterMessages();
			}
			GameNetwork.NetworkComponents.Remove(this);
		}

		// Token: 0x06002AB8 RID: 10936 RVA: 0x000A55AF File Offset: 0x000A37AF
		public virtual void OnUdpNetworkHandlerTick(float dt)
		{
		}

		// Token: 0x06002AB9 RID: 10937 RVA: 0x000A55B1 File Offset: 0x000A37B1
		public virtual void HandleNewClientConnect(PlayerConnectionInfo clientConnectionInfo)
		{
		}

		// Token: 0x06002ABA RID: 10938 RVA: 0x000A55B3 File Offset: 0x000A37B3
		public virtual void HandleEarlyNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x06002ABB RID: 10939 RVA: 0x000A55B5 File Offset: 0x000A37B5
		public virtual void HandleNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x06002ABC RID: 10940 RVA: 0x000A55B7 File Offset: 0x000A37B7
		public virtual void HandleLateNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x06002ABD RID: 10941 RVA: 0x000A55B9 File Offset: 0x000A37B9
		public virtual void HandleNewClientAfterSynchronized(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x06002ABE RID: 10942 RVA: 0x000A55BB File Offset: 0x000A37BB
		public virtual void HandleLateNewClientAfterSynchronized(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x06002ABF RID: 10943 RVA: 0x000A55BD File Offset: 0x000A37BD
		public virtual void OnEveryoneUnSynchronized()
		{
		}

		// Token: 0x06002AC0 RID: 10944 RVA: 0x000A55BF File Offset: 0x000A37BF
		public void HandleEarlyPlayerDisconnect(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x000A55C1 File Offset: 0x000A37C1
		public virtual void HandlePlayerDisconnect(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x000A55C3 File Offset: 0x000A37C3
		public virtual void OnPlayerDisconnectedFromServer(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x000A55C5 File Offset: 0x000A37C5
		public virtual void OnDisconnectedFromServer()
		{
		}

		// Token: 0x04001071 RID: 4209
		private GameNetwork.NetworkMessageHandlerRegistererContainer _missionNetworkMessageHandlerRegisterer;
	}
}
