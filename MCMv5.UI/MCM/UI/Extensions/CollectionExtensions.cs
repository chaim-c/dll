using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TaleWorlds.Library;

namespace MCM.UI.Extensions
{
	// Token: 0x02000027 RID: 39
	[NullableContext(1)]
	[Nullable(0)]
	internal static class CollectionExtensions
	{
		// Token: 0x0600017E RID: 382 RVA: 0x00007F48 File Offset: 0x00006148
		public static MBBindingList<T> AddRange<[Nullable(2)] T>(this MBBindingList<T> list, IEnumerable<T> range)
		{
			foreach (T item in range)
			{
				list.Add(item);
			}
			return list;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00007F9C File Offset: 0x0000619C
		public static IEnumerable<T> Parallel<[Nullable(2)] T>(this IEnumerable<T> enumerable)
		{
			return enumerable.AsParallel<T>().AsOrdered<T>().WithExecutionMode(ParallelExecutionMode.ForceParallelism);
		}
	}
}
