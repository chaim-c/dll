using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x0200003A RID: 58
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class UnselectSiegeWeapon : GameNetworkMessage
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00004130 File Offset: 0x00002330
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x00004138 File Offset: 0x00002338
		public MissionObjectId SiegeWeaponId { get; private set; }

		// Token: 0x060001D2 RID: 466 RVA: 0x00004141 File Offset: 0x00002341
		public UnselectSiegeWeapon(MissionObjectId siegeWeaponId)
		{
			this.SiegeWeaponId = siegeWeaponId;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00004150 File Offset: 0x00002350
		public UnselectSiegeWeapon()
		{
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00004158 File Offset: 0x00002358
		protected override bool OnRead()
		{
			bool result = true;
			this.SiegeWeaponId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			return result;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00004175 File Offset: 0x00002375
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.SiegeWeaponId);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00004182 File Offset: 0x00002382
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.SiegeWeaponsDetailed;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000418A File Offset: 0x0000238A
		protected override string OnGetLogFormat()
		{
			return "Deselect SiegeWeapon with ID: " + this.SiegeWeaponId;
		}
	}
}
