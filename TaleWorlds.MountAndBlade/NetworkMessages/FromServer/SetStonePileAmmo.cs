using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000B8 RID: 184
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetStonePileAmmo : GameNetworkMessage
	{
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x0000CA55 File Offset: 0x0000AC55
		// (set) Token: 0x06000763 RID: 1891 RVA: 0x0000CA5D File Offset: 0x0000AC5D
		public MissionObjectId StonePileId { get; private set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x0000CA66 File Offset: 0x0000AC66
		// (set) Token: 0x06000765 RID: 1893 RVA: 0x0000CA6E File Offset: 0x0000AC6E
		public int AmmoCount { get; private set; }

		// Token: 0x06000766 RID: 1894 RVA: 0x0000CA77 File Offset: 0x0000AC77
		public SetStonePileAmmo(MissionObjectId stonePileId, int ammoCount)
		{
			this.StonePileId = stonePileId;
			this.AmmoCount = ammoCount;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0000CA8D File Offset: 0x0000AC8D
		public SetStonePileAmmo()
		{
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0000CA98 File Offset: 0x0000AC98
		protected override bool OnRead()
		{
			bool result = true;
			this.StonePileId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.AmmoCount = GameNetworkMessage.ReadIntFromPacket(CompressionMission.RangedSiegeWeaponAmmoCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0000CAC7 File Offset: 0x0000ACC7
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.StonePileId);
			GameNetworkMessage.WriteIntToPacket(this.AmmoCount, CompressionMission.RangedSiegeWeaponAmmoCompressionInfo);
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0000CAE4 File Offset: 0x0000ACE4
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.SiegeWeaponsDetailed;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0000CAEC File Offset: 0x0000ACEC
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Set ammo left to: ",
				this.AmmoCount,
				" on StonePile with ID: ",
				this.StonePileId
			});
		}
	}
}
