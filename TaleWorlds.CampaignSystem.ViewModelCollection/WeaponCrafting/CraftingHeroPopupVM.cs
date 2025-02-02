using System;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting
{
	// Token: 0x020000DC RID: 220
	public class CraftingHeroPopupVM : ViewModel
	{
		// Token: 0x0600147F RID: 5247 RVA: 0x0004E0F4 File Offset: 0x0004C2F4
		public CraftingHeroPopupVM(Func<MBBindingList<CraftingAvailableHeroItemVM>> getCraftingHeroes)
		{
			this.GetCraftingHeroes = getCraftingHeroes;
			this.SelectHeroText = new TextObject("{=xaeXEj8J}Select character for smithing", null).ToString();
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x0004E119 File Offset: 0x0004C319
		public void ExecuteOpenPopup()
		{
			this.IsVisible = true;
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x0004E122 File Offset: 0x0004C322
		public void ExecuteClosePopup()
		{
			this.IsVisible = false;
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001482 RID: 5250 RVA: 0x0004E12B File Offset: 0x0004C32B
		// (set) Token: 0x06001483 RID: 5251 RVA: 0x0004E133 File Offset: 0x0004C333
		[DataSourceProperty]
		public bool IsVisible
		{
			get
			{
				return this._isVisible;
			}
			set
			{
				if (value != this._isVisible)
				{
					this._isVisible = value;
					base.OnPropertyChangedWithValue(value, "IsVisible");
				}
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001484 RID: 5252 RVA: 0x0004E151 File Offset: 0x0004C351
		// (set) Token: 0x06001485 RID: 5253 RVA: 0x0004E159 File Offset: 0x0004C359
		[DataSourceProperty]
		public string SelectHeroText
		{
			get
			{
				return this._selectHeroText;
			}
			set
			{
				if (value != this._selectHeroText)
				{
					this._selectHeroText = value;
					base.OnPropertyChangedWithValue<string>(value, "SelectHeroText");
				}
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x0004E17C File Offset: 0x0004C37C
		[DataSourceProperty]
		public MBBindingList<CraftingAvailableHeroItemVM> CraftingHeroes
		{
			get
			{
				return this.GetCraftingHeroes();
			}
		}

		// Token: 0x04000987 RID: 2439
		private readonly Func<MBBindingList<CraftingAvailableHeroItemVM>> GetCraftingHeroes;

		// Token: 0x04000988 RID: 2440
		private bool _isVisible;

		// Token: 0x04000989 RID: 2441
		private string _selectHeroText;
	}
}
