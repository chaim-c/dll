using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000041 RID: 65
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class FlagDominationFlagsRemovedMessage : GameNetworkMessage
	{
		// Token: 0x0600021C RID: 540 RVA: 0x00004701 File Offset: 0x00002901
		protected override void OnWrite()
		{
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00004703 File Offset: 0x00002903
		protected override bool OnRead()
		{
			return true;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00004706 File Offset: 0x00002906
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.GameMode;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000470E File Offset: 0x0000290E
		protected override string OnGetLogFormat()
		{
			return "Flags got removed.";
		}
	}
}
