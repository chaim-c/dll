using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000032 RID: 50
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class SelectAllSiegeWeapons : GameNetworkMessage
	{
		// Token: 0x0600018E RID: 398 RVA: 0x00003D3C File Offset: 0x00001F3C
		protected override bool OnRead()
		{
			return true;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00003D3F File Offset: 0x00001F3F
		protected override void OnWrite()
		{
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00003D41 File Offset: 0x00001F41
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.SiegeWeaponsDetailed;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00003D49 File Offset: 0x00001F49
		protected override string OnGetLogFormat()
		{
			return "Select all siege weapons.";
		}
	}
}
