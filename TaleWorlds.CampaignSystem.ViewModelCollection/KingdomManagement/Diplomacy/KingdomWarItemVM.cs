using System;
using System.Collections.Generic;
using Helpers;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Diplomacy
{
	// Token: 0x02000065 RID: 101
	public class KingdomWarItemVM : KingdomDiplomacyItemVM
	{
		// Token: 0x060008B5 RID: 2229 RVA: 0x00024D4C File Offset: 0x00022F4C
		public KingdomWarItemVM(StanceLink war, Action<KingdomWarItemVM> onSelect, Action<KingdomWarItemVM> onAction) : base(war.Faction1, war.Faction2)
		{
			this._war = war;
			this._onSelect = onSelect;
			this._onAction = onAction;
			this.IsBehaviorSelectionEnabled = (this.Faction1.IsKingdomFaction && this.Faction1.Leader == Hero.MainHero);
			this._prisonersCapturedByFaction1 = DiplomacyHelper.GetPrisonersOfWarTakenByFaction(this.Faction1, this.Faction2);
			this._prisonersCapturedByFaction2 = DiplomacyHelper.GetPrisonersOfWarTakenByFaction(this.Faction2, this.Faction1);
			this._townsCapturedByFaction1 = DiplomacyHelper.GetSuccessfullSiegesInWarForFaction(this.Faction1, war, (Settlement x) => x.IsTown);
			this._townsCapturedByFaction2 = DiplomacyHelper.GetSuccessfullSiegesInWarForFaction(this.Faction2, war, (Settlement x) => x.IsTown);
			this._castlesCapturedByFaction1 = DiplomacyHelper.GetSuccessfullSiegesInWarForFaction(this.Faction1, war, (Settlement x) => x.IsCastle);
			this._castlesCapturedByFaction2 = DiplomacyHelper.GetSuccessfullSiegesInWarForFaction(this.Faction2, war, (Settlement x) => x.IsCastle);
			this._raidsMadeByFaction1 = DiplomacyHelper.GetRaidsInWar(this.Faction1, war, null);
			this._raidsMadeByFaction2 = DiplomacyHelper.GetRaidsInWar(this.Faction2, war, null);
			this.RefreshValues();
			this.WarLog = new MBBindingList<KingdomWarLogItemVM>();
			foreach (ValueTuple<LogEntry, IFaction, IFaction> valueTuple in DiplomacyHelper.GetLogsForWar(war))
			{
				LogEntry item = valueTuple.Item1;
				IFaction item2 = valueTuple.Item2;
				IEncyclopediaLog log;
				if ((log = (item as IEncyclopediaLog)) != null)
				{
					this.WarLog.Add(new KingdomWarLogItemVM(log, item2));
				}
			}
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00024F3C File Offset: 0x0002313C
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.UpdateDiplomacyProperties();
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00024F4A File Offset: 0x0002314A
		protected override void OnSelect()
		{
			this.UpdateDiplomacyProperties();
			this._onSelect(this);
			base.IsSelected = true;
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00024F68 File Offset: 0x00023168
		protected override void UpdateDiplomacyProperties()
		{
			base.UpdateDiplomacyProperties();
			GameTexts.SetVariable("FACTION_1_NAME", this.Faction1.Name.ToString());
			GameTexts.SetVariable("FACTION_2_NAME", this.Faction2.Name.ToString());
			this.WarName = GameTexts.FindText("str_war_faction_versus_faction", null).ToString();
			StanceLink stanceWith = this.Faction1.GetStanceWith(this.Faction2);
			this.Score = stanceWith.GetSuccessfulSieges(this.Faction1) + stanceWith.GetSuccessfulRaids(this.Faction1);
			this.CasualtiesOfFaction1 = stanceWith.GetCasualties(this.Faction1);
			this.CasualtiesOfFaction2 = stanceWith.GetCasualties(this.Faction2);
			int num = MathF.Ceiling(this._war.WarStartDate.ElapsedDaysUntilNow + 0.01f);
			TextObject textObject = GameTexts.FindText("str_for_DAY_days", null);
			textObject.SetTextVariable("DAY", num.ToString());
			textObject.SetTextVariable("DAY_IS_PLURAL", (num > 1) ? 1 : 0);
			this.NumberOfDaysSinceWarBegan = textObject.ToString();
			base.Stats.Add(new KingdomWarComparableStatVM((int)this.Faction1.TotalStrength, (int)this.Faction2.TotalStrength, GameTexts.FindText("str_total_strength", null), this._faction1Color, this._faction2Color, 10000, null, null));
			base.Stats.Add(new KingdomWarComparableStatVM(stanceWith.GetCasualties(this.Faction2), stanceWith.GetCasualties(this.Faction1), GameTexts.FindText("str_war_casualties_inflicted", null), this._faction1Color, this._faction2Color, 5000, null, null));
			base.Stats.Add(new KingdomWarComparableStatVM(this._prisonersCapturedByFaction1.Count, this._prisonersCapturedByFaction2.Count, GameTexts.FindText("str_party_category_prisoners_tooltip", null), this._faction1Color, this._faction2Color, 10, new BasicTooltipViewModel(() => CampaignUIHelper.GetWarPrisonersTooltip(this._prisonersCapturedByFaction1, this.Faction1.Name)), new BasicTooltipViewModel(() => CampaignUIHelper.GetWarPrisonersTooltip(this._prisonersCapturedByFaction2, this.Faction2.Name))));
			base.Stats.Add(new KingdomWarComparableStatVM(this._faction1Towns.Count, this._faction2Towns.Count, GameTexts.FindText("str_towns", null), this._faction1Color, this._faction2Color, 25, new BasicTooltipViewModel(() => CampaignUIHelper.GetWarSuccessfulSiegesTooltip(this._townsCapturedByFaction1, this.Faction1.Name, true)), new BasicTooltipViewModel(() => CampaignUIHelper.GetWarSuccessfulSiegesTooltip(this._townsCapturedByFaction2, this.Faction2.Name, true))));
			base.Stats.Add(new KingdomWarComparableStatVM(this._faction1Castles.Count, this._faction2Castles.Count, GameTexts.FindText("str_castles", null), this._faction1Color, this._faction2Color, 25, new BasicTooltipViewModel(() => CampaignUIHelper.GetWarSuccessfulSiegesTooltip(this._castlesCapturedByFaction1, this.Faction1.Name, false)), new BasicTooltipViewModel(() => CampaignUIHelper.GetWarSuccessfulSiegesTooltip(this._castlesCapturedByFaction2, this.Faction2.Name, false))));
			base.Stats.Add(new KingdomWarComparableStatVM(stanceWith.GetSuccessfulRaids(this.Faction1), stanceWith.GetSuccessfulRaids(this.Faction2), GameTexts.FindText("str_war_successful_raids", null), this._faction1Color, this._faction2Color, 10, new BasicTooltipViewModel(() => CampaignUIHelper.GetWarSuccessfulRaidsTooltip(this._raidsMadeByFaction1, this.Faction1.Name)), new BasicTooltipViewModel(() => CampaignUIHelper.GetWarSuccessfulRaidsTooltip(this._raidsMadeByFaction2, this.Faction2.Name))));
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0002528A File Offset: 0x0002348A
		protected override void ExecuteAction()
		{
			this._onAction(this);
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x00025298 File Offset: 0x00023498
		// (set) Token: 0x060008BB RID: 2235 RVA: 0x000252A0 File Offset: 0x000234A0
		[DataSourceProperty]
		public string WarName
		{
			get
			{
				return this._warName;
			}
			set
			{
				if (value != this._warName)
				{
					this._warName = value;
					base.OnPropertyChangedWithValue<string>(value, "WarName");
				}
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x000252C3 File Offset: 0x000234C3
		// (set) Token: 0x060008BD RID: 2237 RVA: 0x000252CB File Offset: 0x000234CB
		[DataSourceProperty]
		public string NumberOfDaysSinceWarBegan
		{
			get
			{
				return this._numberOfDaysSinceWarBegan;
			}
			set
			{
				if (value != this._numberOfDaysSinceWarBegan)
				{
					this._numberOfDaysSinceWarBegan = value;
					base.OnPropertyChangedWithValue<string>(value, "NumberOfDaysSinceWarBegan");
				}
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x000252EE File Offset: 0x000234EE
		// (set) Token: 0x060008BF RID: 2239 RVA: 0x000252F6 File Offset: 0x000234F6
		[DataSourceProperty]
		public bool IsBehaviorSelectionEnabled
		{
			get
			{
				return this._isBehaviorSelectionEnabled;
			}
			set
			{
				if (value != this._isBehaviorSelectionEnabled)
				{
					this._isBehaviorSelectionEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsBehaviorSelectionEnabled");
				}
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x00025314 File Offset: 0x00023514
		// (set) Token: 0x060008C1 RID: 2241 RVA: 0x0002531C File Offset: 0x0002351C
		[DataSourceProperty]
		public int Score
		{
			get
			{
				return this._score;
			}
			set
			{
				if (value != this._score)
				{
					this._score = value;
					base.OnPropertyChangedWithValue(value, "Score");
				}
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x0002533A File Offset: 0x0002353A
		// (set) Token: 0x060008C3 RID: 2243 RVA: 0x00025342 File Offset: 0x00023542
		[DataSourceProperty]
		public int CasualtiesOfFaction1
		{
			get
			{
				return this._casualtiesOfFaction1;
			}
			set
			{
				if (value != this._casualtiesOfFaction1)
				{
					this._casualtiesOfFaction1 = value;
					base.OnPropertyChangedWithValue(value, "CasualtiesOfFaction1");
				}
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x00025360 File Offset: 0x00023560
		// (set) Token: 0x060008C5 RID: 2245 RVA: 0x00025368 File Offset: 0x00023568
		[DataSourceProperty]
		public int CasualtiesOfFaction2
		{
			get
			{
				return this._casualtiesOfFaction2;
			}
			set
			{
				if (value != this._casualtiesOfFaction2)
				{
					this._casualtiesOfFaction2 = value;
					base.OnPropertyChangedWithValue(value, "CasualtiesOfFaction2");
				}
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x00025386 File Offset: 0x00023586
		// (set) Token: 0x060008C7 RID: 2247 RVA: 0x0002538E File Offset: 0x0002358E
		[DataSourceProperty]
		public MBBindingList<KingdomWarLogItemVM> WarLog
		{
			get
			{
				return this._warLog;
			}
			set
			{
				if (value != this._warLog)
				{
					this._warLog = value;
					base.OnPropertyChangedWithValue<MBBindingList<KingdomWarLogItemVM>>(value, "WarLog");
				}
			}
		}

		// Token: 0x040003E3 RID: 995
		private readonly Action<KingdomWarItemVM> _onSelect;

		// Token: 0x040003E4 RID: 996
		private readonly StanceLink _war;

		// Token: 0x040003E5 RID: 997
		private readonly Action<KingdomWarItemVM> _onAction;

		// Token: 0x040003E6 RID: 998
		private List<Hero> _prisonersCapturedByFaction1;

		// Token: 0x040003E7 RID: 999
		private List<Hero> _prisonersCapturedByFaction2;

		// Token: 0x040003E8 RID: 1000
		private List<Settlement> _townsCapturedByFaction1;

		// Token: 0x040003E9 RID: 1001
		private List<Settlement> _townsCapturedByFaction2;

		// Token: 0x040003EA RID: 1002
		private List<Settlement> _castlesCapturedByFaction1;

		// Token: 0x040003EB RID: 1003
		private List<Settlement> _castlesCapturedByFaction2;

		// Token: 0x040003EC RID: 1004
		private List<Settlement> _raidsMadeByFaction1;

		// Token: 0x040003ED RID: 1005
		private List<Settlement> _raidsMadeByFaction2;

		// Token: 0x040003EE RID: 1006
		private string _warName;

		// Token: 0x040003EF RID: 1007
		private string _numberOfDaysSinceWarBegan;

		// Token: 0x040003F0 RID: 1008
		private int _score;

		// Token: 0x040003F1 RID: 1009
		private bool _isBehaviorSelectionEnabled;

		// Token: 0x040003F2 RID: 1010
		private int _casualtiesOfFaction1;

		// Token: 0x040003F3 RID: 1011
		private int _casualtiesOfFaction2;

		// Token: 0x040003F4 RID: 1012
		private MBBindingList<KingdomWarLogItemVM> _warLog;
	}
}
