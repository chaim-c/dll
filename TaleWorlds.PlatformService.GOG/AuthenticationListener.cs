using System;
using Galaxy.Api;
using TaleWorlds.Library;

namespace TaleWorlds.PlatformService.GOG
{
	// Token: 0x0200000A RID: 10
	public class AuthenticationListener : GlobalAuthListener
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003109 File Offset: 0x00001309
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00003111 File Offset: 0x00001311
		public bool GotResult { get; private set; }

		// Token: 0x06000073 RID: 115 RVA: 0x0000311A File Offset: 0x0000131A
		public AuthenticationListener(GOGPlatformServices gogPlatformServices)
		{
			this._gogPlatformServices = gogPlatformServices;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003129 File Offset: 0x00001329
		public override void OnAuthSuccess()
		{
			Debug.Print("Successfully signed in", 0, Debug.DebugColor.White, 17592186044416UL);
			GalaxyInstance.User().GetGalaxyID();
			this.GotResult = true;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003153 File Offset: 0x00001353
		public override void OnAuthFailure(IAuthListener.FailureReason failureReason)
		{
			Debug.Print("Failed to sign in for reason " + failureReason, 0, Debug.DebugColor.White, 17592186044416UL);
			this.GotResult = true;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000317D File Offset: 0x0000137D
		public override void OnAuthLost()
		{
			Debug.Print("Authorization lost", 0, Debug.DebugColor.White, 17592186044416UL);
			this.GotResult = true;
		}

		// Token: 0x04000020 RID: 32
		private GOGPlatformServices _gogPlatformServices;
	}
}
