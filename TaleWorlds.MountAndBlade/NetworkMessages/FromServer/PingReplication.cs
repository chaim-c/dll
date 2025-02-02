using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000D2 RID: 210
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class PingReplication : GameNetworkMessage
	{
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x0000E727 File Offset: 0x0000C927
		// (set) Token: 0x0600089D RID: 2205 RVA: 0x0000E72F File Offset: 0x0000C92F
		internal NetworkCommunicator Peer { get; private set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x0000E738 File Offset: 0x0000C938
		// (set) Token: 0x0600089F RID: 2207 RVA: 0x0000E740 File Offset: 0x0000C940
		internal int PingValue { get; private set; }

		// Token: 0x060008A0 RID: 2208 RVA: 0x0000E749 File Offset: 0x0000C949
		public PingReplication()
		{
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0000E751 File Offset: 0x0000C951
		internal PingReplication(NetworkCommunicator peer, int ping)
		{
			this.Peer = peer;
			this.PingValue = ping;
			if (this.PingValue > 1023)
			{
				this.PingValue = 1023;
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0000E780 File Offset: 0x0000C980
		protected override bool OnRead()
		{
			bool result = true;
			this.Peer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, true);
			this.PingValue = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.PingValueCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0000E7B0 File Offset: 0x0000C9B0
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.Peer);
			GameNetworkMessage.WriteIntToPacket(this.PingValue, CompressionBasic.PingValueCompressionInfo);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0000E7CD File Offset: 0x0000C9CD
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionDetailed;
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0000E7D5 File Offset: 0x0000C9D5
		protected override string OnGetLogFormat()
		{
			return "PingReplication";
		}

		// Token: 0x040001F3 RID: 499
		public const int MaxPingToReplicate = 1023;
	}
}
