using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HarmonyLib.BUTR.Extensions;
using MCM.Abstractions;
using MCM.Abstractions.Base;
using MCM.Abstractions.Properties;
using MCM.Abstractions.Wrapper;

namespace MCM.Implementation
{
	// Token: 0x02000027 RID: 39
	internal sealed class FluentSettingsPropertyDiscoverer : IFluentSettingsPropertyDiscoverer, ISettingsPropertyDiscoverer
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00005185 File Offset: 0x00003385
		[Nullable(1)]
		public IEnumerable<string> DiscoveryTypes { [NullableContext(1)] get; } = new string[]
		{
			"fluent"
		};

		// Token: 0x060000DF RID: 223 RVA: 0x00005190 File Offset: 0x00003390
		[NullableContext(1)]
		public IEnumerable<ISettingsPropertyDefinition> GetProperties(BaseSettings settings)
		{
			FluentSettingsPropertyDiscoverer.GetSettingPropertyGroupsDelegate getSettingPropertyGroups = FluentSettingsPropertyDiscoverer._getSettingPropertyGroups;
			List<SettingsPropertyGroupDefinition> list;
			if (getSettingPropertyGroups == null)
			{
				list = null;
			}
			else
			{
				list = (from x in getSettingPropertyGroups(settings)
				select new SettingsPropertyGroupDefinitionWrapper(x)).Cast<SettingsPropertyGroupDefinition>().ToList<SettingsPropertyGroupDefinition>();
			}
			List<SettingsPropertyGroupDefinition> settingPropertyGroups = list ?? new List<SettingsPropertyGroupDefinition>();
			IEnumerable<SettingsPropertyGroupDefinition> source = settingPropertyGroups;
			Func<SettingsPropertyGroupDefinition, IEnumerable<ISettingsPropertyDefinition>> selector;
			if ((selector = FluentSettingsPropertyDiscoverer.<>O.<0>__GetAllSettingPropertyDefinitions) == null)
			{
				selector = (FluentSettingsPropertyDiscoverer.<>O.<0>__GetAllSettingPropertyDefinitions = new Func<SettingsPropertyGroupDefinition, IEnumerable<ISettingsPropertyDefinition>>(SettingsUtils.GetAllSettingPropertyDefinitions));
			}
			return source.SelectMany(selector);
		}

		// Token: 0x04000045 RID: 69
		[Nullable(2)]
		private static readonly FluentSettingsPropertyDiscoverer.GetSettingPropertyGroupsDelegate _getSettingPropertyGroups = AccessTools2.GetPropertyGetterDelegate<FluentSettingsPropertyDiscoverer.GetSettingPropertyGroupsDelegate>("MCM.Abstractions.Base.IFluentSettings:SettingPropertyGroups", true);

		// Token: 0x0200017C RID: 380
		// (Invoke) Token: 0x06000A44 RID: 2628
		private delegate List<SettingsPropertyGroupDefinition> GetSettingPropertyGroupsDelegate(object instance);

		// Token: 0x0200017D RID: 381
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000302 RID: 770
			public static Func<SettingsPropertyGroupDefinition, IEnumerable<ISettingsPropertyDefinition>> <0>__GetAllSettingPropertyDefinitions;
		}
	}
}
