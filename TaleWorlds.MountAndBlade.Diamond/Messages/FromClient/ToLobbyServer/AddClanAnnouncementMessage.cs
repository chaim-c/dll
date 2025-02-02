using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000075 RID: 117
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class AddClanAnnouncementMessage : Message
	{
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600024B RID: 587 RVA: 0x000039AF File Offset: 0x00001BAF
		// (set) Token: 0x0600024C RID: 588 RVA: 0x000039B7 File Offset: 0x00001BB7
		[JsonProperty]
		public string Announcement { get; private set; }

		// Token: 0x0600024D RID: 589 RVA: 0x000039C0 File Offset: 0x00001BC0
		public AddClanAnnouncementMessage()
		{
		}

		// Token: 0x0600024E RID: 590 RVA: 0x000039C8 File Offset: 0x00001BC8
		public AddClanAnnouncementMessage(string announcement)
		{
			this.Announcement = announcement;
		}
	}
}
