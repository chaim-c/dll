using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MCM.Abstractions;
using MCM.UI.Extensions;
using MCM.UI.Utils;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace MCM.UI.GUI.ViewModels
{
	// Token: 0x0200001F RID: 31
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class SettingsPropertyGroupVM : ViewModel
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00005C54 File Offset: 0x00003E54
		private ModOptionsVM MainView
		{
			get
			{
				return this.SettingsVM.MainView;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00005C61 File Offset: 0x00003E61
		private SettingsVM SettingsVM { get; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00005C69 File Offset: 0x00003E69
		public SettingsPropertyGroupDefinition SettingPropertyGroupDefinition { get; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00005C71 File Offset: 0x00003E71
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00005C79 File Offset: 0x00003E79
		public string GroupName
		{
			get
			{
				return this._groupName;
			}
			private set
			{
				base.SetField<string>(ref this._groupName, value, "GroupName");
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00005C8E File Offset: 0x00003E8E
		[Nullable(2)]
		public SettingsPropertyGroupVM ParentGroup { [NullableContext(2)] get; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00005C96 File Offset: 0x00003E96
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00005C9E File Offset: 0x00003E9E
		[Nullable(2)]
		public SettingsPropertyVM GroupToggleSettingProperty { [NullableContext(2)] get; [NullableContext(2)] private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00005CA7 File Offset: 0x00003EA7
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00005CAF File Offset: 0x00003EAF
		public string HintText
		{
			get
			{
				return this._hintText;
			}
			private set
			{
				base.SetField<string>(ref this._hintText, value, "HintText");
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00005CC4 File Offset: 0x00003EC4
		public bool SatisfiesSearch
		{
			get
			{
				bool flag = string.IsNullOrEmpty(this.MainView.SearchText);
				return flag || this.GroupName.IndexOf(this.MainView.SearchText, StringComparison.InvariantCultureIgnoreCase) >= 0 || this.AnyChildSettingSatisfiesSearch;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00005D14 File Offset: 0x00003F14
		public bool AnyChildSettingSatisfiesSearch
		{
			get
			{
				bool result;
				if (!this.SettingProperties.Any((SettingsPropertyVM x) => x.SatisfiesSearch))
				{
					result = this.SettingPropertyGroups.Any((SettingsPropertyGroupVM x) => x.SatisfiesSearch);
				}
				else
				{
					result = true;
				}
				return result;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00005D7A File Offset: 0x00003F7A
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00005D82 File Offset: 0x00003F82
		[DataSourceProperty]
		public string GroupNameDisplay
		{
			get
			{
				return this._groupNameDisplay;
			}
			set
			{
				base.SetField<string>(ref this._groupNameDisplay, value, "GroupNameDisplay");
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00005D97 File Offset: 0x00003F97
		[DataSourceProperty]
		public MBBindingList<SettingsPropertyVM> SettingProperties { get; } = new MBBindingList<SettingsPropertyVM>();

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00005D9F File Offset: 0x00003F9F
		[DataSourceProperty]
		public MBBindingList<SettingsPropertyGroupVM> SettingPropertyGroups { get; } = new MBBindingList<SettingsPropertyGroupVM>();

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00005DA7 File Offset: 0x00003FA7
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00005DBC File Offset: 0x00003FBC
		[DataSourceProperty]
		public bool GroupToggle
		{
			get
			{
				SettingsPropertyVM groupToggleSettingProperty = this.GroupToggleSettingProperty;
				return groupToggleSettingProperty == null || groupToggleSettingProperty.BoolValue;
			}
			set
			{
				bool flag = this.GroupToggleSettingProperty != null && this.GroupToggleSettingProperty.BoolValue != value;
				if (flag)
				{
					this.GroupToggleSettingProperty.BoolValue = value;
					base.OnPropertyChanged("GroupToggle");
					base.OnPropertyChanged("IsExpanded");
					this.OnGroupClick();
					this.OnGroupClick();
					base.OnPropertyChanged("GroupNameDisplay");
					foreach (SettingsPropertyVM propSetting in this.SettingProperties)
					{
						propSetting.OnPropertyChanged("IsEnabled");
						propSetting.OnPropertyChanged("IsSettingVisible");
					}
					foreach (SettingsPropertyGroupVM subGroup in this.SettingPropertyGroups)
					{
						subGroup.OnPropertyChanged("IsGroupVisible");
						subGroup.OnPropertyChanged("IsExpanded");
					}
				}
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00005ED8 File Offset: 0x000040D8
		[DataSourceProperty]
		public bool IsGroupVisible
		{
			get
			{
				bool flag = !this.SatisfiesSearch && !this.AnyChildSettingSatisfiesSearch;
				bool result;
				if (flag)
				{
					result = false;
				}
				else
				{
					bool flag2 = this.ParentGroup != null;
					result = (!flag2 || (this.ParentGroup.IsExpanded && this.ParentGroup.GroupToggle));
				}
				return result;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00005F35 File Offset: 0x00004135
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00005F40 File Offset: 0x00004140
		[DataSourceProperty]
		public bool IsExpanded
		{
			get
			{
				return this._isExpanded;
			}
			set
			{
				bool flag = base.SetField<bool>(ref this._isExpanded, value, "IsExpanded");
				if (flag)
				{
					base.OnPropertyChanged("IsGroupVisible");
					foreach (SettingsPropertyGroupVM subGroup in this.SettingPropertyGroups)
					{
						subGroup.OnPropertyChanged("IsGroupVisible");
						subGroup.OnPropertyChanged("IsExpanded");
					}
					foreach (SettingsPropertyVM settingProp in this.SettingProperties)
					{
						settingProp.OnPropertyChanged("IsSettingVisible");
					}
				}
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00006010 File Offset: 0x00004210
		[DataSourceProperty]
		public bool HasGroupToggle
		{
			get
			{
				return this.GroupToggleSettingProperty != null;
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00006020 File Offset: 0x00004220
		public SettingsPropertyGroupVM(SettingsPropertyGroupDefinition definition, SettingsVM settingsVM, [Nullable(2)] SettingsPropertyGroupVM parentGroup = null)
		{
			this.SettingsVM = settingsVM;
			this.SettingPropertyGroupDefinition = definition;
			this.ParentGroup = parentGroup;
			this.AddRange(this.SettingPropertyGroupDefinition.SettingProperties);
			this.SettingPropertyGroups.AddRange(from x in this.SettingPropertyGroupDefinition.SubGroups
			select new SettingsPropertyGroupVM(x, this.SettingsVM, this));
			this.RefreshValues();
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000060CC File Offset: 0x000042CC
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.GroupName = this.SettingPropertyGroupDefinition.GroupName;
			this.HintText = ((this.GroupToggleSettingProperty != null && !string.IsNullOrWhiteSpace(this.GroupToggleSettingProperty.HintText)) ? this.GroupToggleSettingProperty.HintText : string.Empty);
			this.GroupNameDisplay = (this.GroupToggle ? new TextObject(this.GroupName, null).ToString() : new TextObject("{=SettingsPropertyGroupVM_Disabled}{GROUPNAME} (Disabled)", new Dictionary<string, object>
			{
				{
					"GROUPNAME",
					new TextObject(this.GroupName, null).ToString()
				}
			}).ToString());
			this.SettingProperties.Sort(UISettingsUtils.SettingsPropertyVMComparer);
			this.SettingPropertyGroups.Sort(UISettingsUtils.SettingsPropertyGroupVMComparer);
			foreach (SettingsPropertyVM setting in this.SettingProperties)
			{
				setting.RefreshValues();
			}
			foreach (SettingsPropertyGroupVM setting2 in this.SettingPropertyGroups)
			{
				setting2.RefreshValues();
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000621C File Offset: 0x0000441C
		private void AddRange(IEnumerable<ISettingsPropertyDefinition> definitions)
		{
			foreach (ISettingsPropertyDefinition definition in definitions)
			{
				SettingsPropertyVM sp = new SettingsPropertyVM(definition, this.SettingsVM);
				this.SettingProperties.Add(sp);
				sp.Group = this;
				bool isToggle = sp.SettingPropertyDefinition.IsToggle;
				if (isToggle)
				{
					bool hasGroupToggle = this.HasGroupToggle;
					if (hasGroupToggle)
					{
					}
					this.GroupToggleSettingProperty = sp;
				}
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000062AC File Offset: 0x000044AC
		public void NotifySearchChanged()
		{
			foreach (SettingsPropertyGroupVM group in this.SettingPropertyGroups)
			{
				group.NotifySearchChanged();
			}
			foreach (SettingsPropertyVM prop in this.SettingProperties)
			{
				prop.OnPropertyChanged("IsSettingVisible");
			}
			base.OnPropertyChanged("IsGroupVisible");
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000634C File Offset: 0x0000454C
		public void OnHover()
		{
			this.MainView.HintText = this.HintText;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006360 File Offset: 0x00004560
		public void OnHoverEnd()
		{
			this.MainView.HintText = string.Empty;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00006373 File Offset: 0x00004573
		public void OnGroupClick()
		{
			this.IsExpanded = !this.IsExpanded;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00006385 File Offset: 0x00004585
		public override string ToString()
		{
			return this.GroupName;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000638D File Offset: 0x0000458D
		public override int GetHashCode()
		{
			return this.GroupName.GetHashCode();
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000639C File Offset: 0x0000459C
		public override void OnFinalize()
		{
			foreach (SettingsPropertyGroupVM settingPropertyGroup in this.SettingPropertyGroups)
			{
				settingPropertyGroup.OnFinalize();
			}
			foreach (SettingsPropertyVM settingProperty in this.SettingProperties)
			{
				settingProperty.OnFinalize();
			}
			base.OnFinalize();
		}

		// Token: 0x04000044 RID: 68
		private bool _isExpanded = true;

		// Token: 0x04000045 RID: 69
		private string _groupName = string.Empty;

		// Token: 0x04000046 RID: 70
		private string _hintText = string.Empty;

		// Token: 0x04000047 RID: 71
		private string _groupNameDisplay = string.Empty;
	}
}
