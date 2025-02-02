using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MCM.LightInject
{
	// Token: 0x020000E9 RID: 233
	[ExcludeFromCodeCoverage]
	internal static class ImmutableHashTreeExtensions
	{
		// Token: 0x060004ED RID: 1261 RVA: 0x0000D3D8 File Offset: 0x0000B5D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TValue Search<TKey, TValue>(this ImmutableHashTree<TKey, TValue> tree, TKey key)
		{
			int hashCode = key.GetHashCode();
			while (tree.Height != 0 && tree.HashCode != hashCode)
			{
				tree = ((hashCode < tree.HashCode) ? tree.Left : tree.Right);
			}
			bool flag = !tree.IsEmpty && (tree.Key == key || object.Equals(tree.Key, key));
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

		// Token: 0x060004EE RID: 1262 RVA: 0x0000D504 File Offset: 0x0000B704
		public static ImmutableHashTree<TKey, TValue> Add<TKey, TValue>(this ImmutableHashTree<TKey, TValue> tree, TKey key, TValue value)
		{
			bool isEmpty = tree.IsEmpty;
			ImmutableHashTree<TKey, TValue> result;
			if (isEmpty)
			{
				result = new ImmutableHashTree<TKey, TValue>(key, value, tree, tree);
			}
			else
			{
				int hashCode = key.GetHashCode();
				bool flag = hashCode > tree.HashCode;
				if (flag)
				{
					result = ImmutableHashTreeExtensions.AddToRightBranch<TKey, TValue>(tree, key, value);
				}
				else
				{
					bool flag2 = hashCode < tree.HashCode;
					if (flag2)
					{
						result = ImmutableHashTreeExtensions.AddToLeftBranch<TKey, TValue>(tree, key, value);
					}
					else
					{
						result = new ImmutableHashTree<TKey, TValue>(key, value, tree);
					}
				}
			}
			return result;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0000D577 File Offset: 0x0000B777
		public static IEnumerable<KeyValue<TKey, TValue>> InOrder<TKey, TValue>(this ImmutableHashTree<TKey, TValue> hashTree)
		{
			bool flag = !hashTree.IsEmpty;
			if (flag)
			{
				foreach (KeyValue<TKey, TValue> left in hashTree.Left.InOrder<TKey, TValue>())
				{
					yield return new KeyValue<TKey, TValue>(left.Key, left.Value);
					left = null;
				}
				IEnumerator<KeyValue<TKey, TValue>> enumerator = null;
				yield return new KeyValue<TKey, TValue>(hashTree.Key, hashTree.Value);
				int num;
				for (int i = 0; i < hashTree.Duplicates.Items.Length; i = num + 1)
				{
					yield return hashTree.Duplicates.Items[i];
					num = i;
				}
				foreach (KeyValue<TKey, TValue> right in hashTree.Right.InOrder<TKey, TValue>())
				{
					yield return new KeyValue<TKey, TValue>(right.Key, right.Value);
					right = null;
				}
				IEnumerator<KeyValue<TKey, TValue>> enumerator2 = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0000D587 File Offset: 0x0000B787
		private static ImmutableHashTree<TKey, TValue> AddToLeftBranch<TKey, TValue>(ImmutableHashTree<TKey, TValue> tree, TKey key, TValue value)
		{
			return new ImmutableHashTree<TKey, TValue>(tree.Key, tree.Value, tree.Left.Add(key, value), tree.Right);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0000D5AD File Offset: 0x0000B7AD
		private static ImmutableHashTree<TKey, TValue> AddToRightBranch<TKey, TValue>(ImmutableHashTree<TKey, TValue> tree, TKey key, TValue value)
		{
			return new ImmutableHashTree<TKey, TValue>(tree.Key, tree.Value, tree.Left, tree.Right.Add(key, value));
		}
	}
}
