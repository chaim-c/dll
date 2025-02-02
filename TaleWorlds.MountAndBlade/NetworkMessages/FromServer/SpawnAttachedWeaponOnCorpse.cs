using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000BF RID: 191
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SpawnAttachedWeaponOnCorpse : GameNetworkMessage
	{
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x0000D237 File Offset: 0x0000B437
		// (set) Token: 0x060007B9 RID: 1977 RVA: 0x0000D23F File Offset: 0x0000B43F
		public int AgentIndex { get; private set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x0000D248 File Offset: 0x0000B448
		// (set) Token: 0x060007BB RID: 1979 RVA: 0x0000D250 File Offset: 0x0000B450
		public int AttachedIndex { get; private set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x0000D259 File Offset: 0x0000B459
		// (set) Token: 0x060007BD RID: 1981 RVA: 0x0000D261 File Offset: 0x0000B461
		public int ForcedIndex { get; private set; }

		// Token: 0x060007BE RID: 1982 RVA: 0x0000D26A File Offset: 0x0000B46A
		public SpawnAttachedWeaponOnCorpse(int agentIndex, int attachedIndex, int forcedIndex)
		{
			this.AgentIndex = agentIndex;
			this.AttachedIndex = attachedIndex;
			this.ForcedIndex = forcedIndex;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0000D287 File Offset: 0x0000B487
		public SpawnAttachedWeaponOnCorpse()
		{
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0000D290 File Offset: 0x0000B490
		protected override bool OnRead()
		{
			bool result = true;
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.AttachedIndex = GameNetworkMessage.ReadIntFromPacket(CompressionMission.WeaponAttachmentIndexCompressionInfo, ref result);
			this.ForcedIndex = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.MissionObjectIDCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0000D2D1 File Offset: 0x0000B4D1
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			GameNetworkMessage.WriteIntToPacket(this.AttachedIndex, CompressionMission.WeaponAttachmentIndexCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.ForcedIndex, CompressionBasic.MissionObjectIDCompressionInfo);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0000D2FE File Offset: 0x0000B4FE
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Items;
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0000D302 File Offset: 0x0000B502
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"SpawnAttachedWeaponOnCorpse with agent-index: ",
				this.AgentIndex,
				", and with ID: ",
				this.ForcedIndex
			});
		}
	}
}
