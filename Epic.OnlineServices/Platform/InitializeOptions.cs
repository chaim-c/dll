using System;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x0200064F RID: 1615
	public struct InitializeOptions
	{
		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x0600292E RID: 10542 RVA: 0x0003D794 File Offset: 0x0003B994
		// (set) Token: 0x0600292F RID: 10543 RVA: 0x0003D79C File Offset: 0x0003B99C
		public IntPtr AllocateMemoryFunction { get; set; }

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06002930 RID: 10544 RVA: 0x0003D7A5 File Offset: 0x0003B9A5
		// (set) Token: 0x06002931 RID: 10545 RVA: 0x0003D7AD File Offset: 0x0003B9AD
		public IntPtr ReallocateMemoryFunction { get; set; }

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06002932 RID: 10546 RVA: 0x0003D7B6 File Offset: 0x0003B9B6
		// (set) Token: 0x06002933 RID: 10547 RVA: 0x0003D7BE File Offset: 0x0003B9BE
		public IntPtr ReleaseMemoryFunction { get; set; }

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06002934 RID: 10548 RVA: 0x0003D7C7 File Offset: 0x0003B9C7
		// (set) Token: 0x06002935 RID: 10549 RVA: 0x0003D7CF File Offset: 0x0003B9CF
		public Utf8String ProductName { get; set; }

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06002936 RID: 10550 RVA: 0x0003D7D8 File Offset: 0x0003B9D8
		// (set) Token: 0x06002937 RID: 10551 RVA: 0x0003D7E0 File Offset: 0x0003B9E0
		public Utf8String ProductVersion { get; set; }

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06002938 RID: 10552 RVA: 0x0003D7E9 File Offset: 0x0003B9E9
		// (set) Token: 0x06002939 RID: 10553 RVA: 0x0003D7F1 File Offset: 0x0003B9F1
		public IntPtr SystemInitializeOptions { get; set; }

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x0600293A RID: 10554 RVA: 0x0003D7FA File Offset: 0x0003B9FA
		// (set) Token: 0x0600293B RID: 10555 RVA: 0x0003D802 File Offset: 0x0003BA02
		public InitializeThreadAffinity? OverrideThreadAffinity { get; set; }
	}
}
