using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Diplomacy
{
	// Token: 0x02000063 RID: 99
	public class KingdomTruceItemVM : KingdomDiplomacyItemVM
	{
		// Token: 0x06000897 RID: 2199 RVA: 0x000248D2 File Offset: 0x00022AD2
		public KingdomTruceItemVM(IFaction faction1, IFaction faction2, Action<KingdomDiplomacyItemVM> onSelection, Action<KingdomTruceItemVM> onAction) : base(faction1, faction2)
		{
			this._onAction = onAction;
			this._onSelection = onSelection;
			this.UpdateDiplomacyProperties();
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x000248F1 File Offset: 0x00022AF1
		protected override void OnSelect()
		{
			this.UpdateDiplomacyProperties();
			this._onSelection(this);
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00024908 File Offset: 0x00022B08
		protected override void UpdateDiplomacyProperties()
		{
			base.UpdateDiplomacyProperties();
			base.Stats.Add(new KingdomWarComparableStatVM((int)this.Faction1.TotalStrength, (int)this.Faction2.TotalStrength, GameTexts.FindText("str_total_strength", null), this._faction1Color, this._faction2Color, 10000, null, null));
			base.Stats.Add(new KingdomWarComparableStatVM(this._faction1Towns.Count, this._faction2Towns.Count, GameTexts.FindText("str_towns", null), this._faction1Color, this._faction2Color, 25, new BasicTooltipViewModel(() => CampaignUIHelper.GetTruceOwnedSettlementsTooltip(this._faction1Towns, this.Faction1.Name, true)), new BasicTooltipViewModel(() => CampaignUIHelper.GetTruceOwnedSettlementsTooltip(this._faction2Towns, this.Faction2.Name, true))));
			base.Stats.Add(new KingdomWarComparableStatVM(this._faction1Castles.Count, this._faction2Castles.Count, GameTexts.FindText("str_castles", null), this._faction1Color, this._faction2Color, 25, new BasicTooltipViewModel(() => CampaignUIHelper.GetTruceOwnedSettlementsTooltip(this._faction1Castles, this.Faction1.Name, false)), new BasicTooltipViewModel(() => CampaignUIHelper.GetTruceOwnedSettlementsTooltip(this._faction2Castles, this.Faction2.Name, false))));
			StanceLink stanceWith = this._playerKingdom.GetStanceWith(this.Faction2);
			this.TributePaid = stanceWith.GetDailyTributePaid(this._playerKingdom);
			if (stanceWith.IsNeutral && this.TributePaid != 0)
			{
				base.Stats.Add(new KingdomWarComparableStatVM(stanceWith.GetTotalTributePaid(this.Faction2), stanceWith.GetTotalTributePaid(this.Faction1), GameTexts.FindText("str_comparison_tribute_received", null), this._faction1Color, this._faction2Color, 10000, null, null));
			}
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00024A9D File Offset: 0x00022C9D
		protected override void ExecuteAction()
		{
			this._onAction(this);
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x00024AAB File Offset: 0x00022CAB
		// (set) Token: 0x0600089C RID: 2204 RVA: 0x00024AB3 File Offset: 0x00022CB3
		public int TributePaid
		{
			get
			{
				return this._tributePaid;
			}
			set
			{
				if (value != this._tributePaid)
				{
					this._tributePaid = value;
					base.OnPropertyChangedWithValue(value, "TributePaid");
				}
			}
		}

		// Token: 0x040003D5 RID: 981
		private readonly Action<KingdomTruceItemVM> _onAction;

		// Token: 0x040003D6 RID: 982
		private readonly Action<KingdomDiplomacyItemVM> _onSelection;

		// Token: 0x040003D7 RID: 983
		private int _tributePaid;
	}
}
