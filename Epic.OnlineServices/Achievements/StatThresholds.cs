using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200069C RID: 1692
	public struct StatThresholds
	{
		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06002B83 RID: 11139 RVA: 0x0004128E File Offset: 0x0003F48E
		// (set) Token: 0x06002B84 RID: 11140 RVA: 0x00041296 File Offset: 0x0003F496
		public Utf8String Name { get; set; }

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06002B85 RID: 11141 RVA: 0x0004129F File Offset: 0x0003F49F
		// (set) Token: 0x06002B86 RID: 11142 RVA: 0x000412A7 File Offset: 0x0003F4A7
		public int Threshold { get; set; }

		// Token: 0x06002B87 RID: 11143 RVA: 0x000412B0 File Offset: 0x0003F4B0
		internal void Set(ref StatThresholdsInternal other)
		{
			this.Name = other.Name;
			this.Threshold = other.Threshold;
		}
	}
}
