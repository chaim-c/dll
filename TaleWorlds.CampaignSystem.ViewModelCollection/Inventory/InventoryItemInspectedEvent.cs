using System;
using TaleWorlds.CampaignSystem.Inventory;
using TaleWorlds.Core;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Inventory
{
	// Token: 0x02000086 RID: 134
	public class InventoryItemInspectedEvent : EventBase
	{
		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x00036250 File Offset: 0x00034450
		// (set) Token: 0x06000D48 RID: 3400 RVA: 0x00036258 File Offset: 0x00034458
		public ItemRosterElement Item { get; private set; }

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x00036261 File Offset: 0x00034461
		// (set) Token: 0x06000D4A RID: 3402 RVA: 0x00036269 File Offset: 0x00034469
		public InventoryLogic.InventorySide ItemSide { get; private set; }

		// Token: 0x06000D4B RID: 3403 RVA: 0x00036272 File Offset: 0x00034472
		public InventoryItemInspectedEvent(ItemRosterElement item, InventoryLogic.InventorySide itemSide)
		{
			this.ItemSide = itemSide;
			this.Item = item;
		}
	}
}
