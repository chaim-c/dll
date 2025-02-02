using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages
{
	// Token: 0x020000B8 RID: 184
	[EncyclopediaViewModel(typeof(Kingdom))]
	public class EncyclopediaFactionPageVM : EncyclopediaContentPageVM
	{
		// Token: 0x06001218 RID: 4632 RVA: 0x000473E0 File Offset: 0x000455E0
		public EncyclopediaFactionPageVM(EncyclopediaPageArgs args) : base(args)
		{
			this._faction = (base.Obj as Kingdom);
			this.Clans = new MBBindingList<EncyclopediaFactionVM>();
			this.Enemies = new MBBindingList<EncyclopediaFactionVM>();
			this.Settlements = new MBBindingList<EncyclopediaSettlementVM>();
			this.History = new MBBindingList<EncyclopediaHistoryEventVM>();
			base.IsBookmarked = Campaign.Current.EncyclopediaManager.ViewDataTracker.IsEncyclopediaBookmarked(this._faction);
			this.RefreshValues();
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x00047458 File Offset: 0x00045658
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.StrengthHint = new HintViewModel(GameTexts.FindText("str_strength", null), null);
			this.ProsperityHint = new HintViewModel(GameTexts.FindText("str_prosperity", null), null);
			this.MembersText = GameTexts.FindText("str_members", null).ToString();
			this.ClansText = new TextObject("{=bfQLwMUp}Clans", null).ToString();
			this.EnemiesText = new TextObject("{=zZlWRZjO}Wars", null).ToString();
			this.SettlementsText = new TextObject("{=LBNzsqyb}Fiefs", null).ToString();
			this.VillagesText = GameTexts.FindText("str_villages", null).ToString();
			TextObject encyclopediaText = this._faction.EncyclopediaText;
			this.InformationText = (((encyclopediaText != null) ? encyclopediaText.ToString() : null) ?? string.Empty);
			base.UpdateBookmarkHintText();
			this.Refresh();
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x0004753C File Offset: 0x0004573C
		public override void Refresh()
		{
			base.IsLoadingOver = false;
			this.Clans.Clear();
			this.Enemies.Clear();
			this.Settlements.Clear();
			this.History.Clear();
			this.Leader = new HeroVM(this._faction.Leader, false);
			this.LeaderText = GameTexts.FindText("str_leader", null).ToString();
			this.NameText = this._faction.Name.ToString();
			this.DescriptorText = GameTexts.FindText("str_kingdom_faction", null).ToString();
			int num = 0;
			float num2 = 0f;
			EncyclopediaPage pageOf = Campaign.Current.EncyclopediaManager.GetPageOf(typeof(Hero));
			foreach (Hero hero in this._faction.Lords)
			{
				if (pageOf.IsValidEncyclopediaItem(hero))
				{
					num += hero.Gold;
				}
			}
			this.Banner = new ImageIdentifierVM(BannerCode.CreateFrom(this._faction.Banner), true);
			foreach (MobileParty mobileParty in MobileParty.AllLordParties)
			{
				if (mobileParty.MapFaction == this._faction && !mobileParty.IsDisbanding)
				{
					num2 += mobileParty.Party.TotalStrength;
				}
			}
			this.ProsperityText = num.ToString();
			this.StrengthText = num2.ToString();
			for (int i = Campaign.Current.LogEntryHistory.GameActionLogs.Count - 1; i >= 0; i--)
			{
				IEncyclopediaLog encyclopediaLog;
				if ((encyclopediaLog = (Campaign.Current.LogEntryHistory.GameActionLogs[i] as IEncyclopediaLog)) != null && encyclopediaLog.IsVisibleInEncyclopediaPageOf<Kingdom>(this._faction))
				{
					this.History.Add(new EncyclopediaHistoryEventVM(encyclopediaLog));
				}
			}
			EncyclopediaPage pageOf2 = Campaign.Current.EncyclopediaManager.GetPageOf(typeof(Clan));
			using (IEnumerator<IFaction> enumerator3 = (from x in Campaign.Current.Factions
			orderby !x.IsKingdomFaction
			select x).ThenBy((IFaction f) => f.Name.ToString()).GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					IFaction factionObject = enumerator3.Current;
					if (pageOf2.IsValidEncyclopediaItem(factionObject) && factionObject != this._faction && !factionObject.IsBanditFaction && FactionManager.IsAtWarAgainstFaction(this._faction, factionObject.MapFaction) && !this.Enemies.Any((EncyclopediaFactionVM x) => x.Faction == factionObject.MapFaction))
					{
						this.Enemies.Add(new EncyclopediaFactionVM(factionObject.MapFaction));
					}
				}
			}
			foreach (Clan faction in from c in Campaign.Current.Clans
			where c.Kingdom == this._faction
			select c)
			{
				this.Clans.Add(new EncyclopediaFactionVM(faction));
			}
			EncyclopediaPage pageOf3 = Campaign.Current.EncyclopediaManager.GetPageOf(typeof(Settlement));
			foreach (Settlement settlement in from s in Settlement.All
			where s.IsTown || s.IsCastle
			orderby s.IsCastle, s.IsTown
			select s)
			{
				if ((settlement.MapFaction == this._faction || (settlement.OwnerClan == this._faction.RulingClan && settlement.OwnerClan.Leader != null)) && pageOf3.IsValidEncyclopediaItem(settlement))
				{
					this.Settlements.Add(new EncyclopediaSettlementVM(settlement));
				}
			}
			base.IsLoadingOver = true;
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x000479F8 File Offset: 0x00045BF8
		public override string GetName()
		{
			return this._faction.Name.ToString();
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x00047A0C File Offset: 0x00045C0C
		public override string GetNavigationBarURL()
		{
			return HyperlinkTexts.GetGenericHyperlinkText("Home", GameTexts.FindText("str_encyclopedia_home", null).ToString()) + " \\ " + HyperlinkTexts.GetGenericHyperlinkText("ListPage-Kingdoms", GameTexts.FindText("str_encyclopedia_kingdoms", null).ToString()) + " \\ " + this.GetName();
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x00047A74 File Offset: 0x00045C74
		public override void ExecuteSwitchBookmarkedState()
		{
			base.ExecuteSwitchBookmarkedState();
			if (base.IsBookmarked)
			{
				Campaign.Current.EncyclopediaManager.ViewDataTracker.AddEncyclopediaBookmarkToItem(this._faction);
				return;
			}
			Campaign.Current.EncyclopediaManager.ViewDataTracker.RemoveEncyclopediaBookmarkFromItem(this._faction);
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x0600121E RID: 4638 RVA: 0x00047AC4 File Offset: 0x00045CC4
		// (set) Token: 0x0600121F RID: 4639 RVA: 0x00047ACC File Offset: 0x00045CCC
		[DataSourceProperty]
		public MBBindingList<EncyclopediaFactionVM> Clans
		{
			get
			{
				return this._clans;
			}
			set
			{
				if (value != this._clans)
				{
					this._clans = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaFactionVM>>(value, "Clans");
				}
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001220 RID: 4640 RVA: 0x00047AEA File Offset: 0x00045CEA
		// (set) Token: 0x06001221 RID: 4641 RVA: 0x00047AF2 File Offset: 0x00045CF2
		[DataSourceProperty]
		public MBBindingList<EncyclopediaFactionVM> Enemies
		{
			get
			{
				return this._enemies;
			}
			set
			{
				if (value != this._enemies)
				{
					this._enemies = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaFactionVM>>(value, "Enemies");
				}
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x00047B10 File Offset: 0x00045D10
		// (set) Token: 0x06001223 RID: 4643 RVA: 0x00047B18 File Offset: 0x00045D18
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
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaSettlementVM>>(value, "Settlements");
				}
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x00047B36 File Offset: 0x00045D36
		// (set) Token: 0x06001225 RID: 4645 RVA: 0x00047B3E File Offset: 0x00045D3E
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

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001226 RID: 4646 RVA: 0x00047B5C File Offset: 0x00045D5C
		// (set) Token: 0x06001227 RID: 4647 RVA: 0x00047B64 File Offset: 0x00045D64
		[DataSourceProperty]
		public HeroVM Leader
		{
			get
			{
				return this._leader;
			}
			set
			{
				if (value != this._leader)
				{
					this._leader = value;
					base.OnPropertyChangedWithValue<HeroVM>(value, "Leader");
				}
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001228 RID: 4648 RVA: 0x00047B82 File Offset: 0x00045D82
		// (set) Token: 0x06001229 RID: 4649 RVA: 0x00047B8A File Offset: 0x00045D8A
		[DataSourceProperty]
		public ImageIdentifierVM Banner
		{
			get
			{
				return this._banner;
			}
			set
			{
				if (value != this._banner)
				{
					this._banner = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "Banner");
				}
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x00047BA8 File Offset: 0x00045DA8
		// (set) Token: 0x0600122B RID: 4651 RVA: 0x00047BB0 File Offset: 0x00045DB0
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

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x00047BD3 File Offset: 0x00045DD3
		// (set) Token: 0x0600122D RID: 4653 RVA: 0x00047BDB File Offset: 0x00045DDB
		[DataSourceProperty]
		public string MembersText
		{
			get
			{
				return this._membersText;
			}
			set
			{
				if (value != this._membersText)
				{
					this._membersText = value;
					base.OnPropertyChangedWithValue<string>(value, "MembersText");
				}
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x00047BFE File Offset: 0x00045DFE
		// (set) Token: 0x0600122F RID: 4655 RVA: 0x00047C06 File Offset: 0x00045E06
		[DataSourceProperty]
		public string EnemiesText
		{
			get
			{
				return this._enemiesText;
			}
			set
			{
				if (value != this._enemiesText)
				{
					this._enemiesText = value;
					base.OnPropertyChangedWithValue<string>(value, "EnemiesText");
				}
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001230 RID: 4656 RVA: 0x00047C29 File Offset: 0x00045E29
		// (set) Token: 0x06001231 RID: 4657 RVA: 0x00047C31 File Offset: 0x00045E31
		[DataSourceProperty]
		public string ClansText
		{
			get
			{
				return this._clansText;
			}
			set
			{
				if (value != this._clansText)
				{
					this._clansText = value;
					base.OnPropertyChangedWithValue<string>(value, "ClansText");
				}
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x00047C54 File Offset: 0x00045E54
		// (set) Token: 0x06001233 RID: 4659 RVA: 0x00047C5C File Offset: 0x00045E5C
		[DataSourceProperty]
		public string SettlementsText
		{
			get
			{
				return this._settlementsText;
			}
			set
			{
				if (value != this._settlementsText)
				{
					this._settlementsText = value;
					base.OnPropertyChangedWithValue<string>(value, "SettlementsText");
				}
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x00047C7F File Offset: 0x00045E7F
		// (set) Token: 0x06001235 RID: 4661 RVA: 0x00047C87 File Offset: 0x00045E87
		[DataSourceProperty]
		public string VillagesText
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
					base.OnPropertyChangedWithValue<string>(value, "VillagesText");
				}
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001236 RID: 4662 RVA: 0x00047CAA File Offset: 0x00045EAA
		// (set) Token: 0x06001237 RID: 4663 RVA: 0x00047CB2 File Offset: 0x00045EB2
		[DataSourceProperty]
		public string LeaderText
		{
			get
			{
				return this._leaderText;
			}
			set
			{
				if (value != this._leaderText)
				{
					this._leaderText = value;
					base.OnPropertyChangedWithValue<string>(value, "LeaderText");
				}
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001238 RID: 4664 RVA: 0x00047CD5 File Offset: 0x00045ED5
		// (set) Token: 0x06001239 RID: 4665 RVA: 0x00047CDD File Offset: 0x00045EDD
		[DataSourceProperty]
		public string DescriptorText
		{
			get
			{
				return this._descriptorText;
			}
			set
			{
				if (value != this._descriptorText)
				{
					this._descriptorText = value;
					base.OnPropertyChangedWithValue<string>(value, "DescriptorText");
				}
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x0600123A RID: 4666 RVA: 0x00047D00 File Offset: 0x00045F00
		// (set) Token: 0x0600123B RID: 4667 RVA: 0x00047D08 File Offset: 0x00045F08
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

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x0600123C RID: 4668 RVA: 0x00047D2B File Offset: 0x00045F2B
		// (set) Token: 0x0600123D RID: 4669 RVA: 0x00047D33 File Offset: 0x00045F33
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

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x0600123E RID: 4670 RVA: 0x00047D56 File Offset: 0x00045F56
		// (set) Token: 0x0600123F RID: 4671 RVA: 0x00047D5E File Offset: 0x00045F5E
		[DataSourceProperty]
		public string StrengthText
		{
			get
			{
				return this._strengthText;
			}
			set
			{
				if (value != this._strengthText)
				{
					this._strengthText = value;
					base.OnPropertyChangedWithValue<string>(value, "StrengthText");
				}
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x00047D81 File Offset: 0x00045F81
		// (set) Token: 0x06001241 RID: 4673 RVA: 0x00047D89 File Offset: 0x00045F89
		[DataSourceProperty]
		public HintViewModel ProsperityHint
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
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ProsperityHint");
				}
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x00047DA7 File Offset: 0x00045FA7
		// (set) Token: 0x06001243 RID: 4675 RVA: 0x00047DAF File Offset: 0x00045FAF
		[DataSourceProperty]
		public HintViewModel StrengthHint
		{
			get
			{
				return this._strengthHint;
			}
			set
			{
				if (value != this._strengthHint)
				{
					this._strengthHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "StrengthHint");
				}
			}
		}

		// Token: 0x04000869 RID: 2153
		private Kingdom _faction;

		// Token: 0x0400086A RID: 2154
		private MBBindingList<EncyclopediaFactionVM> _clans;

		// Token: 0x0400086B RID: 2155
		private MBBindingList<EncyclopediaFactionVM> _enemies;

		// Token: 0x0400086C RID: 2156
		private MBBindingList<EncyclopediaSettlementVM> _settlements;

		// Token: 0x0400086D RID: 2157
		private MBBindingList<EncyclopediaHistoryEventVM> _history;

		// Token: 0x0400086E RID: 2158
		private HeroVM _leader;

		// Token: 0x0400086F RID: 2159
		private ImageIdentifierVM _banner;

		// Token: 0x04000870 RID: 2160
		private string _membersText;

		// Token: 0x04000871 RID: 2161
		private string _enemiesText;

		// Token: 0x04000872 RID: 2162
		private string _clansText;

		// Token: 0x04000873 RID: 2163
		private string _settlementsText;

		// Token: 0x04000874 RID: 2164
		private string _villagesText;

		// Token: 0x04000875 RID: 2165
		private string _leaderText;

		// Token: 0x04000876 RID: 2166
		private string _descriptorText;

		// Token: 0x04000877 RID: 2167
		private string _prosperityText;

		// Token: 0x04000878 RID: 2168
		private string _strengthText;

		// Token: 0x04000879 RID: 2169
		private string _informationText;

		// Token: 0x0400087A RID: 2170
		private HintViewModel _prosperityHint;

		// Token: 0x0400087B RID: 2171
		private HintViewModel _strengthHint;

		// Token: 0x0400087C RID: 2172
		private string _nameText;
	}
}
