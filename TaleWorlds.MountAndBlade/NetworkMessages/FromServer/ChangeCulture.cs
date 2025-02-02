using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.ObjectSystem;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000050 RID: 80
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class ChangeCulture : GameNetworkMessage
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002BA RID: 698 RVA: 0x000055B2 File Offset: 0x000037B2
		// (set) Token: 0x060002BB RID: 699 RVA: 0x000055BA File Offset: 0x000037BA
		public NetworkCommunicator Peer { get; private set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002BC RID: 700 RVA: 0x000055C3 File Offset: 0x000037C3
		// (set) Token: 0x060002BD RID: 701 RVA: 0x000055CB File Offset: 0x000037CB
		public BasicCultureObject Culture { get; private set; }

		// Token: 0x060002BE RID: 702 RVA: 0x000055D4 File Offset: 0x000037D4
		public ChangeCulture()
		{
		}

		// Token: 0x060002BF RID: 703 RVA: 0x000055DC File Offset: 0x000037DC
		public ChangeCulture(MissionPeer peer, BasicCultureObject culture)
		{
			this.Peer = peer.GetNetworkPeer();
			this.Culture = culture;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x000055F7 File Offset: 0x000037F7
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.Peer);
			GameNetworkMessage.WriteObjectReferenceToPacket(this.Culture, CompressionBasic.GUIDCompressionInfo);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00005614 File Offset: 0x00003814
		protected override bool OnRead()
		{
			bool result = true;
			this.Peer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			this.Culture = (BasicCultureObject)GameNetworkMessage.ReadObjectReferenceFromPacket(MBObjectManager.Instance, CompressionBasic.GUIDCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000564E File Offset: 0x0000384E
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Mission;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00005656 File Offset: 0x00003856
		protected override string OnGetLogFormat()
		{
			return "Requested culture: " + this.Culture.Name;
		}
	}
}
