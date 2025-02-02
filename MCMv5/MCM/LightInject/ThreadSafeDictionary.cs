using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MCM.LightInject
{
	// Token: 0x020000F2 RID: 242
	[ExcludeFromCodeCoverage]
	internal class ThreadSafeDictionary<TKey, TValue> : ConcurrentDictionary<TKey, TValue>
	{
		// Token: 0x060005D6 RID: 1494 RVA: 0x00012205 File Offset: 0x00010405
		public ThreadSafeDictionary()
		{
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0001220F File Offset: 0x0001040F
		public ThreadSafeDictionary(IEqualityComparer<TKey> comparer) : base(comparer)
		{
		}
	}
}
