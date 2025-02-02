using System;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.ObjectSystem;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000087 RID: 135
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class EquipWeaponWithNewEntity : GameNetworkMessage
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x00009AAF File Offset: 0x00007CAF
		// (set) Token: 0x0600054B RID: 1355 RVA: 0x00009AB7 File Offset: 0x00007CB7
		public MissionWeapon Weapon { get; private set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x00009AC0 File Offset: 0x00007CC0
		// (set) Token: 0x0600054D RID: 1357 RVA: 0x00009AC8 File Offset: 0x00007CC8
		public EquipmentIndex SlotIndex { get; private set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x00009AD1 File Offset: 0x00007CD1
		// (set) Token: 0x0600054F RID: 1359 RVA: 0x00009AD9 File Offset: 0x00007CD9
		public int AgentIndex { get; private set; }

		// Token: 0x06000550 RID: 1360 RVA: 0x00009AE2 File Offset: 0x00007CE2
		public EquipWeaponWithNewEntity(int agentIndex, EquipmentIndex slot, MissionWeapon weapon)
		{
			this.AgentIndex = agentIndex;
			this.SlotIndex = slot;
			this.Weapon = weapon;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00009AFF File Offset: 0x00007CFF
		public EquipWeaponWithNewEntity()
		{
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00009B07 File Offset: 0x00007D07
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			ModuleNetworkData.WriteWeaponReferenceToPacket(this.Weapon);
			GameNetworkMessage.WriteIntToPacket((int)this.SlotIndex, CompressionMission.ItemSlotCompressionInfo);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00009B30 File Offset: 0x00007D30
		protected override bool OnRead()
		{
			bool result = true;
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.Weapon = ModuleNetworkData.ReadWeaponReferenceFromPacket(MBObjectManager.Instance, ref result);
			this.SlotIndex = (EquipmentIndex)GameNetworkMessage.ReadIntFromPacket(CompressionMission.ItemSlotCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00009B71 File Offset: 0x00007D71
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Items | MultiplayerMessageFilter.AgentsDetailed;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00009B7C File Offset: 0x00007D7C
		protected override string OnGetLogFormat()
		{
			if (this.AgentIndex < 0)
			{
				return "Not equipping weapon because there is no agent to equip it to,";
			}
			return string.Concat(new object[]
			{
				"Equip weapon with name: ",
				(!this.Weapon.IsEmpty) ? this.Weapon.Item.Name : TextObject.Empty,
				" from SlotIndex: ",
				this.SlotIndex,
				" on agent with agent-index: ",
				this.AgentIndex
			});
		}
	}
}
