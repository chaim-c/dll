using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000082 RID: 130
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class CreateBanner : GameNetworkMessage
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x000091A4 File Offset: 0x000073A4
		// (set) Token: 0x060004F9 RID: 1273 RVA: 0x000091AC File Offset: 0x000073AC
		public NetworkCommunicator Peer { get; private set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x000091B5 File Offset: 0x000073B5
		// (set) Token: 0x060004FB RID: 1275 RVA: 0x000091BD File Offset: 0x000073BD
		public string BannerCode { get; private set; }

		// Token: 0x060004FC RID: 1276 RVA: 0x000091C6 File Offset: 0x000073C6
		public CreateBanner(NetworkCommunicator peer, string bannerCode)
		{
			this.Peer = peer;
			this.BannerCode = bannerCode;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x000091DC File Offset: 0x000073DC
		public CreateBanner()
		{
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x000091E4 File Offset: 0x000073E4
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.Peer);
			GameNetworkMessage.WriteStringToPacket(this.BannerCode);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x000091FC File Offset: 0x000073FC
		protected override bool OnRead()
		{
			bool result = true;
			this.Peer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			this.BannerCode = GameNetworkMessage.ReadStringFromPacket(ref result);
			return result;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00009227 File Offset: 0x00007427
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Peers | MultiplayerMessageFilter.AgentsDetailed;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000922F File Offset: 0x0000742F
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Create banner for peer: ",
				this.Peer.UserName,
				", with index: ",
				this.Peer.Index
			});
		}
	}
}
