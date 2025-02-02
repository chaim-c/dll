using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200003D RID: 61
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class PlayerMessageAll : GameNetworkMessage
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00004417 File Offset: 0x00002617
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x0000441F File Offset: 0x0000261F
		public NetworkCommunicator Player { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00004428 File Offset: 0x00002628
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x00004430 File Offset: 0x00002630
		public string Message { get; private set; }

		// Token: 0x060001F5 RID: 501 RVA: 0x00004439 File Offset: 0x00002639
		public PlayerMessageAll(NetworkCommunicator player, string message)
		{
			this.Player = player;
			this.Message = message;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000444F File Offset: 0x0000264F
		public PlayerMessageAll()
		{
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00004457 File Offset: 0x00002657
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.Player);
			GameNetworkMessage.WriteStringToPacket(this.Message);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00004470 File Offset: 0x00002670
		protected override bool OnRead()
		{
			bool result = true;
			this.Player = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			this.Message = GameNetworkMessage.ReadStringFromPacket(ref result);
			return result;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000449B File Offset: 0x0000269B
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Messaging;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000449F File Offset: 0x0000269F
		protected override string OnGetLogFormat()
		{
			return "Receiving Player message to all: " + this.Message;
		}
	}
}
