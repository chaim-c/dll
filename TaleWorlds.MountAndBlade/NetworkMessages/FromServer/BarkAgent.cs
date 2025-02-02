using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000079 RID: 121
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class BarkAgent : GameNetworkMessage
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x000081D9 File Offset: 0x000063D9
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x000081E1 File Offset: 0x000063E1
		public int AgentIndex { get; private set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x000081EA File Offset: 0x000063EA
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x000081F2 File Offset: 0x000063F2
		public int IndexOfBark { get; private set; }

		// Token: 0x06000467 RID: 1127 RVA: 0x000081FB File Offset: 0x000063FB
		public BarkAgent(int agent, int indexOfBark)
		{
			this.AgentIndex = agent;
			this.IndexOfBark = indexOfBark;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00008211 File Offset: 0x00006411
		public BarkAgent()
		{
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000821C File Offset: 0x0000641C
		protected override bool OnRead()
		{
			bool result = true;
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.IndexOfBark = GameNetworkMessage.ReadIntFromPacket(CompressionMission.BarkIndexCompressionInfo, ref result);
			return result;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000824B File Offset: 0x0000644B
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			GameNetworkMessage.WriteIntToPacket(this.IndexOfBark, CompressionMission.BarkIndexCompressionInfo);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00008268 File Offset: 0x00006468
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.None;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000826C File Offset: 0x0000646C
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"FromServer.BarkAgent agent-index: ",
				this.AgentIndex,
				", IndexOfBark",
				this.IndexOfBark
			});
		}
	}
}
