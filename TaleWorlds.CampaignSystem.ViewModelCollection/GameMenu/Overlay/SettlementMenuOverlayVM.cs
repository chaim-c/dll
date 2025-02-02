using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Overlay;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.CampaignSystem.ViewModelCollection.Quests;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Core.ViewModelCollection.Tutorial;
using TaleWorlds.Library;
using TaleWorlds.Library.EventSystem;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.Overlay
{
	// Token: 0x020000AA RID: 170
	[MenuOverlay("SettlementMenuOverlay")]
	public class SettlementMenuOverlayVM : GameMenuOverlay
	{
		// Token: 0x060010EA RID: 4330 RVA: 0x00042E38 File Offset: 0x00041038
		public SettlementMenuOverlayVM(GameOverlays.MenuOverlayType type)
		{
			this._type = type;
			this._overlayTalkItem = null;
			base.IsInitializationOver = false;
			this._settlement = Settlement.CurrentSettlement;
			this.CharacterList = new MBBindingList<GameMenuPartyItemVM>();
			this.PartyList = new MBBindingList<GameMenuPartyItemVM>();
			this.IssueList = new MBBindingList<StringItemWithHintVM>();
			base.CurrentOverlayType = 0;
			this.CrimeHint = new BasicTooltipViewModel(() => this.GetCrimeTooltip());
			if (Settlement.CurrentSettlement.IsFortification)
			{
				this.RemainingFoodHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownFoodTooltip(this._settlement.Town));
				this.LoyaltyHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownLoyaltyTooltip(this._settlement.Town));
				this.MilitasHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownMilitiaTooltip(this._settlement.Town));
				this.ProsperityHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownProsperityTooltip(this._settlement.Town));
				this.WallsHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownWallsTooltip(this._settlement.Town));
				this.GarrisonHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownGarrisonTooltip(this._settlement.Town));
				this.SecurityHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetTownSecurityTooltip(this._settlement.Town));
			}
			else
			{
				this.MilitasHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetVillageMilitiaTooltip(this._settlement.Village));
				this.LoyaltyHint = new BasicTooltipViewModel();
				this.WallsHint = new BasicTooltipViewModel();
				this.ProsperityHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetVillageProsperityTooltip(this._settlement.Village));
			}
			this.UpdateSettlementOwnerBanner();
			this._contextMenuItem = null;
			base.IsInitializationOver = true;
			CampaignEvents.AfterSettlementEntered.AddNonSerializedListener(this, new Action<MobileParty, Settlement, Hero>(this.OnSettlementEntered));
			CampaignEvents.OnSettlementLeftEvent.AddNonSerializedListener(this, new Action<MobileParty, Settlement>(this.OnSettlementLeft));
			CampaignEvents.OnQuestCompletedEvent.AddNonSerializedListener(this, new Action<QuestBase, QuestBase.QuestCompleteDetails>(this.OnQuestCompleted));
			CampaignEvents.WarDeclared.AddNonSerializedListener(this, new Action<IFaction, IFaction, DeclareWarAction.DeclareWarDetail>(this.OnWarDeclared));
			CampaignEvents.MakePeace.AddNonSerializedListener(this, new Action<IFaction, IFaction, MakePeaceAction.MakePeaceDetail>(this.OnPeaceDeclared));
			CampaignEvents.OnSettlementOwnerChangedEvent.AddNonSerializedListener(this, new Action<Settlement, bool, Hero, Hero, Hero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail>(this.OnSettlementOwnerChanged));
			CampaignEvents.TownRebelliosStateChanged.AddNonSerializedListener(this, new Action<Town, bool>(this.OnTownRebelliousStateChanged));
			Game.Current.EventManager.RegisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
			this.RefreshValues();
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x00043081 File Offset: 0x00041281
		private List<TooltipProperty> GetCrimeTooltip()
		{
			Game game = Game.Current;
			if (game != null)
			{
				game.EventManager.TriggerEvent<SettlementMenuOverlayVM.CrimeValueInspectedInSettlementOverlayEvent>(new SettlementMenuOverlayVM.CrimeValueInspectedInSettlementOverlayEvent());
			}
			return CampaignUIHelper.GetCrimeTooltip(this._settlement);
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x000430A8 File Offset: 0x000412A8
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.PartyFilterHint = new HintViewModel(GameTexts.FindText("str_parties", null), null);
			this.CharacterFilterHint = new HintViewModel(GameTexts.FindText("str_characters", null), null);
			this.Refresh();
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x000430E4 File Offset: 0x000412E4
		protected override void ExecuteOnSetAsActiveContextMenuItem(GameMenuPartyItemVM troop)
		{
			base.ExecuteOnSetAsActiveContextMenuItem(troop);
			base.ContextList.Clear();
			this.IssueList.Clear();
			if (this._contextMenuItem.Character != null && (!this._contextMenuItem.Character.IsHero || !this._contextMenuItem.Character.HeroObject.IsPrisoner))
			{
				bool isEnabled = true;
				TextObject hint = TextObject.Empty;
				this._mostRecentOverlayTalkPermission = null;
				Game.Current.EventManager.TriggerEvent<SettlementMenuOverlayVM.SettlementOverlayTalkPermissionEvent>(new SettlementMenuOverlayVM.SettlementOverlayTalkPermissionEvent(this._contextMenuItem.Character.HeroObject, new Action<bool, TextObject>(this.OnSettlementOverlayTalkPermissionResult)));
				if (this._mostRecentOverlayTalkPermission != null)
				{
					isEnabled = this._mostRecentOverlayTalkPermission.Item1;
					hint = this._mostRecentOverlayTalkPermission.Item2;
				}
				this._overlayTalkItem = new GameMenuOverlayActionVM(new Action<object>(base.ExecuteTroopAction), GameTexts.FindText("str_menu_overlay_context_list", "Conversation").ToString(), isEnabled, GameMenuOverlay.MenuOverlayContextList.Conversation, hint);
				base.ContextList.Add(this._overlayTalkItem);
				bool isEnabled2 = true;
				TextObject hint2 = TextObject.Empty;
				this._mostRecentOverlayQuickTalkPermission = null;
				Game.Current.EventManager.TriggerEvent<SettlementMenuOverlayVM.SettlementOverylayQuickTalkPermissionEvent>(new SettlementMenuOverlayVM.SettlementOverylayQuickTalkPermissionEvent(new Action<bool, TextObject>(this.OnSettlementOverlayQuickTalkPermissionResult)));
				if (this._mostRecentOverlayQuickTalkPermission != null)
				{
					isEnabled2 = this._mostRecentOverlayQuickTalkPermission.Item1;
					hint2 = this._mostRecentOverlayQuickTalkPermission.Item2;
				}
				this._overlayQuickTalkItem = new GameMenuOverlayActionVM(new Action<object>(base.ExecuteTroopAction), GameTexts.FindText("str_menu_overlay_context_list", "QuickConversation").ToString(), isEnabled2, GameMenuOverlay.MenuOverlayContextList.QuickConversation, hint2);
				base.ContextList.Add(this._overlayQuickTalkItem);
				foreach (QuestMarkerVM questMarkerVM in troop.Quests)
				{
					if (questMarkerVM.IssueQuestFlag != CampaignUIHelper.IssueQuestFlags.None)
					{
						GameTexts.SetVariable("STR2", questMarkerVM.QuestTitle);
						string content = string.Empty;
						if (questMarkerVM.IssueQuestFlag == CampaignUIHelper.IssueQuestFlags.ActiveIssue)
						{
							content = "{=!}<img src=\"General\\Icons\\icon_issue_active_square\" extend=\"4\">";
						}
						else if (questMarkerVM.IssueQuestFlag == CampaignUIHelper.IssueQuestFlags.AvailableIssue)
						{
							content = "{=!}<img src=\"General\\Icons\\icon_issue_available_square\" extend=\"4\">";
						}
						else if (questMarkerVM.IssueQuestFlag == CampaignUIHelper.IssueQuestFlags.ActiveStoryQuest)
						{
							content = "{=!}<img src=\"General\\Icons\\icon_story_quest_active_square\" extend=\"4\">";
						}
						else if (questMarkerVM.IssueQuestFlag == CampaignUIHelper.IssueQuestFlags.TrackedIssue)
						{
							content = "{=!}<img src=\"General\\Icons\\issue_target_icon\" extend=\"4\">";
						}
						else if (questMarkerVM.IssueQuestFlag == CampaignUIHelper.IssueQuestFlags.TrackedStoryQuest)
						{
							content = "{=!}<img src=\"General\\Icons\\quest_target_icon\" extend=\"4\">";
						}
						GameTexts.SetVariable("STR1", content);
						string text = GameTexts.FindText("str_STR1_STR2", null).ToString();
						this.IssueList.Add(new StringItemWithHintVM(text, questMarkerVM.QuestHint.HintText));
					}
				}
				if (this._contextMenuItem.Character.IsHero)
				{
					MobileParty partyBelongedTo = this._contextMenuItem.Character.HeroObject.PartyBelongedTo;
					if (((partyBelongedTo != null) ? partyBelongedTo.Army : null) != null && this._contextMenuItem.Character.HeroObject.PartyBelongedTo.Army.LeaderParty == this._contextMenuItem.Character.HeroObject.PartyBelongedTo && MobileParty.MainParty.Army == null && FactionManager.IsAlliedWithFaction(this._contextMenuItem.Character.HeroObject.MapFaction, Hero.MainHero.MapFaction))
					{
						GameMenuOverlayActionVM item = new GameMenuOverlayActionVM(new Action<object>(base.ExecuteTroopAction), GameTexts.FindText("str_menu_overlay_context_list", "JoinArmy").ToString(), true, GameMenuOverlay.MenuOverlayContextList.JoinArmy, null);
						base.ContextList.Add(item);
					}
				}
				if (this._contextMenuItem.Character.IsHero && this._contextMenuItem.Character.HeroObject.PartyBelongedTo == null && this._contextMenuItem.Character.HeroObject.Clan == Clan.PlayerClan && this._contextMenuItem.Character.HeroObject.Age > (float)Campaign.Current.Models.AgeModel.HeroComesOfAge && !Campaign.Current.GetCampaignBehavior<IAlleyCampaignBehavior>().IsHeroAlleyLeaderOfAnyPlayerAlley(this._contextMenuItem.Character.HeroObject))
				{
					GameMenuOverlayActionVM item2 = new GameMenuOverlayActionVM(new Action<object>(base.ExecuteTroopAction), GameTexts.FindText("str_menu_overlay_context_list", "TakeToParty").ToString(), true, GameMenuOverlay.MenuOverlayContextList.TakeToParty, null);
					base.ContextList.Add(item2);
				}
				CampaignEventDispatcher.Instance.OnCharacterPortraitPopUpOpened(this._contextMenuItem.Character);
				return;
			}
			if (this._contextMenuItem.Party != null)
			{
				Hero owner = this._contextMenuItem.Party.Owner;
				if (((owner != null) ? owner.Clan : null) == Hero.MainHero.Clan)
				{
					MobileParty mobileParty = this._contextMenuItem.Party.MobileParty;
					if (mobileParty != null && !mobileParty.IsMainParty)
					{
						MobileParty mobileParty2 = this._contextMenuItem.Party.MobileParty;
						if (mobileParty2 != null && mobileParty2.IsGarrison)
						{
							this._overlayTalkItem = new GameMenuOverlayActionVM(new Action<object>(base.ExecuteTroopAction), GameTexts.FindText("str_menu_overlay_context_list", "ManageGarrison").ToString(), true, GameMenuOverlay.MenuOverlayContextList.ManageGarrison, null);
							base.ContextList.Add(this._overlayTalkItem);
							goto IL_65B;
						}
					}
				}
				if (this._contextMenuItem.Party.MapFaction == Hero.MainHero.MapFaction)
				{
					MobileParty mobileParty3 = this._contextMenuItem.Party.MobileParty;
					if (mobileParty3 != null && !mobileParty3.IsMainParty && (this._contextMenuItem.Party.MobileParty == null || (!this._contextMenuItem.Party.MobileParty.IsVillager && !this._contextMenuItem.Party.MobileParty.IsCaravan && !this._contextMenuItem.Party.MobileParty.IsMilitia)))
					{
						if (this._contextMenuItem.Party.MobileParty.ActualClan == Clan.PlayerClan)
						{
							this._overlayTalkItem = new GameMenuOverlayActionVM(new Action<object>(base.ExecuteTroopAction), GameTexts.FindText("str_menu_overlay_context_list", "ManageTroops").ToString(), true, GameMenuOverlay.MenuOverlayContextList.ManageTroops, null);
							base.ContextList.Add(this._overlayTalkItem);
						}
						else
						{
							this._overlayTalkItem = new GameMenuOverlayActionVM(new Action<object>(base.ExecuteTroopAction), GameTexts.FindText("str_menu_overlay_context_list", "DonateTroops").ToString(), true, GameMenuOverlay.MenuOverlayContextList.DonateTroops, null);
							base.ContextList.Add(this._overlayTalkItem);
						}
					}
				}
				IL_65B:
				if (this._contextMenuItem.Party.LeaderHero != null && this._contextMenuItem.Party.LeaderHero != Hero.MainHero)
				{
					bool flag = this.CharacterList.Any((GameMenuPartyItemVM c) => c.Character == this._contextMenuItem.Party.LeaderHero.CharacterObject);
					TextObject hintText = (!flag) ? GameTexts.FindText("str_menu_overlay_cant_talk_to_party_leader", null) : TextObject.Empty;
					base.ContextList.Add(new StringItemWithEnabledAndHintVM(new Action<object>(base.ExecuteTroopAction), GameTexts.FindText("str_menu_overlay_context_list", "ConverseWithLeader").ToString(), flag, GameMenuOverlay.MenuOverlayContextList.ConverseWithLeader, hintText));
				}
				CharacterObject visualPartyLeader = CampaignUIHelper.GetVisualPartyLeader(this._contextMenuItem.Party);
				if (visualPartyLeader != null)
				{
					CampaignEventDispatcher.Instance.OnCharacterPortraitPopUpOpened(visualPartyLeader);
				}
			}
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x00043818 File Offset: 0x00041A18
		private void OnSettlementOverlayTalkPermissionResult(bool isAvailable, TextObject reasonStr)
		{
			this._mostRecentOverlayTalkPermission = new Tuple<bool, TextObject>(isAvailable, reasonStr);
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x00043827 File Offset: 0x00041A27
		private void OnSettlementOverlayQuickTalkPermissionResult(bool isAvailable, TextObject reasonStr)
		{
			this._mostRecentOverlayQuickTalkPermission = new Tuple<bool, TextObject>(isAvailable, reasonStr);
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x00043836 File Offset: 0x00041A36
		private void OnSettlementOverlayLeaveCharacterPermissionResult(bool isAvailable, TextObject reasonStr)
		{
			this._mostRecentOverlayLeaveCharacterPermission = new Tuple<bool, TextObject>(isAvailable, reasonStr);
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x00043845 File Offset: 0x00041A45
		public override void ExecuteOnOverlayClosed()
		{
			base.ExecuteOnOverlayClosed();
			this.InitLists();
			base.ContextList.Clear();
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x0004385E File Offset: 0x00041A5E
		private void ExecuteCloseTooltip()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x00043865 File Offset: 0x00041A65
		private void ExecuteOpenTooltip()
		{
			InformationManager.ShowTooltip(typeof(Settlement), new object[]
			{
				this._settlement,
				true
			});
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0004388E File Offset: 0x00041A8E
		private void ExecuteSettlementLink()
		{
			Campaign.Current.EncyclopediaManager.GoToLink(this._settlement.EncyclopediaLink);
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x000438AC File Offset: 0x00041AAC
		private bool Contains(MBBindingList<GameMenuPartyItemVM> list, CharacterObject character)
		{
			using (IEnumerator<GameMenuPartyItemVM> enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Character == character)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x000438FC File Offset: 0x00041AFC
		public override void UpdateOverlayType(GameOverlays.MenuOverlayType newType)
		{
			this._type = newType;
			base.UpdateOverlayType(newType);
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x0004390C File Offset: 0x00041B0C
		private void InitLists()
		{
			this.UpdateCharacterList();
			this.UpdatePartyList();
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x0004391C File Offset: 0x00041B1C
		private void UpdateCharacterList()
		{
			if (this._type == GameOverlays.MenuOverlayType.SettlementWithCharacters || this._type == GameOverlays.MenuOverlayType.SettlementWithBoth)
			{
				Dictionary<Hero, bool> dictionary = new Dictionary<Hero, bool>();
				foreach (LocationCharacter locationCharacter in Campaign.Current.GameMenuManager.MenuLocations.SelectMany((Location l) => l.GetCharacterList()))
				{
					if (this.WillBeListed(locationCharacter) && !dictionary.ContainsKey(locationCharacter.Character.HeroObject))
					{
						dictionary.Add(locationCharacter.Character.HeroObject, locationCharacter.UseCivilianEquipment);
					}
				}
				for (int i = this.CharacterList.Count - 1; i >= 0; i--)
				{
					GameMenuPartyItemVM gameMenuPartyItemVM = this.CharacterList[i];
					if (!dictionary.ContainsKey(gameMenuPartyItemVM.Character.HeroObject))
					{
						this.CharacterList.RemoveAt(i);
					}
				}
				using (Dictionary<Hero, bool>.Enumerator enumerator2 = dictionary.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						KeyValuePair<Hero, bool> heroKvp = enumerator2.Current;
						if (!this.CharacterList.Any((GameMenuPartyItemVM x) => x.Character == heroKvp.Key.CharacterObject))
						{
							GameMenuPartyItemVM item = new GameMenuPartyItemVM(new Action<GameMenuPartyItemVM>(this.ExecuteOnSetAsActiveContextMenuItem), heroKvp.Key.CharacterObject, heroKvp.Value);
							this.CharacterList.Add(item);
						}
					}
				}
				this.CharacterList.Sort(new SettlementMenuOverlayVM.CharacterComparer());
				return;
			}
			this.CharacterList.Clear();
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00043ADC File Offset: 0x00041CDC
		private void UpdatePartyList()
		{
			if (this._type == GameOverlays.MenuOverlayType.SettlementWithBoth || this._type == GameOverlays.MenuOverlayType.SettlementWithParties)
			{
				SettlementMenuOverlayVM.<>c__DisplayClass22_0 CS$<>8__locals1 = new SettlementMenuOverlayVM.<>c__DisplayClass22_0();
				Settlement settlement = MobileParty.MainParty.CurrentSettlement ?? MobileParty.MainParty.LastVisitedSettlement;
				CS$<>8__locals1.partiesInSettlement = new List<MobileParty>();
				foreach (MobileParty mobileParty in settlement.Parties)
				{
					if (this.WillBeListed(mobileParty))
					{
						CS$<>8__locals1.partiesInSettlement.Add(mobileParty);
					}
				}
				for (int j = this.PartyList.Count - 1; j >= 0; j--)
				{
					GameMenuPartyItemVM gameMenuPartyItemVM = this.PartyList[j];
					if (!CS$<>8__locals1.partiesInSettlement.Contains(gameMenuPartyItemVM.Party.MobileParty))
					{
						this.PartyList.RemoveAt(j);
					}
				}
				int i2;
				int i;
				for (i = 0; i < CS$<>8__locals1.partiesInSettlement.Count; i = i2 + 1)
				{
					if (!this.PartyList.Any((GameMenuPartyItemVM x) => x.Party == CS$<>8__locals1.partiesInSettlement[i].Party))
					{
						GameMenuPartyItemVM item = new GameMenuPartyItemVM(new Action<GameMenuPartyItemVM>(this.ExecuteOnSetAsActiveContextMenuItem), CS$<>8__locals1.partiesInSettlement[i].Party, false);
						this.PartyList.Add(item);
					}
					i2 = i;
				}
				this.PartyList.Sort(new SettlementMenuOverlayVM.PartyComparer());
				return;
			}
			this.PartyList.Clear();
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x00043C84 File Offset: 0x00041E84
		private void UpdateList<TListItem, TElement>(MBBindingList<TListItem> listToUpdate, IEnumerable<TElement> listInSettlement, IComparer<TListItem> comparer, Func<TListItem, TElement> getElementFromListItem, Func<TElement, bool> doesSettlementHasElement, Func<TElement, TListItem> createListItem)
		{
			HashSet<TElement> hashSet = new HashSet<TElement>();
			for (int i = 0; i < listToUpdate.Count; i++)
			{
				TListItem arg = listToUpdate[i];
				TElement telement = getElementFromListItem(arg);
				if (doesSettlementHasElement(telement))
				{
					hashSet.Add(telement);
				}
				else
				{
					listToUpdate.RemoveAt(i);
					i--;
				}
			}
			foreach (TElement telement2 in listInSettlement)
			{
				if (!hashSet.Contains(telement2))
				{
					listToUpdate.Add(createListItem(telement2));
					hashSet.Add(telement2);
				}
			}
			listToUpdate.Sort(comparer);
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x00043D3C File Offset: 0x00041F3C
		private bool WillBeListed(MobileParty mobileParty)
		{
			return mobileParty != null && mobileParty.IsActive;
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x00043D4C File Offset: 0x00041F4C
		private bool WillBeListed(LocationCharacter locationCharacter)
		{
			Settlement settlement = (MobileParty.MainParty.CurrentSettlement != null) ? MobileParty.MainParty.CurrentSettlement : MobileParty.MainParty.LastVisitedSettlement;
			return locationCharacter.Character.IsHero && !locationCharacter.IsHidden && locationCharacter.Character.HeroObject.PartyBelongedTo != MobileParty.MainParty && locationCharacter.Character.HeroObject.CurrentSettlement == settlement;
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x00043DC0 File Offset: 0x00041FC0
		private bool WillBeListed(CharacterObject character)
		{
			Settlement settlement = (MobileParty.MainParty.CurrentSettlement != null) ? MobileParty.MainParty.CurrentSettlement : MobileParty.MainParty.LastVisitedSettlement;
			return character.IsHero && character.HeroObject.PartyBelongedTo != MobileParty.MainParty && character.HeroObject.CurrentSettlement == settlement;
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x00043E1C File Offset: 0x0004201C
		private void UpdateSettlementOwnerBanner()
		{
			Banner banner = null;
			IFaction mapFaction = this._settlement.MapFaction;
			if (mapFaction != null && mapFaction.IsKingdomFaction && ((Kingdom)this._settlement.MapFaction).RulingClan == this._settlement.OwnerClan)
			{
				banner = this._settlement.OwnerClan.Kingdom.Banner;
			}
			else
			{
				Clan ownerClan = this._settlement.OwnerClan;
				if (((ownerClan != null) ? ownerClan.Banner : null) != null)
				{
					banner = this._settlement.OwnerClan.Banner;
				}
			}
			if (banner != null)
			{
				this.SettlementOwnerBanner = new ImageIdentifierVM(BannerCode.CreateFrom(banner), true);
				return;
			}
			this.SettlementOwnerBanner = new ImageIdentifierVM(ImageIdentifierType.Null);
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x00043ECC File Offset: 0x000420CC
		private void UpdateProperties()
		{
			Settlement currentSettlement = (MobileParty.MainParty.CurrentSettlement != null) ? MobileParty.MainParty.CurrentSettlement : MobileParty.MainParty.LastVisitedSettlement;
			this.IsFortification = currentSettlement.IsFortification;
			IFaction mapFaction = currentSettlement.MapFaction;
			this.IsCrimeEnabled = (mapFaction != null && mapFaction.MainHeroCrimeRating > 0f);
			IFaction mapFaction2 = currentSettlement.MapFaction;
			this.CrimeLbl = ((int)((mapFaction2 != null) ? new float?(mapFaction2.MainHeroCrimeRating) : null).Value).ToString();
			IFaction mapFaction3 = currentSettlement.MapFaction;
			this.CrimeChangeAmount = (int)((mapFaction3 != null) ? new float?(mapFaction3.DailyCrimeRatingChange) : null).Value;
			this.RemainingFoodText = (currentSettlement.IsFortification ? ((int)currentSettlement.Town.FoodStocks).ToString() : "-");
			this.FoodChangeAmount = ((currentSettlement.Town != null) ? ((int)currentSettlement.Town.FoodChange) : 0);
			this.MilitasLbl = ((int)currentSettlement.Militia).ToString();
			this.MilitiaChangeAmount = ((currentSettlement.Town != null) ? ((int)currentSettlement.Town.MilitiaChange) : ((int)currentSettlement.Village.MilitiaChange));
			this.IsLoyaltyRebellionWarning = (currentSettlement.IsTown && currentSettlement.Town.Loyalty < (float)Campaign.Current.Models.SettlementLoyaltyModel.RebelliousStateStartLoyaltyThreshold);
			if (currentSettlement.IsFortification)
			{
				MobileParty garrisonParty = currentSettlement.Town.GarrisonParty;
				this.GarrisonAmount = ((garrisonParty != null) ? garrisonParty.Party.NumberOfAllMembers : 0);
				this.IsNoGarrisonWarning = (this.GarrisonAmount < 1);
			}
			if (currentSettlement.IsFortification)
			{
				MobileParty garrisonParty2 = currentSettlement.Town.GarrisonParty;
				this.GarrisonLbl = (((garrisonParty2 != null) ? garrisonParty2.Party.NumberOfAllMembers.ToString() : null) ?? "0");
				this.GarrisonChangeAmount = currentSettlement.Town.GarrisonChange;
				this.WallsLbl = currentSettlement.Town.GetWallLevel().ToString();
				this.WallsLevel = currentSettlement.Town.GetWallLevel();
			}
			else
			{
				this.GarrisonChangeAmount = 0;
				this.WallsLbl = "-";
				this.GarrisonLbl = "-";
				this.WallsLevel = 1;
			}
			if (currentSettlement.IsFortification)
			{
				this.ProsperityLbl = ((int)currentSettlement.Town.Prosperity).ToString();
				this.ProsperityChangeAmount = (int)currentSettlement.Town.ProsperityChange;
			}
			else
			{
				this.ProsperityLbl = ((int)currentSettlement.Village.Hearth).ToString();
				this.ProsperityChangeAmount = (int)currentSettlement.Village.HearthChange;
			}
			this.SettlementNameLbl = currentSettlement.Name + ((currentSettlement.IsVillage && currentSettlement.Village.VillageState != Village.VillageStates.Normal) ? ("(" + currentSettlement.Village.VillageState.ToString() + ")") : "");
			if (currentSettlement.IsFortification)
			{
				this.LoyaltyLbl = ((int)currentSettlement.Town.Loyalty).ToString();
				this.LoyaltyChangeAmount = (int)currentSettlement.Town.LoyaltyChange;
				this.SecurityLbl = ((int)currentSettlement.Town.Security).ToString();
				this.SecurityChangeAmount = (int)currentSettlement.Town.SecurityChange;
			}
			else
			{
				this.LoyaltyChangeAmount = 0;
				this.LoyaltyLbl = "-";
				this.SecurityChangeAmount = 0;
				this.SecurityLbl = "-";
			}
			Game.Current.EventManager.TriggerEvent<SettlementMenuOverlayVM.SettlementOverlayLeaveCharacterPermissionEvent>(new SettlementMenuOverlayVM.SettlementOverlayLeaveCharacterPermissionEvent(new Action<bool, TextObject>(this.OnSettlementOverlayLeaveCharacterPermissionResult)));
			if (this._mostRecentOverlayLeaveCharacterPermission != null)
			{
				this.CanLeaveMembers = this._mostRecentOverlayLeaveCharacterPermission.Item1;
				this.LeaveMembersHint = (this.CanLeaveMembers ? new HintViewModel(new TextObject("{=aGFxIvqx}Leave Member(s)", null), null) : new HintViewModel(this._mostRecentOverlayLeaveCharacterPermission.Item2, null));
				return;
			}
			this.CanLeaveMembers = Clan.PlayerClan.Heroes.Any((Hero hero) => currentSettlement == hero.StayingInSettlement || (!hero.CharacterObject.IsPlayerCharacter && MobileParty.MainParty.MemberRoster.Contains(hero.CharacterObject)));
			if (!this.CanLeaveMembers)
			{
				this.LeaveMembersHint = new HintViewModel(new TextObject("{=d2K6gMsZ}Leave members. Need at least 1 companion.", null), null);
				return;
			}
			this.LeaveMembersHint = new HintViewModel(new TextObject("{=aGFxIvqx}Leave Member(s)", null), null);
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x000443D4 File Offset: 0x000425D4
		private void OnTutorialNotificationElementIDChange(TutorialNotificationElementChangeEvent obj)
		{
			this._latestTutorialElementID = obj.NewNotificationElementID;
			if (this._latestTutorialElementID != null)
			{
				if (this._latestTutorialElementID != "")
				{
					if (this._latestTutorialElementID == "ApplicapleCompanion" && !this._isCompanionHighlightApplied)
					{
						this._isCompanionHighlightApplied = this.SetPartyItemHighlightState(this._latestTutorialElementID, true);
					}
					else if (this._latestTutorialElementID != "ApplicapleCompanion" && this._isCompanionHighlightApplied)
					{
						this._isCompanionHighlightApplied = this.SetPartyItemHighlightState("ApplicapleCompanion", false);
					}
					if (this._latestTutorialElementID == "ApplicableQuestGivers" && !this._isQuestGiversHighlightApplied)
					{
						this._isQuestGiversHighlightApplied = this.SetPartyItemHighlightState(this._latestTutorialElementID, true);
					}
					else if (this._latestTutorialElementID != "ApplicableQuestGivers" && this._isQuestGiversHighlightApplied)
					{
						this._isCompanionHighlightApplied = this.SetPartyItemHighlightState("ApplicableQuestGivers", false);
					}
					if (this._latestTutorialElementID == "ApplicableNotable" && !this._isNotableHighlightApplied)
					{
						this._isNotableHighlightApplied = this.SetPartyItemHighlightState(this._latestTutorialElementID, true);
					}
					else if (this._latestTutorialElementID != "ApplicableNotable" && this._isNotableHighlightApplied)
					{
						this._isNotableHighlightApplied = this.SetPartyItemHighlightState("ApplicableNotable", false);
					}
					if (this._latestTutorialElementID == "CrimeLabel" && !this.IsCrimeLabelHighlightEnabled)
					{
						this.IsCrimeLabelHighlightEnabled = true;
					}
					else if (this._latestTutorialElementID != "CrimeLabel" && this.IsCrimeLabelHighlightEnabled)
					{
						this.IsCrimeLabelHighlightEnabled = false;
					}
					if (this._latestTutorialElementID == "OverlayTalkButton" && !this._isTalkItemHighlightApplied)
					{
						if (this._overlayTalkItem != null)
						{
							this._overlayTalkItem.IsHiglightEnabled = true;
							this._isTalkItemHighlightApplied = true;
							return;
						}
					}
					else if (this._latestTutorialElementID != "OverlayTalkButton" && this._isTalkItemHighlightApplied && this._overlayTalkItem != null)
					{
						this._overlayTalkItem.IsHiglightEnabled = false;
						this._isTalkItemHighlightApplied = true;
						return;
					}
				}
				else
				{
					if (this._isCompanionHighlightApplied)
					{
						this._isCompanionHighlightApplied = !this.SetPartyItemHighlightState("ApplicapleCompanion", false);
					}
					if (this._isNotableHighlightApplied)
					{
						this._isNotableHighlightApplied = !this.SetPartyItemHighlightState("ApplicableNotable", false);
					}
					if (this._isQuestGiversHighlightApplied)
					{
						this._isQuestGiversHighlightApplied = !this.SetPartyItemHighlightState("ApplicableQuestGivers", false);
					}
					if (this.IsCrimeLabelHighlightEnabled)
					{
						this.IsCrimeLabelHighlightEnabled = false;
					}
					if (this._isTalkItemHighlightApplied && this._overlayTalkItem != null)
					{
						this._overlayTalkItem.IsHiglightEnabled = false;
						this._isTalkItemHighlightApplied = false;
						return;
					}
				}
			}
			else
			{
				if (this._isCompanionHighlightApplied)
				{
					this._isCompanionHighlightApplied = !this.SetPartyItemHighlightState("ApplicapleCompanion", false);
				}
				if (this._isNotableHighlightApplied)
				{
					this._isNotableHighlightApplied = !this.SetPartyItemHighlightState("ApplicableNotable", false);
				}
				if (this._isQuestGiversHighlightApplied)
				{
					this._isQuestGiversHighlightApplied = !this.SetPartyItemHighlightState("ApplicableQuestGivers", false);
				}
				if (this._isTalkItemHighlightApplied && this._overlayTalkItem != null)
				{
					this._overlayTalkItem.IsHiglightEnabled = false;
					this._isTalkItemHighlightApplied = false;
				}
				if (this.IsCrimeLabelHighlightEnabled)
				{
					this.IsCrimeLabelHighlightEnabled = false;
				}
			}
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x000446F4 File Offset: 0x000428F4
		private bool SetPartyItemHighlightState(string condition, bool state)
		{
			bool result = false;
			foreach (GameMenuPartyItemVM gameMenuPartyItemVM in this.CharacterList)
			{
				if (condition == "ApplicapleCompanion" && gameMenuPartyItemVM.Character.IsHero && gameMenuPartyItemVM.Character.HeroObject.IsWanderer && !gameMenuPartyItemVM.Character.HeroObject.IsPlayerCompanion)
				{
					gameMenuPartyItemVM.IsHighlightEnabled = state;
					result = true;
				}
				else if (condition == "ApplicableNotable" && gameMenuPartyItemVM.Character.IsHero && gameMenuPartyItemVM.Character.HeroObject.IsNotable && !gameMenuPartyItemVM.Character.HeroObject.IsPlayerCompanion)
				{
					gameMenuPartyItemVM.IsHighlightEnabled = state;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x000447D4 File Offset: 0x000429D4
		public override void Refresh()
		{
			base.IsInitializationOver = false;
			this.InitLists();
			this.UpdateProperties();
			foreach (GameMenuPartyItemVM gameMenuPartyItemVM in this.CharacterList)
			{
				gameMenuPartyItemVM.RefreshProperties();
			}
			foreach (GameMenuPartyItemVM gameMenuPartyItemVM2 in this.PartyList)
			{
				gameMenuPartyItemVM2.RefreshProperties();
			}
			base.IsInitializationOver = true;
			base.Refresh();
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00044878 File Offset: 0x00042A78
		public void ExecuteAddCompanion()
		{
			List<InquiryElement> list = new List<InquiryElement>();
			foreach (TroopRosterElement troopRosterElement in from m in MobileParty.MainParty.MemberRoster.GetTroopRoster()
			where m.Character.IsHero && m.Character.HeroObject.CanMoveToSettlement()
			select m)
			{
				if (!troopRosterElement.Character.IsPlayerCharacter)
				{
					list.Add(new InquiryElement(troopRosterElement.Character.HeroObject, troopRosterElement.Character.Name.ToString(), new ImageIdentifier(CampaignUIHelper.GetCharacterCode(troopRosterElement.Character, false))));
				}
			}
			MBInformationManager.ShowMultiSelectionInquiry(new MultiSelectionInquiryData(new TextObject("{=aGFxIvqx}Leave Member(s)", null).ToString(), string.Empty, list, true, 1, 0, new TextObject("{=FBYFcrWo}Leave in settlement", null).ToString(), new TextObject("{=3CpNUnVl}Cancel", null).ToString(), new Action<List<InquiryElement>>(this.OnLeaveMembersInSettlement), new Action<List<InquiryElement>>(this.OnLeaveMembersInSettlement), "", false), false, false);
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x00044998 File Offset: 0x00042B98
		private void OnLeaveMembersInSettlement(List<InquiryElement> leftMembers)
		{
			Settlement settlement = (MobileParty.MainParty.CurrentSettlement != null) ? MobileParty.MainParty.CurrentSettlement : MobileParty.MainParty.LastVisitedSettlement;
			foreach (InquiryElement inquiryElement in leftMembers)
			{
				Hero hero = inquiryElement.Identifier as Hero;
				PartyBase.MainParty.MemberRoster.RemoveTroop(hero.CharacterObject, 1, default(UniqueTroopDescriptor), 0);
				if (hero.CharacterObject.IsHero && !settlement.HeroesWithoutParty.Contains(hero.CharacterObject.HeroObject))
				{
					EnterSettlementAction.ApplyForCharacterOnly(hero.CharacterObject.HeroObject, settlement);
				}
			}
			if (leftMembers.Count > 0)
			{
				this.InitLists();
			}
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00044A74 File Offset: 0x00042C74
		public override void OnFinalize()
		{
			base.OnFinalize();
			CampaignEvents.AfterSettlementEntered.ClearListeners(this);
			CampaignEvents.OnSettlementLeftEvent.ClearListeners(this);
			CampaignEvents.OnQuestCompletedEvent.ClearListeners(this);
			CampaignEvents.WarDeclared.ClearListeners(this);
			CampaignEvents.MakePeace.ClearListeners(this);
			CampaignEvents.OnSettlementOwnerChangedEvent.ClearListeners(this);
			CampaignEvents.TownRebelliosStateChanged.ClearListeners(this);
			Game.Current.EventManager.UnregisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x00044AF0 File Offset: 0x00042CF0
		private void OnSettlementEntered(MobileParty arg1, Settlement arg2, Hero arg3)
		{
			Settlement settlement = (MobileParty.MainParty.CurrentSettlement != null) ? MobileParty.MainParty.CurrentSettlement : MobileParty.MainParty.LastVisitedSettlement;
			if (arg2 == settlement)
			{
				this.InitLists();
			}
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x00044B2C File Offset: 0x00042D2C
		private void OnSettlementLeft(MobileParty arg1, Settlement arg2)
		{
			Settlement settlement = (MobileParty.MainParty.CurrentSettlement != null) ? MobileParty.MainParty.CurrentSettlement : MobileParty.MainParty.LastVisitedSettlement;
			if (arg2 == settlement)
			{
				this.InitLists();
			}
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x00044B68 File Offset: 0x00042D68
		private void OnQuestCompleted(QuestBase arg1, QuestBase.QuestCompleteDetails arg2)
		{
			Settlement settlement = (MobileParty.MainParty.CurrentSettlement != null) ? MobileParty.MainParty.CurrentSettlement : MobileParty.MainParty.LastVisitedSettlement;
			Hero questGiver = arg1.QuestGiver;
			if (((questGiver != null) ? questGiver.CurrentSettlement : null) != null && arg1.QuestGiver.CurrentSettlement == settlement)
			{
				this.Refresh();
			}
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x00044BC0 File Offset: 0x00042DC0
		private void OnPeaceDeclared(IFaction faction1, IFaction faction2, MakePeaceAction.MakePeaceDetail detail)
		{
			this.OnPeaceOrWarDeclared(faction1, faction2);
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00044BCA File Offset: 0x00042DCA
		private void OnWarDeclared(IFaction faction1, IFaction faction2, DeclareWarAction.DeclareWarDetail reason)
		{
			this.OnPeaceOrWarDeclared(faction1, faction2);
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x00044BD4 File Offset: 0x00042DD4
		private void OnPeaceOrWarDeclared(IFaction arg1, IFaction arg2)
		{
			Hero mainHero = Hero.MainHero;
			bool flag;
			if (mainHero == null)
			{
				flag = (null != null);
			}
			else
			{
				Settlement currentSettlement = mainHero.CurrentSettlement;
				flag = (((currentSettlement != null) ? currentSettlement.MapFaction : null) != null);
			}
			bool flag2;
			if (flag)
			{
				Hero mainHero2 = Hero.MainHero;
				if (((mainHero2 != null) ? mainHero2.CurrentSettlement.MapFaction : null) != arg1)
				{
					Hero mainHero3 = Hero.MainHero;
					flag2 = (((mainHero3 != null) ? mainHero3.CurrentSettlement.MapFaction : null) == arg2);
				}
				else
				{
					flag2 = true;
				}
			}
			else
			{
				flag2 = false;
			}
			Hero mainHero4 = Hero.MainHero;
			bool flag3;
			if (((mainHero4 != null) ? mainHero4.MapFaction : null) != arg1)
			{
				Hero mainHero5 = Hero.MainHero;
				flag3 = (((mainHero5 != null) ? mainHero5.MapFaction : null) == arg2);
			}
			else
			{
				flag3 = true;
			}
			bool flag4 = flag3;
			if (flag2 || flag4)
			{
				this.InitLists();
			}
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x00044C6E File Offset: 0x00042E6E
		private void OnSettlementOwnerChanged(Settlement settlement, bool openToClaim, Hero newOwner, Hero previousOwner, Hero capturerHero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail detail)
		{
			if (settlement == this._settlement || (this._settlement.IsVillage && settlement.BoundVillages.Contains(this._settlement.Village)))
			{
				this.UpdateSettlementOwnerBanner();
			}
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x00044CA4 File Offset: 0x00042EA4
		private void OnTownRebelliousStateChanged(Town town, bool isRebellious)
		{
			if (this._settlement.IsTown && this._settlement.Town == town)
			{
				this.IsLoyaltyRebellionWarning = (isRebellious || town.Loyalty < (float)Campaign.Current.Models.SettlementLoyaltyModel.RebelliousStateStartLoyaltyThreshold);
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x0600110E RID: 4366 RVA: 0x00044CF5 File Offset: 0x00042EF5
		// (set) Token: 0x0600110F RID: 4367 RVA: 0x00044CFD File Offset: 0x00042EFD
		[DataSourceProperty]
		public string RemainingFoodText
		{
			get
			{
				return this._remainingFoodText;
			}
			set
			{
				if (value != this._remainingFoodText)
				{
					this._remainingFoodText = value;
					base.OnPropertyChangedWithValue<string>(value, "RemainingFoodText");
				}
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x00044D20 File Offset: 0x00042F20
		// (set) Token: 0x06001111 RID: 4369 RVA: 0x00044D28 File Offset: 0x00042F28
		[DataSourceProperty]
		public int ProsperityChangeAmount
		{
			get
			{
				return this._prosperityChangeAmount;
			}
			set
			{
				if (value != this._prosperityChangeAmount)
				{
					this._prosperityChangeAmount = value;
					base.OnPropertyChangedWithValue(value, "ProsperityChangeAmount");
				}
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x00044D46 File Offset: 0x00042F46
		// (set) Token: 0x06001113 RID: 4371 RVA: 0x00044D4E File Offset: 0x00042F4E
		[DataSourceProperty]
		public int MilitiaChangeAmount
		{
			get
			{
				return this._militiaChangeAmount;
			}
			set
			{
				if (value != this._militiaChangeAmount)
				{
					this._militiaChangeAmount = value;
					base.OnPropertyChangedWithValue(value, "MilitiaChangeAmount");
				}
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x00044D6C File Offset: 0x00042F6C
		// (set) Token: 0x06001115 RID: 4373 RVA: 0x00044D74 File Offset: 0x00042F74
		[DataSourceProperty]
		public int GarrisonChangeAmount
		{
			get
			{
				return this._garrisonChangeAmount;
			}
			set
			{
				if (value != this._garrisonChangeAmount)
				{
					this._garrisonChangeAmount = value;
					base.OnPropertyChangedWithValue(value, "GarrisonChangeAmount");
				}
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x00044D92 File Offset: 0x00042F92
		// (set) Token: 0x06001117 RID: 4375 RVA: 0x00044D9A File Offset: 0x00042F9A
		[DataSourceProperty]
		public int GarrisonAmount
		{
			get
			{
				return this._garrisonAmount;
			}
			set
			{
				if (value != this._garrisonAmount)
				{
					this._garrisonAmount = value;
					base.OnPropertyChangedWithValue(value, "GarrisonAmount");
				}
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x00044DB8 File Offset: 0x00042FB8
		// (set) Token: 0x06001119 RID: 4377 RVA: 0x00044DC0 File Offset: 0x00042FC0
		[DataSourceProperty]
		public int CrimeChangeAmount
		{
			get
			{
				return this._crimeChangeAmount;
			}
			set
			{
				if (value != this._crimeChangeAmount)
				{
					this._crimeChangeAmount = value;
					base.OnPropertyChangedWithValue(value, "CrimeChangeAmount");
				}
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x0600111A RID: 4378 RVA: 0x00044DDE File Offset: 0x00042FDE
		// (set) Token: 0x0600111B RID: 4379 RVA: 0x00044DE6 File Offset: 0x00042FE6
		[DataSourceProperty]
		public int LoyaltyChangeAmount
		{
			get
			{
				return this._loyaltyChangeAmount;
			}
			set
			{
				if (value != this._loyaltyChangeAmount)
				{
					this._loyaltyChangeAmount = value;
					base.OnPropertyChangedWithValue(value, "LoyaltyChangeAmount");
				}
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x0600111C RID: 4380 RVA: 0x00044E04 File Offset: 0x00043004
		// (set) Token: 0x0600111D RID: 4381 RVA: 0x00044E0C File Offset: 0x0004300C
		[DataSourceProperty]
		public int SecurityChangeAmount
		{
			get
			{
				return this._securityChangeAmount;
			}
			set
			{
				if (value != this._securityChangeAmount)
				{
					this._securityChangeAmount = value;
					base.OnPropertyChangedWithValue(value, "SecurityChangeAmount");
				}
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x0600111E RID: 4382 RVA: 0x00044E2A File Offset: 0x0004302A
		// (set) Token: 0x0600111F RID: 4383 RVA: 0x00044E32 File Offset: 0x00043032
		[DataSourceProperty]
		public int FoodChangeAmount
		{
			get
			{
				return this._foodChangeAmount;
			}
			set
			{
				if (value != this._foodChangeAmount)
				{
					this._foodChangeAmount = value;
					base.OnPropertyChangedWithValue(value, "FoodChangeAmount");
				}
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001120 RID: 4384 RVA: 0x00044E50 File Offset: 0x00043050
		// (set) Token: 0x06001121 RID: 4385 RVA: 0x00044E58 File Offset: 0x00043058
		[DataSourceProperty]
		public BasicTooltipViewModel RemainingFoodHint
		{
			get
			{
				return this._remainingFoodHint;
			}
			set
			{
				if (value != this._remainingFoodHint)
				{
					this._remainingFoodHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "RemainingFoodHint");
				}
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x00044E76 File Offset: 0x00043076
		// (set) Token: 0x06001123 RID: 4387 RVA: 0x00044E7E File Offset: 0x0004307E
		[DataSourceProperty]
		public BasicTooltipViewModel SecurityHint
		{
			get
			{
				return this._securityHint;
			}
			set
			{
				if (value != this._securityHint)
				{
					this._securityHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "SecurityHint");
				}
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x00044E9C File Offset: 0x0004309C
		// (set) Token: 0x06001125 RID: 4389 RVA: 0x00044EA4 File Offset: 0x000430A4
		[DataSourceProperty]
		public HintViewModel PartyFilterHint
		{
			get
			{
				return this._partyFilterHint;
			}
			set
			{
				if (value != this._partyFilterHint)
				{
					this._partyFilterHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "PartyFilterHint");
				}
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x00044EC2 File Offset: 0x000430C2
		// (set) Token: 0x06001127 RID: 4391 RVA: 0x00044ECA File Offset: 0x000430CA
		[DataSourceProperty]
		public HintViewModel CharacterFilterHint
		{
			get
			{
				return this._characterFilterHint;
			}
			set
			{
				if (value != this._characterFilterHint)
				{
					this._characterFilterHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "CharacterFilterHint");
				}
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x00044EE8 File Offset: 0x000430E8
		// (set) Token: 0x06001129 RID: 4393 RVA: 0x00044EF0 File Offset: 0x000430F0
		[DataSourceProperty]
		public BasicTooltipViewModel MilitasHint
		{
			get
			{
				return this._militasHint;
			}
			set
			{
				if (value != this._militasHint)
				{
					this._militasHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "MilitasHint");
				}
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x00044F0E File Offset: 0x0004310E
		// (set) Token: 0x0600112B RID: 4395 RVA: 0x00044F16 File Offset: 0x00043116
		[DataSourceProperty]
		public BasicTooltipViewModel GarrisonHint
		{
			get
			{
				return this._garrisonHint;
			}
			set
			{
				if (value != this._garrisonHint)
				{
					this._garrisonHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "GarrisonHint");
				}
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x00044F34 File Offset: 0x00043134
		// (set) Token: 0x0600112D RID: 4397 RVA: 0x00044F3C File Offset: 0x0004313C
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

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x00044F5A File Offset: 0x0004315A
		// (set) Token: 0x0600112F RID: 4399 RVA: 0x00044F62 File Offset: 0x00043162
		[DataSourceProperty]
		public BasicTooltipViewModel LoyaltyHint
		{
			get
			{
				return this._loyaltyHint;
			}
			set
			{
				if (value != this._loyaltyHint)
				{
					this._loyaltyHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "LoyaltyHint");
				}
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x00044F80 File Offset: 0x00043180
		// (set) Token: 0x06001131 RID: 4401 RVA: 0x00044F88 File Offset: 0x00043188
		[DataSourceProperty]
		public BasicTooltipViewModel WallsHint
		{
			get
			{
				return this._wallsHint;
			}
			set
			{
				if (value != this._wallsHint)
				{
					this._wallsHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "WallsHint");
				}
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001132 RID: 4402 RVA: 0x00044FA6 File Offset: 0x000431A6
		// (set) Token: 0x06001133 RID: 4403 RVA: 0x00044FAE File Offset: 0x000431AE
		[DataSourceProperty]
		public BasicTooltipViewModel CrimeHint
		{
			get
			{
				return this._crimeHint;
			}
			set
			{
				if (value != this._crimeHint)
				{
					this._crimeHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "CrimeHint");
				}
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001134 RID: 4404 RVA: 0x00044FCC File Offset: 0x000431CC
		// (set) Token: 0x06001135 RID: 4405 RVA: 0x00044FD4 File Offset: 0x000431D4
		[DataSourceProperty]
		public HintViewModel LeaveMembersHint
		{
			get
			{
				return this._leaveMembersHint;
			}
			set
			{
				if (value != this._leaveMembersHint)
				{
					this._leaveMembersHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "LeaveMembersHint");
				}
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x00044FF2 File Offset: 0x000431F2
		// (set) Token: 0x06001137 RID: 4407 RVA: 0x00044FFA File Offset: 0x000431FA
		[DataSourceProperty]
		public ImageIdentifierVM SettlementOwnerBanner
		{
			get
			{
				return this._settlementOwnerBanner;
			}
			set
			{
				if (value != this._settlementOwnerBanner)
				{
					this._settlementOwnerBanner = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "SettlementOwnerBanner");
				}
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x00045018 File Offset: 0x00043218
		// (set) Token: 0x06001139 RID: 4409 RVA: 0x00045020 File Offset: 0x00043220
		[DataSourceProperty]
		public MBBindingList<GameMenuPartyItemVM> CharacterList
		{
			get
			{
				return this._characterList;
			}
			set
			{
				if (value != this._characterList)
				{
					this._characterList = value;
					base.OnPropertyChangedWithValue<MBBindingList<GameMenuPartyItemVM>>(value, "CharacterList");
				}
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x0600113A RID: 4410 RVA: 0x0004503E File Offset: 0x0004323E
		// (set) Token: 0x0600113B RID: 4411 RVA: 0x00045046 File Offset: 0x00043246
		[DataSourceProperty]
		public MBBindingList<GameMenuPartyItemVM> PartyList
		{
			get
			{
				return this._partyList;
			}
			set
			{
				if (value != this._partyList)
				{
					this._partyList = value;
					base.OnPropertyChangedWithValue<MBBindingList<GameMenuPartyItemVM>>(value, "PartyList");
				}
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x0600113C RID: 4412 RVA: 0x00045064 File Offset: 0x00043264
		// (set) Token: 0x0600113D RID: 4413 RVA: 0x0004506C File Offset: 0x0004326C
		[DataSourceProperty]
		public MBBindingList<StringItemWithHintVM> IssueList
		{
			get
			{
				return this._issueList;
			}
			set
			{
				if (value != this._issueList)
				{
					this._issueList = value;
					base.OnPropertyChangedWithValue<MBBindingList<StringItemWithHintVM>>(value, "IssueList");
				}
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x0600113E RID: 4414 RVA: 0x0004508A File Offset: 0x0004328A
		// (set) Token: 0x0600113F RID: 4415 RVA: 0x00045092 File Offset: 0x00043292
		[DataSourceProperty]
		public string MilitasLbl
		{
			get
			{
				return this._militasLbl;
			}
			set
			{
				if (value != this._militasLbl)
				{
					this._militasLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "MilitasLbl");
				}
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x000450B5 File Offset: 0x000432B5
		// (set) Token: 0x06001141 RID: 4417 RVA: 0x000450BD File Offset: 0x000432BD
		[DataSourceProperty]
		public string GarrisonLbl
		{
			get
			{
				return this._garrisonLbl;
			}
			set
			{
				if (value != this._garrisonLbl)
				{
					this._garrisonLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "GarrisonLbl");
				}
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x000450E0 File Offset: 0x000432E0
		// (set) Token: 0x06001143 RID: 4419 RVA: 0x000450E8 File Offset: 0x000432E8
		[DataSourceProperty]
		public string CrimeLbl
		{
			get
			{
				return this._crimeLbl;
			}
			set
			{
				if (value != this._crimeLbl)
				{
					this._crimeLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "CrimeLbl");
				}
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x0004510B File Offset: 0x0004330B
		// (set) Token: 0x06001145 RID: 4421 RVA: 0x00045113 File Offset: 0x00043313
		[DataSourceProperty]
		public bool CanLeaveMembers
		{
			get
			{
				return this._canLeaveMembers;
			}
			set
			{
				if (value != this._canLeaveMembers)
				{
					this._canLeaveMembers = value;
					base.OnPropertyChangedWithValue(value, "CanLeaveMembers");
				}
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x00045131 File Offset: 0x00043331
		// (set) Token: 0x06001147 RID: 4423 RVA: 0x00045139 File Offset: 0x00043339
		[DataSourceProperty]
		public string ProsperityLbl
		{
			get
			{
				return this._prosperityLbl;
			}
			set
			{
				if (value != this._prosperityLbl)
				{
					this._prosperityLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "ProsperityLbl");
				}
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001148 RID: 4424 RVA: 0x0004515C File Offset: 0x0004335C
		// (set) Token: 0x06001149 RID: 4425 RVA: 0x00045164 File Offset: 0x00043364
		[DataSourceProperty]
		public string LoyaltyLbl
		{
			get
			{
				return this._loyaltyLbl;
			}
			set
			{
				if (value != this._loyaltyLbl)
				{
					this._loyaltyLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "LoyaltyLbl");
				}
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x00045187 File Offset: 0x00043387
		// (set) Token: 0x0600114B RID: 4427 RVA: 0x0004518F File Offset: 0x0004338F
		[DataSourceProperty]
		public string SecurityLbl
		{
			get
			{
				return this._securityLbl;
			}
			set
			{
				if (value != this._securityLbl)
				{
					this._securityLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "SecurityLbl");
				}
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x0600114C RID: 4428 RVA: 0x000451B2 File Offset: 0x000433B2
		// (set) Token: 0x0600114D RID: 4429 RVA: 0x000451BA File Offset: 0x000433BA
		[DataSourceProperty]
		public string WallsLbl
		{
			get
			{
				return this._wallsLbl;
			}
			set
			{
				if (value != this._wallsLbl)
				{
					this._wallsLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "WallsLbl");
				}
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x0600114E RID: 4430 RVA: 0x000451DD File Offset: 0x000433DD
		// (set) Token: 0x0600114F RID: 4431 RVA: 0x000451E5 File Offset: 0x000433E5
		[DataSourceProperty]
		public int WallsLevel
		{
			get
			{
				return this._wallsLevel;
			}
			set
			{
				if (value != this._wallsLevel)
				{
					this._wallsLevel = value;
					base.OnPropertyChangedWithValue(value, "WallsLevel");
				}
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x00045203 File Offset: 0x00043403
		// (set) Token: 0x06001151 RID: 4433 RVA: 0x0004520B File Offset: 0x0004340B
		[DataSourceProperty]
		public string SettlementNameLbl
		{
			get
			{
				return this._settlementNameLbl;
			}
			set
			{
				if (value != this._settlementNameLbl)
				{
					this._settlementNameLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "SettlementNameLbl");
				}
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x0004522E File Offset: 0x0004342E
		// (set) Token: 0x06001153 RID: 4435 RVA: 0x00045236 File Offset: 0x00043436
		[DataSourceProperty]
		public bool IsFortification
		{
			get
			{
				return this._isFortification;
			}
			set
			{
				if (value != this._isFortification)
				{
					this._isFortification = value;
					base.OnPropertyChangedWithValue(value, "IsFortification");
				}
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x00045254 File Offset: 0x00043454
		// (set) Token: 0x06001155 RID: 4437 RVA: 0x0004525C File Offset: 0x0004345C
		[DataSourceProperty]
		public bool IsCrimeEnabled
		{
			get
			{
				return this._isCrimeEnabled;
			}
			set
			{
				if (value != this._isCrimeEnabled)
				{
					this._isCrimeEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsCrimeEnabled");
				}
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x0004527A File Offset: 0x0004347A
		// (set) Token: 0x06001157 RID: 4439 RVA: 0x00045282 File Offset: 0x00043482
		[DataSourceProperty]
		public bool IsNoGarrisonWarning
		{
			get
			{
				return this._isNoGarrisonWarning;
			}
			set
			{
				if (value != this._isNoGarrisonWarning)
				{
					this._isNoGarrisonWarning = value;
					base.OnPropertyChangedWithValue(value, "IsNoGarrisonWarning");
				}
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001158 RID: 4440 RVA: 0x000452A0 File Offset: 0x000434A0
		// (set) Token: 0x06001159 RID: 4441 RVA: 0x000452A8 File Offset: 0x000434A8
		[DataSourceProperty]
		public bool IsCrimeLabelHighlightEnabled
		{
			get
			{
				return this._isCrimeLabelHighlightEnabled;
			}
			set
			{
				if (value != this._isCrimeLabelHighlightEnabled)
				{
					this._isCrimeLabelHighlightEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsCrimeLabelHighlightEnabled");
				}
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x0600115A RID: 4442 RVA: 0x000452C6 File Offset: 0x000434C6
		// (set) Token: 0x0600115B RID: 4443 RVA: 0x000452CE File Offset: 0x000434CE
		[DataSourceProperty]
		public bool IsLoyaltyRebellionWarning
		{
			get
			{
				return this._isLoyaltyRebellionWarning;
			}
			set
			{
				if (value != this._isLoyaltyRebellionWarning)
				{
					this._isLoyaltyRebellionWarning = value;
					base.OnPropertyChangedWithValue(value, "IsLoyaltyRebellionWarning");
				}
			}
		}

		// Token: 0x040007DA RID: 2010
		private readonly Settlement _settlement;

		// Token: 0x040007DB RID: 2011
		private GameOverlays.MenuOverlayType _type;

		// Token: 0x040007DC RID: 2012
		private GameMenuOverlayActionVM _overlayTalkItem;

		// Token: 0x040007DD RID: 2013
		private GameMenuOverlayActionVM _overlayQuickTalkItem;

		// Token: 0x040007DE RID: 2014
		private Tuple<bool, TextObject> _mostRecentOverlayTalkPermission;

		// Token: 0x040007DF RID: 2015
		private Tuple<bool, TextObject> _mostRecentOverlayQuickTalkPermission;

		// Token: 0x040007E0 RID: 2016
		private Tuple<bool, TextObject> _mostRecentOverlayLeaveCharacterPermission;

		// Token: 0x040007E1 RID: 2017
		private string _latestTutorialElementID;

		// Token: 0x040007E2 RID: 2018
		private bool _isCompanionHighlightApplied;

		// Token: 0x040007E3 RID: 2019
		private bool _isQuestGiversHighlightApplied;

		// Token: 0x040007E4 RID: 2020
		private bool _isNotableHighlightApplied;

		// Token: 0x040007E5 RID: 2021
		private bool _isTalkItemHighlightApplied;

		// Token: 0x040007E6 RID: 2022
		private string _militasLbl;

		// Token: 0x040007E7 RID: 2023
		private string _garrisonLbl;

		// Token: 0x040007E8 RID: 2024
		private bool _isNoGarrisonWarning;

		// Token: 0x040007E9 RID: 2025
		private bool _isLoyaltyRebellionWarning;

		// Token: 0x040007EA RID: 2026
		private bool _isCrimeLabelHighlightEnabled;

		// Token: 0x040007EB RID: 2027
		private string _crimeLbl;

		// Token: 0x040007EC RID: 2028
		private string _prosperityLbl;

		// Token: 0x040007ED RID: 2029
		private string _loyaltyLbl;

		// Token: 0x040007EE RID: 2030
		private string _securityLbl;

		// Token: 0x040007EF RID: 2031
		private string _wallsLbl;

		// Token: 0x040007F0 RID: 2032
		private string _settlementNameLbl;

		// Token: 0x040007F1 RID: 2033
		private string _remainingFoodText = "";

		// Token: 0x040007F2 RID: 2034
		private int _wallsLevel;

		// Token: 0x040007F3 RID: 2035
		private int _prosperityChangeAmount;

		// Token: 0x040007F4 RID: 2036
		private int _militiaChangeAmount;

		// Token: 0x040007F5 RID: 2037
		private int _garrisonChangeAmount;

		// Token: 0x040007F6 RID: 2038
		private int _garrisonAmount;

		// Token: 0x040007F7 RID: 2039
		private int _crimeChangeAmount;

		// Token: 0x040007F8 RID: 2040
		private int _loyaltyChangeAmount;

		// Token: 0x040007F9 RID: 2041
		private int _securityChangeAmount;

		// Token: 0x040007FA RID: 2042
		private int _foodChangeAmount;

		// Token: 0x040007FB RID: 2043
		private MBBindingList<GameMenuPartyItemVM> _characterList;

		// Token: 0x040007FC RID: 2044
		private MBBindingList<GameMenuPartyItemVM> _partyList;

		// Token: 0x040007FD RID: 2045
		private MBBindingList<StringItemWithHintVM> _issueList;

		// Token: 0x040007FE RID: 2046
		private bool _isFortification;

		// Token: 0x040007FF RID: 2047
		private bool _isCrimeEnabled;

		// Token: 0x04000800 RID: 2048
		private bool _canLeaveMembers;

		// Token: 0x04000801 RID: 2049
		private BasicTooltipViewModel _remainingFoodHint;

		// Token: 0x04000802 RID: 2050
		private BasicTooltipViewModel _militasHint;

		// Token: 0x04000803 RID: 2051
		private BasicTooltipViewModel _garrisonHint;

		// Token: 0x04000804 RID: 2052
		private BasicTooltipViewModel _prosperityHint;

		// Token: 0x04000805 RID: 2053
		private BasicTooltipViewModel _loyaltyHint;

		// Token: 0x04000806 RID: 2054
		private BasicTooltipViewModel _securityHint;

		// Token: 0x04000807 RID: 2055
		private BasicTooltipViewModel _wallsHint;

		// Token: 0x04000808 RID: 2056
		private BasicTooltipViewModel _crimeHint;

		// Token: 0x04000809 RID: 2057
		private HintViewModel _characterFilterHint;

		// Token: 0x0400080A RID: 2058
		private HintViewModel _partyFilterHint;

		// Token: 0x0400080B RID: 2059
		private HintViewModel _leaveMembersHint;

		// Token: 0x0400080C RID: 2060
		private ImageIdentifierVM _settlementOwnerBanner;

		// Token: 0x020001F1 RID: 497
		public class SettlementOverlayTalkPermissionEvent : EventBase
		{
			// Token: 0x17000AE9 RID: 2793
			// (get) Token: 0x060021D1 RID: 8657 RVA: 0x0007467A File Offset: 0x0007287A
			// (set) Token: 0x060021D2 RID: 8658 RVA: 0x00074682 File Offset: 0x00072882
			public Action<bool, TextObject> IsTalkAvailable { get; private set; }

			// Token: 0x060021D3 RID: 8659 RVA: 0x0007468B File Offset: 0x0007288B
			public SettlementOverlayTalkPermissionEvent(Hero heroToTalkTo, Action<bool, TextObject> isTalkAvailable)
			{
				this.HeroToTalkTo = heroToTalkTo;
				this.IsTalkAvailable = isTalkAvailable;
			}

			// Token: 0x040010AF RID: 4271
			public Hero HeroToTalkTo;
		}

		// Token: 0x020001F2 RID: 498
		public class SettlementOverylayQuickTalkPermissionEvent : EventBase
		{
			// Token: 0x17000AEA RID: 2794
			// (get) Token: 0x060021D4 RID: 8660 RVA: 0x000746A1 File Offset: 0x000728A1
			// (set) Token: 0x060021D5 RID: 8661 RVA: 0x000746A9 File Offset: 0x000728A9
			public Action<bool, TextObject> IsTalkAvailable { get; private set; }

			// Token: 0x060021D6 RID: 8662 RVA: 0x000746B2 File Offset: 0x000728B2
			public SettlementOverylayQuickTalkPermissionEvent(Action<bool, TextObject> isTalkAvailable)
			{
				this.IsTalkAvailable = isTalkAvailable;
			}
		}

		// Token: 0x020001F3 RID: 499
		public class SettlementOverlayLeaveCharacterPermissionEvent : EventBase
		{
			// Token: 0x17000AEB RID: 2795
			// (get) Token: 0x060021D7 RID: 8663 RVA: 0x000746C1 File Offset: 0x000728C1
			// (set) Token: 0x060021D8 RID: 8664 RVA: 0x000746C9 File Offset: 0x000728C9
			public Action<bool, TextObject> IsLeaveAvailable { get; private set; }

			// Token: 0x060021D9 RID: 8665 RVA: 0x000746D2 File Offset: 0x000728D2
			public SettlementOverlayLeaveCharacterPermissionEvent(Action<bool, TextObject> isLeaveAvailable)
			{
				this.IsLeaveAvailable = isLeaveAvailable;
			}
		}

		// Token: 0x020001F4 RID: 500
		public class CrimeValueInspectedInSettlementOverlayEvent : EventBase
		{
		}

		// Token: 0x020001F5 RID: 501
		private class CharacterComparer : IComparer<GameMenuPartyItemVM>
		{
			// Token: 0x060021DB RID: 8667 RVA: 0x000746E9 File Offset: 0x000728E9
			public int Compare(GameMenuPartyItemVM x, GameMenuPartyItemVM y)
			{
				return CampaignUIHelper.GetHeroCompareSortIndex(x.Character.HeroObject, y.Character.HeroObject);
			}
		}

		// Token: 0x020001F6 RID: 502
		private class PartyComparer : IComparer<GameMenuPartyItemVM>
		{
			// Token: 0x060021DD RID: 8669 RVA: 0x0007470E File Offset: 0x0007290E
			public int Compare(GameMenuPartyItemVM x, GameMenuPartyItemVM y)
			{
				return CampaignUIHelper.MobilePartyPrecedenceComparerInstance.Compare(x.Party.MobileParty, y.Party.MobileParty);
			}
		}
	}
}
