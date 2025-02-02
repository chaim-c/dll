using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000011 RID: 17
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class AdminMuteUnmutePlayer : GameNetworkMessage
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000028F3 File Offset: 0x00000AF3
		// (set) Token: 0x06000074 RID: 116 RVA: 0x000028FB File Offset: 0x00000AFB
		public NetworkCommunicator PlayerPeer { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002904 File Offset: 0x00000B04
		// (set) Token: 0x06000076 RID: 118 RVA: 0x0000290C File Offset: 0x00000B0C
		public bool Unmute { get; private set; }

		// Token: 0x06000077 RID: 119 RVA: 0x00002915 File Offset: 0x00000B15
		public AdminMuteUnmutePlayer(NetworkCommunicator playerPeer, bool unmute)
		{
			this.PlayerPeer = playerPeer;
			this.Unmute = unmute;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000292B File Offset: 0x00000B2B
		public AdminMuteUnmutePlayer()
		{
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002934 File Offset: 0x00000B34
		protected override bool OnRead()
		{
			bool result = true;
			this.PlayerPeer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			this.Unmute = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000295F File Offset: 0x00000B5F
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.PlayerPeer);
			GameNetworkMessage.WriteBoolToPacket(this.Unmute);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002977 File Offset: 0x00000B77
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000297F File Offset: 0x00000B7F
		protected override string OnGetLogFormat()
		{
			return "Requested to " + (this.Unmute ? " unmute" : "mute") + " player: " + this.PlayerPeer.UserName;
		}
	}
}
