using System;
using System.Collections;
using System.Collections.Generic;

namespace TaleWorlds.DotNet
{
	// Token: 0x02000027 RID: 39
	public sealed class NativeArrayEnumerator<T> : IReadOnlyList<T>, IEnumerable<!0>, IEnumerable, IReadOnlyCollection<T> where T : struct
	{
		// Token: 0x060000EE RID: 238 RVA: 0x00004B69 File Offset: 0x00002D69
		public NativeArrayEnumerator(NativeArray nativeArray)
		{
			this._nativeArray = nativeArray;
		}

		// Token: 0x17000022 RID: 34
		public T this[int index]
		{
			get
			{
				return this._nativeArray.GetElementAt<T>(index);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00004B86 File Offset: 0x00002D86
		public int Count
		{
			get
			{
				return this._nativeArray.GetLength<T>();
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004B93 File Offset: 0x00002D93
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this._nativeArray.GetEnumerator<T>().GetEnumerator();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004BA5 File Offset: 0x00002DA5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._nativeArray.GetEnumerator<T>().GetEnumerator();
		}

		// Token: 0x0400005F RID: 95
		private readonly NativeArray _nativeArray;
	}
}
