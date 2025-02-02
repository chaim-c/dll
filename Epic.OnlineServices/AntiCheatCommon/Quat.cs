using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x02000604 RID: 1540
	public struct Quat
	{
		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x0600278C RID: 10124 RVA: 0x0003AFC6 File Offset: 0x000391C6
		// (set) Token: 0x0600278D RID: 10125 RVA: 0x0003AFCE File Offset: 0x000391CE
		public float w { get; set; }

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x0600278E RID: 10126 RVA: 0x0003AFD7 File Offset: 0x000391D7
		// (set) Token: 0x0600278F RID: 10127 RVA: 0x0003AFDF File Offset: 0x000391DF
		public float x { get; set; }

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06002790 RID: 10128 RVA: 0x0003AFE8 File Offset: 0x000391E8
		// (set) Token: 0x06002791 RID: 10129 RVA: 0x0003AFF0 File Offset: 0x000391F0
		public float y { get; set; }

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06002792 RID: 10130 RVA: 0x0003AFF9 File Offset: 0x000391F9
		// (set) Token: 0x06002793 RID: 10131 RVA: 0x0003B001 File Offset: 0x00039201
		public float z { get; set; }

		// Token: 0x06002794 RID: 10132 RVA: 0x0003B00A File Offset: 0x0003920A
		internal void Set(ref QuatInternal other)
		{
			this.w = other.w;
			this.x = other.x;
			this.y = other.y;
			this.z = other.z;
		}
	}
}
