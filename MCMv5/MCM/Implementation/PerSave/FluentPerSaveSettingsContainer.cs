using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection;
using MCM.Abstractions;
using MCM.Abstractions.Base;
using MCM.Abstractions.Base.PerSave;
using MCM.Abstractions.GameFeatures;
using MCM.Abstractions.PerSave;

namespace MCM.Implementation.PerSave
{
	// Token: 0x02000035 RID: 53
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class FluentPerSaveSettingsContainer : BaseSettingsContainer<FluentPerSaveSettings>, IFluentPerSaveSettingsContainer, IPerSaveSettingsContainer, ISettingsContainer, ISettingsContainerHasSettingsDefinitions, ISettingsContainerCanOverride, ISettingsContainerCanReset, ISettingsContainerPresets, ISettingsContainerHasUnavailable, ISettingsContainerHasSettingsPack, ISettingsContainerCanInvalidateCache
	{
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000154 RID: 340 RVA: 0x000065EC File Offset: 0x000047EC
		// (remove) Token: 0x06000155 RID: 341 RVA: 0x00006624 File Offset: 0x00004824
		[Nullable(2)]
		[method: NullableContext(2)]
		[Nullable(2)]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action InstanceCacheInvalidated;

		// Token: 0x06000156 RID: 342 RVA: 0x0000665C File Offset: 0x0000485C
		[NullableContext(1)]
		public FluentPerSaveSettingsContainer(IGameEventListener gameEventListener)
		{
			this._gameEventListener = gameEventListener;
			this._gameEventListener.GameStarted += this.GameStarted;
			this._gameEventListener.GameEnded += this.GameEnded;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000066A8 File Offset: 0x000048A8
		[NullableContext(1)]
		public void Register(FluentPerSaveSettings settings)
		{
			this.RegisterSettings(settings);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000066B4 File Offset: 0x000048B4
		[NullableContext(1)]
		public void Unregister(FluentPerSaveSettings settings)
		{
			bool flag = this.LoadedSettings.ContainsKey(settings.Id);
			if (flag)
			{
				this.LoadedSettings.Remove(settings.Id);
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000066EC File Offset: 0x000048EC
		[NullableContext(2)]
		protected override void RegisterSettings(FluentPerSaveSettings perSaveSettings)
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

		// Token: 0x0600015A RID: 346 RVA: 0x0000674C File Offset: 0x0000494C
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

		// Token: 0x0600015B RID: 347 RVA: 0x000067C2 File Offset: 0x000049C2
		public void LoadSettings()
		{
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000067C5 File Offset: 0x000049C5
		public void GameStarted()
		{
			this.LoadedSettings.Clear();
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000067D3 File Offset: 0x000049D3
		public void GameEnded()
		{
			this.LoadedSettings.Clear();
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000067E1 File Offset: 0x000049E1
		[NullableContext(1)]
		public IEnumerable<UnavailableSetting> GetUnavailableSettings()
		{
			return Enumerable.Empty<UnavailableSetting>();
		}

		// Token: 0x0400006E RID: 110
		[Nullable(1)]
		private readonly IGameEventListener _gameEventListener;
	}
}
