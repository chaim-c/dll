using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x0200000A RID: 10
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class DuelRequest : GameNetworkMessage
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000024F9 File Offset: 0x000006F9
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002501 File Offset: 0x00000701
		public int RequestedAgentIndex { get; private set; }

		// Token: 0x06000039 RID: 57 RVA: 0x0000250A File Offset: 0x0000070A
		public DuelRequest(int requestedAgentIndex)
		{
			this.RequestedAgentIndex = requestedAgentIndex;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002519 File Offset: 0x00000719
		public DuelRequest()
		{
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002524 File Offset: 0x00000724
		protected override bool OnRead()
		{
			bool result = true;
			this.RequestedAgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			return result;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002541 File Offset: 0x00000741
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.RequestedAgentIndex);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000254E File Offset: 0x0000074E
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.GameMode;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002556 File Offset: 0x00000756
		protected override string OnGetLogFormat()
		{
			return "Duel requested from agent with index: " + this.RequestedAgentIndex;
		}
	}
}
