using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200002B RID: 43
	[Serializable]
	public class GetPlayerByUsernameAndIdMessageResult : FunctionResult
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000027A2 File Offset: 0x000009A2
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x000027AA File Offset: 0x000009AA
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x060000B9 RID: 185 RVA: 0x000027B3 File Offset: 0x000009B3
		public GetPlayerByUsernameAndIdMessageResult()
		{
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000027BB File Offset: 0x000009BB
		public GetPlayerByUsernameAndIdMessageResult(PlayerId playerId)
		{
			this.PlayerId = playerId;
		}
	}
}
