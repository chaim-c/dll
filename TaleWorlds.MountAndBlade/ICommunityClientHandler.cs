using System;
using TaleWorlds.MountAndBlade.Diamond;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002D5 RID: 725
	public interface ICommunityClientHandler
	{
		// Token: 0x060027E8 RID: 10216
		void OnJoinCustomGameResponse(string address, int port, PlayerJoinGameResponseDataFromHost response);

		// Token: 0x060027E9 RID: 10217
		void OnQuitFromGame();
	}
}
