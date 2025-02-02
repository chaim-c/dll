using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000248 RID: 584
	public struct PresenceModificationDataRecordId
	{
		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x00018152 File Offset: 0x00016352
		// (set) Token: 0x06001032 RID: 4146 RVA: 0x0001815A File Offset: 0x0001635A
		public Utf8String Key { get; set; }

		// Token: 0x06001033 RID: 4147 RVA: 0x00018163 File Offset: 0x00016363
		internal void Set(ref PresenceModificationDataRecordIdInternal other)
		{
			this.Key = other.Key;
		}
	}
}
