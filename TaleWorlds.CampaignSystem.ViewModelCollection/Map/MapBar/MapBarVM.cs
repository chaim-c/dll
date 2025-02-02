using System;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Core.ViewModelCollection.Tutorial;
using TaleWorlds.Library;
using TaleWorlds.Library.EventSystem;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapBar
{
	// Token: 0x02000052 RID: 82
	public class MapBarVM : ViewModel
	{
		// Token: 0x060005DA RID: 1498 RVA: 0x0001CCEC File Offset: 0x0001AEEC
		public MapBarVM(INavigationHandler navigationHandler, IMapStateHandler mapStateHandler, Func<MapBarShortcuts> getMapBarShortcuts, Action openArmyManagement)
		{
			this._openArmyManagement = openArmyManagement;
			this._mapStateHandler = mapStateHandler;
			this._refreshTimeSpan = ((Campaign.Current.GetSimplifiedTimeControlMode() == CampaignTimeControlMode.UnstoppableFastForward) ? 0.1f : 2f);
			this._needToBePartOfKingdomText = GameTexts.FindText("str_need_to_be_a_part_of_kingdom", null);
			this._cannotGatherWhileInEventText = GameTexts.FindText("str_cannot_gather_army_while_in_event", null);
			this._needToBeLeaderToManageText = GameTexts.FindText("str_need_to_be_leader_of_army_to_manage", null);
			this._mercenaryCannotManageText = GameTexts.FindText("str_mercenary_cannot_manage_army", null);
			this._cannotGatherWhileInConversationText = GameTexts.FindText("str_cannot_gather_army_during_conversation", null);
			this._cannotGatherWhileInSiegeText = GameTexts.FindText("str_cannot_gather_army_during_siege", null);
			this.TutorialNotification = new ElementNotificationVM();
			this.MapInfo = new MapInfoVM();
			this.MapTimeControl = new MapTimeControlVM(getMapBarShortcuts, new Action(this.OnTimeControlChange), delegate()
			{
				mapStateHandler.ResetCamera(false, false);
			});
			this.MapNavigation = new MapNavigationVM(navigationHandler, getMapBarShortcuts);
			this.GatherArmyHint = new HintViewModel();
			this.OnRefresh();
			this.IsEnabled = true;
			Game.Current.EventManager.RegisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001CE1F File Offset: 0x0001B01F
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.MapInfo.RefreshValues();
			this.MapTimeControl.RefreshValues();
			this.MapNavigation.RefreshValues();
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0001CE48 File Offset: 0x0001B048
		public void OnRefresh()
		{
			this.MapInfo.Refresh();
			this.MapTimeControl.Refresh();
			this.MapNavigation.Refresh();
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001CE6C File Offset: 0x0001B06C
		public void Tick(float dt)
		{
			int simplifiedTimeControlMode = (int)Campaign.Current.GetSimplifiedTimeControlMode();
			this._refreshTimeSpan -= dt;
			if (this._refreshTimeSpan < 0f)
			{
				this.OnRefresh();
				this._refreshTimeSpan = ((simplifiedTimeControlMode == 2) ? 0.1f : 0.2f);
			}
			this.MapInfo.Tick();
			this.MapTimeControl.Tick();
			this.MapNavigation.Tick();
			if (this._mapStateHandler != null)
			{
				this.IsCameraCentered = this._mapStateHandler.IsCameraLockedToPlayerParty();
			}
			this.IsGatherArmyVisible = this.GetIsGatherArmyVisible();
			if (this.IsGatherArmyVisible)
			{
				this.UpdateCanGatherArmyAndReason();
			}
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001CF10 File Offset: 0x0001B110
		private void UpdateCanGatherArmyAndReason()
		{
			bool canGatherArmy = true;
			TextObject textObject = null;
			IFaction mapFaction = Hero.MainHero.MapFaction;
			if (mapFaction != null && !mapFaction.IsKingdomFaction)
			{
				textObject = this._needToBePartOfKingdomText;
				canGatherArmy = false;
			}
			else if (MobileParty.MainParty.MapEvent != null)
			{
				textObject = this._cannotGatherWhileInEventText;
				canGatherArmy = false;
			}
			else if (MobileParty.MainParty.Army != null && MobileParty.MainParty.Army.LeaderParty != MobileParty.MainParty)
			{
				textObject = this._needToBeLeaderToManageText;
				canGatherArmy = false;
			}
			else if (Clan.PlayerClan.IsUnderMercenaryService)
			{
				textObject = this._mercenaryCannotManageText;
				canGatherArmy = false;
			}
			else if (PlayerEncounter.Current != null && PlayerEncounter.EncounterSettlement == null)
			{
				textObject = this._cannotGatherWhileInConversationText;
				canGatherArmy = false;
			}
			else if (PlayerSiege.PlayerSiegeEvent != null)
			{
				textObject = this._cannotGatherWhileInSiegeText;
				canGatherArmy = false;
			}
			this.CanGatherArmy = canGatherArmy;
			this.GatherArmyHint.HintText = (this.CanGatherArmy ? TextObject.Empty : textObject);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001CFF0 File Offset: 0x0001B1F0
		private bool GetIsGatherArmyVisible()
		{
			if (this.MapTimeControl.IsInMap)
			{
				MobileParty mainParty = MobileParty.MainParty;
				if (((mainParty != null) ? mainParty.Army : null) == null && !Hero.MainHero.IsPrisoner && Hero.MainHero.PartyBelongedTo != null && MobileParty.MainParty.MapEvent == null)
				{
					return this.MapTimeControl.IsCenterPanelEnabled;
				}
			}
			return false;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001D04E File Offset: 0x0001B24E
		private void OnTimeControlChange()
		{
			this._refreshTimeSpan = ((Campaign.Current.GetSimplifiedTimeControlMode() == CampaignTimeControlMode.UnstoppableFastForward) ? 0.1f : 2f);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001D06F File Offset: 0x0001B26F
		private void ExecuteResetCamera()
		{
			IMapStateHandler mapStateHandler = this._mapStateHandler;
			if (mapStateHandler == null)
			{
				return;
			}
			mapStateHandler.FastMoveCameraToMainParty();
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001D081 File Offset: 0x0001B281
		public void ExecuteArmyManagement()
		{
			this._openArmyManagement();
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001D090 File Offset: 0x0001B290
		private void OnTutorialNotificationElementIDChange(TutorialNotificationElementChangeEvent obj)
		{
			if (obj.NewNotificationElementID != this._latestTutorialElementID)
			{
				if (this._latestTutorialElementID != null)
				{
					this.TutorialNotification.ElementID = string.Empty;
				}
				this._latestTutorialElementID = obj.NewNotificationElementID;
				if (this._latestTutorialElementID != null)
				{
					this.TutorialNotification.ElementID = this._latestTutorialElementID;
					if (this._latestTutorialElementID == "PartySpeedLabel" && !this.MapInfo.IsInfoBarExtended)
					{
						this.MapInfo.IsInfoBarExtended = true;
					}
				}
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001D118 File Offset: 0x0001B318
		public override void OnFinalize()
		{
			base.OnFinalize();
			this._mapStateHandler = null;
			MapNavigationVM mapNavigation = this._mapNavigation;
			if (mapNavigation != null)
			{
				mapNavigation.OnFinalize();
			}
			MapTimeControlVM mapTimeControl = this._mapTimeControl;
			if (mapTimeControl != null)
			{
				mapTimeControl.OnFinalize();
			}
			this._mapInfo = null;
			this._mapNavigation = null;
			this._mapTimeControl = null;
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

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x0001D18E File Offset: 0x0001B38E
		// (set) Token: 0x060005E6 RID: 1510 RVA: 0x0001D196 File Offset: 0x0001B396
		[DataSourceProperty]
		public MapInfoVM MapInfo
		{
			get
			{
				return this._mapInfo;
			}
			set
			{
				if (value != this._mapInfo)
				{
					this._mapInfo = value;
					base.OnPropertyChangedWithValue<MapInfoVM>(value, "MapInfo");
				}
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x0001D1B4 File Offset: 0x0001B3B4
		// (set) Token: 0x060005E8 RID: 1512 RVA: 0x0001D1BC File Offset: 0x0001B3BC
		[DataSourceProperty]
		public MapTimeControlVM MapTimeControl
		{
			get
			{
				return this._mapTimeControl;
			}
			set
			{
				if (value != this._mapTimeControl)
				{
					this._mapTimeControl = value;
					base.OnPropertyChangedWithValue<MapTimeControlVM>(value, "MapTimeControl");
				}
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x0001D1DA File Offset: 0x0001B3DA
		// (set) Token: 0x060005EA RID: 1514 RVA: 0x0001D1E2 File Offset: 0x0001B3E2
		[DataSourceProperty]
		public MapNavigationVM MapNavigation
		{
			get
			{
				return this._mapNavigation;
			}
			set
			{
				if (value != this._mapNavigation)
				{
					this._mapNavigation = value;
					base.OnPropertyChangedWithValue<MapNavigationVM>(value, "MapNavigation");
				}
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0001D200 File Offset: 0x0001B400
		// (set) Token: 0x060005EC RID: 1516 RVA: 0x0001D208 File Offset: 0x0001B408
		[DataSourceProperty]
		public bool IsGatherArmyVisible
		{
			get
			{
				return this._isGatherArmyVisible;
			}
			set
			{
				if (value != this._isGatherArmyVisible)
				{
					this._isGatherArmyVisible = value;
					base.OnPropertyChangedWithValue(value, "IsGatherArmyVisible");
				}
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x0001D226 File Offset: 0x0001B426
		// (set) Token: 0x060005EE RID: 1518 RVA: 0x0001D22E File Offset: 0x0001B42E
		[DataSourceProperty]
		public bool IsInInfoMode
		{
			get
			{
				return this._isInInfoMode;
			}
			set
			{
				if (value != this._isInInfoMode)
				{
					this._isInInfoMode = value;
					base.OnPropertyChangedWithValue(value, "IsInInfoMode");
				}
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x0001D24C File Offset: 0x0001B44C
		// (set) Token: 0x060005F0 RID: 1520 RVA: 0x0001D254 File Offset: 0x0001B454
		[DataSourceProperty]
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				if (value != this._isEnabled)
				{
					this._isEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsEnabled");
				}
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0001D272 File Offset: 0x0001B472
		// (set) Token: 0x060005F2 RID: 1522 RVA: 0x0001D27A File Offset: 0x0001B47A
		[DataSourceProperty]
		public bool CanGatherArmy
		{
			get
			{
				return this._canGatherArmy;
			}
			set
			{
				if (value != this._canGatherArmy)
				{
					this._canGatherArmy = value;
					base.OnPropertyChangedWithValue(value, "CanGatherArmy");
				}
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0001D298 File Offset: 0x0001B498
		// (set) Token: 0x060005F4 RID: 1524 RVA: 0x0001D2A0 File Offset: 0x0001B4A0
		[DataSourceProperty]
		public HintViewModel GatherArmyHint
		{
			get
			{
				return this._gatherArmyHint;
			}
			set
			{
				if (value != this._gatherArmyHint)
				{
					this._gatherArmyHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "GatherArmyHint");
				}
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0001D2BE File Offset: 0x0001B4BE
		// (set) Token: 0x060005F6 RID: 1526 RVA: 0x0001D2C6 File Offset: 0x0001B4C6
		[DataSourceProperty]
		public bool IsCameraCentered
		{
			get
			{
				return this._isCameraCentered;
			}
			set
			{
				if (value != this._isCameraCentered)
				{
					this._isCameraCentered = value;
					base.OnPropertyChangedWithValue(value, "IsCameraCentered");
				}
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x0001D2E4 File Offset: 0x0001B4E4
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x0001D2EC File Offset: 0x0001B4EC
		[DataSourceProperty]
		public string CurrentScreen
		{
			get
			{
				return this._currentScreen;
			}
			set
			{
				if (this._currentScreen != value)
				{
					this._currentScreen = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentScreen");
				}
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x0001D30F File Offset: 0x0001B50F
		// (set) Token: 0x060005FA RID: 1530 RVA: 0x0001D317 File Offset: 0x0001B517
		[DataSourceProperty]
		public ElementNotificationVM TutorialNotification
		{
			get
			{
				return this._tutorialNotification;
			}
			set
			{
				if (value != this._tutorialNotification)
				{
					this._tutorialNotification = value;
					base.OnPropertyChangedWithValue<ElementNotificationVM>(value, "TutorialNotification");
				}
			}
		}

		// Token: 0x04000288 RID: 648
		private IMapStateHandler _mapStateHandler;

		// Token: 0x04000289 RID: 649
		private readonly TextObject _needToBePartOfKingdomText;

		// Token: 0x0400028A RID: 650
		private readonly TextObject _cannotGatherWhileInEventText;

		// Token: 0x0400028B RID: 651
		private readonly TextObject _needToBeLeaderToManageText;

		// Token: 0x0400028C RID: 652
		private readonly TextObject _mercenaryCannotManageText;

		// Token: 0x0400028D RID: 653
		private readonly TextObject _cannotGatherWhileInConversationText;

		// Token: 0x0400028E RID: 654
		private readonly TextObject _cannotGatherWhileInSiegeText;

		// Token: 0x0400028F RID: 655
		private readonly Action _openArmyManagement;

		// Token: 0x04000290 RID: 656
		private float _refreshTimeSpan;

		// Token: 0x04000291 RID: 657
		private string _latestTutorialElementID;

		// Token: 0x04000292 RID: 658
		private bool _isGatherArmyVisible;

		// Token: 0x04000293 RID: 659
		private MapInfoVM _mapInfo;

		// Token: 0x04000294 RID: 660
		private MapTimeControlVM _mapTimeControl;

		// Token: 0x04000295 RID: 661
		private MapNavigationVM _mapNavigation;

		// Token: 0x04000296 RID: 662
		private bool _isEnabled;

		// Token: 0x04000297 RID: 663
		private bool _isCameraCentered;

		// Token: 0x04000298 RID: 664
		private bool _canGatherArmy;

		// Token: 0x04000299 RID: 665
		private bool _isInInfoMode;

		// Token: 0x0400029A RID: 666
		private string _currentScreen;

		// Token: 0x0400029B RID: 667
		private HintViewModel _gatherArmyHint;

		// Token: 0x0400029C RID: 668
		private ElementNotificationVM _tutorialNotification;
	}
}
