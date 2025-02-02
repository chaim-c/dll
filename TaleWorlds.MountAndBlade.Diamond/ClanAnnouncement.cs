using System;
using Newtonsoft.Json;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000104 RID: 260
	[Serializable]
	public class ClanAnnouncement
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x00007379 File Offset: 0x00005579
		// (set) Token: 0x06000581 RID: 1409 RVA: 0x00007381 File Offset: 0x00005581
		[JsonProperty]
		public int Id { get; private set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x0000738A File Offset: 0x0000558A
		// (set) Token: 0x06000583 RID: 1411 RVA: 0x00007392 File Offset: 0x00005592
		[JsonProperty]
		public string Announcement { get; private set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0000739B File Offset: 0x0000559B
		// (set) Token: 0x06000585 RID: 1413 RVA: 0x000073A3 File Offset: 0x000055A3
		[JsonProperty]
		public PlayerId AuthorId { get; private set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x000073AC File Offset: 0x000055AC
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x000073B4 File Offset: 0x000055B4
		[JsonProperty]
		public DateTime CreationTime { get; private set; }

		// Token: 0x06000588 RID: 1416 RVA: 0x000073BD File Offset: 0x000055BD
		public ClanAnnouncement(int id, string announcement, PlayerId authorId, DateTime creationTime)
		{
			this.Id = id;
			this.Announcement = announcement;
			this.AuthorId = authorId;
			this.CreationTime = creationTime;
		}
	}
}
