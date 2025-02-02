using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x0200001C RID: 28
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class RequestForfeitSpawn : GameNetworkMessage
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00003163 File Offset: 0x00001363
		protected override void OnWrite()
		{
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003165 File Offset: 0x00001365
		protected override bool OnRead()
		{
			return true;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003168 File Offset: 0x00001368
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.GameMode;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003170 File Offset: 0x00001370
		protected override string OnGetLogFormat()
		{
			return "Someone has requested to forfeit spawning.";
		}
	}
}
