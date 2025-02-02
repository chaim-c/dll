using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000012 RID: 18
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class ClanInfoChangedMessage : Message
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004C RID: 76 RVA: 0x0000235C File Offset: 0x0000055C
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002364 File Offset: 0x00000564
		[JsonProperty]
		public ClanHomeInfo ClanHomeInfo { get; private set; }

		// Token: 0x0600004E RID: 78 RVA: 0x0000236D File Offset: 0x0000056D
		public ClanInfoChangedMessage()
		{
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002375 File Offset: 0x00000575
		public ClanInfoChangedMessage(ClanHomeInfo clanHomeInfo)
		{
			this.ClanHomeInfo = clanHomeInfo;
		}
	}
}
