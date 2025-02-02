using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.Attributes
{
	// Token: 0x0200009C RID: 156
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class BaseSettingPropertyAttribute : Attribute, IPropertyDefinitionBase
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000ADE0 File Offset: 0x00008FE0
		public string DisplayName { get; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000ADE8 File Offset: 0x00008FE8
		// (set) Token: 0x06000354 RID: 852 RVA: 0x0000ADF0 File Offset: 0x00008FF0
		public int Order { get; set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000ADF9 File Offset: 0x00008FF9
		// (set) Token: 0x06000356 RID: 854 RVA: 0x0000AE01 File Offset: 0x00009001
		public bool RequireRestart { get; set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000AE0A File Offset: 0x0000900A
		// (set) Token: 0x06000358 RID: 856 RVA: 0x0000AE12 File Offset: 0x00009012
		public string HintText { get; set; }

		// Token: 0x06000359 RID: 857 RVA: 0x0000AE1B File Offset: 0x0000901B
		protected BaseSettingPropertyAttribute(string displayName, int order = -1, bool requireRestart = true, string hintText = "")
		{
			this.DisplayName = displayName;
			this.Order = order;
			this.RequireRestart = requireRestart;
			this.HintText = hintText;
		}
	}
}
