using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Engine;
using TaleWorlds.Engine.Options;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.View.Tableaus
{
	// Token: 0x02000023 RID: 35
	public class CharacterTableau
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000099E4 File Offset: 0x00007BE4
		// (set) Token: 0x0600011B RID: 283 RVA: 0x000099EC File Offset: 0x00007BEC
		public Texture Texture { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000099F5 File Offset: 0x00007BF5
		public bool IsRunningCustomAnimation
		{
			get
			{
				return this._customAnimation != null || this._customAnimationStartScheduled;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00009A0D File Offset: 0x00007C0D
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00009A15 File Offset: 0x00007C15
		public bool ShouldLoopCustomAnimation { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00009A1E File Offset: 0x00007C1E
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00009A26 File Offset: 0x00007C26
		public float CustomAnimationWaitDuration { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00009A2F File Offset: 0x00007C2F
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

		// Token: 0x06000122 RID: 290 RVA: 0x00009A4C File Offset: 0x00007C4C
		public CharacterTableau()
		{
			this._leftHandEquipmentIndex = -1;
			this._rightHandEquipmentIndex = -1;
			this._isVisualsDirty = false;
			this._equipment = new Equipment();
			this.SetEnabled(true);
			this.FirstTimeInit();
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00009B2C File Offset: 0x00007D2C
		public void OnTick(float dt)
		{
			if (this._customAnimationStartScheduled)
			{
				this.StartCustomAnimation();
			}
			if (this._customAnimation != null && this._characterActionSet.IsValid)
			{
				this._customAnimationTimer += dt;
				float actionAnimationDuration = MBActionSet.GetActionAnimationDuration(this._characterActionSet, this._customAnimation);
				if (this._customAnimationTimer > actionAnimationDuration)
				{
					if (this._customAnimationTimer > actionAnimationDuration + this.CustomAnimationWaitDuration)
					{
						if (this.ShouldLoopCustomAnimation)
						{
							this.StartCustomAnimation();
						}
						else
						{
							this.StopCustomAnimationIfCantContinue();
						}
					}
					else
					{
						AgentVisuals agentVisuals = this._agentVisuals;
						if (agentVisuals != null)
						{
							agentVisuals.SetAction(this.GetIdleAction(), 0f, true);
						}
					}
				}
			}
			if (this._isEnabled && this._isRotatingCharacter)
			{
				this.UpdateCharacterRotation((int)Input.MouseMoveX);
			}
			if (this._animationFrequencyThreshold > this._animationGap)
			{
				this._animationGap += dt;
			}
			if (this._isEnabled)
			{
				AgentVisuals agentVisuals2 = this._agentVisuals;
				if (agentVisuals2 != null)
				{
					agentVisuals2.TickVisuals();
				}
				AgentVisuals oldAgentVisuals = this._oldAgentVisuals;
				if (oldAgentVisuals != null)
				{
					oldAgentVisuals.TickVisuals();
				}
				AgentVisuals mountVisuals = this._mountVisuals;
				if (mountVisuals != null)
				{
					mountVisuals.TickVisuals();
				}
				AgentVisuals oldMountVisuals = this._oldMountVisuals;
				if (oldMountVisuals != null)
				{
					oldMountVisuals.TickVisuals();
				}
			}
			if (this.View != null)
			{
				if (this._continuousRenderCamera == null)
				{
					this._continuousRenderCamera = Camera.CreateCamera();
				}
				this.View.SetDoNotRenderThisFrame(false);
			}
			if (this._isVisualsDirty)
			{
				this.RefreshCharacterTableau(this._oldEquipment);
				this._oldEquipment = null;
				this._isVisualsDirty = false;
			}
			if (this._agentVisualLoadingCounter > 0 && this._agentVisuals.GetEntity().CheckResources(true, true))
			{
				this._agentVisualLoadingCounter--;
			}
			if (this._mountVisualLoadingCounter > 0 && this._mountVisuals.GetEntity().CheckResources(true, true))
			{
				this._mountVisualLoadingCounter--;
			}
			if (this._mountVisualLoadingCounter == 0 && this._agentVisualLoadingCounter == 0)
			{
				AgentVisuals oldMountVisuals2 = this._oldMountVisuals;
				if (oldMountVisuals2 != null)
				{
					oldMountVisuals2.SetVisible(false);
				}
				AgentVisuals mountVisuals2 = this._mountVisuals;
				if (mountVisuals2 != null)
				{
					mountVisuals2.SetVisible(this._bodyProperties != BodyProperties.Default);
				}
				AgentVisuals oldAgentVisuals2 = this._oldAgentVisuals;
				if (oldAgentVisuals2 != null)
				{
					oldAgentVisuals2.SetVisible(false);
				}
				AgentVisuals agentVisuals3 = this._agentVisuals;
				if (agentVisuals3 != null)
				{
					agentVisuals3.SetVisible(this._bodyProperties != BodyProperties.Default);
				}
			}
			if (this._isEquipmentIndicesDirty)
			{
				this._agentVisuals.GetVisuals().SetWieldedWeaponIndices(this._rightHandEquipmentIndex, this._leftHandEquipmentIndex);
				this._isEquipmentIndicesDirty = false;
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00009DA0 File Offset: 0x00007FA0
		public float GetCustomAnimationProgressRatio()
		{
			if (!(this._customAnimation != null) || !this._characterActionSet.IsValid)
			{
				return -1f;
			}
			float actionAnimationDuration = MBActionSet.GetActionAnimationDuration(this._characterActionSet, this._customAnimation);
			if (actionAnimationDuration == 0f)
			{
				return -1f;
			}
			return this._customAnimationTimer / actionAnimationDuration;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00009DF8 File Offset: 0x00007FF8
		private void StopCustomAnimationIfCantContinue()
		{
			bool flag = false;
			if (this._agentVisuals != null && this._customAnimation != null && this._customAnimation.Index >= 0)
			{
				ActionIndexValueCache actionAnimationContinueToAction = MBActionSet.GetActionAnimationContinueToAction(this._characterActionSet, ActionIndexValueCache.Create(this._customAnimation));
				if (actionAnimationContinueToAction.Index >= 0)
				{
					this._customAnimationName = actionAnimationContinueToAction.Name;
					this.StartCustomAnimation();
					flag = true;
				}
			}
			if (!flag)
			{
				this.StopCustomAnimation();
				this._customAnimationTimer = -1f;
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00009E75 File Offset: 0x00008075
		private void SetEnabled(bool enabled)
		{
			this._isEnabled = enabled;
			TableauView view = this.View;
			if (view == null)
			{
				return;
			}
			view.SetEnable(this._isEnabled);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00009E94 File Offset: 0x00008094
		public void SetLeftHandWieldedEquipmentIndex(int index)
		{
			this._leftHandEquipmentIndex = index;
			this._isEquipmentIndicesDirty = true;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00009EA4 File Offset: 0x000080A4
		public void SetRightHandWieldedEquipmentIndex(int index)
		{
			this._rightHandEquipmentIndex = index;
			this._isEquipmentIndicesDirty = true;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00009EB4 File Offset: 0x000080B4
		public void SetTargetSize(int width, int height)
		{
			this._isRotatingCharacter = false;
			this._latestWidth = width;
			this._latestHeight = height;
			if (width <= 0 || height <= 0)
			{
				this._tableauSizeX = 10;
				this._tableauSizeY = 10;
			}
			else
			{
				this.RenderScale = NativeOptions.GetConfig(NativeOptions.NativeOptionsType.ResolutionScale) / 100f;
				this._tableauSizeX = (int)((float)width * this._customRenderScale * this.RenderScale);
				this._tableauSizeY = (int)((float)height * this._customRenderScale * this.RenderScale);
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
			this.Texture = TableauView.AddTableau("CharacterTableau", new RenderTargetComponent.TextureUpdateEventHandler(this.CharacterTableauContinuousRenderFunction), this._tableauScene, this._tableauSizeX, this._tableauSizeY);
			this.Texture.TableauView.SetSceneUsesContour(false);
			this.Texture.TableauView.SetFocusedShadowmap(true, ref this._initialSpawnFrame.origin, 2.55f);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00009FDB File Offset: 0x000081DB
		public void SetCharStringID(string charStringId)
		{
			if (this._charStringId != charStringId)
			{
				this._charStringId = charStringId;
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00009FF4 File Offset: 0x000081F4
		public void OnFinalize()
		{
			Camera continuousRenderCamera = this._continuousRenderCamera;
			if (continuousRenderCamera != null)
			{
				continuousRenderCamera.ReleaseCameraEntity();
				this._continuousRenderCamera = null;
			}
			AgentVisuals agentVisuals = this._agentVisuals;
			if (agentVisuals != null)
			{
				agentVisuals.ResetNextFrame();
			}
			this._agentVisuals = null;
			AgentVisuals mountVisuals = this._mountVisuals;
			if (mountVisuals != null)
			{
				mountVisuals.ResetNextFrame();
			}
			this._mountVisuals = null;
			AgentVisuals oldAgentVisuals = this._oldAgentVisuals;
			if (oldAgentVisuals != null)
			{
				oldAgentVisuals.ResetNextFrame();
			}
			this._oldAgentVisuals = null;
			AgentVisuals oldMountVisuals = this._oldMountVisuals;
			if (oldMountVisuals != null)
			{
				oldMountVisuals.ResetNextFrame();
			}
			this._oldMountVisuals = null;
			TableauView view = this.View;
			if (view != null)
			{
				view.SetEnable(false);
			}
			if (this._tableauScene != null)
			{
				if (this._bannerEntity != null)
				{
					this._tableauScene.RemoveEntity(this._bannerEntity, 0);
					this._bannerEntity = null;
				}
				if (this._agentRendererSceneController != null)
				{
					if (view != null)
					{
						view.SetEnable(false);
					}
					if (view != null)
					{
						view.AddClearTask(false);
					}
					MBAgentRendererSceneController.DestructAgentRendererSceneController(this._tableauScene, this._agentRendererSceneController, false);
					this._agentRendererSceneController = null;
					this._tableauScene.ManualInvalidate();
					this._tableauScene = null;
				}
				else
				{
					TableauCacheManager.Current.ReturnCachedInventoryTableauScene();
					TableauCacheManager.Current.ReturnCachedInventoryTableauScene();
					if (view != null)
					{
						view.AddClearTask(true);
					}
					this._tableauScene = null;
				}
			}
			Texture texture = this.Texture;
			if (texture != null)
			{
				texture.ReleaseNextFrame();
			}
			this.Texture = null;
			this._isFinalized = true;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000A154 File Offset: 0x00008354
		public void SetBodyProperties(string bodyPropertiesCode)
		{
			if (this._bodyPropertiesCode != bodyPropertiesCode)
			{
				this._bodyPropertiesCode = bodyPropertiesCode;
				BodyProperties bodyProperties;
				if (!string.IsNullOrEmpty(bodyPropertiesCode) && BodyProperties.FromString(bodyPropertiesCode, out bodyProperties))
				{
					this._bodyProperties = bodyProperties;
				}
				else
				{
					this._bodyProperties = BodyProperties.Default;
				}
				this._isVisualsDirty = true;
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000A1A3 File Offset: 0x000083A3
		public void SetStanceIndex(int index)
		{
			this._stanceIndex = (CharacterViewModel.StanceTypes)index;
			this._isVisualsDirty = true;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000A1B3 File Offset: 0x000083B3
		public void SetCustomRenderScale(float value)
		{
			if (!this._customRenderScale.ApproximatelyEqualsTo(value, 1E-05f))
			{
				this._customRenderScale = value;
				if (this._latestWidth != -1 && this._latestHeight != -1)
				{
					this.SetTargetSize(this._latestWidth, this._latestHeight);
				}
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000A1F4 File Offset: 0x000083F4
		private void AdjustCharacterForStanceIndex()
		{
			switch (this._stanceIndex)
			{
			case CharacterViewModel.StanceTypes.None:
			{
				AgentVisuals agentVisuals = this._agentVisuals;
				if (agentVisuals != null)
				{
					agentVisuals.SetAction(this.GetIdleAction(), 0f, true);
				}
				AgentVisuals oldAgentVisuals = this._oldAgentVisuals;
				if (oldAgentVisuals != null)
				{
					oldAgentVisuals.SetAction(this.GetIdleAction(), 0f, true);
				}
				break;
			}
			case CharacterViewModel.StanceTypes.EmphasizeFace:
			{
				this._camPos = this._camPosGatheredFromScene;
				this._camPos.Elevate(-2f);
				this._camPos.Advance(0.5f);
				AgentVisuals agentVisuals2 = this._agentVisuals;
				if (agentVisuals2 != null)
				{
					agentVisuals2.SetAction(this.GetIdleAction(), 0f, true);
				}
				AgentVisuals oldAgentVisuals2 = this._oldAgentVisuals;
				if (oldAgentVisuals2 != null)
				{
					oldAgentVisuals2.SetAction(this.GetIdleAction(), 0f, true);
				}
				break;
			}
			case CharacterViewModel.StanceTypes.SideView:
			case CharacterViewModel.StanceTypes.OnMount:
				if (this._agentVisuals != null)
				{
					this._camPos = this._camPosGatheredFromScene;
					if (this._equipment[10].Item != null)
					{
						this._camPos.Advance(0.5f);
						this._agentVisuals.SetAction(this._mountVisuals.GetEntity().Skeleton.GetActionAtChannel(0), this._mountVisuals.GetEntity().Skeleton.GetAnimationParameterAtChannel(0), true);
						this._oldAgentVisuals.SetAction(this._mountVisuals.GetEntity().Skeleton.GetActionAtChannel(0), this._mountVisuals.GetEntity().Skeleton.GetAnimationParameterAtChannel(0), true);
					}
					else
					{
						this._camPos.Elevate(-2f);
						this._camPos.Advance(0.5f);
						this._agentVisuals.SetAction(this.GetIdleAction(), 0f, true);
						this._oldAgentVisuals.SetAction(this.GetIdleAction(), 0f, true);
					}
				}
				break;
			case CharacterViewModel.StanceTypes.CelebrateVictory:
			{
				AgentVisuals agentVisuals3 = this._agentVisuals;
				if (agentVisuals3 != null)
				{
					agentVisuals3.SetAction(CharacterTableau.act_cheer_1, 0f, true);
				}
				AgentVisuals oldAgentVisuals3 = this._oldAgentVisuals;
				if (oldAgentVisuals3 != null)
				{
					oldAgentVisuals3.SetAction(CharacterTableau.act_cheer_1, 0f, true);
				}
				break;
			}
			}
			if (this._agentVisuals != null)
			{
				GameEntity entity = this._agentVisuals.GetEntity();
				Skeleton skeleton = entity.Skeleton;
				skeleton.TickAnimations(0.01f, this._agentVisuals.GetVisuals().GetGlobalFrame(), true);
				if (!string.IsNullOrEmpty(this._idleFaceAnim))
				{
					skeleton.SetFacialAnimation(Agent.FacialAnimChannel.Mid, this._idleFaceAnim, false, true);
				}
				entity.ManualInvalidate();
				skeleton.ManualInvalidate();
			}
			if (this._oldAgentVisuals != null)
			{
				GameEntity entity2 = this._oldAgentVisuals.GetEntity();
				Skeleton skeleton2 = entity2.Skeleton;
				skeleton2.TickAnimations(0.01f, this._oldAgentVisuals.GetVisuals().GetGlobalFrame(), true);
				if (!string.IsNullOrEmpty(this._idleFaceAnim))
				{
					skeleton2.SetFacialAnimation(Agent.FacialAnimChannel.Mid, this._idleFaceAnim, false, true);
				}
				entity2.ManualInvalidate();
				skeleton2.ManualInvalidate();
			}
			if (this._mountVisuals != null)
			{
				GameEntity entity3 = this._mountVisuals.GetEntity();
				Skeleton skeleton3 = entity3.Skeleton;
				skeleton3.TickAnimations(0.01f, this._mountVisuals.GetVisuals().GetGlobalFrame(), true);
				if (!string.IsNullOrEmpty(this._idleFaceAnim))
				{
					skeleton3.SetFacialAnimation(Agent.FacialAnimChannel.Mid, this._idleFaceAnim, false, true);
				}
				entity3.ManualInvalidate();
				skeleton3.ManualInvalidate();
			}
			if (this._oldMountVisuals != null)
			{
				GameEntity entity4 = this._oldMountVisuals.GetEntity();
				Skeleton skeleton4 = entity4.Skeleton;
				skeleton4.TickAnimations(0.01f, this._oldMountVisuals.GetVisuals().GetGlobalFrame(), true);
				entity4.ManualInvalidate();
				skeleton4.ManualInvalidate();
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000A574 File Offset: 0x00008774
		private void ForceRefresh()
		{
			int stanceIndex = (int)this._stanceIndex;
			this._stanceIndex = CharacterViewModel.StanceTypes.None;
			this.SetStanceIndex(stanceIndex);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000A596 File Offset: 0x00008796
		public void SetIsFemale(bool isFemale)
		{
			this._isFemale = isFemale;
			this._isVisualsDirty = true;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000A5A6 File Offset: 0x000087A6
		public void SetIsBannerShownInBackground(bool isBannerShownInBackground)
		{
			this._isBannerShownInBackground = isBannerShownInBackground;
			this._isVisualsDirty = true;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000A5B6 File Offset: 0x000087B6
		public void SetRace(int race)
		{
			this._race = race;
			this._isVisualsDirty = true;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000A5C6 File Offset: 0x000087C6
		public void SetIdleAction(string idleAction)
		{
			this._idleAction = ActionIndexCache.Create(idleAction);
			this._isVisualsDirty = true;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000A5DB File Offset: 0x000087DB
		public void SetCustomAnimation(string animation)
		{
			this._customAnimationName = animation;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000A5E4 File Offset: 0x000087E4
		public void StartCustomAnimation()
		{
			if (this._isVisualsDirty || this._agentVisuals == null || string.IsNullOrEmpty(this._customAnimationName))
			{
				this._customAnimationStartScheduled = true;
				return;
			}
			this.StopCustomAnimation();
			this._customAnimation = ActionIndexCache.Create(this._customAnimationName);
			if (this._customAnimation.Index >= 0)
			{
				this._agentVisuals.SetAction(this._customAnimation, 0f, true);
				this._customAnimationStartScheduled = false;
				this._customAnimationTimer = 0f;
				return;
			}
			Debug.FailedAssert("Invalid custom animation in character tableau: " + this._customAnimationName, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.View\\Tableaus\\CharacterTableau.cs", "StartCustomAnimation", 599);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000A68C File Offset: 0x0000888C
		public void StopCustomAnimation()
		{
			if (this._agentVisuals != null && this._customAnimation != null)
			{
				if (MBActionSet.GetActionAnimationContinueToAction(this._characterActionSet, ActionIndexValueCache.Create(this._customAnimation)).Index < 0)
				{
					this._agentVisuals.SetAction(this.GetIdleAction(), 0f, true);
				}
				this._customAnimation = null;
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000A6EE File Offset: 0x000088EE
		public void SetIdleFaceAnim(string idleFaceAnim)
		{
			if (!string.IsNullOrEmpty(idleFaceAnim))
			{
				this._idleFaceAnim = idleFaceAnim;
				this._isVisualsDirty = true;
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000A708 File Offset: 0x00008908
		public void SetEquipmentCode(string equipmentCode)
		{
			if (this._equipmentCode != equipmentCode && !string.IsNullOrEmpty(equipmentCode))
			{
				this._oldEquipment = Equipment.CreateFromEquipmentCode(this._equipmentCode);
				this._equipmentCode = equipmentCode;
				this._equipment = Equipment.CreateFromEquipmentCode(equipmentCode);
				this._bannerItem = this.GetAndRemoveBannerFromEquipment(ref this._equipment);
				this._isVisualsDirty = true;
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000A768 File Offset: 0x00008968
		public void SetIsEquipmentAnimActive(bool value)
		{
			this._isEquipmentAnimActive = value;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000A771 File Offset: 0x00008971
		public void SetMountCreationKey(string value)
		{
			if (this._mountCreationKey != value)
			{
				this._mountCreationKey = value;
				this._isVisualsDirty = true;
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000A78F File Offset: 0x0000898F
		public void SetBannerCode(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				this._banner = null;
			}
			else
			{
				this._banner = BannerCode.CreateFrom(value).CalculateBanner();
			}
			this._isVisualsDirty = true;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000A7BA File Offset: 0x000089BA
		public void SetArmorColor1(uint clothColor1)
		{
			if (this._clothColor1 != clothColor1)
			{
				this._clothColor1 = clothColor1;
				this._isVisualsDirty = true;
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000A7D3 File Offset: 0x000089D3
		public void SetArmorColor2(uint clothColor2)
		{
			if (this._clothColor2 != clothColor2)
			{
				this._clothColor2 = clothColor2;
				this._isVisualsDirty = true;
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000A7EC File Offset: 0x000089EC
		private ActionIndexCache GetIdleAction()
		{
			return this._idleAction ?? CharacterTableau.act_inventory_idle_start;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000A800 File Offset: 0x00008A00
		private void RefreshCharacterTableau(Equipment oldEquipment = null)
		{
			this.UpdateMount(this._stanceIndex == CharacterViewModel.StanceTypes.OnMount);
			this.UpdateBannerItem();
			if (this._mountVisuals == null && this._isCharacterMountPlacesSwapped)
			{
				this._isCharacterMountPlacesSwapped = false;
				this._mainCharacterRotation = 0f;
			}
			if (this._agentVisuals != null)
			{
				bool visibilityExcludeParents = this._oldAgentVisuals.GetEntity().GetVisibilityExcludeParents();
				AgentVisuals agentVisuals = this._agentVisuals;
				this._agentVisuals = this._oldAgentVisuals;
				this._oldAgentVisuals = agentVisuals;
				this._agentVisualLoadingCounter = 1;
				AgentVisualsData copyAgentVisualsData = this._agentVisuals.GetCopyAgentVisualsData();
				MatrixFrame frame = this._isCharacterMountPlacesSwapped ? this._characterMountPositionFrame : this._initialSpawnFrame;
				if (!this._isCharacterMountPlacesSwapped)
				{
					frame.rotation.RotateAboutUp(this._mainCharacterRotation);
				}
				this._characterActionSet = MBGlobals.GetActionSetWithSuffix(copyAgentVisualsData.MonsterData, this._isFemale, "_warrior");
				copyAgentVisualsData.BodyProperties(this._bodyProperties).SkeletonType(this._isFemale ? SkeletonType.Female : SkeletonType.Male).Frame(frame).ActionSet(this._characterActionSet).Equipment(this._equipment).Banner(this._banner).UseMorphAnims(true).ClothColor1(this._clothColor1).ClothColor2(this._clothColor2).Race(this._race);
				if (this._initialLoadingCounter > 0)
				{
					this._initialLoadingCounter--;
				}
				this._agentVisuals.Refresh(false, copyAgentVisualsData, false);
				this._agentVisuals.SetVisible(false);
				if (this._initialLoadingCounter == 0)
				{
					this._oldAgentVisuals.SetVisible(visibilityExcludeParents);
				}
				if (oldEquipment != null && this._animationFrequencyThreshold <= this._animationGap && this._isEquipmentAnimActive)
				{
					if (this._equipment[EquipmentIndex.Gloves].Item != null && oldEquipment[EquipmentIndex.Gloves].Item != this._equipment[EquipmentIndex.Gloves].Item)
					{
						this._agentVisuals.GetVisuals().GetSkeleton().SetAgentActionChannel(0, CharacterTableau.act_inventory_glove_equip, 0f, -0.2f, true);
						this._animationGap = 0f;
					}
					else if (this._equipment[EquipmentIndex.Body].Item != null && oldEquipment[EquipmentIndex.Body].Item != this._equipment[EquipmentIndex.Body].Item)
					{
						this._agentVisuals.GetVisuals().GetSkeleton().SetAgentActionChannel(0, CharacterTableau.act_inventory_cloth_equip, 0f, -0.2f, true);
						this._animationGap = 0f;
					}
				}
				this._agentVisuals.GetEntity().CheckResources(true, true);
			}
			this.AdjustCharacterForStanceIndex();
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000AAA6 File Offset: 0x00008CA6
		public void RotateCharacter(bool value)
		{
			this._isRotatingCharacter = value;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000AAAF File Offset: 0x00008CAF
		public void TriggerCharacterMountPlacesSwap()
		{
			this._mainCharacterRotation = 0f;
			this._isCharacterMountPlacesSwapped = !this._isCharacterMountPlacesSwapped;
			this._isVisualsDirty = true;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000AAD2 File Offset: 0x00008CD2
		public void OnCharacterTableauMouseMove(int mouseMoveX)
		{
			this.UpdateCharacterRotation(mouseMoveX);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000AADC File Offset: 0x00008CDC
		private void UpdateCharacterRotation(int mouseMoveX)
		{
			if (this._agentVisuals != null)
			{
				float num = (float)mouseMoveX * 0.005f;
				this._mainCharacterRotation += num;
				if (this._isCharacterMountPlacesSwapped)
				{
					MatrixFrame frame = this._mountVisuals.GetEntity().GetFrame();
					frame.rotation.RotateAboutUp(num);
					this._mountVisuals.GetEntity().SetFrame(ref frame);
					return;
				}
				MatrixFrame frame2 = this._agentVisuals.GetEntity().GetFrame();
				frame2.rotation.RotateAboutUp(num);
				this._agentVisuals.GetEntity().SetFrame(ref frame2);
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000AB74 File Offset: 0x00008D74
		private void FirstTimeInit()
		{
			if (this._continuousRenderCamera == null)
			{
				this._continuousRenderCamera = Camera.CreateCamera();
			}
			if (this._equipment != null)
			{
				if (this._tableauScene == null)
				{
					if (TableauCacheManager.Current.IsCachedInventoryTableauSceneUsed())
					{
						this._tableauScene = Scene.CreateNewScene(true, false, DecalAtlasGroup.All, "mono_renderscene");
						this._tableauScene.SetName("CharacterTableau");
						this._tableauScene.DisableStaticShadows(true);
						this._tableauScene.SetClothSimulationState(true);
						this._agentRendererSceneController = MBAgentRendererSceneController.CreateNewAgentRendererSceneController(this._tableauScene, 32);
						SceneInitializationData sceneInitializationData = new SceneInitializationData(true);
						sceneInitializationData.InitPhysicsWorld = false;
						sceneInitializationData.DoNotUseLoadingScreen = true;
						this._tableauScene.Read("inventory_character_scene", ref sceneInitializationData, "");
					}
					else
					{
						this._tableauScene = TableauCacheManager.Current.GetCachedInventoryTableauScene();
					}
					this._tableauScene.SetShadow(true);
					this._tableauScene.SetClothSimulationState(true);
					this._camPos = (this._camPosGatheredFromScene = TableauCacheManager.Current.InventorySceneCameraFrame);
					this._mountSpawnPoint = this._tableauScene.FindEntityWithTag("horse_inv").GetGlobalFrame();
					this._bannerSpawnPoint = this._tableauScene.FindEntityWithTag("banner_inv").GetGlobalFrame();
					this._initialSpawnFrame = this._tableauScene.FindEntityWithTag("agent_inv").GetGlobalFrame();
					this._characterMountPositionFrame = new MatrixFrame(this._initialSpawnFrame.rotation, this._mountSpawnPoint.origin);
					this._characterMountPositionFrame.Strafe(-0.25f);
					this._mountCharacterPositionFrame = new MatrixFrame(this._mountSpawnPoint.rotation, this._initialSpawnFrame.origin);
					this._mountCharacterPositionFrame.Strafe(0.25f);
					if (this._agentRendererSceneController != null)
					{
						this._tableauScene.RemoveEntity(this._tableauScene.FindEntityWithTag("agent_inv"), 99);
						this._tableauScene.RemoveEntity(this._tableauScene.FindEntityWithTag("horse_inv"), 100);
						this._tableauScene.RemoveEntity(this._tableauScene.FindEntityWithTag("banner_inv"), 101);
					}
				}
				this.InitializeAgentVisuals();
				this._isVisualsDirty = true;
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000ADA4 File Offset: 0x00008FA4
		private void InitializeAgentVisuals()
		{
			Monster baseMonsterFromRace = FaceGen.GetBaseMonsterFromRace(this._race);
			this._characterActionSet = MBGlobals.GetActionSetWithSuffix(baseMonsterFromRace, this._isFemale, "_warrior");
			this._oldAgentVisuals = AgentVisuals.Create(new AgentVisualsData().Banner(this._banner).Equipment(this._equipment).BodyProperties(this._bodyProperties).Race(this._race).Frame(this._initialSpawnFrame).UseMorphAnims(true).ActionSet(this._characterActionSet).ActionCode(this.GetIdleAction()).Scene(this._tableauScene).Monster(baseMonsterFromRace).PrepareImmediately(false).SkeletonType(this._isFemale ? SkeletonType.Female : SkeletonType.Male).ClothColor1(this._clothColor1).ClothColor2(this._clothColor2).CharacterObjectStringId(this._charStringId), "CharacterTableau", false, false, false);
			this._oldAgentVisuals.SetAgentLodZeroOrMaxExternal(true);
			this._oldAgentVisuals.SetVisible(false);
			this._agentVisuals = AgentVisuals.Create(new AgentVisualsData().Banner(this._banner).Equipment(this._equipment).BodyProperties(this._bodyProperties).Race(this._race).Frame(this._initialSpawnFrame).UseMorphAnims(true).ActionSet(this._characterActionSet).ActionCode(this.GetIdleAction()).Scene(this._tableauScene).Monster(baseMonsterFromRace).PrepareImmediately(false).SkeletonType(this._isFemale ? SkeletonType.Female : SkeletonType.Male).ClothColor1(this._clothColor1).ClothColor2(this._clothColor2).CharacterObjectStringId(this._charStringId), "CharacterTableau", false, false, false);
			this._agentVisuals.SetAgentLodZeroOrMaxExternal(true);
			this._agentVisuals.SetVisible(false);
			this._initialLoadingCounter = 2;
			if (!string.IsNullOrEmpty(this._idleFaceAnim))
			{
				this._agentVisuals.GetVisuals().GetSkeleton().SetFacialAnimation(Agent.FacialAnimChannel.Mid, this._idleFaceAnim, false, true);
				this._oldAgentVisuals.GetVisuals().GetSkeleton().SetFacialAnimation(Agent.FacialAnimChannel.Mid, this._idleFaceAnim, false, true);
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000AFBC File Offset: 0x000091BC
		private void UpdateMount(bool isRiderAgentMounted = false)
		{
			ItemObject item = this._equipment[EquipmentIndex.ArmorItemEndSlot].Item;
			if (((item != null) ? item.HorseComponent : null) != null)
			{
				ItemObject item2 = this._equipment[EquipmentIndex.ArmorItemEndSlot].Item;
				Monster monster = item2.HorseComponent.Monster;
				Equipment equipment = new Equipment();
				equipment[EquipmentIndex.ArmorItemEndSlot] = this._equipment[EquipmentIndex.ArmorItemEndSlot];
				equipment[EquipmentIndex.HorseHarness] = this._equipment[EquipmentIndex.HorseHarness];
				Equipment equipment2 = equipment;
				MatrixFrame frame = this._isCharacterMountPlacesSwapped ? this._mountCharacterPositionFrame : this._mountSpawnPoint;
				if (this._isCharacterMountPlacesSwapped)
				{
					frame.rotation.RotateAboutUp(this._mainCharacterRotation);
				}
				if (this._oldMountVisuals != null)
				{
					this._oldMountVisuals.ResetNextFrame();
				}
				this._oldMountVisuals = this._mountVisuals;
				this._mountVisualLoadingCounter = 3;
				AgentVisualsData agentVisualsData = new AgentVisualsData();
				agentVisualsData.Banner(this._banner).Equipment(equipment2).Frame(frame).Scale(item2.ScaleFactor).ActionSet(MBGlobals.GetActionSet(monster.ActionSetCode)).ActionCode(isRiderAgentMounted ? ((monster.MonsterUsage == "camel") ? CharacterTableau.act_camel_stand : CharacterTableau.act_horse_stand) : this.GetIdleAction()).Scene(this._tableauScene).Monster(monster).PrepareImmediately(false).ClothColor1(this._clothColor1).ClothColor2(this._clothColor2).MountCreationKey(this._mountCreationKey);
				this._mountVisuals = AgentVisuals.Create(agentVisualsData, "MountTableau", false, false, false);
				this._mountVisuals.SetAgentLodZeroOrMaxExternal(true);
				this._mountVisuals.SetVisible(false);
				this._mountVisuals.GetEntity().CheckResources(true, true);
				return;
			}
			if (this._mountVisuals != null)
			{
				this._mountVisuals.Reset();
				this._mountVisuals = null;
				this._mountVisualLoadingCounter = 0;
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000B1A0 File Offset: 0x000093A0
		private void UpdateBannerItem()
		{
			if (this._bannerEntity != null)
			{
				this._tableauScene.RemoveEntity(this._bannerEntity, 0);
				this._bannerEntity = null;
			}
			if (this._isBannerShownInBackground && this._bannerItem != null)
			{
				this._bannerEntity = GameEntity.CreateEmpty(this._tableauScene, true);
				this._bannerEntity.SetFrame(ref this._bannerSpawnPoint);
				this._bannerEntity.AddMultiMesh(this._bannerItem.GetMultiMeshCopy(), true);
				if (this._banner != null)
				{
					this._banner.GetTableauTextureLarge(delegate(Texture t)
					{
						this.OnBannerTableauRenderDone(t);
					});
				}
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000B240 File Offset: 0x00009440
		private void OnBannerTableauRenderDone(Texture newTexture)
		{
			if (this._isFinalized)
			{
				return;
			}
			if (this._bannerEntity == null)
			{
				return;
			}
			foreach (Mesh bannerMesh in this._bannerEntity.GetAllMeshesWithTag("banner_replacement_mesh"))
			{
				this.ApplyBannerTextureToMesh(bannerMesh, newTexture);
			}
			Skeleton skeleton = this._bannerEntity.Skeleton;
			if (((skeleton != null) ? skeleton.GetAllMeshes() : null) != null)
			{
				Skeleton skeleton2 = this._bannerEntity.Skeleton;
				foreach (Mesh mesh in ((skeleton2 != null) ? skeleton2.GetAllMeshes() : null))
				{
					if (mesh.HasTag("banner_replacement_mesh"))
					{
						this.ApplyBannerTextureToMesh(mesh, newTexture);
					}
				}
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000B324 File Offset: 0x00009524
		private void ApplyBannerTextureToMesh(Mesh bannerMesh, Texture bannerTexture)
		{
			if (bannerMesh != null)
			{
				Material material = bannerMesh.GetMaterial().CreateCopy();
				material.SetTexture(Material.MBTextureType.DiffuseMap2, bannerTexture);
				uint num = (uint)material.GetShader().GetMaterialShaderFlagMask("use_tableau_blending", true);
				ulong shaderFlags = material.GetShaderFlags();
				material.SetShaderFlags(shaderFlags | (ulong)num);
				bannerMesh.SetMaterial(material);
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000B37C File Offset: 0x0000957C
		private ItemObject GetAndRemoveBannerFromEquipment(ref Equipment equipment)
		{
			ItemObject result = null;
			ItemObject item = equipment[EquipmentIndex.ExtraWeaponSlot].Item;
			if (item != null && item.IsBannerItem)
			{
				result = equipment[EquipmentIndex.ExtraWeaponSlot].Item;
				equipment[EquipmentIndex.ExtraWeaponSlot] = EquipmentElement.Invalid;
			}
			return result;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000B3C8 File Offset: 0x000095C8
		internal void CharacterTableauContinuousRenderFunction(Texture sender, EventArgs e)
		{
			Scene scene = (Scene)sender.UserData;
			TableauView tableauView = sender.TableauView;
			if (scene == null)
			{
				tableauView.SetContinuousRendering(false);
				tableauView.SetDeleteAfterRendering(true);
				return;
			}
			scene.EnsurePostfxSystem();
			scene.SetDofMode(false);
			scene.SetMotionBlurMode(false);
			scene.SetBloom(true);
			scene.SetDynamicShadowmapCascadesRadiusMultiplier(0.31f);
			tableauView.SetRenderWithPostfx(true);
			float cameraRatio = this._cameraRatio;
			MatrixFrame camPos = this._camPos;
			Camera continuousRenderCamera = this._continuousRenderCamera;
			if (continuousRenderCamera != null)
			{
				continuousRenderCamera.SetFovVertical(0.7853982f, cameraRatio, 0.2f, 200f);
				continuousRenderCamera.Frame = camPos;
				tableauView.SetCamera(continuousRenderCamera);
				tableauView.SetScene(scene);
				tableauView.SetSceneUsesSkybox(false);
				tableauView.SetDeleteAfterRendering(false);
				tableauView.SetContinuousRendering(true);
				tableauView.SetDoNotRenderThisFrame(true);
				tableauView.SetClearColor(0U);
				tableauView.SetFocusedShadowmap(true, ref this._initialSpawnFrame.origin, 1.55f);
			}
		}

		// Token: 0x0400009C RID: 156
		private bool _isFinalized;

		// Token: 0x0400009D RID: 157
		private MatrixFrame _mountSpawnPoint;

		// Token: 0x0400009E RID: 158
		private MatrixFrame _bannerSpawnPoint;

		// Token: 0x0400009F RID: 159
		private float _animationFrequencyThreshold = 2.5f;

		// Token: 0x040000A0 RID: 160
		private MatrixFrame _initialSpawnFrame;

		// Token: 0x040000A1 RID: 161
		private MatrixFrame _characterMountPositionFrame;

		// Token: 0x040000A2 RID: 162
		private MatrixFrame _mountCharacterPositionFrame;

		// Token: 0x040000A3 RID: 163
		private AgentVisuals _agentVisuals;

		// Token: 0x040000A4 RID: 164
		private AgentVisuals _mountVisuals;

		// Token: 0x040000A5 RID: 165
		private int _agentVisualLoadingCounter;

		// Token: 0x040000A6 RID: 166
		private int _mountVisualLoadingCounter;

		// Token: 0x040000A7 RID: 167
		private AgentVisuals _oldAgentVisuals;

		// Token: 0x040000A8 RID: 168
		private AgentVisuals _oldMountVisuals;

		// Token: 0x040000A9 RID: 169
		private int _initialLoadingCounter;

		// Token: 0x040000AA RID: 170
		private ActionIndexCache _idleAction;

		// Token: 0x040000AB RID: 171
		private string _idleFaceAnim;

		// Token: 0x040000AC RID: 172
		private Scene _tableauScene;

		// Token: 0x040000AD RID: 173
		private MBAgentRendererSceneController _agentRendererSceneController;

		// Token: 0x040000AE RID: 174
		private Camera _continuousRenderCamera;

		// Token: 0x040000AF RID: 175
		private float _cameraRatio;

		// Token: 0x040000B0 RID: 176
		private MatrixFrame _camPos;

		// Token: 0x040000B1 RID: 177
		private MatrixFrame _camPosGatheredFromScene;

		// Token: 0x040000B2 RID: 178
		private string _charStringId;

		// Token: 0x040000B3 RID: 179
		private int _tableauSizeX;

		// Token: 0x040000B4 RID: 180
		private int _tableauSizeY;

		// Token: 0x040000B5 RID: 181
		private uint _clothColor1 = new Color(1f, 1f, 1f, 1f).ToUnsignedInteger();

		// Token: 0x040000B6 RID: 182
		private uint _clothColor2 = new Color(1f, 1f, 1f, 1f).ToUnsignedInteger();

		// Token: 0x040000B7 RID: 183
		private bool _isRotatingCharacter;

		// Token: 0x040000B8 RID: 184
		private bool _isCharacterMountPlacesSwapped;

		// Token: 0x040000B9 RID: 185
		private string _mountCreationKey = "";

		// Token: 0x040000BA RID: 186
		private string _equipmentCode = "";

		// Token: 0x040000BB RID: 187
		private bool _isEquipmentAnimActive;

		// Token: 0x040000BC RID: 188
		private float _animationGap;

		// Token: 0x040000BD RID: 189
		private float _mainCharacterRotation;

		// Token: 0x040000BE RID: 190
		private bool _isEnabled;

		// Token: 0x040000BF RID: 191
		private float RenderScale = 1f;

		// Token: 0x040000C0 RID: 192
		private float _customRenderScale = 1f;

		// Token: 0x040000C1 RID: 193
		private int _latestWidth = -1;

		// Token: 0x040000C2 RID: 194
		private int _latestHeight = -1;

		// Token: 0x040000C3 RID: 195
		private string _bodyPropertiesCode;

		// Token: 0x040000C4 RID: 196
		private BodyProperties _bodyProperties = BodyProperties.Default;

		// Token: 0x040000C5 RID: 197
		private bool _isFemale;

		// Token: 0x040000C6 RID: 198
		private CharacterViewModel.StanceTypes _stanceIndex;

		// Token: 0x040000C7 RID: 199
		private Equipment _equipment;

		// Token: 0x040000C8 RID: 200
		private Banner _banner;

		// Token: 0x040000C9 RID: 201
		private int _race;

		// Token: 0x040000CA RID: 202
		private bool _isBannerShownInBackground;

		// Token: 0x040000CB RID: 203
		private ItemObject _bannerItem;

		// Token: 0x040000CC RID: 204
		private GameEntity _bannerEntity;

		// Token: 0x040000CD RID: 205
		private static readonly ActionIndexCache act_cheer_1 = ActionIndexCache.Create("act_arena_winner_1");

		// Token: 0x040000CE RID: 206
		private static readonly ActionIndexCache act_inventory_idle_start = ActionIndexCache.Create("act_inventory_idle_start");

		// Token: 0x040000CF RID: 207
		private static readonly ActionIndexCache act_inventory_glove_equip = ActionIndexCache.Create("act_inventory_glove_equip");

		// Token: 0x040000D0 RID: 208
		private static readonly ActionIndexCache act_inventory_cloth_equip = ActionIndexCache.Create("act_inventory_cloth_equip");

		// Token: 0x040000D1 RID: 209
		private static readonly ActionIndexCache act_horse_stand = ActionIndexCache.Create("act_inventory_idle_start");

		// Token: 0x040000D2 RID: 210
		private static readonly ActionIndexCache act_camel_stand = ActionIndexCache.Create("act_inventory_idle_start");

		// Token: 0x040000D3 RID: 211
		private int _leftHandEquipmentIndex;

		// Token: 0x040000D4 RID: 212
		private int _rightHandEquipmentIndex;

		// Token: 0x040000D5 RID: 213
		private bool _isEquipmentIndicesDirty;

		// Token: 0x040000D6 RID: 214
		private bool _customAnimationStartScheduled;

		// Token: 0x040000D7 RID: 215
		private float _customAnimationTimer;

		// Token: 0x040000D8 RID: 216
		private string _customAnimationName;

		// Token: 0x040000D9 RID: 217
		private ActionIndexCache _customAnimation;

		// Token: 0x040000DA RID: 218
		private MBActionSet _characterActionSet;

		// Token: 0x040000DB RID: 219
		private bool _isVisualsDirty;

		// Token: 0x040000DC RID: 220
		private Equipment _oldEquipment;
	}
}
