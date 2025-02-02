using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000234 RID: 564
	public struct DataRecord
	{
		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x00016FA5 File Offset: 0x000151A5
		// (set) Token: 0x06000F8B RID: 3979 RVA: 0x00016FAD File Offset: 0x000151AD
		public Utf8String Key { get; set; }

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x00016FB6 File Offset: 0x000151B6
		// (set) Token: 0x06000F8D RID: 3981 RVA: 0x00016FBE File Offset: 0x000151BE
		public Utf8String Value { get; set; }

		// Token: 0x06000F8E RID: 3982 RVA: 0x00016FC7 File Offset: 0x000151C7
		internal void Set(ref DataRecordInternal other)
		{
			this.Key = other.Key;
			this.Value = other.Value;
		}
	}
}
