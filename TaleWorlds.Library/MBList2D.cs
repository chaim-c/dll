using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000062 RID: 98
	public class MBList2D<T> : IMBCollection
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000A8AE File Offset: 0x00008AAE
		// (set) Token: 0x06000308 RID: 776 RVA: 0x0000A8B6 File Offset: 0x00008AB6
		public int Count1 { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000A8BF File Offset: 0x00008ABF
		// (set) Token: 0x0600030A RID: 778 RVA: 0x0000A8C7 File Offset: 0x00008AC7
		public int Count2 { get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000A8D0 File Offset: 0x00008AD0
		private int Capacity
		{
			get
			{
				return this._data.Length;
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000A8DA File Offset: 0x00008ADA
		public MBList2D(int count1, int count2)
		{
			this._data = new T[count1 * count2];
			this.Count1 = count1;
			this.Count2 = count2;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000A8FE File Offset: 0x00008AFE
		public T[] RawArray
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x1700004B RID: 75
		public T this[int index1, int index2]
		{
			get
			{
				return this._data[index1 * this.Count2 + index2];
			}
			set
			{
				this._data[index1 * this.Count2 + index2] = value;
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000A938 File Offset: 0x00008B38
		public bool Contains(T item)
		{
			for (int i = 0; i < this.Count1; i++)
			{
				for (int j = 0; j < this.Count2; j++)
				{
					if (this._data[i * this.Count2 + j].Equals(item))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000A994 File Offset: 0x00008B94
		public void Clear()
		{
			for (int i = 0; i < this.Count1 * this.Count2; i++)
			{
				this._data[i] = default(T);
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000A9D0 File Offset: 0x00008BD0
		public void ResetWithNewCount(int newCount1, int newCount2)
		{
			if (this.Count1 == newCount1 && this.Count2 == newCount2)
			{
				this.Clear();
				return;
			}
			this.Count1 = newCount1;
			this.Count2 = newCount2;
			if (this.Capacity < newCount1 * newCount2)
			{
				this._data = new T[MathF.Max(this.Capacity * 2, newCount1 * newCount2)];
				return;
			}
			this.Clear();
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000AA34 File Offset: 0x00008C34
		public void CopyRowTo(int sourceIndex1, int sourceIndex2, MBList2D<T> destination, int destinationIndex1, int destinationIndex2, int copyCount)
		{
			for (int i = 0; i < copyCount; i++)
			{
				destination[destinationIndex1, destinationIndex2 + i] = this[sourceIndex1, sourceIndex2 + i];
			}
		}

		// Token: 0x04000102 RID: 258
		private T[] _data;
	}
}
