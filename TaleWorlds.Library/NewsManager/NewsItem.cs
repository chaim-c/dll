using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TaleWorlds.Library.NewsManager
{
	// Token: 0x020000A5 RID: 165
	public struct NewsItem
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x00014015 File Offset: 0x00012215
		// (set) Token: 0x06000611 RID: 1553 RVA: 0x0001401D File Offset: 0x0001221D
		public string Title { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x00014026 File Offset: 0x00012226
		// (set) Token: 0x06000613 RID: 1555 RVA: 0x0001402E File Offset: 0x0001222E
		public string Description { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x00014037 File Offset: 0x00012237
		// (set) Token: 0x06000615 RID: 1557 RVA: 0x0001403F File Offset: 0x0001223F
		public string ImageSourcePath { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x00014048 File Offset: 0x00012248
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x00014050 File Offset: 0x00012250
		public List<NewsType> Feeds { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x00014059 File Offset: 0x00012259
		// (set) Token: 0x06000619 RID: 1561 RVA: 0x00014061 File Offset: 0x00012261
		public string NewsLink { get; set; }

		// Token: 0x020000EB RID: 235
		[JsonConverter(typeof(StringEnumConverter))]
		public enum NewsTypes
		{
			// Token: 0x040002DA RID: 730
			LauncherSingleplayer,
			// Token: 0x040002DB RID: 731
			LauncherMultiplayer,
			// Token: 0x040002DC RID: 732
			MultiplayerLobby
		}
	}
}
