using System;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x02000641 RID: 1601
	public struct AndroidInitializeOptions
	{
		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x060028B2 RID: 10418 RVA: 0x0003C919 File Offset: 0x0003AB19
		// (set) Token: 0x060028B3 RID: 10419 RVA: 0x0003C921 File Offset: 0x0003AB21
		public IntPtr AllocateMemoryFunction { get; set; }

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x060028B4 RID: 10420 RVA: 0x0003C92A File Offset: 0x0003AB2A
		// (set) Token: 0x060028B5 RID: 10421 RVA: 0x0003C932 File Offset: 0x0003AB32
		public IntPtr ReallocateMemoryFunction { get; set; }

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x060028B6 RID: 10422 RVA: 0x0003C93B File Offset: 0x0003AB3B
		// (set) Token: 0x060028B7 RID: 10423 RVA: 0x0003C943 File Offset: 0x0003AB43
		public IntPtr ReleaseMemoryFunction { get; set; }

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x060028B8 RID: 10424 RVA: 0x0003C94C File Offset: 0x0003AB4C
		// (set) Token: 0x060028B9 RID: 10425 RVA: 0x0003C954 File Offset: 0x0003AB54
		public Utf8String ProductName { get; set; }

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x060028BA RID: 10426 RVA: 0x0003C95D File Offset: 0x0003AB5D
		// (set) Token: 0x060028BB RID: 10427 RVA: 0x0003C965 File Offset: 0x0003AB65
		public Utf8String ProductVersion { get; set; }

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x060028BC RID: 10428 RVA: 0x0003C96E File Offset: 0x0003AB6E
		// (set) Token: 0x060028BD RID: 10429 RVA: 0x0003C976 File Offset: 0x0003AB76
		public IntPtr Reserved { get; set; }

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x060028BE RID: 10430 RVA: 0x0003C97F File Offset: 0x0003AB7F
		// (set) Token: 0x060028BF RID: 10431 RVA: 0x0003C987 File Offset: 0x0003AB87
		public AndroidInitializeOptionsSystemInitializeOptions? SystemInitializeOptions { get; set; }

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x060028C0 RID: 10432 RVA: 0x0003C990 File Offset: 0x0003AB90
		// (set) Token: 0x060028C1 RID: 10433 RVA: 0x0003C998 File Offset: 0x0003AB98
		public InitializeThreadAffinity? OverrideThreadAffinity { get; set; }
	}
}
