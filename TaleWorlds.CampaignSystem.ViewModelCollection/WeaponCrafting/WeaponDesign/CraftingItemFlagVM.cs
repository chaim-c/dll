using System;
using TaleWorlds.CampaignSystem.ViewModelCollection.Inventory;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign
{
	// Token: 0x020000E3 RID: 227
	public class CraftingItemFlagVM : ItemFlagVM
	{
		// Token: 0x0600154C RID: 5452 RVA: 0x00050570 File Offset: 0x0004E770
		public CraftingItemFlagVM(string iconPath, TextObject hint, bool isDisplayed) : base(iconPath, hint)
		{
			this.IsDisplayed = isDisplayed;
			this.IconPath = "SPGeneral\\" + iconPath;
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x00050592 File Offset: 0x0004E792
		// (set) Token: 0x0600154E RID: 5454 RVA: 0x0005059A File Offset: 0x0004E79A
		[DataSourceProperty]
		public bool IsDisplayed
		{
			get
			{
				return this._isDisplayed;
			}
			set
			{
				if (value != this._isDisplayed)
				{
					this._isDisplayed = value;
					base.OnPropertyChangedWithValue(value, "IsDisplayed");
				}
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x000505B8 File Offset: 0x0004E7B8
		// (set) Token: 0x06001550 RID: 5456 RVA: 0x000505C0 File Offset: 0x0004E7C0
		[DataSourceProperty]
		public string IconPath
		{
			get
			{
				return this._iconPath;
			}
			set
			{
				if (value != this._iconPath)
				{
					this._iconPath = value;
					base.OnPropertyChangedWithValue<string>(value, "IconPath");
				}
			}
		}

		// Token: 0x040009E4 RID: 2532
		private bool _isDisplayed;

		// Token: 0x040009E5 RID: 2533
		private string _iconPath;
	}
}
