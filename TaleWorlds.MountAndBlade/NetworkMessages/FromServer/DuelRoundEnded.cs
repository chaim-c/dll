using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000049 RID: 73
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class DuelRoundEnded : GameNetworkMessage
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00004E33 File Offset: 0x00003033
		// (set) Token: 0x0600026F RID: 623 RVA: 0x00004E3B File Offset: 0x0000303B
		public NetworkCommunicator WinnerPeer { get; private set; }

		// Token: 0x06000270 RID: 624 RVA: 0x00004E44 File Offset: 0x00003044
		public DuelRoundEnded(NetworkCommunicator winnerPeer)
		{
			this.WinnerPeer = winnerPeer;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00004E53 File Offset: 0x00003053
		public DuelRoundEnded()
		{
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00004E5C File Offset: 0x0000305C
		protected override bool OnRead()
		{
			bool result = true;
			this.WinnerPeer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			return result;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00004E7A File Offset: 0x0000307A
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.WinnerPeer);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00004E87 File Offset: 0x00003087
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.GameMode;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00004E8F File Offset: 0x0000308F
		protected override string OnGetLogFormat()
		{
			return this.WinnerPeer.UserName + "has won the duel against round.";
		}
	}
}
