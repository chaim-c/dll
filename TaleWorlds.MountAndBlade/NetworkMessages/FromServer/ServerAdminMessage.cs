using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200003F RID: 63
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class ServerAdminMessage : GameNetworkMessage
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00004596 File Offset: 0x00002796
		// (set) Token: 0x06000206 RID: 518 RVA: 0x0000459E File Offset: 0x0000279E
		public string Message { get; private set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000207 RID: 519 RVA: 0x000045A7 File Offset: 0x000027A7
		// (set) Token: 0x06000208 RID: 520 RVA: 0x000045AF File Offset: 0x000027AF
		public bool IsAdminBroadcast { get; private set; }

		// Token: 0x06000209 RID: 521 RVA: 0x000045B8 File Offset: 0x000027B8
		public ServerAdminMessage(string message, bool isAdminBroadcast)
		{
			this.Message = message;
			this.IsAdminBroadcast = isAdminBroadcast;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000045CE File Offset: 0x000027CE
		public ServerAdminMessage()
		{
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000045D6 File Offset: 0x000027D6
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteStringToPacket(this.Message);
			GameNetworkMessage.WriteBoolToPacket(this.IsAdminBroadcast);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000045F0 File Offset: 0x000027F0
		protected override bool OnRead()
		{
			bool result = true;
			this.Message = GameNetworkMessage.ReadStringFromPacket(ref result);
			this.IsAdminBroadcast = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000461A File Offset: 0x0000281A
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Messaging;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000461E File Offset: 0x0000281E
		protected override string OnGetLogFormat()
		{
			return "Admin message from server: " + this.Message;
		}
	}
}
