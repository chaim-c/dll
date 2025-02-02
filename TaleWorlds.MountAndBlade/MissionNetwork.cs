using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200028B RID: 651
	public abstract class MissionNetwork : MissionLogic, IUdpNetworkHandler
	{
		// Token: 0x0600220A RID: 8714 RVA: 0x0007CA84 File Offset: 0x0007AC84
		public override void OnAfterMissionCreated()
		{
			this._missionNetworkMessageHandlerRegisterer = new GameNetwork.NetworkMessageHandlerRegistererContainer();
			this.AddRemoveMessageHandlers(this._missionNetworkMessageHandlerRegisterer);
			this._missionNetworkMessageHandlerRegisterer.RegisterMessages();
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x0007CAA8 File Offset: 0x0007ACA8
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			GameNetwork.AddNetworkHandler(this);
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x0007CAB6 File Offset: 0x0007ACB6
		public override void OnRemoveBehavior()
		{
			GameNetwork.RemoveNetworkHandler(this);
			base.OnRemoveBehavior();
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x0007CAC4 File Offset: 0x0007ACC4
		protected virtual void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegistererContainer registerer)
		{
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x0007CAC6 File Offset: 0x0007ACC6
		public virtual void OnPlayerConnectedToServer(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x0007CAC8 File Offset: 0x0007ACC8
		public virtual void OnPlayerDisconnectedFromServer(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x0007CACA File Offset: 0x0007ACCA
		void IUdpNetworkHandler.OnUdpNetworkHandlerTick(float dt)
		{
			this.OnUdpNetworkHandlerTick();
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x0007CAD2 File Offset: 0x0007ACD2
		void IUdpNetworkHandler.OnUdpNetworkHandlerClose()
		{
			this.OnUdpNetworkHandlerClose();
			GameNetwork.NetworkMessageHandlerRegistererContainer missionNetworkMessageHandlerRegisterer = this._missionNetworkMessageHandlerRegisterer;
			if (missionNetworkMessageHandlerRegisterer == null)
			{
				return;
			}
			missionNetworkMessageHandlerRegisterer.UnregisterMessages();
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x0007CAEA File Offset: 0x0007ACEA
		void IUdpNetworkHandler.HandleNewClientConnect(PlayerConnectionInfo clientConnectionInfo)
		{
			this.HandleNewClientConnect(clientConnectionInfo);
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x0007CAF3 File Offset: 0x0007ACF3
		void IUdpNetworkHandler.HandleEarlyNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
			this.HandleEarlyNewClientAfterLoadingFinished(networkPeer);
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x0007CAFC File Offset: 0x0007ACFC
		void IUdpNetworkHandler.HandleNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
			this.HandleNewClientAfterLoadingFinished(networkPeer);
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x0007CB05 File Offset: 0x0007AD05
		void IUdpNetworkHandler.HandleLateNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
			this.HandleLateNewClientAfterLoadingFinished(networkPeer);
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x0007CB0E File Offset: 0x0007AD0E
		void IUdpNetworkHandler.HandleNewClientAfterSynchronized(NetworkCommunicator networkPeer)
		{
			this.HandleNewClientAfterSynchronized(networkPeer);
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x0007CB17 File Offset: 0x0007AD17
		void IUdpNetworkHandler.HandleLateNewClientAfterSynchronized(NetworkCommunicator networkPeer)
		{
			this.HandleLateNewClientAfterSynchronized(networkPeer);
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x0007CB20 File Offset: 0x0007AD20
		void IUdpNetworkHandler.HandleEarlyPlayerDisconnect(NetworkCommunicator networkPeer)
		{
			this.HandleEarlyPlayerDisconnect(networkPeer);
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x0007CB29 File Offset: 0x0007AD29
		void IUdpNetworkHandler.HandlePlayerDisconnect(NetworkCommunicator networkPeer)
		{
			this.HandlePlayerDisconnect(networkPeer);
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x0007CB32 File Offset: 0x0007AD32
		void IUdpNetworkHandler.OnEveryoneUnSynchronized()
		{
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x0007CB34 File Offset: 0x0007AD34
		void IUdpNetworkHandler.OnPlayerDisconnectedFromServer(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x0007CB36 File Offset: 0x0007AD36
		void IUdpNetworkHandler.OnDisconnectedFromServer()
		{
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x0007CB38 File Offset: 0x0007AD38
		protected virtual void OnUdpNetworkHandlerTick()
		{
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x0007CB3A File Offset: 0x0007AD3A
		protected virtual void OnUdpNetworkHandlerClose()
		{
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x0007CB3C File Offset: 0x0007AD3C
		protected virtual void HandleNewClientConnect(PlayerConnectionInfo clientConnectionInfo)
		{
		}

		// Token: 0x06002220 RID: 8736 RVA: 0x0007CB3E File Offset: 0x0007AD3E
		protected virtual void HandleEarlyNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x0007CB40 File Offset: 0x0007AD40
		protected virtual void HandleNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x0007CB42 File Offset: 0x0007AD42
		protected virtual void HandleLateNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x0007CB44 File Offset: 0x0007AD44
		protected virtual void HandleNewClientAfterSynchronized(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x0007CB46 File Offset: 0x0007AD46
		protected virtual void HandleLateNewClientAfterSynchronized(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x0007CB48 File Offset: 0x0007AD48
		protected virtual void HandleEarlyPlayerDisconnect(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x0007CB4A File Offset: 0x0007AD4A
		protected virtual void HandlePlayerDisconnect(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x04000CC5 RID: 3269
		private GameNetwork.NetworkMessageHandlerRegistererContainer _missionNetworkMessageHandlerRegisterer;
	}
}
