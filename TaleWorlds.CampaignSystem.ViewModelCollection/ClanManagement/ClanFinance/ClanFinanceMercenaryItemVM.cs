using System;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.ClanFinance
{
	// Token: 0x02000115 RID: 277
	public class ClanFinanceMercenaryItemVM : ClanFinanceIncomeItemBaseVM
	{
		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06001A8F RID: 6799 RVA: 0x0006018F File Offset: 0x0005E38F
		// (set) Token: 0x06001A90 RID: 6800 RVA: 0x00060197 File Offset: 0x0005E397
		public Clan Clan { get; private set; }

		// Token: 0x06001A91 RID: 6801 RVA: 0x000601A0 File Offset: 0x0005E3A0
		public ClanFinanceMercenaryItemVM(Action<ClanFinanceIncomeItemBaseVM> onSelection, Action onRefresh) : base(onSelection, onRefresh)
		{
			base.IncomeTypeAsEnum = IncomeTypes.MercenaryService;
			this.Clan = Clan.PlayerClan;
			if (this.Clan.IsUnderMercenaryService)
			{
				base.Name = GameTexts.FindText("str_mercenary_service", null).ToString();
				base.Income = (int)(this.Clan.Influence * (float)this.Clan.MercenaryAwardMultiplier);
				base.Visual = new ImageIdentifierVM(this.Clan.Banner);
				base.IncomeValueText = base.DetermineIncomeText(base.Income);
			}
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x00060231 File Offset: 0x0005E431
		protected override void PopulateStatsList()
		{
			base.ItemProperties.Add(new SelectableItemPropertyVM("TEST", "TEST", false, null));
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x0006024F File Offset: 0x0005E44F
		protected override void PopulateActionList()
		{
		}
	}
}
