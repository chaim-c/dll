using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MCM.LightInject
{
	// Token: 0x020000E8 RID: 232
	[ExcludeFromCodeCoverage]
	internal static class ImmutableMapTreeExtensions
	{
		// Token: 0x060004E9 RID: 1257 RVA: 0x0000D2BC File Offset: 0x0000B4BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TValue Search<TValue>(this ImmutableMapTree<TValue> tree, int key)
		{
			while (tree.Height != 0 && tree.Key != key)
			{
				tree = ((key < tree.Key) ? tree.Left : tree.Right);
			}
			bool flag = !tree.IsEmpty;
			TValue result;
			if (flag)
			{
				result = tree.Value;
			}
			else
			{
				result = default(TValue);
			}
			return result;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0000D328 File Offset: 0x0000B528
		public static ImmutableMapTree<TValue> Add<TValue>(this ImmutableMapTree<TValue> tree, int key, TValue value)
		{
			bool isEmpty = tree.IsEmpty;
			ImmutableMapTree<TValue> result;
			if (isEmpty)
			{
				result = new ImmutableMapTree<TValue>(key, value, tree, tree);
			}
			else
			{
				bool flag = key > tree.Key;
				if (flag)
				{
					result = ImmutableMapTreeExtensions.AddToRightBranch<TValue>(tree, key, value);
				}
				else
				{
					bool flag2 = key < tree.Key;
					if (flag2)
					{
						result = ImmutableMapTreeExtensions.AddToLeftBranch<TValue>(tree, key, value);
					}
					else
					{
						result = new ImmutableMapTree<TValue>(key, value, tree);
					}
				}
			}
			return result;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0000D38B File Offset: 0x0000B58B
		private static ImmutableMapTree<TValue> AddToLeftBranch<TValue>(ImmutableMapTree<TValue> tree, int key, TValue value)
		{
			return new ImmutableMapTree<TValue>(tree.Key, tree.Value, tree.Left.Add(key, value), tree.Right);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000D3B1 File Offset: 0x0000B5B1
		private static ImmutableMapTree<TValue> AddToRightBranch<TValue>(ImmutableMapTree<TValue> tree, int key, TValue value)
		{
			return new ImmutableMapTree<TValue>(tree.Key, tree.Value, tree.Left, tree.Right.Add(key, value));
		}
	}
}
