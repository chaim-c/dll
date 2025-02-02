using System;
using System.Collections;
using System.Collections.Generic;

namespace TaleWorlds.Library
{
	// Token: 0x02000068 RID: 104
	public class MBQueue<T> : IMBCollection, ICollection, IEnumerable, IEnumerable<T>
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000C1B0 File Offset: 0x0000A3B0
		public int Count
		{
			get
			{
				return this._data.Count;
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000C1BD File Offset: 0x0000A3BD
		public MBQueue()
		{
			this._data = new Queue<T>();
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000C1D0 File Offset: 0x0000A3D0
		public MBQueue(Queue<T> queue)
		{
			this._data = new Queue<T>(queue);
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000C1E4 File Offset: 0x0000A3E4
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000C1E7 File Offset: 0x0000A3E7
		public object SyncRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000C1EA File Offset: 0x0000A3EA
		public bool Contains(T item)
		{
			return this._data.Contains(item);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000C1F8 File Offset: 0x0000A3F8
		public IEnumerator<T> GetEnumerator()
		{
			return this._data.GetEnumerator();
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000C20A File Offset: 0x0000A40A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000C212 File Offset: 0x0000A412
		public void Clear()
		{
			this._data.Clear();
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000C21F File Offset: 0x0000A41F
		public void Enqueue(T item)
		{
			this._data.Enqueue(item);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000C22D File Offset: 0x0000A42D
		public T Dequeue()
		{
			return this._data.Dequeue();
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000C23A File Offset: 0x0000A43A
		public void CopyTo(Array array, int index)
		{
			this._data.CopyTo((T[])array, index);
		}

		// Token: 0x04000117 RID: 279
		private readonly Queue<T> _data;
	}
}
