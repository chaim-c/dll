using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection;
using MCM.Abstractions.Base;
using MCM.Abstractions.Properties;

namespace MCM.Abstractions
{
	// Token: 0x02000052 RID: 82
	[NullableContext(1)]
	[Nullable(0)]
	public static class BaseSettingsExtensions
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x0000777C File Offset: 0x0000597C
		public static List<SettingsPropertyGroupDefinition> GetSettingPropertyGroups(this BaseSettings settings)
		{
			return settings.GetUnsortedSettingPropertyGroups().SortDefault().ToList<SettingsPropertyGroupDefinition>();
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00007790 File Offset: 0x00005990
		public static IEnumerable<SettingsPropertyGroupDefinition> GetUnsortedSettingPropertyGroups(this BaseSettings settings)
		{
			IEnumerable<ISettingsPropertyDiscoverer> discoverers = GenericServiceProvider.GetService<IEnumerable<ISettingsPropertyDiscoverer>>() ?? Enumerable.Empty<ISettingsPropertyDiscoverer>();
			Func<string, bool> <>9__1;
			ISettingsPropertyDiscoverer discoverer = discoverers.FirstOrDefault(delegate(ISettingsPropertyDiscoverer x)
			{
				IEnumerable<string> discoveryTypes = x.DiscoveryTypes;
				Func<string, bool> predicate;
				if ((predicate = <>9__1) == null)
				{
					predicate = (<>9__1 = ((string y) => y == settings.DiscoveryType));
				}
				return discoveryTypes.Any(predicate);
			});
			return SettingsUtils.GetSettingsPropertyGroups(settings.SubGroupDelimiter, ((discoverer != null) ? discoverer.GetProperties(settings) : null) ?? Enumerable.Empty<ISettingsPropertyDefinition>());
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000077FC File Offset: 0x000059FC
		public static IEnumerable<ISettingsPropertyDefinition> GetAllSettingPropertyDefinitions(this BaseSettings settings)
		{
			IEnumerable<SettingsPropertyGroupDefinition> settingPropertyGroups = settings.GetSettingPropertyGroups();
			Func<SettingsPropertyGroupDefinition, IEnumerable<ISettingsPropertyDefinition>> selector;
			if ((selector = BaseSettingsExtensions.<>O.<0>__GetAllSettingPropertyDefinitions) == null)
			{
				selector = (BaseSettingsExtensions.<>O.<0>__GetAllSettingPropertyDefinitions = new Func<SettingsPropertyGroupDefinition, IEnumerable<ISettingsPropertyDefinition>>(SettingsUtils.GetAllSettingPropertyDefinitions));
			}
			return settingPropertyGroups.SelectMany(selector);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00007824 File Offset: 0x00005A24
		public static IEnumerable<SettingsPropertyGroupDefinition> GetAllSettingPropertyGroupDefinitions(this BaseSettings settings)
		{
			IEnumerable<SettingsPropertyGroupDefinition> settingPropertyGroups = settings.GetSettingPropertyGroups();
			Func<SettingsPropertyGroupDefinition, IEnumerable<SettingsPropertyGroupDefinition>> selector;
			if ((selector = BaseSettingsExtensions.<>O.<1>__GetAllSettingPropertyGroupDefinitions) == null)
			{
				selector = (BaseSettingsExtensions.<>O.<1>__GetAllSettingPropertyGroupDefinitions = new Func<SettingsPropertyGroupDefinition, IEnumerable<SettingsPropertyGroupDefinition>>(SettingsUtils.GetAllSettingPropertyGroupDefinitions));
			}
			return settingPropertyGroups.SelectMany(selector);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000784C File Offset: 0x00005A4C
		public static IEnumerable<ISettingsPreset> GetExternalPresets(this BaseSettings settings)
		{
			BaseSettingsProvider service = GenericServiceProvider.GetService<BaseSettingsProvider>();
			return ((service != null) ? service.GetPresets(settings.Id) : null) ?? Enumerable.Empty<ISettingsPreset>();
		}

		// Token: 0x02000197 RID: 407
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400039A RID: 922
			[Nullable(0)]
			public static Func<SettingsPropertyGroupDefinition, IEnumerable<ISettingsPropertyDefinition>> <0>__GetAllSettingPropertyDefinitions;

			// Token: 0x0400039B RID: 923
			[Nullable(0)]
			public static Func<SettingsPropertyGroupDefinition, IEnumerable<SettingsPropertyGroupDefinition>> <1>__GetAllSettingPropertyGroupDefinitions;
		}
	}
}
