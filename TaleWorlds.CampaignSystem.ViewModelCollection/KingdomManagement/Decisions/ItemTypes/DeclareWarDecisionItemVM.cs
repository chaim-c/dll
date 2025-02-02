using System;
using System.Linq;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Diplomacy;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Decisions.ItemTypes
{
	// Token: 0x0200006D RID: 109
	public class DeclareWarDecisionItemVM : DecisionItemBaseVM
	{
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x00027731 File Offset: 0x00025931
		private Kingdom _sourceFaction
		{
			get
			{
				return Hero.MainHero.Clan.Kingdom;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x00027742 File Offset: 0x00025942
		public IFaction TargetFaction
		{
			get
			{
				return (this._decision as DeclareWarDecision).FactionToDeclareWarOn;
			}
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00027754 File Offset: 0x00025954
		public DeclareWarDecisionItemVM(DeclareWarDecision decision, Action onDecisionOver) : base(decision, onDecisionOver)
		{
			this._declareWarDecision = decision;
			base.DecisionType = 4;
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0002776C File Offset: 0x0002596C
		protected override void InitValues()
		{
			base.InitValues();
			TextObject textObject = GameTexts.FindText("str_kingdom_decision_declare_war", null);
			this.NameText = textObject.ToString();
			TextObject textObject2 = GameTexts.FindText("str_kingdom_decision_declare_war_desc", null);
			textObject2.SetTextVariable("FACTION", this.TargetFaction.Name);
			this.WarDescriptionText = textObject2.ToString();
			this.SourceFactionBanner = new ImageIdentifierVM(BannerCode.CreateFrom(this._sourceFaction.Banner), true);
			this.TargetFactionBanner = new ImageIdentifierVM(BannerCode.CreateFrom(this.TargetFaction.Banner), true);
			this.LeaderText = GameTexts.FindText("str_leader", null).ToString();
			this.SourceFactionLeader = new HeroVM(this._sourceFaction.Leader, false);
			this.TargetFactionLeader = new HeroVM(this.TargetFaction.Leader, false);
			this.ComparedStats = new MBBindingList<KingdomWarComparableStatVM>();
			Kingdom kingdom = this.TargetFaction as Kingdom;
			string faction1Color = Color.FromUint(this._sourceFaction.Color).ToString();
			string faction2Color = Color.FromUint(kingdom.Color).ToString();
			KingdomWarComparableStatVM item = new KingdomWarComparableStatVM((int)this._sourceFaction.TotalStrength, (int)kingdom.TotalStrength, GameTexts.FindText("str_strength", null), faction1Color, faction2Color, 10000, null, null);
			this.ComparedStats.Add(item);
			KingdomWarComparableStatVM item2 = new KingdomWarComparableStatVM(this._sourceFaction.Armies.Count, kingdom.Armies.Count, GameTexts.FindText("str_armies", null), faction1Color, faction2Color, 5, null, null);
			this.ComparedStats.Add(item2);
			int faction1Stat = this._sourceFaction.Settlements.Count((Settlement settlement) => settlement.IsTown);
			int faction2Stat = kingdom.Settlements.Count((Settlement settlement) => settlement.IsTown);
			KingdomWarComparableStatVM item3 = new KingdomWarComparableStatVM(faction1Stat, faction2Stat, GameTexts.FindText("str_towns", null), faction1Color, faction2Color, 50, null, null);
			this.ComparedStats.Add(item3);
			int faction1Stat2 = this._sourceFaction.Settlements.Count((Settlement settlement) => settlement.IsCastle);
			int faction2Stat2 = this.TargetFaction.Settlements.Count((Settlement settlement) => settlement.IsCastle);
			KingdomWarComparableStatVM item4 = new KingdomWarComparableStatVM(faction1Stat2, faction2Stat2, GameTexts.FindText("str_castles", null), faction1Color, faction2Color, 50, null, null);
			this.ComparedStats.Add(item4);
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x00027A1C File Offset: 0x00025C1C
		// (set) Token: 0x06000984 RID: 2436 RVA: 0x00027A24 File Offset: 0x00025C24
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

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x00027A47 File Offset: 0x00025C47
		// (set) Token: 0x06000986 RID: 2438 RVA: 0x00027A4F File Offset: 0x00025C4F
		[DataSourceProperty]
		public string WarDescriptionText
		{
			get
			{
				return this._warDescriptionText;
			}
			set
			{
				if (value != this._warDescriptionText)
				{
					this._warDescriptionText = value;
					base.OnPropertyChangedWithValue<string>(value, "WarDescriptionText");
				}
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x00027A72 File Offset: 0x00025C72
		// (set) Token: 0x06000988 RID: 2440 RVA: 0x00027A7A File Offset: 0x00025C7A
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

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x00027A98 File Offset: 0x00025C98
		// (set) Token: 0x0600098A RID: 2442 RVA: 0x00027AA0 File Offset: 0x00025CA0
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

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x00027ABE File Offset: 0x00025CBE
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x00027AC6 File Offset: 0x00025CC6
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

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x00027AE4 File Offset: 0x00025CE4
		// (set) Token: 0x0600098E RID: 2446 RVA: 0x00027AEC File Offset: 0x00025CEC
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

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x00027B0F File Offset: 0x00025D0F
		// (set) Token: 0x06000990 RID: 2448 RVA: 0x00027B17 File Offset: 0x00025D17
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

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x00027B35 File Offset: 0x00025D35
		// (set) Token: 0x06000992 RID: 2450 RVA: 0x00027B3D File Offset: 0x00025D3D
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

		// Token: 0x04000446 RID: 1094
		private readonly DeclareWarDecision _declareWarDecision;

		// Token: 0x04000447 RID: 1095
		private string _nameText;

		// Token: 0x04000448 RID: 1096
		private string _warDescriptionText;

		// Token: 0x04000449 RID: 1097
		private ImageIdentifierVM _sourceFactionBanner;

		// Token: 0x0400044A RID: 1098
		private ImageIdentifierVM _targetFactionBanner;

		// Token: 0x0400044B RID: 1099
		private string _leaderText;

		// Token: 0x0400044C RID: 1100
		private HeroVM _sourceFactionLeader;

		// Token: 0x0400044D RID: 1101
		private HeroVM _targetFactionLeader;

		// Token: 0x0400044E RID: 1102
		private MBBindingList<KingdomWarComparableStatVM> _comparedStats;
	}
}
