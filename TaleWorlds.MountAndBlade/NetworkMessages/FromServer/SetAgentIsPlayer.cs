using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200009A RID: 154
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetAgentIsPlayer : GameNetworkMessage
	{
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0000AE51 File Offset: 0x00009051
		// (set) Token: 0x06000626 RID: 1574 RVA: 0x0000AE59 File Offset: 0x00009059
		public int AgentIndex { get; private set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x0000AE62 File Offset: 0x00009062
		// (set) Token: 0x06000628 RID: 1576 RVA: 0x0000AE6A File Offset: 0x0000906A
		public bool IsPlayer { get; private set; }

		// Token: 0x06000629 RID: 1577 RVA: 0x0000AE73 File Offset: 0x00009073
		public SetAgentIsPlayer(int agentIndex, bool isPlayer)
		{
			this.AgentIndex = agentIndex;
			this.IsPlayer = isPlayer;
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0000AE89 File Offset: 0x00009089
		public SetAgentIsPlayer()
		{
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0000AE94 File Offset: 0x00009094
		protected override bool OnRead()
		{
			bool result = true;
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.IsPlayer = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0000AEBE File Offset: 0x000090BE
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			GameNetworkMessage.WriteBoolToPacket(this.IsPlayer);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0000AED6 File Offset: 0x000090D6
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.AgentsDetailed;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0000AEDE File Offset: 0x000090DE
		protected override string OnGetLogFormat()
		{
			return "Set Controller is player on Agent with agent-index: " + this.AgentIndex + (this.IsPlayer ? " - TRUE." : " - FALSE.");
		}
	}
}
