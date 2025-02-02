using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000488 RID: 1160
	public struct CatalogRelease
	{
		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06001E0D RID: 7693 RVA: 0x0002C7DC File Offset: 0x0002A9DC
		// (set) Token: 0x06001E0E RID: 7694 RVA: 0x0002C7E4 File Offset: 0x0002A9E4
		public Utf8String[] CompatibleAppIds { get; set; }

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06001E0F RID: 7695 RVA: 0x0002C7ED File Offset: 0x0002A9ED
		// (set) Token: 0x06001E10 RID: 7696 RVA: 0x0002C7F5 File Offset: 0x0002A9F5
		public Utf8String[] CompatiblePlatforms { get; set; }

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06001E11 RID: 7697 RVA: 0x0002C7FE File Offset: 0x0002A9FE
		// (set) Token: 0x06001E12 RID: 7698 RVA: 0x0002C806 File Offset: 0x0002AA06
		public Utf8String ReleaseNote { get; set; }

		// Token: 0x06001E13 RID: 7699 RVA: 0x0002C80F File Offset: 0x0002AA0F
		internal void Set(ref CatalogReleaseInternal other)
		{
			this.CompatibleAppIds = other.CompatibleAppIds;
			this.CompatiblePlatforms = other.CompatiblePlatforms;
			this.ReleaseNote = other.ReleaseNote;
		}
	}
}
