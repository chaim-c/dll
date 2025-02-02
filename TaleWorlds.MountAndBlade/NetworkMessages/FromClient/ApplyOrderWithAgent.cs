using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000020 RID: 32
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class ApplyOrderWithAgent : GameNetworkMessage
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000032CB File Offset: 0x000014CB
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x000032D3 File Offset: 0x000014D3
		public OrderType OrderType { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000032DC File Offset: 0x000014DC
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x000032E4 File Offset: 0x000014E4
		public int AgentIndex { get; private set; }

		// Token: 0x060000FA RID: 250 RVA: 0x000032ED File Offset: 0x000014ED
		public ApplyOrderWithAgent(OrderType orderType, int agentIndex)
		{
			this.OrderType = orderType;
			this.AgentIndex = agentIndex;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00003303 File Offset: 0x00001503
		public ApplyOrderWithAgent()
		{
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000330C File Offset: 0x0000150C
		protected override bool OnRead()
		{
			bool result = true;
			this.OrderType = (OrderType)GameNetworkMessage.ReadIntFromPacket(CompressionMission.OrderTypeCompressionInfo, ref result);
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			return result;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000333B File Offset: 0x0000153B
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket((int)this.OrderType, CompressionMission.OrderTypeCompressionInfo);
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00003358 File Offset: 0x00001558
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.AgentsDetailed | MultiplayerMessageFilter.Orders;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00003360 File Offset: 0x00001560
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Apply order: ",
				this.OrderType,
				", to agent with index: ",
				this.AgentIndex
			});
		}
	}
}
