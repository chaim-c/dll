using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign
{
	// Token: 0x020000EF RID: 239
	public class WeaponDesignSelectorVM : ViewModel
	{
		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x060015F7 RID: 5623 RVA: 0x00051C8B File Offset: 0x0004FE8B
		public WeaponDesign Design { get; }

		// Token: 0x060015F8 RID: 5624 RVA: 0x00051C94 File Offset: 0x0004FE94
		public WeaponDesignSelectorVM(WeaponDesign design, Action<WeaponDesignSelectorVM> onSelection)
		{
			this._onSelection = onSelection;
			this.Design = design;
			TextObject textObject = new TextObject("{=uZhHh7pm}Crafted {CURR_TEMPLATE_NAME}", null);
			textObject.SetTextVariable("CURR_TEMPLATE_NAME", design.Template.TemplateName);
			TextObject textObject2 = design.WeaponName ?? textObject;
			this.Name = textObject2.ToString();
			this._generatedVisualItem = new ItemObject();
			Crafting.GenerateItem(design, textObject2, Hero.MainHero.Culture, design.Template.ItemModifierGroup, ref this._generatedVisualItem);
			MBObjectManager.Instance.RegisterObject<ItemObject>(this._generatedVisualItem);
			this.Visual = new ImageIdentifierVM(this._generatedVisualItem, "");
			this.WeaponTypeCode = design.Template.StringId;
			this.Hint = new BasicTooltipViewModel(() => this.GetHint());
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x00051D6C File Offset: 0x0004FF6C
		private List<TooltipProperty> GetHint()
		{
			List<TooltipProperty> list = new List<TooltipProperty>();
			list.Add(new TooltipProperty("", this._generatedVisualItem.Name.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.Title));
			foreach (CraftingStatData craftingStatData in Crafting.GetStatDatasFromTemplate(0, this._generatedVisualItem, this.Design.Template))
			{
				if (craftingStatData.IsValid && craftingStatData.CurValue > 0f && craftingStatData.MaxValue > 0f)
				{
					list.Add(new TooltipProperty(craftingStatData.DescriptionText.ToString(), craftingStatData.CurValue.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
				}
			}
			return list;
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x00051E3C File Offset: 0x0005003C
		public void ExecuteSelect()
		{
			Action<WeaponDesignSelectorVM> onSelection = this._onSelection;
			if (onSelection == null)
			{
				return;
			}
			onSelection(this);
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x00051E4F File Offset: 0x0005004F
		public override void OnFinalize()
		{
			base.OnFinalize();
			MBObjectManager.Instance.UnregisterObject(this._generatedVisualItem);
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x060015FC RID: 5628 RVA: 0x00051E67 File Offset: 0x00050067
		// (set) Token: 0x060015FD RID: 5629 RVA: 0x00051E6F File Offset: 0x0005006F
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

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x060015FE RID: 5630 RVA: 0x00051E8D File Offset: 0x0005008D
		// (set) Token: 0x060015FF RID: 5631 RVA: 0x00051E95 File Offset: 0x00050095
		[DataSourceProperty]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value != this._name)
				{
					this._name = value;
					base.OnPropertyChangedWithValue<string>(value, "Name");
				}
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06001600 RID: 5632 RVA: 0x00051EB8 File Offset: 0x000500B8
		// (set) Token: 0x06001601 RID: 5633 RVA: 0x00051EC0 File Offset: 0x000500C0
		[DataSourceProperty]
		public string WeaponTypeCode
		{
			get
			{
				return this._weaponTypeCode;
			}
			set
			{
				if (value != this._weaponTypeCode)
				{
					this._weaponTypeCode = value;
					base.OnPropertyChangedWithValue<string>(value, "WeaponTypeCode");
				}
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06001602 RID: 5634 RVA: 0x00051EE3 File Offset: 0x000500E3
		// (set) Token: 0x06001603 RID: 5635 RVA: 0x00051EEB File Offset: 0x000500EB
		[DataSourceProperty]
		public ImageIdentifierVM Visual
		{
			get
			{
				return this._visual;
			}
			set
			{
				if (value != this._visual)
				{
					this._visual = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "Visual");
				}
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06001604 RID: 5636 RVA: 0x00051F09 File Offset: 0x00050109
		// (set) Token: 0x06001605 RID: 5637 RVA: 0x00051F11 File Offset: 0x00050111
		[DataSourceProperty]
		public BasicTooltipViewModel Hint
		{
			get
			{
				return this._hint;
			}
			set
			{
				if (value != this._hint)
				{
					this._hint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "Hint");
				}
			}
		}

		// Token: 0x04000A3F RID: 2623
		private Action<WeaponDesignSelectorVM> _onSelection;

		// Token: 0x04000A40 RID: 2624
		private ItemObject _generatedVisualItem;

		// Token: 0x04000A41 RID: 2625
		private bool _isSelected;

		// Token: 0x04000A42 RID: 2626
		private string _name;

		// Token: 0x04000A43 RID: 2627
		private string _weaponTypeCode;

		// Token: 0x04000A44 RID: 2628
		private ImageIdentifierVM _visual;

		// Token: 0x04000A45 RID: 2629
		private BasicTooltipViewModel _hint;
	}
}
