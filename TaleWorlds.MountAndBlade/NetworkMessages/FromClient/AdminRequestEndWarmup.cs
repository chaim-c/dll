using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000013 RID: 19
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class AdminRequestEndWarmup : GameNetworkMessage
	{
		// Token: 0x06000088 RID: 136 RVA: 0x00002A6E File Offset: 0x00000C6E
		protected override bool OnRead()
		{
			return true;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002A71 File Offset: 0x00000C71
		protected override void OnWrite()
		{
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002A73 File Offset: 0x00000C73
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002A7B File Offset: 0x00000C7B
		protected override string OnGetLogFormat()
		{
			return "Requested to end warmup";
		}
	}
}
