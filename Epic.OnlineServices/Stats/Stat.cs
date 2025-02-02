using System;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020000B9 RID: 185
	public struct Stat
	{
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x00009F59 File Offset: 0x00008159
		// (set) Token: 0x060006AC RID: 1708 RVA: 0x00009F61 File Offset: 0x00008161
		public Utf8String Name { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x00009F6A File Offset: 0x0000816A
		// (set) Token: 0x060006AE RID: 1710 RVA: 0x00009F72 File Offset: 0x00008172
		public DateTimeOffset? StartTime { get; set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x00009F7B File Offset: 0x0000817B
		// (set) Token: 0x060006B0 RID: 1712 RVA: 0x00009F83 File Offset: 0x00008183
		public DateTimeOffset? EndTime { get; set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x00009F8C File Offset: 0x0000818C
		// (set) Token: 0x060006B2 RID: 1714 RVA: 0x00009F94 File Offset: 0x00008194
		public int Value { get; set; }

		// Token: 0x060006B3 RID: 1715 RVA: 0x00009F9D File Offset: 0x0000819D
		internal void Set(ref StatInternal other)
		{
			this.Name = other.Name;
			this.StartTime = other.StartTime;
			this.EndTime = other.EndTime;
			this.Value = other.Value;
		}
	}
}
