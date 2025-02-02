using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200007C RID: 124
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class ClearAgentTargetFrame : GameNetworkMessage
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x000083BD File Offset: 0x000065BD
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x000083C5 File Offset: 0x000065C5
		public int AgentIndex { get; private set; }

		// Token: 0x06000481 RID: 1153 RVA: 0x000083CE File Offset: 0x000065CE
		public ClearAgentTargetFrame(int agentIndex)
		{
			this.AgentIndex = agentIndex;
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x000083DD File Offset: 0x000065DD
		public ClearAgentTargetFrame()
		{
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x000083E8 File Offset: 0x000065E8
		protected override bool OnRead()
		{
			bool result = true;
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			return result;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00008405 File Offset: 0x00006605
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00008412 File Offset: 0x00006612
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.AgentsDetailed;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000841A File Offset: 0x0000661A
		protected override string OnGetLogFormat()
		{
			return "Clear target frame on agent with agent-index: " + this.AgentIndex;
		}
	}
}
