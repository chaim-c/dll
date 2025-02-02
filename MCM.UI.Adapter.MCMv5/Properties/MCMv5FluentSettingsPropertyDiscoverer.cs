﻿using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HarmonyLib.BUTR.Extensions;
using MCM.Abstractions;
using MCM.Abstractions.Base;
using MCM.Abstractions.Properties;
using MCM.Abstractions.Wrapper;
using MCM.Common;

namespace MCM.UI.Adapter.MCMv5.Properties
{
	// Token: 0x0200000D RID: 13
	internal sealed class MCMv5FluentSettingsPropertyDiscoverer : IAttributeSettingsPropertyDiscoverer, ISettingsPropertyDiscoverer
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000029DF File Offset: 0x00000BDF
		[Nullable(1)]
		public IEnumerable<string> DiscoveryTypes { [NullableContext(1)] get; } = new string[]
		{
			"mcm_v5_fluent"
		};

		// Token: 0x06000027 RID: 39 RVA: 0x000029E8 File Offset: 0x00000BE8
		[NullableContext(1)]
		public IEnumerable<ISettingsPropertyDefinition> GetProperties(BaseSettings settings)
		{
			if (!true)
			{
			}
			IWrapper wrapper = settings as IWrapper;
			object obj4;
			if (wrapper != null)
			{
				object obj2 = wrapper.Object;
				if (obj2 != null)
				{
					obj4 = obj2;
					goto IL_2A;
				}
			}
			obj4 = settings;
			IL_2A:
			if (!true)
			{
			}
			object obj3 = obj4;
			MCMv5FluentSettingsPropertyDiscoverer.GetSettingPropertyGroupsDelegate del = MCMv5FluentSettingsPropertyDiscoverer._cache.GetOrAdd(obj3.GetType(), (Type x) => AccessTools2.GetPropertyGetterDelegate<MCMv5FluentSettingsPropertyDiscoverer.GetSettingPropertyGroupsDelegate>(x, "SettingPropertyGroups", true));
			List<SettingsPropertyGroupDefinition> list;
			if (del == null)
			{
				list = null;
			}
			else
			{
				IEnumerable enumerable = del(obj3);
				if (enumerable == null)
				{
					list = null;
				}
				else
				{
					list = (from object x in enumerable
					select new SettingsPropertyGroupDefinitionWrapper(x)).Cast<SettingsPropertyGroupDefinition>().ToList<SettingsPropertyGroupDefinition>();
				}
			}
			List<SettingsPropertyGroupDefinition> settingPropertyGroups = list ?? new List<SettingsPropertyGroupDefinition>();
			IEnumerable<SettingsPropertyGroupDefinition> source = settingPropertyGroups;
			Func<SettingsPropertyGroupDefinition, IEnumerable<ISettingsPropertyDefinition>> selector;
			if ((selector = MCMv5FluentSettingsPropertyDiscoverer.<>O.<0>__GetAllSettingPropertyDefinitions) == null)
			{
				selector = (MCMv5FluentSettingsPropertyDiscoverer.<>O.<0>__GetAllSettingPropertyDefinitions = new Func<SettingsPropertyGroupDefinition, IEnumerable<ISettingsPropertyDefinition>>(SettingsUtils.GetAllSettingPropertyDefinitions));
			}
			return source.SelectMany(selector);
		}

		// Token: 0x0400000A RID: 10
		[Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		private static readonly ConcurrentDictionary<Type, MCMv5FluentSettingsPropertyDiscoverer.GetSettingPropertyGroupsDelegate> _cache = new ConcurrentDictionary<Type, MCMv5FluentSettingsPropertyDiscoverer.GetSettingPropertyGroupsDelegate>();

		// Token: 0x0200002E RID: 46
		// (Invoke) Token: 0x06000159 RID: 345
		[return: Nullable(2)]
		private delegate IEnumerable GetSettingPropertyGroupsDelegate(object instance);

		// Token: 0x0200002F RID: 47
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400006A RID: 106
			public static Func<SettingsPropertyGroupDefinition, IEnumerable<ISettingsPropertyDefinition>> <0>__GetAllSettingPropertyDefinitions;
		}
	}
}
