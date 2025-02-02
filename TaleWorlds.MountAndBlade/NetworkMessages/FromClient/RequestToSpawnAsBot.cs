using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x0200002F RID: 47
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class RequestToSpawnAsBot : GameNetworkMessage
	{
		// Token: 0x0600017A RID: 378 RVA: 0x00003C5B File Offset: 0x00001E5B
		protected override bool OnRead()
		{
			return true;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00003C5E File Offset: 0x00001E5E
		protected override void OnWrite()
		{
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00003C60 File Offset: 0x00001E60
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.General | MultiplayerMessageFilter.AgentsDetailed;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00003C68 File Offset: 0x00001E68
		protected override string OnGetLogFormat()
		{
			return "Request to spawn as a bot";
		}
	}
}
