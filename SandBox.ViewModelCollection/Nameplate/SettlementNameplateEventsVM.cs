using System;
using System.Collections.Generic;
using System.Linq;
using SandBox.ViewModelCollection.Missions.NameMarker;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Issues;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.Nameplate
{
	// Token: 0x02000017 RID: 23
	public class SettlementNameplateEventsVM : ViewModel
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000B339 File Offset: 0x00009539
		// (set) Token: 0x06000238 RID: 568 RVA: 0x0000B341 File Offset: 0x00009541
		public bool IsEventsRegistered { get; private set; }

		// Token: 0x06000239 RID: 569 RVA: 0x0000B34A File Offset: 0x0000954A
		public SettlementNameplateEventsVM(Settlement settlement)
		{
			this._settlement = settlement;
			this.EventsList = new MBBindingList<SettlementNameplateEventItemVM>();
			this.TrackQuests = new MBBindingList<QuestMarkerVM>();
			this._relatedQuests = new List<QuestBase>();
			if (settlement.IsVillage)
			{
				this.AddPrimaryProductionIcon();
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000B388 File Offset: 0x00009588
		public void Tick()
		{
			if (this._areQuestsDirty)
			{
				this.RefreshQuestCounts();
				this._areQuestsDirty = false;
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000B39F File Offset: 0x0000959F
		private void PopulateEventList()
		{
			if (Campaign.Current.TournamentManager.GetTournamentGame(this._settlement.Town) != null)
			{
				this.EventsList.Add(new SettlementNameplateEventItemVM(SettlementNameplateEventItemVM.SettlementEventType.Tournament));
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000B3D0 File Offset: 0x000095D0
		public void RegisterEvents()
		{
			if (!this.IsEventsRegistered)
			{
				this.PopulateEventList();
				CampaignEvents.TournamentStarted.AddNonSerializedListener(this, new Action<Town>(this.OnTournamentStarted));
				CampaignEvents.TournamentFinished.AddNonSerializedListener(this, new Action<CharacterObject, MBReadOnlyList<CharacterObject>, Town, ItemObject>(this.OnTournamentFinished));
				CampaignEvents.TournamentCancelled.AddNonSerializedListener(this, new Action<Town>(this.OnTournamentCancelled));
				CampaignEvents.OnNewIssueCreatedEvent.AddNonSerializedListener(this, new Action<IssueBase>(this.OnNewIssueCreated));
				CampaignEvents.OnIssueUpdatedEvent.AddNonSerializedListener(this, new Action<IssueBase, IssueBase.IssueUpdateDetails, Hero>(this.OnIssueUpdated));
				CampaignEvents.OnQuestStartedEvent.AddNonSerializedListener(this, new Action<QuestBase>(this.OnQuestStarted));
				CampaignEvents.QuestLogAddedEvent.AddNonSerializedListener(this, new Action<QuestBase, bool>(this.OnQuestLogAdded));
				CampaignEvents.OnQuestCompletedEvent.AddNonSerializedListener(this, new Action<QuestBase, QuestBase.QuestCompleteDetails>(this.OnQuestCompleted));
				CampaignEvents.SettlementEntered.AddNonSerializedListener(this, new Action<MobileParty, Settlement, Hero>(this.OnSettlementEntered));
				CampaignEvents.OnSettlementLeftEvent.AddNonSerializedListener(this, new Action<MobileParty, Settlement>(this.OnSettlementLeft));
				CampaignEvents.HeroPrisonerTaken.AddNonSerializedListener(this, new Action<PartyBase, Hero>(this.OnHeroTakenPrisoner));
				this.IsEventsRegistered = true;
				this.RefreshQuestCounts();
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000B4F8 File Offset: 0x000096F8
		public void UnloadEvents()
		{
			if (this.IsEventsRegistered)
			{
				CampaignEvents.TournamentStarted.ClearListeners(this);
				CampaignEvents.TournamentFinished.ClearListeners(this);
				CampaignEvents.TournamentCancelled.ClearListeners(this);
				CampaignEvents.OnNewIssueCreatedEvent.ClearListeners(this);
				CampaignEvents.OnIssueUpdatedEvent.ClearListeners(this);
				CampaignEvents.OnQuestStartedEvent.ClearListeners(this);
				CampaignEvents.QuestLogAddedEvent.ClearListeners(this);
				CampaignEvents.OnQuestCompletedEvent.ClearListeners(this);
				CampaignEvents.SettlementEntered.ClearListeners(this);
				CampaignEvents.OnSettlementLeftEvent.ClearListeners(this);
				CampaignEvents.HeroPrisonerTaken.ClearListeners(this);
				int num = this.EventsList.Count;
				for (int i = 0; i < num; i++)
				{
					if (this.EventsList[i].EventType != SettlementNameplateEventItemVM.SettlementEventType.Production)
					{
						this.EventsList.RemoveAt(i);
						num--;
						i--;
					}
				}
				this.IsEventsRegistered = false;
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000B5D0 File Offset: 0x000097D0
		private void OnTournamentStarted(Town town)
		{
			if (this._settlement.Town != null && town == this._settlement.Town)
			{
				bool flag = false;
				for (int i = 0; i < this.EventsList.Count; i++)
				{
					if (this.EventsList[i].EventType == SettlementNameplateEventItemVM.SettlementEventType.Tournament)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					this.EventsList.Add(new SettlementNameplateEventItemVM(SettlementNameplateEventItemVM.SettlementEventType.Tournament));
				}
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000B63B File Offset: 0x0000983B
		private void OnTournamentFinished(CharacterObject winner, MBReadOnlyList<CharacterObject> participants, Town town, ItemObject prize)
		{
			this.RemoveTournament(town);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000B644 File Offset: 0x00009844
		private void OnTournamentCancelled(Town town)
		{
			this.RemoveTournament(town);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000B650 File Offset: 0x00009850
		private void RemoveTournament(Town town)
		{
			if (this._settlement.Town != null && town == this._settlement.Town)
			{
				if (this.EventsList.Count((SettlementNameplateEventItemVM e) => e.EventType == SettlementNameplateEventItemVM.SettlementEventType.Tournament) > 0)
				{
					int num = -1;
					for (int i = 0; i < this.EventsList.Count; i++)
					{
						if (this.EventsList[i].EventType == SettlementNameplateEventItemVM.SettlementEventType.Tournament)
						{
							num = i;
							break;
						}
					}
					if (num != -1)
					{
						this.EventsList.RemoveAt(num);
						return;
					}
					Debug.FailedAssert("There should be a tournament item to remove", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox.ViewModelCollection\\Nameplate\\SettlementNameplateEventsVM.cs", "RemoveTournament", 162);
				}
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000B704 File Offset: 0x00009904
		private void RefreshQuestCounts()
		{
			this._relatedQuests.Clear();
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = Campaign.Current.IssueManager.GetNumOfActiveIssuesInSettlement(this._settlement, false);
			int numOfAvailableIssuesInSettlement = Campaign.Current.IssueManager.GetNumOfAvailableIssuesInSettlement(this._settlement);
			this.TrackQuests.Clear();
			List<QuestBase> list;
			if (Campaign.Current.QuestManager.TrackedObjects.TryGetValue(this._settlement, out list))
			{
				foreach (QuestBase questBase in list)
				{
					if (questBase.IsSpecialQuest)
					{
						if (!this.TrackQuests.Any((QuestMarkerVM x) => x.IssueQuestFlag == SandBoxUIHelper.IssueQuestFlags.TrackedStoryQuest))
						{
							this.TrackQuests.Add(new QuestMarkerVM(SandBoxUIHelper.IssueQuestFlags.TrackedStoryQuest));
							this._relatedQuests.Add(questBase);
							continue;
						}
					}
					if (!this.TrackQuests.Any((QuestMarkerVM x) => x.IssueQuestFlag == SandBoxUIHelper.IssueQuestFlags.TrackedIssue))
					{
						this.TrackQuests.Add(new QuestMarkerVM(SandBoxUIHelper.IssueQuestFlags.TrackedIssue));
						this._relatedQuests.Add(questBase);
					}
				}
			}
			List<ValueTuple<bool, QuestBase>> questsRelatedToSettlement = SandBoxUIHelper.GetQuestsRelatedToSettlement(this._settlement);
			for (int i = 0; i < questsRelatedToSettlement.Count; i++)
			{
				if (questsRelatedToSettlement[i].Item1)
				{
					if (questsRelatedToSettlement[i].Item2.IsSpecialQuest)
					{
						num++;
					}
					else
					{
						num4++;
					}
				}
				else if (questsRelatedToSettlement[i].Item2.IsSpecialQuest)
				{
					num3++;
				}
				else
				{
					num2++;
				}
				this._relatedQuests.Add(questsRelatedToSettlement[i].Item2);
			}
			this.HandleIssueCount(numOfAvailableIssuesInSettlement, SettlementNameplateEventItemVM.SettlementEventType.AvailableIssue);
			this.HandleIssueCount(num4, SettlementNameplateEventItemVM.SettlementEventType.ActiveQuest);
			this.HandleIssueCount(num, SettlementNameplateEventItemVM.SettlementEventType.ActiveStoryQuest);
			this.HandleIssueCount(num2, SettlementNameplateEventItemVM.SettlementEventType.TrackedIssue);
			this.HandleIssueCount(num3, SettlementNameplateEventItemVM.SettlementEventType.TrackedStoryQuest);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000B914 File Offset: 0x00009B14
		private void OnNewIssueCreated(IssueBase issue)
		{
			if (issue.IssueSettlement != this._settlement)
			{
				Hero issueOwner = issue.IssueOwner;
				if (((issueOwner != null) ? issueOwner.CurrentSettlement : null) != this._settlement)
				{
					return;
				}
			}
			this._areQuestsDirty = true;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000B945 File Offset: 0x00009B45
		private void OnIssueUpdated(IssueBase issue, IssueBase.IssueUpdateDetails details, Hero hero)
		{
			if (issue.IssueSettlement == this._settlement && issue.IssueQuest == null)
			{
				this._areQuestsDirty = true;
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000B964 File Offset: 0x00009B64
		private void OnQuestStarted(QuestBase quest)
		{
			if (this.IsQuestRelated(quest))
			{
				this._areQuestsDirty = true;
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000B976 File Offset: 0x00009B76
		private void OnQuestLogAdded(QuestBase quest, bool hideInformation)
		{
			if (this.IsQuestRelated(quest))
			{
				this._areQuestsDirty = true;
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000B988 File Offset: 0x00009B88
		private void OnQuestCompleted(QuestBase quest, QuestBase.QuestCompleteDetails details)
		{
			if (this.IsQuestRelated(quest))
			{
				this._areQuestsDirty = true;
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000B99A File Offset: 0x00009B9A
		private void OnSettlementEntered(MobileParty party, Settlement settlement, Hero hero)
		{
			if (settlement == this._settlement)
			{
				this._areQuestsDirty = true;
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000B9AC File Offset: 0x00009BAC
		private void OnSettlementLeft(MobileParty party, Settlement settlement)
		{
			if (settlement == this._settlement)
			{
				this._areQuestsDirty = true;
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000B9BE File Offset: 0x00009BBE
		private void OnHeroTakenPrisoner(PartyBase capturer, Hero prisoner)
		{
			if (prisoner.CurrentSettlement == this._settlement)
			{
				this._areQuestsDirty = true;
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000B9D8 File Offset: 0x00009BD8
		private void AddPrimaryProductionIcon()
		{
			string stringId = this._settlement.Village.VillageType.PrimaryProduction.StringId;
			string productionIconId = stringId.Contains("camel") ? "camel" : ((stringId.Contains("horse") || stringId.Contains("mule")) ? "horse" : stringId);
			this.EventsList.Add(new SettlementNameplateEventItemVM(productionIconId));
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000BA48 File Offset: 0x00009C48
		private void HandleIssueCount(int count, SettlementNameplateEventItemVM.SettlementEventType eventType)
		{
			SettlementNameplateEventItemVM settlementNameplateEventItemVM = this.EventsList.FirstOrDefault((SettlementNameplateEventItemVM e) => e.EventType == eventType);
			if (count > 0 && settlementNameplateEventItemVM == null)
			{
				this.EventsList.Add(new SettlementNameplateEventItemVM(eventType));
				return;
			}
			if (count == 0 && settlementNameplateEventItemVM != null)
			{
				this.EventsList.Remove(settlementNameplateEventItemVM);
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000BAAC File Offset: 0x00009CAC
		private bool IsQuestRelated(QuestBase quest)
		{
			IssueBase issueOfQuest = IssueManager.GetIssueOfQuest(quest);
			return (issueOfQuest != null && issueOfQuest.IssueSettlement == this._settlement) || this._relatedQuests.Contains(quest) || SandBoxUIHelper.IsQuestRelatedToSettlement(quest, this._settlement);
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000BAED File Offset: 0x00009CED
		// (set) Token: 0x0600024F RID: 591 RVA: 0x0000BAF5 File Offset: 0x00009CF5
		[DataSourceProperty]
		public MBBindingList<QuestMarkerVM> TrackQuests
		{
			get
			{
				return this._trackQuests;
			}
			set
			{
				if (value != this._trackQuests)
				{
					this._trackQuests = value;
					base.OnPropertyChangedWithValue<MBBindingList<QuestMarkerVM>>(value, "TrackQuests");
				}
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000BB13 File Offset: 0x00009D13
		// (set) Token: 0x06000251 RID: 593 RVA: 0x0000BB1B File Offset: 0x00009D1B
		public MBBindingList<SettlementNameplateEventItemVM> EventsList
		{
			get
			{
				return this._eventsList;
			}
			set
			{
				if (value != this._eventsList)
				{
					this._eventsList = value;
					base.OnPropertyChangedWithValue<MBBindingList<SettlementNameplateEventItemVM>>(value, "EventsList");
				}
			}
		}

		// Token: 0x0400011A RID: 282
		private List<QuestBase> _relatedQuests;

		// Token: 0x0400011B RID: 283
		private Settlement _settlement;

		// Token: 0x0400011C RID: 284
		private bool _areQuestsDirty;

		// Token: 0x0400011D RID: 285
		private MBBindingList<QuestMarkerVM> _trackQuests;

		// Token: 0x0400011E RID: 286
		private MBBindingList<SettlementNameplateEventItemVM> _eventsList;
	}
}
