using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MCM.Abstractions;
using MCM.Abstractions.Base;
using MCM.UI.Actions;
using MCM.UI.Dropdown;
using MCM.UI.Extensions;
using MCM.UI.Utils;
using TaleWorlds.Library;

namespace MCM.UI.GUI.ViewModels
{
	// Token: 0x02000021 RID: 33
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class SettingsVM : ViewModel
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00007268 File Offset: 0x00005468
		public ModOptionsVM MainView { get; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00007270 File Offset: 0x00005470
		public UndoRedoStack URS { get; } = new UndoRedoStack();

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00007278 File Offset: 0x00005478
		public SettingsDefinition SettingsDefinition { get; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00007280 File Offset: 0x00005480
		[Nullable(2)]
		public BaseSettings SettingsInstance
		{
			[NullableContext(2)]
			get
			{
				BaseSettingsProvider instance = BaseSettingsProvider.Instance;
				return (instance != null) ? instance.GetSettings(this.SettingsDefinition.SettingsId) : null;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000729E File Offset: 0x0000549E
		public MCMSelectorVM<MCMSelectorItemVM<PresetKey>> PresetsSelector { get; } = new MCMSelectorVM<MCMSelectorItemVM<PresetKey>>(Enumerable.Empty<MCMSelectorItemVM<PresetKey>>(), -1);

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000159 RID: 345 RVA: 0x000072A6 File Offset: 0x000054A6
		// (set) Token: 0x0600015A RID: 346 RVA: 0x000072AE File Offset: 0x000054AE
		[DataSourceProperty]
		public string DisplayName
		{
			get
			{
				return this._displayName;
			}
			private set
			{
				base.SetField<string>(ref this._displayName, value, "DisplayName");
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600015B RID: 347 RVA: 0x000072C3 File Offset: 0x000054C3
		// (set) Token: 0x0600015C RID: 348 RVA: 0x000072CB File Offset: 0x000054CB
		[DataSourceProperty]
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				base.SetField<bool>(ref this._isSelected, value, "IsSelected");
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600015D RID: 349 RVA: 0x000072E0 File Offset: 0x000054E0
		[DataSourceProperty]
		public MBBindingList<SettingsPropertyGroupVM> SettingPropertyGroups { get; } = new MBBindingList<SettingsPropertyGroupVM>();

		// Token: 0x0600015E RID: 350 RVA: 0x000072E8 File Offset: 0x000054E8
		public SettingsVM(SettingsDefinition definition, ModOptionsVM mainView)
		{
			this.SettingsDefinition = definition;
			this.MainView = mainView;
			this.ReloadPresetList();
			bool flag = this.SettingsInstance != null;
			if (flag)
			{
				this.SettingPropertyGroups.AddRange(from x in SettingPropertyDefinitionCache.GetSettingPropertyGroups(this.SettingsInstance)
				select new SettingsPropertyGroupVM(x, this, null));
			}
			this.RefreshValues();
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00007398 File Offset: 0x00005598
		public void ReloadPresetList()
		{
			this._cachedPresets.Clear();
			BaseSettings settingsInstance = this.SettingsInstance;
			IEnumerable<ISettingsPreset> enumerable;
			if ((enumerable = ((settingsInstance != null) ? settingsInstance.GetBuiltInPresets().Concat(this.SettingsInstance.GetExternalPresets()) : null)) == null)
			{
				IEnumerable<ISettingsPreset> enumerable2 = Enumerable.Empty<ISettingsPreset>();
				enumerable = enumerable2;
			}
			foreach (ISettingsPreset preset in enumerable)
			{
				this._cachedPresets.Add(new PresetKey(preset), preset.LoadPreset());
			}
			IEnumerable<PresetKey> presets = new List<PresetKey>
			{
				new PresetKey("custom", "{=SettingsVM_Custom}Custom")
			}.Concat(this._cachedPresets.Keys);
			this.PresetsSelector.Refresh(presets, -1);
			this.PresetsSelector.ItemList[0].CanBeSelected = false;
			this.RecalculatePresetIndex();
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00007488 File Offset: 0x00005688
		public void RecalculatePresetIndex()
		{
			bool flag = this.SettingsInstance == null;
			if (!flag)
			{
				int index = 1;
				foreach (BaseSettings preset in this._cachedPresets.Values)
				{
					bool flag2 = SettingPropertyDefinitionCache.Equals(this.SettingsInstance, preset);
					if (flag2)
					{
						this.PresetsSelector.SelectedIndex = index;
						return;
					}
					index++;
				}
				this.PresetsSelector.SelectedIndex = 0;
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00007524 File Offset: 0x00005724
		public override void RefreshValues()
		{
			base.RefreshValues();
			BaseSettings settingsInstance = this.SettingsInstance;
			this.DisplayName = (((settingsInstance != null) ? settingsInstance.DisplayName : null) ?? "ERROR");
			this.SettingPropertyGroups.Sort(UISettingsUtils.SettingsPropertyGroupVMComparer);
			foreach (SettingsPropertyGroupVM group in this.SettingPropertyGroups)
			{
				group.RefreshValues();
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000075B0 File Offset: 0x000057B0
		public bool RestartRequired()
		{
			return this.SettingPropertyGroups.SelectMany((SettingsPropertyGroupVM x) => SettingsUtils.GetAllSettingPropertyDefinitions(x.SettingPropertyGroupDefinition)).Any((ISettingsPropertyDefinition p) => p.RequireRestart && this.URS.RefChanged(p.PropertyReference));
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000075F0 File Offset: 0x000057F0
		public void ChangePreset(string presetId)
		{
			BaseSettings preset;
			bool flag = this.SettingsInstance != null && this._cachedPresets.TryGetValue(new PresetKey(presetId, string.Empty), out preset);
			if (flag)
			{
				UISettingsUtils.OverrideValues(this.URS, this.SettingsInstance, preset);
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00007638 File Offset: 0x00005838
		public void ChangePresetValue(string presetId, string valueId)
		{
			BaseSettings preset;
			bool flag = this.SettingsInstance != null && this._cachedPresets.TryGetValue(new PresetKey(presetId, string.Empty), out preset);
			if (flag)
			{
				ISettingsPropertyDefinition current = SettingPropertyDefinitionCache.GetAllSettingPropertyDefinitions(this.SettingsInstance).FirstOrDefault((ISettingsPropertyDefinition spd) => spd.Id == valueId);
				ISettingsPropertyDefinition @new = SettingPropertyDefinitionCache.GetAllSettingPropertyDefinitions(preset).FirstOrDefault((ISettingsPropertyDefinition spd) => spd.Id == valueId);
				bool flag2 = current != null && @new != null;
				if (flag2)
				{
					UISettingsUtils.OverrideValues(this.URS, current, @new);
				}
			}
			this.RecalculatePresetIndex();
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000076D9 File Offset: 0x000058D9
		public void ResetSettings()
		{
			this.ChangePreset("default");
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000076E8 File Offset: 0x000058E8
		public void SaveSettings()
		{
			bool flag = this.SettingsInstance != null;
			if (flag)
			{
				BaseSettingsProvider instance = BaseSettingsProvider.Instance;
				if (instance != null)
				{
					instance.SaveSettings(this.SettingsInstance);
				}
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000771D File Offset: 0x0000591D
		public void ResetSettingsValue(string valueId)
		{
			this.ChangePresetValue("default", valueId);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00007730 File Offset: 0x00005930
		public override void OnFinalize()
		{
			foreach (SettingsPropertyGroupVM settingPropertyGroup in this.SettingPropertyGroups)
			{
				settingPropertyGroup.OnFinalize();
			}
			base.OnFinalize();
		}

		// Token: 0x04000059 RID: 89
		private string _displayName = string.Empty;

		// Token: 0x0400005A RID: 90
		private bool _isSelected = false;

		// Token: 0x0400005B RID: 91
		private readonly Dictionary<PresetKey, BaseSettings> _cachedPresets = new Dictionary<PresetKey, BaseSettings>();
	}
}
