using System;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Engine.Options;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions
{
	// Token: 0x0200005D RID: 93
	public abstract class GenericOptionDataVM : ViewModel
	{
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x0001BFF3 File Offset: 0x0001A1F3
		public bool IsNative
		{
			get
			{
				return this.Option.IsNative();
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x0001C000 File Offset: 0x0001A200
		public bool IsAction
		{
			get
			{
				return this.Option.IsAction();
			}
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0001C010 File Offset: 0x0001A210
		protected GenericOptionDataVM(OptionsVM optionsVM, IOptionData option, TextObject name, TextObject description, OptionsVM.OptionsDataType typeID)
		{
			this._nameObj = name;
			this._descriptionObj = description;
			this._optionsVM = optionsVM;
			this.Option = option;
			this.OptionTypeID = (int)typeID;
			this.Hint = new HintViewModel();
			this.RefreshValues();
			this.UpdateEnableState();
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0001C06D File Offset: 0x0001A26D
		public virtual void UpdateData(bool initUpdate)
		{
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0001C06F File Offset: 0x0001A26F
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = this._nameObj.ToString();
			this.Description = this._descriptionObj.ToString();
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001C099 File Offset: 0x0001A299
		public object GetOptionType()
		{
			return this.Option.GetOptionType();
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001C0A6 File Offset: 0x0001A2A6
		public IOptionData GetOptionData()
		{
			return this.Option;
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001C0AE File Offset: 0x0001A2AE
		public void ResetToDefault()
		{
			this.SetValue(this.Option.GetDefaultValue());
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0001C0C4 File Offset: 0x0001A2C4
		public void UpdateEnableState()
		{
			ValueTuple<string, bool> isDisabledAndReasonID = this.Option.GetIsDisabledAndReasonID();
			if (!string.IsNullOrEmpty(isDisabledAndReasonID.Item1))
			{
				this.Hint.HintText = Module.CurrentModule.GlobalTextManager.FindText(isDisabledAndReasonID.Item1, null);
			}
			else
			{
				this.Hint.HintText = TextObject.Empty;
			}
			this.IsEnabled = !isDisabledAndReasonID.Item2;
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x0001C12C File Offset: 0x0001A32C
		// (set) Token: 0x06000748 RID: 1864 RVA: 0x0001C134 File Offset: 0x0001A334
		[DataSourceProperty]
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				if (value != this._description)
				{
					this._description = value;
					base.OnPropertyChangedWithValue<string>(value, "Description");
				}
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x0001C157 File Offset: 0x0001A357
		// (set) Token: 0x0600074A RID: 1866 RVA: 0x0001C15F File Offset: 0x0001A35F
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

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x0001C182 File Offset: 0x0001A382
		// (set) Token: 0x0600074C RID: 1868 RVA: 0x0001C18A File Offset: 0x0001A38A
		[DataSourceProperty]
		public string[] ImageIDs
		{
			get
			{
				return this._imageIDs;
			}
			set
			{
				if (value != this._imageIDs)
				{
					this._imageIDs = value;
					base.OnPropertyChangedWithValue<string[]>(value, "ImageIDs");
				}
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x0001C1A8 File Offset: 0x0001A3A8
		// (set) Token: 0x0600074E RID: 1870 RVA: 0x0001C1B0 File Offset: 0x0001A3B0
		[DataSourceProperty]
		public int OptionTypeID
		{
			get
			{
				return this._optionTypeId;
			}
			set
			{
				if (value != this._optionTypeId)
				{
					this._optionTypeId = value;
					base.OnPropertyChangedWithValue(value, "OptionTypeID");
				}
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x0001C1CE File Offset: 0x0001A3CE
		// (set) Token: 0x06000750 RID: 1872 RVA: 0x0001C1D6 File Offset: 0x0001A3D6
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

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x0001C1F4 File Offset: 0x0001A3F4
		// (set) Token: 0x06000752 RID: 1874 RVA: 0x0001C1FC File Offset: 0x0001A3FC
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

		// Token: 0x06000753 RID: 1875
		public abstract void UpdateValue();

		// Token: 0x06000754 RID: 1876
		public abstract void Cancel();

		// Token: 0x06000755 RID: 1877
		public abstract bool IsChanged();

		// Token: 0x06000756 RID: 1878
		public abstract void SetValue(float value);

		// Token: 0x06000757 RID: 1879
		public abstract void ResetData();

		// Token: 0x06000758 RID: 1880
		public abstract void ApplyValue();

		// Token: 0x0400036B RID: 875
		private TextObject _nameObj;

		// Token: 0x0400036C RID: 876
		private TextObject _descriptionObj;

		// Token: 0x0400036D RID: 877
		protected OptionsVM _optionsVM;

		// Token: 0x0400036E RID: 878
		protected IOptionData Option;

		// Token: 0x0400036F RID: 879
		private string _description;

		// Token: 0x04000370 RID: 880
		private string _name;

		// Token: 0x04000371 RID: 881
		private int _optionTypeId = -1;

		// Token: 0x04000372 RID: 882
		private string[] _imageIDs;

		// Token: 0x04000373 RID: 883
		private bool _isEnabled = true;

		// Token: 0x04000374 RID: 884
		private HintViewModel _hint;
	}
}
