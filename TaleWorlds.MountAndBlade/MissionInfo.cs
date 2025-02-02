using System;
using System.Reflection;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001C4 RID: 452
	public class MissionInfo
	{
		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060019F5 RID: 6645 RVA: 0x0005BCD5 File Offset: 0x00059ED5
		// (set) Token: 0x060019F6 RID: 6646 RVA: 0x0005BCDD File Offset: 0x00059EDD
		public string Name { get; set; }

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060019F7 RID: 6647 RVA: 0x0005BCE6 File Offset: 0x00059EE6
		// (set) Token: 0x060019F8 RID: 6648 RVA: 0x0005BCEE File Offset: 0x00059EEE
		public MethodInfo Creator { get; set; }

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060019F9 RID: 6649 RVA: 0x0005BCF7 File Offset: 0x00059EF7
		// (set) Token: 0x060019FA RID: 6650 RVA: 0x0005BCFF File Offset: 0x00059EFF
		public Type Manager { get; set; }

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060019FB RID: 6651 RVA: 0x0005BD08 File Offset: 0x00059F08
		// (set) Token: 0x060019FC RID: 6652 RVA: 0x0005BD10 File Offset: 0x00059F10
		public bool UsableByEditor { get; set; }
	}
}
