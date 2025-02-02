using System;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement
{
	// Token: 0x02000104 RID: 260
	public class ClanFinanceExpenseItemVM : ViewModel
	{
		// Token: 0x06001859 RID: 6233 RVA: 0x00059A5C File Offset: 0x00057C5C
		public ClanFinanceExpenseItemVM(MobileParty mobileParty)
		{
			this._mobileParty = mobileParty;
			this.CurrentWageTooltip = new BasicTooltipViewModel(() => CampaignUIHelper.GetPartyWageTooltip(mobileParty));
			this.MinWage = 100;
			this.MaxWage = 2000;
			this.CurrentWage = this._mobileParty.TotalWage;
			this.CurrentWageValueText = this.CurrentWage.ToString();
			this.IsUnlimitedWage = !this._mobileParty.HasLimitedWage();
			this.CurrentWageLimit = ((this._mobileParty.PaymentLimit == Campaign.Current.Models.PartyWageModel.MaxWage) ? 2000 : this._mobileParty.PaymentLimit);
			this.IsEnabled = true;
			this.RefreshValues();
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x00059B34 File Offset: 0x00057D34
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.CurrentWageText = new TextObject("{=pnFgwLYG}Current Wage", null).ToString();
			this.CurrentWageLimitText = new TextObject("{=sWWxrafa}Current Limit", null).ToString();
			this.TitleText = new TextObject("{=qdoJOH0j}Party Wage", null).ToString();
			this.UnlimitedWageText = new TextObject("{=WySAapWO}Unlimited Wage", null).ToString();
			this.WageLimitHint = new HintViewModel(new TextObject("{=w0slxNAl}If limit is lower than current wage, party will not recruit troops until wage is reduced to the limit. If limit is higher than current wage, party will keep recruiting.", null), null);
			this.UpdateCurrentWageLimitText();
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x00059BBC File Offset: 0x00057DBC
		private void OnCurrentWageLimitUpdated(int newValue)
		{
			if (!this.IsUnlimitedWage)
			{
				this._mobileParty.SetWagePaymentLimit(newValue);
			}
			this.UpdateCurrentWageLimitText();
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x00059BD8 File Offset: 0x00057DD8
		private void OnUnlimitedWageToggled(bool newValue)
		{
			this.CurrentWageLimit = 2000;
			if (newValue)
			{
				this._mobileParty.SetWagePaymentLimit(Campaign.Current.Models.PartyWageModel.MaxWage);
			}
			else
			{
				this._mobileParty.SetWagePaymentLimit(2000);
			}
			this.UpdateCurrentWageLimitText();
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x00059C2C File Offset: 0x00057E2C
		private void UpdateCurrentWageLimitText()
		{
			this.CurrentWageLimitValueText = (this.IsUnlimitedWage ? new TextObject("{=lC5xsoSh}Unlimited", null).ToString() : this.CurrentWageLimit.ToString());
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x0600185E RID: 6238 RVA: 0x00059C67 File Offset: 0x00057E67
		// (set) Token: 0x0600185F RID: 6239 RVA: 0x00059C6F File Offset: 0x00057E6F
		[DataSourceProperty]
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				if (value != this._isEnabled)
				{
					this._isEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsEnabled");
				}
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06001860 RID: 6240 RVA: 0x00059C8D File Offset: 0x00057E8D
		// (set) Token: 0x06001861 RID: 6241 RVA: 0x00059C95 File Offset: 0x00057E95
		[DataSourceProperty]
		public HintViewModel WageLimitHint
		{
			get
			{
				return this._wageLimitHint;
			}
			set
			{
				if (value != this._wageLimitHint)
				{
					this._wageLimitHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "WageLimitHint");
				}
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06001862 RID: 6242 RVA: 0x00059CB3 File Offset: 0x00057EB3
		// (set) Token: 0x06001863 RID: 6243 RVA: 0x00059CBB File Offset: 0x00057EBB
		[DataSourceProperty]
		public BasicTooltipViewModel CurrentWageTooltip
		{
			get
			{
				return this._currentWageTooltip;
			}
			set
			{
				if (value != this._currentWageTooltip)
				{
					this._currentWageTooltip = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "CurrentWageTooltip");
				}
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06001864 RID: 6244 RVA: 0x00059CD9 File Offset: 0x00057ED9
		// (set) Token: 0x06001865 RID: 6245 RVA: 0x00059CE1 File Offset: 0x00057EE1
		[DataSourceProperty]
		public string CurrentWageText
		{
			get
			{
				return this._currentWageText;
			}
			set
			{
				if (value != this._currentWageText)
				{
					this._currentWageText = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentWageText");
				}
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06001866 RID: 6246 RVA: 0x00059D04 File Offset: 0x00057F04
		// (set) Token: 0x06001867 RID: 6247 RVA: 0x00059D0C File Offset: 0x00057F0C
		[DataSourceProperty]
		public string CurrentWageLimitText
		{
			get
			{
				return this._currentWageLimitText;
			}
			set
			{
				if (value != this._currentWageLimitText)
				{
					this._currentWageLimitText = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentWageLimitText");
				}
			}
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06001868 RID: 6248 RVA: 0x00059D2F File Offset: 0x00057F2F
		// (set) Token: 0x06001869 RID: 6249 RVA: 0x00059D37 File Offset: 0x00057F37
		[DataSourceProperty]
		public string CurrentWageValueText
		{
			get
			{
				return this._currentWageValueText;
			}
			set
			{
				if (value != this._currentWageValueText)
				{
					this._currentWageValueText = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentWageValueText");
				}
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x0600186A RID: 6250 RVA: 0x00059D5A File Offset: 0x00057F5A
		// (set) Token: 0x0600186B RID: 6251 RVA: 0x00059D62 File Offset: 0x00057F62
		[DataSourceProperty]
		public string CurrentWageLimitValueText
		{
			get
			{
				return this._currentWageLimitValueText;
			}
			set
			{
				if (value != this._currentWageLimitValueText)
				{
					this._currentWageLimitValueText = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentWageLimitValueText");
				}
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x0600186C RID: 6252 RVA: 0x00059D85 File Offset: 0x00057F85
		// (set) Token: 0x0600186D RID: 6253 RVA: 0x00059D8D File Offset: 0x00057F8D
		[DataSourceProperty]
		public string UnlimitedWageText
		{
			get
			{
				return this._unlimitedWageText;
			}
			set
			{
				if (value != this._unlimitedWageText)
				{
					this._unlimitedWageText = value;
					base.OnPropertyChangedWithValue<string>(value, "UnlimitedWageText");
				}
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x0600186E RID: 6254 RVA: 0x00059DB0 File Offset: 0x00057FB0
		// (set) Token: 0x0600186F RID: 6255 RVA: 0x00059DB8 File Offset: 0x00057FB8
		[DataSourceProperty]
		public string TitleText
		{
			get
			{
				return this._titleText;
			}
			set
			{
				if (value != this._titleText)
				{
					this._titleText = value;
					base.OnPropertyChangedWithValue<string>(value, "TitleText");
				}
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06001870 RID: 6256 RVA: 0x00059DDB File Offset: 0x00057FDB
		// (set) Token: 0x06001871 RID: 6257 RVA: 0x00059DE3 File Offset: 0x00057FE3
		[DataSourceProperty]
		public int CurrentWage
		{
			get
			{
				return this._currentWage;
			}
			set
			{
				if (value != this._currentWage)
				{
					this._currentWage = value;
					base.OnPropertyChangedWithValue(value, "CurrentWage");
				}
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06001872 RID: 6258 RVA: 0x00059E01 File Offset: 0x00058001
		// (set) Token: 0x06001873 RID: 6259 RVA: 0x00059E09 File Offset: 0x00058009
		[DataSourceProperty]
		public int CurrentWageLimit
		{
			get
			{
				return this._currentWageLimit;
			}
			set
			{
				if (value != this._currentWageLimit)
				{
					this._currentWageLimit = value;
					base.OnPropertyChangedWithValue(value, "CurrentWageLimit");
					this.OnCurrentWageLimitUpdated(value);
				}
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06001874 RID: 6260 RVA: 0x00059E2E File Offset: 0x0005802E
		// (set) Token: 0x06001875 RID: 6261 RVA: 0x00059E36 File Offset: 0x00058036
		[DataSourceProperty]
		public int MinWage
		{
			get
			{
				return this._minWage;
			}
			set
			{
				if (value != this._minWage)
				{
					this._minWage = value;
					base.OnPropertyChangedWithValue(value, "MinWage");
				}
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06001876 RID: 6262 RVA: 0x00059E54 File Offset: 0x00058054
		// (set) Token: 0x06001877 RID: 6263 RVA: 0x00059E5C File Offset: 0x0005805C
		[DataSourceProperty]
		public int MaxWage
		{
			get
			{
				return this._maxWage;
			}
			set
			{
				if (value != this._maxWage)
				{
					this._maxWage = value;
					base.OnPropertyChangedWithValue(value, "MaxWage");
				}
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06001878 RID: 6264 RVA: 0x00059E7A File Offset: 0x0005807A
		// (set) Token: 0x06001879 RID: 6265 RVA: 0x00059E82 File Offset: 0x00058082
		[DataSourceProperty]
		public bool IsUnlimitedWage
		{
			get
			{
				return this._isUnlimitedWage;
			}
			set
			{
				if (value != this._isUnlimitedWage)
				{
					this._isUnlimitedWage = value;
					base.OnPropertyChangedWithValue(value, "IsUnlimitedWage");
					this.OnUnlimitedWageToggled(value);
				}
			}
		}

		// Token: 0x04000B76 RID: 2934
		private const int UIWageSliderMaxLimit = 2000;

		// Token: 0x04000B77 RID: 2935
		private const int UIWageSliderMinLimit = 100;

		// Token: 0x04000B78 RID: 2936
		private readonly MobileParty _mobileParty;

		// Token: 0x04000B79 RID: 2937
		private bool _isEnabled;

		// Token: 0x04000B7A RID: 2938
		private int _minWage;

		// Token: 0x04000B7B RID: 2939
		private int _maxWage;

		// Token: 0x04000B7C RID: 2940
		private int _currentWage;

		// Token: 0x04000B7D RID: 2941
		private int _currentWageLimit;

		// Token: 0x04000B7E RID: 2942
		private string _currentWageText;

		// Token: 0x04000B7F RID: 2943
		private string _currentWageLimitText;

		// Token: 0x04000B80 RID: 2944
		private string _currentWageValueText;

		// Token: 0x04000B81 RID: 2945
		private string _currentWageLimitValueText;

		// Token: 0x04000B82 RID: 2946
		private string _unlimitedWageText;

		// Token: 0x04000B83 RID: 2947
		private string _titleText;

		// Token: 0x04000B84 RID: 2948
		private bool _isUnlimitedWage;

		// Token: 0x04000B85 RID: 2949
		private HintViewModel _wageLimitHint;

		// Token: 0x04000B86 RID: 2950
		private BasicTooltipViewModel _currentWageTooltip;
	}
}
