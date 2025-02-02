using System;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.Options;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade.View.Tableaus
{
	// Token: 0x02000024 RID: 36
	public class ItemTableau
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000B528 File Offset: 0x00009728
		// (set) Token: 0x06000150 RID: 336 RVA: 0x0000B530 File Offset: 0x00009730
		public Texture Texture { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000B539 File Offset: 0x00009739
		private TableauView View
		{
			get
			{
				if (this.Texture != null)
				{
					return this.Texture.TableauView;
				}
				return null;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000152 RID: 338 RVA: 0x0000B556 File Offset: 0x00009756
		private bool _isSizeValid
		{
			get
			{
				return this._tableauSizeX > 0 && this._tableauSizeY > 0;
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000B56C File Offset: 0x0000976C
		public ItemTableau()
		{
			this.SetEnabled(true);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000B5C8 File Offset: 0x000097C8
		public void SetTargetSize(int width, int height)
		{
			bool isSizeValid = this._isSizeValid;
			this._isRotating = false;
			if (width <= 0 || height <= 0)
			{
				this._tableauSizeX = 10;
				this._tableauSizeY = 10;
			}
			else
			{
				this.RenderScale = NativeOptions.GetConfig(NativeOptions.NativeOptionsType.ResolutionScale) / 100f;
				this._tableauSizeX = (int)((float)width * this.RenderScale);
				this._tableauSizeY = (int)((float)height * this.RenderScale);
			}
			this._cameraRatio = (float)this._tableauSizeX / (float)this._tableauSizeY;
			TableauView view = this.View;
			if (view != null)
			{
				view.SetEnable(false);
			}
			TableauView view2 = this.View;
			if (view2 != null)
			{
				view2.AddClearTask(true);
			}
			Texture texture = this.Texture;
			if (texture != null)
			{
				texture.ReleaseNextFrame();
			}
			if (!isSizeValid && this._isSizeValid)
			{
				this.Recalculate();
			}
			this.Texture = TableauView.AddTableau("ItemTableau", new RenderTargetComponent.TextureUpdateEventHandler(this.TableauMaterialTabInventoryItemTooltipOnRender), this._tableauScene, this._tableauSizeX, this._tableauSizeY);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000B6B8 File Offset: 0x000098B8
		public void OnFinalize()
		{
			TableauView view = this.View;
			if (view != null)
			{
				view.SetEnable(false);
			}
			Camera camera = this._camera;
			if (camera != null)
			{
				camera.ReleaseCameraEntity();
			}
			this._camera = null;
			TableauView view2 = this.View;
			if (view2 != null)
			{
				view2.AddClearTask(false);
			}
			this._tableauScene = null;
			this.Texture = null;
			this._initialized = false;
			if (this._lockMouse)
			{
				this.UpdateMouseLock(true);
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000B728 File Offset: 0x00009928
		protected void SetEnabled(bool enabled)
		{
			this._isRotatingByDefault = true;
			this._isRotating = false;
			this.ResetCamera();
			this._isEnabled = enabled;
			TableauView view = this.View;
			if (view != null)
			{
				view.SetEnable(this._isEnabled);
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000B76C File Offset: 0x0000996C
		public void SetStringId(string stringId)
		{
			this._stringId = stringId;
			this.Recalculate();
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000B77B File Offset: 0x0000997B
		public void SetAmmo(int ammo)
		{
			this._ammo = ammo;
			this.Recalculate();
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000B78A File Offset: 0x0000998A
		public void SetAverageUnitCost(int averageUnitCost)
		{
			this._averageUnitCost = averageUnitCost;
			this.Recalculate();
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000B799 File Offset: 0x00009999
		public void SetItemModifierId(string itemModifierId)
		{
			this._itemModifierId = itemModifierId;
			this.Recalculate();
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000B7A8 File Offset: 0x000099A8
		public void SetBannerCode(string bannerCode)
		{
			this._bannerCode = bannerCode;
			this.Recalculate();
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000B7B8 File Offset: 0x000099B8
		public void Recalculate()
		{
			if (UiStringHelper.IsStringNoneOrEmptyForUi(this._stringId) || !this._isSizeValid)
			{
				return;
			}
			ItemModifier itemModifier = null;
			ItemObject itemObject = MBObjectManager.Instance.GetObject<ItemObject>(this._stringId);
			if (itemObject == null)
			{
				itemObject = Game.Current.ObjectManager.GetObjectTypeList<ItemObject>().FirstOrDefault((ItemObject item) => item.IsCraftedWeapon && item.WeaponDesign.HashedCode == this._stringId);
			}
			if (!string.IsNullOrEmpty(this._itemModifierId))
			{
				itemModifier = MBObjectManager.Instance.GetObject<ItemModifier>(this._itemModifierId);
			}
			if (itemObject == null)
			{
				return;
			}
			this._itemRosterElement = new ItemRosterElement(itemObject, this._ammo, itemModifier);
			this.RefreshItemTableau();
			if (this._itemTableauEntity != null)
			{
				float num = Screen.RealScreenResolutionWidth / (float)this._tableauSizeX;
				float num2 = Screen.RealScreenResolutionHeight / (float)this._tableauSizeY;
				float num3 = (num > num2) ? num : num2;
				if (num3 < 1f)
				{
					Vec3 globalBoxMax = this._itemTableauEntity.GlobalBoxMax;
					Vec3 globalBoxMin = this._itemTableauEntity.GlobalBoxMin;
					this._itemTableauFrame = this._itemTableauEntity.GetFrame();
					float length = this._itemTableauFrame.rotation.f.Length;
					this._itemTableauFrame.rotation.Orthonormalize();
					this._itemTableauFrame.rotation.ApplyScaleLocal(length * num3);
					this._itemTableauEntity.SetFrame(ref this._itemTableauFrame);
					if (globalBoxMax.NearlyEquals(this._itemTableauEntity.GlobalBoxMax, 1E-05f) && globalBoxMin.NearlyEquals(this._itemTableauEntity.GlobalBoxMin, 1E-05f))
					{
						this._itemTableauEntity.SetBoundingboxDirty();
						this._itemTableauEntity.RecomputeBoundingBox();
					}
					this._itemTableauFrame.origin = this._itemTableauFrame.origin + (globalBoxMax + globalBoxMin - this._itemTableauEntity.GlobalBoxMax - this._itemTableauEntity.GlobalBoxMin) * 0.5f;
					this._itemTableauEntity.SetFrame(ref this._itemTableauFrame);
					this._midPoint = (this._itemTableauEntity.GlobalBoxMax + this._itemTableauEntity.GlobalBoxMin) * 0.5f + (globalBoxMax + globalBoxMin - this._itemTableauEntity.GlobalBoxMax - this._itemTableauEntity.GlobalBoxMin) * 0.5f;
				}
				else
				{
					this._midPoint = (this._itemTableauEntity.GlobalBoxMax + this._itemTableauEntity.GlobalBoxMin) * 0.5f;
				}
				if (this._itemRosterElement.EquipmentElement.Item.ItemType == ItemObject.ItemTypeEnum.HandArmor || this._itemRosterElement.EquipmentElement.Item.ItemType == ItemObject.ItemTypeEnum.Shield)
				{
					this._midPoint *= 1.2f;
				}
				this.ResetCamera();
			}
			this._isRotatingByDefault = true;
			this._isRotating = false;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000BAA4 File Offset: 0x00009CA4
		public void Initialize()
		{
			this._isRotatingByDefault = true;
			this._isRotating = false;
			this._isTranslating = false;
			this._tableauScene = Scene.CreateNewScene(true, true, DecalAtlasGroup.All, "mono_renderscene");
			this._tableauScene.SetName("ItemTableau");
			this._tableauScene.DisableStaticShadows(true);
			this._tableauScene.SetAtmosphereWithName("character_menu_a");
			Vec3 vec = new Vec3(1f, -1f, -1f, -1f);
			this._tableauScene.SetSunDirection(ref vec);
			this._tableauScene.SetClothSimulationState(false);
			this.ResetCamera();
			this._initialized = true;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000BB46 File Offset: 0x00009D46
		private void TranslateCamera(bool value)
		{
			this.TranslateCameraAux(value);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000BB4F File Offset: 0x00009D4F
		private void TranslateCameraAux(bool value)
		{
			this._isRotatingByDefault = (!value && this._isRotatingByDefault);
			this._isTranslating = value;
			this.UpdateMouseLock(false);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000BB74 File Offset: 0x00009D74
		private void ResetCamera()
		{
			this._curCamDisplacement = Vec3.Zero;
			this._curZoomSpeed = 0f;
			if (this._camera != null)
			{
				this._camera.Frame = MatrixFrame.Identity;
				this.SetCamFovHorizontal(1f);
				this.MakeCameraLookMidPoint();
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000BBC6 File Offset: 0x00009DC6
		public void RotateItem(bool value)
		{
			this._isRotatingByDefault = (!value && this._isRotatingByDefault);
			this._isRotating = value;
			this.UpdateMouseLock(false);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000BBE8 File Offset: 0x00009DE8
		public void RotateItemVerticalWithAmount(float value)
		{
			this.UpdateRotation(0f, value / -2f);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000BBFC File Offset: 0x00009DFC
		public void RotateItemHorizontalWithAmount(float value)
		{
			this.UpdateRotation(value / 2f, 0f);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000BC10 File Offset: 0x00009E10
		public void OnTick(float dt)
		{
			float num = Input.MouseMoveX + Input.GetKeyState(InputKey.ControllerLStick).X * 1000f * dt;
			float num2 = Input.MouseMoveY + Input.GetKeyState(InputKey.ControllerLStick).Y * -1000f * dt;
			if (this._isEnabled && (this._isRotating || this._isTranslating) && (!num.ApproximatelyEqualsTo(0f, 1E-05f) || !num2.ApproximatelyEqualsTo(0f, 1E-05f)))
			{
				if (this._isRotating)
				{
					this.UpdateRotation(num, num2);
				}
				if (this._isTranslating)
				{
					this.UpdatePosition(num, num2);
				}
			}
			this.TickCameraZoom(dt);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000BCC4 File Offset: 0x00009EC4
		private void UpdatePosition(float mouseMoveX, float mouseMoveY)
		{
			if (this._initialized)
			{
				Vec3 vec = new Vec3(mouseMoveX / (float)(-(float)this._tableauSizeX), mouseMoveY / (float)this._tableauSizeY, 0f, -1f);
				vec *= 2.2f * this._camera.HorizontalFov;
				this._curCamDisplacement += vec;
				this.MakeCameraLookMidPoint();
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000BD30 File Offset: 0x00009F30
		private void UpdateRotation(float mouseMoveX, float mouseMoveY)
		{
			if (this._initialized)
			{
				this._panRotation += mouseMoveX * 0.004363323f;
				this._tiltRotation += mouseMoveY * 0.004363323f;
				this._tiltRotation = MathF.Clamp(this._tiltRotation, -2.984513f, -0.15707964f);
				MatrixFrame m = this._itemTableauEntity.GetFrame();
				Vec3 vec = (this._itemTableauEntity.GetBoundingBoxMax() + this._itemTableauEntity.GetBoundingBoxMin()) * 0.5f;
				MatrixFrame identity = MatrixFrame.Identity;
				identity.origin = vec;
				MatrixFrame identity2 = MatrixFrame.Identity;
				identity2.origin = -vec;
				m *= identity;
				m.rotation = Mat3.Identity;
				m.rotation.ApplyScaleLocal(this._initialFrame.rotation.GetScaleVector());
				m.rotation.RotateAboutSide(this._tiltRotation);
				m.rotation.RotateAboutUp(this._panRotation);
				m *= identity2;
				this._itemTableauEntity.SetFrame(ref m);
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000BE47 File Offset: 0x0000A047
		public void SetInitialTiltRotation(float amount)
		{
			this._hasInitialTiltRotation = true;
			this._initialTiltRotation = amount;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000BE57 File Offset: 0x0000A057
		public void SetInitialPanRotation(float amount)
		{
			this._hasInitialPanRotation = true;
			this._initialPanRotation = amount;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000BE67 File Offset: 0x0000A067
		public void Zoom(double value)
		{
			this._curZoomSpeed -= (float)(value / 1000.0);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000BE82 File Offset: 0x0000A082
		public void SetItem(ItemRosterElement itemRosterElement)
		{
			this._itemRosterElement = itemRosterElement;
			this.RefreshItemTableau();
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000BE94 File Offset: 0x0000A094
		private void RefreshItemTableau()
		{
			if (!this._initialized)
			{
				this.Initialize();
			}
			if (this._itemTableauEntity != null)
			{
				this._itemTableauEntity.Remove(102);
				this._itemTableauEntity = null;
			}
			if (this._itemRosterElement.EquipmentElement.Item != null)
			{
				ItemObject.ItemTypeEnum itemType = this._itemRosterElement.EquipmentElement.Item.ItemType;
				if (this._itemTableauEntity == null)
				{
					MatrixFrame itemFrameForItemTooltip = this._itemRosterElement.GetItemFrameForItemTooltip();
					itemFrameForItemTooltip.origin.z = itemFrameForItemTooltip.origin.z + 2.5f;
					MetaMesh itemMeshForInventory = this._itemRosterElement.GetItemMeshForInventory(false);
					Banner banner = new Banner(this._bannerCode);
					uint color = 0U;
					uint color2 = 0U;
					if (!string.IsNullOrEmpty(this._bannerCode))
					{
						color = banner.GetPrimaryColor();
						BannerColor bannerColor;
						if (banner.BannerDataList.Count > 0 && BannerManager.ColorPalette.TryGetValue(banner.BannerDataList[1].ColorId, out bannerColor))
						{
							color2 = bannerColor.Color;
						}
					}
					if (itemMeshForInventory != null)
					{
						if (itemType == ItemObject.ItemTypeEnum.HandArmor)
						{
							this._itemTableauEntity = GameEntity.CreateEmpty(this._tableauScene, true);
							AnimationSystemData animationSystemData = Game.Current.DefaultMonster.FillAnimationSystemData(MBActionSet.GetActionSet(Game.Current.DefaultMonster.ActionSetCode), 1f, false);
							this._itemTableauEntity.CreateSkeletonWithActionSet(ref animationSystemData);
							this._itemTableauEntity.SetFrame(ref itemFrameForItemTooltip);
							this._itemTableauEntity.Skeleton.SetAgentActionChannel(0, ActionIndexCache.Create("act_tableau_hand_armor_pose"), 0f, -0.2f, true);
							this._itemTableauEntity.AddMultiMeshToSkeleton(itemMeshForInventory);
							this._itemTableauEntity.Skeleton.TickActionChannels();
							this._itemTableauEntity.Skeleton.TickAnimationsAndForceUpdate(0.01f, itemFrameForItemTooltip, true);
						}
						else if (itemType == ItemObject.ItemTypeEnum.Horse || itemType == ItemObject.ItemTypeEnum.Animal)
						{
							HorseComponent horseComponent = this._itemRosterElement.EquipmentElement.Item.HorseComponent;
							Monster monster = horseComponent.Monster;
							this._itemTableauEntity = GameEntity.CreateEmpty(this._tableauScene, true);
							AnimationSystemData animationSystemData2 = monster.FillAnimationSystemData(MBGlobals.GetActionSet(horseComponent.Monster.ActionSetCode), 1f, false);
							this._itemTableauEntity.CreateSkeletonWithActionSet(ref animationSystemData2);
							this._itemTableauEntity.Skeleton.SetAgentActionChannel(0, ActionIndexCache.Create("act_inventory_idle_start"), 0f, -0.2f, true);
							this._itemTableauEntity.SetFrame(ref itemFrameForItemTooltip);
							this._itemTableauEntity.AddMultiMeshToSkeleton(itemMeshForInventory);
						}
						else if (itemType == ItemObject.ItemTypeEnum.HorseHarness && this._itemRosterElement.EquipmentElement.Item.ArmorComponent != null)
						{
							this._itemTableauEntity = this._tableauScene.AddItemEntity(ref itemFrameForItemTooltip, itemMeshForInventory);
							MetaMesh copy = MetaMesh.GetCopy(this._itemRosterElement.EquipmentElement.Item.ArmorComponent.ReinsMesh, true, true);
							if (copy != null)
							{
								this._itemTableauEntity.AddMultiMesh(copy, true);
							}
						}
						else if (itemType == ItemObject.ItemTypeEnum.Shield)
						{
							if (this._itemRosterElement.EquipmentElement.Item.IsUsingTableau && !banner.BannerDataList.IsEmpty<BannerData>())
							{
								itemMeshForInventory.SetMaterial(this._itemRosterElement.EquipmentElement.Item.GetTableauMaterial(banner));
							}
							this._itemTableauEntity = this._tableauScene.AddItemEntity(ref itemFrameForItemTooltip, itemMeshForInventory);
						}
						else if (itemType == ItemObject.ItemTypeEnum.Banner)
						{
							if (this._itemRosterElement.EquipmentElement.Item.IsUsingTableau && !banner.BannerDataList.IsEmpty<BannerData>())
							{
								itemMeshForInventory.SetMaterial(this._itemRosterElement.EquipmentElement.Item.GetTableauMaterial(banner));
							}
							if (!string.IsNullOrEmpty(this._bannerCode))
							{
								for (int i = 0; i < itemMeshForInventory.MeshCount; i++)
								{
									itemMeshForInventory.GetMeshAtIndex(i).Color = color;
									itemMeshForInventory.GetMeshAtIndex(i).Color2 = color2;
								}
							}
							this._itemTableauEntity = this._tableauScene.AddItemEntity(ref itemFrameForItemTooltip, itemMeshForInventory);
						}
						else
						{
							this._itemTableauEntity = this._tableauScene.AddItemEntity(ref itemFrameForItemTooltip, itemMeshForInventory);
						}
					}
					else
					{
						MBDebug.ShowWarning("[DEBUG]Item with " + this._itemRosterElement.EquipmentElement.Item.StringId + "[DEBUG] string id cannot be found");
					}
				}
				SkinMask p = SkinMask.AllVisible;
				if (this._itemRosterElement.EquipmentElement.Item.HasArmorComponent)
				{
					p = this._itemRosterElement.EquipmentElement.Item.ArmorComponent.MeshesMask;
				}
				string text = "";
				bool flag = this._itemRosterElement.EquipmentElement.Item.ItemFlags.HasAnyFlag(ItemFlags.NotUsableByMale);
				bool flag2 = false;
				if (ItemObject.ItemTypeEnum.HeadArmor == itemType || ItemObject.ItemTypeEnum.Cape == itemType)
				{
					text = "base_head";
					flag2 = true;
				}
				else if (ItemObject.ItemTypeEnum.BodyArmor == itemType)
				{
					if (p.HasAnyFlag(SkinMask.BodyVisible))
					{
						text = "base_body";
						flag2 = true;
					}
				}
				else if (ItemObject.ItemTypeEnum.LegArmor == itemType)
				{
					if (p.HasAnyFlag(SkinMask.LegsVisible))
					{
						text = "base_foot";
						flag2 = true;
					}
				}
				else if (ItemObject.ItemTypeEnum.HandArmor == itemType)
				{
					if (p.HasAnyFlag(SkinMask.HandsVisible))
					{
						MetaMesh copy2 = MetaMesh.GetCopy(flag ? "base_hand_female" : "base_hand", false, false);
						this._itemTableauEntity.AddMultiMeshToSkeleton(copy2);
					}
				}
				else if (ItemObject.ItemTypeEnum.HorseHarness == itemType)
				{
					text = "horse_base_mesh";
					flag2 = false;
				}
				if (text.Length > 0)
				{
					if (flag2 && flag)
					{
						text += "_female";
					}
					MetaMesh copy3 = MetaMesh.GetCopy(text, false, false);
					this._itemTableauEntity.AddMultiMesh(copy3, true);
				}
				TableauView view = this.View;
				if (view != null)
				{
					float radius = (this._itemTableauEntity.GetBoundingBoxMax() - this._itemTableauEntity.GetBoundingBoxMin()).Length * 2f;
					Vec3 origin = this._itemTableauEntity.GetGlobalFrame().origin;
					view.SetFocusedShadowmap(true, ref origin, radius);
				}
				if (this._itemTableauEntity != null)
				{
					this._initialFrame = this._itemTableauEntity.GetFrame();
					Vec3 eulerAngles = this._initialFrame.rotation.GetEulerAngles();
					this._panRotation = eulerAngles.x;
					this._tiltRotation = eulerAngles.z;
					if (this._hasInitialPanRotation)
					{
						this._panRotation = this._initialPanRotation;
					}
					else if (itemType == ItemObject.ItemTypeEnum.Shield)
					{
						this._panRotation = -3.1415927f;
					}
					if (this._hasInitialTiltRotation)
					{
						this._tiltRotation = this._initialTiltRotation;
						return;
					}
					if (itemType == ItemObject.ItemTypeEnum.Shield)
					{
						this._tiltRotation = 0f;
						return;
					}
					this._tiltRotation = -1.5707964f;
				}
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000C52C File Offset: 0x0000A72C
		private void TableauMaterialTabInventoryItemTooltipOnRender(Texture sender, EventArgs e)
		{
			if (this._initialized)
			{
				TableauView tableauView = this.View;
				if (tableauView == null)
				{
					tableauView = sender.TableauView;
					tableauView.SetEnable(this._isEnabled);
				}
				if (this._itemRosterElement.EquipmentElement.Item == null)
				{
					tableauView.SetContinuousRendering(false);
					tableauView.SetDeleteAfterRendering(true);
					return;
				}
				tableauView.SetRenderWithPostfx(true);
				tableauView.SetClearColor(0U);
				tableauView.SetScene(this._tableauScene);
				if (this._camera == null)
				{
					this._camera = Camera.CreateCamera();
					this._camera.SetViewVolume(true, -0.5f, 0.5f, -0.5f, 0.5f, 0.01f, 100f);
					this.ResetCamera();
					tableauView.SetSceneUsesSkybox(false);
				}
				tableauView.SetCamera(this._camera);
				if (this._isRotatingByDefault)
				{
					this.UpdateRotation(1f, 0f);
				}
				tableauView.SetDeleteAfterRendering(false);
				tableauView.SetContinuousRendering(true);
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000C628 File Offset: 0x0000A828
		private void MakeCameraLookMidPoint()
		{
			Vec3 v = this._camera.Frame.rotation.TransformToParent(this._curCamDisplacement);
			Vec3 v2 = this._midPoint + v;
			float f = this._midPoint.Length * 0.5263158f;
			Vec3 position = v2 - this._camera.Direction * f;
			this._camera.Position = position;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000C695 File Offset: 0x0000A895
		private void SetCamFovHorizontal(float camFov)
		{
			this._camera.SetFovHorizontal(camFov, 1f, 0.1f, 50f);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000C6B2 File Offset: 0x0000A8B2
		private void UpdateMouseLock(bool forceUnlock = false)
		{
			this._lockMouse = ((this._isRotating || this._isTranslating) && !forceUnlock);
			MouseManager.LockCursorAtCurrentPosition(this._lockMouse);
			MouseManager.ShowCursor(!this._lockMouse);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000C6EC File Offset: 0x0000A8EC
		private void TickCameraZoom(float dt)
		{
			if (this._camera != null)
			{
				float num = this._camera.HorizontalFov;
				num += this._curZoomSpeed;
				num = MBMath.ClampFloat(num, 0.1f, 2f);
				this.SetCamFovHorizontal(num);
				if (dt > 0f)
				{
					this._curZoomSpeed = MBMath.Lerp(this._curZoomSpeed, 0f, MBMath.ClampFloat(dt * 25.9f, 0f, 1f), 1E-05f);
				}
			}
		}

		// Token: 0x040000DE RID: 222
		private Scene _tableauScene;

		// Token: 0x040000DF RID: 223
		private GameEntity _itemTableauEntity;

		// Token: 0x040000E0 RID: 224
		private MatrixFrame _itemTableauFrame = MatrixFrame.Identity;

		// Token: 0x040000E1 RID: 225
		private bool _isRotating;

		// Token: 0x040000E2 RID: 226
		private bool _isTranslating;

		// Token: 0x040000E3 RID: 227
		private bool _isRotatingByDefault;

		// Token: 0x040000E4 RID: 228
		private bool _initialized;

		// Token: 0x040000E5 RID: 229
		private int _tableauSizeX;

		// Token: 0x040000E6 RID: 230
		private int _tableauSizeY;

		// Token: 0x040000E7 RID: 231
		private float _cameraRatio;

		// Token: 0x040000E8 RID: 232
		private Camera _camera;

		// Token: 0x040000E9 RID: 233
		private Vec3 _midPoint;

		// Token: 0x040000EA RID: 234
		private const float InitialCamFov = 1f;

		// Token: 0x040000EB RID: 235
		private float _curZoomSpeed;

		// Token: 0x040000EC RID: 236
		private Vec3 _curCamDisplacement = Vec3.Zero;

		// Token: 0x040000ED RID: 237
		private bool _isEnabled;

		// Token: 0x040000EE RID: 238
		private float _panRotation;

		// Token: 0x040000EF RID: 239
		private float _tiltRotation;

		// Token: 0x040000F0 RID: 240
		private bool _hasInitialTiltRotation;

		// Token: 0x040000F1 RID: 241
		private float _initialTiltRotation;

		// Token: 0x040000F2 RID: 242
		private bool _hasInitialPanRotation;

		// Token: 0x040000F3 RID: 243
		private float _initialPanRotation;

		// Token: 0x040000F4 RID: 244
		private float RenderScale = 1f;

		// Token: 0x040000F5 RID: 245
		private string _stringId = "";

		// Token: 0x040000F6 RID: 246
		private int _ammo;

		// Token: 0x040000F7 RID: 247
		private int _averageUnitCost;

		// Token: 0x040000F8 RID: 248
		private string _itemModifierId = "";

		// Token: 0x040000F9 RID: 249
		private string _bannerCode = "";

		// Token: 0x040000FA RID: 250
		private ItemRosterElement _itemRosterElement;

		// Token: 0x040000FB RID: 251
		private MatrixFrame _initialFrame;

		// Token: 0x040000FC RID: 252
		private bool _lockMouse;
	}
}
