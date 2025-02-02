using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200009C RID: 156
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetAgentPrefabComponentVisibility : GameNetworkMessage
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0000AFF4 File Offset: 0x000091F4
		// (set) Token: 0x0600063A RID: 1594 RVA: 0x0000AFFC File Offset: 0x000091FC
		public int AgentIndex { get; private set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x0000B005 File Offset: 0x00009205
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x0000B00D File Offset: 0x0000920D
		public int ComponentIndex { get; private set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x0000B016 File Offset: 0x00009216
		// (set) Token: 0x0600063E RID: 1598 RVA: 0x0000B01E File Offset: 0x0000921E
		public bool Visibility { get; private set; }

		// Token: 0x0600063F RID: 1599 RVA: 0x0000B027 File Offset: 0x00009227
		public SetAgentPrefabComponentVisibility(int agentIndex, int componentIndex, bool visibility)
		{
			this.AgentIndex = agentIndex;
			this.ComponentIndex = componentIndex;
			this.Visibility = visibility;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0000B044 File Offset: 0x00009244
		public SetAgentPrefabComponentVisibility()
		{
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0000B04C File Offset: 0x0000924C
		protected override bool OnRead()
		{
			bool result = true;
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.ComponentIndex = GameNetworkMessage.ReadIntFromPacket(CompressionMission.AgentPrefabComponentIndexCompressionInfo, ref result);
			this.Visibility = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0000B088 File Offset: 0x00009288
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			GameNetworkMessage.WriteIntToPacket(this.ComponentIndex, CompressionMission.AgentPrefabComponentIndexCompressionInfo);
			GameNetworkMessage.WriteBoolToPacket(this.Visibility);
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0000B0B0 File Offset: 0x000092B0
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.AgentsDetailed;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0000B0B8 File Offset: 0x000092B8
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Set Component with index: ",
				this.ComponentIndex,
				" to be ",
				this.Visibility ? "visible" : "invisible",
				" on Agent with agent-index: ",
				this.AgentIndex
			});
		}
	}
}
