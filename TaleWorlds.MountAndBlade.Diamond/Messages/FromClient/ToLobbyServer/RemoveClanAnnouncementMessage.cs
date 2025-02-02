using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000BD RID: 189
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class RemoveClanAnnouncementMessage : Message
	{
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000367 RID: 871 RVA: 0x00004548 File Offset: 0x00002748
		// (set) Token: 0x06000368 RID: 872 RVA: 0x00004550 File Offset: 0x00002750
		[JsonProperty]
		public int AnnouncementId { get; private set; }

		// Token: 0x06000369 RID: 873 RVA: 0x00004559 File Offset: 0x00002759
		public RemoveClanAnnouncementMessage()
		{
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00004561 File Offset: 0x00002761
		public RemoveClanAnnouncementMessage(int announcementId)
		{
			this.AnnouncementId = announcementId;
		}
	}
}
