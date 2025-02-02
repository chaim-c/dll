using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;

namespace Mono.Collections.Generic
{
	// Token: 0x02000011 RID: 17
	public class Collection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IList, ICollection, IEnumerable
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00004FD4 File Offset: 0x000031D4
		public int Count
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x17000025 RID: 37
		public T this[int index]
		{
			get
			{
				if (index >= this.size)
				{
					throw new ArgumentOutOfRangeException();
				}
				return this.items[index];
			}
			set
			{
				this.CheckIndex(index);
				if (index == this.size)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.OnSet(value, index);
				this.items[index] = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00005026 File Offset: 0x00003226
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00005030 File Offset: 0x00003230
		public int Capacity
		{
			get
			{
				return this.items.Length;
			}
			set
			{
				if (value < 0 || value < this.size)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.Resize(value);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000EF RID: 239 RVA: 0x0000504C File Offset: 0x0000324C
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x0000504F File Offset: 0x0000324F
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00005052 File Offset: 0x00003252
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700002A RID: 42
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this.CheckIndex(index);
				try
				{
					this[index] = (T)((object)value);
					return;
				}
				catch (InvalidCastException)
				{
				}
				catch (NullReferenceException)
				{
				}
				throw new ArgumentException();
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x000050B0 File Offset: 0x000032B0
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x000050B8 File Offset: 0x000032B8
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000050BB File Offset: 0x000032BB
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000050BE File Offset: 0x000032BE
		public Collection()
		{
			this.items = Empty<T>.Array;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000050D1 File Offset: 0x000032D1
		public Collection(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			this.items = new T[capacity];
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000050F0 File Offset: 0x000032F0
		public Collection(ICollection<T> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			this.items = new T[items.Count];
			items.CopyTo(this.items, 0);
			this.size = this.items.Length;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005140 File Offset: 0x00003340
		public void Add(T item)
		{
			if (this.size == this.items.Length)
			{
				this.Grow(1);
			}
			this.OnAdd(item, this.size);
			this.items[this.size++] = item;
			this.version++;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000519C File Offset: 0x0000339C
		public bool Contains(T item)
		{
			return this.IndexOf(item) != -1;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000051AB File Offset: 0x000033AB
		public int IndexOf(T item)
		{
			return Array.IndexOf<T>(this.items, item, 0, this.size);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000051C0 File Offset: 0x000033C0
		public void Insert(int index, T item)
		{
			this.CheckIndex(index);
			if (this.size == this.items.Length)
			{
				this.Grow(1);
			}
			this.OnInsert(item, index);
			this.Shift(index, 1);
			this.items[index] = item;
			this.version++;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005218 File Offset: 0x00003418
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this.size)
			{
				throw new ArgumentOutOfRangeException();
			}
			T item = this.items[index];
			this.OnRemove(item, index);
			this.Shift(index, -1);
			Array.Clear(this.items, this.size, 1);
			this.version++;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005278 File Offset: 0x00003478
		public bool Remove(T item)
		{
			int num = this.IndexOf(item);
			if (num == -1)
			{
				return false;
			}
			this.OnRemove(item, num);
			this.Shift(num, -1);
			Array.Clear(this.items, this.size, 1);
			this.version++;
			return true;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000052C4 File Offset: 0x000034C4
		public void Clear()
		{
			this.OnClear();
			Array.Clear(this.items, 0, this.size);
			this.size = 0;
			this.version++;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000052F3 File Offset: 0x000034F3
		public void CopyTo(T[] array, int arrayIndex)
		{
			Array.Copy(this.items, 0, array, arrayIndex, this.size);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000530C File Offset: 0x0000350C
		public T[] ToArray()
		{
			T[] array = new T[this.size];
			Array.Copy(this.items, 0, array, 0, this.size);
			return array;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000533A File Offset: 0x0000353A
		private void CheckIndex(int index)
		{
			if (index < 0 || index > this.size)
			{
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005350 File Offset: 0x00003550
		private void Shift(int start, int delta)
		{
			if (delta < 0)
			{
				start -= delta;
			}
			if (start < this.size)
			{
				Array.Copy(this.items, start, this.items, start + delta, this.size - start);
			}
			this.size += delta;
			if (delta < 0)
			{
				Array.Clear(this.items, this.size, -delta);
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000053B1 File Offset: 0x000035B1
		protected virtual void OnAdd(T item, int index)
		{
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000053B3 File Offset: 0x000035B3
		protected virtual void OnInsert(T item, int index)
		{
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000053B5 File Offset: 0x000035B5
		protected virtual void OnSet(T item, int index)
		{
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000053B7 File Offset: 0x000035B7
		protected virtual void OnRemove(T item, int index)
		{
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000053B9 File Offset: 0x000035B9
		protected virtual void OnClear()
		{
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000053BC File Offset: 0x000035BC
		internal virtual void Grow(int desired)
		{
			int num = this.size + desired;
			if (num <= this.items.Length)
			{
				return;
			}
			num = Math.Max(Math.Max(this.items.Length * 2, 4), num);
			this.Resize(num);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000053FC File Offset: 0x000035FC
		protected void Resize(int new_size)
		{
			if (new_size == this.size)
			{
				return;
			}
			if (new_size < this.size)
			{
				throw new ArgumentOutOfRangeException();
			}
			this.items = this.items.Resize(new_size);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000542C File Offset: 0x0000362C
		int IList.Add(object value)
		{
			try
			{
				this.Add((T)((object)value));
				return this.size - 1;
			}
			catch (InvalidCastException)
			{
			}
			catch (NullReferenceException)
			{
			}
			throw new ArgumentException();
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000547C File Offset: 0x0000367C
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005484 File Offset: 0x00003684
		bool IList.Contains(object value)
		{
			return ((IList)this).IndexOf(value) > -1;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005490 File Offset: 0x00003690
		int IList.IndexOf(object value)
		{
			try
			{
				return this.IndexOf((T)((object)value));
			}
			catch (InvalidCastException)
			{
			}
			catch (NullReferenceException)
			{
			}
			return -1;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000054D4 File Offset: 0x000036D4
		void IList.Insert(int index, object value)
		{
			this.CheckIndex(index);
			try
			{
				this.Insert(index, (T)((object)value));
				return;
			}
			catch (InvalidCastException)
			{
			}
			catch (NullReferenceException)
			{
			}
			throw new ArgumentException();
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005520 File Offset: 0x00003720
		void IList.Remove(object value)
		{
			try
			{
				this.Remove((T)((object)value));
			}
			catch (InvalidCastException)
			{
			}
			catch (NullReferenceException)
			{
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005560 File Offset: 0x00003760
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005569 File Offset: 0x00003769
		void ICollection.CopyTo(Array array, int index)
		{
			Array.Copy(this.items, 0, array, index, this.size);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000557F File Offset: 0x0000377F
		public Collection<T>.Enumerator GetEnumerator()
		{
			return new Collection<T>.Enumerator(this);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005587 File Offset: 0x00003787
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Collection<T>.Enumerator(this);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005594 File Offset: 0x00003794
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return new Collection<T>.Enumerator(this);
		}

		// Token: 0x04000127 RID: 295
		internal T[] items;

		// Token: 0x04000128 RID: 296
		internal int size;

		// Token: 0x04000129 RID: 297
		private int version;

		// Token: 0x02000012 RID: 18
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x1700002E RID: 46
			// (get) Token: 0x06000117 RID: 279 RVA: 0x000055A1 File Offset: 0x000037A1
			public T Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x06000118 RID: 280 RVA: 0x000055A9 File Offset: 0x000037A9
			object IEnumerator.Current
			{
				get
				{
					this.CheckState();
					if (this.next <= 0)
					{
						throw new InvalidOperationException();
					}
					return this.current;
				}
			}

			// Token: 0x06000119 RID: 281 RVA: 0x000055CB File Offset: 0x000037CB
			internal Enumerator(Collection<T> collection)
			{
				this = default(Collection<T>.Enumerator);
				this.collection = collection;
				this.version = collection.version;
			}

			// Token: 0x0600011A RID: 282 RVA: 0x000055E8 File Offset: 0x000037E8
			public bool MoveNext()
			{
				this.CheckState();
				if (this.next < 0)
				{
					return false;
				}
				if (this.next < this.collection.size)
				{
					this.current = this.collection.items[this.next++];
					return true;
				}
				this.next = -1;
				return false;
			}

			// Token: 0x0600011B RID: 283 RVA: 0x0000564A File Offset: 0x0000384A
			public void Reset()
			{
				this.CheckState();
				this.next = 0;
			}

			// Token: 0x0600011C RID: 284 RVA: 0x00005659 File Offset: 0x00003859
			private void CheckState()
			{
				if (this.collection == null)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				if (this.version != this.collection.version)
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x0600011D RID: 285 RVA: 0x00005697 File Offset: 0x00003897
			public void Dispose()
			{
				this.collection = null;
			}

			// Token: 0x0400012A RID: 298
			private Collection<T> collection;

			// Token: 0x0400012B RID: 299
			private T current;

			// Token: 0x0400012C RID: 300
			private int next;

			// Token: 0x0400012D RID: 301
			private readonly int version;
		}
	}
}
