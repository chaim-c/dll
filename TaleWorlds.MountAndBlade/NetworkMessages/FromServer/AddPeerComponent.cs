using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200003C RID: 60
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class AddPeerComponent : GameNetworkMessage
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00004326 File Offset: 0x00002526
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x0000432E File Offset: 0x0000252E
		public NetworkCommunicator Peer { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00004337 File Offset: 0x00002537
		// (set) Token: 0x060001EA RID: 490 RVA: 0x0000433F File Offset: 0x0000253F
		public uint ComponentId { get; private set; }

		// Token: 0x060001EB RID: 491 RVA: 0x00004348 File Offset: 0x00002548
		public AddPeerComponent(NetworkCommunicator peer, uint componentId)
		{
			this.Peer = peer;
			this.ComponentId = componentId;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000435E File Offset: 0x0000255E
		public AddPeerComponent()
		{
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00004366 File Offset: 0x00002566
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.Peer);
			GameNetworkMessage.WriteUintToPacket(this.ComponentId, CompressionBasic.PeerComponentCompressionInfo);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00004384 File Offset: 0x00002584
		protected override bool OnRead()
		{
			bool result = true;
			this.Peer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			this.ComponentId = GameNetworkMessage.ReadUintFromPacket(CompressionBasic.PeerComponentCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000043B4 File Offset: 0x000025B4
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Peers;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000043B8 File Offset: 0x000025B8
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Add component with ID: ",
				this.ComponentId,
				" to peer:",
				this.Peer.UserName,
				" with peer-index:",
				this.Peer.Index
			});
		}
	}
}
