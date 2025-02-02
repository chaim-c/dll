using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000041 RID: 65
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class MatchmakerDisabledMessage : Message
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00002CF8 File Offset: 0x00000EF8
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00002D00 File Offset: 0x00000F00
		[JsonProperty]
		public int RemainingTime { get; private set; }

		// Token: 0x06000138 RID: 312 RVA: 0x00002D09 File Offset: 0x00000F09
		public MatchmakerDisabledMessage()
		{
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00002D11 File Offset: 0x00000F11
		public MatchmakerDisabledMessage(int remainingTime)
		{
			this.RemainingTime = remainingTime;
		}
	}
}
