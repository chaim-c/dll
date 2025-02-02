using System;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200009D RID: 157
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetAgentTargetPosition : GameNetworkMessage
	{
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x0000B11B File Offset: 0x0000931B
		// (set) Token: 0x06000646 RID: 1606 RVA: 0x0000B123 File Offset: 0x00009323
		public int AgentIndex { get; private set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x0000B12C File Offset: 0x0000932C
		// (set) Token: 0x06000648 RID: 1608 RVA: 0x0000B134 File Offset: 0x00009334
		public Vec2 Position { get; private set; }

		// Token: 0x06000649 RID: 1609 RVA: 0x0000B13D File Offset: 0x0000933D
		public SetAgentTargetPosition(int agentIndex, ref Vec2 position)
		{
			this.AgentIndex = agentIndex;
			this.Position = position;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0000B158 File Offset: 0x00009358
		public SetAgentTargetPosition()
		{
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0000B160 File Offset: 0x00009360
		protected override bool OnRead()
		{
			bool result = true;
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.Position = GameNetworkMessage.ReadVec2FromPacket(CompressionBasic.PositionCompressionInfo, ref result);
			return result;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0000B18F File Offset: 0x0000938F
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			GameNetworkMessage.WriteVec2ToPacket(this.Position, CompressionBasic.PositionCompressionInfo);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0000B1AC File Offset: 0x000093AC
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.AgentsDetailed;
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0000B1B4 File Offset: 0x000093B4
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Set Target Position: ",
				this.Position,
				" on Agent with agent-index: ",
				this.AgentIndex
			});
		}
	}
}
