using System;
using TaleWorlds.MountAndBlade.Diamond;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000295 RID: 661
	public class MissionCustomGameClientComponent : MissionLobbyComponent
	{
		// Token: 0x06002288 RID: 8840 RVA: 0x0007D4D1 File Offset: 0x0007B6D1
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._lobbyClient = NetworkMain.GameClient;
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x0007D4E4 File Offset: 0x0007B6E4
		public void SetServerEndingBeforeClientLoaded(bool isServerEndingBeforeClientLoaded)
		{
			this._isServerEndedBeforeClientLoaded = isServerEndingBeforeClientLoaded;
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x0007D4F0 File Offset: 0x0007B6F0
		public override void QuitMission()
		{
			base.QuitMission();
			if (GameNetwork.IsServer)
			{
				if (base.CurrentMultiplayerState != MissionLobbyComponent.MultiplayerGameState.Ending && this._lobbyClient.LoggedIn && this._lobbyClient.CurrentState == LobbyClient.State.HostingCustomGame)
				{
					this._lobbyClient.EndCustomGame();
					return;
				}
			}
			else if (!this._isServerEndedBeforeClientLoaded && base.CurrentMultiplayerState != MissionLobbyComponent.MultiplayerGameState.Ending && this._lobbyClient.LoggedIn && this._lobbyClient.CurrentState == LobbyClient.State.InCustomGame)
			{
				this._lobbyClient.QuitFromCustomGame();
			}
		}

		// Token: 0x04000CD8 RID: 3288
		private LobbyClient _lobbyClient;

		// Token: 0x04000CD9 RID: 3289
		private bool _isServerEndedBeforeClientLoaded;
	}
}
