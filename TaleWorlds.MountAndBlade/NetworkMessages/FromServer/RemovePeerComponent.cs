using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000D6 RID: 214
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class RemovePeerComponent : GameNetworkMessage
	{
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x0000EA08 File Offset: 0x0000CC08
		// (set) Token: 0x060008BD RID: 2237 RVA: 0x0000EA10 File Offset: 0x0000CC10
		public NetworkCommunicator Peer { get; private set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x0000EA19 File Offset: 0x0000CC19
		// (set) Token: 0x060008BF RID: 2239 RVA: 0x0000EA21 File Offset: 0x0000CC21
		public uint ComponentId { get; private set; }

		// Token: 0x060008C0 RID: 2240 RVA: 0x0000EA2A File Offset: 0x0000CC2A
		public RemovePeerComponent(NetworkCommunicator peer, uint componentId)
		{
			this.Peer = peer;
			this.ComponentId = componentId;
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0000EA40 File Offset: 0x0000CC40
		public RemovePeerComponent()
		{
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0000EA48 File Offset: 0x0000CC48
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.Peer);
			GameNetworkMessage.WriteUintToPacket(this.ComponentId, CompressionBasic.PeerComponentCompressionInfo);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0000EA68 File Offset: 0x0000CC68
		protected override bool OnRead()
		{
			bool result = true;
			this.Peer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			this.ComponentId = GameNetworkMessage.ReadUintFromPacket(CompressionBasic.PeerComponentCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0000EA98 File Offset: 0x0000CC98
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Peers;
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0000EA9C File Offset: 0x0000CC9C
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Remove component with ID: ",
				this.ComponentId,
				" from peer: ",
				this.Peer.UserName,
				" with peer-index: ",
				this.Peer.Index
			});
		}
	}
}
