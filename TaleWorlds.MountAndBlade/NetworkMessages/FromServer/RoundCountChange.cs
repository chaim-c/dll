using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200006A RID: 106
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class RoundCountChange : GameNetworkMessage
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x00007166 File Offset: 0x00005366
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x0000716E File Offset: 0x0000536E
		public int RoundCount { get; private set; }

		// Token: 0x060003B7 RID: 951 RVA: 0x00007177 File Offset: 0x00005377
		public RoundCountChange(int roundCount)
		{
			this.RoundCount = roundCount;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00007186 File Offset: 0x00005386
		public RoundCountChange()
		{
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00007190 File Offset: 0x00005390
		protected override bool OnRead()
		{
			bool result = true;
			this.RoundCount = GameNetworkMessage.ReadIntFromPacket(CompressionMission.MissionRoundCountCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x000071B2 File Offset: 0x000053B2
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket(this.RoundCount, CompressionMission.MissionRoundCountCompressionInfo);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x000071C4 File Offset: 0x000053C4
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Mission;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x000071CC File Offset: 0x000053CC
		protected override string OnGetLogFormat()
		{
			return "Change round count to: " + this.RoundCount;
		}
	}
}
