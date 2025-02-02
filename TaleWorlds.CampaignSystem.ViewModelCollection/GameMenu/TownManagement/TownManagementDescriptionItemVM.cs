using System;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.TownManagement
{
	// Token: 0x02000097 RID: 151
	public class TownManagementDescriptionItemVM : ViewModel
	{
		// Token: 0x06000EB6 RID: 3766 RVA: 0x0003AC98 File Offset: 0x00038E98
		public TownManagementDescriptionItemVM(TextObject title, int value, int valueChange, TownManagementDescriptionItemVM.DescriptionType type, BasicTooltipViewModel hint = null)
		{
			this._titleObj = title;
			this.Value = value;
			this.ValueChange = valueChange;
			this.Type = (int)type;
			this.Hint = (hint ?? new BasicTooltipViewModel());
			this.RefreshValues();
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x0003ACE6 File Offset: 0x00038EE6
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Title = this._titleObj.ToString();
			this.RefreshIsWarning();
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x0003AD08 File Offset: 0x00038F08
		private void RefreshIsWarning()
		{
			int type = this.Type;
			if (type == 1)
			{
				this.IsWarning = (this.Value < 1);
				return;
			}
			if (type == 5)
			{
				this.IsWarning = (this.Value < Campaign.Current.Models.SettlementLoyaltyModel.RebelliousStateStartLoyaltyThreshold);
				return;
			}
			if (type != 7)
			{
				this.IsWarning = false;
				return;
			}
			this.IsWarning = (this.Value < 1);
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x0003AD74 File Offset: 0x00038F74
		// (set) Token: 0x06000EBA RID: 3770 RVA: 0x0003AD7C File Offset: 0x00038F7C
		[DataSourceProperty]
		public int Type
		{
			get
			{
				return this._type;
			}
			set
			{
				if (value != this._type)
				{
					this._type = value;
					base.OnPropertyChangedWithValue(value, "Type");
				}
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x0003AD9A File Offset: 0x00038F9A
		// (set) Token: 0x06000EBC RID: 3772 RVA: 0x0003ADA2 File Offset: 0x00038FA2
		[DataSourceProperty]
		public string Title
		{
			get
			{
				return this._title;
			}
			set
			{
				if (value != this._title)
				{
					this._title = value;
					base.OnPropertyChangedWithValue<string>(value, "Title");
				}
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x0003ADC5 File Offset: 0x00038FC5
		// (set) Token: 0x06000EBE RID: 3774 RVA: 0x0003ADCD File Offset: 0x00038FCD
		[DataSourceProperty]
		public int Value
		{
			get
			{
				return this._value;
			}
			set
			{
				if (value != this._value)
				{
					this._value = value;
					base.OnPropertyChangedWithValue(value, "Value");
				}
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x0003ADEB File Offset: 0x00038FEB
		// (set) Token: 0x06000EC0 RID: 3776 RVA: 0x0003ADF3 File Offset: 0x00038FF3
		[DataSourceProperty]
		public int ValueChange
		{
			get
			{
				return this._valueChange;
			}
			set
			{
				if (value != this._valueChange)
				{
					this._valueChange = value;
					base.OnPropertyChangedWithValue(value, "ValueChange");
				}
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x0003AE11 File Offset: 0x00039011
		// (set) Token: 0x06000EC2 RID: 3778 RVA: 0x0003AE19 File Offset: 0x00039019
		[DataSourceProperty]
		public BasicTooltipViewModel Hint
		{
			get
			{
				return this._hint;
			}
			set
			{
				if (value != this._hint && value != null)
				{
					this._hint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "Hint");
				}
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x0003AE3A File Offset: 0x0003903A
		// (set) Token: 0x06000EC4 RID: 3780 RVA: 0x0003AE42 File Offset: 0x00039042
		[DataSourceProperty]
		public bool IsWarning
		{
			get
			{
				return this._isWarning;
			}
			set
			{
				if (value != this._isWarning)
				{
					this._isWarning = value;
					base.OnPropertyChangedWithValue(value, "IsWarning");
				}
			}
		}

		// Token: 0x040006D5 RID: 1749
		private readonly TextObject _titleObj;

		// Token: 0x040006D6 RID: 1750
		private int _type = -1;

		// Token: 0x040006D7 RID: 1751
		private string _title;

		// Token: 0x040006D8 RID: 1752
		private int _value;

		// Token: 0x040006D9 RID: 1753
		private int _valueChange;

		// Token: 0x040006DA RID: 1754
		private BasicTooltipViewModel _hint;

		// Token: 0x040006DB RID: 1755
		private bool _isWarning;

		// Token: 0x020001DB RID: 475
		public enum DescriptionType
		{
			// Token: 0x0400105A RID: 4186
			Gold,
			// Token: 0x0400105B RID: 4187
			Production,
			// Token: 0x0400105C RID: 4188
			Militia,
			// Token: 0x0400105D RID: 4189
			Prosperity,
			// Token: 0x0400105E RID: 4190
			Food,
			// Token: 0x0400105F RID: 4191
			Loyalty,
			// Token: 0x04001060 RID: 4192
			Security,
			// Token: 0x04001061 RID: 4193
			Garrison
		}
	}
}
