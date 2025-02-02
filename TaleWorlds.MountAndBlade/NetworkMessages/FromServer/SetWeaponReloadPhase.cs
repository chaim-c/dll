using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000BD RID: 189
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetWeaponReloadPhase : GameNetworkMessage
	{
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x0000CF6A File Offset: 0x0000B16A
		// (set) Token: 0x0600079B RID: 1947 RVA: 0x0000CF72 File Offset: 0x0000B172
		public int AgentIndex { get; private set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x0000CF7B File Offset: 0x0000B17B
		// (set) Token: 0x0600079D RID: 1949 RVA: 0x0000CF83 File Offset: 0x0000B183
		public EquipmentIndex EquipmentIndex { get; private set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x0000CF8C File Offset: 0x0000B18C
		// (set) Token: 0x0600079F RID: 1951 RVA: 0x0000CF94 File Offset: 0x0000B194
		public short ReloadPhase { get; private set; }

		// Token: 0x060007A0 RID: 1952 RVA: 0x0000CF9D File Offset: 0x0000B19D
		public SetWeaponReloadPhase(int agentIndex, EquipmentIndex equipmentIndex, short reloadPhase)
		{
			this.AgentIndex = agentIndex;
			this.EquipmentIndex = equipmentIndex;
			this.ReloadPhase = reloadPhase;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0000CFBA File Offset: 0x0000B1BA
		public SetWeaponReloadPhase()
		{
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0000CFC4 File Offset: 0x0000B1C4
		protected override bool OnRead()
		{
			bool result = true;
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.EquipmentIndex = (EquipmentIndex)GameNetworkMessage.ReadIntFromPacket(CompressionMission.ItemSlotCompressionInfo, ref result);
			this.ReloadPhase = (short)GameNetworkMessage.ReadIntFromPacket(CompressionMission.WeaponReloadPhaseCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0000D006 File Offset: 0x0000B206
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			GameNetworkMessage.WriteIntToPacket((int)this.EquipmentIndex, CompressionMission.ItemSlotCompressionInfo);
			GameNetworkMessage.WriteIntToPacket((int)this.ReloadPhase, CompressionMission.WeaponReloadPhaseCompressionInfo);
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0000D033 File Offset: 0x0000B233
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.EquipmentDetailed;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0000D038 File Offset: 0x0000B238
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Set Reload Phase: ",
				this.ReloadPhase,
				" for weapon with EquipmentIndex: ",
				this.EquipmentIndex,
				" on Agent with agent-index: ",
				this.AgentIndex
			});
		}
	}
}
