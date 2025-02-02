using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000075 RID: 117
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class AssignFormationToPlayer : GameNetworkMessage
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x00007C22 File Offset: 0x00005E22
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x00007C2A File Offset: 0x00005E2A
		public NetworkCommunicator Peer { get; private set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x00007C33 File Offset: 0x00005E33
		// (set) Token: 0x06000434 RID: 1076 RVA: 0x00007C3B File Offset: 0x00005E3B
		public FormationClass FormationClass { get; private set; }

		// Token: 0x06000435 RID: 1077 RVA: 0x00007C44 File Offset: 0x00005E44
		public AssignFormationToPlayer(NetworkCommunicator peer, FormationClass formationClass)
		{
			this.Peer = peer;
			this.FormationClass = formationClass;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00007C5A File Offset: 0x00005E5A
		public AssignFormationToPlayer()
		{
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00007C64 File Offset: 0x00005E64
		protected override bool OnRead()
		{
			bool result = true;
			this.Peer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			this.FormationClass = (FormationClass)GameNetworkMessage.ReadIntFromPacket(CompressionMission.FormationClassCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00007C94 File Offset: 0x00005E94
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.Peer);
			GameNetworkMessage.WriteIntToPacket((int)this.FormationClass, CompressionMission.FormationClassCompressionInfo);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00007CB1 File Offset: 0x00005EB1
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Formations;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00007CB8 File Offset: 0x00005EB8
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Assign formation with index: ",
				(int)this.FormationClass,
				" to NetworkPeer with name: ",
				this.Peer.UserName,
				" and peer-index",
				this.Peer.Index,
				" and make him captain."
			});
		}
	}
}
