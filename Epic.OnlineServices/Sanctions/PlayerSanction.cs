using System;

namespace Epic.OnlineServices.Sanctions
{
	// Token: 0x0200016C RID: 364
	public struct PlayerSanction
	{
		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		// (set) Token: 0x06000A5A RID: 2650 RVA: 0x0000F6A9 File Offset: 0x0000D8A9
		public long TimePlaced { get; set; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0000F6B2 File Offset: 0x0000D8B2
		// (set) Token: 0x06000A5C RID: 2652 RVA: 0x0000F6BA File Offset: 0x0000D8BA
		public Utf8String Action { get; set; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x0000F6C3 File Offset: 0x0000D8C3
		// (set) Token: 0x06000A5E RID: 2654 RVA: 0x0000F6CB File Offset: 0x0000D8CB
		public long TimeExpires { get; set; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x0000F6D4 File Offset: 0x0000D8D4
		// (set) Token: 0x06000A60 RID: 2656 RVA: 0x0000F6DC File Offset: 0x0000D8DC
		public Utf8String ReferenceId { get; set; }

		// Token: 0x06000A61 RID: 2657 RVA: 0x0000F6E5 File Offset: 0x0000D8E5
		internal void Set(ref PlayerSanctionInternal other)
		{
			this.TimePlaced = other.TimePlaced;
			this.Action = other.Action;
			this.TimeExpires = other.TimeExpires;
			this.ReferenceId = other.ReferenceId;
		}
	}
}
