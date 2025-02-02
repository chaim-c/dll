using System;
using Newtonsoft.Json;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000157 RID: 343
	[Serializable]
	public class PremadeGameList
	{
		// Token: 0x170002FE RID: 766
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x0000E24C File Offset: 0x0000C44C
		// (set) Token: 0x0600098B RID: 2443 RVA: 0x0000E253 File Offset: 0x0000C453
		public static PremadeGameList Empty { get; private set; } = new PremadeGameList(new PremadeGameEntry[0]);

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x0000E26D File Offset: 0x0000C46D
		// (set) Token: 0x0600098E RID: 2446 RVA: 0x0000E275 File Offset: 0x0000C475
		[JsonProperty]
		public PremadeGameEntry[] PremadeGameEntries { get; private set; }

		// Token: 0x0600098F RID: 2447 RVA: 0x0000E27E File Offset: 0x0000C47E
		public PremadeGameList(PremadeGameEntry[] entries)
		{
			this.PremadeGameEntries = entries;
		}
	}
}
