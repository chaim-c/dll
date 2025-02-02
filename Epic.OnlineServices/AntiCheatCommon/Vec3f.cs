using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x0200060E RID: 1550
	public struct Vec3f
	{
		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x060027CF RID: 10191 RVA: 0x0003B565 File Offset: 0x00039765
		// (set) Token: 0x060027D0 RID: 10192 RVA: 0x0003B56D File Offset: 0x0003976D
		public float x { get; set; }

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x060027D1 RID: 10193 RVA: 0x0003B576 File Offset: 0x00039776
		// (set) Token: 0x060027D2 RID: 10194 RVA: 0x0003B57E File Offset: 0x0003977E
		public float y { get; set; }

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x060027D3 RID: 10195 RVA: 0x0003B587 File Offset: 0x00039787
		// (set) Token: 0x060027D4 RID: 10196 RVA: 0x0003B58F File Offset: 0x0003978F
		public float z { get; set; }

		// Token: 0x060027D5 RID: 10197 RVA: 0x0003B598 File Offset: 0x00039798
		internal void Set(ref Vec3fInternal other)
		{
			this.x = other.x;
			this.y = other.y;
			this.z = other.z;
		}
	}
}
