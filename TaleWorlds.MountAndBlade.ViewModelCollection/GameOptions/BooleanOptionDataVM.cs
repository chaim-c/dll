using System;
using TaleWorlds.Engine.Options;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions
{
	// Token: 0x0200005A RID: 90
	public class BooleanOptionDataVM : GenericOptionDataVM
	{
		// Token: 0x060006FC RID: 1788 RVA: 0x0001B770 File Offset: 0x00019970
		public BooleanOptionDataVM(OptionsVM optionsVM, IBooleanOptionData option, TextObject name, TextObject description) : base(optionsVM, option, name, description, OptionsVM.OptionsDataType.BooleanOption)
		{
			this._booleanOptionData = option;
			this._initialValue = option.GetValue(false).Equals(1f);
			this.OptionValueAsBoolean = this._initialValue;
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x0001B7B6 File Offset: 0x000199B6
		// (set) Token: 0x060006FE RID: 1790 RVA: 0x0001B7BE File Offset: 0x000199BE
		[DataSourceProperty]
		public bool OptionValueAsBoolean
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
					base.OnPropertyChangedWithValue(value, "OptionValueAsBoolean");
					this.UpdateValue();
				}
			}
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001B7E4 File Offset: 0x000199E4
		public override void UpdateValue()
		{
			this.Option.SetValue((float)(this.OptionValueAsBoolean ? 1 : 0));
			this.Option.Commit();
			this._optionsVM.SetConfig(this.Option, (float)(this.OptionValueAsBoolean ? 1 : 0));
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0001B832 File Offset: 0x00019A32
		public override void Cancel()
		{
			this.OptionValueAsBoolean = this._initialValue;
			this.UpdateValue();
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0001B846 File Offset: 0x00019A46
		public override void SetValue(float value)
		{
			this.OptionValueAsBoolean = ((int)value == 1);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001B853 File Offset: 0x00019A53
		public override void ResetData()
		{
			this.OptionValueAsBoolean = ((int)this.Option.GetDefaultValue() == 1);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001B86A File Offset: 0x00019A6A
		public override bool IsChanged()
		{
			return this._initialValue != this.OptionValueAsBoolean;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0001B87D File Offset: 0x00019A7D
		public override void ApplyValue()
		{
			if (this._initialValue != this.OptionValueAsBoolean)
			{
				this._initialValue = this.OptionValueAsBoolean;
			}
		}

		// Token: 0x04000350 RID: 848
		private bool _initialValue;

		// Token: 0x04000351 RID: 849
		private readonly IBooleanOptionData _booleanOptionData;

		// Token: 0x04000352 RID: 850
		private bool _optionValue;
	}
}
