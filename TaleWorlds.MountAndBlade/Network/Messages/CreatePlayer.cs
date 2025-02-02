using System;

namespace TaleWorlds.MountAndBlade.Network.Messages
{
	// Token: 0x0200039C RID: 924
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class CreatePlayer : GameNetworkMessage
	{
		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x060031F8 RID: 12792 RVA: 0x000CF055 File Offset: 0x000CD255
		// (set) Token: 0x060031F9 RID: 12793 RVA: 0x000CF05D File Offset: 0x000CD25D
		public int PlayerIndex { get; private set; }

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x060031FA RID: 12794 RVA: 0x000CF066 File Offset: 0x000CD266
		// (set) Token: 0x060031FB RID: 12795 RVA: 0x000CF06E File Offset: 0x000CD26E
		public string PlayerName { get; private set; }

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x060031FC RID: 12796 RVA: 0x000CF077 File Offset: 0x000CD277
		// (set) Token: 0x060031FD RID: 12797 RVA: 0x000CF07F File Offset: 0x000CD27F
		public int DisconnectedPeerIndex { get; private set; }

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x060031FE RID: 12798 RVA: 0x000CF088 File Offset: 0x000CD288
		// (set) Token: 0x060031FF RID: 12799 RVA: 0x000CF090 File Offset: 0x000CD290
		public bool IsNonExistingDisconnectedPeer { get; private set; }

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06003200 RID: 12800 RVA: 0x000CF099 File Offset: 0x000CD299
		// (set) Token: 0x06003201 RID: 12801 RVA: 0x000CF0A1 File Offset: 0x000CD2A1
		public bool IsReceiverPeer { get; private set; }

		// Token: 0x06003202 RID: 12802 RVA: 0x000CF0AA File Offset: 0x000CD2AA
		public CreatePlayer(int playerIndex, string playerName, int disconnectedPeerIndex, bool isNonExistingDisconnectedPeer = false, bool isReceiverPeer = false)
		{
			this.PlayerIndex = playerIndex;
			this.PlayerName = playerName;
			this.DisconnectedPeerIndex = disconnectedPeerIndex;
			this.IsNonExistingDisconnectedPeer = isNonExistingDisconnectedPeer;
			this.IsReceiverPeer = isReceiverPeer;
		}

		// Token: 0x06003203 RID: 12803 RVA: 0x000CF0D7 File Offset: 0x000CD2D7
		public CreatePlayer()
		{
		}

		// Token: 0x06003204 RID: 12804 RVA: 0x000CF0E0 File Offset: 0x000CD2E0
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket(this.PlayerIndex, CompressionBasic.PlayerCompressionInfo);
			GameNetworkMessage.WriteStringToPacket(this.PlayerName);
			GameNetworkMessage.WriteIntToPacket(this.DisconnectedPeerIndex, CompressionBasic.PlayerCompressionInfo);
			GameNetworkMessage.WriteBoolToPacket(this.IsNonExistingDisconnectedPeer);
			GameNetworkMessage.WriteBoolToPacket(this.IsReceiverPeer);
		}

		// Token: 0x06003205 RID: 12805 RVA: 0x000CF130 File Offset: 0x000CD330
		protected override bool OnRead()
		{
			bool result = true;
			this.PlayerIndex = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.PlayerCompressionInfo, ref result);
			this.PlayerName = GameNetworkMessage.ReadStringFromPacket(ref result);
			this.DisconnectedPeerIndex = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.PlayerCompressionInfo, ref result);
			this.IsNonExistingDisconnectedPeer = GameNetworkMessage.ReadBoolFromPacket(ref result);
			this.IsReceiverPeer = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x06003206 RID: 12806 RVA: 0x000CF18B File Offset: 0x000CD38B
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Peers;
		}

		// Token: 0x06003207 RID: 12807 RVA: 0x000CF190 File Offset: 0x000CD390
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Create a new player with name: ",
				this.PlayerName,
				" and index: ",
				this.PlayerIndex,
				" and dcedIndex: ",
				this.DisconnectedPeerIndex,
				" which is ",
				(!this.IsNonExistingDisconnectedPeer) ? "not" : "",
				" a NonExistingDisconnectedPeer"
			});
		}
	}
}
