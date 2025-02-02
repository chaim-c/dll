using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign
{
	// Token: 0x020000EE RID: 238
	public class WeaponDesignResultPropertyItemVM : ViewModel
	{
		// Token: 0x060015DE RID: 5598 RVA: 0x000518DC File Offset: 0x0004FADC
		public WeaponDesignResultPropertyItemVM(TextObject description, float value, float changeAmount, bool showFloatingPoint)
		{
			this._description = description;
			this.InitialValue = value;
			this.ChangeAmount = changeAmount;
			this.ShowFloatingPoint = showFloatingPoint;
			this.IsOrderResult = false;
			this.OrderRequirementTooltip = new HintViewModel();
			this.CraftedValueTooltip = new HintViewModel();
			this.BonusPenaltyTooltip = new HintViewModel();
			this.RefreshValues();
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x0005193C File Offset: 0x0004FB3C
		public WeaponDesignResultPropertyItemVM(TextObject description, float craftedValue, float requiredValue, float changeAmount, bool showFloatingPoint, bool isExceedingBeneficial, bool showTooltip = true)
		{
			this._showTooltip = showTooltip;
			this._description = description;
			this.TargetValue = requiredValue;
			this.InitialValue = craftedValue;
			this.ChangeAmount = changeAmount;
			this._isExceedingBeneficial = isExceedingBeneficial;
			this.IsOrderResult = true;
			this.ShowFloatingPoint = showFloatingPoint;
			this.OrderRequirementTooltip = new HintViewModel();
			this.CraftedValueTooltip = new HintViewModel();
			this.BonusPenaltyTooltip = new HintViewModel();
			this.RefreshValues();
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x000519B4 File Offset: 0x0004FBB4
		public override void RefreshValues()
		{
			base.RefreshValues();
			TextObject description = this._description;
			this.PropertyLbl = ((description != null) ? description.ToString() : null);
			TextObject textObject = GameTexts.FindText("str_STR_in_parentheses", null);
			textObject.SetTextVariable("STR", CampaignUIHelper.GetFormattedItemPropertyText(this.TargetValue, this.ShowFloatingPoint));
			this.RequiredValueText = ((this.TargetValue == 0f) ? string.Empty : textObject.ToString());
			this.HasBenefit = (this._isExceedingBeneficial ? (this.InitialValue + this.ChangeAmount >= this.TargetValue) : (this.InitialValue + this.ChangeAmount <= this.TargetValue));
			this.OrderRequirementTooltip.HintText = (this._showTooltip ? GameTexts.FindText("str_crafting_order_requirement_tooltip", null) : TextObject.Empty);
			this.CraftedValueTooltip.HintText = (this._showTooltip ? GameTexts.FindText("str_crafting_crafted_value_tooltip", null) : TextObject.Empty);
			this.BonusPenaltyTooltip.HintText = (this._showTooltip ? GameTexts.FindText("str_crafting_bonus_penalty_tooltip", null) : TextObject.Empty);
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x060015E1 RID: 5601 RVA: 0x00051AD7 File Offset: 0x0004FCD7
		// (set) Token: 0x060015E2 RID: 5602 RVA: 0x00051ADF File Offset: 0x0004FCDF
		[DataSourceProperty]
		public string PropertyLbl
		{
			get
			{
				return this._propertyLbl;
			}
			set
			{
				if (value != this._propertyLbl)
				{
					this._propertyLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "PropertyLbl");
				}
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x060015E3 RID: 5603 RVA: 0x00051B02 File Offset: 0x0004FD02
		// (set) Token: 0x060015E4 RID: 5604 RVA: 0x00051B0A File Offset: 0x0004FD0A
		[DataSourceProperty]
		public float InitialValue
		{
			get
			{
				return this._propertyValue;
			}
			set
			{
				if (value == 0f || value != this._propertyValue)
				{
					this._propertyValue = value;
					base.OnPropertyChangedWithValue(value, "InitialValue");
				}
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x060015E5 RID: 5605 RVA: 0x00051B30 File Offset: 0x0004FD30
		// (set) Token: 0x060015E6 RID: 5606 RVA: 0x00051B38 File Offset: 0x0004FD38
		[DataSourceProperty]
		public float TargetValue
		{
			get
			{
				return this._requiredValue;
			}
			set
			{
				if (value != this._requiredValue)
				{
					this._requiredValue = value;
					base.OnPropertyChangedWithValue(value, "TargetValue");
				}
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x060015E7 RID: 5607 RVA: 0x00051B56 File Offset: 0x0004FD56
		// (set) Token: 0x060015E8 RID: 5608 RVA: 0x00051B5E File Offset: 0x0004FD5E
		[DataSourceProperty]
		public string RequiredValueText
		{
			get
			{
				return this._requiredValueText;
			}
			set
			{
				if (value != this._requiredValueText)
				{
					this._requiredValueText = value;
					base.OnPropertyChangedWithValue<string>(value, "RequiredValueText");
				}
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x060015E9 RID: 5609 RVA: 0x00051B81 File Offset: 0x0004FD81
		// (set) Token: 0x060015EA RID: 5610 RVA: 0x00051B89 File Offset: 0x0004FD89
		[DataSourceProperty]
		public float ChangeAmount
		{
			get
			{
				return this._changeAmount;
			}
			set
			{
				if (this._changeAmount != value)
				{
					this._changeAmount = value;
					base.OnPropertyChangedWithValue(value, "ChangeAmount");
				}
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x060015EB RID: 5611 RVA: 0x00051BA7 File Offset: 0x0004FDA7
		// (set) Token: 0x060015EC RID: 5612 RVA: 0x00051BAF File Offset: 0x0004FDAF
		[DataSourceProperty]
		public bool ShowFloatingPoint
		{
			get
			{
				return this._showFloatingPoint;
			}
			set
			{
				if (this._showFloatingPoint != value)
				{
					this._showFloatingPoint = value;
					base.OnPropertyChangedWithValue(value, "ShowFloatingPoint");
				}
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x060015ED RID: 5613 RVA: 0x00051BCD File Offset: 0x0004FDCD
		// (set) Token: 0x060015EE RID: 5614 RVA: 0x00051BD5 File Offset: 0x0004FDD5
		[DataSourceProperty]
		public bool IsOrderResult
		{
			get
			{
				return this._isOrderResult;
			}
			set
			{
				if (value != this._isOrderResult)
				{
					this._isOrderResult = value;
					base.OnPropertyChangedWithValue(value, "IsOrderResult");
				}
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x060015EF RID: 5615 RVA: 0x00051BF3 File Offset: 0x0004FDF3
		// (set) Token: 0x060015F0 RID: 5616 RVA: 0x00051BFB File Offset: 0x0004FDFB
		[DataSourceProperty]
		public bool HasBenefit
		{
			get
			{
				return this._hasBenefit;
			}
			set
			{
				if (value != this._hasBenefit)
				{
					this._hasBenefit = value;
					base.OnPropertyChangedWithValue(value, "HasBenefit");
				}
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x060015F1 RID: 5617 RVA: 0x00051C19 File Offset: 0x0004FE19
		// (set) Token: 0x060015F2 RID: 5618 RVA: 0x00051C21 File Offset: 0x0004FE21
		[DataSourceProperty]
		public HintViewModel OrderRequirementTooltip
		{
			get
			{
				return this._orderRequirementTooltip;
			}
			set
			{
				if (value != this._orderRequirementTooltip)
				{
					this._orderRequirementTooltip = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "OrderRequirementTooltip");
				}
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x060015F3 RID: 5619 RVA: 0x00051C3F File Offset: 0x0004FE3F
		// (set) Token: 0x060015F4 RID: 5620 RVA: 0x00051C47 File Offset: 0x0004FE47
		[DataSourceProperty]
		public HintViewModel CraftedValueTooltip
		{
			get
			{
				return this._craftedValueTooltip;
			}
			set
			{
				if (value != this._craftedValueTooltip)
				{
					this._craftedValueTooltip = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "CraftedValueTooltip");
				}
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x060015F5 RID: 5621 RVA: 0x00051C65 File Offset: 0x0004FE65
		// (set) Token: 0x060015F6 RID: 5622 RVA: 0x00051C6D File Offset: 0x0004FE6D
		[DataSourceProperty]
		public HintViewModel BonusPenaltyTooltip
		{
			get
			{
				return this._bonusPenaltyTooltip;
			}
			set
			{
				if (value != this._bonusPenaltyTooltip)
				{
					this._bonusPenaltyTooltip = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "BonusPenaltyTooltip");
				}
			}
		}

		// Token: 0x04000A30 RID: 2608
		private readonly TextObject _description;

		// Token: 0x04000A31 RID: 2609
		private bool _isExceedingBeneficial;

		// Token: 0x04000A32 RID: 2610
		private bool _showTooltip;

		// Token: 0x04000A33 RID: 2611
		private string _propertyLbl;

		// Token: 0x04000A34 RID: 2612
		private float _propertyValue;

		// Token: 0x04000A35 RID: 2613
		private float _requiredValue;

		// Token: 0x04000A36 RID: 2614
		private string _requiredValueText;

		// Token: 0x04000A37 RID: 2615
		private float _changeAmount;

		// Token: 0x04000A38 RID: 2616
		private bool _showFloatingPoint;

		// Token: 0x04000A39 RID: 2617
		private bool _isOrderResult;

		// Token: 0x04000A3A RID: 2618
		private bool _hasBenefit;

		// Token: 0x04000A3B RID: 2619
		private HintViewModel _orderRequirementTooltip;

		// Token: 0x04000A3C RID: 2620
		private HintViewModel _craftedValueTooltip;

		// Token: 0x04000A3D RID: 2621
		private HintViewModel _bonusPenaltyTooltip;
	}
}
