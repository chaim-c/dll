﻿using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Core.ViewModelCollection.Selector;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection
{
	// Token: 0x02000005 RID: 5
	public class CampaignOptionItemVM : ViewModel
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002DFA File Offset: 0x00000FFA
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002E02 File Offset: 0x00001002
		public ICampaignOptionData OptionData { get; private set; }

		// Token: 0x06000050 RID: 80 RVA: 0x00002E0C File Offset: 0x0000100C
		public CampaignOptionItemVM(ICampaignOptionData optionData)
		{
			this.OptionData = optionData;
			this.OptionData.GetEnableState();
			this.Hint = new HintViewModel();
			this._dataType = this.OptionData.GetDataType();
			if (this._dataType == CampaignOptionDataType.Boolean)
			{
				this._optionDataAsBoolean = (this.OptionData as BooleanCampaignOptionData);
				this.ValueAsBoolean = (this._optionDataAsBoolean.GetValue() != 0f);
				this.OptionType = 0;
			}
			else if (this._dataType == CampaignOptionDataType.Numeric)
			{
				this._optionDataAsNumeric = (this.OptionData as NumericCampaignOptionData);
				this.OptionType = 1;
				this.MinRange = this._optionDataAsNumeric.MinValue;
				this.MaxRange = this._optionDataAsNumeric.MaxValue;
				this.IsDiscrete = this._optionDataAsNumeric.IsDiscrete;
				this.ValueAsRange = this._optionDataAsNumeric.GetValue();
			}
			else if (this._dataType == CampaignOptionDataType.Selection)
			{
				this._optionDataAsSelection = (this.OptionData as SelectionCampaignOptionData);
				List<TextObject> selections = this._optionDataAsSelection.Selections;
				int selectedIndex = (int)this._optionDataAsSelection.GetValue();
				this.SelectionSelector = new SelectorVM<SelectorItemVM>(selections, selectedIndex, null);
				this.SelectionSelector.SetOnChangeAction(new Action<SelectorVM<SelectorItemVM>>(this.OnSelectionOptionValueChanged));
				this.OptionType = 2;
				this.SelectionSelector.SelectedIndex = (int)this._optionDataAsSelection.GetValue();
			}
			else if (this._dataType == CampaignOptionDataType.Action)
			{
				this._optionDataAsAction = (this.OptionData as ActionCampaignOptionData);
				this.HideOptionName = true;
				this.OptionType = 3;
			}
			this.RefreshValues();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002F9E File Offset: 0x0000119E
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = this.OptionData.GetName();
			this.RefreshDisabledStatus();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002FC0 File Offset: 0x000011C0
		public void RefreshDisabledStatus()
		{
			string description = this.OptionData.GetDescription();
			TextObject textObject = new TextObject("{=!}" + description, null);
			CampaignOptionDisableStatus isDisabledWithReason = this.OptionData.GetIsDisabledWithReason();
			this.IsDisabled = isDisabledWithReason.IsDisabled;
			if (!string.IsNullOrEmpty(isDisabledWithReason.DisabledReason))
			{
				string variable = textObject.ToString();
				string disabledReason = isDisabledWithReason.DisabledReason;
				textObject = GameTexts.FindText("str_string_newline_string", null).CopyTextObject();
				textObject.SetTextVariable("STR1", variable);
				textObject.SetTextVariable("STR2", disabledReason);
			}
			if (this.IsDisabled && isDisabledWithReason.ValueIfDisabled != -1f)
			{
				this.SetValue(isDisabledWithReason.ValueIfDisabled);
			}
			this.Hint.HintText = textObject;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000307D File Offset: 0x0000127D
		public void ExecuteAction()
		{
			ActionCampaignOptionData optionDataAsAction = this._optionDataAsAction;
			if (optionDataAsAction == null)
			{
				return;
			}
			optionDataAsAction.ExecuteAction();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000308F File Offset: 0x0000128F
		public void OnSelectionOptionValueChanged(SelectorVM<SelectorItemVM> selector)
		{
			if (selector.SelectedIndex >= 0 && this._optionDataAsSelection != null)
			{
				this._optionDataAsSelection.SetValue((float)selector.SelectedIndex);
				Action<CampaignOptionItemVM> onValueChanged = this._onValueChanged;
				if (onValueChanged == null)
				{
					return;
				}
				onValueChanged(this);
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000030C8 File Offset: 0x000012C8
		public void SetValue(float value)
		{
			if (this._dataType == CampaignOptionDataType.Boolean)
			{
				this.ValueAsBoolean = (value != 0f);
			}
			else if (this._dataType == CampaignOptionDataType.Numeric)
			{
				this.ValueAsRange = value;
			}
			else if (this._dataType == CampaignOptionDataType.Selection)
			{
				this.SelectionSelector.SelectedIndex = (int)value;
			}
			this.OptionData.SetValue(value);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003124 File Offset: 0x00001324
		public void SetOnValueChangedCallback(Action<CampaignOptionItemVM> onValueChanged)
		{
			this._onValueChanged = onValueChanged;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000057 RID: 87 RVA: 0x0000312D File Offset: 0x0000132D
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00003135 File Offset: 0x00001335
		[DataSourceProperty]
		public bool HideOptionName
		{
			get
			{
				return this._hideOptionName;
			}
			set
			{
				if (value != this._hideOptionName)
				{
					this._hideOptionName = value;
					base.OnPropertyChangedWithValue(value, "HideOptionName");
				}
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00003153 File Offset: 0x00001353
		// (set) Token: 0x0600005A RID: 90 RVA: 0x0000315B File Offset: 0x0000135B
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

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000317E File Offset: 0x0000137E
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00003186 File Offset: 0x00001386
		[DataSourceProperty]
		public HintViewModel Hint
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
					base.OnPropertyChangedWithValue<HintViewModel>(value, "Hint");
				}
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000031A4 File Offset: 0x000013A4
		// (set) Token: 0x0600005E RID: 94 RVA: 0x000031AC File Offset: 0x000013AC
		[DataSourceProperty]
		public int OptionType
		{
			get
			{
				return this._optionType;
			}
			set
			{
				if (value != this._optionType)
				{
					this._optionType = value;
					base.OnPropertyChangedWithValue(value, "OptionType");
				}
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000031CA File Offset: 0x000013CA
		// (set) Token: 0x06000060 RID: 96 RVA: 0x000031D4 File Offset: 0x000013D4
		[DataSourceProperty]
		public bool ValueAsBoolean
		{
			get
			{
				return this._valueAsBoolean;
			}
			set
			{
				if (value != this._valueAsBoolean)
				{
					this._valueAsBoolean = value;
					base.OnPropertyChangedWithValue(value, "ValueAsBoolean");
					Action<CampaignOptionItemVM> onValueChanged = this._onValueChanged;
					if (onValueChanged != null)
					{
						onValueChanged(this);
					}
					this._optionDataAsBoolean.SetValue(value ? 1f : 0f);
				}
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003229 File Offset: 0x00001429
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00003231 File Offset: 0x00001431
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

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000063 RID: 99 RVA: 0x0000324F File Offset: 0x0000144F
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00003257 File Offset: 0x00001457
		[DataSourceProperty]
		public bool IsDisabled
		{
			get
			{
				return this._isDisabled;
			}
			set
			{
				if (value != this._isDisabled)
				{
					this._isDisabled = value;
					base.OnPropertyChangedWithValue(value, "IsDisabled");
				}
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003275 File Offset: 0x00001475
		// (set) Token: 0x06000066 RID: 102 RVA: 0x0000327D File Offset: 0x0000147D
		[DataSourceProperty]
		public float MinRange
		{
			get
			{
				return this._minRange;
			}
			set
			{
				if (value != this._minRange)
				{
					this._minRange = value;
					base.OnPropertyChangedWithValue(value, "MinRange");
				}
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000067 RID: 103 RVA: 0x0000329B File Offset: 0x0000149B
		// (set) Token: 0x06000068 RID: 104 RVA: 0x000032A3 File Offset: 0x000014A3
		[DataSourceProperty]
		public float MaxRange
		{
			get
			{
				return this._maxRange;
			}
			set
			{
				if (value != this._maxRange)
				{
					this._maxRange = value;
					base.OnPropertyChangedWithValue(value, "MaxRange");
				}
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000032C1 File Offset: 0x000014C1
		// (set) Token: 0x0600006A RID: 106 RVA: 0x000032CC File Offset: 0x000014CC
		[DataSourceProperty]
		public float ValueAsRange
		{
			get
			{
				return this._valueAsRange;
			}
			set
			{
				if (value != this._valueAsRange)
				{
					this._valueAsRange = value;
					base.OnPropertyChangedWithValue(value, "ValueAsRange");
					this.ValueAsString = value.ToString("F1");
					Action<CampaignOptionItemVM> onValueChanged = this._onValueChanged;
					if (onValueChanged != null)
					{
						onValueChanged(this);
					}
					this._optionDataAsNumeric.SetValue(value);
				}
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003325 File Offset: 0x00001525
		// (set) Token: 0x0600006C RID: 108 RVA: 0x0000332D File Offset: 0x0000152D
		[DataSourceProperty]
		public string ValueAsString
		{
			get
			{
				return this._valueAsString;
			}
			set
			{
				if (value != this._valueAsString)
				{
					this._valueAsString = value;
					base.OnPropertyChangedWithValue<string>(value, "ValueAsString");
				}
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003350 File Offset: 0x00001550
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00003358 File Offset: 0x00001558
		[DataSourceProperty]
		public SelectorVM<SelectorItemVM> SelectionSelector
		{
			get
			{
				return this._selectionSelector;
			}
			set
			{
				if (value != this._selectionSelector)
				{
					this._selectionSelector = value;
					base.OnPropertyChangedWithValue<SelectorVM<SelectorItemVM>>(value, "SelectionSelector");
				}
			}
		}

		// Token: 0x0400002C RID: 44
		private ActionCampaignOptionData _optionDataAsAction;

		// Token: 0x0400002D RID: 45
		private BooleanCampaignOptionData _optionDataAsBoolean;

		// Token: 0x0400002E RID: 46
		private NumericCampaignOptionData _optionDataAsNumeric;

		// Token: 0x0400002F RID: 47
		private SelectionCampaignOptionData _optionDataAsSelection;

		// Token: 0x04000030 RID: 48
		private Action<CampaignOptionItemVM> _onValueChanged;

		// Token: 0x04000031 RID: 49
		private CampaignOptionDataType _dataType;

		// Token: 0x04000032 RID: 50
		private bool _hideOptionName;

		// Token: 0x04000033 RID: 51
		private int _optionType;

		// Token: 0x04000034 RID: 52
		private string _name;

		// Token: 0x04000035 RID: 53
		private HintViewModel _hint;

		// Token: 0x04000036 RID: 54
		private bool _isDiscrete;

		// Token: 0x04000037 RID: 55
		private bool _isDisabled;

		// Token: 0x04000038 RID: 56
		private float _minRange;

		// Token: 0x04000039 RID: 57
		private float _maxRange;

		// Token: 0x0400003A RID: 58
		private bool _valueAsBoolean;

		// Token: 0x0400003B RID: 59
		private float _valueAsRange;

		// Token: 0x0400003C RID: 60
		private string _valueAsString;

		// Token: 0x0400003D RID: 61
		private SelectorVM<SelectorItemVM> _selectionSelector;
	}
}
