using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000086 RID: 134
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class EquipWeaponFromSpawnedItemEntity : GameNetworkMessage
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x00009921 File Offset: 0x00007B21
		// (set) Token: 0x0600053D RID: 1341 RVA: 0x00009929 File Offset: 0x00007B29
		public MissionObjectId SpawnedItemEntityId { get; private set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x00009932 File Offset: 0x00007B32
		// (set) Token: 0x0600053F RID: 1343 RVA: 0x0000993A File Offset: 0x00007B3A
		public EquipmentIndex SlotIndex { get; private set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x00009943 File Offset: 0x00007B43
		// (set) Token: 0x06000541 RID: 1345 RVA: 0x0000994B File Offset: 0x00007B4B
		public int AgentIndex { get; private set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x00009954 File Offset: 0x00007B54
		// (set) Token: 0x06000543 RID: 1347 RVA: 0x0000995C File Offset: 0x00007B5C
		public bool RemoveWeapon { get; private set; }

		// Token: 0x06000544 RID: 1348 RVA: 0x00009965 File Offset: 0x00007B65
		public EquipWeaponFromSpawnedItemEntity(int agentIndex, EquipmentIndex slot, MissionObjectId spawnedItemEntityId, bool removeWeapon)
		{
			this.AgentIndex = agentIndex;
			this.SlotIndex = slot;
			this.SpawnedItemEntityId = spawnedItemEntityId;
			this.RemoveWeapon = removeWeapon;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0000998A File Offset: 0x00007B8A
		public EquipWeaponFromSpawnedItemEntity()
		{
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00009994 File Offset: 0x00007B94
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			GameNetworkMessage.WriteMissionObjectIdToPacket((this.SpawnedItemEntityId.Id >= 0) ? this.SpawnedItemEntityId : MissionObjectId.Invalid);
			GameNetworkMessage.WriteIntToPacket((int)this.SlotIndex, CompressionMission.ItemSlotCompressionInfo);
			GameNetworkMessage.WriteBoolToPacket(this.RemoveWeapon);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x000099E8 File Offset: 0x00007BE8
		protected override bool OnRead()
		{
			bool result = true;
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.SpawnedItemEntityId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.SlotIndex = (EquipmentIndex)GameNetworkMessage.ReadIntFromPacket(CompressionMission.ItemSlotCompressionInfo, ref result);
			this.RemoveWeapon = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00009A31 File Offset: 0x00007C31
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Items | MultiplayerMessageFilter.AgentsDetailed;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00009A3C File Offset: 0x00007C3C
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"EquipWeaponFromSpawnedItemEntity with missionObjectId: ",
				this.SpawnedItemEntityId,
				" to SlotIndex: ",
				this.SlotIndex,
				" on agent-index: ",
				this.AgentIndex,
				" RemoveWeapon: ",
				this.RemoveWeapon.ToString()
			});
		}
	}
}
