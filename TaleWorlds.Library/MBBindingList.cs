using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TaleWorlds.Library
{
	// Token: 0x02000064 RID: 100
	public class MBBindingList<T> : Collection<T>, IMBBindingList, IList, ICollection, IEnumerable
	{
		// Token: 0x06000329 RID: 809 RVA: 0x0000ADEC File Offset: 0x00008FEC
		public MBBindingList() : base(new List<T>(64))
		{
			this._list = (List<T>)base.Items;
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x0600032A RID: 810 RVA: 0x0000AE0C File Offset: 0x0000900C
		// (remove) Token: 0x0600032B RID: 811 RVA: 0x0000AE2D File Offset: 0x0000902D
		public event ListChangedEventHandler ListChanged
		{
			add
			{
				if (this._eventHandlers == null)
				{
					this._eventHandlers = new List<ListChangedEventHandler>();
				}
				this._eventHandlers.Add(value);
			}
			remove
			{
				if (this._eventHandlers != null)
				{
					this._eventHandlers.Remove(value);
				}
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000AE44 File Offset: 0x00009044
		protected override void ClearItems()
		{
			base.ClearItems();
			this.FireListChanged(ListChangedType.Reset, -1);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000AE54 File Offset: 0x00009054
		protected override void InsertItem(int index, T item)
		{
			base.InsertItem(index, item);
			this.FireListChanged(ListChangedType.ItemAdded, index);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000AE66 File Offset: 0x00009066
		protected override void RemoveItem(int index)
		{
			this.FireListChanged(ListChangedType.ItemBeforeDeleted, index);
			base.RemoveItem(index);
			this.FireListChanged(ListChangedType.ItemDeleted, index);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000AE7F File Offset: 0x0000907F
		protected override void SetItem(int index, T item)
		{
			base.SetItem(index, item);
			this.FireListChanged(ListChangedType.ItemChanged, index);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000AE91 File Offset: 0x00009091
		private void FireListChanged(ListChangedType type, int index)
		{
			this.OnListChanged(new ListChangedEventArgs(type, index));
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000AEA0 File Offset: 0x000090A0
		protected virtual void OnListChanged(ListChangedEventArgs e)
		{
			if (this._eventHandlers != null)
			{
				foreach (ListChangedEventHandler listChangedEventHandler in this._eventHandlers)
				{
					listChangedEventHandler(this, e);
				}
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000AEFC File Offset: 0x000090FC
		public void Sort()
		{
			this._list.Sort();
			this.FireListChanged(ListChangedType.Sorted, -1);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000AF11 File Offset: 0x00009111
		public void Sort(IComparer<T> comparer)
		{
			if (!this.IsOrdered(comparer))
			{
				this._list.Sort(comparer);
				this.FireListChanged(ListChangedType.Sorted, -1);
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000AF30 File Offset: 0x00009130
		public bool IsOrdered(IComparer<T> comparer)
		{
			for (int i = 1; i < this._list.Count; i++)
			{
				if (comparer.Compare(this._list[i - 1], this._list[i]) == 1)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000AF7C File Offset: 0x0000917C
		public void ApplyActionOnAllItems(Action<T> action)
		{
			for (int i = 0; i < this._list.Count; i++)
			{
				T obj = this._list[i];
				action(obj);
			}
		}

		// Token: 0x04000107 RID: 263
		private readonly List<T> _list;

		// Token: 0x04000108 RID: 264
		private List<ListChangedEventHandler> _eventHandlers;
	}
}
