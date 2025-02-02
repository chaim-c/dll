using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000018 RID: 24
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class KickPlayer : GameNetworkMessage
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00002EDB File Offset: 0x000010DB
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00002EE3 File Offset: 0x000010E3
		public NetworkCommunicator PlayerPeer { get; private set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00002EEC File Offset: 0x000010EC
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00002EF4 File Offset: 0x000010F4
		public bool BanPlayer { get; private set; }

		// Token: 0x060000BA RID: 186 RVA: 0x00002EFD File Offset: 0x000010FD
		public KickPlayer(NetworkCommunicator playerPeer, bool banPlayer)
		{
			this.PlayerPeer = playerPeer;
			this.BanPlayer = banPlayer;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00002F13 File Offset: 0x00001113
		public KickPlayer()
		{
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00002F1C File Offset: 0x0000111C
		protected override bool OnRead()
		{
			bool result = true;
			this.PlayerPeer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			this.BanPlayer = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00002F47 File Offset: 0x00001147
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.PlayerPeer);
			GameNetworkMessage.WriteBoolToPacket(this.BanPlayer);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00002F5F File Offset: 0x0000115F
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00002F67 File Offset: 0x00001167
		protected override string OnGetLogFormat()
		{
			return "Requested to kick" + (this.BanPlayer ? " and ban" : "") + " player: " + this.PlayerPeer.UserName;
		}
	}
}
