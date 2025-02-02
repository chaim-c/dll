using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Decisions.ItemTypes;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Decisions
{
	// Token: 0x0200006B RID: 107
	public class KingdomDecisionsVM : ViewModel
	{
		// Token: 0x170002DF RID: 735
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x000261AD File Offset: 0x000243AD
		public bool IsCurrentDecisionActive
		{
			get
			{
				DecisionItemBaseVM currentDecision = this.CurrentDecision;
				return currentDecision != null && currentDecision.IsActive;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x000261C0 File Offset: 0x000243C0
		// (set) Token: 0x0600092F RID: 2351 RVA: 0x000261C8 File Offset: 0x000243C8
		private bool _shouldCheckForDecision { get; set; } = true;

		// Token: 0x06000930 RID: 2352 RVA: 0x000261D4 File Offset: 0x000243D4
		public KingdomDecisionsVM(Action refreshKingdomManagement)
		{
			this._refreshKingdomManagement = refreshKingdomManagement;
			this._examinedDecisionsSinceInit = new List<KingdomDecision>();
			this._examinedDecisionsSinceInit.AddRange(from d in Clan.PlayerClan.Kingdom.UnresolvedDecisions
			where d.ShouldBeCancelled()
			select d);
			this._solvedDecisionsSinceInit = new List<KingdomDecision>();
			CampaignEvents.KingdomDecisionConcluded.AddNonSerializedListener(this, new Action<KingdomDecision, DecisionOutcome, bool>(this.OnKingdomDecisionConcluded));
			this.IsRefreshed = true;
			this.RefreshValues();
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0002626D File Offset: 0x0002446D
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.TitleText = GameTexts.FindText("str_kingdom_decisions", null).ToString();
			DecisionItemBaseVM currentDecision = this.CurrentDecision;
			if (currentDecision == null)
			{
				return;
			}
			currentDecision.RefreshValues();
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0002629C File Offset: 0x0002449C
		public void OnFrameTick()
		{
			this.IsActive = this.IsCurrentDecisionActive;
			IEnumerable<KingdomDecision> source = Clan.PlayerClan.Kingdom.UnresolvedDecisions.Except(this._examinedDecisionsSinceInit);
			if (this._shouldCheckForDecision)
			{
				if (this.CurrentDecision != null)
				{
					DecisionItemBaseVM currentDecision = this.CurrentDecision;
					if (currentDecision == null || currentDecision.IsActive)
					{
						return;
					}
				}
				if (source.Any<KingdomDecision>())
				{
					KingdomDecision kingdomDecision = this._solvedDecisionsSinceInit.LastOrDefault<KingdomDecision>();
					KingdomDecision kingdomDecision2 = (kingdomDecision != null) ? kingdomDecision.GetFollowUpDecision() : null;
					if (kingdomDecision2 != null)
					{
						this.HandleDecision(kingdomDecision2);
						return;
					}
					this.HandleNextDecision();
				}
			}
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x00026328 File Offset: 0x00024528
		public void HandleNextDecision()
		{
			this.HandleDecision(Clan.PlayerClan.Kingdom.UnresolvedDecisions.Except(this._examinedDecisionsSinceInit).FirstOrDefault<KingdomDecision>());
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00026350 File Offset: 0x00024550
		public void HandleDecision(KingdomDecision curDecision)
		{
			KingdomDecision curDecision2 = curDecision;
			if (curDecision2 != null && !curDecision2.ShouldBeCancelled())
			{
				this._shouldCheckForDecision = false;
				this._examinedDecisionsSinceInit.Add(curDecision);
				if (curDecision.IsPlayerParticipant)
				{
					TextObject generalTitle = new KingdomElection(curDecision).GetGeneralTitle();
					GameTexts.SetVariable("DECISION_NAME", generalTitle.ToString());
					string text = curDecision.NeedsPlayerResolution ? GameTexts.FindText("str_you_need_to_resolve_decision", null).ToString() : GameTexts.FindText("str_do_you_want_to_resolve_decision", null).ToString();
					if (!curDecision.NeedsPlayerResolution && curDecision.TriggerTime.IsFuture)
					{
						GameTexts.SetVariable("HOUR", ((int)curDecision.TriggerTime.RemainingHoursFromNow).ToString());
						GameTexts.SetVariable("newline", "\n");
						GameTexts.SetVariable("STR1", text);
						GameTexts.SetVariable("STR2", GameTexts.FindText("str_decision_will_be_resolved_in_hours", null));
						text = GameTexts.FindText("str_string_newline_string", null).ToString();
					}
					this._queryData = new InquiryData(GameTexts.FindText("str_decision", null).ToString(), text, true, !curDecision.NeedsPlayerResolution, GameTexts.FindText("str_ok", null).ToString(), GameTexts.FindText("str_cancel", null).ToString(), delegate()
					{
						this.RefreshWith(curDecision);
					}, delegate()
					{
						this._shouldCheckForDecision = true;
					}, "", 0f, null, null, null);
					this._shouldCheckForDecision = false;
					InformationManager.ShowInquiry(this._queryData, false, false);
					return;
				}
			}
			else
			{
				this._shouldCheckForDecision = false;
				this._queryData = null;
			}
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00026520 File Offset: 0x00024720
		public void RefreshWith(KingdomDecision decision)
		{
			if (decision.IsSingleClanDecision())
			{
				KingdomElection kingdomElection = new KingdomElection(decision);
				kingdomElection.StartElection();
				kingdomElection.ApplySelection();
				InformationManager.ShowInquiry(new InquiryData(GameTexts.FindText("str_decision_outcome", null).ToString(), kingdomElection.GetChosenOutcomeText().ToString(), true, false, GameTexts.FindText("str_ok", null).ToString(), "", delegate()
				{
					this.OnSingleDecisionOver();
				}, null, "", 0f, null, null, null), false, false);
				return;
			}
			this._shouldCheckForDecision = false;
			this.CurrentDecision = this.GetDecisionItemBasedOnType(decision);
			this.CurrentDecision.SetDoneInputKey(this.DoneInputKey);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x000265C6 File Offset: 0x000247C6
		private void OnSingleDecisionOver()
		{
			this._refreshKingdomManagement();
			this._shouldCheckForDecision = true;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x000265DA File Offset: 0x000247DA
		private void OnDecisionOver()
		{
			this._refreshKingdomManagement();
			DecisionItemBaseVM currentDecision = this.CurrentDecision;
			if (currentDecision != null)
			{
				currentDecision.OnFinalize();
			}
			this.CurrentDecision = null;
			this._shouldCheckForDecision = true;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00026606 File Offset: 0x00024806
		private void OnKingdomDecisionConcluded(KingdomDecision decision, DecisionOutcome outcome, bool isPlayerInvolved)
		{
			if (isPlayerInvolved)
			{
				this._solvedDecisionsSinceInit.Add(decision);
			}
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00026618 File Offset: 0x00024818
		private DecisionItemBaseVM GetDecisionItemBasedOnType(KingdomDecision decision)
		{
			SettlementClaimantDecision settlementClaimantDecision;
			if ((settlementClaimantDecision = (decision as SettlementClaimantDecision)) != null)
			{
				return new SettlementDecisionItemVM(settlementClaimantDecision.Settlement, decision, new Action(this.OnDecisionOver));
			}
			SettlementClaimantPreliminaryDecision settlementClaimantPreliminaryDecision;
			if ((settlementClaimantPreliminaryDecision = (decision as SettlementClaimantPreliminaryDecision)) != null)
			{
				return new SettlementDecisionItemVM(settlementClaimantPreliminaryDecision.Settlement, decision, new Action(this.OnDecisionOver));
			}
			ExpelClanFromKingdomDecision decision2;
			if ((decision2 = (decision as ExpelClanFromKingdomDecision)) != null)
			{
				return new ExpelClanDecisionItemVM(decision2, new Action(this.OnDecisionOver));
			}
			KingdomPolicyDecision decision3;
			if ((decision3 = (decision as KingdomPolicyDecision)) != null)
			{
				return new PolicyDecisionItemVM(decision3, new Action(this.OnDecisionOver));
			}
			DeclareWarDecision decision4;
			if ((decision4 = (decision as DeclareWarDecision)) != null)
			{
				return new DeclareWarDecisionItemVM(decision4, new Action(this.OnDecisionOver));
			}
			MakePeaceKingdomDecision decision5;
			if ((decision5 = (decision as MakePeaceKingdomDecision)) != null)
			{
				return new MakePeaceDecisionItemVM(decision5, new Action(this.OnDecisionOver));
			}
			KingSelectionKingdomDecision decision6;
			if ((decision6 = (decision as KingSelectionKingdomDecision)) != null)
			{
				return new KingSelectionDecisionItemVM(decision6, new Action(this.OnDecisionOver));
			}
			Debug.FailedAssert("No defined decision type for this decision! This shouldn't happen", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\KingdomManagement\\Decisions\\KingdomDecisionsVM.cs", "GetDecisionItemBasedOnType", 193);
			return new DecisionItemBaseVM(decision, new Action(this.OnDecisionOver));
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0002672D File Offset: 0x0002492D
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.DoneInputKey.OnFinalize();
			DecisionItemBaseVM currentDecision = this.CurrentDecision;
			if (currentDecision != null)
			{
				currentDecision.OnFinalize();
			}
			this.CurrentDecision = null;
			CampaignEvents.KingdomDecisionConcluded.ClearListeners(this);
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00026763 File Offset: 0x00024963
		public void SetDoneInputKey(HotKey hotKey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x00026772 File Offset: 0x00024972
		// (set) Token: 0x0600093D RID: 2365 RVA: 0x0002677A File Offset: 0x0002497A
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

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x00026798 File Offset: 0x00024998
		// (set) Token: 0x0600093F RID: 2367 RVA: 0x000267A0 File Offset: 0x000249A0
		[DataSourceProperty]
		public DecisionItemBaseVM CurrentDecision
		{
			get
			{
				return this._currentDecision;
			}
			set
			{
				if (value != this._currentDecision)
				{
					this._currentDecision = value;
					base.OnPropertyChangedWithValue<DecisionItemBaseVM>(value, "CurrentDecision");
				}
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x000267BE File Offset: 0x000249BE
		// (set) Token: 0x06000941 RID: 2369 RVA: 0x000267C6 File Offset: 0x000249C6
		[DataSourceProperty]
		public int NotificationCount
		{
			get
			{
				return this._notificationCount;
			}
			set
			{
				if (value != this._notificationCount)
				{
					this._notificationCount = value;
					base.OnPropertyChangedWithValue(value, "NotificationCount");
				}
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x000267E4 File Offset: 0x000249E4
		// (set) Token: 0x06000943 RID: 2371 RVA: 0x000267EC File Offset: 0x000249EC
		[DataSourceProperty]
		public bool IsRefreshed
		{
			get
			{
				return this._isRefreshed;
			}
			set
			{
				if (value != this._isRefreshed)
				{
					this._isRefreshed = value;
					base.OnPropertyChangedWithValue(value, "IsRefreshed");
				}
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x0002680A File Offset: 0x00024A0A
		// (set) Token: 0x06000945 RID: 2373 RVA: 0x00026812 File Offset: 0x00024A12
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

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x00026830 File Offset: 0x00024A30
		// (set) Token: 0x06000947 RID: 2375 RVA: 0x00026838 File Offset: 0x00024A38
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

		// Token: 0x04000424 RID: 1060
		private List<KingdomDecision> _examinedDecisionsSinceInit;

		// Token: 0x04000425 RID: 1061
		private List<KingdomDecision> _solvedDecisionsSinceInit;

		// Token: 0x04000426 RID: 1062
		private readonly Action _refreshKingdomManagement;

		// Token: 0x04000428 RID: 1064
		private InquiryData _queryData;

		// Token: 0x04000429 RID: 1065
		private InputKeyItemVM _doneInputKey;

		// Token: 0x0400042A RID: 1066
		private bool _isRefreshed;

		// Token: 0x0400042B RID: 1067
		private bool _isActive;

		// Token: 0x0400042C RID: 1068
		private int _notificationCount;

		// Token: 0x0400042D RID: 1069
		private string _titleText;

		// Token: 0x0400042E RID: 1070
		private DecisionItemBaseVM _currentDecision;
	}
}
