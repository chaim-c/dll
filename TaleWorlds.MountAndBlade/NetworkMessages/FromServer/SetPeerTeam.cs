using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000AF RID: 175
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetPeerTeam : GameNetworkMessage
	{
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x0000C3A1 File Offset: 0x0000A5A1
		// (set) Token: 0x0600070D RID: 1805 RVA: 0x0000C3A9 File Offset: 0x0000A5A9
		public NetworkCommunicator Peer { get; private set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x0000C3B2 File Offset: 0x0000A5B2
		// (set) Token: 0x0600070F RID: 1807 RVA: 0x0000C3BA File Offset: 0x0000A5BA
		public int TeamIndex { get; private set; }

		// Token: 0x06000710 RID: 1808 RVA: 0x0000C3C3 File Offset: 0x0000A5C3
		public SetPeerTeam(NetworkCommunicator peer, int teamIndex)
		{
			this.Peer = peer;
			this.TeamIndex = teamIndex;
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0000C3D9 File Offset: 0x0000A5D9
		public SetPeerTeam()
		{
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0000C3E4 File Offset: 0x0000A5E4
		protected override bool OnRead()
		{
			bool result = true;
			this.Peer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			this.TeamIndex = GameNetworkMessage.ReadTeamIndexFromPacket(ref result);
			return result;
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0000C40F File Offset: 0x0000A60F
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.Peer);
			GameNetworkMessage.WriteTeamIndexToPacket(this.TeamIndex);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0000C427 File Offset: 0x0000A627
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Peers;
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0000C42C File Offset: 0x0000A62C
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Set Team: ",
				this.TeamIndex,
				" of NetworkPeer with name: ",
				this.Peer.UserName,
				" and peer-index",
				this.Peer.Index
			});
		}
	}
}
