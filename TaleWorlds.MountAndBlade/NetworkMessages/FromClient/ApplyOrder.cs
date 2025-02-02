using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x0200001F RID: 31
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class ApplyOrder : GameNetworkMessage
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000EE RID: 238 RVA: 0x0000324F File Offset: 0x0000144F
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00003257 File Offset: 0x00001457
		public OrderType OrderType { get; private set; }

		// Token: 0x060000F0 RID: 240 RVA: 0x00003260 File Offset: 0x00001460
		public ApplyOrder(OrderType orderType)
		{
			this.OrderType = orderType;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000326F File Offset: 0x0000146F
		public ApplyOrder()
		{
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003278 File Offset: 0x00001478
		protected override bool OnRead()
		{
			bool result = true;
			this.OrderType = (OrderType)GameNetworkMessage.ReadIntFromPacket(CompressionMission.OrderTypeCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000329A File Offset: 0x0000149A
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket((int)this.OrderType, CompressionMission.OrderTypeCompressionInfo);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000032AC File Offset: 0x000014AC
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Orders;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000032B4 File Offset: 0x000014B4
		protected override string OnGetLogFormat()
		{
			return "Apply order: " + this.OrderType;
		}
	}
}
