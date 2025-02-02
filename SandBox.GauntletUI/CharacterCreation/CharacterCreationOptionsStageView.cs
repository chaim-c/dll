using System;
using System.Collections.Generic;
using SandBox.View.CharacterCreation;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using TaleWorlds.CampaignSystem.ViewModelCollection.CharacterCreation.OptionsStage;
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
using TaleWorlds.MountAndBlade.GauntletUI.BodyGenerator;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.ViewModelCollection.EscapeMenu;
using TaleWorlds.ScreenSystem;
using TaleWorlds.TwoDimension;

namespace SandBox.GauntletUI.CharacterCreation
{
	// Token: 0x0200003F RID: 63
	[CharacterCreationStageView(typeof(CharacterCreationOptionsStage))]
	public class CharacterCreationOptionsStageView : CharacterCreationStageViewBase
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00011292 File Offset: 0x0000F492
		// (set) Token: 0x0600025A RID: 602 RVA: 0x0001129A File Offset: 0x0000F49A
		public SceneLayer CharacterLayer { get; private set; }

		// Token: 0x0600025B RID: 603 RVA: 0x000112A4 File Offset: 0x0000F4A4
		public CharacterCreationOptionsStageView(CharacterCreation characterCreation, ControlCharacterCreationStage affirmativeAction, TextObject affirmativeActionText, ControlCharacterCreationStage negativeAction, TextObject negativeActionText, ControlCharacterCreationStage refreshAction, ControlCharacterCreationStageReturnInt getCurrentStageIndexAction, ControlCharacterCreationStageReturnInt getTotalStageCountAction, ControlCharacterCreationStageReturnInt getFurthestIndexAction, ControlCharacterCreationStageWithInt goToIndexAction) : base(affirmativeAction, negativeAction, refreshAction, getCurrentStageIndexAction, getTotalStageCountAction, getFurthestIndexAction, goToIndexAction)
		{
			this._characterCreation = characterCreation;
			this._affirmativeActionText = new TextObject("{=lBQXP6Wj}Start Game", null);
			this._negativeActionText = negativeActionText;
			this.GauntletLayer = new GauntletLayer(1, "GauntletLayer", false);
			this.GauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			this.GauntletLayer.IsFocusLayer = true;
			this.GauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			ScreenManager.TrySetFocus(this.GauntletLayer);
			this._dataSource = new CharacterCreationOptionsStageVM(this._characterCreation, new Action(this.NextStage), this._affirmativeActionText, new Action(this.PreviousStage), this._negativeActionText, getCurrentStageIndexAction(), getTotalStageCountAction(), getFurthestIndexAction(), new Action<int>(this.GoToIndex));
			this._dataSource.SetCancelInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit"));
			this._dataSource.SetDoneInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Confirm"));
			this._movie = this.GauntletLayer.LoadMovie("CharacterCreationOptionsStage", this._dataSource);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000113FC File Offset: 0x0000F5FC
		public override void SetGenericScene(Scene scene)
		{
			this.OpenScene(scene);
			this.AddCharacterEntity();
			this.RefreshMountEntity();
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00011414 File Offset: 0x0000F614
		private void OpenScene(Scene cachedScene)
		{
			this._characterScene = cachedScene;
			this._characterScene.SetShadow(true);
			this._characterScene.SetDynamicShadowmapCascadesRadiusMultiplier(0.1f);
			GameEntity gameEntity = this._characterScene.FindEntityWithName("cradle");
			if (gameEntity != null)
			{
				gameEntity.SetVisibilityExcludeParents(false);
			}
			this._characterScene.SetDoNotWaitForLoadingStatesToRender(true);
			this._characterScene.DisableStaticShadows(true);
			this._camera = Camera.CreateCamera();
			BodyGeneratorView.InitCamera(this._camera, this._cameraPosition);
			this.CharacterLayer = new SceneLayer("SceneLayer", false, true);
			this.CharacterLayer.SetScene(this._characterScene);
			this.CharacterLayer.SetCamera(this._camera);
			this.CharacterLayer.SetSceneUsesShadows(true);
			this.CharacterLayer.SetRenderWithPostfx(true);
			this.CharacterLayer.SetPostfxFromConfig();
			this.CharacterLayer.SceneView.SetResolutionScaling(true);
			int num = -1;
			num &= -5;
			this.CharacterLayer.SetPostfxConfigParams(num);
			this.CharacterLayer.SetPostfxFromConfig();
			if (!this.CharacterLayer.Input.IsCategoryRegistered(HotKeyManager.GetCategory("FaceGenHotkeyCategory")))
			{
				this.CharacterLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("FaceGenHotkeyCategory"));
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00011554 File Offset: 0x0000F754
		private void AddCharacterEntity()
		{
			GameEntity gameEntity = this._characterScene.FindEntityWithTag("spawnpoint_player_1");
			this._initialCharacterFrame = gameEntity.GetFrame();
			this._initialCharacterFrame.origin.z = 0f;
			CharacterObject characterObject = Hero.MainHero.CharacterObject;
			Monster baseMonsterFromRace = TaleWorlds.Core.FaceGen.GetBaseMonsterFromRace(characterObject.Race);
			AgentVisualsData agentVisualsData = new AgentVisualsData().UseMorphAnims(true).Equipment(characterObject.Equipment).BodyProperties(characterObject.GetBodyProperties(characterObject.Equipment, -1)).SkeletonType(characterObject.IsFemale ? SkeletonType.Female : SkeletonType.Male).Frame(this._initialCharacterFrame).ActionSet(MBGlobals.GetActionSetWithSuffix(baseMonsterFromRace, characterObject.IsFemale, "_facegen")).ActionCode(this.act_inventory_idle_start).Scene(this._characterScene).Race(characterObject.Race).Monster(baseMonsterFromRace).PrepareImmediately(true).UseTranslucency(true).UseTesselation(true);
			CharacterCreationContentBase currentCharacterCreationContent = (GameStateManager.Current.ActiveState as CharacterCreationState).CurrentCharacterCreationContent;
			Banner currentPlayerBanner = currentCharacterCreationContent.GetCurrentPlayerBanner();
			CultureObject selectedCulture = currentCharacterCreationContent.GetSelectedCulture();
			if (currentPlayerBanner != null)
			{
				agentVisualsData.ClothColor1(currentPlayerBanner.GetPrimaryColor());
				agentVisualsData.ClothColor2(currentPlayerBanner.GetFirstIconColor());
			}
			else if (currentCharacterCreationContent.GetSelectedCulture() != null)
			{
				agentVisualsData.ClothColor1(selectedCulture.Color);
				agentVisualsData.ClothColor2(selectedCulture.Color2);
			}
			this._agentVisuals = AgentVisuals.Create(agentVisualsData, "facegenvisual", false, false, false);
			this.CharacterLayer.SetFocusedShadowmap(true, ref this._initialCharacterFrame.origin, 0.59999996f);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x000116E0 File Offset: 0x0000F8E0
		private void RefreshCharacterEntityFrame()
		{
			MatrixFrame initialCharacterFrame = this._initialCharacterFrame;
			initialCharacterFrame.rotation.RotateAboutUp(this._charRotationAmount);
			initialCharacterFrame.rotation.ApplyScaleLocal(this._agentVisuals.GetScale());
			this._agentVisuals.GetEntity().SetFrame(ref initialCharacterFrame);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00011730 File Offset: 0x0000F930
		private void RefreshMountEntity()
		{
			this.RemoveMount();
			if (CharacterObject.PlayerCharacter.HasMount())
			{
				FaceGenMount faceGenMount = new FaceGenMount(MountCreationKey.GetRandomMountKey(CharacterObject.PlayerCharacter.Equipment[EquipmentIndex.ArmorItemEndSlot].Item, CharacterObject.PlayerCharacter.GetMountKeySeed()), CharacterObject.PlayerCharacter.Equipment[EquipmentIndex.ArmorItemEndSlot].Item, CharacterObject.PlayerCharacter.Equipment[EquipmentIndex.HorseHarness].Item, "act_inventory_idle_start");
				GameEntity gameEntity = this._characterScene.FindEntityWithTag("spawnpoint_mount_1");
				HorseComponent horseComponent = faceGenMount.HorseItem.HorseComponent;
				Monster monster = horseComponent.Monster;
				this._mountEntity = GameEntity.CreateEmpty(this._characterScene, true);
				AnimationSystemData animationSystemData = monster.FillAnimationSystemData(MBGlobals.GetActionSet(horseComponent.Monster.ActionSetCode), 1f, false);
				this._mountEntity.CreateSkeletonWithActionSet(ref animationSystemData);
				this._mountEntity.Skeleton.SetAgentActionChannel(0, this.act_inventory_idle_start, MBRandom.RandomFloat, -0.2f, true);
				this._mountEntity.EntityFlags |= EntityFlags.AnimateWhenVisible;
				MountVisualCreator.AddMountMeshToEntity(this._mountEntity, faceGenMount.HorseItem, faceGenMount.HarnessItem, faceGenMount.MountKey.ToString(), null);
				MatrixFrame globalFrame = gameEntity.GetGlobalFrame();
				this._mountEntity.SetFrame(ref globalFrame);
				this._agentVisuals.GetVisuals().GetSkeleton().TickAnimationsAndForceUpdate(0.001f, this._initialCharacterFrame, true);
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000118A7 File Offset: 0x0000FAA7
		private void RemoveMount()
		{
			if (this._mountEntity != null)
			{
				this._mountEntity.Remove(117);
			}
			this._mountEntity = null;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x000118CC File Offset: 0x0000FACC
		public override void Tick(float dt)
		{
			base.Tick(dt);
			base.HandleEscapeMenu(this, this.CharacterLayer);
			Scene characterScene = this._characterScene;
			if (characterScene != null)
			{
				characterScene.Tick(dt);
			}
			AgentVisuals agentVisuals = this._agentVisuals;
			if (agentVisuals != null)
			{
				agentVisuals.TickVisuals();
			}
			Vec2 vec = new Vec2(-this.CharacterLayer.Input.GetMouseMoveX(), -this.CharacterLayer.Input.GetMouseMoveY());
			if (this.CharacterLayer.Input.IsHotKeyReleased("Ascend") || this.CharacterLayer.Input.IsHotKeyReleased("Rotate") || this.CharacterLayer.Input.IsHotKeyReleased("Zoom"))
			{
				this.GauntletLayer.InputRestrictions.SetMouseVisibility(true);
			}
			if (this.CharacterLayer.Input.IsHotKeyDown("Rotate"))
			{
				this._charRotationAmount = (this._charRotationAmount - vec.x * 0.5f * dt) % 6.2831855f;
				this.RefreshCharacterEntityFrame();
				MBWindowManager.DontChangeCursorPos();
				this.GauntletLayer.InputRestrictions.SetMouseVisibility(false);
			}
			this.HandleLayerInput();
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000119EC File Offset: 0x0000FBEC
		private void HandleLayerInput()
		{
			if (this.GauntletLayer.Input.IsHotKeyReleased("Exit"))
			{
				UISoundsHelper.PlayUISound("event:/ui/panels/next");
				this._dataSource.OnPreviousStage();
				return;
			}
			if (this.GauntletLayer.Input.IsHotKeyReleased("Confirm") && this._dataSource.CanAdvance)
			{
				UISoundsHelper.PlayUISound("event:/ui/panels/next");
				this._dataSource.OnNextStage();
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00011A60 File Offset: 0x0000FC60
		protected override void OnFinalize()
		{
			base.OnFinalize();
			SpriteData spriteData = UIResourceManager.SpriteData;
			TwoDimensionEngineResourceContext resourceContext = UIResourceManager.ResourceContext;
			ResourceDepot uiresourceDepot = UIResourceManager.UIResourceDepot;
			SpriteCategory spriteCategory = spriteData.SpriteCategories["ui_bannericons"];
			if (spriteCategory.IsLoaded)
			{
				spriteCategory.Unload();
			}
			this.CharacterLayer.SceneView.SetEnable(false);
			this.CharacterLayer.SceneView.ClearAll(false, false);
			this._agentVisuals.Reset();
			this._agentVisuals = null;
			this.GauntletLayer = null;
			CharacterCreationOptionsStageVM dataSource = this._dataSource;
			if (dataSource != null)
			{
				dataSource.OnFinalize();
			}
			this._dataSource = null;
			this.CharacterLayer = null;
			this._characterScene = null;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00011B04 File Offset: 0x0000FD04
		public override IEnumerable<ScreenLayer> GetLayers()
		{
			return new List<ScreenLayer>
			{
				this.CharacterLayer,
				this.GauntletLayer
			};
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00011B23 File Offset: 0x0000FD23
		public override int GetVirtualStageCount()
		{
			return 1;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00011B26 File Offset: 0x0000FD26
		public override void NextStage()
		{
			this._affirmativeAction();
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00011B33 File Offset: 0x0000FD33
		public override void PreviousStage()
		{
			this.RemoveMount();
			this._negativeAction();
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00011B46 File Offset: 0x0000FD46
		public override void LoadEscapeMenuMovie()
		{
			this._escapeMenuDatasource = new EscapeMenuVM(base.GetEscapeMenuItems(this), null);
			this._escapeMenuMovie = this.GauntletLayer.LoadMovie("EscapeMenu", this._escapeMenuDatasource);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00011B77 File Offset: 0x0000FD77
		public override void ReleaseEscapeMenuMovie()
		{
			this.GauntletLayer.ReleaseMovie(this._escapeMenuMovie);
			this._escapeMenuDatasource = null;
			this._escapeMenuMovie = null;
		}

		// Token: 0x04000148 RID: 328
		protected readonly TextObject _affirmativeActionText;

		// Token: 0x04000149 RID: 329
		protected readonly TextObject _negativeActionText;

		// Token: 0x0400014A RID: 330
		private readonly IGauntletMovie _movie;

		// Token: 0x0400014B RID: 331
		private GauntletLayer GauntletLayer;

		// Token: 0x0400014C RID: 332
		private CharacterCreationOptionsStageVM _dataSource;

		// Token: 0x0400014D RID: 333
		private readonly ActionIndexCache act_inventory_idle_start = ActionIndexCache.Create("act_inventory_idle_start");

		// Token: 0x0400014E RID: 334
		private readonly CharacterCreation _characterCreation;

		// Token: 0x0400014F RID: 335
		private Scene _characterScene;

		// Token: 0x04000150 RID: 336
		private Camera _camera;

		// Token: 0x04000151 RID: 337
		private MatrixFrame _initialCharacterFrame;

		// Token: 0x04000152 RID: 338
		private AgentVisuals _agentVisuals;

		// Token: 0x04000153 RID: 339
		private GameEntity _mountEntity;

		// Token: 0x04000155 RID: 341
		private float _charRotationAmount;

		// Token: 0x04000156 RID: 342
		private EscapeMenuVM _escapeMenuDatasource;

		// Token: 0x04000157 RID: 343
		private IGauntletMovie _escapeMenuMovie;
	}
}
