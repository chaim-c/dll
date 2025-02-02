using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.Credits
{
	// Token: 0x02000072 RID: 114
	public class CreditsItemVM : ViewModel
	{
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x00025268 File Offset: 0x00023468
		// (set) Token: 0x06000978 RID: 2424 RVA: 0x00025270 File Offset: 0x00023470
		[DataSourceProperty]
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				if (value != this._text)
				{
					this._text = value;
					base.OnPropertyChangedWithValue<string>(value, "Text");
				}
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x00025293 File Offset: 0x00023493
		// (set) Token: 0x0600097A RID: 2426 RVA: 0x0002529B File Offset: 0x0002349B
		[DataSourceProperty]
		public string Type
		{
			get
			{
				return this._type;
			}
			set
			{
				if (value != this._type)
				{
					this._type = value;
					base.OnPropertyChangedWithValue<string>(value, "Type");
				}
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x000252BE File Offset: 0x000234BE
		// (set) Token: 0x0600097C RID: 2428 RVA: 0x000252C6 File Offset: 0x000234C6
		[DataSourceProperty]
		public MBBindingList<CreditsItemVM> Items
		{
			get
			{
				return this._items;
			}
			set
			{
				if (value != this._items)
				{
					this._items = value;
					base.OnPropertyChangedWithValue<MBBindingList<CreditsItemVM>>(value, "Items");
				}
			}
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x000252E4 File Offset: 0x000234E4
		public CreditsItemVM()
		{
			this._items = new MBBindingList<CreditsItemVM>();
			this.Type = "Entry";
			this.Text = "";
		}

		// Token: 0x04000486 RID: 1158
		private string _text;

		// Token: 0x04000487 RID: 1159
		private string _type;

		// Token: 0x04000488 RID: 1160
		private MBBindingList<CreditsItemVM> _items;
	}
}
