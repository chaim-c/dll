using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.CampaignSystem.Encyclopedia.Pages;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.ScreenSystem;

namespace SandBox.GauntletUI.Encyclopedia
{
	// Token: 0x02000037 RID: 55
	public class EncyclopediaData
	{
		// Token: 0x060001F8 RID: 504 RVA: 0x0000DFC0 File Offset: 0x0000C1C0
		public EncyclopediaData(GauntletMapEncyclopediaView manager, ScreenBase screen, EncyclopediaHomeVM homeDatasource, EncyclopediaNavigatorVM navigatorDatasource)
		{
			this._manager = manager;
			this._screen = screen;
			this._pages = new Dictionary<string, EncyclopediaPage>();
			foreach (EncyclopediaPage encyclopediaPage in Campaign.Current.EncyclopediaManager.GetEncyclopediaPages())
			{
				foreach (string key in encyclopediaPage.GetIdentifierNames())
				{
					if (!this._pages.ContainsKey(key))
					{
						this._pages.Add(key, encyclopediaPage);
					}
				}
			}
			this._homeDatasource = homeDatasource;
			this._lists = new Dictionary<EncyclopediaPage, EncyclopediaListVM>();
			foreach (EncyclopediaPage encyclopediaPage2 in Campaign.Current.EncyclopediaManager.GetEncyclopediaPages())
			{
				if (!this._lists.ContainsKey(encyclopediaPage2))
				{
					EncyclopediaListVM encyclopediaListVM = new EncyclopediaListVM(new EncyclopediaPageArgs(encyclopediaPage2));
					this._manager.ListViewDataController.LoadListData(encyclopediaListVM);
					this._lists.Add(encyclopediaPage2, encyclopediaListVM);
				}
			}
			this._navigatorDatasource = navigatorDatasource;
			this._navigatorDatasource.SetCancelInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit"));
			this._navigatorDatasource.SetPreviousPageInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("SwitchToPreviousTab"));
			this._navigatorDatasource.SetNextPageInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("SwitchToNextTab"));
			Game.Current.EventManager.RegisterEvent<TutorialContextChangedEvent>(new Action<TutorialContextChangedEvent>(this.OnTutorialContextChanged));
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000E174 File Offset: 0x0000C374
		private void OnTutorialContextChanged(TutorialContextChangedEvent obj)
		{
			if (obj.NewContext != TutorialContexts.EncyclopediaWindow)
			{
				this._prevContext = obj.NewContext;
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000E18C File Offset: 0x0000C38C
		internal void OnTick()
		{
			this._navigatorDatasource.CanSwitchTabs = (!Input.IsGamepadActive || !InformationManager.GetIsAnyTooltipActiveAndExtended());
			if (this._activeGauntletLayer.Input.IsHotKeyDownAndReleased("Exit") || (this._activeGauntletLayer.Input.IsGameKeyDownAndReleased(39) && !this._activeGauntletLayer.IsFocusedOnInput()))
			{
				if (this._navigatorDatasource.IsSearchResultsShown)
				{
					this._navigatorDatasource.SearchText = string.Empty;
				}
				else
				{
					this._manager.CloseEncyclopedia();
					UISoundsHelper.PlayUISound("event:/ui/default");
				}
			}
			else if (!this._activeGauntletLayer.IsFocusedOnInput() && this._navigatorDatasource.CanSwitchTabs)
			{
				if ((Input.IsKeyPressed(InputKey.BackSpace) && this._navigatorDatasource.IsBackEnabled) || this._activeGauntletLayer.Input.IsHotKeyReleased("SwitchToPreviousTab"))
				{
					this._navigatorDatasource.ExecuteBack();
				}
				else if (this._activeGauntletLayer.Input.IsHotKeyReleased("SwitchToNextTab"))
				{
					this._navigatorDatasource.ExecuteForward();
				}
			}
			if (this._activeGauntletLayer != null)
			{
				object initialState = this._initialState;
				Game game = Game.Current;
				object obj;
				if (game == null)
				{
					obj = null;
				}
				else
				{
					GameStateManager gameStateManager = game.GameStateManager;
					obj = ((gameStateManager != null) ? gameStateManager.ActiveState : null);
				}
				if (initialState != obj)
				{
					this._manager.CloseEncyclopedia();
				}
			}
			EncyclopediaPageVM activeDatasource = this._activeDatasource;
			if (activeDatasource == null)
			{
				return;
			}
			activeDatasource.OnTick();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000E2E8 File Offset: 0x0000C4E8
		private void SetEncyclopediaPage(string pageId, object obj)
		{
			GauntletLayer activeGauntletLayer = this._activeGauntletLayer;
			if (this._activeGauntletLayer != null && this._activeGauntletMovie != null)
			{
				this._activeGauntletLayer.ReleaseMovie(this._activeGauntletMovie);
			}
			EncyclopediaListVM encyclopediaListVM;
			if ((encyclopediaListVM = (this._activeDatasource as EncyclopediaListVM)) != null)
			{
				EncyclopediaListItemVM encyclopediaListItemVM = encyclopediaListVM.Items.FirstOrDefault((EncyclopediaListItemVM x) => x.Object == obj);
				this._manager.ListViewDataController.SaveListData(encyclopediaListVM, (encyclopediaListItemVM != null) ? encyclopediaListItemVM.Id : encyclopediaListVM.LastSelectedItemId);
			}
			if (this._activeGauntletLayer == null)
			{
				this._activeGauntletLayer = new GauntletLayer(310, "GauntletLayer", false);
				this._navigatorActiveGauntletMovie = this._activeGauntletLayer.LoadMovie("EncyclopediaBar", this._navigatorDatasource);
				this._navigatorDatasource.PageName = this._homeDatasource.GetName();
				this._activeGauntletLayer.IsFocusLayer = true;
				ScreenManager.TrySetFocus(this._activeGauntletLayer);
				this._activeGauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
				this._activeGauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericCampaignPanelsGameKeyCategory"));
				Game.Current.GameStateManager.RegisterActiveStateDisableRequest(this);
				Game.Current.EventManager.TriggerEvent<TutorialContextChangedEvent>(new TutorialContextChangedEvent(TutorialContexts.EncyclopediaWindow));
				this._initialState = Game.Current.GameStateManager.ActiveState;
			}
			if (pageId == "Home")
			{
				this._activeGauntletMovie = this._activeGauntletLayer.LoadMovie("EncyclopediaHome", this._homeDatasource);
				this._homeGauntletMovie = this._activeGauntletMovie;
				this._activeDatasource = this._homeDatasource;
				this._activeDatasource.Refresh();
				Game.Current.EventManager.TriggerEvent<EncyclopediaPageChangedEvent>(new EncyclopediaPageChangedEvent(EncyclopediaPages.Home, false));
			}
			else if (pageId == "ListPage")
			{
				EncyclopediaPage encyclopediaPage = obj as EncyclopediaPage;
				this._activeDatasource = this._lists[encyclopediaPage];
				this._activeGauntletMovie = this._activeGauntletLayer.LoadMovie("EncyclopediaItemList", this._activeDatasource);
				this._activeDatasource.Refresh();
				this._manager.ListViewDataController.LoadListData(this._activeDatasource as EncyclopediaListVM);
				this.SetTutorialListPageContext(encyclopediaPage);
			}
			else
			{
				EncyclopediaPage encyclopediaPage2 = this._pages[pageId];
				this._activeDatasource = this.GetEncyclopediaPageInstance(encyclopediaPage2, obj);
				EncyclopediaContentPageVM encyclopediaContentPageVM = this._activeDatasource as EncyclopediaContentPageVM;
				if (encyclopediaContentPageVM != null)
				{
					encyclopediaContentPageVM.InitializeQuickNavigation(this._lists[encyclopediaPage2]);
				}
				this._activeGauntletMovie = this._activeGauntletLayer.LoadMovie(this._pages[pageId].GetViewFullyQualifiedName(), this._activeDatasource);
				this.SetTutorialPageContext(this._activeDatasource);
			}
			this._navigatorDatasource.NavBarString = this._activeDatasource.GetNavigationBarURL();
			if (activeGauntletLayer != null && activeGauntletLayer != this._activeGauntletLayer)
			{
				this._screen.RemoveLayer(activeGauntletLayer);
				this._screen.AddLayer(this._activeGauntletLayer);
			}
			else if (activeGauntletLayer == null && this._activeGauntletLayer != null)
			{
				this._screen.AddLayer(this._activeGauntletLayer);
			}
			this._activeGauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			this._previousPageID = pageId;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000E61B File Offset: 0x0000C81B
		internal EncyclopediaPageVM ExecuteLink(string pageId, object obj, bool needsRefresh)
		{
			this.SetEncyclopediaPage(pageId, obj);
			return this._activeDatasource;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000E62C File Offset: 0x0000C82C
		private EncyclopediaPageVM GetEncyclopediaPageInstance(EncyclopediaPage page, object o)
		{
			EncyclopediaPageArgs encyclopediaPageArgs = new EncyclopediaPageArgs(o);
			foreach (Type type in typeof(EncyclopediaHomeVM).Assembly.GetTypesSafe(null))
			{
				if (typeof(EncyclopediaPageVM).IsAssignableFrom(type))
				{
					object[] customAttributesSafe = type.GetCustomAttributesSafe(typeof(EncyclopediaViewModel), false);
					for (int i = 0; i < customAttributesSafe.Length; i++)
					{
						EncyclopediaViewModel encyclopediaViewModel;
						if ((encyclopediaViewModel = (customAttributesSafe[i] as EncyclopediaViewModel)) != null && page.HasIdentifierType(encyclopediaViewModel.PageTargetType))
						{
							return Activator.CreateInstance(type, new object[]
							{
								encyclopediaPageArgs
							}) as EncyclopediaPageVM;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000E708 File Offset: 0x0000C908
		public void OnFinalize()
		{
			Game.Current.GameStateManager.UnregisterActiveStateDisableRequest(this);
			this._pages = null;
			this._homeDatasource = null;
			this._lists = null;
			this._activeGauntletMovie = null;
			this._activeDatasource = null;
			this._activeGauntletLayer = null;
			this._navigatorActiveGauntletMovie = null;
			this._navigatorDatasource = null;
			this._initialState = null;
			Game.Current.EventManager.UnregisterEvent<TutorialContextChangedEvent>(new Action<TutorialContextChangedEvent>(this.OnTutorialContextChanged));
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000E780 File Offset: 0x0000C980
		public void CloseEncyclopedia()
		{
			EncyclopediaListVM encyclopediaListVM;
			if ((encyclopediaListVM = (this._activeDatasource as EncyclopediaListVM)) != null)
			{
				this._manager.ListViewDataController.SaveListData(encyclopediaListVM, encyclopediaListVM.LastSelectedItemId);
			}
			this.ResetPageFilters();
			this._activeGauntletLayer.ReleaseMovie(this._activeGauntletMovie);
			this._screen.RemoveLayer(this._activeGauntletLayer);
			this._activeGauntletLayer.InputRestrictions.ResetInputRestrictions();
			this.OnFinalize();
			Game.Current.EventManager.TriggerEvent<EncyclopediaPageChangedEvent>(new EncyclopediaPageChangedEvent(EncyclopediaPages.None, false));
			Game.Current.EventManager.TriggerEvent<TutorialContextChangedEvent>(new TutorialContextChangedEvent(this._prevContext));
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000E824 File Offset: 0x0000CA24
		private void ResetPageFilters()
		{
			foreach (EncyclopediaListVM encyclopediaListVM in this._lists.Values)
			{
				foreach (EncyclopediaFilterGroupVM encyclopediaFilterGroupVM in encyclopediaListVM.FilterGroups)
				{
					foreach (EncyclopediaListFilterVM encyclopediaListFilterVM in encyclopediaFilterGroupVM.Filters)
					{
						encyclopediaListFilterVM.IsSelected = false;
					}
				}
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000E8E0 File Offset: 0x0000CAE0
		private void SetTutorialPageContext(EncyclopediaPageVM _page)
		{
			if (_page is EncyclopediaClanPageVM)
			{
				Game.Current.EventManager.TriggerEvent<EncyclopediaPageChangedEvent>(new EncyclopediaPageChangedEvent(EncyclopediaPages.Clan, false));
				return;
			}
			if (_page is EncyclopediaConceptPageVM)
			{
				Game.Current.EventManager.TriggerEvent<EncyclopediaPageChangedEvent>(new EncyclopediaPageChangedEvent(EncyclopediaPages.Concept, false));
				return;
			}
			if (_page is EncyclopediaFactionPageVM)
			{
				Game.Current.EventManager.TriggerEvent<EncyclopediaPageChangedEvent>(new EncyclopediaPageChangedEvent(EncyclopediaPages.Kingdom, false));
				return;
			}
			if (_page is EncyclopediaUnitPageVM)
			{
				Game.Current.EventManager.TriggerEvent<EncyclopediaPageChangedEvent>(new EncyclopediaPageChangedEvent(EncyclopediaPages.Unit, false));
				return;
			}
			if (_page is EncyclopediaHeroPageVM)
			{
				Game.Current.EventManager.TriggerEvent<EncyclopediaPageChangedEvent>(new EncyclopediaPageChangedEvent(EncyclopediaPages.Hero, false));
				return;
			}
			if (_page is EncyclopediaSettlementPageVM)
			{
				Game.Current.EventManager.TriggerEvent<EncyclopediaPageChangedEvent>(new EncyclopediaPageChangedEvent(EncyclopediaPages.Settlement, false));
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000E9AC File Offset: 0x0000CBAC
		private void SetTutorialListPageContext(EncyclopediaPage _page)
		{
			if (_page is DefaultEncyclopediaClanPage)
			{
				Game.Current.EventManager.TriggerEvent<EncyclopediaPageChangedEvent>(new EncyclopediaPageChangedEvent(EncyclopediaPages.ListClans, false));
				return;
			}
			if (_page is DefaultEncyclopediaConceptPage)
			{
				Game.Current.EventManager.TriggerEvent<EncyclopediaPageChangedEvent>(new EncyclopediaPageChangedEvent(EncyclopediaPages.ListConcepts, false));
				return;
			}
			if (_page is DefaultEncyclopediaFactionPage)
			{
				Game.Current.EventManager.TriggerEvent<EncyclopediaPageChangedEvent>(new EncyclopediaPageChangedEvent(EncyclopediaPages.ListKingdoms, false));
				return;
			}
			if (_page is DefaultEncyclopediaUnitPage)
			{
				Game.Current.EventManager.TriggerEvent<EncyclopediaPageChangedEvent>(new EncyclopediaPageChangedEvent(EncyclopediaPages.ListUnits, false));
				return;
			}
			if (_page is DefaultEncyclopediaHeroPage)
			{
				Game.Current.EventManager.TriggerEvent<EncyclopediaPageChangedEvent>(new EncyclopediaPageChangedEvent(EncyclopediaPages.ListHeroes, false));
				return;
			}
			if (_page is DefaultEncyclopediaSettlementPage)
			{
				Game.Current.EventManager.TriggerEvent<EncyclopediaPageChangedEvent>(new EncyclopediaPageChangedEvent(EncyclopediaPages.ListSettlements, false));
			}
		}

		// Token: 0x040000F6 RID: 246
		private Dictionary<string, EncyclopediaPage> _pages;

		// Token: 0x040000F7 RID: 247
		private string _previousPageID;

		// Token: 0x040000F8 RID: 248
		private EncyclopediaHomeVM _homeDatasource;

		// Token: 0x040000F9 RID: 249
		private IGauntletMovie _homeGauntletMovie;

		// Token: 0x040000FA RID: 250
		private Dictionary<EncyclopediaPage, EncyclopediaListVM> _lists;

		// Token: 0x040000FB RID: 251
		private EncyclopediaPageVM _activeDatasource;

		// Token: 0x040000FC RID: 252
		private GauntletLayer _activeGauntletLayer;

		// Token: 0x040000FD RID: 253
		private IGauntletMovie _activeGauntletMovie;

		// Token: 0x040000FE RID: 254
		private EncyclopediaNavigatorVM _navigatorDatasource;

		// Token: 0x040000FF RID: 255
		private IGauntletMovie _navigatorActiveGauntletMovie;

		// Token: 0x04000100 RID: 256
		private readonly ScreenBase _screen;

		// Token: 0x04000101 RID: 257
		private TutorialContexts _prevContext;

		// Token: 0x04000102 RID: 258
		private readonly GauntletMapEncyclopediaView _manager;

		// Token: 0x04000103 RID: 259
		private object _initialState;
	}
}
