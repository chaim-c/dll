using System;
using System.Collections.Generic;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.Core.ViewModelCollection.Selector
{
	// Token: 0x02000012 RID: 18
	public class SelectorVM<T> : ViewModel where T : SelectorItemVM
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x000036A9 File Offset: 0x000018A9
		public SelectorVM(int selectedIndex, Action<SelectorVM<T>> onChange)
		{
			this.ItemList = new MBBindingList<T>();
			this.HasSingleItem = true;
			this._onChange = onChange;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000036D1 File Offset: 0x000018D1
		public SelectorVM(IEnumerable<string> list, int selectedIndex, Action<SelectorVM<T>> onChange)
		{
			this.ItemList = new MBBindingList<T>();
			this.Refresh(list, selectedIndex, onChange);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000036F4 File Offset: 0x000018F4
		public SelectorVM(IEnumerable<TextObject> list, int selectedIndex, Action<SelectorVM<T>> onChange)
		{
			this.ItemList = new MBBindingList<T>();
			this.Refresh(list, selectedIndex, onChange);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003718 File Offset: 0x00001918
		public void Refresh(IEnumerable<string> list, int selectedIndex, Action<SelectorVM<T>> onChange)
		{
			this.ItemList.Clear();
			this._selectedIndex = -1;
			foreach (string text in list)
			{
				T item = (T)((object)Activator.CreateInstance(typeof(T), new object[]
				{
					text
				}));
				this.ItemList.Add(item);
			}
			this.HasSingleItem = (this.ItemList.Count <= 1);
			this._onChange = onChange;
			this.SelectedIndex = selectedIndex;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000037BC File Offset: 0x000019BC
		public void Refresh(IEnumerable<TextObject> list, int selectedIndex, Action<SelectorVM<T>> onChange)
		{
			this.ItemList.Clear();
			this._selectedIndex = -1;
			foreach (TextObject textObject in list)
			{
				T item = (T)((object)Activator.CreateInstance(typeof(T), new object[]
				{
					textObject
				}));
				this.ItemList.Add(item);
			}
			this.HasSingleItem = (this.ItemList.Count <= 1);
			this._onChange = onChange;
			this.SelectedIndex = selectedIndex;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003860 File Offset: 0x00001A60
		public void Refresh(IEnumerable<T> list, int selectedIndex, Action<SelectorVM<T>> onChange)
		{
			this.ItemList.Clear();
			this._selectedIndex = -1;
			foreach (T item in list)
			{
				this.ItemList.Add(item);
			}
			this.HasSingleItem = (this.ItemList.Count <= 1);
			this._onChange = onChange;
			this.SelectedIndex = selectedIndex;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000038E4 File Offset: 0x00001AE4
		public void SetOnChangeAction(Action<SelectorVM<T>> onChange)
		{
			this._onChange = onChange;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000038ED File Offset: 0x00001AED
		public void AddItem(T item)
		{
			this.ItemList.Add(item);
			this.HasSingleItem = (this.ItemList.Count <= 1);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003914 File Offset: 0x00001B14
		public void ExecuteRandomize()
		{
			MBBindingList<T> itemList = this.ItemList;
			T t;
			if (itemList == null)
			{
				t = default(T);
			}
			else
			{
				t = itemList.GetRandomElementWithPredicate((T i) => i.CanBeSelected);
			}
			T t2 = t;
			if (t2 != null)
			{
				this.SelectedIndex = this.ItemList.IndexOf(t2);
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003978 File Offset: 0x00001B78
		public void ExecuteSelectNextItem()
		{
			MBBindingList<T> itemList = this.ItemList;
			if (itemList != null && itemList.Count > 0)
			{
				for (int num = (this.SelectedIndex + 1) % this.ItemList.Count; num != this.SelectedIndex; num = (num + 1) % this.ItemList.Count)
				{
					if (this.ItemList[num].CanBeSelected)
					{
						this.SelectedIndex = num;
						return;
					}
				}
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000039EC File Offset: 0x00001BEC
		public void ExecuteSelectPreviousItem()
		{
			MBBindingList<T> itemList = this.ItemList;
			if (itemList != null && itemList.Count > 0)
			{
				for (int num = (this.SelectedIndex - 1 >= 0) ? (this.SelectedIndex - 1) : (this.ItemList.Count - 1); num != this.SelectedIndex; num = ((num - 1 >= 0) ? (num - 1) : (this.ItemList.Count - 1)))
				{
					if (this.ItemList[num].CanBeSelected)
					{
						this.SelectedIndex = num;
						return;
					}
				}
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003A78 File Offset: 0x00001C78
		public T GetCurrentItem()
		{
			MBBindingList<T> itemList = this._itemList;
			if (itemList != null && itemList.Count > 0 && this.SelectedIndex >= 0 && this.SelectedIndex < this._itemList.Count)
			{
				return this._itemList[this.SelectedIndex];
			}
			return default(T);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003AD3 File Offset: 0x00001CD3
		public override void RefreshValues()
		{
			base.RefreshValues();
			this._itemList.ApplyActionOnAllItems(delegate(T x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00003B05 File Offset: 0x00001D05
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00003B0D File Offset: 0x00001D0D
		[DataSourceProperty]
		public MBBindingList<T> ItemList
		{
			get
			{
				return this._itemList;
			}
			set
			{
				if (value != this._itemList)
				{
					this._itemList = value;
					base.OnPropertyChangedWithValue<MBBindingList<T>>(value, "ItemList");
				}
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00003B2B File Offset: 0x00001D2B
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00003B34 File Offset: 0x00001D34
		[DataSourceProperty]
		public int SelectedIndex
		{
			get
			{
				return this._selectedIndex;
			}
			set
			{
				if (value != this._selectedIndex)
				{
					this._selectedIndex = value;
					base.OnPropertyChangedWithValue(value, "SelectedIndex");
					if (this.SelectedItem != null)
					{
						this.SelectedItem.IsSelected = false;
					}
					this.SelectedItem = this.GetCurrentItem();
					if (this.SelectedItem != null)
					{
						this.SelectedItem.IsSelected = true;
					}
					Action<SelectorVM<T>> onChange = this._onChange;
					if (onChange == null)
					{
						return;
					}
					onChange(this);
				}
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00003BB6 File Offset: 0x00001DB6
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00003BBE File Offset: 0x00001DBE
		[DataSourceProperty]
		public T SelectedItem
		{
			get
			{
				return this._selectedItem;
			}
			set
			{
				if (value != this._selectedItem)
				{
					this._selectedItem = value;
					base.OnPropertyChangedWithValue<T>(value, "SelectedItem");
				}
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00003BE6 File Offset: 0x00001DE6
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00003BEE File Offset: 0x00001DEE
		[DataSourceProperty]
		public bool HasSingleItem
		{
			get
			{
				return this._hasSingleItem;
			}
			set
			{
				if (value != this._hasSingleItem)
				{
					this._hasSingleItem = value;
					base.OnPropertyChangedWithValue(value, "HasSingleItem");
				}
			}
		}

		// Token: 0x04000058 RID: 88
		private Action<SelectorVM<T>> _onChange;

		// Token: 0x04000059 RID: 89
		private MBBindingList<T> _itemList;

		// Token: 0x0400005A RID: 90
		private int _selectedIndex = -1;

		// Token: 0x0400005B RID: 91
		private T _selectedItem;

		// Token: 0x0400005C RID: 92
		private bool _hasSingleItem;
	}
}
