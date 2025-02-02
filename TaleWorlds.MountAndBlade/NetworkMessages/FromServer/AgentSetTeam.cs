using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000073 RID: 115
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class AgentSetTeam : GameNetworkMessage
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x00007A29 File Offset: 0x00005C29
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x00007A31 File Offset: 0x00005C31
		public int AgentIndex { get; private set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x00007A3A File Offset: 0x00005C3A
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x00007A42 File Offset: 0x00005C42
		public int TeamIndex { get; private set; }

		// Token: 0x0600041F RID: 1055 RVA: 0x00007A4B File Offset: 0x00005C4B
		public AgentSetTeam(int agentIndex, int teamIndex)
		{
			this.AgentIndex = agentIndex;
			this.TeamIndex = teamIndex;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00007A61 File Offset: 0x00005C61
		public AgentSetTeam()
		{
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00007A6C File Offset: 0x00005C6C
		protected override bool OnRead()
		{
			bool result = true;
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.TeamIndex = GameNetworkMessage.ReadTeamIndexFromPacket(ref result);
			return result;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00007A96 File Offset: 0x00005C96
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			GameNetworkMessage.WriteTeamIndexToPacket(this.TeamIndex);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00007AAE File Offset: 0x00005CAE
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Agents;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00007AB6 File Offset: 0x00005CB6
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Assign agent with agent-index: ",
				this.AgentIndex,
				" to team: ",
				this.TeamIndex
			});
		}
	}
}
