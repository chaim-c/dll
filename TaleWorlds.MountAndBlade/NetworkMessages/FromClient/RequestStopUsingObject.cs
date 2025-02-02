using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x0200002E RID: 46
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class RequestStopUsingObject : GameNetworkMessage
	{
		// Token: 0x06000175 RID: 373 RVA: 0x00003C3F File Offset: 0x00001E3F
		protected override bool OnRead()
		{
			return true;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00003C42 File Offset: 0x00001E42
		protected override void OnWrite()
		{
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00003C44 File Offset: 0x00001E44
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionObjectsDetailed;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00003C4C File Offset: 0x00001E4C
		protected override string OnGetLogFormat()
		{
			return "Request to stop using UsableMissionObject";
		}
	}
}
