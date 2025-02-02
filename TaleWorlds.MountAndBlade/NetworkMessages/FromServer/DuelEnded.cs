using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000045 RID: 69
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class DuelEnded : GameNetworkMessage
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00004A7A File Offset: 0x00002C7A
		// (set) Token: 0x06000241 RID: 577 RVA: 0x00004A82 File Offset: 0x00002C82
		public NetworkCommunicator WinnerPeer { get; private set; }

		// Token: 0x06000242 RID: 578 RVA: 0x00004A8B File Offset: 0x00002C8B
		public DuelEnded(NetworkCommunicator winnerPeer)
		{
			this.WinnerPeer = winnerPeer;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00004A9A File Offset: 0x00002C9A
		public DuelEnded()
		{
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00004AA4 File Offset: 0x00002CA4
		protected override bool OnRead()
		{
			bool result = true;
			this.WinnerPeer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			return result;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00004AC2 File Offset: 0x00002CC2
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.WinnerPeer);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00004ACF File Offset: 0x00002CCF
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.GameMode;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00004AD7 File Offset: 0x00002CD7
		protected override string OnGetLogFormat()
		{
			return this.WinnerPeer.UserName + "has won the duel";
		}
	}
}
