using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000040 RID: 64
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class LobbyNotificationsMessage : Message
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00002CD0 File Offset: 0x00000ED0
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00002CD8 File Offset: 0x00000ED8
		[JsonProperty]
		public LobbyNotification[] Notifications { get; private set; }

		// Token: 0x06000134 RID: 308 RVA: 0x00002CE1 File Offset: 0x00000EE1
		public LobbyNotificationsMessage()
		{
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00002CE9 File Offset: 0x00000EE9
		public LobbyNotificationsMessage(LobbyNotification[] notifications)
		{
			this.Notifications = notifications;
		}
	}
}
