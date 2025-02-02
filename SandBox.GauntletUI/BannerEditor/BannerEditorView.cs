using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Party.PartyComponents;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Screens;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.Tableaus;
using TaleWorlds.ScreenSystem;
using TaleWorlds.TwoDimension;

namespace SandBox.GauntletUI.BannerEditor
{
	// Token: 0x02000041 RID: 65
	public class BannerEditorView
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600027D RID: 637 RVA: 0x00012568 File Offset: 0x00010768
		// (set) Token: 0x0600027E RID: 638 RVA: 0x00012570 File Offset: 0x00010770
		public GauntletLayer GauntletLayer { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600027F RID: 639 RVA: 0x00012579 File Offset: 0x00010779
		// (set) Token: 0x06000280 RID: 640 RVA: 0x00012581 File Offset: 0x00010781
		public BannerEditorVM DataSource { get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0001258A File Offset: 0x0001078A
		// (set) Token: 0x06000282 RID: 642 RVA: 0x00012592 File Offset: 0x00010792
		public Banner Banner { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0001259B File Offset: 0x0001079B
		private ItemRosterElement ShieldRosterElement
		{
			get
			{
				return this.DataSource.ShieldRosterElement;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000284 RID: 644 RVA: 0x000125A8 File Offset: 0x000107A8
		private int ShieldSlotIndex
		{
			get
			{
				return this.DataSource.ShieldSlotIndex;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000286 RID: 646 RVA: 0x000125BE File Offset: 0x000107BE
		// (set) Token: 0x06000285 RID: 645 RVA: 0x000125B5 File Offset: 0x000107B5
		public SceneLayer SceneLayer { get; private set; }

		// Token: 0x06000287 RID: 647 RVA: 0x000125C8 File Offset: 0x000107C8
		public BannerEditorView(BasicCharacterObject character, Banner banner, ControlCharacterCreationStage affirmativeAction, TextObject affirmativeActionText, ControlCharacterCreationStage negativeAction, TextObject negativeActionText, ControlCharacterCreationStage onRefresh = null, ControlCharacterCreationStageReturnInt getCurrentStageIndexAction = null, ControlCharacterCreationStageReturnInt getTotalStageCountAction = null, ControlCharacterCreationStageReturnInt getFurthestIndexAction = null, ControlCharacterCreationStageWithInt goToIndexAction = null)
		{
			SpriteData spriteData = UIResourceManager.SpriteData;
			TwoDimensionEngineResourceContext resourceContext = UIResourceManager.ResourceContext;
			ResourceDepot uiresourceDepot = UIResourceManager.UIResourceDepot;
			this._spriteCategory = spriteData.SpriteCategories["ui_bannericons"];
			this._spriteCategory.Load(resourceContext, uiresourceDepot);
			this._character = character;
			this.Banner = banner;
			this._goToIndexAction = goToIndexAction;
			if (getCurrentStageIndexAction == null || getTotalStageCountAction == null || getFurthestIndexAction == null)
			{
				this.DataSource = new BannerEditorVM(this._character, this.Banner, new Action<bool>(this.Exit), new Action(this.RefreshShieldAndCharacter), 0, 0, 0, new Action<int>(this.GoToIndex));
				this.DataSource.Description = new TextObject("{=3ZO5cMLu}Customize your banner's sigil", null).ToString();
				this._isOpenedFromCharacterCreation = true;
			}
			else
			{
				this.DataSource = new BannerEditorVM(this._character, this.Banner, new Action<bool>(this.Exit), new Action(this.RefreshShieldAndCharacter), getCurrentStageIndexAction(), getTotalStageCountAction(), getFurthestIndexAction(), new Action<int>(this.GoToIndex));
				this.DataSource.Description = new TextObject("{=312lNJTM}Customize your personal banner by choosing your clan's sigil", null).ToString();
				this._isOpenedFromCharacterCreation = false;
			}
			this.DataSource.DoneText = affirmativeActionText.ToString();
			this.DataSource.CancelText = negativeActionText.ToString();
			this.GauntletLayer = new GauntletLayer(1, "GauntletLayer", false);
			this._gauntletmovie = this.GauntletLayer.LoadMovie("BannerEditor", this.DataSource);
			this.GauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			this.GauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("FaceGenHotkeyCategory"));
			this.GauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			this.GauntletLayer.IsFocusLayer = true;
			ScreenManager.TrySetFocus(this.GauntletLayer);
			this.DataSource.SetCancelInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit"));
			this.DataSource.SetDoneInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Confirm"));
			this._affirmativeAction = affirmativeAction;
			this._negativeAction = negativeAction;
			this._agentVisuals = new AgentVisuals[2];
			this._currentBannerCode = BannerCode.CreateFrom(this.Banner);
			this.CreateScene();
			Input.ClearKeys();
			this._weaponEquipment.AddEquipmentToSlotWithoutAgent((EquipmentIndex)this.ShieldSlotIndex, this.ShieldRosterElement.EquipmentElement);
			AgentVisualsData copyAgentVisualsData = this._agentVisuals[0].GetCopyAgentVisualsData();
			copyAgentVisualsData.Equipment(this._weaponEquipment).RightWieldedItemIndex(-1).LeftWieldedItemIndex(this.ShieldSlotIndex).Banner(this.Banner).ClothColor1(this.Banner.GetPrimaryColor()).ClothColor2(this.Banner.GetFirstIconColor());
			this._agentVisuals[0].Refresh(false, copyAgentVisualsData, true);
			MissionWeapon shieldWeapon = new MissionWeapon(this.ShieldRosterElement.EquipmentElement.Item, this.ShieldRosterElement.EquipmentElement.ItemModifier, this.Banner);
			Action<TaleWorlds.Engine.Texture> setAction = delegate(TaleWorlds.Engine.Texture tex)
			{
				shieldWeapon.GetWeaponData(false).TableauMaterial.SetTexture(TaleWorlds.Engine.Material.MBTextureType.DiffuseMap2, tex);
			};
			this.Banner.GetTableauTextureLarge(setAction);
			this._agentVisuals[0].SetVisible(false);
			this._agentVisuals[0].GetEntity().CheckResources(true, true);
			AgentVisualsData copyAgentVisualsData2 = this._agentVisuals[1].GetCopyAgentVisualsData();
			copyAgentVisualsData2.Equipment(this._weaponEquipment).RightWieldedItemIndex(-1).LeftWieldedItemIndex(this.ShieldSlotIndex).Banner(this.Banner).ClothColor1(this.Banner.GetPrimaryColor()).ClothColor2(this.Banner.GetFirstIconColor());
			this._agentVisuals[1].Refresh(false, copyAgentVisualsData2, true);
			this._agentVisuals[1].SetVisible(false);
			this._agentVisuals[1].GetEntity().CheckResources(true, true);
			this._checkWhetherAgentVisualIsReady = true;
			this._firstCharacterRender = true;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000129E8 File Offset: 0x00010BE8
		public void OnTick(float dt)
		{
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

		// Token: 0x06000289 RID: 649 RVA: 0x00012AF8 File Offset: 0x00010CF8
		public void OnFinalize()
		{
			if (!this._isOpenedFromCharacterCreation)
			{
				this._spriteCategory.Unload();
			}
			BannerEditorVM dataSource = this.DataSource;
			if (dataSource != null)
			{
				dataSource.OnFinalize();
			}
			this._isFinalized = true;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00012B25 File Offset: 0x00010D25
		public void Exit(bool isCancel)
		{
			MouseManager.ActivateMouseCursor(CursorType.Default);
			this._gauntletmovie = null;
			if (isCancel)
			{
				this._negativeAction();
				return;
			}
			this.SetMapIconAsDirtyForAllPlayerClanParties();
			this._affirmativeAction();
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00012B54 File Offset: 0x00010D54
		private void SetMapIconAsDirtyForAllPlayerClanParties()
		{
			foreach (Hero hero in Clan.PlayerClan.Lords)
			{
				foreach (CaravanPartyComponent caravanPartyComponent in hero.OwnedCaravans)
				{
					PartyBase party = caravanPartyComponent.MobileParty.Party;
					if (party != null)
					{
						party.SetVisualAsDirty();
					}
				}
			}
			foreach (Hero hero2 in Clan.PlayerClan.Companions)
			{
				foreach (CaravanPartyComponent caravanPartyComponent2 in hero2.OwnedCaravans)
				{
					PartyBase party2 = caravanPartyComponent2.MobileParty.Party;
					if (party2 != null)
					{
						party2.SetVisualAsDirty();
					}
				}
			}
			foreach (WarPartyComponent warPartyComponent in Clan.PlayerClan.WarPartyComponents)
			{
				PartyBase party3 = warPartyComponent.MobileParty.Party;
				if (party3 != null)
				{
					party3.SetVisualAsDirty();
				}
			}
			foreach (Settlement settlement in Clan.PlayerClan.Settlements)
			{
				if (settlement.IsVillage && settlement.Village.VillagerPartyComponent != null)
				{
					PartyBase party4 = settlement.Village.VillagerPartyComponent.MobileParty.Party;
					if (party4 != null)
					{
						party4.SetVisualAsDirty();
					}
				}
				else if ((settlement.IsCastle || settlement.IsTown) && settlement.Town.GarrisonParty != null)
				{
					PartyBase party5 = settlement.Town.GarrisonParty.Party;
					if (party5 != null)
					{
						party5.SetVisualAsDirty();
					}
				}
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00012D8C File Offset: 0x00010F8C
		private void CreateScene()
		{
			this._scene = Scene.CreateNewScene(true, true, DecalAtlasGroup.Battle, "mono_renderscene");
			this._scene.SetName("MBBannerEditorScreen");
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
			this.AddCharacterEntity(this._idleAction);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00012F2C File Offset: 0x0001112C
		private void AddCharacterEntity(ActionIndexCache action)
		{
			this._weaponEquipment = new Equipment();
			int i = 0;
			while (i < 12)
			{
				EquipmentElement equipmentFromSlot = this._character.Equipment.GetEquipmentFromSlot((EquipmentIndex)i);
				ItemObject item = equipmentFromSlot.Item;
				if (((item != null) ? item.PrimaryWeapon : null) == null)
				{
					goto IL_76;
				}
				ItemObject item2 = equipmentFromSlot.Item;
				if (((item2 != null) ? item2.PrimaryWeapon : null) != null && !equipmentFromSlot.Item.PrimaryWeapon.IsShield && !equipmentFromSlot.Item.ItemFlags.HasAllFlags(ItemFlags.DropOnWeaponChange))
				{
					goto IL_76;
				}
				IL_83:
				i++;
				continue;
				IL_76:
				this._weaponEquipment.AddEquipmentToSlotWithoutAgent((EquipmentIndex)i, equipmentFromSlot);
				goto IL_83;
			}
			Monster baseMonsterFromRace = TaleWorlds.Core.FaceGen.GetBaseMonsterFromRace(this._character.Race);
			this._agentVisuals[0] = AgentVisuals.Create(new AgentVisualsData().Equipment(this._weaponEquipment).BodyProperties(this._character.GetBodyProperties(this._weaponEquipment, -1)).Frame(this._characterFrame).ActionSet(MBGlobals.GetActionSetWithSuffix(baseMonsterFromRace, this._character.IsFemale, "_facegen")).ActionCode(action).Scene(this._scene).Monster(baseMonsterFromRace).SkeletonType(this._character.IsFemale ? SkeletonType.Female : SkeletonType.Male).Race(this._character.Race).PrepareImmediately(true).UseMorphAnims(true), "BannerEditorChar", false, false, true);
			this._agentVisuals[0].SetAgentLodZeroOrMaxExternal(true);
			this._agentVisuals[0].GetEntity().CheckResources(true, true);
			this._agentVisuals[1] = AgentVisuals.Create(new AgentVisualsData().Equipment(this._weaponEquipment).BodyProperties(this._character.GetBodyProperties(this._weaponEquipment, -1)).Frame(this._characterFrame).ActionSet(MBGlobals.GetActionSetWithSuffix(baseMonsterFromRace, this._character.IsFemale, "_facegen")).ActionCode(action).Scene(this._scene).Race(this._character.Race).Monster(baseMonsterFromRace).SkeletonType(this._character.IsFemale ? SkeletonType.Female : SkeletonType.Male).PrepareImmediately(true).UseMorphAnims(true), "BannerEditorChar", false, false, true);
			this._agentVisuals[1].SetAgentLodZeroOrMaxExternal(true);
			this._agentVisuals[1].GetEntity().CheckResources(true, true);
			this.UpdateBanners();
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00013178 File Offset: 0x00011378
		private void UpdateBanners()
		{
			BannerCode currentBannerCode = BannerCode.CreateFrom(this.Banner);
			this.Banner.GetTableauTextureLarge(delegate(TaleWorlds.Engine.Texture resultTexture)
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
			this._previousBannerCode = BannerCode.CreateFrom(this.Banner);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0001320C File Offset: 0x0001140C
		private void OnNewBannerReadyForBanners(BannerCode bannerCodeOfTexture, TaleWorlds.Engine.Texture newTexture)
		{
			if (!this._isFinalized && this._scene != null && this._currentBannerCode == bannerCodeOfTexture)
			{
				GameEntity gameEntity = this._scene.FindEntityWithTag("banner");
				if (gameEntity != null)
				{
					Mesh firstMesh = gameEntity.GetFirstMesh();
					if (firstMesh != null && this.Banner != null)
					{
						firstMesh.GetMaterial().SetTexture(TaleWorlds.Engine.Material.MBTextureType.DiffuseMap2, newTexture);
					}
				}
				else
				{
					gameEntity = this._scene.FindEntityWithTag("banner_2");
					Mesh firstMesh2 = gameEntity.GetFirstMesh();
					if (firstMesh2 != null && this.Banner != null)
					{
						firstMesh2.GetMaterial().SetTexture(TaleWorlds.Engine.Material.MBTextureType.DiffuseMap2, newTexture);
					}
				}
				this._refreshCharacterAndShieldNextFrame = true;
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x000132C1 File Offset: 0x000114C1
		private void RefreshShieldAndCharacter()
		{
			this._currentBannerCode = BannerCode.CreateFrom(this.Banner);
			this._refreshBannersNextFrame = true;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x000132DC File Offset: 0x000114DC
		private void RefreshShieldAndCharacterAux()
		{
			int agentVisualToShowIndex = this._agentVisualToShowIndex;
			this._agentVisualToShowIndex = (this._agentVisualToShowIndex + 1) % 2;
			AgentVisualsData copyAgentVisualsData = this._agentVisuals[this._agentVisualToShowIndex].GetCopyAgentVisualsData();
			copyAgentVisualsData.Equipment(this._weaponEquipment).RightWieldedItemIndex(-1).LeftWieldedItemIndex(this.ShieldSlotIndex).Banner(this.Banner).Frame(this._characterFrame).BodyProperties(this._character.GetBodyProperties(this._weaponEquipment, -1)).ClothColor1(this.Banner.GetPrimaryColor()).ClothColor2(this.Banner.GetFirstIconColor());
			this._agentVisuals[this._agentVisualToShowIndex].Refresh(false, copyAgentVisualsData, true);
			this._agentVisuals[this._agentVisualToShowIndex].GetEntity().CheckResources(true, true);
			this._agentVisuals[this._agentVisualToShowIndex].GetVisuals().GetSkeleton().TickAnimationsAndForceUpdate(0.001f, this._characterFrame, true);
			this._agentVisuals[this._agentVisualToShowIndex].SetVisible(false);
			this._agentVisuals[this._agentVisualToShowIndex].SetVisible(true);
			this._checkWhetherAgentVisualIsReady = true;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00013404 File Offset: 0x00011604
		private void HandleUserInput()
		{
			if (this.GauntletLayer.Input.IsHotKeyReleased("Confirm"))
			{
				this.DataSource.ExecuteDone();
				UISoundsHelper.PlayUISound("event:/ui/panels/next");
				return;
			}
			if (this.GauntletLayer.Input.IsHotKeyReleased("Exit"))
			{
				this.DataSource.ExecuteCancel();
				UISoundsHelper.PlayUISound("event:/ui/panels/next");
				return;
			}
			if (this.SceneLayer.Input.IsHotKeyReleased("Ascend") || this.SceneLayer.Input.IsHotKeyReleased("Rotate") || this.SceneLayer.Input.IsHotKeyReleased("Zoom"))
			{
				this.GauntletLayer.InputRestrictions.SetMouseVisibility(true);
			}
			Vec2 vec = new Vec2(-this.SceneLayer.Input.GetMouseMoveX(), -this.SceneLayer.Input.GetMouseMoveY());
			if (this.SceneLayer.Input.IsHotKeyDown("Zoom"))
			{
				this._cameraTargetDistanceAdder = MBMath.ClampFloat(this._cameraTargetDistanceAdder + vec.y * 0.002f, 1.5f, 5f);
				MBWindowManager.DontChangeCursorPos();
				this.GauntletLayer.InputRestrictions.SetMouseVisibility(false);
			}
			if (this.SceneLayer.Input.IsHotKeyDown("Rotate"))
			{
				this._cameraTargetRotation = MBMath.WrapAngle(this._cameraTargetRotation - vec.x * 0.004f);
				MBWindowManager.DontChangeCursorPos();
				this.GauntletLayer.InputRestrictions.SetMouseVisibility(false);
			}
			if (this.SceneLayer.Input.IsHotKeyDown("Ascend"))
			{
				this._cameraTargetElevationAdder = MBMath.ClampFloat(this._cameraTargetElevationAdder - vec.y * 0.002f, 0.5f, 1.9f * this._agentVisuals[0].GetScale());
				MBWindowManager.DontChangeCursorPos();
				this.GauntletLayer.InputRestrictions.SetMouseVisibility(false);
			}
			if (this.SceneLayer.Input.GetDeltaMouseScroll() != 0f)
			{
				this._cameraTargetDistanceAdder = MBMath.ClampFloat(this._cameraTargetDistanceAdder - this.SceneLayer.Input.GetDeltaMouseScroll() * 0.001f, 1.5f, 5f);
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00013638 File Offset: 0x00011838
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
			characterFrame.rotation.RotateAboutForward(-0.18849556f);
			this._camera.Frame = characterFrame;
			this.SceneLayer.SetCamera(this._camera);
			SoundManager.SetListenerFrame(characterFrame);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00013790 File Offset: 0x00011990
		public void OnDeactivate()
		{
			this._agentVisuals[0].Reset();
			this._agentVisuals[1].Reset();
			MBAgentRendererSceneController.DestructAgentRendererSceneController(this._scene, this._agentRendererSceneController, false);
			this._agentRendererSceneController = null;
			this._scene.ClearAll();
			this._scene = null;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x000137E2 File Offset: 0x000119E2
		public void GoToIndex(int index)
		{
			this._goToIndexAction(index);
		}

		// Token: 0x0400016B RID: 363
		private IGauntletMovie _gauntletmovie;

		// Token: 0x0400016C RID: 364
		private readonly SpriteCategory _spriteCategory;

		// Token: 0x0400016D RID: 365
		private bool _isFinalized;

		// Token: 0x0400016E RID: 366
		private float _cameraCurrentRotation;

		// Token: 0x0400016F RID: 367
		private float _cameraTargetRotation;

		// Token: 0x04000170 RID: 368
		private float _cameraCurrentDistanceAdder;

		// Token: 0x04000171 RID: 369
		private float _cameraTargetDistanceAdder;

		// Token: 0x04000172 RID: 370
		private float _cameraCurrentElevationAdder;

		// Token: 0x04000173 RID: 371
		private float _cameraTargetElevationAdder;

		// Token: 0x04000174 RID: 372
		private readonly BasicCharacterObject _character;

		// Token: 0x04000175 RID: 373
		private readonly ActionIndexCache _idleAction = ActionIndexCache.Create("act_walk_idle_1h_with_shield_left_stance");

		// Token: 0x04000176 RID: 374
		private Scene _scene;

		// Token: 0x04000177 RID: 375
		private MBAgentRendererSceneController _agentRendererSceneController;

		// Token: 0x04000178 RID: 376
		private AgentVisuals[] _agentVisuals;

		// Token: 0x04000179 RID: 377
		private int _agentVisualToShowIndex;

		// Token: 0x0400017A RID: 378
		private bool _checkWhetherAgentVisualIsReady;

		// Token: 0x0400017B RID: 379
		private bool _firstCharacterRender = true;

		// Token: 0x0400017C RID: 380
		private bool _refreshBannersNextFrame;

		// Token: 0x0400017D RID: 381
		private bool _refreshCharacterAndShieldNextFrame;

		// Token: 0x0400017E RID: 382
		private BannerCode _previousBannerCode;

		// Token: 0x0400017F RID: 383
		private MatrixFrame _characterFrame;

		// Token: 0x04000180 RID: 384
		private Equipment _weaponEquipment;

		// Token: 0x04000181 RID: 385
		private BannerCode _currentBannerCode;

		// Token: 0x04000182 RID: 386
		private Camera _camera;

		// Token: 0x04000184 RID: 388
		private bool _isOpenedFromCharacterCreation;

		// Token: 0x04000185 RID: 389
		private ControlCharacterCreationStage _affirmativeAction;

		// Token: 0x04000186 RID: 390
		private ControlCharacterCreationStage _negativeAction;

		// Token: 0x04000187 RID: 391
		private ControlCharacterCreationStageWithInt _goToIndexAction;
	}
}
