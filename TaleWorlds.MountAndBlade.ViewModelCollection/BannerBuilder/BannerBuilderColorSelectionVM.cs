using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.BannerBuilder
{
	// Token: 0x02000076 RID: 118
	public class BannerBuilderColorSelectionVM : ViewModel
	{
		// Token: 0x0600099E RID: 2462 RVA: 0x00025A28 File Offset: 0x00023C28
		public BannerBuilderColorSelectionVM()
		{
			this.Items = new MBBindingList<BannerBuilderColorItemVM>();
			this.PopulateItems();
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00025A44 File Offset: 0x00023C44
		public void EnableWith(int selectedColorID, Action<BannerBuilderColorItemVM> onSelection)
		{
			this._onSelection = onSelection;
			this.Items.ApplyActionOnAllItems(delegate(BannerBuilderColorItemVM i)
			{
				i.IsSelected = (i.ColorID == selectedColorID);
			});
			this.IsEnabled = true;
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00025A83 File Offset: 0x00023C83
		private void OnItemSelection(BannerBuilderColorItemVM item)
		{
			Action<BannerBuilderColorItemVM> onSelection = this._onSelection;
			if (onSelection != null)
			{
				onSelection(item);
			}
			this._onSelection = null;
			this.IsEnabled = false;
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00025AA8 File Offset: 0x00023CA8
		private void PopulateItems()
		{
			this.Items.Clear();
			MBReadOnlyDictionary<int, BannerColor> readOnlyColorPalette = BannerManager.Instance.ReadOnlyColorPalette;
			for (int i = 0; i < readOnlyColorPalette.Count; i++)
			{
				KeyValuePair<int, BannerColor> keyValuePair = readOnlyColorPalette.ElementAt(i);
				this.Items.Add(new BannerBuilderColorItemVM(new Action<BannerBuilderColorItemVM>(this.OnItemSelection), keyValuePair.Key, keyValuePair.Value));
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x00025B0E File Offset: 0x00023D0E
		// (set) Token: 0x060009A3 RID: 2467 RVA: 0x00025B16 File Offset: 0x00023D16
		[DataSourceProperty]
		public MBBindingList<BannerBuilderColorItemVM> Items
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
					base.OnPropertyChangedWithValue<MBBindingList<BannerBuilderColorItemVM>>(value, "Items");
				}
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x00025B34 File Offset: 0x00023D34
		// (set) Token: 0x060009A5 RID: 2469 RVA: 0x00025B3C File Offset: 0x00023D3C
		[DataSourceProperty]
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				if (value != this._isEnabled)
				{
					this._isEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsEnabled");
				}
			}
		}

		// Token: 0x04000497 RID: 1175
		private Action<BannerBuilderColorItemVM> _onSelection;

		// Token: 0x04000498 RID: 1176
		private MBBindingList<BannerBuilderColorItemVM> _items;

		// Token: 0x04000499 RID: 1177
		private bool _isEnabled;
	}
}
