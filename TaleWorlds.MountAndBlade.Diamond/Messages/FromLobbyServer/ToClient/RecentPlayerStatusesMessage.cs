using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000050 RID: 80
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class RecentPlayerStatusesMessage : Message
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00003003 File Offset: 0x00001203
		// (set) Token: 0x0600017E RID: 382 RVA: 0x0000300B File Offset: 0x0000120B
		[JsonProperty]
		public FriendInfo[] Friends { get; private set; }

		// Token: 0x0600017F RID: 383 RVA: 0x00003014 File Offset: 0x00001214
		public RecentPlayerStatusesMessage()
		{
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000301C File Offset: 0x0000121C
		public RecentPlayerStatusesMessage(FriendInfo[] friends)
		{
			this.Friends = friends;
		}
	}
}
