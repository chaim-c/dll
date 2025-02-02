using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200002A RID: 42
	[Serializable]
	public class GetPlayerBadgesMessageResult : FunctionResult
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x0000277A File Offset: 0x0000097A
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00002782 File Offset: 0x00000982
		[JsonProperty]
		public string[] Badges { get; private set; }

		// Token: 0x060000B5 RID: 181 RVA: 0x0000278B File Offset: 0x0000098B
		public GetPlayerBadgesMessageResult()
		{
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00002793 File Offset: 0x00000993
		public GetPlayerBadgesMessageResult(string[] badges)
		{
			this.Badges = badges;
		}
	}
}
