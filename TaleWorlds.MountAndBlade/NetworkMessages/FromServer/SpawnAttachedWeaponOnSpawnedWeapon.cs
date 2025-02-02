using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000C0 RID: 192
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SpawnAttachedWeaponOnSpawnedWeapon : GameNetworkMessage
	{
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x0000D33B File Offset: 0x0000B53B
		// (set) Token: 0x060007C5 RID: 1989 RVA: 0x0000D343 File Offset: 0x0000B543
		public MissionObjectId SpawnedWeaponId { get; private set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x0000D34C File Offset: 0x0000B54C
		// (set) Token: 0x060007C7 RID: 1991 RVA: 0x0000D354 File Offset: 0x0000B554
		public int AttachmentIndex { get; private set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x0000D35D File Offset: 0x0000B55D
		// (set) Token: 0x060007C9 RID: 1993 RVA: 0x0000D365 File Offset: 0x0000B565
		public int ForcedIndex { get; private set; }

		// Token: 0x060007CA RID: 1994 RVA: 0x0000D36E File Offset: 0x0000B56E
		public SpawnAttachedWeaponOnSpawnedWeapon(MissionObjectId spawnedWeaponId, int attachmentIndex, int forcedIndex)
		{
			this.SpawnedWeaponId = spawnedWeaponId;
			this.AttachmentIndex = attachmentIndex;
			this.ForcedIndex = forcedIndex;
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0000D38B File Offset: 0x0000B58B
		public SpawnAttachedWeaponOnSpawnedWeapon()
		{
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0000D394 File Offset: 0x0000B594
		protected override bool OnRead()
		{
			bool result = true;
			this.SpawnedWeaponId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.AttachmentIndex = GameNetworkMessage.ReadIntFromPacket(CompressionMission.WeaponAttachmentIndexCompressionInfo, ref result);
			this.ForcedIndex = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.MissionObjectIDCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0000D3D5 File Offset: 0x0000B5D5
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.SpawnedWeaponId);
			GameNetworkMessage.WriteIntToPacket(this.AttachmentIndex, CompressionMission.WeaponAttachmentIndexCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.ForcedIndex, CompressionBasic.MissionObjectIDCompressionInfo);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0000D402 File Offset: 0x0000B602
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Items;
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0000D408 File Offset: 0x0000B608
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"SpawnAttachedWeaponOnSpawnedWeapon with Spawned Weapon ID: ",
				this.SpawnedWeaponId,
				" AttachmentIndex: ",
				this.AttachmentIndex,
				" Attached Weapon ID: ",
				this.ForcedIndex
			});
		}
	}
}
