using System;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002F7 RID: 759
	public struct ModIdentifier
	{
		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x0001E4E3 File Offset: 0x0001C6E3
		// (set) Token: 0x06001472 RID: 5234 RVA: 0x0001E4EB File Offset: 0x0001C6EB
		public Utf8String NamespaceId { get; set; }

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x0001E4F4 File Offset: 0x0001C6F4
		// (set) Token: 0x06001474 RID: 5236 RVA: 0x0001E4FC File Offset: 0x0001C6FC
		public Utf8String ItemId { get; set; }

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x0001E505 File Offset: 0x0001C705
		// (set) Token: 0x06001476 RID: 5238 RVA: 0x0001E50D File Offset: 0x0001C70D
		public Utf8String ArtifactId { get; set; }

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x0001E516 File Offset: 0x0001C716
		// (set) Token: 0x06001478 RID: 5240 RVA: 0x0001E51E File Offset: 0x0001C71E
		public Utf8String Title { get; set; }

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x0001E527 File Offset: 0x0001C727
		// (set) Token: 0x0600147A RID: 5242 RVA: 0x0001E52F File Offset: 0x0001C72F
		public Utf8String Version { get; set; }

		// Token: 0x0600147B RID: 5243 RVA: 0x0001E538 File Offset: 0x0001C738
		internal void Set(ref ModIdentifierInternal other)
		{
			this.NamespaceId = other.NamespaceId;
			this.ItemId = other.ItemId;
			this.ArtifactId = other.ArtifactId;
			this.Title = other.Title;
			this.Version = other.Version;
		}
	}
}
