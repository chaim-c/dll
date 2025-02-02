using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Bannerlord.ButterLib.Common.Helpers;
using MCM.Abstractions;
using MCM.Common;
using MCM.UI.Actions;
using MCM.UI.Dropdown;
using MCM.UI.HotKeys;
using MCM.UI.Utils;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace MCM.UI.GUI.ViewModels
{
	// Token: 0x02000020 RID: 32
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class SettingsPropertyVM : ViewModel
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00006443 File Offset: 0x00004643
		[DataSourceProperty]
		public bool IsBool
		{
			get
			{
				return this.SettingType == SettingType.Bool;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00006450 File Offset: 0x00004650
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00006488 File Offset: 0x00004688
		[DataSourceProperty]
		public bool BoolValue
		{
			get
			{
				if (this.IsBool)
				{
					object value = this.PropertyReference.Value;
					if (value is bool)
					{
						return (bool)value;
					}
				}
				return false;
			}
			set
			{
				bool flag = this.IsBool && this.BoolValue != value;
				if (flag)
				{
					this.URS.Do(new SetValueTypeAction<bool>(this.PropertyReference, value));
					base.OnPropertyChanged("BoolValue");
				}
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000115 RID: 277 RVA: 0x000064D7 File Offset: 0x000046D7
		[DataSourceProperty]
		public bool IsButton
		{
			get
			{
				return this.SettingType == SettingType.Button;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000116 RID: 278 RVA: 0x000064E2 File Offset: 0x000046E2
		// (set) Token: 0x06000117 RID: 279 RVA: 0x000064EA File Offset: 0x000046EA
		[DataSourceProperty]
		public string ButtonContent
		{
			get
			{
				return this._buttonContent;
			}
			set
			{
				base.SetField<string>(ref this._buttonContent, value, "ButtonContent");
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000064FF File Offset: 0x000046FF
		public ModOptionsVM MainView
		{
			get
			{
				return this.SettingsVM.MainView;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000119 RID: 281 RVA: 0x0000650C File Offset: 0x0000470C
		public SettingsVM SettingsVM { get; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00006514 File Offset: 0x00004714
		// (set) Token: 0x0600011B RID: 283 RVA: 0x0000651C File Offset: 0x0000471C
		[Nullable(2)]
		public SettingsPropertyGroupVM Group { [NullableContext(2)] get; [NullableContext(2)] set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00006525 File Offset: 0x00004725
		public UndoRedoStack URS
		{
			get
			{
				return this.SettingsVM.URS;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00006532 File Offset: 0x00004732
		public ISettingsPropertyDefinition SettingPropertyDefinition { get; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600011E RID: 286 RVA: 0x0000653A File Offset: 0x0000473A
		public IRef PropertyReference
		{
			get
			{
				return this.SettingPropertyDefinition.PropertyReference;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00006547 File Offset: 0x00004747
		public SettingType SettingType
		{
			get
			{
				return this.SettingPropertyDefinition.SettingType;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00006554 File Offset: 0x00004754
		public string HintText
		{
			get
			{
				return (this.SettingPropertyDefinition.HintText.Length > 0) ? string.Format("{0}: {1}", this.Name, new TextObject(this.SettingPropertyDefinition.HintText, null)) : string.Empty;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00006591 File Offset: 0x00004791
		public string ValueFormat
		{
			get
			{
				return this.SettingPropertyDefinition.ValueFormat;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000122 RID: 290 RVA: 0x0000659E File Offset: 0x0000479E
		[Nullable(2)]
		public IFormatProvider ValueFormatProvider { [NullableContext(2)] get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000123 RID: 291 RVA: 0x000065A6 File Offset: 0x000047A6
		public bool SatisfiesSearch
		{
			get
			{
				return string.IsNullOrEmpty(this.MainView.SearchText) || this.Name.IndexOf(this.MainView.SearchText, StringComparison.InvariantCultureIgnoreCase) >= 0;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000124 RID: 292 RVA: 0x000065DA File Offset: 0x000047DA
		// (set) Token: 0x06000125 RID: 293 RVA: 0x000065E2 File Offset: 0x000047E2
		[DataSourceProperty]
		public string Name
		{
			get
			{
				return this._name;
			}
			private set
			{
				base.SetField<string>(ref this._name, value, "Name");
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000126 RID: 294 RVA: 0x000065F7 File Offset: 0x000047F7
		[DataSourceProperty]
		public bool IsEnabled
		{
			get
			{
				SettingsPropertyGroupVM group = this.Group;
				return group != null && group.GroupToggle;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000660C File Offset: 0x0000480C
		[DataSourceProperty]
		public bool IsSettingVisible
		{
			get
			{
				bool isToggle = this.SettingPropertyDefinition.IsToggle;
				bool result;
				if (isToggle)
				{
					result = false;
				}
				else
				{
					SettingsPropertyGroupVM group = this.Group;
					bool flag = group != null && !group.GroupToggle;
					if (flag)
					{
						result = false;
					}
					else
					{
						SettingsPropertyGroupVM group2 = this.Group;
						bool flag2 = group2 != null && !group2.IsExpanded;
						if (flag2)
						{
							result = false;
						}
						else
						{
							bool flag3 = !this.SatisfiesSearch;
							result = !flag3;
						}
					}
				}
				return result;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000667F File Offset: 0x0000487F
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00006687 File Offset: 0x00004887
		public bool IsSelected { get; set; }

		// Token: 0x0600012A RID: 298 RVA: 0x00006690 File Offset: 0x00004890
		public SettingsPropertyVM(ISettingsPropertyDefinition definition, SettingsVM settingsVM)
		{
			this.SettingsVM = settingsVM;
			this.SettingPropertyDefinition = definition;
			this.ValueFormatProvider = SettingsPropertyVM.<.ctor>g__TryCreateCustomFormatter|50_0(this.SettingPropertyDefinition.CustomFormatter);
			this.NumericValueToggle = (this.IsInt || this.IsFloat);
			this.PropertyReference.PropertyChanged += this.OnPropertyChanged;
			bool isDropdown = this.IsDropdown;
			if (isDropdown)
			{
				INotifyPropertyChanged notifyPropertyChanged = this.PropertyReference.Value as INotifyPropertyChanged;
				bool flag = notifyPropertyChanged != null;
				if (flag)
				{
					notifyPropertyChanged.PropertyChanged += this.OnPropertyChanged;
				}
				this.DropdownValue.PropertyChanged += this.DropdownValue_PropertyChanged;
				this.DropdownValue.PropertyChangedWithValue += this.DropdownValue_PropertyChangedWithValue;
			}
			INotifyPropertyChanged notifyPropertyChanged2 = this.SettingsVM.SettingsInstance;
			bool flag2 = notifyPropertyChanged2 != null;
			if (flag2)
			{
				notifyPropertyChanged2.PropertyChanged += this.OnPropertyChanged;
			}
			this.RefreshValues();
			ResetValueToDefault key = MCMUISubModule.ResetValueToDefault;
			bool flag3 = key != null;
			if (flag3)
			{
				key.IsDownAndReleasedEvent += this.ResetValueToDefaultOnReleasedEvent;
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000067D0 File Offset: 0x000049D0
		public override void OnFinalize()
		{
			ResetValueToDefault key = MCMUISubModule.ResetValueToDefault;
			bool flag = key != null;
			if (flag)
			{
				key.IsDownAndReleasedEvent -= this.ResetValueToDefaultOnReleasedEvent;
			}
			this.PropertyReference.PropertyChanged -= this.OnPropertyChanged;
			bool isDropdown = this.IsDropdown;
			if (isDropdown)
			{
				INotifyPropertyChanged notifyPropertyChanged = this.PropertyReference.Value as INotifyPropertyChanged;
				bool flag2 = notifyPropertyChanged != null;
				if (flag2)
				{
					notifyPropertyChanged.PropertyChanged -= this.OnPropertyChanged;
				}
				this.DropdownValue.PropertyChanged -= this.DropdownValue_PropertyChanged;
				this.DropdownValue.PropertyChangedWithValue -= this.DropdownValue_PropertyChangedWithValue;
			}
			INotifyPropertyChanged notifyPropertyChanged2 = this.SettingsVM.SettingsInstance;
			bool flag3 = notifyPropertyChanged2 != null;
			if (flag3)
			{
				notifyPropertyChanged2.PropertyChanged -= this.OnPropertyChanged;
			}
			base.OnFinalize();
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000068B4 File Offset: 0x00004AB4
		private void OnPropertyChanged([Nullable(2)] object obj, PropertyChangedEventArgs args)
		{
			bool flag = args.PropertyName == "SAVE_TRIGGERED";
			if (!flag)
			{
				switch (this.SettingType)
				{
				case SettingType.Bool:
					base.OnPropertyChanged("BoolValue");
					break;
				case SettingType.Int:
					base.OnPropertyChanged("IntValue");
					base.OnPropertyChanged("NumericValue");
					break;
				case SettingType.Float:
					base.OnPropertyChanged("FloatValue");
					base.OnPropertyChanged("NumericValue");
					break;
				case SettingType.String:
					base.OnPropertyChanged("StringValue");
					break;
				case SettingType.Dropdown:
					this.DropdownValue.SelectedIndex = new SelectedIndexWrapper(this.PropertyReference.Value).SelectedIndex;
					break;
				case SettingType.Button:
					this.ButtonContent = new TextObject(this.SettingPropertyDefinition.Content, null).ToString();
					break;
				}
				this.SettingsVM.RecalculatePresetIndex();
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000069AC File Offset: 0x00004BAC
		private void ResetValueToDefaultOnReleasedEvent()
		{
			bool isSelected = this.IsSelected;
			if (isSelected)
			{
				this.SettingsVM.ResetSettingsValue(this.SettingPropertyDefinition.Id);
				switch (this.SettingType)
				{
				case SettingType.Bool:
					base.OnPropertyChanged("BoolValue");
					break;
				case SettingType.Int:
					base.OnPropertyChanged("IntValue");
					base.OnPropertyChanged("NumericValue");
					break;
				case SettingType.Float:
					base.OnPropertyChanged("FloatValue");
					base.OnPropertyChanged("NumericValue");
					break;
				case SettingType.String:
					base.OnPropertyChanged("StringValue");
					break;
				case SettingType.Dropdown:
					this.DropdownValue.SelectedIndex = new SelectedIndexWrapper(this.PropertyReference.Value).SelectedIndex;
					break;
				}
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006A84 File Offset: 0x00004C84
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = new TextObject(this.SettingPropertyDefinition.DisplayName, null).ToString();
			this.ButtonContent = new TextObject(this.SettingPropertyDefinition.Content, null).ToString();
			this.DropdownValue.RefreshValues();
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006ADF File Offset: 0x00004CDF
		public void OnHover()
		{
			this.IsSelected = true;
			this.MainView.HintText = this.HintText;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006AFC File Offset: 0x00004CFC
		public void OnHoverEnd()
		{
			this.IsSelected = false;
			this.MainView.HintText = string.Empty;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00006B18 File Offset: 0x00004D18
		public void OnValueClick()
		{
			Action val = this.PropertyReference.Value as Action;
			bool flag = val != null;
			if (flag)
			{
				val();
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00006B48 File Offset: 0x00004D48
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00006B50 File Offset: 0x00004D50
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00006B5D File Offset: 0x00004D5D
		[DataSourceProperty]
		public bool IsDropdown
		{
			get
			{
				return this.IsDropdownDefault || this.IsDropdownCheckbox;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00006B70 File Offset: 0x00004D70
		[DataSourceProperty]
		public bool IsDropdownDefault
		{
			get
			{
				return this.SettingType == SettingType.Dropdown && SettingsUtils.IsForTextDropdown(this.PropertyReference.Value);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00006B8E File Offset: 0x00004D8E
		[DataSourceProperty]
		public bool IsDropdownCheckbox
		{
			get
			{
				return this.SettingType == SettingType.Dropdown && SettingsUtils.IsForCheckboxDropdown(this.PropertyReference.Value);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00006BAC File Offset: 0x00004DAC
		[DataSourceProperty]
		public MCMSelectorVM<MCMSelectorItemVM<TextObject>> DropdownValue
		{
			get
			{
				MCMSelectorVM<MCMSelectorItemVM<TextObject>> result;
				if ((result = this._selectorVMWrapper) == null)
				{
					MCMSelectorVM<MCMSelectorItemVM<TextObject>> selectorVMWrapper;
					if (!this.IsDropdown)
					{
						selectorVMWrapper = MCMSelectorVM<MCMSelectorItemVM<TextObject>>.Empty;
					}
					else
					{
						selectorVMWrapper = new MCMSelectorVM<MCMSelectorItemVM<TextObject>, TextObject>(from x in UISettingsUtils.GetDropdownValues(this.PropertyReference)
						select new TextObject(x.ToString(), null), new SelectedIndexWrapper(this.PropertyReference.Value).SelectedIndex);
					}
					result = (this._selectorVMWrapper = selectorVMWrapper);
				}
				return result;
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006C28 File Offset: 0x00004E28
		private void DropdownValue_PropertyChanged([Nullable(2)] object obj, PropertyChangedEventArgs args)
		{
			bool flag = obj != null && args.PropertyName == "SelectedIndex";
			if (flag)
			{
				this.URS.Do(new SetSelectedIndexAction(this.PropertyReference, obj));
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00006C68 File Offset: 0x00004E68
		private void DropdownValue_PropertyChangedWithValue(object obj, PropertyChangedWithValueEventArgs args)
		{
			bool flag = args.PropertyName == "SelectedIndex";
			if (flag)
			{
				this.URS.Do(new SetSelectedIndexAction(this.PropertyReference, obj));
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006CA4 File Offset: 0x00004EA4
		private static TextObject SetNumeric(TextObject textObject, int value)
		{
			LocalizationHelper.SetNumericVariable(textObject, "NUMERIC", value, null);
			return textObject;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00006CC8 File Offset: 0x00004EC8
		private static TextObject SetNumeric(TextObject textObject, float value)
		{
			LocalizationHelper.SetNumericVariable(textObject, "NUMERIC", value, null);
			return textObject;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00006CE9 File Offset: 0x00004EE9
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00006CF1 File Offset: 0x00004EF1
		[DataSourceProperty]
		public bool IsIntVisible { get; private set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00006CFA File Offset: 0x00004EFA
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00006D02 File Offset: 0x00004F02
		[DataSourceProperty]
		public bool IsFloatVisible { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00006D0B File Offset: 0x00004F0B
		[DataSourceProperty]
		public bool IsInt
		{
			get
			{
				return this.SettingType == SettingType.Int;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00006D16 File Offset: 0x00004F16
		[DataSourceProperty]
		public bool IsFloat
		{
			get
			{
				return this.SettingType == SettingType.Float;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00006D24 File Offset: 0x00004F24
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00006D68 File Offset: 0x00004F68
		[DataSourceProperty]
		public float FloatValue
		{
			get
			{
				float result;
				if (!this.IsFloat)
				{
					result = 0f;
				}
				else
				{
					object value = this.PropertyReference.Value;
					if (value is float)
					{
						float val = (float)value;
						result = val;
					}
					else
					{
						result = float.MinValue;
					}
				}
				return result;
			}
			set
			{
				value = MathF.Max(MathF.Min(value, this.MaxFloat), this.MinFloat);
				bool flag = this.IsFloat && MathF.Abs(this.FloatValue - value) >= 1E-06f;
				if (flag)
				{
					this.URS.Do(new SetValueTypeAction<float>(this.PropertyReference, value));
					base.OnPropertyChanged("FloatValue");
					base.OnPropertyChanged("NumericValue");
				}
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00006DE8 File Offset: 0x00004FE8
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00006E28 File Offset: 0x00005028
		[DataSourceProperty]
		public int IntValue
		{
			get
			{
				int result;
				if (!this.IsInt)
				{
					result = 0;
				}
				else
				{
					object value = this.PropertyReference.Value;
					if (value is int)
					{
						int val = (int)value;
						result = val;
					}
					else
					{
						result = int.MinValue;
					}
				}
				return result;
			}
			set
			{
				value = MathF.Max(MathF.Min(value, this.MaxInt), this.MinInt);
				bool flag = this.IsInt && this.IntValue != value;
				if (flag)
				{
					this.URS.Do(new SetValueTypeAction<int>(this.PropertyReference, value));
					base.OnPropertyChanged("IntValue");
					base.OnPropertyChanged("NumericValue");
				}
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00006E9C File Offset: 0x0000509C
		[DataSourceProperty]
		public int MaxInt
		{
			get
			{
				return (int)this.SettingPropertyDefinition.MaxValue;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00006EAE File Offset: 0x000050AE
		[DataSourceProperty]
		public int MinInt
		{
			get
			{
				return (int)this.SettingPropertyDefinition.MinValue;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00006EC0 File Offset: 0x000050C0
		[DataSourceProperty]
		public float MaxFloat
		{
			get
			{
				return (float)this.SettingPropertyDefinition.MaxValue;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00006ED3 File Offset: 0x000050D3
		[DataSourceProperty]
		public float MinFloat
		{
			get
			{
				return (float)this.SettingPropertyDefinition.MinValue;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00006EE6 File Offset: 0x000050E6
		[DataSourceProperty]
		public bool IsNotNumeric
		{
			get
			{
				return !this.IsInt && !this.IsFloat;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00006EFC File Offset: 0x000050FC
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00006F04 File Offset: 0x00005104
		[DataSourceProperty]
		public bool NumericValueToggle { get; private set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00006F10 File Offset: 0x00005110
		[DataSourceProperty]
		public string NumericValue
		{
			get
			{
				SettingType settingType = this.SettingType;
				if (!true)
				{
				}
				string result;
				if (settingType != SettingType.Int)
				{
					if (settingType == SettingType.Float)
					{
						object value = this.PropertyReference.Value;
						if (value is float)
						{
							float val = (float)value;
							result = (string.IsNullOrWhiteSpace(this.ValueFormat) ? string.Format(this.ValueFormatProvider, "{0}", val.ToString("0.00")) : string.Format(this.ValueFormatProvider, "{0}", val.ToString(SettingsPropertyVM.SetNumeric(new TextObject(this.ValueFormat, null), val).ToString())));
							goto IL_127;
						}
					}
				}
				else
				{
					object value = this.PropertyReference.Value;
					if (value is int)
					{
						int val2 = (int)value;
						result = (string.IsNullOrWhiteSpace(this.ValueFormat) ? string.Format(this.ValueFormatProvider, "{0}", val2.ToString("0")) : string.Format(this.ValueFormatProvider, "{0}", val2.ToString(SettingsPropertyVM.SetNumeric(new TextObject(this.ValueFormat, null), val2).ToString())));
						goto IL_127;
					}
				}
				result = string.Empty;
				IL_127:
				if (!true)
				{
				}
				return result;
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000704C File Offset: 0x0000524C
		public void OnEditBoxHover()
		{
			SettingType settingType = this.SettingType;
			SettingType settingType2 = settingType;
			if (settingType2 != SettingType.Int)
			{
				if (settingType2 == SettingType.Float)
				{
					this.IsFloatVisible = !this.IsFloatVisible;
					this.NumericValueToggle = !this.NumericValueToggle;
					base.OnPropertyChanged("IsFloatVisible");
					base.OnPropertyChanged("NumericValueToggle");
				}
			}
			else
			{
				this.IsIntVisible = !this.IsIntVisible;
				this.NumericValueToggle = !this.NumericValueToggle;
				base.OnPropertyChanged("IsIntVisible");
				base.OnPropertyChanged("NumericValueToggle");
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000070E4 File Offset: 0x000052E4
		public void OnEditBoxHoverEnd()
		{
			SettingType settingType = this.SettingType;
			SettingType settingType2 = settingType;
			if (settingType2 != SettingType.Int)
			{
				if (settingType2 == SettingType.Float)
				{
					this.IsFloatVisible = !this.IsFloatVisible;
					this.NumericValueToggle = !this.NumericValueToggle;
					base.OnPropertyChanged("IsFloatVisible");
					base.OnPropertyChanged("NumericValueToggle");
				}
			}
			else
			{
				this.IsIntVisible = !this.IsIntVisible;
				this.NumericValueToggle = !this.NumericValueToggle;
				base.OnPropertyChanged("IsIntVisible");
				base.OnPropertyChanged("NumericValueToggle");
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000717B File Offset: 0x0000537B
		[DataSourceProperty]
		public bool IsString
		{
			get
			{
				return this.SettingType == SettingType.String;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00007188 File Offset: 0x00005388
		// (set) Token: 0x06000152 RID: 338 RVA: 0x000071D4 File Offset: 0x000053D4
		[DataSourceProperty]
		public string StringValue
		{
			get
			{
				string result;
				if (!this.IsString)
				{
					result = string.Empty;
				}
				else if (this.PropertyReference.Value != null)
				{
					string val = this.PropertyReference.Value as string;
					result = ((val != null) ? val : "ERROR");
				}
				else
				{
					result = string.Empty;
				}
				return result;
			}
			set
			{
				bool flag = this.IsString && this.StringValue != value;
				if (flag)
				{
					this.URS.Do(new SetStringAction(this.PropertyReference, value));
					base.OnPropertyChanged("StringValue");
				}
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00007224 File Offset: 0x00005424
		[NullableContext(2)]
		[CompilerGenerated]
		internal static IFormatProvider <.ctor>g__TryCreateCustomFormatter|50_0(Type customFormatter)
		{
			bool flag = customFormatter == null;
			IFormatProvider result;
			if (flag)
			{
				result = null;
			}
			else
			{
				try
				{
					result = (Activator.CreateInstance(customFormatter) as IFormatProvider);
				}
				catch (Exception)
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0400004E RID: 78
		private string _buttonContent = string.Empty;

		// Token: 0x0400004F RID: 79
		private string _name = string.Empty;

		// Token: 0x04000055 RID: 85
		[Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		private MCMSelectorVM<MCMSelectorItemVM<TextObject>> _selectorVMWrapper;
	}
}
