using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200005A RID: 90
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class MultiplayerIntermissionMapItemAdded : GameNetworkMessage
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600032E RID: 814 RVA: 0x00006114 File Offset: 0x00004314
		// (set) Token: 0x0600032F RID: 815 RVA: 0x0000611C File Offset: 0x0000431C
		public string MapId { get; private set; }

		// Token: 0x06000330 RID: 816 RVA: 0x00006125 File Offset: 0x00004325
		public MultiplayerIntermissionMapItemAdded()
		{
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000612D File Offset: 0x0000432D
		public MultiplayerIntermissionMapItemAdded(string mapId)
		{
			this.MapId = mapId;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000613C File Offset: 0x0000433C
		protected override bool OnRead()
		{
			bool result = true;
			this.MapId = GameNetworkMessage.ReadStringFromPacket(ref result);
			return result;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00006159 File Offset: 0x00004359
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteStringToPacket(this.MapId);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00006166 File Offset: 0x00004366
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000616E File Offset: 0x0000436E
		protected override string OnGetLogFormat()
		{
			return "Adding map for voting with id: " + this.MapId + ".";
		}
	}
}
