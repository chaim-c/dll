using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x0200000C RID: 12
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class RequestChangeCharacterMessage : GameNetworkMessage
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000261B File Offset: 0x0000081B
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002623 File Offset: 0x00000823
		public NetworkCommunicator NetworkPeer { get; private set; }

		// Token: 0x0600004B RID: 75 RVA: 0x0000262C File Offset: 0x0000082C
		public RequestChangeCharacterMessage(NetworkCommunicator networkPeer)
		{
			this.NetworkPeer = networkPeer;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000263B File Offset: 0x0000083B
		public RequestChangeCharacterMessage()
		{
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002643 File Offset: 0x00000843
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.NetworkPeer);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002650 File Offset: 0x00000850
		protected override bool OnRead()
		{
			bool result = true;
			this.NetworkPeer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			return result;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000266E File Offset: 0x0000086E
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.GameMode;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002676 File Offset: 0x00000876
		protected override string OnGetLogFormat()
		{
			return this.NetworkPeer.UserName + " has requested to change character.";
		}
	}
}
