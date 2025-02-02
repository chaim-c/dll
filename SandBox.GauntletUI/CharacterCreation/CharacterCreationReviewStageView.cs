using System;
using System.Collections.Generic;
using System.Linq;
using SandBox.View.CharacterCreation;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using TaleWorlds.CampaignSystem.ViewModelCollection.CharacterCreation;
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

namespace SandBox.GauntletUI.CharacterCreation
{
	// Token: 0x02000040 RID: 64
	[CharacterCreationStageView(typeof(CharacterCreationReviewStage))]
	public class CharacterCreationReviewStageView : CharacterCreationStageViewBase
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00011B98 File Offset: 0x0000FD98
		// (set) Token: 0x0600026C RID: 620 RVA: 0x00011BA0 File Offset: 0x0000FDA0
		public SceneLayer CharacterLayer { get; private set; }

		// Token: 0x0600026D RID: 621 RVA: 0x00011BAC File Offset: 0x0000FDAC
		public CharacterCreationReviewStageView(CharacterCreation characterCreation, ControlCharacterCreationStage affirmativeAction, TextObject affirmativeActionText, ControlCharacterCreationStage negativeAction, TextObject negativeActionText, ControlCharacterCreationStage onRefresh, ControlCharacterCreationStageReturnInt getCurrentStageIndexAction, ControlCharacterCreationStageReturnInt getTotalStageCountAction, ControlCharacterCreationStageReturnInt getFurthestIndexAction, ControlCharacterCreationStageWithInt goToIndexAction) : base(affirmativeAction, negativeAction, onRefresh, getCurrentStageIndexAction, getTotalStageCountAction, getFurthestIndexAction, goToIndexAction)
		{
			this._characterCreation = characterCreation;
			this._affirmativeActionText = new TextObject("{=Rvr1bcu8}Next", null);
			this._negativeActionText = negativeActionText;
			this.GauntletLayer = new GauntletLayer(1, "GauntletLayer", false);
			this.GauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			this.GauntletLayer.IsFocusLayer = true;
			this.GauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			ScreenManager.TrySetFocus(this.GauntletLayer);
			CharacterCreationContentBase currentCharacterCreationContent = (GameStateManager.Current.ActiveState as CharacterCreationState).CurrentCharacterCreationContent;
			bool isBannerAndClanNameSet = currentCharacterCreationContent.CharacterCreationStages.Contains(typeof(CharacterCreationBannerEditorStage)) && currentCharacterCreationContent.CharacterCreationStages.Contains(typeof(CharacterCreationClanNamingStage));
			this._dataSource = new CharacterCreationReviewStageVM(this._characterCreation, new Action(this.NextStage), this._affirmativeActionText, new Action(this.PreviousStage), this._negativeActionText, getCurrentStageIndexAction(), getTotalStageCountAction(), getFurthestIndexAction(), new Action<int>(this.GoToIndex), isBannerAndClanNameSet);
			this._dataSource.SetCancelInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit"));
			this._dataSource.SetDoneInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Confirm"));
			this._movie = this.GauntletLayer.LoadMovie("CharacterCreationReviewStage", this._dataSource);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00011D4A File Offset: 0x0000FF4A
		public override void SetGenericScene(Scene scene)
		{
			this.OpenScene(scene);
			this.AddCharacterEntity();
			this.RefreshMountEntity();
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00011D60 File Offset: 0x0000FF60
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

		// Token: 0x06000270 RID: 624 RVA: 0x00011EA0 File Offset: 0x000100A0
		private void AddCharacterEntity()
		{
			GameEntity gameEntity = this._characterScene.FindEntityWithTag("spawnpoint_player_1");
			this._initialCharacterFrame = gameEntity.GetFrame();
			this._initialCharacterFrame.origin.z = 0f;
			ActionIndexCache actionCode = ActionIndexCache.Create("act_childhood_schooled");
			CharacterObject characterObject = Hero.MainHero.CharacterObject;
			Monster baseMonsterFromRace = TaleWorlds.Core.FaceGen.GetBaseMonsterFromRace(characterObject.Race);
			AgentVisualsData agentVisualsData = new AgentVisualsData().UseMorphAnims(true).Equipment(characterObject.Equipment).BodyProperties(characterObject.GetBodyProperties(characterObject.Equipment, -1)).SkeletonType(characterObject.IsFemale ? SkeletonType.Female : SkeletonType.Male).Frame(this._initialCharacterFrame).ActionSet(MBGlobals.GetActionSetWithSuffix(baseMonsterFromRace, characterObject.IsFemale, "_facegen")).ActionCode(actionCode).Scene(this._characterScene).Race(characterObject.Race).Monster(baseMonsterFromRace).UseTranslucency(true).UseTesselation(true);
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
			this._agentVisuals = AgentVisuals.Create(agentVisualsData, "facegenvisual", false, false, true);
			this.CharacterLayer.SetFocusedShadowmap(true, ref this._initialCharacterFrame.origin, 0.59999996f);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00012030 File Offset: 0x00010230
		private void RefreshCharacterEntityFrame()
		{
			if (this._agentVisuals != null)
			{
				MatrixFrame initialCharacterFrame = this._initialCharacterFrame;
				initialCharacterFrame.rotation.RotateAboutUp(this._charRotationAmount);
				initialCharacterFrame.rotation.ApplyScaleLocal(this._agentVisuals.GetScale());
				this._agentVisuals.GetEntity().SetFrame(ref initialCharacterFrame);
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00012088 File Offset: 0x00010288
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
				this._mountEntity.Skeleton.SetAgentActionChannel(0, this.act_inventory_idle_start, 0f, -0.2f, true);
				this._mountEntity.EntityFlags |= EntityFlags.AnimateWhenVisible;
				MountVisualCreator.AddMountMeshToEntity(this._mountEntity, faceGenMount.HorseItem, faceGenMount.HarnessItem, faceGenMount.MountKey.ToString(), null);
				MatrixFrame globalFrame = gameEntity.GetGlobalFrame();
				this._mountEntity.SetFrame(ref globalFrame);
				this._agentVisuals.GetVisuals().GetSkeleton().TickAnimationsAndForceUpdate(0.001f, this._initialCharacterFrame, true);
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000121FF File Offset: 0x000103FF
		private void RemoveMount()
		{
			if (this._mountEntity != null)
			{
				this._mountEntity.Remove(118);
			}
			this._mountEntity = null;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00012224 File Offset: 0x00010424
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

		// Token: 0x06000275 RID: 629 RVA: 0x00012344 File Offset: 0x00010544
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

		// Token: 0x06000276 RID: 630 RVA: 0x000123B8 File Offset: 0x000105B8
		public override void NextStage()
		{
			TextObject textObject = GameTexts.FindText("str_generic_character_firstname", null);
			textObject.SetTextVariable("CHARACTER_FIRSTNAME", new TextObject(this._dataSource.Name, null));
			TextObject textObject2 = GameTexts.FindText("str_generic_character_name", null);
			textObject2.SetTextVariable("CHARACTER_NAME", new TextObject(this._dataSource.Name, null));
			textObject2.SetTextVariable("CHARACTER_GENDER", Hero.MainHero.IsFemale ? 1 : 0);
			textObject.SetTextVariable("CHARACTER_GENDER", Hero.MainHero.IsFemale ? 1 : 0);
			Hero.MainHero.SetName(textObject2, textObject);
			this.RemoveMount();
			this._affirmativeAction();
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0001246C File Offset: 0x0001066C
		protected override void OnFinalize()
		{
			base.OnFinalize();
			this.CharacterLayer.SceneView.SetEnable(false);
			this.CharacterLayer.SceneView.ClearAll(false, false);
			this._agentVisuals.Reset();
			this._agentVisuals = null;
			this.GauntletLayer = null;
			CharacterCreationReviewStageVM dataSource = this._dataSource;
			if (dataSource != null)
			{
				dataSource.OnFinalize();
			}
			this._dataSource = null;
			this.CharacterLayer = null;
			this._characterScene = null;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000124E1 File Offset: 0x000106E1
		public override int GetVirtualStageCount()
		{
			return 1;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000124E4 File Offset: 0x000106E4
		public override void PreviousStage()
		{
			this.RemoveMount();
			this._negativeAction();
		}

		// Token: 0x0600027A RID: 634 RVA: 0x000124F7 File Offset: 0x000106F7
		public override IEnumerable<ScreenLayer> GetLayers()
		{
			return new List<ScreenLayer>
			{
				this.CharacterLayer,
				this.GauntletLayer
			};
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00012516 File Offset: 0x00010716
		public override void LoadEscapeMenuMovie()
		{
			this._escapeMenuDatasource = new EscapeMenuVM(base.GetEscapeMenuItems(this), null);
			this._escapeMenuMovie = this.GauntletLayer.LoadMovie("EscapeMenu", this._escapeMenuDatasource);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00012547 File Offset: 0x00010747
		public override void ReleaseEscapeMenuMovie()
		{
			this.GauntletLayer.ReleaseMovie(this._escapeMenuMovie);
			this._escapeMenuDatasource = null;
			this._escapeMenuMovie = null;
		}

		// Token: 0x04000158 RID: 344
		protected readonly TextObject _affirmativeActionText;

		// Token: 0x04000159 RID: 345
		protected readonly TextObject _negativeActionText;

		// Token: 0x0400015A RID: 346
		private readonly IGauntletMovie _movie;

		// Token: 0x0400015B RID: 347
		private GauntletLayer GauntletLayer;

		// Token: 0x0400015C RID: 348
		private CharacterCreationReviewStageVM _dataSource;

		// Token: 0x0400015D RID: 349
		private readonly ActionIndexCache act_inventory_idle_start = ActionIndexCache.Create("act_inventory_idle_start");

		// Token: 0x0400015E RID: 350
		private readonly CharacterCreation _characterCreation;

		// Token: 0x0400015F RID: 351
		private Scene _characterScene;

		// Token: 0x04000160 RID: 352
		private Camera _camera;

		// Token: 0x04000161 RID: 353
		private MatrixFrame _initialCharacterFrame;

		// Token: 0x04000162 RID: 354
		private AgentVisuals _agentVisuals;

		// Token: 0x04000163 RID: 355
		private GameEntity _mountEntity;

		// Token: 0x04000164 RID: 356
		private float _charRotationAmount;

		// Token: 0x04000166 RID: 358
		private EscapeMenuVM _escapeMenuDatasource;

		// Token: 0x04000167 RID: 359
		private IGauntletMovie _escapeMenuMovie;
	}
}
