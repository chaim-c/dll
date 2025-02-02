using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200004A RID: 74
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class DuelSessionStarted : GameNetworkMessage
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000276 RID: 630 RVA: 0x00004EA6 File Offset: 0x000030A6
		// (set) Token: 0x06000277 RID: 631 RVA: 0x00004EAE File Offset: 0x000030AE
		public NetworkCommunicator RequesterPeer { get; private set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000278 RID: 632 RVA: 0x00004EB7 File Offset: 0x000030B7
		// (set) Token: 0x06000279 RID: 633 RVA: 0x00004EBF File Offset: 0x000030BF
		public NetworkCommunicator RequestedPeer { get; private set; }

		// Token: 0x0600027A RID: 634 RVA: 0x00004EC8 File Offset: 0x000030C8
		public DuelSessionStarted(NetworkCommunicator requesterPeer, NetworkCommunicator requestedPeer)
		{
			this.RequesterPeer = requesterPeer;
			this.RequestedPeer = requestedPeer;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00004EDE File Offset: 0x000030DE
		public DuelSessionStarted()
		{
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00004EE8 File Offset: 0x000030E8
		protected override bool OnRead()
		{
			bool result = true;
			this.RequesterPeer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			this.RequestedPeer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			return result;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00004F14 File Offset: 0x00003114
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.RequesterPeer);
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.RequestedPeer);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00004F2C File Offset: 0x0000312C
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.GameMode;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00004F34 File Offset: 0x00003134
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Duel session started between agent with name: ",
				this.RequestedPeer.UserName,
				" and index: ",
				this.RequestedPeer.Index,
				" and agent with name: ",
				this.RequesterPeer.UserName,
				" and index: ",
				this.RequesterPeer.Index
			});
		}
	}
}
