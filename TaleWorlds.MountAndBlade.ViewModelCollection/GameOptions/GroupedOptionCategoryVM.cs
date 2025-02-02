using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Engine.Options;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Options;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions
{
	// Token: 0x0200005E RID: 94
	public class GroupedOptionCategoryVM : ViewModel
	{
		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x0001C21A File Offset: 0x0001A41A
		public IEnumerable<GenericOptionDataVM> AllOptions
		{
			get
			{
				return this.BaseOptions.Concat(this.Groups.SelectMany((OptionGroupVM g) => g.Options));
			}
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0001C251 File Offset: 0x0001A451
		public GroupedOptionCategoryVM(OptionsVM options, TextObject name, OptionCategory category, bool isEnabled, bool isResetSupported = false)
		{
			this._category = category;
			this._nameTextObject = name;
			this._options = options;
			this.IsEnabled = isEnabled;
			this.IsResetSupported = isResetSupported;
			this.InitializeOptions();
			this.RefreshValues();
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001C28C File Offset: 0x0001A48C
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = this._nameTextObject.ToString();
			this.BaseOptions.ApplyActionOnAllItems(delegate(GenericOptionDataVM b)
			{
				b.RefreshValues();
			});
			this.Groups.ApplyActionOnAllItems(delegate(OptionGroupVM g)
			{
				g.RefreshValues();
			});
			this.ResetText = new TextObject("{=RVIKFCno}Reset to Defaults", null).ToString();
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0001C31C File Offset: 0x0001A51C
		private void InitializeOptions()
		{
			this.BaseOptions = new MBBindingList<GenericOptionDataVM>();
			this.Groups = new MBBindingList<OptionGroupVM>();
			if (this._category == null)
			{
				return;
			}
			if (this._category.Groups != null)
			{
				foreach (OptionGroup optionGroup in this._category.Groups)
				{
					this.Groups.Add(new OptionGroupVM(optionGroup.GroupName, this._options, optionGroup.Options));
				}
			}
			if (this._category.BaseOptions != null)
			{
				foreach (IOptionData option in this._category.BaseOptions)
				{
					this.BaseOptions.Add(this._options.GetOptionItem(option));
				}
			}
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0001C414 File Offset: 0x0001A614
		internal IEnumerable<IOptionData> GetManagedOptions()
		{
			List<IOptionData> managedOptions = new List<IOptionData>();
			this.Groups.ApplyActionOnAllItems(delegate(OptionGroupVM g)
			{
				managedOptions.AppendList(g.GetManagedOptions());
			});
			return managedOptions.AsReadOnly();
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0001C454 File Offset: 0x0001A654
		internal void InitializeDependentConfigs(Action<IOptionData, float> updateDependentConfigs)
		{
			this.Groups.ApplyActionOnAllItems(delegate(OptionGroupVM g)
			{
				g.InitializeDependentConfigs(updateDependentConfigs);
			});
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0001C488 File Offset: 0x0001A688
		internal bool IsChanged()
		{
			if (!this.BaseOptions.Any((GenericOptionDataVM b) => b.IsChanged()))
			{
				return this.Groups.Any((OptionGroupVM g) => g.IsChanged());
			}
			return true;
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0001C4F0 File Offset: 0x0001A6F0
		internal void Cancel()
		{
			this.BaseOptions.ApplyActionOnAllItems(delegate(GenericOptionDataVM b)
			{
				b.Cancel();
			});
			this.Groups.ApplyActionOnAllItems(delegate(OptionGroupVM g)
			{
				g.Cancel();
			});
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001C554 File Offset: 0x0001A754
		public void ResetData()
		{
			this.BaseOptions.ApplyActionOnAllItems(delegate(GenericOptionDataVM b)
			{
				b.ResetData();
			});
			foreach (OptionGroupVM optionGroupVM in this.Groups)
			{
				optionGroupVM.Options.ApplyActionOnAllItems(delegate(GenericOptionDataVM o)
				{
					o.ResetData();
				});
			}
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001C5EC File Offset: 0x0001A7EC
		public void ExecuteResetToDefault()
		{
			InformationManager.ShowInquiry(new InquiryData(new TextObject("{=oZc8oEAP}Reset this category to default", null).ToString(), new TextObject("{=CCBcdzGa}This will reset ALL options of this category to their default states. You won't be able to undo this action. {newline} {newline}Are you sure?", null).ToString(), true, true, new TextObject("{=aeouhelq}Yes", null).ToString(), new TextObject("{=8OkPHu4f}No", null).ToString(), new Action(this.ResetToDefault), null, "", 0f, null, null, null), false, false);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0001C664 File Offset: 0x0001A864
		private void ResetToDefault()
		{
			this.BaseOptions.ApplyActionOnAllItems(delegate(GenericOptionDataVM b)
			{
				b.ResetToDefault();
			});
			this.Groups.ApplyActionOnAllItems(delegate(OptionGroupVM g)
			{
				g.Options.ApplyActionOnAllItems(delegate(GenericOptionDataVM o)
				{
					o.ResetToDefault();
				});
			});
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0001C6C8 File Offset: 0x0001A8C8
		public GenericOptionDataVM GetOption(ManagedOptions.ManagedOptionsType optionType)
		{
			return this.AllOptions.FirstOrDefault((GenericOptionDataVM o) => !o.IsNative && (ManagedOptions.ManagedOptionsType)o.GetOptionType() == optionType);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0001C6FC File Offset: 0x0001A8FC
		public GenericOptionDataVM GetOption(NativeOptions.NativeOptionsType optionType)
		{
			return this.AllOptions.FirstOrDefault((GenericOptionDataVM o) => o.IsNative && (NativeOptions.NativeOptionsType)o.GetOptionType() == optionType);
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x0001C72D File Offset: 0x0001A92D
		// (set) Token: 0x06000767 RID: 1895 RVA: 0x0001C735 File Offset: 0x0001A935
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

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x0001C753 File Offset: 0x0001A953
		// (set) Token: 0x06000769 RID: 1897 RVA: 0x0001C75B File Offset: 0x0001A95B
		[DataSourceProperty]
		public bool IsResetSupported
		{
			get
			{
				return this._isResetSupported;
			}
			set
			{
				if (value != this._isResetSupported)
				{
					this._isResetSupported = value;
					base.OnPropertyChangedWithValue(value, "IsResetSupported");
				}
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x0001C779 File Offset: 0x0001A979
		// (set) Token: 0x0600076B RID: 1899 RVA: 0x0001C781 File Offset: 0x0001A981
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

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x0001C7A4 File Offset: 0x0001A9A4
		// (set) Token: 0x0600076D RID: 1901 RVA: 0x0001C7AC File Offset: 0x0001A9AC
		[DataSourceProperty]
		public string ResetText
		{
			get
			{
				return this._resetText;
			}
			set
			{
				if (value != this._resetText)
				{
					this._resetText = value;
					base.OnPropertyChangedWithValue<string>(value, "ResetText");
				}
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x0001C7CF File Offset: 0x0001A9CF
		// (set) Token: 0x0600076F RID: 1903 RVA: 0x0001C7D7 File Offset: 0x0001A9D7
		[DataSourceProperty]
		public MBBindingList<OptionGroupVM> Groups
		{
			get
			{
				return this._groups;
			}
			set
			{
				if (value != this._groups)
				{
					this._groups = value;
					base.OnPropertyChangedWithValue<MBBindingList<OptionGroupVM>>(value, "Groups");
				}
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x0001C7F5 File Offset: 0x0001A9F5
		// (set) Token: 0x06000771 RID: 1905 RVA: 0x0001C7FD File Offset: 0x0001A9FD
		[DataSourceProperty]
		public MBBindingList<GenericOptionDataVM> BaseOptions
		{
			get
			{
				return this._baseOptions;
			}
			set
			{
				if (value != this._baseOptions)
				{
					this._baseOptions = value;
					base.OnPropertyChangedWithValue<MBBindingList<GenericOptionDataVM>>(value, "BaseOptions");
				}
			}
		}

		// Token: 0x04000375 RID: 885
		private readonly OptionCategory _category;

		// Token: 0x04000376 RID: 886
		private readonly TextObject _nameTextObject;

		// Token: 0x04000377 RID: 887
		protected readonly OptionsVM _options;

		// Token: 0x04000378 RID: 888
		private bool _isEnabled;

		// Token: 0x04000379 RID: 889
		private bool _isResetSupported;

		// Token: 0x0400037A RID: 890
		private string _name;

		// Token: 0x0400037B RID: 891
		private string _resetText;

		// Token: 0x0400037C RID: 892
		private MBBindingList<GenericOptionDataVM> _baseOptions;

		// Token: 0x0400037D RID: 893
		private MBBindingList<OptionGroupVM> _groups;
	}
}
