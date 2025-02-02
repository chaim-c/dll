using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000BB RID: 187
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetWeaponAmmoData : GameNetworkMessage
	{
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x0000CCDD File Offset: 0x0000AEDD
		// (set) Token: 0x06000781 RID: 1921 RVA: 0x0000CCE5 File Offset: 0x0000AEE5
		public int AgentIndex { get; private set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x0000CCEE File Offset: 0x0000AEEE
		// (set) Token: 0x06000783 RID: 1923 RVA: 0x0000CCF6 File Offset: 0x0000AEF6
		public EquipmentIndex WeaponEquipmentIndex { get; private set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x0000CCFF File Offset: 0x0000AEFF
		// (set) Token: 0x06000785 RID: 1925 RVA: 0x0000CD07 File Offset: 0x0000AF07
		public EquipmentIndex AmmoEquipmentIndex { get; private set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x0000CD10 File Offset: 0x0000AF10
		// (set) Token: 0x06000787 RID: 1927 RVA: 0x0000CD18 File Offset: 0x0000AF18
		public short Ammo { get; private set; }

		// Token: 0x06000788 RID: 1928 RVA: 0x0000CD21 File Offset: 0x0000AF21
		public SetWeaponAmmoData(int agentIndex, EquipmentIndex weaponEquipmentIndex, EquipmentIndex ammoEquipmentIndex, short ammo)
		{
			this.AgentIndex = agentIndex;
			this.WeaponEquipmentIndex = weaponEquipmentIndex;
			this.AmmoEquipmentIndex = ammoEquipmentIndex;
			this.Ammo = ammo;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0000CD46 File Offset: 0x0000AF46
		public SetWeaponAmmoData()
		{
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0000CD50 File Offset: 0x0000AF50
		protected override bool OnRead()
		{
			bool result = true;
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.WeaponEquipmentIndex = (EquipmentIndex)GameNetworkMessage.ReadIntFromPacket(CompressionMission.ItemSlotCompressionInfo, ref result);
			this.AmmoEquipmentIndex = (EquipmentIndex)GameNetworkMessage.ReadIntFromPacket(CompressionMission.WieldSlotCompressionInfo, ref result);
			this.Ammo = (short)GameNetworkMessage.ReadIntFromPacket(CompressionMission.ItemDataCompressionInfo, ref result);
			return result;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0000CDA4 File Offset: 0x0000AFA4
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			GameNetworkMessage.WriteIntToPacket((int)this.WeaponEquipmentIndex, CompressionMission.ItemSlotCompressionInfo);
			GameNetworkMessage.WriteIntToPacket((int)this.AmmoEquipmentIndex, CompressionMission.WieldSlotCompressionInfo);
			GameNetworkMessage.WriteIntToPacket((int)this.Ammo, CompressionMission.ItemDataCompressionInfo);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0000CDE1 File Offset: 0x0000AFE1
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.EquipmentDetailed;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0000CDE8 File Offset: 0x0000AFE8
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Set ammo: ",
				this.Ammo,
				" for weapon with EquipmentIndex: ",
				this.WeaponEquipmentIndex,
				" on Agent with agent-index: ",
				this.AgentIndex
			});
		}
	}
}
