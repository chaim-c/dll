using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200009B RID: 155
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetAgentPeer : GameNetworkMessage
	{
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x0000AF09 File Offset: 0x00009109
		// (set) Token: 0x06000630 RID: 1584 RVA: 0x0000AF11 File Offset: 0x00009111
		public int AgentIndex { get; private set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x0000AF1A File Offset: 0x0000911A
		// (set) Token: 0x06000632 RID: 1586 RVA: 0x0000AF22 File Offset: 0x00009122
		public NetworkCommunicator Peer { get; private set; }

		// Token: 0x06000633 RID: 1587 RVA: 0x0000AF2B File Offset: 0x0000912B
		public SetAgentPeer(int agentIndex, NetworkCommunicator peer)
		{
			this.AgentIndex = agentIndex;
			this.Peer = peer;
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0000AF41 File Offset: 0x00009141
		public SetAgentPeer()
		{
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0000AF4C File Offset: 0x0000914C
		protected override bool OnRead()
		{
			bool result = true;
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.Peer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, true);
			return result;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0000AF77 File Offset: 0x00009177
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.Peer);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0000AF8F File Offset: 0x0000918F
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Peers | MultiplayerMessageFilter.Agents;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0000AF98 File Offset: 0x00009198
		protected override string OnGetLogFormat()
		{
			if (this.AgentIndex < 0)
			{
				return "Ignoring the message for invalid agent.";
			}
			return string.Concat(new object[]
			{
				"Set NetworkPeer ",
				(this.Peer != null) ? "" : "(to NULL) ",
				"on Agent with agent-index: ",
				this.AgentIndex
			});
		}
	}
}
