using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000D0 RID: 208
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.DebugFromServer)]
	internal sealed class DebugAgentScaleOnNetworkTest : GameNetworkMessage
	{
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x0000E61E File Offset: 0x0000C81E
		// (set) Token: 0x0600088B RID: 2187 RVA: 0x0000E626 File Offset: 0x0000C826
		internal int AgentToTestIndex { get; private set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x0000E62F File Offset: 0x0000C82F
		// (set) Token: 0x0600088D RID: 2189 RVA: 0x0000E637 File Offset: 0x0000C837
		internal float ScaleToTest { get; private set; }

		// Token: 0x0600088E RID: 2190 RVA: 0x0000E640 File Offset: 0x0000C840
		public DebugAgentScaleOnNetworkTest()
		{
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0000E648 File Offset: 0x0000C848
		internal DebugAgentScaleOnNetworkTest(int agentToTestIndex, float scale)
		{
			this.AgentToTestIndex = agentToTestIndex;
			this.ScaleToTest = scale;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0000E660 File Offset: 0x0000C860
		protected override bool OnRead()
		{
			bool result = true;
			this.AgentToTestIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.ScaleToTest = GameNetworkMessage.ReadFloatFromPacket(CompressionMission.DebugScaleValueCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0000E68F File Offset: 0x0000C88F
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentToTestIndex);
			GameNetworkMessage.WriteFloatToPacket(this.ScaleToTest, CompressionMission.DebugScaleValueCompressionInfo);
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0000E6AC File Offset: 0x0000C8AC
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.AgentsDetailed;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0000E6B4 File Offset: 0x0000C8B4
		protected override string OnGetLogFormat()
		{
			return "DebugAgentScaleOnNetworkTest";
		}
	}
}
