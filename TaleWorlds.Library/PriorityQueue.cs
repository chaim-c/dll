using System;
using System.Collections;
using System.Collections.Generic;

namespace TaleWorlds.Library
{
	// Token: 0x0200007B RID: 123
	public class PriorityQueue<TPriority, TValue> : ICollection<KeyValuePair<TPriority, TValue>>, IEnumerable<KeyValuePair<TPriority, TValue>>, IEnumerable
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0000D96E File Offset: 0x0000BB6E
		private IComparer<TPriority> Comparer
		{
			get
			{
				if (this._customComparer == null)
				{
					return Comparer<TPriority>.Default;
				}
				return this._customComparer;
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000D984 File Offset: 0x0000BB84
		public PriorityQueue()
		{
			this._baseHeap = new List<KeyValuePair<TPriority, TValue>>();
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000D997 File Offset: 0x0000BB97
		public PriorityQueue(int capacity)
		{
			this._baseHeap = new List<KeyValuePair<TPriority, TValue>>(capacity);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000D9AB File Offset: 0x0000BBAB
		public PriorityQueue(int capacity, IComparer<TPriority> comparer)
		{
			if (comparer == null)
			{
				throw new ArgumentNullException();
			}
			this._baseHeap = new List<KeyValuePair<TPriority, TValue>>(capacity);
			this._customComparer = comparer;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000D9CF File Offset: 0x0000BBCF
		public PriorityQueue(IComparer<TPriority> comparer)
		{
			if (comparer == null)
			{
				throw new ArgumentNullException();
			}
			this._baseHeap = new List<KeyValuePair<TPriority, TValue>>();
			this._customComparer = comparer;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000D9F2 File Offset: 0x0000BBF2
		public PriorityQueue(IEnumerable<KeyValuePair<TPriority, TValue>> data) : this(data, Comparer<TPriority>.Default)
		{
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000DA00 File Offset: 0x0000BC00
		public PriorityQueue(IEnumerable<KeyValuePair<TPriority, TValue>> data, IComparer<TPriority> comparer)
		{
			if (data == null || comparer == null)
			{
				throw new ArgumentNullException();
			}
			this._customComparer = comparer;
			this._baseHeap = new List<KeyValuePair<TPriority, TValue>>(data);
			for (int i = this._baseHeap.Count / 2 - 1; i >= 0; i--)
			{
				this.HeapifyFromBeginningToEnd(i);
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000DA53 File Offset: 0x0000BC53
		public static PriorityQueue<TPriority, TValue> MergeQueues(PriorityQueue<TPriority, TValue> pq1, PriorityQueue<TPriority, TValue> pq2)
		{
			if (pq1 == null || pq2 == null)
			{
				throw new ArgumentNullException();
			}
			if (pq1.Comparer != pq2.Comparer)
			{
				throw new InvalidOperationException("Priority queues to be merged must have equal comparers");
			}
			return PriorityQueue<TPriority, TValue>.MergeQueues(pq1, pq2, pq1.Comparer);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000DA88 File Offset: 0x0000BC88
		public static PriorityQueue<TPriority, TValue> MergeQueues(PriorityQueue<TPriority, TValue> pq1, PriorityQueue<TPriority, TValue> pq2, IComparer<TPriority> comparer)
		{
			if (pq1 == null || pq2 == null || comparer == null)
			{
				throw new ArgumentNullException();
			}
			PriorityQueue<TPriority, TValue> priorityQueue = new PriorityQueue<TPriority, TValue>(pq1.Count + pq2.Count, comparer);
			priorityQueue._baseHeap.AddRange(pq1._baseHeap);
			priorityQueue._baseHeap.AddRange(pq2._baseHeap);
			for (int i = priorityQueue._baseHeap.Count / 2 - 1; i >= 0; i--)
			{
				priorityQueue.HeapifyFromBeginningToEnd(i);
			}
			return priorityQueue;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000DAFC File Offset: 0x0000BCFC
		public void Enqueue(TPriority priority, TValue value)
		{
			this.Insert(priority, value);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000DB06 File Offset: 0x0000BD06
		public KeyValuePair<TPriority, TValue> Dequeue()
		{
			if (!this.IsEmpty)
			{
				KeyValuePair<TPriority, TValue> result = this._baseHeap[0];
				this.DeleteRoot();
				return result;
			}
			throw new InvalidOperationException("Priority queue is empty");
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000DB30 File Offset: 0x0000BD30
		public TValue DequeueValue()
		{
			return this.Dequeue().Value;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000DB4B File Offset: 0x0000BD4B
		public KeyValuePair<TPriority, TValue> Peek()
		{
			if (!this.IsEmpty)
			{
				return this._baseHeap[0];
			}
			throw new InvalidOperationException("Priority queue is empty");
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000DB6C File Offset: 0x0000BD6C
		public TValue PeekValue()
		{
			return this.Peek().Value;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0000DB87 File Offset: 0x0000BD87
		public bool IsEmpty
		{
			get
			{
				return this._baseHeap.Count == 0;
			}
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000DB98 File Offset: 0x0000BD98
		private void ExchangeElements(int pos1, int pos2)
		{
			KeyValuePair<TPriority, TValue> value = this._baseHeap[pos1];
			this._baseHeap[pos1] = this._baseHeap[pos2];
			this._baseHeap[pos2] = value;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000DBD8 File Offset: 0x0000BDD8
		private void Insert(TPriority priority, TValue value)
		{
			KeyValuePair<TPriority, TValue> item = new KeyValuePair<TPriority, TValue>(priority, value);
			this._baseHeap.Add(item);
			this.HeapifyFromEndToBeginning(this._baseHeap.Count - 1);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000DC10 File Offset: 0x0000BE10
		private int HeapifyFromEndToBeginning(int pos)
		{
			if (pos >= this._baseHeap.Count)
			{
				return -1;
			}
			IComparer<TPriority> comparer = this.Comparer;
			while (pos > 0)
			{
				int num = (pos - 1) / 2;
				if (comparer.Compare(this._baseHeap[num].Key, this._baseHeap[pos].Key) >= 0)
				{
					break;
				}
				this.ExchangeElements(num, pos);
				pos = num;
			}
			return pos;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000DC80 File Offset: 0x0000BE80
		private void DeleteRoot()
		{
			if (this._baseHeap.Count <= 1)
			{
				this._baseHeap.Clear();
				return;
			}
			this._baseHeap[0] = this._baseHeap[this._baseHeap.Count - 1];
			this._baseHeap.RemoveAt(this._baseHeap.Count - 1);
			this.HeapifyFromBeginningToEnd(0);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000DCEC File Offset: 0x0000BEEC
		private void HeapifyFromBeginningToEnd(int pos)
		{
			if (pos >= this._baseHeap.Count)
			{
				return;
			}
			IComparer<TPriority> comparer = this.Comparer;
			for (;;)
			{
				int num = pos;
				int num2 = 2 * pos + 1;
				int num3 = 2 * pos + 2;
				if (num2 < this._baseHeap.Count && comparer.Compare(this._baseHeap[num].Key, this._baseHeap[num2].Key) < 0)
				{
					num = num2;
				}
				if (num3 < this._baseHeap.Count && comparer.Compare(this._baseHeap[num].Key, this._baseHeap[num3].Key) < 0)
				{
					num = num3;
				}
				if (num == pos)
				{
					break;
				}
				this.ExchangeElements(num, pos);
				pos = num;
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000DDB7 File Offset: 0x0000BFB7
		public void Add(KeyValuePair<TPriority, TValue> item)
		{
			this.Enqueue(item.Key, item.Value);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000DDCD File Offset: 0x0000BFCD
		public void Clear()
		{
			this._baseHeap.Clear();
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000DDDA File Offset: 0x0000BFDA
		public bool Contains(KeyValuePair<TPriority, TValue> item)
		{
			return this._baseHeap.Contains(item);
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0000DDE8 File Offset: 0x0000BFE8
		public int Count
		{
			get
			{
				return this._baseHeap.Count;
			}
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000DDF5 File Offset: 0x0000BFF5
		public void CopyTo(KeyValuePair<TPriority, TValue>[] array, int arrayIndex)
		{
			this._baseHeap.CopyTo(array, arrayIndex);
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0000DE04 File Offset: 0x0000C004
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000DE08 File Offset: 0x0000C008
		public bool Remove(KeyValuePair<TPriority, TValue> item)
		{
			int num = this._baseHeap.IndexOf(item);
			if (num < 0)
			{
				return false;
			}
			this._baseHeap[num] = this._baseHeap[this._baseHeap.Count - 1];
			this._baseHeap.RemoveAt(this._baseHeap.Count - 1);
			if (this.HeapifyFromEndToBeginning(num) == num)
			{
				this.HeapifyFromBeginningToEnd(num);
			}
			return true;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000DE76 File Offset: 0x0000C076
		public IEnumerator<KeyValuePair<TPriority, TValue>> GetEnumerator()
		{
			return this._baseHeap.GetEnumerator();
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000DE88 File Offset: 0x0000C088
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400013E RID: 318
		private readonly List<KeyValuePair<TPriority, TValue>> _baseHeap;

		// Token: 0x0400013F RID: 319
		private readonly IComparer<TPriority> _customComparer;
	}
}
