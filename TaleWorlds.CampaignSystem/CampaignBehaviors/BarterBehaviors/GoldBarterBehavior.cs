using System;
using TaleWorlds.CampaignSystem.BarterSystem;
using TaleWorlds.CampaignSystem.BarterSystem.Barterables;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.BarterBehaviors
{
	// Token: 0x020003FA RID: 1018
	public class GoldBarterBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003EC9 RID: 16073 RVA: 0x001344F0 File Offset: 0x001326F0
		public override void RegisterEvents()
		{
			CampaignEvents.BarterablesRequested.AddNonSerializedListener(this, new Action<BarterData>(this.CheckForBarters));
		}

		// Token: 0x06003ECA RID: 16074 RVA: 0x00134509 File Offset: 0x00132709
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06003ECB RID: 16075 RVA: 0x0013450C File Offset: 0x0013270C
		public void CheckForBarters(BarterData args)
		{
			if ((args.OffererHero != null && args.OtherHero != null && args.OffererHero.Clan != args.OtherHero.Clan) || (args.OffererHero == null && args.OffererParty != null) || (args.OtherHero == null && args.OtherParty != null))
			{
				int val = (args.OffererHero != null) ? args.OffererHero.Gold : args.OffererParty.MobileParty.PartyTradeGold;
				int val2 = (args.OtherHero != null) ? args.OtherHero.Gold : args.OtherParty.MobileParty.PartyTradeGold;
				Barterable barterable = new GoldBarterable(args.OffererHero, args.OtherHero, args.OffererParty, args.OtherParty, val);
				args.AddBarterable<GoldBarterGroup>(barterable, false);
				Barterable barterable2 = new GoldBarterable(args.OtherHero, args.OffererHero, args.OtherParty, args.OffererParty, val2);
				args.AddBarterable<GoldBarterGroup>(barterable2, false);
			}
		}
	}
}
