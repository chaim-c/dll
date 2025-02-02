using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.ObjectSystem;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200004B RID: 75
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class EquipEquipmentToPeer : GameNetworkMessage
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00004FAE File Offset: 0x000031AE
		// (set) Token: 0x06000281 RID: 641 RVA: 0x00004FB6 File Offset: 0x000031B6
		public NetworkCommunicator Peer { get; private set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000282 RID: 642 RVA: 0x00004FBF File Offset: 0x000031BF
		// (set) Token: 0x06000283 RID: 643 RVA: 0x00004FC7 File Offset: 0x000031C7
		public Equipment Equipment { get; private set; }

		// Token: 0x06000284 RID: 644 RVA: 0x00004FD0 File Offset: 0x000031D0
		public EquipEquipmentToPeer(NetworkCommunicator peer, Equipment equipment)
		{
			this.Peer = peer;
			this.Equipment = new Equipment();
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumEquipmentSetSlots; equipmentIndex++)
			{
				this.Equipment[equipmentIndex] = equipment.GetEquipmentFromSlot(equipmentIndex);
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00005015 File Offset: 0x00003215
		public EquipEquipmentToPeer()
		{
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00005020 File Offset: 0x00003220
		protected override bool OnRead()
		{
			bool flag = true;
			this.Peer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref flag, false);
			if (flag)
			{
				this.Equipment = new Equipment();
				for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumEquipmentSetSlots; equipmentIndex++)
				{
					if (flag)
					{
						this.Equipment.AddEquipmentToSlotWithoutAgent(equipmentIndex, ModuleNetworkData.ReadItemReferenceFromPacket(MBObjectManager.Instance, ref flag));
					}
				}
			}
			return flag;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00005074 File Offset: 0x00003274
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.Peer);
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumEquipmentSetSlots; equipmentIndex++)
			{
				ModuleNetworkData.WriteItemReferenceToPacket(this.Equipment.GetEquipmentFromSlot(equipmentIndex));
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000050AA File Offset: 0x000032AA
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Equipment;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x000050AF File Offset: 0x000032AF
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Equip equipment to peer: ",
				this.Peer.UserName,
				" with peer-index:",
				this.Peer.Index
			});
		}
	}
}
