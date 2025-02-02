﻿using System;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Workshops;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003A7 RID: 935
	public interface IWorkshopWarehouseCampaignBehavior
	{
		// Token: 0x06003827 RID: 14375
		bool IsGettingInputsFromWarehouse(Workshop workshop);

		// Token: 0x06003828 RID: 14376
		void SetIsGettingInputsFromWarehouse(Workshop workshop, bool isActive);

		// Token: 0x06003829 RID: 14377
		float GetStockProductionInWarehouseRatio(Workshop workshop);

		// Token: 0x0600382A RID: 14378
		void SetStockProductionInWarehouseRatio(Workshop workshop, float percentage);

		// Token: 0x0600382B RID: 14379
		float GetWarehouseItemRosterWeight(Settlement settlement);

		// Token: 0x0600382C RID: 14380
		bool IsRawMaterialsSufficientInTownMarket(Workshop workshop);

		// Token: 0x0600382D RID: 14381
		int GetInputCount(Workshop workshop);

		// Token: 0x0600382E RID: 14382
		int GetOutputCount(Workshop workshop);

		// Token: 0x0600382F RID: 14383
		ExplainedNumber GetInputDailyChange(Workshop workshop);

		// Token: 0x06003830 RID: 14384
		ExplainedNumber GetOutputDailyChange(Workshop workshop);
	}
}
