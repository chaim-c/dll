using System;
using TaleWorlds.Engine.Options;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.ViewModelCollection.Input;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions
{
	// Token: 0x0200005C RID: 92
	public class ExposureOptionVM : ViewModel
	{
		// Token: 0x06000726 RID: 1830 RVA: 0x0001BD25 File Offset: 0x00019F25
		public ExposureOptionVM(Action<bool> onClose = null)
		{
			this._onClose = onClose;
			this.InitialValue = 0f;
			this.Value = this.InitialValue;
			this.RefreshValues();
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0001BD54 File Offset: 0x00019F54
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.TitleText = Module.CurrentModule.GlobalTextManager.FindText("str_exposure_option_title", null).ToString();
			TextObject textObject = Module.CurrentModule.GlobalTextManager.FindText("str_exposure_option_explainer", null);
			textObject.SetTextVariable("newline", "\n");
			this.ExplanationText = textObject.ToString();
			this.CancelText = new TextObject("{=3CpNUnVl}Cancel", null).ToString();
			this.AcceptText = new TextObject("{=Y94H6XnK}Accept", null).ToString();
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001BDE6 File Offset: 0x00019FE6
		public void ExecuteConfirm()
		{
			this.InitialValue = this.Value;
			NativeOptions.SetConfig(NativeOptions.NativeOptionsType.ExposureCompensation, this.Value);
			Action<bool> onClose = this._onClose;
			if (onClose != null)
			{
				onClose(true);
			}
			this.Visible = false;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001BE1A File Offset: 0x0001A01A
		public void ExecuteCancel()
		{
			this.Value = this.InitialValue;
			NativeOptions.SetConfig(NativeOptions.NativeOptionsType.ExposureCompensation, this.InitialValue);
			this.Visible = false;
			Action<bool> onClose = this._onClose;
			if (onClose == null)
			{
				return;
			}
			onClose(false);
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x0001BE4D File Offset: 0x0001A04D
		// (set) Token: 0x0600072B RID: 1835 RVA: 0x0001BE55 File Offset: 0x0001A055
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

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x0001BE78 File Offset: 0x0001A078
		// (set) Token: 0x0600072D RID: 1837 RVA: 0x0001BE80 File Offset: 0x0001A080
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

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x0001BEA3 File Offset: 0x0001A0A3
		// (set) Token: 0x0600072F RID: 1839 RVA: 0x0001BEAB File Offset: 0x0001A0AB
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

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x0001BECE File Offset: 0x0001A0CE
		// (set) Token: 0x06000731 RID: 1841 RVA: 0x0001BED6 File Offset: 0x0001A0D6
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

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x0001BEF9 File Offset: 0x0001A0F9
		// (set) Token: 0x06000733 RID: 1843 RVA: 0x0001BF01 File Offset: 0x0001A101
		public float Value
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
					NativeOptions.SetConfig(NativeOptions.NativeOptionsType.ExposureCompensation, this.Value);
				}
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x0001BF2C File Offset: 0x0001A12C
		// (set) Token: 0x06000735 RID: 1845 RVA: 0x0001BF34 File Offset: 0x0001A134
		public float InitialValue
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

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x0001BF52 File Offset: 0x0001A152
		// (set) Token: 0x06000737 RID: 1847 RVA: 0x0001BF5A File Offset: 0x0001A15A
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
						this.Value = NativeOptions.GetConfig(NativeOptions.NativeOptionsType.ExposureCompensation);
					}
				}
			}
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0001BF89 File Offset: 0x0001A189
		public void SetCancelInputKey(HotKey hotkey)
		{
			this.CancelInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0001BF98 File Offset: 0x0001A198
		public void SetConfirmInputKey(HotKey hotkey)
		{
			this.ConfirmInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x0001BFA7 File Offset: 0x0001A1A7
		// (set) Token: 0x0600073B RID: 1851 RVA: 0x0001BFAF File Offset: 0x0001A1AF
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

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x0001BFCD File Offset: 0x0001A1CD
		// (set) Token: 0x0600073D RID: 1853 RVA: 0x0001BFD5 File Offset: 0x0001A1D5
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

		// Token: 0x04000361 RID: 865
		private readonly Action<bool> _onClose;

		// Token: 0x04000362 RID: 866
		private string _titleText;

		// Token: 0x04000363 RID: 867
		private string _explanationText;

		// Token: 0x04000364 RID: 868
		private string _cancelText;

		// Token: 0x04000365 RID: 869
		private string _acceptText;

		// Token: 0x04000366 RID: 870
		private float _initialValue;

		// Token: 0x04000367 RID: 871
		private float _value;

		// Token: 0x04000368 RID: 872
		private bool _visible;

		// Token: 0x04000369 RID: 873
		private InputKeyItemVM _cancelInputKey;

		// Token: 0x0400036A RID: 874
		private InputKeyItemVM _confirmInputKey;
	}
}
