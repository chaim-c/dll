using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000062 RID: 98
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class PollProgress : GameNetworkMessage
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000368 RID: 872 RVA: 0x000069B3 File Offset: 0x00004BB3
		// (set) Token: 0x06000369 RID: 873 RVA: 0x000069BB File Offset: 0x00004BBB
		public int VotesAccepted { get; private set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600036A RID: 874 RVA: 0x000069C4 File Offset: 0x00004BC4
		// (set) Token: 0x0600036B RID: 875 RVA: 0x000069CC File Offset: 0x00004BCC
		public int VotesRejected { get; private set; }

		// Token: 0x0600036C RID: 876 RVA: 0x000069D5 File Offset: 0x00004BD5
		public PollProgress(int votesAccepted, int votesRejected)
		{
			this.VotesAccepted = votesAccepted;
			this.VotesRejected = votesRejected;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x000069EB File Offset: 0x00004BEB
		public PollProgress()
		{
		}

		// Token: 0x0600036E RID: 878 RVA: 0x000069F4 File Offset: 0x00004BF4
		protected override bool OnRead()
		{
			bool result = true;
			this.VotesAccepted = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.PlayerCompressionInfo, ref result);
			this.VotesRejected = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.PlayerCompressionInfo, ref result);
			return result;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00006A28 File Offset: 0x00004C28
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket(this.VotesAccepted, CompressionBasic.PlayerCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.VotesRejected, CompressionBasic.PlayerCompressionInfo);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00006A4A File Offset: 0x00004C4A
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00006A52 File Offset: 0x00004C52
		protected override string OnGetLogFormat()
		{
			return "Update on the voting progress.";
		}
	}
}
