using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000040 RID: 64
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class ServerMessage : GameNetworkMessage
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00004630 File Offset: 0x00002830
		// (set) Token: 0x06000210 RID: 528 RVA: 0x00004638 File Offset: 0x00002838
		public string Message { get; private set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00004641 File Offset: 0x00002841
		// (set) Token: 0x06000212 RID: 530 RVA: 0x00004649 File Offset: 0x00002849
		public bool IsMessageTextId { get; private set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00004652 File Offset: 0x00002852
		// (set) Token: 0x06000214 RID: 532 RVA: 0x0000465A File Offset: 0x0000285A
		public bool IsAdminAnnouncement { get; private set; }

		// Token: 0x06000215 RID: 533 RVA: 0x00004663 File Offset: 0x00002863
		public ServerMessage(string message, bool isMessageTextId = false, bool isAdminAnnouncement = false)
		{
			this.Message = message;
			this.IsMessageTextId = isMessageTextId;
			this.IsAdminAnnouncement = isAdminAnnouncement;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00004680 File Offset: 0x00002880
		public ServerMessage()
		{
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00004688 File Offset: 0x00002888
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteStringToPacket(this.Message);
			GameNetworkMessage.WriteBoolToPacket(this.IsMessageTextId);
			GameNetworkMessage.WriteBoolToPacket(this.IsAdminAnnouncement);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000046AC File Offset: 0x000028AC
		protected override bool OnRead()
		{
			bool result = true;
			this.Message = GameNetworkMessage.ReadStringFromPacket(ref result);
			this.IsMessageTextId = GameNetworkMessage.ReadBoolFromPacket(ref result);
			this.IsAdminAnnouncement = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000046E3 File Offset: 0x000028E3
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Messaging;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000046E7 File Offset: 0x000028E7
		protected override string OnGetLogFormat()
		{
			return "Message from server: " + this.Message;
		}
	}
}
