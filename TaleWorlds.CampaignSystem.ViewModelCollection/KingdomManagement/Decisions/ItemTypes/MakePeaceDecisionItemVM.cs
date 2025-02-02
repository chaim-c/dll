using System;
using System.Linq;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Diplomacy;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Decisions.ItemTypes
{
	// Token: 0x02000071 RID: 113
	public class MakePeaceDecisionItemVM : DecisionItemBaseVM
	{
		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x000286A1 File Offset: 0x000268A1
		private Kingdom _sourceFaction
		{
			get
			{
				return Hero.MainHero.Clan.Kingdom;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x000286B2 File Offset: 0x000268B2
		public IFaction TargetFaction
		{
			get
			{
				return (this._decision as MakePeaceKingdomDecision).FactionToMakePeaceWith;
			}
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x000286C4 File Offset: 0x000268C4
		public MakePeaceDecisionItemVM(MakePeaceKingdomDecision decision, Action onDecisionOver) : base(decision, onDecisionOver)
		{
			this._makePeaceDecision = decision;
			base.DecisionType = 5;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x000286DC File Offset: 0x000268DC
		protected override void InitValues()
		{
			base.InitValues();
			TextObject textObject = GameTexts.FindText("str_kingdom_decision_make_peace", null);
			this.NameText = textObject.ToString();
			TextObject textObject2 = GameTexts.FindText("str_kingdom_decision_make_peace_desc", null);
			textObject2.SetTextVariable("FACTION", this.TargetFaction.Name);
			this.PeaceDescriptionText = textObject2.ToString();
			this.SourceFactionBanner = new ImageIdentifierVM(BannerCode.CreateFrom(this._sourceFaction.Banner), true);
			this.TargetFactionBanner = new ImageIdentifierVM(BannerCode.CreateFrom(this.TargetFaction.Banner), true);
			this.LeaderText = GameTexts.FindText("str_leader", null).ToString();
			this.SourceFactionLeader = new HeroVM(this._sourceFaction.Leader, false);
			this.TargetFactionLeader = new HeroVM(this.TargetFaction.Leader, false);
			this.ComparedStats = new MBBindingList<KingdomWarComparableStatVM>();
			Kingdom targetFaction = this.TargetFaction as Kingdom;
			string faction1Color = Color.FromUint(this._sourceFaction.Color).ToString();
			string faction2Color = Color.FromUint(targetFaction.Color).ToString();
			StanceLink stanceLink = this._sourceFaction.Stances.First((StanceLink s) => s.IsAtWar && (s.Faction2 == this.TargetFaction || s.Faction1 == this.TargetFaction));
			KingdomWarComparableStatVM item = new KingdomWarComparableStatVM((int)this._sourceFaction.TotalStrength, (int)targetFaction.TotalStrength, GameTexts.FindText("str_strength", null), faction1Color, faction2Color, 10000, null, null);
			this.ComparedStats.Add(item);
			int faction1Stat = targetFaction.Heroes.Count(delegate(Hero x)
			{
				if (x.IsPrisoner)
				{
					PartyBase partyBelongedToAsPrisoner = x.PartyBelongedToAsPrisoner;
					return ((partyBelongedToAsPrisoner != null) ? partyBelongedToAsPrisoner.MapFaction : null) == this._sourceFaction;
				}
				return false;
			});
			int faction2Stat = this._sourceFaction.Heroes.Count(delegate(Hero x)
			{
				if (x.IsPrisoner)
				{
					PartyBase partyBelongedToAsPrisoner = x.PartyBelongedToAsPrisoner;
					return ((partyBelongedToAsPrisoner != null) ? partyBelongedToAsPrisoner.MapFaction : null) == targetFaction;
				}
				return false;
			});
			KingdomWarComparableStatVM item2 = new KingdomWarComparableStatVM(faction1Stat, faction2Stat, GameTexts.FindText("str_party_category_prisoners_tooltip", null), faction1Color, faction2Color, 10, null, null);
			this.ComparedStats.Add(item2);
			KingdomWarComparableStatVM item3 = new KingdomWarComparableStatVM(stanceLink.GetCasualties(targetFaction), stanceLink.GetCasualties(this._sourceFaction), GameTexts.FindText("str_war_casualties_inflicted", null), faction1Color, faction2Color, 5000, null, null);
			this.ComparedStats.Add(item3);
			KingdomWarComparableStatVM item4 = new KingdomWarComparableStatVM(stanceLink.GetSuccessfulSieges(this._sourceFaction), stanceLink.GetSuccessfulSieges(targetFaction), GameTexts.FindText("str_war_successful_sieges", null), faction1Color, faction2Color, 5, null, null);
			this.ComparedStats.Add(item4);
			KingdomWarComparableStatVM item5 = new KingdomWarComparableStatVM(stanceLink.GetSuccessfulRaids(this._sourceFaction), stanceLink.GetSuccessfulRaids(targetFaction), GameTexts.FindText("str_war_successful_raids", null), faction1Color, faction2Color, 10, null, null);
			this.ComparedStats.Add(item5);
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x0002899F File Offset: 0x00026B9F
		// (set) Token: 0x060009D1 RID: 2513 RVA: 0x000289A7 File Offset: 0x00026BA7
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

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x000289CA File Offset: 0x00026BCA
		// (set) Token: 0x060009D3 RID: 2515 RVA: 0x000289D2 File Offset: 0x00026BD2
		[DataSourceProperty]
		public string PeaceDescriptionText
		{
			get
			{
				return this._peaceDescriptionText;
			}
			set
			{
				if (value != this._peaceDescriptionText)
				{
					this._peaceDescriptionText = value;
					base.OnPropertyChangedWithValue<string>(value, "PeaceDescriptionText");
				}
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x000289F5 File Offset: 0x00026BF5
		// (set) Token: 0x060009D5 RID: 2517 RVA: 0x000289FD File Offset: 0x00026BFD
		[DataSourceProperty]
		public ImageIdentifierVM SourceFactionBanner
		{
			get
			{
				return this._sourceFactionBanner;
			}
			set
			{
				if (value != this._sourceFactionBanner)
				{
					this._sourceFactionBanner = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "SourceFactionBanner");
				}
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x00028A1B File Offset: 0x00026C1B
		// (set) Token: 0x060009D7 RID: 2519 RVA: 0x00028A23 File Offset: 0x00026C23
		[DataSourceProperty]
		public ImageIdentifierVM TargetFactionBanner
		{
			get
			{
				return this._targetFactionBanner;
			}
			set
			{
				if (value != this._targetFactionBanner)
				{
					this._targetFactionBanner = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "TargetFactionBanner");
				}
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x00028A41 File Offset: 0x00026C41
		// (set) Token: 0x060009D9 RID: 2521 RVA: 0x00028A49 File Offset: 0x00026C49
		[DataSourceProperty]
		public MBBindingList<KingdomWarComparableStatVM> ComparedStats
		{
			get
			{
				return this._comparedStats;
			}
			set
			{
				if (value != this._comparedStats)
				{
					this._comparedStats = value;
					base.OnPropertyChangedWithValue<MBBindingList<KingdomWarComparableStatVM>>(value, "ComparedStats");
				}
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x00028A67 File Offset: 0x00026C67
		// (set) Token: 0x060009DB RID: 2523 RVA: 0x00028A6F File Offset: 0x00026C6F
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

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x00028A92 File Offset: 0x00026C92
		// (set) Token: 0x060009DD RID: 2525 RVA: 0x00028A9A File Offset: 0x00026C9A
		[DataSourceProperty]
		public HeroVM SourceFactionLeader
		{
			get
			{
				return this._sourceFactionLeader;
			}
			set
			{
				if (value != this._sourceFactionLeader)
				{
					this._sourceFactionLeader = value;
					base.OnPropertyChangedWithValue<HeroVM>(value, "SourceFactionLeader");
				}
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x00028AB8 File Offset: 0x00026CB8
		// (set) Token: 0x060009DF RID: 2527 RVA: 0x00028AC0 File Offset: 0x00026CC0
		[DataSourceProperty]
		public HeroVM TargetFactionLeader
		{
			get
			{
				return this._targetFactionLeader;
			}
			set
			{
				if (value != this._targetFactionLeader)
				{
					this._targetFactionLeader = value;
					base.OnPropertyChangedWithValue<HeroVM>(value, "TargetFactionLeader");
				}
			}
		}

		// Token: 0x04000468 RID: 1128
		private readonly MakePeaceKingdomDecision _makePeaceDecision;

		// Token: 0x04000469 RID: 1129
		private string _nameText;

		// Token: 0x0400046A RID: 1130
		private string _peaceDescriptionText;

		// Token: 0x0400046B RID: 1131
		private ImageIdentifierVM _sourceFactionBanner;

		// Token: 0x0400046C RID: 1132
		private ImageIdentifierVM _targetFactionBanner;

		// Token: 0x0400046D RID: 1133
		private string _leaderText;

		// Token: 0x0400046E RID: 1134
		private HeroVM _sourceFactionLeader;

		// Token: 0x0400046F RID: 1135
		private HeroVM _targetFactionLeader;

		// Token: 0x04000470 RID: 1136
		private MBBindingList<KingdomWarComparableStatVM> _comparedStats;
	}
}
