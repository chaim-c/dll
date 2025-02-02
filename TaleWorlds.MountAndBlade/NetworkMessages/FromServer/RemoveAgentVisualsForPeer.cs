using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000092 RID: 146
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class RemoveAgentVisualsForPeer : GameNetworkMessage
	{
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x0000A69D File Offset: 0x0000889D
		// (set) Token: 0x060005CA RID: 1482 RVA: 0x0000A6A5 File Offset: 0x000088A5
		public NetworkCommunicator Peer { get; private set; }

		// Token: 0x060005CB RID: 1483 RVA: 0x0000A6AE File Offset: 0x000088AE
		public RemoveAgentVisualsForPeer(NetworkCommunicator peer)
		{
			this.Peer = peer;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0000A6BD File Offset: 0x000088BD
		public RemoveAgentVisualsForPeer()
		{
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0000A6C8 File Offset: 0x000088C8
		protected override bool OnRead()
		{
			bool result = true;
			this.Peer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			return result;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0000A6E6 File Offset: 0x000088E6
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.Peer);
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0000A6F3 File Offset: 0x000088F3
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Agents;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0000A6FB File Offset: 0x000088FB
		protected override string OnGetLogFormat()
		{
			return "Removing all AgentVisuals for peer: " + this.Peer.UserName;
		}
	}
}
