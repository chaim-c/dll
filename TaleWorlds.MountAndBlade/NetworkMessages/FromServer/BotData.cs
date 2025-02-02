using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200004D RID: 77
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class BotData : GameNetworkMessage
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000294 RID: 660 RVA: 0x000051E3 File Offset: 0x000033E3
		// (set) Token: 0x06000295 RID: 661 RVA: 0x000051EB File Offset: 0x000033EB
		public BattleSideEnum Side { get; private set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000296 RID: 662 RVA: 0x000051F4 File Offset: 0x000033F4
		// (set) Token: 0x06000297 RID: 663 RVA: 0x000051FC File Offset: 0x000033FC
		public int KillCount { get; private set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00005205 File Offset: 0x00003405
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0000520D File Offset: 0x0000340D
		public int AssistCount { get; private set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00005216 File Offset: 0x00003416
		// (set) Token: 0x0600029B RID: 667 RVA: 0x0000521E File Offset: 0x0000341E
		public int DeathCount { get; private set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00005227 File Offset: 0x00003427
		// (set) Token: 0x0600029D RID: 669 RVA: 0x0000522F File Offset: 0x0000342F
		public int AliveBotCount { get; private set; }

		// Token: 0x0600029E RID: 670 RVA: 0x00005238 File Offset: 0x00003438
		public BotData(BattleSideEnum side, int kill, int assist, int death, int alive)
		{
			this.Side = side;
			this.KillCount = kill;
			this.AssistCount = assist;
			this.DeathCount = death;
			this.AliveBotCount = alive;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00005265 File Offset: 0x00003465
		public BotData()
		{
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00005270 File Offset: 0x00003470
		protected override bool OnRead()
		{
			bool result = true;
			this.Side = (BattleSideEnum)GameNetworkMessage.ReadIntFromPacket(CompressionMission.TeamSideCompressionInfo, ref result);
			this.KillCount = GameNetworkMessage.ReadIntFromPacket(CompressionMatchmaker.KillDeathAssistCountCompressionInfo, ref result);
			this.AssistCount = GameNetworkMessage.ReadIntFromPacket(CompressionMatchmaker.KillDeathAssistCountCompressionInfo, ref result);
			this.DeathCount = GameNetworkMessage.ReadIntFromPacket(CompressionMatchmaker.KillDeathAssistCountCompressionInfo, ref result);
			this.AliveBotCount = GameNetworkMessage.ReadIntFromPacket(CompressionMission.AgentCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x000052DC File Offset: 0x000034DC
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket((int)this.Side, CompressionMission.TeamSideCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.KillCount, CompressionMatchmaker.KillDeathAssistCountCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.AssistCount, CompressionMatchmaker.KillDeathAssistCountCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.DeathCount, CompressionMatchmaker.KillDeathAssistCountCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.AliveBotCount, CompressionMission.AgentCompressionInfo);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00005339 File Offset: 0x00003539
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.General;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00005340 File Offset: 0x00003540
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"BOTS for side: ",
				this.Side,
				", Kill: ",
				this.KillCount,
				" Death: ",
				this.DeathCount,
				" Assist: ",
				this.AssistCount,
				", Alive: ",
				this.AliveBotCount
			});
		}
	}
}
