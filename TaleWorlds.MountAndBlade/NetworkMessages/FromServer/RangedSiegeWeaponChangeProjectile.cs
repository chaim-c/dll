using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000091 RID: 145
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class RangedSiegeWeaponChangeProjectile : GameNetworkMessage
	{
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x0000A5CF File Offset: 0x000087CF
		// (set) Token: 0x060005C0 RID: 1472 RVA: 0x0000A5D7 File Offset: 0x000087D7
		public MissionObjectId RangedSiegeWeaponId { get; private set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x0000A5E0 File Offset: 0x000087E0
		// (set) Token: 0x060005C2 RID: 1474 RVA: 0x0000A5E8 File Offset: 0x000087E8
		public int Index { get; private set; }

		// Token: 0x060005C3 RID: 1475 RVA: 0x0000A5F1 File Offset: 0x000087F1
		public RangedSiegeWeaponChangeProjectile(MissionObjectId rangedSiegeWeaponId, int index)
		{
			this.RangedSiegeWeaponId = rangedSiegeWeaponId;
			this.Index = index;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0000A607 File Offset: 0x00008807
		public RangedSiegeWeaponChangeProjectile()
		{
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0000A610 File Offset: 0x00008810
		protected override bool OnRead()
		{
			bool result = true;
			this.RangedSiegeWeaponId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.Index = GameNetworkMessage.ReadIntFromPacket(CompressionMission.RangedSiegeWeaponAmmoIndexCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0000A63F File Offset: 0x0000883F
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.RangedSiegeWeaponId);
			GameNetworkMessage.WriteIntToPacket(this.Index, CompressionMission.RangedSiegeWeaponAmmoIndexCompressionInfo);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0000A65C File Offset: 0x0000885C
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.SiegeWeaponsDetailed;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0000A664 File Offset: 0x00008864
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Changed Projectile Type Index to: ",
				this.Index,
				" on RangedSiegeWeapon with ID: ",
				this.RangedSiegeWeaponId
			});
		}
	}
}
