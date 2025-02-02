using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000098 RID: 152
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetAgentActionSet : GameNetworkMessage
	{
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x0000AB56 File Offset: 0x00008D56
		// (set) Token: 0x06000608 RID: 1544 RVA: 0x0000AB5E File Offset: 0x00008D5E
		public int AgentIndex { get; private set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x0000AB67 File Offset: 0x00008D67
		// (set) Token: 0x0600060A RID: 1546 RVA: 0x0000AB6F File Offset: 0x00008D6F
		public MBActionSet ActionSet { get; private set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x0000AB78 File Offset: 0x00008D78
		// (set) Token: 0x0600060C RID: 1548 RVA: 0x0000AB80 File Offset: 0x00008D80
		public int NumPaces { get; private set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x0000AB89 File Offset: 0x00008D89
		// (set) Token: 0x0600060E RID: 1550 RVA: 0x0000AB91 File Offset: 0x00008D91
		public int MonsterUsageSetIndex { get; private set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x0000AB9A File Offset: 0x00008D9A
		// (set) Token: 0x06000610 RID: 1552 RVA: 0x0000ABA2 File Offset: 0x00008DA2
		public float WalkingSpeedLimit { get; private set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0000ABAB File Offset: 0x00008DAB
		// (set) Token: 0x06000612 RID: 1554 RVA: 0x0000ABB3 File Offset: 0x00008DB3
		public float CrouchWalkingSpeedLimit { get; private set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0000ABBC File Offset: 0x00008DBC
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x0000ABC4 File Offset: 0x00008DC4
		public float StepSize { get; private set; }

		// Token: 0x06000615 RID: 1557 RVA: 0x0000ABD0 File Offset: 0x00008DD0
		public SetAgentActionSet(int agentIndex, AnimationSystemData animationSystemData)
		{
			this.AgentIndex = agentIndex;
			this.ActionSet = animationSystemData.ActionSet;
			this.NumPaces = animationSystemData.NumPaces;
			this.MonsterUsageSetIndex = animationSystemData.MonsterUsageSetIndex;
			this.WalkingSpeedLimit = animationSystemData.WalkingSpeedLimit;
			this.CrouchWalkingSpeedLimit = animationSystemData.CrouchWalkingSpeedLimit;
			this.StepSize = animationSystemData.StepSize;
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0000AC32 File Offset: 0x00008E32
		public SetAgentActionSet()
		{
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0000AC3C File Offset: 0x00008E3C
		protected override bool OnRead()
		{
			bool result = true;
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.ActionSet = GameNetworkMessage.ReadActionSetReferenceFromPacket(CompressionMission.ActionSetCompressionInfo, ref result);
			this.NumPaces = GameNetworkMessage.ReadIntFromPacket(CompressionMission.NumberOfPacesCompressionInfo, ref result);
			this.MonsterUsageSetIndex = GameNetworkMessage.ReadIntFromPacket(CompressionMission.MonsterUsageSetCompressionInfo, ref result);
			this.WalkingSpeedLimit = GameNetworkMessage.ReadFloatFromPacket(CompressionMission.WalkingSpeedLimitCompressionInfo, ref result);
			this.CrouchWalkingSpeedLimit = GameNetworkMessage.ReadFloatFromPacket(CompressionMission.WalkingSpeedLimitCompressionInfo, ref result);
			this.StepSize = GameNetworkMessage.ReadFloatFromPacket(CompressionMission.StepSizeCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0000ACC8 File Offset: 0x00008EC8
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			GameNetworkMessage.WriteActionSetReferenceToPacket(this.ActionSet, CompressionMission.ActionSetCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.NumPaces, CompressionMission.NumberOfPacesCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.MonsterUsageSetIndex, CompressionMission.MonsterUsageSetCompressionInfo);
			GameNetworkMessage.WriteFloatToPacket(this.WalkingSpeedLimit, CompressionMission.WalkingSpeedLimitCompressionInfo);
			GameNetworkMessage.WriteFloatToPacket(this.CrouchWalkingSpeedLimit, CompressionMission.WalkingSpeedLimitCompressionInfo);
			GameNetworkMessage.WriteFloatToPacket(this.StepSize, CompressionMission.StepSizeCompressionInfo);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0000AD40 File Offset: 0x00008F40
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.AgentAnimations;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0000AD48 File Offset: 0x00008F48
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Set ActionSet: ",
				this.ActionSet,
				" on agent with agent-index: ",
				this.AgentIndex
			});
		}
	}
}
