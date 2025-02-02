using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Core.ViewModelCollection.Tutorial;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages
{
	// Token: 0x020000BE RID: 190
	[EncyclopediaViewModel(typeof(Settlement))]
	public class EncyclopediaSettlementPageVM : EncyclopediaContentPageVM
	{
		// Token: 0x060012B2 RID: 4786 RVA: 0x000490C4 File Offset: 0x000472C4
		public EncyclopediaSettlementPageVM(EncyclopediaPageArgs args) : base(args)
		{
			this._settlement = (base.Obj as Settlement);
			this.NotableCharacters = new MBBindingList<HeroVM>();
			this.Settlements = new MBBindingList<EncyclopediaSettlementVM>();
			this.History = new MBBindingList<EncyclopediaHistoryEventVM>();
			this._isVisualTrackerSelected = Campaign.Current.VisualTrackerManager.CheckTracked(this._settlement);
			this.IsFortification = this._settlement.IsFortification;
			this.SettlementImageID = this._settlement.SettlementComponent.WaitMeshName;
			base.IsBookmarked = Campaign.Current.EncyclopediaManager.ViewDataTracker.IsEncyclopediaBookmarked(this._settlement);
			Game.Current.EventManager.RegisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
			this.RefreshValues();
			TextObject textObject;
			if (CampaignUIHelper.IsSettlementInformationHidden(this._settlement, out textObject))
			{
				Game.Current.EventManager.TriggerEvent<EncyclopediaPageChangedEvent>(new EncyclopediaPageChangedEvent(EncyclopediaPages.Settlement, true));
			}
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x000491B4 File Offset: 0x000473B4
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.SettlementName = this._settlement.Name.ToString();
			this.SettlementsText = GameTexts.FindText("str_villages", null).ToString();
			this.NotableCharactersText = GameTexts.FindText("str_notable_characters", null).ToString();
			this.OwnerText = GameTexts.FindText("str_owner", null).ToString();
			this.TrackText = GameTexts.FindText("str_settlement_track", null).ToString();
			this.ShowInMapHint = new HintViewModel(GameTexts.FindText("str_show_on_map", null), null);
			this.InformationText = this._settlement.EncyclopediaText.ToString();
			base.UpdateBookmarkHintText();
			this.Refresh();
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x00049270 File Offset: 0x00047470
		public override void Refresh()
		{
			base.IsLoadingOver = false;
			SettlementComponent settlementComponent = this._settlement.SettlementComponent;
			this.NotableCharacters.Clear();
			this.Settlements.Clear();
			this.History.Clear();
			this.IsFortification = this._settlement.IsFortification;
			if (this._settlement.IsFortification)
			{
				this.SettlementType = 0;
				EncyclopediaPage pageOf = Campaign.Current.EncyclopediaManager.GetPageOf(typeof(Settlement));
				using (List<Village>.Enumerator enumerator = this._settlement.BoundVillages.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Village village = enumerator.Current;
						if (pageOf.IsValidEncyclopediaItem(village.Owner.Settlement))
						{
							this.Settlements.Add(new EncyclopediaSettlementVM(village.Owner.Settlement));
						}
					}
					goto IL_F2;
				}
			}
			if (this._settlement.IsVillage)
			{
				this.SettlementType = 1;
			}
			IL_F2:
			if (!this._settlement.IsCastle)
			{
				EncyclopediaPage pageOf2 = Campaign.Current.EncyclopediaManager.GetPageOf(typeof(Hero));
				foreach (Hero hero in this._settlement.Notables)
				{
					if (pageOf2.IsValidEncyclopediaItem(hero))
					{
						this.NotableCharacters.Add(new HeroVM(hero, false));
					}
				}
			}
			GameTexts.SetVariable("STR1", GameTexts.FindText("str_enc_sf_culture", null).ToString());
			GameTexts.SetVariable("STR2", this._settlement.Culture.Name.ToString());
			this.CultureText = GameTexts.FindText("str_STR1_space_STR2", null).ToString();
			this.OwnerText = GameTexts.FindText("str_owner", null).ToString();
			this.Owner = new HeroVM(this._settlement.OwnerClan.Leader, false);
			this.OwnerBanner = new EncyclopediaFactionVM(this._settlement.OwnerClan);
			this.SettlementPath = settlementComponent.BackgroundMeshName;
			this.SettlementCropPosition = (double)settlementComponent.BackgroundCropPosition;
			this.HasBoundSettlement = this._settlement.IsVillage;
			this.BoundSettlement = (this.HasBoundSettlement ? new EncyclopediaSettlementVM(this._settlement.Village.Bound) : null);
			this.BoundSettlementText = "";
			if (this.HasBoundSettlement)
			{
				GameTexts.SetVariable("SETTLEMENT_LINK", this._settlement.Village.Bound.EncyclopediaLinkWithName);
				this.BoundSettlementText = GameTexts.FindText("str_bound_settlement_encyclopedia", null).ToString();
			}
			int num = (int)this._settlement.Militia;
			TextObject textObject;
			bool flag = CampaignUIHelper.IsSettlementInformationHidden(this._settlement, out textObject);
			string text = GameTexts.FindText("str_missing_info_indicator", null).ToString();
			this.MilitasText = (flag ? text : num.ToString());
			if (this._settlement.IsFortification)
			{
				this.FoodHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownFoodTooltip(this._settlement.Town));
				this.LoyaltyHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownLoyaltyTooltip(this._settlement.Town));
				this.MilitasHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownMilitiaTooltip(this._settlement.Town));
				this.ProsperityHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownProsperityTooltip(this._settlement.Town));
				this.WallsHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownWallsTooltip(this._settlement.Town));
				this.GarrisonHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownGarrisonTooltip(this._settlement.Town));
				this.SecurityHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownSecurityTooltip(this._settlement.Town));
				this.ProsperityText = (flag ? text : ((int)this._settlement.Town.Prosperity).ToString());
				this.LoyaltyText = (flag ? text : ((int)this._settlement.Town.Loyalty).ToString());
				this.SecurityText = (flag ? text : ((int)this._settlement.Town.Security).ToString());
				string garrisonText;
				if (!flag)
				{
					MobileParty garrisonParty = this._settlement.Town.GarrisonParty;
					garrisonText = (((garrisonParty != null) ? garrisonParty.Party.NumberOfAllMembers.ToString() : null) ?? "0");
				}
				else
				{
					garrisonText = text;
				}
				this.GarrisonText = garrisonText;
				this.FoodText = (flag ? text : ((int)this._settlement.Town.FoodStocks).ToString());
				this.WallsText = (flag ? text : this._settlement.Town.GetWallLevel().ToString());
			}
			else
			{
				this.MilitasHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetVillageMilitiaTooltip(this._settlement.Village));
				this.ProsperityHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetVillageProsperityTooltip(this._settlement.Village));
				this.LoyaltyHint = new BasicTooltipViewModel();
				this.WallsHint = new BasicTooltipViewModel();
				this.ProsperityText = (flag ? text : ((int)this._settlement.Village.Hearth).ToString());
				this.LoyaltyText = "-";
				this.SecurityText = "-";
				this.FoodText = "-";
				this.GarrisonText = "-";
				this.WallsText = "-";
			}
			this.NameText = this._settlement.Name.ToString();
			for (int i = Campaign.Current.LogEntryHistory.GameActionLogs.Count - 1; i >= 0; i--)
			{
				IEncyclopediaLog encyclopediaLog;
				if ((encyclopediaLog = (Campaign.Current.LogEntryHistory.GameActionLogs[i] as IEncyclopediaLog)) != null && encyclopediaLog.IsVisibleInEncyclopediaPageOf<Settlement>(this._settlement))
				{
					this.History.Add(new EncyclopediaHistoryEventVM(encyclopediaLog));
				}
			}
			this.IsVisualTrackerSelected = Campaign.Current.VisualTrackerManager.CheckTracked(this._settlement);
			base.IsLoadingOver = true;
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x00049868 File Offset: 0x00047A68
		public override string GetName()
		{
			return this._settlement.Name.ToString();
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x0004987C File Offset: 0x00047A7C
		public void ExecuteTrack()
		{
			if (!this.IsVisualTrackerSelected)
			{
				Campaign.Current.VisualTrackerManager.RegisterObject(this._settlement);
				this.IsVisualTrackerSelected = true;
			}
			else
			{
				Campaign.Current.VisualTrackerManager.RemoveTrackedObject(this._settlement, false);
				this.IsVisualTrackerSelected = false;
			}
			Game.Current.EventManager.TriggerEvent<PlayerToggleTrackSettlementFromEncyclopediaEvent>(new PlayerToggleTrackSettlementFromEncyclopediaEvent(this._settlement, this.IsVisualTrackerSelected));
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x000498EC File Offset: 0x00047AEC
		public override string GetNavigationBarURL()
		{
			return HyperlinkTexts.GetGenericHyperlinkText("Home", GameTexts.FindText("str_encyclopedia_home", null).ToString()) + " \\ " + HyperlinkTexts.GetGenericHyperlinkText("ListPage-Settlements", GameTexts.FindText("str_encyclopedia_settlements", null).ToString()) + " \\ " + this.GetName();
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x00049951 File Offset: 0x00047B51
		public void ExecuteBoundSettlementLink()
		{
			if (this.HasBoundSettlement)
			{
				Campaign.Current.EncyclopediaManager.GoToLink(this._settlement.Village.Bound.EncyclopediaLink);
			}
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x00049980 File Offset: 0x00047B80
		public override void ExecuteSwitchBookmarkedState()
		{
			base.ExecuteSwitchBookmarkedState();
			if (base.IsBookmarked)
			{
				Campaign.Current.EncyclopediaManager.ViewDataTracker.AddEncyclopediaBookmarkToItem(this._settlement);
				return;
			}
			Campaign.Current.EncyclopediaManager.ViewDataTracker.RemoveEncyclopediaBookmarkFromItem(this._settlement);
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x000499D0 File Offset: 0x00047BD0
		private void OnTutorialNotificationElementIDChange(TutorialNotificationElementChangeEvent evnt)
		{
			this.IsTrackerButtonHighlightEnabled = (evnt.NewNotificationElementID == "EncyclopediaItemTrackButton");
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x000499E8 File Offset: 0x00047BE8
		public override void OnFinalize()
		{
			base.OnFinalize();
			Game.Current.EventManager.UnregisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x00049A0B File Offset: 0x00047C0B
		// (set) Token: 0x060012BD RID: 4797 RVA: 0x00049A13 File Offset: 0x00047C13
		[DataSourceProperty]
		public EncyclopediaFactionVM OwnerBanner
		{
			get
			{
				return this._ownerBanner;
			}
			set
			{
				if (value != this._ownerBanner)
				{
					this._ownerBanner = value;
					base.OnPropertyChangedWithValue<EncyclopediaFactionVM>(value, "OwnerBanner");
				}
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x060012BE RID: 4798 RVA: 0x00049A31 File Offset: 0x00047C31
		// (set) Token: 0x060012BF RID: 4799 RVA: 0x00049A39 File Offset: 0x00047C39
		[DataSourceProperty]
		public EncyclopediaSettlementVM BoundSettlement
		{
			get
			{
				return this._boundSettlement;
			}
			set
			{
				if (value != this._boundSettlement)
				{
					this._boundSettlement = value;
					base.OnPropertyChangedWithValue<EncyclopediaSettlementVM>(value, "BoundSettlement");
				}
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x060012C0 RID: 4800 RVA: 0x00049A57 File Offset: 0x00047C57
		// (set) Token: 0x060012C1 RID: 4801 RVA: 0x00049A5F File Offset: 0x00047C5F
		[DataSourceProperty]
		public bool IsFortification
		{
			get
			{
				return this._isFortification;
			}
			set
			{
				if (value != this._isFortification)
				{
					this._isFortification = value;
					base.OnPropertyChangedWithValue(value, "IsFortification");
				}
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x00049A7D File Offset: 0x00047C7D
		// (set) Token: 0x060012C3 RID: 4803 RVA: 0x00049A85 File Offset: 0x00047C85
		[DataSourceProperty]
		public bool IsTrackerButtonHighlightEnabled
		{
			get
			{
				return this._isTrackerButtonHighlightEnabled;
			}
			set
			{
				if (value != this._isTrackerButtonHighlightEnabled)
				{
					this._isTrackerButtonHighlightEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsTrackerButtonHighlightEnabled");
				}
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x00049AA3 File Offset: 0x00047CA3
		// (set) Token: 0x060012C5 RID: 4805 RVA: 0x00049AAB File Offset: 0x00047CAB
		[DataSourceProperty]
		public bool HasBoundSettlement
		{
			get
			{
				return this._hasBoundSettlement;
			}
			set
			{
				if (value != this._hasBoundSettlement)
				{
					this._hasBoundSettlement = value;
					base.OnPropertyChangedWithValue(value, "HasBoundSettlement");
				}
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x00049AC9 File Offset: 0x00047CC9
		// (set) Token: 0x060012C7 RID: 4807 RVA: 0x00049AD1 File Offset: 0x00047CD1
		[DataSourceProperty]
		public double SettlementCropPosition
		{
			get
			{
				return this._settlementCropPosition;
			}
			set
			{
				if (value != this._settlementCropPosition)
				{
					this._settlementCropPosition = value;
					base.OnPropertyChangedWithValue(value, "SettlementCropPosition");
				}
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x00049AEF File Offset: 0x00047CEF
		// (set) Token: 0x060012C9 RID: 4809 RVA: 0x00049AF7 File Offset: 0x00047CF7
		[DataSourceProperty]
		public string BoundSettlementText
		{
			get
			{
				return this._boundSettlementText;
			}
			set
			{
				if (value != this._boundSettlementText)
				{
					this._boundSettlementText = value;
					base.OnPropertyChangedWithValue<string>(value, "BoundSettlementText");
				}
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x00049B1A File Offset: 0x00047D1A
		// (set) Token: 0x060012CB RID: 4811 RVA: 0x00049B22 File Offset: 0x00047D22
		[DataSourceProperty]
		public string TrackText
		{
			get
			{
				return this._trackText;
			}
			set
			{
				if (value != this._trackText)
				{
					this._trackText = value;
					base.OnPropertyChangedWithValue<string>(value, "TrackText");
				}
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x00049B45 File Offset: 0x00047D45
		// (set) Token: 0x060012CD RID: 4813 RVA: 0x00049B4D File Offset: 0x00047D4D
		[DataSourceProperty]
		public string SettlementPath
		{
			get
			{
				return this._settlementPath;
			}
			set
			{
				if (value != this._settlementPath)
				{
					this._settlementPath = value;
					base.OnPropertyChangedWithValue<string>(value, "SettlementPath");
				}
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x00049B70 File Offset: 0x00047D70
		// (set) Token: 0x060012CF RID: 4815 RVA: 0x00049B78 File Offset: 0x00047D78
		[DataSourceProperty]
		public string SettlementName
		{
			get
			{
				return this._settlementName;
			}
			set
			{
				if (value != this._settlementName)
				{
					this._settlementName = value;
					base.OnPropertyChangedWithValue<string>(value, "SettlementName");
				}
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x00049B9B File Offset: 0x00047D9B
		// (set) Token: 0x060012D1 RID: 4817 RVA: 0x00049BA3 File Offset: 0x00047DA3
		[DataSourceProperty]
		public string InformationText
		{
			get
			{
				return this._informationText;
			}
			set
			{
				if (value != this._informationText)
				{
					this._informationText = value;
					base.OnPropertyChangedWithValue<string>(value, "InformationText");
				}
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x00049BC6 File Offset: 0x00047DC6
		// (set) Token: 0x060012D3 RID: 4819 RVA: 0x00049BCE File Offset: 0x00047DCE
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

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x00049BEC File Offset: 0x00047DEC
		// (set) Token: 0x060012D5 RID: 4821 RVA: 0x00049BF4 File Offset: 0x00047DF4
		[DataSourceProperty]
		public string SettlementsText
		{
			get
			{
				return this._villagesText;
			}
			set
			{
				if (value != this._villagesText)
				{
					this._villagesText = value;
					base.OnPropertyChanged("VillagesText");
				}
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x00049C16 File Offset: 0x00047E16
		// (set) Token: 0x060012D7 RID: 4823 RVA: 0x00049C1E File Offset: 0x00047E1E
		[DataSourceProperty]
		public string SettlementImageID
		{
			get
			{
				return this._settlementImageID;
			}
			set
			{
				if (value != this._settlementImageID)
				{
					this._settlementImageID = value;
					base.OnPropertyChangedWithValue<string>(value, "SettlementImageID");
				}
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x00049C41 File Offset: 0x00047E41
		// (set) Token: 0x060012D9 RID: 4825 RVA: 0x00049C49 File Offset: 0x00047E49
		[DataSourceProperty]
		public string NotableCharactersText
		{
			get
			{
				return this._notableCharactersText;
			}
			set
			{
				if (value != this._notableCharactersText)
				{
					this._notableCharactersText = value;
					base.OnPropertyChangedWithValue<string>(value, "NotableCharactersText");
				}
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x00049C6C File Offset: 0x00047E6C
		// (set) Token: 0x060012DB RID: 4827 RVA: 0x00049C74 File Offset: 0x00047E74
		[DataSourceProperty]
		public int SettlementType
		{
			get
			{
				return this._settlementType;
			}
			set
			{
				if (value != this._settlementType)
				{
					this._settlementType = value;
					base.OnPropertyChangedWithValue(value, "SettlementType");
				}
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x00049C92 File Offset: 0x00047E92
		// (set) Token: 0x060012DD RID: 4829 RVA: 0x00049C9A File Offset: 0x00047E9A
		[DataSourceProperty]
		public MBBindingList<EncyclopediaHistoryEventVM> History
		{
			get
			{
				return this._history;
			}
			set
			{
				if (value != this._history)
				{
					this._history = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaHistoryEventVM>>(value, "History");
				}
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x060012DE RID: 4830 RVA: 0x00049CB8 File Offset: 0x00047EB8
		// (set) Token: 0x060012DF RID: 4831 RVA: 0x00049CC0 File Offset: 0x00047EC0
		[DataSourceProperty]
		public MBBindingList<EncyclopediaSettlementVM> Settlements
		{
			get
			{
				return this._settlements;
			}
			set
			{
				if (value != this._settlements)
				{
					this._settlements = value;
					base.OnPropertyChanged("Villages");
				}
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060012E0 RID: 4832 RVA: 0x00049CDD File Offset: 0x00047EDD
		// (set) Token: 0x060012E1 RID: 4833 RVA: 0x00049CE5 File Offset: 0x00047EE5
		[DataSourceProperty]
		public MBBindingList<HeroVM> NotableCharacters
		{
			get
			{
				return this._notableCharacters;
			}
			set
			{
				if (value != this._notableCharacters)
				{
					this._notableCharacters = value;
					base.OnPropertyChangedWithValue<MBBindingList<HeroVM>>(value, "NotableCharacters");
				}
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x060012E2 RID: 4834 RVA: 0x00049D03 File Offset: 0x00047F03
		// (set) Token: 0x060012E3 RID: 4835 RVA: 0x00049D0B File Offset: 0x00047F0B
		[DataSourceProperty]
		public HintViewModel ShowInMapHint
		{
			get
			{
				return this._showInMapHint;
			}
			set
			{
				if (value != this._showInMapHint)
				{
					this._showInMapHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ShowInMapHint");
				}
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x060012E4 RID: 4836 RVA: 0x00049D29 File Offset: 0x00047F29
		// (set) Token: 0x060012E5 RID: 4837 RVA: 0x00049D31 File Offset: 0x00047F31
		[DataSourceProperty]
		public BasicTooltipViewModel MilitasHint
		{
			get
			{
				return this._militasHint;
			}
			set
			{
				if (value != this._militasHint)
				{
					this._militasHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "MilitasHint");
				}
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x00049D4F File Offset: 0x00047F4F
		// (set) Token: 0x060012E7 RID: 4839 RVA: 0x00049D57 File Offset: 0x00047F57
		[DataSourceProperty]
		public BasicTooltipViewModel FoodHint
		{
			get
			{
				return this._foodHint;
			}
			set
			{
				if (value != this._foodHint)
				{
					this._foodHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "FoodHint");
				}
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x060012E8 RID: 4840 RVA: 0x00049D75 File Offset: 0x00047F75
		// (set) Token: 0x060012E9 RID: 4841 RVA: 0x00049D7D File Offset: 0x00047F7D
		[DataSourceProperty]
		public BasicTooltipViewModel GarrisonHint
		{
			get
			{
				return this._garrisonHint;
			}
			set
			{
				if (value != this._garrisonHint)
				{
					this._garrisonHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "GarrisonHint");
				}
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x060012EA RID: 4842 RVA: 0x00049D9B File Offset: 0x00047F9B
		// (set) Token: 0x060012EB RID: 4843 RVA: 0x00049DA3 File Offset: 0x00047FA3
		[DataSourceProperty]
		public BasicTooltipViewModel ProsperityHint
		{
			get
			{
				return this._prosperityHint;
			}
			set
			{
				if (value != this._prosperityHint)
				{
					this._prosperityHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "ProsperityHint");
				}
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x060012EC RID: 4844 RVA: 0x00049DC1 File Offset: 0x00047FC1
		// (set) Token: 0x060012ED RID: 4845 RVA: 0x00049DC9 File Offset: 0x00047FC9
		[DataSourceProperty]
		public BasicTooltipViewModel LoyaltyHint
		{
			get
			{
				return this._loyaltyHint;
			}
			set
			{
				if (value != this._loyaltyHint)
				{
					this._loyaltyHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "LoyaltyHint");
				}
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x00049DE7 File Offset: 0x00047FE7
		// (set) Token: 0x060012EF RID: 4847 RVA: 0x00049DEF File Offset: 0x00047FEF
		[DataSourceProperty]
		public BasicTooltipViewModel SecurityHint
		{
			get
			{
				return this._securityHint;
			}
			set
			{
				if (value != this._securityHint)
				{
					this._securityHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "SecurityHint");
				}
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x060012F0 RID: 4848 RVA: 0x00049E0D File Offset: 0x0004800D
		// (set) Token: 0x060012F1 RID: 4849 RVA: 0x00049E15 File Offset: 0x00048015
		[DataSourceProperty]
		public BasicTooltipViewModel WallsHint
		{
			get
			{
				return this._wallsHint;
			}
			set
			{
				if (value != this._wallsHint)
				{
					this._wallsHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "WallsHint");
				}
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x060012F2 RID: 4850 RVA: 0x00049E33 File Offset: 0x00048033
		// (set) Token: 0x060012F3 RID: 4851 RVA: 0x00049E3B File Offset: 0x0004803B
		[DataSourceProperty]
		public string MilitasText
		{
			get
			{
				return this._militasText;
			}
			set
			{
				if (value != this._militasText)
				{
					this._militasText = value;
					base.OnPropertyChangedWithValue<string>(value, "MilitasText");
				}
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x060012F4 RID: 4852 RVA: 0x00049E5E File Offset: 0x0004805E
		// (set) Token: 0x060012F5 RID: 4853 RVA: 0x00049E66 File Offset: 0x00048066
		[DataSourceProperty]
		public string ProsperityText
		{
			get
			{
				return this._prosperityText;
			}
			set
			{
				if (value != this._prosperityText)
				{
					this._prosperityText = value;
					base.OnPropertyChangedWithValue<string>(value, "ProsperityText");
				}
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x060012F6 RID: 4854 RVA: 0x00049E89 File Offset: 0x00048089
		// (set) Token: 0x060012F7 RID: 4855 RVA: 0x00049E91 File Offset: 0x00048091
		[DataSourceProperty]
		public string LoyaltyText
		{
			get
			{
				return this._loyaltyText;
			}
			set
			{
				if (value != this._loyaltyText)
				{
					this._loyaltyText = value;
					base.OnPropertyChangedWithValue<string>(value, "LoyaltyText");
				}
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x060012F8 RID: 4856 RVA: 0x00049EB4 File Offset: 0x000480B4
		// (set) Token: 0x060012F9 RID: 4857 RVA: 0x00049EBC File Offset: 0x000480BC
		[DataSourceProperty]
		public string SecurityText
		{
			get
			{
				return this._securityText;
			}
			set
			{
				if (value != this._securityText)
				{
					this._securityText = value;
					base.OnPropertyChangedWithValue<string>(value, "SecurityText");
				}
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x00049EDF File Offset: 0x000480DF
		// (set) Token: 0x060012FB RID: 4859 RVA: 0x00049EE7 File Offset: 0x000480E7
		[DataSourceProperty]
		public string WallsText
		{
			get
			{
				return this._wallsText;
			}
			set
			{
				if (value != this._wallsText)
				{
					this._wallsText = value;
					base.OnPropertyChangedWithValue<string>(value, "WallsText");
				}
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x060012FC RID: 4860 RVA: 0x00049F0A File Offset: 0x0004810A
		// (set) Token: 0x060012FD RID: 4861 RVA: 0x00049F12 File Offset: 0x00048112
		[DataSourceProperty]
		public string FoodText
		{
			get
			{
				return this._foodText;
			}
			set
			{
				if (value != this._foodText)
				{
					this._foodText = value;
					base.OnPropertyChangedWithValue<string>(value, "FoodText");
				}
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x060012FE RID: 4862 RVA: 0x00049F35 File Offset: 0x00048135
		// (set) Token: 0x060012FF RID: 4863 RVA: 0x00049F3D File Offset: 0x0004813D
		[DataSourceProperty]
		public string GarrisonText
		{
			get
			{
				return this._garrisonText;
			}
			set
			{
				if (value != this._garrisonText)
				{
					this._garrisonText = value;
					base.OnPropertyChangedWithValue<string>(value, "GarrisonText");
				}
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001300 RID: 4864 RVA: 0x00049F60 File Offset: 0x00048160
		// (set) Token: 0x06001301 RID: 4865 RVA: 0x00049F68 File Offset: 0x00048168
		[DataSourceProperty]
		public string NameText
		{
			get
			{
				return this._nameText;
			}
			set
			{
				if (value != this._nameText)
				{
					this._nameText = value;
					base.OnPropertyChangedWithValue<string>(value, "NameText");
				}
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x00049F8B File Offset: 0x0004818B
		// (set) Token: 0x06001303 RID: 4867 RVA: 0x00049F93 File Offset: 0x00048193
		[DataSourceProperty]
		public string CultureText
		{
			get
			{
				return this._cultureText;
			}
			set
			{
				if (value != this._cultureText)
				{
					this._cultureText = value;
					base.OnPropertyChangedWithValue<string>(value, "CultureText");
				}
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001304 RID: 4868 RVA: 0x00049FB6 File Offset: 0x000481B6
		// (set) Token: 0x06001305 RID: 4869 RVA: 0x00049FBE File Offset: 0x000481BE
		[DataSourceProperty]
		public string OwnerText
		{
			get
			{
				return this._ownerText;
			}
			set
			{
				if (value != this._ownerText)
				{
					this._ownerText = value;
					base.OnPropertyChangedWithValue<string>(value, "OwnerText");
				}
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001306 RID: 4870 RVA: 0x00049FE1 File Offset: 0x000481E1
		// (set) Token: 0x06001307 RID: 4871 RVA: 0x00049FE9 File Offset: 0x000481E9
		[DataSourceProperty]
		public bool IsVisualTrackerSelected
		{
			get
			{
				return this._isVisualTrackerSelected;
			}
			set
			{
				if (value != this._isVisualTrackerSelected)
				{
					this._isVisualTrackerSelected = value;
					base.OnPropertyChangedWithValue(value, "IsVisualTrackerSelected");
				}
			}
		}

		// Token: 0x040008AF RID: 2223
		private readonly Settlement _settlement;

		// Token: 0x040008B0 RID: 2224
		private int _settlementType;

		// Token: 0x040008B1 RID: 2225
		private MBBindingList<EncyclopediaHistoryEventVM> _history;

		// Token: 0x040008B2 RID: 2226
		private MBBindingList<EncyclopediaSettlementVM> _settlements;

		// Token: 0x040008B3 RID: 2227
		private EncyclopediaSettlementVM _boundSettlement;

		// Token: 0x040008B4 RID: 2228
		private MBBindingList<HeroVM> _notableCharacters;

		// Token: 0x040008B5 RID: 2229
		private EncyclopediaFactionVM _ownerBanner;

		// Token: 0x040008B6 RID: 2230
		private HintViewModel _showInMapHint;

		// Token: 0x040008B7 RID: 2231
		private BasicTooltipViewModel _militasHint;

		// Token: 0x040008B8 RID: 2232
		private BasicTooltipViewModel _prosperityHint;

		// Token: 0x040008B9 RID: 2233
		private BasicTooltipViewModel _loyaltyHint;

		// Token: 0x040008BA RID: 2234
		private BasicTooltipViewModel _securityHint;

		// Token: 0x040008BB RID: 2235
		private BasicTooltipViewModel _wallsHint;

		// Token: 0x040008BC RID: 2236
		private BasicTooltipViewModel _garrisonHint;

		// Token: 0x040008BD RID: 2237
		private BasicTooltipViewModel _foodHint;

		// Token: 0x040008BE RID: 2238
		private HeroVM _owner;

		// Token: 0x040008BF RID: 2239
		private string _ownerText;

		// Token: 0x040008C0 RID: 2240
		private string _militasText;

		// Token: 0x040008C1 RID: 2241
		private string _garrisonText;

		// Token: 0x040008C2 RID: 2242
		private string _prosperityText;

		// Token: 0x040008C3 RID: 2243
		private string _loyaltyText;

		// Token: 0x040008C4 RID: 2244
		private string _securityText;

		// Token: 0x040008C5 RID: 2245
		private string _wallsText;

		// Token: 0x040008C6 RID: 2246
		private string _foodText;

		// Token: 0x040008C7 RID: 2247
		private string _nameText;

		// Token: 0x040008C8 RID: 2248
		private string _cultureText;

		// Token: 0x040008C9 RID: 2249
		private string _villagesText;

		// Token: 0x040008CA RID: 2250
		private string _notableCharactersText;

		// Token: 0x040008CB RID: 2251
		private string _settlementPath;

		// Token: 0x040008CC RID: 2252
		private string _settlementName;

		// Token: 0x040008CD RID: 2253
		private string _informationText;

		// Token: 0x040008CE RID: 2254
		private string _settlementImageID;

		// Token: 0x040008CF RID: 2255
		private string _boundSettlementText;

		// Token: 0x040008D0 RID: 2256
		private string _trackText;

		// Token: 0x040008D1 RID: 2257
		private double _settlementCropPosition;

		// Token: 0x040008D2 RID: 2258
		private bool _isFortification;

		// Token: 0x040008D3 RID: 2259
		private bool _isVisualTrackerSelected;

		// Token: 0x040008D4 RID: 2260
		private bool _hasBoundSettlement;

		// Token: 0x040008D5 RID: 2261
		private bool _isTrackerButtonHighlightEnabled;

		// Token: 0x02000203 RID: 515
		private enum SettlementTypes
		{
			// Token: 0x040010D0 RID: 4304
			Town,
			// Token: 0x040010D1 RID: 4305
			LoneVillage,
			// Token: 0x040010D2 RID: 4306
			VillageWithCastle
		}
	}
}
