using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000BC RID: 188
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetWeaponNetworkData : GameNetworkMessage
	{
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x0000CE42 File Offset: 0x0000B042
		// (set) Token: 0x0600078F RID: 1935 RVA: 0x0000CE4A File Offset: 0x0000B04A
		public int AgentIndex { get; private set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x0000CE53 File Offset: 0x0000B053
		// (set) Token: 0x06000791 RID: 1937 RVA: 0x0000CE5B File Offset: 0x0000B05B
		public EquipmentIndex WeaponEquipmentIndex { get; private set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x0000CE64 File Offset: 0x0000B064
		// (set) Token: 0x06000793 RID: 1939 RVA: 0x0000CE6C File Offset: 0x0000B06C
		public short DataValue { get; private set; }

		// Token: 0x06000794 RID: 1940 RVA: 0x0000CE75 File Offset: 0x0000B075
		public SetWeaponNetworkData(int agent, EquipmentIndex weaponEquipmentIndex, short dataValue)
		{
			this.AgentIndex = agent;
			this.WeaponEquipmentIndex = weaponEquipmentIndex;
			this.DataValue = dataValue;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0000CE92 File Offset: 0x0000B092
		public SetWeaponNetworkData()
		{
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0000CE9C File Offset: 0x0000B09C
		protected override bool OnRead()
		{
			bool result = true;
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.WeaponEquipmentIndex = (EquipmentIndex)GameNetworkMessage.ReadIntFromPacket(CompressionMission.ItemSlotCompressionInfo, ref result);
			this.DataValue = (short)GameNetworkMessage.ReadIntFromPacket(CompressionMission.ItemDataCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0000CEDE File Offset: 0x0000B0DE
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			GameNetworkMessage.WriteIntToPacket((int)this.WeaponEquipmentIndex, CompressionMission.ItemSlotCompressionInfo);
			GameNetworkMessage.WriteIntToPacket((int)this.DataValue, CompressionMission.ItemDataCompressionInfo);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0000CF0B File Offset: 0x0000B10B
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.EquipmentDetailed;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0000CF10 File Offset: 0x0000B110
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Set Network data: ",
				this.DataValue,
				" for weapon with EquipmentIndex: ",
				this.WeaponEquipmentIndex,
				" on Agent with agent-index: ",
				this.AgentIndex
			});
		}
	}
}
