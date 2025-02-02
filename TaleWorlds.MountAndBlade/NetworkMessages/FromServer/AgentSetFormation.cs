using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000072 RID: 114
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class AgentSetFormation : GameNetworkMessage
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0000795C File Offset: 0x00005B5C
		// (set) Token: 0x06000412 RID: 1042 RVA: 0x00007964 File Offset: 0x00005B64
		public int AgentIndex { get; private set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0000796D File Offset: 0x00005B6D
		// (set) Token: 0x06000414 RID: 1044 RVA: 0x00007975 File Offset: 0x00005B75
		public int FormationIndex { get; private set; }

		// Token: 0x06000415 RID: 1045 RVA: 0x0000797E File Offset: 0x00005B7E
		public AgentSetFormation(int agentIndex, int formationIndex)
		{
			this.AgentIndex = agentIndex;
			this.FormationIndex = formationIndex;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00007994 File Offset: 0x00005B94
		public AgentSetFormation()
		{
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000799C File Offset: 0x00005B9C
		protected override bool OnRead()
		{
			bool result = true;
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.FormationIndex = GameNetworkMessage.ReadIntFromPacket(CompressionMission.FormationClassCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000079CB File Offset: 0x00005BCB
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			GameNetworkMessage.WriteIntToPacket(this.FormationIndex, CompressionMission.FormationClassCompressionInfo);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x000079E8 File Offset: 0x00005BE8
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Formations | MultiplayerMessageFilter.AgentsDetailed;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000079F0 File Offset: 0x00005BF0
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Assign agent with agent-index: ",
				this.AgentIndex,
				" to formation with index: ",
				this.FormationIndex
			});
		}
	}
}
