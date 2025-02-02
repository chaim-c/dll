using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000294 RID: 660
	public class MissionCommunityClientComponent : MissionLobbyComponent
	{
		// Token: 0x06002284 RID: 8836 RVA: 0x0007D47C File Offset: 0x0007B67C
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._communityClient = NetworkMain.CommunityClient;
		}

		// Token: 0x06002285 RID: 8837 RVA: 0x0007D48F File Offset: 0x0007B68F
		public void SetServerEndingBeforeClientLoaded(bool isServerEndingBeforeClientLoaded)
		{
			this._isServerEndedBeforeClientLoaded = isServerEndingBeforeClientLoaded;
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x0007D498 File Offset: 0x0007B698
		public override void QuitMission()
		{
			base.QuitMission();
			if (!this._isServerEndedBeforeClientLoaded && base.CurrentMultiplayerState != MissionLobbyComponent.MultiplayerGameState.Ending && this._communityClient.IsInGame)
			{
				this._communityClient.QuitFromGame();
			}
		}

		// Token: 0x04000CD6 RID: 3286
		private CommunityClient _communityClient;

		// Token: 0x04000CD7 RID: 3287
		private bool _isServerEndedBeforeClientLoaded;
	}
}
