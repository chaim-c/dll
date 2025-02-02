using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.FaceGenerator
{
	// Token: 0x0200006C RID: 108
	public class FacegenListItemVM : ViewModel
	{
		// Token: 0x0600087E RID: 2174 RVA: 0x00020B6F File Offset: 0x0001ED6F
		public void ExecuteAction()
		{
			this._setSelected(this, true);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00020B7E File Offset: 0x0001ED7E
		public FacegenListItemVM(string imagePath, int index, Action<FacegenListItemVM, bool> setSelected)
		{
			this.ImagePath = imagePath;
			this.Index = index;
			this.IsSelected = false;
			this._setSelected = setSelected;
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x00020BB0 File Offset: 0x0001EDB0
		// (set) Token: 0x06000881 RID: 2177 RVA: 0x00020BB8 File Offset: 0x0001EDB8
		[DataSourceProperty]
		public string ImagePath
		{
			get
			{
				return this._imagePath;
			}
			set
			{
				if (value != this._imagePath)
				{
					this._imagePath = value;
					base.OnPropertyChangedWithValue<string>(value, "ImagePath");
				}
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x00020BDB File Offset: 0x0001EDDB
		// (set) Token: 0x06000883 RID: 2179 RVA: 0x00020BE3 File Offset: 0x0001EDE3
		[DataSourceProperty]
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				if (value != this._isSelected)
				{
					this._isSelected = value;
					base.OnPropertyChangedWithValue(value, "IsSelected");
				}
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x00020C01 File Offset: 0x0001EE01
		// (set) Token: 0x06000885 RID: 2181 RVA: 0x00020C09 File Offset: 0x0001EE09
		[DataSourceProperty]
		public int Index
		{
			get
			{
				return this._index;
			}
			set
			{
				if (value != this._index)
				{
					this._index = value;
					base.OnPropertyChangedWithValue(value, "Index");
				}
			}
		}

		// Token: 0x040003EF RID: 1007
		private readonly Action<FacegenListItemVM, bool> _setSelected;

		// Token: 0x040003F0 RID: 1008
		private string _imagePath;

		// Token: 0x040003F1 RID: 1009
		private bool _isSelected = true;

		// Token: 0x040003F2 RID: 1010
		private int _index = -1;
	}
}
