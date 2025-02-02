using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x0200003B RID: 59
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class SendVoiceRecord : GameNetworkMessage
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x000041A1 File Offset: 0x000023A1
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x000041A9 File Offset: 0x000023A9
		public byte[] Buffer { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001DA RID: 474 RVA: 0x000041B2 File Offset: 0x000023B2
		// (set) Token: 0x060001DB RID: 475 RVA: 0x000041BA File Offset: 0x000023BA
		public int BufferLength { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001DC RID: 476 RVA: 0x000041C3 File Offset: 0x000023C3
		// (set) Token: 0x060001DD RID: 477 RVA: 0x000041CB File Offset: 0x000023CB
		public List<VirtualPlayer> ReceiverList { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001DE RID: 478 RVA: 0x000041D4 File Offset: 0x000023D4
		// (set) Token: 0x060001DF RID: 479 RVA: 0x000041DC File Offset: 0x000023DC
		public bool HasReceiverList { get; private set; }

		// Token: 0x060001E0 RID: 480 RVA: 0x000041E5 File Offset: 0x000023E5
		public SendVoiceRecord()
		{
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000041ED File Offset: 0x000023ED
		public SendVoiceRecord(byte[] buffer, int bufferLength)
		{
			this.Buffer = buffer;
			this.BufferLength = bufferLength;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00004203 File Offset: 0x00002403
		public SendVoiceRecord(byte[] buffer, int bufferLength, List<VirtualPlayer> receiverList)
		{
			this.Buffer = buffer;
			this.BufferLength = bufferLength;
			this.ReceiverList = receiverList;
			this.HasReceiverList = true;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00004228 File Offset: 0x00002428
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteByteArrayToPacket(this.Buffer, 0, this.BufferLength);
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

		// Token: 0x060001E4 RID: 484 RVA: 0x00004290 File Offset: 0x00002490
		protected override bool OnRead()
		{
			bool result = true;
			this.Buffer = new byte[1440];
			this.BufferLength = GameNetworkMessage.ReadByteArrayFromPacket(this.Buffer, 0, 1440, ref result);
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

		// Token: 0x060001E5 RID: 485 RVA: 0x0000431B File Offset: 0x0000251B
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.None;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000431F File Offset: 0x0000251F
		protected override string OnGetLogFormat()
		{
			return string.Empty;
		}
	}
}
