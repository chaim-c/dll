using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000007 RID: 7
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class PlayerMessageAll : GameNetworkMessage
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000212C File Offset: 0x0000032C
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002134 File Offset: 0x00000334
		public string Message { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000213D File Offset: 0x0000033D
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002145 File Offset: 0x00000345
		public List<VirtualPlayer> ReceiverList { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000214E File Offset: 0x0000034E
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002156 File Offset: 0x00000356
		public bool HasReceiverList { get; private set; }

		// Token: 0x06000019 RID: 25 RVA: 0x0000215F File Offset: 0x0000035F
		public PlayerMessageAll(string message, List<VirtualPlayer> receiverList)
		{
			this.Message = message;
			this.ReceiverList = receiverList;
			this.HasReceiverList = true;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000217C File Offset: 0x0000037C
		public PlayerMessageAll(string message)
		{
			this.Message = message;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000218B File Offset: 0x0000038B
		public PlayerMessageAll()
		{
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002194 File Offset: 0x00000394
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteStringToPacket(this.Message);
			int num = 0;
			if (this.ReceiverList != null)
			{
				num = this.ReceiverList.Count;
			}
			GameNetworkMessage.WriteBoolToPacket(this.HasReceiverList);
			GameNetworkMessage.WriteIntToPacket(num, CompressionBasic.PlayerCompressionInfo);
			for (int i = 0; i < num; i++)
			{
				GameNetworkMessage.WriteVirtualPlayerReferenceToPacket(this.ReceiverList[i]);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000021F8 File Offset: 0x000003F8
		protected override bool OnRead()
		{
			bool result = true;
			this.Message = GameNetworkMessage.ReadStringFromPacket(ref result);
			this.HasReceiverList = GameNetworkMessage.ReadBoolFromPacket(ref result);
			int num = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.PlayerCompressionInfo, ref result);
			if (this.HasReceiverList)
			{
				this.ReceiverList = new List<VirtualPlayer>();
				if (num > 0)
				{
					for (int i = 0; i < num; i++)
					{
						VirtualPlayer item = GameNetworkMessage.ReadVirtualPlayerReferenceToPacket(ref result, false);
						this.ReceiverList.Add(item);
					}
				}
			}
			return result;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002267 File Offset: 0x00000467
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Messaging;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000226B File Offset: 0x0000046B
		protected override string OnGetLogFormat()
		{
			return "Receiving Player message to all: " + this.Message;
		}
	}
}
