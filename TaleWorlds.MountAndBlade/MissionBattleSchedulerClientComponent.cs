using System;
using TaleWorlds.MountAndBlade.Diamond;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000293 RID: 659
	public class MissionBattleSchedulerClientComponent : MissionLobbyComponent
	{
		// Token: 0x06002282 RID: 8834 RVA: 0x0007D43F File Offset: 0x0007B63F
		public override void QuitMission()
		{
			base.QuitMission();
			if (base.CurrentMultiplayerState != MissionLobbyComponent.MultiplayerGameState.Ending && NetworkMain.GameClient.LoggedIn && NetworkMain.GameClient.CurrentState == LobbyClient.State.AtBattle)
			{
				NetworkMain.GameClient.QuitFromMatchmakerGame();
			}
		}
	}
}
