using System;
using System.Diagnostics.CodeAnalysis;

namespace MCM.LightInject
{
	// Token: 0x0200011B RID: 283
	[ExcludeFromCodeCoverage]
	internal sealed class ImmutableMapTree<TValue>
	{
		// Token: 0x060006B6 RID: 1718 RVA: 0x00015566 File Offset: 0x00013766
		public ImmutableMapTree(int key, TValue value, ImmutableMapTree<TValue> hashTree)
		{
			this.Key = key;
			this.Value = value;
			this.Height = hashTree.Height;
			this.Left = hashTree.Left;
			this.Right = hashTree.Right;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x000155A4 File Offset: 0x000137A4
		public ImmutableMapTree(int key, TValue value, ImmutableMapTree<TValue> left, ImmutableMapTree<TValue> right)
		{
			int balance = left.Height - right.Height;
			bool flag = balance == -2;
			if (flag)
			{
				bool flag2 = right.IsLeftHeavy();
				if (flag2)
				{
					right = ImmutableMapTree<TValue>.RotateRight(right);
				}
				this.Key = right.Key;
				this.Value = right.Value;
				this.Left = new ImmutableMapTree<TValue>(key, value, left, right.Left);
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
						left = ImmutableMapTree<TValue>.RotateLeft(left);
					}
					this.Key = left.Key;
					this.Value = left.Value;
					this.Right = new ImmutableMapTree<TValue>(key, value, left.Right, right);
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
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x000156C4 File Offset: 0x000138C4
		private ImmutableMapTree()
		{
			this.IsEmpty = true;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x000156D8 File Offset: 0x000138D8
		private static ImmutableMapTree<TValue> RotateLeft(ImmutableMapTree<TValue> node)
		{
			return new ImmutableMapTree<TValue>(node.Right.Key, node.Right.Value, new ImmutableMapTree<TValue>(node.Key, node.Value, node.Left, node.Right.Left), node.Right.Right);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00015734 File Offset: 0x00013934
		private static ImmutableMapTree<TValue> RotateRight(ImmutableMapTree<TValue> node)
		{
			return new ImmutableMapTree<TValue>(node.Left.Key, node.Left.Value, node.Left.Left, new ImmutableMapTree<TValue>(node.Key, node.Value, node.Left.Right, node.Right));
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0001578E File Offset: 0x0001398E
		private bool IsLeftHeavy()
		{
			return this.Left.Height > this.Right.Height;
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x000157A8 File Offset: 0x000139A8
		private bool IsRightHeavy()
		{
			return this.Right.Height > this.Left.Height;
		}

		// Token: 0x04000201 RID: 513
		public static readonly ImmutableMapTree<TValue> Empty = new ImmutableMapTree<TValue>();

		// Token: 0x04000202 RID: 514
		public readonly int Key;

		// Token: 0x04000203 RID: 515
		public readonly TValue Value;

		// Token: 0x04000204 RID: 516
		public readonly ImmutableMapTree<TValue> Left;

		// Token: 0x04000205 RID: 517
		public readonly ImmutableMapTree<TValue> Right;

		// Token: 0x04000206 RID: 518
		public readonly int Height;

		// Token: 0x04000207 RID: 519
		public readonly bool IsEmpty;
	}
}
