using System;
using Galaxy.Api;
using TaleWorlds.Library;

namespace TaleWorlds.PlatformService.GOG
{
	// Token: 0x0200000C RID: 12
	public class GogServicesConnectionStateListener : GlobalGogServicesConnectionStateListener
	{
		// Token: 0x0600007C RID: 124 RVA: 0x000031C7 File Offset: 0x000013C7
		public override void OnConnectionStateChange(GogServicesConnectionState connected)
		{
			Debug.Print("Connection state to GOG services changed to " + connected, 0, Debug.DebugColor.White, 17592186044416UL);
		}
	}
}
