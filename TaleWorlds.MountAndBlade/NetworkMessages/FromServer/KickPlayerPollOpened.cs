using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000055 RID: 85
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class KickPlayerPollOpened : GameNetworkMessage
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00005BB8 File Offset: 0x00003DB8
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x00005BC0 File Offset: 0x00003DC0
		public NetworkCommunicator InitiatorPeer { get; private set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00005BC9 File Offset: 0x00003DC9
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x00005BD1 File Offset: 0x00003DD1
		public NetworkCommunicator PlayerPeer { get; private set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00005BDA File Offset: 0x00003DDA
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x00005BE2 File Offset: 0x00003DE2
		public bool BanPlayer { get; private set; }

		// Token: 0x060002FA RID: 762 RVA: 0x00005BEB File Offset: 0x00003DEB
		public KickPlayerPollOpened(NetworkCommunicator initiatorPeer, NetworkCommunicator playerPeer, bool banPlayer)
		{
			this.InitiatorPeer = initiatorPeer;
			this.PlayerPeer = playerPeer;
			this.BanPlayer = banPlayer;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00005C08 File Offset: 0x00003E08
		public KickPlayerPollOpened()
		{
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00005C10 File Offset: 0x00003E10
		protected override bool OnRead()
		{
			bool result = true;
			this.InitiatorPeer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			this.PlayerPeer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			this.BanPlayer = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00005C49 File Offset: 0x00003E49
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.InitiatorPeer);
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.PlayerPeer);
			GameNetworkMessage.WriteBoolToPacket(this.BanPlayer);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00005C6C File Offset: 0x00003E6C
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00005C74 File Offset: 0x00003E74
		protected override string OnGetLogFormat()
		{
			string[] array = new string[5];
			int num = 0;
			NetworkCommunicator initiatorPeer = this.InitiatorPeer;
			array[num] = ((initiatorPeer != null) ? initiatorPeer.UserName : null);
			array[1] = " wants to start poll to kick";
			array[2] = (this.BanPlayer ? " and ban" : "");
			array[3] = " player: ";
			int num2 = 4;
			NetworkCommunicator playerPeer = this.PlayerPeer;
			array[num2] = ((playerPeer != null) ? playerPeer.UserName : null);
			return string.Concat(array);
		}
	}
}
