using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000B1 RID: 177
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetRangedSiegeWeaponAmmo : GameNetworkMessage
	{
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x0000C559 File Offset: 0x0000A759
		// (set) Token: 0x06000721 RID: 1825 RVA: 0x0000C561 File Offset: 0x0000A761
		public MissionObjectId RangedSiegeWeaponId { get; private set; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0000C56A File Offset: 0x0000A76A
		// (set) Token: 0x06000723 RID: 1827 RVA: 0x0000C572 File Offset: 0x0000A772
		public int AmmoCount { get; private set; }

		// Token: 0x06000724 RID: 1828 RVA: 0x0000C57B File Offset: 0x0000A77B
		public SetRangedSiegeWeaponAmmo(MissionObjectId rangedSiegeWeaponId, int ammoCount)
		{
			this.RangedSiegeWeaponId = rangedSiegeWeaponId;
			this.AmmoCount = ammoCount;
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0000C591 File Offset: 0x0000A791
		public SetRangedSiegeWeaponAmmo()
		{
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0000C59C File Offset: 0x0000A79C
		protected override bool OnRead()
		{
			bool result = true;
			this.RangedSiegeWeaponId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.AmmoCount = GameNetworkMessage.ReadIntFromPacket(CompressionMission.RangedSiegeWeaponAmmoCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0000C5CB File Offset: 0x0000A7CB
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.RangedSiegeWeaponId);
			GameNetworkMessage.WriteIntToPacket(this.AmmoCount, CompressionMission.RangedSiegeWeaponAmmoCompressionInfo);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0000C5E8 File Offset: 0x0000A7E8
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.SiegeWeaponsDetailed;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0000C5F0 File Offset: 0x0000A7F0
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Set ammo left to: ",
				this.AmmoCount,
				" on RangedSiegeWeapon with ID: ",
				this.RangedSiegeWeaponId
			});
		}
	}
}
