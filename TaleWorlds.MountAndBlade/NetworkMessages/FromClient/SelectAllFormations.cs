using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000031 RID: 49
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class SelectAllFormations : GameNetworkMessage
	{
		// Token: 0x06000189 RID: 393 RVA: 0x00003D23 File Offset: 0x00001F23
		protected override bool OnRead()
		{
			return true;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00003D26 File Offset: 0x00001F26
		protected override void OnWrite()
		{
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00003D28 File Offset: 0x00001F28
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Formations;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00003D2D File Offset: 0x00001F2D
		protected override string OnGetLogFormat()
		{
			return "Select all formations";
		}
	}
}
