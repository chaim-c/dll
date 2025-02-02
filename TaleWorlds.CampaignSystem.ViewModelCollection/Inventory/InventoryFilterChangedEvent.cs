using System;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Inventory
{
	// Token: 0x02000085 RID: 133
	public class InventoryFilterChangedEvent : EventBase
	{
		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x00036230 File Offset: 0x00034430
		// (set) Token: 0x06000D45 RID: 3397 RVA: 0x00036238 File Offset: 0x00034438
		public SPInventoryVM.Filters NewFilter { get; private set; }

		// Token: 0x06000D46 RID: 3398 RVA: 0x00036241 File Offset: 0x00034441
		public InventoryFilterChangedEvent(SPInventoryVM.Filters newFilter)
		{
			this.NewFilter = newFilter;
		}
	}
}
