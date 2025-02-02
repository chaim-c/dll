using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.BannerBuilder
{
	// Token: 0x02000077 RID: 119
	public class BannerBuilderItemVM : ViewModel
	{
		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x00025B5A File Offset: 0x00023D5A
		// (set) Token: 0x060009A7 RID: 2471 RVA: 0x00025B62 File Offset: 0x00023D62
		public BannerIconData IconData { get; private set; }

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x00025B6B File Offset: 0x00023D6B
		// (set) Token: 0x060009A9 RID: 2473 RVA: 0x00025B73 File Offset: 0x00023D73
		public string BackgroundTextureID { get; private set; }

		// Token: 0x060009AA RID: 2474 RVA: 0x00025B7C File Offset: 0x00023D7C
		public BannerBuilderItemVM(int key, BannerIconData iconData, Action<BannerBuilderItemVM> onItemSelection)
		{
			this.MeshID = key;
			this.IconData = iconData;
			this._onItemSelection = onItemSelection;
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00025B99 File Offset: 0x00023D99
		public BannerBuilderItemVM(int key, string backgroundTextureID, Action<BannerBuilderItemVM> onItemSelection)
		{
			this.MeshID = key;
			this.BackgroundTextureID = backgroundTextureID;
			this._onItemSelection = onItemSelection;
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00025BB6 File Offset: 0x00023DB6
		public void ExecuteSelection()
		{
			Action<BannerBuilderItemVM> onItemSelection = this._onItemSelection;
			if (onItemSelection == null)
			{
				return;
			}
			onItemSelection(this);
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x00025BC9 File Offset: 0x00023DC9
		// (set) Token: 0x060009AE RID: 2478 RVA: 0x00025BD1 File Offset: 0x00023DD1
		[DataSourceProperty]
		public int MeshID
		{
			get
			{
				return this._meshID;
			}
			set
			{
				if (value != this._meshID)
				{
					this._meshID = value;
					base.OnPropertyChangedWithValue(value, "MeshID");
					this.MeshIDAsString = this._meshID.ToString();
				}
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x00025C00 File Offset: 0x00023E00
		// (set) Token: 0x060009B0 RID: 2480 RVA: 0x00025C08 File Offset: 0x00023E08
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

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x00025C26 File Offset: 0x00023E26
		// (set) Token: 0x060009B2 RID: 2482 RVA: 0x00025C2E File Offset: 0x00023E2E
		[DataSourceProperty]
		public string MeshIDAsString
		{
			get
			{
				return this._meshIDAsString;
			}
			set
			{
				if (value != this._meshIDAsString)
				{
					this._meshIDAsString = value;
					base.OnPropertyChangedWithValue<string>(value, "MeshIDAsString");
				}
			}
		}

		// Token: 0x0400049C RID: 1180
		private readonly Action<BannerBuilderItemVM> _onItemSelection;

		// Token: 0x0400049D RID: 1181
		public int _meshID;

		// Token: 0x0400049E RID: 1182
		public string _meshIDAsString;

		// Token: 0x0400049F RID: 1183
		public bool _isSelected;
	}
}
