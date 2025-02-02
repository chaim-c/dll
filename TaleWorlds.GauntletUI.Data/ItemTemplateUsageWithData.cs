using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI.PrefabSystem;

namespace TaleWorlds.GauntletUI.Data
{
	// Token: 0x02000009 RID: 9
	public class ItemTemplateUsageWithData
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000039C2 File Offset: 0x00001BC2
		// (set) Token: 0x06000078 RID: 120 RVA: 0x000039CA File Offset: 0x00001BCA
		public Dictionary<string, WidgetAttributeTemplate> GivenParameters { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000039D3 File Offset: 0x00001BD3
		// (set) Token: 0x0600007A RID: 122 RVA: 0x000039DB File Offset: 0x00001BDB
		public ItemTemplateUsage ItemTemplateUsage { get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000039E4 File Offset: 0x00001BE4
		public WidgetTemplate DefaultItemTemplate
		{
			get
			{
				return this.ItemTemplateUsage.DefaultItemTemplate;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000039F1 File Offset: 0x00001BF1
		public WidgetTemplate FirstItemTemplate
		{
			get
			{
				return this.ItemTemplateUsage.FirstItemTemplate;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000039FE File Offset: 0x00001BFE
		public WidgetTemplate LastItemTemplate
		{
			get
			{
				return this.ItemTemplateUsage.LastItemTemplate;
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003A0B File Offset: 0x00001C0B
		public ItemTemplateUsageWithData(ItemTemplateUsage itemTemplateUsage)
		{
			this.GivenParameters = new Dictionary<string, WidgetAttributeTemplate>();
			this.ItemTemplateUsage = itemTemplateUsage;
		}
	}
}
