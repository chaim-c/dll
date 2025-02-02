using System;
using System.Diagnostics.CodeAnalysis;

namespace MCM.LightInject
{
	// Token: 0x02000119 RID: 281
	[ExcludeFromCodeCoverage]
	internal sealed class ImmutableHashTable<TKey, TValue>
	{
		// Token: 0x060006A9 RID: 1705 RVA: 0x000150A4 File Offset: 0x000132A4
		internal ImmutableHashTable(ImmutableHashTable<TKey, TValue> previous, TKey key, TValue value)
		{
			this.Count = previous.Count + 1;
			bool flag = previous.Count >= previous.Divisor;
			if (flag)
			{
				this.Divisor = previous.Divisor * 2;
				this.Buckets = new ImmutableHashTree<TKey, TValue>[this.Divisor];
				this.InitializeBuckets(0, this.Divisor);
				this.AddExistingValues(previous);
			}
			else
			{
				this.Divisor = previous.Divisor;
				this.Buckets = new ImmutableHashTree<TKey, TValue>[this.Divisor];
				Array.Copy(previous.Buckets, this.Buckets, previous.Divisor);
			}
			int hashCode = key.GetHashCode();
			int bucketIndex = hashCode & this.Divisor - 1;
			this.Buckets[bucketIndex] = this.Buckets[bucketIndex].Add(key, value);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0001517C File Offset: 0x0001337C
		private ImmutableHashTable()
		{
			this.Buckets = new ImmutableHashTree<TKey, TValue>[2];
			this.Divisor = 2;
			this.InitializeBuckets(0, 2);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x000151A4 File Offset: 0x000133A4
		private void AddExistingValues(ImmutableHashTable<TKey, TValue> previous)
		{
			foreach (ImmutableHashTree<TKey, TValue> bucket in previous.Buckets)
			{
				foreach (KeyValue<TKey, TValue> keyValue in bucket.InOrder<TKey, TValue>())
				{
					TKey key = keyValue.Key;
					int hashCode = key.GetHashCode();
					int bucketIndex = hashCode & this.Divisor - 1;
					this.Buckets[bucketIndex] = this.Buckets[bucketIndex].Add(keyValue.Key, keyValue.Value);
				}
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001525C File Offset: 0x0001345C
		private void InitializeBuckets(int startIndex, int count)
		{
			for (int i = startIndex; i < count; i++)
			{
				this.Buckets[i] = ImmutableHashTree<TKey, TValue>.Empty;
			}
		}

		// Token: 0x040001F4 RID: 500
		public static readonly ImmutableHashTable<TKey, TValue> Empty = new ImmutableHashTable<TKey, TValue>();

		// Token: 0x040001F5 RID: 501
		public readonly int Count;

		// Token: 0x040001F6 RID: 502
		internal readonly ImmutableHashTree<TKey, TValue>[] Buckets;

		// Token: 0x040001F7 RID: 503
		internal readonly int Divisor;
	}
}
