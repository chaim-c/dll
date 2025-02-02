using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Bannerlord.ModuleManager;
using MCM.Common;

namespace MCM.Abstractions
{
	// Token: 0x02000053 RID: 83
	[NullableContext(1)]
	[Nullable(0)]
	internal static class CollectionExtensions
	{
		// Token: 0x060001B7 RID: 439 RVA: 0x00007870 File Offset: 0x00005A70
		public static IOrderedEnumerable<SettingsPropertyGroupDefinition> SortDefault(this IEnumerable<SettingsPropertyGroupDefinition> enumerable)
		{
			return (from x in enumerable
			orderby x.GroupName == SettingsPropertyGroupDefinition.DefaultGroupName descending, x.Order descending
			select x).ThenByDescending((SettingsPropertyGroupDefinition x) => LocalizationUtils.Localize(x.GroupName, null), new AlphanumComparatorFast());
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000078F0 File Offset: 0x00005AF0
		public static IOrderedEnumerable<ISettingsPropertyDefinition> SortDefault(this IEnumerable<ISettingsPropertyDefinition> enumerable)
		{
			return (from x in enumerable
			orderby x.Order
			select x).ThenBy((ISettingsPropertyDefinition x) => LocalizationUtils.Localize(x.DisplayName, null), new AlphanumComparatorFast());
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000794C File Offset: 0x00005B4C
		[return: Nullable(2)]
		public static SettingsPropertyGroupDefinition GetGroupFromName(this IEnumerable<SettingsPropertyGroupDefinition> groupsList, string groupName)
		{
			return groupsList.FirstOrDefault((SettingsPropertyGroupDefinition x) => x.GroupNameRaw == groupName);
		}
	}
}
