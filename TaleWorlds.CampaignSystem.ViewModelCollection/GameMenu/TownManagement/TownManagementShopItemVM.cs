using System;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.TownManagement
{
	// Token: 0x02000099 RID: 153
	public class TownManagementShopItemVM : ViewModel
	{
		// Token: 0x06000ED7 RID: 3799 RVA: 0x0003B164 File Offset: 0x00039364
		public TownManagementShopItemVM(Workshop workshop)
		{
			this._workshop = workshop;
			this.IsEmpty = (this._workshop.WorkshopType == null);
			if (!this.IsEmpty)
			{
				this.ShopId = this._workshop.WorkshopType.StringId;
			}
			else
			{
				this.ShopId = "empty";
			}
			this.RefreshValues();
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0003B1C4 File Offset: 0x000393C4
		public override void RefreshValues()
		{
			base.RefreshValues();
			if (!this.IsEmpty)
			{
				this.ShopName = this._workshop.WorkshopType.Name.ToString();
				return;
			}
			this.ShopName = GameTexts.FindText("str_empty", null).ToString();
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0003B211 File Offset: 0x00039411
		public void ExecuteBeginHint()
		{
			if (this._workshop.WorkshopType != null)
			{
				InformationManager.ShowTooltip(typeof(Workshop), new object[]
				{
					this._workshop
				});
			}
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0003B23E File Offset: 0x0003943E
		public void ExecuteEndHint()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x0003B245 File Offset: 0x00039445
		// (set) Token: 0x06000EDC RID: 3804 RVA: 0x0003B24D File Offset: 0x0003944D
		[DataSourceProperty]
		public bool IsEmpty
		{
			get
			{
				return this._isEmpty;
			}
			set
			{
				if (value != this._isEmpty)
				{
					this._isEmpty = value;
					base.OnPropertyChangedWithValue(value, "IsEmpty");
				}
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x0003B26B File Offset: 0x0003946B
		// (set) Token: 0x06000EDE RID: 3806 RVA: 0x0003B273 File Offset: 0x00039473
		[DataSourceProperty]
		public string ShopName
		{
			get
			{
				return this._shopName;
			}
			set
			{
				if (value != this._shopName)
				{
					this._shopName = value;
					base.OnPropertyChangedWithValue<string>(value, "ShopName");
				}
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x0003B296 File Offset: 0x00039496
		// (set) Token: 0x06000EE0 RID: 3808 RVA: 0x0003B29E File Offset: 0x0003949E
		[DataSourceProperty]
		public string ShopId
		{
			get
			{
				return this._shopId;
			}
			set
			{
				if (value != this._shopId)
				{
					this._shopId = value;
					base.OnPropertyChangedWithValue<string>(value, "ShopId");
				}
			}
		}

		// Token: 0x040006E6 RID: 1766
		private readonly Workshop _workshop;

		// Token: 0x040006E7 RID: 1767
		private bool _isEmpty;

		// Token: 0x040006E8 RID: 1768
		private string _shopName;

		// Token: 0x040006E9 RID: 1769
		private string _shopId;
	}
}
