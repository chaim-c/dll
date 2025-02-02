using System;
using TaleWorlds.Library;

namespace TaleWorlds.Core.ViewModelCollection.Generic
{
	// Token: 0x0200001F RID: 31
	public class BindingListStringItem : ViewModel
	{
		// Token: 0x0600019B RID: 411 RVA: 0x00005672 File Offset: 0x00003872
		public BindingListStringItem(string value)
		{
			this.Item = value;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00005681 File Offset: 0x00003881
		// (set) Token: 0x0600019D RID: 413 RVA: 0x00005689 File Offset: 0x00003889
		[DataSourceProperty]
		public string Item
		{
			get
			{
				return this._item;
			}
			set
			{
				if (value != this._item)
				{
					this._item = value;
					base.OnPropertyChangedWithValue<string>(value, "Item");
				}
			}
		}

		// Token: 0x0400009F RID: 159
		private string _item;
	}
}
