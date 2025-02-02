using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000063 RID: 99
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class PollRequestRejected : GameNetworkMessage
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00006A59 File Offset: 0x00004C59
		// (set) Token: 0x06000373 RID: 883 RVA: 0x00006A61 File Offset: 0x00004C61
		public int Reason { get; private set; }

		// Token: 0x06000374 RID: 884 RVA: 0x00006A6A File Offset: 0x00004C6A
		public PollRequestRejected(int reason)
		{
			this.Reason = reason;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00006A79 File Offset: 0x00004C79
		public PollRequestRejected()
		{
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00006A84 File Offset: 0x00004C84
		protected override bool OnRead()
		{
			bool result = true;
			this.Reason = GameNetworkMessage.ReadIntFromPacket(CompressionMission.MultiplayerPollRejectReasonCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00006AA6 File Offset: 0x00004CA6
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket(this.Reason, CompressionMission.MultiplayerPollRejectReasonCompressionInfo);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00006AB8 File Offset: 0x00004CB8
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00006AC0 File Offset: 0x00004CC0
		protected override string OnGetLogFormat()
		{
			return "Poll request rejected (" + (MultiplayerPollRejectReason)this.Reason + ")";
		}
	}
}
