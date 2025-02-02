using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x0200002B RID: 43
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class CreateBanner : GameNetworkMessage
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00003A84 File Offset: 0x00001C84
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00003A8C File Offset: 0x00001C8C
		public string BannerCode { get; private set; }

		// Token: 0x0600015C RID: 348 RVA: 0x00003A95 File Offset: 0x00001C95
		public CreateBanner(string bannerCode)
		{
			this.BannerCode = bannerCode;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00003AA4 File Offset: 0x00001CA4
		public CreateBanner()
		{
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00003AAC File Offset: 0x00001CAC
		protected override bool OnRead()
		{
			bool result = true;
			this.BannerCode = GameNetworkMessage.ReadStringFromPacket(ref result);
			return result;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00003AC9 File Offset: 0x00001CC9
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteStringToPacket(this.BannerCode);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00003AD6 File Offset: 0x00001CD6
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Peers | MultiplayerMessageFilter.AgentsDetailed;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00003ADE File Offset: 0x00001CDE
		protected override string OnGetLogFormat()
		{
			return "Clients has updated his banner";
		}
	}
}
