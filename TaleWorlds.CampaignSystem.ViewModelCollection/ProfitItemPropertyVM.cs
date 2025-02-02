using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection
{
	// Token: 0x02000019 RID: 25
	public class ProfitItemPropertyVM : ViewModel
	{
		// Token: 0x06000179 RID: 377 RVA: 0x0000B2F6 File Offset: 0x000094F6
		public ProfitItemPropertyVM(string name, int value, ProfitItemPropertyVM.PropertyType type = ProfitItemPropertyVM.PropertyType.None, ImageIdentifierVM governorVisual = null, BasicTooltipViewModel hint = null)
		{
			this.Name = name;
			this.Value = value;
			this.Type = (int)type;
			this.GovernorVisual = governorVisual;
			this.Hint = hint;
			this.RefreshValues();
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000B329 File Offset: 0x00009529
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.ColonText = GameTexts.FindText("str_colon", null).ToString();
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000B347 File Offset: 0x00009547
		// (set) Token: 0x0600017C RID: 380 RVA: 0x0000B34F File Offset: 0x0000954F
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
					this.ShowGovernorPortrait = (this._type == 5);
					base.OnPropertyChangedWithValue(value, "Type");
				}
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000B37C File Offset: 0x0000957C
		// (set) Token: 0x0600017E RID: 382 RVA: 0x0000B384 File Offset: 0x00009584
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

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000B3A7 File Offset: 0x000095A7
		// (set) Token: 0x06000180 RID: 384 RVA: 0x0000B3AF File Offset: 0x000095AF
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
					this.ValueString = this._value.ToString("+0;-#");
					base.OnPropertyChangedWithValue(value, "Value");
				}
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000B3E3 File Offset: 0x000095E3
		// (set) Token: 0x06000182 RID: 386 RVA: 0x0000B3EB File Offset: 0x000095EB
		[DataSourceProperty]
		public string ValueString
		{
			get
			{
				return this._valueString;
			}
			private set
			{
				if (value != this._valueString)
				{
					this._valueString = value;
					base.OnPropertyChangedWithValue<string>(value, "ValueString");
				}
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000B40E File Offset: 0x0000960E
		// (set) Token: 0x06000184 RID: 388 RVA: 0x0000B416 File Offset: 0x00009616
		[DataSourceProperty]
		public BasicTooltipViewModel Hint
		{
			get
			{
				return this._hint;
			}
			set
			{
				if (value != this._hint)
				{
					this._hint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "Hint");
				}
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0000B434 File Offset: 0x00009634
		// (set) Token: 0x06000186 RID: 390 RVA: 0x0000B43C File Offset: 0x0000963C
		[DataSourceProperty]
		public string ColonText
		{
			get
			{
				return this._colonText;
			}
			set
			{
				if (value != this._colonText)
				{
					this._colonText = value;
					base.OnPropertyChangedWithValue<string>(value, "ColonText");
				}
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000B45F File Offset: 0x0000965F
		// (set) Token: 0x06000188 RID: 392 RVA: 0x0000B467 File Offset: 0x00009667
		[DataSourceProperty]
		public ImageIdentifierVM GovernorVisual
		{
			get
			{
				return this._governorVisual;
			}
			set
			{
				if (value != this._governorVisual)
				{
					this._governorVisual = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "GovernorVisual");
				}
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000B485 File Offset: 0x00009685
		// (set) Token: 0x0600018A RID: 394 RVA: 0x0000B48D File Offset: 0x0000968D
		[DataSourceProperty]
		public bool ShowGovernorPortrait
		{
			get
			{
				return this._showGovernorPortrait;
			}
			private set
			{
				if (value != this._showGovernorPortrait)
				{
					this._showGovernorPortrait = value;
					base.OnPropertyChangedWithValue(value, "ShowGovernorPortrait");
				}
			}
		}

		// Token: 0x040000B0 RID: 176
		private int _type;

		// Token: 0x040000B1 RID: 177
		private string _name;

		// Token: 0x040000B2 RID: 178
		private int _value;

		// Token: 0x040000B3 RID: 179
		private string _valueString;

		// Token: 0x040000B4 RID: 180
		private BasicTooltipViewModel _hint;

		// Token: 0x040000B5 RID: 181
		private string _colonText;

		// Token: 0x040000B6 RID: 182
		private ImageIdentifierVM _governorVisual;

		// Token: 0x040000B7 RID: 183
		private bool _showGovernorPortrait;

		// Token: 0x02000158 RID: 344
		public enum PropertyType
		{
			// Token: 0x04000F34 RID: 3892
			None,
			// Token: 0x04000F35 RID: 3893
			Tax,
			// Token: 0x04000F36 RID: 3894
			Tariff,
			// Token: 0x04000F37 RID: 3895
			Garrison,
			// Token: 0x04000F38 RID: 3896
			Village,
			// Token: 0x04000F39 RID: 3897
			Governor
		}
	}
}
