using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000034 RID: 52
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class SelectSiegeWeapon : GameNetworkMessage
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00003DC8 File Offset: 0x00001FC8
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00003DD0 File Offset: 0x00001FD0
		public MissionObjectId SiegeWeaponId { get; private set; }

		// Token: 0x0600019C RID: 412 RVA: 0x00003DD9 File Offset: 0x00001FD9
		public SelectSiegeWeapon(MissionObjectId siegeWeaponId)
		{
			this.SiegeWeaponId = siegeWeaponId;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00003DE8 File Offset: 0x00001FE8
		public SelectSiegeWeapon()
		{
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00003DF0 File Offset: 0x00001FF0
		protected override bool OnRead()
		{
			bool result = true;
			this.SiegeWeaponId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			return result;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00003E0D File Offset: 0x0000200D
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.SiegeWeaponId);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00003E1A File Offset: 0x0000201A
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.SiegeWeaponsDetailed;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00003E22 File Offset: 0x00002022
		protected override string OnGetLogFormat()
		{
			return "Select SiegeWeapon with ID: " + this.SiegeWeaponId;
		}
	}
}
