using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200001A RID: 26
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class CustomGameServerListResponse : FunctionResult
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000024E4 File Offset: 0x000006E4
		// (set) Token: 0x06000073 RID: 115 RVA: 0x000024EC File Offset: 0x000006EC
		[JsonProperty]
		public AvailableCustomGames AvailableCustomGames { get; private set; }

		// Token: 0x06000074 RID: 116 RVA: 0x000024F5 File Offset: 0x000006F5
		public CustomGameServerListResponse()
		{
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000024FD File Offset: 0x000006FD
		public CustomGameServerListResponse(AvailableCustomGames availableCustomGames)
		{
			this.AvailableCustomGames = availableCustomGames;
		}
	}
}
