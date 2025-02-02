using System;
using System.Diagnostics.CodeAnalysis;

namespace MCM.LightInject
{
	// Token: 0x0200011A RID: 282
	[ExcludeFromCodeCoverage]
	internal sealed class ImmutableHashTree<TKey, TValue>
	{
		// Token: 0x060006AE RID: 1710 RVA: 0x00015298 File Offset: 0x00013498
		public ImmutableHashTree(TKey key, TValue value, ImmutableHashTree<TKey, TValue> hashTree)
		{
			this.Duplicates = hashTree.Duplicates.Add(new KeyValue<TKey, TValue>(key, value));
			this.Key = hashTree.Key;
			this.Value = hashTree.Value;
			this.Height = hashTree.Height;
			this.HashCode = hashTree.HashCode;
			this.Left = hashTree.Left;
			this.Right = hashTree.Right;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00015310 File Offset: 0x00013510
		public ImmutableHashTree(TKey key, TValue value, ImmutableHashTree<TKey, TValue> left, ImmutableHashTree<TKey, TValue> right)
		{
			int balance = left.Height - right.Height;
			bool flag = balance == -2;
			if (flag)
			{
				bool flag2 = right.IsLeftHeavy();
				if (flag2)
				{
					right = ImmutableHashTree<TKey, TValue>.RotateRight(right);
				}
				this.Key = right.Key;
				this.Value = right.Value;
				this.Left = new ImmutableHashTree<TKey, TValue>(key, value, left, right.Left);
				this.Right = right.Right;
			}
			else
			{
				bool flag3 = balance == 2;
				if (flag3)
				{
					bool flag4 = left.IsRightHeavy();
					if (flag4)
					{
						left = ImmutableHashTree<TKey, TValue>.RotateLeft(left);
					}
					this.Key = left.Key;
					this.Value = left.Value;
					this.Right = new ImmutableHashTree<TKey, TValue>(key, value, left.Right, right);
					this.Left = left.Left;
				}
				else
				{
					this.Key = key;
					this.Value = value;
					this.Left = left;
					this.Right = right;
				}
			}
			this.Height = 1 + Math.Max(this.Left.Height, this.Right.Height);
			this.Duplicates = ImmutableList<KeyValue<TKey, TValue>>.Empty;
			this.HashCode = this.Key.GetHashCode();
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00015452 File Offset: 0x00013652
		private ImmutableHashTree()
		{
			this.IsEmpty = true;
			this.Duplicates = ImmutableList<KeyValue<TKey, TValue>>.Empty;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00015470 File Offset: 0x00013670
		private static ImmutableHashTree<TKey, TValue> RotateLeft(ImmutableHashTree<TKey, TValue> node)
		{
			return new ImmutableHashTree<TKey, TValue>(node.Right.Key, node.Right.Value, new ImmutableHashTree<TKey, TValue>(node.Key, node.Value, node.Left, node.Right.Left), node.Right.Right);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x000154CC File Offset: 0x000136CC
		private static ImmutableHashTree<TKey, TValue> RotateRight(ImmutableHashTree<TKey, TValue> node)
		{
			return new ImmutableHashTree<TKey, TValue>(node.Left.Key, node.Left.Value, node.Left.Left, new ImmutableHashTree<TKey, TValue>(node.Key, node.Value, node.Left.Right, node.Right));
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00015526 File Offset: 0x00013726
		private bool IsLeftHeavy()
		{
			return this.Left.Height > this.Right.Height;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00015540 File Offset: 0x00013740
		private bool IsRightHeavy()
		{
			return this.Right.Height > this.Left.Height;
		}

		// Token: 0x040001F8 RID: 504
		public static readonly ImmutableHashTree<TKey, TValue> Empty = new ImmutableHashTree<TKey, TValue>();

		// Token: 0x040001F9 RID: 505
		public readonly TKey Key;

		// Token: 0x040001FA RID: 506
		public readonly TValue Value;

		// Token: 0x040001FB RID: 507
		public readonly ImmutableList<KeyValue<TKey, TValue>> Duplicates;

		// Token: 0x040001FC RID: 508
		public readonly int HashCode;

		// Token: 0x040001FD RID: 509
		public readonly ImmutableHashTree<TKey, TValue> Left;

		// Token: 0x040001FE RID: 510
		public readonly ImmutableHashTree<TKey, TValue> Right;

		// Token: 0x040001FF RID: 511
		public readonly int Height;

		// Token: 0x04000200 RID: 512
		public readonly bool IsEmpty;
	}
}
