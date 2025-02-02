using System;
using System.Linq;
using TaleWorlds.CampaignSystem.BarterSystem.Barterables;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Core.ViewModelCollection.Selector;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Diplomacy
{
	// Token: 0x02000062 RID: 98
	public class KingdomDiplomacyVM : KingdomCategoryVM
	{
		// Token: 0x06000859 RID: 2137 RVA: 0x00023850 File Offset: 0x00021A50
		public KingdomDiplomacyVM(Action<KingdomDecision> forceDecision)
		{
			this._forceDecision = forceDecision;
			this._playerKingdom = (Hero.MainHero.MapFaction as Kingdom);
			this.PlayerWars = new MBBindingList<KingdomWarItemVM>();
			this.PlayerTruces = new MBBindingList<KingdomTruceItemVM>();
			this.WarsSortController = new KingdomWarSortControllerVM(ref this._playerWars);
			this.ActionHint = new HintViewModel();
			this.ExecuteShowStatComparisons();
			this.RefreshValues();
			this.SetDefaultSelectedItem();
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x000238C4 File Offset: 0x00021AC4
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.BehaviorSelection = new SelectorVM<SelectorItemVM>(0, new Action<SelectorVM<SelectorItemVM>>(this.OnBehaviorSelectionChanged));
			this.BehaviorSelection.AddItem(new SelectorItemVM(GameTexts.FindText("str_kingdom_war_strategy_balanced", null), GameTexts.FindText("str_kingdom_war_strategy_balanced_desc", null)));
			this.BehaviorSelection.AddItem(new SelectorItemVM(GameTexts.FindText("str_kingdom_war_strategy_defensive", null), GameTexts.FindText("str_kingdom_war_strategy_defensive_desc", null)));
			this.BehaviorSelection.AddItem(new SelectorItemVM(GameTexts.FindText("str_kingdom_war_strategy_offensive", null), GameTexts.FindText("str_kingdom_war_strategy_offensive_desc", null)));
			this.RefreshDiplomacyList();
			Kingdom kingdom = Clan.PlayerClan.Kingdom;
			int notificationCount;
			if (kingdom == null)
			{
				notificationCount = 0;
			}
			else
			{
				notificationCount = kingdom.UnresolvedDecisions.Count((KingdomDecision d) => !d.ShouldBeCancelled());
			}
			base.NotificationCount = notificationCount;
			this.BehaviorSelectionTitle = GameTexts.FindText("str_kingdom_war_strategy", null).ToString();
			base.NoItemSelectedText = GameTexts.FindText("str_kingdom_no_war_selected", null).ToString();
			this.PlayerWarsText = GameTexts.FindText("str_kingdom_at_war", null).ToString();
			this.PlayerTrucesText = GameTexts.FindText("str_kingdom_at_peace", null).ToString();
			this.WarsText = GameTexts.FindText("str_diplomatic_group", null).ToString();
			this.ShowStatBarsHint = new HintViewModel(GameTexts.FindText("str_kingdom_war_show_comparison_bars", null), null);
			this.ShowWarLogsHint = new HintViewModel(GameTexts.FindText("str_kingdom_war_show_war_logs", null), null);
			this.PlayerWars.ApplyActionOnAllItems(delegate(KingdomWarItemVM x)
			{
				x.RefreshValues();
			});
			this.PlayerTruces.ApplyActionOnAllItems(delegate(KingdomTruceItemVM x)
			{
				x.RefreshValues();
			});
			KingdomDiplomacyItemVM currentSelectedDiplomacyItem = this.CurrentSelectedDiplomacyItem;
			if (currentSelectedDiplomacyItem == null)
			{
				return;
			}
			currentSelectedDiplomacyItem.RefreshValues();
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00023AA8 File Offset: 0x00021CA8
		public void RefreshDiplomacyList()
		{
			this.PlayerWars.Clear();
			this.PlayerTruces.Clear();
			foreach (StanceLink stanceLink in from x in this._playerKingdom.Stances
			where x.IsAtWar
			select x into w
			orderby w.Faction1.Name.ToString() + w.Faction2.Name.ToString()
			select w)
			{
				if (stanceLink.Faction1.IsKingdomFaction && stanceLink.Faction2.IsKingdomFaction)
				{
					this.PlayerWars.Add(new KingdomWarItemVM(stanceLink, new Action<KingdomWarItemVM>(this.OnDiplomacyItemSelection), new Action<KingdomWarItemVM>(this.OnDeclarePeace)));
				}
			}
			foreach (Kingdom kingdom in Kingdom.All)
			{
				if (kingdom != this._playerKingdom && !kingdom.IsEliminated && (FactionManager.IsAlliedWithFaction(kingdom, this._playerKingdom) || FactionManager.IsNeutralWithFaction(kingdom, this._playerKingdom)))
				{
					this.PlayerTruces.Add(new KingdomTruceItemVM(this._playerKingdom, kingdom, new Action<KingdomDiplomacyItemVM>(this.OnDiplomacyItemSelection), new Action<KingdomTruceItemVM>(this.OnDeclareWar)));
				}
			}
			GameTexts.SetVariable("STR", this.PlayerWars.Count);
			this.NumOfPlayerWarsText = GameTexts.FindText("str_STR_in_parentheses", null).ToString();
			GameTexts.SetVariable("STR", this.PlayerTruces.Count);
			this.NumOfPlayerTrucesText = GameTexts.FindText("str_STR_in_parentheses", null).ToString();
			this.SetDefaultSelectedItem();
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00023C88 File Offset: 0x00021E88
		public void SelectKingdom(Kingdom kingdom)
		{
			bool flag = false;
			foreach (KingdomWarItemVM kingdomWarItemVM in this.PlayerWars)
			{
				if (kingdomWarItemVM.Faction1 == kingdom || kingdomWarItemVM.Faction2 == kingdom)
				{
					this.OnSetCurrentDiplomacyItem(kingdomWarItemVM);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				foreach (KingdomTruceItemVM kingdomTruceItemVM in this.PlayerTruces)
				{
					if (kingdomTruceItemVM.Faction1 == kingdom || kingdomTruceItemVM.Faction2 == kingdom)
					{
						this.OnSetCurrentDiplomacyItem(kingdomTruceItemVM);
						flag = true;
						break;
					}
				}
			}
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00023D48 File Offset: 0x00021F48
		private void OnSetCurrentDiplomacyItem(KingdomDiplomacyItemVM item)
		{
			if (item is KingdomWarItemVM)
			{
				this.OnSetWarItem(item as KingdomWarItemVM);
			}
			else if (item is KingdomTruceItemVM)
			{
				this.OnSetPeaceItem(item as KingdomTruceItemVM);
			}
			this.RefreshCurrentWarVisuals(item);
			this.UpdateBehaviorSelection();
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00023D84 File Offset: 0x00021F84
		private void OnSetWarItem(KingdomWarItemVM item)
		{
			this._currentItemsUnresolvedDecision = Clan.PlayerClan.Kingdom.UnresolvedDecisions.FirstOrDefault(delegate(KingdomDecision d)
			{
				MakePeaceKingdomDecision makePeaceKingdomDecision2;
				return (makePeaceKingdomDecision2 = (d as MakePeaceKingdomDecision)) != null && makePeaceKingdomDecision2.FactionToMakePeaceWith == item.Faction2 && !d.ShouldBeCancelled();
			});
			if (this._currentItemsUnresolvedDecision != null)
			{
				MakePeaceKingdomDecision makePeaceKingdomDecision = this._currentItemsUnresolvedDecision as MakePeaceKingdomDecision;
				this._dailyPeaceTributeToPay = ((makePeaceKingdomDecision != null) ? makePeaceKingdomDecision.DailyTributeToBePaid : 0);
				this.ActionName = GameTexts.FindText("str_resolve", null).ToString();
				this.ActionInfluenceCost = 0;
				TextObject hintText;
				this.IsActionEnabled = this.GetActionStatusForDiplomacyItemWithReason(item, true, out hintText);
				this.ActionHint.HintText = hintText;
				this.ProposeActionExplanationText = GameTexts.FindText("str_resolve_explanation", null).ToString();
				return;
			}
			this.ActionName = ((this._playerKingdom.Clans.Count > 1) ? GameTexts.FindText("str_policy_propose", null).ToString() : GameTexts.FindText("str_policy_enact", null).ToString());
			this.ActionInfluenceCost = Campaign.Current.Models.DiplomacyModel.GetInfluenceCostOfProposingPeace(Clan.PlayerClan);
			PeaceBarterable peaceBarterable = new PeaceBarterable(this._playerKingdom.Leader, this._playerKingdom, item.Faction2, CampaignTime.Years(1f));
			int num = -peaceBarterable.GetValueForFaction(item.Faction2);
			if (item.Faction2 is Kingdom)
			{
				foreach (Clan faction in ((Kingdom)item.Faction2).Clans)
				{
					int num2 = -peaceBarterable.GetValueForFaction(faction);
					if (num2 > num)
					{
						num = num2;
					}
				}
			}
			int num3 = num;
			if (num3 > -5000 && num3 < 5000)
			{
				num3 = 0;
			}
			this._dailyPeaceTributeToPay = Campaign.Current.Models.DiplomacyModel.GetDailyTributeForValue(num3);
			this._dailyPeaceTributeToPay = 10 * (this._dailyPeaceTributeToPay / 10);
			TextObject hintText2;
			this.IsActionEnabled = this.GetActionStatusForDiplomacyItemWithReason(item, false, out hintText2);
			this.ActionHint.HintText = hintText2;
			TextObject textObject = (this._dailyPeaceTributeToPay == 0) ? GameTexts.FindText("str_propose_peace_explanation", null) : ((this._dailyPeaceTributeToPay > 0) ? GameTexts.FindText("str_propose_peace_explanation_pay_tribute", null) : GameTexts.FindText("str_propose_peace_explanation_get_tribute", null));
			this.ProposeActionExplanationText = textObject.SetTextVariable("SUPPORT", this.CalculatePeaceSupport(item, this._dailyPeaceTributeToPay)).SetTextVariable("TRIBUTE", MathF.Abs(this._dailyPeaceTributeToPay)).ToString();
			Kingdom kingdom = Clan.PlayerClan.Kingdom;
			base.NotificationCount = ((kingdom != null) ? kingdom.UnresolvedDecisions.Count : 0);
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0002404C File Offset: 0x0002224C
		private void OnSetPeaceItem(KingdomTruceItemVM item)
		{
			this._currentItemsUnresolvedDecision = Clan.PlayerClan.Kingdom.UnresolvedDecisions.FirstOrDefault(delegate(KingdomDecision d)
			{
				DeclareWarDecision declareWarDecision;
				return (declareWarDecision = (d as DeclareWarDecision)) != null && declareWarDecision.FactionToDeclareWarOn == item.Faction2 && !d.ShouldBeCancelled();
			});
			if (this._currentItemsUnresolvedDecision != null)
			{
				this.ActionName = GameTexts.FindText("str_resolve", null).ToString();
				this.ActionInfluenceCost = 0;
				TextObject hintText;
				this.IsActionEnabled = this.GetActionStatusForDiplomacyItemWithReason(item, true, out hintText);
				this.ActionHint.HintText = hintText;
				this.ProposeActionExplanationText = GameTexts.FindText("str_resolve_explanation", null).ToString();
				return;
			}
			this.ActionName = ((this._playerKingdom.Clans.Count > 1) ? GameTexts.FindText("str_policy_propose", null).ToString() : GameTexts.FindText("str_policy_enact", null).ToString());
			this.ActionInfluenceCost = Campaign.Current.Models.DiplomacyModel.GetInfluenceCostOfProposingWar(Clan.PlayerClan);
			TextObject hintText2;
			this.IsActionEnabled = this.GetActionStatusForDiplomacyItemWithReason(item, false, out hintText2);
			this.ActionHint.HintText = hintText2;
			this.ProposeActionExplanationText = GameTexts.FindText("str_propose_war_explanation", null).SetTextVariable("SUPPORT", KingdomDiplomacyVM.CalculateWarSupport(item.Faction2)).ToString();
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00024194 File Offset: 0x00022394
		private bool GetActionStatusForDiplomacyItemWithReason(KingdomDiplomacyItemVM item, bool isResolve, out TextObject disabledReason)
		{
			TextObject textObject;
			if (!CampaignUIHelper.GetMapScreenActionIsEnabledWithReason(out textObject))
			{
				disabledReason = textObject;
				return false;
			}
			if (!isResolve && Clan.PlayerClan.Influence < (float)this.ActionInfluenceCost)
			{
				disabledReason = GameTexts.FindText("str_warning_you_dont_have_enough_influence", null);
				return false;
			}
			if (Clan.PlayerClan.IsUnderMercenaryService)
			{
				disabledReason = GameTexts.FindText("str_cannot_propose_war_truce_while_mercenary", null);
				return false;
			}
			KingdomTruceItemVM kingdomTruceItemVM;
			TextObject textObject3;
			if ((kingdomTruceItemVM = (item as KingdomTruceItemVM)) != null)
			{
				TextObject textObject2;
				if (!Campaign.Current.Models.KingdomDecisionPermissionModel.IsWarDecisionAllowedBetweenKingdoms(kingdomTruceItemVM.Faction1 as Kingdom, kingdomTruceItemVM.Faction2 as Kingdom, out textObject2))
				{
					disabledReason = textObject2;
					return false;
				}
			}
			else if (item is KingdomWarItemVM && !Campaign.Current.Models.KingdomDecisionPermissionModel.IsPeaceDecisionAllowedBetweenKingdoms(item.Faction1 as Kingdom, item.Faction2 as Kingdom, out textObject3))
			{
				disabledReason = textObject3;
				return false;
			}
			disabledReason = TextObject.Empty;
			return true;
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0002426F File Offset: 0x0002246F
		private void RefreshCurrentWarVisuals(KingdomDiplomacyItemVM item)
		{
			if (item != null)
			{
				if (this.CurrentSelectedDiplomacyItem != null)
				{
					this.CurrentSelectedDiplomacyItem.IsSelected = false;
				}
				this.CurrentSelectedDiplomacyItem = item;
				if (this.CurrentSelectedDiplomacyItem != null)
				{
					this.CurrentSelectedDiplomacyItem.IsSelected = true;
				}
			}
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x000242A3 File Offset: 0x000224A3
		private void OnDiplomacyItemSelection(KingdomDiplomacyItemVM item)
		{
			if (this.CurrentSelectedDiplomacyItem != item)
			{
				if (this.CurrentSelectedDiplomacyItem != null)
				{
					this.CurrentSelectedDiplomacyItem.IsSelected = false;
				}
				this.CurrentSelectedDiplomacyItem = item;
				base.IsAcceptableItemSelected = (item != null);
				this.OnSetCurrentDiplomacyItem(item);
			}
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x000242DC File Offset: 0x000224DC
		private void OnDeclareWar(KingdomTruceItemVM item)
		{
			if (this._currentItemsUnresolvedDecision != null)
			{
				this._forceDecision(this._currentItemsUnresolvedDecision);
				return;
			}
			DeclareWarDecision declareWarDecision = new DeclareWarDecision(Clan.PlayerClan, item.Faction2);
			Clan.PlayerClan.Kingdom.AddDecision(declareWarDecision, false);
			this._forceDecision(declareWarDecision);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00024334 File Offset: 0x00022534
		private void OnDeclarePeace(KingdomWarItemVM item)
		{
			if (this._currentItemsUnresolvedDecision != null)
			{
				this._forceDecision(this._currentItemsUnresolvedDecision);
				return;
			}
			MakePeaceKingdomDecision makePeaceKingdomDecision = new MakePeaceKingdomDecision(Clan.PlayerClan, item.Faction2 as Kingdom, this._dailyPeaceTributeToPay, true);
			Clan.PlayerClan.Kingdom.AddDecision(makePeaceKingdomDecision, false);
			this._forceDecision(makePeaceKingdomDecision);
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00024398 File Offset: 0x00022598
		private void ExecuteAction()
		{
			if (this.CurrentSelectedDiplomacyItem != null)
			{
				if (this.CurrentSelectedDiplomacyItem is KingdomWarItemVM)
				{
					this.OnDeclarePeace(this.CurrentSelectedDiplomacyItem as KingdomWarItemVM);
					return;
				}
				if (this.CurrentSelectedDiplomacyItem is KingdomTruceItemVM)
				{
					this.OnDeclareWar(this.CurrentSelectedDiplomacyItem as KingdomTruceItemVM);
				}
			}
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x000243EA File Offset: 0x000225EA
		private void ExecuteShowWarLogs()
		{
			this.IsDisplayingWarLogs = true;
			this.IsDisplayingStatComparisons = false;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x000243FA File Offset: 0x000225FA
		private void ExecuteShowStatComparisons()
		{
			this.IsDisplayingWarLogs = false;
			this.IsDisplayingStatComparisons = true;
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0002440C File Offset: 0x0002260C
		private void SetDefaultSelectedItem()
		{
			KingdomDiplomacyItemVM kingdomDiplomacyItemVM = this.PlayerWars.FirstOrDefault<KingdomWarItemVM>();
			KingdomDiplomacyItemVM kingdomDiplomacyItemVM2 = this.PlayerTruces.FirstOrDefault<KingdomTruceItemVM>();
			this.OnDiplomacyItemSelection(kingdomDiplomacyItemVM ?? kingdomDiplomacyItemVM2);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00024440 File Offset: 0x00022640
		private void UpdateBehaviorSelection()
		{
			if (Hero.MainHero.MapFaction.IsKingdomFaction && Hero.MainHero.MapFaction.Leader == Hero.MainHero && this.CurrentSelectedDiplomacyItem != null)
			{
				StanceLink stanceWith = Hero.MainHero.MapFaction.GetStanceWith(this.CurrentSelectedDiplomacyItem.Faction2);
				this.BehaviorSelection.SelectedIndex = stanceWith.BehaviorPriority;
			}
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x000244A8 File Offset: 0x000226A8
		private void OnBehaviorSelectionChanged(SelectorVM<SelectorItemVM> s)
		{
			if (!this._isChangingDiplomacyItem && Hero.MainHero.MapFaction.IsKingdomFaction && Hero.MainHero.MapFaction.Leader == Hero.MainHero && this.CurrentSelectedDiplomacyItem != null)
			{
				Hero.MainHero.MapFaction.GetStanceWith(this.CurrentSelectedDiplomacyItem.Faction2).BehaviorPriority = s.SelectedIndex;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x00024511 File Offset: 0x00022711
		// (set) Token: 0x0600086C RID: 2156 RVA: 0x00024519 File Offset: 0x00022719
		[DataSourceProperty]
		public MBBindingList<KingdomWarItemVM> PlayerWars
		{
			get
			{
				return this._playerWars;
			}
			set
			{
				if (value != this._playerWars)
				{
					this._playerWars = value;
					base.OnPropertyChangedWithValue<MBBindingList<KingdomWarItemVM>>(value, "PlayerWars");
				}
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x00024537 File Offset: 0x00022737
		// (set) Token: 0x0600086E RID: 2158 RVA: 0x0002453F File Offset: 0x0002273F
		[DataSourceProperty]
		public int ActionInfluenceCost
		{
			get
			{
				return this._actionInfluenceCost;
			}
			set
			{
				if (value != this._actionInfluenceCost)
				{
					this._actionInfluenceCost = value;
					base.OnPropertyChangedWithValue(value, "ActionInfluenceCost");
				}
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x0002455D File Offset: 0x0002275D
		// (set) Token: 0x06000870 RID: 2160 RVA: 0x00024565 File Offset: 0x00022765
		[DataSourceProperty]
		public bool IsActionEnabled
		{
			get
			{
				return this._isActionEnabled;
			}
			set
			{
				if (value != this._isActionEnabled)
				{
					this._isActionEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsActionEnabled");
				}
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x00024583 File Offset: 0x00022783
		// (set) Token: 0x06000872 RID: 2162 RVA: 0x0002458B File Offset: 0x0002278B
		[DataSourceProperty]
		public bool IsDisplayingWarLogs
		{
			get
			{
				return this._isDisplayingWarLogs;
			}
			set
			{
				if (value != this._isDisplayingWarLogs)
				{
					this._isDisplayingWarLogs = value;
					base.OnPropertyChangedWithValue(value, "IsDisplayingWarLogs");
				}
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x000245A9 File Offset: 0x000227A9
		// (set) Token: 0x06000874 RID: 2164 RVA: 0x000245B1 File Offset: 0x000227B1
		[DataSourceProperty]
		public bool IsDisplayingStatComparisons
		{
			get
			{
				return this._isDisplayingStatComparisons;
			}
			set
			{
				if (value != this._isDisplayingStatComparisons)
				{
					this._isDisplayingStatComparisons = value;
					base.OnPropertyChangedWithValue(value, "IsDisplayingStatComparisons");
				}
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x000245CF File Offset: 0x000227CF
		// (set) Token: 0x06000876 RID: 2166 RVA: 0x000245D7 File Offset: 0x000227D7
		[DataSourceProperty]
		public bool IsWar
		{
			get
			{
				return this._isWar;
			}
			set
			{
				if (value != this._isWar)
				{
					this._isWar = value;
					if (!value)
					{
						this.ExecuteShowStatComparisons();
					}
					base.OnPropertyChangedWithValue(value, "IsWar");
				}
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x000245FE File Offset: 0x000227FE
		// (set) Token: 0x06000878 RID: 2168 RVA: 0x00024606 File Offset: 0x00022806
		[DataSourceProperty]
		public string ActionName
		{
			get
			{
				return this._actionName;
			}
			set
			{
				if (value != this._actionName)
				{
					this._actionName = value;
					base.OnPropertyChangedWithValue<string>(value, "ActionName");
				}
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x00024629 File Offset: 0x00022829
		// (set) Token: 0x0600087A RID: 2170 RVA: 0x00024631 File Offset: 0x00022831
		[DataSourceProperty]
		public string ProposeActionExplanationText
		{
			get
			{
				return this._proposeActionExplanationText;
			}
			set
			{
				if (value != this._proposeActionExplanationText)
				{
					this._proposeActionExplanationText = value;
					base.OnPropertyChangedWithValue<string>(value, "ProposeActionExplanationText");
				}
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x00024654 File Offset: 0x00022854
		// (set) Token: 0x0600087C RID: 2172 RVA: 0x0002465C File Offset: 0x0002285C
		[DataSourceProperty]
		public string BehaviorSelectionTitle
		{
			get
			{
				return this._behaviorSelectionTitle;
			}
			set
			{
				if (value != this._behaviorSelectionTitle)
				{
					this._behaviorSelectionTitle = value;
					base.OnPropertyChangedWithValue<string>(value, "BehaviorSelectionTitle");
				}
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x0002467F File Offset: 0x0002287F
		// (set) Token: 0x0600087E RID: 2174 RVA: 0x00024687 File Offset: 0x00022887
		[DataSourceProperty]
		public MBBindingList<KingdomTruceItemVM> PlayerTruces
		{
			get
			{
				return this._playerTruces;
			}
			set
			{
				if (value != this._playerTruces)
				{
					this._playerTruces = value;
					base.OnPropertyChangedWithValue<MBBindingList<KingdomTruceItemVM>>(value, "PlayerTruces");
				}
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x000246A5 File Offset: 0x000228A5
		// (set) Token: 0x06000880 RID: 2176 RVA: 0x000246AD File Offset: 0x000228AD
		[DataSourceProperty]
		public KingdomDiplomacyItemVM CurrentSelectedDiplomacyItem
		{
			get
			{
				return this._currentSelectedItem;
			}
			set
			{
				if (value != this._currentSelectedItem)
				{
					this._isChangingDiplomacyItem = true;
					this._currentSelectedItem = value;
					this.IsWar = (value is KingdomWarItemVM);
					base.OnPropertyChangedWithValue<KingdomDiplomacyItemVM>(value, "CurrentSelectedDiplomacyItem");
					this._isChangingDiplomacyItem = false;
				}
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x000246E8 File Offset: 0x000228E8
		// (set) Token: 0x06000882 RID: 2178 RVA: 0x000246F0 File Offset: 0x000228F0
		[DataSourceProperty]
		public KingdomWarSortControllerVM WarsSortController
		{
			get
			{
				return this._warsSortController;
			}
			set
			{
				if (value != this._warsSortController)
				{
					this._warsSortController = value;
					base.OnPropertyChangedWithValue<KingdomWarSortControllerVM>(value, "WarsSortController");
				}
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x0002470E File Offset: 0x0002290E
		// (set) Token: 0x06000884 RID: 2180 RVA: 0x00024716 File Offset: 0x00022916
		[DataSourceProperty]
		public string PlayerWarsText
		{
			get
			{
				return this._playerWarsText;
			}
			set
			{
				if (value != this._playerWarsText)
				{
					this._playerWarsText = value;
					base.OnPropertyChangedWithValue<string>(value, "PlayerWarsText");
				}
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x00024739 File Offset: 0x00022939
		// (set) Token: 0x06000886 RID: 2182 RVA: 0x00024741 File Offset: 0x00022941
		[DataSourceProperty]
		public string WarsText
		{
			get
			{
				return this._warsText;
			}
			set
			{
				if (value != this._warsText)
				{
					this._warsText = value;
					base.OnPropertyChangedWithValue<string>(value, "WarsText");
				}
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x00024764 File Offset: 0x00022964
		// (set) Token: 0x06000888 RID: 2184 RVA: 0x0002476C File Offset: 0x0002296C
		[DataSourceProperty]
		public string NumOfPlayerWarsText
		{
			get
			{
				return this._numOfPlayerWarsText;
			}
			set
			{
				if (value != this._numOfPlayerWarsText)
				{
					this._numOfPlayerWarsText = value;
					base.OnPropertyChangedWithValue<string>(value, "NumOfPlayerWarsText");
				}
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x0002478F File Offset: 0x0002298F
		// (set) Token: 0x0600088A RID: 2186 RVA: 0x00024797 File Offset: 0x00022997
		[DataSourceProperty]
		public string PlayerTrucesText
		{
			get
			{
				return this._otherWarsText;
			}
			set
			{
				if (value != this._otherWarsText)
				{
					this._otherWarsText = value;
					base.OnPropertyChangedWithValue<string>(value, "PlayerTrucesText");
				}
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x000247BA File Offset: 0x000229BA
		// (set) Token: 0x0600088C RID: 2188 RVA: 0x000247C2 File Offset: 0x000229C2
		[DataSourceProperty]
		public string NumOfPlayerTrucesText
		{
			get
			{
				return this._numOfOtherWarsText;
			}
			set
			{
				if (value != this._numOfOtherWarsText)
				{
					this._numOfOtherWarsText = value;
					base.OnPropertyChangedWithValue<string>(value, "NumOfPlayerTrucesText");
				}
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x000247E5 File Offset: 0x000229E5
		// (set) Token: 0x0600088E RID: 2190 RVA: 0x000247ED File Offset: 0x000229ED
		[DataSourceProperty]
		public SelectorVM<SelectorItemVM> BehaviorSelection
		{
			get
			{
				return this._behaviorSelection;
			}
			set
			{
				if (value != this._behaviorSelection)
				{
					this._behaviorSelection = value;
					base.OnPropertyChangedWithValue<SelectorVM<SelectorItemVM>>(value, "BehaviorSelection");
				}
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x0002480B File Offset: 0x00022A0B
		// (set) Token: 0x06000890 RID: 2192 RVA: 0x00024813 File Offset: 0x00022A13
		[DataSourceProperty]
		public HintViewModel ShowStatBarsHint
		{
			get
			{
				return this._showStatBarsHint;
			}
			set
			{
				if (value != this._showStatBarsHint)
				{
					this._showStatBarsHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ShowStatBarsHint");
				}
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x00024831 File Offset: 0x00022A31
		// (set) Token: 0x06000892 RID: 2194 RVA: 0x00024839 File Offset: 0x00022A39
		[DataSourceProperty]
		public HintViewModel ShowWarLogsHint
		{
			get
			{
				return this._showWarLogsHint;
			}
			set
			{
				if (value != this._showWarLogsHint)
				{
					this._showWarLogsHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ShowWarLogsHint");
				}
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x00024857 File Offset: 0x00022A57
		// (set) Token: 0x06000894 RID: 2196 RVA: 0x0002485F File Offset: 0x00022A5F
		[DataSourceProperty]
		public HintViewModel ActionHint
		{
			get
			{
				return this._actionHint;
			}
			set
			{
				if (value != this._actionHint)
				{
					this._actionHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ActionHint");
				}
			}
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0002487D File Offset: 0x00022A7D
		private static int CalculateWarSupport(IFaction faction)
		{
			return MathF.Round(new KingdomElection(new DeclareWarDecision(Clan.PlayerClan, faction)).GetLikelihoodForSponsor(Clan.PlayerClan) * 100f);
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x000248A4 File Offset: 0x00022AA4
		private int CalculatePeaceSupport(KingdomWarItemVM policy, int dailyTributeToBePaid)
		{
			return MathF.Round(new KingdomElection(new MakePeaceKingdomDecision(Clan.PlayerClan, policy.Faction2, dailyTributeToBePaid, true)).GetLikelihoodForSponsor(Clan.PlayerClan) * 100f);
		}

		// Token: 0x040003BB RID: 955
		private KingdomDecision _currentItemsUnresolvedDecision;

		// Token: 0x040003BC RID: 956
		private readonly Action<KingdomDecision> _forceDecision;

		// Token: 0x040003BD RID: 957
		private readonly Kingdom _playerKingdom;

		// Token: 0x040003BE RID: 958
		private int _dailyPeaceTributeToPay;

		// Token: 0x040003BF RID: 959
		private bool _isChangingDiplomacyItem;

		// Token: 0x040003C0 RID: 960
		private MBBindingList<KingdomWarItemVM> _playerWars;

		// Token: 0x040003C1 RID: 961
		private MBBindingList<KingdomTruceItemVM> _playerTruces;

		// Token: 0x040003C2 RID: 962
		private KingdomWarSortControllerVM _warsSortController;

		// Token: 0x040003C3 RID: 963
		private KingdomDiplomacyItemVM _currentSelectedItem;

		// Token: 0x040003C4 RID: 964
		private SelectorVM<SelectorItemVM> _behaviorSelection;

		// Token: 0x040003C5 RID: 965
		private HintViewModel _showStatBarsHint;

		// Token: 0x040003C6 RID: 966
		private HintViewModel _showWarLogsHint;

		// Token: 0x040003C7 RID: 967
		private HintViewModel _actionHint;

		// Token: 0x040003C8 RID: 968
		private string _playerWarsText;

		// Token: 0x040003C9 RID: 969
		private string _numOfPlayerWarsText;

		// Token: 0x040003CA RID: 970
		private string _otherWarsText;

		// Token: 0x040003CB RID: 971
		private string _numOfOtherWarsText;

		// Token: 0x040003CC RID: 972
		private string _warsText;

		// Token: 0x040003CD RID: 973
		private string _actionName;

		// Token: 0x040003CE RID: 974
		private string _proposeActionExplanationText;

		// Token: 0x040003CF RID: 975
		private string _behaviorSelectionTitle;

		// Token: 0x040003D0 RID: 976
		private int _actionInfluenceCost;

		// Token: 0x040003D1 RID: 977
		private bool _isActionEnabled;

		// Token: 0x040003D2 RID: 978
		private bool _isDisplayingWarLogs;

		// Token: 0x040003D3 RID: 979
		private bool _isDisplayingStatComparisons;

		// Token: 0x040003D4 RID: 980
		private bool _isWar;
	}
}
