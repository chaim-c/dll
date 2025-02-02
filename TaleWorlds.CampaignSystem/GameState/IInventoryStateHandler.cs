using System;
using TaleWorlds.CampaignSystem.Inventory;

namespace TaleWorlds.CampaignSystem.GameState
{
	// Token: 0x02000339 RID: 825
	public interface IInventoryStateHandler
	{
		// Token: 0x06002EB3 RID: 11955
		void FilterInventoryAtOpening(InventoryManager.InventoryCategoryType inventoryCategoryType);

		// Token: 0x06002EB4 RID: 11956
		void ExecuteLootingScript();

		// Token: 0x06002EB5 RID: 11957
		void ExecuteSellAllLoot();

		// Token: 0x06002EB6 RID: 11958
		void ExecuteBuyConsumableItem();
	}
}
