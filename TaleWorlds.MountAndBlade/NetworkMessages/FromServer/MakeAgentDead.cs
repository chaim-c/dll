using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000090 RID: 144
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class MakeAgentDead : GameNetworkMessage
	{
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0000A4DB File Offset: 0x000086DB
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x0000A4E3 File Offset: 0x000086E3
		public int AgentIndex { get; private set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0000A4EC File Offset: 0x000086EC
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x0000A4F4 File Offset: 0x000086F4
		public bool IsKilled { get; private set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0000A4FD File Offset: 0x000086FD
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x0000A505 File Offset: 0x00008705
		public ActionIndexValueCache ActionCodeIndex { get; private set; }

		// Token: 0x060005B9 RID: 1465 RVA: 0x0000A50E File Offset: 0x0000870E
		public MakeAgentDead(int agentIndex, bool isKilled, ActionIndexValueCache actionCodeIndex)
		{
			this.AgentIndex = agentIndex;
			this.IsKilled = isKilled;
			this.ActionCodeIndex = actionCodeIndex;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0000A52B File Offset: 0x0000872B
		public MakeAgentDead()
		{
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0000A534 File Offset: 0x00008734
		protected override bool OnRead()
		{
			bool result = true;
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.IsKilled = GameNetworkMessage.ReadBoolFromPacket(ref result);
			this.ActionCodeIndex = new ActionIndexValueCache(GameNetworkMessage.ReadIntFromPacket(CompressionBasic.ActionCodeCompressionInfo, ref result));
			return result;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0000A578 File Offset: 0x00008778
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			GameNetworkMessage.WriteBoolToPacket(this.IsKilled);
			GameNetworkMessage.WriteIntToPacket(this.ActionCodeIndex.Index, CompressionBasic.ActionCodeCompressionInfo);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0000A5B3 File Offset: 0x000087B3
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.EquipmentDetailed;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0000A5B8 File Offset: 0x000087B8
		protected override string OnGetLogFormat()
		{
			return "Make Agent Dead on Agent with agent-index: " + this.AgentIndex;
		}
	}
}
