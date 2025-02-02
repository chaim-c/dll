using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002E3 RID: 739
	public interface IGameNetworkHandler
	{
		// Token: 0x06002878 RID: 10360
		void OnNewPlayerConnect(PlayerConnectionInfo playerConnectionInfo, NetworkCommunicator networkPeer);

		// Token: 0x06002879 RID: 10361
		void OnInitialize();

		// Token: 0x0600287A RID: 10362
		void OnPlayerConnectedToServer(NetworkCommunicator peer);

		// Token: 0x0600287B RID: 10363
		void OnPlayerDisconnectedFromServer(NetworkCommunicator peer);

		// Token: 0x0600287C RID: 10364
		void OnDisconnectedFromServer();

		// Token: 0x0600287D RID: 10365
		void OnStartMultiplayer();

		// Token: 0x0600287E RID: 10366
		void OnStartReplay();

		// Token: 0x0600287F RID: 10367
		void OnEndMultiplayer();

		// Token: 0x06002880 RID: 10368
		void OnEndReplay();

		// Token: 0x06002881 RID: 10369
		void OnHandleConsoleCommand(string command);
	}
}
