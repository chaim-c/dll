using System;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.ObjectSystem;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000C2 RID: 194
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SpawnWeaponWithNewEntity : GameNetworkMessage
	{
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x0000D67E File Offset: 0x0000B87E
		// (set) Token: 0x060007E3 RID: 2019 RVA: 0x0000D686 File Offset: 0x0000B886
		public MissionWeapon Weapon { get; private set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x0000D68F File Offset: 0x0000B88F
		// (set) Token: 0x060007E5 RID: 2021 RVA: 0x0000D697 File Offset: 0x0000B897
		public Mission.WeaponSpawnFlags WeaponSpawnFlags { get; private set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x0000D6A0 File Offset: 0x0000B8A0
		// (set) Token: 0x060007E7 RID: 2023 RVA: 0x0000D6A8 File Offset: 0x0000B8A8
		public int ForcedIndex { get; private set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x0000D6B1 File Offset: 0x0000B8B1
		// (set) Token: 0x060007E9 RID: 2025 RVA: 0x0000D6B9 File Offset: 0x0000B8B9
		public MatrixFrame Frame { get; private set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x0000D6C2 File Offset: 0x0000B8C2
		// (set) Token: 0x060007EB RID: 2027 RVA: 0x0000D6CA File Offset: 0x0000B8CA
		public MissionObjectId ParentMissionObjectId { get; private set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x0000D6D3 File Offset: 0x0000B8D3
		// (set) Token: 0x060007ED RID: 2029 RVA: 0x0000D6DB File Offset: 0x0000B8DB
		public bool IsVisible { get; private set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x0000D6E4 File Offset: 0x0000B8E4
		// (set) Token: 0x060007EF RID: 2031 RVA: 0x0000D6EC File Offset: 0x0000B8EC
		public bool HasLifeTime { get; private set; }

		// Token: 0x060007F0 RID: 2032 RVA: 0x0000D6F5 File Offset: 0x0000B8F5
		public SpawnWeaponWithNewEntity(MissionWeapon weapon, Mission.WeaponSpawnFlags weaponSpawnFlags, int forcedIndex, MatrixFrame frame, MissionObjectId parentMissionObjectId, bool isVisible, bool hasLifeTime)
		{
			this.Weapon = weapon;
			this.WeaponSpawnFlags = weaponSpawnFlags;
			this.ForcedIndex = forcedIndex;
			this.Frame = frame;
			this.ParentMissionObjectId = parentMissionObjectId;
			this.IsVisible = isVisible;
			this.HasLifeTime = hasLifeTime;
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0000D732 File Offset: 0x0000B932
		public SpawnWeaponWithNewEntity()
		{
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0000D73C File Offset: 0x0000B93C
		protected override bool OnRead()
		{
			bool result = true;
			this.Weapon = ModuleNetworkData.ReadWeaponReferenceFromPacket(MBObjectManager.Instance, ref result);
			this.Frame = GameNetworkMessage.ReadMatrixFrameFromPacket(ref result);
			this.WeaponSpawnFlags = (Mission.WeaponSpawnFlags)GameNetworkMessage.ReadUintFromPacket(CompressionMission.SpawnedItemWeaponSpawnFlagCompressionInfo, ref result);
			this.ForcedIndex = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.MissionObjectIDCompressionInfo, ref result);
			this.ParentMissionObjectId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.IsVisible = GameNetworkMessage.ReadBoolFromPacket(ref result);
			this.HasLifeTime = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0000D7B8 File Offset: 0x0000B9B8
		protected override void OnWrite()
		{
			ModuleNetworkData.WriteWeaponReferenceToPacket(this.Weapon);
			GameNetworkMessage.WriteMatrixFrameToPacket(this.Frame);
			GameNetworkMessage.WriteUintToPacket((uint)this.WeaponSpawnFlags, CompressionMission.SpawnedItemWeaponSpawnFlagCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.ForcedIndex, CompressionBasic.MissionObjectIDCompressionInfo);
			GameNetworkMessage.WriteMissionObjectIdToPacket((this.ParentMissionObjectId.Id >= 0) ? this.ParentMissionObjectId : MissionObjectId.Invalid);
			GameNetworkMessage.WriteBoolToPacket(this.IsVisible);
			GameNetworkMessage.WriteBoolToPacket(this.HasLifeTime);
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0000D831 File Offset: 0x0000BA31
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Items;
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0000D838 File Offset: 0x0000BA38
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Spawn Weapon with name: ",
				this.Weapon.Item.Name,
				", and with ID: ",
				this.ForcedIndex
			});
		}
	}
}
