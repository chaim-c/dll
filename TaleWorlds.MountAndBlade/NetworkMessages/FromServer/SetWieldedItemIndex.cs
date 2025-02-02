using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000BE RID: 190
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetWieldedItemIndex : GameNetworkMessage
	{
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x0000D092 File Offset: 0x0000B292
		// (set) Token: 0x060007A7 RID: 1959 RVA: 0x0000D09A File Offset: 0x0000B29A
		public int AgentIndex { get; private set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0000D0A3 File Offset: 0x0000B2A3
		// (set) Token: 0x060007A9 RID: 1961 RVA: 0x0000D0AB File Offset: 0x0000B2AB
		public bool IsLeftHand { get; private set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x0000D0B4 File Offset: 0x0000B2B4
		// (set) Token: 0x060007AB RID: 1963 RVA: 0x0000D0BC File Offset: 0x0000B2BC
		public bool IsWieldedInstantly { get; private set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x0000D0C5 File Offset: 0x0000B2C5
		// (set) Token: 0x060007AD RID: 1965 RVA: 0x0000D0CD File Offset: 0x0000B2CD
		public bool IsWieldedOnSpawn { get; private set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x0000D0D6 File Offset: 0x0000B2D6
		// (set) Token: 0x060007AF RID: 1967 RVA: 0x0000D0DE File Offset: 0x0000B2DE
		public EquipmentIndex WieldedItemIndex { get; private set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x0000D0E7 File Offset: 0x0000B2E7
		// (set) Token: 0x060007B1 RID: 1969 RVA: 0x0000D0EF File Offset: 0x0000B2EF
		public int MainHandCurrentUsageIndex { get; private set; }

		// Token: 0x060007B2 RID: 1970 RVA: 0x0000D0F8 File Offset: 0x0000B2F8
		public SetWieldedItemIndex(int agentIndex, bool isLeftHand, bool isWieldedInstantly, bool isWieldedOnSpawn, EquipmentIndex wieldedItemIndex, int mainHandCurUsageIndex)
		{
			this.AgentIndex = agentIndex;
			this.IsLeftHand = isLeftHand;
			this.IsWieldedInstantly = isWieldedInstantly;
			this.IsWieldedOnSpawn = isWieldedOnSpawn;
			this.WieldedItemIndex = wieldedItemIndex;
			this.MainHandCurrentUsageIndex = mainHandCurUsageIndex;
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0000D12D File Offset: 0x0000B32D
		public SetWieldedItemIndex()
		{
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0000D138 File Offset: 0x0000B338
		protected override bool OnRead()
		{
			bool result = true;
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.IsLeftHand = GameNetworkMessage.ReadBoolFromPacket(ref result);
			this.IsWieldedInstantly = GameNetworkMessage.ReadBoolFromPacket(ref result);
			this.IsWieldedOnSpawn = GameNetworkMessage.ReadBoolFromPacket(ref result);
			this.WieldedItemIndex = (EquipmentIndex)GameNetworkMessage.ReadIntFromPacket(CompressionMission.WieldSlotCompressionInfo, ref result);
			this.MainHandCurrentUsageIndex = GameNetworkMessage.ReadIntFromPacket(CompressionMission.WeaponUsageIndexCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0000D1A0 File Offset: 0x0000B3A0
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			GameNetworkMessage.WriteBoolToPacket(this.IsLeftHand);
			GameNetworkMessage.WriteBoolToPacket(this.IsWieldedInstantly);
			GameNetworkMessage.WriteBoolToPacket(this.IsWieldedOnSpawn);
			GameNetworkMessage.WriteIntToPacket((int)this.WieldedItemIndex, CompressionMission.WieldSlotCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.MainHandCurrentUsageIndex, CompressionMission.WeaponUsageIndexCompressionInfo);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0000D1F9 File Offset: 0x0000B3F9
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.EquipmentDetailed;
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0000D1FE File Offset: 0x0000B3FE
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Set Wielded Item Index to: ",
				this.WieldedItemIndex,
				" on Agent with agent-index: ",
				this.AgentIndex
			});
		}
	}
}
