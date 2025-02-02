using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Overlay;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Tutorial;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu
{
	// Token: 0x0200008D RID: 141
	public class GameMenuVM : ViewModel
	{
		// Token: 0x17000484 RID: 1156
		// (set) Token: 0x06000DD9 RID: 3545 RVA: 0x00037E93 File Offset: 0x00036093
		public bool IsInspected
		{
			set
			{
				if (this._isInspected == value)
				{
					return;
				}
				this._isInspected = value;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x00037EA6 File Offset: 0x000360A6
		// (set) Token: 0x06000DDB RID: 3547 RVA: 0x00037EAE File Offset: 0x000360AE
		public MenuContext MenuContext { get; private set; }

		// Token: 0x06000DDC RID: 3548 RVA: 0x00037EB8 File Offset: 0x000360B8
		public GameMenuVM(MenuContext menuContext)
		{
			this.MenuContext = menuContext;
			this._gameMenuManager = Campaign.Current.GameMenuManager;
			this.ItemList = new MBBindingList<GameMenuItemVM>();
			this.ProgressItemList = new MBBindingList<GameMenuItemProgressVM>();
			this._shortcutKeys = new Dictionary<GameMenuOption.LeaveType, GameKey>();
			this._menuTextAttributeStrings = new Dictionary<string, string>();
			this._menuTextAttributes = new Dictionary<string, object>();
			this.Background = menuContext.CurrentBackgroundMeshName;
			this.IsInSiegeMode = (PlayerSiege.PlayerSiegeEvent != null);
			Game.Current.EventManager.RegisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x00037F5C File Offset: 0x0003615C
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.ItemList.ApplyActionOnAllItems(delegate(GameMenuItemVM x)
			{
				x.RefreshValues();
			});
			this.ProgressItemList.ApplyActionOnAllItems(delegate(GameMenuItemProgressVM x)
			{
				x.RefreshValues();
			});
			this.Refresh(true);
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x00037FCC File Offset: 0x000361CC
		public void Refresh(bool forceUpdateItems)
		{
			TextObject menuTitle = this.MenuContext.GameMenu.MenuTitle;
			this.TitleText = ((menuTitle != null) ? menuTitle.ToString() : null);
			GameMenu gameMenu = this.MenuContext.GameMenu;
			this.IsEncounterMenu = (gameMenu != null && gameMenu.OverlayType == GameOverlays.MenuOverlayType.Encounter);
			this.Background = (string.IsNullOrEmpty(this.MenuContext.CurrentBackgroundMeshName) ? "wait_guards_stop" : this.MenuContext.CurrentBackgroundMeshName);
			if (forceUpdateItems)
			{
				this.ItemList.Clear();
				this.ProgressItemList.Clear();
				int virtualMenuOptionAmount = this._gameMenuManager.GetVirtualMenuOptionAmount(this.MenuContext);
				for (int i = 0; i < virtualMenuOptionAmount; i++)
				{
					this._gameMenuManager.SetCurrentRepeatableIndex(this.MenuContext, i);
					if (this._gameMenuManager.GetVirtualMenuOptionConditionsHold(this.MenuContext, i))
					{
						TextObject textObject;
						TextObject textObject2;
						if (this._gameMenuManager.GetVirtualGameMenuOption(this.MenuContext, i).IsRepeatable)
						{
							textObject = new TextObject(this._gameMenuManager.GetVirtualMenuOptionText(this.MenuContext, i).ToString(), null);
							textObject2 = new TextObject(this._gameMenuManager.GetVirtualMenuOptionText2(this.MenuContext, i).ToString(), null);
						}
						else
						{
							textObject = this._gameMenuManager.GetVirtualMenuOptionText(this.MenuContext, i);
							textObject2 = this._gameMenuManager.GetVirtualMenuOptionText2(this.MenuContext, i);
						}
						TextObject virtualMenuOptionTooltip = this._gameMenuManager.GetVirtualMenuOptionTooltip(this.MenuContext, i);
						TextObject textObject3 = textObject;
						TextObject textObject4 = textObject2;
						TextObject tooltip = virtualMenuOptionTooltip;
						GameMenu.MenuAndOptionType virtualMenuAndOptionType = this._gameMenuManager.GetVirtualMenuAndOptionType(this.MenuContext);
						GameMenuOption virtualGameMenuOption = this._gameMenuManager.GetVirtualGameMenuOption(this.MenuContext, i);
						GameKey shortcutKey = this._shortcutKeys.ContainsKey(virtualGameMenuOption.OptionLeaveType) ? this._shortcutKeys[virtualGameMenuOption.OptionLeaveType] : null;
						GameMenuItemVM gameMenuItemVM = new GameMenuItemVM(this.MenuContext, i, textObject3, (textObject4 == TextObject.Empty) ? textObject3 : textObject4, tooltip, virtualMenuAndOptionType, virtualGameMenuOption, shortcutKey);
						if (!string.IsNullOrEmpty(this._latestTutorialElementID))
						{
							gameMenuItemVM.IsHighlightEnabled = (gameMenuItemVM.OptionID == this._latestTutorialElementID);
						}
						this.ItemList.Add(gameMenuItemVM);
						if (virtualMenuAndOptionType == GameMenu.MenuAndOptionType.WaitMenuShowOnlyProgressOption || virtualMenuAndOptionType == GameMenu.MenuAndOptionType.WaitMenuShowProgressAndHoursOption)
						{
							this.ProgressItemList.Add(new GameMenuItemProgressVM(this.MenuContext, i));
						}
					}
				}
			}
			MobileParty mainParty = MobileParty.MainParty;
			if (((mainParty != null) ? mainParty.MapEvent : null) != null)
			{
				int[] array = new int[2];
				foreach (PartyBase partyBase in MobileParty.MainParty.MapEvent.InvolvedParties)
				{
					BattleSideEnum side = partyBase.Side;
					BattleSideEnum side2 = PartyBase.MainParty.Side;
					if (partyBase.Side != BattleSideEnum.None)
					{
						array[(int)partyBase.Side] += partyBase.NumberOfHealthyMembers;
					}
				}
				if (MobileParty.MainParty.MapEvent.IsRaid && !this._plunderEventRegistered)
				{
					this.PlunderItems = new MBBindingList<GameMenuPlunderItemVM>();
					CampaignEvents.ItemsLooted.AddNonSerializedListener(this, new Action<MobileParty, ItemRoster>(this.OnItemsPlundered));
					this._plunderEventRegistered = true;
				}
			}
			else if (this._plunderEventRegistered)
			{
				CampaignEvents.ItemsLooted.ClearListeners(this);
				this._plunderEventRegistered = false;
				MBBindingList<GameMenuPlunderItemVM> plunderItems = this.PlunderItems;
				if (plunderItems != null)
				{
					plunderItems.Clear();
				}
			}
			this._requireContextTextUpdate = true;
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x00038324 File Offset: 0x00036524
		public void OnFrameTick()
		{
			this.IsInSiegeMode = (PlayerSiege.PlayerSiegeEvent != null);
			if (this._requireContextTextUpdate)
			{
				this._menuText = this._gameMenuManager.GetMenuText(this.MenuContext);
				this.ContextText = this._menuText.ToString();
				this._menuTextAttributes.Clear();
				this._menuTextAttributeStrings.Clear();
				TextObject menuText = this._menuText;
				if (((menuText != null) ? menuText.Attributes : null) != null)
				{
					foreach (KeyValuePair<string, object> keyValuePair in this._menuText.Attributes)
					{
						this._menuTextAttributes[keyValuePair.Key] = keyValuePair.Value;
						this._menuTextAttributeStrings[keyValuePair.Key] = keyValuePair.Value.ToString();
					}
				}
				this._requireContextTextUpdate = false;
			}
			foreach (GameMenuItemVM gameMenuItemVM in this.ItemList)
			{
				gameMenuItemVM.Refresh();
			}
			foreach (GameMenuItemProgressVM gameMenuItemProgressVM in this.ProgressItemList)
			{
				gameMenuItemProgressVM.OnTick();
			}
			if (Campaign.Current.GameMode == CampaignGameMode.Campaign)
			{
				this.IsNight = Campaign.Current.IsNight;
			}
			this._requireContextTextUpdate = this.IsMenuTextChanged();
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x000384BC File Offset: 0x000366BC
		private bool IsMenuTextChanged()
		{
			GameMenuManager gameMenuManager = this._gameMenuManager;
			TextObject textObject = (gameMenuManager != null) ? gameMenuManager.GetMenuText(this.MenuContext) : null;
			if (this._menuText != textObject)
			{
				return true;
			}
			int count = this._menuTextAttributes.Count;
			TextObject menuText = this._menuText;
			int? num;
			if (menuText == null)
			{
				num = null;
			}
			else
			{
				Dictionary<string, object> attributes = menuText.Attributes;
				num = ((attributes != null) ? new int?(attributes.Count) : null);
			}
			int? num2 = num;
			if (!(count == num2.GetValueOrDefault() & num2 != null))
			{
				return true;
			}
			foreach (string key in this._menuTextAttributes.Keys)
			{
				object obj = null;
				object obj2 = this._menuTextAttributes[key];
				TextObject menuText2 = this._menuText;
				if (menuText2 == null || !menuText2.Attributes.TryGetValue(key, out obj))
				{
					return true;
				}
				if (obj2 != obj)
				{
					return true;
				}
				if (this._menuTextAttributeStrings[key] != obj.ToString())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x000385EC File Offset: 0x000367EC
		public void UpdateMenuContext(MenuContext newMenuContext)
		{
			this.MenuContext = newMenuContext;
			this.Refresh(true);
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x000385FC File Offset: 0x000367FC
		public override void OnFinalize()
		{
			CampaignEvents.ItemsLooted.ClearListeners(this);
			Game.Current.EventManager.UnregisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
			this._gameMenuManager = null;
			this.MenuContext = null;
			this.ItemList.ApplyActionOnAllItems(delegate(GameMenuItemVM x)
			{
				x.OnFinalize();
			});
			this.ItemList.Clear();
			this.ItemList = null;
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x00038679 File Offset: 0x00036879
		public void AddHotKey(GameMenuOption.LeaveType leaveType, GameKey gameKey)
		{
			if (this._shortcutKeys.ContainsKey(leaveType))
			{
				this._shortcutKeys[leaveType] = gameKey;
				return;
			}
			this._shortcutKeys.Add(leaveType, gameKey);
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x000386A4 File Offset: 0x000368A4
		private void OnItemsPlundered(MobileParty mobileParty, ItemRoster newItems)
		{
			if (mobileParty == MobileParty.MainParty)
			{
				foreach (ItemRosterElement item in newItems)
				{
					for (int i = 0; i < item.Amount; i++)
					{
						this.PlunderItems.Add(new GameMenuPlunderItemVM(item));
					}
				}
			}
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x00038710 File Offset: 0x00036910
		public void ExecuteLink(string link)
		{
			Campaign.Current.EncyclopediaManager.GoToLink(link);
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x00038724 File Offset: 0x00036924
		protected virtual GameMenuItemVM CreateGameMenuItemVM(int indexOfMenuCondition)
		{
			GameMenuOption virtualGameMenuOption = this._gameMenuManager.GetVirtualGameMenuOption(this.MenuContext, indexOfMenuCondition);
			GameKey shortcutKey = this._shortcutKeys.ContainsKey(virtualGameMenuOption.OptionLeaveType) ? this._shortcutKeys[virtualGameMenuOption.OptionLeaveType] : null;
			return new GameMenuItemVM(this.MenuContext, indexOfMenuCondition, TextObject.Empty, TextObject.Empty, TextObject.Empty, GameMenu.MenuAndOptionType.RegularMenuOption, virtualGameMenuOption, shortcutKey);
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x0003878A File Offset: 0x0003698A
		// (set) Token: 0x06000DE8 RID: 3560 RVA: 0x00038792 File Offset: 0x00036992
		[DataSourceProperty]
		public bool IsNight
		{
			get
			{
				return this._isNight;
			}
			set
			{
				if (value != this._isNight)
				{
					this._isNight = value;
					base.OnPropertyChangedWithValue(value, "IsNight");
				}
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x000387B0 File Offset: 0x000369B0
		// (set) Token: 0x06000DEA RID: 3562 RVA: 0x000387B8 File Offset: 0x000369B8
		[DataSourceProperty]
		public bool IsInSiegeMode
		{
			get
			{
				return this._isInSiegeMode;
			}
			set
			{
				if (value != this._isInSiegeMode)
				{
					this._isInSiegeMode = value;
					base.OnPropertyChangedWithValue(value, "IsInSiegeMode");
				}
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x000387D6 File Offset: 0x000369D6
		// (set) Token: 0x06000DEC RID: 3564 RVA: 0x000387DE File Offset: 0x000369DE
		[DataSourceProperty]
		public bool IsEncounterMenu
		{
			get
			{
				return this._isEncounterMenu;
			}
			set
			{
				if (value != this._isEncounterMenu)
				{
					this._isEncounterMenu = value;
					base.OnPropertyChangedWithValue(value, "IsEncounterMenu");
				}
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x000387FC File Offset: 0x000369FC
		// (set) Token: 0x06000DEE RID: 3566 RVA: 0x00038804 File Offset: 0x00036A04
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

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000DEF RID: 3567 RVA: 0x00038827 File Offset: 0x00036A27
		// (set) Token: 0x06000DF0 RID: 3568 RVA: 0x0003882F File Offset: 0x00036A2F
		[DataSourceProperty]
		public string ContextText
		{
			get
			{
				return this._contextText;
			}
			set
			{
				if (value != this._contextText)
				{
					this._contextText = value;
					base.OnPropertyChangedWithValue<string>(value, "ContextText");
				}
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x00038852 File Offset: 0x00036A52
		// (set) Token: 0x06000DF2 RID: 3570 RVA: 0x0003885A File Offset: 0x00036A5A
		[DataSourceProperty]
		public MBBindingList<GameMenuItemVM> ItemList
		{
			get
			{
				return this._itemList;
			}
			set
			{
				if (value != this._itemList)
				{
					this._itemList = value;
					base.OnPropertyChangedWithValue<MBBindingList<GameMenuItemVM>>(value, "ItemList");
				}
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x00038878 File Offset: 0x00036A78
		// (set) Token: 0x06000DF4 RID: 3572 RVA: 0x00038880 File Offset: 0x00036A80
		[DataSourceProperty]
		public MBBindingList<GameMenuItemProgressVM> ProgressItemList
		{
			get
			{
				return this._progressItemList;
			}
			set
			{
				if (value != this._progressItemList)
				{
					this._progressItemList = value;
					base.OnPropertyChangedWithValue<MBBindingList<GameMenuItemProgressVM>>(value, "ProgressItemList");
				}
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x0003889E File Offset: 0x00036A9E
		// (set) Token: 0x06000DF6 RID: 3574 RVA: 0x000388A6 File Offset: 0x00036AA6
		[DataSourceProperty]
		public string Background
		{
			get
			{
				return this._background;
			}
			set
			{
				if (value != this._background)
				{
					this._background = value;
					base.OnPropertyChangedWithValue<string>(value, "Background");
				}
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x000388C9 File Offset: 0x00036AC9
		// (set) Token: 0x06000DF8 RID: 3576 RVA: 0x000388D1 File Offset: 0x00036AD1
		[DataSourceProperty]
		public MBBindingList<GameMenuPlunderItemVM> PlunderItems
		{
			get
			{
				return this._plunderItems;
			}
			set
			{
				if (value != this._plunderItems)
				{
					this._plunderItems = value;
					base.OnPropertyChangedWithValue<MBBindingList<GameMenuPlunderItemVM>>(value, "PlunderItems");
				}
			}
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x000388F0 File Offset: 0x00036AF0
		private void OnTutorialNotificationElementIDChange(TutorialNotificationElementChangeEvent obj)
		{
			if (obj.NewNotificationElementID != this._latestTutorialElementID)
			{
				this._latestTutorialElementID = obj.NewNotificationElementID;
			}
			if (this._latestTutorialElementID != null)
			{
				if (this._latestTutorialElementID != string.Empty)
				{
					if (this._latestTutorialElementID == "town_backstreet" && !this._isTavernButtonHighlightApplied)
					{
						this._isTavernButtonHighlightApplied = this.SetGameMenuButtonHighlightState(this._latestTutorialElementID, true);
					}
					else if (this._latestTutorialElementID != "town_backstreet" && this._isTavernButtonHighlightApplied)
					{
						this._isTavernButtonHighlightApplied = !this.SetGameMenuButtonHighlightState("town_backstreet", false);
					}
					if (this._latestTutorialElementID == "sell_all_prisoners" && !this._isSellPrisonerButtonHighlightApplied)
					{
						this._isSellPrisonerButtonHighlightApplied = this.SetGameMenuButtonHighlightState(this._latestTutorialElementID, true);
					}
					else if (this._latestTutorialElementID != "sell_all_prisoners" && this._isSellPrisonerButtonHighlightApplied)
					{
						this._isSellPrisonerButtonHighlightApplied = !this.SetGameMenuButtonHighlightState("sell_all_prisoners", false);
					}
					if (this._latestTutorialElementID == "storymode_tutorial_village_buy" && !this._isShopButtonHighlightApplied)
					{
						this._isShopButtonHighlightApplied = this.SetGameMenuButtonHighlightState(this._latestTutorialElementID, true);
					}
					else if (this._latestTutorialElementID != "storymode_tutorial_village_buy" && this._isShopButtonHighlightApplied)
					{
						this._isShopButtonHighlightApplied = !this.SetGameMenuButtonHighlightState("storymode_tutorial_village_buy", false);
					}
					if (this._latestTutorialElementID == "storymode_tutorial_village_recruit" && !this._isRecruitButtonHighlightApplied)
					{
						this._isRecruitButtonHighlightApplied = this.SetGameMenuButtonHighlightState(this._latestTutorialElementID, true);
					}
					else if (this._latestTutorialElementID != "storymode_tutorial_village_recruit" && this._isRecruitButtonHighlightApplied)
					{
						this._isRecruitButtonHighlightApplied = !this.SetGameMenuButtonHighlightState("storymode_tutorial_village_recruit", false);
					}
					if (this._latestTutorialElementID == "hostile_action" && !this._isHostileActionButtonHighlightApplied)
					{
						this._isHostileActionButtonHighlightApplied = this.SetGameMenuButtonHighlightState(this._latestTutorialElementID, true);
					}
					else if (this._latestTutorialElementID != "hostile_action" && this._isHostileActionButtonHighlightApplied)
					{
						this._isHostileActionButtonHighlightApplied = !this.SetGameMenuButtonHighlightState("hostile_action", false);
					}
					if (this._latestTutorialElementID == "town_besiege" && !this._isTownBesiegeButtonHighlightApplied)
					{
						this._isTownBesiegeButtonHighlightApplied = this.SetGameMenuButtonHighlightState(this._latestTutorialElementID, true);
					}
					else if (this._latestTutorialElementID != "town_besiege" && this._isTownBesiegeButtonHighlightApplied)
					{
						this._isTownBesiegeButtonHighlightApplied = !this.SetGameMenuButtonHighlightState("town_besiege", false);
					}
					if (this._latestTutorialElementID == "storymode_tutorial_village_enter" && !this._isEnterTutorialVillageButtonHighlightApplied)
					{
						this._isEnterTutorialVillageButtonHighlightApplied = this.SetGameMenuButtonHighlightState(this._latestTutorialElementID, true);
						return;
					}
					if (this._latestTutorialElementID != "storymode_tutorial_village_enter" && this._isEnterTutorialVillageButtonHighlightApplied)
					{
						this._isEnterTutorialVillageButtonHighlightApplied = !this.SetGameMenuButtonHighlightState("storymode_tutorial_village_enter", false);
						return;
					}
				}
				else
				{
					if (this._isTavernButtonHighlightApplied)
					{
						this._isTavernButtonHighlightApplied = !this.SetGameMenuButtonHighlightState("town_backstreet", false);
					}
					if (this._isSellPrisonerButtonHighlightApplied)
					{
						this._isSellPrisonerButtonHighlightApplied = !this.SetGameMenuButtonHighlightState("sell_all_prisoners", false);
					}
					if (this._isShopButtonHighlightApplied)
					{
						this._isShopButtonHighlightApplied = !this.SetGameMenuButtonHighlightState("storymode_tutorial_village_buy", false);
					}
					if (this._isRecruitButtonHighlightApplied)
					{
						this._isRecruitButtonHighlightApplied = !this.SetGameMenuButtonHighlightState("storymode_tutorial_village_recruit", false);
					}
					if (this._isHostileActionButtonHighlightApplied)
					{
						this._isHostileActionButtonHighlightApplied = !this.SetGameMenuButtonHighlightState("hostile_action", false);
					}
					if (this._isTownBesiegeButtonHighlightApplied)
					{
						this._isTownBesiegeButtonHighlightApplied = !this.SetGameMenuButtonHighlightState("town_besiege", false);
					}
					if (this._isEnterTutorialVillageButtonHighlightApplied)
					{
						this._isEnterTutorialVillageButtonHighlightApplied = !this.SetGameMenuButtonHighlightState("storymode_tutorial_village_enter", false);
						return;
					}
				}
			}
			else
			{
				if (this._isTavernButtonHighlightApplied)
				{
					this._isTavernButtonHighlightApplied = !this.SetGameMenuButtonHighlightState("town_backstreet", false);
				}
				if (this._isSellPrisonerButtonHighlightApplied)
				{
					this._isSellPrisonerButtonHighlightApplied = !this.SetGameMenuButtonHighlightState("sell_all_prisoners", false);
				}
				if (this._isShopButtonHighlightApplied)
				{
					this._isShopButtonHighlightApplied = !this.SetGameMenuButtonHighlightState("storymode_tutorial_village_buy", false);
				}
				if (this._isRecruitButtonHighlightApplied)
				{
					this._isRecruitButtonHighlightApplied = !this.SetGameMenuButtonHighlightState("storymode_tutorial_village_recruit", false);
				}
				if (this._isHostileActionButtonHighlightApplied)
				{
					this._isHostileActionButtonHighlightApplied = !this.SetGameMenuButtonHighlightState("hostile_action", false);
				}
				if (this._isTownBesiegeButtonHighlightApplied)
				{
					this._isTownBesiegeButtonHighlightApplied = !this.SetGameMenuButtonHighlightState("town_besiege", false);
				}
				if (this._isEnterTutorialVillageButtonHighlightApplied)
				{
					this._isEnterTutorialVillageButtonHighlightApplied = !this.SetGameMenuButtonHighlightState("storymode_tutorial_village_enter", false);
				}
			}
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00038D70 File Offset: 0x00036F70
		private bool SetGameMenuButtonHighlightState(string buttonID, bool state)
		{
			foreach (GameMenuItemVM gameMenuItemVM in this.ItemList)
			{
				if (gameMenuItemVM.OptionID == buttonID)
				{
					gameMenuItemVM.IsHighlightEnabled = state;
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000662 RID: 1634
		private bool _isInspected;

		// Token: 0x04000663 RID: 1635
		private bool _plunderEventRegistered;

		// Token: 0x04000664 RID: 1636
		private GameMenuManager _gameMenuManager;

		// Token: 0x04000665 RID: 1637
		private Dictionary<GameMenuOption.LeaveType, GameKey> _shortcutKeys;

		// Token: 0x04000666 RID: 1638
		private Dictionary<string, string> _menuTextAttributeStrings;

		// Token: 0x04000667 RID: 1639
		private Dictionary<string, object> _menuTextAttributes;

		// Token: 0x04000668 RID: 1640
		private TextObject _menuText = TextObject.Empty;

		// Token: 0x0400066A RID: 1642
		private MBBindingList<GameMenuItemVM> _itemList;

		// Token: 0x0400066B RID: 1643
		private MBBindingList<GameMenuItemProgressVM> _progressItemList;

		// Token: 0x0400066C RID: 1644
		private string _titleText;

		// Token: 0x0400066D RID: 1645
		private string _contextText;

		// Token: 0x0400066E RID: 1646
		private string _background;

		// Token: 0x0400066F RID: 1647
		private bool _isNight;

		// Token: 0x04000670 RID: 1648
		private bool _isInSiegeMode;

		// Token: 0x04000671 RID: 1649
		private bool _isEncounterMenu;

		// Token: 0x04000672 RID: 1650
		private MBBindingList<GameMenuPlunderItemVM> _plunderItems;

		// Token: 0x04000673 RID: 1651
		private string _latestTutorialElementID;

		// Token: 0x04000674 RID: 1652
		private bool _isTavernButtonHighlightApplied;

		// Token: 0x04000675 RID: 1653
		private bool _isSellPrisonerButtonHighlightApplied;

		// Token: 0x04000676 RID: 1654
		private bool _isShopButtonHighlightApplied;

		// Token: 0x04000677 RID: 1655
		private bool _isRecruitButtonHighlightApplied;

		// Token: 0x04000678 RID: 1656
		private bool _isHostileActionButtonHighlightApplied;

		// Token: 0x04000679 RID: 1657
		private bool _isTownBesiegeButtonHighlightApplied;

		// Token: 0x0400067A RID: 1658
		private bool _isEnterTutorialVillageButtonHighlightApplied;

		// Token: 0x0400067B RID: 1659
		private bool _requireContextTextUpdate;
	}
}
