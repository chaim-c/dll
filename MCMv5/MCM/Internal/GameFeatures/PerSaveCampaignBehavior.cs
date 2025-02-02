using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection;
using MCM.Abstractions.Base.PerSave;
using MCM.Abstractions.GameFeatures;
using MCM.Implementation;
using MCM.Implementation.PerSave;
using TaleWorlds.CampaignSystem;

namespace MCM.Internal.GameFeatures
{
	// Token: 0x02000010 RID: 16
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class PerSaveCampaignBehavior : CampaignBehaviorBase, IPerSaveSettingsProvider
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00003354 File Offset: 0x00001554
		[Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		public Dictionary<string, string> Settings
		{
			[return: Nullable(new byte[]
			{
				2,
				1,
				1
			})]
			get
			{
				return this._settings;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000335C File Offset: 0x0000155C
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<Dictionary<string, string>>("_settings", ref this._settings);
			bool isLoading = dataStore.IsLoading;
			if (isLoading)
			{
				if (this._settings == null)
				{
					this._settings = new Dictionary<string, string>();
				}
				PerSaveSettingsContainer perSaveSettingsContainer = GenericServiceProvider.GetService<PerSaveSettingsContainer>();
				if (perSaveSettingsContainer != null)
				{
					perSaveSettingsContainer.LoadSettings();
				}
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000033AD File Offset: 0x000015AD
		public override void RegisterEvents()
		{
			CampaignEvents.OnNewGameCreatedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnNewGameCreatedEvent));
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000033C8 File Offset: 0x000015C8
		private void OnNewGameCreatedEvent(CampaignGameStarter campaignGameStarter)
		{
			this._settings = new Dictionary<string, string>();
			PerSaveSettingsContainer perSaveSettingsContainer = GenericServiceProvider.GetService<PerSaveSettingsContainer>();
			if (perSaveSettingsContainer != null)
			{
				perSaveSettingsContainer.LoadSettings();
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000033F4 File Offset: 0x000015F4
		public bool SaveSettings(PerSaveSettings perSaveSettings)
		{
			JsonSettingsFormat jsonSettingsFormat = GenericServiceProvider.GetService<JsonSettingsFormat>();
			bool flag = this._settings == null || jsonSettingsFormat == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				string key = Path.Combine(perSaveSettings.FolderName, perSaveSettings.SubFolder, perSaveSettings.Id) ?? "";
				this._settings[key] = jsonSettingsFormat.SaveJson(perSaveSettings);
				result = true;
			}
			return result;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000345C File Offset: 0x0000165C
		public void LoadSettings(PerSaveSettings perSaveSettings)
		{
			JsonSettingsFormat jsonSettingsFormat = GenericServiceProvider.GetService<JsonSettingsFormat>();
			bool flag = this._settings == null || jsonSettingsFormat == null;
			if (!flag)
			{
				string key = Path.Combine(perSaveSettings.FolderName, perSaveSettings.SubFolder, perSaveSettings.Id) ?? "";
				string jsonData;
				bool flag2 = this._settings.TryGetValue(key, out jsonData);
				if (flag2)
				{
					jsonSettingsFormat.LoadFromJson(perSaveSettings, jsonData);
				}
			}
		}

		// Token: 0x04000014 RID: 20
		[Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		private Dictionary<string, string> _settings = new Dictionary<string, string>();
	}
}
