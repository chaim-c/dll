using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ComparerExtensions
{
	// Token: 0x0200000B RID: 11
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class TypedKeyComparer<[Nullable(2)] T, [Nullable(2)] TKey> : KeyComparer<T>
	{
		// Token: 0x06000025 RID: 37 RVA: 0x0000267C File Offset: 0x0000087C
		public TypedKeyComparer(Func<T, TKey> keySelector, IComparer<TKey> keyComparer)
		{
			this._keySelector = keySelector;
			this._keyComparer = keyComparer;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002694 File Offset: 0x00000894
		// (set) Token: 0x06000027 RID: 39 RVA: 0x0000269C File Offset: 0x0000089C
		public bool Descending { get; set; }

		// Token: 0x06000028 RID: 40 RVA: 0x000026A8 File Offset: 0x000008A8
		public override int Compare(T x, T y)
		{
			TKey key = this._keySelector(x);
			TKey key2 = this._keySelector(y);
			return this.Descending ? this._keyComparer.Compare(key2, key) : this._keyComparer.Compare(key, key2);
		}

		// Token: 0x04000007 RID: 7
		private readonly Func<T, TKey> _keySelector;

		// Token: 0x04000008 RID: 8
		private readonly IComparer<TKey> _keyComparer;
	}
}
