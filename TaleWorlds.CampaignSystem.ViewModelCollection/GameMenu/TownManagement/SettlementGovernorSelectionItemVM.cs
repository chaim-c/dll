using System;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.TownManagement
{
	// Token: 0x02000093 RID: 147
	public class SettlementGovernorSelectionItemVM : ViewModel
	{
		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x00039E11 File Offset: 0x00038011
		public Hero Governor { get; }

		// Token: 0x06000E67 RID: 3687 RVA: 0x00039E1C File Offset: 0x0003801C
		public SettlementGovernorSelectionItemVM(Hero governor, Action<SettlementGovernorSelectionItemVM> onSelection)
		{
			this.Governor = governor;
			this._onSelection = onSelection;
			if (governor != null)
			{
				this.Visual = new ImageIdentifierVM(CampaignUIHelper.GetCharacterCode(this.Governor.CharacterObject, true));
				this.GovernorHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetHeroGovernorEffectsTooltip(this.Governor, Settlement.CurrentSettlement));
			}
			else
			{
				this.Visual = new ImageIdentifierVM(ImageIdentifierType.Null);
				this.GovernorHint = new BasicTooltipViewModel();
			}
			this.RefreshValues();
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x00039E94 File Offset: 0x00038094
		public override void RefreshValues()
		{
			base.RefreshValues();
			if (this.Governor != null)
			{
				this.Name = this.Governor.Name.ToString();
				return;
			}
			this.Visual = new ImageIdentifierVM(ImageIdentifierType.Null);
			this.Name = new TextObject("{=koX9okuG}None", null).ToString();
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x00039EE8 File Offset: 0x000380E8
		public void OnSelection()
		{
			Settlement currentSettlement = Settlement.CurrentSettlement;
			Hero hero;
			if (currentSettlement == null)
			{
				hero = null;
			}
			else
			{
				Town town = currentSettlement.Town;
				hero = ((town != null) ? town.Governor : null);
			}
			Hero hero2 = hero;
			bool flag = this.Governor == null;
			if (hero2 != this.Governor && (!flag || hero2 != null))
			{
				ValueTuple<TextObject, TextObject> governorSelectionConfirmationPopupTexts = CampaignUIHelper.GetGovernorSelectionConfirmationPopupTexts(hero2, this.Governor, currentSettlement);
				InformationManager.ShowInquiry(new InquiryData(governorSelectionConfirmationPopupTexts.Item1.ToString(), governorSelectionConfirmationPopupTexts.Item2.ToString(), true, true, GameTexts.FindText("str_yes", null).ToString(), GameTexts.FindText("str_no", null).ToString(), delegate()
				{
					this._onSelection(this);
				}, null, "", 0f, null, null, null), false, false);
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x00039F99 File Offset: 0x00038199
		// (set) Token: 0x06000E6B RID: 3691 RVA: 0x00039FA1 File Offset: 0x000381A1
		[DataSourceProperty]
		public ImageIdentifierVM Visual
		{
			get
			{
				return this._visual;
			}
			set
			{
				if (value != this._visual)
				{
					this._visual = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "Visual");
				}
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x00039FBF File Offset: 0x000381BF
		// (set) Token: 0x06000E6D RID: 3693 RVA: 0x00039FC7 File Offset: 0x000381C7
		[DataSourceProperty]
		public BasicTooltipViewModel GovernorHint
		{
			get
			{
				return this._governorHint;
			}
			set
			{
				if (value != this._governorHint)
				{
					this._governorHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "GovernorHint");
				}
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x00039FE5 File Offset: 0x000381E5
		// (set) Token: 0x06000E6F RID: 3695 RVA: 0x00039FED File Offset: 0x000381ED
		[DataSourceProperty]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value != this._name)
				{
					this._name = value;
					base.OnPropertyChangedWithValue<string>(value, "Name");
				}
			}
		}

		// Token: 0x040006AC RID: 1708
		private readonly Action<SettlementGovernorSelectionItemVM> _onSelection;

		// Token: 0x040006AE RID: 1710
		private ImageIdentifierVM _visual;

		// Token: 0x040006AF RID: 1711
		private string _name;

		// Token: 0x040006B0 RID: 1712
		private BasicTooltipViewModel _governorHint;
	}
}
