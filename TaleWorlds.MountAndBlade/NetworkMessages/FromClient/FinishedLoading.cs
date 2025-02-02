using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x0200002D RID: 45
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class FinishedLoading : GameNetworkMessage
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00003BCF File Offset: 0x00001DCF
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00003BD7 File Offset: 0x00001DD7
		public int BattleIndex { get; private set; }

		// Token: 0x0600016E RID: 366 RVA: 0x00003BE0 File Offset: 0x00001DE0
		public FinishedLoading()
		{
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public FinishedLoading(int battleIndex)
		{
			this.BattleIndex = battleIndex;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00003BF8 File Offset: 0x00001DF8
		protected override bool OnRead()
		{
			bool result = true;
			this.BattleIndex = GameNetworkMessage.ReadIntFromPacket(CompressionMission.AutomatedBattleIndexCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00003C1A File Offset: 0x00001E1A
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket(this.BattleIndex, CompressionMission.AutomatedBattleIndexCompressionInfo);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00003C2C File Offset: 0x00001E2C
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.General;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00003C30 File Offset: 0x00001E30
		protected override string OnGetLogFormat()
		{
			return "Finished Loading";
		}
	}
}
