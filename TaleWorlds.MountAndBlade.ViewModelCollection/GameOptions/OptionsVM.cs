using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.Options;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Options;
using TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions.GameKeys;
using TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions.GamepadOptions;
using TaleWorlds.MountAndBlade.ViewModelCollection.Input;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions
{
	// Token: 0x02000063 RID: 99
	public class OptionsVM : ViewModel
	{
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x0001CEF2 File Offset: 0x0001B0F2
		// (set) Token: 0x060007AD RID: 1965 RVA: 0x0001CEFA File Offset: 0x0001B0FA
		public OptionsVM.OptionsMode CurrentOptionsMode { get; private set; }

		// Token: 0x060007AE RID: 1966 RVA: 0x0001CF04 File Offset: 0x0001B104
		public OptionsVM(bool autoHandleClose, OptionsVM.OptionsMode optionsMode, Action<KeyOptionVM> onKeybindRequest, Action onBrightnessExecute = null, Action onExposureExecute = null)
		{
			this._onKeybindRequest = onKeybindRequest;
			this._autoHandleClose = autoHandleClose;
			this.CurrentOptionsMode = optionsMode;
			this._onBrightnessExecute = onBrightnessExecute;
			this._onExposureExecute = onExposureExecute;
			this._groupedCategories = new List<GroupedOptionCategoryVM>();
			NativeOptions.RefreshOptionsData();
			bool isMultiplayer = this.CurrentOptionsMode == OptionsVM.OptionsMode.Multiplayer;
			bool isMainMenu = this.CurrentOptionsMode == OptionsVM.OptionsMode.MainMenu;
			this._gameplayOptionCategory = new GroupedOptionCategoryVM(this, new TextObject("{=2zcrC0h1}Gameplay", null), OptionsProvider.GetGameplayOptionCategory(isMainMenu, isMultiplayer), true, true);
			this._audioOptionCategory = new GroupedOptionCategoryVM(this, new TextObject("{=xebFLnH2}Audio", null), OptionsProvider.GetAudioOptionCategory(isMultiplayer), true, false);
			this._videoOptionCategory = new GroupedOptionCategoryVM(this, new TextObject("{=gamevideo}Video", null), OptionsProvider.GetVideoOptionCategory(isMainMenu, new Action(this.OnBrightnessClick), new Action(this.OnExposureClick), new Action(this.ExecuteBenchmark)), true, false);
			bool isEnabled = true;
			this._performanceOptionCategory = new GroupedOptionCategoryVM(this, new TextObject("{=fM9E7frB}Performance", null), OptionsProvider.GetPerformanceOptionCategory(isMultiplayer), isEnabled, false);
			this._groupedCategories.Add(this._videoOptionCategory);
			this._groupedCategories.Add(this._audioOptionCategory);
			this._groupedCategories.Add(this._gameplayOptionCategory);
			this._performanceManagedOptions = this._performanceOptionCategory.GetManagedOptions();
			this._gameKeyCategory = new GameKeyOptionCategoryVM(this._onKeybindRequest, OptionsProvider.GetGameKeyCategoriesList(this.CurrentOptionsMode == OptionsVM.OptionsMode.Multiplayer));
			TextObject name = new TextObject("{=SQpGQzTI}Controller", null);
			this._gamepadCategory = new GamepadOptionCategoryVM(this, name, OptionsProvider.GetControllerOptionCategory(), true, true);
			this._categories = new List<ViewModel>();
			this._categories.Add(this._videoOptionCategory);
			this._categories.Add(this._performanceOptionCategory);
			this._categories.Add(this._audioOptionCategory);
			this._categories.Add(this._gameplayOptionCategory);
			this._categories.Add(this._gameKeyCategory);
			this._categories.Add(this._gamepadCategory);
			this.SetSelectedCategory(0);
			if (onBrightnessExecute == null)
			{
				this.BrightnessPopUp = new BrightnessOptionVM(null);
			}
			if (onExposureExecute == null)
			{
				this.ExposurePopUp = new ExposureOptionVM(null);
			}
			if (Game.Current != null && this._autoHandleClose)
			{
				Game.Current.GameStateManager.RegisterActiveStateDisableRequest(this);
			}
			this._refreshRateOption = this.VideoOptions.GetOption(NativeOptions.NativeOptionsType.RefreshRate);
			this._resolutionOption = this.VideoOptions.GetOption(NativeOptions.NativeOptionsType.ScreenResolution);
			this._monitorOption = this.VideoOptions.GetOption(NativeOptions.NativeOptionsType.SelectedMonitor);
			this._displayModeOption = this.VideoOptions.GetOption(NativeOptions.NativeOptionsType.DisplayMode);
			this._overallOption = (this.PerformanceOptions.GetOption(NativeOptions.NativeOptionsType.OverAll) as StringOptionDataVM);
			this._dlssOption = this.PerformanceOptions.GetOption(NativeOptions.NativeOptionsType.DLSS);
			this._dynamicResolutionOptions = new List<GenericOptionDataVM>
			{
				this.PerformanceOptions.GetOption(NativeOptions.NativeOptionsType.DynamicResolution),
				this.PerformanceOptions.GetOption(NativeOptions.NativeOptionsType.DynamicResolutionTarget)
			};
			this.IsConsole = true;
			GroupedOptionCategoryVM performanceOptionCategory = this._performanceOptionCategory;
			if (performanceOptionCategory != null)
			{
				performanceOptionCategory.InitializeDependentConfigs(new Action<IOptionData, float>(this.UpdateDependentConfigs));
			}
			this.IsConsole = false;
			this.GameVersionText = Utilities.GetApplicationVersionWithBuildNumber().ToString();
			this.RefreshValues();
			Input.OnGamepadActiveStateChanged = (Action)Delegate.Combine(Input.OnGamepadActiveStateChanged, new Action(this.OnGamepadActiveStateChanged));
			this._isInitialized = true;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0001D273 File Offset: 0x0001B473
		public OptionsVM(OptionsVM.OptionsMode optionsMode, Action onClose, Action<KeyOptionVM> onKeybindRequest, Action onBrightnessExecute = null, Action onExposureExecute = null) : this(false, optionsMode, onKeybindRequest, null, null)
		{
			this._onClose = onClose;
			this._onBrightnessExecute = onBrightnessExecute;
			this._onExposureExecute = onExposureExecute;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0001D298 File Offset: 0x0001B498
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.OptionsLbl = new TextObject("{=NqarFr4P}Options", null).ToString();
			this.CancelLbl = new TextObject("{=3CpNUnVl}Cancel", null).ToString();
			this.DoneLbl = new TextObject("{=WiNRdfsm}Done", null).ToString();
			this.ResetLbl = new TextObject("{=mAxXKaXp}Reset", null).ToString();
			this.VideoMemoryUsageName = Module.CurrentModule.GlobalTextManager.FindText("str_gpu_memory_usage", null).ToString();
			this.GameKeyOptionGroups.RefreshValues();
			this.GamepadOptions.RefreshValues();
			BrightnessOptionVM brightnessPopUp = this.BrightnessPopUp;
			if (brightnessPopUp != null)
			{
				brightnessPopUp.RefreshValues();
			}
			ExposureOptionVM exposurePopUp = this.ExposurePopUp;
			if (exposurePopUp != null)
			{
				exposurePopUp.RefreshValues();
			}
			this.UpdateVideoMemoryUsage();
			this._categories.ForEach(delegate(ViewModel g)
			{
				g.RefreshValues();
			});
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0001D38B File Offset: 0x0001B58B
		public void ExecuteCloseOptions()
		{
			if (this._onClose != null)
			{
				this._onClose();
			}
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001D3A0 File Offset: 0x0001B5A0
		protected void OnBrightnessClick()
		{
			if (this._onBrightnessExecute == null)
			{
				this.BrightnessPopUp.Visible = true;
				return;
			}
			this._onBrightnessExecute();
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0001D3C2 File Offset: 0x0001B5C2
		protected void OnExposureClick()
		{
			if (this._onExposureExecute == null)
			{
				this.ExposurePopUp.Visible = true;
				return;
			}
			this._onExposureExecute();
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0001D3E4 File Offset: 0x0001B5E4
		public ViewModel GetActiveCategory()
		{
			if (this.CategoryIndex >= 0 && this.CategoryIndex < this._categories.Count)
			{
				return this._categories[this.CategoryIndex];
			}
			return null;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0001D415 File Offset: 0x0001B615
		public int GetIndexOfCategory(ViewModel categoryVM)
		{
			return this._categories.IndexOf(categoryVM);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0001D424 File Offset: 0x0001B624
		public float GetConfig(IOptionData data)
		{
			if (!data.IsNative())
			{
				return ManagedOptions.GetConfig((ManagedOptions.ManagedOptionsType)data.GetOptionType());
			}
			NativeOptions.NativeOptionsType nativeOptionsType = (NativeOptions.NativeOptionsType)data.GetOptionType();
			if (nativeOptionsType == NativeOptions.NativeOptionsType.OverAll)
			{
				return (float)NativeConfig.AutoGFXQuality;
			}
			return NativeOptions.GetConfig(nativeOptionsType);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0001D468 File Offset: 0x0001B668
		public void SetConfig(IOptionData data, float val)
		{
			if (!this._isInitialized)
			{
				return;
			}
			this.UpdateDependentConfigs(data, val);
			this.UpdateEnabledStates();
			NativeOptions.ConfigQuality autoGFXQuality = NativeConfig.AutoGFXQuality;
			NativeOptions.ConfigQuality configQuality = this.IsManagedOptionsConflictWithOverallSettings((int)autoGFXQuality) ? NativeOptions.ConfigQuality.GFXCustom : autoGFXQuality;
			if (data.IsNative())
			{
				NativeOptions.NativeOptionsType nativeOptionsType = (NativeOptions.NativeOptionsType)data.GetOptionType();
				if (nativeOptionsType == NativeOptions.NativeOptionsType.OverAll)
				{
					if (MathF.Abs(val - (float)this._overallConfigCount) <= 0.01f)
					{
						goto IL_1AE;
					}
					Utilities.SetGraphicsPreset((int)val);
					foreach (GenericOptionDataVM genericOptionDataVM in this.VideoOptions.AllOptions)
					{
						if (!genericOptionDataVM.IsAction)
						{
							float num = genericOptionDataVM.IsNative ? this.GetDefaultOptionForOverallNativeSettings((NativeOptions.NativeOptionsType)genericOptionDataVM.GetOptionType(), (int)val) : this.GetDefaultOptionForOverallManagedSettings((ManagedOptions.ManagedOptionsType)genericOptionDataVM.GetOptionType(), (int)val);
							if (num >= 0f)
							{
								genericOptionDataVM.SetValue(num);
								genericOptionDataVM.UpdateValue();
							}
						}
					}
					using (IEnumerator<GenericOptionDataVM> enumerator = this.PerformanceOptions.AllOptions.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							GenericOptionDataVM genericOptionDataVM2 = enumerator.Current;
							float num2 = genericOptionDataVM2.IsNative ? this.GetDefaultOptionForOverallNativeSettings((NativeOptions.NativeOptionsType)genericOptionDataVM2.GetOptionType(), (int)val) : this.GetDefaultOptionForOverallManagedSettings((ManagedOptions.ManagedOptionsType)genericOptionDataVM2.GetOptionType(), (int)val);
							if (num2 >= 0f)
							{
								genericOptionDataVM2.SetValue(num2);
								genericOptionDataVM2.UpdateValue();
							}
						}
						goto IL_1AE;
					}
				}
				if (this._overallOption != null && (this._overallOption.Selector.SelectedIndex != this._overallConfigCount || configQuality != NativeOptions.ConfigQuality.GFXCustom) && OptionsProvider.GetDefaultNativeOptions().ContainsKey(nativeOptionsType))
				{
					this._overallOption.Selector.SelectedIndex = (int)configQuality;
				}
				IL_1AE:
				if (!this._isCancelling && (nativeOptionsType == NativeOptions.NativeOptionsType.SelectedAdapter || nativeOptionsType == NativeOptions.NativeOptionsType.SoundDevice || nativeOptionsType == NativeOptions.NativeOptionsType.SoundOutput))
				{
					InformationManager.ShowInquiry(new InquiryData(Module.CurrentModule.GlobalTextManager.FindText("str_option_restart_required", null).ToString(), Module.CurrentModule.GlobalTextManager.FindText("str_option_restart_required_desc", null).ToString(), true, false, Module.CurrentModule.GlobalTextManager.FindText("str_ok", null).ToString(), string.Empty, null, null, "", 0f, null, null, null), false, false);
				}
				this.UpdateVideoMemoryUsage();
				return;
			}
			ManagedOptions.ManagedOptionsType key = (ManagedOptions.ManagedOptionsType)data.GetOptionType();
			if (this._overallOption != null && (this._overallOption.Selector.SelectedIndex != this._overallConfigCount || configQuality != NativeOptions.ConfigQuality.GFXCustom) && OptionsProvider.GetDefaultManagedOptions().ContainsKey(key))
			{
				this._overallOption.Selector.SelectedIndex = (int)configQuality;
			}
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0001D71C File Offset: 0x0001B91C
		private void UpdateEnabledStates()
		{
			foreach (GenericOptionDataVM genericOptionDataVM in this._groupedCategories.SelectMany((GroupedOptionCategoryVM c) => c.AllOptions))
			{
				genericOptionDataVM.UpdateEnableState();
			}
			foreach (GenericOptionDataVM genericOptionDataVM2 in this._performanceOptionCategory.AllOptions)
			{
				genericOptionDataVM2.UpdateEnableState();
			}
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0001D7C8 File Offset: 0x0001B9C8
		private void UpdateDependentConfigs(IOptionData data, float val)
		{
			if (data.IsNative())
			{
				NativeOptions.NativeOptionsType nativeOptionsType = (NativeOptions.NativeOptionsType)data.GetOptionType();
				if (nativeOptionsType == NativeOptions.NativeOptionsType.SelectedMonitor || nativeOptionsType == NativeOptions.NativeOptionsType.DLSS)
				{
					NativeOptions.RefreshOptionsData();
					this._resolutionOption.UpdateData(false);
					this._refreshRateOption.UpdateData(false);
				}
				if (nativeOptionsType == NativeOptions.NativeOptionsType.ScreenResolution || nativeOptionsType == NativeOptions.NativeOptionsType.SelectedMonitor)
				{
					NativeOptions.RefreshOptionsData();
					this._refreshRateOption.UpdateData(false);
					if (NativeOptions.GetIsDLSSAvailable())
					{
						GenericOptionDataVM dlssOption = this._dlssOption;
						if (dlssOption == null)
						{
							return;
						}
						dlssOption.UpdateData(false);
					}
				}
			}
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0001D844 File Offset: 0x0001BA44
		private bool IsManagedOptionsConflictWithOverallSettings(int overallSettingsOption)
		{
			return this._performanceManagedOptions.Any((IOptionData o) => (float)((int)o.GetValue(false)) != this.GetDefaultOptionForOverallManagedSettings((ManagedOptions.ManagedOptionsType)o.GetOptionType(), overallSettingsOption));
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0001D87C File Offset: 0x0001BA7C
		private float GetDefaultOptionForOverallNativeSettings(NativeOptions.NativeOptionsType option, int overallSettingsOption)
		{
			if (overallSettingsOption >= this._overallConfigCount || overallSettingsOption < 0)
			{
				return -1f;
			}
			float[] array;
			if (OptionsProvider.GetDefaultNativeOptions().TryGetValue(option, out array))
			{
				return array[overallSettingsOption];
			}
			return -1f;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0001D8B4 File Offset: 0x0001BAB4
		private float GetDefaultOptionForOverallManagedSettings(ManagedOptions.ManagedOptionsType option, int overallSettingsOption)
		{
			if (overallSettingsOption >= this._overallConfigCount || overallSettingsOption < 0)
			{
				return -1f;
			}
			float[] array;
			if (OptionsProvider.GetDefaultManagedOptions().TryGetValue(option, out array))
			{
				return array[overallSettingsOption];
			}
			return -1f;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0001D8EC File Offset: 0x0001BAEC
		private bool IsCategoryAvailable(ViewModel category)
		{
			GameKeyOptionCategoryVM gameKeyOptionCategoryVM;
			if ((gameKeyOptionCategoryVM = (category as GameKeyOptionCategoryVM)) != null)
			{
				return gameKeyOptionCategoryVM.IsEnabled;
			}
			GamepadOptionCategoryVM gamepadOptionCategoryVM;
			if ((gamepadOptionCategoryVM = (category as GamepadOptionCategoryVM)) != null)
			{
				return gamepadOptionCategoryVM.IsEnabled;
			}
			GroupedOptionCategoryVM groupedOptionCategoryVM;
			return (groupedOptionCategoryVM = (category as GroupedOptionCategoryVM)) == null || groupedOptionCategoryVM.IsEnabled;
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0001D92D File Offset: 0x0001BB2D
		private int GetPreviousAvailableCategoryIndex(int currentCategoryIndex)
		{
			if (--currentCategoryIndex < 0)
			{
				currentCategoryIndex = this._categories.Count - 1;
			}
			if (!this.IsCategoryAvailable(this._categories[currentCategoryIndex]))
			{
				return this.GetPreviousAvailableCategoryIndex(currentCategoryIndex);
			}
			return currentCategoryIndex;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0001D964 File Offset: 0x0001BB64
		public void SelectPreviousCategory()
		{
			int previousAvailableCategoryIndex = this.GetPreviousAvailableCategoryIndex(this.CategoryIndex);
			this.SetSelectedCategory(previousAvailableCategoryIndex);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0001D985 File Offset: 0x0001BB85
		private int GetNextAvailableCategoryIndex(int currentCategoryIndex)
		{
			if (++currentCategoryIndex >= this._categories.Count)
			{
				currentCategoryIndex = 0;
			}
			if (!this.IsCategoryAvailable(this._categories[currentCategoryIndex]))
			{
				return this.GetNextAvailableCategoryIndex(currentCategoryIndex);
			}
			return currentCategoryIndex;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0001D9BC File Offset: 0x0001BBBC
		public void SelectNextCategory()
		{
			int nextAvailableCategoryIndex = this.GetNextAvailableCategoryIndex(this.CategoryIndex);
			this.SetSelectedCategory(nextAvailableCategoryIndex);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0001D9DD File Offset: 0x0001BBDD
		private void SetSelectedCategory(int index)
		{
			this.CategoryIndex = index;
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0001D9E6 File Offset: 0x0001BBE6
		private void OnGamepadActiveStateChanged()
		{
			if (!this.IsCategoryAvailable(this._categories[this.CategoryIndex]))
			{
				if (this.GetNextAvailableCategoryIndex(this.CategoryIndex) > this.CategoryIndex)
				{
					this.SelectNextCategory();
					return;
				}
				this.SelectPreviousCategory();
			}
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0001DA24 File Offset: 0x0001BC24
		public override void OnFinalize()
		{
			base.OnFinalize();
			InputKeyItemVM doneInputKey = this.DoneInputKey;
			if (doneInputKey != null)
			{
				doneInputKey.OnFinalize();
			}
			InputKeyItemVM cancelInputKey = this.CancelInputKey;
			if (cancelInputKey != null)
			{
				cancelInputKey.OnFinalize();
			}
			InputKeyItemVM previousTabInputKey = this.PreviousTabInputKey;
			if (previousTabInputKey != null)
			{
				previousTabInputKey.OnFinalize();
			}
			InputKeyItemVM nextTabInputKey = this.NextTabInputKey;
			if (nextTabInputKey != null)
			{
				nextTabInputKey.OnFinalize();
			}
			InputKeyItemVM resetInputKey = this.ResetInputKey;
			if (resetInputKey != null)
			{
				resetInputKey.OnFinalize();
			}
			GamepadOptionCategoryVM gamepadOptions = this.GamepadOptions;
			if (gamepadOptions != null)
			{
				gamepadOptions.OnFinalize();
			}
			GameKeyOptionCategoryVM gameKeyOptionGroups = this.GameKeyOptionGroups;
			if (gameKeyOptionGroups != null)
			{
				gameKeyOptionGroups.OnFinalize();
			}
			ExposureOptionVM exposurePopUp = this.ExposurePopUp;
			if (exposurePopUp != null)
			{
				exposurePopUp.OnFinalize();
			}
			BrightnessOptionVM brightnessPopUp = this.BrightnessPopUp;
			if (brightnessPopUp != null)
			{
				brightnessPopUp.OnFinalize();
			}
			Input.OnGamepadActiveStateChanged = (Action)Delegate.Remove(Input.OnGamepadActiveStateChanged, new Action(this.OnGamepadActiveStateChanged));
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0001DAF0 File Offset: 0x0001BCF0
		protected void HandleCancel(bool autoHandleClose)
		{
			this._isCancelling = true;
			this._groupedCategories.ForEach(delegate(GroupedOptionCategoryVM c)
			{
				c.Cancel();
			});
			this._gameKeyCategory.Cancel();
			this._performanceOptionCategory.Cancel();
			this.CloseScreen(autoHandleClose);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0001DB4B File Offset: 0x0001BD4B
		private void CloseScreen(bool autoHandleClose)
		{
			this.ExecuteCloseOptions();
			if (autoHandleClose)
			{
				if (Game.Current != null)
				{
					Game.Current.GameStateManager.UnregisterActiveStateDisableRequest(this);
				}
				ScreenManager.PopScreen();
			}
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0001DB74 File Offset: 0x0001BD74
		public void ExecuteCancel()
		{
			if (this.IsOptionsChanged())
			{
				string text = new TextObject("{=peUP9ZZj}Are you sure? You made some changes and they will be lost.", null).ToString();
				InformationManager.ShowInquiry(new InquiryData("", text, true, true, new TextObject("{=aeouhelq}Yes", null).ToString(), new TextObject("{=8OkPHu4f}No", null).ToString(), delegate()
				{
					this.HandleCancel(this._autoHandleClose);
				}, null, "", 0f, null, null, null), false, false);
				return;
			}
			this.HandleCancel(this._autoHandleClose);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0001DBF8 File Offset: 0x0001BDF8
		protected void OnDone()
		{
			this.ApplyChangedOptions();
			GenericOptionDataVM monitorOption = this._monitorOption;
			if (monitorOption == null || !monitorOption.IsChanged())
			{
				GenericOptionDataVM resolutionOption = this._resolutionOption;
				if (resolutionOption == null || !resolutionOption.IsChanged())
				{
					GenericOptionDataVM refreshRateOption = this._refreshRateOption;
					if (refreshRateOption == null || !refreshRateOption.IsChanged())
					{
						GenericOptionDataVM displayModeOption = this._displayModeOption;
						if (displayModeOption == null || !displayModeOption.IsChanged())
						{
							this.CloseScreen(this._autoHandleClose);
							return;
						}
					}
				}
			}
			InformationManager.ShowInquiry(new InquiryData(new TextObject("{=lCZMJt2k}Video Options Have Been Changed", null).ToString(), new TextObject("{=pK4EyTZC}Do you want to keep these settings?", null).ToString(), true, true, Module.CurrentModule.GlobalTextManager.FindText("str_ok", null).ToString(), new TextObject("{=3CpNUnVl}Cancel", null).ToString(), delegate()
			{
				GenericOptionDataVM monitorOption2 = this._monitorOption;
				if (monitorOption2 != null)
				{
					monitorOption2.ApplyValue();
				}
				GenericOptionDataVM resolutionOption2 = this._resolutionOption;
				if (resolutionOption2 != null)
				{
					resolutionOption2.ApplyValue();
				}
				GenericOptionDataVM refreshRateOption2 = this._refreshRateOption;
				if (refreshRateOption2 != null)
				{
					refreshRateOption2.ApplyValue();
				}
				GenericOptionDataVM displayModeOption2 = this._displayModeOption;
				if (displayModeOption2 != null)
				{
					displayModeOption2.ApplyValue();
				}
				this.CloseScreen(this._autoHandleClose);
			}, delegate()
			{
				GenericOptionDataVM monitorOption2 = this._monitorOption;
				if (monitorOption2 != null)
				{
					monitorOption2.Cancel();
				}
				GenericOptionDataVM resolutionOption2 = this._resolutionOption;
				if (resolutionOption2 != null)
				{
					resolutionOption2.Cancel();
				}
				GenericOptionDataVM refreshRateOption2 = this._refreshRateOption;
				if (refreshRateOption2 != null)
				{
					refreshRateOption2.Cancel();
				}
				GenericOptionDataVM displayModeOption2 = this._displayModeOption;
				if (displayModeOption2 != null)
				{
					displayModeOption2.Cancel();
				}
				NativeOptions.ApplyConfigChanges(true);
			}, "", 10f, delegate()
			{
				GenericOptionDataVM monitorOption2 = this._monitorOption;
				if (monitorOption2 != null)
				{
					monitorOption2.Cancel();
				}
				GenericOptionDataVM resolutionOption2 = this._resolutionOption;
				if (resolutionOption2 != null)
				{
					resolutionOption2.Cancel();
				}
				GenericOptionDataVM refreshRateOption2 = this._refreshRateOption;
				if (refreshRateOption2 != null)
				{
					refreshRateOption2.Cancel();
				}
				GenericOptionDataVM displayModeOption2 = this._displayModeOption;
				if (displayModeOption2 != null)
				{
					displayModeOption2.Cancel();
				}
				NativeOptions.ApplyConfigChanges(true);
			}, null, null), false, false);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0001DCF4 File Offset: 0x0001BEF4
		private void ApplyChangedOptions()
		{
			int trail_amount = 0;
			int texture_budget = 0;
			int hdr = 0;
			int dof_mode = 0;
			int motion_blur = 0;
			int ssr = 0;
			int num = 0;
			int texture_filtering = 0;
			int sharpen_amount = 0;
			int dynamic_resolution_target = 0;
			IEnumerable<GenericOptionDataVM> enumerable = this._groupedCategories.SelectMany((GroupedOptionCategoryVM c) => c.AllOptions);
			foreach (GenericOptionDataVM genericOptionDataVM in enumerable)
			{
				genericOptionDataVM.UpdateValue();
				if (genericOptionDataVM.IsNative && !genericOptionDataVM.GetOptionData().IsAction())
				{
					NativeOptions.NativeOptionsType nativeOptionsType = (NativeOptions.NativeOptionsType)genericOptionDataVM.GetOptionType();
					if (nativeOptionsType <= NativeOptions.NativeOptionsType.TextureQuality)
					{
						if (nativeOptionsType <= NativeOptions.NativeOptionsType.SelectedMonitor)
						{
							if (nativeOptionsType == NativeOptions.NativeOptionsType.TrailAmount)
							{
								trail_amount = (genericOptionDataVM.IsChanged() ? 1 : 0);
								continue;
							}
							if (nativeOptionsType - NativeOptions.NativeOptionsType.DisplayMode > 1)
							{
								continue;
							}
						}
						else if (nativeOptionsType - NativeOptions.NativeOptionsType.ScreenResolution > 1)
						{
							if (nativeOptionsType == NativeOptions.NativeOptionsType.TextureBudget)
							{
								texture_budget = (genericOptionDataVM.IsChanged() ? 1 : 0);
								continue;
							}
							if (nativeOptionsType != NativeOptions.NativeOptionsType.TextureQuality)
							{
								continue;
							}
							texture_budget = (genericOptionDataVM.IsChanged() ? 1 : 0);
							continue;
						}
					}
					else if (nativeOptionsType <= NativeOptions.NativeOptionsType.SSR)
					{
						if (nativeOptionsType == NativeOptions.NativeOptionsType.TextureFiltering)
						{
							texture_filtering = (genericOptionDataVM.IsChanged() ? 1 : 0);
							continue;
						}
						if (nativeOptionsType == NativeOptions.NativeOptionsType.DepthOfField)
						{
							dof_mode = (genericOptionDataVM.IsChanged() ? 1 : 0);
							continue;
						}
						if (nativeOptionsType != NativeOptions.NativeOptionsType.SSR)
						{
							continue;
						}
						ssr = (genericOptionDataVM.IsChanged() ? 1 : 0);
						continue;
					}
					else
					{
						switch (nativeOptionsType)
						{
						case NativeOptions.NativeOptionsType.Bloom:
							hdr = (genericOptionDataVM.IsChanged() ? 1 : 0);
							continue;
						case NativeOptions.NativeOptionsType.FilmGrain:
							continue;
						case NativeOptions.NativeOptionsType.MotionBlur:
							motion_blur = (genericOptionDataVM.IsChanged() ? 1 : 0);
							continue;
						case NativeOptions.NativeOptionsType.SharpenAmount:
							sharpen_amount = (genericOptionDataVM.IsChanged() ? 1 : 0);
							continue;
						default:
							if (nativeOptionsType != NativeOptions.NativeOptionsType.DynamicResolution)
							{
								if (nativeOptionsType != NativeOptions.NativeOptionsType.DynamicResolutionTarget)
								{
									continue;
								}
								dynamic_resolution_target = (genericOptionDataVM.IsChanged() ? 1 : 0);
								continue;
							}
							break;
						}
					}
					num = ((num == 1 || genericOptionDataVM.IsChanged()) ? 1 : 0);
				}
			}
			NativeOptions.Apply(texture_budget, sharpen_amount, hdr, dof_mode, motion_blur, ssr, num, texture_filtering, trail_amount, dynamic_resolution_target);
			SaveResult saveResult = NativeOptions.SaveConfig();
			SaveResult saveResult2 = ManagedOptions.SaveConfig();
			if (saveResult != SaveResult.Success || saveResult2 != SaveResult.Success)
			{
				SaveResult saveResult3 = (saveResult != SaveResult.Success) ? saveResult : saveResult2;
				InformationManager.ShowInquiry(new InquiryData(new TextObject("{=oZrVNUOk}Error", null).ToString(), Module.CurrentModule.GlobalTextManager.FindText("str_config_save_result", saveResult3.ToString()).ToString(), true, false, Module.CurrentModule.GlobalTextManager.FindText("str_ok", null).ToString(), null, null, null, "", 0f, null, null, null), false, false);
			}
			bool throwEvent = this.GameKeyOptionGroups.IsChanged();
			this.GameKeyOptionGroups.ApplyValues();
			HotKeyManager.Save(throwEvent);
			enumerable = enumerable.Concat(this._performanceOptionCategory.AllOptions);
			enumerable = from x in enumerable
			where x != this._monitorOption && x != this._resolutionOption && x != this._refreshRateOption && x != this._displayModeOption
			select x;
			foreach (GenericOptionDataVM genericOptionDataVM2 in enumerable)
			{
				genericOptionDataVM2.ApplyValue();
			}
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0001E050 File Offset: 0x0001C250
		protected void ExecuteBenchmark()
		{
			GameStateManager.StateActivateCommand = "state_string.benchmark_start";
			bool flag;
			CommandLineFunctionality.CallFunction("benchmark.cpu_benchmark", "", out flag);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0001E07C File Offset: 0x0001C27C
		public void ExecuteDone()
		{
			bool flag = false;
			int currentEstimatedGPUMemoryCostMB = Utilities.GetCurrentEstimatedGPUMemoryCostMB();
			int gpumemoryMB = Utilities.GetGPUMemoryMB();
			if (!flag && gpumemoryMB <= currentEstimatedGPUMemoryCostMB)
			{
				InformationManager.ShowInquiry(new InquiryData(Module.CurrentModule.GlobalTextManager.FindText("str_gpu_memory_caution_title", null).ToString(), Module.CurrentModule.GlobalTextManager.FindText("str_gpu_memory_caution_text", null).ToString(), true, false, Module.CurrentModule.GlobalTextManager.FindText("str_ok", null).ToString(), null, delegate()
				{
					this.OnDone();
				}, null, "", 0f, null, null, null), false, false);
				return;
			}
			this.OnDone();
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0001E11C File Offset: 0x0001C31C
		protected void ExecuteReset()
		{
			InformationManager.ShowInquiry(new InquiryData("", new TextObject("{=cDzWYQrz}Reset to default settings?", null).ToString(), true, true, new TextObject("{=oHaWR73d}Ok", null).ToString(), new TextObject("{=3CpNUnVl}Cancel", null).ToString(), new Action(this.OnResetToDefaults), null, "", 0f, null, null, null), false, false);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0001E188 File Offset: 0x0001C388
		public bool IsOptionsChanged()
		{
			return (this._groupedCategories.Any((GroupedOptionCategoryVM c) => c.IsChanged()) || this.GameKeyOptionGroups.IsChanged()) | this._performanceOptionCategory.IsChanged();
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0001E1DB File Offset: 0x0001C3DB
		private void OnResetToDefaults()
		{
			this._groupedCategories.ForEach(delegate(GroupedOptionCategoryVM g)
			{
				g.ResetData();
			});
			this._performanceOptionCategory.ResetData();
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0001E214 File Offset: 0x0001C414
		private void UpdateVideoMemoryUsage()
		{
			int currentEstimatedGPUMemoryCostMB = Utilities.GetCurrentEstimatedGPUMemoryCostMB();
			int gpumemoryMB = Utilities.GetGPUMemoryMB();
			this.VideoMemoryUsageNormalized = (float)currentEstimatedGPUMemoryCostMB / (float)gpumemoryMB;
			TextObject textObject = Module.CurrentModule.GlobalTextManager.FindText("str_gpu_memory_usage_value_text", null);
			textObject.SetTextVariable("ESTIMATED", currentEstimatedGPUMemoryCostMB);
			textObject.SetTextVariable("TOTAL", gpumemoryMB);
			this.VideoMemoryUsageText = textObject.ToString();
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0001E274 File Offset: 0x0001C474
		internal GenericOptionDataVM GetOptionItem(IOptionData option)
		{
			bool flag = Input.ControllerType.IsPlaystation();
			MBTextManager.SetTextVariable("IS_PLAYSTATION", flag ? 1 : 0);
			if (!option.IsAction())
			{
				string text = option.IsNative() ? ((NativeOptions.NativeOptionsType)option.GetOptionType()).ToString() : ((ManagedOptions.ManagedOptionsType)option.GetOptionType()).ToString();
				TextObject name = Module.CurrentModule.GlobalTextManager.FindText("str_options_type", text);
				TextObject textObject = Module.CurrentModule.GlobalTextManager.FindText("str_options_description", text);
				textObject.SetTextVariable("newline", "\n");
				if (option is IBooleanOptionData)
				{
					return new BooleanOptionDataVM(this, option as IBooleanOptionData, name, textObject)
					{
						ImageIDs = new string[]
						{
							text + "_0",
							text + "_1"
						}
					};
				}
				if (option is INumericOptionData)
				{
					return new NumericOptionDataVM(this, option as INumericOptionData, name, textObject);
				}
				if (option is ISelectionOptionData)
				{
					ISelectionOptionData selectionOptionData = option as ISelectionOptionData;
					StringOptionDataVM stringOptionDataVM = new StringOptionDataVM(this, selectionOptionData, name, textObject);
					string[] array = new string[selectionOptionData.GetSelectableOptionsLimit()];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = text + "_" + i;
					}
					stringOptionDataVM.ImageIDs = array;
					return stringOptionDataVM;
				}
				ActionOptionData actionOptionData;
				if ((actionOptionData = (option as ActionOptionData)) != null)
				{
					TextObject optionActionName = Module.CurrentModule.GlobalTextManager.FindText("str_options_type_action", text);
					return new ActionOptionDataVM(actionOptionData.OnAction, this, actionOptionData, name, optionActionName, textObject);
				}
				Debug.FailedAssert("Given option data does not match with any option type!", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.ViewModelCollection\\GameOptions\\OptionsVM.cs", "GetOptionItem", 902);
				return null;
			}
			else
			{
				ActionOptionData actionOptionData2;
				if ((actionOptionData2 = (option as ActionOptionData)) != null)
				{
					string variation = option.GetOptionType() as string;
					TextObject optionActionName2 = Module.CurrentModule.GlobalTextManager.FindText("str_options_type_action", variation);
					TextObject name2 = Module.CurrentModule.GlobalTextManager.FindText("str_options_type", variation);
					TextObject textObject2 = Module.CurrentModule.GlobalTextManager.FindText("str_options_description", variation);
					textObject2.SetTextVariable("newline", "\n");
					return new ActionOptionDataVM(actionOptionData2.OnAction, this, actionOptionData2, name2, optionActionName2, textObject2);
				}
				Debug.FailedAssert("Given option data does not match with any option type!", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.ViewModelCollection\\GameOptions\\OptionsVM.cs", "GetOptionItem", 925);
				return null;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x0001E4D3 File Offset: 0x0001C6D3
		// (set) Token: 0x060007D2 RID: 2002 RVA: 0x0001E4DB File Offset: 0x0001C6DB
		[DataSourceProperty]
		public int CategoryIndex
		{
			get
			{
				return this._categoryIndex;
			}
			set
			{
				if (value != this._categoryIndex)
				{
					this._categoryIndex = value;
					base.OnPropertyChangedWithValue(value, "CategoryIndex");
				}
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x0001E4F9 File Offset: 0x0001C6F9
		// (set) Token: 0x060007D4 RID: 2004 RVA: 0x0001E501 File Offset: 0x0001C701
		[DataSourceProperty]
		public string OptionsLbl
		{
			get
			{
				return this._optionsLbl;
			}
			set
			{
				if (value != this._optionsLbl)
				{
					this._optionsLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "OptionsLbl");
				}
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x0001E524 File Offset: 0x0001C724
		// (set) Token: 0x060007D6 RID: 2006 RVA: 0x0001E52C File Offset: 0x0001C72C
		[DataSourceProperty]
		public string CancelLbl
		{
			get
			{
				return this._cancelLbl;
			}
			set
			{
				if (value != this._cancelLbl)
				{
					this._cancelLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "CancelLbl");
				}
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x0001E54F File Offset: 0x0001C74F
		// (set) Token: 0x060007D8 RID: 2008 RVA: 0x0001E557 File Offset: 0x0001C757
		[DataSourceProperty]
		public string DoneLbl
		{
			get
			{
				return this._doneLbl;
			}
			set
			{
				if (value != this._doneLbl)
				{
					this._doneLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "DoneLbl");
				}
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x0001E57A File Offset: 0x0001C77A
		// (set) Token: 0x060007DA RID: 2010 RVA: 0x0001E582 File Offset: 0x0001C782
		[DataSourceProperty]
		public string ResetLbl
		{
			get
			{
				return this._resetLbl;
			}
			set
			{
				if (value != this._resetLbl)
				{
					this._resetLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "ResetLbl");
				}
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x0001E5A5 File Offset: 0x0001C7A5
		// (set) Token: 0x060007DC RID: 2012 RVA: 0x0001E5AD File Offset: 0x0001C7AD
		[DataSourceProperty]
		public string GameVersionText
		{
			get
			{
				return this._gameVersionText;
			}
			set
			{
				if (value != this._gameVersionText)
				{
					this._gameVersionText = value;
					base.OnPropertyChangedWithValue<string>(value, "GameVersionText");
				}
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x0001E5D0 File Offset: 0x0001C7D0
		// (set) Token: 0x060007DE RID: 2014 RVA: 0x0001E5D8 File Offset: 0x0001C7D8
		[DataSourceProperty]
		public bool IsConsole
		{
			get
			{
				return this._isConsole;
			}
			set
			{
				if (value != this._isConsole)
				{
					this._isConsole = value;
					base.OnPropertyChangedWithValue(value, "IsConsole");
				}
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x0001E5F6 File Offset: 0x0001C7F6
		// (set) Token: 0x060007E0 RID: 2016 RVA: 0x0001E5FE File Offset: 0x0001C7FE
		public bool IsDevelopmentMode
		{
			get
			{
				return this._isDevelopmentMode;
			}
			set
			{
				if (value != this._isDevelopmentMode)
				{
					this._isDevelopmentMode = value;
					base.OnPropertyChangedWithValue(value, "IsDevelopmentMode");
				}
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x0001E61C File Offset: 0x0001C81C
		// (set) Token: 0x060007E2 RID: 2018 RVA: 0x0001E624 File Offset: 0x0001C824
		[DataSourceProperty]
		public string VideoMemoryUsageName
		{
			get
			{
				return this._videoMemoryUsageName;
			}
			set
			{
				if (this._videoMemoryUsageName != value)
				{
					this._videoMemoryUsageName = value;
					base.OnPropertyChangedWithValue<string>(value, "VideoMemoryUsageName");
				}
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x0001E647 File Offset: 0x0001C847
		// (set) Token: 0x060007E4 RID: 2020 RVA: 0x0001E64F File Offset: 0x0001C84F
		[DataSourceProperty]
		public string VideoMemoryUsageText
		{
			get
			{
				return this._videoMemoryUsageText;
			}
			set
			{
				if (this._videoMemoryUsageText != value)
				{
					this._videoMemoryUsageText = value;
					base.OnPropertyChangedWithValue<string>(value, "VideoMemoryUsageText");
				}
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x0001E672 File Offset: 0x0001C872
		// (set) Token: 0x060007E6 RID: 2022 RVA: 0x0001E67A File Offset: 0x0001C87A
		[DataSourceProperty]
		public float VideoMemoryUsageNormalized
		{
			get
			{
				return this._videoMemoryUsageNormalized;
			}
			set
			{
				if (this._videoMemoryUsageNormalized != value)
				{
					this._videoMemoryUsageNormalized = value;
					base.OnPropertyChangedWithValue(value, "VideoMemoryUsageNormalized");
				}
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x0001E698 File Offset: 0x0001C898
		[DataSourceProperty]
		public GameKeyOptionCategoryVM GameKeyOptionGroups
		{
			get
			{
				return this._gameKeyCategory;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x0001E6A0 File Offset: 0x0001C8A0
		[DataSourceProperty]
		public GamepadOptionCategoryVM GamepadOptions
		{
			get
			{
				return this._gamepadCategory;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x0001E6A8 File Offset: 0x0001C8A8
		[DataSourceProperty]
		public GroupedOptionCategoryVM PerformanceOptions
		{
			get
			{
				return this._performanceOptionCategory;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x0001E6B0 File Offset: 0x0001C8B0
		[DataSourceProperty]
		public GroupedOptionCategoryVM AudioOptions
		{
			get
			{
				return this._audioOptionCategory;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x0001E6B8 File Offset: 0x0001C8B8
		[DataSourceProperty]
		public GroupedOptionCategoryVM GameplayOptions
		{
			get
			{
				return this._gameplayOptionCategory;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x0001E6C0 File Offset: 0x0001C8C0
		[DataSourceProperty]
		public GroupedOptionCategoryVM VideoOptions
		{
			get
			{
				return this._videoOptionCategory;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x0001E6C8 File Offset: 0x0001C8C8
		// (set) Token: 0x060007EE RID: 2030 RVA: 0x0001E6D0 File Offset: 0x0001C8D0
		[DataSourceProperty]
		public BrightnessOptionVM BrightnessPopUp
		{
			get
			{
				return this._brightnessPopUp;
			}
			set
			{
				if (value != this._brightnessPopUp)
				{
					this._brightnessPopUp = value;
					base.OnPropertyChangedWithValue<BrightnessOptionVM>(value, "BrightnessPopUp");
				}
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x0001E6EE File Offset: 0x0001C8EE
		// (set) Token: 0x060007F0 RID: 2032 RVA: 0x0001E6F6 File Offset: 0x0001C8F6
		[DataSourceProperty]
		public ExposureOptionVM ExposurePopUp
		{
			get
			{
				return this._exposurePopUp;
			}
			set
			{
				if (value != this._exposurePopUp)
				{
					this._exposurePopUp = value;
					base.OnPropertyChangedWithValue<ExposureOptionVM>(value, "ExposurePopUp");
				}
			}
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0001E714 File Offset: 0x0001C914
		public void SetDoneInputKey(HotKey hotkey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0001E723 File Offset: 0x0001C923
		public void SetCancelInputKey(HotKey hotkey)
		{
			this.CancelInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0001E732 File Offset: 0x0001C932
		public void SetPreviousTabInputKey(HotKey hotkey)
		{
			this.PreviousTabInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0001E741 File Offset: 0x0001C941
		public void SetNextTabInputKey(HotKey hotkey)
		{
			this.NextTabInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0001E750 File Offset: 0x0001C950
		public void SetResetInputKey(HotKey hotkey)
		{
			this.ResetInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x0001E75F File Offset: 0x0001C95F
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x0001E767 File Offset: 0x0001C967
		public InputKeyItemVM DoneInputKey
		{
			get
			{
				return this._doneInputKey;
			}
			set
			{
				if (value != this._doneInputKey)
				{
					this._doneInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "DoneInputKey");
				}
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x0001E785 File Offset: 0x0001C985
		// (set) Token: 0x060007F9 RID: 2041 RVA: 0x0001E78D File Offset: 0x0001C98D
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

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x0001E7AB File Offset: 0x0001C9AB
		// (set) Token: 0x060007FB RID: 2043 RVA: 0x0001E7B3 File Offset: 0x0001C9B3
		[DataSourceProperty]
		public InputKeyItemVM PreviousTabInputKey
		{
			get
			{
				return this._previousTabInputKey;
			}
			set
			{
				if (value != this._previousTabInputKey)
				{
					this._previousTabInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "PreviousTabInputKey");
				}
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x0001E7D1 File Offset: 0x0001C9D1
		// (set) Token: 0x060007FD RID: 2045 RVA: 0x0001E7D9 File Offset: 0x0001C9D9
		[DataSourceProperty]
		public InputKeyItemVM NextTabInputKey
		{
			get
			{
				return this._nextTabInputKey;
			}
			set
			{
				if (value != this._nextTabInputKey)
				{
					this._nextTabInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "NextTabInputKey");
				}
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x0001E7F7 File Offset: 0x0001C9F7
		// (set) Token: 0x060007FF RID: 2047 RVA: 0x0001E7FF File Offset: 0x0001C9FF
		[DataSourceProperty]
		public InputKeyItemVM ResetInputKey
		{
			get
			{
				return this._resetInputKey;
			}
			set
			{
				if (value != this._resetInputKey)
				{
					this._resetInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "ResetInputKey");
				}
			}
		}

		// Token: 0x04000398 RID: 920
		private readonly Action _onClose;

		// Token: 0x04000399 RID: 921
		private readonly Action _onBrightnessExecute;

		// Token: 0x0400039A RID: 922
		private readonly Action _onExposureExecute;

		// Token: 0x0400039B RID: 923
		private readonly StringOptionDataVM _overallOption;

		// Token: 0x0400039C RID: 924
		private readonly GenericOptionDataVM _dlssOption;

		// Token: 0x0400039D RID: 925
		private readonly List<GenericOptionDataVM> _dynamicResolutionOptions = new List<GenericOptionDataVM>();

		// Token: 0x0400039E RID: 926
		private readonly GenericOptionDataVM _refreshRateOption;

		// Token: 0x0400039F RID: 927
		private readonly GenericOptionDataVM _resolutionOption;

		// Token: 0x040003A0 RID: 928
		private readonly GenericOptionDataVM _monitorOption;

		// Token: 0x040003A1 RID: 929
		private readonly GenericOptionDataVM _displayModeOption;

		// Token: 0x040003A2 RID: 930
		private readonly bool _autoHandleClose;

		// Token: 0x040003A3 RID: 931
		protected readonly GroupedOptionCategoryVM _gameplayOptionCategory;

		// Token: 0x040003A4 RID: 932
		private readonly GroupedOptionCategoryVM _audioOptionCategory;

		// Token: 0x040003A5 RID: 933
		private readonly GroupedOptionCategoryVM _videoOptionCategory;

		// Token: 0x040003A6 RID: 934
		protected readonly GameKeyOptionCategoryVM _gameKeyCategory;

		// Token: 0x040003A7 RID: 935
		private readonly GamepadOptionCategoryVM _gamepadCategory;

		// Token: 0x040003A8 RID: 936
		private bool _isInitialized;

		// Token: 0x040003A9 RID: 937
		private Action<KeyOptionVM> _onKeybindRequest;

		// Token: 0x040003AA RID: 938
		protected readonly GroupedOptionCategoryVM _performanceOptionCategory;

		// Token: 0x040003AB RID: 939
		private readonly int _overallConfigCount = NativeSelectionOptionData.GetOptionsLimit(NativeOptions.NativeOptionsType.OverAll) - 1;

		// Token: 0x040003AC RID: 940
		private bool _isCancelling;

		// Token: 0x040003AD RID: 941
		private readonly IEnumerable<IOptionData> _performanceManagedOptions;

		// Token: 0x040003AE RID: 942
		protected readonly List<GroupedOptionCategoryVM> _groupedCategories;

		// Token: 0x040003AF RID: 943
		private readonly List<ViewModel> _categories;

		// Token: 0x040003B0 RID: 944
		private int _categoryIndex;

		// Token: 0x040003B1 RID: 945
		private string _optionsLbl;

		// Token: 0x040003B2 RID: 946
		private string _cancelLbl;

		// Token: 0x040003B3 RID: 947
		private string _doneLbl;

		// Token: 0x040003B4 RID: 948
		private string _resetLbl;

		// Token: 0x040003B5 RID: 949
		private string _gameVersionText;

		// Token: 0x040003B6 RID: 950
		private bool _isDevelopmentMode;

		// Token: 0x040003B7 RID: 951
		private bool _isConsole;

		// Token: 0x040003B8 RID: 952
		private float _videoMemoryUsageNormalized;

		// Token: 0x040003B9 RID: 953
		private string _videoMemoryUsageName;

		// Token: 0x040003BA RID: 954
		private string _videoMemoryUsageText;

		// Token: 0x040003BB RID: 955
		private BrightnessOptionVM _brightnessPopUp;

		// Token: 0x040003BC RID: 956
		private ExposureOptionVM _exposurePopUp;

		// Token: 0x040003BD RID: 957
		private InputKeyItemVM _doneInputKey;

		// Token: 0x040003BE RID: 958
		private InputKeyItemVM _cancelInputKey;

		// Token: 0x040003BF RID: 959
		private InputKeyItemVM _previousTabInputKey;

		// Token: 0x040003C0 RID: 960
		private InputKeyItemVM _nextTabInputKey;

		// Token: 0x040003C1 RID: 961
		private InputKeyItemVM _resetInputKey;

		// Token: 0x020000E1 RID: 225
		public enum OptionsDataType
		{
			// Token: 0x0400062F RID: 1583
			None = -1,
			// Token: 0x04000630 RID: 1584
			BooleanOption,
			// Token: 0x04000631 RID: 1585
			NumericOption,
			// Token: 0x04000632 RID: 1586
			MultipleSelectionOption = 3,
			// Token: 0x04000633 RID: 1587
			InputOption,
			// Token: 0x04000634 RID: 1588
			ActionOption
		}

		// Token: 0x020000E2 RID: 226
		public enum OptionsMode
		{
			// Token: 0x04000636 RID: 1590
			MainMenu,
			// Token: 0x04000637 RID: 1591
			Singleplayer,
			// Token: 0x04000638 RID: 1592
			Multiplayer
		}
	}
}
