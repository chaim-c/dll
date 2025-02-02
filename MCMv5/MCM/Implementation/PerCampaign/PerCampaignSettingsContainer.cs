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
using MCM.Abstractions.Base.PerCampaign;
using MCM.Abstractions.GameFeatures;
using MCM.Abstractions.PerCampaign;
using MCM.Common;

namespace MCM.Implementation.PerCampaign
{
	// Token: 0x02000038 RID: 56
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class PerCampaignSettingsContainer : BaseSettingsContainer<PerCampaignSettings>, IPerCampaignSettingsContainer, ISettingsContainer, ISettingsContainerHasSettingsDefinitions, ISettingsContainerCanOverride, ISettingsContainerCanReset, ISettingsContainerPresets, ISettingsContainerHasUnavailable, ISettingsContainerHasSettingsPack, ISettingsContainerCanInvalidateCache
	{
		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000175 RID: 373 RVA: 0x00006EB8 File Offset: 0x000050B8
		// (remove) Token: 0x06000176 RID: 374 RVA: 0x00006EF0 File Offset: 0x000050F0
		[Nullable(2)]
		[method: NullableContext(2)]
		[Nullable(2)]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action InstanceCacheInvalidated;

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00006F25 File Offset: 0x00005125
		[Nullable(1)]
		protected override GameDirectory RootFolder { [NullableContext(1)] get; }

		// Token: 0x06000178 RID: 376 RVA: 0x00006F30 File Offset: 0x00005130
		[NullableContext(1)]
		public PerCampaignSettingsContainer(IBUTRLogger<PerCampaignSettingsContainer> logger, IGameEventListener gameEventListener)
		{
			this._logger = logger;
			this._gameEventListener = gameEventListener;
			this._gameEventListener.GameStarted += this.GameStarted;
			this._gameEventListener.GameLoaded += this.GameLoaded;
			this._gameEventListener.GameEnded += this.GameEnded;
			IFileSystemProvider fileSystemProvider = GenericServiceProvider.GetService<IFileSystemProvider>();
			this.RootFolder = fileSystemProvider.GetDirectory(base.RootFolder, "PerCampaign");
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00006FB8 File Offset: 0x000051B8
		[NullableContext(2)]
		protected override void RegisterSettings(PerCampaignSettings settings)
		{
			bool flag = settings == null;
			if (!flag)
			{
				bool flag2 = GenericServiceProvider.GameScopeServiceProvider == null;
				if (!flag2)
				{
					ICampaignIdProvider campaignIdProvider = GenericServiceProvider.GetService<ICampaignIdProvider>();
					string id;
					bool flag3;
					if (campaignIdProvider != null)
					{
						id = campaignIdProvider.GetCurrentCampaignId();
						flag3 = (id == null);
					}
					else
					{
						flag3 = true;
					}
					bool flag4 = flag3;
					if (!flag4)
					{
						IFileSystemProvider fileSystemProvider = GenericServiceProvider.GetService<IFileSystemProvider>();
						bool flag5 = fileSystemProvider == null;
						if (!flag5)
						{
							this.LoadedSettings.Add(settings.Id, settings);
							GameDirectory campaignDirectory = fileSystemProvider.GetOrCreateDirectory(this.RootFolder, id);
							GameDirectory folderDirectory = fileSystemProvider.GetOrCreateDirectory(campaignDirectory, settings.FolderName);
							GameDirectory directory = string.IsNullOrEmpty(settings.SubFolder) ? folderDirectory : fileSystemProvider.GetOrCreateDirectory(folderDirectory, settings.SubFolder);
							IEnumerable<ISettingsFormat> settingsFormats = GenericServiceProvider.GetService<IEnumerable<ISettingsFormat>>() ?? Array.Empty<ISettingsFormat>();
							Func<string, bool> <>9__1;
							ISettingsFormat settingsFormat = settingsFormats.FirstOrDefault(delegate(ISettingsFormat x)
							{
								IEnumerable<string> formatTypes = x.FormatTypes;
								Func<string, bool> predicate;
								if ((predicate = <>9__1) == null)
								{
									predicate = (<>9__1 = ((string y) => y == settings.FormatType));
								}
								return formatTypes.Any(predicate);
							});
							if (settingsFormat != null)
							{
								settingsFormat.Load(settings, directory, settings.Id);
							}
							settings.OnPropertyChanged("LOADING_COMPLETE");
						}
					}
				}
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000070FC File Offset: 0x000052FC
		[NullableContext(1)]
		public override bool SaveSettings(BaseSettings settings)
		{
			PerCampaignSettings perCampaignSettings = settings as PerCampaignSettings;
			bool flag = perCampaignSettings == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = GenericServiceProvider.GameScopeServiceProvider == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					ICampaignIdProvider campaignIdProvider = GenericServiceProvider.GetService<ICampaignIdProvider>();
					string id;
					bool flag3;
					if (campaignIdProvider != null)
					{
						id = campaignIdProvider.GetCurrentCampaignId();
						flag3 = (id == null);
					}
					else
					{
						flag3 = true;
					}
					bool flag4 = flag3;
					if (flag4)
					{
						result = false;
					}
					else
					{
						IFileSystemProvider fileSystemProvider = GenericServiceProvider.GetService<IFileSystemProvider>();
						bool flag5 = fileSystemProvider == null;
						if (flag5)
						{
							result = false;
						}
						else
						{
							GameDirectory campaignDirectory = fileSystemProvider.GetOrCreateDirectory(this.RootFolder, id);
							GameDirectory folderDirectory = fileSystemProvider.GetOrCreateDirectory(campaignDirectory, settings.FolderName);
							GameDirectory directory = string.IsNullOrEmpty(settings.SubFolder) ? folderDirectory : fileSystemProvider.GetOrCreateDirectory(folderDirectory, settings.SubFolder);
							IEnumerable<ISettingsFormat> settingsFormats = GenericServiceProvider.GetService<IEnumerable<ISettingsFormat>>() ?? Array.Empty<ISettingsFormat>();
							Func<string, bool> <>9__1;
							ISettingsFormat settingsFormat = settingsFormats.FirstOrDefault(delegate(ISettingsFormat x)
							{
								IEnumerable<string> formatTypes = x.FormatTypes;
								Func<string, bool> predicate;
								if ((predicate = <>9__1) == null)
								{
									predicate = (<>9__1 = ((string y) => y == perCampaignSettings.FormatType));
								}
								return formatTypes.Any(predicate);
							});
							if (settingsFormat != null)
							{
								settingsFormat.Save(perCampaignSettings, directory, perCampaignSettings.Id);
							}
							settings.OnPropertyChanged("SAVE_TRIGGERED");
							result = true;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00007228 File Offset: 0x00005428
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

		// Token: 0x0600017C RID: 380 RVA: 0x00007250 File Offset: 0x00005450
		private void GameLoaded()
		{
			this.LoadSettings();
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000725A File Offset: 0x0000545A
		[NullableContext(1)]
		private IEnumerable<PerCampaignSettings> GetPerCampaignSettings()
		{
			foreach (Assembly assembly in from a in AccessTools2.AllAssemblies()
			where !a.IsDynamic
			select a)
			{
				IEnumerable<PerCampaignSettings> settings;
				try
				{
					settings = AccessTools2.GetTypesFromAssemblyIfValid(assembly, true).Where((Type t) => t.IsClass && !t.IsAbstract).Where((Type t) => t.GetConstructor(Type.EmptyTypes) != null).Where((Type t) => typeof(PerCampaignSettings).IsAssignableFrom(t)).Where((Type t) => !typeof(EmptyPerCampaignSettings).IsAssignableFrom(t)).Where((Type t) => !typeof(IWrapper).IsAssignableFrom(t)).Select(delegate(Type t)
					{
						PerCampaignSettings result;
						try
						{
							result = (Activator.CreateInstance(t) as PerCampaignSettings);
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
					}).OfType<PerCampaignSettings>().ToList<PerCampaignSettings>();
				}
				catch (TypeLoadException ex2)
				{
					TypeLoadException ex = ex2;
					settings = Array.Empty<PerCampaignSettings>();
					this._logger.LogError(ex, string.Format("Error while handling assembly {0}!", assembly), Array.Empty<object>());
				}
				foreach (PerCampaignSettings setting in settings)
				{
					yield return setting;
					setting = null;
				}
				IEnumerator<PerCampaignSettings> enumerator2 = null;
				settings = null;
				assembly = null;
			}
			IEnumerator<Assembly> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000726C File Offset: 0x0000546C
		private void LoadSettings()
		{
			foreach (PerCampaignSettings setting in this.GetPerCampaignSettings())
			{
				this.RegisterSettings(setting);
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000072C0 File Offset: 0x000054C0
		[NullableContext(1)]
		public IEnumerable<UnavailableSetting> GetUnavailableSettings()
		{
			IEnumerable<UnavailableSetting> result;
			if (this._hasGameStarted)
			{
				IEnumerable<UnavailableSetting> enumerable = Array.Empty<UnavailableSetting>();
				result = enumerable;
			}
			else
			{
				result = from setting in this.GetPerCampaignSettings()
				select new UnavailableSetting(setting.Id, setting.DisplayName, UnavailableSettingType.PerCampaign);
			}
			return result;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00007308 File Offset: 0x00005508
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

		// Token: 0x04000077 RID: 119
		[Nullable(1)]
		private readonly IBUTRLogger _logger;

		// Token: 0x04000078 RID: 120
		[Nullable(1)]
		private readonly IGameEventListener _gameEventListener;

		// Token: 0x04000079 RID: 121
		private bool _hasGameStarted;
	}
}
