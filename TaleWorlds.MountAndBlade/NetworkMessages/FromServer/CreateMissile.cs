using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000084 RID: 132
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class CreateMissile : GameNetworkMessage
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x00009405 File Offset: 0x00007605
		// (set) Token: 0x06000513 RID: 1299 RVA: 0x0000940D File Offset: 0x0000760D
		public int MissileIndex { get; private set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x00009416 File Offset: 0x00007616
		// (set) Token: 0x06000515 RID: 1301 RVA: 0x0000941E File Offset: 0x0000761E
		public int AgentIndex { get; private set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x00009427 File Offset: 0x00007627
		// (set) Token: 0x06000517 RID: 1303 RVA: 0x0000942F File Offset: 0x0000762F
		public EquipmentIndex WeaponIndex { get; private set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x00009438 File Offset: 0x00007638
		// (set) Token: 0x06000519 RID: 1305 RVA: 0x00009440 File Offset: 0x00007640
		public MissionWeapon Weapon { get; private set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x00009449 File Offset: 0x00007649
		// (set) Token: 0x0600051B RID: 1307 RVA: 0x00009451 File Offset: 0x00007651
		public Vec3 Position { get; private set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0000945A File Offset: 0x0000765A
		// (set) Token: 0x0600051D RID: 1309 RVA: 0x00009462 File Offset: 0x00007662
		public Vec3 Direction { get; private set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x0000946B File Offset: 0x0000766B
		// (set) Token: 0x0600051F RID: 1311 RVA: 0x00009473 File Offset: 0x00007673
		public float Speed { get; private set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0000947C File Offset: 0x0000767C
		// (set) Token: 0x06000521 RID: 1313 RVA: 0x00009484 File Offset: 0x00007684
		public Mat3 Orientation { get; private set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x0000948D File Offset: 0x0000768D
		// (set) Token: 0x06000523 RID: 1315 RVA: 0x00009495 File Offset: 0x00007695
		public bool HasRigidBody { get; private set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0000949E File Offset: 0x0000769E
		// (set) Token: 0x06000525 RID: 1317 RVA: 0x000094A6 File Offset: 0x000076A6
		public MissionObjectId MissionObjectToIgnoreId { get; private set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x000094AF File Offset: 0x000076AF
		// (set) Token: 0x06000527 RID: 1319 RVA: 0x000094B7 File Offset: 0x000076B7
		public bool IsPrimaryWeaponShot { get; private set; }

		// Token: 0x06000528 RID: 1320 RVA: 0x000094C0 File Offset: 0x000076C0
		public CreateMissile(int missileIndex, int agentIndex, EquipmentIndex weaponIndex, MissionWeapon weapon, Vec3 position, Vec3 direction, float speed, Mat3 orientation, bool hasRigidBody, MissionObjectId missionObjectToIgnoreId, bool isPrimaryWeaponShot)
		{
			this.MissileIndex = missileIndex;
			this.AgentIndex = agentIndex;
			this.WeaponIndex = weaponIndex;
			this.Weapon = weapon;
			this.Position = position;
			this.Direction = direction;
			this.Speed = speed;
			this.Orientation = orientation;
			this.HasRigidBody = hasRigidBody;
			this.MissionObjectToIgnoreId = missionObjectToIgnoreId;
			this.IsPrimaryWeaponShot = isPrimaryWeaponShot;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00009528 File Offset: 0x00007728
		public CreateMissile()
		{
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00009530 File Offset: 0x00007730
		protected override bool OnRead()
		{
			bool result = true;
			this.MissileIndex = GameNetworkMessage.ReadIntFromPacket(CompressionMission.MissileCompressionInfo, ref result);
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.WeaponIndex = (EquipmentIndex)GameNetworkMessage.ReadIntFromPacket(CompressionMission.WieldSlotCompressionInfo, ref result);
			if (this.WeaponIndex == EquipmentIndex.None)
			{
				this.Weapon = ModuleNetworkData.ReadMissileWeaponReferenceFromPacket(Game.Current.ObjectManager, ref result);
			}
			this.Position = GameNetworkMessage.ReadVec3FromPacket(CompressionBasic.PositionCompressionInfo, ref result);
			this.Direction = GameNetworkMessage.ReadVec3FromPacket(CompressionBasic.UnitVectorCompressionInfo, ref result);
			this.Speed = GameNetworkMessage.ReadFloatFromPacket(CompressionMission.MissileSpeedCompressionInfo, ref result);
			this.HasRigidBody = GameNetworkMessage.ReadBoolFromPacket(ref result);
			if (this.HasRigidBody)
			{
				this.Orientation = GameNetworkMessage.ReadRotationMatrixFromPacket(ref result);
				this.MissionObjectToIgnoreId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			}
			else
			{
				Vec3 f = GameNetworkMessage.ReadVec3FromPacket(CompressionBasic.UnitVectorCompressionInfo, ref result);
				this.Orientation = new Mat3(Vec3.Side, f, Vec3.Up);
				this.Orientation.Orthonormalize();
			}
			this.IsPrimaryWeaponShot = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00009638 File Offset: 0x00007838
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket(this.MissileIndex, CompressionMission.MissileCompressionInfo);
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			GameNetworkMessage.WriteIntToPacket((int)this.WeaponIndex, CompressionMission.WieldSlotCompressionInfo);
			if (this.WeaponIndex == EquipmentIndex.None)
			{
				ModuleNetworkData.WriteMissileWeaponReferenceToPacket(this.Weapon);
			}
			GameNetworkMessage.WriteVec3ToPacket(this.Position, CompressionBasic.PositionCompressionInfo);
			GameNetworkMessage.WriteVec3ToPacket(this.Direction, CompressionBasic.UnitVectorCompressionInfo);
			GameNetworkMessage.WriteFloatToPacket(this.Speed, CompressionMission.MissileSpeedCompressionInfo);
			GameNetworkMessage.WriteBoolToPacket(this.HasRigidBody);
			if (this.HasRigidBody)
			{
				GameNetworkMessage.WriteRotationMatrixToPacket(this.Orientation);
				GameNetworkMessage.WriteMissionObjectIdToPacket((this.MissionObjectToIgnoreId.Id >= 0) ? this.MissionObjectToIgnoreId : MissionObjectId.Invalid);
			}
			else
			{
				GameNetworkMessage.WriteVec3ToPacket(this.Orientation.f, CompressionBasic.UnitVectorCompressionInfo);
			}
			GameNetworkMessage.WriteBoolToPacket(this.IsPrimaryWeaponShot);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00009714 File Offset: 0x00007914
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionDetailed;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0000971C File Offset: 0x0000791C
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Create a missile with index: ",
				this.MissileIndex,
				" on agent with agent-index: ",
				this.AgentIndex
			});
		}
	}
}
