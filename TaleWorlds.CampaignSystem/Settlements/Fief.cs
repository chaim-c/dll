﻿using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Party.PartyComponents;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.Settlements
{
	// Token: 0x0200035D RID: 861
	public abstract class Fief : SettlementComponent
	{
		// Token: 0x060031E7 RID: 12775 RVA: 0x000D0FDC File Offset: 0x000CF1DC
		protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			base.AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x060031E8 RID: 12776 RVA: 0x000D0FE5 File Offset: 0x000CF1E5
		internal static object AutoGeneratedGetMemberValueFoodStocks(object o)
		{
			return ((Fief)o).FoodStocks;
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x060031E9 RID: 12777 RVA: 0x000D0FF7 File Offset: 0x000CF1F7
		// (set) Token: 0x060031EA RID: 12778 RVA: 0x000D0FFF File Offset: 0x000CF1FF
		[SaveableProperty(100)]
		public float FoodStocks { get; set; }

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x060031EB RID: 12779 RVA: 0x000D1008 File Offset: 0x000CF208
		public float Militia
		{
			get
			{
				return base.Owner.Settlement.Militia;
			}
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x060031EC RID: 12780 RVA: 0x000D101A File Offset: 0x000CF21A
		public MobileParty GarrisonParty
		{
			get
			{
				GarrisonPartyComponent garrisonPartyComponent = this.GarrisonPartyComponent;
				if (garrisonPartyComponent == null)
				{
					return null;
				}
				return garrisonPartyComponent.MobileParty;
			}
		}

		// Token: 0x04001026 RID: 4134
		[CachedData]
		public GarrisonPartyComponent GarrisonPartyComponent;
	}
}
