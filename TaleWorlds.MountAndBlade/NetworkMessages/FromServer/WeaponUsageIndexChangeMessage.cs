using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000CF RID: 207
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class WeaponUsageIndexChangeMessage : GameNetworkMessage
	{
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x0000E4F3 File Offset: 0x0000C6F3
		// (set) Token: 0x0600087F RID: 2175 RVA: 0x0000E4FB File Offset: 0x0000C6FB
		public int AgentIndex { get; private set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x0000E504 File Offset: 0x0000C704
		// (set) Token: 0x06000881 RID: 2177 RVA: 0x0000E50C File Offset: 0x0000C70C
		public EquipmentIndex SlotIndex { get; private set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x0000E515 File Offset: 0x0000C715
		// (set) Token: 0x06000883 RID: 2179 RVA: 0x0000E51D File Offset: 0x0000C71D
		public int UsageIndex { get; private set; }

		// Token: 0x06000884 RID: 2180 RVA: 0x0000E526 File Offset: 0x0000C726
		public WeaponUsageIndexChangeMessage()
		{
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0000E52E File Offset: 0x0000C72E
		public WeaponUsageIndexChangeMessage(int agentIndex, EquipmentIndex slotIndex, int usageIndex)
		{
			this.AgentIndex = agentIndex;
			this.SlotIndex = slotIndex;
			this.UsageIndex = usageIndex;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0000E54C File Offset: 0x0000C74C
		protected override bool OnRead()
		{
			bool result = true;
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.SlotIndex = (EquipmentIndex)GameNetworkMessage.ReadIntFromPacket(CompressionMission.ItemSlotCompressionInfo, ref result);
			this.UsageIndex = (int)((short)GameNetworkMessage.ReadIntFromPacket(CompressionMission.WeaponUsageIndexCompressionInfo, ref result));
			return result;
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0000E58E File Offset: 0x0000C78E
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			GameNetworkMessage.WriteIntToPacket((int)this.SlotIndex, CompressionMission.ItemSlotCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.UsageIndex, CompressionMission.WeaponUsageIndexCompressionInfo);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0000E5BB File Offset: 0x0000C7BB
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionDetailed;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0000E5C4 File Offset: 0x0000C7C4
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Set Weapon Usage Index: ",
				this.UsageIndex,
				" for weapon with EquipmentIndex: ",
				this.SlotIndex,
				" on Agent with agent-index: ",
				this.AgentIndex
			});
		}
	}
}
