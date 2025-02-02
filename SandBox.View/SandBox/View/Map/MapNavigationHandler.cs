using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Inventory;
using TaleWorlds.CampaignSystem.Issues;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Library.EventSystem;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ScreenSystem;

namespace SandBox.View.Map
{
	// Token: 0x02000047 RID: 71
	public class MapNavigationHandler : INavigationHandler
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000268 RID: 616 RVA: 0x000160ED File Offset: 0x000142ED
		// (set) Token: 0x06000269 RID: 617 RVA: 0x000160F5 File Offset: 0x000142F5
		public bool IsNavigationLocked { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600026A RID: 618 RVA: 0x000160FE File Offset: 0x000142FE
		private InquiryData _unsavedChangesInquiry
		{
			get
			{
				return this.GetUnsavedChangedInquiry();
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00016106 File Offset: 0x00014306
		private InquiryData _unapplicableChangesInquiry
		{
			get
			{
				return this.GetUnapplicableChangedInquiry();
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0001610E File Offset: 0x0001430E
		public MapNavigationHandler()
		{
			this._game = Game.Current;
			this._needToBeInKingdomText = GameTexts.FindText("str_need_to_be_in_kingdom", null);
			this._clanScreenPermissionEvent = new MapNavigationHandler.ClanScreenPermissionEvent(new Action<bool, TextObject>(this.OnClanScreenPermission));
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0001614C File Offset: 0x0001434C
		private InquiryData GetUnsavedChangedInquiry()
		{
			return new InquiryData(string.Empty, GameTexts.FindText("str_unsaved_changes", null).ToString(), true, true, GameTexts.FindText("str_apply", null).ToString(), GameTexts.FindText("str_cancel", null).ToString(), null, null, "", 0f, null, null, null);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x000161A4 File Offset: 0x000143A4
		private InquiryData GetUnapplicableChangedInquiry()
		{
			return new InquiryData(string.Empty, GameTexts.FindText("str_unapplicable_changes", null).ToString(), true, true, GameTexts.FindText("str_apply", null).ToString(), GameTexts.FindText("str_cancel", null).ToString(), null, null, "", 0f, null, null, null);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000161FC File Offset: 0x000143FC
		private bool IsMapTopScreen()
		{
			return ScreenManager.TopScreen is MapScreen;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0001620C File Offset: 0x0001440C
		private bool IsNavigationBarEnabled()
		{
			if (Hero.MainHero != null)
			{
				Hero mainHero = Hero.MainHero;
				if (mainHero == null || !mainHero.IsDead)
				{
					Campaign campaign = Campaign.Current;
					if (campaign == null || !campaign.SaveHandler.IsSaving)
					{
						MapScreen mapScreen;
						return !this.IsNavigationLocked && (InventoryManager.InventoryLogic == null || InventoryManager.Instance.CurrentMode == InventoryMode.Default) && (PartyScreenManager.PartyScreenLogic == null || PartyScreenManager.Instance.CurrentMode == PartyScreenMode.Normal) && PlayerEncounter.CurrentBattleSimulation == null && ((mapScreen = (ScreenManager.TopScreen as MapScreen)) == null || (!mapScreen.IsInArmyManagement && !mapScreen.IsMarriageOfferPopupActive && !mapScreen.IsMapCheatsActive)) && !this.EscapeMenuActive;
					}
				}
			}
			return false;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000271 RID: 625 RVA: 0x000162C0 File Offset: 0x000144C0
		public bool PartyEnabled
		{
			get
			{
				if (!this.IsNavigationBarEnabled())
				{
					return false;
				}
				if (this.PartyActive)
				{
					return false;
				}
				if (Hero.MainHero.HeroState == Hero.CharacterStates.Prisoner)
				{
					return false;
				}
				if (MobileParty.MainParty.MapEvent != null)
				{
					return false;
				}
				Mission mission = Mission.Current;
				return mission == null || mission.IsPartyWindowAccessAllowed;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000272 RID: 626 RVA: 0x00016317 File Offset: 0x00014517
		public bool InventoryEnabled
		{
			get
			{
				if (!this.IsNavigationBarEnabled())
				{
					return false;
				}
				if (this.InventoryActive)
				{
					return false;
				}
				if (Hero.MainHero.HeroState == Hero.CharacterStates.Prisoner)
				{
					return false;
				}
				Mission mission = Mission.Current;
				return mission == null || mission.IsInventoryAccessAllowed;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00016355 File Offset: 0x00014555
		public bool QuestsEnabled
		{
			get
			{
				if (!this.IsNavigationBarEnabled())
				{
					return false;
				}
				if (this.QuestsActive)
				{
					return false;
				}
				Mission mission = Mission.Current;
				return mission == null || mission.IsQuestScreenAccessAllowed;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000274 RID: 628 RVA: 0x00016384 File Offset: 0x00014584
		public bool CharacterDeveloperEnabled
		{
			get
			{
				if (!this.IsNavigationBarEnabled())
				{
					return false;
				}
				if (this.CharacterDeveloperActive)
				{
					return false;
				}
				Mission mission = Mission.Current;
				return mission == null || mission.IsCharacterWindowAccessAllowed;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000275 RID: 629 RVA: 0x000163B4 File Offset: 0x000145B4
		public NavigationPermissionItem ClanPermission
		{
			get
			{
				if (!this.IsNavigationBarEnabled())
				{
					return new NavigationPermissionItem(false, null);
				}
				if (this.ClanActive)
				{
					return new NavigationPermissionItem(false, null);
				}
				Mission mission = Mission.Current;
				if (mission != null && !mission.IsClanWindowAccessAllowed)
				{
					return new NavigationPermissionItem(false, null);
				}
				this._mostRecentClanScreenPermission = null;
				Game.Current.EventManager.TriggerEvent<MapNavigationHandler.ClanScreenPermissionEvent>(this._clanScreenPermissionEvent);
				NavigationPermissionItem? mostRecentClanScreenPermission = this._mostRecentClanScreenPermission;
				if (mostRecentClanScreenPermission == null)
				{
					return new NavigationPermissionItem(true, null);
				}
				return mostRecentClanScreenPermission.GetValueOrDefault();
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0001643F File Offset: 0x0001463F
		public void OnClanScreenPermission(bool isAvailable, TextObject reasonString)
		{
			if (!isAvailable)
			{
				this._mostRecentClanScreenPermission = new NavigationPermissionItem?(new NavigationPermissionItem(isAvailable, reasonString));
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000277 RID: 631 RVA: 0x00016458 File Offset: 0x00014658
		public NavigationPermissionItem KingdomPermission
		{
			get
			{
				if (!this.IsNavigationBarEnabled())
				{
					return new NavigationPermissionItem(false, null);
				}
				if (this.KingdomActive)
				{
					return new NavigationPermissionItem(false, null);
				}
				if (!Hero.MainHero.MapFaction.IsKingdomFaction)
				{
					return new NavigationPermissionItem(false, this._needToBeInKingdomText);
				}
				Mission mission = Mission.Current;
				if (mission != null && !mission.IsKingdomWindowAccessAllowed)
				{
					return new NavigationPermissionItem(false, null);
				}
				return new NavigationPermissionItem(true, null);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000278 RID: 632 RVA: 0x000164C8 File Offset: 0x000146C8
		public bool EscapeMenuEnabled
		{
			get
			{
				return this.IsNavigationBarEnabled() && !this.EscapeMenuActive && this._game.GameStateManager.ActiveState is MapState;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000279 RID: 633 RVA: 0x000164F6 File Offset: 0x000146F6
		public bool PartyActive
		{
			get
			{
				return this._game.GameStateManager.ActiveState is PartyState;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600027A RID: 634 RVA: 0x00016510 File Offset: 0x00014710
		public bool InventoryActive
		{
			get
			{
				return this._game.GameStateManager.ActiveState is InventoryState;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0001652A File Offset: 0x0001472A
		public bool QuestsActive
		{
			get
			{
				return this._game.GameStateManager.ActiveState is QuestsState;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600027C RID: 636 RVA: 0x00016544 File Offset: 0x00014744
		public bool CharacterDeveloperActive
		{
			get
			{
				return this._game.GameStateManager.ActiveState is CharacterDeveloperState;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0001655E File Offset: 0x0001475E
		public bool ClanActive
		{
			get
			{
				return this._game.GameStateManager.ActiveState is ClanState;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00016578 File Offset: 0x00014778
		public bool KingdomActive
		{
			get
			{
				return this._game.GameStateManager.ActiveState is KingdomState;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600027F RID: 639 RVA: 0x00016592 File Offset: 0x00014792
		public bool EscapeMenuActive
		{
			get
			{
				if (this._game.GameStateManager.ActiveState is MapState)
				{
					MapScreen instance = MapScreen.Instance;
					return instance != null && instance.IsEscapeMenuOpened;
				}
				return false;
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x000165BD File Offset: 0x000147BD
		void INavigationHandler.OpenQuests()
		{
			this.PrepareToOpenQuestsScreen(delegate
			{
				this.OpenQuestsAction();
			});
		}

		// Token: 0x06000281 RID: 641 RVA: 0x000165D4 File Offset: 0x000147D4
		void INavigationHandler.OpenQuests(IssueBase issue)
		{
			this.PrepareToOpenQuestsScreen(delegate
			{
				this.OpenQuestsAction(issue);
			});
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00016608 File Offset: 0x00014808
		void INavigationHandler.OpenQuests(QuestBase quest)
		{
			this.PrepareToOpenQuestsScreen(delegate
			{
				this.OpenQuestsAction(quest);
			});
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0001663C File Offset: 0x0001483C
		void INavigationHandler.OpenQuests(JournalLogEntry log)
		{
			this.PrepareToOpenQuestsScreen(delegate
			{
				this.OpenQuestsAction(log);
			});
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00016670 File Offset: 0x00014870
		private void PrepareToOpenQuestsScreen(Action openQuestsAction)
		{
			if (this.QuestsEnabled)
			{
				IChangeableScreen changeableScreen;
				if ((changeableScreen = (ScreenManager.TopScreen as IChangeableScreen)) != null && changeableScreen.AnyUnsavedChanges())
				{
					InquiryData inquiryData = changeableScreen.CanChangesBeApplied() ? this._unsavedChangesInquiry : this._unapplicableChangesInquiry;
					inquiryData.SetAffirmativeAction(delegate
					{
						this.ApplyCurrentChanges();
						openQuestsAction();
					});
					InformationManager.ShowInquiry(inquiryData, false, false);
					return;
				}
				if (!this.IsMapTopScreen())
				{
					this._game.GameStateManager.PopState(0);
				}
				openQuestsAction();
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00016704 File Offset: 0x00014904
		private void OpenQuestsAction()
		{
			QuestsState gameState = this._game.GameStateManager.CreateState<QuestsState>();
			this._game.GameStateManager.PushState(gameState, 0);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00016734 File Offset: 0x00014934
		private void OpenQuestsAction(IssueBase issue)
		{
			QuestsState gameState = this._game.GameStateManager.CreateState<QuestsState>(new object[]
			{
				issue
			});
			this._game.GameStateManager.PushState(gameState, 0);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00016770 File Offset: 0x00014970
		private void OpenQuestsAction(QuestBase quest)
		{
			QuestsState gameState = this._game.GameStateManager.CreateState<QuestsState>(new object[]
			{
				quest
			});
			this._game.GameStateManager.PushState(gameState, 0);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000167AC File Offset: 0x000149AC
		private void OpenQuestsAction(JournalLogEntry log)
		{
			QuestsState gameState = this._game.GameStateManager.CreateState<QuestsState>(new object[]
			{
				log
			});
			this._game.GameStateManager.PushState(gameState, 0);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x000167E8 File Offset: 0x000149E8
		void INavigationHandler.OpenInventory()
		{
			if (this.InventoryEnabled)
			{
				IChangeableScreen changeableScreen;
				if ((changeableScreen = (ScreenManager.TopScreen as IChangeableScreen)) != null)
				{
					if (changeableScreen.AnyUnsavedChanges())
					{
						InquiryData inquiryData = changeableScreen.CanChangesBeApplied() ? this._unsavedChangesInquiry : this._unapplicableChangesInquiry;
						inquiryData.SetAffirmativeAction(delegate
						{
							this.ApplyCurrentChanges();
							this.OpenInventoryScreenAction();
						});
						InformationManager.ShowInquiry(inquiryData, false, false);
						return;
					}
					this.OpenInventoryScreenAction();
					return;
				}
				else
				{
					this.OpenInventoryScreenAction();
				}
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00016850 File Offset: 0x00014A50
		private void OpenInventoryScreenAction()
		{
			if (!this.IsMapTopScreen())
			{
				this._game.GameStateManager.PopState(0);
			}
			InventoryManager.OpenScreenAsInventory(null);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00016874 File Offset: 0x00014A74
		void INavigationHandler.OpenParty()
		{
			if (this.PartyEnabled)
			{
				IChangeableScreen changeableScreen;
				if ((changeableScreen = (ScreenManager.TopScreen as IChangeableScreen)) != null)
				{
					if (changeableScreen.AnyUnsavedChanges())
					{
						InquiryData inquiryData = changeableScreen.CanChangesBeApplied() ? this._unsavedChangesInquiry : this._unapplicableChangesInquiry;
						inquiryData.SetAffirmativeAction(delegate
						{
							this.ApplyCurrentChanges();
							this.OpenPartyScreenAction();
						});
						InformationManager.ShowInquiry(inquiryData, false, false);
						return;
					}
					this.OpenPartyScreenAction();
					return;
				}
				else
				{
					this.OpenPartyScreenAction();
				}
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000168DC File Offset: 0x00014ADC
		private void OpenPartyScreenAction()
		{
			if (this.PartyEnabled)
			{
				if (!this.IsMapTopScreen())
				{
					this._game.GameStateManager.PopState(0);
				}
				PartyScreenManager.OpenScreenAsNormal();
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00016904 File Offset: 0x00014B04
		void INavigationHandler.OpenCharacterDeveloper()
		{
			this.PrepareToOpenCharacterDeveloper(delegate
			{
				this.OpenCharacterDeveloperScreenAction();
			});
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00016918 File Offset: 0x00014B18
		void INavigationHandler.OpenCharacterDeveloper(Hero hero)
		{
			this.PrepareToOpenCharacterDeveloper(delegate
			{
				this.OpenCharacterDeveloperScreenAction(hero);
			});
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0001694C File Offset: 0x00014B4C
		private void PrepareToOpenCharacterDeveloper(Action openCharacterDeveloperAction)
		{
			if (this.CharacterDeveloperEnabled)
			{
				IChangeableScreen changeableScreen;
				if ((changeableScreen = (ScreenManager.TopScreen as IChangeableScreen)) != null && changeableScreen.AnyUnsavedChanges())
				{
					InquiryData inquiryData = changeableScreen.CanChangesBeApplied() ? this._unsavedChangesInquiry : this._unapplicableChangesInquiry;
					inquiryData.SetAffirmativeAction(delegate
					{
						this.ApplyCurrentChanges();
						openCharacterDeveloperAction();
					});
					InformationManager.ShowInquiry(inquiryData, false, false);
					return;
				}
				if (!this.IsMapTopScreen())
				{
					this._game.GameStateManager.PopState(0);
				}
				openCharacterDeveloperAction();
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x000169E0 File Offset: 0x00014BE0
		private void OpenCharacterDeveloperScreenAction()
		{
			CharacterDeveloperState gameState = this._game.GameStateManager.CreateState<CharacterDeveloperState>();
			this._game.GameStateManager.PushState(gameState, 0);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00016A10 File Offset: 0x00014C10
		private void OpenCharacterDeveloperScreenAction(Hero hero)
		{
			CharacterDeveloperState gameState = this._game.GameStateManager.CreateState<CharacterDeveloperState>(new object[]
			{
				hero
			});
			this._game.GameStateManager.PushState(gameState, 0);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00016A4A File Offset: 0x00014C4A
		void INavigationHandler.OpenKingdom()
		{
			this.PrepareToOpenKingdomScreen(delegate
			{
				this.OpenKingdomAction();
			});
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00016A60 File Offset: 0x00014C60
		void INavigationHandler.OpenKingdom(Army army)
		{
			this.PrepareToOpenKingdomScreen(delegate
			{
				this.OpenKingdomAction(army);
			});
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00016A94 File Offset: 0x00014C94
		void INavigationHandler.OpenKingdom(Settlement settlement)
		{
			this.PrepareToOpenKingdomScreen(delegate
			{
				this.OpenKingdomAction(settlement);
			});
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00016AC8 File Offset: 0x00014CC8
		void INavigationHandler.OpenKingdom(Clan clan)
		{
			this.PrepareToOpenKingdomScreen(delegate
			{
				this.OpenKingdomAction(clan);
			});
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00016AFC File Offset: 0x00014CFC
		void INavigationHandler.OpenKingdom(PolicyObject policy)
		{
			this.PrepareToOpenKingdomScreen(delegate
			{
				this.OpenKingdomAction(policy);
			});
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00016B30 File Offset: 0x00014D30
		void INavigationHandler.OpenKingdom(IFaction faction)
		{
			this.PrepareToOpenKingdomScreen(delegate
			{
				this.OpenKingdomAction(faction);
			});
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00016B64 File Offset: 0x00014D64
		void INavigationHandler.OpenKingdom(KingdomDecision decision)
		{
			this.PrepareToOpenKingdomScreen(delegate
			{
				this.OpenKingdomAction(decision);
			});
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00016B98 File Offset: 0x00014D98
		private void PrepareToOpenKingdomScreen(Action openKingdomAction)
		{
			if (this.KingdomPermission.IsAuthorized)
			{
				IChangeableScreen changeableScreen;
				if ((changeableScreen = (ScreenManager.TopScreen as IChangeableScreen)) != null && changeableScreen.AnyUnsavedChanges())
				{
					InquiryData inquiryData = changeableScreen.CanChangesBeApplied() ? this._unsavedChangesInquiry : this._unapplicableChangesInquiry;
					inquiryData.SetAffirmativeAction(delegate
					{
						this.ApplyCurrentChanges();
						openKingdomAction();
					});
					InformationManager.ShowInquiry(inquiryData, false, false);
					return;
				}
				if (!this.IsMapTopScreen())
				{
					this._game.GameStateManager.PopState(0);
				}
				openKingdomAction();
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00016C34 File Offset: 0x00014E34
		private void OpenKingdomAction()
		{
			KingdomState gameState = this._game.GameStateManager.CreateState<KingdomState>();
			this._game.GameStateManager.PushState(gameState, 0);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00016C64 File Offset: 0x00014E64
		private void OpenKingdomAction(Army army)
		{
			KingdomState gameState = this._game.GameStateManager.CreateState<KingdomState>(new object[]
			{
				army
			});
			this._game.GameStateManager.PushState(gameState, 0);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00016CA0 File Offset: 0x00014EA0
		private void OpenKingdomAction(Settlement settlement)
		{
			KingdomState gameState = this._game.GameStateManager.CreateState<KingdomState>(new object[]
			{
				settlement
			});
			this._game.GameStateManager.PushState(gameState, 0);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00016CDC File Offset: 0x00014EDC
		private void OpenKingdomAction(Clan clan)
		{
			KingdomState gameState = this._game.GameStateManager.CreateState<KingdomState>(new object[]
			{
				clan
			});
			this._game.GameStateManager.PushState(gameState, 0);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00016D18 File Offset: 0x00014F18
		private void OpenKingdomAction(PolicyObject policy)
		{
			KingdomState gameState = this._game.GameStateManager.CreateState<KingdomState>(new object[]
			{
				policy
			});
			this._game.GameStateManager.PushState(gameState, 0);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00016D54 File Offset: 0x00014F54
		private void OpenKingdomAction(IFaction faction)
		{
			KingdomState gameState = this._game.GameStateManager.CreateState<KingdomState>(new object[]
			{
				faction
			});
			this._game.GameStateManager.PushState(gameState, 0);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00016D90 File Offset: 0x00014F90
		private void OpenKingdomAction(KingdomDecision decision)
		{
			KingdomState gameState = this._game.GameStateManager.CreateState<KingdomState>(new object[]
			{
				decision
			});
			this._game.GameStateManager.PushState(gameState, 0);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00016DCA File Offset: 0x00014FCA
		void INavigationHandler.OpenClan()
		{
			this.PrepareToOpenClanScreen(delegate
			{
				this.OpenClanScreenAction();
			});
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00016DE0 File Offset: 0x00014FE0
		void INavigationHandler.OpenClan(Hero hero)
		{
			this.PrepareToOpenClanScreen(delegate
			{
				this.OpenClanScreenAction(hero);
			});
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00016E14 File Offset: 0x00015014
		void INavigationHandler.OpenClan(PartyBase party)
		{
			this.PrepareToOpenClanScreen(delegate
			{
				this.OpenClanScreenAction(party);
			});
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00016E48 File Offset: 0x00015048
		void INavigationHandler.OpenClan(Settlement settlement)
		{
			this.PrepareToOpenClanScreen(delegate
			{
				this.OpenClanScreenAction(settlement);
			});
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00016E7C File Offset: 0x0001507C
		void INavigationHandler.OpenClan(Workshop workshop)
		{
			this.PrepareToOpenClanScreen(delegate
			{
				this.OpenClanScreenAction(workshop);
			});
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00016EB0 File Offset: 0x000150B0
		void INavigationHandler.OpenClan(Alley alley)
		{
			this.PrepareToOpenClanScreen(delegate
			{
				this.OpenClanScreenAction(alley);
			});
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00016EE4 File Offset: 0x000150E4
		private void PrepareToOpenClanScreen(Action openClanScreenAction)
		{
			if (this.ClanPermission.IsAuthorized)
			{
				IChangeableScreen changeableScreen;
				if ((changeableScreen = (ScreenManager.TopScreen as IChangeableScreen)) != null && changeableScreen.AnyUnsavedChanges())
				{
					InquiryData inquiryData = changeableScreen.CanChangesBeApplied() ? this._unsavedChangesInquiry : this._unapplicableChangesInquiry;
					inquiryData.SetAffirmativeAction(delegate
					{
						this.ApplyCurrentChanges();
						openClanScreenAction();
					});
					InformationManager.ShowInquiry(inquiryData, false, false);
					return;
				}
				if (!this.IsMapTopScreen())
				{
					this._game.GameStateManager.PopState(0);
				}
				openClanScreenAction();
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00016F80 File Offset: 0x00015180
		private void OpenClanScreenAction()
		{
			ClanState gameState = this._game.GameStateManager.CreateState<ClanState>();
			this._game.GameStateManager.PushState(gameState, 0);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00016FB0 File Offset: 0x000151B0
		private void OpenClanScreenAction(Hero hero)
		{
			ClanState gameState = this._game.GameStateManager.CreateState<ClanState>(new object[]
			{
				hero
			});
			this._game.GameStateManager.PushState(gameState, 0);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00016FEC File Offset: 0x000151EC
		private void OpenClanScreenAction(PartyBase party)
		{
			ClanState gameState = this._game.GameStateManager.CreateState<ClanState>(new object[]
			{
				party
			});
			this._game.GameStateManager.PushState(gameState, 0);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00017028 File Offset: 0x00015228
		private void OpenClanScreenAction(Settlement settlement)
		{
			ClanState gameState = this._game.GameStateManager.CreateState<ClanState>(new object[]
			{
				settlement
			});
			this._game.GameStateManager.PushState(gameState, 0);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00017064 File Offset: 0x00015264
		private void OpenClanScreenAction(Workshop workshop)
		{
			ClanState gameState = this._game.GameStateManager.CreateState<ClanState>(new object[]
			{
				workshop
			});
			this._game.GameStateManager.PushState(gameState, 0);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x000170A0 File Offset: 0x000152A0
		private void OpenClanScreenAction(Alley alley)
		{
			ClanState gameState = this._game.GameStateManager.CreateState<ClanState>(new object[]
			{
				alley
			});
			this._game.GameStateManager.PushState(gameState, 0);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x000170DA File Offset: 0x000152DA
		void INavigationHandler.OpenEscapeMenu()
		{
			if (this.EscapeMenuEnabled)
			{
				MapScreen instance = MapScreen.Instance;
				if (instance == null)
				{
					return;
				}
				instance.OpenEscapeMenu();
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x000170F4 File Offset: 0x000152F4
		private void ApplyCurrentChanges()
		{
			IChangeableScreen changeableScreen;
			if ((changeableScreen = (ScreenManager.TopScreen as IChangeableScreen)) != null && changeableScreen.AnyUnsavedChanges())
			{
				if (changeableScreen.CanChangesBeApplied())
				{
					changeableScreen.ApplyChanges();
				}
				else
				{
					changeableScreen.ResetChanges();
				}
			}
			if (!this.IsMapTopScreen())
			{
				this._game.GameStateManager.PopState(0);
			}
		}

		// Token: 0x04000154 RID: 340
		private readonly Game _game;

		// Token: 0x04000155 RID: 341
		private NavigationPermissionItem? _mostRecentClanScreenPermission;

		// Token: 0x04000156 RID: 342
		private readonly TextObject _needToBeInKingdomText;

		// Token: 0x04000157 RID: 343
		private readonly MapNavigationHandler.ClanScreenPermissionEvent _clanScreenPermissionEvent;

		// Token: 0x0200007D RID: 125
		public class ClanScreenPermissionEvent : EventBase
		{
			// Token: 0x1700007E RID: 126
			// (get) Token: 0x06000463 RID: 1123 RVA: 0x00023384 File Offset: 0x00021584
			// (set) Token: 0x06000464 RID: 1124 RVA: 0x0002338C File Offset: 0x0002158C
			public Action<bool, TextObject> IsClanScreenAvailable { get; private set; }

			// Token: 0x06000465 RID: 1125 RVA: 0x00023395 File Offset: 0x00021595
			public ClanScreenPermissionEvent(Action<bool, TextObject> isClanScreenAvailable)
			{
				this.IsClanScreenAvailable = isClanScreenAvailable;
			}
		}
	}
}
