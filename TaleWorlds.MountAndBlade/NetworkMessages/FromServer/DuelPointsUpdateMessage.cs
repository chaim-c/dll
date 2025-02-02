using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.MissionRepresentatives;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000046 RID: 70
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class DuelPointsUpdateMessage : GameNetworkMessage
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00004AEE File Offset: 0x00002CEE
		// (set) Token: 0x06000249 RID: 585 RVA: 0x00004AF6 File Offset: 0x00002CF6
		public int Bounty { get; private set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00004AFF File Offset: 0x00002CFF
		// (set) Token: 0x0600024B RID: 587 RVA: 0x00004B07 File Offset: 0x00002D07
		public int Score { get; private set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00004B10 File Offset: 0x00002D10
		// (set) Token: 0x0600024D RID: 589 RVA: 0x00004B18 File Offset: 0x00002D18
		public int NumberOfWins { get; private set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00004B21 File Offset: 0x00002D21
		// (set) Token: 0x0600024F RID: 591 RVA: 0x00004B29 File Offset: 0x00002D29
		public NetworkCommunicator NetworkCommunicator { get; private set; }

		// Token: 0x06000250 RID: 592 RVA: 0x00004B32 File Offset: 0x00002D32
		public DuelPointsUpdateMessage()
		{
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00004B3A File Offset: 0x00002D3A
		public DuelPointsUpdateMessage(DuelMissionRepresentative representative)
		{
			this.Bounty = representative.Bounty;
			this.Score = representative.Score;
			this.NumberOfWins = representative.NumberOfWins;
			this.NetworkCommunicator = representative.GetNetworkPeer();
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00004B72 File Offset: 0x00002D72
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket(this.Bounty, CompressionMatchmaker.ScoreCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.Score, CompressionMatchmaker.ScoreCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.NumberOfWins, CompressionMatchmaker.KillDeathAssistCountCompressionInfo);
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.NetworkCommunicator);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00004BB0 File Offset: 0x00002DB0
		protected override bool OnRead()
		{
			bool result = true;
			this.Bounty = GameNetworkMessage.ReadIntFromPacket(CompressionMatchmaker.ScoreCompressionInfo, ref result);
			this.Score = GameNetworkMessage.ReadIntFromPacket(CompressionMatchmaker.ScoreCompressionInfo, ref result);
			this.NumberOfWins = GameNetworkMessage.ReadIntFromPacket(CompressionMatchmaker.KillDeathAssistCountCompressionInfo, ref result);
			this.NetworkCommunicator = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			return result;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00004C04 File Offset: 0x00002E04
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.GameMode;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00004C0C File Offset: 0x00002E0C
		protected override string OnGetLogFormat()
		{
			return "PointUpdateMessage";
		}
	}
}
