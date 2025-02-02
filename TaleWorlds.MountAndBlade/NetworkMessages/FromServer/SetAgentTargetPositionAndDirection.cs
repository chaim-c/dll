using System;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200009E RID: 158
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetAgentTargetPositionAndDirection : GameNetworkMessage
	{
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x0000B1ED File Offset: 0x000093ED
		// (set) Token: 0x06000650 RID: 1616 RVA: 0x0000B1F5 File Offset: 0x000093F5
		public int AgentIndex { get; private set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x0000B1FE File Offset: 0x000093FE
		// (set) Token: 0x06000652 RID: 1618 RVA: 0x0000B206 File Offset: 0x00009406
		public Vec2 Position { get; private set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x0000B20F File Offset: 0x0000940F
		// (set) Token: 0x06000654 RID: 1620 RVA: 0x0000B217 File Offset: 0x00009417
		public Vec3 Direction { get; private set; }

		// Token: 0x06000655 RID: 1621 RVA: 0x0000B220 File Offset: 0x00009420
		public SetAgentTargetPositionAndDirection(int agentIndex, ref Vec2 position, ref Vec3 direction)
		{
			this.AgentIndex = agentIndex;
			this.Position = position;
			this.Direction = direction;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0000B247 File Offset: 0x00009447
		public SetAgentTargetPositionAndDirection()
		{
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0000B250 File Offset: 0x00009450
		protected override bool OnRead()
		{
			bool result = true;
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.Position = GameNetworkMessage.ReadVec2FromPacket(CompressionBasic.PositionCompressionInfo, ref result);
			this.Direction = GameNetworkMessage.ReadVec3FromPacket(CompressionBasic.UnitVectorCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0000B291 File Offset: 0x00009491
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			GameNetworkMessage.WriteVec2ToPacket(this.Position, CompressionBasic.PositionCompressionInfo);
			GameNetworkMessage.WriteVec3ToPacket(this.Direction, CompressionBasic.UnitVectorCompressionInfo);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0000B2BE File Offset: 0x000094BE
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.AgentsDetailed;
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0000B2C8 File Offset: 0x000094C8
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Set TargetPositionAndDirection: ",
				this.Position,
				" ",
				this.Direction,
				" on Agent with agent-index: ",
				this.AgentIndex
			});
		}
	}
}
