using System;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000025 RID: 37
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class ApplyOrderWithPosition : GameNetworkMessage
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00003752 File Offset: 0x00001952
		// (set) Token: 0x0600012B RID: 299 RVA: 0x0000375A File Offset: 0x0000195A
		public OrderType OrderType { get; private set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00003763 File Offset: 0x00001963
		// (set) Token: 0x0600012D RID: 301 RVA: 0x0000376B File Offset: 0x0000196B
		public Vec3 Position { get; private set; }

		// Token: 0x0600012E RID: 302 RVA: 0x00003774 File Offset: 0x00001974
		public ApplyOrderWithPosition(OrderType orderType, Vec3 position)
		{
			this.OrderType = orderType;
			this.Position = position;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000378A File Offset: 0x0000198A
		public ApplyOrderWithPosition()
		{
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00003794 File Offset: 0x00001994
		protected override bool OnRead()
		{
			bool result = true;
			this.OrderType = (OrderType)GameNetworkMessage.ReadIntFromPacket(CompressionMission.OrderTypeCompressionInfo, ref result);
			this.Position = GameNetworkMessage.ReadVec3FromPacket(CompressionMission.OrderPositionCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000037C8 File Offset: 0x000019C8
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket((int)this.OrderType, CompressionMission.OrderTypeCompressionInfo);
			GameNetworkMessage.WriteVec3ToPacket(this.Position, CompressionMission.OrderPositionCompressionInfo);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000037EA File Offset: 0x000019EA
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Orders;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000037F2 File Offset: 0x000019F2
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Apply order: ",
				this.OrderType,
				", to position: ",
				this.Position
			});
		}
	}
}
