using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000005 RID: 5
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class AdminRequestAnnouncement : GameNetworkMessage
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002058 File Offset: 0x00000258
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002060 File Offset: 0x00000260
		public string Message { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002069 File Offset: 0x00000269
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002071 File Offset: 0x00000271
		public bool IsAdminBroadcast { get; private set; }

		// Token: 0x06000008 RID: 8 RVA: 0x0000207A File Offset: 0x0000027A
		public AdminRequestAnnouncement(string message, bool isAdminBroadcast)
		{
			this.Message = message;
			this.IsAdminBroadcast = isAdminBroadcast;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002090 File Offset: 0x00000290
		public AdminRequestAnnouncement()
		{
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002098 File Offset: 0x00000298
		protected override bool OnRead()
		{
			bool result = true;
			this.Message = GameNetworkMessage.ReadStringFromPacket(ref result);
			this.IsAdminBroadcast = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020C2 File Offset: 0x000002C2
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteStringToPacket(this.Message);
			GameNetworkMessage.WriteBoolToPacket(this.IsAdminBroadcast);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020DA File Offset: 0x000002DA
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Messaging;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000020E0 File Offset: 0x000002E0
		protected override string OnGetLogFormat()
		{
			return "AdminRequestAnnouncement: " + this.Message + " " + this.IsAdminBroadcast.ToString();
		}
	}
}
