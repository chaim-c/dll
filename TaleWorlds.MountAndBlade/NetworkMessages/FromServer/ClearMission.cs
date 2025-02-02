using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200007D RID: 125
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class ClearMission : GameNetworkMessage
	{
		// Token: 0x06000488 RID: 1160 RVA: 0x00008439 File Offset: 0x00006639
		protected override bool OnRead()
		{
			return true;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000843C File Offset: 0x0000663C
		protected override void OnWrite()
		{
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000843E File Offset: 0x0000663E
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Mission;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00008446 File Offset: 0x00006646
		protected override string OnGetLogFormat()
		{
			return "Clear Mission";
		}
	}
}
