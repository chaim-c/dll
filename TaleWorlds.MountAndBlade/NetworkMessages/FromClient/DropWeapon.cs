using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x0200002C RID: 44
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class DropWeapon : GameNetworkMessage
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00003AE5 File Offset: 0x00001CE5
		// (set) Token: 0x06000163 RID: 355 RVA: 0x00003AED File Offset: 0x00001CED
		public bool IsDefendPressed { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00003AF6 File Offset: 0x00001CF6
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00003AFE File Offset: 0x00001CFE
		public EquipmentIndex ForcedSlotIndexToDropWeaponFrom { get; private set; }

		// Token: 0x06000166 RID: 358 RVA: 0x00003B07 File Offset: 0x00001D07
		public DropWeapon(bool isDefendPressed, EquipmentIndex forcedSlotIndexToDropWeaponFrom)
		{
			this.IsDefendPressed = isDefendPressed;
			this.ForcedSlotIndexToDropWeaponFrom = forcedSlotIndexToDropWeaponFrom;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00003B1D File Offset: 0x00001D1D
		public DropWeapon()
		{
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00003B28 File Offset: 0x00001D28
		protected override bool OnRead()
		{
			bool result = true;
			this.IsDefendPressed = GameNetworkMessage.ReadBoolFromPacket(ref result);
			this.ForcedSlotIndexToDropWeaponFrom = (EquipmentIndex)GameNetworkMessage.ReadIntFromPacket(CompressionMission.WieldSlotCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00003B57 File Offset: 0x00001D57
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteBoolToPacket(this.IsDefendPressed);
			GameNetworkMessage.WriteIntToPacket((int)this.ForcedSlotIndexToDropWeaponFrom, CompressionMission.WieldSlotCompressionInfo);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00003B74 File Offset: 0x00001D74
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Items;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00003B78 File Offset: 0x00001D78
		protected override string OnGetLogFormat()
		{
			bool flag = this.ForcedSlotIndexToDropWeaponFrom != EquipmentIndex.None;
			return "Dropping " + ((!flag) ? "equipped" : "") + " weapon" + (flag ? (" " + (int)this.ForcedSlotIndexToDropWeaponFrom) : "");
		}
	}
}
