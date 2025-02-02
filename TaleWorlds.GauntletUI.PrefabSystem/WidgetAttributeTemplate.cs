using System;

namespace TaleWorlds.GauntletUI.PrefabSystem
{
	// Token: 0x02000012 RID: 18
	public class WidgetAttributeTemplate
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002E68 File Offset: 0x00001068
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00002E70 File Offset: 0x00001070
		public WidgetAttributeKeyType KeyType { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002E79 File Offset: 0x00001079
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00002E81 File Offset: 0x00001081
		public WidgetAttributeValueType ValueType { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002E8A File Offset: 0x0000108A
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00002E92 File Offset: 0x00001092
		public string Key { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002E9B File Offset: 0x0000109B
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00002EA3 File Offset: 0x000010A3
		public string Value { get; set; }
	}
}
