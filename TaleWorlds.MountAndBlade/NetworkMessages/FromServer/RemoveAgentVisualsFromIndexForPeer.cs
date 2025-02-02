using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000093 RID: 147
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class RemoveAgentVisualsFromIndexForPeer : GameNetworkMessage
	{
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0000A712 File Offset: 0x00008912
		// (set) Token: 0x060005D2 RID: 1490 RVA: 0x0000A71A File Offset: 0x0000891A
		public NetworkCommunicator Peer { get; private set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x0000A723 File Offset: 0x00008923
		// (set) Token: 0x060005D4 RID: 1492 RVA: 0x0000A72B File Offset: 0x0000892B
		public int VisualsIndex { get; private set; }

		// Token: 0x060005D5 RID: 1493 RVA: 0x0000A734 File Offset: 0x00008934
		public RemoveAgentVisualsFromIndexForPeer(NetworkCommunicator peer, int index)
		{
			this.Peer = peer;
			this.VisualsIndex = index;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0000A74A File Offset: 0x0000894A
		public RemoveAgentVisualsFromIndexForPeer()
		{
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0000A754 File Offset: 0x00008954
		protected override bool OnRead()
		{
			bool result = true;
			this.Peer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			this.VisualsIndex = GameNetworkMessage.ReadIntFromPacket(CompressionMission.AgentOffsetCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0000A784 File Offset: 0x00008984
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.Peer);
			GameNetworkMessage.WriteIntToPacket(this.VisualsIndex, CompressionMission.AgentOffsetCompressionInfo);
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0000A7A1 File Offset: 0x000089A1
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Agents;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0000A7A9 File Offset: 0x000089A9
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Removing AgentVisuals with Index: ",
				this.VisualsIndex,
				", for peer: ",
				this.Peer.UserName
			});
		}
	}
}
