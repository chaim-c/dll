using System;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.CraftingSystem;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting
{
	// Token: 0x020000DB RID: 219
	public class CraftingAvailableHeroItemVM : ViewModel
	{
		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x0004DD59 File Offset: 0x0004BF59
		public Hero Hero { get; }

		// Token: 0x06001463 RID: 5219 RVA: 0x0004DD64 File Offset: 0x0004BF64
		public CraftingAvailableHeroItemVM(Hero hero, Action<CraftingAvailableHeroItemVM> onSelection)
		{
			this._onSelection = onSelection;
			this._craftingBehavior = Campaign.Current.GetCampaignBehavior<ICraftingCampaignBehavior>();
			this.Hero = hero;
			this.HeroData = new HeroVM(this.Hero, false);
			this.Hint = new BasicTooltipViewModel(() => CampaignUIHelper.GetCraftingHeroTooltip(this.Hero, this._craftingOrder));
			this.CraftingPerks = new MBBindingList<CraftingPerkVM>();
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x0004DDC9 File Offset: 0x0004BFC9
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.HeroData.RefreshValues();
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0004DDDC File Offset: 0x0004BFDC
		public void RefreshStamina()
		{
			this.CurrentStamina = (float)this._craftingBehavior.GetHeroCraftingStamina(this.Hero);
			this.MaxStamina = this._craftingBehavior.GetMaxHeroCraftingStamina(this.Hero);
			int content = (int)(this.CurrentStamina / (float)this.MaxStamina * 100f);
			GameTexts.SetVariable("NUMBER", content);
			this.StaminaPercentage = GameTexts.FindText("str_NUMBER_percent", null).ToString();
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0004DE4F File Offset: 0x0004C04F
		public void RefreshOrderAvailability(CraftingOrder order)
		{
			this._craftingOrder = order;
			if (order != null)
			{
				this.IsDisabled = !order.IsOrderAvailableForHero(this.Hero);
				return;
			}
			this.IsDisabled = false;
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0004DE78 File Offset: 0x0004C078
		public void RefreshSkills()
		{
			this.SmithySkillLevel = this.Hero.GetSkillValue(DefaultSkills.Crafting);
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x0004DE90 File Offset: 0x0004C090
		public void RefreshPerks()
		{
			this.CraftingPerks.Clear();
			foreach (PerkObject perkObject in PerkObject.All)
			{
				if (perkObject.Skill == DefaultSkills.Crafting && this.Hero.GetPerkValue(perkObject))
				{
					this.CraftingPerks.Add(new CraftingPerkVM(perkObject));
				}
			}
			this.PerksText = ((this.CraftingPerks.Count > 0) ? new TextObject("{=8lCWWK9G}Smithing Perks", null).ToString() : new TextObject("{=WHRq5Dp0}No Smithing Perks", null).ToString());
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x0004DF48 File Offset: 0x0004C148
		public void ExecuteSelection()
		{
			Action<CraftingAvailableHeroItemVM> onSelection = this._onSelection;
			if (onSelection == null)
			{
				return;
			}
			onSelection(this);
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x0004DF5B File Offset: 0x0004C15B
		// (set) Token: 0x0600146B RID: 5227 RVA: 0x0004DF63 File Offset: 0x0004C163
		[DataSourceProperty]
		public bool IsDisabled
		{
			get
			{
				return this._isDisabled;
			}
			set
			{
				if (value != this._isDisabled)
				{
					this._isDisabled = value;
					base.OnPropertyChangedWithValue(value, "IsDisabled");
				}
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x0004DF81 File Offset: 0x0004C181
		// (set) Token: 0x0600146D RID: 5229 RVA: 0x0004DF89 File Offset: 0x0004C189
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

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x0004DFA7 File Offset: 0x0004C1A7
		// (set) Token: 0x0600146F RID: 5231 RVA: 0x0004DFAF File Offset: 0x0004C1AF
		[DataSourceProperty]
		public HeroVM HeroData
		{
			get
			{
				return this._heroData;
			}
			set
			{
				if (value != this._heroData)
				{
					this._heroData = value;
					base.OnPropertyChangedWithValue<HeroVM>(value, "HeroData");
				}
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x0004DFCD File Offset: 0x0004C1CD
		// (set) Token: 0x06001471 RID: 5233 RVA: 0x0004DFD5 File Offset: 0x0004C1D5
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

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x0004DFF3 File Offset: 0x0004C1F3
		// (set) Token: 0x06001473 RID: 5235 RVA: 0x0004DFFB File Offset: 0x0004C1FB
		[DataSourceProperty]
		public float CurrentStamina
		{
			get
			{
				return this._currentStamina;
			}
			set
			{
				if (value != this._currentStamina)
				{
					this._currentStamina = value;
					base.OnPropertyChangedWithValue(value, "CurrentStamina");
				}
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x0004E019 File Offset: 0x0004C219
		// (set) Token: 0x06001475 RID: 5237 RVA: 0x0004E021 File Offset: 0x0004C221
		[DataSourceProperty]
		public int MaxStamina
		{
			get
			{
				return this._maxStamina;
			}
			set
			{
				if (value != this._maxStamina)
				{
					this._maxStamina = value;
					base.OnPropertyChangedWithValue(value, "MaxStamina");
				}
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x0004E03F File Offset: 0x0004C23F
		// (set) Token: 0x06001477 RID: 5239 RVA: 0x0004E047 File Offset: 0x0004C247
		[DataSourceProperty]
		public string StaminaPercentage
		{
			get
			{
				return this._staminaPercentage;
			}
			set
			{
				if (value != this._staminaPercentage)
				{
					this._staminaPercentage = value;
					base.OnPropertyChangedWithValue<string>(value, "StaminaPercentage");
				}
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x0004E06A File Offset: 0x0004C26A
		// (set) Token: 0x06001479 RID: 5241 RVA: 0x0004E072 File Offset: 0x0004C272
		[DataSourceProperty]
		public int SmithySkillLevel
		{
			get
			{
				return this._smithySkillLevel;
			}
			set
			{
				if (value != this._smithySkillLevel)
				{
					this._smithySkillLevel = value;
					base.OnPropertyChangedWithValue(value, "SmithySkillLevel");
				}
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x0004E090 File Offset: 0x0004C290
		// (set) Token: 0x0600147B RID: 5243 RVA: 0x0004E098 File Offset: 0x0004C298
		[DataSourceProperty]
		public MBBindingList<CraftingPerkVM> CraftingPerks
		{
			get
			{
				return this._craftingPerks;
			}
			set
			{
				if (value != this._craftingPerks)
				{
					this._craftingPerks = value;
					base.OnPropertyChangedWithValue<MBBindingList<CraftingPerkVM>>(value, "CraftingPerks");
				}
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x0004E0B6 File Offset: 0x0004C2B6
		// (set) Token: 0x0600147D RID: 5245 RVA: 0x0004E0BE File Offset: 0x0004C2BE
		[DataSourceProperty]
		public string PerksText
		{
			get
			{
				return this._perksText;
			}
			set
			{
				if (value != this._perksText)
				{
					this._perksText = value;
					base.OnPropertyChangedWithValue<string>(value, "PerksText");
				}
			}
		}

		// Token: 0x0400097A RID: 2426
		private readonly Action<CraftingAvailableHeroItemVM> _onSelection;

		// Token: 0x0400097B RID: 2427
		private readonly ICraftingCampaignBehavior _craftingBehavior;

		// Token: 0x0400097C RID: 2428
		private CraftingOrder _craftingOrder;

		// Token: 0x0400097D RID: 2429
		private HeroVM _heroData;

		// Token: 0x0400097E RID: 2430
		private BasicTooltipViewModel _hint;

		// Token: 0x0400097F RID: 2431
		private float _currentStamina;

		// Token: 0x04000980 RID: 2432
		private int _maxStamina;

		// Token: 0x04000981 RID: 2433
		private string _staminaPercentage;

		// Token: 0x04000982 RID: 2434
		private bool _isDisabled;

		// Token: 0x04000983 RID: 2435
		private bool _isSelected;

		// Token: 0x04000984 RID: 2436
		private int _smithySkillLevel;

		// Token: 0x04000985 RID: 2437
		private MBBindingList<CraftingPerkVM> _craftingPerks;

		// Token: 0x04000986 RID: 2438
		private string _perksText;
	}
}
