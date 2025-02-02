using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000D8 RID: 216
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SendVoiceToPlay : GameNetworkMessage
	{
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x0000EBF7 File Offset: 0x0000CDF7
		// (set) Token: 0x060008D1 RID: 2257 RVA: 0x0000EBFF File Offset: 0x0000CDFF
		public NetworkCommunicator Peer { get; private set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x0000EC08 File Offset: 0x0000CE08
		// (set) Token: 0x060008D3 RID: 2259 RVA: 0x0000EC10 File Offset: 0x0000CE10
		public byte[] Buffer { get; private set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x0000EC19 File Offset: 0x0000CE19
		// (set) Token: 0x060008D5 RID: 2261 RVA: 0x0000EC21 File Offset: 0x0000CE21
		public int BufferLength { get; private set; }

		// Token: 0x060008D6 RID: 2262 RVA: 0x0000EC2A File Offset: 0x0000CE2A
		public SendVoiceToPlay()
		{
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0000EC32 File Offset: 0x0000CE32
		public SendVoiceToPlay(NetworkCommunicator peer, byte[] buffer, int bufferLength)
		{
			this.Peer = peer;
			this.Buffer = buffer;
			this.BufferLength = bufferLength;
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0000EC4F File Offset: 0x0000CE4F
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.Peer);
			GameNetworkMessage.WriteByteArrayToPacket(this.Buffer, 0, this.BufferLength);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0000EC70 File Offset: 0x0000CE70
		protected override bool OnRead()
		{
			bool result = true;
			this.Peer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			this.Buffer = new byte[1440];
			this.BufferLength = GameNetworkMessage.ReadByteArrayFromPacket(this.Buffer, 0, 1440, ref result);
			return result;
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0000ECB7 File Offset: 0x0000CEB7
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.None;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0000ECBB File Offset: 0x0000CEBB
		protected override string OnGetLogFormat()
		{
			return string.Empty;
		}
	}
}
