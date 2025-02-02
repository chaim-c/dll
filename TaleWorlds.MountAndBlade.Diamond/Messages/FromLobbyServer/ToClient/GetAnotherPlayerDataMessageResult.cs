using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000020 RID: 32
	[Serializable]
	public class GetAnotherPlayerDataMessageResult : FunctionResult
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000025E4 File Offset: 0x000007E4
		// (set) Token: 0x0600008C RID: 140 RVA: 0x000025EC File Offset: 0x000007EC
		[JsonProperty]
		public PlayerData AnotherPlayerData { get; private set; }

		// Token: 0x0600008D RID: 141 RVA: 0x000025F5 File Offset: 0x000007F5
		public GetAnotherPlayerDataMessageResult()
		{
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000025FD File Offset: 0x000007FD
		public GetAnotherPlayerDataMessageResult(PlayerData playerData)
		{
			this.AnotherPlayerData = playerData;
		}
	}
}
