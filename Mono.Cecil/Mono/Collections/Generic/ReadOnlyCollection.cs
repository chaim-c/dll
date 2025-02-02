using System;
using System.Collections;
using System.Collections.Generic;

namespace Mono.Collections.Generic
{
	// Token: 0x020000F3 RID: 243
	public sealed class ReadOnlyCollection<T> : Collection<T>, ICollection<!0>, IEnumerable<!0>, IList, ICollection, IEnumerable
	{
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x0001DFA9 File Offset: 0x0001C1A9
		public static ReadOnlyCollection<T> Empty
		{
			get
			{
				ReadOnlyCollection<T> result;
				if ((result = ReadOnlyCollection<T>.empty) == null)
				{
					result = (ReadOnlyCollection<T>.empty = new ReadOnlyCollection<T>());
				}
				return result;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0001DFBF File Offset: 0x0001C1BF
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x0001DFC2 File Offset: 0x0001C1C2
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x0001DFC5 File Offset: 0x0001C1C5
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0001DFC8 File Offset: 0x0001C1C8
		private ReadOnlyCollection()
		{
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0001DFD0 File Offset: 0x0001C1D0
		public ReadOnlyCollection(T[] array)
		{
			if (array == null)
			{
				throw new ArgumentNullException();
			}
			this.Initialize(array, array.Length);
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0001DFEB File Offset: 0x0001C1EB
		public ReadOnlyCollection(Collection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException();
			}
			this.Initialize(collection.items, collection.size);
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0001E00E File Offset: 0x0001C20E
		private void Initialize(T[] items, int size)
		{
			this.items = new T[size];
			Array.Copy(items, 0, this.items, 0, size);
			this.size = size;
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0001E032 File Offset: 0x0001C232
		internal override void Grow(int desired)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0001E039 File Offset: 0x0001C239
		protected override void OnAdd(T item, int index)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0001E040 File Offset: 0x0001C240
		protected override void OnClear()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0001E047 File Offset: 0x0001C247
		protected override void OnInsert(T item, int index)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0001E04E File Offset: 0x0001C24E
		protected override void OnRemove(T item, int index)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0001E055 File Offset: 0x0001C255
		protected override void OnSet(T item, int index)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x04000631 RID: 1585
		private static ReadOnlyCollection<T> empty;
	}
}
