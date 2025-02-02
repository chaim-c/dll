using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000047 RID: 71
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class DuelPreparationStartedForTheFirstTime : GameNetworkMessage
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00004C13 File Offset: 0x00002E13
		// (set) Token: 0x06000257 RID: 599 RVA: 0x00004C1B File Offset: 0x00002E1B
		public NetworkCommunicator RequesterPeer { get; private set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00004C24 File Offset: 0x00002E24
		// (set) Token: 0x06000259 RID: 601 RVA: 0x00004C2C File Offset: 0x00002E2C
		public NetworkCommunicator RequesteePeer { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00004C35 File Offset: 0x00002E35
		// (set) Token: 0x0600025B RID: 603 RVA: 0x00004C3D File Offset: 0x00002E3D
		public int AreaIndex { get; private set; }

		// Token: 0x0600025C RID: 604 RVA: 0x00004C46 File Offset: 0x00002E46
		public DuelPreparationStartedForTheFirstTime(NetworkCommunicator requesterPeer, NetworkCommunicator requesteePeer, int areaIndex)
		{
			this.RequesterPeer = requesterPeer;
			this.RequesteePeer = requesteePeer;
			this.AreaIndex = areaIndex;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00004C63 File Offset: 0x00002E63
		public DuelPreparationStartedForTheFirstTime()
		{
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00004C6C File Offset: 0x00002E6C
		protected override bool OnRead()
		{
			bool result = true;
			this.RequesterPeer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			this.RequesteePeer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			this.AreaIndex = GameNetworkMessage.ReadIntFromPacket(CompressionMission.DuelAreaIndexCompressionInfo, ref result);
			return result;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00004CAA File Offset: 0x00002EAA
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.RequesterPeer);
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.RequesteePeer);
			GameNetworkMessage.WriteIntToPacket(this.AreaIndex, CompressionMission.DuelAreaIndexCompressionInfo);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00004CD2 File Offset: 0x00002ED2
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.GameMode;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00004CDC File Offset: 0x00002EDC
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Duel started between agent with name: ",
				this.RequesteePeer.UserName,
				" and index: ",
				this.RequesteePeer.Index,
				" and agent with name: ",
				this.RequesterPeer.UserName,
				" and index: ",
				this.RequesterPeer.Index
			});
		}
	}
}
