using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000057 RID: 87
	[Serializable]
	public class ShowAnnouncementMessage : Message
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000315C File Offset: 0x0000135C
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00003164 File Offset: 0x00001364
		[JsonProperty]
		public Announcement Announcement { get; private set; }

		// Token: 0x060001A0 RID: 416 RVA: 0x0000316D File Offset: 0x0000136D
		public ShowAnnouncementMessage()
		{
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00003175 File Offset: 0x00001375
		public ShowAnnouncementMessage(Announcement announcement)
		{
			this.Announcement = announcement;
		}
	}
}
