using System;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting
{
	// Token: 0x020000DF RID: 223
	public class CraftingResourceItemVM : ViewModel
	{
		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x0004E4DB File Offset: 0x0004C6DB
		// (set) Token: 0x060014A7 RID: 5287 RVA: 0x0004E4E3 File Offset: 0x0004C6E3
		public ItemObject ResourceItem { get; private set; }

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x0004E4EC File Offset: 0x0004C6EC
		// (set) Token: 0x060014A9 RID: 5289 RVA: 0x0004E4F4 File Offset: 0x0004C6F4
		public CraftingMaterials ResourceMaterial { get; private set; }

		// Token: 0x060014AA RID: 5290 RVA: 0x0004E500 File Offset: 0x0004C700
		public CraftingResourceItemVM(CraftingMaterials material, int amount, int changeAmount = 0)
		{
			this.ResourceMaterial = material;
			Campaign campaign = Campaign.Current;
			ItemObject resourceItem;
			if (campaign == null)
			{
				resourceItem = null;
			}
			else
			{
				GameModels models = campaign.Models;
				if (models == null)
				{
					resourceItem = null;
				}
				else
				{
					SmithingModel smithingModel = models.SmithingModel;
					resourceItem = ((smithingModel != null) ? smithingModel.GetCraftingMaterialItem(material) : null);
				}
			}
			this.ResourceItem = resourceItem;
			ItemObject resourceItem2 = this.ResourceItem;
			string text;
			if (resourceItem2 == null)
			{
				text = null;
			}
			else
			{
				TextObject name = resourceItem2.Name;
				text = ((name != null) ? name.ToString() : null);
			}
			this.ResourceName = (text ?? "none");
			this.ResourceHint = new HintViewModel(new TextObject("{=!}" + this.ResourceName, null), null);
			this.ResourceAmount = amount;
			ItemObject resourceItem3 = this.ResourceItem;
			this.ResourceItemStringId = (((resourceItem3 != null) ? resourceItem3.StringId : null) ?? "none");
			this.ResourceMaterialTypeAsStr = this.ResourceMaterial.ToString();
			this.ResourceChangeAmount = changeAmount;
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x0004E5E2 File Offset: 0x0004C7E2
		// (set) Token: 0x060014AC RID: 5292 RVA: 0x0004E5EA File Offset: 0x0004C7EA
		[DataSourceProperty]
		public string ResourceName
		{
			get
			{
				return this._resourceName;
			}
			set
			{
				if (value != this._resourceName)
				{
					this._resourceName = value;
					base.OnPropertyChangedWithValue<string>(value, "ResourceName");
				}
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x060014AD RID: 5293 RVA: 0x0004E60D File Offset: 0x0004C80D
		// (set) Token: 0x060014AE RID: 5294 RVA: 0x0004E615 File Offset: 0x0004C815
		[DataSourceProperty]
		public HintViewModel ResourceHint
		{
			get
			{
				return this._resourceHint;
			}
			set
			{
				if (value != this._resourceHint)
				{
					this._resourceHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ResourceHint");
				}
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060014AF RID: 5295 RVA: 0x0004E633 File Offset: 0x0004C833
		// (set) Token: 0x060014B0 RID: 5296 RVA: 0x0004E63B File Offset: 0x0004C83B
		[DataSourceProperty]
		public string ResourceMaterialTypeAsStr
		{
			get
			{
				return this._resourceMaterialTypeAsStr;
			}
			set
			{
				if (value != this._resourceMaterialTypeAsStr)
				{
					this._resourceMaterialTypeAsStr = value;
					base.OnPropertyChangedWithValue<string>(value, "ResourceMaterialTypeAsStr");
				}
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060014B1 RID: 5297 RVA: 0x0004E65E File Offset: 0x0004C85E
		// (set) Token: 0x060014B2 RID: 5298 RVA: 0x0004E666 File Offset: 0x0004C866
		[DataSourceProperty]
		public int ResourceAmount
		{
			get
			{
				return this._resourceUsageAmount;
			}
			set
			{
				if (value != this._resourceUsageAmount)
				{
					this._resourceUsageAmount = value;
					base.OnPropertyChangedWithValue(value, "ResourceAmount");
				}
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x0004E684 File Offset: 0x0004C884
		// (set) Token: 0x060014B4 RID: 5300 RVA: 0x0004E68C File Offset: 0x0004C88C
		[DataSourceProperty]
		public int ResourceChangeAmount
		{
			get
			{
				return this._resourceChangeAmount;
			}
			set
			{
				if (value != this._resourceChangeAmount)
				{
					this._resourceChangeAmount = value;
					base.OnPropertyChangedWithValue(value, "ResourceChangeAmount");
				}
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x060014B5 RID: 5301 RVA: 0x0004E6AA File Offset: 0x0004C8AA
		// (set) Token: 0x060014B6 RID: 5302 RVA: 0x0004E6B2 File Offset: 0x0004C8B2
		[DataSourceProperty]
		public string ResourceItemStringId
		{
			get
			{
				return this._resourceItemStringId;
			}
			set
			{
				if (value != this._resourceItemStringId)
				{
					this._resourceItemStringId = value;
					base.OnPropertyChangedWithValue<string>(value, "ResourceItemStringId");
				}
			}
		}

		// Token: 0x0400099C RID: 2460
		private string _resourceName;

		// Token: 0x0400099D RID: 2461
		private string _resourceItemStringId;

		// Token: 0x0400099E RID: 2462
		private int _resourceUsageAmount;

		// Token: 0x0400099F RID: 2463
		private int _resourceChangeAmount;

		// Token: 0x040009A0 RID: 2464
		private string _resourceMaterialTypeAsStr;

		// Token: 0x040009A1 RID: 2465
		private HintViewModel _resourceHint;
	}
}
