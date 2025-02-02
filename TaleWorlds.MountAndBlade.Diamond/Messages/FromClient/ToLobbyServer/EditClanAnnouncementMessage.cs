using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000091 RID: 145
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class EditClanAnnouncementMessage : Message
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00003E5C File Offset: 0x0000205C
		// (set) Token: 0x060002BF RID: 703 RVA: 0x00003E64 File Offset: 0x00002064
		[JsonProperty]
		public int AnnouncementId { get; private set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00003E6D File Offset: 0x0000206D
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x00003E75 File Offset: 0x00002075
		[JsonProperty]
		public string Text { get; private set; }

		// Token: 0x060002C2 RID: 706 RVA: 0x00003E7E File Offset: 0x0000207E
		public EditClanAnnouncementMessage()
		{
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00003E86 File Offset: 0x00002086
		public EditClanAnnouncementMessage(int announcementId, string text)
		{
			this.AnnouncementId = announcementId;
			this.Text = text;
		}
	}
}
