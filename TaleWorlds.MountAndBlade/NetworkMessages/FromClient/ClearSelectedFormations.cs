using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x0200002A RID: 42
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class ClearSelectedFormations : GameNetworkMessage
	{
		// Token: 0x06000156 RID: 342 RVA: 0x00003A73 File Offset: 0x00001C73
		protected override bool OnRead()
		{
			return true;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00003A76 File Offset: 0x00001C76
		protected override void OnWrite()
		{
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00003A78 File Offset: 0x00001C78
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Formations;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00003A7D File Offset: 0x00001C7D
		protected override string OnGetLogFormat()
		{
			return "Clear Selected Formations";
		}
	}
}
