using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000088 RID: 136
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class ExistingObjectsBegin : GameNetworkMessage
	{
		// Token: 0x06000557 RID: 1367 RVA: 0x00009C0C File Offset: 0x00007E0C
		protected override bool OnRead()
		{
			return true;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00009C0F File Offset: 0x00007E0F
		protected override void OnWrite()
		{
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00009C11 File Offset: 0x00007E11
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.General;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00009C15 File Offset: 0x00007E15
		protected override string OnGetLogFormat()
		{
			return "Started receiving existing objects";
		}
	}
}
