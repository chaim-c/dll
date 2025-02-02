using System;
using TaleWorlds.Library;

namespace TaleWorlds.Core.ViewModelCollection.Generic
{
	// Token: 0x0200001E RID: 30
	public class BindingListFloatItem : ViewModel
	{
		// Token: 0x06000198 RID: 408 RVA: 0x0000563D File Offset: 0x0000383D
		public BindingListFloatItem(float value)
		{
			this.Item = value;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000564C File Offset: 0x0000384C
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00005654 File Offset: 0x00003854
		[DataSourceProperty]
		public float Item
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
					base.OnPropertyChangedWithValue(value, "Item");
				}
			}
		}

		// Token: 0x0400009E RID: 158
		private float _item;
	}
}
