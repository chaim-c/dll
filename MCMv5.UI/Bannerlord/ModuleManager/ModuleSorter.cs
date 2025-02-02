using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Bannerlord.ModuleManager
{
	// Token: 0x02000055 RID: 85
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ModuleSorter
	{
		// Token: 0x06000389 RID: 905 RVA: 0x0000EA78 File Offset: 0x0000CC78
		public static IList<ModuleInfoExtended> Sort(IReadOnlyCollection<ModuleInfoExtended> source)
		{
			ModuleInfoExtended[] correctModules = (from x in source
			where ModuleUtilities.AreDependenciesPresent(source, x)
			orderby x.IsOfficial descending
			select x).ThenByDescending((ModuleInfoExtended mim) => mim.Id, new AlphanumComparatorFast()).ToArray<ModuleInfoExtended>();
			return ModuleSorter.TopologySort<ModuleInfoExtended>(correctModules, (ModuleInfoExtended module) => ModuleUtilities.GetDependencies(correctModules, module));
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000EB20 File Offset: 0x0000CD20
		public static IList<ModuleInfoExtended> Sort(IReadOnlyCollection<ModuleInfoExtended> source, ModuleSorterOptions options)
		{
			ModuleInfoExtended[] correctModules = (from x in source
			where ModuleUtilities.AreDependenciesPresent(source, x)
			orderby x.IsOfficial descending
			select x).ThenByDescending((ModuleInfoExtended mim) => mim.Id, new AlphanumComparatorFast()).ToArray<ModuleInfoExtended>();
			return ModuleSorter.TopologySort<ModuleInfoExtended>(correctModules, (ModuleInfoExtended module) => ModuleUtilities.GetDependencies(correctModules, module, options));
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000EBCC File Offset: 0x0000CDCC
		public static IList<T> TopologySort<[Nullable(2)] T>(IEnumerable<T> source, Func<T, IEnumerable<T>> getDependencies)
		{
			List<T> list = new List<T>();
			HashSet<T> visited = new HashSet<T>();
			Action<T> <>9__0;
			foreach (T item3 in source)
			{
				T item2 = item3;
				Action<T> addItem;
				if ((addItem = <>9__0) == null)
				{
					addItem = (<>9__0 = delegate(T item)
					{
						list.Add(item);
					});
				}
				ModuleSorter.Visit<T>(item2, getDependencies, addItem, visited);
			}
			return list;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000EC60 File Offset: 0x0000CE60
		public static void Visit<[Nullable(2)] T>(T item, Func<T, IEnumerable<T>> getDependencies, Action<T> addItem, HashSet<T> visited)
		{
			bool flag = visited.Contains(item);
			if (!flag)
			{
				visited.Add(item);
				IEnumerable<T> enumerable = getDependencies(item);
				bool flag2 = enumerable != null;
				if (flag2)
				{
					foreach (T item2 in enumerable)
					{
						ModuleSorter.Visit<T>(item2, getDependencies, addItem, visited);
					}
				}
				addItem(item);
			}
		}
	}
}
