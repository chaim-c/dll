using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200004E RID: 78
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class BotsControlledChange : GameNetworkMessage
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x000053C8 File Offset: 0x000035C8
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x000053D0 File Offset: 0x000035D0
		public NetworkCommunicator Peer { get; private set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x000053D9 File Offset: 0x000035D9
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x000053E1 File Offset: 0x000035E1
		public int AliveCount { get; private set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x000053EA File Offset: 0x000035EA
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x000053F2 File Offset: 0x000035F2
		public int TotalCount { get; private set; }

		// Token: 0x060002AA RID: 682 RVA: 0x000053FB File Offset: 0x000035FB
		public BotsControlledChange(NetworkCommunicator peer, int aliveCount, int totalCount)
		{
			this.Peer = peer;
			this.AliveCount = aliveCount;
			this.TotalCount = totalCount;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00005418 File Offset: 0x00003618
		public BotsControlledChange()
		{
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00005420 File Offset: 0x00003620
		protected override bool OnRead()
		{
			bool result = true;
			this.Peer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			this.AliveCount = GameNetworkMessage.ReadIntFromPacket(CompressionMission.AgentOffsetCompressionInfo, ref result);
			this.TotalCount = GameNetworkMessage.ReadIntFromPacket(CompressionMission.AgentOffsetCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00005462 File Offset: 0x00003662
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.Peer);
			GameNetworkMessage.WriteIntToPacket(this.AliveCount, CompressionMission.AgentOffsetCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.TotalCount, CompressionMission.AgentOffsetCompressionInfo);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000548F File Offset: 0x0000368F
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00005498 File Offset: 0x00003698
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Bot Controlled Count Changed. Peer: ",
				this.Peer.UserName,
				" now has ",
				this.AliveCount,
				" alive bots, out of: ",
				this.TotalCount,
				" total bots."
			});
		}
	}
}
