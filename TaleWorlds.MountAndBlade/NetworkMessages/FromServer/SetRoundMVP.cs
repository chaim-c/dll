using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000B2 RID: 178
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetRoundMVP : GameNetworkMessage
	{
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x0000C629 File Offset: 0x0000A829
		// (set) Token: 0x0600072B RID: 1835 RVA: 0x0000C631 File Offset: 0x0000A831
		public NetworkCommunicator MVPPeer { get; private set; }

		// Token: 0x0600072C RID: 1836 RVA: 0x0000C63A File Offset: 0x0000A83A
		public SetRoundMVP(NetworkCommunicator mvpPeer, int mvpCount)
		{
			this.MVPPeer = mvpPeer;
			this.MVPCount = mvpCount;
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0000C650 File Offset: 0x0000A850
		public SetRoundMVP()
		{
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0000C658 File Offset: 0x0000A858
		protected override bool OnRead()
		{
			bool result = true;
			this.MVPPeer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			this.MVPCount = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.RoundTotalCompressionInfo, ref result);
			return result;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0000C688 File Offset: 0x0000A888
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.MVPPeer);
			GameNetworkMessage.WriteIntToPacket(this.MVPCount, CompressionBasic.RoundTotalCompressionInfo);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0000C6A5 File Offset: 0x0000A8A5
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Mission | MultiplayerMessageFilter.GameMode;
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0000C6AD File Offset: 0x0000A8AD
		protected override string OnGetLogFormat()
		{
			return "MVP selected as: " + this.MVPPeer.UserName + ".";
		}

		// Token: 0x04000199 RID: 409
		public int MVPCount;
	}
}
