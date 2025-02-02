using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Screens;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.ViewModelCollection.FaceGenerator;
using TaleWorlds.ObjectSystem;
using TaleWorlds.ScreenSystem;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.BodyGenerator
{
	// Token: 0x02000036 RID: 54
	public class BodyGeneratorView : IFaceGeneratorHandler
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000F68E File Offset: 0x0000D88E
		private IInputContext DebugInput
		{
			get
			{
				return Input.DebugInput;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000F695 File Offset: 0x0000D895
		// (set) Token: 0x06000276 RID: 630 RVA: 0x0000F69D File Offset: 0x0000D89D
		public FaceGenVM DataSource { get; private set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000F6A6 File Offset: 0x0000D8A6
		// (set) Token: 0x06000278 RID: 632 RVA: 0x0000F6AE File Offset: 0x0000D8AE
		public GauntletLayer GauntletLayer { get; private set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000F6B7 File Offset: 0x0000D8B7
		// (set) Token: 0x0600027A RID: 634 RVA: 0x0000F6BF File Offset: 0x0000D8BF
		public SceneLayer SceneLayer { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000F6C8 File Offset: 0x0000D8C8
		// (set) Token: 0x0600027C RID: 636 RVA: 0x0000F6D0 File Offset: 0x0000D8D0
		public BodyGenerator BodyGen { get; private set; }

		// Token: 0x0600027D RID: 637 RVA: 0x0000F6DC File Offset: 0x0000D8DC
		public BodyGeneratorView(ControlCharacterCreationStage affirmativeAction, TextObject affirmativeActionText, ControlCharacterCreationStage negativeAction, TextObject negativeActionText, BasicCharacterObject character, bool openedFromMultiplayer, IFaceGeneratorCustomFilter filter, Equipment dressedEquipment = null, ControlCharacterCreationStageReturnInt getCurrentStageIndexAction = null, ControlCharacterCreationStageReturnInt getTotalStageCountAction = null, ControlCharacterCreationStageReturnInt getFurthestIndexAction = null, ControlCharacterCreationStageWithInt goToIndexAction = null)
		{
			this._affirmativeAction = affirmativeAction;
			this._negativeAction = negativeAction;
			this._getCurrentStageIndexAction = getCurrentStageIndexAction;
			this._getTotalStageCountAction = getTotalStageCountAction;
			this._getFurthestIndexAction = getFurthestIndexAction;
			this._goToIndexAction = goToIndexAction;
			this._openedFromMultiplayer = openedFromMultiplayer;
			this.BodyGen = new BodyGenerator(character);
			this._dressedEquipment = (dressedEquipment ?? this.BodyGen.Character.Equipment.Clone(false));
			if (!this._dressedEquipment[EquipmentIndex.ExtraWeaponSlot].IsEmpty && this._dressedEquipment[EquipmentIndex.ExtraWeaponSlot].Item.IsBannerItem)
			{
				this._dressedEquipment[EquipmentIndex.ExtraWeaponSlot] = EquipmentElement.Invalid;
			}
			FaceGenerationParams faceGenerationParams = this.BodyGen.InitBodyGenerator(false);
			faceGenerationParams.UseCache = true;
			faceGenerationParams.UseGpuMorph = true;
			this.SkeletonType = (this.BodyGen.IsFemale ? SkeletonType.Female : SkeletonType.Male);
			SpriteData spriteData = UIResourceManager.SpriteData;
			TwoDimensionEngineResourceContext resourceContext = UIResourceManager.ResourceContext;
			ResourceDepot uiresourceDepot = UIResourceManager.UIResourceDepot;
			this._facegenCategory = spriteData.SpriteCategories["ui_facegen"];
			this._facegenCategory.Load(resourceContext, uiresourceDepot);
			this.OpenScene();
			this.AddCharacterEntity();
			bool openedFromMultiplayer2 = this._openedFromMultiplayer;
			if (this._getCurrentStageIndexAction == null || this._getTotalStageCountAction == null || this._getFurthestIndexAction == null)
			{
				this.DataSource = new FaceGenVM(this.BodyGen, this, new Action<float>(this.OnHeightChanged), new Action(this.OnAgeChanged), affirmativeActionText, negativeActionText, 0, 0, 0, new Action<int>(this.GoToIndex), openedFromMultiplayer2, openedFromMultiplayer, filter);
			}
			else
			{
				this.DataSource = new FaceGenVM(this.BodyGen, this, new Action<float>(this.OnHeightChanged), new Action(this.OnAgeChanged), affirmativeActionText, negativeActionText, this._getCurrentStageIndexAction(), this._getTotalStageCountAction(), this._getFurthestIndexAction(), new Action<int>(this.GoToIndex), true, openedFromMultiplayer, filter);
			}
			this.DataSource.SetPreviousTabInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("SwitchToPreviousTab"));
			this.DataSource.SetNextTabInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("SwitchToNextTab"));
			this.DataSource.SetCancelInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit"));
			this.DataSource.SetDoneInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Confirm"));
			this.DataSource.AddCameraControlInputKey(HotKeyManager.GetCategory("FaceGenHotkeyCategory").GetGameKey(55));
			this.DataSource.AddCameraControlInputKey(HotKeyManager.GetCategory("FaceGenHotkeyCategory").GetGameKey(56));
			this.DataSource.AddCameraControlInputKey(HotKeyManager.GetCategory("FaceGenHotkeyCategory").RegisteredGameAxisKeys.FirstOrDefault((GameAxisKey x) => x.Id == "CameraAxisX"));
			this.DataSource.AddCameraControlInputKey(HotKeyManager.GetCategory("FaceGenHotkeyCategory").RegisteredGameAxisKeys.FirstOrDefault((GameAxisKey x) => x.Id == "CameraAxisY"));
			this.DataSource.SetFaceGenerationParams(faceGenerationParams);
			this.DataSource.Refresh(true);
			this.GauntletLayer = new GauntletLayer(1, "GauntletLayer", false);
			this.GauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			this.GauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("Generic"));
			this.GauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			this.GauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("FaceGenHotkeyCategory"));
			this.GauntletLayer.InputRestrictions.SetCanOverrideFocusOnHit(true);
			this.GauntletLayer.IsFocusLayer = true;
			ScreenManager.TrySetFocus(this.GauntletLayer);
			this._viewMovie = this.GauntletLayer.LoadMovie("FaceGen", this.DataSource);
			if (!this._openedFromMultiplayer)
			{
				this._templateBodyProperties = new List<BodyProperties>();
				this._templateBodyProperties.Add(MBObjectManager.Instance.GetObject<BasicCharacterObject>("facgen_template_test_char_0").GetBodyProperties(null, -1));
				this._templateBodyProperties.Add(MBObjectManager.Instance.GetObject<BasicCharacterObject>("facgen_template_test_char_1").GetBodyProperties(null, -1));
				this._templateBodyProperties.Add(MBObjectManager.Instance.GetObject<BasicCharacterObject>("facgen_template_test_char_2").GetBodyProperties(null, -1));
				this._templateBodyProperties.Add(MBObjectManager.Instance.GetObject<BasicCharacterObject>("facgen_template_test_char_3").GetBodyProperties(null, -1));
				this._templateBodyProperties.Add(MBObjectManager.Instance.GetObject<BasicCharacterObject>("facgen_template_test_char_4").GetBodyProperties(null, -1));
				this._templateBodyProperties.Add(MBObjectManager.Instance.GetObject<BasicCharacterObject>("facgen_template_test_char_5").GetBodyProperties(null, -1));
				this._templateBodyProperties.Add(MBObjectManager.Instance.GetObject<BasicCharacterObject>("facgen_template_test_char_6").GetBodyProperties(null, -1));
				this._templateBodyProperties.Add(MBObjectManager.Instance.GetObject<BasicCharacterObject>("facgen_template_test_char_7").GetBodyProperties(null, -1));
				this._templateBodyProperties.Add(MBObjectManager.Instance.GetObject<BasicCharacterObject>("facgen_template_test_char_8").GetBodyProperties(null, -1));
				this._templateBodyProperties.Add(MBObjectManager.Instance.GetObject<BasicCharacterObject>("facgen_template_test_char_9").GetBodyProperties(null, -1));
			}
			((IFaceGeneratorHandler)this).RefreshCharacterEntity();
			this.SceneLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("Generic"));
			this.SceneLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			this.SceneLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("FaceGenHotkeyCategory"));
			this.DataSource.SelectedGender = (this.BodyGen.IsFemale ? 1 : 0);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000FCE0 File Offset: 0x0000DEE0
		private void OpenScene()
		{
			this._facegenScene = Scene.CreateNewScene(true, false, DecalAtlasGroup.All, "mono_renderscene");
			this._facegenScene.DisableStaticShadows(true);
			SceneInitializationData sceneInitializationData = default(SceneInitializationData);
			sceneInitializationData.InitPhysicsWorld = false;
			this._facegenScene.Read("character_menu_new", ref sceneInitializationData, "");
			this._facegenScene.SetShadow(true);
			this._facegenScene.SetDynamicShadowmapCascadesRadiusMultiplier(0.1f);
			GameEntity gameEntity = this._facegenScene.FindEntityWithName("cradle");
			if (gameEntity != null)
			{
				gameEntity.SetVisibilityExcludeParents(false);
			}
			this._facegenScene.DisableStaticShadows(true);
			this._agentRendererSceneController = MBAgentRendererSceneController.CreateNewAgentRendererSceneController(this._facegenScene, 32);
			this._camera = Camera.CreateCamera();
			this._defaultCameraGlobalFrame = BodyGeneratorView.InitCamera(this._camera, new Vec3(6.45f, 5.15f, 1.75f, -1f));
			this._targetCameraGlobalFrame = this._defaultCameraGlobalFrame;
			this.SceneLayer = new SceneLayer("SceneLayer", true, true);
			this.SceneLayer.IsFocusLayer = true;
			this.SceneLayer.SetScene(this._facegenScene);
			this.SceneLayer.SetCamera(this._camera);
			this.SceneLayer.SetSceneUsesShadows(true);
			this.SceneLayer.SetRenderWithPostfx(true);
			this.SceneLayer.SetPostfxFromConfig();
			this.SceneLayer.SceneView.SetResolutionScaling(true);
			this.SceneLayer.InputRestrictions.SetCanOverrideFocusOnHit(true);
			int num = -1;
			num &= -5;
			this.SceneLayer.SetPostfxConfigParams(num);
			this.SceneLayer.SetPostfxFromConfig();
			this.SceneLayer.SceneView.SetAcceptGlobalDebugRenderObjects(true);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000FE80 File Offset: 0x0000E080
		private void AddCharacterEntity()
		{
			GameEntity gameEntity = this._facegenScene.FindEntityWithTag("spawnpoint_player_1");
			this._initialCharacterFrame = gameEntity.GetFrame();
			this._initialCharacterFrame.origin.z = 0f;
			this._visualToShow = null;
			this._visualsBeingPrepared = new List<KeyValuePair<AgentVisuals, int>>();
			Monster baseMonsterFromRace = FaceGen.GetBaseMonsterFromRace(this.BodyGen.Race);
			AgentVisualsData data = new AgentVisualsData().UseMorphAnims(true).Equipment(this.BodyGen.Character.Equipment).BodyProperties(this.BodyGen.Character.GetBodyProperties(this.BodyGen.Character.Equipment, -1)).Race(this.BodyGen.Race).Frame(this._initialCharacterFrame).ActionSet(MBGlobals.GetActionSetWithSuffix(baseMonsterFromRace, this.BodyGen.IsFemale, "_facegen")).Scene(this._facegenScene).Monster(baseMonsterFromRace).UseTranslucency(true).UseTesselation(false).PrepareImmediately(true);
			this._nextVisualToShow = AgentVisuals.Create(data, "facegenvisual", false, false, false);
			this._nextVisualToShow.GetEntity().Skeleton.SetAgentActionChannel(1, this.act_inventory_idle_start_cached, 0f, -0.2f, true);
			this._nextVisualToShow.GetEntity();
			this._nextVisualToShow.SetAgentLodZeroOrMaxExternal(true);
			this._nextVisualToShow.GetEntity().CheckResources(true, true);
			this._nextVisualToShow.SetVisible(false);
			this._visualsBeingPrepared.Add(new KeyValuePair<AgentVisuals, int>(this._nextVisualToShow, 1));
			this.SceneLayer.SetFocusedShadowmap(true, ref this._initialCharacterFrame.origin, 0.59999996f);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00010027 File Offset: 0x0000E227
		private void SetNewBodyPropertiesAndBodyGen(BodyProperties bodyProperties)
		{
			this.BodyGen.CurrentBodyProperties = bodyProperties;
			((IFaceGeneratorHandler)this).RefreshCharacterEntity();
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0001003C File Offset: 0x0000E23C
		public void ResetFaceToDefault()
		{
			MBBodyProperties.ProduceNumericKeyWithDefaultValues(ref this.BodyGen.CurrentBodyProperties, this.BodyGen.Character.Equipment.EarsAreHidden, this.BodyGen.Character.Equipment.MouthIsHidden, this.BodyGen.Race, this.BodyGen.IsFemale ? 1 : 0, (int)this.BodyGen.Character.Age);
			((IFaceGeneratorHandler)this).RefreshCharacterEntity();
		}

		// Token: 0x06000282 RID: 642 RVA: 0x000100B6 File Offset: 0x0000E2B6
		private void OnHeightChanged(float sliderValue)
		{
		}

		// Token: 0x06000283 RID: 643 RVA: 0x000100B8 File Offset: 0x0000E2B8
		private void OnAgeChanged()
		{
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000100BA File Offset: 0x0000E2BA
		[CommandLineFunctionality.CommandLineArgumentFunction("show_debug", "facegen")]
		public static string FaceGenShowDebug(List<string> strings)
		{
			FaceGen.ShowDebugValues = !FaceGen.ShowDebugValues;
			return "FaceGen: Show Debug Values are " + (FaceGen.ShowDebugValues ? "enabled" : "disabled");
		}

		// Token: 0x06000285 RID: 645 RVA: 0x000100E6 File Offset: 0x0000E2E6
		[CommandLineFunctionality.CommandLineArgumentFunction("toggle_update_deform_keys", "facegen")]
		public static string FaceGenUpdateDeformKeys(List<string> strings)
		{
			FaceGen.UpdateDeformKeys = !FaceGen.UpdateDeformKeys;
			return "FaceGen: update deform keys is now " + (FaceGen.UpdateDeformKeys ? "enabled" : "disabled");
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00010112 File Offset: 0x0000E312
		public bool ReadyToRender()
		{
			return this.SceneLayer != null && this.SceneLayer.SceneView != null && this.SceneLayer.SceneView.ReadyToRender();
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00010144 File Offset: 0x0000E344
		public void OnTick(float dt)
		{
			this.DataSource.CharacterGamepadControlsEnabled = (Input.IsGamepadActive && this.SceneLayer.IsHitThisFrame);
			this.TickUserInputs(dt);
			if (this.SceneLayer != null && this.SceneLayer.ReadyToRender())
			{
				LoadingWindow.DisableGlobalLoadingWindow();
			}
			if (this._refreshCharacterEntityNextFrame)
			{
				this.RefreshCharacterEntityAux();
				this._refreshCharacterEntityNextFrame = false;
			}
			if (this._visualToShow != null)
			{
				Skeleton skeleton = this._visualToShow.GetVisuals().GetSkeleton();
				bool flag = skeleton.GetAnimationParameterAtChannel(1) > 0.6f;
				if (skeleton.GetActionAtChannel(1) == this.act_command_leftstance_cached && flag)
				{
					this._visualToShow.GetEntity().Skeleton.SetAgentActionChannel(1, this.act_inventory_idle_cached, 0f, -0.2f, true);
				}
			}
			if (!this._openedFromMultiplayer)
			{
				if (this.DebugInput.IsHotKeyReleased("MbFaceGeneratorScreenHotkeySetFaceKeyMin"))
				{
					this.BodyGen.BodyPropertiesMin = this.BodyGen.CurrentBodyProperties;
				}
				else if (this.DebugInput.IsHotKeyReleased("MbFaceGeneratorScreenHotkeySetFaceKeyMax"))
				{
					this.BodyGen.BodyPropertiesMax = this.BodyGen.CurrentBodyProperties;
				}
				else if (this.DebugInput.IsHotKeyPressed("Reset"))
				{
					string hairTags = "";
					string beardTags = "";
					string tatooTags = "";
					this.BodyGen.CurrentBodyProperties = MBBodyProperties.GetRandomBodyProperties(this.BodyGen.Race, this.BodyGen.IsFemale, this.BodyGen.BodyPropertiesMin, this.BodyGen.BodyPropertiesMax, 0, MBRandom.RandomInt(), hairTags, beardTags, tatooTags);
					this.SetNewBodyPropertiesAndBodyGen(this.BodyGen.CurrentBodyProperties);
					this.DataSource.SetBodyProperties(this.BodyGen.CurrentBodyProperties, false, 0, -1, false);
					this.DataSource.UpdateFacegen();
				}
			}
			if (this.DebugInput.IsHotKeyReleased("MbFaceGeneratorScreenHotkeySetCurFaceKeyToMin"))
			{
				this.BodyGen.CurrentBodyProperties = this.BodyGen.BodyPropertiesMin;
				this.SetNewBodyPropertiesAndBodyGen(this.BodyGen.BodyPropertiesMin);
				this.DataSource.SetBodyProperties(this.BodyGen.CurrentBodyProperties, false, 0, -1, false);
				this.DataSource.UpdateFacegen();
			}
			else if (this.DebugInput.IsHotKeyReleased("MbFaceGeneratorScreenHotkeySetCurFaceKeyToMax"))
			{
				this.BodyGen.CurrentBodyProperties = this.BodyGen.BodyPropertiesMax;
				this.SetNewBodyPropertiesAndBodyGen(this.BodyGen.BodyPropertiesMax);
				this.DataSource.SetBodyProperties(this.BodyGen.CurrentBodyProperties, false, 0, -1, false);
				this.DataSource.UpdateFacegen();
			}
			if (this.DebugInput.IsHotKeyDown("FaceGeneratorExtendedDebugKey") && this.DebugInput.IsHotKeyDown("MbFaceGeneratorScreenHotkeyResetFaceToDefault"))
			{
				this.ResetFaceToDefault();
				this.DataSource.SetBodyProperties(this.BodyGen.CurrentBodyProperties, false, 0, -1, false);
				this.DataSource.UpdateFacegen();
			}
			Utilities.CheckResourceModifications();
			if (this.DebugInput.IsHotKeyReleased("Refresh"))
			{
				((IFaceGeneratorHandler)this).RefreshCharacterEntity();
			}
			Scene facegenScene = this._facegenScene;
			if (facegenScene != null)
			{
				facegenScene.Tick(dt);
			}
			if (this._visualToShow != null)
			{
				this._visualToShow.TickVisuals();
			}
			foreach (KeyValuePair<AgentVisuals, int> keyValuePair in this._visualsBeingPrepared)
			{
				keyValuePair.Key.TickVisuals();
			}
			for (int i = 0; i < this._visualsBeingPrepared.Count; i++)
			{
				AgentVisuals key = this._visualsBeingPrepared[i].Key;
				int value = this._visualsBeingPrepared[i].Value;
				key.SetVisible(false);
				if (key.GetEntity().CheckResources(false, true))
				{
					if (value > 0)
					{
						this._visualsBeingPrepared[i] = new KeyValuePair<AgentVisuals, int>(key, value - 1);
					}
					else
					{
						if (key == this._nextVisualToShow)
						{
							if (this._visualToShow != null)
							{
								this._visualToShow.Reset();
							}
							this._visualToShow = key;
							this._visualToShow.SetVisible(true);
							this._nextVisualToShow = null;
							if (this._setMorphAnimNextFrame)
							{
								this._visualToShow.GetEntity().Skeleton.SetFacialAnimation(Agent.FacialAnimChannel.High, this._nextMorphAnimToSet, true, this._nextMorphAnimLoopValue);
								this._setMorphAnimNextFrame = false;
							}
						}
						else
						{
							this._visualsBeingPrepared[i].Key.Reset();
						}
						this._visualsBeingPrepared[i] = this._visualsBeingPrepared[this._visualsBeingPrepared.Count - 1];
						this._visualsBeingPrepared.RemoveAt(this._visualsBeingPrepared.Count - 1);
						i--;
					}
				}
			}
			SoundManager.SetListenerFrame(this._camera.Frame);
			this.UpdateCamera(dt);
			this.TickLayerInputs();
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00010624 File Offset: 0x0000E824
		public void OnFinalize()
		{
			this._facegenCategory.Unload();
			this.ClearAgentVisuals();
			MBAgentRendererSceneController.DestructAgentRendererSceneController(this._facegenScene, this._agentRendererSceneController, false);
			this._agentRendererSceneController = null;
			this._facegenScene.ClearAll();
			this._facegenScene = null;
			this.SceneLayer.SceneView.SetEnable(false);
			this.SceneLayer.SceneView.ClearAll(true, true);
			FaceGenVM dataSource = this.DataSource;
			if (dataSource != null)
			{
				dataSource.OnFinalize();
			}
			this.DataSource = null;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x000106A8 File Offset: 0x0000E8A8
		private void TickLayerInputs()
		{
			if (this.IsHotKeyReleasedOnAnyLayer("Exit"))
			{
				UISoundsHelper.PlayUISound("event:/ui/panels/next");
				((IFaceGeneratorHandler)this).Cancel();
				return;
			}
			if (this.IsHotKeyReleasedOnAnyLayer("Confirm"))
			{
				UISoundsHelper.PlayUISound("event:/ui/panels/next");
				((IFaceGeneratorHandler)this).Done();
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x000106E8 File Offset: 0x0000E8E8
		private void TickUserInputs(float dt)
		{
			if (this.SceneLayer.Input.IsHotKeyReleased("Ascend") || this.SceneLayer.Input.IsHotKeyReleased("Rotate") || this.SceneLayer.Input.IsHotKeyReleased("Zoom"))
			{
				this.GauntletLayer.InputRestrictions.SetMouseVisibility(true);
			}
			Vec2 vec = new Vec2(-this.SceneLayer.Input.GetMouseMoveX(), -this.SceneLayer.Input.GetMouseMoveY());
			bool flag = this.SceneLayer.Input.IsHotKeyDown("Zoom");
			float gameKeyState = this.SceneLayer.Input.GetGameKeyState(55);
			float num = this.SceneLayer.Input.GetGameKeyState(56) - gameKeyState;
			float num2 = flag ? (vec.y * 0.002f) : ((num != 0f) ? (num * 0.02f) : (this.SceneLayer.Input.GetDeltaMouseScroll() * -0.001f));
			float length = (this._targetCameraGlobalFrame.origin.AsVec2 - this._initialCharacterFrame.origin.AsVec2).Length;
			this._cameraCurrentDistanceAdder = MBMath.ClampFloat(this._cameraCurrentDistanceAdder + num2, 0.3f - length, 3f - length);
			if (flag)
			{
				MBWindowManager.DontChangeCursorPos();
				this.GauntletLayer.InputRestrictions.SetMouseVisibility(false);
			}
			float num3 = this.SceneLayer.Input.GetGameKeyAxis("CameraAxisX");
			if (MathF.Abs(num3) < 0.1f)
			{
				num3 = 0f;
			}
			else
			{
				num3 = (num3 - (float)MathF.Sign(num3) * 0.1f) / 0.9f;
			}
			bool flag2 = this.SceneLayer.Input.IsHotKeyDown("Rotate");
			float num4 = flag2 ? (vec.x * -0.004f) : (num3 * -0.02f);
			this._characterTargetRotation = MBMath.WrapAngle(this._characterTargetRotation + num4);
			if (flag2)
			{
				MBWindowManager.DontChangeCursorPos();
				this.GauntletLayer.InputRestrictions.SetMouseVisibility(false);
			}
			if (this.SceneLayer.Input.IsHotKeyDown("Ascend"))
			{
				float num5 = (this._visualToShow != null) ? this._visualToShow.GetScale() : 1f;
				float value = this._cameraCurrentElevationAdder - vec.y * 0.002f;
				float minValue = 0.15f - this._targetCameraGlobalFrame.origin.z;
				float maxValue = 1.9f * num5 - this._targetCameraGlobalFrame.origin.z;
				this._cameraCurrentElevationAdder = MBMath.ClampFloat(value, minValue, maxValue);
				MBWindowManager.DontChangeCursorPos();
				this.GauntletLayer.InputRestrictions.SetMouseVisibility(false);
			}
			else if (Input.IsGamepadActive)
			{
				float num6 = -this.SceneLayer.Input.GetGameKeyAxis("CameraAxisY");
				if (MathF.Abs(num6) > 0.1f)
				{
					num6 = (num6 - (float)MathF.Sign(num6) * 0.1f) / 0.9f;
					float num7 = (this._visualToShow != null) ? this._visualToShow.GetScale() : 1f;
					float value2 = this._cameraCurrentElevationAdder - num6 * 0.01f;
					float minValue2 = 0.15f - this._targetCameraGlobalFrame.origin.z;
					float maxValue2 = 1.9f * num7 - this._targetCameraGlobalFrame.origin.z;
					this._cameraCurrentElevationAdder = MBMath.ClampFloat(value2, minValue2, maxValue2);
				}
			}
			if (this.IsHotKeyPressedOnAnyLayer("SwitchToPreviousTab"))
			{
				UISoundsHelper.PlayUISound("event:/ui/tab");
				this.DataSource.SelectPreviousTab();
			}
			else if (this.IsHotKeyPressedOnAnyLayer("SwitchToNextTab"))
			{
				UISoundsHelper.PlayUISound("event:/ui/tab");
				this.DataSource.SelectNextTab();
			}
			if (this.SceneLayer.Input.IsControlDown() || this.GauntletLayer.Input.IsControlDown())
			{
				if (this.IsHotKeyPressedOnAnyLayer("Copy"))
				{
					Input.SetClipboardText(this.BodyGen.CurrentBodyProperties.ToString());
					return;
				}
				if (this.IsHotKeyPressedOnAnyLayer("Paste"))
				{
					BodyProperties bodyProperties;
					if (BodyProperties.FromString(Input.GetClipboardText(), out bodyProperties))
					{
						this.DataSource.SetBodyProperties(bodyProperties, !FaceGen.ShowDebugValues, 0, -1, true);
						return;
					}
					InformationManager.ShowInquiry(new InquiryData(GameTexts.FindText("str_error", null).ToString(), GameTexts.FindText("str_facegen_error_on_paste", null).ToString(), false, true, "", GameTexts.FindText("str_ok", null).ToString(), null, null, "", 0f, null, null, null), false, false);
				}
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00010B7B File Offset: 0x0000ED7B
		private bool IsHotKeyReleasedOnAnyLayer(string hotkeyName)
		{
			return this.GauntletLayer.Input.IsHotKeyReleased(hotkeyName) || this.SceneLayer.Input.IsHotKeyReleased(hotkeyName);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00010BA3 File Offset: 0x0000EDA3
		private bool IsHotKeyPressedOnAnyLayer(string hotkeyName)
		{
			return this.GauntletLayer.Input.IsHotKeyPressed(hotkeyName) || this.SceneLayer.Input.IsHotKeyPressed(hotkeyName);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00010BCC File Offset: 0x0000EDCC
		private void RefreshCharacterEntityAux()
		{
			SkeletonType skeletonType = this.SkeletonType;
			if (skeletonType < SkeletonType.KidsStart)
			{
				skeletonType = (this.BodyGen.IsFemale ? SkeletonType.Female : SkeletonType.Male);
			}
			this._currentAgentVisualIndex = (this._currentAgentVisualIndex + 1) % 2;
			Monster baseMonsterFromRace = FaceGen.GetBaseMonsterFromRace(this.BodyGen.Race);
			AgentVisualsData data = new AgentVisualsData().UseMorphAnims(true).Scene(this._facegenScene).Monster(baseMonsterFromRace).UseTranslucency(true).UseTesselation(false).SkeletonType(skeletonType).Equipment(this.IsDressed ? this._dressedEquipment : null).BodyProperties(this.BodyGen.CurrentBodyProperties).Race(this.BodyGen.Race).PrepareImmediately(true);
			AgentVisuals agentVisuals = this._visualToShow ?? this._nextVisualToShow;
			ActionIndexCache actionAtChannel = agentVisuals.GetEntity().Skeleton.GetActionAtChannel(1);
			float animationParameterAtChannel = agentVisuals.GetVisuals().GetSkeleton().GetAnimationParameterAtChannel(1);
			this._nextVisualToShow = AgentVisuals.Create(data, "facegenvisual", false, false, false);
			this._nextVisualToShow.SetAgentLodZeroOrMax(true);
			this._nextVisualToShow.GetEntity().Skeleton.SetAgentActionChannel(1, actionAtChannel, animationParameterAtChannel, -0.2f, true);
			this._nextVisualToShow.GetEntity().SetEnforcedMaximumLodLevel(0);
			this._nextVisualToShow.GetEntity().CheckResources(true, true);
			this._nextVisualToShow.SetVisible(false);
			MatrixFrame initialCharacterFrame = this._initialCharacterFrame;
			initialCharacterFrame.rotation.RotateAboutUp(this._characterCurrentRotation);
			initialCharacterFrame.rotation.ApplyScaleLocal(this._nextVisualToShow.GetScale());
			this._nextVisualToShow.GetEntity().SetFrame(ref initialCharacterFrame);
			this._nextVisualToShow.GetVisuals().GetSkeleton().SetAnimationParameterAtChannel(1, animationParameterAtChannel);
			this._nextVisualToShow.GetVisuals().GetSkeleton().TickAnimationsAndForceUpdate(0.001f, initialCharacterFrame, true);
			this._nextVisualToShow.SetVisible(false);
			this._visualsBeingPrepared.Add(new KeyValuePair<AgentVisuals, int>(this._nextVisualToShow, 1));
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00010DC0 File Offset: 0x0000EFC0
		void IFaceGeneratorHandler.MakeVoice(int voiceIndex, float pitch)
		{
			if (this._makeSound)
			{
				AgentVisuals visualToShow = this._visualToShow;
				if (visualToShow == null)
				{
					return;
				}
				visualToShow.MakeRandomVoiceForFacegen();
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00010DDA File Offset: 0x0000EFDA
		void IFaceGeneratorHandler.RefreshCharacterEntity()
		{
			this._refreshCharacterEntityNextFrame = true;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00010DE3 File Offset: 0x0000EFE3
		void IFaceGeneratorHandler.SetFacialAnimation(string faceAnimation, bool loop)
		{
			this._setMorphAnimNextFrame = true;
			this._nextMorphAnimToSet = faceAnimation;
			this._nextMorphAnimLoopValue = loop;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00010DFC File Offset: 0x0000EFFC
		private void ClearAgentVisuals()
		{
			if (this._visualToShow != null)
			{
				this._visualToShow.Reset();
				this._visualToShow = null;
			}
			foreach (KeyValuePair<AgentVisuals, int> keyValuePair in this._visualsBeingPrepared)
			{
				keyValuePair.Key.Reset();
			}
			this._visualsBeingPrepared.Clear();
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00010E7C File Offset: 0x0000F07C
		void IFaceGeneratorHandler.Done()
		{
			this.BodyGen.SaveCurrentCharacter();
			this.ClearAgentVisuals();
			if (Mission.Current != null)
			{
				Mission.Current.MainAgent.UpdateBodyProperties(this.BodyGen.CurrentBodyProperties);
				Mission.Current.MainAgent.EquipItemsFromSpawnEquipment(false);
			}
			this._affirmativeAction();
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00010ED6 File Offset: 0x0000F0D6
		void IFaceGeneratorHandler.Cancel()
		{
			this._negativeAction();
			this.ClearAgentVisuals();
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00010EE9 File Offset: 0x0000F0E9
		void IFaceGeneratorHandler.ChangeToFaceCamera()
		{
			this._cameraLookMode = 1;
			this._cameraCurrentElevationAdder = 0f;
			this._cameraCurrentDistanceAdder = 0f;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00010F08 File Offset: 0x0000F108
		void IFaceGeneratorHandler.ChangeToEyeCamera()
		{
			this._cameraLookMode = 2;
			this._cameraCurrentElevationAdder = 0f;
			this._cameraCurrentDistanceAdder = 0f;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00010F27 File Offset: 0x0000F127
		void IFaceGeneratorHandler.ChangeToNoseCamera()
		{
			this._cameraLookMode = 3;
			this._cameraCurrentElevationAdder = 0f;
			this._cameraCurrentDistanceAdder = 0f;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00010F46 File Offset: 0x0000F146
		void IFaceGeneratorHandler.ChangeToMouthCamera()
		{
			this._cameraLookMode = 4;
			this._cameraCurrentElevationAdder = 0f;
			this._cameraCurrentDistanceAdder = 0f;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00010F65 File Offset: 0x0000F165
		void IFaceGeneratorHandler.ChangeToBodyCamera()
		{
			this._cameraLookMode = 0;
			this._cameraCurrentElevationAdder = 0f;
			this._cameraCurrentDistanceAdder = 0f;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00010F84 File Offset: 0x0000F184
		void IFaceGeneratorHandler.ChangeToHairCamera()
		{
			this._cameraLookMode = 1;
			this._cameraCurrentElevationAdder = 0f;
			this._cameraCurrentDistanceAdder = 0f;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00010FA3 File Offset: 0x0000F1A3
		void IFaceGeneratorHandler.UndressCharacterEntity()
		{
			this.IsDressed = false;
			((IFaceGeneratorHandler)this).RefreshCharacterEntity();
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00010FB2 File Offset: 0x0000F1B2
		void IFaceGeneratorHandler.DressCharacterEntity()
		{
			this.IsDressed = true;
			((IFaceGeneratorHandler)this).RefreshCharacterEntity();
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00010FC4 File Offset: 0x0000F1C4
		void IFaceGeneratorHandler.DefaultFace()
		{
			FaceGenerationParams faceGenerationParams = this.BodyGen.InitBodyGenerator(false);
			faceGenerationParams.UseCache = true;
			faceGenerationParams.UseGpuMorph = true;
			MBBodyProperties.TransformFaceKeysToDefaultFace(ref faceGenerationParams);
			this.DataSource.SetFaceGenerationParams(faceGenerationParams);
			this.DataSource.Refresh(true);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0001100D File Offset: 0x0000F20D
		private void GoToIndex(int index)
		{
			this.BodyGen.SaveCurrentCharacter();
			this.ClearAgentVisuals();
			this._goToIndexAction(index);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0001102C File Offset: 0x0000F22C
		public static MatrixFrame InitCamera(Camera camera, Vec3 cameraPosition)
		{
			camera.SetFovVertical(0.7853982f, Screen.AspectRatio, 0.02f, 200f);
			MatrixFrame matrixFrame = Camera.ConstructCameraFromPositionElevationBearing(cameraPosition, -0.195f, 163.17f);
			camera.Frame = matrixFrame;
			return matrixFrame;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0001106C File Offset: 0x0000F26C
		private void UpdateCamera(float dt)
		{
			this._characterCurrentRotation += MBMath.WrapAngle(this._characterTargetRotation - this._characterCurrentRotation) * MathF.Min(1f, 20f * dt);
			this._targetCameraGlobalFrame.origin = this._defaultCameraGlobalFrame.origin;
			if (this._visualToShow != null)
			{
				MatrixFrame initialCharacterFrame = this._initialCharacterFrame;
				initialCharacterFrame.rotation.RotateAboutUp(this._characterCurrentRotation);
				initialCharacterFrame.rotation.ApplyScaleLocal(this._visualToShow.GetScale());
				this._visualToShow.GetEntity().SetFrame(ref initialCharacterFrame);
				float z = this._visualToShow.GetGlobalStableEyePoint(true).z;
				float z2 = this._visualToShow.GetGlobalStableNeckPoint(true).z;
				float scale = this._visualToShow.GetScale();
				switch (this._cameraLookMode)
				{
				case 1:
				{
					Vec2 vec = new Vec2(6.45f, 6.75f);
					vec += (vec - this._initialCharacterFrame.origin.AsVec2) * (scale - 1f);
					this._targetCameraGlobalFrame.origin = new Vec3(vec, z + (z - z2) * 0.75f, -1f);
					break;
				}
				case 2:
				{
					Vec2 vec = new Vec2(6.45f, 7f);
					vec += (vec - this._initialCharacterFrame.origin.AsVec2) * (scale - 1f);
					this._targetCameraGlobalFrame.origin = new Vec3(vec, z + (z - z2) * 0.5f, -1f);
					break;
				}
				case 3:
				{
					Vec2 vec = new Vec2(6.45f, 7f);
					vec += (vec - this._initialCharacterFrame.origin.AsVec2) * (scale - 1f);
					this._targetCameraGlobalFrame.origin = new Vec3(vec, z + (z - z2) * 0.25f, -1f);
					break;
				}
				case 4:
				{
					Vec2 vec = new Vec2(6.45f, 7f);
					vec += (vec - this._initialCharacterFrame.origin.AsVec2) * (scale - 1f);
					this._targetCameraGlobalFrame.origin = new Vec3(vec, z - (z - z2) * 0.25f, -1f);
					break;
				}
				}
			}
			Vec2 v = (this._targetCameraGlobalFrame.origin.AsVec2 - this._initialCharacterFrame.origin.AsVec2).Normalized();
			Vec3 origin = this._targetCameraGlobalFrame.origin;
			origin.AsVec2 = this._targetCameraGlobalFrame.origin.AsVec2 + v * this._cameraCurrentDistanceAdder;
			origin.z += this._cameraCurrentElevationAdder;
			this._camera.Frame = new MatrixFrame(this._camera.Frame.rotation, this._camera.Frame.origin * (1f - 10f * dt) + origin * 10f * dt);
			this.SceneLayer.SetCamera(this._camera);
		}

		// Token: 0x04000169 RID: 361
		private const int ViewOrderPriority = 1;

		// Token: 0x0400016A RID: 362
		private Scene _facegenScene;

		// Token: 0x0400016B RID: 363
		private MBAgentRendererSceneController _agentRendererSceneController;

		// Token: 0x0400016C RID: 364
		private IGauntletMovie _viewMovie;

		// Token: 0x04000170 RID: 368
		private AgentVisuals _visualToShow;

		// Token: 0x04000171 RID: 369
		private List<KeyValuePair<AgentVisuals, int>> _visualsBeingPrepared;

		// Token: 0x04000172 RID: 370
		private readonly bool _openedFromMultiplayer;

		// Token: 0x04000173 RID: 371
		private AgentVisuals _nextVisualToShow;

		// Token: 0x04000174 RID: 372
		private int _currentAgentVisualIndex;

		// Token: 0x04000175 RID: 373
		private bool _refreshCharacterEntityNextFrame;

		// Token: 0x04000176 RID: 374
		private MatrixFrame _initialCharacterFrame;

		// Token: 0x04000177 RID: 375
		private bool _setMorphAnimNextFrame;

		// Token: 0x04000178 RID: 376
		private string _nextMorphAnimToSet = "";

		// Token: 0x04000179 RID: 377
		private bool _nextMorphAnimLoopValue;

		// Token: 0x0400017A RID: 378
		private readonly ActionIndexCache act_inventory_idle_cached = ActionIndexCache.Create("act_inventory_idle");

		// Token: 0x0400017B RID: 379
		private List<BodyProperties> _templateBodyProperties;

		// Token: 0x0400017C RID: 380
		private readonly ActionIndexCache act_inventory_idle_start_cached = ActionIndexCache.Create("act_inventory_idle_start");

		// Token: 0x0400017D RID: 381
		private readonly ActionIndexCache act_command_leftstance_cached = ActionIndexCache.Create("act_command_leftstance");

		// Token: 0x0400017F RID: 383
		private readonly ControlCharacterCreationStage _affirmativeAction;

		// Token: 0x04000180 RID: 384
		private readonly ControlCharacterCreationStage _negativeAction;

		// Token: 0x04000181 RID: 385
		private readonly ControlCharacterCreationStageReturnInt _getTotalStageCountAction;

		// Token: 0x04000182 RID: 386
		private readonly ControlCharacterCreationStageReturnInt _getCurrentStageIndexAction;

		// Token: 0x04000183 RID: 387
		private readonly ControlCharacterCreationStageReturnInt _getFurthestIndexAction;

		// Token: 0x04000184 RID: 388
		private readonly ControlCharacterCreationStageWithInt _goToIndexAction;

		// Token: 0x04000185 RID: 389
		public bool IsDressed;

		// Token: 0x04000186 RID: 390
		public SkeletonType SkeletonType;

		// Token: 0x04000187 RID: 391
		private Equipment _dressedEquipment;

		// Token: 0x04000188 RID: 392
		private bool _makeSound = true;

		// Token: 0x04000189 RID: 393
		private Camera _camera;

		// Token: 0x0400018A RID: 394
		private int _cameraLookMode;

		// Token: 0x0400018B RID: 395
		private MatrixFrame _targetCameraGlobalFrame;

		// Token: 0x0400018C RID: 396
		private MatrixFrame _defaultCameraGlobalFrame;

		// Token: 0x0400018D RID: 397
		private float _characterCurrentRotation;

		// Token: 0x0400018E RID: 398
		private float _characterTargetRotation;

		// Token: 0x0400018F RID: 399
		private float _cameraCurrentDistanceAdder;

		// Token: 0x04000190 RID: 400
		private float _cameraCurrentElevationAdder;

		// Token: 0x04000191 RID: 401
		private SpriteCategory _facegenCategory;
	}
}
