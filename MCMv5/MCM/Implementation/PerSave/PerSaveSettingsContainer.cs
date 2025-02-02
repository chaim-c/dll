using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection;
using BUTR.DependencyInjection.Logger;
using HarmonyLib.BUTR.Extensions;
using MCM.Abstractions;
using MCM.Abstractions.Base;
using MCM.Abstractions.Base.PerSave;
using MCM.Abstractions.GameFeatures;
using MCM.Abstractions.PerSave;
using MCM.Common;

namespace MCM.Implementation.PerSave
{
	// Token: 0x02000036 RID: 54
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class PerSaveSettingsContainer : BaseSettingsContainer<PerSaveSettings>, IPerSaveSettingsContainer, ISettingsContainer, ISettingsContainerHasSettingsDefinitions, ISettingsContainerCanOverride, ISettingsContainerCanReset, ISettingsContainerPresets, ISettingsContainerHasUnavailable, ISettingsContainerHasSettingsPack, ISettingsContainerCanInvalidateCache
	{
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600015F RID: 351 RVA: 0x000067E8 File Offset: 0x000049E8
		// (remove) Token: 0x06000160 RID: 352 RVA: 0x00006820 File Offset: 0x00004A20
		[Nullable(2)]
		[method: NullableContext(2)]
		[Nullable(2)]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action InstanceCacheInvalidated;

		// Token: 0x06000161 RID: 353 RVA: 0x00006858 File Offset: 0x00004A58
		[NullableContext(1)]
		public PerSaveSettingsContainer(IBUTRLogger<PerSaveSettingsContainer> logger, IGameEventListener gameEventListener)
		{
			this._logger = logger;
			this._gameEventListener = gameEventListener;
			this._gameEventListener.GameStarted += this.GameStarted;
			this._gameEventListener.GameEnded += this.GameEnded;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000068AC File Offset: 0x00004AAC
		[NullableContext(2)]
		protected override void RegisterSettings(PerSaveSettings perSaveSettings)
		{
			bool flag = GenericServiceProvider.GameScopeServiceProvider == null;
			if (!flag)
			{
				IPerSaveSettingsProvider behavior = GenericServiceProvider.GetService<IPerSaveSettingsProvider>();
				bool flag2 = behavior == null;
				if (!flag2)
				{
					bool flag3 = perSaveSettings == null;
					if (!flag3)
					{
						this.LoadedSettings.Add(perSaveSettings.Id, perSaveSettings);
						behavior.LoadSettings(perSaveSettings);
						perSaveSettings.OnPropertyChanged("LOADING_COMPLETE");
					}
				}
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000690C File Offset: 0x00004B0C
		[NullableContext(1)]
		public override bool SaveSettings(BaseSettings settings)
		{
			bool flag = GenericServiceProvider.GameScopeServiceProvider == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				IPerSaveSettingsProvider behavior = GenericServiceProvider.GetService<IPerSaveSettingsProvider>();
				bool flag2 = behavior == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					PerSaveSettings saveSettings = settings as PerSaveSettings;
					bool flag3 = saveSettings == null || !this.LoadedSettings.ContainsKey(saveSettings.Id);
					if (flag3)
					{
						result = false;
					}
					else
					{
						behavior.SaveSettings(saveSettings);
						settings.OnPropertyChanged("SAVE_TRIGGERED");
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006982 File Offset: 0x00004B82
		private void GameStarted()
		{
			this._hasGameStarted = true;
			Action instanceCacheInvalidated = this.InstanceCacheInvalidated;
			if (instanceCacheInvalidated != null)
			{
				instanceCacheInvalidated();
			}
			this.LoadedSettings.Clear();
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000069AA File Offset: 0x00004BAA
		[NullableContext(1)]
		private IEnumerable<PerSaveSettings> GetPerSaveSettings()
		{
			foreach (Assembly assembly in from a in AccessTools2.AllAssemblies()
			where !a.IsDynamic
			select a)
			{
				IEnumerable<PerSaveSettings> settings;
				try
				{
					settings = AccessTools2.GetTypesFromAssemblyIfValid(assembly, true).Where((Type t) => t.IsClass && !t.IsAbstract).Where((Type t) => t.GetConstructor(Type.EmptyTypes) != null).Where((Type t) => typeof(PerSaveSettings).IsAssignableFrom(t)).Where((Type t) => !typeof(EmptyPerSaveSettings).IsAssignableFrom(t)).Where((Type t) => !typeof(IWrapper).IsAssignableFrom(t)).Select(delegate(Type t)
					{
						PerSaveSettings result;
						try
						{
							result = (Activator.CreateInstance(t) as PerSaveSettings);
						}
						catch (Exception e)
						{
							try
							{
								this._logger.LogError(e, string.Format("Failed to initialize type {0}", t), Array.Empty<object>());
							}
							catch (Exception)
							{
								this._logger.LogError(e, "Failed to initialize and log type!", Array.Empty<object>());
							}
							result = null;
						}
						return result;
					}).OfType<PerSaveSettings>().ToList<PerSaveSettings>();
				}
				catch (TypeLoadException ex2)
				{
					TypeLoadException ex = ex2;
					settings = Array.Empty<PerSaveSettings>();
					this._logger.LogError(ex, string.Format("Error while handling assembly {0}!", assembly), Array.Empty<object>());
				}
				foreach (PerSaveSettings setting in settings)
				{
					yield return setting;
					setting = null;
				}
				IEnumerator<PerSaveSettings> enumerator2 = null;
				settings = null;
				assembly = null;
			}
			IEnumerator<Assembly> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000069BC File Offset: 0x00004BBC
		public void LoadSettings()
		{
			foreach (PerSaveSettings setting in this.GetPerSaveSettings())
			{
				this.RegisterSettings(setting);
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00006A10 File Offset: 0x00004C10
		[NullableContext(1)]
		public IEnumerable<UnavailableSetting> GetUnavailableSettings()
		{
			IEnumerable<UnavailableSetting> result;
			if (this._hasGameStarted)
			{
				IEnumerable<UnavailableSetting> enumerable = Enumerable.Empty<UnavailableSetting>();
				result = enumerable;
			}
			else
			{
				result = from setting in this.GetPerSaveSettings()
				select new UnavailableSetting(setting.Id, setting.DisplayName, UnavailableSettingType.PerSave);
			}
			return result;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00006A58 File Offset: 0x00004C58
		private void GameEnded()
		{
			this._hasGameStarted = false;
			Action instanceCacheInvalidated = this.InstanceCacheInvalidated;
			if (instanceCacheInvalidated != null)
			{
				instanceCacheInvalidated();
			}
			this.LoadedSettings.Clear();
		}

		// Token: 0x04000070 RID: 112
		[Nullable(1)]
		private readonly IBUTRLogger _logger;

		// Token: 0x04000071 RID: 113
		[Nullable(1)]
		private readonly IGameEventListener _gameEventListener;

		// Token: 0x04000072 RID: 114
		private bool _hasGameStarted;
	}
}
