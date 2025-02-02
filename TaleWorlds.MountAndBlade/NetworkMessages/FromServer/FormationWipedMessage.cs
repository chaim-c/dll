using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200008B RID: 139
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class FormationWipedMessage : GameNetworkMessage
	{
		// Token: 0x0600056B RID: 1387 RVA: 0x00009CDA File Offset: 0x00007EDA
		protected override void OnWrite()
		{
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00009CDC File Offset: 0x00007EDC
		protected override bool OnRead()
		{
			return true;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00009CDF File Offset: 0x00007EDF
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionDetailed;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00009CE7 File Offset: 0x00007EE7
		protected override string OnGetLogFormat()
		{
			return "FormationWipedMessage";
		}
	}
}
