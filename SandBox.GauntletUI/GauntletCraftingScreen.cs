using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting;
using TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Screens;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.ObjectSystem;
using TaleWorlds.ScreenSystem;
using TaleWorlds.TwoDimension;

namespace SandBox.GauntletUI
{
	// Token: 0x02000007 RID: 7
	[GameStateScreen(typeof(CraftingState))]
	public class GauntletCraftingScreen : ScreenBase, ICraftingStateHandler, IGameStateListener
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002BD2 File Offset: 0x00000DD2
		private SceneView SceneView
		{
			get
			{
				return this._sceneLayer.SceneView;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002BDF File Offset: 0x00000DDF
		public GauntletCraftingScreen(CraftingState craftingState)
		{
			this._craftingState = craftingState;
			this._craftingState.Handler = this;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002C08 File Offset: 0x00000E08
		private void ReloadPieces()
		{
			string key = GauntletCraftingScreen._reloadXmlPath.Key;
			string text = GauntletCraftingScreen._reloadXmlPath.Value;
			if (!text.EndsWith(".xml"))
			{
				text += ".xml";
			}
			GauntletCraftingScreen._reloadXmlPath = new KeyValuePair<string, string>(null, null);
			XmlDocument xmlDocument = Game.Current.ObjectManager.LoadXMLFromFileSkipValidation(ModuleHelper.GetModuleFullPath(key) + "ModuleData/" + text, "");
			if (xmlDocument != null)
			{
				foreach (object obj in xmlDocument.ChildNodes[1].ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					XmlAttributeCollection attributes = xmlNode.Attributes;
					if (attributes != null)
					{
						string innerText = attributes["id"].InnerText;
						CraftingPiece @object = Game.Current.ObjectManager.GetObject<CraftingPiece>(innerText);
						if (@object != null)
						{
							@object.Deserialize(Game.Current.ObjectManager, xmlNode);
						}
					}
				}
				this._craftingState.CraftingLogic.ReIndex(true);
				this.RefreshItemEntity(this._dataSource.IsInCraftingMode);
				this._dataSource.WeaponDesign.RefreshItem();
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002D4C File Offset: 0x00000F4C
		public void Initialize()
		{
			SpriteData spriteData = UIResourceManager.SpriteData;
			TwoDimensionEngineResourceContext resourceContext = UIResourceManager.ResourceContext;
			ResourceDepot uiresourceDepot = UIResourceManager.UIResourceDepot;
			this._craftingCategory = spriteData.SpriteCategories["ui_crafting"];
			this._craftingCategory.Load(resourceContext, uiresourceDepot);
			this._gauntletLayer = new GauntletLayer(1, "GauntletLayer", false);
			this._gauntletMovie = this._gauntletLayer.LoadMovie("Crafting", this._dataSource);
			this._gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			this._gauntletLayer.InputRestrictions.SetCanOverrideFocusOnHit(true);
			base.AddLayer(this._gauntletLayer);
			this.OpenScene();
			this.RefreshItemEntity(true);
			this._isInitialized = true;
			Game game = Game.Current;
			if (game == null)
			{
				return;
			}
			game.EventManager.TriggerEvent<TutorialContextChangedEvent>(new TutorialContextChangedEvent(TutorialContexts.CraftingScreen));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002E1C File Offset: 0x0000101C
		protected override void OnInitialize()
		{
			this.Initialize();
			this._sceneLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("Generic"));
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("Generic"));
			this._sceneLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("CraftingHotkeyCategory"));
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("CraftingHotkeyCategory"));
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002E98 File Offset: 0x00001098
		protected override void OnFinalize()
		{
			base.OnFinalize();
			Game game = Game.Current;
			if (game != null)
			{
				game.EventManager.TriggerEvent<TutorialContextChangedEvent>(new TutorialContextChangedEvent(TutorialContexts.None));
			}
			this.SceneView.ClearAll(true, true);
			this._craftingCategory.Unload();
			CraftingVM dataSource = this._dataSource;
			if (dataSource == null)
			{
				return;
			}
			dataSource.OnFinalize();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002EF0 File Offset: 0x000010F0
		protected override void OnFrameTick(float dt)
		{
			LoadingWindow.DisableGlobalLoadingWindow();
			base.OnFrameTick(dt);
			this._dataSource.CanSwitchTabs = (!Input.IsGamepadActive || !InformationManager.GetIsAnyTooltipActiveAndExtended());
			this._dataSource.AreGamepadControlHintsEnabled = (Input.IsGamepadActive && this._sceneLayer.IsHitThisFrame && this._dataSource.IsInCraftingMode);
			if (this._dataSource.IsInCraftingMode)
			{
				this._dataSource.WeaponDesign.WeaponControlsEnabled = this._sceneLayer.IsHitThisFrame;
			}
			if (this._sceneLayer.Input.IsControlDown() || this._gauntletLayer.Input.IsControlDown())
			{
				if (this._sceneLayer.Input.IsHotKeyPressed("Copy") || this._gauntletLayer.Input.IsHotKeyPressed("Copy"))
				{
					this.CopyXmlCode();
				}
				else if (this._sceneLayer.Input.IsHotKeyPressed("Paste") || this._gauntletLayer.Input.IsHotKeyPressed("Paste"))
				{
					this.PasteXmlCode();
				}
			}
			if (this._craftingState.CraftingLogic.CurrentCraftingTemplate == null)
			{
				return;
			}
			if (!this._sceneLayer.Input.IsHotKeyDown("Rotate") && !this._sceneLayer.Input.IsHotKeyDown("Zoom"))
			{
				this._sceneLayer.InputRestrictions.SetMouseVisibility(true);
			}
			this._craftingScene.Tick(dt);
			if (Input.IsGamepadActive || (!this._gauntletLayer.IsFocusedOnInput() && !this._sceneLayer.IsFocusedOnInput()))
			{
				if (this.IsHotKeyReleasedInAnyLayer("Exit"))
				{
					UISoundsHelper.PlayUISound("event:/ui/default");
					this._dataSource.ExecuteCancel();
				}
				else if (this.IsHotKeyReleasedInAnyLayer("Confirm"))
				{
					bool isInCraftingMode = this._dataSource.IsInCraftingMode;
					bool isInRefinementMode = this._dataSource.IsInRefinementMode;
					bool isInSmeltingMode = this._dataSource.IsInSmeltingMode;
					ValueTuple<bool, bool> valueTuple = this._dataSource.ExecuteConfirm();
					bool item = valueTuple.Item1;
					bool item2 = valueTuple.Item2;
					if (item)
					{
						if (item2)
						{
							if (isInCraftingMode)
							{
								UISoundsHelper.PlayUISound("event:/ui/crafting/craft_success");
							}
							else if (isInRefinementMode)
							{
								UISoundsHelper.PlayUISound("event:/ui/crafting/refine_success");
							}
							else if (isInSmeltingMode)
							{
								UISoundsHelper.PlayUISound("event:/ui/crafting/smelt_success");
							}
						}
						else
						{
							UISoundsHelper.PlayUISound("event:/ui/default");
						}
					}
				}
				else if (this._dataSource.CanSwitchTabs)
				{
					if (this.IsHotKeyReleasedInAnyLayer("SwitchToPreviousTab"))
					{
						if (this._dataSource.IsInSmeltingMode)
						{
							UISoundsHelper.PlayUISound("event:/ui/crafting/refine_tab");
							this._dataSource.ExecuteSwitchToRefinement();
						}
						else if (this._dataSource.IsInCraftingMode)
						{
							UISoundsHelper.PlayUISound("event:/ui/crafting/smelt_tab");
							this._dataSource.ExecuteSwitchToSmelting();
						}
						else if (this._dataSource.IsInRefinementMode)
						{
							UISoundsHelper.PlayUISound("event:/ui/crafting/craft_tab");
							this._dataSource.ExecuteSwitchToCrafting();
						}
					}
					else if (this.IsHotKeyReleasedInAnyLayer("SwitchToNextTab"))
					{
						if (this._dataSource.IsInSmeltingMode)
						{
							UISoundsHelper.PlayUISound("event:/ui/crafting/craft_tab");
							this._dataSource.ExecuteSwitchToCrafting();
						}
						else if (this._dataSource.IsInCraftingMode)
						{
							UISoundsHelper.PlayUISound("event:/ui/crafting/refine_tab");
							this._dataSource.ExecuteSwitchToRefinement();
						}
						else if (this._dataSource.IsInRefinementMode)
						{
							UISoundsHelper.PlayUISound("event:/ui/crafting/smelt_tab");
							this._dataSource.ExecuteSwitchToSmelting();
						}
					}
				}
			}
			bool flag = false;
			if (GauntletCraftingScreen._reloadXmlPath.Key != null && GauntletCraftingScreen._reloadXmlPath.Value != null)
			{
				this.ReloadPieces();
				flag = true;
			}
			if (!flag)
			{
				if (base.DebugInput.IsHotKeyPressed("Reset"))
				{
					this.OnResetCamera();
				}
				if (this._dataSource.IsInCraftingMode)
				{
					float num = 0f;
					float num2 = 0f;
					if (Input.IsGamepadActive)
					{
						num = this._sceneLayer.Input.GetGameKeyAxis("CameraAxisX");
						num2 = this._sceneLayer.Input.GetGameKeyAxis("CameraAxisY");
					}
					else if (this._sceneLayer.Input.IsHotKeyDown("Rotate") || this._sceneLayer.Input.IsHotKeyDown("Zoom"))
					{
						num = this._sceneLayer.Input.GetMouseMoveX();
						num2 = this._sceneLayer.Input.GetMouseMoveY();
					}
					if (num != 0f || num2 != 0f)
					{
						this.OnMouseMove(num, num2, dt);
					}
					this.ZoomTick(dt);
				}
				this._craftingScene.SetDepthOfFieldParameters(this._dofParams.x, this._dofParams.z, false);
				this._craftingScene.SetDepthOfFieldFocus(this._initialEntityFrame.origin.Distance(this._cameraFrame.origin));
				if (this._dataSource.IsInCraftingMode)
				{
					this._craftingEntity.SetFrame(ref this._craftingEntityFrame);
				}
				this.SceneView.SetCamera(this._camera);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000033E1 File Offset: 0x000015E1
		private void OnClose()
		{
			ICampaignMission campaignMission = CampaignMission.Current;
			if (campaignMission != null)
			{
				campaignMission.EndMission();
			}
			Game.Current.GameStateManager.PopState(0);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003403 File Offset: 0x00001603
		private void OnResetCamera()
		{
			this._sceneLayer.InputRestrictions.SetMouseVisibility(true);
			this.ResetEntityAndCamera();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000341C File Offset: 0x0000161C
		private void OnWeaponCrafted()
		{
			WeaponDesignResultPopupVM craftingResultPopup = this._dataSource.WeaponDesign.CraftingResultPopup;
			if (craftingResultPopup == null)
			{
				return;
			}
			craftingResultPopup.SetDoneInputKey(HotKeyManager.GetCategory("CraftingHotkeyCategory").GetHotKey("Confirm"));
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000344C File Offset: 0x0000164C
		public void OnCraftingLogicInitialized()
		{
			CraftingVM dataSource = this._dataSource;
			if (dataSource != null)
			{
				dataSource.OnFinalize();
			}
			this._dataSource = new CraftingVM(this._craftingState.CraftingLogic, new Action(this.OnClose), new Action(this.OnResetCamera), new Action(this.OnWeaponCrafted), new Func<WeaponComponentData, ItemObject.ItemUsageSetFlags>(this.GetItemUsageSetFlag))
			{
				OnItemRefreshed = new CraftingVM.OnItemRefreshedDelegate(this.RefreshItemEntity)
			};
			this._dataSource.WeaponDesign.CraftingHistory.SetDoneKey(HotKeyManager.GetCategory("CraftingHotkeyCategory").GetHotKey("Confirm"));
			this._dataSource.WeaponDesign.CraftingHistory.SetCancelKey(HotKeyManager.GetCategory("CraftingHotkeyCategory").GetHotKey("Exit"));
			this._dataSource.SetConfirmInputKey(HotKeyManager.GetCategory("CraftingHotkeyCategory").GetHotKey("Confirm"));
			this._dataSource.SetExitInputKey(HotKeyManager.GetCategory("CraftingHotkeyCategory").GetHotKey("Exit"));
			this._dataSource.SetPreviousTabInputKey(HotKeyManager.GetCategory("CraftingHotkeyCategory").GetHotKey("SwitchToPreviousTab"));
			this._dataSource.SetNextTabInputKey(HotKeyManager.GetCategory("CraftingHotkeyCategory").GetHotKey("SwitchToNextTab"));
			this._dataSource.AddCameraControlInputKey(HotKeyManager.GetCategory("CraftingHotkeyCategory").GetGameKey(55));
			this._dataSource.AddCameraControlInputKey(HotKeyManager.GetCategory("CraftingHotkeyCategory").GetGameKey(56));
			this._dataSource.AddCameraControlInputKey(HotKeyManager.GetCategory("CraftingHotkeyCategory").RegisteredGameAxisKeys.FirstOrDefault((GameAxisKey x) => x.Id == "CameraAxisX"));
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003606 File Offset: 0x00001806
		public void OnCraftingLogicRefreshed()
		{
			this._dataSource.OnCraftingLogicRefreshed(this._craftingState.CraftingLogic);
			if (this._isInitialized)
			{
				this.RefreshItemEntity(true);
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003630 File Offset: 0x00001830
		private void OpenScene()
		{
			this._craftingScene = Scene.CreateNewScene(true, false, DecalAtlasGroup.All, "mono_renderscene");
			this._craftingScene.SetName("GauntletCraftingScreen");
			SceneInitializationData sceneInitializationData = default(SceneInitializationData);
			sceneInitializationData.InitPhysicsWorld = false;
			this._craftingScene.Read("crafting_menu_outdoor", ref sceneInitializationData, "");
			this._craftingScene.DisableStaticShadows(true);
			this._craftingScene.SetShadow(true);
			this._craftingScene.SetClothSimulationState(true);
			this.InitializeEntityAndCamera();
			this._sceneLayer = new SceneLayer("SceneLayer", true, true);
			this._sceneLayer.IsFocusLayer = true;
			this._sceneLayer.InputRestrictions.SetCanOverrideFocusOnHit(true);
			base.AddLayer(this._sceneLayer);
			this.SceneView.SetScene(this._craftingScene);
			this.SceneView.SetCamera(this._camera);
			this.SceneView.SetSceneUsesShadows(true);
			this.SceneView.SetAcceptGlobalDebugRenderObjects(true);
			this.SceneView.SetRenderWithPostfx(true);
			this.SceneView.SetResolutionScaling(true);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003740 File Offset: 0x00001940
		private void InitializeEntityAndCamera()
		{
			GameEntity gameEntity = this._craftingScene.FindEntityWithTag("weapon_point");
			MatrixFrame globalFrame = gameEntity.GetGlobalFrame();
			this._craftingScene.RemoveEntity(gameEntity, 114);
			globalFrame.Elevate(1.6f);
			this._craftingEntityFrame = globalFrame;
			this._initialEntityFrame = this._craftingEntityFrame;
			this._craftingEntity = GameEntity.CreateEmpty(this._craftingScene, true);
			this._craftingEntity.SetFrame(ref this._craftingEntityFrame);
			this._camera = Camera.CreateCamera();
			this._dofParams = default(Vec3);
			this._curCamSpeed = new Vec2(0f, 0f);
			GameEntity gameEntity2 = this._craftingScene.FindEntityWithTag("camera_point");
			gameEntity2.GetCameraParamsFromCameraScript(this._camera, ref this._dofParams);
			float fovVertical = this._camera.GetFovVertical();
			float aspectRatio = Screen.AspectRatio;
			float near = this._camera.Near;
			float far = this._camera.Far;
			this._camera.SetFovVertical(fovVertical, aspectRatio, near, far);
			this._craftingScene.SetDepthOfFieldParameters(this._dofParams.x, this._dofParams.z, false);
			this._craftingScene.SetDepthOfFieldFocus(this._dofParams.y);
			this._cameraFrame = gameEntity2.GetFrame();
			this._initialCameraFrame = this._cameraFrame;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003894 File Offset: 0x00001A94
		private void RefreshItemEntity(bool isItemVisible)
		{
			this._dataSource.WeaponDesign.CurrentWeaponHasScabbard = false;
			if (this._craftingEntity != null)
			{
				this._craftingEntityFrame = this._craftingEntity.GetFrame();
				this._craftingEntity.Remove(115);
				this._craftingEntity = null;
			}
			if (isItemVisible)
			{
				this._craftingEntity = GameEntity.CreateEmpty(this._craftingScene, true);
				this._craftingEntity.SetFrame(ref this._craftingEntityFrame);
				this._craftedData = this._craftingState.CraftingLogic.CurrentWeaponDesign;
				if (this._craftedData != null)
				{
					this._craftingEntityFrame = this._craftingEntity.GetFrame();
					float num = this._craftedData.CraftedWeaponLength / 2f;
					this._craftingEntity.SetFrame(ref this._craftingEntityFrame);
					BladeData bladeData = this._craftedData.UsedPieces[0].CraftingPiece.BladeData;
					this._dataSource.WeaponDesign.CurrentWeaponHasScabbard = !string.IsNullOrEmpty(bladeData.HolsterMeshName);
					MetaMesh metaMesh;
					if (!this._dataSource.WeaponDesign.IsScabbardVisible)
					{
						metaMesh = CraftedDataView.BuildWeaponMesh(this._craftedData, -num, false, false);
					}
					else
					{
						metaMesh = CraftedDataView.BuildHolsterMeshWithWeapon(this._craftedData, -num, false);
						if (metaMesh == null)
						{
							metaMesh = CraftedDataView.BuildWeaponMesh(this._craftedData, -num, false, false);
						}
					}
					this._craftingEntity = this._craftingScene.AddItemEntity(ref this._craftingEntityFrame, metaMesh);
				}
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003A04 File Offset: 0x00001C04
		private void OnMouseMove(float deltaX, float deltaY, float dT)
		{
			if (!base.DebugInput.IsControlDown() && !base.DebugInput.IsAltDown())
			{
				if (Input.IsGamepadActive)
				{
					if (Mathf.Abs(deltaX) > 0.1f)
					{
						deltaX = (deltaX - Mathf.Sign(deltaX) * 0.1f) / 0.9f;
						this._craftingEntityFrame.rotation.RotateAboutUp(2f * deltaX * 3.1415927f / 180f);
					}
					if (Mathf.Abs(deltaY) > 0.1f)
					{
						deltaY = (deltaY - Mathf.Sign(deltaY) * 0.1f) / 0.9f;
						this._craftingEntityFrame.rotation.RotateAboutSide(2f * deltaY * 3.1415927f / 180f);
					}
				}
				else if (this._sceneLayer.Input.IsHotKeyDown("Rotate"))
				{
					Vec2 vec = new Vec2(0.02f, 0.02f);
					Vec2 vec2 = new Vec2(deltaX, -deltaY);
					Vec2 vec3 = new Vec2(vec2.x / vec.x, vec2.y / vec.y);
					Vec2 v = new Vec2(dT * vec3.x, dT * vec3.y);
					float f = 0.95f;
					this._curCamSpeed = this._curCamSpeed * f + v;
					Vec2 vec4 = new Vec2(this._curCamSpeed.x * dT, this._curCamSpeed.y * dT);
					this._craftingEntityFrame.rotation.RotateAboutAnArbitraryVector(Vec3.Side, vec4.y * 3.1415927f / 180f);
					this._craftingEntityFrame.rotation.RotateAboutAnArbitraryVector(Vec3.Up, vec4.x * 3.1415927f / 180f);
					MBWindowManager.DontChangeCursorPos();
					this._sceneLayer.InputRestrictions.SetMouseVisibility(false);
					this._gauntletLayer.InputRestrictions.SetMouseVisibility(false);
				}
				else if (this._sceneLayer.Input.IsHotKeyDown("Zoom"))
				{
					float num = (MathF.Abs(deltaX) >= MathF.Abs(deltaY)) ? deltaX : deltaY;
					this._craftingEntityFrame.rotation.RotateAboutUp(num * 3.1415927f / 180f * 0.15f);
					MBWindowManager.DontChangeCursorPos();
					this._sceneLayer.InputRestrictions.SetMouseVisibility(false);
					this._gauntletLayer.InputRestrictions.SetMouseVisibility(false);
				}
				if (this._sceneLayer.Input.IsHotKeyDown("Rotate") && this._sceneLayer.Input.IsHotKeyDown("Zoom"))
				{
					this.ResetEntityAndCamera();
				}
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003CA0 File Offset: 0x00001EA0
		private float GetActiveZoomAmount()
		{
			if (Input.IsGamepadActive)
			{
				float gameKeyState = this._sceneLayer.Input.GetGameKeyState(55);
				return this._sceneLayer.Input.GetGameKeyState(56) - gameKeyState;
			}
			return MBMath.ClampFloat(this._zoomAmount - (float)MathF.Sign(this._sceneLayer.Input.GetDeltaMouseScroll()) * 0.05f, -0.6f, 0.5f);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003D10 File Offset: 0x00001F10
		private void ZoomTick(float dt)
		{
			this._zoomAmount = this.GetActiveZoomAmount();
			if (MathF.Abs(this._zoomAmount) < 1E-05f)
			{
				this._zoomAmount = 0f;
				return;
			}
			int num = MathF.Sign(this._zoomAmount);
			Vec3 v = (float)(-(float)num) * (this._initialEntityFrame.origin - this._cameraFrame.origin);
			v.Normalize();
			float num2 = Input.IsGamepadActive ? 2f : 5f;
			float num3 = dt * num2;
			this._cameraFrame.origin = this._cameraFrame.origin + v * num3;
			this._zoomAmount += (float)(-(float)num) * num3;
			float num4 = this._initialEntityFrame.origin.Distance(this._cameraFrame.origin);
			if (num4 > 3.3f)
			{
				this._cameraFrame.origin = this._cameraFrame.origin + (float)(-(float)num) * v * (num4 - 3.3f);
				num4 = this._initialEntityFrame.origin.Distance(this._cameraFrame.origin);
				this._zoomAmount = 0f;
			}
			else if (num4 < 0.55f)
			{
				this._cameraFrame.origin = this._cameraFrame.origin + (float)(-(float)num) * v * (num4 - 0.55f);
				num4 = this._initialEntityFrame.origin.Distance(this._cameraFrame.origin);
				this._zoomAmount = 0f;
			}
			else if (num != MathF.Sign(this._zoomAmount))
			{
				this._zoomAmount = 0f;
			}
			this._camera.Frame = this._cameraFrame;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003EDA File Offset: 0x000020DA
		private void ResetEntityAndCamera()
		{
			this._zoomAmount = 0f;
			this._craftingEntityFrame = this._initialEntityFrame;
			this._cameraFrame = this._initialCameraFrame;
			this._camera.Frame = this._cameraFrame;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003F10 File Offset: 0x00002110
		private void CopyXmlCode()
		{
			Input.SetClipboardText(this._craftingState.CraftingLogic.GetXmlCodeForCurrentItem(this._craftingState.CraftingLogic.GetCurrentCraftedItemObject(false)));
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003F38 File Offset: 0x00002138
		private void PasteXmlCode()
		{
			string clipboardText = Input.GetClipboardText();
			if (!string.IsNullOrEmpty(clipboardText))
			{
				ItemObject @object = MBObjectManager.Instance.GetObject<ItemObject>(clipboardText);
				if (@object != null)
				{
					this.SwithToCraftedItem(@object);
					return;
				}
				CraftingTemplate craftingTemplate;
				ValueTuple<CraftingPiece, int>[] pieces;
				if (this._craftingState.CraftingLogic.TryGetWeaponPropertiesFromXmlCode(clipboardText, out craftingTemplate, out pieces))
				{
					this._dataSource.SetCurrentDesignManually(craftingTemplate, pieces);
				}
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003F90 File Offset: 0x00002190
		private void SwithToCraftedItem(ItemObject itemObject)
		{
			if (itemObject != null && itemObject.IsCraftedWeapon)
			{
				if (!this._dataSource.IsInCraftingMode)
				{
					this._dataSource.ExecuteSwitchToCrafting();
				}
				WeaponDesign weaponDesign = itemObject.WeaponDesign;
				if (this._craftingState.CraftingLogic.CurrentCraftingTemplate != weaponDesign.Template)
				{
					this._dataSource.WeaponDesign.SelectPrimaryWeaponClass(weaponDesign.Template);
				}
				foreach (WeaponDesignElement weaponDesignElement in weaponDesign.UsedPieces)
				{
					if (weaponDesignElement.IsValid)
					{
						this._dataSource.WeaponDesign.SwitchToPiece(weaponDesignElement);
					}
				}
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000402A File Offset: 0x0000222A
		private ItemObject.ItemUsageSetFlags GetItemUsageSetFlag(WeaponComponentData item)
		{
			if (!string.IsNullOrEmpty(item.ItemUsage))
			{
				return MBItem.GetItemUsageSetFlags(item.ItemUsage);
			}
			return (ItemObject.ItemUsageSetFlags)0;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00004046 File Offset: 0x00002246
		private bool IsHotKeyReleasedInAnyLayer(string hotKeyId)
		{
			return this._sceneLayer.Input.IsHotKeyReleased(hotKeyId) || this._gauntletLayer.Input.IsHotKeyReleased(hotKeyId);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000406E File Offset: 0x0000226E
		void IGameStateListener.OnInitialize()
		{
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00004070 File Offset: 0x00002270
		void IGameStateListener.OnFinalize()
		{
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00004072 File Offset: 0x00002272
		void IGameStateListener.OnActivate()
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00004074 File Offset: 0x00002274
		void IGameStateListener.OnDeactivate()
		{
		}

		// Token: 0x0400000B RID: 11
		private const float _controllerRotationSensitivity = 2f;

		// Token: 0x0400000C RID: 12
		private Scene _craftingScene;

		// Token: 0x0400000D RID: 13
		private SceneLayer _sceneLayer;

		// Token: 0x0400000E RID: 14
		private readonly CraftingState _craftingState;

		// Token: 0x0400000F RID: 15
		private CraftingVM _dataSource;

		// Token: 0x04000010 RID: 16
		private GauntletLayer _gauntletLayer;

		// Token: 0x04000011 RID: 17
		private IGauntletMovie _gauntletMovie;

		// Token: 0x04000012 RID: 18
		private SpriteCategory _craftingCategory;

		// Token: 0x04000013 RID: 19
		private Camera _camera;

		// Token: 0x04000014 RID: 20
		private MatrixFrame _cameraFrame;

		// Token: 0x04000015 RID: 21
		private MatrixFrame _initialCameraFrame;

		// Token: 0x04000016 RID: 22
		private Vec3 _dofParams;

		// Token: 0x04000017 RID: 23
		private Vec2 _curCamSpeed;

		// Token: 0x04000018 RID: 24
		private float _zoomAmount;

		// Token: 0x04000019 RID: 25
		private GameEntity _craftingEntity;

		// Token: 0x0400001A RID: 26
		private MatrixFrame _craftingEntityFrame = MatrixFrame.Identity;

		// Token: 0x0400001B RID: 27
		private MatrixFrame _initialEntityFrame;

		// Token: 0x0400001C RID: 28
		private WeaponDesign _craftedData;

		// Token: 0x0400001D RID: 29
		private bool _isInitialized;

		// Token: 0x0400001E RID: 30
		private static KeyValuePair<string, string> _reloadXmlPath;
	}
}
