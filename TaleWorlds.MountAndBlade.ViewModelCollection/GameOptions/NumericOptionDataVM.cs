using System;
using TaleWorlds.Engine.Options;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions
{
	// Token: 0x02000061 RID: 97
	public class NumericOptionDataVM : GenericOptionDataVM
	{
		// Token: 0x0600078D RID: 1933 RVA: 0x0001CA3C File Offset: 0x0001AC3C
		public NumericOptionDataVM(OptionsVM optionsVM, INumericOptionData option, TextObject name, TextObject description) : base(optionsVM, option, name, description, OptionsVM.OptionsDataType.NumericOption)
		{
			this._numericOptionData = option;
			this._initialValue = this._numericOptionData.GetValue(false);
			this.Min = this._numericOptionData.GetMinValue();
			this.Max = this._numericOptionData.GetMaxValue();
			this.IsDiscrete = this._numericOptionData.GetIsDiscrete();
			this.DiscreteIncrementInterval = this._numericOptionData.GetDiscreteIncrementInterval();
			this.UpdateContinuously = this._numericOptionData.GetShouldUpdateContinuously();
			this.OptionValue = this._initialValue;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001CAD0 File Offset: 0x0001ACD0
		private string GetValueAsString()
		{
			string result = this.IsDiscrete ? ((int)this._optionValue).ToString() : this._optionValue.ToString("F");
			if (this._numericOptionData.IsNative() || this._numericOptionData.IsAction())
			{
				return result;
			}
			ManagedOptions.ManagedOptionsType managedOptionsType = (ManagedOptions.ManagedOptionsType)this._numericOptionData.GetOptionType();
			if (managedOptionsType != ManagedOptions.ManagedOptionsType.AutoSaveInterval)
			{
				return result;
			}
			if ((int)this.Min < (int)this._optionValue)
			{
				return result;
			}
			return new TextObject("{=1JlzQIXE}Disabled", null).ToString();
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x0001CB5D File Offset: 0x0001AD5D
		// (set) Token: 0x06000790 RID: 1936 RVA: 0x0001CB65 File Offset: 0x0001AD65
		[DataSourceProperty]
		public int DiscreteIncrementInterval
		{
			get
			{
				return this._discreteIncrementInterval;
			}
			set
			{
				if (value != this._discreteIncrementInterval)
				{
					this._discreteIncrementInterval = value;
					base.OnPropertyChangedWithValue(value, "DiscreteIncrementInterval");
				}
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x0001CB83 File Offset: 0x0001AD83
		// (set) Token: 0x06000792 RID: 1938 RVA: 0x0001CB8B File Offset: 0x0001AD8B
		[DataSourceProperty]
		public float Min
		{
			get
			{
				return this._min;
			}
			set
			{
				if (value != this._min)
				{
					this._min = value;
					base.OnPropertyChangedWithValue(value, "Min");
				}
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x0001CBA9 File Offset: 0x0001ADA9
		// (set) Token: 0x06000794 RID: 1940 RVA: 0x0001CBB1 File Offset: 0x0001ADB1
		[DataSourceProperty]
		public float Max
		{
			get
			{
				return this._max;
			}
			set
			{
				if (value != this._max)
				{
					this._max = value;
					base.OnPropertyChangedWithValue(value, "Max");
				}
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x0001CBCF File Offset: 0x0001ADCF
		// (set) Token: 0x06000796 RID: 1942 RVA: 0x0001CBD7 File Offset: 0x0001ADD7
		[DataSourceProperty]
		public float OptionValue
		{
			get
			{
				return this._optionValue;
			}
			set
			{
				if (value != this._optionValue)
				{
					this._optionValue = value;
					base.OnPropertyChangedWithValue(value, "OptionValue");
					base.OnPropertyChanged("OptionValueAsString");
					this.UpdateValue();
				}
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x0001CC06 File Offset: 0x0001AE06
		// (set) Token: 0x06000798 RID: 1944 RVA: 0x0001CC0E File Offset: 0x0001AE0E
		[DataSourceProperty]
		public bool IsDiscrete
		{
			get
			{
				return this._isDiscrete;
			}
			set
			{
				if (value != this._isDiscrete)
				{
					this._isDiscrete = value;
					base.OnPropertyChangedWithValue(value, "IsDiscrete");
				}
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x0001CC2C File Offset: 0x0001AE2C
		// (set) Token: 0x0600079A RID: 1946 RVA: 0x0001CC34 File Offset: 0x0001AE34
		[DataSourceProperty]
		public bool UpdateContinuously
		{
			get
			{
				return this._updateContinuously;
			}
			set
			{
				if (value != this._updateContinuously)
				{
					this._updateContinuously = value;
					base.OnPropertyChangedWithValue(value, "UpdateContinuously");
				}
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x0001CC52 File Offset: 0x0001AE52
		[DataSourceProperty]
		public string OptionValueAsString
		{
			get
			{
				return this.GetValueAsString();
			}
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0001CC5A File Offset: 0x0001AE5A
		public override void UpdateValue()
		{
			this.Option.SetValue(this.OptionValue);
			this.Option.Commit();
			this._optionsVM.SetConfig(this.Option, this.OptionValue);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0001CC8F File Offset: 0x0001AE8F
		public override void Cancel()
		{
			this.OptionValue = this._initialValue;
			this.UpdateValue();
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0001CCA3 File Offset: 0x0001AEA3
		public override void SetValue(float value)
		{
			this.OptionValue = value;
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0001CCAC File Offset: 0x0001AEAC
		public override void ResetData()
		{
			this.OptionValue = this.Option.GetDefaultValue();
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0001CCBF File Offset: 0x0001AEBF
		public override bool IsChanged()
		{
			return this._initialValue != this.OptionValue;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0001CCD2 File Offset: 0x0001AED2
		public override void ApplyValue()
		{
			if (this._initialValue != this.OptionValue)
			{
				this._initialValue = this.OptionValue;
			}
		}

		// Token: 0x0400038B RID: 907
		private float _initialValue;

		// Token: 0x0400038C RID: 908
		private INumericOptionData _numericOptionData;

		// Token: 0x0400038D RID: 909
		private int _discreteIncrementInterval;

		// Token: 0x0400038E RID: 910
		private float _min;

		// Token: 0x0400038F RID: 911
		private float _max;

		// Token: 0x04000390 RID: 912
		private float _optionValue;

		// Token: 0x04000391 RID: 913
		private bool _isDiscrete;

		// Token: 0x04000392 RID: 914
		private bool _updateContinuously;
	}
}
