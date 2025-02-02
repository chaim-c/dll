using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Diplomacy
{
	// Token: 0x02000060 RID: 96
	public abstract class KingdomDiplomacyItemVM : KingdomItemVM
	{
		// Token: 0x06000832 RID: 2098 RVA: 0x00022FD0 File Offset: 0x000211D0
		protected KingdomDiplomacyItemVM(IFaction faction1, IFaction faction2)
		{
			this._playerKingdom = Hero.MainHero.MapFaction;
			if (faction1 == this._playerKingdom || faction2 == this._playerKingdom)
			{
				this.Faction1 = this._playerKingdom;
				this.Faction2 = ((faction1 != this._playerKingdom) ? faction1 : faction2);
			}
			else
			{
				this.Faction1 = faction1;
				this.Faction2 = faction2;
			}
			this._faction1Color = Color.FromUint(this.Faction1.Color).ToString();
			this._faction2Color = Color.FromUint(this.Faction2.Color).ToString();
			this.Stats = new MBBindingList<KingdomWarComparableStatVM>();
			this.PopulateSettlements();
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00023090 File Offset: 0x00021290
		protected virtual void UpdateDiplomacyProperties()
		{
			this.Stats.Clear();
			this.Faction1Visual = new ImageIdentifierVM(BannerCode.CreateFrom(this.Faction1.Banner), true);
			this.Faction2Visual = new ImageIdentifierVM(BannerCode.CreateFrom(this.Faction2.Banner), true);
			int dailyTributePaid = this._playerKingdom.GetStanceWith(this.Faction2).GetDailyTributePaid(this._playerKingdom);
			TextObject textObject = new TextObject("{=SDhQWonF}Paying {DENAR}{GOLD_ICON} as tribute per day.", null);
			textObject.SetTextVariable("DENAR", MathF.Abs(dailyTributePaid));
			this.Faction1TributeText = ((dailyTributePaid > 0) ? textObject.ToString() : string.Empty);
			this.Faction2TributeText = ((dailyTributePaid < 0) ? textObject.ToString() : string.Empty);
			this.Faction1Name = this.Faction1.Name.ToString();
			this.Faction2Name = this.Faction2.Name.ToString();
			TextObject textObject2 = new TextObject("{=OyyJSyIX}{FACTION_1} is paying {DENAR}{GOLD_ICON} as tribute to {FACTION_2}", null);
			TextObject textObject3 = textObject2.CopyTextObject();
			this.Faction1TributeHint = ((dailyTributePaid > 0) ? new HintViewModel(textObject2.SetTextVariable("DENAR", MathF.Abs(dailyTributePaid)).SetTextVariable("FACTION_1", this.Faction1Name).SetTextVariable("FACTION_2", this.Faction2Name), null) : new HintViewModel());
			this.Faction2TributeHint = ((dailyTributePaid < 0) ? new HintViewModel(textObject3.SetTextVariable("DENAR", MathF.Abs(dailyTributePaid)).SetTextVariable("FACTION_1", this.Faction2Name).SetTextVariable("FACTION_2", this.Faction1Name), null) : new HintViewModel());
			this.Faction1Leader = new HeroVM(this.Faction1.Leader, false);
			this.Faction2Leader = new HeroVM(this.Faction2.Leader, false);
			this.Faction1OwnedClans = new MBBindingList<KingdomDiplomacyFactionItemVM>();
			if (this.Faction1.IsKingdomFaction)
			{
				foreach (Clan faction in (this.Faction1 as Kingdom).Clans)
				{
					this.Faction1OwnedClans.Add(new KingdomDiplomacyFactionItemVM(faction));
				}
			}
			this.Faction2OwnedClans = new MBBindingList<KingdomDiplomacyFactionItemVM>();
			if (this.Faction2.IsKingdomFaction)
			{
				foreach (Clan faction2 in (this.Faction2 as Kingdom).Clans)
				{
					this.Faction2OwnedClans.Add(new KingdomDiplomacyFactionItemVM(faction2));
				}
			}
			this.Faction2OtherWars = new MBBindingList<KingdomDiplomacyFactionItemVM>();
			foreach (StanceLink stanceLink in this.Faction2.Stances)
			{
				if (stanceLink.IsAtWar && stanceLink.Faction1 != this.Faction1 && stanceLink.Faction2 != this.Faction1 && (stanceLink.Faction1.IsKingdomFaction || stanceLink.Faction1.Leader == Hero.MainHero) && (stanceLink.Faction2.IsKingdomFaction || stanceLink.Faction2.Leader == Hero.MainHero) && !stanceLink.Faction1.IsRebelClan && !stanceLink.Faction2.IsRebelClan && !stanceLink.Faction1.IsBanditFaction && !stanceLink.Faction2.IsBanditFaction)
				{
					this.Faction2OtherWars.Add(new KingdomDiplomacyFactionItemVM((stanceLink.Faction1 == this.Faction2) ? stanceLink.Faction2 : stanceLink.Faction1));
				}
			}
			this.IsFaction2OtherWarsVisible = (this.Faction2OtherWars.Count > 0);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00023470 File Offset: 0x00021670
		private void PopulateSettlements()
		{
			this._faction1Towns = new List<Settlement>();
			this._faction1Castles = new List<Settlement>();
			this._faction2Towns = new List<Settlement>();
			this._faction2Castles = new List<Settlement>();
			foreach (Settlement settlement in this.Faction1.Settlements)
			{
				if (settlement.IsTown)
				{
					this._faction1Towns.Add(settlement);
				}
				else if (settlement.IsCastle)
				{
					this._faction1Castles.Add(settlement);
				}
			}
			foreach (Settlement settlement2 in this.Faction2.Settlements)
			{
				if (settlement2.IsTown)
				{
					this._faction2Towns.Add(settlement2);
				}
				else if (settlement2.IsCastle)
				{
					this._faction2Castles.Add(settlement2);
				}
			}
		}

		// Token: 0x06000835 RID: 2101
		protected abstract void ExecuteAction();

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x00023584 File Offset: 0x00021784
		// (set) Token: 0x06000837 RID: 2103 RVA: 0x0002358C File Offset: 0x0002178C
		[DataSourceProperty]
		public MBBindingList<KingdomDiplomacyFactionItemVM> Faction1OwnedClans
		{
			get
			{
				return this._faction1OwnedClans;
			}
			set
			{
				if (value != this._faction1OwnedClans)
				{
					this._faction1OwnedClans = value;
					base.OnPropertyChangedWithValue<MBBindingList<KingdomDiplomacyFactionItemVM>>(value, "Faction1OwnedClans");
				}
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x000235AA File Offset: 0x000217AA
		// (set) Token: 0x06000839 RID: 2105 RVA: 0x000235B2 File Offset: 0x000217B2
		[DataSourceProperty]
		public MBBindingList<KingdomDiplomacyFactionItemVM> Faction2OwnedClans
		{
			get
			{
				return this._faction2OwnedClans;
			}
			set
			{
				if (value != this._faction2OwnedClans)
				{
					this._faction2OwnedClans = value;
					base.OnPropertyChangedWithValue<MBBindingList<KingdomDiplomacyFactionItemVM>>(value, "Faction2OwnedClans");
				}
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x000235D0 File Offset: 0x000217D0
		// (set) Token: 0x0600083B RID: 2107 RVA: 0x000235D8 File Offset: 0x000217D8
		[DataSourceProperty]
		public MBBindingList<KingdomDiplomacyFactionItemVM> Faction2OtherWars
		{
			get
			{
				return this._faction2OtherWars;
			}
			set
			{
				if (value != this._faction2OtherWars)
				{
					this._faction2OtherWars = value;
					base.OnPropertyChangedWithValue<MBBindingList<KingdomDiplomacyFactionItemVM>>(value, "Faction2OtherWars");
				}
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x000235F6 File Offset: 0x000217F6
		// (set) Token: 0x0600083D RID: 2109 RVA: 0x000235FE File Offset: 0x000217FE
		[DataSourceProperty]
		public MBBindingList<KingdomWarComparableStatVM> Stats
		{
			get
			{
				return this._stats;
			}
			set
			{
				if (value != this._stats)
				{
					this._stats = value;
					base.OnPropertyChangedWithValue<MBBindingList<KingdomWarComparableStatVM>>(value, "Stats");
				}
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x0002361C File Offset: 0x0002181C
		// (set) Token: 0x0600083F RID: 2111 RVA: 0x00023624 File Offset: 0x00021824
		[DataSourceProperty]
		public ImageIdentifierVM Faction1Visual
		{
			get
			{
				return this._faction1Visual;
			}
			set
			{
				if (value != this._faction1Visual)
				{
					this._faction1Visual = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "Faction1Visual");
				}
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x00023642 File Offset: 0x00021842
		// (set) Token: 0x06000841 RID: 2113 RVA: 0x0002364A File Offset: 0x0002184A
		[DataSourceProperty]
		public ImageIdentifierVM Faction2Visual
		{
			get
			{
				return this._faction2Visual;
			}
			set
			{
				if (value != this._faction2Visual)
				{
					this._faction2Visual = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "Faction2Visual");
				}
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x00023668 File Offset: 0x00021868
		// (set) Token: 0x06000843 RID: 2115 RVA: 0x00023670 File Offset: 0x00021870
		[DataSourceProperty]
		public string Faction1Name
		{
			get
			{
				return this._faction1Name;
			}
			set
			{
				if (value != this._faction1Name)
				{
					this._faction1Name = value;
					base.OnPropertyChangedWithValue<string>(value, "Faction1Name");
				}
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x00023693 File Offset: 0x00021893
		// (set) Token: 0x06000845 RID: 2117 RVA: 0x0002369B File Offset: 0x0002189B
		[DataSourceProperty]
		public string Faction2Name
		{
			get
			{
				return this._faction2Name;
			}
			set
			{
				if (value != this._faction2Name)
				{
					this._faction2Name = value;
					base.OnPropertyChangedWithValue<string>(value, "Faction2Name");
				}
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x000236BE File Offset: 0x000218BE
		// (set) Token: 0x06000847 RID: 2119 RVA: 0x000236C6 File Offset: 0x000218C6
		[DataSourceProperty]
		public string Faction1TributeText
		{
			get
			{
				return this._faction1TributeText;
			}
			set
			{
				if (value != this._faction1TributeText)
				{
					this._faction1TributeText = value;
					base.OnPropertyChangedWithValue<string>(value, "Faction1TributeText");
				}
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x000236E9 File Offset: 0x000218E9
		// (set) Token: 0x06000849 RID: 2121 RVA: 0x000236F1 File Offset: 0x000218F1
		[DataSourceProperty]
		public string Faction2TributeText
		{
			get
			{
				return this._faction2TributeText;
			}
			set
			{
				if (value != this._faction2TributeText)
				{
					this._faction2TributeText = value;
					base.OnPropertyChangedWithValue<string>(value, "Faction2TributeText");
				}
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x00023714 File Offset: 0x00021914
		// (set) Token: 0x0600084B RID: 2123 RVA: 0x0002371C File Offset: 0x0002191C
		[DataSourceProperty]
		public HintViewModel Faction1TributeHint
		{
			get
			{
				return this._faction1TributeHint;
			}
			set
			{
				if (value != this._faction1TributeHint)
				{
					this._faction1TributeHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "Faction1TributeHint");
				}
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x0002373A File Offset: 0x0002193A
		// (set) Token: 0x0600084D RID: 2125 RVA: 0x00023742 File Offset: 0x00021942
		[DataSourceProperty]
		public HintViewModel Faction2TributeHint
		{
			get
			{
				return this._faction2TributeHint;
			}
			set
			{
				if (value != this._faction2TributeHint)
				{
					this._faction2TributeHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "Faction2TributeHint");
				}
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x00023760 File Offset: 0x00021960
		// (set) Token: 0x0600084F RID: 2127 RVA: 0x00023768 File Offset: 0x00021968
		[DataSourceProperty]
		public bool IsFaction2OtherWarsVisible
		{
			get
			{
				return this._isFaction2OtherWarsVisible;
			}
			set
			{
				if (value != this._isFaction2OtherWarsVisible)
				{
					this._isFaction2OtherWarsVisible = value;
					base.OnPropertyChangedWithValue(value, "IsFaction2OtherWarsVisible");
				}
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x00023786 File Offset: 0x00021986
		// (set) Token: 0x06000851 RID: 2129 RVA: 0x0002378E File Offset: 0x0002198E
		[DataSourceProperty]
		public HeroVM Faction1Leader
		{
			get
			{
				return this._faction1Leader;
			}
			set
			{
				if (value != this._faction1Leader)
				{
					this._faction1Leader = value;
					base.OnPropertyChangedWithValue<HeroVM>(value, "Faction1Leader");
				}
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x000237AC File Offset: 0x000219AC
		// (set) Token: 0x06000853 RID: 2131 RVA: 0x000237B4 File Offset: 0x000219B4
		[DataSourceProperty]
		public HeroVM Faction2Leader
		{
			get
			{
				return this._faction2Leader;
			}
			set
			{
				if (value != this._faction2Leader)
				{
					this._faction2Leader = value;
					base.OnPropertyChangedWithValue<HeroVM>(value, "Faction2Leader");
				}
			}
		}

		// Token: 0x040003A1 RID: 929
		public readonly IFaction Faction1;

		// Token: 0x040003A2 RID: 930
		public readonly IFaction Faction2;

		// Token: 0x040003A3 RID: 931
		protected readonly string _faction1Color;

		// Token: 0x040003A4 RID: 932
		protected readonly string _faction2Color;

		// Token: 0x040003A5 RID: 933
		protected readonly IFaction _playerKingdom;

		// Token: 0x040003A6 RID: 934
		protected List<Settlement> _faction1Towns;

		// Token: 0x040003A7 RID: 935
		protected List<Settlement> _faction2Towns;

		// Token: 0x040003A8 RID: 936
		protected List<Settlement> _faction1Castles;

		// Token: 0x040003A9 RID: 937
		protected List<Settlement> _faction2Castles;

		// Token: 0x040003AA RID: 938
		private MBBindingList<KingdomWarComparableStatVM> _stats;

		// Token: 0x040003AB RID: 939
		private ImageIdentifierVM _faction1Visual;

		// Token: 0x040003AC RID: 940
		private ImageIdentifierVM _faction2Visual;

		// Token: 0x040003AD RID: 941
		private HeroVM _faction1Leader;

		// Token: 0x040003AE RID: 942
		private HeroVM _faction2Leader;

		// Token: 0x040003AF RID: 943
		private string _faction1Name;

		// Token: 0x040003B0 RID: 944
		private string _faction2Name;

		// Token: 0x040003B1 RID: 945
		private string _faction1TributeText;

		// Token: 0x040003B2 RID: 946
		private string _faction2TributeText;

		// Token: 0x040003B3 RID: 947
		private HintViewModel _faction1TributeHint;

		// Token: 0x040003B4 RID: 948
		private HintViewModel _faction2TributeHint;

		// Token: 0x040003B5 RID: 949
		private bool _isFaction2OtherWarsVisible;

		// Token: 0x040003B6 RID: 950
		private MBBindingList<KingdomDiplomacyFactionItemVM> _faction1OwnedClans;

		// Token: 0x040003B7 RID: 951
		private MBBindingList<KingdomDiplomacyFactionItemVM> _faction2OwnedClans;

		// Token: 0x040003B8 RID: 952
		private MBBindingList<KingdomDiplomacyFactionItemVM> _faction2OtherWars;
	}
}
