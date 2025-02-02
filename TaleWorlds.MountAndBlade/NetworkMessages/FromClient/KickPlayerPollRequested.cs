using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000019 RID: 25
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class KickPlayerPollRequested : GameNetworkMessage
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00002F97 File Offset: 0x00001197
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00002F9F File Offset: 0x0000119F
		public NetworkCommunicator PlayerPeer { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00002FA8 File Offset: 0x000011A8
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00002FB0 File Offset: 0x000011B0
		public bool BanPlayer { get; private set; }

		// Token: 0x060000C4 RID: 196 RVA: 0x00002FB9 File Offset: 0x000011B9
		public KickPlayerPollRequested(NetworkCommunicator playerPeer, bool banPlayer)
		{
			this.PlayerPeer = playerPeer;
			this.BanPlayer = banPlayer;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00002FCF File Offset: 0x000011CF
		public KickPlayerPollRequested()
		{
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00002FD8 File Offset: 0x000011D8
		protected override bool OnRead()
		{
			bool result = true;
			this.PlayerPeer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, true);
			this.BanPlayer = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003003 File Offset: 0x00001203
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.PlayerPeer);
			GameNetworkMessage.WriteBoolToPacket(this.BanPlayer);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000301B File Offset: 0x0000121B
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003023 File Offset: 0x00001223
		protected override string OnGetLogFormat()
		{
			string str = "Requested to start poll to kick";
			string str2 = this.BanPlayer ? " and ban" : "";
			string str3 = " player: ";
			NetworkCommunicator playerPeer = this.PlayerPeer;
			return str + str2 + str3 + ((playerPeer != null) ? playerPeer.UserName : null);
		}
	}
}
