using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000061 RID: 97
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class PollCancelled : GameNetworkMessage
	{
		// Token: 0x06000364 RID: 868 RVA: 0x0000699F File Offset: 0x00004B9F
		protected override bool OnRead()
		{
			return true;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x000069A2 File Offset: 0x00004BA2
		protected override void OnWrite()
		{
		}

		// Token: 0x06000366 RID: 870 RVA: 0x000069A4 File Offset: 0x00004BA4
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000069AC File Offset: 0x00004BAC
		protected override string OnGetLogFormat()
		{
			return "Poll cancelled.";
		}
	}
}
