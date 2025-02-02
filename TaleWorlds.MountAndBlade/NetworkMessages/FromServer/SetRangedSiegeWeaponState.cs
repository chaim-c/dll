using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000B0 RID: 176
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetRangedSiegeWeaponState : GameNetworkMessage
	{
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x0000C48B File Offset: 0x0000A68B
		// (set) Token: 0x06000717 RID: 1815 RVA: 0x0000C493 File Offset: 0x0000A693
		public MissionObjectId RangedSiegeWeaponId { get; private set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x0000C49C File Offset: 0x0000A69C
		// (set) Token: 0x06000719 RID: 1817 RVA: 0x0000C4A4 File Offset: 0x0000A6A4
		public RangedSiegeWeapon.WeaponState State { get; private set; }

		// Token: 0x0600071A RID: 1818 RVA: 0x0000C4AD File Offset: 0x0000A6AD
		public SetRangedSiegeWeaponState(MissionObjectId rangedSiegeWeaponId, RangedSiegeWeapon.WeaponState state)
		{
			this.RangedSiegeWeaponId = rangedSiegeWeaponId;
			this.State = state;
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0000C4C3 File Offset: 0x0000A6C3
		public SetRangedSiegeWeaponState()
		{
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0000C4CC File Offset: 0x0000A6CC
		protected override bool OnRead()
		{
			bool result = true;
			this.RangedSiegeWeaponId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.State = (RangedSiegeWeapon.WeaponState)GameNetworkMessage.ReadIntFromPacket(CompressionMission.RangedSiegeWeaponStateCompressionInfo, ref result);
			return result;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0000C4FB File Offset: 0x0000A6FB
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.RangedSiegeWeaponId);
			GameNetworkMessage.WriteIntToPacket((int)this.State, CompressionMission.RangedSiegeWeaponStateCompressionInfo);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0000C518 File Offset: 0x0000A718
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.SiegeWeaponsDetailed;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0000C520 File Offset: 0x0000A720
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Set RangedSiegeWeapon State to: ",
				this.State,
				" on RangedSiegeWeapon with ID: ",
				this.RangedSiegeWeaponId
			});
		}
	}
}
