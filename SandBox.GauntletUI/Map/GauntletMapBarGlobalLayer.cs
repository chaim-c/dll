using System;
using SandBox.View.Map;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.ViewModelCollection.ArmyManagement;
using TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapBar;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.ScreenSystem;
using TaleWorlds.TwoDimension;

namespace SandBox.GauntletUI.Map
{
	// Token: 0x02000022 RID: 34
	public class GauntletMapBarGlobalLayer : GlobalLayer
	{
		// Token: 0x06000145 RID: 325 RVA: 0x0000A1C8 File Offset: 0x000083C8
		public void Initialize(MapScreen mapScreen, float contextAlphaModifider)
		{
			this._mapScreen = mapScreen;
			this._contextAlphaModifider = contextAlphaModifider;
			this._mapNavigationHandler = new MapNavigationHandler();
			this._mapNavigationHandlerAsInterface = this._mapNavigationHandler;
			this._mapDataSource = new MapBarVM(this._mapNavigationHandler, this._mapScreen, new Func<MapBarShortcuts>(this.GetMapBarShortcuts), new Action(this.OpenArmyManagement));
			this._gauntletLayer = new GauntletLayer(202, "GauntletLayer", false);
			base.Layer = this._gauntletLayer;
			SpriteData spriteData = UIResourceManager.SpriteData;
			this._mapBarCategory = spriteData.SpriteCategories["ui_mapbar"];
			this._mapBarCategory.Load(UIResourceManager.ResourceContext, UIResourceManager.UIResourceDepot);
			this._movie = this._gauntletLayer.LoadMovie("MapBar", this._mapDataSource);
			this._encyclopediaManager = mapScreen.EncyclopediaScreenManager;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000A2A4 File Offset: 0x000084A4
		public void OnFinalize()
		{
			ArmyManagementVM armyManagementVM = this._armyManagementVM;
			if (armyManagementVM != null)
			{
				armyManagementVM.OnFinalize();
			}
			this._mapDataSource.OnFinalize();
			IGauntletMovie gauntletArmyManagementMovie = this._gauntletArmyManagementMovie;
			if (gauntletArmyManagementMovie != null)
			{
				gauntletArmyManagementMovie.Release();
			}
			this._movie.Release();
			this._mapBarCategory.Unload();
			this._armyManagementVM = null;
			this._gauntletLayer = null;
			this._mapDataSource = null;
			this._encyclopediaManager = null;
			this._mapScreen = null;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000A317 File Offset: 0x00008517
		public void Refresh()
		{
			MapBarVM mapDataSource = this._mapDataSource;
			if (mapDataSource == null)
			{
				return;
			}
			mapDataSource.OnRefresh();
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000A32C File Offset: 0x0000852C
		private MapBarShortcuts GetMapBarShortcuts()
		{
			return new MapBarShortcuts
			{
				EscapeMenuHotkey = Game.Current.GameTextManager.GetHotKeyGameText("GenericPanelGameKeyCategory", "ToggleEscapeMenu").ToString(),
				CharacterHotkey = Game.Current.GameTextManager.GetHotKeyGameText("GenericCampaignPanelsGameKeyCategory", 37).ToString(),
				QuestHotkey = Game.Current.GameTextManager.GetHotKeyGameText("GenericCampaignPanelsGameKeyCategory", 42).ToString(),
				PartyHotkey = Game.Current.GameTextManager.GetHotKeyGameText("GenericCampaignPanelsGameKeyCategory", 43).ToString(),
				KingdomHotkey = Game.Current.GameTextManager.GetHotKeyGameText("GenericCampaignPanelsGameKeyCategory", 40).ToString(),
				ClanHotkey = Game.Current.GameTextManager.GetHotKeyGameText("GenericCampaignPanelsGameKeyCategory", 41).ToString(),
				InventoryHotkey = Game.Current.GameTextManager.GetHotKeyGameText("GenericCampaignPanelsGameKeyCategory", 38).ToString(),
				FastForwardHotkey = Game.Current.GameTextManager.GetHotKeyGameText("MapHotKeyCategory", 61).ToString(),
				PauseHotkey = Game.Current.GameTextManager.GetHotKeyGameText("MapHotKeyCategory", 59).ToString(),
				PlayHotkey = Game.Current.GameTextManager.GetHotKeyGameText("MapHotKeyCategory", 60).ToString()
			};
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000A49C File Offset: 0x0000869C
		protected override void OnTick(float dt)
		{
			base.OnTick(dt);
			this._gauntletLayer.UIContext.ContextAlpha = MathF.Lerp(this._gauntletLayer.UIContext.ContextAlpha, this._contextAlphaTarget, dt * this._contextAlphaModifider, 1E-05f);
			GameState activeState = Game.Current.GameStateManager.ActiveState;
			ScreenBase topScreen = ScreenManager.TopScreen;
			PanelScreenStatus panelScreenStatus = new PanelScreenStatus(topScreen);
			if (this._mapNavigationHandler != null)
			{
				this._mapNavigationHandler.IsNavigationLocked = panelScreenStatus.IsCurrentScreenLocksNavigation;
			}
			if (topScreen is MapScreen || panelScreenStatus.IsAnyPanelScreenOpen)
			{
				this._mapDataSource.IsEnabled = true;
				this._mapDataSource.CurrentScreen = topScreen.GetType().Name;
				bool flag = ScreenManager.TopScreen is MapScreen;
				this._mapDataSource.MapTimeControl.IsInMap = flag;
				base.Layer.InputRestrictions.SetInputRestrictions(false, InputUsageMask.All);
				if (!(activeState is MapState))
				{
					this._mapDataSource.MapTimeControl.IsCenterPanelEnabled = false;
					if (panelScreenStatus.IsAnyPanelScreenOpen)
					{
						this.HandlePanelSwitching(panelScreenStatus);
					}
				}
				else
				{
					MapState mapState = (MapState)activeState;
					if (flag)
					{
						MapScreen mapScreen = ScreenManager.TopScreen as MapScreen;
						mapScreen.SetIsBarExtended(this._mapDataSource.MapInfo.IsInfoBarExtended);
						this._mapDataSource.MapTimeControl.IsInRecruitment = mapScreen.IsInRecruitment;
						this._mapDataSource.MapTimeControl.IsInBattleSimulation = mapScreen.IsInBattleSimulation;
						this._mapDataSource.MapTimeControl.IsEncyclopediaOpen = this._encyclopediaManager.IsEncyclopediaOpen;
						this._mapDataSource.MapTimeControl.IsInArmyManagement = mapScreen.IsInArmyManagement;
						this._mapDataSource.MapTimeControl.IsInTownManagement = mapScreen.IsInTownManagement;
						this._mapDataSource.MapTimeControl.IsInHideoutTroopManage = mapScreen.IsInHideoutTroopManage;
						this._mapDataSource.MapTimeControl.IsInCampaignOptions = mapScreen.IsInCampaignOptions;
						this._mapDataSource.MapTimeControl.IsEscapeMenuOpened = mapScreen.IsEscapeMenuOpened;
						this._mapDataSource.MapTimeControl.IsMarriageOfferPopupActive = mapScreen.IsMarriageOfferPopupActive;
						this._mapDataSource.MapTimeControl.IsMapCheatsActive = mapScreen.IsMapCheatsActive;
						if (this._armyManagementVM != null)
						{
							this.HandleArmyManagementInput();
						}
					}
					else
					{
						this._mapDataSource.MapTimeControl.IsCenterPanelEnabled = false;
					}
				}
				this._mapDataSource.Tick(dt);
				return;
			}
			this._mapDataSource.IsEnabled = false;
			base.Layer.InputRestrictions.ResetInputRestrictions();
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000A71C File Offset: 0x0000891C
		private void HandleArmyManagementInput()
		{
			if (this._armyManagementLayer.Input.IsHotKeyReleased("Exit"))
			{
				UISoundsHelper.PlayUISound("event:/ui/default");
				this._armyManagementVM.ExecuteCancel();
				return;
			}
			if (this._armyManagementLayer.Input.IsHotKeyReleased("Confirm"))
			{
				UISoundsHelper.PlayUISound("event:/ui/default");
				this._armyManagementVM.ExecuteDone();
				return;
			}
			if (this._armyManagementLayer.Input.IsHotKeyReleased("Reset"))
			{
				UISoundsHelper.PlayUISound("event:/ui/default");
				this._armyManagementVM.ExecuteReset();
				return;
			}
			if (this._armyManagementLayer.Input.IsHotKeyReleased("RemoveParty") && this._armyManagementVM.FocusedItem != null)
			{
				UISoundsHelper.PlayUISound("event:/ui/default");
				this._armyManagementVM.FocusedItem.ExecuteAction();
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000A7F0 File Offset: 0x000089F0
		private void HandlePanelSwitching(PanelScreenStatus screenStatus)
		{
			GauntletLayer gauntletLayer = ScreenManager.TopScreen.FindLayer<GauntletLayer>();
			if (((gauntletLayer != null) ? gauntletLayer.Input : null) == null || gauntletLayer.IsFocusedOnInput())
			{
				return;
			}
			InputContext input = gauntletLayer.Input;
			if (input.IsGameKeyReleased(37) && !screenStatus.IsCharacterScreenOpen)
			{
				INavigationHandler mapNavigationHandlerAsInterface = this._mapNavigationHandlerAsInterface;
				if (mapNavigationHandlerAsInterface == null)
				{
					return;
				}
				mapNavigationHandlerAsInterface.OpenCharacterDeveloper();
				return;
			}
			else if (input.IsGameKeyReleased(43) && !screenStatus.IsPartyScreenOpen)
			{
				INavigationHandler mapNavigationHandlerAsInterface2 = this._mapNavigationHandlerAsInterface;
				if (mapNavigationHandlerAsInterface2 == null)
				{
					return;
				}
				mapNavigationHandlerAsInterface2.OpenParty();
				return;
			}
			else if (input.IsGameKeyReleased(42) && !screenStatus.IsQuestsScreenOpen)
			{
				INavigationHandler mapNavigationHandlerAsInterface3 = this._mapNavigationHandlerAsInterface;
				if (mapNavigationHandlerAsInterface3 == null)
				{
					return;
				}
				mapNavigationHandlerAsInterface3.OpenQuests();
				return;
			}
			else if (input.IsGameKeyReleased(38) && !screenStatus.IsInventoryScreenOpen)
			{
				INavigationHandler mapNavigationHandlerAsInterface4 = this._mapNavigationHandlerAsInterface;
				if (mapNavigationHandlerAsInterface4 == null)
				{
					return;
				}
				mapNavigationHandlerAsInterface4.OpenInventory();
				return;
			}
			else
			{
				if (!input.IsGameKeyReleased(41) || screenStatus.IsClanScreenOpen)
				{
					if (input.IsGameKeyReleased(40) && !screenStatus.IsKingdomScreenOpen)
					{
						INavigationHandler mapNavigationHandlerAsInterface5 = this._mapNavigationHandlerAsInterface;
						if (mapNavigationHandlerAsInterface5 == null)
						{
							return;
						}
						mapNavigationHandlerAsInterface5.OpenKingdom();
					}
					return;
				}
				INavigationHandler mapNavigationHandlerAsInterface6 = this._mapNavigationHandlerAsInterface;
				if (mapNavigationHandlerAsInterface6 == null)
				{
					return;
				}
				mapNavigationHandlerAsInterface6.OpenClan();
				return;
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000A8F8 File Offset: 0x00008AF8
		private void OpenArmyManagement()
		{
			if (this._gauntletLayer != null)
			{
				SpriteData spriteData = UIResourceManager.SpriteData;
				TwoDimensionEngineResourceContext resourceContext = UIResourceManager.ResourceContext;
				ResourceDepot uiresourceDepot = UIResourceManager.UIResourceDepot;
				this._armyManagementLayer = new GauntletLayer(300, "GauntletLayer", false);
				this._armyManagementCategory = spriteData.SpriteCategories["ui_armymanagement"];
				this._armyManagementCategory.Load(resourceContext, uiresourceDepot);
				this._armyManagementVM = new ArmyManagementVM(new Action(this.CloseArmyManagement));
				this._gauntletArmyManagementMovie = this._armyManagementLayer.LoadMovie("ArmyManagement", this._armyManagementVM);
				this._armyManagementLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
				this._armyManagementLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("Generic"));
				this._armyManagementLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
				this._armyManagementLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericCampaignPanelsGameKeyCategory"));
				this._armyManagementLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("ArmyManagementHotkeyCategory"));
				this._armyManagementLayer.IsFocusLayer = true;
				ScreenManager.TrySetFocus(this._armyManagementLayer);
				this._mapScreen.AddLayer(this._armyManagementLayer);
				this._armyManagementVM.SetCancelInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit"));
				this._armyManagementVM.SetDoneInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Confirm"));
				this._armyManagementVM.SetResetInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Reset"));
				this._armyManagementVM.SetRemoveInputKey(HotKeyManager.GetCategory("ArmyManagementHotkeyCategory").GetHotKey("RemoveParty"));
				this._timeControlModeBeforeArmyManagementOpened = Campaign.Current.TimeControlMode;
				Campaign.Current.TimeControlMode = CampaignTimeControlMode.Stop;
				Campaign.Current.SetTimeControlModeLock(true);
				MapScreen mapScreen;
				if ((mapScreen = (ScreenManager.TopScreen as MapScreen)) != null)
				{
					mapScreen.SetIsInArmyManagement(true);
				}
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000AAE8 File Offset: 0x00008CE8
		private void CloseArmyManagement()
		{
			this._armyManagementVM.OnFinalize();
			this._armyManagementLayer.ReleaseMovie(this._gauntletArmyManagementMovie);
			this._mapScreen.RemoveLayer(this._armyManagementLayer);
			this._armyManagementCategory.Unload();
			Game.Current.EventManager.TriggerEvent<TutorialContextChangedEvent>(new TutorialContextChangedEvent(TutorialContexts.MapWindow));
			this._gauntletArmyManagementMovie = null;
			this._armyManagementVM = null;
			this._armyManagementLayer = null;
			Campaign.Current.SetTimeControlModeLock(false);
			Campaign.Current.TimeControlMode = this._timeControlModeBeforeArmyManagementOpened;
			MapScreen mapScreen;
			if ((mapScreen = (ScreenManager.TopScreen as MapScreen)) != null)
			{
				mapScreen.SetIsInArmyManagement(false);
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000AB87 File Offset: 0x00008D87
		internal bool IsEscaped()
		{
			if (this._armyManagementVM != null)
			{
				this._armyManagementVM.ExecuteCancel();
				return true;
			}
			return false;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000AB9F File Offset: 0x00008D9F
		internal void OnMapConversationStart()
		{
			this._contextAlphaTarget = 0f;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000ABAC File Offset: 0x00008DAC
		internal void OnMapConversationEnd()
		{
			this._contextAlphaTarget = 1f;
		}

		// Token: 0x0400008E RID: 142
		private MapBarVM _mapDataSource;

		// Token: 0x0400008F RID: 143
		private GauntletLayer _gauntletLayer;

		// Token: 0x04000090 RID: 144
		private IGauntletMovie _movie;

		// Token: 0x04000091 RID: 145
		private SpriteCategory _mapBarCategory;

		// Token: 0x04000092 RID: 146
		private MapScreen _mapScreen;

		// Token: 0x04000093 RID: 147
		private MapNavigationHandler _mapNavigationHandler;

		// Token: 0x04000094 RID: 148
		private INavigationHandler _mapNavigationHandlerAsInterface;

		// Token: 0x04000095 RID: 149
		private MapEncyclopediaView _encyclopediaManager;

		// Token: 0x04000096 RID: 150
		private float _contextAlphaTarget = 1f;

		// Token: 0x04000097 RID: 151
		private float _contextAlphaModifider;

		// Token: 0x04000098 RID: 152
		private GauntletLayer _armyManagementLayer;

		// Token: 0x04000099 RID: 153
		private SpriteCategory _armyManagementCategory;

		// Token: 0x0400009A RID: 154
		private ArmyManagementVM _armyManagementVM;

		// Token: 0x0400009B RID: 155
		private IGauntletMovie _gauntletArmyManagementMovie;

		// Token: 0x0400009C RID: 156
		private CampaignTimeControlMode _timeControlModeBeforeArmyManagementOpened;
	}
}
