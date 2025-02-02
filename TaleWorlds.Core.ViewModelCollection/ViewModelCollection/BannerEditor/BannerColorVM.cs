using System;
using TaleWorlds.Library;

namespace TaleWorlds.Core.ViewModelCollection.BannerEditor
{
	// Token: 0x02000026 RID: 38
	public class BannerColorVM : ViewModel
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00005A1D File Offset: 0x00003C1D
		public int ColorID { get; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00005A25 File Offset: 0x00003C25
		public uint Color { get; }

		// Token: 0x060001C6 RID: 454 RVA: 0x00005A30 File Offset: 0x00003C30
		public BannerColorVM(int colorID, uint color, Action<BannerColorVM> onSelection)
		{
			this.Color = color;
			this.ColorAsStr = TaleWorlds.Library.Color.FromUint(this.Color).ToString();
			this.ColorID = colorID;
			this._onSelection = onSelection;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00005A77 File Offset: 0x00003C77
		public void ExecuteSelectIcon()
		{
			this._onSelection(this);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00005A85 File Offset: 0x00003C85
		public void SetOnSelectionAction(Action<BannerColorVM> onSelection)
		{
			this._onSelection = onSelection;
			this.IsSelected = false;
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00005A95 File Offset: 0x00003C95
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00005A9D File Offset: 0x00003C9D
		[DataSourceProperty]
		public string ColorAsStr
		{
			get
			{
				return this._colorAsStr;
			}
			set
			{
				if (value != this._colorAsStr)
				{
					this._colorAsStr = value;
					base.OnPropertyChangedWithValue<string>(value, "ColorAsStr");
				}
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00005AC0 File Offset: 0x00003CC0
		// (set) Token: 0x060001CC RID: 460 RVA: 0x00005AC8 File Offset: 0x00003CC8
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

		// Token: 0x040000B8 RID: 184
		private Action<BannerColorVM> _onSelection;

		// Token: 0x040000B9 RID: 185
		private string _colorAsStr;

		// Token: 0x040000BA RID: 186
		private bool _isSelected;
	}
}
