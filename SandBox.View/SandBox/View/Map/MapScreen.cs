using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Helpers;
using SandBox.View.Menu;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Inventory;
using TaleWorlds.CampaignSystem.Map;
using TaleWorlds.CampaignSystem.Overlay;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.Screens;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.MountAndBlade.View.Scripts;
using TaleWorlds.MountAndBlade.ViewModelCollection.EscapeMenu;
using TaleWorlds.ObjectSystem;
using TaleWorlds.ScreenSystem;

namespace SandBox.View.Map
{
	// Token: 0x0200004D RID: 77
	[GameStateScreen(typeof(MapState))]
	public class MapScreen : ScreenBase, IMapStateHandler, IGameStateListener
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060002BE RID: 702 RVA: 0x000171B0 File Offset: 0x000153B0
		// (set) Token: 0x060002BF RID: 703 RVA: 0x000171B7 File Offset: 0x000153B7
		public static MapScreen Instance { get; private set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x000171BF File Offset: 0x000153BF
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x000171C7 File Offset: 0x000153C7
		public CampaignMapSiegePrefabEntityCache PrefabEntityCache { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x000171D0 File Offset: 0x000153D0
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x000171D8 File Offset: 0x000153D8
		public MapEncyclopediaView EncyclopediaScreenManager { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x000171E1 File Offset: 0x000153E1
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x000171E9 File Offset: 0x000153E9
		public MapNotificationView MapNotificationView { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x000171F2 File Offset: 0x000153F2
		public bool IsInMenu
		{
			get
			{
				return this._menuViewContext != null;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x000171FD File Offset: 0x000153FD
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x00017205 File Offset: 0x00015405
		public bool IsEscapeMenuOpened { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0001720E File Offset: 0x0001540E
		// (set) Token: 0x060002CA RID: 714 RVA: 0x00017216 File Offset: 0x00015416
		public PartyVisual CurrentVisualOfTooltip { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0001721F File Offset: 0x0001541F
		// (set) Token: 0x060002CC RID: 716 RVA: 0x00017227 File Offset: 0x00015427
		public SceneLayer SceneLayer { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00017230 File Offset: 0x00015430
		public IInputContext Input
		{
			get
			{
				return this.SceneLayer.Input;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0001723D File Offset: 0x0001543D
		public bool IsReady
		{
			get
			{
				return this._isReadyForRender;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00017245 File Offset: 0x00015445
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x0001724D File Offset: 0x0001544D
		public bool IsInBattleSimulation { get; private set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00017256 File Offset: 0x00015456
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x0001725E File Offset: 0x0001545E
		public bool IsInTownManagement { get; private set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00017267 File Offset: 0x00015467
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x0001726F File Offset: 0x0001546F
		public bool IsInHideoutTroopManage { get; private set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00017278 File Offset: 0x00015478
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x00017280 File Offset: 0x00015480
		public bool IsInArmyManagement { get; private set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00017289 File Offset: 0x00015489
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x00017291 File Offset: 0x00015491
		public bool IsInRecruitment { get; private set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0001729A File Offset: 0x0001549A
		// (set) Token: 0x060002DA RID: 730 RVA: 0x000172A2 File Offset: 0x000154A2
		public bool IsBarExtended { get; private set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060002DB RID: 731 RVA: 0x000172AB File Offset: 0x000154AB
		// (set) Token: 0x060002DC RID: 732 RVA: 0x000172B3 File Offset: 0x000154B3
		public bool IsInCampaignOptions { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060002DD RID: 733 RVA: 0x000172BC File Offset: 0x000154BC
		// (set) Token: 0x060002DE RID: 734 RVA: 0x000172C4 File Offset: 0x000154C4
		public bool IsMarriageOfferPopupActive { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060002DF RID: 735 RVA: 0x000172CD File Offset: 0x000154CD
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x000172D5 File Offset: 0x000154D5
		public bool IsMapCheatsActive { get; private set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x000172E0 File Offset: 0x000154E0
		public Dictionary<Tuple<Material, BannerCode>, Material> BannerTexturedMaterialCache
		{
			get
			{
				Dictionary<Tuple<Material, BannerCode>, Material> result;
				if ((result = this._bannerTexturedMaterialCache) == null)
				{
					result = (this._bannerTexturedMaterialCache = new Dictionary<Tuple<Material, BannerCode>, Material>());
				}
				return result;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00017305 File Offset: 0x00015505
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x0001730D File Offset: 0x0001550D
		public bool MapSceneCursorActive
		{
			get
			{
				return this._mapSceneCursorActive;
			}
			set
			{
				if (this._mapSceneCursorActive != value)
				{
					this._mapSceneCursorActive = value;
				}
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0001731F File Offset: 0x0001551F
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x00017327 File Offset: 0x00015527
		public GameEntity ContourMaskEntity { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00017330 File Offset: 0x00015530
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x00017338 File Offset: 0x00015538
		public List<Mesh> InactiveLightMeshes { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00017341 File Offset: 0x00015541
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x00017349 File Offset: 0x00015549
		public List<Mesh> ActiveLightMeshes { get; private set; }

		// Token: 0x060002EA RID: 746 RVA: 0x00017354 File Offset: 0x00015554
		public MapScreen(MapState mapState)
		{
			this._mapState = mapState;
			mapState.Handler = this;
			this._periodicCampaignUIEvents = new List<MBCampaignEvent>();
			this.InitializeVisuals();
			CampaignMusicHandler.Create();
			this._mapViews = new ObservableCollection<MapView>();
			this._mapViewsCopyCache = new MapView[0];
			this._mapCameraView = (MapCameraView)this.AddMapView<MapCameraView>(Array.Empty<object>());
			this.MapTracksCampaignBehavior = Campaign.Current.GetCampaignBehavior<IMapTracksCampaignBehavior>();
		}

		// Token: 0x060002EB RID: 747 RVA: 0x000174E4 File Offset: 0x000156E4
		public void OnHoverMapEntity(IMapEntity mapEntity)
		{
			uint hashCode = (uint)mapEntity.GetHashCode();
			if (this._tooltipTargetHash != hashCode)
			{
				this._tooltipTargetHash = hashCode;
				this._tooltipTargetObject = null;
				mapEntity.OnHover();
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00017515 File Offset: 0x00015715
		public void SetupMapTooltipForTrack(Track track)
		{
			if (this._tooltipTargetObject != track)
			{
				this._tooltipTargetObject = track;
				this._tooltipTargetHash = 0U;
				InformationManager.ShowTooltip(typeof(Track), new object[]
				{
					track
				});
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00017547 File Offset: 0x00015747
		public void RemoveMapTooltip()
		{
			if (this._tooltipTargetObject != null || this._tooltipTargetHash != 0U)
			{
				this._tooltipTargetObject = null;
				this._tooltipTargetHash = 0U;
				MBInformationManager.HideInformations();
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0001756C File Offset: 0x0001576C
		private static void PreloadTextures()
		{
			List<string> list = new List<string>();
			list.Add("gui_map_circle_enemy");
			list.Add("gui_map_circle_enemy_selected");
			list.Add("gui_map_circle_neutral");
			list.Add("gui_map_circle_neutral_selected");
			for (int i = 2; i <= 5; i++)
			{
				list.Add("gui_map_circle_enemy_selected_" + i);
				list.Add("gui_map_circle_neutral_selected_" + i);
			}
			for (int j = 0; j < list.Count; j++)
			{
				Texture.GetFromResource(list[j]).PreloadTexture(false);
			}
			list.Clear();
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0001760C File Offset: 0x0001580C
		private void HandleSiegeEngineHoverEnd()
		{
			if (this._preSelectedSiegeEntityID != UIntPtr.Zero)
			{
				MapScreen.FrameAndVisualOfEngines[this._preSelectedSiegeEntityID].Item2.OnMapHoverSiegeEngineEnd();
				this._preSelectedSiegeEntityID = UIntPtr.Zero;
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00017648 File Offset: 0x00015848
		private void SetCameraOfSceneLayer()
		{
			this.SceneLayer.SetCamera(this._mapCameraView.Camera);
			Vec3 origin = this._mapCameraView.CameraFrame.origin;
			origin.z = 0f;
			this.SceneLayer.SetFocusedShadowmap(false, ref origin, 0f);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0001769C File Offset: 0x0001589C
		protected override void OnResume()
		{
			base.OnResume();
			MapScreen.PreloadTextures();
			this._isSoundOn = true;
			this.RestartAmbientSounds();
			if (this._gpuMemoryCleared)
			{
				this._gpuMemoryCleared = false;
			}
			for (int i = this._mapViews.Count - 1; i >= 0; i--)
			{
				this._mapViews[i].OnResume();
			}
			MenuContext menuContext = this._mapState.MenuContext;
			if (this._menuViewContext != null)
			{
				if (menuContext != null && menuContext != this._menuViewContext.MenuContext)
				{
					this._menuViewContext.UpdateMenuContext(menuContext);
				}
				else if (menuContext == null)
				{
					this.ExitMenuContext();
				}
			}
			MenuViewContext menuViewContext = this._menuViewContext;
			if (menuViewContext != null)
			{
				menuViewContext.OnResume();
			}
			(Campaign.Current.MapSceneWrapper as MapScene).ValidateAgentVisualsReseted();
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0001775A File Offset: 0x0001595A
		protected override void OnPause()
		{
			base.OnPause();
			MBInformationManager.HideInformations();
			this.PauseAmbientSounds();
			this._isSoundOn = false;
			this._activatedFrameNo = Utilities.EngineFrameNo;
			this.HandleIfSceneIsReady();
			this._conversationOverThisFrame = false;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0001778C File Offset: 0x0001598C
		protected override void OnActivate()
		{
			base.OnActivate();
			this._mapCameraView.OnActivate(this._leftButtonDraggingMode, this._clickedPosition);
			this._activatedFrameNo = Utilities.EngineFrameNo;
			this.HandleIfSceneIsReady();
			Game.Current.EventManager.TriggerEvent<TutorialContextChangedEvent>(new TutorialContextChangedEvent(TutorialContexts.MapWindow));
			this.SetCameraOfSceneLayer();
			this.RestartAmbientSounds();
			PartyBase.MainParty.SetVisualAsDirty();
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x000177F2 File Offset: 0x000159F2
		public void ClearGPUMemory()
		{
			if (true)
			{
				this.SceneLayer.ClearRuntimeGPUMemory(true);
			}
			Texture.ReleaseGpuMemories();
			this._gpuMemoryCleared = true;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00017810 File Offset: 0x00015A10
		protected override void OnDeactivate()
		{
			Game game = Game.Current;
			if (game != null)
			{
				game.EventManager.TriggerEvent<TutorialContextChangedEvent>(new TutorialContextChangedEvent(TutorialContexts.None));
			}
			this.PauseAmbientSounds();
			MenuViewContext menuViewContext = this._menuViewContext;
			if (menuViewContext != null)
			{
				menuViewContext.StopAllSounds();
			}
			MBInformationManager.HideInformations();
			for (int i = this._mapViews.Count - 1; i >= 0; i--)
			{
				this._mapViews[i].OnDeactivate();
			}
			base.OnDeactivate();
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00017884 File Offset: 0x00015A84
		public override void OnFocusChangeOnGameWindow(bool focusGained)
		{
			base.OnFocusChangeOnGameWindow(focusGained);
			if (!focusGained && BannerlordConfig.StopGameOnFocusLost)
			{
				Func<bool> isAnyInquiryActive = InformationManager.IsAnyInquiryActive;
				if (isAnyInquiryActive != null && !isAnyInquiryActive())
				{
					MapEncyclopediaView encyclopediaScreenManager = this.EncyclopediaScreenManager;
					if (encyclopediaScreenManager == null || !encyclopediaScreenManager.IsEncyclopediaOpen)
					{
						if (this._mapViews.All((MapView m) => m.IsOpeningEscapeMenuOnFocusChangeAllowed()))
						{
							this.OnEscapeMenuToggled(true);
						}
					}
				}
			}
			this._focusLost = !focusGained;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0001790C File Offset: 0x00015B0C
		public MapView AddMapView<T>(params object[] parameters) where T : MapView, new()
		{
			for (int i = 0; i < this._mapViews.Count; i++)
			{
				T t;
				if ((t = (this._mapViews[i] as T)) != null)
				{
					Debug.FailedAssert("Map view already added to the list", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox.View\\Map\\MapScreen.cs", "AddMapView", 492);
					Debug.Print("Map view already added to the list: " + typeof(T).Name + ". Returning existing view instead of creating new one.", 0, Debug.DebugColor.White, 17592186044416UL);
					return t;
				}
			}
			MapView mapView = SandBoxViewCreator.CreateMapView<T>(parameters);
			mapView.MapScreen = this;
			mapView.MapState = this._mapState;
			this._mapViews.Add(mapView);
			mapView.CreateLayout();
			return mapView;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x000179CC File Offset: 0x00015BCC
		public T GetMapView<T>() where T : MapView
		{
			foreach (MapView mapView in this._mapViews)
			{
				if (mapView is T)
				{
					return (T)((object)mapView);
				}
			}
			return default(T);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00017A30 File Offset: 0x00015C30
		public void RemoveMapView(MapView mapView)
		{
			mapView.OnFinalize();
			this._mapViews.Remove(mapView);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00017A48 File Offset: 0x00015C48
		public void AddEncounterOverlay(GameOverlays.MenuOverlayType type)
		{
			if (this._encounterOverlay == null)
			{
				this._encounterOverlay = this.AddMapView<MapOverlayView>(new object[]
				{
					type
				});
				for (int i = this._mapViews.Count - 1; i >= 0; i--)
				{
					this._mapViews[i].OnOverlayCreated();
				}
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00017AA4 File Offset: 0x00015CA4
		public void AddArmyOverlay(GameOverlays.MapOverlayType type)
		{
			if (this._armyOverlay == null)
			{
				this._armyOverlay = this.AddMapView<MapOverlayView>(new object[]
				{
					type
				});
				for (int i = this._mapViews.Count - 1; i >= 0; i--)
				{
					this._mapViews[i].OnOverlayCreated();
				}
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00017B00 File Offset: 0x00015D00
		public void RemoveEncounterOverlay()
		{
			if (this._encounterOverlay != null)
			{
				this.RemoveMapView(this._encounterOverlay);
				this._encounterOverlay = null;
				for (int i = this._mapViews.Count - 1; i >= 0; i--)
				{
					this._mapViews[i].OnOverlayClosed();
				}
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00017B54 File Offset: 0x00015D54
		public void RemoveArmyOverlay()
		{
			if (this._armyOverlay != null)
			{
				this.RemoveMapView(this._armyOverlay);
				this._armyOverlay = null;
				for (int i = this._mapViews.Count - 1; i >= 0; i--)
				{
					this._mapViews[i].OnOverlayClosed();
				}
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00017BA8 File Offset: 0x00015DA8
		protected override void OnInitialize()
		{
			base.OnInitialize();
			if (MBDebug.TestModeEnabled)
			{
				this.CheckValidityOfItems();
			}
			MapScreen.Instance = this;
			this._mapCameraView.Initialize();
			ViewSubModule.BannerTexturedMaterialCache = this.BannerTexturedMaterialCache;
			this.SceneLayer = new SceneLayer("SceneLayer", true, false);
			this.SceneLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("Generic"));
			this.SceneLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			this.SceneLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericCampaignPanelsGameKeyCategory"));
			this.SceneLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("MapHotKeyCategory"));
			base.AddLayer(this.SceneLayer);
			this._mapScene = ((MapScene)Campaign.Current.MapSceneWrapper).Scene;
			Utilities.SetAllocationAlwaysValidScene(null);
			this.SceneLayer.SetScene(this._mapScene);
			this.SceneLayer.SceneView.SetEnable(false);
			this.SceneLayer.SetSceneUsesShadows(true);
			this.SceneLayer.SetRenderWithPostfx(true);
			this.SceneLayer.SetSceneUsesContour(true);
			this.SceneLayer.SceneView.SetAcceptGlobalDebugRenderObjects(true);
			this.SceneLayer.SceneView.SetResolutionScaling(true);
			this.CollectTickableMapMeshes();
			this.MapNotificationView = (this.AddMapView<MapNotificationView>(Array.Empty<object>()) as MapNotificationView);
			this.AddMapView<MapBasicView>(Array.Empty<object>());
			this.AddMapView<MapSettlementNameplateView>(Array.Empty<object>());
			this.AddMapView<MapPartyNameplateView>(Array.Empty<object>());
			this.AddMapView<MapEventVisualsView>(Array.Empty<object>());
			this.AddMapView<MapMobilePartyTrackerView>(Array.Empty<object>());
			this.AddMapView<MapSaveView>(Array.Empty<object>());
			this.AddMapView<MapGamepadEffectsView>(Array.Empty<object>());
			this.EncyclopediaScreenManager = (this.AddMapView<MapEncyclopediaView>(Array.Empty<object>()) as MapEncyclopediaView);
			this.AddMapView<MapBarView>(Array.Empty<object>());
			this._mapReadyView = (this.AddMapView<MapReadyView>(Array.Empty<object>()) as MapReadyView);
			this._mapReadyView.SetIsMapSceneReady(false);
			this._mouseRay = new Ray(Vec3.Zero, Vec3.Up, float.MaxValue);
			if (PlayerSiege.PlayerSiegeEvent != null)
			{
				if (this != null)
				{
					((IMapStateHandler)this).OnPlayerSiegeActivated();
				}
			}
			this.PrefabEntityCache = this.SceneLayer.SceneView.GetScene().GetFirstEntityWithScriptComponent<CampaignMapSiegePrefabEntityCache>().GetFirstScriptOfType<CampaignMapSiegePrefabEntityCache>();
			CampaignEvents.OnSaveOverEvent.AddNonSerializedListener(this, new Action<bool, string>(this.OnSaveOver));
			CampaignEvents.OnMarriageOfferedToPlayerEvent.AddNonSerializedListener(this, new Action<Hero, Hero>(this.OnMarriageOfferedToPlayer));
			CampaignEvents.OnMarriageOfferCanceledEvent.AddNonSerializedListener(this, new Action<Hero, Hero>(this.OnMarriageOfferCanceled));
			GameEntity firstEntityWithScriptComponent = this._mapScene.GetFirstEntityWithScriptComponent<MapColorGradeManager>();
			if (firstEntityWithScriptComponent != null)
			{
				this._colorGradeManager = firstEntityWithScriptComponent.GetFirstScriptOfType<MapColorGradeManager>();
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00017E59 File Offset: 0x00016059
		private void OnSaveOver(bool isSuccessful, string newSaveGameName)
		{
			if (this._exitOnSaveOver)
			{
				if (isSuccessful)
				{
					this.OnExit();
				}
				this._exitOnSaveOver = false;
			}
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00017E73 File Offset: 0x00016073
		private void OnMarriageOfferedToPlayer(Hero suitor, Hero maiden)
		{
			this._marriageOfferPopupView = this.AddMapView<MarriageOfferPopupView>(new object[]
			{
				suitor,
				maiden
			});
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00017E8F File Offset: 0x0001608F
		private void OnMarriageOfferCanceled(Hero suitor, Hero maiden)
		{
			if (this._marriageOfferPopupView != null)
			{
				this.RemoveMapView(this._marriageOfferPopupView);
				this._marriageOfferPopupView = null;
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00017EAC File Offset: 0x000160AC
		protected override void OnFinalize()
		{
			for (int i = this._mapViews.Count - 1; i >= 0; i--)
			{
				this._mapViews[i].OnFinalize();
			}
			PartyVisualManager.Current.OnFinalized();
			base.OnFinalize();
			if (this._mapScene != null)
			{
				this._mapScene.ClearAll();
			}
			Common.MemoryCleanupGC(false);
			this._characterBannerMaterialCache.Clear();
			this._characterBannerMaterialCache = null;
			ViewSubModule.BannerTexturedMaterialCache = null;
			MBMusicManager.Current.DeactivateCampaignMode();
			MBMusicManager.Current.OnCampaignMusicHandlerFinalize();
			CampaignEvents.OnSaveOverEvent.ClearListeners(this);
			CampaignEvents.OnMarriageOfferedToPlayerEvent.ClearListeners(this);
			CampaignEvents.OnMarriageOfferCanceledEvent.ClearListeners(this);
			this._mapScene = null;
			this._campaign = null;
			this._navigationHandler = null;
			this._mapCameraView = null;
			MapScreen.Instance = null;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00017F80 File Offset: 0x00016180
		public void OnHourlyTick()
		{
			for (int i = this._mapViews.Count - 1; i >= 0; i--)
			{
				this._mapViews[i].OnHourlyTick();
			}
			Kingdom kingdom = Clan.PlayerClan.Kingdom;
			object obj;
			if (kingdom == null)
			{
				obj = null;
			}
			else
			{
				obj = kingdom.UnresolvedDecisions.FirstOrDefault((KingdomDecision d) => d.NotifyPlayer && d.IsEnforced && d.IsPlayerParticipant && !d.ShouldBeCancelled());
			}
			this._isKingdomDecisionsDirty = (obj != null);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00017FFC File Offset: 0x000161FC
		private void OnRenderingStateChanged(bool startedRendering)
		{
			if (startedRendering && this._isSceneViewEnabled && this._conversationDataCache != null)
			{
				Campaign.Current.ConversationManager.Handler = null;
				Game.Current.GameStateManager.UnregisterActiveStateDisableRequest(this);
				this.HandleMapConversationInit(this._conversationDataCache.Item1, this._conversationDataCache.Item2);
				this._conversationDataCache.Item3.ApplyHandlerChangesTo(this._mapConversationView as IConversationStateHandler);
				this._conversationDataCache = null;
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0001807C File Offset: 0x0001627C
		private void ShowNextKingdomDecisionPopup()
		{
			Kingdom kingdom = Clan.PlayerClan.Kingdom;
			KingdomDecision kingdomDecision;
			if (kingdom == null)
			{
				kingdomDecision = null;
			}
			else
			{
				kingdomDecision = kingdom.UnresolvedDecisions.FirstOrDefault((KingdomDecision d) => d.NotifyPlayer && d.IsEnforced && d.IsPlayerParticipant && !d.ShouldBeCancelled());
			}
			KingdomDecision kingdomDecision2 = kingdomDecision;
			if (kingdomDecision2 != null)
			{
				InquiryData data = new InquiryData(new TextObject("{=A7349NHy}Critical Kingdom Decision", null).ToString(), kingdomDecision2.GetChooseTitle().ToString(), true, false, new TextObject("{=bFzZwwjT}Examine", null).ToString(), "", delegate()
				{
					this.OpenKingdom();
				}, null, "", 0f, null, null, null);
				kingdomDecision2.NotifyPlayer = false;
				InformationManager.ShowInquiry(data, true, false);
				this._isKingdomDecisionsDirty = false;
				return;
			}
			Debug.FailedAssert("There is no dirty decision but still demanded one", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox.View\\Map\\MapScreen.cs", "ShowNextKingdomDecisionPopup", 773);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00018148 File Offset: 0x00016348
		void IMapStateHandler.OnMenuModeTick(float dt)
		{
			for (int i = this._mapViews.Count - 1; i >= 0; i--)
			{
				this._mapViews[i].OnMenuModeTick(dt);
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00018180 File Offset: 0x00016380
		private void HandleIfBlockerStatesDisabled()
		{
			bool isReadyForRender = this._isReadyForRender;
			bool flag = this.SceneLayer.SceneView.ReadyToRender() && this.SceneLayer.SceneView.CheckSceneReadyToRender();
			bool flag2 = (this._isSceneViewEnabled || this._mapConversationView != null) && flag;
			if (LoadingWindow.IsLoadingWindowActive && flag2)
			{
				LoadingWindow.DisableGlobalLoadingWindow();
			}
			this._mapReadyView.SetIsMapSceneReady(flag2);
			this._isReadyForRender = flag2;
			if (isReadyForRender != this._isReadyForRender)
			{
				this.OnRenderingStateChanged(this._isReadyForRender);
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00018208 File Offset: 0x00016408
		private void CheckCursorState()
		{
			Vec3 zero = Vec3.Zero;
			Vec3 zero2 = Vec3.Zero;
			this.SceneLayer.SceneView.TranslateMouse(ref zero, ref zero2, -1f);
			Vec3 vec = zero;
			Vec3 vec2 = zero2;
			PathFaceRecord nullFaceRecord = PathFaceRecord.NullFaceRecord;
			float num;
			Vec3 vec3;
			this.GetCursorIntersectionPoint(ref vec, ref vec2, out num, out vec3, ref nullFaceRecord, BodyFlags.CommonFocusRayCastExcludeFlags);
			bool flag = Campaign.Current.MapSceneWrapper.AreFacesOnSameIsland(nullFaceRecord, MobileParty.MainParty.CurrentNavigationFace, false);
			this.SceneLayer.ActiveCursor = (flag ? CursorType.Default : CursorType.Disabled);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0001828C File Offset: 0x0001648C
		private void HandleIfSceneIsReady()
		{
			int num = Utilities.EngineFrameNo - this._activatedFrameNo;
			bool flag = this._isSceneViewEnabled;
			if (num < 5)
			{
				flag = false;
				MapColorGradeManager colorGradeManager = this._colorGradeManager;
				if (colorGradeManager != null)
				{
					colorGradeManager.ApplyAtmosphere(true);
				}
			}
			else
			{
				int num2 = (this._mapConversationView != null) ? 1 : 0;
				bool flag2 = ScreenManager.TopScreen == this;
				flag = (num2 == 0 && flag2);
			}
			if (flag != this._isSceneViewEnabled)
			{
				this._isSceneViewEnabled = flag;
				this.SceneLayer.SceneView.SetEnable(this._isSceneViewEnabled);
				if (this._isSceneViewEnabled)
				{
					this._mapScene.CheckResources();
					if (this._focusLost && !this.IsEscapeMenuOpened)
					{
						this.OnFocusChangeOnGameWindow(false);
					}
				}
			}
			this.HandleIfBlockerStatesDisabled();
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00018333 File Offset: 0x00016533
		void IMapStateHandler.StartCameraAnimation(Vec2 targetPosition, float animationStopDuration)
		{
			this._mapCameraView.StartCameraAnimation(targetPosition, animationStopDuration);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00018344 File Offset: 0x00016544
		void IMapStateHandler.BeforeTick(float dt)
		{
			this.HandleIfSceneIsReady();
			bool flag = MobileParty.MainParty != null && PartyBase.MainParty.IsValid;
			if (flag && !this._mapCameraView.CameraAnimationInProgress)
			{
				if (!this.IsInMenu && this.SceneLayer.Input.IsHotKeyPressed("MapChangeCursorMode"))
				{
					this._mapSceneCursorWanted = !this._mapSceneCursorWanted;
				}
				if (this.SceneLayer.Input.IsHotKeyPressed("MapClick"))
				{
					this._secondLastPressTime = this._lastPressTime;
					this._lastPressTime = (double)Time.ApplicationTime;
				}
				this._leftButtonDoubleClickOnSceneWidget = false;
				if (this.SceneLayer.Input.IsHotKeyReleased("MapClick"))
				{
					Vec2 mousePositionPixel = this.SceneLayer.Input.GetMousePositionPixel();
					float applicationTime = Time.ApplicationTime;
					this._leftButtonDoubleClickOnSceneWidget = ((double)applicationTime - this._lastReleaseTime < 0.30000001192092896 && (double)applicationTime - this._secondLastPressTime < 0.44999998807907104 && mousePositionPixel.Distance(this._oldMousePosition) < 10f);
					if (this._leftButtonDoubleClickOnSceneWidget)
					{
						this._waitForDoubleClickUntilTime = 0f;
					}
					this._oldMousePosition = this.SceneLayer.Input.GetMousePositionPixel();
					this._lastReleaseTime = (double)applicationTime;
				}
				if (this.IsReady)
				{
					this.HandleMouse(dt);
				}
			}
			float deltaMouseScroll = this.SceneLayer.Input.GetDeltaMouseScroll();
			Vec3 zero = Vec3.Zero;
			Vec3 zero2 = Vec3.Zero;
			this.SceneLayer.SceneView.TranslateMouse(ref zero, ref zero2, -1f);
			float gameKeyAxis = this.SceneLayer.Input.GetGameKeyAxis("CameraAxisX");
			float num;
			Vec3 projectedPosition;
			bool rayCastForClosestEntityOrTerrainCondition = this._mapScene.RayCastForClosestEntityOrTerrain(zero, zero2, out num, out projectedPosition, 0.01f, BodyFlags.CameraCollisionRayCastExludeFlags);
			float rx = 0f;
			float ry = 0f;
			float num2 = 1f;
			bool flag2 = !TaleWorlds.InputSystem.Input.IsGamepadActive && !this.IsInMenu && ScreenManager.FocusedLayer == this.SceneLayer;
			bool flag3 = TaleWorlds.InputSystem.Input.IsGamepadActive && this.MapSceneCursorActive;
			if (flag2 || flag3)
			{
				if (this.SceneLayer.Input.IsGameKeyDown(54))
				{
					num2 = this._mapCameraView.CameraFastMoveMultiplier;
				}
				rx = this.SceneLayer.Input.GetGameKeyAxis("MapMovementAxisX") * num2;
				ry = this.SceneLayer.Input.GetGameKeyAxis("MapMovementAxisY") * num2;
			}
			this._ignoreLeftMouseRelease = false;
			if (this.SceneLayer.Input.IsKeyPressed(InputKey.LeftMouseButton))
			{
				this._clickedPositionPixel = this.SceneLayer.Input.GetMousePositionPixel();
				this._mapScene.RayCastForClosestEntityOrTerrain(this._mouseRay.Origin, this._mouseRay.EndPoint, out num, out this._clickedPosition, 0.01f, BodyFlags.CameraCollisionRayCastExludeFlags);
				if (this.CurrentVisualOfTooltip != null)
				{
					this.RemoveMapTooltip();
				}
				this._leftButtonDraggingMode = false;
			}
			else if (this.SceneLayer.Input.IsKeyDown(InputKey.LeftMouseButton) && !this.SceneLayer.Input.IsKeyReleased(InputKey.LeftMouseButton) && (this.SceneLayer.Input.GetMousePositionPixel().DistanceSquared(this._clickedPositionPixel) > 300f || this._leftButtonDraggingMode) && !this.IsInMenu)
			{
				this._leftButtonDraggingMode = true;
			}
			else if (this._leftButtonDraggingMode)
			{
				this._leftButtonDraggingMode = false;
				this._ignoreLeftMouseRelease = true;
			}
			if (this.SceneLayer.Input.IsKeyDown(InputKey.MiddleMouseButton))
			{
				MBWindowManager.DontChangeCursorPos();
			}
			if (this.SceneLayer.Input.IsKeyReleased(InputKey.LeftMouseButton))
			{
				this._clickedPositionPixel = this.SceneLayer.Input.GetMousePositionPixel();
			}
			this.MapSceneCursorActive = (!this.SceneLayer.Input.GetIsMouseActive() && !this.IsInMenu && ScreenManager.FocusedLayer == this.SceneLayer && this._mapSceneCursorWanted);
			MapCameraView.InputInformation inputInformation;
			inputInformation.IsMainPartyValid = flag;
			inputInformation.IsMapReady = this.IsReady;
			inputInformation.IsControlDown = this.SceneLayer.Input.IsControlDown();
			inputInformation.IsMouseActive = this.SceneLayer.Input.GetIsMouseActive();
			inputInformation.CheatModeEnabled = Game.Current.CheatMode;
			inputInformation.DeltaMouseScroll = deltaMouseScroll;
			inputInformation.LeftMouseButtonPressed = this.SceneLayer.Input.IsKeyPressed(InputKey.LeftMouseButton);
			inputInformation.LeftMouseButtonDown = this.SceneLayer.Input.IsKeyDown(InputKey.LeftMouseButton);
			inputInformation.LeftMouseButtonReleased = this.SceneLayer.Input.IsKeyReleased(InputKey.LeftMouseButton);
			inputInformation.MiddleMouseButtonDown = this.SceneLayer.Input.IsKeyDown(InputKey.MiddleMouseButton);
			inputInformation.RightMouseButtonDown = this.SceneLayer.Input.IsKeyDown(InputKey.RightMouseButton);
			inputInformation.RotateLeftKeyDown = this.SceneLayer.Input.IsGameKeyDown(57);
			inputInformation.RotateRightKeyDown = this.SceneLayer.Input.IsGameKeyDown(58);
			inputInformation.PartyMoveUpKey = this.SceneLayer.Input.IsGameKeyDown(49);
			inputInformation.PartyMoveDownKey = this.SceneLayer.Input.IsGameKeyDown(50);
			inputInformation.PartyMoveLeftKey = this.SceneLayer.Input.IsGameKeyDown(52);
			inputInformation.PartyMoveRightKey = this.SceneLayer.Input.IsGameKeyDown(51);
			inputInformation.MapZoomIn = this.SceneLayer.Input.GetGameKeyState(55);
			inputInformation.MapZoomOut = this.SceneLayer.Input.GetGameKeyState(56);
			inputInformation.CameraFollowModeKeyPressed = this.SceneLayer.Input.IsGameKeyPressed(63);
			inputInformation.MousePositionPixel = this.SceneLayer.Input.GetMousePositionPixel();
			inputInformation.ClickedPositionPixel = this._clickedPositionPixel;
			inputInformation.ClickedPosition = this._clickedPosition;
			inputInformation.LeftButtonDraggingMode = this._leftButtonDraggingMode;
			inputInformation.IsInMenu = this.IsInMenu;
			inputInformation.WorldMouseNear = zero;
			inputInformation.WorldMouseFar = zero2;
			inputInformation.MouseSensitivity = this.SceneLayer.Input.GetMouseSensitivity();
			inputInformation.MouseMoveX = this.SceneLayer.Input.GetMouseMoveX();
			inputInformation.MouseMoveY = this.SceneLayer.Input.GetMouseMoveY();
			inputInformation.HorizontalCameraInput = gameKeyAxis;
			inputInformation.RayCastForClosestEntityOrTerrainCondition = rayCastForClosestEntityOrTerrainCondition;
			inputInformation.ProjectedPosition = projectedPosition;
			inputInformation.RX = rx;
			inputInformation.RY = ry;
			inputInformation.RS = num2;
			inputInformation.Dt = dt;
			this._mapCameraView.OnBeforeTick(inputInformation);
			this._mapCursor.SetVisible(this.MapSceneCursorActive);
			if (flag && !this._campaign.TimeControlModeLock)
			{
				if (this._mapState.AtMenu)
				{
					if (Campaign.Current.CurrentMenuContext == null)
					{
						goto IL_9AD;
					}
					GameMenu gameMenu = Campaign.Current.CurrentMenuContext.GameMenu;
					if (gameMenu == null || !gameMenu.IsWaitActive)
					{
						goto IL_9AD;
					}
				}
				float applicationTime2 = Time.ApplicationTime;
				if (this.SceneLayer.Input.IsGameKeyPressed(62) && this._timeToggleTimer == 3.4028235E+38f)
				{
					this._timeToggleTimer = applicationTime2;
				}
				if (this.SceneLayer.Input.IsGameKeyPressed(62) && applicationTime2 - this._timeToggleTimer > 0.4f)
				{
					if (this._campaign.TimeControlMode == CampaignTimeControlMode.StoppablePlay || this._campaign.TimeControlMode == CampaignTimeControlMode.UnstoppablePlay)
					{
						this._campaign.SetTimeSpeed(2);
					}
					else if (this._campaign.TimeControlMode == CampaignTimeControlMode.StoppableFastForward || this._campaign.TimeControlMode == CampaignTimeControlMode.UnstoppableFastForward)
					{
						this._campaign.SetTimeSpeed(1);
					}
					else if (this._campaign.TimeControlMode == CampaignTimeControlMode.Stop)
					{
						this._campaign.SetTimeSpeed(1);
					}
					else if (this._campaign.TimeControlMode == CampaignTimeControlMode.FastForwardStop)
					{
						this._campaign.SetTimeSpeed(2);
					}
					this._timeToggleTimer = float.MaxValue;
					this._ignoreNextTimeToggle = true;
				}
				else if (this.SceneLayer.Input.IsGameKeyPressed(62))
				{
					if (this._ignoreNextTimeToggle)
					{
						this._ignoreNextTimeToggle = false;
					}
					else
					{
						this._waitForDoubleClickUntilTime = 0f;
						if (this._campaign.TimeControlMode == CampaignTimeControlMode.UnstoppableFastForward || this._campaign.TimeControlMode == CampaignTimeControlMode.UnstoppablePlay || ((this._campaign.TimeControlMode == CampaignTimeControlMode.StoppableFastForward || this._campaign.TimeControlMode == CampaignTimeControlMode.StoppablePlay) && !this._campaign.IsMainPartyWaiting))
						{
							this._campaign.SetTimeSpeed(0);
						}
						else if (this._campaign.TimeControlMode == CampaignTimeControlMode.Stop || this._campaign.TimeControlMode == CampaignTimeControlMode.StoppablePlay)
						{
							this._campaign.SetTimeSpeed(1);
						}
						else if (this._campaign.TimeControlMode == CampaignTimeControlMode.FastForwardStop || this._campaign.TimeControlMode == CampaignTimeControlMode.StoppableFastForward)
						{
							this._campaign.SetTimeSpeed(2);
						}
					}
					this._timeToggleTimer = float.MaxValue;
				}
				else if (this.SceneLayer.Input.IsGameKeyPressed(59))
				{
					this._waitForDoubleClickUntilTime = 0f;
					this._campaign.SetTimeSpeed(0);
				}
				else if (this.SceneLayer.Input.IsGameKeyPressed(60))
				{
					this._waitForDoubleClickUntilTime = 0f;
					this._campaign.SetTimeSpeed(1);
				}
				else if (this.SceneLayer.Input.IsGameKeyPressed(61))
				{
					this._waitForDoubleClickUntilTime = 0f;
					this._campaign.SetTimeSpeed(2);
				}
				else if (this.SceneLayer.Input.IsGameKeyPressed(64))
				{
					if (this._campaign.TimeControlMode == CampaignTimeControlMode.UnstoppableFastForward || this._campaign.TimeControlMode == CampaignTimeControlMode.StoppableFastForward)
					{
						this._campaign.SetTimeSpeed(0);
					}
					else
					{
						this._campaign.SetTimeSpeed(2);
					}
				}
			}
			IL_9AD:
			if (!flag && this.CurrentVisualOfTooltip != null)
			{
				this.CurrentVisualOfTooltip = null;
				this.RemoveMapTooltip();
			}
			this.SetCameraOfSceneLayer();
			if (!this.SceneLayer.Input.GetIsMouseActive() && Campaign.Current.GameStarted)
			{
				this._mapCursor.BeforeTick(dt);
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00018D48 File Offset: 0x00016F48
		void IMapStateHandler.Tick(float dt)
		{
			if (this._mapViewsCopyCache.Length != this._mapViews.Count || !this._mapViewsCopyCache.SequenceEqual(this._mapViews))
			{
				this._mapViewsCopyCache = new MapView[this._mapViews.Count];
				this._mapViews.CopyTo(this._mapViewsCopyCache, 0);
			}
			if (!this.IsInMenu)
			{
				if (this._isKingdomDecisionsDirty)
				{
					this.ShowNextKingdomDecisionPopup();
				}
				else
				{
					if (ViewModel.UIDebugMode && base.DebugInput.IsHotKeyDown("UIExtendedDebugKey") && base.DebugInput.IsHotKeyPressed("MapScreenHotkeyOpenEncyclopedia"))
					{
						this.OpenEncyclopedia();
					}
					bool cheatMode = Game.Current.CheatMode;
					if (cheatMode && base.DebugInput.IsHotKeyPressed("MapScreenHotkeySwitchCampaignTrueSight"))
					{
						this._campaign.TrueSight = !this._campaign.TrueSight;
					}
					if (cheatMode)
					{
						base.DebugInput.IsHotKeyPressed("MapScreenPrintMultiLineText");
					}
					for (int i = this._mapViewsCopyCache.Length - 1; i >= 0; i--)
					{
						if (!this._mapViewsCopyCache[i].IsFinalized)
						{
							this._mapViewsCopyCache[i].OnFrameTick(dt);
						}
					}
				}
			}
			this._conversationOverThisFrame = false;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00018E78 File Offset: 0x00017078
		void IMapStateHandler.OnIdleTick(float dt)
		{
			this.HandleIfSceneIsReady();
			this.RemoveMapTooltip();
			if (this._mapViewsCopyCache.Length != this._mapViews.Count || !this._mapViewsCopyCache.SequenceEqual(this._mapViews))
			{
				this._mapViewsCopyCache = new MapView[this._mapViews.Count];
				this._mapViews.CopyTo(this._mapViewsCopyCache, 0);
			}
			for (int i = this._mapViewsCopyCache.Length - 1; i >= 0; i--)
			{
				if (!this._mapViewsCopyCache[i].IsFinalized)
				{
					this._mapViewsCopyCache[i].OnIdleTick(dt);
				}
			}
			this._conversationOverThisFrame = false;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00018F1C File Offset: 0x0001711C
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			MBDebug.SetErrorReportScene(this._mapScene);
			this.UpdateMenuView();
			if (this.IsInMenu)
			{
				this._menuViewContext.OnFrameTick(dt);
				if (this.SceneLayer.Input.IsGameKeyPressed(4))
				{
					GameMenuOption leaveMenuOption = Campaign.Current.GameMenuManager.GetLeaveMenuOption(this._menuViewContext.MenuContext);
					if (leaveMenuOption != null)
					{
						UISoundsHelper.PlayUISound("event:/ui/default");
						if (this._menuViewContext.MenuContext.GameMenu.IsWaitMenu)
						{
							this._menuViewContext.MenuContext.GameMenu.EndWait();
						}
						leaveMenuOption.RunConsequence(this._menuViewContext.MenuContext);
					}
				}
			}
			else if (Campaign.Current != null && !this.IsInBattleSimulation && !this.IsInArmyManagement && !this.IsMarriageOfferPopupActive && !this.IsMapCheatsActive)
			{
				Kingdom kingdom = Clan.PlayerClan.Kingdom;
				bool flag;
				if (kingdom == null)
				{
					flag = (null != null);
				}
				else
				{
					MBReadOnlyList<KingdomDecision> unresolvedDecisions = kingdom.UnresolvedDecisions;
					if (unresolvedDecisions == null)
					{
						flag = (null != null);
					}
					else
					{
						flag = (unresolvedDecisions.FirstOrDefault((KingdomDecision d) => d.NeedsPlayerResolution && !d.ShouldBeCancelled()) != null);
					}
				}
				if (flag)
				{
					this.OpenKingdom();
				}
			}
			if (this._partyIconNeedsRefreshing)
			{
				this._partyIconNeedsRefreshing = false;
				PartyBase.MainParty.SetVisualAsDirty();
			}
			for (int i = this._mapViews.Count - 1; i >= 0; i--)
			{
				this._mapViews[i].OnMapScreenUpdate(dt);
			}
			this.RefreshMapSiegeOverlayRequired();
			if (PlayerSiege.PlayerSiegeEvent != null && this._playerSiegeMachineSlotMeshesAdded)
			{
				this.TickSiegeMachineCircles();
			}
			this._timeSinceCreation += dt;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x000190B4 File Offset: 0x000172B4
		private void UpdateMenuView()
		{
			if (this._latestMenuContext == null && this.IsInMenu)
			{
				this.ExitMenuContext();
				return;
			}
			if ((!this.IsInMenu && this._latestMenuContext != null) || (this.IsInMenu && this._menuViewContext.MenuContext != this._latestMenuContext))
			{
				this.EnterMenuContext(this._latestMenuContext);
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00019110 File Offset: 0x00017310
		private void EnterMenuContext(MenuContext menuContext)
		{
			this._mapCameraView.SetCameraMode(MapCameraView.CameraFollowMode.FollowParty);
			Campaign.Current.CameraFollowParty = PartyBase.MainParty;
			if (!this.IsInMenu)
			{
				this._menuViewContext = new MenuViewContext(this, menuContext);
			}
			else
			{
				this._menuViewContext.UpdateMenuContext(menuContext);
			}
			this._menuViewContext.OnInitialize();
			this._menuViewContext.OnActivate();
			if (this._mapConversationView != null)
			{
				this._menuViewContext.OnMapConversationActivated();
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00019184 File Offset: 0x00017384
		private void ExitMenuContext()
		{
			this._menuViewContext.OnGameStateDeactivate();
			this._menuViewContext.OnDeactivate();
			this._menuViewContext.OnFinalize();
			this._menuViewContext = null;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x000191AE File Offset: 0x000173AE
		private void OpenBannerEditorScreen()
		{
			if (Campaign.Current.IsBannerEditorEnabled)
			{
				this._partyIconNeedsRefreshing = true;
				Game.Current.GameStateManager.PushState(Game.Current.GameStateManager.CreateState<BannerEditorState>(), 0);
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x000191E4 File Offset: 0x000173E4
		private void OpenFaceGeneratorScreen()
		{
			if (Campaign.Current.IsFaceGenEnabled)
			{
				IFaceGeneratorCustomFilter faceGeneratorFilter = CharacterHelper.GetFaceGeneratorFilter();
				BarberState gameState = Game.Current.GameStateManager.CreateState<BarberState>(new object[]
				{
					Hero.MainHero.CharacterObject,
					faceGeneratorFilter
				});
				GameStateManager.Current.PushState(gameState, 0);
			}
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00019236 File Offset: 0x00017436
		public void OnExit()
		{
			this._mapCameraView.OnExit();
			MBGameManager.EndGame();
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00019248 File Offset: 0x00017448
		private void SetMapSiegeOverlayState(bool isActive)
		{
			this._mapCameraView.OnSetMapSiegeOverlayState(isActive, this._mapSiegeOverlayView == null);
			if (this._mapSiegeOverlayView != null && !isActive)
			{
				this.RemoveMapView(this._mapSiegeOverlayView);
				this._mapSiegeOverlayView = null;
				return;
			}
			if (this._mapSiegeOverlayView == null && isActive && PlayerSiege.PlayerSiegeEvent != null)
			{
				this._mapSiegeOverlayView = this.AddMapView<MapSiegeOverlayView>(Array.Empty<object>());
				if (!this._playerSiegeMachineSlotMeshesAdded)
				{
					this.InitializeSiegeCircleVisuals();
					this._playerSiegeMachineSlotMeshesAdded = true;
				}
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x000192C4 File Offset: 0x000174C4
		private void RefreshMapSiegeOverlayRequired()
		{
			this._mapCameraView.OnRefreshMapSiegeOverlayRequired(this._mapSiegeOverlayView == null);
			if (this._playerSiegeMachineSlotMeshesAdded && PlayerSiege.PlayerSiegeEvent != null)
			{
				Settlement besiegedSettlement = PlayerSiege.PlayerSiegeEvent.BesiegedSettlement;
				if (besiegedSettlement != null && besiegedSettlement.CurrentSiegeState == Settlement.SiegeState.InTheLordsHall)
				{
					this.RemoveSiegeCircleVisuals();
					this._playerSiegeMachineSlotMeshesAdded = false;
					return;
				}
			}
			if (PlayerSiege.PlayerSiegeEvent == null && this._mapSiegeOverlayView != null)
			{
				this.RemoveMapView(this._mapSiegeOverlayView);
				this._mapSiegeOverlayView = null;
				if (this._playerSiegeMachineSlotMeshesAdded)
				{
					this.RemoveSiegeCircleVisuals();
					this._playerSiegeMachineSlotMeshesAdded = false;
					return;
				}
			}
			else if (PlayerSiege.PlayerSiegeEvent != null && this._mapSiegeOverlayView == null)
			{
				this._mapSiegeOverlayView = this.AddMapView<MapSiegeOverlayView>(Array.Empty<object>());
				if (!this._playerSiegeMachineSlotMeshesAdded)
				{
					this.InitializeSiegeCircleVisuals();
					this._playerSiegeMachineSlotMeshesAdded = true;
				}
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0001938C File Offset: 0x0001758C
		private void OnEscapeMenuToggled(bool isOpened = false)
		{
			this._mapCameraView.OnEscapeMenuToggled(isOpened);
			if (this.IsEscapeMenuOpened == isOpened)
			{
				return;
			}
			this.IsEscapeMenuOpened = isOpened;
			if (isOpened)
			{
				List<EscapeMenuItemVM> escapeMenuItems = this.GetEscapeMenuItems();
				Game.Current.GameStateManager.RegisterActiveStateDisableRequest(this);
				this._escapeMenuView = this.AddMapView<MapEscapeMenuView>(new object[]
				{
					escapeMenuItems
				});
				return;
			}
			this.RemoveMapView(this._escapeMenuView);
			this._escapeMenuView = null;
			Game.Current.GameStateManager.UnregisterActiveStateDisableRequest(this);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0001940C File Offset: 0x0001760C
		private void CheckValidityOfItems()
		{
			foreach (ItemObject itemObject in MBObjectManager.Instance.GetObjectTypeList<ItemObject>())
			{
				if (itemObject.IsUsingTeamColor)
				{
					MetaMesh copy = MetaMesh.GetCopy(itemObject.MultiMeshName, false, false);
					for (int i = 0; i < copy.MeshCount; i++)
					{
						Material material = copy.GetMeshAtIndex(i).GetMaterial();
						if (material.Name != "vertex_color_lighting_skinned" && material.Name != "vertex_color_lighting" && material.GetTexture(Material.MBTextureType.DiffuseMap2) == null)
						{
							MBDebug.ShowWarning("Item object(" + itemObject.Name + ") has 'Using Team Color' flag but does not have a mask texture in diffuse2 slot. ");
							break;
						}
					}
				}
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x000194EC File Offset: 0x000176EC
		public void GetCursorIntersectionPoint(ref Vec3 clippedMouseNear, ref Vec3 clippedMouseFar, out float closestDistanceSquared, out Vec3 intersectionPoint, ref PathFaceRecord currentFace, BodyFlags excludedBodyFlags = BodyFlags.CommonFocusRayCastExcludeFlags)
		{
			(clippedMouseFar - clippedMouseNear).Normalize();
			Vec3 vec = clippedMouseFar - clippedMouseNear;
			float maxDistance = vec.Normalize();
			this._mouseRay.Reset(clippedMouseNear, vec, maxDistance);
			intersectionPoint = Vec3.Zero;
			closestDistanceSquared = 1E+12f;
			float num;
			Vec3 vec2;
			if (this.SceneLayer.SceneView.RayCastForClosestEntityOrTerrain(clippedMouseNear, clippedMouseFar, out num, out vec2, 0.01f, excludedBodyFlags))
			{
				closestDistanceSquared = num * num;
				intersectionPoint = clippedMouseNear + vec * num;
			}
			currentFace = Campaign.Current.MapSceneWrapper.GetFaceIndex(intersectionPoint.AsVec2);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x000195B7 File Offset: 0x000177B7
		public void FastMoveCameraToPosition(Vec2 target)
		{
			this._mapCameraView.FastMoveCameraToPosition(target, this.IsInMenu);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x000195CC File Offset: 0x000177CC
		private void HandleMouse(float dt)
		{
			if (Campaign.Current.GameStarted)
			{
				Track track = null;
				Vec3 zero = Vec3.Zero;
				Vec3 zero2 = Vec3.Zero;
				this.SceneLayer.SceneView.TranslateMouse(ref zero, ref zero2, -1f);
				Vec3 vec = zero;
				Vec3 vec2 = zero2;
				PathFaceRecord nullFaceRecord = PathFaceRecord.NullFaceRecord;
				this.CheckCursorState();
				float x;
				Vec3 vec3;
				this.GetCursorIntersectionPoint(ref vec, ref vec2, out x, out vec3, ref nullFaceRecord, BodyFlags.CommonFocusRayCastExcludeFlags);
				float num;
				Vec3 vec4;
				this.GetCursorIntersectionPoint(ref vec, ref vec2, out num, out vec4, ref nullFaceRecord, BodyFlags.Disabled | BodyFlags.Moveable | BodyFlags.AILimiter | BodyFlags.Barrier | BodyFlags.Barrier3D | BodyFlags.Ragdoll | BodyFlags.RagdollLimiter | BodyFlags.DoNotCollideWithRaycast);
				int num2 = this._mapScene.SelectEntitiesCollidedWith(ref this._mouseRay, this._intersectionInfos, this._intersectedEntityIDs);
				bool flag = false;
				float num3 = MathF.Sqrt(x) + 1f;
				float num4 = num3;
				PartyVisual partyVisual = null;
				PartyVisual partyVisual2 = null;
				bool flag2 = false;
				for (int i = num2 - 1; i >= 0; i--)
				{
					UIntPtr uintPtr = this._intersectedEntityIDs[i];
					if (uintPtr != UIntPtr.Zero)
					{
						PartyVisual partyVisual3;
						if (MapScreen.VisualsOfEntities.TryGetValue(uintPtr, out partyVisual3) && partyVisual3.IsVisibleOrFadingOut())
						{
							PartyVisual partyVisual4 = partyVisual3;
							Intersection intersection = this._intersectionInfos[i];
							vec3 = zero - intersection.IntersectionPoint;
							float num5 = vec3.Length;
							if (partyVisual4.PartyBase.IsMobile)
							{
								num5 -= 1.5f;
							}
							if (num5 < num4)
							{
								num4 = num5;
								if (!partyVisual4.PartyBase.IsMobile || partyVisual4.PartyBase.MobileParty.AttachedTo == null)
								{
									partyVisual = partyVisual3;
								}
								else
								{
									partyVisual = PartyVisualManager.Current.GetVisualOfParty(partyVisual4.PartyBase.MobileParty.AttachedTo.Party);
								}
								flag = true;
							}
							if (num5 < num3 && (!partyVisual4.PartyBase.IsMobile || (partyVisual4.PartyBase != PartyBase.MainParty && (partyVisual4.PartyBase.MobileParty.AttachedTo == null || partyVisual4.PartyBase.MobileParty.AttachedTo != MobileParty.MainParty))))
							{
								num3 = num5;
								if (partyVisual4.PartyBase.IsMobile && partyVisual4.PartyBase.MobileParty.AttachedTo != null)
								{
									partyVisual2 = PartyVisualManager.Current.GetVisualOfParty(partyVisual4.PartyBase.MobileParty.AttachedTo.Party);
								}
								else
								{
									partyVisual2 = partyVisual4;
								}
							}
						}
						else if (ScreenManager.FirstHitLayer == this.SceneLayer && MapScreen.FrameAndVisualOfEngines.ContainsKey(uintPtr))
						{
							flag2 = true;
							if (this._preSelectedSiegeEntityID != uintPtr)
							{
								Tuple<MatrixFrame, PartyVisual> tuple = MapScreen.FrameAndVisualOfEngines[uintPtr];
								tuple.Item2.OnMapHoverSiegeEngine(tuple.Item1);
								this._preSelectedSiegeEntityID = uintPtr;
							}
						}
					}
				}
				if (!flag2)
				{
					this.HandleSiegeEngineHoverEnd();
				}
				Array.Clear(this._intersectedEntityIDs, 0, num2);
				Array.Clear(this._intersectionInfos, 0, num2);
				if (flag)
				{
					if (this._displayedContextMenuType < 0)
					{
						this.SceneLayer.ActiveCursor = CursorType.Default;
					}
				}
				else
				{
					track = this._campaign.GetEntityComponent<MapTracksVisual>().GetTrackOnMouse(this._mouseRay, vec4);
				}
				float gameKeyAxis = this.SceneLayer.Input.GetGameKeyAxis("CameraAxisY");
				this._mapCameraView.HandleMouse(this.SceneLayer.Input.IsKeyDown(InputKey.RightMouseButton), gameKeyAxis, this.SceneLayer.Input.GetMouseMoveY(), dt);
				if (this.SceneLayer.Input.IsKeyDown(InputKey.RightMouseButton))
				{
					MBWindowManager.DontChangeCursorPos();
				}
				if (ScreenManager.FirstHitLayer == this.SceneLayer && this.SceneLayer.Input.IsHotKeyReleased("MapClick") && !this._leftButtonDraggingMode && !this._ignoreLeftMouseRelease)
				{
					if (this._leftButtonDoubleClickOnSceneWidget)
					{
						this.HandleLeftMouseButtonClick(this._preSelectedSiegeEntityID, this._preVisualOfSelectedEntity, vec4, nullFaceRecord);
					}
					else
					{
						this.HandleLeftMouseButtonClick(this._preSelectedSiegeEntityID, partyVisual2, vec4, nullFaceRecord);
						this._preVisualOfSelectedEntity = partyVisual2;
					}
				}
				if (Campaign.Current.TimeControlMode == CampaignTimeControlMode.StoppableFastForward && this._waitForDoubleClickUntilTime > 0f && this._waitForDoubleClickUntilTime < Time.ApplicationTime)
				{
					Campaign.Current.TimeControlMode = CampaignTimeControlMode.StoppablePlay;
					this._waitForDoubleClickUntilTime = 0f;
				}
				if (ScreenManager.FirstHitLayer == this.SceneLayer)
				{
					if (partyVisual != null)
					{
						if (this.CurrentVisualOfTooltip != partyVisual)
						{
							this.RemoveMapTooltip();
						}
						IMapEntity mapEntity = partyVisual.GetMapEntity();
						if (this.SceneLayer.Input.IsGameKeyPressed(66))
						{
							mapEntity.OnOpenEncyclopedia();
							this._mapCursor.SetVisible(false);
						}
						ITrackableCampaignObject obj;
						if ((obj = (mapEntity as ITrackableCampaignObject)) != null && this.SceneLayer.Input.IsGameKeyPressed(65))
						{
							if (Campaign.Current.VisualTrackerManager.CheckTracked(obj))
							{
								Campaign.Current.VisualTrackerManager.RemoveTrackedObject(obj, false);
							}
							else
							{
								Campaign.Current.VisualTrackerManager.RegisterObject(obj);
							}
						}
						this.OnHoverMapEntity(mapEntity);
						this.CurrentVisualOfTooltip = partyVisual;
						return;
					}
					if (track != null)
					{
						this.CurrentVisualOfTooltip = null;
						this.SetupMapTooltipForTrack(track);
						return;
					}
					if (!this.TooltipHandlingDisabled)
					{
						this.CurrentVisualOfTooltip = null;
						this.RemoveMapTooltip();
						return;
					}
				}
				else
				{
					this.CurrentVisualOfTooltip = null;
					this.RemoveMapTooltip();
					this.HandleSiegeEngineHoverEnd();
				}
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00019AD8 File Offset: 0x00017CD8
		private void HandleLeftMouseButtonClick(UIntPtr selectedSiegeEntityID, PartyVisual visualOfSelectedEntity, Vec3 intersectionPoint, PathFaceRecord mouseOverFaceIndex)
		{
			this._mapCameraView.HandleLeftMouseButtonClick(this.SceneLayer.Input.GetIsMouseActive());
			if (!this._mapState.AtMenu)
			{
				if (((visualOfSelectedEntity != null) ? visualOfSelectedEntity.GetMapEntity() : null) != null)
				{
					IMapEntity mapEntity = visualOfSelectedEntity.GetMapEntity();
					if (visualOfSelectedEntity.PartyBase == PartyBase.MainParty)
					{
						MobileParty.MainParty.Ai.SetMoveModeHold();
						return;
					}
					PathFaceRecord faceIndex = Campaign.Current.MapSceneWrapper.GetFaceIndex(mapEntity.InteractionPosition);
					if (this._mapScene.DoesPathExistBetweenFaces(faceIndex.FaceIndex, MobileParty.MainParty.CurrentNavigationFace.FaceIndex, false) && this._mapCameraView.ProcessCameraInput && PartyBase.MainParty.MapEvent == null)
					{
						if (mapEntity.OnMapClick(this.SceneLayer.Input.IsHotKeyDown("MapFollowModifier")))
						{
							if (!this._leftButtonDoubleClickOnSceneWidget && Campaign.Current.TimeControlMode == CampaignTimeControlMode.StoppableFastForward)
							{
								this._waitForDoubleClickUntilTime = Time.ApplicationTime + 0.3f;
								Campaign.Current.TimeControlMode = CampaignTimeControlMode.StoppableFastForward;
							}
							else
							{
								Campaign.Current.TimeControlMode = (this._leftButtonDoubleClickOnSceneWidget ? CampaignTimeControlMode.StoppableFastForward : CampaignTimeControlMode.StoppablePlay);
							}
							if (TaleWorlds.InputSystem.Input.IsGamepadActive)
							{
								if (mapEntity.IsMobileEntity)
								{
									if (mapEntity.IsAllyOf(PartyBase.MainParty.MapFaction))
									{
										UISoundsHelper.PlayUISound("event:/ui/campaign/click_party");
									}
									else
									{
										UISoundsHelper.PlayUISound("event:/ui/campaign/click_party_enemy");
									}
								}
								else if (mapEntity.IsAllyOf(PartyBase.MainParty.MapFaction))
								{
									UISoundsHelper.PlayUISound("event:/ui/campaign/click_settlement");
								}
								else
								{
									UISoundsHelper.PlayUISound("event:/ui/campaign/click_settlement_enemy");
								}
							}
						}
						MobileParty.MainParty.Ai.ForceAiNoPathMode = false;
						return;
					}
				}
				else if (mouseOverFaceIndex.IsValid())
				{
					bool flag;
					if (this.Input.IsControlDown() && Game.Current.CheatMode)
					{
						if (MobileParty.MainParty.Army != null)
						{
							foreach (MobileParty mobileParty in MobileParty.MainParty.Army.LeaderParty.AttachedParties)
							{
								mobileParty.Position2D += intersectionPoint.AsVec2 - MobileParty.MainParty.Position2D;
							}
						}
						MobileParty.MainParty.Position2D = intersectionPoint.AsVec2;
						MobileParty.MainParty.Ai.SetMoveModeHold();
						foreach (MobileParty mobileParty2 in MobileParty.All)
						{
							mobileParty2.Party.UpdateVisibilityAndInspected(0f);
						}
						foreach (Settlement settlement in Settlement.All)
						{
							settlement.Party.UpdateVisibilityAndInspected(0f);
						}
						MBDebug.Print(string.Concat(new object[]
						{
							"main party cheat move! - ",
							intersectionPoint.x,
							" ",
							intersectionPoint.y
						}), 0, Debug.DebugColor.White, 17592186044416UL);
						flag = true;
					}
					else
					{
						flag = Campaign.Current.MapSceneWrapper.AreFacesOnSameIsland(mouseOverFaceIndex, MobileParty.MainParty.CurrentNavigationFace, false);
					}
					if (flag && this._mapCameraView.ProcessCameraInput && MobileParty.MainParty.MapEvent == null)
					{
						this._mapState.ProcessTravel(intersectionPoint.AsVec2);
						if (!this._leftButtonDoubleClickOnSceneWidget && Campaign.Current.TimeControlMode == CampaignTimeControlMode.StoppableFastForward)
						{
							this._waitForDoubleClickUntilTime = Time.ApplicationTime + 0.3f;
							Campaign.Current.TimeControlMode = CampaignTimeControlMode.StoppableFastForward;
						}
						else
						{
							Campaign.Current.TimeControlMode = (this._leftButtonDoubleClickOnSceneWidget ? CampaignTimeControlMode.StoppableFastForward : CampaignTimeControlMode.StoppablePlay);
						}
					}
					this.OnTerrainClick();
					return;
				}
			}
			else
			{
				if (selectedSiegeEntityID != UIntPtr.Zero)
				{
					Tuple<MatrixFrame, PartyVisual> tuple = MapScreen.FrameAndVisualOfEngines[selectedSiegeEntityID];
					this.OnSiegeEngineFrameClick(tuple.Item1);
					return;
				}
				this.OnTerrainClick();
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00019EEC File Offset: 0x000180EC
		private void OnTerrainClick()
		{
			foreach (MapView mapView in this._mapViews)
			{
				mapView.OnMapTerrainClick();
			}
			this._mapCursor.OnMapTerrainClick();
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00019F44 File Offset: 0x00018144
		private void OnSiegeEngineFrameClick(MatrixFrame siegeFrame)
		{
			foreach (MapView mapView in this._mapViews)
			{
				mapView.OnSiegeEngineClick(siegeFrame);
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00019F90 File Offset: 0x00018190
		private void InitializeSiegeCircleVisuals()
		{
			Settlement besiegedSettlement = PlayerSiege.PlayerSiegeEvent.BesiegedSettlement;
			PartyVisual visualOfParty = PartyVisualManager.Current.GetVisualOfParty(besiegedSettlement.Party);
			MapScene mapScene = Campaign.Current.MapSceneWrapper as MapScene;
			MatrixFrame[] array = visualOfParty.GetDefenderRangedSiegeEngineFrames();
			this._defenderMachinesCircleEntities = new GameEntity[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				MatrixFrame matrixFrame = array[i];
				this._defenderMachinesCircleEntities[i] = GameEntity.CreateEmpty(mapScene.Scene, true);
				this._defenderMachinesCircleEntities[i].Name = "dRangedMachineCircle_" + i;
				Decal decal = Decal.CreateDecal(null);
				decal.SetMaterial(Material.GetFromResource(this._defenderRangedMachineDecalMaterialName));
				decal.SetFactor1Linear(this._preperationOrEnemySiegeEngineDecalColor);
				this._defenderMachinesCircleEntities[i].AddComponent(decal);
				MatrixFrame matrixFrame2 = matrixFrame;
				if (this._isNewDecalScaleImplementationEnabled)
				{
					matrixFrame2.Scale(new Vec3(this._defenderMachineCircleDecalScale, this._defenderMachineCircleDecalScale, this._defenderMachineCircleDecalScale, -1f));
				}
				this._defenderMachinesCircleEntities[i].SetGlobalFrame(matrixFrame2);
				this._defenderMachinesCircleEntities[i].SetVisibilityExcludeParents(true);
				mapScene.Scene.AddDecalInstance(decal, "editor_set", true);
			}
			array = visualOfParty.GetAttackerBatteringRamSiegeEngineFrames();
			this._attackerRamMachinesCircleEntities = new GameEntity[array.Length];
			for (int j = 0; j < array.Length; j++)
			{
				MatrixFrame matrixFrame3 = array[j];
				this._attackerRamMachinesCircleEntities[j] = GameEntity.CreateEmpty(mapScene.Scene, true);
				this._attackerRamMachinesCircleEntities[j].Name = "InitializeSiegeCircleVisuals";
				this._attackerRamMachinesCircleEntities[j].Name = "aRamMachineCircle_" + j;
				Decal decal2 = Decal.CreateDecal(null);
				decal2.SetMaterial(Material.GetFromResource(this._attackerRamMachineDecalMaterialName));
				decal2.SetFactor1Linear(this._preperationOrEnemySiegeEngineDecalColor);
				this._attackerRamMachinesCircleEntities[j].AddComponent(decal2);
				MatrixFrame matrixFrame4 = matrixFrame3;
				if (this._isNewDecalScaleImplementationEnabled)
				{
					matrixFrame4.Scale(new Vec3(this._attackerMachineDecalScale, this._attackerMachineDecalScale, this._attackerMachineDecalScale, -1f));
				}
				this._attackerRamMachinesCircleEntities[j].SetGlobalFrame(matrixFrame4);
				this._attackerRamMachinesCircleEntities[j].SetVisibilityExcludeParents(true);
				mapScene.Scene.AddDecalInstance(decal2, "editor_set", true);
			}
			array = visualOfParty.GetAttackerTowerSiegeEngineFrames();
			this._attackerTowerMachinesCircleEntities = new GameEntity[array.Length];
			for (int k = 0; k < array.Length; k++)
			{
				MatrixFrame matrixFrame5 = array[k];
				this._attackerTowerMachinesCircleEntities[k] = GameEntity.CreateEmpty(mapScene.Scene, true);
				this._attackerTowerMachinesCircleEntities[k].Name = "aTowerMachineCircle_" + k;
				Decal decal3 = Decal.CreateDecal(null);
				decal3.SetMaterial(Material.GetFromResource(this._attackerTowerMachineDecalMaterialName));
				decal3.SetFactor1Linear(this._preperationOrEnemySiegeEngineDecalColor);
				this._attackerTowerMachinesCircleEntities[k].AddComponent(decal3);
				MatrixFrame matrixFrame6 = matrixFrame5;
				if (this._isNewDecalScaleImplementationEnabled)
				{
					matrixFrame6.Scale(new Vec3(this._attackerMachineDecalScale, this._attackerMachineDecalScale, this._attackerMachineDecalScale, -1f));
				}
				this._attackerTowerMachinesCircleEntities[k].SetGlobalFrame(matrixFrame6);
				this._attackerTowerMachinesCircleEntities[k].SetVisibilityExcludeParents(true);
				mapScene.Scene.AddDecalInstance(decal3, "editor_set", true);
			}
			array = visualOfParty.GetAttackerRangedSiegeEngineFrames();
			this._attackerRangedMachinesCircleEntities = new GameEntity[array.Length];
			for (int l = 0; l < array.Length; l++)
			{
				MatrixFrame matrixFrame7 = array[l];
				this._attackerRangedMachinesCircleEntities[l] = GameEntity.CreateEmpty(mapScene.Scene, true);
				this._attackerRangedMachinesCircleEntities[l].Name = "aRangedMachineCircle_" + l;
				Decal decal4 = Decal.CreateDecal(null);
				decal4.SetMaterial(Material.GetFromResource(this._emptyAttackerRangedDecalMaterialName));
				decal4.SetFactor1Linear(this._preperationOrEnemySiegeEngineDecalColor);
				this._attackerRangedMachinesCircleEntities[l].AddComponent(decal4);
				MatrixFrame matrixFrame8 = matrixFrame7;
				if (this._isNewDecalScaleImplementationEnabled)
				{
					matrixFrame8.Scale(new Vec3(this._attackerMachineDecalScale, this._attackerMachineDecalScale, this._attackerMachineDecalScale, -1f));
				}
				this._attackerRangedMachinesCircleEntities[l].SetGlobalFrame(matrixFrame8);
				this._attackerRangedMachinesCircleEntities[l].SetVisibilityExcludeParents(true);
				mapScene.Scene.AddDecalInstance(decal4, "editor_set", true);
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0001A3D8 File Offset: 0x000185D8
		private void TickSiegeMachineCircles()
		{
			SiegeEvent playerSiegeEvent = PlayerSiege.PlayerSiegeEvent;
			bool isPlayerLeader = playerSiegeEvent != null && playerSiegeEvent.IsPlayerSiegeEvent && Campaign.Current.Models.EncounterModel.GetLeaderOfSiegeEvent(playerSiegeEvent, PlayerSiege.PlayerSide) == Hero.MainHero;
			bool isPreparationComplete = playerSiegeEvent.BesiegerCamp.IsPreparationComplete;
			Settlement besiegedSettlement = playerSiegeEvent.BesiegedSettlement;
			PartyVisual visualOfParty = PartyVisualManager.Current.GetVisualOfParty(besiegedSettlement.Party);
			Tuple<MatrixFrame, PartyVisual> tuple = null;
			if (this._preSelectedSiegeEntityID != UIntPtr.Zero)
			{
				tuple = MapScreen.FrameAndVisualOfEngines[this._preSelectedSiegeEntityID];
			}
			for (int i = 0; i < visualOfParty.GetDefenderRangedSiegeEngineFrames().Length; i++)
			{
				bool isEmpty = playerSiegeEvent.GetSiegeEventSide(BattleSideEnum.Defender).SiegeEngines.DeployedRangedSiegeEngines[i] == null;
				bool isEnemy = PlayerSiege.PlayerSide > BattleSideEnum.Defender;
				string desiredMaterialName = this.GetDesiredMaterialName(true, false, isEmpty, false);
				Decal decal = this._defenderMachinesCircleEntities[i].GetComponentAtIndex(0, GameEntity.ComponentType.Decal) as Decal;
				Material material = decal.GetMaterial();
				if (((material != null) ? material.Name : null) != desiredMaterialName)
				{
					decal.SetMaterial(Material.GetFromResource(desiredMaterialName));
				}
				bool isHovered = tuple != null && this._defenderMachinesCircleEntities[i].GetGlobalFrame().NearlyEquals(tuple.Item1, 1E-05f);
				uint desiredDecalColor = this.GetDesiredDecalColor(isPreparationComplete, isHovered, isEnemy, isEmpty, isPlayerLeader);
				if (desiredDecalColor != decal.GetFactor1())
				{
					decal.SetFactor1(desiredDecalColor);
				}
			}
			for (int j = 0; j < visualOfParty.GetAttackerRangedSiegeEngineFrames().Length; j++)
			{
				bool isEmpty2 = playerSiegeEvent.GetSiegeEventSide(BattleSideEnum.Attacker).SiegeEngines.DeployedRangedSiegeEngines[j] == null;
				bool isEnemy2 = PlayerSiege.PlayerSide != BattleSideEnum.Attacker;
				string desiredMaterialName2 = this.GetDesiredMaterialName(true, true, isEmpty2, false);
				Decal decal2 = this._attackerRangedMachinesCircleEntities[j].GetComponentAtIndex(0, GameEntity.ComponentType.Decal) as Decal;
				Material material2 = decal2.GetMaterial();
				if (((material2 != null) ? material2.Name : null) != desiredMaterialName2)
				{
					decal2.SetMaterial(Material.GetFromResource(desiredMaterialName2));
				}
				bool isHovered2 = tuple != null && this._attackerRangedMachinesCircleEntities[j].GetGlobalFrame().NearlyEquals(tuple.Item1, 1E-05f);
				uint desiredDecalColor2 = this.GetDesiredDecalColor(isPreparationComplete, isHovered2, isEnemy2, isEmpty2, isPlayerLeader);
				if (desiredDecalColor2 != decal2.GetFactor1())
				{
					decal2.SetFactor1(desiredDecalColor2);
				}
			}
			for (int k = 0; k < visualOfParty.GetAttackerBatteringRamSiegeEngineFrames().Length; k++)
			{
				bool isEmpty3 = playerSiegeEvent.GetSiegeEventSide(BattleSideEnum.Attacker).SiegeEngines.DeployedMeleeSiegeEngines[k] == null;
				bool isEnemy3 = PlayerSiege.PlayerSide != BattleSideEnum.Attacker;
				string desiredMaterialName3 = this.GetDesiredMaterialName(false, true, isEmpty3, false);
				Decal decal3 = this._attackerRamMachinesCircleEntities[k].GetComponentAtIndex(0, GameEntity.ComponentType.Decal) as Decal;
				Material material3 = decal3.GetMaterial();
				if (((material3 != null) ? material3.Name : null) != desiredMaterialName3)
				{
					decal3.SetMaterial(Material.GetFromResource(desiredMaterialName3));
				}
				bool isHovered3 = tuple != null && this._attackerRamMachinesCircleEntities[k].GetGlobalFrame().NearlyEquals(tuple.Item1, 1E-05f);
				uint desiredDecalColor3 = this.GetDesiredDecalColor(isPreparationComplete, isHovered3, isEnemy3, isEmpty3, isPlayerLeader);
				if (desiredDecalColor3 != decal3.GetFactor1())
				{
					decal3.SetFactor1(desiredDecalColor3);
				}
			}
			for (int l = 0; l < visualOfParty.GetAttackerTowerSiegeEngineFrames().Length; l++)
			{
				bool isEmpty4 = playerSiegeEvent.GetSiegeEventSide(BattleSideEnum.Attacker).SiegeEngines.DeployedMeleeSiegeEngines[visualOfParty.GetAttackerBatteringRamSiegeEngineFrames().Length + l] == null;
				bool isEnemy4 = PlayerSiege.PlayerSide != BattleSideEnum.Attacker;
				string desiredMaterialName4 = this.GetDesiredMaterialName(false, true, isEmpty4, true);
				Decal decal4 = this._attackerTowerMachinesCircleEntities[l].GetComponentAtIndex(0, GameEntity.ComponentType.Decal) as Decal;
				Material material4 = decal4.GetMaterial();
				if (((material4 != null) ? material4.Name : null) != desiredMaterialName4)
				{
					decal4.SetMaterial(Material.GetFromResource(desiredMaterialName4));
				}
				bool isHovered4 = tuple != null && this._attackerTowerMachinesCircleEntities[l].GetGlobalFrame().NearlyEquals(tuple.Item1, 1E-05f);
				uint desiredDecalColor4 = this.GetDesiredDecalColor(isPreparationComplete, isHovered4, isEnemy4, isEmpty4, isPlayerLeader);
				if (desiredDecalColor4 != decal4.GetFactor1())
				{
					decal4.SetFactor1(desiredDecalColor4);
				}
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0001A7F8 File Offset: 0x000189F8
		private uint GetDesiredDecalColor(bool isPrepOver, bool isHovered, bool isEnemy, bool isEmpty, bool isPlayerLeader)
		{
			isPrepOver = true;
			if (!isPrepOver || isEnemy)
			{
				return this._preperationOrEnemySiegeEngineDecalColor;
			}
			if (isHovered && isPlayerLeader)
			{
				return this._hoveredSiegeEngineDecalColor;
			}
			if (!isEmpty)
			{
				return this._withMachineSiegeEngineDecalColor;
			}
			if (isPlayerLeader)
			{
				float ratio = MathF.PingPong(0f, this._machineDecalAnimLoopTime, this._timeSinceCreation) / this._machineDecalAnimLoopTime;
				Color start = Color.FromUint(this._normalStartSiegeEngineDecalColor);
				Color end = Color.FromUint(this._normalEndSiegeEngineDecalColor);
				return Color.Lerp(start, end, ratio).ToUnsignedInteger();
			}
			return this._normalStartSiegeEngineDecalColor;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0001A87C File Offset: 0x00018A7C
		private string GetDesiredMaterialName(bool isRanged, bool isAttacker, bool isEmpty, bool isTower)
		{
			if (isRanged)
			{
				if (!isAttacker)
				{
					return this._defenderRangedMachineDecalMaterialName;
				}
				return this._attackerRangedMachineDecalMaterialName;
			}
			else
			{
				if (!isTower)
				{
					return this._attackerRamMachineDecalMaterialName;
				}
				return this._attackerTowerMachineDecalMaterialName;
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0001A8A4 File Offset: 0x00018AA4
		private void RemoveSiegeCircleVisuals()
		{
			if (this._playerSiegeMachineSlotMeshesAdded)
			{
				MapScene mapScene = Campaign.Current.MapSceneWrapper as MapScene;
				for (int i = 0; i < this._defenderMachinesCircleEntities.Length; i++)
				{
					this._defenderMachinesCircleEntities[i].SetVisibilityExcludeParents(false);
					mapScene.Scene.RemoveEntity(this._defenderMachinesCircleEntities[i], 107);
					this._defenderMachinesCircleEntities[i] = null;
				}
				for (int j = 0; j < this._attackerRamMachinesCircleEntities.Length; j++)
				{
					this._attackerRamMachinesCircleEntities[j].SetVisibilityExcludeParents(false);
					mapScene.Scene.RemoveEntity(this._attackerRamMachinesCircleEntities[j], 108);
					this._attackerRamMachinesCircleEntities[j] = null;
				}
				for (int k = 0; k < this._attackerTowerMachinesCircleEntities.Length; k++)
				{
					this._attackerTowerMachinesCircleEntities[k].SetVisibilityExcludeParents(false);
					mapScene.Scene.RemoveEntity(this._attackerTowerMachinesCircleEntities[k], 109);
					this._attackerTowerMachinesCircleEntities[k] = null;
				}
				for (int l = 0; l < this._attackerRangedMachinesCircleEntities.Length; l++)
				{
					this._attackerRangedMachinesCircleEntities[l].SetVisibilityExcludeParents(false);
					mapScene.Scene.RemoveEntity(this._attackerRangedMachinesCircleEntities[l], 110);
					this._attackerRangedMachinesCircleEntities[l] = null;
				}
				this._playerSiegeMachineSlotMeshesAdded = false;
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0001A9D8 File Offset: 0x00018BD8
		void IMapStateHandler.AfterTick(float dt)
		{
			if (ScreenManager.TopScreen == this)
			{
				this.TickVisuals(dt);
				SceneLayer sceneLayer = this.SceneLayer;
				if (sceneLayer != null && sceneLayer.Input.IsGameKeyPressed(53))
				{
					Campaign.Current.SaveHandler.QuickSaveCurrentGame();
				}
			}
			base.DebugInput.IsHotKeyPressed("MapScreenHotkeyShowPos");
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0001AA30 File Offset: 0x00018C30
		void IMapStateHandler.AfterWaitTick(float dt)
		{
			if (!this.SceneLayer.Input.IsShiftDown() && !this.SceneLayer.Input.IsControlDown())
			{
				bool flag = false;
				if (this.SceneLayer.Input.IsGameKeyPressed(38) && this._navigationHandler.InventoryEnabled)
				{
					this.OpenInventory();
					flag = true;
				}
				else if (this.SceneLayer.Input.IsGameKeyPressed(43) && this._navigationHandler.PartyEnabled)
				{
					this.OpenParty();
					flag = true;
				}
				else if (this.SceneLayer.Input.IsGameKeyPressed(39) && !this.IsInArmyManagement && !this.IsMapCheatsActive)
				{
					this.OpenEncyclopedia();
					flag = true;
				}
				else if (this.SceneLayer.Input.IsGameKeyPressed(36) && !this.IsInArmyManagement && !this.IsMarriageOfferPopupActive && !this.IsMapCheatsActive)
				{
					this.OpenBannerEditorScreen();
					flag = true;
				}
				else if (this.SceneLayer.Input.IsGameKeyPressed(40) && this._navigationHandler.KingdomPermission.IsAuthorized)
				{
					this.OpenKingdom();
					flag = true;
				}
				else if (this.SceneLayer.Input.IsGameKeyPressed(42) && this._navigationHandler.QuestsEnabled)
				{
					this.OpenQuestsScreen();
					flag = true;
				}
				else if (this.SceneLayer.Input.IsGameKeyPressed(41) && this._navigationHandler.ClanPermission.IsAuthorized)
				{
					this.OpenClanScreen();
					flag = true;
				}
				else if (this.SceneLayer.Input.IsGameKeyPressed(37) && this._navigationHandler.CharacterDeveloperEnabled)
				{
					this.OpenCharacterDevelopmentScreen();
					flag = true;
				}
				else if (this.SceneLayer.Input.IsHotKeyReleased("ToggleEscapeMenu"))
				{
					if (!this._mapViews.Any((MapView m) => m.IsEscaped()))
					{
						this.OpenEscapeMenu();
						flag = true;
					}
				}
				else if (this.SceneLayer.Input.IsGameKeyPressed(44))
				{
					this.OpenFaceGeneratorScreen();
					flag = true;
				}
				else if (TaleWorlds.InputSystem.Input.IsGamepadActive)
				{
					this.HandleCheatMenuInput(dt);
				}
				if (flag)
				{
					this._mapCursor.SetVisible(false);
				}
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0001AC7C File Offset: 0x00018E7C
		private void HandleCheatMenuInput(float dt)
		{
			if (!this.IsMapCheatsActive && this.Input.IsKeyDown(InputKey.ControllerLBumper) && this.Input.IsKeyDown(InputKey.ControllerRTrigger) && this.Input.IsKeyDown(InputKey.ControllerLDown))
			{
				this._cheatPressTimer += dt;
				if (this._cheatPressTimer > 0.55f)
				{
					this.OpenGameplayCheats();
				}
				return;
			}
			this._cheatPressTimer = 0f;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0001ACF4 File Offset: 0x00018EF4
		void IMapStateHandler.OnRefreshState()
		{
			if (Game.Current.GameStateManager.ActiveState is MapState)
			{
				if (MobileParty.MainParty.Army != null && this._armyOverlay == null)
				{
					this.AddArmyOverlay(GameOverlays.MapOverlayType.Army);
					return;
				}
				if (MobileParty.MainParty.Army == null && this._armyOverlay != null)
				{
					for (int i = this._mapViews.Count - 1; i >= 0; i--)
					{
						this._mapViews[i].OnArmyLeft();
					}
					for (int j = this._mapViews.Count - 1; j >= 0; j--)
					{
						this._mapViews[j].OnDispersePlayerLeadedArmy();
					}
				}
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0001AD9C File Offset: 0x00018F9C
		void IMapStateHandler.OnExitingMenuMode()
		{
			this._latestMenuContext = null;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0001ADA5 File Offset: 0x00018FA5
		void IMapStateHandler.OnEnteringMenuMode(MenuContext menuContext)
		{
			this._latestMenuContext = menuContext;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0001ADB0 File Offset: 0x00018FB0
		void IMapStateHandler.OnMainPartyEncounter()
		{
			for (int i = this._mapViews.Count - 1; i >= 0; i--)
			{
				this._mapViews[i].OnMainPartyEncounter();
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0001ADE6 File Offset: 0x00018FE6
		void IMapStateHandler.OnSignalPeriodicEvents()
		{
			this.DeleteMarkedPeriodicEvents();
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0001ADEE File Offset: 0x00018FEE
		void IMapStateHandler.OnBattleSimulationStarted(BattleSimulation battleSimulation)
		{
			this.IsInBattleSimulation = true;
			this._battleSimulationView = this.AddMapView<BattleSimulationMapView>(new object[]
			{
				battleSimulation
			});
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0001AE0D File Offset: 0x0001900D
		void IMapStateHandler.OnBattleSimulationEnded()
		{
			this.IsInBattleSimulation = false;
			this.RemoveMapView(this._battleSimulationView);
			this._battleSimulationView = null;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0001AE29 File Offset: 0x00019029
		void IMapStateHandler.OnSiegeEngineClick(MatrixFrame siegeEngineFrame)
		{
			this._mapCameraView.SiegeEngineClick(siegeEngineFrame);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0001AE37 File Offset: 0x00019037
		void IGameStateListener.OnInitialize()
		{
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0001AE39 File Offset: 0x00019039
		void IMapStateHandler.OnPlayerSiegeActivated()
		{
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0001AE3B File Offset: 0x0001903B
		void IMapStateHandler.OnPlayerSiegeDeactivated()
		{
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0001AE3D File Offset: 0x0001903D
		void IMapStateHandler.OnGameplayCheatsEnabled()
		{
			this.OpenGameplayCheats();
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0001AE45 File Offset: 0x00019045
		void IGameStateListener.OnActivate()
		{
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0001AE47 File Offset: 0x00019047
		void IGameStateListener.OnDeactivate()
		{
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0001AE4C File Offset: 0x0001904C
		void IMapStateHandler.OnMapConversationStarts(ConversationCharacterData playerCharacterData, ConversationCharacterData conversationPartnerData)
		{
			if (this._isReadyForRender || this._conversationOverThisFrame)
			{
				this.HandleMapConversationInit(playerCharacterData, conversationPartnerData);
				return;
			}
			MapScreen.TempConversationStateHandler tempConversationStateHandler = new MapScreen.TempConversationStateHandler();
			this._conversationDataCache = new Tuple<ConversationCharacterData, ConversationCharacterData, MapScreen.TempConversationStateHandler>(playerCharacterData, conversationPartnerData, tempConversationStateHandler);
			Campaign.Current.ConversationManager.Handler = tempConversationStateHandler;
			Game.Current.GameStateManager.RegisterActiveStateDisableRequest(this);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0001AEA8 File Offset: 0x000190A8
		private void HandleMapConversationInit(ConversationCharacterData playerCharacterData, ConversationCharacterData conversationPartnerData)
		{
			if (this._mapConversationView == null)
			{
				for (int i = this._mapViews.Count - 1; i >= 0; i--)
				{
					this._mapViews[i].OnMapConversationStart();
				}
			}
			MenuViewContext menuViewContext = this._menuViewContext;
			if (menuViewContext != null)
			{
				menuViewContext.OnMapConversationActivated();
			}
			if (this._mapConversationView == null)
			{
				this._mapConversationView = this.AddMapView<MapConversationView>(new object[]
				{
					playerCharacterData,
					conversationPartnerData
				});
			}
			else
			{
				for (int j = this._mapViews.Count - 1; j >= 0; j--)
				{
					this._mapViews[j].OnMapConversationUpdate(playerCharacterData, conversationPartnerData);
				}
			}
			this._mapCursor.SetVisible(false);
			this.HandleIfSceneIsReady();
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0001AF64 File Offset: 0x00019164
		void IMapStateHandler.OnMapConversationOver()
		{
			this._conversationOverThisFrame = true;
			for (int i = this._mapViews.Count - 1; i >= 0; i--)
			{
				this._mapViews[i].OnMapConversationOver();
			}
			MenuViewContext menuViewContext = this._menuViewContext;
			if (menuViewContext != null)
			{
				menuViewContext.OnMapConversationDeactivated();
			}
			this.HandleMapConversationOver();
			this._activatedFrameNo = Utilities.EngineFrameNo;
			this.HandleIfSceneIsReady();
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0001AFC9 File Offset: 0x000191C9
		private void HandleMapConversationOver()
		{
			if (this._mapConversationView != null)
			{
				this.RemoveMapView(this._mapConversationView);
			}
			this._mapConversationView = null;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0001AFE8 File Offset: 0x000191E8
		private void InitializeVisuals()
		{
			this.InactiveLightMeshes = new List<Mesh>();
			this.ActiveLightMeshes = new List<Mesh>();
			MapScene mapScene = Campaign.Current.MapSceneWrapper as MapScene;
			this._targetCircleEntitySmall = GameEntity.CreateEmpty(mapScene.Scene, true);
			this._targetCircleEntitySmall.Name = "tCircleSmall";
			this._targetCircleEntityBig = GameEntity.CreateEmpty(mapScene.Scene, true);
			this._targetCircleEntityBig.Name = "tCircleBig";
			this._targetCircleTown = GameEntity.CreateEmpty(mapScene.Scene, true);
			this._targetCircleTown.Name = "tTown";
			this._partyOutlineEntity = GameEntity.CreateEmpty(mapScene.Scene, true);
			this._partyOutlineEntity.Name = "sCircle";
			this._townOutlineEntity = GameEntity.CreateEmpty(mapScene.Scene, true);
			this._townOutlineEntity.Name = "sSettlementOutline";
			this._targetDecalMeshSmall = Decal.CreateDecal(null);
			if (this._targetDecalMeshSmall != null)
			{
				this._settlementOutlineMesh = this._targetDecalMeshSmall.CreateCopy();
				Material fromResource = Material.GetFromResource("decal_city_circle_a");
				if (fromResource != null)
				{
					this._settlementOutlineMesh.SetMaterial(fromResource);
				}
				this._targetTownMesh = this._settlementOutlineMesh.CreateCopy();
				this._targetDecalMeshSmall = this._targetDecalMeshSmall.CreateCopy();
				Material fromResource2 = Material.GetFromResource("map_circle_decal");
				if (fromResource2 != null)
				{
					this._targetDecalMeshSmall.SetMaterial(fromResource2);
				}
				else
				{
					MBDebug.ShowWarning("Material(map_circle_decal) for party circles could not be found.");
				}
				this._targetDecalMeshBig = this._targetDecalMeshSmall.CreateCopy();
				this._partyOutlineMesh = this._targetDecalMeshSmall.CreateCopy();
				mapScene.Scene.AddDecalInstance(this._targetDecalMeshSmall, "editor_set", false);
				mapScene.Scene.AddDecalInstance(this._targetDecalMeshBig, "editor_set", false);
				mapScene.Scene.AddDecalInstance(this._partyOutlineMesh, "editor_set", false);
				mapScene.Scene.AddDecalInstance(this._settlementOutlineMesh, "editor_set", false);
				mapScene.Scene.AddDecalInstance(this._targetTownMesh, "editor_set", false);
				this._targetCircleEntitySmall.AddComponent(this._targetDecalMeshSmall);
				this._targetCircleEntityBig.AddComponent(this._targetDecalMeshBig);
				this._partyOutlineEntity.AddComponent(this._partyOutlineMesh);
				this._townOutlineEntity.AddComponent(this._settlementOutlineMesh);
				this._targetCircleTown.AddComponent(this._targetTownMesh);
			}
			else
			{
				MBDebug.ShowWarning("Mesh(decal_mesh) for party circles could not be found.");
			}
			this._mapCursor.Initialize(this);
			this._campaign = Campaign.Current;
			this._campaign.AddEntityComponent<MapTracksVisual>();
			this._campaign.AddEntityComponent<MapWeatherVisualManager>();
			this._campaign.AddEntityComponent<MapAudioManager>();
			this._campaign.AddEntityComponent<PartyVisualManager>();
			this.ContourMaskEntity = GameEntity.CreateEmpty(mapScene.Scene, true);
			this.ContourMaskEntity.Name = "aContourMask";
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0001B2C0 File Offset: 0x000194C0
		internal void TickCircles(float realDt)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			float num = 0.5f;
			float num2 = 0.5f;
			int num3 = 0;
			int num4 = 0;
			uint factor1Linear = 4293199122U;
			uint factor1Linear2 = 4293199122U;
			uint factor1Linear3 = 4293199122U;
			bool flag4 = false;
			bool flag5 = false;
			MatrixFrame matrixFrame = MatrixFrame.Identity;
			PartyBase partyBase = null;
			if (MobileParty.MainParty.Ai.PartyMoveMode == MoveModeType.Point && MobileParty.MainParty.DefaultBehavior != AiBehavior.GoToSettlement && MobileParty.MainParty.DefaultBehavior != AiBehavior.Hold && !MobileParty.MainParty.Ai.ForceAiNoPathMode && MobileParty.MainParty.MapEvent == null && MobileParty.MainParty.TargetPosition.DistanceSquared(MobileParty.MainParty.Position2D) > 0.01f)
			{
				flag = true;
				flag2 = true;
				num = 0.238846f;
				num2 = 0.278584f;
				num3 = 4;
				num4 = 5;
				factor1Linear = 4293993473U;
				factor1Linear2 = 4293993473U;
				matrixFrame.origin = new Vec3(MobileParty.MainParty.TargetPosition, 0f, -1f);
				flag5 = true;
			}
			else
			{
				if (MobileParty.MainParty.Ai.PartyMoveMode == MoveModeType.Party && MobileParty.MainParty.Ai.MoveTargetParty != null && MobileParty.MainParty.Ai.MoveTargetParty.IsVisible)
				{
					if (MobileParty.MainParty.Ai.MoveTargetParty.CurrentSettlement == null || MobileParty.MainParty.Ai.MoveTargetParty.CurrentSettlement.IsHideout)
					{
						partyBase = MobileParty.MainParty.Ai.MoveTargetParty.Party;
					}
					else
					{
						partyBase = MobileParty.MainParty.Ai.MoveTargetParty.CurrentSettlement.Party;
					}
				}
				else if (MobileParty.MainParty.DefaultBehavior == AiBehavior.GoToSettlement && MobileParty.MainParty.TargetSettlement != null)
				{
					partyBase = MobileParty.MainParty.TargetSettlement.Party;
				}
				if (partyBase != null)
				{
					bool flag6 = FactionManager.IsAtWarAgainstFaction(partyBase.MapFaction, Hero.MainHero.MapFaction);
					bool flag7 = FactionManager.IsAlliedWithFaction(partyBase.MapFaction, Hero.MainHero.MapFaction);
					matrixFrame = PartyVisualManager.Current.GetVisualOfParty(partyBase).CircleLocalFrame;
					if (partyBase.IsMobile)
					{
						flag = true;
						num3 = this.GetCircleIndex();
						factor1Linear = (flag6 ? this._enemyPartyDecalColor : (flag7 ? this._allyPartyDecalColor : this._neutralPartyDecalColor));
						num = matrixFrame.rotation.GetScaleVector().x * 1.2f;
					}
					else if (partyBase.IsSettlement && (partyBase.Settlement.IsTown || partyBase.Settlement.IsCastle))
					{
						flag4 = true;
						flag3 = true;
						factor1Linear3 = (flag6 ? this._enemyPartyDecalColor : (flag7 ? this._allyPartyDecalColor : this._neutralPartyDecalColor));
						num = matrixFrame.rotation.GetScaleVector().x * 1.2f;
					}
					else
					{
						flag = true;
						num3 = 5;
						factor1Linear = (flag6 ? this._enemyPartyDecalColor : (flag7 ? this._allyPartyDecalColor : this._neutralPartyDecalColor));
						num = matrixFrame.rotation.GetScaleVector().x * 1.2f;
					}
					if (!flag4)
					{
						matrixFrame.origin += new Vec3(partyBase.Position2D + (partyBase.IsMobile ? (partyBase.MobileParty.EventPositionAdder + partyBase.MobileParty.ArmyPositionAdder) : Vec2.Zero), 0f, -1f);
					}
				}
			}
			if (flag5)
			{
				float num5 = (this._mapCameraView.CameraDistance + 80f) * (this._mapCameraView.CameraDistance + 80f) / 5000f;
				num5 = MathF.Clamp(num5, 0.2f, 45f);
				num *= num5;
				num2 *= num5;
			}
			if (partyBase == null)
			{
				this._targetCircleRotationStartTime = 0f;
			}
			else if (this._targetCircleRotationStartTime == 0f)
			{
				this._targetCircleRotationStartTime = MBCommon.GetApplicationTime();
			}
			Vec3 normalAt = this._mapScene.GetNormalAt(matrixFrame.origin.AsVec2);
			if (!flag4)
			{
				Vec3 origin = this._targetCircleTown.GetGlobalFrame().origin;
				matrixFrame.origin.z = ((origin.AsVec2 != matrixFrame.origin.AsVec2) ? this._mapScene.GetTerrainHeight(matrixFrame.origin.AsVec2, true) : origin.z);
			}
			MatrixFrame identity = MatrixFrame.Identity;
			identity.origin = matrixFrame.origin;
			identity.rotation.u = normalAt;
			MatrixFrame matrixFrame2 = identity;
			identity.rotation.ApplyScaleLocal(new Vec3(num, num, num, -1f));
			matrixFrame2.rotation.ApplyScaleLocal(new Vec3(num2, num2, num2, -1f));
			this._targetCircleEntitySmall.SetVisibilityExcludeParents(flag);
			this._targetCircleEntityBig.SetVisibilityExcludeParents(flag2);
			this._targetCircleTown.SetVisibilityExcludeParents(flag3);
			if (flag)
			{
				this._targetDecalMeshSmall.SetVectorArgument(0.166f, 1f, 0.166f * (float)num3, 0f);
				this._targetDecalMeshSmall.SetFactor1Linear(factor1Linear);
				this._targetCircleEntitySmall.SetGlobalFrame(identity);
			}
			if (flag2)
			{
				this._targetDecalMeshBig.SetVectorArgument(0.166f, 1f, 0.166f * (float)num4, 0f);
				this._targetDecalMeshBig.SetFactor1Linear(factor1Linear2);
				this._targetCircleEntityBig.SetGlobalFrame(matrixFrame2);
			}
			if (flag3)
			{
				this._targetTownMesh.SetVectorArgument(1f, 1f, 0f, 0f);
				this._targetTownMesh.SetFactor1Linear(factor1Linear3);
				this._targetCircleTown.SetGlobalFrame(matrixFrame);
			}
			MatrixFrame matrixFrame3 = MatrixFrame.Identity;
			if (this.CurrentVisualOfTooltip == null || ((partyBase != null) ? partyBase.MapEntity : null) == this.CurrentVisualOfTooltip.GetMapEntity())
			{
				this._townOutlineEntity.SetVisibilityExcludeParents(false);
				this._partyOutlineEntity.SetVisibilityExcludeParents(false);
				return;
			}
			this._mapCursor.OnAnotherEntityHighlighted();
			IMapEntity mapEntity = this.CurrentVisualOfTooltip.GetMapEntity();
			if (mapEntity == null || !mapEntity.ShowCircleAroundEntity)
			{
				this._townOutlineEntity.SetVisibilityExcludeParents(false);
				this._partyOutlineEntity.SetVisibilityExcludeParents(false);
				return;
			}
			bool flag8 = mapEntity.IsEnemyOf(Hero.MainHero.MapFaction);
			bool flag9 = mapEntity.IsAllyOf(Hero.MainHero.MapFaction);
			Settlement settlement;
			flag4 = ((settlement = (mapEntity as Settlement)) != null && settlement.IsFortification);
			Vec3 origin2;
			if (flag4)
			{
				origin2 = this._townOutlineEntity.GetGlobalFrame().origin;
				matrixFrame3 = this.CurrentVisualOfTooltip.CircleLocalFrame;
				if (flag8)
				{
					this._settlementOutlineMesh.SetFactor1Linear(this._enemyPartyDecalColor);
				}
				else if (flag9)
				{
					this._settlementOutlineMesh.SetFactor1Linear(this._allyPartyDecalColor);
				}
				else
				{
					this._settlementOutlineMesh.SetFactor1Linear(this._neutralPartyDecalColor);
				}
			}
			else
			{
				origin2 = this._partyOutlineEntity.GetGlobalFrame().origin;
				matrixFrame3.origin = this.CurrentVisualOfTooltip.GetVisualPosition() + this.CurrentVisualOfTooltip.CircleLocalFrame.origin;
				matrixFrame3.rotation = this.CurrentVisualOfTooltip.CircleLocalFrame.rotation;
				this._partyOutlineMesh.SetFactor1Linear(flag8 ? this._enemyPartyDecalColor : (flag9 ? this._allyPartyDecalColor : this._neutralPartyDecalColor));
				this._partyOutlineMesh.SetVectorArgument(0.166f, 1f, 0.83f, 0f);
			}
			matrixFrame3.origin.z = ((origin2.AsVec2 != matrixFrame3.origin.AsVec2) ? this._mapScene.GetTerrainHeight(matrixFrame3.origin.AsVec2, true) : origin2.z);
			if (flag4)
			{
				matrixFrame3.rotation.u = normalAt * matrixFrame3.rotation.u.Length;
				this._townOutlineEntity.SetGlobalFrame(matrixFrame3);
				this._townOutlineEntity.SetVisibilityExcludeParents(true);
				this._partyOutlineEntity.SetVisibilityExcludeParents(false);
				return;
			}
			this._partyOutlineEntity.SetGlobalFrame(matrixFrame3);
			this._townOutlineEntity.SetVisibilityExcludeParents(false);
			this._partyOutlineEntity.SetVisibilityExcludeParents(true);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0001BAF0 File Offset: 0x00019CF0
		public void SetIsInTownManagement(bool isInTownManagement)
		{
			if (this.IsInTownManagement != isInTownManagement)
			{
				this.IsInTownManagement = isInTownManagement;
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0001BB02 File Offset: 0x00019D02
		public void SetIsInHideoutTroopManage(bool isInHideoutTroopManage)
		{
			if (this.IsInHideoutTroopManage != isInHideoutTroopManage)
			{
				this.IsInHideoutTroopManage = isInHideoutTroopManage;
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0001BB14 File Offset: 0x00019D14
		public void SetIsInArmyManagement(bool isInArmyManagement)
		{
			if (this.IsInArmyManagement != isInArmyManagement)
			{
				this.IsInArmyManagement = isInArmyManagement;
				if (!this.IsInArmyManagement)
				{
					MenuViewContext menuViewContext = this._menuViewContext;
					if (menuViewContext == null)
					{
						return;
					}
					menuViewContext.OnResume();
				}
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0001BB3E File Offset: 0x00019D3E
		public void SetIsInRecruitment(bool isInRecruitment)
		{
			if (this.IsInRecruitment != isInRecruitment)
			{
				this.IsInRecruitment = isInRecruitment;
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0001BB50 File Offset: 0x00019D50
		public void SetIsBarExtended(bool isBarExtended)
		{
			if (this.IsBarExtended != isBarExtended)
			{
				this.IsBarExtended = isBarExtended;
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0001BB62 File Offset: 0x00019D62
		public void SetIsInCampaignOptions(bool isInCampaignOptions)
		{
			if (this.IsInCampaignOptions != isInCampaignOptions)
			{
				this.IsInCampaignOptions = isInCampaignOptions;
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0001BB74 File Offset: 0x00019D74
		public void SetIsMarriageOfferPopupActive(bool isMarriageOfferPopupActive)
		{
			if (this.IsMarriageOfferPopupActive != isMarriageOfferPopupActive)
			{
				this.IsMarriageOfferPopupActive = isMarriageOfferPopupActive;
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0001BB86 File Offset: 0x00019D86
		public void SetIsMapCheatsActive(bool isMapCheatsActive)
		{
			if (this.IsMapCheatsActive != isMapCheatsActive)
			{
				this.IsMapCheatsActive = isMapCheatsActive;
				this._cheatPressTimer = 0f;
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0001BBA4 File Offset: 0x00019DA4
		private void TickVisuals(float realDt)
		{
			if (this._campaign.CampaignDt < 1E-05f)
			{
				this.ApplySoundSceneProps(realDt);
			}
			else
			{
				this.ApplySoundSceneProps(this._campaign.CampaignDt);
			}
			this._mapScene.TimeOfDay = CampaignTime.Now.CurrentHourInDay;
			float seasonTimeFactor;
			float num;
			Campaign.Current.Models.MapWeatherModel.GetSeasonTimeFactorOfCampaignTime(CampaignTime.Now, out seasonTimeFactor, out num, false);
			MBMapScene.SetSeasonTimeFactor(this._mapScene, seasonTimeFactor);
			if (!NativeConfig.DisableSound && ScreenManager.TopScreen is MapScreen)
			{
				this._soundCalculationTime += realDt;
				if (this._isSoundOn)
				{
					this.TickStepSounds();
				}
				if (this._soundCalculationTime > 0.2f)
				{
					this._soundCalculationTime -= 0.2f;
				}
			}
			if (this.IsReady)
			{
				foreach (CampaignEntityComponent campaignEntityComponent in this._campaign.CampaignEntityComponents)
				{
					CampaignEntityVisualComponent campaignEntityVisualComponent = campaignEntityComponent as CampaignEntityVisualComponent;
					if (campaignEntityVisualComponent != null)
					{
						campaignEntityVisualComponent.OnVisualTick(this, realDt, this._campaign.CampaignDt);
					}
				}
			}
			MBMapScene.TickVisuals(this._mapScene, Campaign.CurrentTime % 24f, this._tickedMapMeshes);
			this.TickCircles(realDt);
			MBWindowManager.PreDisplay();
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0001BD00 File Offset: 0x00019F00
		private void TickStepSounds()
		{
			if (Campaign.Current.CampaignDt > 0f)
			{
				MobileParty mainParty = MobileParty.MainParty;
				float seeingRange = mainParty.SeeingRange;
				LocatableSearchData<MobileParty> locatableSearchData = MobileParty.StartFindingLocatablesAroundPosition(mainParty.Position2D, seeingRange + 25f);
				for (MobileParty mobileParty = MobileParty.FindNextLocatable(ref locatableSearchData); mobileParty != null; mobileParty = MobileParty.FindNextLocatable(ref locatableSearchData))
				{
					if (!mobileParty.IsMilitia && !mobileParty.IsGarrison)
					{
						this.StepSounds(mobileParty);
					}
				}
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0001BD68 File Offset: 0x00019F68
		private void StepSounds(MobileParty party)
		{
			if (party.IsVisible && party.MemberRoster.TotalManCount > 0)
			{
				PartyVisual visualOfParty = PartyVisualManager.Current.GetVisualOfParty(party.Party);
				if (visualOfParty.HumanAgentVisuals != null)
				{
					TerrainType faceTerrainType = Campaign.Current.MapSceneWrapper.GetFaceTerrainType(party.CurrentNavigationFace);
					AgentVisuals agentVisuals = null;
					int soundType = 0;
					if (visualOfParty.CaravanMountAgentVisuals != null)
					{
						soundType = 3;
						agentVisuals = visualOfParty.CaravanMountAgentVisuals;
					}
					else if (visualOfParty.HumanAgentVisuals != null)
					{
						if (visualOfParty.MountAgentVisuals != null)
						{
							soundType = 1;
							agentVisuals = visualOfParty.MountAgentVisuals;
						}
						else
						{
							soundType = 0;
							agentVisuals = visualOfParty.HumanAgentVisuals;
						}
					}
					MBMapScene.TickStepSound(this._mapScene, agentVisuals.GetVisuals(), (int)faceTerrainType, soundType);
				}
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0001BE0B File Offset: 0x0001A00B
		public void SetMouseVisible(bool value)
		{
			this.SceneLayer.InputRestrictions.SetMouseVisibility(value);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0001BE1E File Offset: 0x0001A01E
		public bool GetMouseVisible()
		{
			return MBMapScene.GetMouseVisible();
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0001BE25 File Offset: 0x0001A025
		public void RestartAmbientSounds()
		{
			if (this._mapScene != null)
			{
				this._mapScene.ResumeSceneSounds();
			}
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0001BE40 File Offset: 0x0001A040
		void IGameStateListener.OnFinalize()
		{
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0001BE42 File Offset: 0x0001A042
		public void PauseAmbientSounds()
		{
			if (this._mapScene != null)
			{
				this._mapScene.PauseSceneSounds();
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0001BE5D File Offset: 0x0001A05D
		public void StopSoundSceneProps()
		{
			if (this._mapScene != null)
			{
				this._mapScene.FinishSceneSounds();
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0001BE78 File Offset: 0x0001A078
		public void ApplySoundSceneProps(float dt)
		{
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0001BE7C File Offset: 0x0001A07C
		private void CollectTickableMapMeshes()
		{
			this._tickedMapEntities = this._mapScene.FindEntitiesWithTag("ticked_map_entity").ToArray<GameEntity>();
			this._tickedMapMeshes = new Mesh[this._tickedMapEntities.Length];
			for (int i = 0; i < this._tickedMapEntities.Length; i++)
			{
				this._tickedMapMeshes[i] = this._tickedMapEntities[i].GetFirstMesh();
			}
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0001BEDF File Offset: 0x0001A0DF
		public void OnPauseTick(float dt)
		{
			this.ApplySoundSceneProps(dt);
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0001BEE8 File Offset: 0x0001A0E8
		public static Dictionary<UIntPtr, PartyVisual> VisualsOfEntities
		{
			get
			{
				return SandBoxViewSubModule.VisualsOfEntities;
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0001BEF0 File Offset: 0x0001A0F0
		public MBCampaignEvent CreatePeriodicUIEvent(CampaignTime triggerPeriod, CampaignTime initialWait)
		{
			MBCampaignEvent mbcampaignEvent = new MBCampaignEvent(triggerPeriod, initialWait);
			this._periodicCampaignUIEvents.Add(mbcampaignEvent);
			return mbcampaignEvent;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0001BF12 File Offset: 0x0001A112
		internal static Dictionary<UIntPtr, Tuple<MatrixFrame, PartyVisual>> FrameAndVisualOfEngines
		{
			get
			{
				return SandBoxViewSubModule.FrameAndVisualOfEngines;
			}
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0001BF1C File Offset: 0x0001A11C
		private void DeleteMarkedPeriodicEvents()
		{
			for (int i = this._periodicCampaignUIEvents.Count - 1; i >= 0; i--)
			{
				if (this._periodicCampaignUIEvents[i].isEventDeleted)
				{
					this._periodicCampaignUIEvents.RemoveAt(i);
				}
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0001BF60 File Offset: 0x0001A160
		public void DeletePeriodicUIEvent(MBCampaignEvent campaignEvent)
		{
			campaignEvent.isEventDeleted = true;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0001BF69 File Offset: 0x0001A169
		private static float CalculateCameraElevation(float cameraDistance)
		{
			return cameraDistance * 0.5f * 0.015f + 0.35f;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0001BF7E File Offset: 0x0001A17E
		public void OpenOptions()
		{
			ScreenManager.PushScreen(ViewCreator.CreateOptionsScreen(false));
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0001BF8B File Offset: 0x0001A18B
		public void OpenEncyclopedia()
		{
			Campaign.Current.EncyclopediaManager.GoToLink("LastPage", "");
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0001BFA6 File Offset: 0x0001A1A6
		public void OpenSaveLoad(bool isSaving)
		{
			ScreenManager.PushScreen(SandBoxViewCreator.CreateSaveLoadScreen(isSaving));
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0001BFB3 File Offset: 0x0001A1B3
		private void OpenGameplayCheats()
		{
			this._mapCheatsView = this.AddMapView<MapCheatsView>(Array.Empty<object>());
			this.IsMapCheatsActive = true;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0001BFCD File Offset: 0x0001A1CD
		public void CloseGameplayCheats()
		{
			if (this._mapCheatsView != null)
			{
				this.RemoveMapView(this._mapCheatsView);
				return;
			}
			Debug.FailedAssert("Requested remove map cheats but cheats is not enabled", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox.View\\Map\\MapScreen.cs", "CloseGameplayCheats", 3433);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0001BFFD File Offset: 0x0001A1FD
		public void CloseEscapeMenu()
		{
			this.OnEscapeMenuToggled(false);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0001C006 File Offset: 0x0001A206
		public void OpenEscapeMenu()
		{
			this.OnEscapeMenuToggled(true);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0001C010 File Offset: 0x0001A210
		public void CloseCampaignOptions()
		{
			if (this._campaignOptionsView == null)
			{
				Debug.FailedAssert("Trying to close campaign options when it's not set", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox.View\\Map\\MapScreen.cs", "CloseCampaignOptions", 3451);
				this._campaignOptionsView = this.GetMapView<MapCampaignOptionsView>();
				if (this._campaignOptionsView == null)
				{
					Debug.FailedAssert("Trying to close campaign options when it's not open", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox.View\\Map\\MapScreen.cs", "CloseCampaignOptions", 3456);
					this.IsInCampaignOptions = false;
					this._campaignOptionsView = null;
					return;
				}
			}
			if (this._campaignOptionsView != null)
			{
				this.RemoveMapView(this._campaignOptionsView);
			}
			this._campaignOptionsView = null;
			this.IsInCampaignOptions = false;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0001C09C File Offset: 0x0001A29C
		private List<EscapeMenuItemVM> GetEscapeMenuItems()
		{
			bool isMapConversationActive = this._mapConversationView != null;
			bool isAtSaveLimit = MBSaveLoad.IsMaxNumberOfSavesReached();
			List<EscapeMenuItemVM> list = new List<EscapeMenuItemVM>();
			list.Add(new EscapeMenuItemVM(new TextObject("{=e139gKZc}Return to the Game", null), delegate(object o)
			{
				this.OnEscapeMenuToggled(false);
			}, null, () => new Tuple<bool, TextObject>(false, TextObject.Empty), true));
			list.Add(new EscapeMenuItemVM(new TextObject("{=PXT6aA4J}Campaign Options", null), delegate(object o)
			{
				this._campaignOptionsView = this.AddMapView<MapCampaignOptionsView>(Array.Empty<object>());
				this.IsInCampaignOptions = true;
			}, null, () => new Tuple<bool, TextObject>(false, TextObject.Empty), false));
			list.Add(new EscapeMenuItemVM(new TextObject("{=NqarFr4P}Options", null), delegate(object o)
			{
				this.OnEscapeMenuToggled(false);
				this.OpenOptions();
			}, null, () => new Tuple<bool, TextObject>(false, TextObject.Empty), false));
			list.Add(new EscapeMenuItemVM(new TextObject("{=bV75iwKa}Save", null), delegate(object o)
			{
				this.OnEscapeMenuToggled(false);
				Campaign.Current.SaveHandler.QuickSaveCurrentGame();
			}, null, () => this.GetIsEscapeMenuOptionDisabledReason(isMapConversationActive, false, false), false));
			list.Add(new EscapeMenuItemVM(new TextObject("{=e0KdfaNe}Save As", null), delegate(object o)
			{
				this.OnEscapeMenuToggled(false);
				this.OpenSaveLoad(true);
			}, null, () => this.GetIsEscapeMenuOptionDisabledReason(isMapConversationActive, CampaignOptions.IsIronmanMode, false), false));
			list.Add(new EscapeMenuItemVM(new TextObject("{=9NuttOBC}Load", null), delegate(object o)
			{
				this.OnEscapeMenuToggled(false);
				this.OpenSaveLoad(false);
			}, null, () => this.GetIsEscapeMenuOptionDisabledReason(isMapConversationActive, CampaignOptions.IsIronmanMode, false), false));
			list.Add(new EscapeMenuItemVM(new TextObject("{=AbEh2y8o}Save And Exit", null), delegate(object o)
			{
				Campaign.Current.SaveHandler.QuickSaveCurrentGame();
				this.OnEscapeMenuToggled(false);
				InformationManager.HideInquiry();
				this._exitOnSaveOver = true;
			}, null, () => this.GetIsEscapeMenuOptionDisabledReason(isMapConversationActive, false, isAtSaveLimit), false));
			Action <>9__16;
			list.Add(new EscapeMenuItemVM(new TextObject("{=RamV6yLM}Exit to Main Menu", null), delegate(object o)
			{
				string titleText = GameTexts.FindText("str_exit", null).ToString();
				string text = GameTexts.FindText("str_mission_exit_query", null).ToString();
				bool isAffirmativeOptionShown = true;
				bool isNegativeOptionShown = true;
				string affirmativeText = GameTexts.FindText("str_yes", null).ToString();
				string negativeText = GameTexts.FindText("str_no", null).ToString();
				Action affirmativeAction = new Action(this.OnExitToMainMenu);
				Action negativeAction;
				if ((negativeAction = <>9__16) == null)
				{
					negativeAction = (<>9__16 = delegate()
					{
						this.OnEscapeMenuToggled(false);
					});
				}
				InformationManager.ShowInquiry(new InquiryData(titleText, text, isAffirmativeOptionShown, isNegativeOptionShown, affirmativeText, negativeText, affirmativeAction, negativeAction, "", 0f, null, null, null), false, false);
			}, null, () => this.GetIsEscapeMenuOptionDisabledReason(false, CampaignOptions.IsIronmanMode, false), false));
			return list;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0001C290 File Offset: 0x0001A490
		private Tuple<bool, TextObject> GetIsEscapeMenuOptionDisabledReason(bool isMapConversationActive, bool isIronmanMode, bool isAtSaveLimit)
		{
			if (isIronmanMode)
			{
				return new Tuple<bool, TextObject>(true, GameTexts.FindText("str_pause_menu_disabled_hint", "IronmanMode"));
			}
			if (isMapConversationActive)
			{
				return new Tuple<bool, TextObject>(true, GameTexts.FindText("str_pause_menu_disabled_hint", "OngoingConversation"));
			}
			if (isAtSaveLimit)
			{
				return new Tuple<bool, TextObject>(true, GameTexts.FindText("str_pause_menu_disabled_hint", "SaveLimitReached"));
			}
			return new Tuple<bool, TextObject>(false, TextObject.Empty);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0001C2F3 File Offset: 0x0001A4F3
		private void OpenParty()
		{
			if (Hero.MainHero.HeroState != Hero.CharacterStates.Prisoner && Hero.MainHero != null)
			{
				Hero mainHero = Hero.MainHero;
				if (mainHero != null && !mainHero.IsDead)
				{
					PartyScreenManager.OpenScreenAsNormal();
				}
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0001C324 File Offset: 0x0001A524
		public void OpenInventory()
		{
			if (Hero.MainHero != null)
			{
				Hero mainHero = Hero.MainHero;
				if (mainHero != null && !mainHero.IsDead)
				{
					InventoryManager.OpenScreenAsInventory(null);
				}
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0001C34C File Offset: 0x0001A54C
		private void OpenKingdom()
		{
			if (Hero.MainHero != null)
			{
				Hero mainHero = Hero.MainHero;
				if (mainHero != null && !mainHero.IsDead && Hero.MainHero.MapFaction.IsKingdomFaction)
				{
					KingdomState gameState = Game.Current.GameStateManager.CreateState<KingdomState>();
					Game.Current.GameStateManager.PushState(gameState, 0);
				}
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0001C3A8 File Offset: 0x0001A5A8
		private void OnExitToMainMenu()
		{
			this.OnEscapeMenuToggled(false);
			InformationManager.HideInquiry();
			this.OnExit();
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0001C3BC File Offset: 0x0001A5BC
		private void OpenQuestsScreen()
		{
			if (Hero.MainHero != null)
			{
				Hero mainHero = Hero.MainHero;
				if (mainHero != null && !mainHero.IsDead)
				{
					Game.Current.GameStateManager.PushState(Game.Current.GameStateManager.CreateState<QuestsState>(), 0);
				}
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0001C3FA File Offset: 0x0001A5FA
		private void OpenClanScreen()
		{
			if (Hero.MainHero != null)
			{
				Hero mainHero = Hero.MainHero;
				if (mainHero != null && !mainHero.IsDead)
				{
					Game.Current.GameStateManager.PushState(Game.Current.GameStateManager.CreateState<ClanState>(), 0);
				}
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0001C438 File Offset: 0x0001A638
		private void OpenCharacterDevelopmentScreen()
		{
			if (Hero.MainHero != null)
			{
				Hero mainHero = Hero.MainHero;
				if (mainHero != null && !mainHero.IsDead)
				{
					Game.Current.GameStateManager.PushState(Game.Current.GameStateManager.CreateState<CharacterDeveloperState>(), 0);
				}
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0001C476 File Offset: 0x0001A676
		public void OpenFacegenScreenAux()
		{
			this.OpenFaceGeneratorScreen();
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0001C480 File Offset: 0x0001A680
		private int GetCircleIndex()
		{
			int num = (int)((MBCommon.GetApplicationTime() - this._targetCircleRotationStartTime) / 0.1f) % 10;
			if (num >= 5)
			{
				num = 10 - num - 1;
			}
			return num;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0001C4B0 File Offset: 0x0001A6B0
		public void FastMoveCameraToMainParty()
		{
			this._mapCameraView.FastMoveCameraToMainParty();
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0001C4BD File Offset: 0x0001A6BD
		public void ResetCamera(bool resetDistance, bool teleportToMainParty)
		{
			this._mapCameraView.ResetCamera(resetDistance, teleportToMainParty);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0001C4CC File Offset: 0x0001A6CC
		public void TeleportCameraToMainParty()
		{
			this._mapCameraView.TeleportCameraToMainParty();
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0001C4D9 File Offset: 0x0001A6D9
		public bool IsCameraLockedToPlayerParty()
		{
			return this._mapCameraView.IsCameraLockedToPlayerParty();
		}

		// Token: 0x04000159 RID: 345
		private const float DoubleClickTimeLimit = 0.3f;

		// Token: 0x0400015D RID: 349
		private MenuViewContext _menuViewContext;

		// Token: 0x0400015E RID: 350
		private MenuContext _latestMenuContext;

		// Token: 0x0400015F RID: 351
		private bool _partyIconNeedsRefreshing;

		// Token: 0x04000160 RID: 352
		private uint _tooltipTargetHash;

		// Token: 0x04000161 RID: 353
		private object _tooltipTargetObject;

		// Token: 0x04000162 RID: 354
		private readonly ObservableCollection<MapView> _mapViews;

		// Token: 0x04000163 RID: 355
		private MapView[] _mapViewsCopyCache;

		// Token: 0x04000164 RID: 356
		private MapView _encounterOverlay;

		// Token: 0x04000165 RID: 357
		private MapView _armyOverlay;

		// Token: 0x04000166 RID: 358
		private MapReadyView _mapReadyView;

		// Token: 0x04000167 RID: 359
		private MapView _escapeMenuView;

		// Token: 0x04000168 RID: 360
		private MapView _battleSimulationView;

		// Token: 0x04000169 RID: 361
		private MapView _mapSiegeOverlayView;

		// Token: 0x0400016A RID: 362
		private MapView _campaignOptionsView;

		// Token: 0x0400016B RID: 363
		private MapView _mapConversationView;

		// Token: 0x0400016C RID: 364
		private MapView _marriageOfferPopupView;

		// Token: 0x0400016D RID: 365
		private MapView _mapCheatsView;

		// Token: 0x0400016E RID: 366
		public MapCameraView _mapCameraView;

		// Token: 0x0400016F RID: 367
		private MapNavigationHandler _navigationHandler = new MapNavigationHandler();

		// Token: 0x04000172 RID: 370
		private const int _frameDelayAmountForRenderActivation = 5;

		// Token: 0x04000173 RID: 371
		private float _timeSinceCreation;

		// Token: 0x04000174 RID: 372
		private bool _leftButtonDraggingMode;

		// Token: 0x04000175 RID: 373
		private UIntPtr _preSelectedSiegeEntityID;

		// Token: 0x04000176 RID: 374
		private Vec2 _oldMousePosition;

		// Token: 0x04000177 RID: 375
		private Vec2 _clickedPositionPixel;

		// Token: 0x04000178 RID: 376
		private Vec3 _clickedPosition;

		// Token: 0x04000179 RID: 377
		private Ray _mouseRay;

		// Token: 0x0400017A RID: 378
		private PartyVisual _preVisualOfSelectedEntity;

		// Token: 0x0400017B RID: 379
		private int _activatedFrameNo = Utilities.EngineFrameNo;

		// Token: 0x0400017C RID: 380
		public Dictionary<Tuple<Material, BannerCode>, Material> _characterBannerMaterialCache = new Dictionary<Tuple<Material, BannerCode>, Material>();

		// Token: 0x0400017D RID: 381
		private Tuple<ConversationCharacterData, ConversationCharacterData, MapScreen.TempConversationStateHandler> _conversationDataCache;

		// Token: 0x0400017E RID: 382
		private readonly int _displayedContextMenuType = -1;

		// Token: 0x0400017F RID: 383
		private double _lastReleaseTime;

		// Token: 0x04000180 RID: 384
		private double _lastPressTime;

		// Token: 0x04000181 RID: 385
		private double _secondLastPressTime;

		// Token: 0x04000182 RID: 386
		private bool _leftButtonDoubleClickOnSceneWidget;

		// Token: 0x04000183 RID: 387
		private float _waitForDoubleClickUntilTime;

		// Token: 0x04000184 RID: 388
		private float _timeToggleTimer = float.MaxValue;

		// Token: 0x04000185 RID: 389
		private bool _ignoreNextTimeToggle;

		// Token: 0x04000186 RID: 390
		private bool _exitOnSaveOver;

		// Token: 0x04000188 RID: 392
		private Scene _mapScene;

		// Token: 0x04000189 RID: 393
		private Campaign _campaign;

		// Token: 0x0400018A RID: 394
		private readonly MapState _mapState;

		// Token: 0x0400018B RID: 395
		private bool _isSceneViewEnabled;

		// Token: 0x0400018C RID: 396
		private bool _isReadyForRender;

		// Token: 0x0400018D RID: 397
		private bool _gpuMemoryCleared;

		// Token: 0x0400018E RID: 398
		private bool _focusLost;

		// Token: 0x0400018F RID: 399
		private bool _isKingdomDecisionsDirty;

		// Token: 0x04000190 RID: 400
		private bool _conversationOverThisFrame;

		// Token: 0x04000191 RID: 401
		private float _cheatPressTimer;

		// Token: 0x0400019B RID: 411
		private Dictionary<Tuple<Material, BannerCode>, Material> _bannerTexturedMaterialCache;

		// Token: 0x0400019C RID: 412
		private GameEntity _targetCircleEntitySmall;

		// Token: 0x0400019D RID: 413
		private GameEntity _targetCircleEntityBig;

		// Token: 0x0400019E RID: 414
		private GameEntity _targetCircleTown;

		// Token: 0x0400019F RID: 415
		private GameEntity _partyOutlineEntity;

		// Token: 0x040001A0 RID: 416
		private GameEntity _townOutlineEntity;

		// Token: 0x040001A1 RID: 417
		private Decal _targetDecalMeshSmall;

		// Token: 0x040001A2 RID: 418
		private Decal _targetDecalMeshBig;

		// Token: 0x040001A3 RID: 419
		private Decal _partyOutlineMesh;

		// Token: 0x040001A4 RID: 420
		private Decal _settlementOutlineMesh;

		// Token: 0x040001A5 RID: 421
		private Decal _targetTownMesh;

		// Token: 0x040001A6 RID: 422
		private float _targetCircleRotationStartTime;

		// Token: 0x040001A7 RID: 423
		private MapCursor _mapCursor = new MapCursor();

		// Token: 0x040001A8 RID: 424
		private bool _mapSceneCursorWanted = true;

		// Token: 0x040001A9 RID: 425
		private bool _mapSceneCursorActive;

		// Token: 0x040001AD RID: 429
		public IMapTracksCampaignBehavior MapTracksCampaignBehavior;

		// Token: 0x040001AE RID: 430
		private bool _isSoundOn = true;

		// Token: 0x040001AF RID: 431
		private float _soundCalculationTime;

		// Token: 0x040001B0 RID: 432
		private const float SoundCalculationInterval = 0.2f;

		// Token: 0x040001B1 RID: 433
		private uint _enemyPartyDecalColor = 4281663744U;

		// Token: 0x040001B2 RID: 434
		private uint _allyPartyDecalColor = 4279308800U;

		// Token: 0x040001B3 RID: 435
		private uint _neutralPartyDecalColor = 4294919959U;

		// Token: 0x040001B4 RID: 436
		private MapColorGradeManager _colorGradeManager;

		// Token: 0x040001B5 RID: 437
		private bool _playerSiegeMachineSlotMeshesAdded;

		// Token: 0x040001B6 RID: 438
		private GameEntity[] _defenderMachinesCircleEntities;

		// Token: 0x040001B7 RID: 439
		private GameEntity[] _attackerRamMachinesCircleEntities;

		// Token: 0x040001B8 RID: 440
		private GameEntity[] _attackerTowerMachinesCircleEntities;

		// Token: 0x040001B9 RID: 441
		private GameEntity[] _attackerRangedMachinesCircleEntities;

		// Token: 0x040001BA RID: 442
		private string _emptyAttackerRangedDecalMaterialName = "decal_siege_ranged";

		// Token: 0x040001BB RID: 443
		private string _attackerRamMachineDecalMaterialName = "decal_siege_ram";

		// Token: 0x040001BC RID: 444
		private string _attackerTowerMachineDecalMaterialName = "decal_siege_tower";

		// Token: 0x040001BD RID: 445
		private string _attackerRangedMachineDecalMaterialName = "decal_siege_ranged";

		// Token: 0x040001BE RID: 446
		private string _defenderRangedMachineDecalMaterialName = "decal_defender_ranged_siege";

		// Token: 0x040001BF RID: 447
		private uint _preperationOrEnemySiegeEngineDecalColor = 4287064638U;

		// Token: 0x040001C0 RID: 448
		private uint _normalStartSiegeEngineDecalColor = 4278394186U;

		// Token: 0x040001C1 RID: 449
		private float _defenderMachineCircleDecalScale = 0.25f;

		// Token: 0x040001C2 RID: 450
		private float _attackerMachineDecalScale = 0.38f;

		// Token: 0x040001C3 RID: 451
		private bool _isNewDecalScaleImplementationEnabled;

		// Token: 0x040001C4 RID: 452
		private uint _normalEndSiegeEngineDecalColor = 4284320212U;

		// Token: 0x040001C5 RID: 453
		private uint _hoveredSiegeEngineDecalColor = 4293956364U;

		// Token: 0x040001C6 RID: 454
		private uint _withMachineSiegeEngineDecalColor = 4283683126U;

		// Token: 0x040001C7 RID: 455
		private float _machineDecalAnimLoopTime = 0.5f;

		// Token: 0x040001C8 RID: 456
		public bool TooltipHandlingDisabled;

		// Token: 0x040001C9 RID: 457
		private readonly UIntPtr[] _intersectedEntityIDs = new UIntPtr[128];

		// Token: 0x040001CA RID: 458
		private readonly Intersection[] _intersectionInfos = new Intersection[128];

		// Token: 0x040001CB RID: 459
		private GameEntity[] _tickedMapEntities;

		// Token: 0x040001CC RID: 460
		private Mesh[] _tickedMapMeshes;

		// Token: 0x040001CD RID: 461
		private readonly List<MBCampaignEvent> _periodicCampaignUIEvents;

		// Token: 0x040001CE RID: 462
		private bool _ignoreLeftMouseRelease;

		// Token: 0x02000091 RID: 145
		private enum TerrainTypeSoundSlot
		{
			// Token: 0x0400031E RID: 798
			dismounted,
			// Token: 0x0400031F RID: 799
			mounted,
			// Token: 0x04000320 RID: 800
			mounted_slow,
			// Token: 0x04000321 RID: 801
			caravan,
			// Token: 0x04000322 RID: 802
			ambient
		}

		// Token: 0x02000092 RID: 146
		private class TempConversationStateHandler : IConversationStateHandler
		{
			// Token: 0x0600048C RID: 1164 RVA: 0x000235B9 File Offset: 0x000217B9
			void IConversationStateHandler.ExecuteConversationContinue()
			{
				this._actionQueue.Enqueue(delegate
				{
					IConversationStateHandler tempHandler = this._tempHandler;
					if (tempHandler == null)
					{
						return;
					}
					tempHandler.ExecuteConversationContinue();
				});
			}

			// Token: 0x0600048D RID: 1165 RVA: 0x000235D2 File Offset: 0x000217D2
			void IConversationStateHandler.OnConversationActivate()
			{
				this._actionQueue.Enqueue(delegate
				{
					IConversationStateHandler tempHandler = this._tempHandler;
					if (tempHandler == null)
					{
						return;
					}
					tempHandler.OnConversationActivate();
				});
			}

			// Token: 0x0600048E RID: 1166 RVA: 0x000235EB File Offset: 0x000217EB
			void IConversationStateHandler.OnConversationContinue()
			{
				this._actionQueue.Enqueue(delegate
				{
					IConversationStateHandler tempHandler = this._tempHandler;
					if (tempHandler == null)
					{
						return;
					}
					tempHandler.OnConversationContinue();
				});
			}

			// Token: 0x0600048F RID: 1167 RVA: 0x00023604 File Offset: 0x00021804
			void IConversationStateHandler.OnConversationDeactivate()
			{
				this._actionQueue.Enqueue(delegate
				{
					IConversationStateHandler tempHandler = this._tempHandler;
					if (tempHandler == null)
					{
						return;
					}
					tempHandler.OnConversationDeactivate();
				});
			}

			// Token: 0x06000490 RID: 1168 RVA: 0x0002361D File Offset: 0x0002181D
			void IConversationStateHandler.OnConversationInstall()
			{
				this._actionQueue.Enqueue(delegate
				{
					IConversationStateHandler tempHandler = this._tempHandler;
					if (tempHandler == null)
					{
						return;
					}
					tempHandler.OnConversationInstall();
				});
			}

			// Token: 0x06000491 RID: 1169 RVA: 0x00023636 File Offset: 0x00021836
			void IConversationStateHandler.OnConversationUninstall()
			{
				this._actionQueue.Enqueue(delegate
				{
					IConversationStateHandler tempHandler = this._tempHandler;
					if (tempHandler == null)
					{
						return;
					}
					tempHandler.OnConversationUninstall();
				});
			}

			// Token: 0x06000492 RID: 1170 RVA: 0x0002364F File Offset: 0x0002184F
			public void ApplyHandlerChangesTo(IConversationStateHandler newHandler)
			{
				this._tempHandler = newHandler;
				while (this._actionQueue.Count > 0)
				{
					Action action = this._actionQueue.Dequeue();
					if (action != null)
					{
						action();
					}
				}
				this._tempHandler = null;
			}

			// Token: 0x04000323 RID: 803
			private Queue<Action> _actionQueue = new Queue<Action>();

			// Token: 0x04000324 RID: 804
			private IConversationStateHandler _tempHandler;
		}
	}
}
