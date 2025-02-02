using System;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000026 RID: 38
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class ApplyOrderWithTwoPositions : GameNetworkMessage
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000134 RID: 308 RVA: 0x0000382B File Offset: 0x00001A2B
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00003833 File Offset: 0x00001A33
		public OrderType OrderType { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000383C File Offset: 0x00001A3C
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00003844 File Offset: 0x00001A44
		public Vec3 Position1 { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000138 RID: 312 RVA: 0x0000384D File Offset: 0x00001A4D
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00003855 File Offset: 0x00001A55
		public Vec3 Position2 { get; private set; }

		// Token: 0x0600013A RID: 314 RVA: 0x0000385E File Offset: 0x00001A5E
		public ApplyOrderWithTwoPositions(OrderType orderType, Vec3 position1, Vec3 position2)
		{
			this.OrderType = orderType;
			this.Position1 = position1;
			this.Position2 = position2;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000387B File Offset: 0x00001A7B
		public ApplyOrderWithTwoPositions()
		{
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00003884 File Offset: 0x00001A84
		protected override bool OnRead()
		{
			bool result = true;
			this.OrderType = (OrderType)GameNetworkMessage.ReadIntFromPacket(CompressionMission.OrderTypeCompressionInfo, ref result);
			this.Position1 = GameNetworkMessage.ReadVec3FromPacket(CompressionMission.OrderPositionCompressionInfo, ref result);
			this.Position2 = GameNetworkMessage.ReadVec3FromPacket(CompressionMission.OrderPositionCompressionInfo, ref result);
			return result;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000038CA File Offset: 0x00001ACA
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket((int)this.OrderType, CompressionMission.OrderTypeCompressionInfo);
			GameNetworkMessage.WriteVec3ToPacket(this.Position1, CompressionMission.OrderPositionCompressionInfo);
			GameNetworkMessage.WriteVec3ToPacket(this.Position2, CompressionMission.OrderPositionCompressionInfo);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000038FC File Offset: 0x00001AFC
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Orders;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00003904 File Offset: 0x00001B04
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Apply order: ",
				this.OrderType,
				", to position 1: ",
				this.Position1,
				" and position 2: ",
				this.Position2
			});
		}
	}
}
