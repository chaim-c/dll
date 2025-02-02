using System;
using System.Linq;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Decisions.ItemTypes
{
	// Token: 0x0200006E RID: 110
	public class ExpelClanDecisionItemVM : DecisionItemBaseVM
	{
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x00027B5C File Offset: 0x00025D5C
		public ExpelClanFromKingdomDecision ExpelDecision
		{
			get
			{
				ExpelClanFromKingdomDecision result;
				if ((result = this._expelDecision) == null)
				{
					result = (this._expelDecision = (this._decision as ExpelClanFromKingdomDecision));
				}
				return result;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x00027B87 File Offset: 0x00025D87
		public Clan Clan
		{
			get
			{
				return this.ExpelDecision.ClanToExpel;
			}
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00027B94 File Offset: 0x00025D94
		public ExpelClanDecisionItemVM(ExpelClanFromKingdomDecision decision, Action onDecisionOver) : base(decision, onDecisionOver)
		{
			base.DecisionType = 2;
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00027BA8 File Offset: 0x00025DA8
		protected override void InitValues()
		{
			base.InitValues();
			base.DecisionType = 2;
			this.Members = new MBBindingList<HeroVM>();
			this.Fiefs = new MBBindingList<EncyclopediaSettlementVM>();
			GameTexts.SetVariable("RENOWN", this.Clan.Renown);
			string variableName = "STR1";
			TextObject encyclopediaText = this.Clan.EncyclopediaText;
			GameTexts.SetVariable(variableName, (encyclopediaText != null) ? encyclopediaText.ToString() : null);
			GameTexts.SetVariable("STR2", GameTexts.FindText("str_encyclopedia_renown", null).ToString());
			this.InformationText = GameTexts.FindText("str_STR1_space_STR2", null).ToString();
			this.Leader = new HeroVM(this.Clan.Leader, false);
			this.LeaderText = GameTexts.FindText("str_leader", null).ToString();
			this.MembersText = GameTexts.FindText("str_members", null).ToString();
			this.SettlementsText = GameTexts.FindText("str_fiefs", null).ToString();
			this.NameText = this.Clan.Name.ToString();
			int num = 0;
			float num2 = 0f;
			EncyclopediaPage pageOf = Campaign.Current.EncyclopediaManager.GetPageOf(typeof(Hero));
			foreach (Hero hero in this.Clan.Heroes)
			{
				if (hero.IsAlive && hero.Age >= 18f && pageOf.IsValidEncyclopediaItem(hero))
				{
					if (hero != this.Leader.Hero)
					{
						this.Members.Add(new HeroVM(hero, false));
					}
					num += hero.Gold;
				}
			}
			foreach (Hero hero2 in this.Clan.Companions)
			{
				if (hero2.IsAlive && hero2.Age >= 18f && pageOf.IsValidEncyclopediaItem(hero2))
				{
					if (hero2 != this.Leader.Hero)
					{
						this.Members.Add(new HeroVM(hero2, false));
					}
					num += hero2.Gold;
				}
			}
			foreach (MobileParty mobileParty in MobileParty.AllLordParties)
			{
				if (mobileParty.ActualClan == this.Clan && !mobileParty.IsDisbanding)
				{
					num2 += mobileParty.Party.TotalStrength;
				}
			}
			this.ProsperityText = num.ToString();
			this.ProsperityHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetClanProsperityTooltip(this.Clan));
			this.StrengthText = num2.ToString();
			this.StrengthHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetClanStrengthTooltip(this.Clan));
			foreach (Town town in from s in this.Clan.Fiefs
			orderby s.IsCastle, s.IsTown
			select s)
			{
				if (town.Settlement.OwnerClan == this.Clan)
				{
					this.Fiefs.Add(new EncyclopediaSettlementVM(town.Settlement));
				}
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x00027F50 File Offset: 0x00026150
		// (set) Token: 0x06000998 RID: 2456 RVA: 0x00027F58 File Offset: 0x00026158
		[DataSourceProperty]
		public MBBindingList<HeroVM> Members
		{
			get
			{
				return this._members;
			}
			set
			{
				if (value != this._members)
				{
					this._members = value;
					base.OnPropertyChangedWithValue<MBBindingList<HeroVM>>(value, "Members");
				}
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x00027F76 File Offset: 0x00026176
		// (set) Token: 0x0600099A RID: 2458 RVA: 0x00027F7E File Offset: 0x0002617E
		[DataSourceProperty]
		public MBBindingList<EncyclopediaSettlementVM> Fiefs
		{
			get
			{
				return this._fiefs;
			}
			set
			{
				if (value != this._fiefs)
				{
					this._fiefs = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaSettlementVM>>(value, "Fiefs");
				}
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x00027F9C File Offset: 0x0002619C
		// (set) Token: 0x0600099C RID: 2460 RVA: 0x00027FA4 File Offset: 0x000261A4
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

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x00027FC2 File Offset: 0x000261C2
		// (set) Token: 0x0600099E RID: 2462 RVA: 0x00027FCA File Offset: 0x000261CA
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

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x00027FED File Offset: 0x000261ED
		// (set) Token: 0x060009A0 RID: 2464 RVA: 0x00027FF5 File Offset: 0x000261F5
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

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x00028018 File Offset: 0x00026218
		// (set) Token: 0x060009A2 RID: 2466 RVA: 0x00028020 File Offset: 0x00026220
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

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x00028043 File Offset: 0x00026243
		// (set) Token: 0x060009A4 RID: 2468 RVA: 0x0002804B File Offset: 0x0002624B
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

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0002806E File Offset: 0x0002626E
		// (set) Token: 0x060009A6 RID: 2470 RVA: 0x00028076 File Offset: 0x00026276
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

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x00028099 File Offset: 0x00026299
		// (set) Token: 0x060009A8 RID: 2472 RVA: 0x000280A1 File Offset: 0x000262A1
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

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x000280C4 File Offset: 0x000262C4
		// (set) Token: 0x060009AA RID: 2474 RVA: 0x000280CC File Offset: 0x000262CC
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

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x000280EF File Offset: 0x000262EF
		// (set) Token: 0x060009AC RID: 2476 RVA: 0x000280F7 File Offset: 0x000262F7
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

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x00028115 File Offset: 0x00026315
		// (set) Token: 0x060009AE RID: 2478 RVA: 0x0002811D File Offset: 0x0002631D
		[DataSourceProperty]
		public BasicTooltipViewModel StrengthHint
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
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "StrengthHint");
				}
			}
		}

		// Token: 0x0400044F RID: 1103
		private ExpelClanFromKingdomDecision _expelDecision;

		// Token: 0x04000450 RID: 1104
		private MBBindingList<HeroVM> _members;

		// Token: 0x04000451 RID: 1105
		private MBBindingList<EncyclopediaSettlementVM> _fiefs;

		// Token: 0x04000452 RID: 1106
		private HeroVM _leader;

		// Token: 0x04000453 RID: 1107
		private string _nameText;

		// Token: 0x04000454 RID: 1108
		private string _membersText;

		// Token: 0x04000455 RID: 1109
		private string _settlementsText;

		// Token: 0x04000456 RID: 1110
		private string _leaderText;

		// Token: 0x04000457 RID: 1111
		private string _informationText;

		// Token: 0x04000458 RID: 1112
		private string _prosperityText;

		// Token: 0x04000459 RID: 1113
		private string _strengthText;

		// Token: 0x0400045A RID: 1114
		private BasicTooltipViewModel _prosperityHint;

		// Token: 0x0400045B RID: 1115
		private BasicTooltipViewModel _strengthHint;
	}
}
