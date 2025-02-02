using System;
using TaleWorlds.Engine.Options;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.ViewModelCollection.Input;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions
{
	// Token: 0x0200005B RID: 91
	public class BrightnessOptionVM : ViewModel
	{
		// Token: 0x06000705 RID: 1797 RVA: 0x0001B899 File Offset: 0x00019A99
		public BrightnessOptionVM(Action<bool> onClose = null)
		{
			this._onClose = onClose;
			this.RefreshOptionValues();
			this.RefreshValues();
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001B8B4 File Offset: 0x00019AB4
		private void RefreshOptionValues()
		{
			this.InitialValue = 50;
			this.InitialValue1 = NativeOptions.GetConfig(NativeOptions.NativeOptionsType.BrightnessMax);
			this.InitialValue2 = NativeOptions.GetConfig(NativeOptions.NativeOptionsType.BrightnessMin);
			if (NativeOptions.GetConfig(NativeOptions.NativeOptionsType.BrightnessCalibrated) < 2f)
			{
				this.Value1 = 0;
				this.Value2 = 0;
				return;
			}
			this.Value1 = MathF.Round((this.InitialValue1 - 1f) / 0.003f) - 2;
			this.Value2 = MathF.Round(this.InitialValue2 / 0.003f) + 2;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001B938 File Offset: 0x00019B38
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.TitleText = Module.CurrentModule.GlobalTextManager.FindText("str_brightness_option_title", null).ToString();
			TextObject textObject = Module.CurrentModule.GlobalTextManager.FindText("str_brightness_option_explainer", null);
			textObject.SetTextVariable("newline", "\n");
			this.ExplanationText = textObject.ToString();
			this.CancelText = new TextObject("{=3CpNUnVl}Cancel", null).ToString();
			this.AcceptText = new TextObject("{=Y94H6XnK}Accept", null).ToString();
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001B9CC File Offset: 0x00019BCC
		public void ExecuteConfirm()
		{
			this.InitialValue = this.Value;
			NativeOptions.SetConfig(NativeOptions.NativeOptionsType.Brightness, (float)this.Value);
			float num = (float)(this.Value1 + 2) * 0.003f + 1f;
			float num2 = (float)(this.Value2 - 2) * 0.003f;
			this.InitialValue1 = num;
			this.InitialValue2 = num2;
			NativeOptions.SetConfig(NativeOptions.NativeOptionsType.BrightnessMax, num);
			NativeOptions.SetConfig(NativeOptions.NativeOptionsType.BrightnessMin, num2);
			NativeOptions.SetConfig(NativeOptions.NativeOptionsType.BrightnessCalibrated, 2f);
			Action<bool> onClose = this._onClose;
			if (onClose != null)
			{
				onClose(true);
			}
			this.Visible = false;
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001BA5C File Offset: 0x00019C5C
		public void ExecuteCancel()
		{
			this.Value = this.InitialValue;
			NativeOptions.SetConfig(NativeOptions.NativeOptionsType.Brightness, (float)this.InitialValue);
			NativeOptions.SetConfig(NativeOptions.NativeOptionsType.BrightnessMax, this.InitialValue1);
			NativeOptions.SetConfig(NativeOptions.NativeOptionsType.BrightnessMin, this.InitialValue2);
			this.Visible = false;
			Action<bool> onClose = this._onClose;
			if (onClose == null)
			{
				return;
			}
			onClose(false);
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x0001BAB5 File Offset: 0x00019CB5
		// (set) Token: 0x0600070B RID: 1803 RVA: 0x0001BABD File Offset: 0x00019CBD
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

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x0001BAE0 File Offset: 0x00019CE0
		// (set) Token: 0x0600070D RID: 1805 RVA: 0x0001BAE8 File Offset: 0x00019CE8
		[DataSourceProperty]
		public string ExplanationText
		{
			get
			{
				return this._explanationText;
			}
			set
			{
				if (value != this._explanationText)
				{
					this._explanationText = value;
					base.OnPropertyChangedWithValue<string>(value, "ExplanationText");
				}
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x0001BB0B File Offset: 0x00019D0B
		// (set) Token: 0x0600070F RID: 1807 RVA: 0x0001BB13 File Offset: 0x00019D13
		[DataSourceProperty]
		public string CancelText
		{
			get
			{
				return this._cancelText;
			}
			set
			{
				if (value != this._cancelText)
				{
					this._cancelText = value;
					base.OnPropertyChangedWithValue<string>(value, "CancelText");
				}
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x0001BB36 File Offset: 0x00019D36
		// (set) Token: 0x06000711 RID: 1809 RVA: 0x0001BB3E File Offset: 0x00019D3E
		[DataSourceProperty]
		public string AcceptText
		{
			get
			{
				return this._acceptText;
			}
			set
			{
				if (value != this._acceptText)
				{
					this._acceptText = value;
					base.OnPropertyChangedWithValue<string>(value, "AcceptText");
				}
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x0001BB61 File Offset: 0x00019D61
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x0001BB69 File Offset: 0x00019D69
		public int Value
		{
			get
			{
				return this._value;
			}
			set
			{
				if (this._value != value)
				{
					this._value = value;
					base.OnPropertyChangedWithValue(value, "Value");
				}
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x0001BB87 File Offset: 0x00019D87
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x0001BB8F File Offset: 0x00019D8F
		public int InitialValue
		{
			get
			{
				return this._initialValue;
			}
			set
			{
				if (this._initialValue != value)
				{
					this._initialValue = value;
					base.OnPropertyChangedWithValue(value, "InitialValue");
				}
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x0001BBAD File Offset: 0x00019DAD
		// (set) Token: 0x06000717 RID: 1815 RVA: 0x0001BBB5 File Offset: 0x00019DB5
		public float InitialValue1
		{
			get
			{
				return this._initialValue1;
			}
			set
			{
				if (this._initialValue1 != value)
				{
					this._initialValue1 = value;
					base.OnPropertyChangedWithValue(value, "InitialValue1");
				}
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x0001BBD3 File Offset: 0x00019DD3
		// (set) Token: 0x06000719 RID: 1817 RVA: 0x0001BBDB File Offset: 0x00019DDB
		public float InitialValue2
		{
			get
			{
				return this._initialValue2;
			}
			set
			{
				if (this._initialValue2 != value)
				{
					this._initialValue2 = value;
					base.OnPropertyChangedWithValue(value, "InitialValue2");
				}
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x0001BBF9 File Offset: 0x00019DF9
		// (set) Token: 0x0600071B RID: 1819 RVA: 0x0001BC04 File Offset: 0x00019E04
		public int Value1
		{
			get
			{
				return this._value1;
			}
			set
			{
				if (this._value1 != value)
				{
					float value2 = (float)(value + 2) * 0.003f + 1f;
					NativeOptions.SetConfig(NativeOptions.NativeOptionsType.BrightnessMax, value2);
					this._value1 = value;
					base.OnPropertyChangedWithValue(value, "Value1");
				}
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x0001BC46 File Offset: 0x00019E46
		// (set) Token: 0x0600071D RID: 1821 RVA: 0x0001BC50 File Offset: 0x00019E50
		public int Value2
		{
			get
			{
				return this._value2;
			}
			set
			{
				if (this._value2 != value)
				{
					float value2 = (float)(value - 2) * 0.003f;
					NativeOptions.SetConfig(NativeOptions.NativeOptionsType.BrightnessMin, value2);
					this._value2 = value;
					base.OnPropertyChangedWithValue(value, "Value2");
				}
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x0001BC8C File Offset: 0x00019E8C
		// (set) Token: 0x0600071F RID: 1823 RVA: 0x0001BC94 File Offset: 0x00019E94
		public bool Visible
		{
			get
			{
				return this._visible;
			}
			set
			{
				if (this._visible != value)
				{
					this._visible = value;
					base.OnPropertyChangedWithValue(value, "Visible");
					if (value)
					{
						this.RefreshOptionValues();
					}
				}
			}
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0001BCBB File Offset: 0x00019EBB
		public void SetCancelInputKey(HotKey hotkey)
		{
			this.CancelInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0001BCCA File Offset: 0x00019ECA
		public void SetConfirmInputKey(HotKey hotkey)
		{
			this.ConfirmInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0001BCD9 File Offset: 0x00019ED9
		// (set) Token: 0x06000723 RID: 1827 RVA: 0x0001BCE1 File Offset: 0x00019EE1
		[DataSourceProperty]
		public InputKeyItemVM CancelInputKey
		{
			get
			{
				return this._cancelInputKey;
			}
			set
			{
				if (value != this._cancelInputKey)
				{
					this._cancelInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "CancelInputKey");
				}
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x0001BCFF File Offset: 0x00019EFF
		// (set) Token: 0x06000725 RID: 1829 RVA: 0x0001BD07 File Offset: 0x00019F07
		[DataSourceProperty]
		public InputKeyItemVM ConfirmInputKey
		{
			get
			{
				return this._confirmInputKey;
			}
			set
			{
				if (value != this._confirmInputKey)
				{
					this._confirmInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "ConfirmInputKey");
				}
			}
		}

		// Token: 0x04000353 RID: 851
		private readonly Action<bool> _onClose;

		// Token: 0x04000354 RID: 852
		private string _titleText;

		// Token: 0x04000355 RID: 853
		private string _explanationText;

		// Token: 0x04000356 RID: 854
		private string _cancelText;

		// Token: 0x04000357 RID: 855
		private string _acceptText;

		// Token: 0x04000358 RID: 856
		private int _initialValue;

		// Token: 0x04000359 RID: 857
		private float _initialValue1;

		// Token: 0x0400035A RID: 858
		private float _initialValue2;

		// Token: 0x0400035B RID: 859
		private int _value;

		// Token: 0x0400035C RID: 860
		private int _value1;

		// Token: 0x0400035D RID: 861
		private int _value2;

		// Token: 0x0400035E RID: 862
		private bool _visible;

		// Token: 0x0400035F RID: 863
		private InputKeyItemVM _cancelInputKey;

		// Token: 0x04000360 RID: 864
		private InputKeyItemVM _confirmInputKey;
	}
}
