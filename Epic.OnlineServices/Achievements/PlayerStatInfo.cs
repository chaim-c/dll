using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000696 RID: 1686
	public struct PlayerStatInfo
	{
		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x06002B5D RID: 11101 RVA: 0x00040F10 File Offset: 0x0003F110
		// (set) Token: 0x06002B5E RID: 11102 RVA: 0x00040F18 File Offset: 0x0003F118
		public Utf8String Name { get; set; }

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x06002B5F RID: 11103 RVA: 0x00040F21 File Offset: 0x0003F121
		// (set) Token: 0x06002B60 RID: 11104 RVA: 0x00040F29 File Offset: 0x0003F129
		public int CurrentValue { get; set; }

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x06002B61 RID: 11105 RVA: 0x00040F32 File Offset: 0x0003F132
		// (set) Token: 0x06002B62 RID: 11106 RVA: 0x00040F3A File Offset: 0x0003F13A
		public int ThresholdValue { get; set; }

		// Token: 0x06002B63 RID: 11107 RVA: 0x00040F43 File Offset: 0x0003F143
		internal void Set(ref PlayerStatInfoInternal other)
		{
			this.Name = other.Name;
			this.CurrentValue = other.CurrentValue;
			this.ThresholdValue = other.ThresholdValue;
		}
	}
}
