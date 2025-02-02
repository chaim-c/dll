using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.ObjectSystem;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000078 RID: 120
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class AttachWeaponToWeaponInAgentEquipmentSlot : GameNetworkMessage
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00008027 File Offset: 0x00006227
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x0000802F File Offset: 0x0000622F
		public MissionWeapon Weapon { get; private set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00008038 File Offset: 0x00006238
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x00008040 File Offset: 0x00006240
		public EquipmentIndex SlotIndex { get; private set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x00008049 File Offset: 0x00006249
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x00008051 File Offset: 0x00006251
		public int AgentIndex { get; private set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x0000805A File Offset: 0x0000625A
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x00008062 File Offset: 0x00006262
		public MatrixFrame AttachLocalFrame { get; private set; }

		// Token: 0x0600045D RID: 1117 RVA: 0x0000806B File Offset: 0x0000626B
		public AttachWeaponToWeaponInAgentEquipmentSlot(MissionWeapon weapon, int agentIndex, EquipmentIndex slot, MatrixFrame attachLocalFrame)
		{
			this.Weapon = weapon;
			this.AgentIndex = agentIndex;
			this.SlotIndex = slot;
			this.AttachLocalFrame = attachLocalFrame;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00008090 File Offset: 0x00006290
		public AttachWeaponToWeaponInAgentEquipmentSlot()
		{
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00008098 File Offset: 0x00006298
		protected override void OnWrite()
		{
			ModuleNetworkData.WriteWeaponReferenceToPacket(this.Weapon);
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			GameNetworkMessage.WriteIntToPacket((int)this.SlotIndex, CompressionMission.ItemSlotCompressionInfo);
			GameNetworkMessage.WriteVec3ToPacket(this.AttachLocalFrame.origin, CompressionBasic.LocalPositionCompressionInfo);
			GameNetworkMessage.WriteRotationMatrixToPacket(this.AttachLocalFrame.rotation);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x000080F0 File Offset: 0x000062F0
		protected override bool OnRead()
		{
			bool flag = true;
			this.Weapon = ModuleNetworkData.ReadWeaponReferenceFromPacket(MBObjectManager.Instance, ref flag);
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref flag);
			this.SlotIndex = (EquipmentIndex)GameNetworkMessage.ReadIntFromPacket(CompressionMission.ItemSlotCompressionInfo, ref flag);
			Vec3 o = GameNetworkMessage.ReadVec3FromPacket(CompressionBasic.LocalPositionCompressionInfo, ref flag);
			Mat3 rot = GameNetworkMessage.ReadRotationMatrixFromPacket(ref flag);
			if (flag)
			{
				this.AttachLocalFrame = new MatrixFrame(rot, o);
			}
			return flag;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00008156 File Offset: 0x00006356
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Items | MultiplayerMessageFilter.AgentsDetailed;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00008160 File Offset: 0x00006360
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"AttachWeaponToWeaponInAgentEquipmentSlot with name: ",
				(!this.Weapon.IsEmpty) ? this.Weapon.Item.Name : TextObject.Empty,
				" to SlotIndex: ",
				this.SlotIndex,
				" on agent-index: ",
				this.AgentIndex
			});
		}
	}
}
