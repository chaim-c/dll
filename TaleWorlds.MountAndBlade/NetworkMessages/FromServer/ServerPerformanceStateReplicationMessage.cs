using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000D4 RID: 212
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class ServerPerformanceStateReplicationMessage : GameNetworkMessage
	{
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x0000E7DC File Offset: 0x0000C9DC
		// (set) Token: 0x060008A7 RID: 2215 RVA: 0x0000E7E4 File Offset: 0x0000C9E4
		internal ServerPerformanceState ServerPerformanceProblemState { get; private set; }

		// Token: 0x060008A8 RID: 2216 RVA: 0x0000E7ED File Offset: 0x0000C9ED
		public ServerPerformanceStateReplicationMessage()
		{
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0000E7F5 File Offset: 0x0000C9F5
		internal ServerPerformanceStateReplicationMessage(ServerPerformanceState serverPerformanceProblemState)
		{
			this.ServerPerformanceProblemState = serverPerformanceProblemState;
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0000E804 File Offset: 0x0000CA04
		protected override bool OnRead()
		{
			bool result = true;
			this.ServerPerformanceProblemState = (ServerPerformanceState)GameNetworkMessage.ReadIntFromPacket(CompressionBasic.ServerPerformanceStateCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0000E826 File Offset: 0x0000CA26
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket((int)this.ServerPerformanceProblemState, CompressionBasic.ServerPerformanceStateCompressionInfo);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0000E838 File Offset: 0x0000CA38
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionDetailed;
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0000E840 File Offset: 0x0000CA40
		protected override string OnGetLogFormat()
		{
			return "ServerPerformanceStateReplicationMessage";
		}
	}
}
