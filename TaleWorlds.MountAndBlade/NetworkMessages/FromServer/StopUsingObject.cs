using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000C5 RID: 197
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class StopUsingObject : GameNetworkMessage
	{
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x0000DB16 File Offset: 0x0000BD16
		// (set) Token: 0x06000811 RID: 2065 RVA: 0x0000DB1E File Offset: 0x0000BD1E
		public int AgentIndex { get; private set; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x0000DB27 File Offset: 0x0000BD27
		// (set) Token: 0x06000813 RID: 2067 RVA: 0x0000DB2F File Offset: 0x0000BD2F
		public bool IsSuccessful { get; private set; }

		// Token: 0x06000814 RID: 2068 RVA: 0x0000DB38 File Offset: 0x0000BD38
		public StopUsingObject(int agentIndex, bool isSuccessful)
		{
			this.AgentIndex = agentIndex;
			this.IsSuccessful = isSuccessful;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0000DB4E File Offset: 0x0000BD4E
		public StopUsingObject()
		{
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0000DB58 File Offset: 0x0000BD58
		protected override bool OnRead()
		{
			bool result = true;
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.IsSuccessful = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0000DB82 File Offset: 0x0000BD82
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			GameNetworkMessage.WriteBoolToPacket(this.IsSuccessful);
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0000DB9A File Offset: 0x0000BD9A
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.AgentsDetailed | MultiplayerMessageFilter.MissionObjectsDetailed;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0000DBA2 File Offset: 0x0000BDA2
		protected override string OnGetLogFormat()
		{
			return "Stop using Object on Agent with agent-index: " + this.AgentIndex;
		}
	}
}
