using System;
using TaleWorlds.GauntletUI.PrefabSystem;

namespace TaleWorlds.GauntletUI.Data
{
	// Token: 0x02000008 RID: 8
	public class ItemTemplateUsage
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003980 File Offset: 0x00001B80
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00003988 File Offset: 0x00001B88
		public WidgetTemplate DefaultItemTemplate { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003991 File Offset: 0x00001B91
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00003999 File Offset: 0x00001B99
		public WidgetTemplate FirstItemTemplate { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000039A2 File Offset: 0x00001BA2
		// (set) Token: 0x06000075 RID: 117 RVA: 0x000039AA File Offset: 0x00001BAA
		public WidgetTemplate LastItemTemplate { get; set; }

		// Token: 0x06000076 RID: 118 RVA: 0x000039B3 File Offset: 0x00001BB3
		public ItemTemplateUsage(WidgetTemplate defaultItemTemplate)
		{
			this.DefaultItemTemplate = defaultItemTemplate;
		}
	}
}
