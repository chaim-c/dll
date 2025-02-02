using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000C3 RID: 195
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class StartSwitchingWeaponUsageIndex : GameNetworkMessage
	{
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x0000D884 File Offset: 0x0000BA84
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x0000D88C File Offset: 0x0000BA8C
		public int AgentIndex { get; private set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x0000D895 File Offset: 0x0000BA95
		// (set) Token: 0x060007F9 RID: 2041 RVA: 0x0000D89D File Offset: 0x0000BA9D
		public EquipmentIndex EquipmentIndex { get; private set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x0000D8A6 File Offset: 0x0000BAA6
		// (set) Token: 0x060007FB RID: 2043 RVA: 0x0000D8AE File Offset: 0x0000BAAE
		public int UsageIndex { get; private set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x0000D8B7 File Offset: 0x0000BAB7
		// (set) Token: 0x060007FD RID: 2045 RVA: 0x0000D8BF File Offset: 0x0000BABF
		public Agent.UsageDirection CurrentMovementFlagUsageDirection { get; private set; }

		// Token: 0x060007FE RID: 2046 RVA: 0x0000D8C8 File Offset: 0x0000BAC8
		public StartSwitchingWeaponUsageIndex(int agentIndex, EquipmentIndex equipmentIndex, int usageIndex, Agent.UsageDirection currentMovementFlagUsageDirection)
		{
			this.AgentIndex = agentIndex;
			this.EquipmentIndex = equipmentIndex;
			this.UsageIndex = usageIndex;
			this.CurrentMovementFlagUsageDirection = currentMovementFlagUsageDirection;
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0000D8ED File Offset: 0x0000BAED
		public StartSwitchingWeaponUsageIndex()
		{
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0000D8F8 File Offset: 0x0000BAF8
		protected override bool OnRead()
		{
			bool result = true;
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.EquipmentIndex = (EquipmentIndex)GameNetworkMessage.ReadIntFromPacket(CompressionMission.ItemSlotCompressionInfo, ref result);
			this.UsageIndex = (int)((short)GameNetworkMessage.ReadIntFromPacket(CompressionMission.WeaponUsageIndexCompressionInfo, ref result));
			this.CurrentMovementFlagUsageDirection = (Agent.UsageDirection)GameNetworkMessage.ReadIntFromPacket(CompressionMission.UsageDirectionCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0000D94C File Offset: 0x0000BB4C
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			GameNetworkMessage.WriteIntToPacket((int)this.EquipmentIndex, CompressionMission.ItemSlotCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.UsageIndex, CompressionMission.WeaponUsageIndexCompressionInfo);
			GameNetworkMessage.WriteIntToPacket((int)this.CurrentMovementFlagUsageDirection, CompressionMission.UsageDirectionCompressionInfo);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0000D989 File Offset: 0x0000BB89
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.EquipmentDetailed;
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0000D990 File Offset: 0x0000BB90
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"StartSwitchingWeaponUsageIndex: ",
				this.UsageIndex,
				" for weapon with EquipmentIndex: ",
				this.EquipmentIndex,
				" on Agent with agent-index: ",
				this.AgentIndex
			});
		}
	}
}
