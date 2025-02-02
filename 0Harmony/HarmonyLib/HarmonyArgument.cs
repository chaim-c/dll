using System;

namespace HarmonyLib
{
	// Token: 0x02000034 RID: 52
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Parameter, AllowMultiple = true)]
	public class HarmonyArgument : Attribute
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00009605 File Offset: 0x00007805
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x0000960D File Offset: 0x0000780D
		public string OriginalName { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00009616 File Offset: 0x00007816
		// (set) Token: 0x060000FA RID: 250 RVA: 0x0000961E File Offset: 0x0000781E
		public int Index { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00009627 File Offset: 0x00007827
		// (set) Token: 0x060000FC RID: 252 RVA: 0x0000962F File Offset: 0x0000782F
		public string NewName { get; private set; }

		// Token: 0x060000FD RID: 253 RVA: 0x00009638 File Offset: 0x00007838
		public HarmonyArgument(string originalName) : this(originalName, null)
		{
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00009642 File Offset: 0x00007842
		public HarmonyArgument(int index) : this(index, null)
		{
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000964C File Offset: 0x0000784C
		public HarmonyArgument(string originalName, string newName)
		{
			this.OriginalName = originalName;
			this.Index = -1;
			this.NewName = newName;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00009669 File Offset: 0x00007869
		public HarmonyArgument(int index, string name)
		{
			this.OriginalName = null;
			this.Index = index;
			this.NewName = name;
		}
	}
}
