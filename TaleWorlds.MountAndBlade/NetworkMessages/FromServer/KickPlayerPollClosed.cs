using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000054 RID: 84
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class KickPlayerPollClosed : GameNetworkMessage
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00005ACE File Offset: 0x00003CCE
		// (set) Token: 0x060002EB RID: 747 RVA: 0x00005AD6 File Offset: 0x00003CD6
		public NetworkCommunicator PlayerPeer { get; private set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00005ADF File Offset: 0x00003CDF
		// (set) Token: 0x060002ED RID: 749 RVA: 0x00005AE7 File Offset: 0x00003CE7
		public bool Accepted { get; private set; }

		// Token: 0x060002EE RID: 750 RVA: 0x00005AF0 File Offset: 0x00003CF0
		public KickPlayerPollClosed(NetworkCommunicator playerPeer, bool accepted)
		{
			this.PlayerPeer = playerPeer;
			this.Accepted = accepted;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00005B06 File Offset: 0x00003D06
		public KickPlayerPollClosed()
		{
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00005B10 File Offset: 0x00003D10
		protected override bool OnRead()
		{
			bool result = true;
			this.PlayerPeer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			this.Accepted = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00005B3B File Offset: 0x00003D3B
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.PlayerPeer);
			GameNetworkMessage.WriteBoolToPacket(this.Accepted);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00005B53 File Offset: 0x00003D53
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00005B5C File Offset: 0x00003D5C
		protected override string OnGetLogFormat()
		{
			string[] array = new string[5];
			array[0] = "Poll is closed. ";
			int num = 1;
			NetworkCommunicator playerPeer = this.PlayerPeer;
			array[num] = ((playerPeer != null) ? playerPeer.UserName : null);
			array[2] = " is ";
			array[3] = (this.Accepted ? "" : "not ");
			array[4] = "kicked.";
			return string.Concat(array);
		}
	}
}
