using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000D7 RID: 215
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SynchronizingDone : GameNetworkMessage
	{
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x0000EAFB File Offset: 0x0000CCFB
		// (set) Token: 0x060008C7 RID: 2247 RVA: 0x0000EB03 File Offset: 0x0000CD03
		public NetworkCommunicator Peer { get; private set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x0000EB0C File Offset: 0x0000CD0C
		// (set) Token: 0x060008C9 RID: 2249 RVA: 0x0000EB14 File Offset: 0x0000CD14
		public bool Synchronized { get; private set; }

		// Token: 0x060008CA RID: 2250 RVA: 0x0000EB1D File Offset: 0x0000CD1D
		public SynchronizingDone(NetworkCommunicator peer, bool synchronized)
		{
			this.Peer = peer;
			this.Synchronized = synchronized;
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0000EB33 File Offset: 0x0000CD33
		public SynchronizingDone()
		{
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0000EB3C File Offset: 0x0000CD3C
		protected override bool OnRead()
		{
			bool result = true;
			this.Peer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			this.Synchronized = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0000EB67 File Offset: 0x0000CD67
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.Peer);
			GameNetworkMessage.WriteBoolToPacket(this.Synchronized);
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0000EB7F File Offset: 0x0000CD7F
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.General;
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0000EB84 File Offset: 0x0000CD84
		protected override string OnGetLogFormat()
		{
			string str = string.Concat(new object[]
			{
				"peer with name: ",
				this.Peer.UserName,
				", and index: ",
				this.Peer.Index
			});
			if (!this.Synchronized)
			{
				return "Synchronized: FALSE for " + str + " (Peer will not receive broadcasted messages)";
			}
			return "Synchronized: TRUE for " + str + " (received all initial data from the server and will now receive broadcasted messages)";
		}
	}
}
