using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x0200000B RID: 11
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class DuelResponse : GameNetworkMessage
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000256D File Offset: 0x0000076D
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002575 File Offset: 0x00000775
		public NetworkCommunicator Peer { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000041 RID: 65 RVA: 0x0000257E File Offset: 0x0000077E
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002586 File Offset: 0x00000786
		public bool Accepted { get; private set; }

		// Token: 0x06000043 RID: 67 RVA: 0x0000258F File Offset: 0x0000078F
		public DuelResponse(NetworkCommunicator peer, bool accepted)
		{
			this.Peer = peer;
			this.Accepted = accepted;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000025A5 File Offset: 0x000007A5
		public DuelResponse()
		{
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000025B0 File Offset: 0x000007B0
		protected override bool OnRead()
		{
			bool result = true;
			this.Peer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			this.Accepted = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000025DB File Offset: 0x000007DB
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.Peer);
			GameNetworkMessage.WriteBoolToPacket(this.Accepted);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000025F3 File Offset: 0x000007F3
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.GameMode;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000025FB File Offset: 0x000007FB
		protected override string OnGetLogFormat()
		{
			return "Duel Response: " + (this.Accepted ? " Accepted" : " Not accepted");
		}
	}
}
