using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Armies;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Settlements
{
	// Token: 0x0200005B RID: 91
	public class KingdomSettlementItemVM : KingdomItemVM
	{
		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x00020A71 File Offset: 0x0001EC71
		// (set) Token: 0x06000774 RID: 1908 RVA: 0x00020A79 File Offset: 0x0001EC79
		public int Garrison { get; private set; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x00020A82 File Offset: 0x0001EC82
		// (set) Token: 0x06000776 RID: 1910 RVA: 0x00020A8A File Offset: 0x0001EC8A
		public int Militia { get; private set; }

		// Token: 0x06000777 RID: 1911 RVA: 0x00020A94 File Offset: 0x0001EC94
		public KingdomSettlementItemVM(Settlement settlement, Action<KingdomSettlementItemVM> onSelect)
		{
			this.Settlement = settlement;
			this._onSelect = onSelect;
			this.Name = settlement.Name.ToString();
			this.Villages = new MBBindingList<KingdomSettlementVillageItemVM>();
			SettlementComponent settlementComponent = settlement.SettlementComponent;
			this.SettlementImagePath = ((settlementComponent == null) ? "placeholder" : (settlementComponent.BackgroundMeshName + "_t"));
			this.ItemProperties = new MBBindingList<SelectableFiefItemPropertyVM>();
			this.ImageName = ((settlementComponent != null) ? settlementComponent.WaitMeshName : "");
			this.Owner = new HeroVM(settlement.OwnerClan.Leader, false);
			this.OwnerClanBanner = new ImageIdentifierVM(this.Settlement.OwnerClan.Banner);
			this.OwnerClanBanner_9 = new ImageIdentifierVM(BannerCode.CreateFrom(this.Settlement.OwnerClan.Banner), true);
			Town town = settlement.Town;
			this.WallLevel = ((town == null) ? -1 : town.GetWallLevel());
			if (town != null)
			{
				this.Prosperity = MathF.Round(town.Prosperity);
				this.IconPath = town.BackgroundMeshName;
			}
			else if (settlement.IsCastle)
			{
				this.Prosperity = MathF.Round(settlement.Town.Prosperity);
				this.IconPath = "";
			}
			foreach (Village village in this.Settlement.BoundVillages)
			{
				this.Villages.Add(new KingdomSettlementVillageItemVM(village));
			}
			int defenders;
			if (!this.Settlement.IsFortification)
			{
				defenders = (int)this.Settlement.Militia;
			}
			else
			{
				MobileParty garrisonParty = this.Settlement.Town.GarrisonParty;
				defenders = ((garrisonParty != null) ? garrisonParty.Party.NumberOfAllMembers : 0);
			}
			this.Defenders = defenders;
			this.RefreshValues();
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00020C70 File Offset: 0x0001EE70
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Villages.ApplyActionOnAllItems(delegate(KingdomSettlementVillageItemVM x)
			{
				x.RefreshValues();
			});
			this.UpdateProperties();
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00020CA8 File Offset: 0x0001EEA8
		private void UpdateProperties()
		{
			this.ItemProperties.Clear();
			int num = (int)this.Settlement.Militia;
			List<TooltipProperty> militiaHint = this.Settlement.IsVillage ? CampaignUIHelper.GetVillageMilitiaTooltip(this.Settlement.Village) : CampaignUIHelper.GetTownMilitiaTooltip(this.Settlement.Town);
			int changeAmount = (this.Settlement.Town != null) ? ((int)this.Settlement.Town.MilitiaChange) : ((int)this.Settlement.Village.MilitiaChange);
			this.ItemProperties.Add(new SelectableFiefItemPropertyVM(GameTexts.FindText("str_militia", null).ToString(), num.ToString(), changeAmount, SelectableItemPropertyVM.PropertyType.Militia, new BasicTooltipViewModel(() => militiaHint), false));
			BasicTooltipViewModel hint5;
			if (this.Settlement.Town != null)
			{
				BasicTooltipViewModel hint = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownFoodTooltip(this.Settlement.Town));
				int changeAmount2 = (int)this.Settlement.Town.FoodChange;
				this.ItemProperties.Add(new SelectableFiefItemPropertyVM(GameTexts.FindText("str_food_stocks", null).ToString(), ((int)this.Settlement.Town.FoodStocks).ToString(), changeAmount2, SelectableItemPropertyVM.PropertyType.Food, hint, false));
				BasicTooltipViewModel hint2 = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownGarrisonTooltip(this.Settlement.Town));
				int garrisonChange = this.Settlement.Town.GarrisonChange;
				Collection<SelectableFiefItemPropertyVM> itemProperties = this.ItemProperties;
				string name = GameTexts.FindText("str_garrison", null).ToString();
				MobileParty garrisonParty = this.Settlement.Town.GarrisonParty;
				itemProperties.Add(new SelectableFiefItemPropertyVM(name, ((garrisonParty != null) ? garrisonParty.Party.NumberOfAllMembers.ToString() : null) ?? "0", garrisonChange, SelectableItemPropertyVM.PropertyType.Garrison, hint2, false));
				BasicTooltipViewModel hint3 = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownLoyaltyTooltip(this.Settlement.Town));
				int changeAmount3 = (int)this.Settlement.Town.LoyaltyChange;
				bool isWarning = this.Settlement.IsTown && this.Settlement.Town.Loyalty < (float)Campaign.Current.Models.SettlementLoyaltyModel.RebelliousStateStartLoyaltyThreshold;
				this.ItemProperties.Add(new SelectableFiefItemPropertyVM(GameTexts.FindText("str_loyalty", null).ToString(), string.Format("{0:0.#}", this.Settlement.Town.Loyalty), changeAmount3, SelectableItemPropertyVM.PropertyType.Loyalty, hint3, isWarning));
				BasicTooltipViewModel hint4 = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownWallsTooltip(this.Settlement.Town));
				this.ItemProperties.Add(new SelectableFiefItemPropertyVM(GameTexts.FindText("str_walls", null).ToString(), this.Settlement.Town.GetWallLevel().ToString(), 0, SelectableItemPropertyVM.PropertyType.Wall, hint4, false));
				hint5 = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownProsperityTooltip(this.Settlement.Town));
				BasicTooltipViewModel hint6 = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownSecurityTooltip(this.Settlement.Town));
				int changeAmount4 = (int)this.Settlement.Town.SecurityChange;
				this.ItemProperties.Add(new SelectableFiefItemPropertyVM(GameTexts.FindText("str_security", null).ToString(), string.Format("{0:0.#}", this.Settlement.Town.Security), changeAmount4, SelectableItemPropertyVM.PropertyType.Security, hint6, false));
			}
			else
			{
				hint5 = new BasicTooltipViewModel(() => CampaignUIHelper.GetVillageProsperityTooltip(this.Settlement.Village));
			}
			int changeAmount5 = (this.Settlement.Town != null) ? ((int)this.Settlement.Town.ProsperityChange) : ((int)this.Settlement.Village.HearthChange);
			if (this.Settlement.IsFortification)
			{
				this.ItemProperties.Add(new SelectableFiefItemPropertyVM(GameTexts.FindText("str_prosperity", null).ToString(), string.Format("{0:0.#}", this.Settlement.Town.Prosperity), changeAmount5, SelectableItemPropertyVM.PropertyType.Prosperity, hint5, false));
			}
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0002107B File Offset: 0x0001F27B
		protected override void OnSelect()
		{
			base.OnSelect();
			this._onSelect(this);
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0002108F File Offset: 0x0001F28F
		private void ExecuteBeginHint()
		{
			InformationManager.ShowTooltip(typeof(Settlement), new object[]
			{
				this.Settlement,
				true
			});
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x000210B8 File Offset: 0x0001F2B8
		private void ExecuteEndHint()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x000210BF File Offset: 0x0001F2BF
		public void ExecuteLink()
		{
			if (this.Settlement != null)
			{
				Campaign.Current.EncyclopediaManager.GoToLink(this.Settlement.EncyclopediaLink);
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x000210E3 File Offset: 0x0001F2E3
		// (set) Token: 0x0600077F RID: 1919 RVA: 0x000210EB File Offset: 0x0001F2EB
		[DataSourceProperty]
		public MBBindingList<SelectableFiefItemPropertyVM> ItemProperties
		{
			get
			{
				return this._itemProperties;
			}
			set
			{
				if (value != this._itemProperties)
				{
					this._itemProperties = value;
					base.OnPropertyChangedWithValue<MBBindingList<SelectableFiefItemPropertyVM>>(value, "ItemProperties");
				}
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x00021109 File Offset: 0x0001F309
		// (set) Token: 0x06000781 RID: 1921 RVA: 0x00021111 File Offset: 0x0001F311
		[DataSourceProperty]
		public MBBindingList<KingdomSettlementVillageItemVM> Villages
		{
			get
			{
				return this._villages;
			}
			set
			{
				if (value != this._villages)
				{
					this._villages = value;
					base.OnPropertyChangedWithValue<MBBindingList<KingdomSettlementVillageItemVM>>(value, "Villages");
				}
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x0002112F File Offset: 0x0001F32F
		// (set) Token: 0x06000783 RID: 1923 RVA: 0x00021137 File Offset: 0x0001F337
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

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x0002115A File Offset: 0x0001F35A
		// (set) Token: 0x06000785 RID: 1925 RVA: 0x00021162 File Offset: 0x0001F362
		[DataSourceProperty]
		public int Defenders
		{
			get
			{
				return this._defenders;
			}
			set
			{
				if (value != this._defenders)
				{
					this._defenders = value;
					base.OnPropertyChangedWithValue(value, "Defenders");
				}
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x00021180 File Offset: 0x0001F380
		// (set) Token: 0x06000787 RID: 1927 RVA: 0x00021188 File Offset: 0x0001F388
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

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x000211AB File Offset: 0x0001F3AB
		// (set) Token: 0x06000789 RID: 1929 RVA: 0x000211B3 File Offset: 0x0001F3B3
		[DataSourceProperty]
		public string ImageName
		{
			get
			{
				return this._imageName;
			}
			set
			{
				if (value != this._imageName)
				{
					this._imageName = value;
					base.OnPropertyChangedWithValue<string>(value, "ImageName");
				}
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x000211D6 File Offset: 0x0001F3D6
		// (set) Token: 0x0600078B RID: 1931 RVA: 0x000211DE File Offset: 0x0001F3DE
		[DataSourceProperty]
		public string SettlementImagePath
		{
			get
			{
				return this._settlementImagePath;
			}
			set
			{
				if (value != this._settlementImagePath)
				{
					this._settlementImagePath = value;
					base.OnPropertyChangedWithValue<string>(value, "SettlementImagePath");
				}
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x00021201 File Offset: 0x0001F401
		// (set) Token: 0x0600078D RID: 1933 RVA: 0x00021209 File Offset: 0x0001F409
		[DataSourceProperty]
		public string GovernorName
		{
			get
			{
				return this._governorName;
			}
			set
			{
				if (value != this._governorName)
				{
					this._governorName = value;
					base.OnPropertyChangedWithValue<string>(value, "GovernorName");
				}
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x0002122C File Offset: 0x0001F42C
		// (set) Token: 0x0600078F RID: 1935 RVA: 0x00021234 File Offset: 0x0001F434
		[DataSourceProperty]
		public ImageIdentifierVM OwnerClanBanner
		{
			get
			{
				return this._ownerClanBanner;
			}
			set
			{
				if (value != this._ownerClanBanner)
				{
					this._ownerClanBanner = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "OwnerClanBanner");
				}
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x00021252 File Offset: 0x0001F452
		// (set) Token: 0x06000791 RID: 1937 RVA: 0x0002125A File Offset: 0x0001F45A
		[DataSourceProperty]
		public ImageIdentifierVM OwnerClanBanner_9
		{
			get
			{
				return this._ownerClanBanner_9;
			}
			set
			{
				if (value != this._ownerClanBanner_9)
				{
					this._ownerClanBanner_9 = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "OwnerClanBanner_9");
				}
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x00021278 File Offset: 0x0001F478
		// (set) Token: 0x06000793 RID: 1939 RVA: 0x00021280 File Offset: 0x0001F480
		[DataSourceProperty]
		public HeroVM Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				if (value != this._owner)
				{
					this._owner = value;
					base.OnPropertyChangedWithValue<HeroVM>(value, "Owner");
				}
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x0002129E File Offset: 0x0001F49E
		// (set) Token: 0x06000795 RID: 1941 RVA: 0x000212A6 File Offset: 0x0001F4A6
		[DataSourceProperty]
		public int WallLevel
		{
			get
			{
				return this._wallLevel;
			}
			set
			{
				if (value != this._wallLevel)
				{
					this._wallLevel = value;
					base.OnPropertyChangedWithValue(value, "WallLevel");
				}
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x000212C4 File Offset: 0x0001F4C4
		// (set) Token: 0x06000797 RID: 1943 RVA: 0x000212CC File Offset: 0x0001F4CC
		[DataSourceProperty]
		public int Prosperity
		{
			get
			{
				return this._prosperity;
			}
			set
			{
				if (value != this._prosperity)
				{
					this._prosperity = value;
					base.OnPropertyChangedWithValue(value, "Prosperity");
				}
			}
		}

		// Token: 0x04000344 RID: 836
		private readonly Action<KingdomSettlementItemVM> _onSelect;

		// Token: 0x04000345 RID: 837
		public readonly Settlement Settlement;

		// Token: 0x04000348 RID: 840
		private string _iconPath;

		// Token: 0x04000349 RID: 841
		private string _name;

		// Token: 0x0400034A RID: 842
		private string _imageName;

		// Token: 0x0400034B RID: 843
		private string _settlementImagePath;

		// Token: 0x0400034C RID: 844
		private string _governorName;

		// Token: 0x0400034D RID: 845
		private ImageIdentifierVM _ownerClanBanner;

		// Token: 0x0400034E RID: 846
		private ImageIdentifierVM _ownerClanBanner_9;

		// Token: 0x0400034F RID: 847
		private HeroVM _owner;

		// Token: 0x04000350 RID: 848
		private MBBindingList<SelectableFiefItemPropertyVM> _itemProperties;

		// Token: 0x04000351 RID: 849
		private MBBindingList<KingdomSettlementVillageItemVM> _villages;

		// Token: 0x04000352 RID: 850
		private int _wallLevel;

		// Token: 0x04000353 RID: 851
		private int _prosperity;

		// Token: 0x04000354 RID: 852
		private int _defenders;
	}
}
