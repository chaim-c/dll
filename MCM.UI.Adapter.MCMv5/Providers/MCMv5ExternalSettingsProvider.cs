using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection.Logger;
using HarmonyLib.BUTR.Extensions;
using MCM.Abstractions;
using MCM.Abstractions.Base;

namespace MCM.UI.Adapter.MCMv5.Providers
{
	// Token: 0x0200000A RID: 10
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class MCMv5ExternalSettingsProvider : IExternalSettingsProvider, IExternalSettingsProviderHasInitialize
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002368 File Offset: 0x00000568
		public IEnumerable<SettingsDefinition> SettingsDefinitions
		{
			get
			{
				return from x in this._settingsProviderWrappers.SelectMany((MCMv5SettingsProviderWrapper x) => x.SettingsDefinitions)
				where x.SettingsId != "MCM_v5"
				select x;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023C3 File Offset: 0x000005C3
		public MCMv5ExternalSettingsProvider(IBUTRLogger<MCMv5ExternalSettingsProvider> logger)
		{
			this._logger = logger;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023E0 File Offset: 0x000005E0
		public void Initialize()
		{
			IEnumerable<object> foreignBaseSettingsProviders = (from x in AccessTools2.AllTypes()
			where x.Assembly != typeof(BaseSettings).Assembly
			where !x.IsSubclassOf(typeof(BaseSettingsProvider)) && ReflectionUtils.ImplementsOrImplementsEquivalent(x, "MCM.Abstractions.BaseSettingsProvider", true)
			select x).Select(delegate(Type x)
			{
				PropertyInfo prop = AccessTools2.DeclaredProperty(x, "Instance", true);
				if (prop != null)
				{
					MethodInfo getMethod = prop.GetMethod;
					if (getMethod != null && getMethod.IsStatic)
					{
						return prop.GetValue(null);
					}
				}
				return null;
			}).OfType<object>();
			this._settingsProviderWrappers.AddRange(from x in foreignBaseSettingsProviders
			select new MCMv5SettingsProviderWrapper(x));
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002498 File Offset: 0x00000698
		[return: Nullable(2)]
		public BaseSettings GetSettings(string id)
		{
			foreach (MCMv5SettingsProviderWrapper settingsProvider in this._settingsProviderWrappers)
			{
				BaseSettings settings = settingsProvider.GetSettings(id);
				bool flag = settings != null;
				if (flag)
				{
					return settings;
				}
			}
			this._logger.LogWarning("GetSettings " + id + " returned null", Array.Empty<object>());
			return null;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002528 File Offset: 0x00000728
		public void SaveSettings(BaseSettings settings)
		{
			foreach (MCMv5SettingsProviderWrapper settingsProvider in this._settingsProviderWrappers)
			{
				settingsProvider.SaveSettings(settings);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002580 File Offset: 0x00000780
		public void ResetSettings(BaseSettings settings)
		{
			foreach (MCMv5SettingsProviderWrapper settingsProvider in this._settingsProviderWrappers)
			{
				settingsProvider.ResetSettings(settings);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000025D8 File Offset: 0x000007D8
		public void OverrideSettings(BaseSettings settings)
		{
			foreach (MCMv5SettingsProviderWrapper settingsProvider in this._settingsProviderWrappers)
			{
				settingsProvider.OverrideSettings(settings);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002630 File Offset: 0x00000830
		public IEnumerable<ISettingsPreset> GetPresets(string id)
		{
			foreach (MCMv5SettingsProviderWrapper settingsProvider in this._settingsProviderWrappers)
			{
				foreach (ISettingsPreset preset in settingsProvider.GetPresets(id))
				{
					yield return preset;
					preset = null;
				}
				IEnumerator<ISettingsPreset> enumerator2 = null;
				settingsProvider = null;
			}
			List<MCMv5SettingsProviderWrapper>.Enumerator enumerator = default(List<MCMv5SettingsProviderWrapper>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x04000006 RID: 6
		private readonly IBUTRLogger _logger;

		// Token: 0x04000007 RID: 7
		private readonly List<MCMv5SettingsProviderWrapper> _settingsProviderWrappers = new List<MCMv5SettingsProviderWrapper>();
	}
}
