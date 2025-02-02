using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Screens;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.MountAndBlade.View.Tableaus;
using TaleWorlds.MountAndBlade.ViewModelCollection.BannerBuilder;
using TaleWorlds.ObjectSystem;
using TaleWorlds.ScreenSystem;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI
{
	// Token: 0x02000003 RID: 3
	[GameStateScreen(typeof(BannerBuilderState))]
	public class GauntletBannerBuilderScreen : ScreenBase, IGameStateListener
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020DF File Offset: 0x000002DF
		// (set) Token: 0x06000008 RID: 8 RVA: 0x000020D6 File Offset: 0x000002D6
		public SceneLayer SceneLayer { get; private set; }

		// Token: 0x0600000A RID: 10 RVA: 0x000020E7 File Offset: 0x000002E7
		public GauntletBannerBuilderScreen(BannerBuilderState state)
		{
			this._state = state;
			this._character = MBObjectManager.Instance.GetObject<BasicCharacterObject>("main_hero");
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002124 File Offset: 0x00000324
		protected override void OnInitialize()
		{
			base.OnInitialize();
			SpriteData spriteData = UIResourceManager.SpriteData;
			TwoDimensionEngineResourceContext resourceContext = UIResourceManager.ResourceContext;
			ResourceDepot uiresourceDepot = UIResourceManager.UIResourceDepot;
			this._bannerIconsCategory = spriteData.SpriteCategories["ui_bannericons"];
			this._bannerIconsCategory.Load(resourceContext, uiresourceDepot);
			this._bannerBuilderCategory = spriteData.SpriteCategories["ui_bannerbuilder"];
			this._bannerBuilderCategory.Load(resourceContext, uiresourceDepot);
			this._agentVisuals = new AgentVisuals[2];
			string initialKey = string.IsNullOrWhiteSpace(this._state.DefaultBannerKey) ? "11.163.166.1528.1528.764.764.1.0.0.133.171.171.483.483.764.764.0.0.0" : this._state.DefaultBannerKey;
			this._dataSource = new BannerBuilderVM(this._character, initialKey, new Action<bool>(this.Exit), new Action(this.Refresh), new Action(this.CopyBannerCode));
			this._gauntletLayer = new GauntletLayer(100, "GauntletLayer", false);
			this._gauntletLayer.IsFocusLayer = true;
			base.AddLayer(this._gauntletLayer);
			this._gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			ScreenManager.TrySetFocus(this._gauntletLayer);
			this._movie = this._gauntletLayer.LoadMovie("BannerBuilderScreen", this._dataSource);
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("FaceGenHotkeyCategory"));
			this._dataSource.SetCancelInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit"));
			this._dataSource.SetDoneInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Confirm"));
			this.CreateScene();
			base.AddLayer(this.SceneLayer);
			this._checkWhetherAgentVisualIsReady = true;
			this._firstCharacterRender = true;
			this.RefreshShieldAndCharacter();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022F2 File Offset: 0x000004F2
		private void Refresh()
		{
			this.RefreshShieldAndCharacter();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022FC File Offset: 0x000004FC
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			if (this._isFinalized)
			{
				return;
			}
			this.HandleUserInput();
			if (this._isFinalized)
			{
				return;
			}
			this.UpdateCamera(dt);
			SceneLayer sceneLayer = this.SceneLayer;
			if (sceneLayer != null && sceneLayer.ReadyToRender())
			{
				LoadingWindow.DisableGlobalLoadingWindow();
			}
			Scene scene = this._scene;
			if (scene != null)
			{
				scene.Tick(dt);
			}
			if (this._refreshBannersNextFrame)
			{
				this.UpdateBanners();
				this._refreshBannersNextFrame = false;
			}
			if (this._refreshCharacterAndShieldNextFrame)
			{
				this.RefreshShieldAndCharacterAux();
				this._refreshCharacterAndShieldNextFrame = false;
			}
			if (this._checkWhetherAgentVisualIsReady)
			{
				int num = (this._agentVisualToShowIndex + 1) % 2;
				if (this._agentVisuals[this._agentVisualToShowIndex].GetEntity().CheckResources(this._firstCharacterRender, true))
				{
					this._agentVisuals[num].SetVisible(false);
					this._agentVisuals[this._agentVisualToShowIndex].SetVisible(true);
					this._checkWhetherAgentVisualIsReady = false;
					this._firstCharacterRender = false;
					return;
				}
				if (!this._firstCharacterRender)
				{
					this._agentVisuals[num].SetVisible(true);
				}
				this._agentVisuals[this._agentVisualToShowIndex].SetVisible(false);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002414 File Offset: 0x00000614
		private void CreateScene()
		{
			this._scene = Scene.CreateNewScene(true, true, DecalAtlasGroup.Battle, "mono_renderscene");
			this._scene.SetName("BannerBuilderScreen");
			SceneInitializationData sceneInitializationData = default(SceneInitializationData);
			sceneInitializationData.InitPhysicsWorld = false;
			this._scene.Read("banner_editor_scene", ref sceneInitializationData, "");
			this._scene.SetShadow(true);
			this._scene.DisableStaticShadows(true);
			this._scene.SetDynamicShadowmapCascadesRadiusMultiplier(0.1f);
			this._agentRendererSceneController = MBAgentRendererSceneController.CreateNewAgentRendererSceneController(this._scene, 32);
			float aspectRatio = Screen.AspectRatio;
			GameEntity gameEntity = this._scene.FindEntityWithTag("spawnpoint_player");
			this._characterFrame = gameEntity.GetFrame();
			this._characterFrame.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
			this._cameraTargetDistanceAdder = 3.5f;
			this._cameraCurrentDistanceAdder = this._cameraTargetDistanceAdder;
			this._cameraTargetElevationAdder = 1.15f;
			this._cameraCurrentElevationAdder = this._cameraTargetElevationAdder;
			this._camera = Camera.CreateCamera();
			this._camera.SetFovVertical(0.6981317f, aspectRatio, 0.2f, 200f);
			this.SceneLayer = new SceneLayer("SceneLayer", true, true);
			this.SceneLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("FaceGenHotkeyCategory"));
			this.SceneLayer.SetScene(this._scene);
			this.UpdateCamera(0f);
			this.SceneLayer.SetSceneUsesShadows(true);
			this.SceneLayer.SceneView.SetResolutionScaling(true);
			int num = -1;
			num &= -5;
			this.SceneLayer.SetPostfxConfigParams(num);
			this.AddCharacterEntities(this._idleAction);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000025B4 File Offset: 0x000007B4
		private void AddCharacterEntities(ActionIndexCache action)
		{
			this._weaponEquipment = new Equipment();
			for (int i = 0; i < 12; i++)
			{
				EquipmentElement equipmentFromSlot = this._character.Equipment.GetEquipmentFromSlot((EquipmentIndex)i);
				ItemObject item = equipmentFromSlot.Item;
				if (((item != null) ? item.PrimaryWeapon : null) == null || (!equipmentFromSlot.Item.PrimaryWeapon.IsShield && !equipmentFromSlot.Item.ItemFlags.HasAllFlags(ItemFlags.DropOnWeaponChange)))
				{
					this._weaponEquipment.AddEquipmentToSlotWithoutAgent((EquipmentIndex)i, equipmentFromSlot);
				}
			}
			this._weaponEquipment.AddEquipmentToSlotWithoutAgent((EquipmentIndex)this._dataSource.ShieldSlotIndex, this._dataSource.ShieldRosterElement.EquipmentElement);
			Monster baseMonsterFromRace = FaceGen.GetBaseMonsterFromRace(this._character.Race);
			this._agentVisuals[0] = AgentVisuals.Create(new AgentVisualsData().Equipment(this._weaponEquipment).BodyProperties(this._character.GetBodyProperties(this._weaponEquipment, -1)).Frame(this._characterFrame).ActionSet(MBGlobals.GetActionSetWithSuffix(baseMonsterFromRace, this._character.IsFemale, "_facegen")).ActionCode(action).Scene(this._scene).Monster(baseMonsterFromRace).SkeletonType(this._character.IsFemale ? SkeletonType.Female : SkeletonType.Male).Race(this._character.Race).PrepareImmediately(true).RightWieldedItemIndex(-1).LeftWieldedItemIndex(this._dataSource.ShieldSlotIndex).ClothColor1(this._dataSource.CurrentBanner.GetPrimaryColor()).ClothColor2(this._dataSource.CurrentBanner.GetFirstIconColor()).Banner(this._dataSource.CurrentBanner).UseMorphAnims(true), "BannerEditorChar", false, false, true);
			this._agentVisuals[0].SetAgentLodZeroOrMaxExternal(true);
			this._agentVisuals[0].Refresh(false, this._agentVisuals[0].GetCopyAgentVisualsData(), true);
			MissionWeapon shieldWeapon = new MissionWeapon(this._dataSource.ShieldRosterElement.EquipmentElement.Item, this._dataSource.ShieldRosterElement.EquipmentElement.ItemModifier, this._dataSource.CurrentBanner);
			Action<TaleWorlds.Engine.Texture> setAction = delegate(TaleWorlds.Engine.Texture tex)
			{
				shieldWeapon.GetWeaponData(false).TableauMaterial.SetTexture(TaleWorlds.Engine.Material.MBTextureType.DiffuseMap2, tex);
			};
			this._dataSource.CurrentBanner.GetTableauTextureLarge(setAction);
			this._agentVisuals[0].SetVisible(false);
			this._agentVisuals[0].GetEntity().CheckResources(true, true);
			this._agentVisuals[1] = AgentVisuals.Create(new AgentVisualsData().Equipment(this._weaponEquipment).BodyProperties(this._character.GetBodyProperties(this._weaponEquipment, -1)).Frame(this._characterFrame).ActionSet(MBGlobals.GetActionSetWithSuffix(baseMonsterFromRace, this._character.IsFemale, "_facegen")).ActionCode(action).Scene(this._scene).Race(this._character.Race).Monster(baseMonsterFromRace).SkeletonType(this._character.IsFemale ? SkeletonType.Female : SkeletonType.Male).PrepareImmediately(true).RightWieldedItemIndex(-1).LeftWieldedItemIndex(this._dataSource.ShieldSlotIndex).Banner(this._dataSource.CurrentBanner).ClothColor1(this._dataSource.CurrentBanner.GetPrimaryColor()).ClothColor2(this._dataSource.CurrentBanner.GetFirstIconColor()).UseMorphAnims(true), "BannerEditorChar", false, false, true);
			this._agentVisuals[1].SetAgentLodZeroOrMaxExternal(true);
			this._agentVisuals[1].Refresh(false, this._agentVisuals[1].GetCopyAgentVisualsData(), true);
			this._agentVisuals[1].SetVisible(false);
			this._agentVisuals[1].GetEntity().CheckResources(true, true);
			this.UpdateBanners();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002974 File Offset: 0x00000B74
		private void UpdateBanners()
		{
			BannerCode currentBannerCode = BannerCode.CreateFrom(this._dataSource.CurrentBanner);
			this._dataSource.CurrentBanner.GetTableauTextureLarge(delegate(TaleWorlds.Engine.Texture resultTexture)
			{
				this.OnNewBannerReadyForBanners(currentBannerCode, resultTexture);
			});
			if (this._previousBannerCode != null)
			{
				TableauCacheManager tableauCacheManager = TableauCacheManager.Current;
				if (tableauCacheManager != null)
				{
					tableauCacheManager.ForceReleaseBanner(this._previousBannerCode, true, true);
				}
				TableauCacheManager tableauCacheManager2 = TableauCacheManager.Current;
				if (tableauCacheManager2 != null)
				{
					tableauCacheManager2.ForceReleaseBanner(this._previousBannerCode, true, false);
				}
			}
			this._previousBannerCode = BannerCode.CreateFrom(this._dataSource.CurrentBanner);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002A18 File Offset: 0x00000C18
		private void OnNewBannerReadyForBanners(BannerCode bannerCodeOfTexture, TaleWorlds.Engine.Texture newTexture)
		{
			if (!this._isFinalized && this._scene != null && this._currentBannerCode == bannerCodeOfTexture)
			{
				GameEntity gameEntity = this._scene.FindEntityWithTag("banner");
				if (gameEntity != null)
				{
					Mesh firstMesh = gameEntity.GetFirstMesh();
					if (firstMesh != null && this._dataSource.CurrentBanner != null)
					{
						firstMesh.GetMaterial().SetTexture(TaleWorlds.Engine.Material.MBTextureType.DiffuseMap2, newTexture);
					}
				}
				else
				{
					gameEntity = this._scene.FindEntityWithTag("banner_2");
					Mesh firstMesh2 = gameEntity.GetFirstMesh();
					if (firstMesh2 != null && this._dataSource.CurrentBanner != null)
					{
						firstMesh2.GetMaterial().SetTexture(TaleWorlds.Engine.Material.MBTextureType.DiffuseMap2, newTexture);
					}
				}
				this._refreshCharacterAndShieldNextFrame = true;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002ADA File Offset: 0x00000CDA
		protected override void OnFinalize()
		{
			base.OnFinalize();
			this._bannerIconsCategory.Unload();
			this._bannerBuilderCategory.Unload();
			this._dataSource.OnFinalize();
			this._isFinalized = true;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002B0A File Offset: 0x00000D0A
		private void RefreshShieldAndCharacter()
		{
			this._currentBannerCode = BannerCode.CreateFrom(this._dataSource.CurrentBanner);
			this._dataSource.BannerCodeAsString = this._currentBannerCode.Code;
			this._refreshBannersNextFrame = true;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002B40 File Offset: 0x00000D40
		private void RefreshShieldAndCharacterAux()
		{
			int agentVisualToShowIndex = this._agentVisualToShowIndex;
			this._agentVisualToShowIndex = (this._agentVisualToShowIndex + 1) % 2;
			AgentVisualsData copyAgentVisualsData = this._agentVisuals[this._agentVisualToShowIndex].GetCopyAgentVisualsData();
			copyAgentVisualsData.Equipment(this._weaponEquipment).RightWieldedItemIndex(-1).LeftWieldedItemIndex(this._dataSource.ShieldSlotIndex).Banner(this._dataSource.CurrentBanner).Frame(this._characterFrame).BodyProperties(this._character.GetBodyProperties(this._weaponEquipment, -1)).ClothColor1(this._dataSource.CurrentBanner.GetPrimaryColor()).ClothColor2(this._dataSource.CurrentBanner.GetFirstIconColor());
			this._agentVisuals[this._agentVisualToShowIndex].Refresh(false, copyAgentVisualsData, true);
			this._agentVisuals[this._agentVisualToShowIndex].GetEntity().CheckResources(true, true);
			this._agentVisuals[this._agentVisualToShowIndex].GetVisuals().GetSkeleton().TickAnimationsAndForceUpdate(0.001f, this._characterFrame, true);
			this._agentVisuals[this._agentVisualToShowIndex].SetVisible(false);
			this._agentVisuals[this._agentVisualToShowIndex].SetVisible(true);
			this._checkWhetherAgentVisualIsReady = true;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002C7C File Offset: 0x00000E7C
		private void HandleUserInput()
		{
			if (this._gauntletLayer.IsFocusedOnInput())
			{
				return;
			}
			if (this._gauntletLayer.Input.IsHotKeyReleased("Confirm"))
			{
				this._dataSource.ExecuteDone();
				return;
			}
			if (this._gauntletLayer.Input.IsHotKeyReleased("Exit"))
			{
				this._dataSource.ExecuteCancel();
				return;
			}
			if (this.SceneLayer.Input.IsHotKeyReleased("Ascend") || this.SceneLayer.Input.IsHotKeyReleased("Rotate") || this.SceneLayer.Input.IsHotKeyReleased("Zoom"))
			{
				this._gauntletLayer.InputRestrictions.SetMouseVisibility(true);
			}
			Vec2 vec = new Vec2(-this.SceneLayer.Input.GetMouseMoveX(), -this.SceneLayer.Input.GetMouseMoveY());
			if (this.SceneLayer.Input.IsHotKeyDown("Zoom"))
			{
				this._cameraTargetDistanceAdder = MBMath.ClampFloat(this._cameraTargetDistanceAdder + vec.y * 0.002f, 1.5f, 5f);
				MBWindowManager.DontChangeCursorPos();
				this._gauntletLayer.InputRestrictions.SetMouseVisibility(false);
			}
			if (this.SceneLayer.Input.IsHotKeyDown("Rotate"))
			{
				this._cameraTargetRotation = MBMath.WrapAngle(this._cameraTargetRotation - vec.x * 0.004f);
				MBWindowManager.DontChangeCursorPos();
				this._gauntletLayer.InputRestrictions.SetMouseVisibility(false);
			}
			if (this.SceneLayer.Input.IsHotKeyDown("Ascend"))
			{
				this._cameraTargetElevationAdder = MBMath.ClampFloat(this._cameraTargetElevationAdder - vec.y * 0.002f, 0.5f, 1.9f * this._agentVisuals[0].GetScale());
				MBWindowManager.DontChangeCursorPos();
				this._gauntletLayer.InputRestrictions.SetMouseVisibility(false);
			}
			if (this.SceneLayer.Input.GetDeltaMouseScroll() != 0f)
			{
				this._cameraTargetDistanceAdder = MBMath.ClampFloat(this._cameraTargetDistanceAdder - this.SceneLayer.Input.GetDeltaMouseScroll() * 0.001f, 1.5f, 5f);
			}
			if (Input.DebugInput.IsHotKeyPressed("Copy"))
			{
				this.CopyBannerCode();
			}
			if (Input.DebugInput.IsHotKeyPressed("Duplicate"))
			{
				this._dataSource.ExecuteDuplicateCurrentLayer();
			}
			if (Input.DebugInput.IsHotKeyPressed("Paste"))
			{
				this._dataSource.SetBannerCode(Input.GetClipboardText());
				this.RefreshShieldAndCharacter();
			}
			if (Input.DebugInput.IsKeyPressed(InputKey.Delete))
			{
				this._dataSource.DeleteCurrentLayer();
			}
			Vec2 vec2 = new Vec2(0f, 0f);
			if (Input.DebugInput.IsKeyReleased(InputKey.Left))
			{
				vec2.x = -1f;
			}
			else if (Input.DebugInput.IsKeyReleased(InputKey.Right))
			{
				vec2.x = 1f;
			}
			if (Input.DebugInput.IsKeyReleased(InputKey.Down))
			{
				vec2.y = 1f;
			}
			else if (Input.DebugInput.IsKeyReleased(InputKey.Up))
			{
				vec2.y = -1f;
			}
			if (vec2.x != 0f || vec2.y != 0f)
			{
				this._dataSource.TranslateCurrentLayerWith(vec2);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002FD0 File Offset: 0x000011D0
		private void UpdateCamera(float dt)
		{
			this._cameraCurrentRotation += MBMath.WrapAngle(this._cameraTargetRotation - this._cameraCurrentRotation) * MathF.Min(1f, 10f * dt);
			this._cameraCurrentElevationAdder += MBMath.WrapAngle(this._cameraTargetElevationAdder - this._cameraCurrentElevationAdder) * MathF.Min(1f, 10f * dt);
			this._cameraCurrentDistanceAdder += MBMath.WrapAngle(this._cameraTargetDistanceAdder - this._cameraCurrentDistanceAdder) * MathF.Min(1f, 10f * dt);
			MatrixFrame characterFrame = this._characterFrame;
			characterFrame.rotation.RotateAboutUp(this._cameraCurrentRotation);
			characterFrame.origin += this._cameraCurrentElevationAdder * characterFrame.rotation.u + this._cameraCurrentDistanceAdder * characterFrame.rotation.f;
			characterFrame.rotation.RotateAboutSide(-1.5707964f);
			characterFrame.rotation.RotateAboutUp(3.1415927f);
			characterFrame.rotation.RotateAboutForward(0.18849556f);
			this._camera.Frame = characterFrame;
			this.SceneLayer.SetCamera(this._camera);
			SoundManager.SetListenerFrame(characterFrame);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00003127 File Offset: 0x00001327
		private void CopyBannerCode()
		{
			Input.SetClipboardText(this._dataSource.GetBannerCode());
			InformationManager.DisplayMessage(new InformationMessage("Banner code copied to the clipboard."));
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00003148 File Offset: 0x00001348
		public void Exit(bool isCancel)
		{
			MouseManager.ActivateMouseCursor(CursorType.Default);
			GameStateManager.Current.PopState(0);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000315B File Offset: 0x0000135B
		void IGameStateListener.OnActivate()
		{
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000315D File Offset: 0x0000135D
		void IGameStateListener.OnDeactivate()
		{
			this._agentVisuals[0].Reset();
			this._agentVisuals[1].Reset();
			MBAgentRendererSceneController.DestructAgentRendererSceneController(this._scene, this._agentRendererSceneController, false);
			this._agentRendererSceneController = null;
			this._scene = null;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003199 File Offset: 0x00001399
		void IGameStateListener.OnInitialize()
		{
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000319B File Offset: 0x0000139B
		void IGameStateListener.OnFinalize()
		{
		}

		// Token: 0x04000006 RID: 6
		private BannerBuilderVM _dataSource;

		// Token: 0x04000007 RID: 7
		private GauntletLayer _gauntletLayer;

		// Token: 0x04000008 RID: 8
		private IGauntletMovie _movie;

		// Token: 0x04000009 RID: 9
		private SpriteCategory _bannerIconsCategory;

		// Token: 0x0400000A RID: 10
		private SpriteCategory _bannerBuilderCategory;

		// Token: 0x0400000B RID: 11
		private BannerBuilderState _state;

		// Token: 0x0400000C RID: 12
		private bool _isFinalized;

		// Token: 0x0400000D RID: 13
		private Camera _camera;

		// Token: 0x0400000E RID: 14
		private AgentVisuals[] _agentVisuals;

		// Token: 0x0400000F RID: 15
		private readonly ActionIndexCache _idleAction = ActionIndexCache.Create("act_walk_idle_1h_with_shield_left_stance");

		// Token: 0x04000010 RID: 16
		private Scene _scene;

		// Token: 0x04000011 RID: 17
		private MBAgentRendererSceneController _agentRendererSceneController;

		// Token: 0x04000012 RID: 18
		private MatrixFrame _characterFrame;

		// Token: 0x04000013 RID: 19
		private Equipment _weaponEquipment;

		// Token: 0x04000014 RID: 20
		private BannerCode _currentBannerCode;

		// Token: 0x04000015 RID: 21
		private float _cameraCurrentRotation;

		// Token: 0x04000016 RID: 22
		private float _cameraTargetRotation;

		// Token: 0x04000017 RID: 23
		private float _cameraCurrentDistanceAdder;

		// Token: 0x04000018 RID: 24
		private float _cameraTargetDistanceAdder;

		// Token: 0x04000019 RID: 25
		private float _cameraCurrentElevationAdder;

		// Token: 0x0400001A RID: 26
		private float _cameraTargetElevationAdder;

		// Token: 0x0400001B RID: 27
		private int _agentVisualToShowIndex;

		// Token: 0x0400001C RID: 28
		private bool _refreshCharacterAndShieldNextFrame;

		// Token: 0x0400001D RID: 29
		private bool _refreshBannersNextFrame;

		// Token: 0x0400001E RID: 30
		private bool _checkWhetherAgentVisualIsReady;

		// Token: 0x0400001F RID: 31
		private bool _firstCharacterRender = true;

		// Token: 0x04000020 RID: 32
		private BannerCode _previousBannerCode;

		// Token: 0x04000021 RID: 33
		private BasicCharacterObject _character;

		// Token: 0x04000022 RID: 34
		private const string DefaultBannerKey = "11.163.166.1528.1528.764.764.1.0.0.133.171.171.483.483.764.764.0.0.0";
	}
}
