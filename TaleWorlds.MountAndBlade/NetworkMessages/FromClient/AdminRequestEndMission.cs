using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000006 RID: 6
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class AdminRequestEndMission : GameNetworkMessage
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002118 File Offset: 0x00000318
		protected override bool OnRead()
		{
			return true;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000211B File Offset: 0x0000031B
		protected override void OnWrite()
		{
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000211D File Offset: 0x0000031D
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002125 File Offset: 0x00000325
		protected override string OnGetLogFormat()
		{
			return "AdminRequestEndMission called";
		}
	}
}
