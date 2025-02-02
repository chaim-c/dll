using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HarmonyLib.BUTR.Extensions;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace MCM.UI.Dropdown
{
	// Token: 0x0200002E RID: 46
	[NullableContext(1)]
	[Nullable(0)]
	internal class MCMSelectorVM<[Nullable(0)] TSelectorItemVM> : ViewModel where TSelectorItemVM : ViewModel
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00008372 File Offset: 0x00006572
		public static MCMSelectorVM<TSelectorItemVM> Empty
		{
			get
			{
				return new MCMSelectorVM<TSelectorItemVM>();
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00008379 File Offset: 0x00006579
		[DataSourceProperty]
		public MBBindingList<TSelectorItemVM> ItemList { get; } = new MBBindingList<TSelectorItemVM>();

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00008381 File Offset: 0x00006581
		// (set) Token: 0x0600019C RID: 412 RVA: 0x0000838C File Offset: 0x0000658C
		[DataSourceProperty]
		public int SelectedIndex
		{
			get
			{
				return this._selectedIndex;
			}
			set
			{
				bool flag = base.SetField<int>(ref this._selectedIndex, value, "SelectedIndex");
				if (flag)
				{
					bool flag2 = this.SelectedItem != null;
					if (flag2)
					{
						MCMSelectorVM<TSelectorItemVM>._setIsSelectedDelegate(this.SelectedItem, false);
					}
					this.SelectedItem = this.GetCurrentItem();
					bool flag3 = this.SelectedItem != null;
					if (flag3)
					{
						MCMSelectorVM<TSelectorItemVM>._setIsSelectedDelegate(this.SelectedItem, true);
					}
				}
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00008411 File Offset: 0x00006611
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00008419 File Offset: 0x00006619
		[Nullable(2)]
		[DataSourceProperty]
		public TSelectorItemVM SelectedItem
		{
			[NullableContext(2)]
			get
			{
				return this._selectedItem;
			}
			[NullableContext(2)]
			set
			{
				base.SetField<TSelectorItemVM>(ref this._selectedItem, value, "SelectedItem");
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000842E File Offset: 0x0000662E
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00008436 File Offset: 0x00006636
		[DataSourceProperty]
		public bool HasSingleItem
		{
			get
			{
				return this._hasSingleItem;
			}
			set
			{
				base.SetField<bool>(ref this._hasSingleItem, value, "HasSingleItem");
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000844B File Offset: 0x0000664B
		public MCMSelectorVM()
		{
			this.HasSingleItem = true;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000846F File Offset: 0x0000666F
		public MCMSelectorVM(IEnumerable<object> list, int selectedIndex)
		{
			this.Refresh(list, selectedIndex);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00008494 File Offset: 0x00006694
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.ItemList.ApplyActionOnAllItems(delegate(TSelectorItemVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000084CC File Offset: 0x000066CC
		public void Refresh(IEnumerable<object> list, int selectedIndex)
		{
			this.ItemList.Clear();
			this._selectedIndex = -1;
			foreach (object obj in list)
			{
				TSelectorItemVM val = Activator.CreateInstance(typeof(TSelectorItemVM), new object[]
				{
					obj
				}) as TSelectorItemVM;
				bool flag = val != null;
				if (flag)
				{
					this.ItemList.Add(val);
				}
			}
			this.HasSingleItem = (this.ItemList.Count <= 1);
			this.SelectedIndex = selectedIndex;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00008584 File Offset: 0x00006784
		[NullableContext(2)]
		public TSelectorItemVM GetCurrentItem()
		{
			bool flag = this.ItemList.Count > 0 && this.SelectedIndex >= 0 && this.SelectedIndex < this.ItemList.Count;
			TSelectorItemVM result;
			if (flag)
			{
				result = this.ItemList[this.SelectedIndex];
			}
			else
			{
				result = default(TSelectorItemVM);
			}
			return result;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000085E5 File Offset: 0x000067E5
		public void AddItem(TSelectorItemVM item)
		{
			this.ItemList.Add(item);
			this.HasSingleItem = (this.ItemList.Count <= 1);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00008610 File Offset: 0x00006810
		public void ExecuteRandomize()
		{
			TSelectorItemVM element = this.ItemList.GetRandomElementWithPredicate((TSelectorItemVM i) => MCMSelectorVM<TSelectorItemVM>._canBeSelectedDelegate(i));
			bool flag = element != null;
			if (flag)
			{
				this.SelectedIndex = this.ItemList.IndexOf(element);
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000866C File Offset: 0x0000686C
		public void ExecuteSelectNextItem()
		{
			bool flag = this.ItemList.Count > 0;
			if (flag)
			{
				for (int num = (this.SelectedIndex + 1) % this.ItemList.Count; num != this.SelectedIndex; num = (num + 1) % this.ItemList.Count)
				{
					bool flag2 = MCMSelectorVM<TSelectorItemVM>._canBeSelectedDelegate(this.ItemList[num]);
					if (flag2)
					{
						this.SelectedIndex = num;
						break;
					}
				}
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000086EC File Offset: 0x000068EC
		public void ExecuteSelectPreviousItem()
		{
			bool flag = this.ItemList.Count > 0;
			if (flag)
			{
				for (int num = (this.SelectedIndex - 1 >= 0) ? (this.SelectedIndex - 1) : (this.ItemList.Count - 1); num != this.SelectedIndex; num = ((num - 1 >= 0) ? (num - 1) : (this.ItemList.Count - 1)))
				{
					bool flag2 = MCMSelectorVM<TSelectorItemVM>._canBeSelectedDelegate(this.ItemList[num]);
					if (flag2)
					{
						this.SelectedIndex = num;
						break;
					}
				}
			}
		}

		// Token: 0x0400006F RID: 111
		[Nullable(new byte[]
		{
			1,
			0
		})]
		private static readonly MCMSelectorVM<TSelectorItemVM>.CanBeSelectedDelegate _canBeSelectedDelegate = AccessTools2.GetPropertyGetterDelegate<MCMSelectorVM<TSelectorItemVM>.CanBeSelectedDelegate>(typeof(TSelectorItemVM), "CanBeSelected", true) ?? ((TSelectorItemVM _) => false);

		// Token: 0x04000070 RID: 112
		[Nullable(new byte[]
		{
			1,
			0
		})]
		private static readonly MCMSelectorVM<TSelectorItemVM>.SetIsSelectedDelegate _setIsSelectedDelegate = AccessTools2.GetPropertySetterDelegate<MCMSelectorVM<TSelectorItemVM>.SetIsSelectedDelegate>(typeof(TSelectorItemVM), "IsSelected", true) ?? delegate(object _, bool _)
		{
		};

		// Token: 0x04000071 RID: 113
		protected int _selectedIndex = -1;

		// Token: 0x04000072 RID: 114
		[Nullable(2)]
		private TSelectorItemVM _selectedItem;

		// Token: 0x04000073 RID: 115
		private bool _hasSingleItem;

		// Token: 0x020000A3 RID: 163
		// (Invoke) Token: 0x0600054B RID: 1355
		[NullableContext(0)]
		private delegate bool CanBeSelectedDelegate(TSelectorItemVM instance);

		// Token: 0x020000A4 RID: 164
		// (Invoke) Token: 0x0600054F RID: 1359
		[NullableContext(0)]
		private delegate void SetIsSelectedDelegate(object instance, bool value);
	}
}
