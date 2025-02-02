using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000089 RID: 137
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class ExistingObjectsEnd : GameNetworkMessage
	{
		// Token: 0x0600055C RID: 1372 RVA: 0x00009C24 File Offset: 0x00007E24
		protected override bool OnRead()
		{
			return true;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00009C27 File Offset: 0x00007E27
		protected override void OnWrite()
		{
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00009C29 File Offset: 0x00007E29
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.General;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00009C2D File Offset: 0x00007E2D
		protected override string OnGetLogFormat()
		{
			return "Finished receiving existing objects";
		}
	}
}
