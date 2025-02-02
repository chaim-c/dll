using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MCM.LightInject
{
	// Token: 0x020000E7 RID: 231
	[ExcludeFromCodeCoverage]
	internal static class ImmutableHashTableExtensions
	{
		// Token: 0x060004E6 RID: 1254 RVA: 0x0000D080 File Offset: 0x0000B280
		[ExcludeFromCodeCoverage]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TValue Search<TKey, TValue>(this ImmutableHashTable<TKey, TValue> hashTable, TKey key)
		{
			int hashCode = key.GetHashCode();
			int bucketIndex = hashCode & hashTable.Divisor - 1;
			ImmutableHashTree<TKey, TValue> tree = hashTable.Buckets[bucketIndex];
			while (tree.Height != 0 && tree.HashCode != hashCode)
			{
				tree = ((hashCode < tree.HashCode) ? tree.Left : tree.Right);
			}
			bool flag = tree.Height != 0 && (tree.Key == key || object.Equals(tree.Key, key));
			TValue result;
			if (flag)
			{
				result = tree.Value;
			}
			else
			{
				bool flag2 = tree.Duplicates.Items.Length != 0;
				if (flag2)
				{
					foreach (KeyValue<TKey, TValue> keyValue in tree.Duplicates.Items)
					{
						bool flag3 = keyValue.Key == key || object.Equals(keyValue.Key, key);
						if (flag3)
						{
							return keyValue.Value;
						}
					}
				}
				result = default(TValue);
			}
			return result;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000D1C3 File Offset: 0x0000B3C3
		public static ImmutableHashTable<TKey, TValue> Add<TKey, TValue>(this ImmutableHashTable<TKey, TValue> hashTable, TKey key, TValue value)
		{
			return new ImmutableHashTable<TKey, TValue>(hashTable, key, value);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
		[ExcludeFromCodeCoverage]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static GetInstanceDelegate Search(this ImmutableHashTable<Type, GetInstanceDelegate> hashTable, Type key)
		{
			int hashCode = key.GetHashCode();
			int bucketIndex = hashCode & hashTable.Divisor - 1;
			ImmutableHashTree<Type, GetInstanceDelegate> tree = hashTable.Buckets[bucketIndex];
			while (tree.Height != 0 && tree.HashCode != hashCode)
			{
				tree = ((hashCode < tree.HashCode) ? tree.Left : tree.Right);
			}
			bool flag = tree.Height != 0 && tree.Key == key;
			GetInstanceDelegate result;
			if (flag)
			{
				result = tree.Value;
			}
			else
			{
				bool flag2 = tree.Duplicates.Items.Length != 0;
				if (flag2)
				{
					foreach (KeyValue<Type, GetInstanceDelegate> keyValue in tree.Duplicates.Items)
					{
						bool flag3 = keyValue.Key == key;
						if (flag3)
						{
							return keyValue.Value;
						}
					}
				}
				result = null;
			}
			return result;
		}
	}
}
