using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x0200000E RID: 14
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class RequestPerkChange : GameNetworkMessage
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002709 File Offset: 0x00000909
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002711 File Offset: 0x00000911
		public int PerkListIndex { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000271A File Offset: 0x0000091A
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002722 File Offset: 0x00000922
		public int PerkIndex { get; private set; }

		// Token: 0x0600005D RID: 93 RVA: 0x0000272B File Offset: 0x0000092B
		public RequestPerkChange(int perkListIndex, int perkIndex)
		{
			this.PerkListIndex = perkListIndex;
			this.PerkIndex = perkIndex;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002741 File Offset: 0x00000941
		public RequestPerkChange()
		{
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000274C File Offset: 0x0000094C
		protected override bool OnRead()
		{
			bool result = true;
			this.PerkListIndex = GameNetworkMessage.ReadIntFromPacket(CompressionMission.PerkListIndexCompressionInfo, ref result);
			this.PerkIndex = GameNetworkMessage.ReadIntFromPacket(CompressionMission.PerkIndexCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002780 File Offset: 0x00000980
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket(this.PerkListIndex, CompressionMission.PerkListIndexCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.PerkIndex, CompressionMission.PerkIndexCompressionInfo);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000027A2 File Offset: 0x000009A2
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Equipment;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000027A7 File Offset: 0x000009A7
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Requesting perk selection in list ",
				this.PerkListIndex,
				" change to ",
				this.PerkIndex
			});
		}
	}
}
