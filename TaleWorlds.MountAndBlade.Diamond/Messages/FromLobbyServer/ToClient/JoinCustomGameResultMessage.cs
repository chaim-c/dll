using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200003B RID: 59
	[Serializable]
	public class JoinCustomGameResultMessage : Message
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00002B15 File Offset: 0x00000D15
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00002B1D File Offset: 0x00000D1D
		[JsonProperty]
		public JoinGameData JoinGameData { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00002B26 File Offset: 0x00000D26
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00002B2E File Offset: 0x00000D2E
		[JsonProperty]
		public bool Success { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00002B37 File Offset: 0x00000D37
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00002B3F File Offset: 0x00000D3F
		[JsonProperty]
		public CustomGameJoinResponse Response { get; private set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00002B48 File Offset: 0x00000D48
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00002B50 File Offset: 0x00000D50
		[JsonProperty]
		public string MatchId { get; private set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00002B59 File Offset: 0x00000D59
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00002B61 File Offset: 0x00000D61
		[JsonProperty]
		public bool IsAdmin { get; private set; }

		// Token: 0x06000115 RID: 277 RVA: 0x00002B6A File Offset: 0x00000D6A
		public JoinCustomGameResultMessage()
		{
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00002B72 File Offset: 0x00000D72
		private JoinCustomGameResultMessage(JoinGameData joinGameData, bool success, CustomGameJoinResponse response, string matchId, bool isAdmin)
		{
			this.JoinGameData = joinGameData;
			this.Success = success;
			this.Response = response;
			this.MatchId = matchId;
			this.IsAdmin = isAdmin;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00002B9F File Offset: 0x00000D9F
		public static JoinCustomGameResultMessage CreateSuccess(JoinGameData joinGameData, string matchId, bool isAdmin)
		{
			return new JoinCustomGameResultMessage(joinGameData, true, CustomGameJoinResponse.Success, matchId, isAdmin);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00002BAB File Offset: 0x00000DAB
		public static JoinCustomGameResultMessage CreateFailed(CustomGameJoinResponse response)
		{
			return new JoinCustomGameResultMessage(null, false, response, null, false);
		}
	}
}
