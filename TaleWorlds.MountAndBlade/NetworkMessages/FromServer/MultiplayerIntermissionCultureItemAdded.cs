using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000058 RID: 88
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class MultiplayerIntermissionCultureItemAdded : GameNetworkMessage
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00005FDD File Offset: 0x000041DD
		// (set) Token: 0x0600031D RID: 797 RVA: 0x00005FE5 File Offset: 0x000041E5
		public string CultureId { get; private set; }

		// Token: 0x0600031E RID: 798 RVA: 0x00005FEE File Offset: 0x000041EE
		public MultiplayerIntermissionCultureItemAdded()
		{
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00005FF6 File Offset: 0x000041F6
		public MultiplayerIntermissionCultureItemAdded(string cultureId)
		{
			this.CultureId = cultureId;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00006008 File Offset: 0x00004208
		protected override bool OnRead()
		{
			bool result = true;
			this.CultureId = GameNetworkMessage.ReadStringFromPacket(ref result);
			return result;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00006025 File Offset: 0x00004225
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteStringToPacket(this.CultureId);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00006032 File Offset: 0x00004232
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000603A File Offset: 0x0000423A
		protected override string OnGetLogFormat()
		{
			return "Adding culture for voting with id: " + this.CultureId + ".";
		}
	}
}
