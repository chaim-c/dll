using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bannerlord.ModuleManager;
using BUTR.DependencyInjection;
using ComparerExtensions;
using MCM.Abstractions;
using MCM.Abstractions.Base;
using MCM.Abstractions.GameFeatures;
using MCM.UI.Dropdown;
using MCM.UI.Extensions;
using MCM.UI.Patches;
using MCM.UI.Utils;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ScreenSystem;

namespace MCM.UI.GUI.ViewModels
{
	// Token: 0x0200001D RID: 29
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ModOptionsVM : ViewModel
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00004438 File Offset: 0x00002638
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00004440 File Offset: 0x00002640
		[Nullable(2)]
		private SettingsEntryVM SelectedEntry
		{
			[NullableContext(2)]
			get
			{
				return this._selectedEntry;
			}
			[NullableContext(2)]
			set
			{
				SettingsEntryVM selectedEntry = this._selectedEntry;
				MCMSelectorVM<MCMSelectorItemVM<PresetKey>> mcmselectorVM;
				if (selectedEntry == null)
				{
					mcmselectorVM = null;
				}
				else
				{
					SettingsVM settingsVM = selectedEntry.SettingsVM;
					mcmselectorVM = ((settingsVM != null) ? settingsVM.PresetsSelector : null);
				}
				MCMSelectorVM<MCMSelectorItemVM<PresetKey>> oldModPresetsSelector = mcmselectorVM;
				bool flag = oldModPresetsSelector != null;
				if (flag)
				{
					oldModPresetsSelector.PropertyChanged -= this.OnModPresetsSelectorChange;
				}
				bool flag2 = base.SetField<SettingsEntryVM>(ref this._selectedEntry, value, "SelectedMod");
				if (flag2)
				{
					base.OnPropertyChanged("IsSettingVisible");
					base.OnPropertyChanged("IsSettingUnavailableVisible");
					base.OnPropertyChanged("SelectedDisplayName");
					base.OnPropertyChanged("SomethingSelected");
					this.DoPresetsSelectorCopyWithoutEvents(delegate
					{
						SettingsEntryVM selectedEntry2 = this.SelectedEntry;
						MCMSelectorVM<MCMSelectorItemVM<PresetKey>> mcmselectorVM2;
						if (selectedEntry2 == null)
						{
							mcmselectorVM2 = null;
						}
						else
						{
							SettingsVM settingsVM2 = selectedEntry2.SettingsVM;
							mcmselectorVM2 = ((settingsVM2 != null) ? settingsVM2.PresetsSelector : null);
						}
						MCMSelectorVM<MCMSelectorItemVM<PresetKey>> modPresetsSelector = mcmselectorVM2;
						bool flag3 = modPresetsSelector != null;
						if (flag3)
						{
							modPresetsSelector.PropertyChanged += this.OnModPresetsSelectorChange;
							this.PresetsSelectorCopy.Refresh(from x in this.SelectedEntry.SettingsVM.PresetsSelector.ItemList
							select x.OriginalItem, this.SelectedEntry.SettingsVM.PresetsSelector.SelectedIndex);
						}
						else
						{
							this.PresetsSelectorCopy.Refresh(Enumerable.Empty<PresetKey>(), -1);
						}
					});
					base.OnPropertyChanged("IsPresetsSelectorVisible");
				}
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000044F1 File Offset: 0x000026F1
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x000044F9 File Offset: 0x000026F9
		public bool IsDisabled { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004502 File Offset: 0x00002702
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x0000450A File Offset: 0x0000270A
		[DataSourceProperty]
		public string Name
		{
			get
			{
				return this._titleLabel;
			}
			set
			{
				base.SetField<string>(ref this._titleLabel, value, "Name");
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000451F File Offset: 0x0000271F
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00004527 File Offset: 0x00002727
		[DataSourceProperty]
		public string DoneButtonText
		{
			get
			{
				return this._doneButtonText;
			}
			set
			{
				base.SetField<string>(ref this._doneButtonText, value, "DoneButtonText");
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000453C File Offset: 0x0000273C
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00004544 File Offset: 0x00002744
		[DataSourceProperty]
		public string CancelButtonText
		{
			get
			{
				return this._cancelButtonText;
			}
			set
			{
				base.SetField<string>(ref this._cancelButtonText, value, "CancelButtonText");
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00004559 File Offset: 0x00002759
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00004561 File Offset: 0x00002761
		[DataSourceProperty]
		public string ModsText
		{
			get
			{
				return this._modsText;
			}
			set
			{
				base.SetField<string>(ref this._modsText, value, "ModsText");
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00004576 File Offset: 0x00002776
		[DataSourceProperty]
		public MBBindingList<SettingsEntryVM> ModSettingsList { get; } = new MBBindingList<SettingsEntryVM>();

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000BD RID: 189 RVA: 0x0000457E File Offset: 0x0000277E
		[Nullable(2)]
		[DataSourceProperty]
		public SettingsVM SelectedMod
		{
			[NullableContext(2)]
			get
			{
				SettingsEntryVM selectedEntry = this.SelectedEntry;
				return (selectedEntry != null) ? selectedEntry.SettingsVM : null;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00004592 File Offset: 0x00002792
		[DataSourceProperty]
		public string SelectedDisplayName
		{
			get
			{
				return (this.SelectedEntry == null) ? this.ModNameNotSpecifiedText : this.SelectedEntry.DisplayName;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000045AF File Offset: 0x000027AF
		[DataSourceProperty]
		public bool SomethingSelected
		{
			get
			{
				return this.SelectedEntry != null;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000045BD File Offset: 0x000027BD
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x000045C8 File Offset: 0x000027C8
		[DataSourceProperty]
		public string HintText
		{
			get
			{
				return this._hintText;
			}
			set
			{
				bool flag = base.SetField<string>(ref this._hintText, value, "HintText");
				if (flag)
				{
					base.OnPropertyChanged("IsHintVisible");
				}
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000045FA File Offset: 0x000027FA
		[DataSourceProperty]
		public bool IsHintVisible
		{
			get
			{
				return !string.IsNullOrWhiteSpace(this.HintText);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x0000460A File Offset: 0x0000280A
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00004614 File Offset: 0x00002814
		[DataSourceProperty]
		public string SearchText
		{
			get
			{
				return this._searchText;
			}
			set
			{
				bool flag;
				if (base.SetField<string>(ref this._searchText, value, "SearchText"))
				{
					SettingsEntryVM selectedEntry = this.SelectedEntry;
					if (selectedEntry == null)
					{
						flag = false;
					}
					else
					{
						SettingsVM settingsVM = selectedEntry.SettingsVM;
						int? num = (settingsVM != null) ? new int?(settingsVM.SettingPropertyGroups.Count) : null;
						int num2 = 0;
						flag = (num.GetValueOrDefault() > num2 & num != null);
					}
				}
				else
				{
					flag = false;
				}
				bool flag2 = flag;
				if (flag2)
				{
					foreach (SettingsPropertyGroupVM group in this.SelectedEntry.SettingsVM.SettingPropertyGroups)
					{
						group.NotifySearchChanged();
					}
				}
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000046D8 File Offset: 0x000028D8
		[DataSourceProperty]
		public MCMSelectorVM<MCMSelectorItemVM<PresetKey>> PresetsSelectorCopy { get; } = new MCMSelectorVM<MCMSelectorItemVM<PresetKey>>(Enumerable.Empty<PresetKey>(), -1);

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000046E0 File Offset: 0x000028E0
		[DataSourceProperty]
		public bool IsPresetsSelectorVisible
		{
			get
			{
				return this.SelectedEntry != null;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000046EE File Offset: 0x000028EE
		[DataSourceProperty]
		public bool IsSettingVisible
		{
			get
			{
				SettingsEntryVM selectedEntry = this.SelectedEntry;
				return ((selectedEntry != null) ? selectedEntry.SettingsVM : null) != null;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00004708 File Offset: 0x00002908
		[DataSourceProperty]
		public bool IsSettingUnavailableVisible
		{
			get
			{
				SettingsEntryVM selectedEntry = this.SelectedEntry;
				return ((selectedEntry != null) ? selectedEntry.SettingsVM : null) == null;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x0000471F File Offset: 0x0000291F
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00004727 File Offset: 0x00002927
		[DataSourceProperty]
		public string ModNameNotSpecifiedText
		{
			get
			{
				return this._modNameNotSpecifiedText;
			}
			set
			{
				base.SetField<string>(ref this._modNameNotSpecifiedText, value, "ModNameNotSpecifiedText");
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000CB RID: 203 RVA: 0x0000473C File Offset: 0x0000293C
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00004744 File Offset: 0x00002944
		[DataSourceProperty]
		public string UnavailableText
		{
			get
			{
				return this._unavailableText;
			}
			set
			{
				base.SetField<string>(ref this._unavailableText, value, "UnavailableText");
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000475C File Offset: 0x0000295C
		public ModOptionsVM()
		{
			this._logger = (GenericServiceProvider.GetService<ILogger<ModOptionsVM>>() ?? NullLogger<ModOptionsVM>.Instance);
			this.SearchText = string.Empty;
			this.InitializeModSettings();
			this.RefreshValues();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004813 File Offset: 0x00002A13
		private void InitializeModSettings()
		{
			Task.Factory.StartNew(delegate(object syncContext)
			{
				try
				{
					SynchronizationContext uiContext = syncContext as SynchronizationContext;
					bool flag = uiContext != null;
					if (flag)
					{
						bool flag2 = SynchronizationContext.Current == uiContext;
						if (flag2)
						{
							LoggerExtensions.LogWarning(this._logger, "SynchronizationContext.Current is the UI SynchronizationContext", Array.Empty<object>());
						}
						BaseSettingsProvider instance = BaseSettingsProvider.Instance;
						IEnumerable<SettingsVM> enumerable;
						if (instance == null)
						{
							enumerable = null;
						}
						else
						{
							enumerable = from vm in instance.SettingsDefinitions.Parallel<SettingsDefinition>().Select(delegate(SettingsDefinition s)
							{
								SettingsVM result;
								try
								{
									result = new SettingsVM(s, this);
								}
								catch (Exception e2)
								{
									LoggerExtensions.LogError(this._logger, e2, "Error while creating a ViewModel for settings {Id}", new object[]
									{
										s.SettingsId
									});
									InformationManager.DisplayMessage(new InformationMessage(new TextObject("{=HNduGf7H5a}There was an error while parsing settings from '" + s.SettingsId + "'! Please contact the MCM developers and the mod developer!", null).ToString(), Colors.Red));
									result = null;
								}
								return result;
							})
							where vm != null
							select vm;
						}
						IEnumerable<SettingsVM> settingsVM = enumerable;
						IEnumerable<SettingsVM> enumerable2;
						if ((enumerable2 = settingsVM) == null)
						{
							IEnumerable<SettingsVM> enumerable3 = Enumerable.Empty<SettingsVM>();
							enumerable2 = enumerable3;
						}
						foreach (SettingsVM viewModel in enumerable2)
						{
							uiContext.Send(delegate(object state)
							{
								SettingsVM vm = state as SettingsVM;
								bool flag3 = vm != null;
								if (flag3)
								{
									this.ModSettingsList.Add(new SettingsEntryVM(vm, new Action<SettingsEntryVM>(this.ExecuteSelect)));
									vm.RefreshValues();
								}
							}, viewModel);
						}
						this.ModSettingsList.Sort((from x in KeyComparer<SettingsEntryVM>
						orderby x.Id.StartsWith("MCM") || x.Id.StartsWith("Testing") || x.Id.StartsWith("ModLib") descending
						select x).ThenByDescending((SettingsEntryVM x) => x.DisplayName, new AlphanumComparatorFast()));
					}
				}
				catch (Exception e)
				{
					LoggerExtensions.LogError(this._logger, e, "Error while creating ViewModels for the settings", Array.Empty<object>());
					InformationManager.DisplayMessage(new InformationMessage(new TextObject("{=JLKaTyJcyu}There was a major error while building the settings list! Please contact the MCM developers!", null).ToString(), Colors.Red));
				}
			}, SynchronizationContext.Current);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004834 File Offset: 0x00002A34
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = new TextObject("{=ModOptionsVM_Name}Mod Options", null).ToString();
			this.DoneButtonText = new TextObject("{=WiNRdfsm}Done", null).ToString();
			this.CancelButtonText = new TextObject("{=3CpNUnVl}Cancel", null).ToString();
			this.ModsText = new TextObject("{=ModOptionsPageView_Mods}Mods", null).ToString();
			this.ModNameNotSpecifiedText = new TextObject("{=ModOptionsVM_NotSpecified}Mod Name not Specified.", null).ToString();
			this.UnavailableText = new TextObject("{=ModOptionsVM_Unavailable}Settings are available within a Game Session!", null).ToString();
			this.PresetsSelectorCopy.RefreshValues();
			foreach (SettingsEntryVM viewModel in this.ModSettingsList)
			{
				viewModel.RefreshValues();
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004924 File Offset: 0x00002B24
		private void OnPresetsSelectorChange(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			MCMSelectorVM<MCMSelectorItemVM<PresetKey>> selector = sender as MCMSelectorVM<MCMSelectorItemVM<PresetKey>>;
			bool flag = selector == null;
			if (!flag)
			{
				bool flag2 = propertyChangedEventArgs.PropertyName != "SelectedIndex";
				if (!flag2)
				{
					bool isDisabled = this.IsDisabled;
					if (isDisabled)
					{
						SettingsEntryVM selectedEntry = this.SelectedEntry;
						bool flag3;
						if (selectedEntry == null)
						{
							flag3 = (null != null);
						}
						else
						{
							SettingsVM settingsVM = selectedEntry.SettingsVM;
							flag3 = (((settingsVM != null) ? settingsVM.PresetsSelector : null) != null);
						}
						bool flag4 = flag3 && selector.SelectedIndex == -1;
						if (flag4)
						{
							this.DoPresetsSelectorCopyWithoutEvents(delegate
							{
								this.PresetsSelectorCopy.SelectedIndex = this.SelectedEntry.SettingsVM.PresetsSelector.SelectedIndex;
							});
						}
					}
					else
					{
						bool flag5 = selector.SelectedItem == null || selector.SelectedIndex == -1;
						if (!flag5)
						{
							bool flag6 = selector.ItemList.Count < selector.SelectedIndex;
							if (!flag6)
							{
								PresetKey presetKey = selector.ItemList[selector.SelectedIndex].OriginalItem;
								string titleText = new TextObject("{=ModOptionsVM_ChangeToPreset}Change to preset '{PRESET}'", new Dictionary<string, object>
								{
									{
										"PRESET",
										presetKey.Name
									}
								}).ToString();
								string value = "{=ModOptionsVM_Discard}Are you sure you wish to discard the current settings for {NAME} to '{ITEM}'?";
								Dictionary<string, object> dictionary = new Dictionary<string, object>();
								string key = "NAME";
								SettingsEntryVM selectedEntry2 = this.SelectedEntry;
								dictionary.Add(key, (selectedEntry2 != null) ? selectedEntry2.DisplayName : null);
								dictionary.Add("ITEM", presetKey.Name);
								Action <>9__3;
								InformationManager.ShowInquiry(InquiryDataUtils.Create(titleText, new TextObject(value, dictionary).ToString(), true, true, new TextObject("{=aeouhelq}Yes", null).ToString(), new TextObject("{=8OkPHu4f}No", null).ToString(), delegate
								{
									bool flag7 = this.SelectedEntry != null;
									if (flag7)
									{
										SettingsVM settingsVM2 = this.SelectedEntry.SettingsVM;
										if (settingsVM2 != null)
										{
											settingsVM2.ChangePreset(presetKey.Id);
										}
										SettingsEntryVM selectedMod = this.SelectedEntry;
										this.ExecuteSelect(null);
										this.ExecuteSelect(selectedMod);
									}
								}, delegate
								{
									ModOptionsVM <>4__this = this;
									Action action;
									if ((action = <>9__3) == null)
									{
										action = (<>9__3 = delegate()
										{
											MCMSelectorVM<MCMSelectorItemVM<PresetKey>> presetsSelectorCopy = this.PresetsSelectorCopy;
											SettingsEntryVM selectedEntry3 = this.SelectedEntry;
											int? num;
											if (selectedEntry3 == null)
											{
												num = null;
											}
											else
											{
												SettingsVM settingsVM2 = selectedEntry3.SettingsVM;
												num = ((settingsVM2 != null) ? new int?(settingsVM2.PresetsSelector.SelectedIndex) : null);
											}
											int? num2 = num;
											presetsSelectorCopy.SelectedIndex = num2.GetValueOrDefault(-1);
										});
									}
									<>4__this.DoPresetsSelectorCopyWithoutEvents(action);
								}), false, false);
							}
						}
					}
				}
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004ADC File Offset: 0x00002CDC
		private void OnModPresetsSelectorChange(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			MCMSelectorVM<MCMSelectorItemVM<PresetKey>> selector = sender as MCMSelectorVM<MCMSelectorItemVM<PresetKey>>;
			bool flag = selector == null;
			if (!flag)
			{
				bool flag2 = propertyChangedEventArgs.PropertyName != "SelectedIndex";
				if (!flag2)
				{
					this.DoPresetsSelectorCopyWithoutEvents(delegate
					{
						this.PresetsSelectorCopy.SelectedIndex = selector.SelectedIndex;
					});
				}
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004B40 File Offset: 0x00002D40
		public void ExecuteClose()
		{
			bool isDisabled = this.IsDisabled;
			if (!isDisabled)
			{
				foreach (SettingsVM viewModel in (from x in this.ModSettingsList
				select x.SettingsVM).OfType<SettingsVM>())
				{
					viewModel.URS.UndoAll();
					viewModel.URS.ClearStack();
				}
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004BD8 File Offset: 0x00002DD8
		public bool ExecuteCancel()
		{
			return this.ExecuteCancelInternal(true, null);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004BE2 File Offset: 0x00002DE2
		public void StartTyping()
		{
			OptionsVMPatch.BlockSwitch = true;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004BEB File Offset: 0x00002DEB
		public void StopTyping()
		{
			OptionsVMPatch.BlockSwitch = false;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004BF4 File Offset: 0x00002DF4
		[NullableContext(2)]
		public bool ExecuteCancelInternal(bool popScreen, Action onClose = null)
		{
			this.OnFinalize();
			if (popScreen)
			{
				ScreenManager.PopScreen();
			}
			else if (onClose != null)
			{
				onClose();
			}
			foreach (SettingsVM viewModel in (from x in this.ModSettingsList
			select x.SettingsVM).OfType<SettingsVM>())
			{
				viewModel.URS.UndoAll();
				viewModel.URS.ClearStack();
			}
			return true;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004CA8 File Offset: 0x00002EA8
		public void ExecuteDone()
		{
			this.ExecuteDoneInternal(true, null);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004CB4 File Offset: 0x00002EB4
		[NullableContext(2)]
		public void ExecuteDoneInternal(bool popScreen, Action onClose = null)
		{
			List<SettingsVM> settingsVMs = (from x in this.ModSettingsList
			select x.SettingsVM).OfType<SettingsVM>().ToList<SettingsVM>();
			bool flag = !settingsVMs.Any((SettingsVM x) => x.URS.ChangesMade);
			if (flag)
			{
				this.OnFinalize();
				if (popScreen)
				{
					ScreenManager.PopScreen();
				}
				else
				{
					Action onClose2 = onClose;
					if (onClose2 != null)
					{
						onClose2();
					}
				}
			}
			else
			{
				List<SettingsVM> changedModSettings = (from x in settingsVMs
				where x.URS.ChangesMade
				select x).ToList<SettingsVM>();
				bool requireRestart = changedModSettings.Any((SettingsVM x) => x.RestartRequired());
				bool flag2 = requireRestart;
				if (flag2)
				{
					InformationManager.ShowInquiry(InquiryDataUtils.CreateTranslatable("{=ModOptionsVM_RestartTitle}Game Needs to Restart", "{=ModOptionsVM_RestartDesc}The game needs to be restarted to apply mod settings changes. Do you want to close the game now?", true, true, "{=aeouhelq}Yes", "{=3CpNUnVl}Cancel", delegate
					{
						foreach (SettingsVM changedModSetting2 in changedModSettings)
						{
							changedModSetting2.SaveSettings();
							changedModSetting2.URS.ClearStack();
						}
						this.OnFinalize();
						Action onClose4 = onClose;
						if (onClose4 != null)
						{
							onClose4();
						}
						Utilities.QuitGame();
					}, delegate
					{
					}), false, false);
				}
				else
				{
					foreach (SettingsVM changedModSetting in changedModSettings)
					{
						changedModSetting.SaveSettings();
						changedModSetting.URS.ClearStack();
					}
					this.OnFinalize();
					if (popScreen)
					{
						ScreenManager.PopScreen();
					}
					else
					{
						Action onClose3 = onClose;
						if (onClose3 != null)
						{
							onClose3();
						}
					}
				}
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004EA0 File Offset: 0x000030A0
		[NullableContext(2)]
		public void ExecuteSelect(SettingsEntryVM viewModel)
		{
			bool isDisabled = this.IsDisabled;
			if (!isDisabled)
			{
				bool flag = this.SelectedEntry != viewModel;
				if (flag)
				{
					bool flag2 = this.SelectedEntry != null;
					if (flag2)
					{
						this.SelectedEntry.IsSelected = false;
					}
					this.SelectedEntry = viewModel;
					bool flag3 = this.SelectedEntry != null;
					if (flag3)
					{
						this.SelectedEntry.IsSelected = true;
					}
				}
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004F10 File Offset: 0x00003110
		private void RefreshPresetList()
		{
			SettingsEntryVM selectedEntry = this.SelectedEntry;
			bool flag = ((selectedEntry != null) ? selectedEntry.SettingsVM : null) == null;
			if (!flag)
			{
				this.SelectedEntry.SettingsVM.ReloadPresetList();
				this.DoPresetsSelectorCopyWithoutEvents(delegate
				{
					this.PresetsSelectorCopy.Refresh(from x in this.SelectedEntry.SettingsVM.PresetsSelector.ItemList
					select x.OriginalItem, this.SelectedEntry.SettingsVM.PresetsSelector.SelectedIndex);
				});
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004F60 File Offset: 0x00003160
		private void OverridePreset(Action onOverride)
		{
			InformationManager.ShowInquiry(InquiryDataUtils.CreateTranslatable("{=ModOptionsVM_OverridePreset}Preset Already Exists", "{=ModOptionsVM_OverridePresetDesc}Preset already exists! Do you want to override it?", true, true, "{=aeouhelq}Yes", "{=3CpNUnVl}Cancel", onOverride, delegate
			{
			}), false, false);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004FB4 File Offset: 0x000031B4
		public void ExecuteManagePresets()
		{
			ModOptionsVM.<>c__DisplayClass76_0 CS$<>8__locals1 = new ModOptionsVM.<>c__DisplayClass76_0();
			CS$<>8__locals1.<>4__this = this;
			ModOptionsVM.<>c__DisplayClass76_0 CS$<>8__locals2 = CS$<>8__locals1;
			SettingsEntryVM selectedEntry = this.SelectedEntry;
			BaseSettings settings;
			if (selectedEntry == null)
			{
				settings = null;
			}
			else
			{
				SettingsVM settingsVM = selectedEntry.SettingsVM;
				settings = ((settingsVM != null) ? settingsVM.SettingsInstance : null);
			}
			CS$<>8__locals2.settings = settings;
			bool flag = CS$<>8__locals1.settings == null;
			if (!flag)
			{
				CS$<>8__locals1.fileSystem = GenericServiceProvider.GetService<IFileSystemProvider>();
				bool flag2 = CS$<>8__locals1.fileSystem == null;
				if (!flag2)
				{
					MCMSelectorItemVM<PresetKey> selectedItem = this.PresetsSelectorCopy.SelectedItem;
					bool flag3 = ((selectedItem != null) ? selectedItem.OriginalItem : null) == null;
					if (!flag3)
					{
						List<InquiryElement> inquiries = new List<InquiryElement>
						{
							new InquiryElement("import_preset", new TextObject("{=ModOptionsVM_ManagePresetsImport}Import a new Preset", null).ToString(), null)
						};
						bool flag4 = this.PresetsSelectorCopy.SelectedItem.OriginalItem.Id == "custom";
						if (flag4)
						{
							inquiries.Add(new InquiryElement("save_preset", new TextObject("{=ModOptionsVM_SaveAsPreset}Save As Preset", null).ToString(), null));
						}
						string id = this.PresetsSelectorCopy.SelectedItem.OriginalItem.Id;
						bool flag5 = !(id == "custom") && !(id == "default");
						if (flag5)
						{
							inquiries.Add(new InquiryElement("export_preset", new TextObject("{=ModOptionsVM_ManagePresetsExport}Export Preset '{PRESETNAME}'", new Dictionary<string, object>
							{
								{
									"PRESETNAME",
									this.PresetsSelectorCopy.SelectedItem.OriginalItem.Name
								}
							}).ToString(), null));
							inquiries.Add(new InquiryElement("delete_preset", new TextObject("{=ModOptionsVM_ManagePresetsDelete}Delete Preset '{PRESETNAME}'", new Dictionary<string, object>
							{
								{
									"PRESETNAME",
									this.PresetsSelectorCopy.SelectedItem.OriginalItem.Name
								}
							}).ToString(), null));
						}
						MBInformationManager.ShowMultiSelectionInquiry(InquiryDataUtils.CreateMultiTranslatable("{=ModOptionsVM_ManagePresets}Manage Presets", "", inquiries, true, 1, 1, "{=5Unqsx3N}Confirm", "{=3CpNUnVl}Cancel", new Action<List<InquiryElement>>(CS$<>8__locals1.<ExecuteManagePresets>g__OnActionSelected|4), delegate(List<InquiryElement> _)
						{
						}), false, false);
					}
				}
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000051E0 File Offset: 0x000033E0
		public void ExecuteManageSettingsPack()
		{
			string titleText = "{=ModOptionsVM_ManagePacks}Manage Settings Packs";
			string descriptionText = "";
			List<InquiryElement> list = new List<InquiryElement>();
			list.Add(new InquiryElement("import_pack", new TextObject("{=ModOptionsVM_ManagePackImport}Import a Settings Pack", null).ToString(), null));
			list.Add(new InquiryElement("export_pack", new TextObject("{=ModOptionsVM_ManagePackExport}Export Settings Pack", null).ToString(), null));
			MBInformationManager.ShowMultiSelectionInquiry(InquiryDataUtils.CreateMultiTranslatable(titleText, descriptionText, list, true, 1, 1, "{=5Unqsx3N}Confirm", "{=3CpNUnVl}Cancel", new Action<List<InquiryElement>>(this.<ExecuteManageSettingsPack>g__OnActionSelected|77_2), delegate(List<InquiryElement> _)
			{
			}), false, false);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000528C File Offset: 0x0000348C
		public override void OnFinalize()
		{
			foreach (SettingsEntryVM modSettings in this.ModSettingsList)
			{
				modSettings.OnFinalize();
			}
			SettingPropertyDefinitionCache.Clear();
			base.OnFinalize();
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000052EC File Offset: 0x000034EC
		private void DoPresetsSelectorCopyWithoutEvents(Action action)
		{
			this.PresetsSelectorCopy.PropertyChanged -= this.OnPresetsSelectorChange;
			action();
			this.PresetsSelectorCopy.PropertyChanged += this.OnPresetsSelectorChange;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000056C8 File Offset: 0x000038C8
		[CompilerGenerated]
		private void <ExecuteManageSettingsPack>g__ImportPack|77_0()
		{
			OpenFileDialog dialog = new OpenFileDialog
			{
				Title = "Import Settings Pack",
				Filter = "MCM Settings Pack (.zip)|*.zip",
				CheckFileExists = true,
				CheckPathExists = true,
				ReadOnlyChecked = true,
				Multiselect = false,
				ValidateNames = true
			};
			bool flag = dialog.ShowDialog() == DialogResult.OK;
			if (flag)
			{
				bool flag2 = string.IsNullOrEmpty(dialog.FileName) || !File.Exists(dialog.FileName);
				if (!flag2)
				{
					using (FileStream file = File.OpenRead(dialog.FileName))
					{
						using (ZipArchive archive = new ZipArchive(file, ZipArchiveMode.Read))
						{
							List<SettingSnapshot> snapshots = new List<SettingSnapshot>();
							foreach (ZipArchiveEntry entry in archive.Entries)
							{
								using (Stream stream = entry.Open())
								{
									using (StreamReader reader = new StreamReader(stream))
									{
										snapshots.Add(new SettingSnapshot(entry.FullName, reader.ReadToEnd()));
									}
								}
							}
							BaseSettingsProvider instance = BaseSettingsProvider.Instance;
							using (IEnumerator<BaseSettings> enumerator2 = (((instance != null) ? instance.LoadAvailableSnapshots(snapshots) : null) ?? Enumerable.Empty<BaseSettings>()).GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									BaseSettings settings = enumerator2.Current;
									SettingsEntryVM settingsEntryVM = this.ModSettingsList.FirstOrDefault((SettingsEntryVM x) => x.Id == settings.Id);
									if (settingsEntryVM == null)
									{
										goto IL_191;
									}
									SettingsVM viewModel = settingsEntryVM.SettingsVM;
									if (viewModel == null)
									{
										goto IL_191;
									}
									BaseSettings current = viewModel.SettingsInstance;
									int num = (current != null) ? 1 : 0;
									IL_192:
									bool flag3 = num == 0;
									if (flag3)
									{
										continue;
									}
									UISettingsUtils.OverrideValues(viewModel.URS, current, settings);
									continue;
									IL_191:
									num = 0;
									goto IL_192;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005950 File Offset: 0x00003B50
		[CompilerGenerated]
		internal static void <ExecuteManageSettingsPack>g__ExportPack|77_1()
		{
			SaveFileDialog dialog = new SaveFileDialog
			{
				Title = "Export Settings Pack",
				Filter = "MCM Settings Pack (.zip)|*.zip",
				FileName = "MySettingsPack.zip",
				ValidateNames = true,
				OverwritePrompt = true
			};
			bool flag = dialog.ShowDialog() == DialogResult.OK;
			if (flag)
			{
				BaseSettingsProvider instance = BaseSettingsProvider.Instance;
				IEnumerable<SettingSnapshot> snapshots = ((instance != null) ? instance.SaveAvailableSnapshots() : null) ?? Enumerable.Empty<SettingSnapshot>();
				using (FileStream file = File.Create(dialog.FileName))
				{
					using (ZipArchive archive = new ZipArchive(file, ZipArchiveMode.Create))
					{
						foreach (SettingSnapshot snapshot in snapshots)
						{
							ZipArchiveEntry entry = archive.CreateEntry(snapshot.Path, CompressionLevel.Optimal);
							using (Stream stream = entry.Open())
							{
								using (StreamWriter writer = new StreamWriter(stream))
								{
									writer.Write(snapshot.JsonContent);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005AB4 File Offset: 0x00003CB4
		[CompilerGenerated]
		private void <ExecuteManageSettingsPack>g__OnActionSelected|77_2(List<InquiryElement> selected)
		{
			object identifier = selected[0].Identifier;
			object obj = identifier;
			string text = obj as string;
			if (text != null)
			{
				if (!(text == "import_pack"))
				{
					if (text == "export_pack")
					{
						ModOptionsVM.<ExecuteManageSettingsPack>g__ExportPack|77_1();
					}
				}
				else
				{
					this.<ExecuteManageSettingsPack>g__ImportPack|77_0();
				}
			}
		}

		// Token: 0x04000033 RID: 51
		private readonly ILogger<ModOptionsVM> _logger;

		// Token: 0x04000034 RID: 52
		private string _titleLabel = string.Empty;

		// Token: 0x04000035 RID: 53
		private string _doneButtonText = string.Empty;

		// Token: 0x04000036 RID: 54
		private string _cancelButtonText = string.Empty;

		// Token: 0x04000037 RID: 55
		private string _modsText = string.Empty;

		// Token: 0x04000038 RID: 56
		private string _hintText = string.Empty;

		// Token: 0x04000039 RID: 57
		private string _modNameNotSpecifiedText = string.Empty;

		// Token: 0x0400003A RID: 58
		private string _unavailableText = string.Empty;

		// Token: 0x0400003B RID: 59
		[Nullable(2)]
		private SettingsEntryVM _selectedEntry;

		// Token: 0x0400003C RID: 60
		private string _searchText = string.Empty;
	}
}
