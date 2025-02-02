using System;
using TaleWorlds.Library;

namespace TaleWorlds.GauntletUI.Data
{
	// Token: 0x0200000C RID: 12
	internal class ViewBindDataInfo
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00004329 File Offset: 0x00002529
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00004331 File Offset: 0x00002531
		internal GauntletView Owner { get; private set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000094 RID: 148 RVA: 0x0000433A File Offset: 0x0000253A
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00004342 File Offset: 0x00002542
		internal string Property { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000096 RID: 150 RVA: 0x0000434B File Offset: 0x0000254B
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00004353 File Offset: 0x00002553
		internal BindingPath Path { get; private set; }

		// Token: 0x06000098 RID: 152 RVA: 0x0000435C File Offset: 0x0000255C
		internal ViewBindDataInfo(GauntletView view, string property, BindingPath path)
		{
			this.Owner = view;
			this.Property = property;
			this.Path = path;
		}
	}
}
