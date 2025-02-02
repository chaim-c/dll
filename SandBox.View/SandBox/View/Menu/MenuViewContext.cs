using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Overlay;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.ScreenSystem;

namespace SandBox.View.Menu
{
	// Token: 0x02000031 RID: 49
	public class MenuViewContext : IMenuContextHandler
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00011927 File Offset: 0x0000FB27
		internal GameMenu CurGameMenu
		{
			get
			{
				return this._menuContext.GameMenu;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00011934 File Offset: 0x0000FB34
		public MenuContext MenuContext
		{
			get
			{
				return this._menuContext;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0001193C File Offset: 0x0000FB3C
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00011944 File Offset: 0x0000FB44
		public List<MenuView> MenuViews { get; private set; }

		// Token: 0x06000187 RID: 391 RVA: 0x00011950 File Offset: 0x0000FB50
		public MenuViewContext(ScreenBase screen, MenuContext menuContext)
		{
			this._screen = screen;
			this._menuContext = menuContext;
			this.MenuViews = new List<MenuView>();
			this._menuContext.Handler = this;
			if (Campaign.Current.GameMode != CampaignGameMode.Tutorial && this.CurGameMenu.StringId != "siege_test_menu")
			{
				((IMenuContextHandler)this).OnMenuCreate();
				((IMenuContextHandler)this).OnMenuActivate();
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000119B8 File Offset: 0x0000FBB8
		public void UpdateMenuContext(MenuContext menuContext)
		{
			this._menuContext = menuContext;
			this._menuContext.Handler = this;
			this.MenuViews.ForEach(delegate(MenuView m)
			{
				m.MenuContext = menuContext;
			});
			this.MenuViews.ForEach(delegate(MenuView m)
			{
				m.OnMenuContextUpdated(menuContext);
			});
			this.CheckAndInitializeOverlay();
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00011A1E File Offset: 0x0000FC1E
		public void AddLayer(ScreenLayer layer)
		{
			this._screen.AddLayer(layer);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00011A2C File Offset: 0x0000FC2C
		public void RemoveLayer(ScreenLayer layer)
		{
			this._screen.RemoveLayer(layer);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00011A3A File Offset: 0x0000FC3A
		public T FindLayer<T>() where T : ScreenLayer
		{
			return this._screen.FindLayer<T>();
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00011A47 File Offset: 0x0000FC47
		public T FindLayer<T>(string name) where T : ScreenLayer
		{
			return this._screen.FindLayer<T>(name);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00011A58 File Offset: 0x0000FC58
		public void OnFrameTick(float dt)
		{
			for (int i = 0; i < this.MenuViews.Count; i++)
			{
				MenuView menuView = this.MenuViews[i];
				menuView.OnFrameTick(dt);
				if (menuView.Removed)
				{
					i--;
				}
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00011A9C File Offset: 0x0000FC9C
		public void OnResume()
		{
			for (int i = 0; i < this.MenuViews.Count; i++)
			{
				this.MenuViews[i].OnResume();
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00011AD0 File Offset: 0x0000FCD0
		public void OnHourlyTick()
		{
			for (int i = 0; i < this.MenuViews.Count; i++)
			{
				this.MenuViews[i].OnHourlyTick();
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00011B04 File Offset: 0x0000FD04
		public void OnActivate()
		{
			MenuContext menuContext = this.MenuContext;
			if (!string.IsNullOrEmpty((menuContext != null) ? menuContext.CurrentAmbientSoundID : null))
			{
				this.PlayAmbientSound(this.MenuContext.CurrentAmbientSoundID);
			}
			MenuContext menuContext2 = this.MenuContext;
			if (!string.IsNullOrEmpty((menuContext2 != null) ? menuContext2.CurrentPanelSoundID : null))
			{
				this.PlayPanelSound(this.MenuContext.CurrentPanelSoundID);
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00011B65 File Offset: 0x0000FD65
		public void OnDeactivate()
		{
			this.StopAllSounds();
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00011B6D File Offset: 0x0000FD6D
		public void OnInitialize()
		{
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00011B6F File Offset: 0x0000FD6F
		public void OnFinalize()
		{
			this.ClearMenuViews();
			MBInformationManager.HideInformations();
			this._menuContext = null;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00011B84 File Offset: 0x0000FD84
		private void ClearMenuViews()
		{
			foreach (MenuView menuView in this.MenuViews.ToArray())
			{
				this.RemoveMenuView(menuView);
			}
			this._menuCharacterDeveloper = null;
			this._menuOverlayBase = null;
			this._menuRecruitVolunteers = null;
			this._menuTownManagement = null;
			this._menuTroopSelection = null;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00011BD9 File Offset: 0x0000FDD9
		public void StopAllSounds()
		{
			SoundEvent ambientSound = this._ambientSound;
			if (ambientSound != null)
			{
				ambientSound.Release();
			}
			SoundEvent panelSound = this._panelSound;
			if (panelSound == null)
			{
				return;
			}
			panelSound.Release();
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00011BFC File Offset: 0x0000FDFC
		private void PlayAmbientSound(string ambientSoundID)
		{
			SoundEvent ambientSound = this._ambientSound;
			if (ambientSound != null)
			{
				ambientSound.Release();
			}
			this._ambientSound = SoundEvent.CreateEventFromString(ambientSoundID, null);
			this._ambientSound.Play();
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00011C28 File Offset: 0x0000FE28
		private void PlayPanelSound(string panelSoundID)
		{
			SoundEvent panelSound = this._panelSound;
			if (panelSound != null)
			{
				panelSound.Release();
			}
			this._panelSound = SoundEvent.CreateEventFromString(panelSoundID, null);
			this._panelSound.Play();
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00011C54 File Offset: 0x0000FE54
		void IMenuContextHandler.OnAmbientSoundIDSet(string ambientSoundID)
		{
			this.PlayAmbientSound(ambientSoundID);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00011C5D File Offset: 0x0000FE5D
		void IMenuContextHandler.OnPanelSoundIDSet(string panelSoundID)
		{
			this.PlayPanelSound(panelSoundID);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00011C68 File Offset: 0x0000FE68
		void IMenuContextHandler.OnMenuCreate()
		{
			bool flag = Campaign.Current.GameMode == CampaignGameMode.Tutorial || this.CurGameMenu.StringId == "siege_test_menu";
			if (flag && this._currentMenuBackground == null)
			{
				this._currentMenuBackground = this.AddMenuView<MenuBackgroundView>(Array.Empty<object>());
			}
			if (this._currentMenuBase == null)
			{
				this._currentMenuBase = this.AddMenuView<MenuBaseView>(Array.Empty<object>());
			}
			if (!flag)
			{
				this.CheckAndInitializeOverlay();
			}
			this.StopAllSounds();
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00011CE0 File Offset: 0x0000FEE0
		void IMenuContextHandler.OnMenuActivate()
		{
			foreach (MenuView menuView in this.MenuViews)
			{
				menuView.OnActivate();
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00011D30 File Offset: 0x0000FF30
		public void OnMapConversationActivated()
		{
			for (int i = 0; i < this.MenuViews.Count; i++)
			{
				MenuView menuView = this.MenuViews[i];
				menuView.OnMapConversationActivated();
				if (menuView.Removed)
				{
					i--;
				}
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00011D70 File Offset: 0x0000FF70
		public void OnMapConversationDeactivated()
		{
			for (int i = 0; i < this.MenuViews.Count; i++)
			{
				MenuView menuView = this.MenuViews[i];
				menuView.OnMapConversationDeactivated();
				if (menuView.Removed)
				{
					i--;
				}
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00011DB0 File Offset: 0x0000FFB0
		public void OnGameStateDeactivate()
		{
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00011DB2 File Offset: 0x0000FFB2
		public void OnGameStateInitialize()
		{
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00011DB4 File Offset: 0x0000FFB4
		public void OnGameStateFinalize()
		{
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00011DB8 File Offset: 0x0000FFB8
		private void CheckAndInitializeOverlay()
		{
			GameOverlays.MenuOverlayType menuOverlayType = Campaign.Current.GameMenuManager.GetMenuOverlayType(this._menuContext);
			if (menuOverlayType != GameOverlays.MenuOverlayType.None)
			{
				if (menuOverlayType != this._currentOverlayType)
				{
					if (this._menuOverlayBase != null && ((this._currentOverlayType != GameOverlays.MenuOverlayType.Encounter && menuOverlayType == GameOverlays.MenuOverlayType.Encounter) || (this._currentOverlayType == GameOverlays.MenuOverlayType.Encounter && (menuOverlayType == GameOverlays.MenuOverlayType.SettlementWithBoth || menuOverlayType == GameOverlays.MenuOverlayType.SettlementWithCharacters || menuOverlayType == GameOverlays.MenuOverlayType.SettlementWithParties))))
					{
						this.RemoveMenuView(this._menuOverlayBase);
						this._menuOverlayBase = null;
					}
					if (this._menuOverlayBase == null)
					{
						this._menuOverlayBase = this.AddMenuView<MenuOverlayBaseView>(Array.Empty<object>());
					}
					else
					{
						this._menuOverlayBase.OnOverlayTypeChange(menuOverlayType);
					}
				}
				else
				{
					MenuView menuOverlayBase = this._menuOverlayBase;
					if (menuOverlayBase != null)
					{
						menuOverlayBase.OnOverlayTypeChange(menuOverlayType);
					}
				}
			}
			else
			{
				if (this._menuOverlayBase != null)
				{
					this.RemoveMenuView(this._menuOverlayBase);
					this._menuOverlayBase = null;
				}
				if (this._currentMenuBackground != null)
				{
					this.RemoveMenuView(this._currentMenuBackground);
					this._currentMenuBackground = null;
				}
			}
			this._currentOverlayType = menuOverlayType;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00011EA4 File Offset: 0x000100A4
		public void CloseCharacterDeveloper()
		{
			this.RemoveMenuView(this._menuCharacterDeveloper);
			this._menuCharacterDeveloper = null;
			foreach (MenuView menuView in this.MenuViews)
			{
				menuView.OnCharacterDeveloperClosed();
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00011F08 File Offset: 0x00010108
		public MenuView AddMenuView<T>(params object[] parameters) where T : MenuView, new()
		{
			MenuView menuView = SandBoxViewCreator.CreateMenuView<T>(parameters);
			menuView.MenuViewContext = this;
			menuView.MenuContext = this._menuContext;
			this.MenuViews.Add(menuView);
			menuView.OnInitialize();
			return menuView;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00011F44 File Offset: 0x00010144
		public T GetMenuView<T>() where T : MenuView
		{
			foreach (MenuView menuView in this.MenuViews)
			{
				T t = menuView as T;
				if (t != null)
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00011FB4 File Offset: 0x000101B4
		public void RemoveMenuView(MenuView menuView)
		{
			menuView.OnFinalize();
			menuView.Removed = true;
			this.MenuViews.Remove(menuView);
			if (menuView.ShouldUpdateMenuAfterRemoved)
			{
				this.MenuViews.ForEach(delegate(MenuView m)
				{
					m.OnMenuContextUpdated(this._menuContext);
				});
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00011FF0 File Offset: 0x000101F0
		void IMenuContextHandler.OnBackgroundMeshNameSet(string name)
		{
			foreach (MenuView menuView in this.MenuViews)
			{
				menuView.OnBackgroundMeshNameSet(name);
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00012044 File Offset: 0x00010244
		void IMenuContextHandler.OnOpenTownManagement()
		{
			if (this._menuTownManagement == null)
			{
				this._menuTownManagement = this.AddMenuView<MenuTownManagementView>(Array.Empty<object>());
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0001205F File Offset: 0x0001025F
		public void CloseTownManagement()
		{
			this.RemoveMenuView(this._menuTownManagement);
			this._menuTownManagement = null;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00012074 File Offset: 0x00010274
		void IMenuContextHandler.OnOpenRecruitVolunteers()
		{
			if (this._menuRecruitVolunteers == null)
			{
				this._menuRecruitVolunteers = this.AddMenuView<MenuRecruitVolunteersView>(Array.Empty<object>());
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0001208F File Offset: 0x0001028F
		public void CloseRecruitVolunteers()
		{
			this.RemoveMenuView(this._menuRecruitVolunteers);
			this._menuRecruitVolunteers = null;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000120A4 File Offset: 0x000102A4
		void IMenuContextHandler.OnOpenTournamentLeaderboard()
		{
			if (this._menuTournamentLeaderboard == null)
			{
				this._menuTournamentLeaderboard = this.AddMenuView<MenuTournamentLeaderboardView>(Array.Empty<object>());
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000120BF File Offset: 0x000102BF
		public void CloseTournamentLeaderboard()
		{
			this.RemoveMenuView(this._menuTournamentLeaderboard);
			this._menuTournamentLeaderboard = null;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000120D4 File Offset: 0x000102D4
		void IMenuContextHandler.OnOpenTroopSelection(TroopRoster fullRoster, TroopRoster initialSelections, Func<CharacterObject, bool> canChangeStatusOfTroop, Action<TroopRoster> onDone, int maxSelectableTroopCount, int minSelectableTroopCount)
		{
			if (this._menuTroopSelection == null)
			{
				this._menuTroopSelection = this.AddMenuView<MenuTroopSelectionView>(new object[]
				{
					fullRoster,
					initialSelections,
					canChangeStatusOfTroop,
					onDone,
					maxSelectableTroopCount,
					minSelectableTroopCount
				});
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00012120 File Offset: 0x00010320
		public void CloseTroopSelection()
		{
			this.RemoveMenuView(this._menuTroopSelection);
			this._menuTroopSelection = null;
		}

		// Token: 0x040000E7 RID: 231
		private MenuContext _menuContext;

		// Token: 0x040000E8 RID: 232
		private MenuView _currentMenuBase;

		// Token: 0x040000E9 RID: 233
		private MenuView _currentMenuBackground;

		// Token: 0x040000EA RID: 234
		private MenuView _menuCharacterDeveloper;

		// Token: 0x040000EB RID: 235
		private MenuView _menuOverlayBase;

		// Token: 0x040000EC RID: 236
		private MenuView _menuRecruitVolunteers;

		// Token: 0x040000ED RID: 237
		private MenuView _menuTournamentLeaderboard;

		// Token: 0x040000EE RID: 238
		private MenuView _menuTroopSelection;

		// Token: 0x040000EF RID: 239
		private MenuView _menuTownManagement;

		// Token: 0x040000F0 RID: 240
		private SoundEvent _panelSound;

		// Token: 0x040000F1 RID: 241
		private SoundEvent _ambientSound;

		// Token: 0x040000F2 RID: 242
		private GameOverlays.MenuOverlayType _currentOverlayType;

		// Token: 0x040000F4 RID: 244
		private ScreenBase _screen;
	}
}
