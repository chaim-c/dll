using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace MCM.LightInject
{
	// Token: 0x020000F3 RID: 243
	[ExcludeFromCodeCoverage]
	internal class LazyConcurrentDictionary<TKey, TValue>
	{
		// Token: 0x060005D8 RID: 1496 RVA: 0x0001221A File Offset: 0x0001041A
		public LazyConcurrentDictionary()
		{
			this.concurrentDictionary = new ConcurrentDictionary<TKey, Lazy<TValue>>();
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00012230 File Offset: 0x00010430
		public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
		{
			Lazy<TValue> lazyResult = this.concurrentDictionary.GetOrAdd(key, (TKey k) => new Lazy<TValue>(() => valueFactory(k), LazyThreadSafetyMode.ExecutionAndPublication));
			return lazyResult.Value;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00012270 File Offset: 0x00010470
		public bool ContainsUninitializedValue(TKey key)
		{
			Lazy<TValue> value;
			bool flag = this.concurrentDictionary.TryGetValue(key, out value);
			return flag && !value.IsValueCreated;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x000122A4 File Offset: 0x000104A4
		public void Remove(TKey key)
		{
			Lazy<TValue> lazy;
			this.concurrentDictionary.TryRemove(key, out lazy);
		}

		// Token: 0x040001A2 RID: 418
		private readonly ConcurrentDictionary<TKey, Lazy<TValue>> concurrentDictionary;
	}
}
