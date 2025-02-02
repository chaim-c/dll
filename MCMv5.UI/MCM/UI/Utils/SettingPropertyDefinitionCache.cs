using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HarmonyLib.BUTR.Extensions;
using MCM.Abstractions;
using MCM.Abstractions.Base;

namespace MCM.UI.Utils
{
	// Token: 0x02000012 RID: 18
	[NullableContext(1)]
	[Nullable(0)]
	internal static class SettingPropertyDefinitionCache
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00003179 File Offset: 0x00001379
		public static void Clear()
		{
			SettingPropertyDefinitionCache.ClearDelegate clearMethod = SettingPropertyDefinitionCache.ClearMethod;
			if (clearMethod != null)
			{
				clearMethod(SettingPropertyDefinitionCache._cache);
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003194 File Offset: 0x00001394
		public static IEnumerable<SettingsPropertyGroupDefinition> GetSettingPropertyGroups(BaseSettings settings)
		{
			List<SettingsPropertyGroupDefinition> list;
			bool flag = !SettingPropertyDefinitionCache._cache.TryGetValue(settings, out list);
			if (flag)
			{
				list = settings.GetSettingPropertyGroups();
				SettingPropertyDefinitionCache._cache.Add(settings, list);
			}
			return list;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000031D1 File Offset: 0x000013D1
		public static IEnumerable<ISettingsPropertyDefinition> GetAllSettingPropertyDefinitions(BaseSettings settings)
		{
			IEnumerable<SettingsPropertyGroupDefinition> settingPropertyGroups = SettingPropertyDefinitionCache.GetSettingPropertyGroups(settings);
			Func<SettingsPropertyGroupDefinition, IEnumerable<ISettingsPropertyDefinition>> selector;
			if ((selector = SettingPropertyDefinitionCache.<>O.<0>__GetAllSettingPropertyDefinitions) == null)
			{
				selector = (SettingPropertyDefinitionCache.<>O.<0>__GetAllSettingPropertyDefinitions = new Func<SettingsPropertyGroupDefinition, IEnumerable<ISettingsPropertyDefinition>>(SettingsUtils.GetAllSettingPropertyDefinitions));
			}
			return settingPropertyGroups.SelectMany(selector);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000031F9 File Offset: 0x000013F9
		public static IEnumerable<SettingsPropertyGroupDefinition> GetAllSettingPropertyGroupDefinitions(BaseSettings settings)
		{
			IEnumerable<SettingsPropertyGroupDefinition> settingPropertyGroups = SettingPropertyDefinitionCache.GetSettingPropertyGroups(settings);
			Func<SettingsPropertyGroupDefinition, IEnumerable<SettingsPropertyGroupDefinition>> selector;
			if ((selector = SettingPropertyDefinitionCache.<>O.<1>__GetAllSettingPropertyGroupDefinitions) == null)
			{
				selector = (SettingPropertyDefinitionCache.<>O.<1>__GetAllSettingPropertyGroupDefinitions = new Func<SettingsPropertyGroupDefinition, IEnumerable<SettingsPropertyGroupDefinition>>(SettingsUtils.GetAllSettingPropertyGroupDefinitions));
			}
			return settingPropertyGroups.SelectMany(selector);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003224 File Offset: 0x00001424
		public static bool Equals(BaseSettings settings1, BaseSettings settings2)
		{
			Dictionary<ValueTuple<string, string>, ISettingsPropertyDefinition> setDict = SettingPropertyDefinitionCache.GetAllSettingPropertyDefinitions(settings1).ToDictionary((ISettingsPropertyDefinition x) => new ValueTuple<string, string>(x.DisplayName, x.GroupName), (ISettingsPropertyDefinition x) => x);
			Dictionary<ValueTuple<string, string>, ISettingsPropertyDefinition> setDict2 = SettingPropertyDefinitionCache.GetAllSettingPropertyDefinitions(settings2).ToDictionary((ISettingsPropertyDefinition x) => new ValueTuple<string, string>(x.DisplayName, x.GroupName), (ISettingsPropertyDefinition x) => x);
			bool flag = setDict.Count != setDict2.Count;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				foreach (KeyValuePair<ValueTuple<string, string>, ISettingsPropertyDefinition> kv in setDict)
				{
					ISettingsPropertyDefinition spd2;
					bool flag2 = !setDict2.TryGetValue(kv.Key, out spd2) || !SettingsUtils.Equals(kv.Value, spd2);
					if (flag2)
					{
						return false;
					}
				}
				result = true;
			}
			return result;
		}

		// Token: 0x0400001D RID: 29
		[Nullable(2)]
		private static readonly SettingPropertyDefinitionCache.ClearDelegate ClearMethod = AccessTools2.GetDelegate<SettingPropertyDefinitionCache.ClearDelegate>(typeof(ConditionalWeakTable<BaseSettings, List<SettingsPropertyGroupDefinition>>), "Clear", null, null, true);

		// Token: 0x0400001E RID: 30
		private static readonly ConditionalWeakTable<BaseSettings, List<SettingsPropertyGroupDefinition>> _cache = new ConditionalWeakTable<BaseSettings, List<SettingsPropertyGroupDefinition>>();

		// Token: 0x02000082 RID: 130
		// (Invoke) Token: 0x060004CE RID: 1230
		[NullableContext(0)]
		private delegate void ClearDelegate(ConditionalWeakTable<BaseSettings, List<SettingsPropertyGroupDefinition>> instance);

		// Token: 0x02000083 RID: 131
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400016E RID: 366
			[Nullable(0)]
			public static Func<SettingsPropertyGroupDefinition, IEnumerable<ISettingsPropertyDefinition>> <0>__GetAllSettingPropertyDefinitions;

			// Token: 0x0400016F RID: 367
			[Nullable(0)]
			public static Func<SettingsPropertyGroupDefinition, IEnumerable<SettingsPropertyGroupDefinition>> <1>__GetAllSettingPropertyGroupDefinitions;
		}
	}
}
