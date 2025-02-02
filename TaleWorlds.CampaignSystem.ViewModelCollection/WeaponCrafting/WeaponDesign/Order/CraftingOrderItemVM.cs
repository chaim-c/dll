using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.CraftingSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection.Quests;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign.Order
{
	// Token: 0x020000F1 RID: 241
	public class CraftingOrderItemVM : ViewModel
	{
		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x060016BC RID: 5820 RVA: 0x0005515F File Offset: 0x0005335F
		public CraftingOrder CraftingOrder { get; }

		// Token: 0x060016BD RID: 5821 RVA: 0x00055168 File Offset: 0x00053368
		public CraftingOrderItemVM(CraftingOrder order, Action<CraftingOrderItemVM> onSelection, Func<CraftingAvailableHeroItemVM> getCurrentCraftingHero, List<CraftingStatData> orderStatDatas, CampaignUIHelper.IssueQuestFlags questFlags = CampaignUIHelper.IssueQuestFlags.None)
		{
			this.CraftingOrder = order;
			this._orderOwner = order.OrderOwner;
			this._getCurrentCraftingHero = getCurrentCraftingHero;
			this._orderStatDatas = orderStatDatas;
			this._onSelection = onSelection;
			this.WeaponAttributes = new MBBindingList<WeaponAttributeVM>();
			this.OrderOwnerData = new HeroVM(this._orderOwner, false);
			this._weaponTemplate = order.PreCraftedWeaponDesignItem.WeaponDesign.Template;
			this.OrderWeaponTypeCode = this._weaponTemplate.StringId;
			this.Quests = this.GetQuestMarkers(questFlags);
			this.IsQuestOrder = (this.Quests.Count > 0);
			this.RefreshValues();
			this.RefreshStats();
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x00055228 File Offset: 0x00053428
		private MBBindingList<QuestMarkerVM> GetQuestMarkers(CampaignUIHelper.IssueQuestFlags flags)
		{
			MBBindingList<QuestMarkerVM> mbbindingList = new MBBindingList<QuestMarkerVM>();
			if ((flags & CampaignUIHelper.IssueQuestFlags.ActiveIssue) != CampaignUIHelper.IssueQuestFlags.None)
			{
				mbbindingList.Add(new QuestMarkerVM(CampaignUIHelper.IssueQuestFlags.ActiveIssue, null, null));
			}
			if ((flags & CampaignUIHelper.IssueQuestFlags.ActiveStoryQuest) != CampaignUIHelper.IssueQuestFlags.None)
			{
				mbbindingList.Add(new QuestMarkerVM(CampaignUIHelper.IssueQuestFlags.ActiveStoryQuest, null, null));
			}
			return mbbindingList;
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x00055264 File Offset: 0x00053464
		public void RefreshStats()
		{
			this.WeaponAttributes.Clear();
			ItemObject preCraftedWeaponDesignItem = this.CraftingOrder.PreCraftedWeaponDesignItem;
			if (((preCraftedWeaponDesignItem != null) ? preCraftedWeaponDesignItem.Weapons : null) == null)
			{
				Debug.FailedAssert("Crafting order does not contain any valid weapons", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\Crafting\\WeaponDesign\\Order\\CraftingOrderItemVM.cs", "RefreshStats", 71);
				return;
			}
			this.CraftingOrder.GetStatWeapon();
			foreach (CraftingStatData craftingStatData in this._orderStatDatas)
			{
				if (craftingStatData.IsValid)
				{
					this.WeaponAttributes.Add(new WeaponAttributeVM(craftingStatData.Type, craftingStatData.DamageType, craftingStatData.DescriptionText.ToString(), craftingStatData.CurValue));
				}
			}
			IEnumerable<Hero> source = from x in CraftingHelper.GetAvailableHeroesForCrafting()
			where this.CraftingOrder.IsOrderAvailableForHero(x)
			select x;
			this.HasAvailableHeroes = source.Any<Hero>();
			this.OrderPrice = this.CraftingOrder.BaseGoldReward;
			this.RefreshDifficulty();
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x00055368 File Offset: 0x00053568
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.OrderNumberText = GameTexts.FindText("str_crafting_order_header", null).ToString();
			this.OrderWeaponType = this._weaponTemplate.TemplateName.ToString();
			this.OrderDifficultyLabelText = this._difficultyText.ToString();
			this.OrderDifficultyValueText = MathF.Round(this.CraftingOrder.OrderDifficulty).ToString();
			this.DisabledReasonHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetCraftingOrderDisabledReasonTooltip(this._getCurrentCraftingHero().Hero, this.CraftingOrder));
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x000553F0 File Offset: 0x000535F0
		private void RefreshDifficulty()
		{
			Hero hero = this._getCurrentCraftingHero().Hero;
			int skillValue = hero.GetSkillValue(DefaultSkills.Crafting);
			this.IsEnabled = this.CraftingOrder.IsOrderAvailableForHero(hero);
			this.IsDifficultySuitableForHero = (this.CraftingOrder.OrderDifficulty < (float)skillValue);
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x00055441 File Offset: 0x00053641
		public void ExecuteSelectOrder()
		{
			Action<CraftingOrderItemVM> onSelection = this._onSelection;
			if (onSelection == null)
			{
				return;
			}
			onSelection(this);
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x060016C3 RID: 5827 RVA: 0x00055454 File Offset: 0x00053654
		// (set) Token: 0x060016C4 RID: 5828 RVA: 0x0005545C File Offset: 0x0005365C
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

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x060016C5 RID: 5829 RVA: 0x0005547A File Offset: 0x0005367A
		// (set) Token: 0x060016C6 RID: 5830 RVA: 0x00055482 File Offset: 0x00053682
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

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x000554A0 File Offset: 0x000536A0
		// (set) Token: 0x060016C8 RID: 5832 RVA: 0x000554A8 File Offset: 0x000536A8
		[DataSourceProperty]
		public bool HasAvailableHeroes
		{
			get
			{
				return this._hasAvailableHeroes;
			}
			set
			{
				if (value != this._hasAvailableHeroes)
				{
					this._hasAvailableHeroes = value;
					base.OnPropertyChangedWithValue(value, "HasAvailableHeroes");
				}
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x000554C6 File Offset: 0x000536C6
		// (set) Token: 0x060016CA RID: 5834 RVA: 0x000554CE File Offset: 0x000536CE
		[DataSourceProperty]
		public bool IsDifficultySuitableForHero
		{
			get
			{
				return this._isDifficultySuitableForHero;
			}
			set
			{
				if (value != this._isDifficultySuitableForHero)
				{
					this._isDifficultySuitableForHero = value;
					base.OnPropertyChangedWithValue(value, "IsDifficultySuitableForHero");
				}
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x060016CB RID: 5835 RVA: 0x000554EC File Offset: 0x000536EC
		// (set) Token: 0x060016CC RID: 5836 RVA: 0x000554F4 File Offset: 0x000536F4
		[DataSourceProperty]
		public bool IsQuestOrder
		{
			get
			{
				return this._isQuestOrder;
			}
			set
			{
				if (value != this._isQuestOrder)
				{
					this._isQuestOrder = value;
					base.OnPropertyChangedWithValue(value, "IsQuestOrder");
				}
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x00055512 File Offset: 0x00053712
		// (set) Token: 0x060016CE RID: 5838 RVA: 0x0005551A File Offset: 0x0005371A
		[DataSourceProperty]
		public int OrderPrice
		{
			get
			{
				return this._orderPrice;
			}
			set
			{
				if (value != this._orderPrice)
				{
					this._orderPrice = value;
					base.OnPropertyChangedWithValue(value, "OrderPrice");
				}
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x00055538 File Offset: 0x00053738
		// (set) Token: 0x060016D0 RID: 5840 RVA: 0x00055540 File Offset: 0x00053740
		[DataSourceProperty]
		public string OrderDifficultyLabelText
		{
			get
			{
				return this._orderDifficultyLabelText;
			}
			set
			{
				if (value != this._orderDifficultyLabelText)
				{
					this._orderDifficultyLabelText = value;
					base.OnPropertyChangedWithValue<string>(value, "OrderDifficultyLabelText");
				}
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x00055563 File Offset: 0x00053763
		// (set) Token: 0x060016D2 RID: 5842 RVA: 0x0005556B File Offset: 0x0005376B
		[DataSourceProperty]
		public string OrderDifficultyValueText
		{
			get
			{
				return this._orderDifficultyValueText;
			}
			set
			{
				if (value != this._orderDifficultyValueText)
				{
					this._orderDifficultyValueText = value;
					base.OnPropertyChangedWithValue<string>(value, "OrderDifficultyValueText");
				}
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x060016D3 RID: 5843 RVA: 0x0005558E File Offset: 0x0005378E
		// (set) Token: 0x060016D4 RID: 5844 RVA: 0x00055596 File Offset: 0x00053796
		[DataSourceProperty]
		public string OrderNumberText
		{
			get
			{
				return this._orderNumberText;
			}
			set
			{
				if (value != this._orderNumberText)
				{
					this._orderNumberText = value;
					base.OnPropertyChangedWithValue<string>(value, "OrderNumberText");
				}
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x060016D5 RID: 5845 RVA: 0x000555B9 File Offset: 0x000537B9
		// (set) Token: 0x060016D6 RID: 5846 RVA: 0x000555C1 File Offset: 0x000537C1
		[DataSourceProperty]
		public string OrderWeaponType
		{
			get
			{
				return this._orderWeaponType;
			}
			set
			{
				if (value != this._orderWeaponType)
				{
					this._orderWeaponType = value;
					base.OnPropertyChangedWithValue<string>(value, "OrderWeaponType");
				}
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x060016D7 RID: 5847 RVA: 0x000555E4 File Offset: 0x000537E4
		// (set) Token: 0x060016D8 RID: 5848 RVA: 0x000555EC File Offset: 0x000537EC
		[DataSourceProperty]
		public string OrderWeaponTypeCode
		{
			get
			{
				return this._orderWeaponTypeCode;
			}
			set
			{
				if (value != this._orderWeaponTypeCode)
				{
					this._orderWeaponTypeCode = value;
					base.OnPropertyChangedWithValue<string>(value, "OrderWeaponTypeCode");
				}
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x060016D9 RID: 5849 RVA: 0x0005560F File Offset: 0x0005380F
		// (set) Token: 0x060016DA RID: 5850 RVA: 0x00055617 File Offset: 0x00053817
		[DataSourceProperty]
		public HeroVM OrderOwnerData
		{
			get
			{
				return this._orderOwnerData;
			}
			set
			{
				if (value != this._orderOwnerData)
				{
					this._orderOwnerData = value;
					base.OnPropertyChangedWithValue<HeroVM>(value, "OrderOwnerData");
				}
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x060016DB RID: 5851 RVA: 0x00055635 File Offset: 0x00053835
		// (set) Token: 0x060016DC RID: 5852 RVA: 0x0005563D File Offset: 0x0005383D
		[DataSourceProperty]
		public BasicTooltipViewModel DisabledReasonHint
		{
			get
			{
				return this._disabledReasonHint;
			}
			set
			{
				if (value != this._disabledReasonHint)
				{
					this._disabledReasonHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "DisabledReasonHint");
				}
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x060016DD RID: 5853 RVA: 0x0005565B File Offset: 0x0005385B
		// (set) Token: 0x060016DE RID: 5854 RVA: 0x00055663 File Offset: 0x00053863
		[DataSourceProperty]
		public MBBindingList<QuestMarkerVM> Quests
		{
			get
			{
				return this._quests;
			}
			set
			{
				if (value != this._quests)
				{
					this._quests = value;
					base.OnPropertyChangedWithValue<MBBindingList<QuestMarkerVM>>(value, "Quests");
				}
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x060016DF RID: 5855 RVA: 0x00055681 File Offset: 0x00053881
		// (set) Token: 0x060016E0 RID: 5856 RVA: 0x00055689 File Offset: 0x00053889
		[DataSourceProperty]
		public MBBindingList<WeaponAttributeVM> WeaponAttributes
		{
			get
			{
				return this._weaponAttributes;
			}
			set
			{
				if (value != this._weaponAttributes)
				{
					this._weaponAttributes = value;
					base.OnPropertyChangedWithValue<MBBindingList<WeaponAttributeVM>>(value, "WeaponAttributes");
				}
			}
		}

		// Token: 0x04000A98 RID: 2712
		private Hero _orderOwner;

		// Token: 0x04000A99 RID: 2713
		private Action<CraftingOrderItemVM> _onSelection;

		// Token: 0x04000A9A RID: 2714
		private Func<CraftingAvailableHeroItemVM> _getCurrentCraftingHero;

		// Token: 0x04000A9B RID: 2715
		private CraftingTemplate _weaponTemplate;

		// Token: 0x04000A9C RID: 2716
		private TextObject _difficultyText = new TextObject("{=udPWHmOm}Difficulty:", null);

		// Token: 0x04000A9D RID: 2717
		private List<CraftingStatData> _orderStatDatas;

		// Token: 0x04000A9E RID: 2718
		private bool _isEnabled;

		// Token: 0x04000A9F RID: 2719
		private bool _isSelected;

		// Token: 0x04000AA0 RID: 2720
		private bool _hasAvailableHeroes;

		// Token: 0x04000AA1 RID: 2721
		private bool _isDifficultySuitableForHero;

		// Token: 0x04000AA2 RID: 2722
		private bool _isQuestOrder;

		// Token: 0x04000AA3 RID: 2723
		private int _orderPrice;

		// Token: 0x04000AA4 RID: 2724
		private string _orderDifficultyLabelText;

		// Token: 0x04000AA5 RID: 2725
		private string _orderDifficultyValueText;

		// Token: 0x04000AA6 RID: 2726
		private string _orderNumberText;

		// Token: 0x04000AA7 RID: 2727
		private string _orderWeaponType;

		// Token: 0x04000AA8 RID: 2728
		private string _orderWeaponTypeCode;

		// Token: 0x04000AA9 RID: 2729
		private HeroVM _orderOwnerData;

		// Token: 0x04000AAA RID: 2730
		private BasicTooltipViewModel _disabledReasonHint;

		// Token: 0x04000AAB RID: 2731
		private MBBindingList<QuestMarkerVM> _quests;

		// Token: 0x04000AAC RID: 2732
		private MBBindingList<WeaponAttributeVM> _weaponAttributes;
	}
}
