using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.ObjectSystem;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000C6 RID: 198
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SynchronizeAgentSpawnEquipment : GameNetworkMessage
	{
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x0000DBB9 File Offset: 0x0000BDB9
		// (set) Token: 0x0600081B RID: 2075 RVA: 0x0000DBC1 File Offset: 0x0000BDC1
		public int AgentIndex { get; private set; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x0000DBCA File Offset: 0x0000BDCA
		// (set) Token: 0x0600081D RID: 2077 RVA: 0x0000DBD2 File Offset: 0x0000BDD2
		public Equipment SpawnEquipment { get; private set; }

		// Token: 0x0600081E RID: 2078 RVA: 0x0000DBDC File Offset: 0x0000BDDC
		public SynchronizeAgentSpawnEquipment(int agentIndex, Equipment spawnEquipment)
		{
			this.AgentIndex = agentIndex;
			this.SpawnEquipment = new Equipment();
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumEquipmentSetSlots; equipmentIndex++)
			{
				this.SpawnEquipment[equipmentIndex] = spawnEquipment.GetEquipmentFromSlot(equipmentIndex);
			}
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0000DC21 File Offset: 0x0000BE21
		public SynchronizeAgentSpawnEquipment()
		{
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0000DC2C File Offset: 0x0000BE2C
		protected override bool OnRead()
		{
			bool result = true;
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.SpawnEquipment = new Equipment();
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumEquipmentSetSlots; equipmentIndex++)
			{
				this.SpawnEquipment.AddEquipmentToSlotWithoutAgent(equipmentIndex, ModuleNetworkData.ReadItemReferenceFromPacket(MBObjectManager.Instance, ref result));
			}
			return result;
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0000DC7C File Offset: 0x0000BE7C
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumEquipmentSetSlots; equipmentIndex++)
			{
				ModuleNetworkData.WriteItemReferenceToPacket(this.SpawnEquipment.GetEquipmentFromSlot(equipmentIndex));
			}
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0000DCB2 File Offset: 0x0000BEB2
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Agents;
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0000DCBA File Offset: 0x0000BEBA
		protected override string OnGetLogFormat()
		{
			return "Equipment synchronized for agent-index: " + this.AgentIndex;
		}
	}
}
