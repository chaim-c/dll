using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Overlay;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.GameMenus
{
	// Token: 0x020000DC RID: 220
	public class GameMenu
	{
		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x0600138B RID: 5003 RVA: 0x000582E2 File Offset: 0x000564E2
		// (set) Token: 0x0600138C RID: 5004 RVA: 0x000582EA File Offset: 0x000564EA
		public GameMenu.MenuAndOptionType Type { get; private set; }

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x0600138D RID: 5005 RVA: 0x000582F3 File Offset: 0x000564F3
		// (set) Token: 0x0600138E RID: 5006 RVA: 0x000582FB File Offset: 0x000564FB
		public string StringId { get; private set; }

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x00058304 File Offset: 0x00056504
		// (set) Token: 0x06001390 RID: 5008 RVA: 0x0005830C File Offset: 0x0005650C
		public object RelatedObject { get; private set; }

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001391 RID: 5009 RVA: 0x00058315 File Offset: 0x00056515
		// (set) Token: 0x06001392 RID: 5010 RVA: 0x0005831D File Offset: 0x0005651D
		public TextObject MenuTitle { get; private set; }

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x00058326 File Offset: 0x00056526
		// (set) Token: 0x06001394 RID: 5012 RVA: 0x0005832E File Offset: 0x0005652E
		public GameOverlays.MenuOverlayType OverlayType { get; private set; }

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001395 RID: 5013 RVA: 0x00058337 File Offset: 0x00056537
		// (set) Token: 0x06001396 RID: 5014 RVA: 0x0005833F File Offset: 0x0005653F
		public bool IsReady { get; private set; }

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001397 RID: 5015 RVA: 0x00058348 File Offset: 0x00056548
		public int MenuItemAmount
		{
			get
			{
				return this._menuItems.Count;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001398 RID: 5016 RVA: 0x00058355 File Offset: 0x00056555
		public object CurrentRepeatableObject
		{
			get
			{
				if (this.MenuRepeatObjects.Count <= this.CurrentRepeatableIndex)
				{
					return null;
				}
				return this.MenuRepeatObjects[this.CurrentRepeatableIndex];
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001399 RID: 5017 RVA: 0x0005837D File Offset: 0x0005657D
		// (set) Token: 0x0600139A RID: 5018 RVA: 0x00058385 File Offset: 0x00056585
		public bool IsWaitMenu { get; private set; }

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x0600139B RID: 5019 RVA: 0x0005838E File Offset: 0x0005658E
		// (set) Token: 0x0600139C RID: 5020 RVA: 0x00058396 File Offset: 0x00056596
		public bool IsWaitActive { get; private set; }

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x0600139D RID: 5021 RVA: 0x0005839F File Offset: 0x0005659F
		public bool IsEmpty
		{
			get
			{
				return this.MenuRepeatObjects.Count == 0 && this.MenuItemAmount == 0;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x0600139E RID: 5022 RVA: 0x000583B9 File Offset: 0x000565B9
		// (set) Token: 0x0600139F RID: 5023 RVA: 0x000583C1 File Offset: 0x000565C1
		public float Progress { get; private set; }

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x000583CA File Offset: 0x000565CA
		// (set) Token: 0x060013A1 RID: 5025 RVA: 0x000583D2 File Offset: 0x000565D2
		public float TargetWaitHours { get; private set; }

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x000583DB File Offset: 0x000565DB
		// (set) Token: 0x060013A3 RID: 5027 RVA: 0x000583E3 File Offset: 0x000565E3
		public OnTickDelegate OnTick { get; private set; }

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060013A4 RID: 5028 RVA: 0x000583EC File Offset: 0x000565EC
		// (set) Token: 0x060013A5 RID: 5029 RVA: 0x000583F4 File Offset: 0x000565F4
		public OnConditionDelegate OnCondition { get; private set; }

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x000583FD File Offset: 0x000565FD
		// (set) Token: 0x060013A7 RID: 5031 RVA: 0x00058405 File Offset: 0x00056605
		public OnConsequenceDelegate OnConsequence { get; private set; }

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x0005840E File Offset: 0x0005660E
		// (set) Token: 0x060013A9 RID: 5033 RVA: 0x00058416 File Offset: 0x00056616
		public int CurrentRepeatableIndex { get; set; }

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x0005841F File Offset: 0x0005661F
		public IEnumerable<GameMenuOption> MenuOptions
		{
			get
			{
				return this._menuItems;
			}
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x00058427 File Offset: 0x00056627
		internal GameMenu(string idString)
		{
			this.StringId = idString;
			this._menuItems = new List<GameMenuOption>();
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x0005844C File Offset: 0x0005664C
		internal void Initialize(TextObject text, OnInitDelegate initDelegate, GameOverlays.MenuOverlayType overlay, GameMenu.MenuFlags flags = GameMenu.MenuFlags.None, object relatedObject = null)
		{
			this.CurrentRepeatableIndex = 0;
			this.LastSelectedMenuObject = null;
			this._defaultText = text;
			this.OnInit = initDelegate;
			this.OverlayType = overlay;
			this.AutoSelectFirst = ((flags & GameMenu.MenuFlags.AutoSelectFirst) > GameMenu.MenuFlags.None);
			this.RelatedObject = relatedObject;
			this.IsReady = true;
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x00058498 File Offset: 0x00056698
		internal void Initialize(TextObject text, OnInitDelegate initDelegate, OnConditionDelegate condition, OnConsequenceDelegate consequence, OnTickDelegate tick, GameMenu.MenuAndOptionType type, GameOverlays.MenuOverlayType overlay, float targetWaitHours = 0f, GameMenu.MenuFlags flags = GameMenu.MenuFlags.None, object relatedObject = null)
		{
			this.CurrentRepeatableIndex = 0;
			this.LastSelectedMenuObject = null;
			this._defaultText = text;
			this.OnInit = initDelegate;
			this.OverlayType = overlay;
			this.AutoSelectFirst = ((flags & GameMenu.MenuFlags.AutoSelectFirst) > GameMenu.MenuFlags.None);
			this.RelatedObject = relatedObject;
			this.OnConsequence = consequence;
			this.OnCondition = condition;
			this.Type = type;
			this.OnTick = tick;
			this.TargetWaitHours = targetWaitHours;
			this.IsWaitMenu = (type > GameMenu.MenuAndOptionType.RegularMenuOption);
			this.IsReady = true;
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x00058517 File Offset: 0x00056717
		private void AddOption(GameMenuOption newOption, int index = -1)
		{
			if (index >= 0 && this._menuItems.Count >= index)
			{
				this._menuItems.Insert(index, newOption);
				return;
			}
			this._menuItems.Add(newOption);
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x00058545 File Offset: 0x00056745
		public bool GetMenuOptionConditionsHold(Game game, MenuContext menuContext, int menuItemNumber)
		{
			if (this.IsWaitMenu)
			{
				return this._menuItems[menuItemNumber].GetConditionsHold(game, menuContext) && this.RunWaitMenuCondition(menuContext);
			}
			return this._menuItems[menuItemNumber].GetConditionsHold(game, menuContext);
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x00058581 File Offset: 0x00056781
		public TextObject GetMenuOptionText(int menuItemNumber)
		{
			return this._menuItems[menuItemNumber].Text;
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x00058594 File Offset: 0x00056794
		public GameMenuOption GetGameMenuOption(int menuItemNumber)
		{
			return this._menuItems[menuItemNumber];
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x000585A2 File Offset: 0x000567A2
		public TextObject GetMenuOptionText2(int menuItemNumber)
		{
			return this._menuItems[menuItemNumber].Text2;
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x000585B5 File Offset: 0x000567B5
		public string GetMenuOptionIdString(int menuItemNumber)
		{
			return this._menuItems[menuItemNumber].IdString;
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x000585C8 File Offset: 0x000567C8
		public TextObject GetMenuOptionTooltip(int menuItemNumber)
		{
			return this._menuItems[menuItemNumber].Tooltip;
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x000585DB File Offset: 0x000567DB
		public bool GetMenuOptionIsLeave(int menuItemNumber)
		{
			return this._menuItems[menuItemNumber].IsLeave;
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x000585EE File Offset: 0x000567EE
		public void SetProgressOfWaitingInMenu(float progress)
		{
			this.Progress = progress;
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x000585F7 File Offset: 0x000567F7
		public void SetTargetedWaitingTimeAndInitialProgress(float targetedWaitingTime, float initialProgress)
		{
			this.TargetWaitHours = targetedWaitingTime;
			this.SetProgressOfWaitingInMenu(initialProgress);
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x00058608 File Offset: 0x00056808
		public GameMenuOption GetLeaveMenuOption(Game game, MenuContext menuContext)
		{
			for (int i = 0; i < this._menuItems.Count; i++)
			{
				if (this._menuItems[i].IsLeave && this._menuItems[i].IsEnabled && this._menuItems[i].GetConditionsHold(game, menuContext))
				{
					return this._menuItems[i];
				}
			}
			return null;
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x00058674 File Offset: 0x00056874
		public void RunOnTick(MenuContext menuContext, float dt)
		{
			if (this.IsWaitMenu && this.IsWaitActive)
			{
				if (this.OnTick != null)
				{
					MenuCallbackArgs args = new MenuCallbackArgs(menuContext, this.MenuTitle);
					this.OnTick(args, CampaignTime.Now - this._previousTickTime);
					this._previousTickTime = CampaignTime.Now;
				}
				if (this.Progress >= 1f)
				{
					this.EndWait();
					this.RunWaitMenuConsequence(menuContext);
				}
			}
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x000586E8 File Offset: 0x000568E8
		public bool RunWaitMenuCondition(MenuContext menuContext)
		{
			if (this.OnCondition != null)
			{
				MenuCallbackArgs args = new MenuCallbackArgs(menuContext, this.MenuTitle);
				bool flag = this.OnCondition(args);
				if (flag && !this.IsWaitActive)
				{
					menuContext.GameMenu.StartWait();
				}
				return flag;
			}
			return true;
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x00058730 File Offset: 0x00056930
		public void RunWaitMenuConsequence(MenuContext menuContext)
		{
			if (this.OnConsequence != null)
			{
				MenuCallbackArgs args = new MenuCallbackArgs(menuContext, this.MenuTitle);
				this.OnConsequence(args);
			}
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x00058760 File Offset: 0x00056960
		public void RunMenuOptionConsequence(MenuContext menuContext, int menuItemNumber)
		{
			if (menuItemNumber >= this._menuItems.Count)
			{
				menuItemNumber = this._menuItems.Count - 1;
			}
			if (this._menuItems[menuItemNumber].IsLeave && this.IsWaitMenu)
			{
				this.EndWait();
			}
			this._menuItems[menuItemNumber].RunConsequence(menuContext);
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x000587BD File Offset: 0x000569BD
		public void StartWait()
		{
			this._previousTickTime = CampaignTime.Now;
			this.IsWaitActive = true;
			Campaign.Current.TimeControlMode = CampaignTimeControlMode.UnstoppableFastForward;
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x000587DC File Offset: 0x000569DC
		public void EndWait()
		{
			this.IsWaitActive = false;
			Campaign.Current.TimeControlMode = CampaignTimeControlMode.Stop;
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x000587F0 File Offset: 0x000569F0
		public void RunOnInit(Game game, MenuContext menuContext)
		{
			MenuCallbackArgs menuCallbackArgs = new MenuCallbackArgs(menuContext, this.MenuTitle);
			if (this.OnInit != null)
			{
				Debug.Print("[GAME MENU] " + menuContext.GameMenu.StringId, 0, Debug.DebugColor.White, 17592186044416UL);
				this.OnInit(menuCallbackArgs);
				this.MenuTitle = menuCallbackArgs.MenuTitle;
			}
			CampaignEventDispatcher.Instance.OnGameMenuOpened(menuCallbackArgs);
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x0005885C File Offset: 0x00056A5C
		public void PreInit(MenuContext menuContext)
		{
			MenuCallbackArgs args = new MenuCallbackArgs(menuContext, this.MenuTitle);
			CampaignEventDispatcher.Instance.BeforeGameMenuOpened(args);
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x00058884 File Offset: 0x00056A84
		public void AfterInit(MenuContext menuContext)
		{
			MenuCallbackArgs args = new MenuCallbackArgs(menuContext, this.MenuTitle);
			CampaignEventDispatcher.Instance.AfterGameMenuOpened(args);
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x000588A9 File Offset: 0x00056AA9
		public TextObject GetText()
		{
			return this._defaultText;
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060013C3 RID: 5059 RVA: 0x000588B1 File Offset: 0x00056AB1
		// (set) Token: 0x060013C4 RID: 5060 RVA: 0x000588B9 File Offset: 0x00056AB9
		public bool AutoSelectFirst { get; private set; }

		// Token: 0x060013C5 RID: 5061 RVA: 0x000588C4 File Offset: 0x00056AC4
		public static void ActivateGameMenu(string menuId)
		{
			Campaign.Current.TimeControlMode = CampaignTimeControlMode.Stop;
			if (Campaign.Current.CurrentMenuContext == null)
			{
				Campaign.Current.GameMenuManager.SetNextMenu(menuId);
				MapState mapState = Game.Current.GameStateManager.LastOrDefault<MapState>();
				if (mapState != null)
				{
					mapState.EnterMenuMode();
				}
				if (mapState != null)
				{
					MenuContext menuContext = mapState.MenuContext;
					bool? flag;
					if (menuContext == null)
					{
						flag = null;
					}
					else
					{
						GameMenu gameMenu = menuContext.GameMenu;
						flag = ((gameMenu != null) ? new bool?(gameMenu.IsWaitMenu) : null);
					}
					bool? flag2 = flag;
					bool flag3 = true;
					if (flag2.GetValueOrDefault() == flag3 & flag2 != null)
					{
						mapState.MenuContext.GameMenu.StartWait();
						return;
					}
				}
			}
			else
			{
				GameMenu.SwitchToMenu(menuId);
			}
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x00058978 File Offset: 0x00056B78
		public static void SwitchToMenu(string menuId)
		{
			Campaign.Current.TimeControlMode = CampaignTimeControlMode.Stop;
			MenuContext currentMenuContext = Campaign.Current.CurrentMenuContext;
			if (currentMenuContext != null)
			{
				currentMenuContext.SwitchToMenu(menuId);
				if (currentMenuContext.GameMenu.IsWaitMenu && Campaign.Current.TimeControlMode == CampaignTimeControlMode.Stop)
				{
					currentMenuContext.GameMenu.StartWait();
				}
				MenuContext currentMenuContext2 = Campaign.Current.CurrentMenuContext;
				if (currentMenuContext2 != null && currentMenuContext2.GameMenu.StringId == menuId)
				{
					currentMenuContext2.GameMenu.AfterInit(currentMenuContext2);
					return;
				}
			}
			else
			{
				Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\GameMenus\\GameMenu.cs", "SwitchToMenu", 363);
			}
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x00058A10 File Offset: 0x00056C10
		public static void ExitToLast()
		{
			Campaign.Current.TimeControlMode = CampaignTimeControlMode.Stop;
			Campaign.Current.GameMenuManager.ExitToLast();
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x00058A2C File Offset: 0x00056C2C
		internal void AddOption(string optionId, TextObject optionText, GameMenuOption.OnConditionDelegate condition, GameMenuOption.OnConsequenceDelegate consequence, int index = -1, bool isLeave = false, bool isRepeatable = false, object relatedObject = null)
		{
			this.AddOption(new GameMenuOption(GameMenu.MenuAndOptionType.RegularMenuOption, optionId, optionText, optionText, condition, consequence, isLeave, isRepeatable, relatedObject), index);
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x00058A53 File Offset: 0x00056C53
		internal void RemoveMenuOption(GameMenuOption option)
		{
			this._menuItems.Remove(option);
		}

		// Token: 0x040006C1 RID: 1729
		private TextObject _defaultText;

		// Token: 0x040006C7 RID: 1735
		public OnInitDelegate OnInit;

		// Token: 0x040006C9 RID: 1737
		public List<object> MenuRepeatObjects = new List<object>();

		// Token: 0x040006CA RID: 1738
		public object LastSelectedMenuObject;

		// Token: 0x040006D2 RID: 1746
		private CampaignTime _previousTickTime;

		// Token: 0x040006D3 RID: 1747
		private readonly List<GameMenuOption> _menuItems;

		// Token: 0x020004EC RID: 1260
		public enum MenuFlags
		{
			// Token: 0x0400151E RID: 5406
			None,
			// Token: 0x0400151F RID: 5407
			AutoSelectFirst
		}

		// Token: 0x020004ED RID: 1261
		public enum MenuAndOptionType
		{
			// Token: 0x04001521 RID: 5409
			RegularMenuOption,
			// Token: 0x04001522 RID: 5410
			WaitMenuShowProgressAndHoursOption,
			// Token: 0x04001523 RID: 5411
			WaitMenuShowOnlyProgressOption,
			// Token: 0x04001524 RID: 5412
			WaitMenuHideProgressAndHoursOption
		}
	}
}
