using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TaleWorlds.Library.NewsManager
{
	// Token: 0x020000A6 RID: 166
	public struct NewsType
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x0001406A File Offset: 0x0001226A
		// (set) Token: 0x0600061B RID: 1563 RVA: 0x00014072 File Offset: 0x00012272
		[JsonConverter(typeof(StringEnumConverter))]
		public NewsItem.NewsTypes Type { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x0001407B File Offset: 0x0001227B
		// (set) Token: 0x0600061D RID: 1565 RVA: 0x00014083 File Offset: 0x00012283
		public int Index { get; set; }
	}
}
