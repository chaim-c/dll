using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection;
using MCM.Abstractions;
using MCM.Abstractions.Base;
using MCM.Abstractions.GameFeatures;

namespace MCM.Implementation
{
	// Token: 0x02000025 RID: 37
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class BaseSettingsContainer<[Nullable(0)] TSettings> : ISettingsContainer, ISettingsContainerHasSettingsDefinitions, ISettingsContainerCanOverride, ISettingsContainerCanReset, ISettingsContainerPresets, ISettingsContainerHasSettingsPack where TSettings : BaseSettings
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00004D14 File Offset: 0x00002F14
		protected virtual GameDirectory RootFolder { get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00004D1C File Offset: 0x00002F1C
		protected virtual Dictionary<string, TSettings> LoadedSettings { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004D24 File Offset: 0x00002F24
		public virtual IEnumerable<SettingsDefinition> SettingsDefinitions
		{
			get
			{
				return from s in this.LoadedSettings.Values
				select new SettingsDefinition(s.Id, s.DisplayName, s.GetSettingPropertyGroups());
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004D58 File Offset: 0x00002F58
		[NullableContext(2)]
		protected virtual void RegisterSettings(TSettings settings)
		{
			bool flag = settings == null || this.LoadedSettings.ContainsKey(settings.Id);
			if (!flag)
			{
				IFileSystemProvider fileSystemProvider = GenericServiceProvider.GetService<IFileSystemProvider>();
				bool flag2 = fileSystemProvider == null;
				if (!flag2)
				{
					this.LoadedSettings.Add(settings.Id, settings);
					GameDirectory folderDirectory = fileSystemProvider.GetOrCreateDirectory(this.RootFolder, settings.FolderName);
					GameDirectory directory = string.IsNullOrEmpty(settings.SubFolder) ? folderDirectory : fileSystemProvider.GetOrCreateDirectory(folderDirectory, settings.SubFolder);
					IEnumerable<ISettingsFormat> settingsFormats = GenericServiceProvider.GetService<IEnumerable<ISettingsFormat>>() ?? Enumerable.Empty<ISettingsFormat>();
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

		// Token: 0x060000D1 RID: 209 RVA: 0x00004E98 File Offset: 0x00003098
		[return: Nullable(2)]
		public virtual BaseSettings GetSettings(string id)
		{
			TSettings result;
			return this.LoadedSettings.TryGetValue(id, out result) ? result : default(TSettings);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004EC8 File Offset: 0x000030C8
		public virtual bool SaveSettings(BaseSettings settings)
		{
			bool flag = !(settings is TSettings) || !this.LoadedSettings.ContainsKey(settings.Id);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				IFileSystemProvider fileSystemProvider = GenericServiceProvider.GetService<IFileSystemProvider>();
				bool flag2 = fileSystemProvider == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					GameDirectory folderDirectory = fileSystemProvider.GetOrCreateDirectory(this.RootFolder, settings.FolderName);
					GameDirectory directory = string.IsNullOrEmpty(settings.SubFolder) ? folderDirectory : fileSystemProvider.GetOrCreateDirectory(folderDirectory, settings.SubFolder);
					IEnumerable<ISettingsFormat> settingsFormats = GenericServiceProvider.GetService<IEnumerable<ISettingsFormat>>() ?? Enumerable.Empty<ISettingsFormat>();
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
						settingsFormat.Save(settings, directory, settings.Id);
					}
					settings.OnPropertyChanged("SAVE_TRIGGERED");
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004FD4 File Offset: 0x000031D4
		public virtual bool OverrideSettings(BaseSettings settings)
		{
			bool flag = !(settings is TSettings) || !this.LoadedSettings.ContainsKey(settings.Id);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				SettingsUtils.OverrideSettings(this.LoadedSettings[settings.Id], settings);
				result = true;
			}
			return result;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000502C File Offset: 0x0000322C
		public virtual bool ResetSettings(BaseSettings settings)
		{
			bool flag = !(settings is TSettings) || !this.LoadedSettings.ContainsKey(settings.Id);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				SettingsUtils.ResetSettings(this.LoadedSettings[settings.Id]);
				result = true;
			}
			return result;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00005082 File Offset: 0x00003282
		public IEnumerable<ISettingsPreset> GetPresets(string settingsId)
		{
			TSettings settings;
			bool flag = !this.LoadedSettings.TryGetValue(settingsId, out settings);
			if (flag)
			{
				yield break;
			}
			IFileSystemProvider fileSystemProvider = GenericServiceProvider.GetService<IFileSystemProvider>();
			bool flag2 = fileSystemProvider == null;
			if (flag2)
			{
				yield break;
			}
			GameDirectory presetsDirectory = fileSystemProvider.GetOrCreateDirectory(fileSystemProvider.GetModSettingsDirectory(), "Presets");
			GameDirectory settingsDirectory = fileSystemProvider.GetOrCreateDirectory(presetsDirectory, settingsId);
			foreach (GameFile filePath in fileSystemProvider.GetFiles(settingsDirectory, "*.json"))
			{
				JsonSettingsPreset preset = JsonSettingsPreset.FromFile(settings, filePath);
				bool flag3 = preset != null;
				if (flag3)
				{
					yield return preset;
				}
				preset = null;
				filePath = null;
			}
			GameFile[] array = null;
			yield break;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000509C File Offset: 0x0000329C
		public bool SavePresets(ISettingsPreset preset)
		{
			TSettings settings;
			bool flag = !this.LoadedSettings.TryGetValue(preset.SettingsId, out settings);
			return !flag && preset.SavePreset(settings);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000050D8 File Offset: 0x000032D8
		public IEnumerable<SettingSnapshot> SaveAvailableSnapshots()
		{
			JsonSettingsFormat jsonFormat = GenericServiceProvider.GetService<JsonSettingsFormat>();
			bool flag = jsonFormat == null;
			if (flag)
			{
				yield break;
			}
			foreach (TSettings setting in this.LoadedSettings.Values)
			{
				string json = jsonFormat.SaveJson(setting);
				string folderName = (!string.IsNullOrEmpty(setting.FolderName)) ? (setting.FolderName + "/") : string.Empty;
				string subFolderName = (!string.IsNullOrEmpty(setting.SubFolder)) ? (setting.SubFolder + "/") : string.Empty;
				string path = folderName + subFolderName + setting.Id + ".json";
				yield return new SettingSnapshot(path, json);
				json = null;
				folderName = null;
				subFolderName = null;
				path = null;
				setting = default(TSettings);
			}
			Dictionary<string, TSettings>.ValueCollection.Enumerator enumerator = default(Dictionary<string, TSettings>.ValueCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000050E8 File Offset: 0x000032E8
		public IEnumerable<BaseSettings> LoadAvailableSnapshots(IEnumerable<SettingSnapshot> snapshots)
		{
			JsonSettingsFormat jsonFormat = GenericServiceProvider.GetService<JsonSettingsFormat>();
			bool flag = jsonFormat == null;
			if (flag)
			{
				yield break;
			}
			Dictionary<string, string> snapshotDict = snapshots.ToDictionary((SettingSnapshot x) => x.Path, (SettingSnapshot x) => x.JsonContent);
			foreach (TSettings setting in this.LoadedSettings.Values)
			{
				string folderName = (!string.IsNullOrEmpty(setting.FolderName)) ? (setting.FolderName + "/") : string.Empty;
				string subFolderName = (!string.IsNullOrEmpty(setting.SubFolder)) ? (setting.SubFolder + "/") : string.Empty;
				string path = folderName + subFolderName + setting.Id + ".json";
				string json;
				bool flag2 = snapshotDict.TryGetValue(path, out json);
				if (flag2)
				{
					yield return jsonFormat.LoadFromJson(setting.CreateNew(), json);
				}
				folderName = null;
				subFolderName = null;
				path = null;
				json = null;
				setting = default(TSettings);
			}
			Dictionary<string, TSettings>.ValueCollection.Enumerator enumerator = default(Dictionary<string, TSettings>.ValueCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000050FF File Offset: 0x000032FF
		protected BaseSettingsContainer()
		{
			IFileSystemProvider service = GenericServiceProvider.GetService<IFileSystemProvider>();
			this.RootFolder = (((service != null) ? service.GetModSettingsDirectory() : null) ?? new GameDirectory(PlatformDirectoryType.User, "Configs\\ModSettings\\"));
			this.LoadedSettings = new Dictionary<string, TSettings>();
			base..ctor();
		}
	}
}
