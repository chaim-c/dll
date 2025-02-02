using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Core.ViewModelCollection.Tutorial;
using TaleWorlds.Library;
using TaleWorlds.Library.EventSystem;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Decisions.ItemTypes
{
	// Token: 0x0200006C RID: 108
	public class DecisionItemBaseVM : ViewModel
	{
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x00026863 File Offset: 0x00024A63
		// (set) Token: 0x0600094A RID: 2378 RVA: 0x0002686B File Offset: 0x00024A6B
		public KingdomElection KingdomDecisionMaker { get; private set; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x00026874 File Offset: 0x00024A74
		private float _currentInfluenceCost
		{
			get
			{
				if (this._currentSelectedOption != null && !this._currentSelectedOption.IsOptionForAbstain)
				{
					if (!this.IsPlayerSupporter)
					{
						return (float)Campaign.Current.Models.ClanPoliticsModel.GetInfluenceRequiredToOverrideKingdomDecision(this.KingdomDecisionMaker.PossibleOutcomes.MaxBy((DecisionOutcome o) => o.WinChance), this._currentSelectedOption.Option, this._decision);
					}
					if (this._currentSelectedOption.CurrentSupportWeight != Supporter.SupportWeights.Choose)
					{
						return (float)this.KingdomDecisionMaker.GetInfluenceCostOfOutcome(this._currentSelectedOption.Option, Clan.PlayerClan, this._currentSelectedOption.CurrentSupportWeight);
					}
				}
				return 0f;
			}
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00026934 File Offset: 0x00024B34
		public DecisionItemBaseVM(KingdomDecision decision, Action onDecisionOver)
		{
			this._decision = decision;
			this._onDecisionOver = onDecisionOver;
			this.DecisionType = 0;
			this.DecisionOptionsList = new MBBindingList<DecisionOptionVM>();
			this.EndDecisionHint = new HintViewModel();
			CampaignEvents.KingdomDecisionConcluded.AddNonSerializedListener(this, new Action<KingdomDecision, DecisionOutcome, bool>(this.OnKingdomDecisionConcluded));
			this.RefreshValues();
			this.InitValues();
			Game game = Game.Current;
			if (game == null)
			{
				return;
			}
			EventManager eventManager = game.EventManager;
			if (eventManager == null)
			{
				return;
			}
			eventManager.RegisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x000269CC File Offset: 0x00024BCC
		private void OnKingdomDecisionConcluded(KingdomDecision decision, DecisionOutcome outcome, bool isPlayerInvolved)
		{
			if (decision == this._decision)
			{
				this.IsKingsDecisionOver = true;
				this.CurrentStageIndex = 1;
				foreach (DecisionOptionVM decisionOptionVM in this.DecisionOptionsList)
				{
					if (decisionOptionVM.Option == outcome)
					{
						decisionOptionVM.IsKingsOutcome = true;
					}
					decisionOptionVM.AfterKingChooseOutcome();
				}
			}
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00026A40 File Offset: 0x00024C40
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.DoneText = GameTexts.FindText("str_done", null).ToString();
			GameTexts.SetVariable("TOTAL_INFLUENCE", MathF.Round(Hero.MainHero.Clan.Influence));
			this.TotalInfluenceText = GameTexts.FindText("str_total_influence", null).ToString();
			this.RefreshInfluenceCost();
			MBBindingList<DecisionOptionVM> decisionOptionsList = this.DecisionOptionsList;
			if (decisionOptionsList == null)
			{
				return;
			}
			decisionOptionsList.ApplyActionOnAllItems(delegate(DecisionOptionVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00026AD4 File Offset: 0x00024CD4
		protected virtual void InitValues()
		{
			this.DecisionOptionsList.Clear();
			this.KingdomDecisionMaker = new KingdomElection(this._decision);
			this.KingdomDecisionMaker.StartElection();
			this.CurrentStageIndex = ((!this.KingdomDecisionMaker.IsPlayerChooser) ? 0 : 1);
			this.IsPlayerSupporter = !this.KingdomDecisionMaker.IsPlayerChooser;
			this.KingdomDecisionMaker.DetermineOfficialSupport();
			foreach (DecisionOutcome decisionOutcome in this.KingdomDecisionMaker.PossibleOutcomes)
			{
				DecisionOptionVM item = new DecisionOptionVM(decisionOutcome, this._decision, this.KingdomDecisionMaker, new Action<DecisionOptionVM>(this.OnChangeVote), new Action<DecisionOptionVM>(this.OnSupportStrengthChange))
				{
					WinPercentage = MathF.Round(decisionOutcome.WinChance * 100f),
					InitialPercentage = MathF.Round(decisionOutcome.WinChance * 100f)
				};
				this.DecisionOptionsList.Add(item);
			}
			if (this.IsPlayerSupporter)
			{
				DecisionOptionVM item2 = new DecisionOptionVM(null, null, this.KingdomDecisionMaker, new Action<DecisionOptionVM>(this.OnAbstain), new Action<DecisionOptionVM>(this.OnSupportStrengthChange));
				this.DecisionOptionsList.Add(item2);
			}
			this.TitleText = this.KingdomDecisionMaker.GetTitle().ToString();
			this.DescriptionText = this.KingdomDecisionMaker.GetDescription().ToString();
			this.RefreshInfluenceCost();
			this.RefreshCanEndDecision();
			this.RefreshRelationChangeText();
			this.IsActive = true;
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00026C68 File Offset: 0x00024E68
		private void OnChangeVote(DecisionOptionVM target)
		{
			if (this._currentSelectedOption != target)
			{
				if (this._currentSelectedOption != null)
				{
					this._currentSelectedOption.IsSelected = false;
				}
				this._currentSelectedOption = target;
				this._currentSelectedOption.IsSelected = true;
				this.KingdomDecisionMaker.OnPlayerSupport((!this._currentSelectedOption.IsOptionForAbstain) ? this._currentSelectedOption.Option : null, this._currentSelectedOption.CurrentSupportWeight);
				this.RefreshWinPercentages();
				this.RefreshInfluenceCost();
				this.RefreshCanEndDecision();
				this.RefreshRelationChangeText();
			}
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x00026CF0 File Offset: 0x00024EF0
		private void OnAbstain(DecisionOptionVM target)
		{
			if (this._currentSelectedOption != target)
			{
				if (this._currentSelectedOption != null)
				{
					this._currentSelectedOption.IsSelected = false;
				}
				this._currentSelectedOption = target;
				this._currentSelectedOption.IsSelected = true;
				this.KingdomDecisionMaker.OnPlayerSupport((!this._currentSelectedOption.IsOptionForAbstain) ? this._currentSelectedOption.Option : null, this._currentSelectedOption.CurrentSupportWeight);
				this.RefreshWinPercentages();
				this.RefreshInfluenceCost();
				this.RefreshCanEndDecision();
				this.RefreshRelationChangeText();
			}
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00026D76 File Offset: 0x00024F76
		private void OnSupportStrengthChange(DecisionOptionVM option)
		{
			this.RefreshWinPercentages();
			this.RefreshCanEndDecision();
			this.RefreshRelationChangeText();
			this.RefreshInfluenceCost();
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00026D90 File Offset: 0x00024F90
		private void RefreshWinPercentages()
		{
			this.KingdomDecisionMaker.DetermineOfficialSupport();
			using (List<DecisionOutcome>.Enumerator enumerator = this.KingdomDecisionMaker.PossibleOutcomes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					DecisionOutcome option = enumerator.Current;
					DecisionOptionVM decisionOptionVM = this.DecisionOptionsList.FirstOrDefault((DecisionOptionVM c) => c.Option == option);
					if (decisionOptionVM == null)
					{
						Debug.FailedAssert("Couldn't find option to update win chance for!", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\KingdomManagement\\Decisions\\ItemTypes\\DecisionItemBaseVM.cs", "RefreshWinPercentages", 210);
					}
					else
					{
						decisionOptionVM.WinPercentage = (int)MathF.Round(option.WinChance * 100f, 2);
					}
				}
			}
			int num = (from d in this.DecisionOptionsList
			where !d.IsOptionForAbstain
			select d).Sum((DecisionOptionVM d) => d.WinPercentage);
			if (num != 100)
			{
				int num2 = 100 - num;
				List<DecisionOptionVM> list = (from opt in this.DecisionOptionsList
				where opt.Sponsor != null
				select opt).ToList<DecisionOptionVM>();
				int num3 = (from opt in list
				select opt.WinPercentage).Sum();
				if (num3 == 0)
				{
					int num4 = num2 / list.Count;
					foreach (DecisionOptionVM decisionOptionVM2 in list)
					{
						decisionOptionVM2.WinPercentage += num4;
					}
					list[0].WinPercentage += num2 - num4 * list.Count;
					return;
				}
				int num5 = 0;
				foreach (DecisionOptionVM decisionOptionVM3 in (from opt in list
				where opt.WinPercentage > 0
				select opt).ToList<DecisionOptionVM>())
				{
					int num6 = MathF.Floor((float)num2 * ((float)decisionOptionVM3.WinPercentage / (float)num3));
					decisionOptionVM3.WinPercentage += num6;
					num5 += num6;
				}
				list[0].WinPercentage += num2 - num5;
			}
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0002702C File Offset: 0x0002522C
		private void RefreshInfluenceCost()
		{
			if (this._currentInfluenceCost > 0f)
			{
				GameTexts.SetVariable("AMOUNT", this._currentInfluenceCost);
				GameTexts.SetVariable("INFLUENCE_ICON", "{=!}<img src=\"General\\Icons\\Influence@2x\" extend=\"7\">");
				this.InfluenceCostText = GameTexts.FindText("str_decision_influence_cost", null).ToString();
				return;
			}
			this.InfluenceCostText = "";
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00027088 File Offset: 0x00025288
		private void RefreshRelationChangeText()
		{
			this.RelationChangeText = "";
			DecisionOptionVM currentSelectedOption = this._currentSelectedOption;
			if (currentSelectedOption != null && !currentSelectedOption.IsOptionForAbstain)
			{
				foreach (DecisionOptionVM decisionOptionVM in this.DecisionOptionsList)
				{
					DecisionOutcome option = decisionOptionVM.Option;
					if (((option != null) ? option.SponsorClan : null) != null && decisionOptionVM.Option.SponsorClan != Clan.PlayerClan)
					{
						bool flag = this._currentSelectedOption == decisionOptionVM;
						GameTexts.SetVariable("HERO_NAME", decisionOptionVM.Option.SponsorClan.Leader.EncyclopediaLinkWithName);
						string text = flag ? GameTexts.FindText("str_decision_relation_increase", null).ToString() : GameTexts.FindText("str_decision_relation_decrease", null).ToString();
						if (string.IsNullOrEmpty(this.RelationChangeText))
						{
							this.RelationChangeText = text;
						}
						else
						{
							GameTexts.SetVariable("newline", "\n");
							GameTexts.SetVariable("STR1", this.RelationChangeText);
							GameTexts.SetVariable("STR2", text);
							this.RelationChangeText = GameTexts.FindText("str_string_newline_string", null).ToString();
						}
					}
				}
			}
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x000271C8 File Offset: 0x000253C8
		private void RefreshCanEndDecision()
		{
			bool flag = this._currentSelectedOption != null && (!this.IsPlayerSupporter || this._currentSelectedOption.CurrentSupportWeight > Supporter.SupportWeights.Choose);
			bool flag2 = this._currentInfluenceCost <= Clan.PlayerClan.Influence || this._currentInfluenceCost == 0f;
			DecisionOptionVM currentSelectedOption = this._currentSelectedOption;
			bool flag3 = currentSelectedOption != null && currentSelectedOption.IsOptionForAbstain;
			this.CanEndDecision = (!this._finalSelectionDone && (flag3 || (flag && flag2)));
			if (this.CanEndDecision)
			{
				this.EndDecisionHint.HintText = TextObject.Empty;
				return;
			}
			if (flag)
			{
				if (!flag2)
				{
					this.EndDecisionHint.HintText = GameTexts.FindText("str_decision_not_enough_influence", null);
				}
				return;
			}
			if (this.IsPlayerSupporter)
			{
				this.EndDecisionHint.HintText = GameTexts.FindText("str_decision_need_to_select_an_option_and_support", null);
				return;
			}
			this.EndDecisionHint.HintText = GameTexts.FindText("str_decision_need_to_select_an_outcome", null);
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x000272B5 File Offset: 0x000254B5
		protected void ExecuteLink(string link)
		{
			Campaign.Current.EncyclopediaManager.GoToLink(link);
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x000272C8 File Offset: 0x000254C8
		protected void ExecuteShowStageTooltip()
		{
			if (!this.IsPlayerSupporter)
			{
				MBInformationManager.ShowHint(GameTexts.FindText("str_decision_second_stage_player_decider", null).ToString());
				return;
			}
			if (this.CurrentStageIndex == 0)
			{
				MBInformationManager.ShowHint(GameTexts.FindText("str_decision_first_stage_player_supporter", null).ToString());
				return;
			}
			MBInformationManager.ShowHint(GameTexts.FindText("str_decision_second_stage_player_supporter", null).ToString());
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00027326 File Offset: 0x00025526
		protected void ExecuteHideStageTooltip()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0002732D File Offset: 0x0002552D
		public void ExecuteFinalSelection()
		{
			if (this.CanEndDecision)
			{
				this.KingdomDecisionMaker.ApplySelection();
				this._finalSelectionDone = true;
				this.RefreshCanEndDecision();
			}
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00027350 File Offset: 0x00025550
		protected void ExecuteDone()
		{
			TextObject chosenOutcomeText = this.KingdomDecisionMaker.GetChosenOutcomeText();
			this.IsActive = false;
			InformationManager.ShowInquiry(new InquiryData(GameTexts.FindText("str_decision_outcome", null).ToString(), chosenOutcomeText.ToString(), true, false, GameTexts.FindText("str_ok", null).ToString(), "", delegate()
			{
				this._onDecisionOver();
			}, null, "", 0f, null, null, null), false, false);
			CampaignEvents.KingdomDecisionConcluded.ClearListeners(this);
			this._currentSelectedOption = null;
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x000273D5 File Offset: 0x000255D5
		public override void OnFinalize()
		{
			base.OnFinalize();
			Game game = Game.Current;
			if (game == null)
			{
				return;
			}
			EventManager eventManager = game.EventManager;
			if (eventManager == null)
			{
				return;
			}
			eventManager.UnregisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x00027404 File Offset: 0x00025604
		private void OnTutorialNotificationElementIDChange(TutorialNotificationElementChangeEvent obj)
		{
			if (this._latestTutorialElementID != obj.NewNotificationElementID)
			{
				this._latestTutorialElementID = obj.NewNotificationElementID;
				if (this._isDecisionOptionsHighlightEnabled && this._latestTutorialElementID != this._decisionOptionsHighlightID)
				{
					this.SetOptionsHighlight(false);
					this._isDecisionOptionsHighlightEnabled = false;
					return;
				}
				if (!this._isDecisionOptionsHighlightEnabled && this._latestTutorialElementID == this._decisionOptionsHighlightID)
				{
					this.SetOptionsHighlight(true);
					this._isDecisionOptionsHighlightEnabled = true;
				}
			}
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00027484 File Offset: 0x00025684
		private void SetOptionsHighlight(bool state)
		{
			for (int i = 0; i < this.DecisionOptionsList.Count; i++)
			{
				DecisionOptionVM decisionOptionVM = this.DecisionOptionsList[i];
				if (decisionOptionVM.CanBeChosen)
				{
					decisionOptionVM.IsHighlightEnabled = state;
				}
			}
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x000274C3 File Offset: 0x000256C3
		public void SetDoneInputKey(InputKeyItemVM inputKeyItemVM)
		{
			this.DoneInputKey = inputKeyItemVM;
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x000274CC File Offset: 0x000256CC
		// (set) Token: 0x06000961 RID: 2401 RVA: 0x000274D4 File Offset: 0x000256D4
		[DataSourceProperty]
		public InputKeyItemVM DoneInputKey
		{
			get
			{
				return this._doneInputKey;
			}
			set
			{
				if (value != this._doneInputKey)
				{
					this._doneInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "DoneInputKey");
				}
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x000274F2 File Offset: 0x000256F2
		// (set) Token: 0x06000963 RID: 2403 RVA: 0x000274FA File Offset: 0x000256FA
		[DataSourceProperty]
		public HintViewModel EndDecisionHint
		{
			get
			{
				return this._endDecisionHint;
			}
			set
			{
				if (value != this._endDecisionHint)
				{
					this._endDecisionHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "EndDecisionHint");
				}
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x00027518 File Offset: 0x00025718
		// (set) Token: 0x06000965 RID: 2405 RVA: 0x00027520 File Offset: 0x00025720
		[DataSourceProperty]
		public int DecisionType
		{
			get
			{
				return this._decisionType;
			}
			set
			{
				if (value != this._decisionType)
				{
					this._decisionType = value;
					base.OnPropertyChangedWithValue(value, "DecisionType");
				}
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x0002753E File Offset: 0x0002573E
		// (set) Token: 0x06000967 RID: 2407 RVA: 0x00027546 File Offset: 0x00025746
		[DataSourceProperty]
		public string TotalInfluenceText
		{
			get
			{
				return this._totalInfluenceText;
			}
			set
			{
				if (value != this._totalInfluenceText)
				{
					this._totalInfluenceText = value;
					base.OnPropertyChangedWithValue<string>(value, "TotalInfluenceText");
				}
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x00027569 File Offset: 0x00025769
		// (set) Token: 0x06000969 RID: 2409 RVA: 0x00027571 File Offset: 0x00025771
		[DataSourceProperty]
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
			set
			{
				if (value != this._isActive)
				{
					this._isActive = value;
					base.OnPropertyChangedWithValue(value, "IsActive");
				}
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x0002758F File Offset: 0x0002578F
		// (set) Token: 0x0600096B RID: 2411 RVA: 0x00027597 File Offset: 0x00025797
		[DataSourceProperty]
		public int CurrentStageIndex
		{
			get
			{
				return this._currentStageIndex;
			}
			set
			{
				if (value != this._currentStageIndex)
				{
					this._currentStageIndex = value;
					base.OnPropertyChangedWithValue(value, "CurrentStageIndex");
				}
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x000275B5 File Offset: 0x000257B5
		// (set) Token: 0x0600096D RID: 2413 RVA: 0x000275BD File Offset: 0x000257BD
		[DataSourceProperty]
		public bool IsPlayerSupporter
		{
			get
			{
				return this._isPlayerSupporter;
			}
			set
			{
				if (value != this._isPlayerSupporter)
				{
					this._isPlayerSupporter = value;
					base.OnPropertyChangedWithValue(value, "IsPlayerSupporter");
				}
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x000275DB File Offset: 0x000257DB
		// (set) Token: 0x0600096F RID: 2415 RVA: 0x000275E3 File Offset: 0x000257E3
		[DataSourceProperty]
		public bool CanEndDecision
		{
			get
			{
				return this._canEndDecision;
			}
			set
			{
				if (value != this._canEndDecision)
				{
					this._canEndDecision = value;
					base.OnPropertyChangedWithValue(value, "CanEndDecision");
				}
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x00027601 File Offset: 0x00025801
		// (set) Token: 0x06000971 RID: 2417 RVA: 0x00027609 File Offset: 0x00025809
		[DataSourceProperty]
		public bool IsKingsDecisionOver
		{
			get
			{
				return this._isKingsDecisionOver;
			}
			set
			{
				if (value != this._isKingsDecisionOver)
				{
					this._isKingsDecisionOver = value;
					base.OnPropertyChangedWithValue(value, "IsKingsDecisionOver");
				}
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x00027627 File Offset: 0x00025827
		// (set) Token: 0x06000973 RID: 2419 RVA: 0x0002762F File Offset: 0x0002582F
		[DataSourceProperty]
		public string RelationChangeText
		{
			get
			{
				return this._increaseRelationText;
			}
			set
			{
				if (value != this._increaseRelationText)
				{
					this._increaseRelationText = value;
					base.OnPropertyChangedWithValue<string>(value, "RelationChangeText");
				}
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x00027652 File Offset: 0x00025852
		// (set) Token: 0x06000975 RID: 2421 RVA: 0x0002765A File Offset: 0x0002585A
		[DataSourceProperty]
		public string DescriptionText
		{
			get
			{
				return this._descriptionText;
			}
			set
			{
				if (value != this._descriptionText)
				{
					this._descriptionText = value;
					base.OnPropertyChangedWithValue<string>(value, "DescriptionText");
				}
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x0002767D File Offset: 0x0002587D
		// (set) Token: 0x06000977 RID: 2423 RVA: 0x00027685 File Offset: 0x00025885
		[DataSourceProperty]
		public string TitleText
		{
			get
			{
				return this._titleText;
			}
			set
			{
				if (value != this._titleText)
				{
					this._titleText = value;
					base.OnPropertyChangedWithValue<string>(value, "TitleText");
				}
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x000276A8 File Offset: 0x000258A8
		// (set) Token: 0x06000979 RID: 2425 RVA: 0x000276B0 File Offset: 0x000258B0
		[DataSourceProperty]
		public string DoneText
		{
			get
			{
				return this._doneText;
			}
			set
			{
				if (value != this._doneText)
				{
					this._doneText = value;
					base.OnPropertyChangedWithValue<string>(value, "DoneText");
				}
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x000276D3 File Offset: 0x000258D3
		// (set) Token: 0x0600097B RID: 2427 RVA: 0x000276DB File Offset: 0x000258DB
		[DataSourceProperty]
		public string InfluenceCostText
		{
			get
			{
				return this._influenceCostText;
			}
			set
			{
				if (value != this._influenceCostText)
				{
					this._influenceCostText = value;
					base.OnPropertyChangedWithValue<string>(value, "InfluenceCostText");
				}
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x000276FE File Offset: 0x000258FE
		// (set) Token: 0x0600097D RID: 2429 RVA: 0x00027706 File Offset: 0x00025906
		[DataSourceProperty]
		public MBBindingList<DecisionOptionVM> DecisionOptionsList
		{
			get
			{
				return this._decisionOptionsList;
			}
			set
			{
				if (value != this._decisionOptionsList)
				{
					this._decisionOptionsList = value;
					base.OnPropertyChangedWithValue<MBBindingList<DecisionOptionVM>>(value, "DecisionOptionsList");
				}
			}
		}

		// Token: 0x04000430 RID: 1072
		protected readonly KingdomDecision _decision;

		// Token: 0x04000431 RID: 1073
		private readonly Action _onDecisionOver;

		// Token: 0x04000432 RID: 1074
		private DecisionOptionVM _currentSelectedOption;

		// Token: 0x04000433 RID: 1075
		private bool _finalSelectionDone;

		// Token: 0x04000434 RID: 1076
		private bool _isDecisionOptionsHighlightEnabled;

		// Token: 0x04000435 RID: 1077
		private string _decisionOptionsHighlightID = "DecisionOptions";

		// Token: 0x04000436 RID: 1078
		private string _latestTutorialElementID;

		// Token: 0x04000437 RID: 1079
		private InputKeyItemVM _doneInputKey;

		// Token: 0x04000438 RID: 1080
		private int _decisionType;

		// Token: 0x04000439 RID: 1081
		private bool _isActive;

		// Token: 0x0400043A RID: 1082
		private bool _isPlayerSupporter;

		// Token: 0x0400043B RID: 1083
		private bool _canEndDecision;

		// Token: 0x0400043C RID: 1084
		private bool _isKingsDecisionOver;

		// Token: 0x0400043D RID: 1085
		private int _currentStageIndex = -1;

		// Token: 0x0400043E RID: 1086
		private string _titleText;

		// Token: 0x0400043F RID: 1087
		private string _doneText;

		// Token: 0x04000440 RID: 1088
		private string _descriptionText;

		// Token: 0x04000441 RID: 1089
		private string _influenceCostText;

		// Token: 0x04000442 RID: 1090
		private string _totalInfluenceText;

		// Token: 0x04000443 RID: 1091
		private string _increaseRelationText;

		// Token: 0x04000444 RID: 1092
		private HintViewModel _endDecisionHint;

		// Token: 0x04000445 RID: 1093
		private MBBindingList<DecisionOptionVM> _decisionOptionsList;

		// Token: 0x020001AB RID: 427
		protected enum DecisionTypes
		{
			// Token: 0x04000FEC RID: 4076
			Default,
			// Token: 0x04000FED RID: 4077
			Settlement,
			// Token: 0x04000FEE RID: 4078
			ExpelClan,
			// Token: 0x04000FEF RID: 4079
			Policy,
			// Token: 0x04000FF0 RID: 4080
			DeclareWar,
			// Token: 0x04000FF1 RID: 4081
			MakePeace,
			// Token: 0x04000FF2 RID: 4082
			KingSelection
		}
	}
}
