using System;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x02000190 RID: 400
	public struct ParticipantMetadata
	{
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x00010D96 File Offset: 0x0000EF96
		// (set) Token: 0x06000B66 RID: 2918 RVA: 0x00010D9E File Offset: 0x0000EF9E
		public Utf8String Key { get; set; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x00010DA7 File Offset: 0x0000EFA7
		// (set) Token: 0x06000B68 RID: 2920 RVA: 0x00010DAF File Offset: 0x0000EFAF
		public Utf8String Value { get; set; }

		// Token: 0x06000B69 RID: 2921 RVA: 0x00010DB8 File Offset: 0x0000EFB8
		internal void Set(ref ParticipantMetadataInternal other)
		{
			this.Key = other.Key;
			this.Value = other.Value;
		}
	}
}
