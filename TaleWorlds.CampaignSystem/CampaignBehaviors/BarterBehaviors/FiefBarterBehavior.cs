﻿using System;
using TaleWorlds.CampaignSystem.BarterSystem;
using TaleWorlds.CampaignSystem.BarterSystem.Barterables;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.BarterBehaviors
{
	// Token: 0x020003F9 RID: 1017
	public class FiefBarterBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003EC5 RID: 16069 RVA: 0x001343B3 File Offset: 0x001325B3
		public override void RegisterEvents()
		{
			CampaignEvents.BarterablesRequested.AddNonSerializedListener(this, new Action<BarterData>(this.CheckForBarters));
		}

		// Token: 0x06003EC6 RID: 16070 RVA: 0x001343CC File Offset: 0x001325CC
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06003EC7 RID: 16071 RVA: 0x001343D0 File Offset: 0x001325D0
		public void CheckForBarters(BarterData args)
		{
			if (args.OffererHero != null && args.OtherHero != null && args.OffererHero.GetPerkValue(DefaultPerks.Trade.EverythingHasAPrice))
			{
				foreach (Settlement settlement in Settlement.All)
				{
					if (!settlement.IsVillage)
					{
						Clan ownerClan = settlement.OwnerClan;
						if (((ownerClan != null) ? ownerClan.Leader : null) == args.OffererHero && !args.OtherHero.Clan.IsUnderMercenaryService)
						{
							Barterable barterable = new FiefBarterable(settlement, args.OffererHero, args.OtherHero);
							args.AddBarterable<FiefBarterGroup>(barterable, false);
						}
						else
						{
							Clan ownerClan2 = settlement.OwnerClan;
							if (((ownerClan2 != null) ? ownerClan2.Leader : null) == args.OtherHero && !args.OffererHero.Clan.IsUnderMercenaryService)
							{
								Barterable barterable2 = new FiefBarterable(settlement, args.OtherHero, args.OffererHero);
								args.AddBarterable<FiefBarterGroup>(barterable2, false);
							}
						}
					}
				}
			}
		}
	}
}
