using System;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.ClanFinance
{
	// Token: 0x02000114 RID: 276
	public class ClanFinanceCommonAreaItemVM : ClanFinanceIncomeItemBaseVM
	{
		// Token: 0x06001A8C RID: 6796 RVA: 0x000600A8 File Offset: 0x0005E2A8
		public ClanFinanceCommonAreaItemVM(Alley alley, Action<ClanFinanceIncomeItemBaseVM> onSelection, Action onRefresh) : base(onSelection, onRefresh)
		{
			base.IncomeTypeAsEnum = IncomeTypes.CommonArea;
			this._alley = alley;
			GameTexts.SetVariable("SETTLEMENT_NAME", alley.Settlement.Name);
			GameTexts.SetVariable("COMMON_AREA_NAME", alley.Name);
			base.Name = GameTexts.FindText("str_clan_finance_common_area", null).ToString();
			base.Income = Campaign.Current.Models.AlleyModel.GetDailyIncomeOfAlley(alley);
			base.Visual = ((alley.Owner.CharacterObject != null) ? new ImageIdentifierVM(CharacterCode.CreateFrom(alley.Owner.CharacterObject)) : new ImageIdentifierVM(ImageIdentifierType.Null));
			base.IncomeValueText = base.DetermineIncomeText(base.Income);
			this.PopulateActionList();
			this.PopulateStatsList();
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x0006016F File Offset: 0x0005E36F
		protected override void PopulateActionList()
		{
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x00060171 File Offset: 0x0005E371
		protected override void PopulateStatsList()
		{
			base.ItemProperties.Add(new SelectableItemPropertyVM("TEST", "TEST", false, null));
		}

		// Token: 0x04000C8C RID: 3212
		private Alley _alley;
	}
}
