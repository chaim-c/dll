using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000123 RID: 291
	public interface IBattleServerSessionHandler
	{
		// Token: 0x06000677 RID: 1655
		void OnConnected();

		// Token: 0x06000678 RID: 1656
		void OnCantConnect();

		// Token: 0x06000679 RID: 1657
		void OnDisconnected();

		// Token: 0x0600067A RID: 1658
		void OnNewPlayer(BattlePeer peer);

		// Token: 0x0600067B RID: 1659
		void OnStartGame(string sceneName, string gameType, string faction1, string faction2, int minRequiredPlayerCountToStartBattle, int battleSize, string[] profanityList, string[] allowList);

		// Token: 0x0600067C RID: 1660
		void OnPlayerFledBattle(BattlePeer peer, out BattleResult battleResult);

		// Token: 0x0600067D RID: 1661
		void OnEndMission();

		// Token: 0x0600067E RID: 1662
		void OnStopServer();
	}
}
