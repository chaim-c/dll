﻿using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.ViewModelCollection.Education;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Screens;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.MountAndBlade.ViewModelCollection.EscapeMenu;
using TaleWorlds.ScreenSystem;

namespace SandBox.GauntletUI
{
	// Token: 0x02000008 RID: 8
	[GameStateScreen(typeof(EducationState))]
	public class GauntletEducationScreen : ScreenBase, IGameStateListener
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00004076 File Offset: 0x00002276
		// (set) Token: 0x06000046 RID: 70 RVA: 0x0000407E File Offset: 0x0000227E
		public SceneLayer CharacterLayer { get; private set; }

		// Token: 0x06000047 RID: 71 RVA: 0x00004087 File Offset: 0x00002287
		public GauntletEducationScreen(EducationState educationState)
		{
			this._educationState = educationState;
			this._child = this._educationState.Child;
			this._agentVisuals = new List<AgentVisuals>();
			this._preloadHelper = new PreloadHelper();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000040BD File Offset: 0x000022BD
		private void OnOptionSelect(EducationCampaignBehavior.EducationCharacterProperties[] characterProperties)
		{
			this.RefreshSceneCharacters(characterProperties);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000040C8 File Offset: 0x000022C8
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			if (this.CharacterLayer.SceneView.ReadyToRender() && !this._startedRendering)
			{
				this._preloadHelper.WaitForMeshesToBeLoaded();
				LoadingWindow.DisableGlobalLoadingWindow();
				this._startedRendering = true;
			}
			Scene characterScene = this._characterScene;
			if (characterScene != null)
			{
				characterScene.Tick(dt);
			}
			List<AgentVisuals> agentVisuals = this._agentVisuals;
			if (agentVisuals != null)
			{
				agentVisuals.ForEach(delegate(AgentVisuals v)
				{
					if (v != null)
					{
						v.TickVisuals();
					}
				});
			}
			if (this._startedRendering)
			{
				if (this._gauntletLayer.Input.IsHotKeyReleased("ToggleEscapeMenu"))
				{
					this.ToggleEscapeMenu();
					return;
				}
				if (this._gauntletLayer.Input.IsHotKeyReleased("Exit"))
				{
					UISoundsHelper.PlayUISound("event:/ui/default");
					this._dataSource.ExecutePreviousStage();
					return;
				}
				if (this._gauntletLayer.Input.IsHotKeyReleased("Confirm") && this._dataSource.CanAdvance)
				{
					UISoundsHelper.PlayUISound("event:/ui/default");
					this._dataSource.ExecuteNextStage();
				}
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000041DE File Offset: 0x000023DE
		private void ToggleEscapeMenu()
		{
			if (this._isEscapeOpen)
			{
				this.RemoveEscapeMenu();
				return;
			}
			this.OpenEscapeMenu();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000041F5 File Offset: 0x000023F5
		private void CloseEducationScreen(bool isCancel)
		{
			Game.Current.GameStateManager.PopState(0);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00004208 File Offset: 0x00002408
		private void OpenScene()
		{
			this._characterScene = Scene.CreateNewScene(true, false, DecalAtlasGroup.All, "mono_renderscene");
			SceneInitializationData sceneInitializationData = default(SceneInitializationData);
			sceneInitializationData.InitPhysicsWorld = false;
			this._characterScene.Read("character_menu_new", ref sceneInitializationData, "");
			this._characterScene.SetShadow(true);
			this._characterScene.SetDynamicShadowmapCascadesRadiusMultiplier(0.1f);
			this._agentRendererSceneController = MBAgentRendererSceneController.CreateNewAgentRendererSceneController(this._characterScene, 32);
			this._camera = Camera.CreateCamera();
			this._camera.SetFovVertical(0.7853982f, Screen.AspectRatio, 0.02f, 200f);
			this._camera.Frame = Camera.ConstructCameraFromPositionElevationBearing(new Vec3(6.45f, 4.35f, 1.6f, -1f), -0.195f, 163.17f);
			this.CharacterLayer = new SceneLayer("SceneLayer", true, true);
			this.CharacterLayer.SetScene(this._characterScene);
			this.CharacterLayer.SetCamera(this._camera);
			this.CharacterLayer.SetSceneUsesShadows(true);
			this.CharacterLayer.SetRenderWithPostfx(true);
			this.CharacterLayer.SetPostfxFromConfig();
			this.CharacterLayer.SceneView.SetResolutionScaling(true);
			if (!this.CharacterLayer.Input.IsCategoryRegistered(HotKeyManager.GetCategory("FaceGenHotkeyCategory")))
			{
				this.CharacterLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("FaceGenHotkeyCategory"));
			}
			int num = -1;
			num &= -5;
			this.CharacterLayer.SetPostfxConfigParams(num);
			this.CharacterLayer.SetPostfxFromConfig();
			GameEntity gameEntity = this._characterScene.FindEntityWithName("_to_carry_bd_basket_a");
			if (gameEntity != null)
			{
				gameEntity.SetVisibilityExcludeParents(false);
			}
			GameEntity gameEntity2 = this._characterScene.FindEntityWithName("_to_carry_merchandise_hides_b");
			if (gameEntity2 != null)
			{
				gameEntity2.SetVisibilityExcludeParents(false);
			}
			GameEntity gameEntity3 = this._characterScene.FindEntityWithName("_to_carry_foods_basket_apple");
			if (gameEntity3 != null)
			{
				gameEntity3.SetVisibilityExcludeParents(false);
			}
			GameEntity gameEntity4 = this._characterScene.FindEntityWithName("_to_carry_bd_fabric_c");
			if (gameEntity4 != null)
			{
				gameEntity4.SetVisibilityExcludeParents(false);
			}
			GameEntity gameEntity5 = this._characterScene.FindEntityWithName("notebook");
			if (gameEntity5 != null)
			{
				gameEntity5.SetVisibilityExcludeParents(false);
			}
			GameEntity gameEntity6 = this._characterScene.FindEntityWithName("baby");
			if (gameEntity6 != null)
			{
				gameEntity6.SetVisibilityExcludeParents(false);
			}
			GameEntity gameEntity7 = this._characterScene.FindEntityWithName("blacksmith_hammer");
			if (gameEntity7 != null)
			{
				gameEntity7.SetVisibilityExcludeParents(false);
			}
			this._cradleEntity = this._characterScene.FindEntityWithName("cradle");
			GameEntity cradleEntity = this._cradleEntity;
			if (cradleEntity == null)
			{
				return;
			}
			cradleEntity.SetVisibilityExcludeParents(false);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00004484 File Offset: 0x00002684
		private void RefreshSceneCharacters(EducationCampaignBehavior.EducationCharacterProperties[] characterProperties)
		{
			List<float> list = new List<float>();
			GameEntity cradleEntity = this._cradleEntity;
			if (cradleEntity != null)
			{
				cradleEntity.SetVisibilityExcludeParents(false);
			}
			if (this._agentVisuals != null)
			{
				foreach (AgentVisuals agentVisuals in this._agentVisuals)
				{
					Skeleton skeleton = agentVisuals.GetVisuals().GetSkeleton();
					list.Add(skeleton.GetAnimationParameterAtChannel(0));
					agentVisuals.Reset();
				}
				this._agentVisuals.Clear();
			}
			if (characterProperties != null && !characterProperties.IsEmpty<EducationCampaignBehavior.EducationCharacterProperties>())
			{
				bool flag = characterProperties.Length == 1;
				string tag = "";
				for (int i = 0; i < characterProperties.Length; i++)
				{
					if (flag)
					{
						tag = "spawnpoint_player_1";
					}
					else if (i == 0)
					{
						tag = "spawnpoint_player_brother_stage";
					}
					else if (i == 1)
					{
						tag = "spawnpoint_brother_brother_stage";
					}
					MatrixFrame frame = this._characterScene.FindEntityWithTag(tag).GetFrame();
					frame.origin.z = 0f;
					string text = "act_inventory_idle_start";
					if (!string.IsNullOrWhiteSpace(characterProperties[i].ActionId))
					{
						text = characterProperties[i].ActionId;
					}
					string prefabId = characterProperties[i].PrefabId;
					bool useOffHand = characterProperties[i].UseOffHand;
					bool flag2 = false;
					Equipment equipment = characterProperties[i].Equipment.Clone(false);
					if (!string.IsNullOrEmpty(prefabId) && Game.Current.ObjectManager.GetObject<ItemObject>(prefabId) != null)
					{
						ItemObject @object = Game.Current.ObjectManager.GetObject<ItemObject>(prefabId);
						equipment.AddEquipmentToSlotWithoutAgent(useOffHand ? EquipmentIndex.WeaponItemBeginSlot : EquipmentIndex.Weapon1, new EquipmentElement(@object, null, null, false));
						flag2 = true;
					}
					AgentVisuals agentVisuals2 = AgentVisuals.Create(GauntletEducationScreen.CreateAgentVisual(characterProperties[i].Character, frame, equipment, text, this._characterScene, this._child.Culture), "facegenvisual0", false, false, false);
					agentVisuals2.GetVisuals().GetSkeleton().TickAnimationsAndForceUpdate(0.001f, frame, true);
					if (!string.IsNullOrWhiteSpace(text))
					{
						ActionIndexCache actionIndex = ActionIndexCache.Create(text);
						agentVisuals2.GetVisuals().GetSkeleton().SetAgentActionChannel(0, actionIndex, 0f, -0.2f, true);
					}
					if (!flag2 && !string.IsNullOrEmpty(prefabId) && GameEntity.Instantiate(this._characterScene, prefabId, true) != null)
					{
						agentVisuals2.AddPrefabToAgentVisualBoneByRealBoneIndex(prefabId, characterProperties[i].GetUsedHandBoneIndex());
					}
					this.CharacterLayer.SetFocusedShadowmap(true, ref frame.origin, 0.59999996f);
					this._agentVisuals.Add(agentVisuals2);
					if (MathF.Round(this._child.Age) <= 4 && this._cradleEntity != null)
					{
						MatrixFrame matrixFrame = new MatrixFrame(this._cradleEntity.GetFrame().rotation, frame.origin);
						this._cradleEntity.SetFrame(ref matrixFrame);
						this._cradleEntity.SetVisibilityExcludeParents(true);
					}
				}
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00004780 File Offset: 0x00002980
		private void PreloadCharactersAndEquipment(List<BasicCharacterObject> characters, List<Equipment> equipments)
		{
			this._preloadHelper.PreloadCharacters(characters);
			this._preloadHelper.PreloadEquipments(equipments);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000479C File Offset: 0x0000299C
		void IGameStateListener.OnActivate()
		{
			base.OnActivate();
			this._gauntletLayer = new GauntletLayer(1, "GauntletLayer", false);
			this._gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericCampaignPanelsGameKeyCategory"));
			this._gauntletLayer.IsFocusLayer = true;
			ScreenManager.TrySetFocus(this._gauntletLayer);
			base.AddLayer(this._gauntletLayer);
			this.OpenScene();
			base.AddLayer(this.CharacterLayer);
			this._dataSource = new EducationVM(this._educationState.Child, new Action<bool>(this.CloseEducationScreen), new Action<EducationCampaignBehavior.EducationCharacterProperties[]>(this.OnOptionSelect), new Action<List<BasicCharacterObject>, List<Equipment>>(this.PreloadCharactersAndEquipment));
			this._dataSource.SetCancelInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit"));
			this._dataSource.SetDoneInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Confirm"));
			this._gauntletLayer.LoadMovie("EducationScreen", this._dataSource);
			Game.Current.EventManager.TriggerEvent<TutorialContextChangedEvent>(new TutorialContextChangedEvent(TutorialContexts.EducationScreen));
			LoadingWindow.EnableGlobalLoadingWindow();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000048E8 File Offset: 0x00002AE8
		void IGameStateListener.OnDeactivate()
		{
			base.OnDeactivate();
			base.RemoveLayer(this._gauntletLayer);
			this._gauntletLayer.IsFocusLayer = false;
			ScreenManager.TryLoseFocus(this._gauntletLayer);
			Game.Current.EventManager.TriggerEvent<TutorialContextChangedEvent>(new TutorialContextChangedEvent(TutorialContexts.None));
			LoadingWindow.EnableGlobalLoadingWindow();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00004938 File Offset: 0x00002B38
		void IGameStateListener.OnFinalize()
		{
			base.OnFinalize();
			this.CharacterLayer.SceneView.SetEnable(false);
			this.CharacterLayer.SceneView.ClearAll(true, true);
			this._dataSource.OnFinalize();
			this._dataSource = null;
			this._gauntletLayer = null;
			List<AgentVisuals> agentVisuals = this._agentVisuals;
			if (agentVisuals != null)
			{
				agentVisuals.ForEach(delegate(AgentVisuals v)
				{
					if (v != null)
					{
						v.Reset();
					}
				});
			}
			this._agentVisuals = null;
			this.CharacterLayer = null;
			MBAgentRendererSceneController.DestructAgentRendererSceneController(this._characterScene, this._agentRendererSceneController, false);
			this._agentRendererSceneController = null;
			this._characterScene = null;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000049E5 File Offset: 0x00002BE5
		void IGameStateListener.OnInitialize()
		{
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000049E8 File Offset: 0x00002BE8
		private static AgentVisualsData CreateAgentVisual(CharacterObject character, MatrixFrame characterFrame, Equipment equipment, string actionName, Scene scene, CultureObject childsCulture)
		{
			ActionIndexCache actionCode = ActionIndexCache.Create(actionName);
			BodyProperties bodyProperties2;
			if (character.Age < 3f)
			{
				BodyProperties bodyProperties = character.GetBodyProperties(equipment, -1);
				bodyProperties2 = new BodyProperties(new DynamicBodyProperties(3f, bodyProperties.Weight, bodyProperties.Build), bodyProperties.StaticProperties);
			}
			else
			{
				bodyProperties2 = character.GetBodyProperties(equipment, -1);
			}
			Monster baseMonsterFromRace = TaleWorlds.Core.FaceGen.GetBaseMonsterFromRace(character.Race);
			AgentVisualsData agentVisualsData = new AgentVisualsData().UseMorphAnims(true).Equipment(equipment).BodyProperties(bodyProperties2).Frame(characterFrame).ActionSet(MBGlobals.GetActionSetWithSuffix(baseMonsterFromRace, character.IsFemale, "_facegen")).ActionCode(actionCode).Scene(scene).Monster(baseMonsterFromRace).PrepareImmediately(true).UseTranslucency(true).UseTesselation(true).RightWieldedItemIndex(0).LeftWieldedItemIndex(1).SkeletonType(character.IsFemale ? SkeletonType.Female : SkeletonType.Male);
			if (childsCulture != null)
			{
				agentVisualsData.ClothColor1(Clan.PlayerClan.Color);
				agentVisualsData.ClothColor2(Clan.PlayerClan.Color2);
			}
			return agentVisualsData;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00004AED File Offset: 0x00002CED
		private void OpenEscapeMenu()
		{
			this._escapeMenuDatasource = new EscapeMenuVM(this.GetEscapeMenuItems(), null);
			this._escapeMenuMovie = this._gauntletLayer.LoadMovie("EscapeMenu", this._escapeMenuDatasource);
			this._isEscapeOpen = true;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00004B24 File Offset: 0x00002D24
		private void RemoveEscapeMenu()
		{
			this._gauntletLayer.ReleaseMovie(this._escapeMenuMovie);
			this._escapeMenuDatasource = null;
			this._escapeMenuMovie = null;
			this._isEscapeOpen = false;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00004B4C File Offset: 0x00002D4C
		private List<EscapeMenuItemVM> GetEscapeMenuItems()
		{
			TextObject ironmanDisabledReason = GameTexts.FindText("str_pause_menu_disabled_hint", "IronmanMode");
			TextObject educationDisabledReason = GameTexts.FindText("str_pause_menu_disabled_hint", "Education");
			List<EscapeMenuItemVM> list = new List<EscapeMenuItemVM>();
			list.Add(new EscapeMenuItemVM(new TextObject("{=UAD5gWKK}Return to Education", null), delegate(object o)
			{
				this.RemoveEscapeMenu();
			}, null, () => new Tuple<bool, TextObject>(false, TextObject.Empty), true));
			list.Add(new EscapeMenuItemVM(new TextObject("{=PXT6aA4J}Campaign Options", null), delegate(object o)
			{
			}, null, () => new Tuple<bool, TextObject>(true, educationDisabledReason), false));
			list.Add(new EscapeMenuItemVM(new TextObject("{=NqarFr4P}Options", null), delegate(object o)
			{
			}, null, () => new Tuple<bool, TextObject>(true, educationDisabledReason), false));
			list.Add(new EscapeMenuItemVM(new TextObject("{=bV75iwKa}Save", null), delegate(object o)
			{
			}, null, () => new Tuple<bool, TextObject>(true, educationDisabledReason), false));
			list.Add(new EscapeMenuItemVM(new TextObject("{=e0KdfaNe}Save As", null), delegate(object o)
			{
			}, null, () => new Tuple<bool, TextObject>(true, educationDisabledReason), false));
			list.Add(new EscapeMenuItemVM(new TextObject("{=9NuttOBC}Load", null), delegate(object o)
			{
			}, null, () => new Tuple<bool, TextObject>(true, educationDisabledReason), false));
			list.Add(new EscapeMenuItemVM(new TextObject("{=AbEh2y8o}Save And Exit", null), delegate(object o)
			{
			}, null, () => new Tuple<bool, TextObject>(true, educationDisabledReason), false));
			list.Add(new EscapeMenuItemVM(new TextObject("{=RamV6yLM}Exit to Main Menu", null), delegate(object o)
			{
				this.RemoveEscapeMenu();
				MBGameManager.EndGame();
			}, null, () => new Tuple<bool, TextObject>(CampaignOptions.IsIronmanMode, ironmanDisabledReason), false));
			return list;
		}

		// Token: 0x0400001F RID: 31
		private readonly EducationState _educationState;

		// Token: 0x04000020 RID: 32
		private readonly Hero _child;

		// Token: 0x04000021 RID: 33
		private readonly PreloadHelper _preloadHelper;

		// Token: 0x04000022 RID: 34
		private EducationVM _dataSource;

		// Token: 0x04000023 RID: 35
		private GauntletLayer _gauntletLayer;

		// Token: 0x04000024 RID: 36
		private bool _startedRendering;

		// Token: 0x04000026 RID: 38
		private Scene _characterScene;

		// Token: 0x04000027 RID: 39
		private MBAgentRendererSceneController _agentRendererSceneController;

		// Token: 0x04000028 RID: 40
		private Camera _camera;

		// Token: 0x04000029 RID: 41
		private List<AgentVisuals> _agentVisuals;

		// Token: 0x0400002A RID: 42
		private GameEntity _cradleEntity;

		// Token: 0x0400002B RID: 43
		private EscapeMenuVM _escapeMenuDatasource;

		// Token: 0x0400002C RID: 44
		private IGauntletMovie _escapeMenuMovie;

		// Token: 0x0400002D RID: 45
		private bool _isEscapeOpen;
	}
}
