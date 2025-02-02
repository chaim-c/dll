using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection;
using MCM.Abstractions;
using MCM.Abstractions.Base;
using MCM.Abstractions.Base.PerCampaign;
using MCM.Abstractions.GameFeatures;
using MCM.Abstractions.PerCampaign;

namespace MCM.Implementation.PerCampaign
{
	// Token: 0x02000037 RID: 55
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class FluentPerCampaignSettingsContainer : BaseSettingsContainer<FluentPerCampaignSettings>, IFluentPerCampaignSettingsContainer, IPerCampaignSettingsContainer, ISettingsContainer, ISettingsContainerHasSettingsDefinitions, ISettingsContainerCanOverride, ISettingsContainerCanReset, ISettingsContainerPresets, ISettingsContainerHasUnavailable, ISettingsContainerHasSettingsPack, ISettingsContainerCanInvalidateCache
	{
		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600016A RID: 362 RVA: 0x00006B00 File Offset: 0x00004D00
		// (remove) Token: 0x0600016B RID: 363 RVA: 0x00006B38 File Offset: 0x00004D38
		[Nullable(2)]
		[method: NullableContext(2)]
		[Nullable(2)]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action InstanceCacheInvalidated;

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00006B6D File Offset: 0x00004D6D
		[Nullable(1)]
		protected override GameDirectory RootFolder { [NullableContext(1)] get; }

		// Token: 0x0600016D RID: 365 RVA: 0x00006B78 File Offset: 0x00004D78
		[NullableContext(1)]
		public FluentPerCampaignSettingsContainer(IGameEventListener gameEventListener)
		{
			this._gameEventListener = gameEventListener;
			this._gameEventListener.GameStarted += this.GameStarted;
			this._gameEventListener.GameEnded += this.GameEnded;
			IFileSystemProvider fileSystemProvider = GenericServiceProvider.GetService<IFileSystemProvider>();
			this.RootFolder = fileSystemProvider.GetDirectory(base.RootFolder, "PerCampaign");
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00006BE4 File Offset: 0x00004DE4
		[NullableContext(2)]
		protected override void RegisterSettings(FluentPerCampaignSettings settings)
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

		// Token: 0x0600016F RID: 367 RVA: 0x00006D28 File Offset: 0x00004F28
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

		// Token: 0x06000170 RID: 368 RVA: 0x00006E54 File Offset: 0x00005054
		[NullableContext(1)]
		public void Register(FluentPerCampaignSettings settings)
		{
			this.RegisterSettings(settings);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00006E60 File Offset: 0x00005060
		[NullableContext(1)]
		public void Unregister(FluentPerCampaignSettings settings)
		{
			bool flag = this.LoadedSettings.ContainsKey(settings.Id);
			if (flag)
			{
				this.LoadedSettings.Remove(settings.Id);
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00006E95 File Offset: 0x00005095
		private void GameStarted()
		{
			this.LoadedSettings.Clear();
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00006EA3 File Offset: 0x000050A3
		private void GameEnded()
		{
			this.LoadedSettings.Clear();
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00006EB1 File Offset: 0x000050B1
		[NullableContext(1)]
		public IEnumerable<UnavailableSetting> GetUnavailableSettings()
		{
			return Array.Empty<UnavailableSetting>();
		}

		// Token: 0x04000074 RID: 116
		[Nullable(1)]
		private readonly IGameEventListener _gameEventListener;
	}
}
