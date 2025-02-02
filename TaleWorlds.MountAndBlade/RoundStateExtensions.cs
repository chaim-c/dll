using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000309 RID: 777
	public static class RoundStateExtensions
	{
		// Token: 0x06002A4E RID: 10830 RVA: 0x000A4A50 File Offset: 0x000A2C50
		public static bool StateHasVisualTimer(this MultiplayerRoundState roundState)
		{
			return roundState - MultiplayerRoundState.Preparation <= 1;
		}
	}
}
