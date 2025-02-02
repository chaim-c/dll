using System;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.ObjectSystem;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000077 RID: 119
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class AttachWeaponToSpawnedWeapon : GameNetworkMessage
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x00007ED1 File Offset: 0x000060D1
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x00007ED9 File Offset: 0x000060D9
		public MissionWeapon Weapon { get; private set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x00007EE2 File Offset: 0x000060E2
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x00007EEA File Offset: 0x000060EA
		public MissionObjectId MissionObjectId { get; private set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x00007EF3 File Offset: 0x000060F3
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x00007EFB File Offset: 0x000060FB
		public MatrixFrame AttachLocalFrame { get; private set; }

		// Token: 0x0600044F RID: 1103 RVA: 0x00007F04 File Offset: 0x00006104
		public AttachWeaponToSpawnedWeapon(MissionWeapon weapon, MissionObjectId missionObjectId, MatrixFrame attachLocalFrame)
		{
			this.Weapon = weapon;
			this.MissionObjectId = missionObjectId;
			this.AttachLocalFrame = attachLocalFrame;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00007F21 File Offset: 0x00006121
		public AttachWeaponToSpawnedWeapon()
		{
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00007F29 File Offset: 0x00006129
		protected override void OnWrite()
		{
			ModuleNetworkData.WriteWeaponReferenceToPacket(this.Weapon);
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.MissionObjectId);
			GameNetworkMessage.WriteVec3ToPacket(this.AttachLocalFrame.origin, CompressionBasic.LocalPositionCompressionInfo);
			GameNetworkMessage.WriteRotationMatrixToPacket(this.AttachLocalFrame.rotation);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00007F68 File Offset: 0x00006168
		protected override bool OnRead()
		{
			bool flag = true;
			this.Weapon = ModuleNetworkData.ReadWeaponReferenceFromPacket(MBObjectManager.Instance, ref flag);
			this.MissionObjectId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref flag);
			Vec3 o = GameNetworkMessage.ReadVec3FromPacket(CompressionBasic.LocalPositionCompressionInfo, ref flag);
			Mat3 rot = GameNetworkMessage.ReadRotationMatrixFromPacket(ref flag);
			if (flag)
			{
				this.AttachLocalFrame = new MatrixFrame(rot, o);
			}
			return flag;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00007FBC File Offset: 0x000061BC
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Items | MultiplayerMessageFilter.AgentsDetailed;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00007FC4 File Offset: 0x000061C4
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"AttachWeaponToSpawnedWeapon with name: ",
				(!this.Weapon.IsEmpty) ? this.Weapon.Item.Name : TextObject.Empty,
				" to MissionObject: ",
				this.MissionObjectId
			});
		}
	}
}
