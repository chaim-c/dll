using System;
using System.Threading.Tasks;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000125 RID: 293
	public interface ICustomBattleServerSessionHandler
	{
		// Token: 0x06000680 RID: 1664
		void OnConnected();

		// Token: 0x06000681 RID: 1665
		void OnCantConnect();

		// Token: 0x06000682 RID: 1666
		void OnDisconnected();

		// Token: 0x06000683 RID: 1667
		void OnStateChanged(CustomBattleServer.State state);

		// Token: 0x06000684 RID: 1668
		void OnSuccessfulGameRegister();

		// Token: 0x06000685 RID: 1669
		Task<PlayerJoinGameResponseDataFromHost[]> OnClientWantsToConnectCustomGame(PlayerJoinGameData[] playerJoinData);

		// Token: 0x06000686 RID: 1670
		void OnClientQuitFromCustomGame(PlayerId playerId);

		// Token: 0x06000687 RID: 1671
		void OnGameFinished();

		// Token: 0x06000688 RID: 1672
		void OnChatFilterListsReceived(string[] profanityList, string[] allowList);

		// Token: 0x06000689 RID: 1673
		void OnPlayerKickRequested(PlayerId playerID, bool isBanning);
	}
}
