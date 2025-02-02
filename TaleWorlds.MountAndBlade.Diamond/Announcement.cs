using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000EC RID: 236
	[Serializable]
	public class Announcement
	{
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x00005174 File Offset: 0x00003374
		// (set) Token: 0x0600047B RID: 1147 RVA: 0x0000517C File Offset: 0x0000337C
		public int Id { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00005185 File Offset: 0x00003385
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x0000518D File Offset: 0x0000338D
		public Guid BattleId { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00005196 File Offset: 0x00003396
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x0000519E File Offset: 0x0000339E
		public AnnouncementType Type { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x000051A7 File Offset: 0x000033A7
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x000051AF File Offset: 0x000033AF
		public string Text { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x000051B8 File Offset: 0x000033B8
		// (set) Token: 0x06000483 RID: 1155 RVA: 0x000051C0 File Offset: 0x000033C0
		public bool IsEnabled { get; set; }

		// Token: 0x06000484 RID: 1156 RVA: 0x000051C9 File Offset: 0x000033C9
		public Announcement()
		{
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x000051D1 File Offset: 0x000033D1
		public Announcement(int id, Guid battleId, AnnouncementType type, string text, bool isEnabled)
		{
			this.Id = id;
			this.BattleId = battleId;
			this.Type = type;
			this.Text = text;
			this.IsEnabled = isEnabled;
		}
	}
}
