using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000021 RID: 33
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class ApplyOrderWithFormation : GameNetworkMessage
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00003399 File Offset: 0x00001599
		// (set) Token: 0x06000101 RID: 257 RVA: 0x000033A1 File Offset: 0x000015A1
		public OrderType OrderType { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000102 RID: 258 RVA: 0x000033AA File Offset: 0x000015AA
		// (set) Token: 0x06000103 RID: 259 RVA: 0x000033B2 File Offset: 0x000015B2
		public int FormationIndex { get; private set; }

		// Token: 0x06000104 RID: 260 RVA: 0x000033BB File Offset: 0x000015BB
		public ApplyOrderWithFormation(OrderType orderType, int formationIndex)
		{
			this.OrderType = orderType;
			this.FormationIndex = formationIndex;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000033D1 File Offset: 0x000015D1
		public ApplyOrderWithFormation()
		{
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000033DC File Offset: 0x000015DC
		protected override bool OnRead()
		{
			bool result = true;
			this.OrderType = (OrderType)GameNetworkMessage.ReadIntFromPacket(CompressionMission.OrderTypeCompressionInfo, ref result);
			this.FormationIndex = GameNetworkMessage.ReadIntFromPacket(CompressionMission.FormationClassCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00003410 File Offset: 0x00001610
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket((int)this.OrderType, CompressionMission.OrderTypeCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.FormationIndex, CompressionMission.FormationClassCompressionInfo);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00003432 File Offset: 0x00001632
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Formations | MultiplayerMessageFilter.Orders;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000343A File Offset: 0x0000163A
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Apply order: ",
				this.OrderType,
				", to formation with index: ",
				this.FormationIndex
			});
		}
	}
}
