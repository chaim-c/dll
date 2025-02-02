using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200006D RID: 109
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class RoundWinnerChange : GameNetworkMessage
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00007373 File Offset: 0x00005573
		// (set) Token: 0x060003D2 RID: 978 RVA: 0x0000737B File Offset: 0x0000557B
		public BattleSideEnum RoundWinner { get; private set; }

		// Token: 0x060003D3 RID: 979 RVA: 0x00007384 File Offset: 0x00005584
		public RoundWinnerChange(BattleSideEnum roundWinner)
		{
			this.RoundWinner = roundWinner;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00007393 File Offset: 0x00005593
		public RoundWinnerChange()
		{
			this.RoundWinner = BattleSideEnum.None;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x000073A4 File Offset: 0x000055A4
		protected override bool OnRead()
		{
			bool result = true;
			this.RoundWinner = (BattleSideEnum)GameNetworkMessage.ReadIntFromPacket(CompressionMission.TeamSideCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x000073C6 File Offset: 0x000055C6
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket((int)this.RoundWinner, CompressionMission.TeamSideCompressionInfo);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x000073D8 File Offset: 0x000055D8
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Mission;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x000073E0 File Offset: 0x000055E0
		protected override string OnGetLogFormat()
		{
			return "Change round winner to: " + this.RoundWinner.ToString();
		}
	}
}
