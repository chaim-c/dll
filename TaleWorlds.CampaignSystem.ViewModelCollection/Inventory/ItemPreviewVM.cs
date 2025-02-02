using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Inventory
{
	// Token: 0x02000081 RID: 129
	public class ItemPreviewVM : ViewModel
	{
		// Token: 0x06000BB2 RID: 2994 RVA: 0x0002FD71 File Offset: 0x0002DF71
		public ItemPreviewVM(Action onClosed)
		{
			this._onClosed = onClosed;
			this.ItemTableau = new ItemCollectionElementViewModel();
			this.RefreshValues();
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0002FD91 File Offset: 0x0002DF91
		public override void OnFinalize()
		{
			this.ItemTableau.OnFinalize();
			this.ItemTableau = null;
			base.OnFinalize();
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002FDAB File Offset: 0x0002DFAB
		public void Open(EquipmentElement item)
		{
			this.ItemTableau.FillFrom(item, BannerCode.CreateFrom(Clan.PlayerClan.Banner).Code);
			this.ItemName = item.Item.Name.ToString();
			this.IsSelected = true;
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0002FDEB File Offset: 0x0002DFEB
		public void ExecuteClose()
		{
			this.Close();
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002FDF3 File Offset: 0x0002DFF3
		public void Close()
		{
			this._onClosed();
			this.IsSelected = false;
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x0002FE07 File Offset: 0x0002E007
		// (set) Token: 0x06000BB8 RID: 3000 RVA: 0x0002FE0F File Offset: 0x0002E00F
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
					base.OnPropertyChanged("IsSelected");
				}
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x0002FE2C File Offset: 0x0002E02C
		// (set) Token: 0x06000BBA RID: 3002 RVA: 0x0002FE34 File Offset: 0x0002E034
		[DataSourceProperty]
		public string ItemName
		{
			get
			{
				return this._itemName;
			}
			set
			{
				if (value != this._itemName)
				{
					this._itemName = value;
					base.OnPropertyChanged("ItemName");
				}
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x0002FE56 File Offset: 0x0002E056
		// (set) Token: 0x06000BBC RID: 3004 RVA: 0x0002FE5E File Offset: 0x0002E05E
		[DataSourceProperty]
		public ItemCollectionElementViewModel ItemTableau
		{
			get
			{
				return this._itemTableau;
			}
			set
			{
				if (value != this._itemTableau)
				{
					this._itemTableau = value;
					base.OnPropertyChangedWithValue<ItemCollectionElementViewModel>(value, "ItemTableau");
				}
			}
		}

		// Token: 0x04000567 RID: 1383
		private Action _onClosed;

		// Token: 0x04000568 RID: 1384
		private bool _isSelected;

		// Token: 0x04000569 RID: 1385
		private string _itemName;

		// Token: 0x0400056A RID: 1386
		private ItemCollectionElementViewModel _itemTableau;
	}
}
