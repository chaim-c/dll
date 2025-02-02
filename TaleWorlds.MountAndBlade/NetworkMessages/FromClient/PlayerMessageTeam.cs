using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000008 RID: 8
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class PlayerMessageTeam : GameNetworkMessage
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000227D File Offset: 0x0000047D
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002285 File Offset: 0x00000485
		public string Message { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000228E File Offset: 0x0000048E
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002296 File Offset: 0x00000496
		public List<VirtualPlayer> ReceiverList { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000229F File Offset: 0x0000049F
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000022A7 File Offset: 0x000004A7
		public bool HasReceiverList { get; private set; }

		// Token: 0x06000026 RID: 38 RVA: 0x000022B0 File Offset: 0x000004B0
		public PlayerMessageTeam(string message, List<VirtualPlayer> receiverList)
		{
			this.Message = message;
			this.ReceiverList = receiverList;
			this.HasReceiverList = true;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000022CD File Offset: 0x000004CD
		public PlayerMessageTeam(string message)
		{
			this.Message = message;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000022DC File Offset: 0x000004DC
		public PlayerMessageTeam()
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000022E4 File Offset: 0x000004E4
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

		// Token: 0x0600002A RID: 42 RVA: 0x00002348 File Offset: 0x00000548
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

		// Token: 0x0600002B RID: 43 RVA: 0x000023B7 File Offset: 0x000005B7
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Messaging;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000023BB File Offset: 0x000005BB
		protected override string OnGetLogFormat()
		{
			return "Receiving Player message to team: " + this.Message;
		}
	}
}
