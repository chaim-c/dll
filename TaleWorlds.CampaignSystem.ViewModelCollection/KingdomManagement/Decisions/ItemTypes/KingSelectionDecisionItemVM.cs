using System;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Decisions.ItemTypes
{
	// Token: 0x02000070 RID: 112
	public class KingSelectionDecisionItemVM : DecisionItemBaseVM
	{
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x0002815D File Offset: 0x0002635D
		public IFaction TargetFaction
		{
			get
			{
				return (this._decision as KingSelectionKingdomDecision).Kingdom;
			}
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0002816F File Offset: 0x0002636F
		public KingSelectionDecisionItemVM(KingSelectionKingdomDecision decision, Action onDecisionOver) : base(decision, onDecisionOver)
		{
			this._kingSelectionDecision = decision;
			base.DecisionType = 6;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00028188 File Offset: 0x00026388
		protected override void InitValues()
		{
			base.InitValues();
			TextObject textObject = GameTexts.FindText("str_kingdom_decision_king_selection", null);
			textObject.SetTextVariable("FACTION", this.TargetFaction.Name);
			this.NameText = textObject.ToString();
			this.FactionBanner = new ImageIdentifierVM(BannerCode.CreateFrom(this.TargetFaction.Banner), true);
			this.FactionName = this.TargetFaction.Culture.Name.ToString();
			bool flag = true;
			bool flag2 = true;
			int num = 0;
			int num2 = 0;
			foreach (Settlement settlement in this.TargetFaction.Settlements)
			{
				if (settlement.IsTown)
				{
					if (flag)
					{
						this.SettlementsListText = settlement.EncyclopediaLinkWithName.ToString();
						flag = false;
					}
					else
					{
						GameTexts.SetVariable("LEFT", this.SettlementsListText);
						GameTexts.SetVariable("RIGHT", settlement.EncyclopediaLinkWithName);
						this.SettlementsListText = GameTexts.FindText("str_LEFT_comma_RIGHT", null).ToString();
					}
					num++;
				}
				else if (settlement.IsCastle)
				{
					if (flag2)
					{
						this.CastlesListText = settlement.EncyclopediaLinkWithName.ToString();
						flag2 = false;
					}
					else
					{
						GameTexts.SetVariable("LEFT", this.CastlesListText);
						GameTexts.SetVariable("RIGHT", settlement.EncyclopediaLinkWithName);
						this.CastlesListText = GameTexts.FindText("str_LEFT_comma_RIGHT", null).ToString();
					}
					num2++;
				}
			}
			TextObject variable = GameTexts.FindText("str_settlements", null);
			TextObject textObject2 = GameTexts.FindText("str_STR_in_parentheses", null);
			textObject2.SetTextVariable("STR", num);
			TextObject textObject3 = GameTexts.FindText("str_LEFT_RIGHT", null);
			textObject3.SetTextVariable("LEFT", variable);
			textObject3.SetTextVariable("RIGHT", textObject2);
			this.SettlementsText = textObject3.ToString();
			TextObject variable2 = GameTexts.FindText("str_castles", null);
			TextObject textObject4 = GameTexts.FindText("str_STR_in_parentheses", null);
			textObject4.SetTextVariable("STR", num2);
			TextObject textObject5 = GameTexts.FindText("str_LEFT_RIGHT", null);
			textObject5.SetTextVariable("LEFT", variable2);
			textObject5.SetTextVariable("RIGHT", textObject4);
			this.CastlesText = textObject5.ToString();
			this.TotalStrengthText = GameTexts.FindText("str_total_strength", null).ToString();
			this.TotalStrength = (int)this.TargetFaction.TotalStrength;
			this.ActivePoliciesText = GameTexts.FindText("str_active_policies", null).ToString();
			Kingdom kingdom = this.TargetFaction as Kingdom;
			foreach (PolicyObject policyObject in kingdom.ActivePolicies)
			{
				if (policyObject == kingdom.ActivePolicies[0])
				{
					this.ActivePoliciesListText = policyObject.Name.ToString();
				}
				else
				{
					GameTexts.SetVariable("LEFT", this.ActivePoliciesListText);
					GameTexts.SetVariable("RIGHT", policyObject.Name.ToString());
					this.ActivePoliciesListText = GameTexts.FindText("str_LEFT_comma_RIGHT", null).ToString();
				}
			}
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x000284C0 File Offset: 0x000266C0
		private void ExecuteLocationLink(string link)
		{
			Campaign.Current.EncyclopediaManager.GoToLink(link);
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x000284D2 File Offset: 0x000266D2
		// (set) Token: 0x060009B7 RID: 2487 RVA: 0x000284DA File Offset: 0x000266DA
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

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x000284FD File Offset: 0x000266FD
		// (set) Token: 0x060009B9 RID: 2489 RVA: 0x00028505 File Offset: 0x00026705
		[DataSourceProperty]
		public string FactionName
		{
			get
			{
				return this._factionName;
			}
			set
			{
				if (value != this._factionName)
				{
					this._factionName = value;
					base.OnPropertyChangedWithValue<string>(value, "FactionName");
				}
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x00028528 File Offset: 0x00026728
		// (set) Token: 0x060009BB RID: 2491 RVA: 0x00028530 File Offset: 0x00026730
		[DataSourceProperty]
		public ImageIdentifierVM FactionBanner
		{
			get
			{
				return this._factionBanner;
			}
			set
			{
				if (value != this._factionBanner)
				{
					this._factionBanner = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "FactionBanner");
				}
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x0002854E File Offset: 0x0002674E
		// (set) Token: 0x060009BD RID: 2493 RVA: 0x00028556 File Offset: 0x00026756
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

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x00028579 File Offset: 0x00026779
		// (set) Token: 0x060009BF RID: 2495 RVA: 0x00028581 File Offset: 0x00026781
		[DataSourceProperty]
		public string SettlementsListText
		{
			get
			{
				return this._settlementsListText;
			}
			set
			{
				if (value != this._settlementsListText)
				{
					this._settlementsListText = value;
					base.OnPropertyChangedWithValue<string>(value, "SettlementsListText");
				}
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x000285A4 File Offset: 0x000267A4
		// (set) Token: 0x060009C1 RID: 2497 RVA: 0x000285AC File Offset: 0x000267AC
		[DataSourceProperty]
		public string CastlesText
		{
			get
			{
				return this._castlesText;
			}
			set
			{
				if (value != this._castlesText)
				{
					this._castlesText = value;
					base.OnPropertyChangedWithValue<string>(value, "CastlesText");
				}
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x000285CF File Offset: 0x000267CF
		// (set) Token: 0x060009C3 RID: 2499 RVA: 0x000285D7 File Offset: 0x000267D7
		[DataSourceProperty]
		public string CastlesListText
		{
			get
			{
				return this._castlesListText;
			}
			set
			{
				if (value != this._castlesListText)
				{
					this._castlesListText = value;
					base.OnPropertyChangedWithValue<string>(value, "CastlesListText");
				}
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x000285FA File Offset: 0x000267FA
		// (set) Token: 0x060009C5 RID: 2501 RVA: 0x00028602 File Offset: 0x00026802
		[DataSourceProperty]
		public string TotalStrengthText
		{
			get
			{
				return this._totalStrengthText;
			}
			set
			{
				if (value != this._totalStrengthText)
				{
					this._totalStrengthText = value;
					base.OnPropertyChangedWithValue<string>(value, "TotalStrengthText");
				}
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x00028625 File Offset: 0x00026825
		// (set) Token: 0x060009C7 RID: 2503 RVA: 0x0002862D File Offset: 0x0002682D
		[DataSourceProperty]
		public int TotalStrength
		{
			get
			{
				return this._totalStrength;
			}
			set
			{
				if (value != this._totalStrength)
				{
					this._totalStrength = value;
					base.OnPropertyChangedWithValue(value, "TotalStrength");
				}
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x0002864B File Offset: 0x0002684B
		// (set) Token: 0x060009C9 RID: 2505 RVA: 0x00028653 File Offset: 0x00026853
		[DataSourceProperty]
		public string ActivePoliciesText
		{
			get
			{
				return this._activePoliciesText;
			}
			set
			{
				if (value != this._activePoliciesText)
				{
					this._activePoliciesText = value;
					base.OnPropertyChangedWithValue<string>(value, "ActivePoliciesText");
				}
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x00028676 File Offset: 0x00026876
		// (set) Token: 0x060009CB RID: 2507 RVA: 0x0002867E File Offset: 0x0002687E
		[DataSourceProperty]
		public string ActivePoliciesListText
		{
			get
			{
				return this._activePoliciesListText;
			}
			set
			{
				if (value != this._activePoliciesListText)
				{
					this._activePoliciesListText = value;
					base.OnPropertyChangedWithValue<string>(value, "ActivePoliciesListText");
				}
			}
		}

		// Token: 0x0400045C RID: 1116
		private readonly KingSelectionKingdomDecision _kingSelectionDecision;

		// Token: 0x0400045D RID: 1117
		private string _nameText;

		// Token: 0x0400045E RID: 1118
		private string _factionName;

		// Token: 0x0400045F RID: 1119
		private ImageIdentifierVM _factionBanner;

		// Token: 0x04000460 RID: 1120
		private string _settlementsText;

		// Token: 0x04000461 RID: 1121
		private string _settlementsListText;

		// Token: 0x04000462 RID: 1122
		private string _castlesText;

		// Token: 0x04000463 RID: 1123
		private string _castlesListText;

		// Token: 0x04000464 RID: 1124
		private int _totalStrength;

		// Token: 0x04000465 RID: 1125
		private string _totalStrengthText;

		// Token: 0x04000466 RID: 1126
		private string _activePoliciesText;

		// Token: 0x04000467 RID: 1127
		private string _activePoliciesListText;
	}
}
