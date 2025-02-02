using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TaleWorlds.Library
{
	// Token: 0x02000063 RID: 99
	public class MBArrayList<T> : IMBCollection, ICollection, IEnumerable, IEnumerable<T>
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0000AA64 File Offset: 0x00008C64
		// (set) Token: 0x06000315 RID: 789 RVA: 0x0000AA6C File Offset: 0x00008C6C
		public int Count { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000AA75 File Offset: 0x00008C75
		public int Capacity
		{
			get
			{
				return this._data.Length;
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000AA7F File Offset: 0x00008C7F
		public MBArrayList()
		{
			this._data = new T[1];
			this.Count = 0;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000AA9A File Offset: 0x00008C9A
		public MBArrayList(List<T> list)
		{
			this._data = list.ToArray();
			this.Count = this._data.Length;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000AABC File Offset: 0x00008CBC
		public MBArrayList(IEnumerable<T> list)
		{
			this._data = list.ToArray<T>();
			this.Count = this._data.Length;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000AADE File Offset: 0x00008CDE
		public T[] RawArray
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000AAE6 File Offset: 0x00008CE6
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000AAE9 File Offset: 0x00008CE9
		public object SyncRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000051 RID: 81
		public T this[int index]
		{
			get
			{
				return this._data[index];
			}
			set
			{
				this._data[index] = value;
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000AB0C File Offset: 0x00008D0C
		public int IndexOf(T item)
		{
			int result = -1;
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			for (int i = 0; i < this.Count; i++)
			{
				if (@default.Equals(this._data[i], item))
				{
					result = i;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000AB4C File Offset: 0x00008D4C
		public bool Contains(T item)
		{
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			for (int i = 0; i < this.Count; i++)
			{
				if (@default.Equals(this._data[i], item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000AB88 File Offset: 0x00008D88
		public IEnumerator<T> GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.Count; i = num + 1)
			{
				yield return this._data[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000AB97 File Offset: 0x00008D97
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000ABA0 File Offset: 0x00008DA0
		public void Clear()
		{
			for (int i = 0; i < this.Count; i++)
			{
				this._data[i] = default(T);
			}
			this.Count = 0;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000ABDC File Offset: 0x00008DDC
		private void EnsureCapacity(int newMinimumCapacity)
		{
			if (newMinimumCapacity > this.Capacity)
			{
				T[] array = new T[MathF.Max(this.Capacity * 2, newMinimumCapacity)];
				this.CopyTo(array, 0);
				this._data = array;
			}
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000AC18 File Offset: 0x00008E18
		public void Add(T item)
		{
			this.EnsureCapacity(this.Count + 1);
			this._data[this.Count] = item;
			int count = this.Count;
			this.Count = count + 1;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000AC58 File Offset: 0x00008E58
		public void AddRange(IEnumerable<T> list)
		{
			foreach (T t in list)
			{
				this.EnsureCapacity(this.Count + 1);
				this._data[this.Count] = t;
				int count = this.Count;
				this.Count = count + 1;
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000ACCC File Offset: 0x00008ECC
		public bool Remove(T item)
		{
			int num = this.IndexOf(item);
			if (num >= 0)
			{
				for (int i = num; i < this.Count - 1; i++)
				{
					this._data[num] = this._data[num + 1];
				}
				int count = this.Count;
				this.Count = count - 1;
				this._data[this.Count] = default(T);
				return true;
			}
			return false;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000AD40 File Offset: 0x00008F40
		public void CopyTo(Array array, int index)
		{
			T[] array2;
			if ((array2 = (array as T[])) != null)
			{
				for (int i = 0; i < this.Count; i++)
				{
					array2[i + index] = this._data[i];
				}
				return;
			}
			array.GetType().GetElementType();
			object[] array3 = array as object[];
			try
			{
				for (int j = 0; j < this.Count; j++)
				{
					array3[index++] = this._data[j];
				}
			}
			catch (ArrayTypeMismatchException)
			{
				Debug.FailedAssert("Invalid array type", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\MBArrayList.cs", "CopyTo", 210);
			}
		}

		// Token: 0x04000105 RID: 261
		private T[] _data;
	}
}
