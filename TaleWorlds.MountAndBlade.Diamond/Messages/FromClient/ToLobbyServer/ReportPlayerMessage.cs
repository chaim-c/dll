using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000C0 RID: 192
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class ReportPlayerMessage : Message
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000373 RID: 883 RVA: 0x000045C0 File Offset: 0x000027C0
		// (set) Token: 0x06000374 RID: 884 RVA: 0x000045C8 File Offset: 0x000027C8
		[JsonProperty]
		public Guid GameId { get; private set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000375 RID: 885 RVA: 0x000045D1 File Offset: 0x000027D1
		// (set) Token: 0x06000376 RID: 886 RVA: 0x000045D9 File Offset: 0x000027D9
		[JsonProperty]
		public PlayerId ReportedPlayerId { get; private set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000377 RID: 887 RVA: 0x000045E2 File Offset: 0x000027E2
		// (set) Token: 0x06000378 RID: 888 RVA: 0x000045EA File Offset: 0x000027EA
		[JsonProperty]
		public string ReportedPlayerName { get; private set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000379 RID: 889 RVA: 0x000045F3 File Offset: 0x000027F3
		// (set) Token: 0x0600037A RID: 890 RVA: 0x000045FB File Offset: 0x000027FB
		[JsonProperty]
		public PlayerReportType Type { get; private set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00004604 File Offset: 0x00002804
		// (set) Token: 0x0600037C RID: 892 RVA: 0x0000460C File Offset: 0x0000280C
		[JsonProperty]
		public string Message { get; private set; }

		// Token: 0x0600037D RID: 893 RVA: 0x00004615 File Offset: 0x00002815
		public ReportPlayerMessage()
		{
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000461D File Offset: 0x0000281D
		public ReportPlayerMessage(Guid gameId, PlayerId reportedPlayerId, string reportedPlayerName, PlayerReportType type, string message)
		{
			this.GameId = gameId;
			this.ReportedPlayerId = reportedPlayerId;
			this.ReportedPlayerName = reportedPlayerName;
			this.Type = type;
			this.Message = message;
		}
	}
}
