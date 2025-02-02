using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000029 RID: 41
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class CancelCheering : GameNetworkMessage
	{
		// Token: 0x06000151 RID: 337 RVA: 0x00003A5B File Offset: 0x00001C5B
		protected override bool OnRead()
		{
			return true;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00003A5E File Offset: 0x00001C5E
		protected override void OnWrite()
		{
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00003A60 File Offset: 0x00001C60
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.None;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00003A64 File Offset: 0x00001C64
		protected override string OnGetLogFormat()
		{
			return "FromClient.CancelCheering";
		}
	}
}
