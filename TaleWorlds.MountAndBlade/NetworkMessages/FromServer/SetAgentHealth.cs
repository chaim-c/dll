using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000099 RID: 153
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetAgentHealth : GameNetworkMessage
	{
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x0000AD81 File Offset: 0x00008F81
		// (set) Token: 0x0600061C RID: 1564 RVA: 0x0000AD89 File Offset: 0x00008F89
		public int AgentIndex { get; private set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x0000AD92 File Offset: 0x00008F92
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x0000AD9A File Offset: 0x00008F9A
		public int Health { get; private set; }

		// Token: 0x0600061F RID: 1567 RVA: 0x0000ADA3 File Offset: 0x00008FA3
		public SetAgentHealth(int agentIndex, int newHealth)
		{
			this.AgentIndex = agentIndex;
			this.Health = newHealth;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0000ADB9 File Offset: 0x00008FB9
		public SetAgentHealth()
		{
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0000ADC4 File Offset: 0x00008FC4
		protected override bool OnRead()
		{
			bool result = true;
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.Health = GameNetworkMessage.ReadIntFromPacket(CompressionMission.AgentHealthCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0000ADF3 File Offset: 0x00008FF3
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			GameNetworkMessage.WriteIntToPacket(this.Health, CompressionMission.AgentHealthCompressionInfo);
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0000AE10 File Offset: 0x00009010
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.AgentsDetailed;
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0000AE18 File Offset: 0x00009018
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Set agent health to: ",
				this.Health,
				", for agent-index: ",
				this.AgentIndex
			});
		}
	}
}
