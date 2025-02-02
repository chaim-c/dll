using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MCM.UI.Dropdown
{
	// Token: 0x0200002F RID: 47
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal class MCMSelectorVM<[Nullable(0)] TSelectorItemVM, TSelectorItemVMValueType> : MCMSelectorVM<TSelectorItemVM> where TSelectorItemVM : MCMSelectorItemVM<TSelectorItemVMValueType> where TSelectorItemVMValueType : class
	{
		// Token: 0x060001AB RID: 427 RVA: 0x000087ED File Offset: 0x000069ED
		public MCMSelectorVM(IEnumerable<TSelectorItemVMValueType> list, int selectedIndex)
		{
			this.Refresh(list, selectedIndex);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00008800 File Offset: 0x00006A00
		public void Refresh(IEnumerable<TSelectorItemVMValueType> list, int selectedIndex)
		{
			base.ItemList.Clear();
			this._selectedIndex = -1;
			foreach (TSelectorItemVMValueType @ref in list)
			{
				TSelectorItemVM val = Activator.CreateInstance(typeof(TSelectorItemVM), new object[]
				{
					@ref
				}) as TSelectorItemVM;
				bool flag = val != null;
				if (flag)
				{
					base.ItemList.Add(val);
				}
			}
			base.HasSingleItem = (base.ItemList.Count <= 1);
			base.SelectedIndex = selectedIndex;
		}
	}
}
