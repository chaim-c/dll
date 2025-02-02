using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000CD RID: 205
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class UpdateNotificationsMessage : Message
	{
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060003BA RID: 954 RVA: 0x000048B2 File Offset: 0x00002AB2
		// (set) Token: 0x060003BB RID: 955 RVA: 0x000048BA File Offset: 0x00002ABA
		[JsonProperty]
		public int[] SeenNotificationIds { get; private set; }

		// Token: 0x060003BC RID: 956 RVA: 0x000048C3 File Offset: 0x00002AC3
		public UpdateNotificationsMessage()
		{
		}

		// Token: 0x060003BD RID: 957 RVA: 0x000048CB File Offset: 0x00002ACB
		public UpdateNotificationsMessage(int[] seenNotificationIds)
		{
			this.SeenNotificationIds = seenNotificationIds;
		}
	}
}
