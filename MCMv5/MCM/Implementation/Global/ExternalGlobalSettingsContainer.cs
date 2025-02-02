using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection;
using BUTR.DependencyInjection.Logger;
using MCM.Abstractions;
using MCM.Abstractions.Base;
using MCM.Abstractions.Base.Global;
using MCM.Abstractions.Global;

namespace MCM.Implementation.Global
{
	// Token: 0x0200003A RID: 58
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ExternalGlobalSettingsContainer : IFluentGlobalSettingsContainer, IGlobalSettingsContainer, ISettingsContainer, ISettingsContainerHasSettingsDefinitions, ISettingsContainerCanOverride, ISettingsContainerCanReset, ISettingsContainerPresets, ISettingsContainerHasSettingsPack
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000184 RID: 388 RVA: 0x000073FA File Offset: 0x000055FA
		private Dictionary<string, ExternalGlobalSettings> LoadedSettings { get; } = new Dictionary<string, ExternalGlobalSettings>();

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00007402 File Offset: 0x00005602
		public IEnumerable<SettingsDefinition> SettingsDefinitions
		{
			get
			{
				return from s in this.LoadedSettings.Values
				select new SettingsDefinition(s.Id, s.DisplayName, s.GetSettingPropertyGroups());
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00007433 File Offset: 0x00005633
		public ExternalGlobalSettingsContainer(IBUTRLogger<ExternalGlobalSettingsContainer> logger)
		{
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00007448 File Offset: 0x00005648
		[return: Nullable(2)]
		public BaseSettings GetSettings(string id)
		{
			ExternalGlobalSettings result;
			return this.LoadedSettings.TryGetValue(id, out result) ? result : null;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000746C File Offset: 0x0000566C
		public bool SaveSettings(BaseSettings settings)
		{
			bool flag = !(settings is ExternalGlobalSettings) || !this.LoadedSettings.ContainsKey(settings.Id);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				settings.OnPropertyChanged("SAVE_TRIGGERED");
				result = true;
			}
			return result;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000074B4 File Offset: 0x000056B4
		public bool OverrideSettings(BaseSettings settings)
		{
			bool flag = !(settings is ExternalGlobalSettings) || !this.LoadedSettings.ContainsKey(settings.Id);
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

		// Token: 0x0600018A RID: 394 RVA: 0x00007508 File Offset: 0x00005708
		public bool ResetSettings(BaseSettings settings)
		{
			bool flag = !(settings is ExternalGlobalSettings) || !this.LoadedSettings.ContainsKey(settings.Id);
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

		// Token: 0x0600018B RID: 395 RVA: 0x00007559 File Offset: 0x00005759
		public IEnumerable<ISettingsPreset> GetPresets(string settingsId)
		{
			yield break;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00007570 File Offset: 0x00005770
		public void Register(FluentGlobalSettings settings)
		{
			ExternalGlobalSettings externalGlobalSettings = settings as ExternalGlobalSettings;
			bool flag = externalGlobalSettings == null;
			if (!flag)
			{
				this.LoadedSettings.Add(externalGlobalSettings.Id, externalGlobalSettings);
				settings.OnPropertyChanged("LOADING_COMPLETE");
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000075B4 File Offset: 0x000057B4
		public void Unregister(FluentGlobalSettings settings)
		{
			ExternalGlobalSettings externalGlobalSettings = settings as ExternalGlobalSettings;
			bool flag = externalGlobalSettings == null;
			if (!flag)
			{
				bool flag2 = this.LoadedSettings.ContainsKey(externalGlobalSettings.Id);
				if (flag2)
				{
					this.LoadedSettings.Remove(externalGlobalSettings.Id);
				}
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00007600 File Offset: 0x00005800
		public bool SavePresets(ISettingsPreset preset)
		{
			ExternalGlobalSettings settings;
			bool flag = !this.LoadedSettings.TryGetValue(preset.SettingsId, out settings);
			return !flag && preset.SavePreset(settings);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00007637 File Offset: 0x00005837
		public IEnumerable<SettingSnapshot> SaveAvailableSnapshots()
		{
			JsonSettingsFormat jsonFormat = GenericServiceProvider.GetService<JsonSettingsFormat>();
			bool flag = jsonFormat == null;
			if (flag)
			{
				yield break;
			}
			foreach (ExternalGlobalSettings setting in this.LoadedSettings.Values)
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
				setting = null;
			}
			Dictionary<string, ExternalGlobalSettings>.ValueCollection.Enumerator enumerator = default(Dictionary<string, ExternalGlobalSettings>.ValueCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00007647 File Offset: 0x00005847
		public IEnumerable<BaseSettings> LoadAvailableSnapshots(IEnumerable<SettingSnapshot> snapshots)
		{
			JsonSettingsFormat jsonFormat = GenericServiceProvider.GetService<JsonSettingsFormat>();
			bool flag = jsonFormat == null;
			if (flag)
			{
				yield break;
			}
			Dictionary<string, string> snapshotDict = snapshots.ToDictionary((SettingSnapshot x) => x.Path, (SettingSnapshot x) => x.JsonContent);
			foreach (ExternalGlobalSettings setting in this.LoadedSettings.Values)
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
				setting = null;
			}
			Dictionary<string, ExternalGlobalSettings>.ValueCollection.Enumerator enumerator = default(Dictionary<string, ExternalGlobalSettings>.ValueCollection.Enumerator);
			yield break;
			yield break;
		}
	}
}
