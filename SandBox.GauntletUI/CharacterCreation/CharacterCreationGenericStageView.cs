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
	// Token: 0x0200003E RID: 62
	[CharacterCreationStageView(typeof(CharacterCreationGenericStage))]
	public class CharacterCreationGenericStageView : CharacterCreationStageViewBase
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000245 RID: 581 RVA: 0x000101DA File Offset: 0x0000E3DA
		// (set) Token: 0x06000246 RID: 582 RVA: 0x000101E2 File Offset: 0x0000E3E2
		public SceneLayer CharacterLayer { get; private set; }

		// Token: 0x06000247 RID: 583 RVA: 0x000101EC File Offset: 0x0000E3EC
		public CharacterCreationGenericStageView(CharacterCreation characterCreation, ControlCharacterCreationStage affirmativeAction, TextObject affirmativeActionText, ControlCharacterCreationStage negativeAction, TextObject negativeActionText, ControlCharacterCreationStage onRefresh, ControlCharacterCreationStageReturnInt getCurrentStageIndexAction, ControlCharacterCreationStageReturnInt getTotalStageCountAction, ControlCharacterCreationStageReturnInt getFurthestIndexAction, ControlCharacterCreationStageWithInt goToIndexAction) : base(affirmativeAction, negativeAction, onRefresh, getCurrentStageIndexAction, getTotalStageCountAction, getFurthestIndexAction, goToIndexAction)
		{
			this._characterCreation = characterCreation;
			this._affirmativeActionText = affirmativeActionText;
			this._negativeActionText = negativeActionText;
			this.GauntletLayer = new GauntletLayer(1, "GauntletLayer", false);
			this.GauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			this.GauntletLayer.IsFocusLayer = true;
			this.GauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			ScreenManager.TrySetFocus(this.GauntletLayer);
			CharacterCreationGenericStageVM dataSource;
			(dataSource = new CharacterCreationGenericStageVM(this._characterCreation, new Action(this.NextStage), this._affirmativeActionText, new Action(this.PreviousStage), this._negativeActionText, this._stageIndex, getCurrentStageIndexAction(), getTotalStageCountAction(), getFurthestIndexAction(), new Action<int>(this.GoToIndex))).OnOptionSelection = new Action(this.OnSelectionChanged);
			this._dataSource = dataSource;
			this.CreateHotKeyVisuals();
			this._movie = this.GauntletLayer.LoadMovie("CharacterCreationGenericStage", this._dataSource);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0001032A File Offset: 0x0000E52A
		public override void SetGenericScene(Scene scene)
		{
			this.OpenScene(scene);
			this.RefreshCharacterEntity();
			this.RefreshMountEntity();
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00010340 File Offset: 0x0000E540
		private void CreateHotKeyVisuals()
		{
			CharacterCreationGenericStageVM dataSource = this._dataSource;
			if (dataSource != null)
			{
				dataSource.SetCancelInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit"));
			}
			CharacterCreationGenericStageVM dataSource2 = this._dataSource;
			if (dataSource2 == null)
			{
				return;
			}
			dataSource2.SetDoneInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Confirm"));
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00010398 File Offset: 0x0000E598
		private void OpenScene(Scene cachedScene)
		{
			this._characterScene = cachedScene;
			this._characterScene.SetShadow(true);
			this._characterScene.SetDynamicShadowmapCascadesRadiusMultiplier(0.1f);
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
			if (!this.CharacterLayer.Input.IsCategoryRegistered(HotKeyManager.GetCategory("FaceGenHotkeyCategory")))
			{
				this.CharacterLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("FaceGenHotkeyCategory"));
			}
			int num = -1;
			num &= -5;
			this.CharacterLayer.SetPostfxConfigParams(num);
			this.CharacterLayer.SetPostfxFromConfig();
			GameEntity gameEntity = this._characterScene.FindEntityWithName("cradle");
			if (gameEntity == null)
			{
				return;
			}
			gameEntity.SetVisibilityExcludeParents(false);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000104D4 File Offset: 0x0000E6D4
		private void RefreshCharacterEntity()
		{
			List<float> list = new List<float>();
			bool isPlayerAlone = this._characterCreation.IsPlayerAlone;
			bool hasSecondaryCharacter = this._characterCreation.HasSecondaryCharacter;
			if (this._playerOrParentAgentVisuals != null && this._characterCreation.FaceGenChars.Count == 1)
			{
				foreach (AgentVisuals agentVisuals in this._playerOrParentAgentVisuals)
				{
					Skeleton skeleton = agentVisuals.GetVisuals().GetSkeleton();
					list.Add(skeleton.GetAnimationParameterAtChannel(0));
				}
			}
			if (this._playerOrParentAgentVisualsPrevious != null)
			{
				foreach (AgentVisuals agentVisuals2 in this._playerOrParentAgentVisualsPrevious)
				{
					agentVisuals2.Reset();
				}
			}
			this._playerOrParentAgentVisualsPrevious = new List<AgentVisuals>();
			if (this._playerOrParentAgentVisuals != null)
			{
				foreach (AgentVisuals item in this._playerOrParentAgentVisuals)
				{
					this._playerOrParentAgentVisualsPrevious.Add(item);
				}
			}
			this._checkForVisualVisibility = 1;
			if (this._characterCreation.FaceGenChars.Count > 0)
			{
				this._playerOrParentAgentVisuals = new List<AgentVisuals>();
				int num = this._characterCreation.FaceGenChars.Count;
				int num2 = 0;
				using (List<FaceGenChar>.Enumerator enumerator2 = this._characterCreation.FaceGenChars.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						FaceGenChar character = enumerator2.Current;
						string tag = isPlayerAlone ? "spawnpoint_player_1" : "spawnpoint_player_3";
						if (hasSecondaryCharacter)
						{
							if (this._characterCreation.FaceGenChars.ElementAt(num2).ActionName.ToString().Contains("horse"))
							{
								tag = "spawnpoint_mount_1";
							}
							else if (num2 == 0)
							{
								tag = "spawnpoint_player_brother_stage";
							}
							else if (num2 == 1)
							{
								tag = "spawnpoint_brother_brother_stage";
							}
						}
						GameEntity gameEntity = this._characterScene.FindEntityWithTag(tag);
						this._initialCharacterFrame = gameEntity.GetFrame();
						this._initialCharacterFrame.origin.z = 0f;
						AgentVisuals agentVisuals3 = AgentVisuals.Create(this.CreateAgentVisual(character, this._initialCharacterFrame, isPlayerAlone, (GameStateManager.Current.ActiveState as CharacterCreationState).CurrentCharacterCreationContent.GetSelectedParentType(), num2 == 2), "facegenvisual" + num.ToString(), false, false, false);
						agentVisuals3.SetVisible(false);
						this._playerOrParentAgentVisuals.Add(agentVisuals3);
						this._playerOrParentAgentVisuals[num2].GetVisuals().GetSkeleton().TickAnimationsAndForceUpdate(0.001f, this._initialCharacterFrame, true);
						if (isPlayerAlone || hasSecondaryCharacter)
						{
							MBReadOnlyList<FaceGenChar> faceGenChars = this._characterCreation.FaceGenChars;
							ActionIndexCache actionIndex = ActionIndexCache.Create((faceGenChars != null) ? faceGenChars.ElementAt(num2).ActionName : null);
							this._playerOrParentAgentVisuals[num2].GetVisuals().GetSkeleton().SetAgentActionChannel(0, actionIndex, 0f, -0.2f, true);
						}
						if (num2 == 0 && !string.IsNullOrEmpty(this._characterCreation.PrefabId) && GameEntity.Instantiate(this._characterScene, this._characterCreation.PrefabId, true) != null)
						{
							this._playerOrParentAgentVisuals[num2].AddPrefabToAgentVisualBoneByRealBoneIndex(this._characterCreation.PrefabId, this._characterCreation.PrefabBoneUsage);
						}
						this._playerOrParentAgentVisuals[num2].SetAgentLodZeroOrMax(true);
						this._playerOrParentAgentVisuals[num2].GetEntity().SetEnforcedMaximumLodLevel(0);
						this._playerOrParentAgentVisuals[num2].GetEntity().CheckResources(true, true);
						this.CharacterLayer.SetFocusedShadowmap(true, ref this._initialCharacterFrame.origin, 0.59999996f);
						num++;
						num2++;
					}
					return;
				}
			}
			this._playerOrParentAgentVisuals = null;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00010904 File Offset: 0x0000EB04
		private void RefreshMountEntity()
		{
			this.RemoveShownMount();
			if (this._characterCreation.FaceGenMount != null)
			{
				GameEntity gameEntity = this._characterScene.FindEntityWithTag("spawnpoint_mount_1");
				HorseComponent horseComponent = this._characterCreation.FaceGenMount.HorseItem.HorseComponent;
				Monster monster = horseComponent.Monster;
				this._mountEntityToPrepare = GameEntity.CreateEmpty(this._characterScene, true);
				AnimationSystemData animationSystemData = monster.FillAnimationSystemData(MBGlobals.GetActionSet(horseComponent.Monster.ActionSetCode), 1f, false);
				this._mountEntityToPrepare.CreateSkeletonWithActionSet(ref animationSystemData);
				ActionIndexCache actionIndex = ActionIndexCache.Create(this._characterCreation.FaceGenMount.ActionName);
				this._mountEntityToPrepare.Skeleton.SetAgentActionChannel(0, actionIndex, 0f, -0.2f, true);
				this._mountEntityToPrepare.EntityFlags |= EntityFlags.AnimateWhenVisible;
				MountVisualCreator.AddMountMeshToEntity(this._mountEntityToPrepare, this._characterCreation.FaceGenMount.HorseItem, this._characterCreation.FaceGenMount.HarnessItem, this._characterCreation.FaceGenMount.MountKey.ToString(), null);
				MatrixFrame globalFrame = gameEntity.GetGlobalFrame();
				this._mountEntityToPrepare.SetFrame(ref globalFrame);
				this._mountEntityToPrepare.SetVisibilityExcludeParents(false);
				this._mountEntityToPrepare.SetEnforcedMaximumLodLevel(0);
				this._mountEntityToPrepare.CheckResources(true, false);
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00010A52 File Offset: 0x0000EC52
		private void RemoveShownMount()
		{
			if (this._mountEntityToShow != null)
			{
				this._mountEntityToShow.Remove(116);
			}
			this._mountEntityToShow = this._mountEntityToPrepare;
			this._mountEntityToPrepare = null;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00010A84 File Offset: 0x0000EC84
		private AgentVisualsData CreateAgentVisual(FaceGenChar character, MatrixFrame characterFrame, bool isPlayerEntity, int selectedParentType = 0, bool isChildAgent = false)
		{
			ActionIndexCache actionCode = isChildAgent ? ActionIndexCache.Create("act_character_creation_toddler_" + selectedParentType) : ActionIndexCache.Create(character.IsFemale ? ("act_character_creation_female_default_" + selectedParentType) : ("act_character_creation_male_default_" + selectedParentType));
			Monster baseMonsterFromRace = TaleWorlds.Core.FaceGen.GetBaseMonsterFromRace(character.Race);
			AgentVisualsData agentVisualsData = new AgentVisualsData().UseMorphAnims(true).Equipment(character.Equipment).BodyProperties(character.BodyProperties).Frame(characterFrame).ActionSet(MBGlobals.GetActionSetWithSuffix(baseMonsterFromRace, character.IsFemale, "_facegen")).ActionCode(actionCode).Scene(this._characterScene).Monster(baseMonsterFromRace).UseTranslucency(true).UseTesselation(true).RightWieldedItemIndex(0).LeftWieldedItemIndex(1).Race(CharacterObject.PlayerCharacter.Race).SkeletonType(character.IsFemale ? SkeletonType.Female : SkeletonType.Male);
			CharacterCreationContentBase currentCharacterCreationContent = ((CharacterCreationState)GameStateManager.Current.ActiveState).CurrentCharacterCreationContent;
			if (currentCharacterCreationContent.GetSelectedCulture() != null)
			{
				agentVisualsData.ClothColor1(currentCharacterCreationContent.GetSelectedCulture().Color);
				agentVisualsData.ClothColor2(currentCharacterCreationContent.GetSelectedCulture().Color2);
			}
			if (!isPlayerEntity && !isChildAgent)
			{
				agentVisualsData.Scale(character.IsFemale ? 0.99f : 1f);
			}
			if (!isPlayerEntity && isChildAgent)
			{
				agentVisualsData.Scale(0.5f);
			}
			return agentVisualsData;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00010BF1 File Offset: 0x0000EDF1
		private void OnSelectionChanged()
		{
			this.RefreshCharacterEntity();
			this.RefreshMountEntity();
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00010C00 File Offset: 0x0000EE00
		public override void Tick(float dt)
		{
			base.Tick(dt);
			base.HandleEscapeMenu(this, this.CharacterLayer);
			Scene characterScene = this._characterScene;
			if (characterScene != null)
			{
				characterScene.Tick(dt);
			}
			foreach (AgentVisuals agentVisuals in this._playerOrParentAgentVisuals)
			{
				agentVisuals.TickVisuals();
			}
			if (this._playerOrParentAgentVisuals != null && this._checkForVisualVisibility > 0)
			{
				bool flag = true;
				using (List<AgentVisuals>.Enumerator enumerator = this._playerOrParentAgentVisuals.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator.Current.GetEntity().CheckResources(false, true))
						{
							flag = false;
						}
					}
				}
				if (this._mountEntityToPrepare != null && !this._mountEntityToPrepare.CheckResources(false, true))
				{
					flag = false;
				}
				if (flag)
				{
					this._checkForVisualVisibility--;
					if (this._checkForVisualVisibility == 0)
					{
						foreach (AgentVisuals agentVisuals2 in this._playerOrParentAgentVisuals)
						{
							agentVisuals2.SetVisible(true);
						}
						foreach (AgentVisuals agentVisuals3 in this._playerOrParentAgentVisualsPrevious)
						{
							agentVisuals3.SetVisible(false);
							agentVisuals3.Reset();
						}
						if (this._mountEntityToPrepare != null)
						{
							this._mountEntityToPrepare.SetVisibilityExcludeParents(true);
						}
						if (this._mountEntityToShow != null)
						{
							this._mountEntityToShow.SetVisibilityExcludeParents(false);
							this._characterScene.RemoveEntity(this._mountEntityToShow, 116);
						}
						this._mountEntityToShow = this._mountEntityToPrepare;
						this._mountEntityToPrepare = null;
						this._playerOrParentAgentVisualsPrevious.Clear();
					}
				}
			}
			if (this.CharacterLayer.Input.IsHotKeyReleased("Ascend") || this.CharacterLayer.Input.IsHotKeyReleased("Rotate") || this.CharacterLayer.Input.IsHotKeyReleased("Zoom"))
			{
				this.GauntletLayer.InputRestrictions.SetMouseVisibility(true);
			}
			this.HandleLayerInput();
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00010E60 File Offset: 0x0000F060
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

		// Token: 0x06000252 RID: 594 RVA: 0x00010ED4 File Offset: 0x0000F0D4
		public override void NextStage()
		{
			this._stageIndex++;
			if (this._stageIndex < this._characterCreation.CharacterCreationMenuCount)
			{
				if (this._movie != null)
				{
					this.GauntletLayer.ReleaseMovie(this._movie);
					this._movie = null;
				}
				if (this._dataSource != null)
				{
					this._dataSource.OnOptionSelection = null;
				}
				CharacterCreationGenericStageVM dataSource;
				(dataSource = new CharacterCreationGenericStageVM(this._characterCreation, new Action(this.NextStage), this._affirmativeActionText, new Action(this.PreviousStage), this._negativeActionText, this._stageIndex, this._getCurrentStageIndexAction(), this._getTotalStageCountAction(), this._getFurthestIndexAction(), new Action<int>(this.GoToIndex))).OnOptionSelection = new Action(this.OnSelectionChanged);
				this._dataSource = dataSource;
				this.CreateHotKeyVisuals();
				this._movie = this.GauntletLayer.LoadMovie("CharacterCreationGenericStage", this._dataSource);
				this.RefreshCharacterEntity();
				this.RefreshMountEntity();
				return;
			}
			this.RefreshMountEntity();
			this._affirmativeAction();
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00010FF8 File Offset: 0x0000F1F8
		public override void PreviousStage()
		{
			this._stageIndex--;
			if (this._stageIndex >= 0)
			{
				if (this._movie != null)
				{
					this.GauntletLayer.ReleaseMovie(this._movie);
					this._movie = null;
				}
				if (this._dataSource != null)
				{
					this._dataSource.OnOptionSelection = null;
				}
				CharacterCreationGenericStageVM dataSource;
				(dataSource = new CharacterCreationGenericStageVM(this._characterCreation, new Action(this.NextStage), this._affirmativeActionText, new Action(this.PreviousStage), this._negativeActionText, this._stageIndex, this._getCurrentStageIndexAction(), this._getTotalStageCountAction(), this._getFurthestIndexAction(), new Action<int>(this.GoToIndex))).OnOptionSelection = new Action(this.OnSelectionChanged);
				this._dataSource = dataSource;
				this.CreateHotKeyVisuals();
				this._movie = this.GauntletLayer.LoadMovie("CharacterCreationGenericStage", this._dataSource);
				this.RefreshCharacterEntity();
				this.RefreshMountEntity();
				return;
			}
			this.RefreshMountEntity();
			this._negativeAction();
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00011110 File Offset: 0x0000F310
		protected override void OnFinalize()
		{
			base.OnFinalize();
			if (this._playerOrParentAgentVisuals != null)
			{
				foreach (AgentVisuals agentVisuals in this._playerOrParentAgentVisuals)
				{
					agentVisuals.Reset();
				}
			}
			if (this._playerOrParentAgentVisualsPrevious != null)
			{
				foreach (AgentVisuals agentVisuals2 in this._playerOrParentAgentVisualsPrevious)
				{
					agentVisuals2.Reset();
				}
			}
			this.CharacterLayer.SceneView.SetEnable(false);
			this.CharacterLayer.SceneView.ClearAll(false, false);
			this._playerOrParentAgentVisuals = null;
			this._playerOrParentAgentVisualsPrevious = null;
			this.GauntletLayer = null;
			CharacterCreationGenericStageVM dataSource = this._dataSource;
			if (dataSource != null)
			{
				dataSource.OnFinalize();
			}
			this._dataSource = null;
			this.CharacterLayer = null;
			this._characterScene = null;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00011214 File Offset: 0x0000F414
		public override int GetVirtualStageCount()
		{
			return this._characterCreation.CharacterCreationMenuCount;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00011221 File Offset: 0x0000F421
		public override IEnumerable<ScreenLayer> GetLayers()
		{
			return new List<ScreenLayer>
			{
				this.CharacterLayer,
				this.GauntletLayer
			};
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00011240 File Offset: 0x0000F440
		public override void LoadEscapeMenuMovie()
		{
			this._escapeMenuDatasource = new EscapeMenuVM(base.GetEscapeMenuItems(this), null);
			this._escapeMenuMovie = this.GauntletLayer.LoadMovie("EscapeMenu", this._escapeMenuDatasource);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00011271 File Offset: 0x0000F471
		public override void ReleaseEscapeMenuMovie()
		{
			this.GauntletLayer.ReleaseMovie(this._escapeMenuMovie);
			this._escapeMenuDatasource = null;
			this._escapeMenuMovie = null;
		}

		// Token: 0x04000134 RID: 308
		protected readonly TextObject _affirmativeActionText;

		// Token: 0x04000135 RID: 309
		protected readonly TextObject _negativeActionText;

		// Token: 0x04000136 RID: 310
		private IGauntletMovie _movie;

		// Token: 0x04000137 RID: 311
		private GauntletLayer GauntletLayer;

		// Token: 0x04000138 RID: 312
		private CharacterCreationGenericStageVM _dataSource;

		// Token: 0x04000139 RID: 313
		private int _stageIndex;

		// Token: 0x0400013A RID: 314
		private readonly ActionIndexCache act_inventory_idle_start = ActionIndexCache.Create("act_inventory_idle_start");

		// Token: 0x0400013B RID: 315
		private readonly ActionIndexCache act_horse_stand_1 = ActionIndexCache.Create("act_horse_stand_1");

		// Token: 0x0400013C RID: 316
		private readonly CharacterCreation _characterCreation;

		// Token: 0x0400013D RID: 317
		private Scene _characterScene;

		// Token: 0x0400013E RID: 318
		private Camera _camera;

		// Token: 0x0400013F RID: 319
		private MatrixFrame _initialCharacterFrame;

		// Token: 0x04000140 RID: 320
		private List<AgentVisuals> _playerOrParentAgentVisuals;

		// Token: 0x04000141 RID: 321
		private List<AgentVisuals> _playerOrParentAgentVisualsPrevious;

		// Token: 0x04000142 RID: 322
		private int _checkForVisualVisibility;

		// Token: 0x04000143 RID: 323
		private GameEntity _mountEntityToPrepare;

		// Token: 0x04000144 RID: 324
		private GameEntity _mountEntityToShow;

		// Token: 0x04000146 RID: 326
		private EscapeMenuVM _escapeMenuDatasource;

		// Token: 0x04000147 RID: 327
		private IGauntletMovie _escapeMenuMovie;
	}
}
