using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000315 RID: 789
	public interface IUdpNetworkHandler
	{
		// Token: 0x06002AC4 RID: 10948
		void OnUdpNetworkHandlerClose();

		// Token: 0x06002AC5 RID: 10949
		void OnUdpNetworkHandlerTick(float dt);

		// Token: 0x06002AC6 RID: 10950
		void HandleNewClientConnect(PlayerConnectionInfo clientConnectionInfo);

		// Token: 0x06002AC7 RID: 10951
		void HandleEarlyNewClientAfterLoadingFinished(NetworkCommunicator networkPeer);

		// Token: 0x06002AC8 RID: 10952
		void HandleNewClientAfterLoadingFinished(NetworkCommunicator networkPeer);

		// Token: 0x06002AC9 RID: 10953
		void HandleLateNewClientAfterLoadingFinished(NetworkCommunicator networkPeer);

		// Token: 0x06002ACA RID: 10954
		void HandleNewClientAfterSynchronized(NetworkCommunicator networkPeer);

		// Token: 0x06002ACB RID: 10955
		void HandleLateNewClientAfterSynchronized(NetworkCommunicator networkPeer);

		// Token: 0x06002ACC RID: 10956
		void HandleEarlyPlayerDisconnect(NetworkCommunicator networkPeer);

		// Token: 0x06002ACD RID: 10957
		void HandlePlayerDisconnect(NetworkCommunicator networkPeer);

		// Token: 0x06002ACE RID: 10958
		void OnPlayerDisconnectedFromServer(NetworkCommunicator networkPeer);

		// Token: 0x06002ACF RID: 10959
		void OnDisconnectedFromServer();

		// Token: 0x06002AD0 RID: 10960
		void OnEveryoneUnSynchronized();
	}
}
