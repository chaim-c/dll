using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection;
using BUTR.DependencyInjection.Logger;
using MCM.Abstractions;
using MCM.Abstractions.Base;
using MCM.Abstractions.Base.Global;
using MCM.Abstractions.Base.PerCampaign;
using MCM.Abstractions.Base.PerSave;
using MCM.Abstractions.Global;
using MCM.Abstractions.PerCampaign;
using MCM.Abstractions.PerSave;

namespace MCM.Implementation
{
	// Token: 0x02000028 RID: 40
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class DefaultSettingsProvider : BaseSettingsProvider
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x0000523C File Offset: 0x0000343C
		public override IEnumerable<SettingsDefinition> SettingsDefinitions
		{
			get
			{
				return this._settingsContainers.OfType<ISettingsContainerHasSettingsDefinitions>().SelectMany((ISettingsContainerHasSettingsDefinitions sp) => sp.SettingsDefinitions).Concat(this._externalSettingsProviders.SelectMany((IExternalSettingsProvider x) => x.SettingsDefinitions));
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000052A8 File Offset: 0x000034A8
		public DefaultSettingsProvider(IBUTRLogger<DefaultSettingsProvider> logger)
		{
			this._logger = logger;
			List<IGlobalSettingsContainer> globalSettingsContainers = (GenericServiceProvider.GetService<IEnumerable<IGlobalSettingsContainer>>() ?? Enumerable.Empty<IGlobalSettingsContainer>()).ToList<IGlobalSettingsContainer>();
			List<IPerSaveSettingsContainer> perSaveSettingsContainers = (GenericServiceProvider.GetService<IEnumerable<IPerSaveSettingsContainer>>() ?? Enumerable.Empty<IPerSaveSettingsContainer>()).ToList<IPerSaveSettingsContainer>();
			List<IPerCampaignSettingsContainer> perCampaignSettingsContainers = (GenericServiceProvider.GetService<IEnumerable<IPerCampaignSettingsContainer>>() ?? Enumerable.Empty<IPerCampaignSettingsContainer>()).ToList<IPerCampaignSettingsContainer>();
			List<IExternalSettingsProvider> externalSettingsProviders = (GenericServiceProvider.GetService<IEnumerable<IExternalSettingsProvider>>() ?? Enumerable.Empty<IExternalSettingsProvider>()).ToList<IExternalSettingsProvider>();
			foreach (IGlobalSettingsContainer globalSettingsContainer in globalSettingsContainers)
			{
				ISettingsContainerCanInvalidateCache canInvalidateCache = globalSettingsContainer as ISettingsContainerCanInvalidateCache;
				bool flag = canInvalidateCache != null;
				if (flag)
				{
					ISettingsContainerCanInvalidateCache settingsContainerCanInvalidateCache = canInvalidateCache;
					Action value;
					if ((value = DefaultSettingsProvider.<>O.<0>__InvalidateCache) == null)
					{
						value = (DefaultSettingsProvider.<>O.<0>__InvalidateCache = new Action(GlobalSettings.InvalidateCache));
					}
					settingsContainerCanInvalidateCache.InstanceCacheInvalidated += value;
				}
				logger.LogInformation(string.Format("Found Global container {0} ({1})", globalSettingsContainer.GetType(), globalSettingsContainer.SettingsDefinitions.Count<SettingsDefinition>()), Array.Empty<object>());
			}
			foreach (IPerSaveSettingsContainer perSaveSettingsContainer in perSaveSettingsContainers)
			{
				ISettingsContainerCanInvalidateCache canInvalidateCache2 = perSaveSettingsContainer;
				bool flag2 = canInvalidateCache2 != null;
				if (flag2)
				{
					ISettingsContainerCanInvalidateCache settingsContainerCanInvalidateCache2 = canInvalidateCache2;
					Action value2;
					if ((value2 = DefaultSettingsProvider.<>O.<1>__InvalidateCache) == null)
					{
						value2 = (DefaultSettingsProvider.<>O.<1>__InvalidateCache = new Action(PerSaveSettings.InvalidateCache));
					}
					settingsContainerCanInvalidateCache2.InstanceCacheInvalidated += value2;
				}
				logger.LogInformation(string.Format("Found PerSave container {0} ({1})", perSaveSettingsContainer.GetType(), perSaveSettingsContainer.SettingsDefinitions.Count<SettingsDefinition>()), Array.Empty<object>());
			}
			foreach (IPerCampaignSettingsContainer perCampaignSettingsContainer in perCampaignSettingsContainers)
			{
				ISettingsContainerCanInvalidateCache canInvalidateCache3 = perCampaignSettingsContainer;
				bool flag3 = canInvalidateCache3 != null;
				if (flag3)
				{
					ISettingsContainerCanInvalidateCache settingsContainerCanInvalidateCache3 = canInvalidateCache3;
					Action value3;
					if ((value3 = DefaultSettingsProvider.<>O.<2>__InvalidateCache) == null)
					{
						value3 = (DefaultSettingsProvider.<>O.<2>__InvalidateCache = new Action(PerCampaignSettings.InvalidateCache));
					}
					settingsContainerCanInvalidateCache3.InstanceCacheInvalidated += value3;
				}
				logger.LogInformation(string.Format("Found Campaign container {0} ({1})", perCampaignSettingsContainer.GetType(), perCampaignSettingsContainer.SettingsDefinitions.Count<SettingsDefinition>()), Array.Empty<object>());
			}
			foreach (IExternalSettingsProvider externalSettingsProvider in externalSettingsProviders)
			{
				IExternalSettingsProviderCanInvalidateCache canInvalidateCache4 = externalSettingsProvider as IExternalSettingsProviderCanInvalidateCache;
				bool flag4 = canInvalidateCache4 != null;
				if (flag4)
				{
					canInvalidateCache4.InstanceCacheInvalidated += delegate(ExternalSettingsProviderInvalidateCacheType cacheType)
					{
						switch (cacheType)
						{
						case ExternalSettingsProviderInvalidateCacheType.Global:
							GlobalSettings.InvalidateCache();
							break;
						case ExternalSettingsProviderInvalidateCacheType.PerCampaign:
							PerCampaignSettings.InvalidateCache();
							break;
						case ExternalSettingsProviderInvalidateCacheType.PerSave:
							PerSaveSettings.InvalidateCache();
							break;
						}
					};
				}
				logger.LogInformation(string.Format("Found external provider {0} ({1})", externalSettingsProvider.GetType(), externalSettingsProvider.SettingsDefinitions.Count<SettingsDefinition>()), Array.Empty<object>());
			}
			this._settingsContainers = Enumerable.Empty<ISettingsContainer>().Concat(globalSettingsContainers).Concat(perSaveSettingsContainers).Concat(perCampaignSettingsContainers).ToList<ISettingsContainer>();
			this._externalSettingsProviders = externalSettingsProviders.ToList<IExternalSettingsProvider>();
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000055C8 File Offset: 0x000037C8
		[return: Nullable(2)]
		public override BaseSettings GetSettings(string id)
		{
			foreach (ISettingsContainer settingsContainer in this._settingsContainers)
			{
				BaseSettings settings = settingsContainer.GetSettings(id);
				bool flag = settings != null;
				if (flag)
				{
					return settings;
				}
			}
			foreach (IExternalSettingsProvider settingsProvider in this._externalSettingsProviders)
			{
				BaseSettings settings2 = settingsProvider.GetSettings(id);
				bool flag2 = settings2 != null;
				if (flag2)
				{
					return settings2;
				}
			}
			this._logger.LogWarning("GetSettings " + id + " returned null", Array.Empty<object>());
			return null;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000056B8 File Offset: 0x000038B8
		public override void SaveSettings(BaseSettings settings)
		{
			foreach (ISettingsContainer settingsContainer in this._settingsContainers)
			{
				settingsContainer.SaveSettings(settings);
			}
			foreach (IExternalSettingsProvider settingsProvider in this._externalSettingsProviders)
			{
				settingsProvider.SaveSettings(settings);
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005754 File Offset: 0x00003954
		public override void ResetSettings(BaseSettings settings)
		{
			foreach (ISettingsContainerCanReset settingsContainer in this._settingsContainers.OfType<ISettingsContainerCanReset>())
			{
				settingsContainer.ResetSettings(settings);
			}
			foreach (IExternalSettingsProvider settingsProvider in this._externalSettingsProviders)
			{
				settingsProvider.ResetSettings(settings);
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000057F0 File Offset: 0x000039F0
		public override void OverrideSettings(BaseSettings settings)
		{
			foreach (ISettingsContainerCanOverride settingsContainer in this._settingsContainers.OfType<ISettingsContainerCanOverride>())
			{
				settingsContainer.OverrideSettings(settings);
			}
			foreach (IExternalSettingsProvider settingsProvider in this._externalSettingsProviders)
			{
				settingsProvider.OverrideSettings(settings);
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000588C File Offset: 0x00003A8C
		public override IEnumerable<ISettingsPreset> GetPresets(string id)
		{
			foreach (ISettingsContainerPresets settingsContainer in this._settingsContainers.OfType<ISettingsContainerPresets>())
			{
				foreach (ISettingsPreset preset in settingsContainer.GetPresets(id))
				{
					yield return preset;
					preset = null;
				}
				IEnumerator<ISettingsPreset> enumerator2 = null;
				settingsContainer = null;
			}
			IEnumerator<ISettingsContainerPresets> enumerator = null;
			foreach (IExternalSettingsProvider settingsProvider in this._externalSettingsProviders)
			{
				foreach (ISettingsPreset preset2 in settingsProvider.GetPresets(id))
				{
					yield return preset2;
					preset2 = null;
				}
				IEnumerator<ISettingsPreset> enumerator4 = null;
				settingsProvider = null;
			}
			List<IExternalSettingsProvider>.Enumerator enumerator3 = default(List<IExternalSettingsProvider>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000058A3 File Offset: 0x00003AA3
		public override IEnumerable<UnavailableSetting> GetUnavailableSettings()
		{
			foreach (ISettingsContainerHasUnavailable settingsContainer in this._settingsContainers.OfType<ISettingsContainerHasUnavailable>())
			{
				foreach (UnavailableSetting unavailableSetting in settingsContainer.GetUnavailableSettings())
				{
					yield return unavailableSetting;
					unavailableSetting = null;
				}
				IEnumerator<UnavailableSetting> enumerator2 = null;
				settingsContainer = null;
			}
			IEnumerator<ISettingsContainerHasUnavailable> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000058B3 File Offset: 0x00003AB3
		public override IEnumerable<SettingSnapshot> SaveAvailableSnapshots()
		{
			foreach (ISettingsContainerHasSettingsPack settingsContainer in this._settingsContainers.OfType<ISettingsContainerHasSettingsPack>())
			{
				foreach (SettingSnapshot snapshot in settingsContainer.SaveAvailableSnapshots())
				{
					yield return snapshot;
					snapshot = null;
				}
				IEnumerator<SettingSnapshot> enumerator2 = null;
				settingsContainer = null;
			}
			IEnumerator<ISettingsContainerHasSettingsPack> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000058C3 File Offset: 0x00003AC3
		public override IEnumerable<BaseSettings> LoadAvailableSnapshots(IEnumerable<SettingSnapshot> snapshots)
		{
			foreach (ISettingsContainerHasSettingsPack settingsContainer in this._settingsContainers.OfType<ISettingsContainerHasSettingsPack>())
			{
				foreach (BaseSettings settings in settingsContainer.LoadAvailableSnapshots(snapshots))
				{
					yield return settings;
					settings = null;
				}
				IEnumerator<BaseSettings> enumerator2 = null;
				settingsContainer = null;
			}
			IEnumerator<ISettingsContainerHasSettingsPack> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x04000047 RID: 71
		private readonly IBUTRLogger _logger;

		// Token: 0x04000048 RID: 72
		private readonly List<ISettingsContainer> _settingsContainers;

		// Token: 0x04000049 RID: 73
		private readonly List<IExternalSettingsProvider> _externalSettingsProviders;

		// Token: 0x0200017F RID: 383
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000305 RID: 773
			[Nullable(0)]
			public static Action <0>__InvalidateCache;

			// Token: 0x04000306 RID: 774
			[Nullable(0)]
			public static Action <1>__InvalidateCache;

			// Token: 0x04000307 RID: 775
			[Nullable(0)]
			public static Action <2>__InvalidateCache;
		}
	}
}
